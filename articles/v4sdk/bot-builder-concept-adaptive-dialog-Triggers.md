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
---

# Events and triggers in adaptive dialogs

Adaptive dialogs introduce a new event based approach to model conversations. Any sub-system in your bot can emit events and all adaptive dialogs contain one or more event handlers called _triggers_ that enable you to react to these events.  Any time an event fires, the active adaptive dialog's triggers are evaluated and if any trigger matches the current event, the [actions][2] associated with that trigger execute. If an event is not handled in the active dialog, it will be passed up to its parent dialog to be evaluated. This process continues until it is either handled or reaches the bots root dialog. If no event handler (_trigger_) is found, the event will be ignored and no action will be taken.

<!--TODO P2: preBubble/consultation/postBubble phases https://github.com/MicrosoftDocs/bot-docs-pr/pull/2109#discussion_r418164608 --->

## Prerequisites

* [how bots work][10]
* [Introduction to adaptive dialogs][1]
* [dialog libraries][5]

## Anatomy of a trigger

The bot framework SDK provides various pre-defined triggers designed to handle common event types.  For example the `OnIntent` trigger fires anytime the [recognizer][8] detects an [intent][6]. If you are using a [LUIS][7] recognizer it will also return a [prediction score][9] that measures the degree of confidence LUIS has for its prediction results. In order to increase the reliability and accuracy of your bot, you may only want to execute the `OnIntent` trigger if the confidence rating is 80% or higher. You can accomplish this by adding a _condition_. Triggers all contain an optional `Condition` property that when defined, must evaluate to _true_ in order for the trigger to execute. The `Condition` property is a string, but must contain a valid [adaptive expression][4] to work. The above examples `Condition` property would look something like: `Condition = "#<IntentName>.Score >= 0.8"`. Adaptive expressions enable sophisticated conditions that can handle virtually any scenario that you might have.

All triggers also contain a list of _Actions_. Actions contain the code that will execute when an event occurs.  This is the heart of the trigger. You can learn more about actions and what built in actions are provided in the bot framework SDK in the article [Actions in adaptive dialogs][2].

## Trigger types

_Triggers_ enable you to catch and respond to events. The broadest trigger from which all other triggers are derived is the `OnCondition` trigger that allows you to catch and attach a list of actions to execute when a specific event is emitted by any of the bots sub-systems.

Triggers are listed in the following sections, categorized and grouped by trigger type.

Each table in the following sections list all of the triggers supported by adaptive dialogs that are currently in the SDK as well as the events they are based on.

### Base trigger

The `OnCondition` trigger is the base trigger that all triggers derive from. When defining triggers in an adaptive dialog they are defined as a list of `OnCondition` objects as demonstrated in the following example:

```csharp
Triggers = new List<OnCondition>()
{
    ...
}
```

### Recognizer event triggers

[Recognizers][8] extract meaningful pieces of information from a user's input in the form of _intents_ and _entities_ and when they do, they emit events. For example the `recognizedIntent` event fires when the recognizer picks up an intent (or extracts entities) from a given user utterance. You handle these events using the _recognizer event triggers_.

| Event cause               | Trigger name  | Base event    | Description                                                       |
| ------------------------- | ------------- | ------------- | ----------------------------------------------------------------- |
| Assign entity to property | OnAssignEntity| RecognizedIntent | Triggered to assign an entity to a property.                      |
| Choose entity             | OnChooseEntity| RecognizedIntent | Occurs when there are multiple possible resolutions of an entity. |
| Choose Intent | OnChooseIntent |ChooseIntent | This trigger is run when ambiguity has been detected between intents from multiple recognizers in a [CrossTrainedRecognizerSet][11].|
| Intent recognized| OnIntent | RecognizedIntent | Actions to perform when specified intent is recognized.           |
|QnAMatch intent|OnQnAMatch| RecognizedIntent |This trigger is run when the [QnAMakerRecognizer][12] has returned a QnAMatch intent. The entity @answer will have the QnAMaker answer.|
|Unknown intent recognized|[OnUnknownIntent](#Recognizer-trigger-examples)| UnknownIntent | Actions to perform when user input is unrecognized or no match is found in any of the `OnIntent` triggers. |

#### Recognizer trigger examples

Examples of `OnIntent` and `OnUnknownIntent` triggers are given in the example below to demonstrate the use of the Recognizer triggers.

> [!NOTE]
>
> * The `OnIntent` trigger enables you to handle the 'recognizedIntent' event. The 'recognizedIntent' event is raised by the [recognizer][8]. All of the bot framework SDK built-in recognizers emit this event when they successfully identify a user _input_ so that your bot can respond appropriately.
> * Use the `OnUnknownIntent` trigger to catch and respond when a 'recognizedIntent' event was not caught and handled by any of the other trigger. <!--This is especially helpful to capture and handle cases where your dialog wishes to participate in consultation.-->

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

### Dialog events

The dialog triggers handle dialog specific events which are related to the "lifecycle" of the dialog.  There are currently 6 dialog triggers in the bot framework SDK and they all derive from the `OnDialogEvent` class.

> You should use _dialog triggers_ to:
>
> * Take action immediately when the dialog starts, even before the recognizer is called.
> * Take actions when a "cancel" event occurs.
> * Take actions on messages received or sent.
> * Evaluate and take action based on the content of an incoming activity.

| Trigger name     | Base event   | Description                                                                    |
| ---------------- | ------------ | ------------------------------------------------------------------------------ |
| OnBeginDialog    | BeginDialog  | Actions to perform when this dialog begins.                                    |
| OnCancelDialog   | CancelDialog | Actions to perform on cancel dialog event (when this dialog ends).             |
| OnChooseProperty |ChooseProperty| This event occurs when there are multiple possible entity to property mappings.|
| OnEndOfActions   | EndOfActions | This event occurs once all actions and ambiguity events have been processed.   |
| OnError          | Error        | Action to perform when an 'Error' dialog event occurs.                         |
| OnRepromptDialog |RepromptDialog| Actions to perform when 'RepromptDialog' event occurs.                         |

<!--| Clear Property  |  OnClearProperty  | This event occurs any time a property needs to be be cleared.                  |-->

> [!TIP]
> Most dialogs include an `OnBeginDialog` trigger that responds to the `BeginDialog` event. This trigger automatically fires when the dialog begins, which can allow the bot to respond immediately with a [welcome message](#dialog-event-trigger-examples) or a [prompt for user input][14] etc.

#### Dialog event trigger example

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

### Activity events

Activity triggers enable you to associate actions to any incoming activity from the client such as when a new user joins and the bot begins a new conversation. Additional information on activities can be found in [Bot Framework Activity schema][3].

All activity events have a base event of `ActivityReceived` and are further refined by their `ActivityType`. The Base class that all activity triggers derive from is `OnActivity`.

| Event cause         | ActivityType | Trigger name                 | Description                                                                       |
| ------------------- | ------------ | ---------------------------- | --------------------------------------------------------------------------------- |
| Greeting            | ConversationUpdate | OnConversationUpdateActivity | Handle the events fired when a user begins a new conversation with the bot. |
| Conversation ended  | EndOfConversation | OnEndOfConversationActivity  | Actions to perform on receipt of an activity with type 'EndOfConversation'.  |
| Event received      | Event        | OnEventActivity              | Actions to perform on receipt of an activity with type 'Event'.                   |
| Handover to human   | Handoff      | OnHandoffActivity            | Actions to perform on receipt of an activity with type 'HandOff'.                 |
| Conversation invoked| Invoke       | OnInvokeActivity             | Actions to perform on receipt of an activity with type 'Invoke'.                  |
| User is typing      | Typing       | OnTypingActivity             | Actions to perform on receipt of an activity with type 'Typing'.                  |

#### Activity events examples

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

### Message events

<!-- Should the Message events section be combined with the Activity events section? -->

**Message event** triggers allow you to react to any message event such as when a message is updated (`MessageUpdate`) or deleted (`MessageDeletion`) or when someone reacts (`MessageReaction`) to a message (for example, some of the common message reactions include a Like, Heart, Laugh, Surprised, Sad and Angry reactions).

Message events are a type of activity event and as such, all message events have a base event of `ActivityReceived` and are further refined by ActivityType. The Base class that all message triggers derive from is `OnActivity`.

| Event cause      | ActivityType    | Trigger name             | Description                                                                                         |
| ---------------- | --------------- | ------------------------ | --------------------------------------------------------------------------------------------------- |
| Message received | Message         | OnMessageActivity        | Actions to perform on receipt of an activity with type 'MessageReceived'. Overrides Intent trigger. |
| Message deleted  | MessageDeletion | OnMessageDeleteActivity  | Actions to perform on receipt of an activity with type 'MessageDelete'.                             |
| Message reaction | MessageReaction | OnMessageReactionActivity| Actions to perform on receipt of an activity with type 'MessageReaction'.                           |
| Message updated  | MessageUpdate   | OnMessageUpdateActivity  | Actions to perform on receipt of an activity with type 'MessageUpdate'.                             |

<!--TODO P1: Need Message event examples
#### Message event examples

--->

### Custom events

You can emit your own events by adding the [EmitEvent][13] action to any trigger, then you can handle that custom event in any trigger in any dialog in your bot by defining a _custom event_ trigger. A custom event trigger is the `OnDialogEvent` trigger that in effect becomes a custom trigger when you set the `Event` property to the same value as the EmitEvent's `EventName` property.

> [!TIP]
> You can allow other dialogs in your bot to handle your custom event by setting the EmitEvent's `BubbleEvent` property to true.

| Event cause  | Trigger name  | Base event          | Base class  | Description                                                                                                   |
| ------------ | ------------- | ------------------- | ----------- | ------------------------------------------------------------------------------------------------------------- |
| Custom event | OnDialogEvent | See [EmitEvent][13] | OnCondition | Actions to perform when a custom event is detected. Use 'Emit a custom event' action to raise a custom event. |

<!--Was: OnCustomEvent-->

#### Custom event example

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

* [Introduction to adaptive dialogs][1]
* [Dialog libraries][5]
* [Actions in adaptive dialogs][2]

[1]:bot-builder-adaptive-dialog-Introduction.md
[2]:bot-builder-adaptive-dialog-Actions.md
[3]:https://github.com/Microsoft/botframework-sdk/blob/master/specs/botframework-activity/botframework-activity.md
[4]:https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/common-expression-language
[5]:bot-builder-concept-dialog.md
[6]:https://aka.ms/bot-service-add-luis-to-bot
[7]:https://www.luis.ai/home
[8]:bot-builder-adaptive-dialog-recognizer.md
[9]:https://aka.ms/luis-prediction-scores
[10]:https://aka.ms/how-bots-work#the-activity-processing-stack
[11]:bot-builder-adaptive-dialog-recognizer.md#Cross-Trained-Recognizer
[12]:bot-builder-adaptive-dialog-recognizer.md#QnAMaker-Recognizer
[13]:bot-builder-adaptive-dialog-Actions.md#EmitEvent
[14]:bot-builder-adaptive-dialog-input.md
