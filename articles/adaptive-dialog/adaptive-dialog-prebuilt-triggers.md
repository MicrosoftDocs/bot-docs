---
title: Events and triggers in adaptive dialogs - reference guide
description: Describing the adaptive dialog prebuilt triggers
keywords: bot, triggers, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 06/09/2020
monikerRange: 'azure-bot-service-4.0'
---

# Events and triggers in adaptive dialogs - reference guide

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

For an introduction to this topic, see the [Events and triggers in adaptive dialogs](../v4sdk/bot-builder-concept-adaptive-dialog-triggers.md) concept article.

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

> [!TIP]
> Use the `OnUnknownIntent` trigger to catch and respond when a "none" intent occurs. Using the `OnIntent` trigger to handle a "none" intent can produce unexpected results.

### Recognizer event example

Examples of `OnIntent` and `OnUnknownIntent` triggers are given in the example below to demonstrate the use of the Recognizer triggers.

> [!NOTE]
>
> - The `OnIntent` trigger lets you handle the `recognizedIntent` event. The `recognizedIntent` event is raised by a [recognizer][recognizers]. With the exception of the [QnA Maker recognizer][qna-maker-recognizer], all of the Bot Framework SDK built-in recognizers emit this event when they successfully identify a user _input_ so that your bot can respond appropriately.
> - Use the `OnUnknownIntent` trigger to catch and respond when a `recognizedIntent` event isn't caught and handled by any of the other triggers. This means that any unhandled intent (including "none") can cause it to trigger, but only if there aren't any currently executing actions for the dialog.<!--Use the `OnUnknownIntent` trigger to catch and respond when a "none" intent occurs. This is especially helpful to capture and handle cases where your dialog wishes to participate in consultation.-->

```csharp
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

> [!TIP]
> If you are using the [declarative][declarative] approach to adaptive dialogs, your dialogs are defined as `.dialog` files that are used to create your dialogs at run-time. These dialogs can also be modified at run time by updating the `.dialog` file directly then handling the `resourceExplorer.Changed` event to reload the dialog. You can also capture the `DialogEvents.VersionChanged` event in a [trigger][triggers] to take any needed actions that may result from a dialog changing in the middle of a conversation with a user. For more information see [Auto reload dialogs when file changes][auto-reload-dialogs-when-file-changes] in the the [Using declarative assets in adaptive dialogs][declarative] article.

### Dialog event example

This example demonstrates sending a welcome message to the user, using the `OnBeginDialog` trigger.

```cs
var welcomeDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
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
| Greeting            | `ConversationUpdate` | `OnConversationUpdateActivity` | Actions to perform on receipt of a `conversationUpdate` activity, when the bot or a user joins or leaves a conversation. |
| Conversation ended  | `EndOfConversation` | `OnEndOfConversationActivity`  | Actions to perform on receipt of an `endOfConversation` activity.  |
| Event received      | `Event`        | `OnEventActivity`              | Actions to perform on receipt of an `event` activity.                   |
| Handover to human   | `Handoff`      | `OnHandoffActivity`            | Actions to perform on receipt of a `handOff` activity.   |
| Conversation invoked| `Invoke`       | `OnInvokeActivity`             | Actions to perform on receipt of an `invoke` activity.                  |
| User is typing      | `Typing`       | `OnTypingActivity`             | Actions to perform on receipt of a `typing` activity.                  |

### Activity event example

#### OnConversationUpdateActivity

The `OnConversationUpdateActivity` trigger lets you handle an _activity received_ event. The `OnConversationUpdateActivity` trigger will only fire when the activity received is a `conversationUpdate` activity.

The following code snippet demonstrates how you can create an `OnConversationUpdateActivity` trigger:

```csharp
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

| Event cause | ActivityType | Trigger name | Description |
|--|--|--|--|
| Message received | `Message` | `OnMessageActivity` | Actions to perform on receipt of an activity with type `MessageReceived`. <!--Overrides Intent trigger.--> |
| Message deleted | `MessageDeletion` | `OnMessageDeleteActivity` | Actions to perform on receipt of an activity with type `MessageDelete`. |
| Message reaction | `MessageReaction` | `OnMessageReactionActivity` | Actions to perform on receipt of an activity with type `MessageReaction`. |
| Message updated | `MessageUpdate` | `OnMessageUpdateActivity` | Actions to perform on receipt of an activity with type `MessageUpdate`. |

<!--TODO P1: Need Message event examples
### Message event examples

--->

## Custom event trigger

You can emit your own events by adding the [EmitEvent][emitevent] action to any trigger, then you can handle that custom event in any trigger in any dialog in your bot by defining a _custom event_ trigger. A custom event trigger is the `OnDialogEvent` trigger that in effect becomes a custom trigger when you set the `Event` property to the same value as the EmitEvent's `EventName` property.

> [!TIP]
> You can allow other dialogs in your bot to handle your custom event by setting the EmitEvent's `BubbleEvent` property to true.

| Event cause  | Trigger name    | Base class    | Description                                                                                                              |
| ------------ | --------------- | ------------- | ------------------------------------------------------------------------------------------------------------------------ |
| Custom event | `OnDialogEvent` | `OnCondition` | Actions to perform when a custom event is detected. Use [Emit a custom event][emitevent] action to raise a custom event. |

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
- [Dialogs library][bot-builder-concept-dialog]
- [Actions in adaptive dialogs][actions]

[introduction]:../v4sdk/bot-builder-adaptive-dialog-introduction.md
[actions]:../v4sdk/bot-builder-concept-adaptive-dialog-actions.md
[triggers]:../v4sdk/bot-builder-concept-adaptive-dialog-triggers.md
[inputs]:../v4sdk/bot-builder-concept-adaptive-dialog-inputs.md
[recognizers]:../v4sdk/bot-builder-concept-adaptive-dialog-recognizers.md

[declarative]:../v4sdk/bot-builder-concept-adaptive-dialog-declarative.md
[auto-reload-dialogs-when-file-changes]:../v4sdk/bot-builder-concept-adaptive-dialog-declarative.md#auto-reload-dialogs-when-file-changes

[bot-builder-concept-dialog]:../v4sdk/bot-builder-concept-dialog.md

[recognizers-cross-trained-recognizer-set]:../v4sdk/bot-builder-concept-adaptive-dialog-recognizers.md#cross-trained-recognizer-set
[qna-maker-recognizer]:adaptive-dialog-prebuilt-recognizers.md#qna-maker-recognizer
[emitevent]:adaptive-dialog-prebuilt-actions.md#emitevent

[botframework-activity]:https://github.com/microsoft/botframework-sdk/blob/master/specs/botframework-activity/botframework-activity.md
