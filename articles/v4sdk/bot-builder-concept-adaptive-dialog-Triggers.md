---
title: Events and triggers - Bot framework SDK - adaptive dialogs
description: Collecting user input using adaptive dialogs
keywords: bot, user, Events, triggers, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 04/27/2020
---

>[!IMPORTANT]
> There is a difference in the way that triggers are grouped between this article and the Composer UI, is that a problem?
>
> Triggers are grouped into these three categories in this article:
>
> * recognizer
> * dialog
> * activity
>
> Triggers are grouped in the Composer UI as follows:
>
> * Intent recognized
> * Unknown intent
> * Dialog events
> * Activities
> * Message events
> * Custom event

# Events and triggers in adaptive dialogs

Using the new adaptive dialog architecture provided in the Bot Framework SDK makes it easier to collect and validate a variety of data types and handle scenarios in which users input invalid or unrecognized data. Key components of this new architecture are _events_ and _triggers_. Adaptive dialogs introduce a new event model and every adaptive dialog contains one or more event handlers called _triggers_. Any time an event is emitted by any sub-system in your bot, the active dialog will check to see if it has a trigger defined to handle that event and if an event is not handled in a child dialog, it will be passed up to its parent dialog. This process continues until it is either handled or reaches the bots root dialog. If no event handler (_trigger_) is found, the event will be ignored and no action will be taken.

## Prerequisites

* A general understanding of adaptive dialogs in the Bot Framework V4 SDK is helpful. For more information, see [introduction to adaptive dialogs][1] and [dialog libraries][5].

## Anatomy of a trigger

### Conditions

All triggers contain a `Condition` property that, when defined, must be met in order for the trigger to execute. The `Condition` property is a string, but must contain a valid [adaptive expression][4] to work. Adaptive expressions enable sophisticated conditions that can enabled virtually any scenario that you might have. If the condition evaluates to false, processing of the event continues with the next trigger. Some triggers have built in conditions as listed in the table that appears in the [Trigger types](#-trigger-types) section below, however you can still define your own conditions in addition to the built in condition.

### Actions

All triggers also contain a list of _Actions_. Actions contain the code that will execute when an event occurs.  This is the heart of the trigger. You can learn more about actions and what built in actions are provided in the bot framework SDK in the article [Actions in adaptive dialogs][2].

## Trigger types

_Triggers_ enable you to catch and respond to events. The broadest trigger from which all other triggers are derived is the `OnCondition` trigger that allows you to catch and attach a list of actions to execute when a specific event is emitted by any of the bots sub-systems.

The table below list all of the triggers supported by adaptive dialogs that are currently in the SDK as well as the events they are based on. All triggers support `conditions` and some have default `conditions` that are defined in the SDK, these are also listed in this table when applicable.

### Base trigger

The `OnCondition` trigger is the base trigger that all triggers derive from. When definding triggers in an adaptive dialog they are defined as a list of OnCondition objects:

| Trigger Name           | Description                          |
| ---------------------- | ------------------------------------ |
| OnCondition            | All triggers derive from this class. |

```csharp
Triggers = new List<OnCondition>()
{
    ...
}
```

### Recognizer event triggers

<!-----Need summary All triggers based on the OnDialogEvent trigger.-------->

| Trigger Name                 | Description                          | Base event       | Condition                              |
| ---------------------------- | ------------------------------------ | ---------------- | -------------------------------------- |
| OnChooseIntent | Only available if a cross trained recognizer is configured on the dialog. | RecognizedIntent | turn.recognized.intent == 'ChooseIntent' |
| OnIntent                     | Intent event handler                 | RecognizedIntent | turn.recognized.intent == 'IntentName' |
| OnUnknownIntent              | Unknown intent event handler         | UnknownIntent    |                                        |
| OnQnAMatch | QnA Match intent. Only available if QnA recognizer is configured on the dialog | RecognizedIntent | turn.recognized.intent == 'QnAMatch' |

#### Recognizer trigger examples

Examples of `OnIntent` and `OnUnknownIntent` triggers are given below to demonstrate the use of the Recognizer triggers.

##### OnIntent

The `OnIntent` trigger enables you to handle the 'recognizedIntent' event. The 'recognizedIntent' event is raised by the recognizer. All of the bot framework SDK built-in recognizers emit this event when they successfully identify a user _input_ so that your bot can respond appropriately.

The following code snippet demonstrates how you can create an `OnIntent` trigger:

``` C#
// Create the root dialog as an adaptive dialog.
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

// Create an OnIntent trigger named helpTrigger that handles the intent named "HelpIntent".
var helpTrigger = new OnIntent("HelpIntent");

// Create a list of actions to execute when the trigger named "helpTrigger" fires.
var helpActions = new List<Dialog>();
helpActions.Add(new SendActivity("Hello, I'm the samples bot. At the moment, I respond to only help!"));
helpTrigger.Actions = helpActions;

// Add the trigger "helpTrigger" to root dialog
rootDialog.Triggers.Add(helpTrigger);
```

**Note:** You can use the `OnIntent` to also trigger on 'entities' generated by a recognizer, but it has to be within the context of an OnIntent.

##### OnUnknownIntent

Use this trigger to catch and respond to a case when a 'recognizedIntent' event was not caught and handled by any of the other trigger. This is especially helpful to capture and handle cases where your dialog wishes to participate in consultation.

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

### Dialog event triggers

<!-----Need summary All triggers based on the OnDialogEvent trigger -------->

| Trigger Name       | Description                          | Base event       | Condition                              |
| ------------------ | ------------------------------------ | ---------------- | -------------------------------------- |
| OnAssignEntity     | Occurs when an entity should be assigned to a property. | AssignEntity  | N/A                    |
| OnBeginDialog      | Occurs when a new dialog start       | BeginDialog      | N/A                                    |
| OnCancelDialog     | On dialog cancelled                  | CancelDialog     | N/A                                    |
| OnChooseEntity     | Occurs when there are multiple possible resolutions of an entity. | ChooseEntity | N/A           |
| OnChooseProperty   | Occurs when there are multiple possible entity to property mappings. | ChooseProperty | N/A      |
| OnClearProperty    | Occurs any time a property should be cleared. | ClearProperty | N/A                              |
| OnCustomEvent      | Handles custom events raised via EmitEvent | N/A        | N/A                                    |
| OnError            | On error event                       | Error            | N/A                                    |
| OnRepromptDialog   | On reprompt                          | RepromptDialog   | N/A                                    |

### Dialog event trigger examples

<!-----Need example - probably OnBeginDialog-------->

### Activity event triggers

Activity triggers enable you to associate actions to any incoming activity from the client, more information on activities can be found in [Bot Framework Activity schema][3].

| Trigger Name                 | Description                          | Base event       | Condition                              |
| ---------------------------- | ------------------------------------ | ---------------- | -------------------------------------- |
| OnActivity                   | Generic activity handler             | ActivityRecieved | N/A                                    |
| OnConversationUpdateActivity | Conversation update activity handler | ActivityRecieved | ActivityTypes.ConversationUpdate       |
| OnEndOfConversationActivity  | End of conversation activity handler | ActivityRecieved | ActivityTypes.EndOfConversation        |
| OnEventActivity              | Event activity handler               | ActivityRecieved | ActivityTypes.Event                    |
| OnHandoffActivity            | Hand off activity handler            | ActivityRecieved | ActivityTypes.Handoff                  |
| OnInvokeActivity             | Invoke activity handler              | ActivityRecieved | ActivityTypes.Invoke                   |
| OnMessageActivity            | Message activity handler             | ActivityRecieved | ActivityTypes.Message                  |
| OnMessageDeletionActivity    | Message deletion activity handler    | ActivityRecieved | ActivityTypes.MessageDeletion          |
| OnMessageReactionActivity    | Message reaction activity handler    | ActivityRecieved | ActivityTypes.MessageReaction          |
| OnMessageUpdateActivity      | Message update activity handler      | ActivityRecieved | ActivityTypes.MessageUpdate            |
| OnTypingActivity             | Typing activity handler              | ActivityRecieved | ActivityTypes.Typing                   |

#### Activity event trigger examples

##### OnConversationUpdateActivity

The `OnConversationUpdateActivity` trigger is one of the triggers that enable you to handle an 'ActivityRecieved' event. The `OnConversationUpdateActivity` trigger will only fire when the following condition is met: _ActivityTypes.ConversationUpdate_.

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

## Additional Information

* [Introduction to adaptive dialogs][1]
* [Dialog libraries][5]
* [Actions in adaptive dialogs][2]


[1]:bot-builder-adaptive-dialog-Introduction.md
[2]:bot-builder-adaptive-dialog-Actions.md
[3]:https://github.com/Microsoft/botframework-sdk/blob/master/specs/botframework-activity/botframework-activity.md
[4]:https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/common-expression-language
[5]:bot-builder-concept-dialog.md