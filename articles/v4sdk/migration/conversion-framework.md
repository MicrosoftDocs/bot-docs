---
title: Migrate an existing bot within the same .NET Framework project | Microsoft Docs
description: We take an existing v3 bot and migrate it to the v4 SDK, using the same project.
keywords: bot migration, formflow, dialogs, v3 bot
author: JonathanFingold
ms.author: v-jofing
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 02/11/2019
monikerRange: 'azure-bot-service-4.0'
---

# Migrate a .NET SDK v3 bot to v4

In this article we'll convert the v3 [ContosoHelpdeskChatBot](https://github.com/Microsoft/intelligent-apps/tree/master/ContosoHelpdeskChatBot/ContosoHelpdeskChatBot) into a v4 bot _without converting the project type_. It will remain a .NET Framework project.
This conversion is broken down into these steps:

1. Update and install NuGet packages
1. Update your Global.asax.cs file
1. Update your MessagesController class
1. Convert your dialogs

<!--TODO: Link to the converted bot...[ContosoHelpdeskChatBot](https://github.com/EricDahlvang/intelligent-apps/tree/v4netframework/ContosoHelpdeskChatBot).-->

Bot Framework SDK v4 is based on the same underlying REST API as SDK v3. However, SDK v4 is a refactoring of the previous version of the SDK to allow developers more flexibility and control over their bots. Major changes in the SDK include:

- State is managed via state management objects and property accessors.
- Setting up turn handler and passing activities to it has changed.
- Scoreables no longer exist. You can check for "global" commands in the turn handler, before passing control to your dialogs.
- A new Dialogs library that is very different from the one in the previous version. You'll need to convert old dialogs to the new dialog system using component and waterfall dialogs and the community implementation of Formflow dialogs for v4.

For more information about specific changes, see [differences between the v3 and v4 .NET SDK](migration-about.md).

## Update and install NuGet packages

1. Update **Microsoft.Bot.Builder.Azure** to the latest stable version.

    This will update the **Microsoft.Bot.Builder** and **Microsoft.Bot.Connector** packages as well, since they are dependencies.

1. Delete the **Microsoft.Bot.Builder.History** package. This is not part of the v4 SDK.
1. Add **Autofac.WebApi2**

    We'll use this to help with dependency injection in ASP.NET.

1. Add **Bot.Builder.Community.Dialogs.Formflow**

    This is a community library for building v4 dialogs from v3 Formflow definition files. It has **Microsoft.Bot.Builder.Dialogs** as one of its dependencies, so this is also installed for us.

If you build at this point, you will get compiler errors. You can ignore these. Once we're finished with our conversion, we'll have working code.

<!--
## Add a BotDataBag class

This file will contain wrappers to add a v3-style **IBotDataBag** to make dialog conversion simpler.

```csharp
using System.Collections.Generic;

namespace ContosoHelpdeskChatBot
{
    public class BotDataBag : Dictionary<string, object>, IBotDataBag
    {
        public bool RemoveValue(string key)
        {
            return base.Remove(key);
        }

        public void SetValue<T>(string key, T value)
        {
            this[key] = value;
        }

        public bool TryGetValue<T>(string key, out T value)
        {
            if (!ContainsKey(key))
            {
                value = default(T);
                return false;
            }

            value = (T)this[key];

            return true;
        }
    }

    public interface IBotDataBag
    {
        int Count { get; }

        void Clear();

        bool ContainsKey(string key);

        bool RemoveValue(string key);

        void SetValue<T>(string key, T value);

        bool TryGetValue<T>(string key, out T value);
    }
}
```
-->

## Update your Global.asax.cs file

Some of the scaffolding has changed, and we have to set up parts of the [state management](/en-us/azure/bot-service/bot-builder-concept-state?view=azure-bot-service-4.0) infrastructure ourselves in v4. For instance, v4 uses a bot adapter to handle authentication and forward activities to your bot code, and we have declare our state properties up front.

We'll create a state property for `DialogState`, which we now need for dialog support in v4. We'll use dependency injection to get necessary information to the controller and bot code.

In **Global.asax.cs**:

1. Update the `using` statements:
    ```csharp
    using System.Configuration;
    using System.Reflection;
    using System.Web.Http;
    using Autofac;
    using Autofac.Integration.WebApi;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector.Authentication;
    ```
1. Remove these lines from the **Application_Start** method
    ```csharp
    BotConfig.UpdateConversationContainer();
    this.RegisterBotModules();
    ```
    And, insert this line.
    ```csharp
    GlobalConfiguration.Configure(BotConfig.Register);
    ```
1. Remove the **RegisterBotModules** method, which is no longer referenced.
1. Replace the **BotConfig.UpdateConversationContainer** method with this **BotConfig.Register** method, where we'll register objects we need to support dependency injection.
    > [!NOTE]
    > This bot is not using _user_ or _private conversation_ state. The lines to include these are commented out here.
    ```csharp
    public static void Register(HttpConfiguration config)
    {
        ContainerBuilder builder = new ContainerBuilder();
        builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

        SimpleCredentialProvider credentialProvider = new SimpleCredentialProvider(
            ConfigurationManager.AppSettings[MicrosoftAppCredentials.MicrosoftAppIdKey],
            ConfigurationManager.AppSettings[MicrosoftAppCredentials.MicrosoftAppPasswordKey]);

        builder.RegisterInstance(credentialProvider).As<ICredentialProvider>();

        // The Memory Storage used here is for local bot debugging only. When the bot
        // is restarted, everything stored in memory will be gone.
        IStorage dataStore = new MemoryStorage();

        // Create Conversation State object.
        // The Conversation State object is where we persist anything at the conversation-scope.
        ConversationState conversationState = new ConversationState(dataStore);
        builder.RegisterInstance(conversationState).As<ConversationState>();

        //var userState = new UserState(dataStore);
        //var privateConversationState = new PrivateConversationState(dataStore);

        // Create the dialog state property acccessor.
        IStatePropertyAccessor<DialogState> dialogStateAccessor
            = conversationState.CreateProperty<DialogState>(nameof(DialogState));
        builder.RegisterInstance(dialogStateAccessor).As<IStatePropertyAccessor<DialogState>>();

        IContainer container = builder.Build();
        AutofacWebApiDependencyResolver resolver = new AutofacWebApiDependencyResolver(container);
        config.DependencyResolver = resolver;
    }
    ```

## Update your MessagesController class

This is where the turn handler occurs in v4, so this need to change a lot. Except for the turn handler itself, most of this can be thought of as boilerplate. In your **Controllers\MessagesController.cs** file:

1. Update the `using` statements:
    ```csharp
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using Bot.Builder.Community.Dialogs.FormFlow;
    using ContosoHelpdeskChatBot.Dialogs;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Bot.Schema;
    ```
1. Remove the `[BotAuthentication]` attribute from the class. In v4, the bot's adapter will handle authentication.
1. Add these fields. **ConversationState** will manage state scoped to the conversation, and **IStatePropertyAccessor\<DialogState>** is needed to support dialogs in v4.
    ```csharp
    private readonly ConversationState _conversationState;
    private readonly ICredentialProvider _credentialProvider;
    private readonly IStatePropertyAccessor<DialogState> _dialogData;

    private readonly DialogSet _dialogs;
    ```
1. Add a constructor to:
    - Initialize the instance fields.
    - Use the dependency injection in ASP.NET to get the parameters values. (We registered instances of these classes in **Global.asax.cs** to support this.)
    - Create and initialize a dialog set, from which we can create a dialog context. (We need to do this explicitly in v4.)
    ```csharp
    public MessagesController(
        ConversationState conversationState,
        ICredentialProvider credentialProvider,
        IStatePropertyAccessor<DialogState> dialogData)
    {
        _conversationState = conversationState;
        _dialogData = dialogData;
        _credentialProvider = credentialProvider;

        _dialogs = new DialogSet(dialogData);
        _dialogs.Add(new RootDialog(nameof(RootDialog)));
    }
    ```
1. Replace the body of the **Post** method. This is where we'll create our adapter and use it to call our message loop (turn handler). We're using `SaveChangesAsync` at the end of each turn to save any changes the bot made to state.

    ```csharp
    public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
    {

        var botFrameworkAdapter = new BotFrameworkAdapter(_credentialProvider);

        var invokeResponse = await botFrameworkAdapter.ProcessActivityAsync(
            Request.Headers.Authorization?.ToString(),
            activity,
            OnTurnAsync,
            default(CancellationToken));

        if (invokeResponse == null)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        else
        {
            return Request.CreateResponse(invokeResponse.Status);
        }
    }
    ```
1. Add an **OnTurnAsync** method that contains the bot's [turn handler](/en-us/azure/bot-service/bot-builder-dialog-manage-conversation-flow?view=azure-bot-service-4.0#update-the-bot-turn-handler-to-call-the-dialog code.
    > [!NOTE]
    > Scoreables do not exist as such in v4. We check for a `cancel` message from the user in the bot's turn handler, before continuing any active dialog.
    ```csharp
    protected async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken)
    {
        // We're only handling message activities in this bot.
        if (turnContext.Activity.Type == ActivityTypes.Message)
        {
            // Create the dialog context for our dialog set.
            DialogContext dc = await _dialogs.CreateContextAsync(turnContext, cancellationToken);

            // Globally interrupt the dialog stack if the user sent 'cancel'.
            if (turnContext.Activity.Text.Equals("cancel", StringComparison.InvariantCultureIgnoreCase))
            {
                Activity reply = turnContext.Activity.CreateReply($"Ok restarting conversation.");
                await turnContext.SendActivityAsync(reply);
                await dc.CancelAllDialogsAsync();
            }

            try
            {
                // Continue the active dialog, if any. If we just cancelled all dialog, the
                // dialog stack will be empty, and this will return DialogTurnResult.Empty.
                DialogTurnResult dialogResult = await dc.ContinueDialogAsync();
                switch (dialogResult.Status)
                {
                    case DialogTurnStatus.Empty:
                        // There was no active dialog in the dialog stack; start the root dialog.
                        await dc.BeginDialogAsync(nameof(RootDialog));
                        break;

                    case DialogTurnStatus.Complete:
                        // The last dialog on the stack completed and the stack is empty.
                        await dc.EndDialogAsync();
                        break;

                    case DialogTurnStatus.Waiting:
                    case DialogTurnStatus.Cancelled:
                        // The active dialog is waiting for a response from the user, or all
                        // dialogs were cancelled and the stack is empty. In either case, we
                        // don't need to do anything here.
                        break;
                }
            }
            catch (FormCanceledException)
            {
                // One of the dialogs threw an exception to clear the dialog stack.
                await turnContext.SendActivityAsync("Cancelled.");
                await dc.CancelAllDialogsAsync();
                await dc.BeginDialogAsync(nameof(RootDialog));
            }
        }
    }
    ```
1. Since we're only handling _message_ activities, we can delete the **HandleSystemMessage** method.

### Delete the CancelScorable and GlobalMessageHandlersBotModule classes

Since scorables don't exist in v4 and we've updated the turn handler to react to a `cancel` message, we can delete the **CancelScorable** (in **Dialogs\CancelScorable.cs**) and **GlobalMessageHandlersBotModule** classes.

## Convert your dialogs

We'll make many changes to the original dialogs to migrate them to the v4 SDK. Don't worry about the compiler errors for now. These will resolve once we've finished the conversion.
In the interest of not modifying the original code more than necessary, there will remain some compiler warnings after we've finished the migration.

All of our dialogs will derive from `ComponentDialog`, instead of implementing the `IDialog<object>` interface of v3.

This bot has four dialogs that we need to convert:

| | |
|---|---|
| [RootDialog](#update-the-root-dialog) | Presents options and starts the other dialogs. |
| [InstallAppDialog](#update-the-install-app-dialog) | Handles requests to install an app on a machine. |
| [LocalAdminDialog](#update-the-local-admin-dialog) | Handles requests for local machine admin rights. |
| [ResetPasswordDialog](#update-the-reset-password-dialog) | Handles requests to reset your password. |

These gather input, but do not perform any of these operations on your machine.

### Make solution-wide dialog changes

1. For the entire solution, Replace all occurrences of `IDialog<object>` with `ComponentDialog`.
1. For the entire solution, Replace all occurrences of `IDialogContext` with `DialogContext`.
1. For each dialog class, remove the `[Serializable]` attribute.

Control flow and messaging within dialogs are no longer handled the same way, so we'll need to revise this as we convert each dialog.

| Operation | v3 code | v4 code |
| :--- | :--- | :--- |
| Handle the start of your dialog | Implement `IDialog.StartAsync` | Make this the first step of a waterfall dialog, or implement `Dialog.BeginDialogAsync` |
| Handle continuation of your dialog | Call `IDialogContext.Wait` | Add additional steps to a waterfall dialog, or implement `Dialog.ContinueDialogAsync` |
| Send a message to the user | Call `IDialogContext.PostAsync` | Call `ITurnContext.SendActivityAsync` |
| Start a child dialog | Call `IDialogContext.Call` | Call `DialogContext.BeginDialogAsync` |
| Signal that the current dialog has completed | Call `IDialogContext.Done` | Call `DialogContext.EndDialogAsync` |
| Get the user's input | Use an `IAwaitable<IMessageActivity>` parameter | Use a prompt from within a waterfall, or use `ITurnContext.Activity` |

Notes about the v4 code:

- Use the `DialogContext.Context` property to get the current turn context.
- Waterfall steps have a `WaterfallStepContext` parameter, which derives from `DialogContext`.
- All concrete dialog and prompt classes derive from the abstract `Dialog` class.
- You assign an ID when you create a component dialog. Each dialog in a dialog set needs to be assigned an ID unique within that set.

### Update the root dialog

In this bot, the root dialog prompts the user for a choice from a set of options, and then starts a child dialog based on that choice. This then loops for the lifetime of the conversation.

- We can set the main flow up as a waterfall dialog, which is a new concept in the v4 SDK. It will run through a fixed set of steps in order. For more information, see [Implement sequential conversation flow](/en-us/azure/bot-service/bot-builder-prompts?view=azure-bot-service-4.0).
- Prompting is now handled through prompt classes, which are short child dialogs that prompt for input, do some minimal processing and validation, and return a value. For more information, see [gather user input using a dialog prompt](/en-us/azure/bot-service/bot-builder-prompts?view=azure-bot-service-4.0).

In the **Dialogs/RootDialog.cs** file:

1. Update the `using` statements:
    ```csharp
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Dialogs.Choices;
    ```
1. We need to convert `HelpdeskOptions` options from a list of strings to a list of choices. This will be used with a choice prompt, which will accept the choice number (in the list), the choice value, or any of the choice's synonyms as valid input.
    ```csharp
    private static List<Choice> HelpdeskOptions = new List<Choice>()
    {
        new Choice(InstallAppOption) { Synonyms = new List<string>(){ "install" } },
        new Choice(ResetPasswordOption) { Synonyms = new List<string>(){ "password" } },
        new Choice(LocalAdminOption)  { Synonyms = new List<string>(){ "admin" } }
    };
    ```
1. Add a constructor. This code does the following:
   - Each instance of a dialog is assigned an ID when it is created. The dialog ID is part of the dialog set to which the dialog is being added. Recall that the bot has a dialog set that was initialized in the **MessageController** class. Each `ComponentDialog` has its own internal dialog set, with its own set of dialog IDs.
   - It adds the other dialogs, including the choice prompt, as child dialogs. Here, we're just using the class name for each dialog ID.
   - It defines a three-step waterfall dialog. We'll implement those in a moment.
     - The dialog will first prompt the user to choose a task to perform.
     - Then, start the child dialog associated with that choice.
     - And finally, restart itself.
   - Each step of the waterfall is a delegate, and we'll implement those next, taking existing code from the original dialog where we can.
   - When you start a component dialog, it will start its _initial dialog_. By default, this is the first child dialog added to a component dialog. To assign a different child as the initial dialog, you would manually set the component's `InitialDialogId` property.
    ```csharp
    public RootDialog(string id)
        : base(id)
    {
        AddDialog(new WaterfallDialog("choiceswaterfall", new WaterfallStep[]
            {
                PromptForOptionsAsync,
                ShowChildDialogAsync,
                ResumeAfterAsync,
            }));
        AddDialog(new InstallAppDialog(nameof(InstallAppDialog)));
        AddDialog(new LocalAdminDialog(nameof(LocalAdminDialog)));
        AddDialog(new ResetPasswordDialog(nameof(ResetPasswordDialog)));
        AddDialog(new ChoicePrompt("options"));
    }
    ```
1. We can delete the **StartAsync** method. When a component dialog begins, it automatically starts its _initial_ dialog. In this case, that's the waterfall dialog we defined in the constructor. That also automatically starts at its first step.
1. We will delete the **MessageReceivedAsync** and **ShowOptions** methods, and replace them with the first step of our waterfall. These two methods greeted the user and asked them to choose one of the available options.
   - Here you can see the choice list and the greeting and error messages are provided as options in the call to our choice prompt.
   - We don't have to specify the next method to call in the dialog, as the waterfall will continue to the next step when the choice prompt completes.
   - The choice prompt will loop until it receives valid input or the whole dialog stack is canceled.
    ```csharp
    private async Task<DialogTurnResult> PromptForOptionsAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        // Prompt the user for a response using our choice prompt.
        return await stepContext.PromptAsync(
            "options",
            new PromptOptions()
            {
                Choices = HelpdeskOptions,
                Prompt = MessageFactory.Text(GreetMessage),
                RetryPrompt = MessageFactory.Text(ErrorMessage)
            },
            cancellationToken);
    }
    ```
1. We can replace **OnOptionSelected** with the second step of our waterfall. We still start a child dialog based on the user's input.
   - The choice prompt returns a `FoundChoice` value. This shows up in the step context's `Result` property. The dialog stack treats all return values as objects. If the return value is from one of your dialogs, then you know what type of value the object is. See [prompt types](/articles/v4sdk/bot-builder-concept-dialog.md#prompt-types) for a list what each prompt type returns.
   - Since the choice prompt won't throw an exception, can remove the try-catch block.
   - We need to add a fall through so that this method always returns an appropriate value. This code should never get hit, but if it does it will allow the dialog to "fail gracefully".
    ```csharp
    private async Task<DialogTurnResult> ShowChildDialogAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        // string optionSelected = await userReply;
        string optionSelected = (stepContext.Result as FoundChoice).Value;

        switch (optionSelected)
        {
            case InstallAppOption:
                //context.Call(new InstallAppDialog(), this.ResumeAfterOptionDialog);
                //break;
                return await stepContext.BeginDialogAsync(
                    nameof(InstallAppDialog),
                    cancellationToken);
            case ResetPasswordOption:
                //context.Call(new ResetPasswordDialog(), this.ResumeAfterOptionDialog);
                //break;
                return await stepContext.BeginDialogAsync(
                    nameof(ResetPasswordDialog),
                    cancellationToken);
            case LocalAdminOption:
                //context.Call(new LocalAdminDialog(), this.ResumeAfterOptionDialog);
                //break;
                return await stepContext.BeginDialogAsync(
                    nameof(LocalAdminDialog),
                    cancellationToken);
        }

        // We shouldn't get here, but fail gracefully if we do.
        await stepContext.Context.SendActivityAsync(
            "I don't recognize that option.",
            cancellationToken: cancellationToken);
        // Continue through to the next step without starting a child dialog.
        return await stepContext.NextAsync(cancellationToken: cancellationToken);
    }
    ```
1. Finally, replace the old **ResumeAfterOptionDialog** method with the last step of our waterfall.
    - Instead of ending the dialog and returning the ticket number as we did in the original dialog, we're restarting the waterfall by replacing on the stack the original instance with a new instance of itself. We can do this, since the original app always ignored the return value (the ticket number) and restarted the root dialog.
    ```csharp
    private async Task<DialogTurnResult> ResumeAfterAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        try
        {
            //var message = await userReply;
            var message = stepContext.Context.Activity;

            int ticketNumber = new Random().Next(0, 20000);
            //await context.PostAsync($"Thank you for using the Helpdesk Bot. Your ticket number is {ticketNumber}.");
            await stepContext.Context.SendActivityAsync(
                $"Thank you for using the Helpdesk Bot. Your ticket number is {ticketNumber}.",
                cancellationToken: cancellationToken);

            //context.Done(ticketNumber);
        }
        catch (Exception ex)
        {
            // await context.PostAsync($"Failed with message: {ex.Message}");
            await stepContext.Context.SendActivityAsync(
                $"Failed with message: {ex.Message}",
                cancellationToken: cancellationToken);

            // In general resume from task after calling a child dialog is a good place to handle exceptions
            // try catch will capture exceptions from the bot framework awaitable object which is essentially "userReply"
            logger.Error(ex);
        }

        // Replace on the stack the current instance of the waterfall with a new instance,
        // and start from the top.
        return await stepContext.ReplaceDialogAsync(
            "choiceswaterfall",
            cancellationToken: cancellationToken);
    }
    ```

### Update the install app dialog

The install app dialog performs a few logical tasks, which we'll set up as a 4-step waterfall dialog. How you factor existing code into waterfall steps is a logical exercise for each dialog. For each step, the original method the code came from is noted.

1. Asks the user for a search string.
1. Queries a database for potential matches.
   - If there is one hit, select this and continue.
   - If there are multiple hits, it asks the user to choose one.
   - If there are no hits, the dialog exits.
1. Asks the user for a machine to install the app on.
1. Writes the information to a database and sends a confirmation message.

In the **Dialogs/InstallAppDialog.cs** file:

1. Update the `using` statements:
    ```csharp
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using ContosoHelpdeskChatBot.Models;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Dialogs.Choices;
    ```
1. Define constants for the IDs we'll use for the prompts and dialogs. This makes the dialog code easier to maintain, as the string to use is defined in one spot.
    ```csharp
    // Set up our dialog and prompt IDs as constants.
    private const string MainId = "mainDialog";
    private const string TextId = "textPrompt";
    private const string ChoiceId = "choicePrompt";
    ```
1. Define constants for the keys we'll use to track dialog state.
    ```csharp
    // Set up keys for managing collected information.
    private const string InstallInfo = "installInfo";
    ```
1. Add a constructor and initialize the component's dialog set. We're explicitly setting the `InitialDialogId` property this time, which means that the main waterfall dialog does not need to be the first one you add to the set. For instance, if you prefer to add prompts first, this would allow you to do so without causing a run-time issue.
    ```csharp
    public InstallAppDialog(string id)
        : base(id)
    {
        // Initialize our dialogs and prompts.
        InitialDialogId = MainId;
        AddDialog(new WaterfallDialog(MainId, new WaterfallStep[] {
            GetSearchTermAsync,
            ResolveAppNameAsync,
            GetMachineNameAsync,
            SubmitRequestAsync,
        }));
        AddDialog(new TextPrompt(TextId));
        AddDialog(new ChoicePrompt(ChoiceId));
    }
    ```
1. We can replace **StartAsync** with the first step of our waterfall.
    - We have to manage state ourselves, so we'll track the install app object in dialog state.
    - The message asking the user for input becomes an option in the call to the prompt.
    ```csharp
    private async Task<DialogTurnResult> GetSearchTermAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        // Create an object in dialog state in which to track our collected information.
        stepContext.Values[InstallInfo] = new InstallApp();

        // Ask for the search term.
        return await stepContext.PromptAsync(
            TextId,
            new PromptOptions
            {
                Prompt = MessageFactory.Text("Ok let's get started. What is the name of the application? "),
            },
            cancellationToken);
    }
    ```
1. We can replace **appNameAsync** and **multipleAppsAsync** with the second step of our waterfall.
    - We're getting the prompt result now, instead of just looking at the user's last message.
    - The database query and if statements are organized the same as in **appNameAsync**. The code in each block of the if statement has been updated to work with v4 dialogs.
        - If we have one hit, we'll update dialog state and continue with the next step.
        - If we have multiple hits, we'll use our choice prompt to ask the user to choose from the list of options. This means we can just delete **multipleAppsAsync**.
        - If we have no hits, we'll end this dialog and return null to the root dialog.
    ```csharp
    private async Task<DialogTurnResult> ResolveAppNameAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        // Get the result from the text prompt.
        var appname = stepContext.Result as string;

        // Query the database for matches.
        var names = await this.getAppsAsync(appname);

        if (names.Count == 1)
        {
            // Get our tracking information from dialog state and add the app name.
            var install = stepContext.Values[InstallInfo] as InstallApp;
            install.AppName = names.First();

            return await stepContext.NextAsync();
        }
        else if (names.Count > 1)
        {
            // Ask the user to choose from the list of matches.
            return await stepContext.PromptAsync(
                ChoiceId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("I found the following applications. Please choose one:"),
                    Choices = ChoiceFactory.ToChoices(names),
                },
                cancellationToken);
        }
        else
        {
            // If no matches, exit this dialog.
            await stepContext.Context.SendActivityAsync(
                $"Sorry, I did not find any application with the name '{appname}'.",
                cancellationToken: cancellationToken);

            return await stepContext.EndDialogAsync(null, cancellationToken);
        }
    }
    ```
1. **appNameAsync** also asked the user for their machine name after it resolved the query. We'll capture that portion of the logic in the next step of the waterfall.
    - Again, in v4 we have to manage state ourselves. The only tricky thing here is that we can get to this step through two different logic branches in the previous step.
    - We'll ask the user for a machine name using the same text prompt as before, just supplying different options this time.
    ```csharp
    private async Task<DialogTurnResult> GetMachineNameAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        // Get the tracking info. If we don't already have an app name,
        // Then we used the choice prompt to get it in the previous step.
        var install = stepContext.Values[InstallInfo] as InstallApp;
        if (install.AppName is null)
        {
            install.AppName = (stepContext.Result as FoundChoice).Value;
        }

        // We now need the machine name, so prompt for it.
        return await stepContext.PromptAsync(
            TextId,
            new PromptOptions
            {
                Prompt = MessageFactory.Text(
                    $"Found {install.AppName}. What is the name of the machine to install application?"),
            },
            cancellationToken);
    }
    ```
1. The logic from **machineNameAsync** is wrapped up in the final step of our waterfall.
    - We retrieve the machine name from the text prompt result and update dialog state.
    - We are removing the call to update the database, as the supporting code is in a different project.
    - Then we're sending the success message to the user and ending the dialog.
    ```csharp
    private async Task<DialogTurnResult> SubmitRequestAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        if (stepContext.Reason != DialogReason.CancelCalled)
        {
            // Get the tracking info and add the machine name.
            var install = stepContext.Values[InstallInfo] as InstallApp;
            install.MachineName = stepContext.Context.Activity.Text;

            //TODO: Save to this information to the database.
        }

        await stepContext.Context.SendActivityAsync(
            $"Great, your request to install {install.AppName} on {install.MachineName} has been scheduled.",
            cancellationToken: cancellationToken);

        return await stepContext.EndDialogAsync(null, cancellationToken);
    }
    ```
1. To simulate the database, I've updated **getAppsAsync** to query a static list, instead of the database.
    ```csharp
    private async Task<List<string>> getAppsAsync(string Name)
    {
        List<string> names = new List<string>();

        // Simulate querying the database for applications that match.
        return (from app in AppMsis
            where app.ToLower().Contains(Name.ToLower())
            select app).ToList();
    }

    // Example list of app names in the database.
    private static readonly List<string> AppMsis = new List<string>
    {
        "µTorrent 3.5.0.44178",
        "7-Zip 17.1",
        "Ad-Aware 9.0",
        "Adobe AIR 2.5.1.17730",
        "Adobe Flash Player (IE) 28.0.0.105",
        "Adobe Flash Player (Non-IE) 27.0.0.130",
        "Adobe Reader 11.0.14",
        "Adobe Shockwave Player 12.3.1.201",
        "Advanced SystemCare Personal 11.0.3",
        "Auslogics Disk Defrag 3.6",
        "avast! 4 Home Edition 4.8.1351",
        "AVG Anti-Virus Free Edition 9.0.0.698",
        "Bonjour 3.1.0.1",
        "CCleaner 5.24.5839",
        "Chmod Calculator 20132.4",
        "CyberLink PowerDVD 17.0.2101.62",
        "DAEMON Tools Lite 4.46.1.328",
        "FileZilla Client 3.5",
        "Firefox 57.0",
        "Foxit Reader 4.1.1.805",
        "Google Chrome 66.143.49260",
        "Google Earth 7.3.0.3832",
        "Google Toolbar (IE) 7.5.8231.2252",
        "GSpot 2701.0",
        "Internet Explorer 903235.0",
        "iTunes 12.7.0.166",
        "Java Runtime Environment 6 Update 17",
        "K-Lite Codec Pack 12.1",
        "Malwarebytes Anti-Malware 2.2.1.1043",
        "Media Player Classic 6.4.9.0",
        "Microsoft Silverlight 5.1.50907",
        "Mozilla Thunderbird 57.0",
        "Nero Burning ROM 19.1.1005",
        "OpenOffice.org 3.1.1 Build 9420",
        "Opera 12.18.1873",
        "Paint.NET 4.0.19",
        "Picasa 3.9.141.259",
        "QuickTime 7.79.80.95",
        "RealPlayer SP 12.0.0.319",
        "Revo Uninstaller 1.95",
        "Skype 7.40.151",
        "Spybot - Search & Destroy 1.6.2.46",
        "SpywareBlaster 4.6",
        "TuneUp Utilities 2009 14.0.1000.353",
        "Unlocker 1.9.2",
        "VLC media player 1.1.6",
        "Winamp 5.56 Build 2512",
        "Windows Live Messenger 2009 16.4.3528.331",
        "WinPatrol 2010 31.0.2014",
        "WinRAR 5.0",
    };
    ```

### Update the local admin dialog

In v3, this dialog greeted the user, started the Formflow dialog, and then saved the result off to a database. This translates easily into a two-step waterfall.

1. Update the `using` statements. Note that this dialog includes a v3 Formflow dialog. In v4 we can use the community Formflow library.
    ```csharp
    using System.Threading;
    using System.Threading.Tasks;
    using Bot.Builder.Community.Dialogs.FormFlow;
    using ContosoHelpdeskChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    ```
1. We can remove the instance property for `LocalAdmin`, as the result will be available in dialog state.
1. Define constants for the IDs we'll use for the dialogs. In the community library, the constructed dialog ID is always set to the name of the class the dialog produces.
    ```csharp
    // Set up our dialog and prompt IDs as constants.
    private const string MainDialog = "mainDialog";
    private static string AdminDialog { get; } = nameof(LocalAdminPrompt);
    ```
1. Add a constructor and initialize the component's dialog set. The Formflow dialog is created in the same way. We're just adding it to the dialog set of our component in the constructor.
    ```csharp
    public LocalAdminDialog(string dialogId) : base(dialogId)
    {
        InitialDialogId = MainDialog;

        AddDialog(new WaterfallDialog(MainDialog, new WaterfallStep[]
        {
            BeginFormflowAsync,
            SaveResultAsync,
        }));
        AddDialog(FormDialog.FromForm(BuildLocalAdminForm, FormOptions.PromptInStart));
    }
    ```
1. We can replace **StartAsync** with the first step of our waterfall. We already created the Formflow in the constructor, and the other two statements translate to this.
    ```csharp
    private async Task<DialogTurnResult> BeginFormflowAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await stepContext.Context.SendActivityAsync("Great I will help you request local machine admin.");

        // Begin the Formflow dialog.
        return await stepContext.BeginDialogAsync(AdminDialog, cancellationToken: cancellationToken);
    }
    ```
1. We can replace **ResumeAfterLocalAdminFormDialog** with the second step of our waterfall. We have to get the return value from the step context, instead of from an instance property.
    ```csharp
    private async Task<DialogTurnResult> SaveResultAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        // Get the result from the Formflow dialog when it ends.
        if (stepContext.Reason != DialogReason.CancelCalled)
        {
            var admin = stepContext.Result as LocalAdminPrompt;

            //TODO: Save to this information to the database.
        }

        return await stepContext.EndDialogAsync(null, cancellationToken);
    }
    ```
1. **BuildLocalAdminForm** remains largely the same, except we don't have the Formflow update the instance property.
    ```csharp
    // Nearly the same as before.
    private IForm<LocalAdminPrompt> BuildLocalAdminForm()
    {
        //here's an example of how validation can be used in form builder
        return new FormBuilder<LocalAdminPrompt>()
            .Field(nameof(LocalAdminPrompt.MachineName),
            validate: async (state, value) =>
            {
                ValidateResult result = new ValidateResult { IsValid = true, Value = value };
                //add validation here

                //this.admin.MachineName = (string)value;
                return result;
            })
            .Field(nameof(LocalAdminPrompt.AdminDuration),
            validate: async (state, value) =>
            {
                ValidateResult result = new ValidateResult { IsValid = true, Value = value };
                //add validation here

                //this.admin.AdminDuration = Convert.ToInt32((long)value) as int?;
                return result;
            })
            .Build();
    }
    ```

### Update the reset password dialog

In v3, this dialog greeted the user, authorized the user with a pass code, failed out or started the Formflow dialog, and then reset the password. This still translates well into a waterfall.

1. Update the `using` statements. Note that this dialog includes a v3 Formflow dialog. In v4 we can use the community Formflow library.
    ```csharp
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Bot.Builder.Community.Dialogs.FormFlow;
    using ContosoHelpdeskChatBot.Models;
    using Microsoft.Bot.Builder.Dialogs;
    ```
1. Define constants for the IDs we'll use for the dialogs. In the community library, the constructed dialog ID is always set to the name of the class the dialog produces.
    ```csharp
    // Set up our dialog and prompt IDs as constants.
    private const string MainDialog = "mainDialog";
    private static string ResetDialog { get; } = nameof(ResetPasswordPrompt);
    ```
1. Add a constructor and initialize the component's dialog set. The Formflow dialog is created in the same way. We're just adding it to the dialog set of our component in the constructor.
    ```csharp
    public ResetPasswordDialog(string dialogId) : base(dialogId)
    {
        InitialDialogId = MainDialog;

        AddDialog(new WaterfallDialog(MainDialog, new WaterfallStep[]
        {
            BeginFormflowAsync,
            ProcessRequestAsync,
        }));
        AddDialog(FormDialog.FromForm(BuildResetPasswordForm, FormOptions.PromptInStart));
    }
    ```
1. We can replace **StartAsync** with the first step of our waterfall. We already created the Formflow in the constructor. Otherwise, we're keeping the same logic, just translating the v3 calls to their v4 equivalents.
    ```csharp
    private async Task<DialogTurnResult> BeginFormflowAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        await stepContext.Context.SendActivityAsync("Alright I will help you create a temp password.");

        // Check the passcode and fail out or begin the Formflow dialog.
        if (sendPassCode(stepContext))
        {
            return await stepContext.BeginDialogAsync(ResetDialog, cancellationToken: cancellationToken);
        }
        else
        {
            //here we can simply fail the current dialog because we have root dialog handling all exceptions
            throw new Exception("Failed to send SMS. Make sure email & phone number has been added to database.");
        }
    }
    ```
1. **sendPassCode** is left mainly as an exercise. The original code is commented out, and the method just returns true. Also, we can remove the email address again, as it wasn't used in the original bot.
    ```csharp
    private bool sendPassCode(DialogContext context)
    {
        //bool result = false;

        //Recipient Id varies depending on channel
        //refer ChannelAccount class https://docs.botframework.com/en-us/csharp/builder/sdkreference/dd/def/class_microsoft_1_1_bot_1_1_connector_1_1_channel_account.html#a0b89cf01fdd73cbc00a524dce9e2ad1a
        //as well as Activity class https://docs.botframework.com/en-us/csharp/builder/sdkreference/dc/d2f/class_microsoft_1_1_bot_1_1_connector_1_1_activity.html
        //int passcode = new Random().Next(1000, 9999);
        //Int64? smsNumber = 0;
        //string smsMessage = "Your Contoso Pass Code is ";
        //string countryDialPrefix = "+1";

        // TODO: save PassCode to database
        //using (var db = new ContosoHelpdeskContext())
        //{
        //    var reset = db.ResetPasswords.Where(r => r.EmailAddress == email).ToList();
        //    if (reset.Count >= 1)
        //    {
        //        reset.First().PassCode = passcode;
        //        smsNumber = reset.First().MobileNumber;
        //        result = true;
        //    }

        //    db.SaveChanges();
        //}

        // TODO: send passcode to user via SMS.
        //if (result)
        //{
        //    result = Helper.SendSms($"{countryDialPrefix}{smsNumber.ToString()}", $"{smsMessage} {passcode}");
        //}

        //return result;
        return true;
    }
    ```
1. **BuildResetPasswordForm** has no changes.
1. We can replace **ResumeAfterLocalAdminFormDialog** with the second step of our waterfall, and we'll get the return value from the step context. We've removed the email address that the original dialog didn't do anything with, and we've provided a dummy result instead of querying the database. We're keeping the same logic, just translating the v3 calls to their v4 equivalents.
    ```csharp
    private async Task<DialogTurnResult> ProcessRequestAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        // Get the result from the Formflow dialog when it ends.
        if (stepContext.Reason != DialogReason.CancelCalled)
        {
            var prompt = stepContext.Result as ResetPasswordPrompt;
            int? passcode;

            // TODO: Retrieve the passcode from the database.
            passcode = 1111;

            if (prompt.PassCode == passcode)
            {
                string temppwd = "TempPwd" + new Random().Next(0, 5000);
                await stepContext.Context.SendActivityAsync(
                    $"Your temp password is {temppwd}",
                    cancellationToken: cancellationToken);
            }
        }

        return await stepContext.EndDialogAsync(null, cancellationToken);
    }
    ```

### Update models as necessary

We need to update `using` statements in some of the models that reference the Formflow library.

1. In `LocalAdminPrompt`, change them to this:
    ```csharp
    using Bot.Builder.Community.Dialogs.FormFlow;
    ```
1. In `ResetPasswordPrompt`, change them to this:
    ```csharp
    using Bot.Builder.Community.Dialogs.FormFlow;
    using System;
    ```

## Update Web.config

Comment out the configuration keys for **MicrosoftAppId** and **MicrosoftAppPassword**. This will allow you to debug the bot locally without needing to provide these values to the Emulator.

## Run and test your bot in the Emulator

At this point, we should be able to run the bot locally in IIS and attach to it with the Emulator.

1. Run the bot in IIS.
1. Start the emulator and connect to the bot's endpoint (for example, **http://localhost:3978/api/messages**).
    - If this is the first time you are running the bot then click **File > New Bot** and follow the instructions on screen. Otherwise, click **File > Open Bot** to open an existing bot.
    - Double check your port settings in the configuration. For example, if the bot opened in your browser to `http://localhost:3979/`, then in the Emulator, set the bot's endpoint to `http://localhost:3979/api/messages`.
1. All four dialogs should work, and you can set breakpoints in the waterfall steps to check what the dialog context and dialog state is at these points.

## Additional resources

v4 conceptual topics:

- [How bots work](../bot-builder-basics.md)
- [Managing state](../bot-builder-concept-state.md)
- [Dialogs library](../bot-builder-concept-dialog.md)

v4 how-to topics:

- [Send and receive text messages](../bot-builder-howto-send-messages.md)
- [Save user and conversation data](../bot-builder-howto-v4-state.md)
- [Implement sequential conversation flow](../bot-builder-dialog-manage-conversation-flow.md)
- [Gather user input using a dialog prompt](../bot-builder-prompts.md)

<!-- TODO:
- The conceptual piece
- The migration to a .NET Core project
-->
