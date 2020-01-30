---
title: Differences between the v3 and v4 NodeJS SDK - Bot Service
description: Describes the differences between the v3 and v4 NodeJS SDK.
keywords: bot migration, dialogs, state
author: mmiele
ms.author: v-mimiel
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 11/28/2019
monikerRange: 'azure-bot-service-4.0'
---

# Differences between the v3 and v4 JavaScript SDK

Version 4 of the Bot Framework SDK supports the same underlying Bot Framework Service as version 3. However, v4 is a refactoring of the previous version of the SDK to allow developers more flexibility and control over their bots. Major changes in the SDK include:

- Introduction of a bot adapter
  - It is part of the activity processing stack
  - Handles Bot Framework authentication
  - Manages incoming and outgoing traffic between a channel and your bot's turn handler, encapsulating the calls to the Bot Framework Connector
  - Initializes context for each turn
  - For more information, see [how bots work](../bot-builder-basics.md).
- Refactored state management
  - State data is no longer automatically available within a bot
  - It is now managed via state management objects and property accessors
  - For more information, see [managing state](../bot-builder-concept-state.md)
- A new Dialogs library
  - v3 dialogs must be rewritten for the new dialog library
  - Scoreables no longer exist. You can check for "global" commands, before passing control to your dialogs. Depending on how you design your v4 bot, this could be in the message handler or a parent dialog. For an example, see how to [handle user interruptions](../bot-builder-howto-handle-user-interrupt.md).
  - For more information, see [dialogs library](../bot-builder-concept-dialog.md).

## Activity processing

When you create the adapter for your bot, you also provide a message handler delegate that will receive incoming activities from channels and users. The adapter creates a turn context object for each received activity. It passes the turn context object to the bot's turn handler, and then disposes the object when the turn completes.

The turn handler can receive many types of activities. In general, you will want to forward only _message_ activities to any dialogs your bot contains. If you derive your bot from `ActivityHandler`, the bot's turn handler will forward all message activities to `OnMessage`. Override this method to add message handling logic. For more information about activity types, see the [activity schema](https://github.com/Microsoft/botframework-sdk/blob/master/specs/botframework-activity/botframework-activity.md).

### Handling turns

When handling a message, use the turn context to get information about the incoming activity and to send activities to the user:

|Activity |Description |
|:---|:---|
| To get the incoming activity | Get the turn context's `Activity` property. |
| To create and send an activity to the user | Call the turn context's `SendActivity` method.<br/> For more information, see [send and receive a text message](../../rest-api/bot-framework-rest-direct-line-1-1-receive-messages.md) and [add media to messages](../bot-builder-howto-add-media-attachments.md). |

The `MessageFactory` class provides some helper methods for creating and formating activities.

### Interruptions

Handle these in the bot's message loop. For a description of how to do this with v4 dialogs, see how to [handle user interruptions](../bot-builder-howto-handle-user-interrupt.md).

## State management

In v3, you could store conversation data in the Bot State Service, part of the larger suite of services provided by the Bot Framework. However, this service has been retired since March 31st, 2018. Beginning with v4, the design considerations for managing state are just like any Web App and there are a number of options available. Caching it in memory and in the same process is usually the easiest; however, for production apps you should store state more permanently, such as in an SQL or NoSQL database or as blobs.

v4 doesn't use `UserData`, `ConversationData`, and `PrivateConversationData` properties and data bags to manage state.
​State is now managed via state management objects and property accessors as described in [managing state](../bot-builder-concept-state.md).

v4 defines `UserState`, `ConversationState`, and `PrivateConversationState` classes that manage state data for the bot. You need to create a state property accessor for each property you want to persist, instead of just reading and writing to a predefined data bag.

### Setting up state

State should be configured in the application entry point file, commonly 'index.js' or 'app.js' in NodeJS applications. 

1. Initialize one or more objects that implement the `Storage` interface provided by botbuilder-core. This represents the backing store for your bot's data.
    The v4 SDK provides a few [storage layers](../bot-builder-concept-state.md#storage-layer).
    You can also implement your own, to connect to a different type of store.
1. Then, create and register [state management](../bot-builder-concept-state.md#state-management) objects as necessary.
    You have the same scopes available as in v3, and you can create others if you need to.
1. Finally, create and register [state property accessors](../bot-builder-concept-state.md#state-property-accessors) for the properties your bot needs.
    Within a state management object, each property accessor needs a unique name.

### Using state

Use the state property accessors to get and update your properties, and use the state management objects to write any changes to storage. With the understanding that you should take concurrency issues into account, here is how to accomplish some common tasks.

| Task|Description |
|:---|:---|
| To create a state property accessor | Call your `BotState` object's `createProperty` method. <br/>`BotState` is the abstract base class for conversation, private conversation, and user state. |
| To get the current value of a property | Call `StatePropertyAccessor.get(TurnContext)`.<br/>If no value has been previously set, then it will use the default factory parameter to generate a value. |
| To persist state changes to storage | Call `BotState.saveChanges(TurnContext, boolean)` for any of the state management objects in which state has changed before exiting the turn handler. |

### Managing concurrency

Your bot may need to manage state concurrency. For more information, see the [saving state](../bot-builder-concept-state.md#saving-state) section of **Managing state**, and the [manage concurrency using eTags](../bot-builder-howto-v4-storage.md#manage-concurrency-using-etags) section of **Write directly to storage**.

## Dialogs library

Here are some of the major changes to dialogs:

- The dialogs library is now a separate npm package: **botbuilder-dialogs**.
- Dialog state is managed through a `DialogState` state class and property accessors.
  - The dialog state property is now persisted between turns, as opposed to the dialog object itself.
- Within a turn, you create a dialog context for a _dialog set_.
  - This dialog context encapsulates the dialog stack. This information is persisted within the dialog state property.
- Both versions contain an abstract `Dialog` class, however in v3 it extends the 'ActionSet' class while in v4 it extends 'Object'.

### Defining dialogs

While v3 provided a flexible way to implement dialogs using the `Dialog` class, it meant that you had to implement your own code for features such as validation. In v4, there are now prompt classes that will automatically validate the user input for you, constrain it to a specific type (such as an integer), and prompt the user again until they provide valid input. In general, this means less code to write as a developer.

You have a few options for how to define dialogs now:

|Dialog Type| Description |
|:---|:---|
| A component dialog, derived from the `ComponentDialog` class | Allows you to encapsulate dialog code without naming conflicts with the outer contexts. See [reuse dialogs](../bot-builder-concept-dialog.md).|
| A waterfall dialog, an instance of the `WaterfallDialog` class | Designed to work well with prompt dialogs, which prompt for and validate various types of user input. A waterfall automates most of the process for you, but imposes a certain form to your dialog code; see [sequential conversation flow](../bot-builder-dialog-manage-conversation-flow.md). |
| A custom dialog, derived from the abstract `Dialog` class | This gives you the most flexibility in how your dialogs behave, but also requires you to know more about how the dialog stack is implemented. |

When you create a waterfall dialog, you define the steps of the dialog in the constructor. The order of steps executed follows exactly how you have declared it and automatically moves forward one after another.

You can also create complex control flows by using multiple dialogs; see [advanced conversation flow](../bot-builder-dialog-manage-complex-conversation-flow.md).

To access a dialog, you need to put an instance of it in a _dialog set_ and then generate a _dialog context_ for that set. You need to provide a dialog state property accessor when you create a dialog set. This allows the framework to persist dialog state from one turn to the next. [Managing state](../bot-builder-concept-state.md) describes how state is managed in v4.

### Using dialogs

Here's a list of common operations in v3, and how to accomplish them within a waterfall dialog. Note that each step of a waterfall dialog is expected to return a `DialogTurnResult` value. If it doesn't, the waterfall may end early.

| Operation | v3 | v4 |
|:---|:---|:---|
| Handle the start of your dialog | call `session.beginDialog`, passing in the id of the dialog | call `DialogContext.beginDialog` |
| Send an activity | Call `session.send`. | Call `TurnContext.sendActivity`.<br/>Use the step context's `Context` property to get the turn context (`step.context.sendActivity`).  |
| Wait for a user's response | call a prompt from within the waterfall step, ex: `builder.Prompts.text(session, 'Please enter your destination')`. Retrieve the response in the next step. | Return await `TurnContext.prompt` to begin a prompt dialog. Then retrieve the result in the next step of the waterfall. |
| Handle continuation of your dialog | Automatic | Add additional steps to a waterfall dialog, or implement `Dialog.continueDialog` |
| Signal the end of processing until the user's next message | Call `session.endDialog`. | Return `Dialog.EndOfTurn`. |
| Begin a child dialog | Call `session.beginDialog`. | Return await the step context's `beginDialog` method.<br/>If the child dialog returns a value, that value is available in the next step of the waterfall via the step context's `Result` property. |
| Replace the current dialog with a new dialog | Call `session.replaceDialog`. | Return await `ITurnContext.replaceDialog`. |
| Signal that the current dialog has completed | Call `session.endDialog`. | return await the step context's `endDialog` method. |
| Fail out of a dialog. | Call `session.pruneDialogStack`. | Throw an exception to be caught at another level of the bot, end the step with a status of `Cancelled`, or call the step or dialog context's `cancelAllDialogs`. |

Other notes about the v4 code:

- The various `Prompt` derived classes in v4 implement user prompts as separate, two-step dialogs. See how to [implement sequential conversation flow][sequential-flow].
- Use `DialogSet.createContext` to create a dialog context for the current turn.
- From within a dialog, use the `DialogContext.context` property to get the current turn context.
- Waterfall steps have a `WaterfallStepContext` parameter, which derives from `DialogContext`.
- All concrete dialog and prompt classes derive from the abstract `Dialog` class.
- You assign an ID when you create a component dialog. Each dialog in a dialog set needs to be assigned an ID unique within that set.

### Passing state between and within dialogs

The
[dialog state](../bot-builder-concept-dialog.md#dialog-state),
[waterfall step context properties](../bot-builder-concept-dialog.md#waterfall-step-context-properties), and
[using dialogs](../bot-builder-concept-dialog.md#using-dialogs)
sections of the **dialogs library** article describe how to manage dialog state in v4.

### Get user response

To get the user's activity in a turn, get it from the turn context.

To prompt the user and receive the result of a prompt:

- Add an appropriate prompt instance to your dialog set.
- Call the prompt from a step in a waterfall dialog.
- Retrieve the result from the step context's `Result` property in the following step.

## Additional resources

Please, refer to the following resources for more details and background information.

| Topic | Description |
| :--- | :--- |
|[Migrate a Javascript SDK v3 bot to v4](https://docs.microsoft.com/azure/bot-service/migration/conversion-javascript?view=azure-bot-service-4.0)| Porting v3 JavaScript bot to v4|
| [What's new in Bot Framework](https://docs.microsoft.com/azure/bot-service/what-is-new?view=azure-bot-service-4.0) | Bot Framework and Azure Bot Service key features and improvements|
|[How bots work](../bot-builder-basics.md)|The internal mechanism of a bot|
|[Managing state](../bot-builder-concept-state.md)|Abstractions to make state management easier|
|[Dialogs library](../bot-builder-concept-dialog.md)| Central concepts to manage a conversation|
|[Send and receive text messages](../bot-builder-howto-send-messages.md)|Primary way a bot communicate with users|
|[Send Media](../bot-builder-howto-add-media-attachments.md)|Media attachments, such as images, video, audio, and files| 
|[Sequential conversation flow](../bot-builder-dialog-manage-conversation-flow.md)| Questioning as the main way a bot interacts with users|
|[Save user and conversation data](../bot-builder-howto-v4-state.md)|Tracking a conversation while stateless|
|[Complex Flow](../bot-builder-dialog-manage-complex-conversation-flow.md)|Manage complex conversation flows |
|[Reuse Dialogs](../bot-builder-compositcontrol.md)|Create independent dialogs to handle specific scenarios|
|[Interruptions](../bot-builder-howto-handle-user-interrupt.md)| Handling interruptions to create a robust bot|
|[Activity Schema](https://aka.ms/botSpecs-activitySchema)|Schema for humans and automated software|
