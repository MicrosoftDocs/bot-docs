---
title: How to use proactive messaging | Microsoft Docs
description: Understand how to send proactive messages with your bot.
keywords: proactive message
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 09/23/2018
monikerRange: 'azure-bot-service-4.0'
---

# How to use proactive messaging

[!INCLUDE [pre-release-label](~/includes/pre-release-label.md)]

Typically, each message that a bot sends to the user directly relates to the user's prior input.
In some cases, a bot may need to send the user a message that is not directly related to the current topic of conversation or to the last message the user sent. These types of messages are called _proactive messages_.

## Uses

Proactive messages can be useful in a variety of scenarios. If a bot sets a timer or reminder, it will need to notify the user when the time arrives. Or, if a bot receives a notification from an external system, it may need to communicate that information to the user immediately. For example, if the user has previously asked the bot to monitor the price of a product, the bot can alert the user if the price of the product has dropped by 20%. Or, if a bot requires some time to compile a response to the user's question, it may inform the user of the delay and allow the conversation to continue in the meantime. When the bot finishes compiling the response to the question, it will share that information with the user.

When implementing proactive messages in your bot:

- Don't send several proactive messages within a short amount of time. Some channels enforce restrictions on how frequently a bot can send messages to the user, and will disable the bot if it violates those restrictions.
- Don't send proactive messages to users who have not previously interacted with the bot or solicited contact with the bot through another means such as e-mail or SMS.

An **ad hoc proactive message** is the simplest type of proactive message.
The bot simply interjects the message into the conversation whenever it is triggered, without any regard for whether the user is currently engaged in a separate topic of conversation with the bot and will not attempt to change the conversation in any way.

To handle notifications more smoothly, consider other ways to integrate the notification into the conversation flow, such as setting a flag in the conversation state or adding the notification to a queue.

## Prerequisites

To send a proactive message, your bot needs to have a valid app ID and password. However, for local testing in the Emulator, you can use a placeholder app ID.

To get an app ID and password to use for your bot, you can log in to the [Azure portal](https://portal.azure.com) and create a **Bot Channels Registration** resource. For testing purposes, you can then use this app ID and password for your bot locally, without having to deploy to Azure.

> [!TIP]
> If you do not already have a subscription, you can register for a <a href="https://azure.microsoft.com/en-us/free/" target="_blank">free account</a>.

### Required libraries

If you start from one of the BotBuilder templates, the required libraries are installed for you. These are the specific BotBuilder libraries required for proactive messaging:

# [C#](#tab/csharp)

The **Microsoft.Bot.Builder.Integration.AspNet.Core** NuGet package. (Installing this will also install the **Microsoft.Bot.Builder** package.)

# [JavaScript](#tab/javascript)

The **Microsoft.Bot.Builder** npm package.

---

## Notes on the sample code

Code for this article is taken from the proactive messages sample [[C#](https://aka.ms/proactive-sample-cs)|[JS](https://aka.ms/proactive-sample-js)].

This sample models user tasks that can take an indeterminate amount of time. The bot stores information about the task, tells the user that it will get back to them when the task finishes, and lets the conversation proceed. When the task completes, the bot sends the confirmation message proactively on the original conversation.

## Define job data and state

In this scenario, we're tracking arbitrary jobs that can be created by various users in different conversations. We'll need to store information about each job, including the conversation reference and a job identifier.

- We'll need the conversation reference so we can send the proactive message to the right conversation.
- We'll need a way to identify jobs. For this example, we use a simple timestamp.
- We'll need to store job state independent of conversation or user state.

# [C#](#tab/csharp)

We need to define classes for job data and state middleware. We also need to register our bot and setup a state property accessor for the job log.

### Define a class for job data

The **JobLog** class tracks job data, indexed by job number (the time-stamp). Job data is defined as an inner class of a dictionary.

```csharp
namespace Microsoft.BotBuilderSamples
{
    /// <summary>Contains a dictionary of job data, indexed by job number.</summary>
    /// <remarks>The JobLog class tracks all the outstanding jobs.  Each job is
    /// identified by a unique key.</remarks>
    public class JobLog : Dictionary<long, JobLog.JobData>
    {
        /// <summary>Describes the state of a job.</summary>
        public class JobData
        {
            /// <summary>Gets or sets the time-stamp for the job.</summary>
            /// <value>
            /// The time-stamp for the job when the job needs to fire.
            /// </value>
            public long TimeStamp { get; set; } = 0;

            /// <summary>Gets or sets a value indicating whether indicates whether the job has completed.</summary>
            /// <value>
            /// A value indicating whether indicates whether the job has completed.
            /// </value>
            public bool Completed { get; set; } = false;

            /// <summary>
            /// Gets or sets the conversation reference to which to send status updates.
            /// </summary>
            /// <value>
            /// The conversation reference to which to send status updates.
            /// </value>
            public ConversationReference Conversation { get; set; }
        }
    }
}
```

### Define a state middleware class

The **JobState** class manages the job state, independent of conversation or user state.

```csharp
using Microsoft.Bot.Builder;

/// <summary>A <see cref="BotState"/> for managing bot state for "bot jobs".</summary>
/// <remarks>Independent from both <see cref="UserState"/> and <see cref="ConversationState"/> because
/// the process of running the jobs and notifying the user interacts with the
/// bot as a distinct user on a separate conversation.</remarks>
public class JobState : BotState
{
    /// <summary>The key used to cache the state information in the turn context.</summary>
    private const string StorageKey = "ProactiveBot.JobState";

    /// <summary>
    /// Initializes a new instance of the <see cref="JobState"/> class.</summary>
    /// <param name="storage">The storage provider to use.</param>
    public JobState(IStorage storage)
        : base(storage, StorageKey)
    {
    }

    /// <summary>Gets the storage key for caching state information.</summary>
    /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
    /// for processing this conversation turn.</param>
    /// <returns>The storage key.</returns>
    protected override string GetStorageKey(ITurnContext turnContext) => StorageKey;
}
```

### Define the state property accessors for the app

The **ProactiveAccessors** class defines an accessor that the bot can use to get and set the job state.

```csharp
using Microsoft.Bot.Builder;

namespace Microsoft.BotBuilderSamples
{
    /// <summary>
    /// Defines <see cref="IStatePropertyAccessor{T}"/> for use with this bot.
    /// </summary>
    public class ProactiveAccessors
    {
        /// <summary>A unique ID to use for this property accessor.</summary>
        public const string JobLogDataName = "ProactiveBot.JobLogAccessor";

        public ProactiveAccessors(JobState jobState)
        {
            JobState = jobState;
        }

        /// <summary>Gets or sets the state property accessor for the job log.</summary>
        /// <value>"Running" jobs (represented by <see cref="JobLog.JobData"/>).</value>
        public IStatePropertyAccessor<JobLog> JobLogData { get; set; }

        /// <summary>
        /// Gets the JobState object.
        /// </summary>
        /// <value>
        /// User and Conversation independent state object.
        /// </value>
        public JobState JobState { get; }
    }
}
```

### Register the bot and required services

1. The set of using statements is expanded to reference these namespaces:

    ```csharp
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.BotFramework;
    using Microsoft.Bot.Builder.Integration;
    using Microsoft.Bot.Builder.Integration.AspNet.Core;
    using Microsoft.Bot.Builder.TraceExtensions;
    using Microsoft.Bot.Configuration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    ```

1. The `ConfigureServices` method registers the bot, including error handling and state management. It also registers the bot's endpoint service and the job state accessor.

    ```csharp
    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> specifies the contract for a collection of service descriptors.</param>
    /// <seealso cref="IStatePropertyAccessor{T}"/>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/dependency-injection"/>
    /// <seealso cref="https://docs.microsoft.com/en-us/azure/bot-service/bot-service-manage-channels?view=azure-bot-service-4.0"/>
    public void ConfigureServices(IServiceCollection services)
    {
        // Register the proactive bot.
        services.AddBot<ProactiveBot>(options =>
        {
            options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);

            // Set up error handling. (Trace output goes to the Emulator log, but not to the user.)
            options.OnTurnError = async (context, exception) =>
            {
                await context.TraceActivityAsync("Proactive bot exception", exception);
                await context.SendActivityAsync("Sorry, it looks like something went wrong!");
            };

            // The Memory Storage used here is for local bot debugging only. When the bot
            // is restarted, everything stored in memory will be gone.
            IStorage dataStore = new MemoryStorage();

            // Create Job State object.
            // The Job State object is where we persist anything at the job-scope.
            // It's independent of any user or conversation.
            var jobState = new JobState(dataStore);
            options.State.Add(jobState);
        });

        // Validate .bot file endpoint.
        services.AddSingleton(sp =>
        {
            var config = BotConfiguration.Load(@".\ProactiveBot.bot");
            var endpointService = (EndpointService)config.Services.First(s => s.Type == "endpoint")
                ?? throw new InvalidOperationException(".bot file 'endpoint' must be configured prior to running.");

            return endpointService;
        });

        // Create and register the state accessors for use with this bot.
        // Accessors created here are passed into the IBot-derived class on every turn.
        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<BotFrameworkOptions>>().Value
                ?? throw new InvalidOperationException(
                    "BotFrameworkOptions must be configured prior to setting up the state accessors.");

            var jobState = options.State.OfType<JobState>().FirstOrDefault();
            if (jobState == null)
            {
                throw new InvalidOperationException("JobState must be defined and added before adding conversation-scoped state accessors.");
            }

            // Create the custom state accessor.
            // State accessors enable other components to read and write individual properties of state.
            return new ProactiveAccessors(jobState)
            {
                // Create the state property accessor for job data.
                JobLogData = jobState.CreateProperty<JobLog>(ProactiveAccessors.JobLogDataName),
            };
        });
    }
    ```

# [JavaScript](#tab/javascript)

### Set up the server code

The **index.js** file  does the following:

- Includes the required packages and services
- References the bot class and the **.bot** file
- Creates the HTTP server
- Creates the bot adapter and storage objects
- Creates the bot and starts the server, passing activities to the bot

```javascript
const restify = require('restify');
const path = require('path');
const CONFIG_ERROR = 1;

// Import required bot services. See https://aka.ms/bot-services to learn more about the different part of a bot.
const { BotFrameworkAdapter, BotState, MemoryStorage } = require('botbuilder');
const { BotConfiguration } = require('botframework-config');

const MainDialog = require('./dialogs/mainDialog');

// Read botFilePath and botFileSecret from .env file.
// Note: Ensure you have a .env file and include botFilePath and botFileSecret.
const ENV_FILE = path.join(__dirname, '.env');
const env = require('dotenv').config({path: ENV_FILE});

// Create HTTP server.
let server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log(`\n${ server.name } listening to ${ server.url }.`);
    console.log(`\nGet Bot Framework Emulator: https://aka.ms/botframework-emulator.`);
    console.log(`\nTo talk to your bot, open proactive-messages.bot file in the Emulator.`);
});

// .bot file path
const BOT_FILE = path.join(__dirname, (process.env.botFilePath || ''));

// Read the bot's configuration from a .bot file identified by BOT_FILE.
// This includes information about the bot's endpoints and configuration.
let botConfig;
try {
    botConfig = BotConfiguration.loadSync(BOT_FILE, process.env.botFileSecret);
} catch (err) {
    console.log(`Error reading bot file. Please ensure you have valid botFilePath and botFileSecret set for your environment.`);
    console.log(err);
    process.exit(CONFIG_ERROR);
}

// Define the name of the bot, as specified in .bot file.
// See https://aka.ms/about-bot-file to learn more about .bot files.
const BOT_CONFIGURATION = 'proactive-messages-bot';

// Load the configuration profile specific to this bot identity.
const endpointConfig = botConfig.findServiceByNameOrId(BOT_CONFIGURATION);

// Create the adapter. See https://aka.ms/about-bot-adapter to learn more about using information from
// the .bot file when configuring your adapter.
const adapter = new BotFrameworkAdapter({
    appId: endpointConfig.appId || process.env.MicrosoftAppId,
    appPassword: endpointConfig.appPassword || process.env.MicrosoftAppPassword
});

// Define the state store for your bot. See https://aka.ms/about-bot-state to learn more about using MemoryStorage.
// A bot requires a state storage system to persist the dialog and user state between messages.
const memoryStorage = new MemoryStorage();

// Create state manager with in-memory storage provider. 
const botState = new BotState(memoryStorage, () => 'proactiveBot.botState');

// CAUTION: The Memory Storage used here is for local bot debugging only. When the bot
// is restarted, anything stored in memory will be gone. 
// For production bots use the Azure Cosmos DB storage, or Azure Blob storage providers. 
// const { CosmosDbStorage } = require('botbuilder-azure');
// const STORAGE_CONFIGURATION = 'cosmosDB'; // Cosmos DB configuration in your .bot file
// const cosmosConfig = botConfig.findServiceByNameOrId(STORAGE_CONFIGURATION);
// const cosmosStorage = new CosmosDbStorage({serviceEndpoint: cosmosConfig.connectionString, 
//                                            authKey: ?, 
//                                            databaseId: cosmosConfig.database, 
//                                            collectionId: cosmosConfig.collection});

// Create the main dialog, which serves as the bot's main handler.
const mainDlg = new MainDialog(botState, adapter);


// Listen for incoming requests.
server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (turnContext) => {
        // Route the message to the bot's main handler.
        await mainDlg.onTurn(turnContext);
    });
});
```

---

<!--TODO: (Post-Ignite) -- link to a second topic on how to write a job completion DirectLine client that will generate appropriate job completed event activities.-->

## Define the bot

The user can ask the bot to create and run a job for them. A separate job service could notify the bot when a job has completed.

The bot is designed to:

- Create a job in response to a `run` or `run job` message from the user.
- Show all registered jobs in response to a `show` or `show jobs` message from the user.
- Complete a job in response to a _job completed_ event that identifies the completed job.
- Simulate a job completed event in response to a `done <jobIdentifier>` message.
- Send a proactive message to the user, using the original conversation, when the job completes.

We do not show how to implement a system that can send event activities to our bot.
<!--TODO: DirectLine--Add back in once the DirectLine topic is added back to the TOC.
See [how to create a Direct Line bot and client](bot-builder-howto-direct-line.md) for information on how to do so.
-->

# [C#](#tab/csharp)

The bot has a few aspects:

- initialization code
- a turn handler
- methods for creating and completing the jobs

### Declare the class

```csharp
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Configuration;
using Microsoft.Bot.Schema;

namespace Microsoft.BotBuilderSamples
{
    /// <summary>
    /// For each interaction from the user, an instance of this class is called.
    /// This is a Transient lifetime service.  Transient lifetime services are created
    /// each time they're requested. For each Activity received, a new instance of this
    /// class is created. Objects that are expensive to construct, or have a lifetime
    /// beyond the single Turn, should be carefully managed.
    /// </summary>
    public class ProactiveBot : IBot
    {
        /// <summary>The name of events that signal that a job has completed.</summary>
        public const string JobCompleteEventName = "jobComplete";
    }
}
```

### Add initialization code

```csharp
/// <summary>
/// Initializes a new instance of the <see cref="ProactiveBot"/> class.</summary>
/// <param name="accessors">The state accessors for use with the bot.</param>
/// <param name="endpointService">The <see cref="EndpointService"/> portion of the <see cref="BotConfiguration"/>.</param>
public ProactiveBot(ProactiveAccessors accessors, EndpointService endpointService)
{
    StateAccessors = accessors ?? throw new ArgumentNullException(nameof(accessors));

    // Validate AppId.
    // Note: For local testing, .bot AppId is empty for the Bot Framework Emulator.
    AppId = string.IsNullOrWhiteSpace(endpointService.AppId) ? "1" : endpointService.AppId;
}

/// <summary>Gets the bot's app ID.</summary>
/// <remarks>AppId required to continue a conversation.
/// See <see cref="BotAdapter.ContinueConversationAsync"/> for more details.</remarks>
private string AppId { get; }

/// <summary>Gets the state accessors for use with the bot.</summary>
private ProactiveAccessors StateAccessors { get; }
```

### Add a turn handler

```csharp
/// <summary>
/// Every conversation turn will call this method.
/// Proactive messages use existing conversations (turns) with the user to deliver proactive messages.
/// Proactive messages can be ad-hoc or dialog-based. This is demonstrating ad-hoc, which doesn't
/// have to consider an interruption to an existing conversation, which may upset the dialog flow.
/// Note: The Email channel may send a proactive message outside the context of a active conversation.
/// </summary>
/// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
/// for processing this conversation turn. </param>
/// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
/// or threads to receive notice of cancellation.</param>
/// <returns>A task that represents the work queued to execute.</returns>
/// <remarks>
/// In the scenario, the bot is being called by users as normal (to start jobs and such) and by some
/// theoretical service (to signal when jobs are complete). The service is sending activities to the
/// bot on a separate conversation from the user conversations.</remarks>
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
{
    // See https://aka.ms/about-bot-activity-message to learn more about the message and other activity types.
    if (turnContext.Activity.Type != ActivityTypes.Message)
    {
        // Handle non-message activities.
        await OnSystemActivityAsync(turnContext);
    }
    else
    {
        // Get the job log.
        // The job log is a dictionary of all outstanding jobs in the system.
        JobLog jobLog = await StateAccessors.JobLogData.GetAsync(turnContext, () => new JobLog());

        // Get the user's text input for the message.
        var text = turnContext.Activity.Text.Trim().ToLowerInvariant();
        switch (text)
        {
            case "run":
            case "run job":

                // Start a virtual job for the user.
                JobLog.JobData job = CreateJob(turnContext, jobLog);

                // Set the new property
                await StateAccessors.JobLogData.SetAsync(turnContext, jobLog);

                // Now save it into the JobState
                await StateAccessors.JobState.SaveChangesAsync(turnContext);

                await turnContext.SendActivityAsync(
                    $"We're starting job {job.TimeStamp} for you. We'll notify you when it's complete.");

                break;

            case "show":
            case "show jobs":

                // Display information for all jobs in the log.
                if (jobLog.Count > 0)
                {
                    await turnContext.SendActivityAsync(
                        "| Job number &nbsp; | Conversation ID &nbsp; | Completed |<br>" +
                        "| :--- | :---: | :---: |<br>" +
                        string.Join("<br>", jobLog.Values.Select(j =>
                            $"| {j.TimeStamp} &nbsp; | {j.Conversation.Conversation.Id.Split('|')[0]} &nbsp; | {j.Completed} |")));
                }
                else
                {
                    await turnContext.SendActivityAsync("The job log is empty.");
                }

                break;

            default:
                // Check whether this is simulating a job completed event.
                string[] parts = text?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts != null && parts.Length == 2
                    && parts[0].Equals("done", StringComparison.InvariantCultureIgnoreCase)
                    && long.TryParse(parts[1], out long jobNumber))
                {
                    if (!jobLog.TryGetValue(jobNumber, out JobLog.JobData jobInfo))
                    {
                        await turnContext.SendActivityAsync($"The log does not contain a job {jobInfo.TimeStamp}.");
                    }
                    else if (jobInfo.Completed)
                    {
                        await turnContext.SendActivityAsync($"Job {jobInfo.TimeStamp} is already complete.");
                    }
                    else
                    {
                        await turnContext.SendActivityAsync($"Completing job {jobInfo.TimeStamp}.");

                        // Send the proactive message.
                        await CompleteJobAsync(turnContext.Adapter, AppId, jobInfo);
                    }
                }

                break;
        }

        if (!turnContext.Responded)
        {
            await turnContext.SendActivityAsync(
                "Type `run` or `run job` to start a new job.\r\n" +
                "Type `show` or `show jobs` to display the job log.\r\n" +
                "Type `done <jobNumber>` to complete a job.");
        }
    }
}

// Handles non-message activities.
private async Task OnSystemActivityAsync(ITurnContext turnContext)
{
    // On a job completed event, mark the job as complete and notify the user.
    if (turnContext.Activity.Type is ActivityTypes.Event)
    {
        var jobLog = await StateAccessors.JobLogData.GetAsync(turnContext, () => new JobLog());
        var activity = turnContext.Activity.AsEventActivity();
        if (activity.Name == JobCompleteEventName
            && activity.Value is long timestamp
            && jobLog.ContainsKey(timestamp)
            && !jobLog[timestamp].Completed)
        {
            await CompleteJobAsync(turnContext.Adapter, AppId, jobLog[timestamp]);
        }
    }
}
```

### Add job creation and completion methods

To start a job, the bot creates the job and records information about it, and the current conversation, in the job log. When the bot receives a job completed event in any conversation, it validates the job ID before calling the code to complete the job.

The code to complete the job gets the job log from state, and then marks the job as complete and sends a proactive message, using the adapter's _continue conversation_ method.

- The continue conversation call prompts the channel to initiate a turn independent of the user.
- The adapter runs the associated callback in place of the bot's normal on turn handler. This turn has its own turn context from which we retrieve the state information and send the proactive message to the user.

```csharp
// Creates and "starts" a new job.
private JobLog.JobData CreateJob(ITurnContext turnContext, JobLog jobLog)
{
    JobLog.JobData jobInfo = new JobLog.JobData
    {
        TimeStamp = DateTime.Now.ToBinary(),
        Conversation = turnContext.Activity.GetConversationReference(),
    };

    jobLog[jobInfo.TimeStamp] = jobInfo;

    return jobInfo;
}

// Sends a proactive message to the user.
private async Task CompleteJobAsync(
    BotAdapter adapter,
    string botId,
    JobLog.JobData jobInfo,
    CancellationToken cancellationToken = default(CancellationToken))
{
    await adapter.ContinueConversationAsync(botId, jobInfo.Conversation, CreateCallback(jobInfo), cancellationToken);
}

// Creates the turn logic to use for the proactive message.
private BotCallbackHandler CreateCallback(JobLog.JobData jobInfo)
{
    return async (turnContext, token) =>
    {
        // Get the job log from state, and retrieve the job.
        JobLog jobLog = await StateAccessors.JobLogData.GetAsync(turnContext, () => new JobLog());

        // Perform bookkeeping.
        jobLog[jobInfo.TimeStamp].Completed = true;

        // Set the new property
        await StateAccessors.JobLogData.SetAsync(turnContext, jobLog);

        // Now save it into the JobState
        await StateAccessors.JobState.SaveChangesAsync(turnContext);

        // Send the user a proactive confirmation message.
        await turnContext.SendActivityAsync($"Job {jobInfo.TimeStamp} is complete.");
    };
}
```

# [JavaScript](#tab/javascript)

The bot is defined in **dialogs/mainDialog/index.js** and has a few aspects:

- initialization code
- a turn handler
- methods for creating and completing the jobs

### Declare the class and add initialization code

```javascript
const { ActivityTypes, TurnContext } = require('botbuilder');

const JOBS_LIST = 'jobs';

class MainDialog {
    /**
     * 
     * @param {BotState} botState A BotState object used to store information for the bot independent of user or conversation.
     * @param {BotAdapter} adapter A BotAdapter used to send and receive messages.
     */
    constructor(botState, adapter) {
        this.botState = botState;
        this.adapter = adapter;

        this.jobsList = this.botState.createProperty(JOBS_LIST);
    }
}

// Helper function to check if object is empty.
function isEmpty(obj) {
    for(var key in obj) {
        if(obj.hasOwnProperty(key))
            return false;
    }
    return true;
};

module.exports = MainDialog;
```

### The turn handler

The `onTurn` and `showJobs` methods are defined  within the `MainDialog` class.`onTurn` handles input from users. It would also receive event activities from the hypothetical job fulfillment system. `showJobs` formats and sends the job log.

```javascript
/**
    * 
    * @param {TurnContext} turnContext A TurnContext object representing an incoming message to be handled by the bot.
    */
async onTurn(turnContext) {
    // See https://aka.ms/about-bot-activity-message to learn more about the message and other activity types.
    if (turnContext.activity.type === ActivityTypes.Message) {

        const utterance = (turnContext.activity.text || '').trim().toLowerCase();

        // If user types in run, create a new job.
        if (utterance === 'run'){
            await this.createJob(turnContext);
        } else if (utterance === 'show') {
            await this.showJobs(turnContext);
        } else {
            const words = utterance.split(' ');

            // If the user types done and a Job Id Number,
            // we check if the second word input is a number.
            if (words[0] === 'done' && !isNaN(parseInt(words[1]))) {
                var jobIdNumber = words[1];
                await this.completeJob(turnContext, jobIdNumber);

            } else if (words[0] === 'done' && (words.length < 2 || isNaN(parseInt(words[1])))) {
                await turnContext.sendActivity('Enter the job ID number after "done".');
            }
        }

        if (!turnContext.responded) {
            await turnContext.sendActivity(`Say "run" to start a job, or "done <job>" to complete one.`);
        }

    } else if (turnContext.activity.type === 'event' && turnContext.activity.name === 'jobCompleted') {
        var jobIdNumber = turnContext.activity.value;
        if (!isNaN(parseInt(jobIdNumber))) {
            await this.completeJob(turnContext, jobIdNumber);
        }
    }

    await this.botState.saveChanges(turnContext);
}

// Show a list of the pending jobs
async showJobs(turnContext) {
    const jobs = await this.jobsList.get(turnContext, {});
    if (Object.keys(jobs).length) {
        await turnContext.sendActivity(
            '| Job number &nbsp; | Conversation ID &nbsp; | Completed |<br>' +
            '| :--- | :---: | :---: |<br>' +
            Object.keys(jobs).map((key) => {
                return `${ key } &nbsp; | ${ jobs[key].reference.conversation.id.split('|')[0] } &nbsp; | ${ jobs[key].completed }`
            }).join('<br>'));
    } else {
        await turnContext.sendActivity('The job log is empty.');
    }
}
```

### Logic to start a job

The `createJob` method is defined  within the `MainDialog` class. It creates and logs new jobs for the user. In theory, it would also forward this information to the job fulfillment system.

```javascript
// Save job ID and conversation reference.
async createJob(turnContext) {

    // Create a unique job ID.
    var date = new Date();
    var jobIdNumber = date.getTime();

    // Get the conversation reference.
    const reference = TurnContext.getConversationReference(turnContext.activity);

    // Get the list of jobs. Default it to {} if it is empty.
    const jobs = await this.jobsList.get(turnContext, {});

    // Try to find previous information about the saved job.
    const jobInfo = jobs[jobIdNumber];

    try {
        if (isEmpty(jobInfo)){
            // Job object is empty so we have to create it
            await turnContext.sendActivity(`Need to create new job ID: ${ jobIdNumber }`);

            // Update jobInfo with new info
            jobs[jobIdNumber] = { completed: false, reference: reference };

            try {
                // Save to storage
                await this.jobsList.set(turnContext, jobs);
                // Notify the user that the job has been processed 
                await turnContext.sendActivity('Successful write to log.');
            } catch(err) {
                await turnContext.sendActivity(`Write failed: ${ err.message }`);
            }
        }
    } catch(err){
        await turnContext.sendActivity(`Read rejected. ${ err.message }`);
    }
}
```

### Logic to complete a job

The `completeJob` method is defined  within the `MainDialog` class. It performs some bookkeeping and sends the proactive message to the user (in the user's original conversation) that their job completed.

```javascript
async completeJob(turnContext, jobIdNumber) {
    // Get the list of jobs from the bot's state property accessor.
    const jobs = await this.jobsList.get(turnContext, {});

    // Find the appropriate job in the list of jobs.
    let jobInfo = jobs[jobIdNumber];

    // If no job was found, notify the user of this error state.
    if (isEmpty(jobInfo)) {
        await turnContext.sendActivity(`Sorry no job with ID ${ jobIdNumber }.`);
    } else {
        // Found a job with the ID passed in.
        const reference = jobInfo.reference;
        const completed = jobInfo.completed;

        // If the job is not yet completed and conversation reference exists,
        // use the adapter to continue the conversation with the job's original creator.
        if (reference && !completed) {
            // Since we are going to proactively send a message to the user who started the job,
            // we need to create the turnContext based on the stored reference value.
            await this.adapter.continueConversation(reference, async (proactiveTurnContext) => {
                // Complete the job.
                jobInfo.completed = true;
                // Save the updated job.
                await this.jobsList.set(turnContext, jobs);
                // Notify the user that the job is complete.
                await proactiveTurnContext.sendActivity(`Your queued job ${ jobIdNumber } just completed.`);
            });

            // Send a message to the person who completed the job.
            await turnContext.sendActivity('Job completed. Notification sent.');

        }
        // The job has already been completed.
        else if (completed) {
            await turnContext.sendActivity('This job is already completed, please start a new job.');
        };
    };
};
```

---

## Test your bot

Build an run your bot locally and open two Emulator windows.

1. Note that the conversation ID is different in the two windows.
1. In the first window, type `run` a couple times to start a few jobs.
1. In the second window, type `show` to see a list of the jobs in the log.
1. In the second window, type `done <jobNumber>`, where `<jobNumber>` is one of the job numbers from the log, without the angle brackets. (The bot code is designed to interpret this as if it were a jobComplete event.)
1. Note that the bot sends a proactive message to the user in the first window.

<!--TODO: Recreate the screen shots once we're happy with both the C# and JS versions of the code.-->

Your conversation might look like this from the user's perspective:

![User's emulator session](~/v4sdk/media/how-to-proactive/user.png)

And look like this from the simulated job system's perspective:

![Job system's emulator session](~/v4sdk/media/how-to-proactive/job-system.png)

<!-- Add a next steps section. -->
