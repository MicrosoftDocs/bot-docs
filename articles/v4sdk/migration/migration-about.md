---
title: Differences between the v3 and v4 SDK | Microsoft Docs
description: Describes the differences between the v3 and v4 SDK.
keywords: bot migration, formflow, dialogs, state
author: JonathanFingold
ms.author: v-jofing
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 05/23/2019
monikerRange: 'azure-bot-service-4.0'
---

# Differences between the v3 and v4 .NET SDK

Version 4 of the Bot Framework SDK supports the same underlying Bot Framework Service as version 3. However, v4 is a refactoring of the previous version of the SDK to allow developers more flexibility and control over their bots. Major changes in the SDK include:

- Introduction of a bot adapter. The adapter is part of the activity processing stack.
  - The adapter handles Bot Framework authentication.
  - The adapter manages incoming and outgoing traffic between a channel and your bot's turn handler, encapsulating the calls to the Bot Framework Connector.
  - The adapter initializes context for each turn.
  - For more details, see [how bots work][about-bots].
- Refactored state management.
  - State data is no longer automatically available within a bot.
  - State is now managed via state management objects and property accessors.
  - For more details, see [managing state][about-state].
- A new Dialogs library.
  - v3 dialogs will need to be rewritten for the new dialog library.
  - Scoreables no longer exist. You can check for "global" commands, before passing control to your dialogs. Depending on how you design your v4 bot, this could be in the message handler or a parent dialog. For an example, see how to [handle user interruptions][interruptions].
  - For more details, see [dialogs library][about-dialogs].
- Support for ASP.NET Core.
  - The templates for creating new C# bots target the ASP.NET Core framework.
  - You can still use ASP.NET for your bots, but our focus for v4 is on supporting the ASP.NET Core framework.
  - See the [introduction to ASP.NET Core](https://docs.microsoft.com/aspnet/core/) for more information about this framework.

## Activity processing

When you create the adapter for your bot, you also provide a message handler delegate that will receive incoming activities from channels and users. The adapter creates a turn context object for each received activity. It passes the turn context object to the bot's turn handler, and then disposes the object when the turn completes.

The turn handler can receive many types of activities. In general, you will want to forward only _message_ activities to any dialogs your bot contains. If you derive your bot from `ActivityHandler`, the bot's turn handler will forward all message activities to `OnMessageActivityAsync`. Override this method to add message handling logic. For detailed information about activity types, see the [activity schema][].

### Handling turns

When handling a message, use the turn context to get information about the incoming activity and to send activities to the user:

| | |
|-|-|
| To get the incoming activity | Get the turn context's `Activity` property. |
| To create and send an activity to the user | Call the turn context's `SendActivityAsync` method.<br/>For more information, see [send and receive a text message][send-messages] and [add media to messages][send-media]. |

The `MessageFactory` class provides some helper methods for creating and formating activities.

### Scorables is gone

Handle these in the bot's message loop. For a description of how to do this with v4 dialogs, see how to [handle user interruptions][interruptions].

Composable scorable dispatch trees and composable chain dialogs, such as _default exception_, are also gone. One way to reproduce this functionality, is to implement it within your bot's turn handler.

## State management

In v3, you could store conversation data in the Bot State Service, part of the larger suite of services provided by the Bot Framework. However, the service has been retired since March 31st, 2018. Beginning with v4, the design considerations on managing state is just like any Web App and there are a number of options available. Caching it in memory and in the same process is usually the easiest; however, for production apps you should store state more permanently, such as in an SQL or NoSQL database or as blobs.

v4 doesn't use `UserData`, `ConversationData`, and `PrivateConversationData` properties and data bags to manage state.
​State is now managed via state management objects and property accessors as described in [managing state][about-state].

v4 defines `UserState`, `ConversationState`, and `PrivateConversationState` classes that manage state data for the bot. You need to create a state property accessor for each property you want to persist, instead of just reading and writing to a predefined data bag.

### Setting up state

State should be configured as singletons where possible, in **Startup.cs** for .NET Core or in **Global.asax.cs** for .NET Framework.

1. Initialize one or more of the `IStorage` objects. This represents the backing store for your bot's data.
    The v4 SDK provides a few [storage layers](../bot-builder-concept-state.md#storage-layer).
    You can also implement your own, to connect to a different type of store.
1. Then, create and register [state management](../bot-builder-concept-state.md#state-management) objects as necessary.
    You have the same scopes available as in v3, and you can create others if you need to.
1. Then, create and register [state property accessors](../bot-builder-concept-state.md#state-property-accessors) for the properties your bot needs.
    Within a state management object, each property accessor needs a unique name.

### Using state

You can use dependency injection to access these whenever your bot is created.
(In ASP.NET, a new instance of your bot or message controller is created for every turn.)
Use the state property accessors to get and update your properties, and use the state management objects to write any changes to storage. With the understanding that you should take concurrency issues into account, here is how to accomplish some common tasks.

| | |
|-|-|
| To create a state property accessor | Call `BotState.CreateProperty<T>`.<br/>`BotState` is the abstract base class for conversation, private conversation, and user state. |
| To get the current value of a property | Call `IStatePropertyAccessor<T>.GetAsync`.<br/>If no value has been previously set, then it will use the default factory parameter to generate a value. |
| To update the current, cached value of a property | Call `IStatePropertyAccessor<T>.SetAsync`.<br/>This only updates the cache, and not the backing storage layer. |
| To persist state changes to storage | Call `BotState.SaveChangesAsync` for any of the state management objects in which state has changed before exiting the turn handler. |

### Managing concurrency

Your bot may need to manage state concurrency. For more information, see the [saving state](../bot-builder-concept-state.md#saving-state) section of **Managing state**, and the [manage concurrency using eTags](../bot-builder-howto-v4-storage.md#manage-concurrency-using-etags) section of **Write directly to storage**.

## Dialogs library

Here are some of the major changes to dialogs:

- The dialogs library is now a separate NuGet package: **Microsoft.Bot.Builder.Dialogs**.
- Dialog classes no longer need to be serializable. Dialog state is managed through a `DialogState` state property accessor.
  - The dialog state property is now persisted between turns, as opposed to the dialog object itself.
- The `IDialogContext` interface is replaced by the `DialogContext` class. Within a turn, you create a dialog context for a _dialog set_.
  - This dialog context encapsulates the dialog stack (the old stack frame). This information is persisted within the dialog state property.
- The `IDialog` interface is replaced by the abstract `Dialog` class.

### Defining dialogs

While v3 provided a flexible way to implement dialogs using the `IDialog` interface, it meant that you had to implement your own code for features such as validation. In v4, there are now prompt classes that will automatically validate the user input for you, constrain it to a specific type (such as an integer), and prompt the user again until they provide valid input. In general, this means less code to write as a developer.

You have a few options for how to define dialogs now:

| | |
|:--|:--|
| A component dialog, derived from the `ComponentDialog` class | Allows you to encapsulate dialog code without naming conflicts with the outer contexts. See [reuse dialogs][reuse-dialogs]. |
| A waterfall dialog, an instance of the `WaterfallDialog` class | Designed to work well with prompt dialogs, which prompt for and validate various types of user input. A waterfall automates most of the process for you, but imposes a certain form to your dialog code; see [sequential conversation flow][sequential-flow]. |
| A custom dialog, derived from the abstract `Dialog` class | This gives you the most flexibility in how your dialogs behave, but also requires you to know more about how the dialog stack is implemented. |

In v3, you used `FormFlow` to perform a set number of steps for a task. In v4, the waterfall dialog replaces FormFlow. When you create a waterfall dialog, you define the steps of the dialog in the constructor. The order of steps executed follows exactly how you have declared it and automatically moves forward one after another.

You can also create complex control flows by using multiple dialogs; see [advanced conversation flow][complex-flow].

To access a dialog, you need to put an instance of it in a _dialog set_ and then generate a _dialog context_ for that set. You need to provide a dialog state property accessor when you create a dialog set. This allows the framework to persist dialog state from one turn to the next. [Managing state][about-state] describes how state is managed in v4.

### Using dialogs

Here's a list of common operations in v3, and how to accomplish them within a waterfall dialog. Note that each step of a waterfall dialog is expected to return a `DialogTurnResult` value. If it doesn't, the waterfall may end early.

| Operation | v3 | v4 |
|:---|:---|:---|
| Handle the start of your dialog | Implement `IDialog.StartAsync` | Make this the first step of a waterfall dialog. |
| Send an activity | Call `IDialogContext.PostAsync`. | Call `ITurnContext.SendActivityAsync`.<br/>Use the step context's `Context` property to get the turn context.  |
| Wait for a user's response | Use an `IAwaitable<IMessageActivity>` parameter and call `IDialogContext.Wait` | Return await `ITurnContext.PromptAsync` to begin a prompt dialog. Then retrieve the result in the next step of the waterfall. |
| Handle continuation of your dialog | Call `IDialogContext.Wait`. | Add additional steps to a waterfall dialog, or implement `Dialog.ContinueDialogAsync` |
| Signal the end of processing until the user's next message | Call `IDialogContext.Wait`. | Return `Dialog.EndOfTurn`. |
| Begin a child dialog | Call `IDialogContext.Call`. | Return await the step context's `BeginDialogAsync` method.<br/>If the child dialog returns a value, that value is available in the next step of the waterfall via the step context's `Result` property. |
| Replace the current dialog with a new dialog | Call `IDialogContext.Forward`. | Return await `ITurnContext.ReplaceDialogAsync`. |
| Signal that the current dialog has completed | Call `IDialogContext.Done`. | return await the step context's `EndDialogAsync` method. |
| Fail out of a dialog. | Call `IDialogContext.Fail`. | Throw an exception to be caught at another level of the bot, end the step with a status of `Cancelled`, or call the step or dialog context's `CancelAllDialogsAsync`.<br/>Note that in v4, exceptions within a dialog are propagated along the C# stack, instead of the dialog stack. |

Other notes about the v4 code:

- The various `Prompt` derived classes in v4 implement user prompts as separate, two-step dialogs. See how to [implement sequential conversation flow][sequential-flow].
- Use `DialogSet.CreateContextAsync` to create a dialog context for the current turn.
- From within a dialog, use the `DialogContext.Context` property to get the current turn context.
- Waterfall steps have a `WaterfallStepContext` parameter, which derives from `DialogContext`.
- All concrete dialog and prompt classes derive from the abstract `Dialog` class.
- You assign an ID when you create a component dialog. Each dialog in a dialog set needs to be assigned an ID unique within that set.

### Passing state between and within dialogs

The
[dialog state](../bot-builder-concept-dialog.md#dialog-state),
[waterfall step context properties](../bot-builder-concept-dialog.md#waterfall-step-context-properties), and
[using dialogs](../bot-builder-concept-dialog.md#using-dialogs)
sections of the **dialogs library** article describe how to manage dialog state in v4.

### IAwaitable is gone

To get the user's activity in a turn, get it from the turn context.

To prompt the user and receive the result of a prompt:

- Add an appropriate prompt instance to your dialog set.
- Call the prompt from a step in a waterfall dialog.
- Retrieve the result from the step context's `Result` property in the following step.

### Formflow

In v3, Formflow was part of the C# SDK, but not part of the JavaScript SDK. It is not part of the v4 SDK, but a community version exists for C#.

| NuGet package name | Community GitHub repo |
|-|-|
| Bot.Builder.Community.Dialogs.Formflow | [BotBuilderCommunity/botbuilder-community-dotnet/libraries/Bot.Builder.Community.Dialogs.FormFlow](https://github.com/BotBuilderCommunity/botbuilder-community-dotnet/tree/master/libraries/Bot.Builder.Community.Dialogs.FormFlow) |

## Additional resources

- [Migrate a .NET SDK v3 bot to v4](conversion-framework.md)

<!-- -->

[about-bots]: ../bot-builder-basics.md
[about-state]: ../bot-builder-concept-state.md
[about-dialogs]: ../bot-builder-concept-dialog.md

[send-messages]: ../bot-builder-howto-send-messages.md
[send-media]: ../bot-builder-howto-add-media-attachments.md

[sequential-flow]: ../bot-builder-dialog-manage-conversation-flow.md
[complex-flow]: ../bot-builder-dialog-manage-complex-conversation-flow.md
[reuse-dialogs]: ../bot-builder-compositcontrol.md
[interruptions]: ../bot-builder-howto-handle-user-interrupt.md

[activity schema]: https://aka.ms/botSpecs-activitySchema