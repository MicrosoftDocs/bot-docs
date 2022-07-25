---
title: Adaptive dialog events and triggers in Bot Framework SDK
description: Describes the adaptive dialog prebuilt triggers. Triggers handle dialog specific events that are related to the lifecycle of the dialog.
keywords: bot, triggers, adaptive dialogs
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: reference
ms.date: 09/27/2021
monikerRange: 'azure-bot-service-4.0'
---

# Events and triggers in adaptive dialogs - reference guide

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

For an introduction to this topic, see the [Triggers](/composer/concept-events-and-triggers) concept article in the Composer documentation.

## Recognizer event triggers

| Event cause               | Trigger name  | Base event    | Description                                                       |
| ------------------------- | ------------- | ------------- | ----------------------------------------------------------------- |
| Choose Intent | `OnChooseIntent` |`ChooseIntent` | This trigger is run when ambiguity has been detected between intents from multiple recognizers in a [CrossTrainedRecognizerSet][recognizers-cross-trained-recognizer-set].|
| Intent recognized| `OnIntent` | `RecognizedIntent` | Actions to perform when specified intent is recognized.           |
|QnAMatch intent|`OnQnAMatch`| `RecognizedIntent` |This trigger is run when the [QnAMakerRecognizer][qna-maker-recognizer] has returned a `QnAMatch` intent. The entity `@answer` will have the `QnAMaker` answer.|
|Unknown intent recognized| `OnUnknownIntent` | `UnknownIntent` | Actions to perform when user input is unrecognized or no match is found in any of the `OnIntent` triggers. You can also use this as your first trigger in your root dialog in place of the `OnBeginDialog` to preform any needed tasks when the dialog first starts. |

The `OnIntent` trigger lets you handle the `recognizedIntent` event. The `recognizedIntent` event is raised by a recognizer. With the exception of the [QnA Maker recognizer][qna-maker-recognizer], all of the Bot Framework SDK built-in recognizers emit this event when they successfully identify a user _input_ so that your bot can respond appropriately.

Use the `OnUnknownIntent` trigger to catch and respond when a `recognizedIntent` event isn't caught and handled by any of the other triggers. This means that any unhandled intent (including "none") can cause it to trigger, but only if there aren't any currently executing actions for the dialog. Use the `OnUnknownIntent` trigger to catch and respond when a "none" intent occurs. Using the `OnIntent` trigger to handle a "none" intent can produce unexpected results.

<!--Use the `OnUnknownIntent` trigger to catch and respond when a "none" intent occurs. This is especially helpful to capture and handle cases where your dialog wishes to participate in consultation.-->

## Dialog event triggers

Dialog triggers handle dialog specific events that are related to the _lifecycle_ of the dialog.  There are currently 6 dialog triggers in the Bot Framework SDK and they all derive from the `OnDialogEvent` class.

> [!TIP]
> These aren't like normal interruption event handlers where the child's actions will continue running after the handler's actions complete. For all of the events below, the bot will run a new set of actions and will end the turn once those actions have finished.

| Trigger name     | Base event   | Description                                                                                                                         |
| ---------------- | ------------ | ----------------------------------------------------------------------------------------------------------------------------------- |
| `OnBeginDialog`    | `BeginDialog`  | Actions to perform when this dialog begins. For use with child dialogs only, not to be used in your root dialog, In root dialogs, use `OnUnknownIntent` to perform dialog initialization activities.|
| `OnCancelDialog`   | `CancelDialog` | This event allows you to prevent the current dialog from being cancelled due to a child dialog executing a `CancelAllDialogs` action. |
| `OnEndOfActions`   | `EndOfActions` | This event occurs once all actions and ambiguity events have been processed.                                                        |
| `OnError`          | `Error`        | Actions to perform when an `Error` dialog event occurs. This event is similar to `OnCancelDialog` in that you are preventing the adaptive dialog that contains this trigger from ending, in this case due to an error in a child dialog.|
| `OnRepromptDialog` |`RepromptDialog`| Actions to perform when `RepromptDialog` event occurs.                                                                              |
| `OnDialog` | `DialogEvents.VersionChanged` | |

## Activity event triggers

Activity triggers let you associate actions to any incoming activity from the client such as when a new user joins and the bot begins a new conversation. Additional information on activities can be found in [Bot Framework Activity schema][botframework-activity].

All activity events have a base event of `ActivityReceived` and are further refined by their _activity type_. The Base class that all activity triggers derive from is `OnActivity`.

| Event cause         | ActivityType   | Trigger name                   | Description                                                                       |
| ------------------- | -------------- | ------------------------------ | --------------------------------------------------------------------------------- |
| Greeting            | `ConversationUpdate` | `OnConversationUpdateActivity` | Actions to perform on receipt of a `conversationUpdate` activity, when the bot or a user joins or leaves a conversation. |
| Conversation ended  | `EndOfConversation` | `OnEndOfConversationActivity`  | Actions to perform on receipt of an `endOfConversation` activity.  |
| Event received      | `Event`        | `OnEventActivity`              | Actions to perform on receipt of an `event` activity.                   |
| Handover to human   | `Handoff`      | `OnHandoffActivity`            | Actions to perform on receipt of a `handOff` activity.                  |
| Conversation invoked| `Invoke`       | `OnInvokeActivity`             | Actions to perform on receipt of an `invoke` activity.                  |
| User is typing      | `Typing`       | `OnTypingActivity`             | Actions to perform on receipt of a `typing` activity.                   |

## Message event triggers

Message event triggers allow you to react to any message event such as when a message is updated (`MessageUpdate`) or deleted (`MessageDeletion`) or when someone reacts (`MessageReaction`) to a message (for example, some of the common message reactions include a Like, Heart, Laugh, Surprised, Sad and Angry reactions).

Message events are a type of activity event and, as such, all message events have a base event of `ActivityReceived` and are further refined by _activity type_. The Base class that all message triggers derive from is `OnActivity`.

| Event cause | ActivityType | Trigger name | Description |
|--|--|--|--|
| Message received | `Message` | `OnMessageActivity` | Actions to perform on receipt of an activity with type `MessageReceived`. <!--Overrides Intent trigger.--> |
| Message deleted | `MessageDeletion` | `OnMessageDeleteActivity` | Actions to perform on receipt of an activity with type `MessageDelete`. |
| Message reaction | `MessageReaction` | `OnMessageReactionActivity` | Actions to perform on receipt of an activity with type `MessageReaction`. |
| Message updated | `MessageUpdate` | `OnMessageUpdateActivity` | Actions to perform on receipt of an activity with type `MessageUpdate`. |

## Custom event trigger

You can emit your own events by adding the [EmitEvent][emitevent] action to any trigger, then you can handle that custom event in any trigger in any dialog in your bot by defining a _custom event_ trigger. A custom event trigger is the `OnDialogEvent` trigger that in effect becomes a custom trigger when you set the `Event` property to the same value as the EmitEvent's `EventName` property.

> [!TIP]
> You can allow other dialogs in your bot to handle your custom event by setting the EmitEvent's `BubbleEvent` property to true.

| Event cause  | Trigger name    | Base class    | Description                                                                                                              |
| ------------ | --------------- | ------------- | ------------------------------------------------------------------------------------------------------------------------ |
| Custom event | `OnDialogEvent` | `OnCondition` | Actions to perform when a custom event is detected. Use [Emit a custom event][emitevent] action to raise a custom event. |

[triggers]:../v4sdk/bot-builder-concept-adaptive-dialog-triggers.md
[inputs]:../v4sdk/bot-builder-concept-adaptive-dialog-inputs.md

[recognizers-cross-trained-recognizer-set]:../v4sdk/bot-builder-concept-adaptive-dialog-recognizers.md#cross-trained-recognizer-set
[qna-maker-recognizer]:adaptive-dialog-prebuilt-recognizers.md#qna-maker-recognizer
[emitevent]: /composer/concept-events-and-triggers#custom-events

[botframework-activity]:https://github.com/microsoft/botframework-sdk/blob/master/specs/botframework-activity/botframework-activity.md
