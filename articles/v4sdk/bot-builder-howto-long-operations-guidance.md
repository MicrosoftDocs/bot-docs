---
title: Manage a long-running operation
description: Learn how to avoid time-out issues and use proactive messages to handle long-running operations within a bot.
keywords: long operations, time out, 15 seconds
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: azure-ai-bot-service
ms.topic: how-to
ms.date: 08/10/2022
monikerRange: 'azure-bot-service-4.0'
ms.custom:
  - evergreen
---

# Manage a long-running operation

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Proper handling of long-running operations is an important aspect of a robust bot. When the Azure AI Bot Service sends an activity to your bot from a channel, the bot is expected to process the activity quickly. If the bot doesn't complete the operation within 10 to 15 seconds, depending on the channel, the Azure AI Bot Service will time out and report back to the client a `504:GatewayTimeout`, as described in [How bots work][concept-basics].

This article describes how to use an external service to execute the operation and to notify the bot when it has completed.

## Prerequisites

- If you don't have an Azure subscription, create a [free](https://azure.microsoft.com/free/) account before you begin.
- Familiarity with
  [prompts in waterfall dialogs](bot-builder-concept-waterfall-dialogs.md#prompts) and
  [proactive messaging](bot-builder-howto-proactive-message.md).
- Familiarity with
  [Azure Queue Storage](/azure/storage/queues/storage-queues-introduction) and
  [Azure Functions C# script](/azure/azure-functions/functions-reference-csharp).
- A copy of the **multi-turn prompt** sample in [C#](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/05.multi-turn-prompt).

## About this sample

This article begins with the multi-turn prompt sample bot and adds code for performing long-running operations. It also demonstrates how to respond to a user after the operation has completed. In the updated sample:

- The bot asks the user which long-running operation to perform.
- The bot receives an activity from the user, and determines which operation to perform.
- The bot notifies the user the operation will take some time and sends the operation to a C# function.
  - The bot saves state, indicating there's an operation in progress.
  - While the operation is running, the bot responds to messages from the user, notifying them the operation is still in progress.
  - Azure Functions manages the long-running operation and sends an `event` activity to the bot, notifying it that the operation completed.
- The bot resumes the conversation and sends a proactive message to notify the user that the operation completed. The bot then clears the operation state mentioned earlier.

This example defines a `LongOperationPrompt` class, derived from the abstract `ActivityPrompt` class. When the `LongOperationPrompt` queues the activity to be processed, it includes a choice from the user within the activity's _value_ property. This activity is then consumed by Azure Functions, modified, and wrapped in a different `event` activity before it's sent back to the bot using a Direct Line client. Within the bot, the event activity is used to resume the conversation by calling the adapter's _continue conversation_ method. The dialog stack is then loaded, and the `LongOperationPrompt` completes.

This article touches on many different technologies. See the [additional information](#additional-information) section for links to associated articles.

## Create an Azure Storage account

Create an Azure Storage account, and retrieve the connection string. You'll need to add the connection string to your bot's configuration file.

For more information, see [create a storage account](/azure/storage/common/storage-account-create) and [copy your credentials from the Azure portal](/azure/storage/queues/storage-dotnet-how-to-use-queues?tabs=dotnet#copy-your-credentials-from-the-azure-portal).

## Create a bot resource

1. Setup Dev Tunnels and retrieve a URL to be used as the bot's _messaging endpoint_ during local debugging. The messaging endpoint will be the HTTPS forwarding URL with `/api/messages/` appended&mdash;the default port for new bots is 3978.

    For more information, see how to [debug a bot using devtunnel](../bot-service-debug-channel-devtunnel.md).

1. Create an Azure Bot resource in the Azure portal or with the Azure CLI. Set the bot's messaging endpoint to the one you created with Dev Tunnels. After the bot resource is created, obtain the bot's Microsoft app ID and password. Enable the Direct Line channel, and retrieve a Direct Line secret. You'll add these to your bot code and C# function.

    For more information, see how to [manage a bot](../bot-service-manage-overview.md) and how to [connect a bot to Direct Line](../bot-service-channel-connect-directline.md).

## Create the C# function

1. Create an Azure Functions app based on the .NET Core runtime stack.

    For more information, see how to [create a function app](/azure/azure-functions/functions-create-function-app-portal) and the [Azure Functions C# script reference](/azure/azure-functions/functions-reference-csharp).

1. Add a `DirectLineSecret` application setting to the Function App.

    For more information, see how to [manage your function app](/azure/azure-functions/functions-how-to-use-azure-function-app-settings).

1. Within the Function App, add a function based on the [Azure Queue Storage template](/azure/azure-functions/functions-bindings-storage-queue-trigger).

    Set the desired queue name, and choose the `Azure Storage Account` created in an earlier step. This queue name will also be placed in the bot's **appsettings.json** file.

1. Add a **function.proj** file to the function.

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

1. Update **run.csx** with the following code:

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

        if(originalActivity.Value.ToString().Equals("option 1", StringComparison.OrdinalIgnoreCase))
        {
            originalActivity.Value = " (Result for long operation one!)";
        }
        else if(originalActivity.Value.ToString().Equals("option 2", StringComparison.OrdinalIgnoreCase))
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

## Create the bot

1. Start with a copy of the C# [Multi-Turn-Prompt](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/05.multi-turn-prompt) sample.
1. Add the **Azure.Storage.Queues** NuGet package to your project.
1. Add the connection string for the Azure Storage account you created earlier, and the Storage Queue Name, to your bot's configuration file.

    Ensure the queue name is the same as the one you used to create the Queue Trigger Function earlier. Also add the values for the `MicrosoftAppId` and `MicrosoftAppPassword` properties that you generated earlier when you created the Azure Bot resource.

    **appsettings.json**

    ```json
    {
      "MicrosoftAppId": "<your-bot-app-id>",
      "MicrosoftAppPassword": "<your-bot-app-password>",
      "StorageQueueName": "<your-azure-storage-queue-name>",
      "QueueStorageConnection": "<your-storage-connection-string>"
    }
    ```

1. Add an `IConfiguration` parameter to **DialogBot.cs** in order to retrieve the `MicrsofotAppId`. Also add an `OnEventActivityAsync` handler for the `LongOperationResponse` from the Azure Function.

    **Bots\DialogBot.cs**

    ```csharp
    protected readonly IStatePropertyAccessor<DialogState> DialogState;
    protected readonly Dialog Dialog;
    protected readonly BotState ConversationState;
    protected readonly ILogger Logger;
    private readonly string _botId;

    /// <summary>
    /// Create an instance of <see cref="DialogBot{T}"/>.
    /// </summary>
    /// <param name="configuration"><see cref="IConfiguration"/> used to retrieve MicrosoftAppId
    /// which is used in ContinueConversationAsync.</param>
    /// <param name="conversationState"><see cref="ConversationState"/> used to store the DialogStack.</param>
    /// <param name="dialog">The RootDialog for this bot.</param>
    /// <param name="logger"><see cref="ILogger"/> to use.</param>
    public DialogBot(IConfiguration configuration, ConversationState conversationState, T dialog, ILogger<DialogBot<T>> logger)
    {
        _botId = configuration["MicrosoftAppId"] ?? Guid.NewGuid().ToString();
        ConversationState = conversationState;
        Dialog = dialog;
        Logger = logger;
        DialogState = ConversationState.CreateProperty<DialogState>(nameof(DialogState));
    }

    public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
    {
        await base.OnTurnAsync(turnContext, cancellationToken);

        // Save any state changes that might have occurred during the turn.
        await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
    }

    protected override async Task OnEventActivityAsync(ITurnContext<IEventActivity> turnContext, CancellationToken cancellationToken)
    {
        // The event from the Azure Function will have a name of 'LongOperationResponse'
        if (turnContext.Activity.ChannelId == Channels.Directline && turnContext.Activity.Name == "LongOperationResponse")
        {
            // The response will have the original conversation reference activity in the .Value
            // This original activity was sent to the Azure Function via Azure.Storage.Queues in AzureQueuesService.cs.
            var continueConversationActivity = (turnContext.Activity.Value as JObject)?.ToObject<Activity>();
            await turnContext.Adapter.ContinueConversationAsync(_botId, continueConversationActivity.GetConversationReference(), async (context, cancellation) =>
            {
                Logger.LogInformation("Running dialog with Activity from LongOperationResponse.");

                // ContinueConversationAsync resets the .Value of the event being continued to Null, 
                //so change it back before running the dialog stack. (The .Value contains the response 
                //from the Azure Function)
                context.Activity.Value = continueConversationActivity.Value;
                await Dialog.RunAsync(context, DialogState, cancellationToken);

                // Save any state changes that might have occurred during the inner turn.
                await ConversationState.SaveChangesAsync(context, false, cancellationToken);
            }, cancellationToken);
        }
        else
        {
            await base.OnEventActivityAsync(turnContext, cancellationToken);
        }
    }
    ```

1. Create an Azure Queues service to queue activities to be processed.

    **AzureQueuesService.cs**

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

Remove the old dialog and replace it with new dialogs to support the operations.

1. Remove the **UserProfileDialog.cs** file.
1. Add a custom prompt dialog that asks the user which operation to perform.

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

1. Add a prompt options class for the custom prompt.

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

1. Add the dialog that uses the custom prompt to get the user's choice and initiates the long-running operation.

    **Dialogs\LongOperationDialog.cs**

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
            // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it's a Prompt Dialog.
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

In **Startup.cs**, update the `ConfigureServices` method to register the `LongOperationDialog` and add the `AzureQueuesService`.

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

1. If you haven't done so already, install the [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md).
1. Run the sample locally on your machine.
1. Start the Emulator and connect to your bot.
1. Choose a long operation to start.
    - The bot sends a _one moment, please_ message and queues the Azure function.
    - If the user tries to interact with the bot before the operation completes, the bot replies with a _still working_ message.
    - Once the operation completes, the bot sends a proactive message to the user to let them know it finished.

:::image type="content" source="./media/how-to-long-operations/long-operations-bot-example.png" alt-text="Sample transcript with the user initiating a long operation and eventually receiving a proactive message that the operation completed.":::

## Additional information

| Tool or feature | Resources |
|:-|:-|
| Azure Functions | [Create a function app](/azure/azure-functions/functions-create-function-app-portal)<br/>[Azure Functions C# script](/azure/azure-functions/functions-reference-csharp)<br/>[Manage your function app](/azure/azure-functions/functions-how-to-use-azure-function-app-settings) |
| Azure portal | [Manage a bot](../bot-service-manage-overview.md)<br/>[Connect a bot to Direct Line](../bot-service-channel-connect-directline.md) |
| Azure Storage | [Azure Queue Storage](/azure/storage/queues/storage-queues-introduction)<br/>[Create a storage account](/azure/storage/common/storage-account-create)<br/>[Copy your credentials from the Azure portal](/azure/storage/queues/storage-dotnet-how-to-use-queues?tabs=dotnet#copy-your-credentials-from-the-azure-portal)<br/>[How to Use Queues](/azure/storage/queues/storage-dotnet-how-to-use-queues) |
| Bot basics | [How bots work][concept-basics]<br/>[Prompts in waterfall dialogs](bot-builder-concept-waterfall-dialogs.md#prompts)<br/>[Proactive messaging](bot-builder-howto-proactive-message.md) |
| Dev Tunnels | [Debug a bot using devtunnel](../bot-service-debug-channel-devtunnel.md) |

[concept-basics]: bot-builder-basics.md
[concept-dialogs]: bot-builder-concept-dialog.md
