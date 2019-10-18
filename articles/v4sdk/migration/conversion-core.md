---
title: Migrate an existing bot in a new .NET Core project | Microsoft Docs
description: We take an existing .NET v3 bot and migrate it to the .NET v4 SDK, using a new .NET Core project.
keywords: bot migration, formflow, dialogs, v3 bot
author: JonathanFingold
ms.author: v-jofing
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 06/17/2019
monikerRange: 'azure-bot-service-4.0'
---

# Migrate a .NET v3 bot to a .NET Core v4 bot

In this article we'll convert the [v3 ContosoHelpdeskChatBot](https://github.com/microsoft/BotBuilder-Samples/tree/master/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V3) into a v4 bot _in a new .NET Core project_.
This conversion is broken down into these steps:

1. Create the new project using a template.
1. Install additional NuGet packages as necessary.
1. Personalize your bot, update your Startup.cs file, and update your controller class.
1. Update your bot class.
1. Copy over and update your dialogs and models.
1. Final porting step.

The result of this conversion is the [.NET Core v4 ContosoHelpdeskChatBot](https://github.com/microsoft/BotBuilder-Samples/tree/master/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore).
To migrate to a .NET Framework v4 bot _without converting the project type_, see [Migrate a .NET v3 bot to a .NET Framework v4 bot](conversion-framework.md).

Bot Framework SDK v4 is based on the same underlying REST API as SDK v3. However, SDK v4 is a refactoring of the previous version of the SDK to allow developers more flexibility and control over their bots. Major changes in the SDK include:

- State is managed via state management objects and property accessors.
- Setting up turn handler and passing activities to it has changed.
- Scorables no longer exist. You can check for "global" commands in the turn handler, before passing control to your dialogs.
- A new Dialogs library that is very different from the one in the previous version. You'll need to convert old dialogs to the new dialog system using component and waterfall dialogs and the community implementation of Formflow dialogs for v4.

For more information about specific changes, see [differences between the v3 and v4 .NET SDK](migration-about.md).

## Create the new project using a template

Create a new project for your bot.

1. If you haven't done so already, install the Bot Framework SDK v4 [template for C#](https://aka.ms/bot-vsix).
1. Open Visual Studio, and create a new Echo Bot project from the template. Name your project `ContosoHelpdeskChatBot`.

## Install additional NuGet packages

The template installs most of the packages you will need, including the **Microsoft.Bot.Builder** and **Microsoft.Bot.Connector** packages.

1. Add **Bot.Builder.Community.Dialogs.Formflow**

    This is a community library for building v4 dialogs from v3 Formflow definition files. It has **Microsoft.Bot.Builder.Dialogs** as one of its dependencies, so this is also installed for us.

1. Add **log4net** to support logging.

## Personalize your bot

1. Rename your bot file from **Bots\EchoBot.cs** to **Bots\DialogBot.cs** and rename the `EchoBot` class to `DialogBot`.
1. Rename your controller from **Controllers\BotController.cs** to **Controllers\MessagesController.cs** and rename the `BotController` class to `MessagesController`.

## Update your Startup.cs file

The way state and how the bot receives incoming activities has changed. We have to set up parts of the [state management](../bot-builder-concept-state.md) infrastructure ourselves in v4. For instance, v4 uses a bot adapter to handle authentication and forward activities to your bot code, and we have declare our state properties up front.

We'll create a state property for `DialogState`, which we now need for dialog support in v4. We'll use dependency injection to get necessary information to the controller and bot code.

In **Startup.cs**:

1. Update the `using` directives:
    [!code-csharp[Using statements](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Startup.cs?range=4-13)]

1. Remove this constructor:
    ```csharp
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    ```

1. Remove the `Configuration` property.

1. Update the `ConfigureServices` method with this code:
    [!code-csharp[Using statements](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Startup.cs?range=19-41)]

You are going to have compile time errors at this time. We'll fix them in the next steps. 

## MessagesController class
This class handles a request. Dependency Injection will provide the Adapter and IBot implementation at runtime. This template class is unchanged. 

This is where the bot starts a turn in v4, this is quite different form the v3 message controller. Except for the bot's turn handler itself, most of this can be thought of as boilerplate.

The bot turn handler will be defined in the **Bots\DialogBot.cs**.

### Ignore the CancelScorable and GlobalMessageHandlersBotModule classes

Since scorables don't exist in v4 we just ignore these classes. We'll update the turn handler to react to a `cancel` message.

## Update your bot class

In v4, the turn handler or message loop logic is primarily in a bot file. We're deriving from `ActivityHandler`, which defines handlers for common types of activities.

1. Update the **Bots\DialogBots.cs** file.

1. Update the `using` directives:
    [!code-csharp[Using statements](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Bots/DialogBot.cs?range=4-8)]

1. Update `DialogBot` to include a generic parameter for the dialog.
    [!code-csharp[Class definition](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Bots/DialogBot.cs?range=19)]

1. Add these fields and a constructor to initialize them. Again, ASP.NET uses dependency injection to get the parameters values.
    [!code-csharp[Fields and constructor](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Bots/DialogBot.cs?range=21-28)]

1. Update `OnMessageActivityAsync` implementation to invoke our main dialog. (We'll define the `Run` extension method shortly.)
    [!code-csharp[OnMessageActivityAsync](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Bots/DialogBot.cs?range=38-47)]

1. Update `OnTurnAsync` to save our conversation state at the end of the turn. In v4, we have to do this explicitly to write state out to the persistence layer. `ActivityHandler.OnTurnAsync` method calls specific activity handler methods, based on the type of activity received, so we save state after the call to the base method.
    [!code-csharp[OnTurnAsync](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Bots/DialogBot.cs?range=30-36)]

### Create the Run extension method

We're creating an extension method to consolidate the code needed to run a bare component dialog from our bot.

Create a **DialogExtensions.cs** file and implement a `Run` extension method.
[!code-csharp[The extension](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/DialogExtensions.cs?range=4-26)]

## Copy over and convert your dialogs

We'll make many changes to the original v3 dialogs to migrate them to the v4 SDK. Don't worry about the compiler errors for now. These will resolve once we've finished the conversion.
In the interest of not modifying the original code more than necessary, there will remain some compiler warnings after we've finished the migration.

All of our dialogs will derive from `ComponentDialog`, instead of implementing the `IDialog<object>` interface of v3.

This bot has four dialogs that we need to convert:

| | |
|---|---|
| [RootDialog](#update-the-root-dialog) | Presents options and starts the other dialogs. |
| [InstallAppDialog](#update-the-install-app-dialog) | Handles requests to install an app on a machine. |
| [LocalAdminDialog](#update-the-local-admin-dialog) | Handles requests for local machine admin rights. |
| [ResetPasswordDialog](#update-the-reset-password-dialog) | Handles requests to reset your password. |

We won't copy over the `CancelScorable` class, as scorables no longer exist. You can check for _global_ commands in the turn handler, before passing control to your dialogs.

These gather input, but do not perform any of these operations on your machine.

1. Create a **Dialogs** folder in your project.
1. Copy these files from the v3 project's dialogs directory into your new dialogs directory.
    - **InstallAppDialog.cs**
    - **LocalAdminDialog.cs**
    - **ResetPasswordDialog.cs**
    - **RootDialog.cs**

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

- Within dialog code, use the `DialogContext.Context` property to get the current turn context.
- Waterfall steps have a `WaterfallStepContext` parameter, which derives from `DialogContext`.
- All concrete dialog and prompt classes derive from the abstract `Dialog` class.
- You assign an ID when you create a component dialog. Each dialog in a dialog set needs to be assigned an ID unique within that set.

### Update the root dialog

In this bot, the root dialog prompts the user for a choice from a set of options, and then starts a child dialog based on that choice. This then loops for the lifetime of the conversation.

- We can set the main flow up as a waterfall dialog, which is a new concept in the v4 SDK. It will run through a fixed set of steps in order. For more information, see [Implement sequential conversation flow](~/v4sdk/bot-builder-dialog-manage-conversation-flow.md).
- Prompting is now handled through prompt classes, which are short child dialogs that prompt for input, do some minimal processing and validation, and return a value. For more information, see [gather user input using a dialog prompt](~/v4sdk/bot-builder-prompts.md).

In the **Dialogs/RootDialog.cs** file:

1. Update the `using` directives:
    [!code-csharp[Using statements](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/RootDialog.cs?range=4-10)]

1. We need to convert `HelpdeskOptions` options from a list of strings to a list of choices. This will be used with a choice prompt, which will accept the choice number (in the list), the choice value, or any of the choice's synonyms as valid input.
    [!code-csharp[HelpDeskOptions](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/RootDialog.cs?range=28-33)]

1. Add a constructor. This code does the following:
   - Each instance of a dialog is assigned an ID when it is created. The dialog ID is part of the dialog set to which the dialog is being added. Recall that the bot was initialized with a dialog object in the `MessageController` class. Each `ComponentDialog` has its own internal dialog set, with its own set of dialog IDs.
   - It adds the other dialogs, including the choice prompt, as child dialogs. Here, we're just using the class name for each dialog ID.
   - It defines a three-step waterfall dialog. We'll implement those in a moment.
     - The dialog will first prompt the user to choose a task to perform.
     - Then, start the child dialog associated with that choice.
     - And finally, restart itself.
   - Each step of the waterfall is a delegate, and we'll implement those next, taking existing code from the original dialog where we can.
   - When you start a component dialog, it will start its _initial dialog_. By default, this is the first child dialog added to a component dialog. We're explicitly setting the `InitialDialogId` property, which means that the main waterfall dialog does not need to be the first one you add to the set. For instance, if you prefer to add prompts first, this would allow you to do so without causing a run-time issue.
    [!code-csharp[Constructor](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/RootDialog.cs?range=35-49)]

1. We can delete the `StartAsync` method. When a component dialog begins, it automatically starts its _initial_ dialog. In this case, that's the waterfall dialog we defined in the constructor. That also automatically starts at its first step.

1. We will delete the `MessageReceivedAsync` and `ShowOptions` methods, and replace them with the first step of our waterfall. These two methods greeted the user and asked them to choose one of the available options.
   - Here you can see the choice list and the greeting and error messages are provided as options in the call to our choice prompt.
   - We don't have to specify the next method to call in the dialog, as the waterfall will continue to the next step when the choice prompt completes.
   - The choice prompt will loop until it receives valid input or the whole dialog stack is canceled.
    [!code-csharp[PromptForOptionsAsync](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/RootDialog.cs?range=51-65)]

1. We can replace `OnOptionSelected` with the second step of our waterfall. We still start a child dialog based on the user's input.
   - The choice prompt returns a `FoundChoice` value. This shows up in the step context's `Result` property. The dialog stack treats all return values as objects. If the return value is from one of your dialogs, then you know what type of value the object is. See [prompt types](../bot-builder-concept-dialog.md#prompt-types) for a list what each prompt type returns.
   - Since the choice prompt won't throw an exception, we can remove the try-catch block.
   - We need to add a fall through so that this method always returns an appropriate value. This code should never get hit, but if it does it will allow the dialog to "fail gracefully".
    [!code-csharp[ShowChildDialogAsync](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/RootDialog.cs?range=67-102)]

1. Finally, replace the old `ResumeAfterOptionDialog` method with the last step of our waterfall.
    - Instead of ending the dialog and returning the ticket number as we did in the original dialog, we're restarting the waterfall by replacing on the stack the original instance with a new instance of itself. We can do this, since the original app always ignored the return value (the ticket number) and restarted the root dialog.
    [!code-csharp[ResumeAfterAsync](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/RootDialog.cs?range=104-138)]

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

1. Update the `using` directives:
    [!code-csharp[Using statements](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/InstallAppDialog.cs?range=4-11)]

1. Define a constant for the key we'll use to track collected information.
    [!code-csharp[Key ID](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/InstallAppDialog.cs?range=17-18)]

1. Add a constructor and initialize the component's dialog set.
    [!code-csharp[Constructor](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/InstallAppDialog.cs?range=20-33)]

1. We can replace `StartAsync` with the first step of our waterfall.
    - We have to manage state ourselves, so we'll track the install app object in dialog state.
    - The message asking the user for input becomes an option in the call to the prompt.
    [!code-csharp[GetSearchTermAsync](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/InstallAppDialog.cs?range=35-50)]

1. We can replace `appNameAsync` and `multipleAppsAsync` with the second step of our waterfall.
    - We're getting the prompt result now, instead of just looking at the user's last message.
    - The database query and if statements are organized the same as in `appNameAsync`. The code in each block of the if statement has been updated to work with v4 dialogs.
        - If we have one hit, we'll update dialog state and continue with the next step.
        - If we have multiple hits, we'll use our choice prompt to ask the user to choose from the list of options. This means we can just delete `multipleAppsAsync`.
        - If we have no hits, we'll end this dialog and return null to the root dialog.
    [!code-csharp[ResolveAppNameAsync](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/InstallAppDialog.cs?range=52-91)]

1. `appNameAsync` also asked the user for their machine name after it resolved the query. We'll capture that portion of the logic in the next step of the waterfall.
    - Again, in v4 we have to manage state ourselves. The only tricky thing here is that we can get to this step through two different logic branches in the previous step.
    - We'll ask the user for a machine name using the same text prompt as before, just supplying different options this time.
    [!code-csharp[GetMachineNameAsync](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/InstallAppDialog.cs?range=93-114)]

1. The logic from `machineNameAsync` is wrapped up in the final step of our waterfall.
    - We retrieve the machine name from the text prompt result and update dialog state.
    - We are removing the call to update the database, as the supporting code is in a different project.
    - Then we're sending the success message to the user and ending the dialog.
    [!code-csharp[SubmitRequestAsync](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/InstallAppDialog.cs?range=116-135)]

1. To simulate the database call, we mock up `getAppsAsync` to query a static list, instead of the database.
    [!code-csharp[GetAppsAsync](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/InstallAppDialog.cs?range=137-200)]

### Update the local admin dialog

In v3, this dialog greeted the user, started the Formflow dialog, and then saved the result off to a database. This translates easily into a two-step waterfall.

1. Update the `using` directives. Note that this dialog includes a v3 Formflow dialog. In v4 we can use the community Formflow library.
    [!code-csharp[Using statements](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/LocalAdminDialog.cs?range=4-8)]

1. We can remove the instance property for `LocalAdmin`, as the result will be available in dialog state.

1. Add a constructor and initialize the component's dialog set. The Formflow dialog is created in the same way. We're just adding it to the dialog set of our component in the constructor.
    [!code-csharp[Constructor](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/LocalAdminDialog.cs?range=14-23)]

1. We can replace `StartAsync` with the first step of our waterfall. We already created the Formflow in the constructor, and the other two statements translate to this. Note that `FormBuilder` assigns the model's type name as the ID of the generated dialog, which is `LocalAdminPrompt` for this model.
    [!code-csharp[BeginFormflowAsync](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/LocalAdminDialog.cs?range=25-35)]

1. We can replace `ResumeAfterLocalAdminFormDialog` with the second step of our waterfall. We have to get the return value from the step context, instead of from an instance property.
    [!code-csharp[SaveResultAsync](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/LocalAdminDialog.cs?range=37-50)]

1. `BuildLocalAdminForm` remains largely the same, except we don't have the Formflow update the instance property.
    [!code-csharp[BuildLocalAdminForm](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/LocalAdminDialog.cs?range=52-76)]

### Update the reset password dialog

In v3, this dialog greeted the user, authorized the user with a pass code, failed out or started the Formflow dialog, and then reset the password. This still translates well into a waterfall.

1. Update the `using` directives. Note that this dialog includes a v3 Formflow dialog. In v4 we can use the community Formflow library.
    [!code-csharp[Using statements](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/ResetPasswordDialog.cs?range=4-9)]

1. Add a constructor and initialize the component's dialog set. The Formflow dialog is created in the same way. We're just adding it to the dialog set of our component in the constructor.
    [!code-csharp[Constructor](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/ResetPasswordDialog.cs?range=15-25)]

1. We can replace `StartAsync` with the first step of our waterfall. We already created the Formflow in the constructor. Otherwise, we're keeping the same logic, just translating the v3 calls to their v4 equivalents.
    [!code-csharp[BeginFormflowAsync](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/ResetPasswordDialog.cs?range=27-45)]

1. `sendPassCode` is left mainly as an exercise. The original code is commented out, and the method just returns true. Also, we can remove the email address again, as it wasn't used in the original bot.
    [!code-csharp[SendPassCode](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/ResetPasswordDialog.cs?range=47-81)]

1. `BuildResetPasswordForm` has no changes.

1. We can replace `ResumeAfterResetPasswordFormDialog` with the second step of our waterfall, and we'll get the return value from the step context. We've removed the email address that the original dialog didn't do anything with, and we've provided a dummy result instead of querying the database. We're keeping the same logic, just translating the v3 calls to their v4 equivalents.
    [!code-csharp[ProcessRequestAsync](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Dialogs/ResetPasswordDialog.cs?range=90-113)]

## Copy over and update models as necessary
You can use the same v3 models with the v4 community form flow library. 

1. Create a **Models** folder in your project.
1. Copy these files from the v3 project's models directory into your new models directory.
    - **InstallApp.cs**
    - **LocalAdmin.cs**
    - **LocalAdminPrompt.cs**
    - **ResetPassword.cs**
    - **ResetPasswordPrompt.cs**

### Update using directives

We need to update `using` directives in the model classes as shown next.

1. In **InstallApps.cs** change them to this:
    [!code-csharp[Using statements](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Models/InstallApp.cs?range=4-5)]

1. In **LocalAdmin.cs** change them to this:
    [!code-csharp[Using statements](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Models/LocalAdmin.cs?range=4-5)]

1. In **LocalAdminPrompt.cs** change them to this:
    [!code-csharp[Using statements](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Models/LocalAdminPrompt.cs?range=4)]

1. In **ResetPassword.cs** change them to this:
    [!code-csharp[Using statements](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Models/ResetPassword.cs?range=4-5)]
    Also, delete the `using` statements inside the namespace.

1. In **ResetPasswordPrompt.cs** change them to this:
    [!code-csharp[Using statements](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Models/ResetPasswordPrompt.cs?range=4-5)]

### Additional changes

In **ResetPassword.cs** change the return type of the `MobileNumber` as follows:
[!code-csharp[MobileNumber](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/Models/ResetPassword.cs?range=17)]

## Final porting steps 
To complete the porting process, perform these steps:

1. Create an `AdapterWithErrorHandler` class to define an adapter which includes an error handler that can catch exceptions in the middleware or application. The adapter processes and directs incoming activities in through the bot middleware pipeline to your bot’s logic and then back out again. Use the following code to create the class:
 [!code-csharp[MobileNumber](~/../botbuilder-samples/MigrationV3V4/CSharp/ContosoHelpdeskChatBot-V4NetCore/ContosoHelpdeskChatBot/AdapterWithErrorHandler.cs?range=4-46)]
1. Modify the **wwwroot\default.htm** page as you see fit.

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
- [Debug with the emulator](../../bot-service-debug-emulator.md)
- [Add telemetry to your bot](../bot-builder-telemetry.md)
