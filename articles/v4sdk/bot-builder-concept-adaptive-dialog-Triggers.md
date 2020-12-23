---
title: Events and triggers in adaptive dialogs
description: Events and triggers in adaptive dialogs
keywords: bot, user, Events, triggers, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 04/27/2020
monikerRange: 'azure-bot-service-4.0'
---

# Events and triggers in adaptive dialogs

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Adaptive dialogs introduce a new event based approach to model conversations. Any sub-system in your bot can emit events and all adaptive dialogs contain one or more event handlers called _triggers_ that enable you to react to these events.  Any time an event fires, the active adaptive dialog's triggers are evaluated and if any trigger matches the current event, the [actions][actions] associated with that trigger execute. If an event is not handled in the active dialog, it will be passed up to its parent dialog to be evaluated. This process continues until it is either handled or reaches the bots root dialog. If no event handler (_trigger_) is found, the event will be ignored and no action will be taken.

<!--TODO P3: preBubble/consultation/postBubble phases - Possibly document in an advanced section at some point.
From: v-jofin: (https://github.com/MicrosoftDocs/bot-docs-pr/pull/2109#discussion_r418164608)
There is also the post-bubble/trailing-edge process where everyone who didn't handle the event on the leading-edge gets a second chance on the trailing edge, from the root back down to the leaf.
From Stevenic: (https://github.com/MicrosoftDocs/bot-docs-pr/pull/2109#discussion_r425335486)
"If anything it seems like these details would be in an "Advanced Event Concepts" section as I'm not sure most devs ever need to worry about this or even understand it. I had to create this mechanism to get our consultation stuff to work but it's a super advanced concept."
--->

## Prerequisites

* [How bots work][how-bots-work]
* [Introduction to adaptive dialogs][introduction]
* [Dialog libraries][concept-dialog]

## Anatomy of a trigger

 A trigger is made up of a condition and one or more actions. Bot Framework SDK offers several triggers, each with a set of predefined conditions that examine either the eventName or eventValue. You can add additional conditions to a trigger, giving you additional control when the trigger executes.

The Bot Framework SDK provides various pre-defined triggers designed to handle common event types.  For example the `OnIntent` trigger fires anytime the [recognizer][recognizers] detects an [intent][recognizer-intents]. If you are using a [LUIS][luis] recognizer it will also return a [prediction score][luis-prediction-scores] that measures the degree of confidence LUIS has for its prediction results. In order to increase the reliability and accuracy of your bot, you may only want to execute the `OnIntent` trigger if the confidence rating is 80% or higher. You can accomplish this by adding a _condition_. Triggers all contain an optional `Condition` property that when defined, must evaluate to _true_ in order for the trigger to execute. The `Condition` property is a string, but must contain a valid [adaptive expression][adaptive-expressions] to work. The above examples `Condition` property would look something like: `Condition = "#<IntentName>.Score >= 0.8"`. Adaptive expressions enable sophisticated conditions that can handle virtually any scenario that you might have.

All triggers also contain a list of _Actions_. Actions represent what your bot does in response to a trigger.  This is the heart of the trigger. You can learn more about actions and what built in actions are provided in the Bot Framework SDK in the article [Actions in adaptive dialogs][actions].

## Trigger types

_Triggers_ enable you to catch and respond to events. The broadest trigger from which all other triggers are derived is the `OnCondition` trigger that allows you to catch and attach a list of actions to execute when a specific event is emitted by any of the bots sub-systems.

Triggers are listed in the following sections, categorized and grouped by trigger type.

Each table in the following sections list all of the triggers supported by adaptive dialogs that are currently in the SDK as well as the events they are based on.

### Base trigger

The `OnCondition` trigger is the base trigger that all triggers derive from. When defining triggers in an adaptive dialog they are defined as a list of `OnCondition` objects.

For more information and an example, see the [Base trigger][base-trigger] section in the Adaptive dialogs prebuilt triggers article.

### Recognizer event triggers

[Recognizers][recognizers] extract meaningful pieces of information from a user's input in the form of _intents_ and _entities_ and when they do, they emit events. For example the `recognizedIntent` event fires when the recognizer picks up an intent (or extracts entities) from a given user utterance. You handle this events using the `OnIntent` trigger.

The following list shows some of the _recognizer event triggers_ available in the Bot Framework SDK:

* **Choose Intent**. The `OnChooseIntent` trigger executes when there is ambiguity between intents from multiple recognizers in a [CrossTrainedRecognizerSet][recognizer-cross-trained-recognizer-set].
* **Intent recognized**. The `OnIntent` trigger executes when the specified intent is recognized.
* **QnA Match**. The `OnQnAMatch` trigger executes when the [QnAMakerRecognizer][qna-maker-recognizer] has returned a `QnAMatch` intent.
* **No intent was recognized**. The `OnUnknownIntent` trigger executes when user input is unrecognized or no match is found in any of the `OnIntent` triggers. You can also use this as your first trigger in your root dialog in place of the `OnBeginDialog` to preform any needed tasks when the dialog first starts.

For detailed information and examples, see the [Recognizer event triggers][recognizer-event-triggers] section in the Adaptive dialogs prebuilt triggers article.

### Dialog event triggers

Dialog triggers handle dialog specific events that are related to the "lifecycle" of the dialog.  There are currently 6 dialog triggers in the Bot Framework SDK and they all derive from the `OnDialogEvent` class.

> [!TIP]
> These aren't like normal interruption event handlers where the a child's actions will continue running after the handlers actions complete. For all of the events below the bot will be running a new set of actions and will end the turn once those actions have finished.

For the _dialog trigger_ to:

* Take action immediately when the dialog starts, even before the recognizer is called, use the `OnBeginDialog` trigger.
* Prevent a dialog from being canceled when any of its child dialogs execute a `CancelAllDialogs` action, use the `OnCancelDialog` trigger.
* Take action when all actions and ambiguity events have been processed, use the `OnEndOfActions` trigger.
* Handle an error condition, use the `OnError` trigger.

For detailed information and examples, see the [Dialog events][dialog-event-triggers] section in the Adaptive dialogs prebuilt triggers article.

### Activity event triggers

Activity triggers let you associate actions to any incoming activity from the client such as when a new user joins and the bot begins a new conversation. Additional information on activities can be found in [Bot Framework Activity schema][botframework-activity].

All activity events have a base event of `ActivityReceived` and are further refined by their _activity type_. The Base class that all activity triggers derive from is `OnActivity`.

* **Conversation update**. Use this to handle events fired when a user begins a new conversation with the bot.
* **Conversation ended**. Actions to perform on receipt of an activity with type `EndOfConversation`.
* **Event received**. Actions to perform on receipt of an activity with type `Event`.
* **Handover to human**. Actions to perform on receipt of an activity with type `HandOff`.
* **Conversation invoked**. Actions to perform on receipt of an activity with type `Invoke`.
* **User is typing**. Actions to perform on receipt of an activity with type `Typing`.

For detailed information and examples, see the [Activity event triggers][activity-event-triggers] section in the Adaptive dialogs prebuilt triggers article.

### Message event triggers

**Message event** triggers allow you to react to any message event such as when a message is updated (`MessageUpdate`) or deleted (`MessageDeletion`) or when someone reacts (`MessageReaction`) to a message (for example, some of the common message reactions include a Like, Heart, Laugh, Surprised, Sad and Angry reactions).

Message events are a type of activity event and, as such, all message events have a base event of `ActivityReceived` and are further refined by _activity type_. The Base class that all message triggers derive from is `OnActivity`.

* **Message received**. Actions to perform on receipt of an activity with type `MessageReceived`.
* **Message deleted**. Actions to perform on receipt of an activity with type `MessageDelete`.
* **Message reaction**. Actions to perform on receipt of an activity with type `MessageReaction`.
* **Message updated**. Actions to perform on receipt of an activity with type `MessageUpdate`.

For detailed information and examples, see the [Message event triggers][message-event-triggers] section in the Adaptive dialogs prebuilt triggers article.

### Custom event trigger

You can emit your own events by adding the [EmitEvent][emitevent-action] action to any trigger, then you can handle that custom event in any trigger in any dialog in your bot by defining a _custom event_ trigger. A custom event trigger is the `OnDialogEvent` trigger that in effect becomes a custom trigger when you set the `Event` property to the same value as the EmitEvent's `EventName` property.

> [!TIP]
> You can allow other dialogs in your bot to handle your custom event by setting the EmitEvent's `BubbleEvent` property to true.
For more information and an example, see the [Custom events][custom-events] section in the Adaptive dialogs prebuilt triggers article.

For detailed information and an example, see the [Custom event triggers][custom-event-triggers] section in the Adaptive dialogs prebuilt triggers article.

## Additional Information

* [Introduction to adaptive dialogs][introduction]
* [Dialog libraries][concept-dialog]
* [Actions in adaptive dialogs][actions]
* For more detailed information on triggers in adaptive dialogs, including examples, see the [Triggers in adaptive dialogs - reference guide][triggers-ref].

<!-- Adaptive dialog links-->
[introduction]:bot-builder-adaptive-dialog-introduction.md
[actions]:bot-builder-concept-adaptive-dialog-actions.md
[inputs]:bot-builder-concept-adaptive-dialog-inputs.md

[emitevent-action]:../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#emitevent
[recognizer-intents]:bot-builder-concept-adaptive-dialog-recognizers.md#intents
[recognizers]:bot-builder-concept-adaptive-dialog-recognizers.md
[recognizer-cross-trained-recognizer-set]:bot-builder-concept-adaptive-dialog-recognizers.md#cross-trained-recognizer-set
[qna-maker-recognizer]:bot-builder-concept-adaptive-dialog-recognizers.md#qna-maker-recognizer

<!-- Reference article links-->
[triggers-ref]: ../adaptive-dialog/adaptive-dialog-prebuilt-triggers.md
[base-trigger]:../adaptive-dialog/adaptive-dialog-prebuilt-triggers.md#base-trigger
[recognizer-event-triggers]:../adaptive-dialog/adaptive-dialog-prebuilt-triggers.md#recognizer-event-triggers
[dialog-event-triggers]:../adaptive-dialog/adaptive-dialog-prebuilt-triggers.md#dialog-event-triggers
[activity-event-triggers]:../adaptive-dialog/adaptive-dialog-prebuilt-triggers.md#activity-event-triggers
[message-event-triggers]:../adaptive-dialog/adaptive-dialog-prebuilt-triggers.md#message-event-triggers
[custom-event-trigger]:../adaptive-dialog/adaptive-dialog-prebuilt-triggers.md#custom-event-trigger

<!-- BF SDK links-->
[botframework-activity]:https://github.com/microsoft/botframework-sdk/blob/master/specs/botframework-activity/botframework-activity.md
[adaptive-expressions]:bot-builder-concept-adaptive-expressions.md
[concept-dialog]:bot-builder-concept-dialog.md
[how-bots-work]:bot-builder-basics.md

<!-- External links-->
[luis]:https://www.luis.ai/home
[luis-prediction-scores]:/azure/cognitive-services/luis/luis-concept-prediction-scores
