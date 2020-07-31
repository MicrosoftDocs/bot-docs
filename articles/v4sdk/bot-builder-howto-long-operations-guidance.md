---
title: Long operations guidance - Bot Service
description: Learn how to handle long operations within a bot.
keywords: long operations, timeout, 15 seconds
author: ericdahlvang
ms.author: ericdahlvang
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 07/30/2020
monikerRange: 'azure-bot-service-4.0'
---

# Long operations guidance

[!INCLUDE[applies-to](../includes/applies-to.md)]

Properly handling long operations is an important aspect of a robust bot. When the Azure Bot Service sends an activity to your bot from a channel, the bot is expected to process the activity quickly.  If the bot does not complete the operation within 10 to 15 seconds, depending on channel, the Azure Bot Service will timeout and report back to the client a `504:GatewayTimeout`, as described in [How bots work][concept-basics]. The guidance in this topic is to have an external service execute the operation and notify the bot when it has completed. 

## Prerequisites

- If you don't have an Azure subscription, create a [free](https://azure.microsoft.com/free/) account before you begin.
- Familiarity with [Prompts](https://docs.microsoft.com/azure/bot-service/bot-builder-primitive-prompts)
- Bot Framework Sample [Multi-Turn-Prompt](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/05.multi-turn-prompt).
- Familiarity with [Azure Storage Queues](https://docs.microsoft.com/azure/storage/queues/storage-queues-introduction).
- Familiarity with Azure Functions [C# script](https://docs.microsoft.com/azure/azure-functions/functions-reference-csharp).

## About this samples

The sample code in this article begins with the structure of the multi-turn prompt sample bot, then extends that bot's functionality by adding additional code (provided later). This extended code performs a long running operation, and demonstrates how to respond to a user after the operation has completed. In the updated sample:

- The bot asks the user which long operation to perform.
- The bot receives an activity from the user, and determines which long operation to perform.
- Bot notifies the user the operation will take some time.
- Bot sends the operation off to an Azure Function.
- Bot saves state, indicating there is an operation in progress.
- Bot responds to messages from the user, notifying them the operation is still in progress.
- Azure Function executes the long running operation.
- Azure Functions sends an `event` activity to the bot, notifying it that the operation completed.
- The bot resumes the conversation and sends a proactive message to notify the user that the operation completed. The bot then clears the operation state mentioned earlier.

This example uses a `LongOperationPrompt`, derived from `ActivityPrompt`. When the `LongOperationPrompt` queues the Activity to be processed, it includes a choice from the user within the .Value property. This Activity is then used in the Azure Function, modifed, and wrapped in a different Event Activity before it is sent back to the bot using the Direct Line Client. Within the bot, the Event activity is used to resume the conversation by calling Adapter.ContinueConversation.  The dialog stack is then loaded, and the `LongOperationPrompt` completes.

## Create an Azure Storage account

Create an Azure Storage account, and retrieve the connection string.
(See [create a storage account](https://docs.microsoft.com/azure/storage/common/storage-account-create) and [copy your credentials from the Azure portal](https://docs.microsoft.com/azure/storage/queues/storage-dotnet-how-to-use-queues?tabs=dotnet#copy-your-credentials-from-the-azure-portal) for more information.)

## Create a Bot Channels Registration

Before creating the registration, setup `ngrok` and retrieve a url to be used as the bot's `Messaging Endpoint` during local debugging. See [Debugging using Ngrok](https://docs.microsoft.com/azure/bot-service/bot-service-debug-channel-ngrok).

Create a Bot Channels Registration in the Azure Portal, or using the Az Cli. Set the Messaging Endpoint to the `ngrok` url, with `\api\messages` appended. After the bot registration is created, obtain the `MicrosoftAppId` and `MicrosoftAppPassword`, enable the `Direct Line` channel, and retrieve a `Direct Line` secret.  These will be added to the bot, and Azure Function respectively.  

See:
- [Register a bot](https://docs.microsoft.com/azure/bot-service/bot-service-quickstart-registration)
- [Find Your Azure Bot’s AppID and AppSecret](https://blog.botframework.com/2018/07/03/find-your-azure-bots-appid-and-appsecret/)
- [Connect a bot to Direct Line](https://docs.microsoft.com/azure/bot-service/bot-service-channel-connect-directline)

## Create the Azure Function

Create an Azure Function App based on the .Net Core runtime stack. [See Create Function App](https://docs.microsoft.com/azure/azure-functions/functions-create-function-app-portal) Create  [Azure Functions C# script](https://docs.microsoft.com/azure/azure-functions/functions-reference-csharp).  

Add a `DirectLineSecret` application setting to the Function App. See [Functioni App Settings](https://docs.microsoft.com/azure/azure-functions/functions-how-to-use-azure-function-app-settings)

Within the Function App, add a function based on the [Azure Queue Storage template](https://docs.microsoft.com/azure/azure-functions/functions-bindings-storage-queue-trigger).  Set the desired queue name, and choose the `Azure Storage Account` created in an earlier step.  This queue name will also be placed in the bot's `appsettings.json`.

Add a `function.proj` file to the function.

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <TargetFramework>netstandard2.0</TargetFramework>
    </PropertyGroup>

    <ItemGroup>
        <PackageReference Include="Microsoft.Bot.Connector.DirectLine" Version="3.0.2" />
        <PackageReference Include="Microsoft.Rest.ClientRuntime" Version="2.3.4" />
    </ItemGroup>
</Project>
```

Update `run.csx` with the following code:

```csharp
#r "Newtonsoft.Json"

using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Bot.Connector.DirectLine;
using System.Threading;

public static async Task Run(string queueItem, ILogger log)
{
    log.LogInformation($"C# Queue trigger function processing");

    JsonSerializerSettings jsonSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
    var originalActivity =  JsonConvert.DeserializeObject<Activity>(queueItem, jsonSettings);
    // Perform long operation here....
    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(15));

    if(originalActivity.Value.ToString() == "option 1") 
    {
        originalActivity.Value = " (Result for long operation one!)";
    }
    else if(originalActivity.Value.ToString() == "option 2") 
    {
        originalActivity.Value = " (A different result for operation two!)";
    }

    originalActivity.Value = "LongOperationComplete:" + originalActivity.Value;
    var responseActivity =  new Activity("event");
    responseActivity.Value = originalActivity;
    responseActivity.Name = "LongOperationResponse";
    responseActivity.From = new ChannelAccount("GenerateReport", "AzureFunction");

    var directLineSecret = Environment.GetEnvironmentVariable("DirectLineSecret");            
    using(DirectLineClient client = new DirectLineClient(directLineSecret)) 
    {
        var conversation = await client.Conversations.StartConversationAsync();
        await client.Conversations.PostActivityAsync(conversation.ConversationId, responseActivity);
    }

    log.LogInformation($"Done...");
}
```

## Creating the Bot

Starting with the [Multi-Turn-Prompt](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/05.multi-turn-prompt) sample, add the [Azure.Storage.Queues](https://www.nuget.org/packages/Azure.Storage.Queues/) package.  For more information, see [How to Use Queues](https://docs.microsoft.com/azure/storage/queues/storage-dotnet-how-to-use-queues).

Add the connection string for the `Azure Storage` account created earlier, and Storage Queue Name, to `appsettings.json`.  Ensure the queue name is the same as what was used to create the Queue Trigger Function earlier.  Also add the `MicrosoftAppId`  and `MicrosoftAppPassword` from the `Bot Channels Registration`.

```json
{
  "MicrosoftAppId": "MyMicrosoftAppId",
  "MicrosoftAppPassword": "MyMicrosoftAppPassword",
  "StorageQueueName": "longprocessqueue",
  "QueueStorageConnection": "DefaultEndpointsProtocol=https;AccountName=myStorageAccountName;AccountKey=myAccountKey=="
}
```

**AzureQueuesService.cs**

Create an Azure Queues service to queue activities which need to be processed.

```csharp
/// <summary>
/// Service used to queue messages to an Azure.Storage.Queues.
/// </summary>
public class AzureQueuesService
{
    private static JsonSerializerSettings jsonSettings = new JsonSerializerSettings() 
        { 
            Formatting = Formatting.Indented, 
            NullValueHandling = NullValueHandling.Ignore 
        };

    private bool _createQueuIfNotExists = true;
    private readonly QueueClient _queueClient;

    /// <summary>
    /// Creates a new instance of <see cref="AzureQueuesService"/>.
    /// </summary>
    /// <param name="config"><see cref="IConfiguration"/> used to retrieve 
    /// StorageQueueName and QueueStorageConnection from appsettings.json.</param>
    public AzureQueuesService(IConfiguration config)
    {
        var queueName = config["StorageQueueName"];
        var connectionString = config["QueueStorageConnection"];

        _queueClient = new QueueClient(connectionString, queueName);
    }

    /// <summary>
    /// Queue and Activity, with option in the Activity.Value to Azure.Storage.Queues
    /// 
    /// <seealso cref="https://github.com/microsoft/botbuilder-dotnet/blob/master/libraries/Microsoft.Bot.Builder.Azure/Queues/ContinueConversationLater.cs"/>
    /// </summary>
    /// <param name="referenceActivity">Activity to queue after a call to GetContinuationActivity.</param>
    /// <param name="option">The option the user chose, which will be passed within the .Value of the activity queued.</param>
    /// <param name="cancellationToken">Cancellation token for the async operation.</param>
    /// <returns>Queued <see cref="Azure.Storage.Queues.Models.SendReceipt.MessageId"/>.</returns>
    public async Task<string> QueueActivityToProcess(Activity referenceActivity, string option, CancellationToken cancellationToken)
    {
        if (_createQueuIfNotExists)
        {
            _createQueuIfNotExists = false;
            await _queueClient.CreateIfNotExistsAsync().ConfigureAwait(false);
        }

        // create ContinuationActivity from the conversation reference.
        var activity = referenceActivity.GetConversationReference().GetContinuationActivity();
        // Pass the user's choice in the .Value
        activity.Value = option;

        var message = Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(activity, jsonSettings)));
            
        // Aend ResumeConversation event, it will get posted back to us with a specific value, giving us 
        // the ability to process it and do the right thing.
        var reciept = await _queueClient.SendMessageAsync(message, cancellationToken).ConfigureAwait(false);
        return reciept.Value.MessageId;
    }
}
```

## Dialogs

Remove the `UserProfileDialog.cs` file, and add the following to the Dialogs folder:

**Dialogs\LongOperationPrompt.cs**

```csharp
/// <summary>
/// <see cref="ActivityPrompt"/> implementation which will queue an activity,
/// along with the <see cref="LongOperationPromptOptions.LongOperationOption"/>,
/// and wait for an <see cref="ActivityTypes.Event"/> with name of "ContinueConversation"
/// and Value containing the text: "LongOperationComplete".
/// 
/// The result of this prompt will be the received Event Activity, which is sent by 
/// the Azure Function after it finishes the long operation.
/// </summary>
public class LongOperationPrompt : ActivityPrompt
{
    private readonly AzureQueuesService _queueService;
    
    /// <summary>
    /// Create a new instance of <see cref="LongOperationPrompt"/>.
    /// </summary>
    /// <param name="dialogId">Id of this <see cref="LongOperationPrompt"/>.</param>
    /// <param name="validator">Validator to use for this prompt.</param>
    /// <param name="queueService"><see cref="AzureQueuesService"/> to use for Enqueuing the activity to process.</param>
    public LongOperationPrompt(string dialogId, PromptValidator<Activity> validator, AzureQueuesService queueService) 
        : base(dialogId, validator)
    {
        _queueService = queueService;
    }

    public async override Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options, CancellationToken cancellationToken = default)
    {
        // When the dialog begins, queue the option chosen within the Activity queued.
        await _queueService.QueueActivityToProcess(dc.Context.Activity, (options as LongOperationPromptOptions).LongOperationOption, cancellationToken);

        return await base.BeginDialogAsync(dc, options, cancellationToken);
    }

    protected override Task<PromptRecognizerResult<Activity>> OnRecognizeAsync(ITurnContext turnContext, IDictionary<string, object> state, PromptOptions options, CancellationToken cancellationToken = default)
    {
        var result = new PromptRecognizerResult<Activity>() { Succeeded = false };

        if(turnContext.Activity.Type == ActivityTypes.Event 
            && turnContext.Activity.Name == "ContinueConversation" 
            && turnContext.Activity.Value != null
            // Custom validation within LongOperationPrompt.  
            // 'LongOperationComplete' is added to the Activity.Value in the Queue consumer (See: Azure Function)
            && turnContext.Activity.Value.ToString().Contains("LongOperationComplete", System.StringComparison.InvariantCultureIgnoreCase))
        {
            result.Succeeded = true;
            result.Value = turnContext.Activity;
        }

        return Task.FromResult(result);
    }
}
```

**Dialogs\LongOperationPromptOptions.cs**

```csharp
/// <summary>
/// Options sent to <see cref="LongOperationPrompt"/> demonstrating how a value
/// can be passed along with the queued activity.
/// </summary>
public class LongOperationPromptOptions : PromptOptions
{
    /// <summary>
    /// This is a property sent through the Queue, and is used
    /// in the queue consumer (the Azure Function) to differentiate 
    /// between long operations chosen by the user.
    /// </summary>
    public string LongOperationOption { get; set; }
}
```

**LongOperationDialog.cs**

```csharp
/// <summary>
/// This dialog demonstrates how to use the <see cref="LongOperationPrompt"/>.
/// 
/// The user is provided an option to perform any of three long operations.
/// Their choice is then sent to the <see cref="LongOperationPrompt"/>.
/// When the prompt completes, the result is received as an Activity in the
/// final Waterfall step.
/// </summary>
public class LongOperationDialog : ComponentDialog
{
    public LongOperationDialog(AzureQueuesService queueService)
        : base(nameof(LongOperationDialog))
    {
        // This array defines how the Waterfall will execute.
        var waterfallSteps = new WaterfallStep[]
        {
            OperationTimeStepAsync,
            LongOperationStepAsync,
            OperationCompleteStepAsync,
        };

        // Add named dialogs to the DialogSet. These names are saved in the dialog state.
        AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
        AddDialog(new LongOperationPrompt(nameof(LongOperationPrompt), (vContext, token) =>
        {
            return Task.FromResult(vContext.Recognized.Succeeded);
        }, queueService));
        AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));

        // The initial child Dialog to run.
        InitialDialogId = nameof(WaterfallDialog);
    }

    private static async Task<DialogTurnResult> OperationTimeStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is a Prompt Dialog.
        // Running a prompt here means the next WaterfallStep will be run when the user's response is received.
        return await stepContext.PromptAsync(nameof(ChoicePrompt),
            new PromptOptions
            {
                Prompt = MessageFactory.Text("Please select a long operation test option."),
                Choices = ChoiceFactory.ToChoices(new List<string> { "option 1", "option 2", "option 3" }),
            }, cancellationToken);
    }

    private static async Task<DialogTurnResult> LongOperationStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        var value = ((FoundChoice)stepContext.Result).Value;
        stepContext.Values["longOperationOption"] = value;

        var prompt = MessageFactory.Text("...one moment please....");
        // The reprompt will be shown if the user messages the bot while the long operation is being performed.
        var retryPrompt = MessageFactory.Text($"Still performing the long operation: {value} ... (is the Azure Function executing from the queue?)");
        return await stepContext.PromptAsync(nameof(LongOperationPrompt),
                                                    new LongOperationPromptOptions
                                                    {
                                                        Prompt = prompt,
                                                        RetryPrompt = retryPrompt,
                                                        LongOperationOption = value,
                                                    }, cancellationToken);
    }

    private static async Task<DialogTurnResult> OperationCompleteStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        stepContext.Values["longOperationResult"] = stepContext.Result;
        await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Thanks for waiting. { (stepContext.Result as Activity).Value}"), cancellationToken);

        // Start over by replacing the dialog with itself.
        return await stepContext.ReplaceDialogAsync(nameof(WaterfallDialog), null, cancellationToken);
    }
}
```

## Register services and Dialog

**Startup.cs**

Update `ConfigureServices`, registering the `LongOperationDialog` and adding the `AzureQueuesService`.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers().AddNewtonsoftJson();

    // Create the Bot Framework Adapter with error handling enabled.
    services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

    // In production, this should be a persistent storage provider.bot
    services.AddSingleton<IStorage>(new MemoryStorage());

    // Create the Conversation state. (Used by the Dialog system itself.)
    services.AddSingleton<ConversationState>();

    // The Dialog that will be run by the bot.
    services.AddSingleton<LongOperationDialog>();

    // Service used to queue into Azure.Storage.Queues
    services.AddSingleton<AzureQueuesService>();

    // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
    services.AddTransient<IBot, DialogBot<LongOperationDialog>>();
}
```


## To test the bot

1. If you have not done so already, install the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme).
1. Run the sample locally on your machine.
1. Start the emulator, connect to your bot, and send messages as shown below.


![Bot Example](~/media/how-to-long-operations/long-operations-bot-example.png)


<!-- Footnote-style links -->

[concept-basics]: bot-builder-basics.md
[concept-dialogs]: bot-builder-concept-dialog.md
