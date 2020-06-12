---
title: Adaptive dialogs prebuilt triggers
description: Describing the adaptive dialogs prebuilt triggers
keywords: bot, triggers, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 06/09/2020
monikerRange: 'azure-bot-service-4.0'
---

# Adaptive dialogs prebuilt triggers

[!INCLUDE [applies-to-v4](../includes/applies-to.md)]

This article lists the available prebuilt triggers grouped by their general purpose:

- [Base trigger](#base-trigger)
- [Recognizer event triggers](#recognizer-event-triggers)
- [Dialog event triggers](#dialog-event-triggers)
- [Activity events](#activity-event-triggers)
- [Message events](#message-event-triggers)
- [Custom event trigger](#custom-event-trigger)

## Base trigger

The `OnCondition` trigger is the base trigger that all triggers derive from. When defining triggers in an adaptive dialog they are defined as a list of `OnCondition` objects as demonstrated in the following example:

```csharp
Triggers = new List<OnCondition>()
{
    ...
}
```

## Recognizer event triggers

| Event cause               | Trigger name  | Base event    | Description                                                       |
| ------------------------- | ------------- | ------------- | ----------------------------------------------------------------- |
| Choose Intent | `OnChooseIntent` |`ChooseIntent` | This trigger is run when ambiguity has been detected between intents from multiple recognizers in a [CrossTrainedRecognizerSet][recognizers-cross-trained-recognizer-set].|
| Intent recognized| `OnIntent` | `RecognizedIntent` | Actions to perform when specified intent is recognized.           |
|QnAMatch intent|`OnQnAMatch`| `RecognizedIntent` |This trigger is run when the [QnAMakerRecognizer][qna-maker-recognizer] has returned a `QnAMatch` intent. The entity `@answer` will have the `QnAMaker` answer.|
|Unknown intent recognized| `OnUnknownIntent` | `UnknownIntent` | Actions to perform when user input is unrecognized or no match is found in any of the `OnIntent` triggers. You can also use this as your first trigger in your root dialog in place of the `OnBeginDialog` to preform any needed tasks when the dialog first starts. |

### Recognizer event example

Examples of `OnIntent` and `OnUnknownIntent` triggers are given in the example below to demonstrate the use of the Recognizer triggers.

> [!NOTE]
>
> - The `OnIntent` trigger lets you handle the `recognizedIntent` event. The `recognizedIntent` event is raised by the [recognizer][8]. All of the Bot Framework SDK built-in recognizers emit this event when they successfully identify a user _input_ so that your bot can respond appropriately.
> - Use the `OnUnknownIntent` trigger to catch and respond when a `recognizedIntent` event isn't caught and handled by any of the other triggers. <!--This is especially helpful to capture and handle cases where your dialog wishes to participate in consultation.-->

``` C#
// Create the root dialog as an Adaptive dialog.
var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog));

// Add a regex recognizer
rootDialog.Recognizer = new RegexRecognizer()
{
    Intents = new List<IntentPattern>()
    {
        new IntentPattern()
        {
            Intent = "HelpIntent",
            Pattern = "(?i)help"
        },
        new IntentPattern()
        {
            Intent = "CancelIntent",
            Pattern = "(?i)cancel|never mind"
        }
    }
};

// Create an OnIntent trigger named "helpTrigger" that handles the intent named "HelpIntent".
var helpTrigger = new OnIntent("HelpIntent");

// Create a list of actions to execute when the trigger named "helpTrigger" fires.
var helpActions = new List<Dialog>();
helpActions.Add(new SendActivity("Hello, I'm the samples bot. At the moment, I respond to only help!"));
helpTrigger.Actions = helpActions;

// Add the OnIntent trigger "helpTrigger" to the root dialog
rootDialog.Triggers.Add(helpTrigger);

// Create a trigger to handle the unhandled intent events. The unknown intent trigger fires when a  
// recognizedIntent event raised by the recognizer is not handled by any OnIntent trigger in the dialog.
// Given the RegEx recognizer added to this dialog, this trigger will fire when the user says 'cancel'.
// The RegexRecognizer returned the 'cancel' intent, however, we have no trigger attached to handled it.
// The OnUnknownIntent trigger will also fire when user says 'yo' (or any other word that does not map
// to any intent in the recognizer). When the recognizer parses the user input and does not detect an
// intent, it will return a 'none' intent and since there is no OnIntent trigger to handle  a 'none'
// intent, the unknown intent trigger fires.
var unhandledIntentTrigger = new OnUnknownIntent();
var unhandledIntentActions = new List<Dialog>();
unhandledIntentActions.Add(new SendActivity("Sorry, I did not recognize that"));
unhandledIntentTrigger.Actions = unhandledIntentActions;

// Add the OnUnknownIntent trigger "unhandledIntentTrigger" to the root dialog
rootDialog.Triggers.Add(unhandledIntentTrigger);
```

## Dialog events

Dialog triggers handle dialog specific events that are related to the "lifecycle" of the dialog.  There are currently 6 dialog triggers in the Bot Framework SDK and they all derive from the `OnDialogEvent` class.

> [!TIP]
> These aren't like normal interruption event handlers where the a child's actions will continue running after the handlers actions complete. For all of the events below the bot will be running a new set of actions and will end the turn once those actions have finished.

| Trigger name     | Base event   | Description                                                                                                                         |
| ---------------- | ------------ | ----------------------------------------------------------------------------------------------------------------------------------- |
| `OnBeginDialog`    | `BeginDialog`  | Actions to perform when this dialog begins. For use with child dialogs only, not to be used in your root dialog, In root dialogs, use `OnUnknownIntent` to perform dialog initialization activities.|
| `OnCancelDialog`   | `CancelDialog` | This event allows you to prevent the current dialog from being cancelled due to a child dialog executing a `CancelAllDialogs` action. |
| `OnEndOfActions`   | `EndOfActions` | This event occurs once all actions and ambiguity events have been processed.                                                        |
| `OnError`          | `Error`        | Action to perform when an `Error` dialog event occurs. This event is similar to `OnCancelDialog` in that you are preventing the current dialog from ending, in this case due to an error in a child dialog.|
| `OnRepromptDialog` |`RepromptDialog`| Actions to perform when `RepromptDialog` event occurs.                                                                              |

### Dialog event example

This example demonstrates sending a welcome message to the user, using the `OnBeginDialog` trigger.

```cs
var adaptiveDialog = new AdaptiveDialog()
{
    Triggers = new List<OnCondition>()
    {
        new OnBeginDialog()
        {
            Actions = new List<Dialog>()
            {
                new SendActivity("Hello world!")
            }
        }
    }
};
```

## Activity event triggers

Activity triggers let you associate actions to any incoming activity from the client such as when a new user joins and the bot begins a new conversation. Additional information on activities can be found in [Bot Framework Activity schema][botframework-activity].

All activity events have a base event of `ActivityReceived` and are further refined by their _activity type_. The Base class that all activity triggers derive from is `OnActivity`.

| Event cause         | ActivityType   | Trigger name                   | Description                                                                       |
| ------------------- | -------------- | ------------------------------ | --------------------------------------------------------------------------------- |
| Greeting            | `ConversationUpdate` | `OnConversationUpdateActivity` | Handle the events fired when a user begins a new conversation with the bot. |
| Conversation ended  | `EndOfConversation` | `OnEndOfConversationActivity`  | Actions to perform on receipt of an activity with type `EndOfConversation`.  |
| Event received      | `Event`        | `OnEventActivity`              | Actions to perform on receipt of an activity with type `Event`.                   |
| Handover to human   | `Handoff`      | `OnHandoffActivity`            | Actions to perform on receipt of an activity with type `HandOff`.                 |
| Conversation invoked| `Invoke`       | `OnInvokeActivity`             | Actions to perform on receipt of an activity with type `Invoke`.                  |
| User is typing      | `Typing`       | `OnTypingActivity`             | Actions to perform on receipt of an activity with type `Typing`.                  |

### Activity event example

#### OnConversationUpdateActivity

The `OnConversationUpdateActivity` trigger is one of the triggers that let you handle an _activity received_ event. The `OnConversationUpdateActivity` trigger will only fire when the _ActivityTypes.conversationUpdate_ condition is met.

The following code snippet demonstrates how you can create an `OnConversationUpdateActivity` trigger:

```C#
var myDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
{
    Generator = new TemplateEngineLanguageGenerator(new TemplateEngine().AddFile(Path.Combine(".", "myDialog.lg"))),
    Triggers = new List<OnCondition>() {
        new OnConversationUpdateActivity() {
            Actions = new List<Dialog>() {
                new SendActivity("${Welcome-user()}")
            }
        }
    }
};
```

## Message event triggers

<!-- Should the Message events section be combined with the Activity events section? -->

Message event triggers allow you to react to any message event such as when a message is updated (`MessageUpdate`) or deleted (`MessageDeletion`) or when someone reacts (`MessageReaction`) to a message (for example, some of the common message reactions include a Like, Heart, Laugh, Surprised, Sad and Angry reactions).

Message events are a type of activity event and, as such, all message events have a base event of `ActivityReceived` and are further refined by _activity type_. The Base class that all message triggers derive from is `OnActivity`.

| Event cause      | ActivityType      | Trigger name               | Description                                                               |
| ---------------- | ----------------- | -------------------------- | ------------------------------------------------------------------------- |
| Message received | `Message`         | `OnMessageActivity`        | Actions to perform on receipt of an activity with type `MessageReceived`. | <!--Overrides Intent trigger.-->
| Message deleted  | `MessageDeletion` | `OnMessageDeleteActivity`  | Actions to perform on receipt of an activity with type `MessageDelete`.   |
| Message reaction | `MessageReaction` | `OnMessageReactionActivity`| Actions to perform on receipt of an activity with type `MessageReaction`. |
| Message updated  | `MessageUpdate`   | `OnMessageUpdateActivity`  | Actions to perform on receipt of an activity with type `MessageUpdate`.   |

<!--TODO P1: Need Message event examples
### Message event examples

--->

## Custom event trigger

You can emit your own events by adding the [EmitEvent][emitevent] action to any trigger, then you can handle that custom event in any trigger in any dialog in your bot by defining a _custom event_ trigger. A custom event trigger is the `OnDialogEvent` trigger that in effect becomes a custom trigger when you set the `Event` property to the same value as the EmitEvent's `EventName` property.

> [!TIP]
> You can allow other dialogs in your bot to handle your custom event by setting the EmitEvent's `BubbleEvent` property to true.

| Event cause  | Trigger name    | Base class    | Description                                                                                                        |
| ------------ | --------------- | ------------- | ------------------------------------------------------------------------------------------------------------------ |
| Custom event | `OnDialogEvent` | `OnCondition` | Actions to perform when a custom event is detected. Use [Emit a custom event][emitevent]' action to raise a custom event. |

<!--Was: OnCustomEvent-->

### Custom event example

```csharp
var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
    {
        Generator = new TemplateEngineLanguageGenerator(),
        Triggers = new List<OnCondition>()
        {
            new OnUnknownIntent()
            {
                Actions = new List<Dialog>()
                {
                    new TextInput()
                    {
                        Prompt = new ActivityTemplate("What's your name?"),
                        Property = "user.name",
                        AlwaysPrompt = true,
                        OutputFormat = "toLower(this.value)"
                    },
                    new EmitEvent()
                    {
                        EventName = "contoso.custom",
                        EventValue = "=user.name",
                        BubbleEvent = true,
                    },
                    new SendActivity("Your name is ${user.name}"),
                    new SendActivity("And you are ${$userType}")
                }
            },
            new OnDialogEvent()
            {
                Event = "contoso.custom",

                // You can use conditions (expression) to examine value of the event as part of the trigger selection process.
                Condition = "turn.dialogEvent.value && (substring(turn.dialogEvent.value, 0, 1) == 'v')",
                Actions = new List<Dialog>()
                {
                    new SendActivity("In custom event: '${turn.dialogEvent.name}' with the following value '${turn.dialogEvent.value}'"),
                    new SetProperty()
                    {
                        Property = "$userType",
                        Value = "VIP"
                    }
                }
            },
            new OnDialogEvent()
            {
                Event = "contoso.custom",

                // You can use conditions (expression) to examine value of the event as part of the trigger selection process.
                Condition = "turn.dialogEvent.value && (substring(turn.dialogEvent.value, 0, 1) == 's')",
                Actions = new List<Dialog>()
                {
                    new SendActivity("In custom event: '${turn.dialogEvent.name}' with the following value '${turn.dialogEvent.value}'"),
                    new SetProperty()
                    {
                        Property = "$userType",
                        Value = "Special"
                    }
                }
            },
            new OnDialogEvent()
            {
                Event = "contoso.custom",
                Actions = new List<Dialog>()
                {
                    new SendActivity("In custom event: '${turn.dialogEvent.name}' with the following value '${turn.dialogEvent.value}'"),
                    new SetProperty()
                    {
                        Property = "$userType",
                        Value = "regular customer"
                    }
                }
            }
        }
    };
```

## Additional Information

- [Introduction to adaptive dialogs][introduction]
- [Dialog libraries][bot-builder-concept-dialog]
- [Actions in adaptive dialogs][actions]

[introduction]:../v4sdk/bot-builder-adaptive-dialog-introduction.md
[actions]:../v4sdk/bot-builder-concept-adaptive-dialog-actions.md
[inputs]:../v4sdk/bot-builder-concept-adaptive-dialog-inputs.md
[Recognizers]:bot-builder-concept-adaptive-dialog-recognizers.md
[bot-builder-concept-dialog]:../v4sdk/bot-builder-concept-dialog.md

[recognizers-cross-trained-recognizer-set]:../v4sdk/bot-builder-concept-adaptive-dialog-recognizers.md#cross-trained-recognizer-set
[qna-maker-recognizer]:../v4sdk/bot-builder-concept-adaptive-dialog-recognizers.md#qna-maker-recognizer <!--[qna-maker-recognizer]:adaptive-dialog-prebuilt-recognizers.md#qna-maker-recognizer-->
[emitevent]:../v4sdk/bot-builder-concept-adaptive-dialog-actions.md#emitevent                           <!--[emitevent]:adaptive-dialog-prebuilt-actions.md#emitevent-->

[botframework-activity]:https://github.com/microsoft/botframework-sdk/blob/master/specs/botframework-activity/botframework-activity.md