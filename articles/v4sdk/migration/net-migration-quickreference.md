---
title: .NET v3 to v4 migration quick reference | Microsoft Docs
description: An outline of the major differences in the v3 and v4 .NET Bot Framework SDK.
keywords: .net, bot migration, dialogs, v3 bot
author: JonathanFingold
ms.author: v-jofing
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 05/31/2019
monikerRange: 'azure-bot-service-4.0'
---

# .NET migration quick reference

The BotBuilder .NET SDK v4 introduces several fundamental changes that affect how bots are authored. The purpose of this guide is to provide a quick reference to highlight common differences between accomplishing tasks in the v3 and v4 SDKs.

- How information passes between a bot and channels has changed. In v3, you used the _Conversation_ object and _SendAsync_ method to process a message, and Autofac was used extensively for loading various dependencies. In v4, you use the _Adapter_ and _TurnContext_ objects to process a message, and you can use the dependency injection library of your choice.

- Also, dialogs and bot instances have been further decoupled. In v3, dialogs were built into the core SDK and the stack was handled internally, and child dialogs were loaded with the _Call_ and _Forward_ methods. In v4, you now pass dialogs into bot instances as arguments, providing greater compositional flexibility and developer control of the dialog stack, and child dialogs are loaded with the _BeginDialogAsync_ and _ReplaceDialogAsync_ methods.

- Moreover, v4 provides an `ActivityHandler` class, which helps automate the handling of different types of activities, such as _message_, _conversation update_, and _event_ activities.

These improvements result in changes in syntax for developing bots in .NET, especially around creating bot objects, defining dialogs, and coding event handling logic.

The rest of this topic compares the constructs in the .NET Bot Framework SDK v3 to their equivalent in v4.

## To process incoming messages

### v3

```cs
[BotAuthentication]
public class MessagesController : ApiController
{
    public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
    {
        if (activity.GetActivityType() == ActivityTypes.Message)
        {
            await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
        }

        return Request.CreateResponse(HttpStatusCode.OK);
    }
}
```

### v4

```cs
[Route("api/messages")]
[ApiController]
public class BotController : ControllerBase
{
    private readonly IBotFrameworkHttpAdapter Adapter;
    private readonly IBot Bot;

    public BotController(IBotFrameworkHttpAdapter adapter, IBot bot)
    {
        Adapter = adapter;
        Bot = bot;
    }

    [HttpPost]
    public async Task PostAsync()
    {
        await Adapter.ProcessAsync(Request, Response, Bot);
    }
}
```

## To send a message to a user

### v3

```cs
await context.PostAsync("Hello and welcome to the help desk bot.");
```

### v4

```cs
await turnContext.SendActivityAsync("Hello and welcome to the help desk bot.");
```

## To load a root dialog

### v3

```cs
await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
```

### v4

```cs
// Create a DialogExtensions class with a Run method.
public static class DialogExtensions
{
    public static async Task Run(
        this Dialog dialog,
        ITurnContext turnContext,
        IStatePropertyAccessor<DialogState> accessor,
        CancellationToken cancellationToken)
    {
        var dialogSet = new DialogSet(accessor);
        dialogSet.Add(dialog);

        var dialogContext = await dialogSet.CreateContextAsync(turnContext, cancellationToken);

        var results = await dialogContext.ContinueDialogAsync(cancellationToken);
        if (results.Status == DialogTurnStatus.Empty)
        {
            await dialogContext.BeginDialogAsync(dialog.Id, null, cancellationToken);
        }
    }
}

// Call it from the ActivityHandler's OnMessageActivityAsync override
protected override async Task OnMessageActivityAsync(
    ITurnContext<IMessageActivity> turnContext,
    CancellationToken cancellationToken)
{
    // Run the Dialog with the new message Activity.
    await Dialog.Run(
        turnContext,
        ConversationState.CreateProperty<DialogState>("DialogState"),
        cancellationToken);
}
```

## To start a child dialog

### v3

```cs
context.Call(new NextDialog(), this.ResumeAfterNextDialog);
```

or

```cs
await context.Forward(new NextDialog(), this.ResumeAfterNextDialog, message);
```

### v4

```cs
dialogContext.BeginDialogAsync("<child-dialog-id>", options);
```

or

```cs
dialogContext.ReplaceDialogAsync("<child-dialog-id>", options);
```

## To end a dialog

### v3

```cs
context.Done(ReturnValue);
```

### v4

```cs
await context.EndDialogAsync(ReturnValue);
```

## To prompt a user for input

### v3

```cs
PromptDialog.Choice(
    context,
    this.OnOptionSelected,
    Options, PromptMessage,
    ErrorMessage,
    3,
    PromptStyle.PerLine);
```

### v4

```cs
// In the dialog's constructor, register the prompt, and waterfall steps.
AddDialog(new TextPrompt(nameof(TextPrompt)));
AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
{
    FirstStepAsync,
    SecondStepAsync,
}));

// The initial child Dialog to run.
InitialDialogId = nameof(WaterfallDialog);

// ...

// In the first step, invoke the prompt.
private async Task<DialogTurnResult> FirstStepAsync(
    WaterfallStepContext stepContext,
    CancellationToken cancellationToken)
{
    return await stepContext.PromptAsync(
        nameof(TextPrompt),
        new PromptOptions { Prompt = MessageFactory.Text("Please enter your destination.") },
        cancellationToken);
}

// In the second step, retrieve the Result from the stepContext.
private async Task<DialogTurnResult> SecondStepAsync(
    WaterfallStepContext stepContext,
    CancellationToken cancellationToken)
{
    var destination = (string)stepContext.Result;
}
```

## To save information to dialog state

### v3

All dialogs, and their fields, were auto-serialized in V3.

### v4

```cs
// StepContext values are auto-serialized in V4, and scoped to the dialog.
stepContext.values.destination = destination;
```

## To write changes in state to the persistance layer

### v3

State data is auto-saved by default at the end of the turn.

### v4

```cs
// You now must explicitly save state changes before the end of the turn.
await this.conversationState.saveChanges(context, false);
await this.userState.saveChanges(context, false);
```

## To create and register state storage

### v3

```cs
// Autofac was used internally by the sdk, and state was automatic.
Conversation.UpdateContainer(
builder =>
{
    builder.RegisterModule(new AzureModule(Assembly.GetExecutingAssembly()));
    var store = new InMemoryDataStore();
    builder.Register(c => store)
        .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
        .AsSelf()
        .SingleInstance();
});
```

### v4

```cs
// Create the storage we'll be using for User and Conversation state.
// In-memory storage is great for testing purposes.
services.AddSingleton<IStorage, MemoryStorage>();

// Create the user state (used in this bot's Dialog implementation).
services.AddSingleton<UserState>();

// Create the conversation state (used by the Dialog system itself).
services.AddSingleton<ConversationState>();

// The dialog that will be run by the bot.
services.AddSingleton<MainDialog>();

// Create the bot as a transient. In this case the ASP.NET controller is expecting an IBot.
services.AddTransient<IBot, DialogBot>();

// In the bot's ActivityHandler implementation, call SaveChangesAsync after the OnTurnAsync completes.
public class DialogBot : ActivityHandler
{
    protected readonly Dialog Dialog;
    protected readonly BotState ConversationState;
    protected readonly BotState UserState;

    public DialogBot(ConversationState conversationState, UserState userState, Dialog dialog)
    {
        ConversationState = conversationState;
        UserState = userState;
    }

    public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
    {
        await base.OnTurnAsync(turnContext, cancellationToken);

        // Save any state changes that might have ocurred during the turn.
        await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
        await UserState.SaveChangesAsync(turnContext, false, cancellationToken);
    }

    protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
    {
        // Run the dialog, passing in the message activity for this turn.
        await Dialog.Run(turnContext, ConversationState.CreateProperty<DialogState>("DialogState"), cancellationToken);
    }
}
```

## To catch an error thrown from a dialog

### v3

```cs
// Create a custom IPostToBot implementation to catch exceptions.
public sealed class CustomPostUnhandledExceptionToUser : IPostToBot
{
    private readonly IPostToBot inner;
    private readonly IBotToUser botToUser;
    private readonly ResourceManager resources;
    private readonly System.Diagnostics.TraceListener trace;

    public CustomPostUnhandledExceptionToUser(IPostToBot inner, IBotToUser botToUser, ResourceManager resources, System.Diagnostics.TraceListener trace)
    {
        SetField.NotNull(out this.inner, nameof(inner), inner);
        SetField.NotNull(out this.botToUser, nameof(botToUser), botToUser);
        SetField.NotNull(out this.resources, nameof(resources), resources);
        SetField.NotNull(out this.trace, nameof(trace), trace);
    }

    async Task IPostToBot.PostAsync(IActivity activity, CancellationToken token)
    {
        try
        {
            await this.inner.PostAsync(activity, token);
        }
        catch (Exception ex)
        {
            try
            {
                // Log exception and send custom error message here.
                await this.botToUser.PostAsync("custom error message");
            }
            catch (Exception inner)
            {
                this.trace.WriteLine(inner);
            }

            throw;
        }
    }
}

// Register this using AutoFac, replacing the default PostUnhandledExceptionToUser.
builder
  .RegisterType<CustomPostUnhandledExceptionToUser>()
  .Keyed<IPostToBot>(typeof(PostUnhandledExceptionToUser));
```

### v4

```cs
// Provide an error handler in your implementation of the BotFrameworkHttpAdapter.
public class AdapterWithErrorHandler : BotFrameworkHttpAdapter
{
    public AdapterWithErrorHandler(
        ICredentialProvider credentialProvider,
        ILogger<BotFrameworkHttpAdapter> logger,
        ConversationState conversationState = null)
        : base(credentialProvider)
    {
        OnTurnError = async (turnContext, exception) =>
        {
            // Log any leaked exception from the application.
            logger.LogError($"Exception caught : {exception.Message}");

            // Send a catch-all apology to the user.
            await turnContext.SendActivityAsync("Sorry, it looks like something went wrong.");

            if (conversationState != null)
            {
                try
                {
                    // Delete the conversation state for the current conversation, to prevent the
                    // bot from getting stuck in a error-loop caused by being in a bad state.
                    // Conversation state is similar to "cookie-state" in a web page.
                    await conversationState.DeleteAsync(turnContext);
                }
                catch (Exception e)
                {
                    logger.LogError(
                        $"Exception caught on attempting to Delete ConversationState : {e.Message}");
                }
            }
        };
    }
}
```

## To process different activity types

### v3

```cs
// Within your MessageController, check the message type.
string messageType = activity.GetActivityType();
if (messageType == ActivityTypes.Message)
{
    await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
}
else if (messageType == ActivityTypes.DeleteUserData)
{
}
else if (messageType == ActivityTypes.ConversationUpdate)
{
}
else if (messageType == ActivityTypes.ContactRelationUpdate)
{
}
else if (messageType == ActivityTypes.Typing)
{
}
```

### v4

```cs
// In the bot's ActivityHandler implementation, override relevant methods.

protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
{
    // Handle message activities here.
}

protected override Task OnConversationUpdateActivityAsync(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
{
    // Handle conversation update activities in general here.
}

protected override Task OnEventActivityAsync(ITurnContext<IEventActivity> turnContext, CancellationToken cancellationToken)
{
    // Handle event activities in general here.
}
```

## To log all activities

### v3

[IActivityLogger](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.history.iactivitylogger) was used.

```csharp
builder.RegisterType<ActivityLoggerImplementation>().AsImplementedInterfaces().InstancePerDependency(); 

public class ActivityLoggerImplementation : IActivityLogger
{
    async Task IActivityLogger.LogAsync(IActivity activity)
    {
        // Store the activity.
    }
}
```

### v4

Use [ITranscriptLogger](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.itranscriptlogger).

```csharp
var transcriptMiddleware = new TranscriptLoggerMiddleware(new TranscriptLoggerImplementation(Configuration.GetSection("StorageConnectionString").Value));
adapter.Use(transcriptMiddleware);

public class TranscriptLoggerImplementation : ITranscriptLogger
{
    async Task ITranscriptLogger.LogActivityAsync(IActivity activity)
    {
        // Store the activity.
    }
}
```

## To add bot state storage

The interface for storing _user data_, _conversation data_, and _private conversation data_ has changed.

### v3

State was persisted using an `IBotDataStore` implementation, and injecting it into the dialog state system of the SDK using Autofac.  Microsoft provided `MemoryStorage`, `DocumentDbBotDataStore`, `TableBotDataStore`, and `SqlBotDataStore` classes in [Microsoft.Bot.Builder.Azure](https://github.com/Microsoft/BotBuilder-Azure/).

[IBotDataStore<BotData>](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.dialogs.internals.ibotdatastore-1?view=botbuilder-dotnet-3.0) was used to persist data.

```csharp
Task<bool> FlushAsync(IAddress key, CancellationToken cancellationToken);
Task<T> LoadAsync(IAddress key, BotStoreType botStoreType, CancellationToken cancellationToken);
Task SaveAsync(IAddress key, BotStoreType botStoreType, T data, CancellationToken cancellationToken);
```

```csharp
var dbPath = ConfigurationManager.AppSettings["DocDbPath"];
var dbKey = ConfigurationManager.AppSettings["DocDbKey"];
var docDbUri = new Uri(dbPath);
var storage = new DocumentDbBotDataStore(docDbUri, dbKey);
builder.Register(c => storage)
                .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
                .AsSelf()
                .SingleInstance();
```

### v4

The storage layer uses the `IStorage` interface, specify the storage-layer object when creating each state-management object for your bot, such as `UserState`, `ConversationState`, or `PrivateConversationState`. The state-management object provides keys to the underlying storage layer and also acts as a property manager. For instance, use `IPropertyManager.CreateProperty<T>(string name)` to create a state property accessor.  These property accessors are used to retrieve and store values into and out of the bot's underlying storage.

Use [IStorage](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.istorage?view=botbuilder-dotnet-stable) to persist data.

```csharp
Task DeleteAsync(string[] keys, CancellationToken cancellationToken = default(CancellationToken));
Task<IDictionary<string, object>> ReadAsync(string[] keys, CancellationToken cancellationToken = default(CancellationToken));
Task WriteAsync(IDictionary<string, object> changes, CancellationToken cancellationToken = default(CancellationToken));
```

```csharp
var storageOptions = new CosmosDbStorageOptions()
{
    AuthKey = configuration["cosmosKey"],
    CollectionId = configuration["cosmosCollection"],
    CosmosDBEndpoint = new Uri(configuration["cosmosPath"]),
    DatabaseId = configuration["cosmosDatabase"]
};

IStorage dataStore = new CosmosDbStorage(storageOptions);
var conversationState = new ConversationState(dataStore);
services.AddSingleton(conversationState);

```

## To use Form Flow

### v3

[Microsoft.Bot.Builder.FormFlow](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.formflow?view=botbuilder-dotnet-3.0) was included within the core Bot Builder SDK.

### v4

[Bot.Builder.Community.Dialogs.FormFlow](https://www.nuget.org/packages/Bot.Builder.Community.Dialogs.FormFlow/) is now a Bot Builder Community library.  The source is available on the community [repository](https://github.com/BotBuilderCommunity/botbuilder-community-dotnet/tree/develop/libraries/Bot.Builder.Community.Dialogs.FormFlow).

## To use LuisDialog

### v3

[Microsoft.Bot.Builder.Dialogs.LuisDialog](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.dialogs.luisdialog-1?view=botbuilder-dotnet-3.0) was included within the core Bot Builder SDK.

### v4

[Bot.Builder.Community.Dialogs.Luis](https://www.nuget.org/packages/Bot.Builder.Community.Dialogs.Luis/) is now a Bot Builder Community library.  The source is available on the community [repository](https://github.com/BotBuilderCommunity/botbuilder-community-dotnet/tree/develop/libraries/Bot.Builder.Community.Dialogs.Luis).