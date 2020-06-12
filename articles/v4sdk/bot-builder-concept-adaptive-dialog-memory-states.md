---
title: Managing state in Adaptive Dialogs
description: Managing state in adaptive dialogs
keywords: bot, Managing state, User scope, Conversation scope, Dialog scope, Settings scope, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 05/08/2020
---

# Managing state in adaptive dialogs

The terms _Stateful_ and _stateless_ are adjectives that describe whether an application is designed to remember one or more preceding events in a given sequence of interactions with a user (or any other activity). Stateful means the application _does_ keep track of the state of its interactions, usually by saving values in memory in the form a properties. Stateless means the application does _not_ keep track of the state of its interactions, which means that there is no memory of any previous interactions and all incoming request must contain all relevant information that is required to perform the requested action. You can think of _state_ as the bot's current set of values or contents, such as the conversation ID or the active user's name.

The Bot Framework SDK follows the same paradigms as modern web applications and does not actively manage state, however the Bot Framework SDK does provide some abstractions that make state management much easier to incorporate into your bot. This topic is covered in detail in the Bot Framework SDK [Managing state][managing-state] article, it is recommended that you read and understand the information covered in that article before reading this article.

## Prerequisites

* A general understanding of [How bots work][bot-builder-basics].
* A general understanding of adaptive dialogs. For more information, see [Introduction to adaptive dialogs][introduction] and [Dialog libraries][concept-dialog].
* See the Bot Framework SDK [Managing state][managing-state] article for an overview of state management.

## Managing state

A bot is inherently stateless. For some bots, the bot can either operate without additional information, or the information required is guaranteed to be contained within the incoming activity. For other bots, state (such as where in the conversation we are or the response received from the user) is necessary for the bot to have a useful conversation.

The Bot Framework SDK defines memory scopes to help developers store and retrieve values in the bot's memory, for use when processing loops and branches, when creating dynamic messages, and other behaviors in the bot.

This makes it possible for bots built using the Bot Framework SDK to do things like:

* Pass information between dialogs.
* Store user profiles and preferences.
* Remember things between sessions such as the last search query or the last selection made by the user.

Adaptive dialogs provide a way to access and manage memory and all adaptive dialogs use this model, meaning that all components that read from or write to memory have a consistent way to handle information within the appropriate scopes.

> [!NOTE]
> All memory properties, in all memory scopes, are property bags, meaning you can add properties to them as needed.

Here are the different memory scopes available in adaptive dialogs:

* [User scope](#user-scope)
* [Conversation scope](#conversation-scope)
* [Dialog scope](#dialog-scope)
  * [Dialog sub-scopes](#dialog-sub-scopes)
* [Turn scope](#turn-scope)
  * [Turn sub-scopes](#turn-sub-scopes)
* [Settings scope](#settings-scope)
* [This scope](#this-scope)
* [Class scope](#class-scope)
* [Memory short-hand notations](#memory-short-hand-notations)

> [!TIP]
> All property paths are case-insensitive. For example, `user.name` is the same as `user.Name`. Also, if you do not have a property named `user.name` and you create a property named `user.name.first` the `user.name` object will automatically be created for you.

## User scope

User scope is persistent data scoped to the ID of the user you are conversing with.  

Examples:

    user.name
    user.address.city

## Conversation scope

Conversation scope is persistent data scoped to the ID of the conversation you are having.  

Examples:

    conversation.hasAccepted
    conversation.dateStarted
    conversation.lastMaleReference
    conversation.lastFemaleReference
    conversation.lastLocationReference

In the following example demonstrates how you might use the conversation scope to gather input from the user, creating a new `PropertyAssignment` object for use in the `SetProperties` [action][setproperties-action], getting the value from the conversation scope.

```csharp
new PropertyAssignment()
{
    Property = "conversation.flightBooking.departureCity",
    Value = "=turn.recognized.entities.fromCity.location"
},
```

## Dialog scope

Dialog scope persists data for the life of the associated dialog, providing memory space for each dialog to have internal persistent bookkeeping. Dialog scope is cleared when the associated dialog ends.

Dialog scope shorthand examples:

```markdown
The shorthand for `dialog.orderStarted`:

    $orderStarted

The shorthand for `dialog.shoppingCart`:

    $shoppingCart
```

All options passed into `BeginDialog` when creating a new adaptive dialog become properties of that dialog and can be accessed as long as it is in scope. You access these properties by name: dialog.\<propertyName>. For example, if the caller passed {a : '1', b: '2'} then they will be set as dialog.a and dialog.b.

### Dialog sub-scopes

All trigger actions in an adaptive dialog have their own sub-scopes and are accessed by name, for example the `Foreach` action is accessed as `dialog.Foreach`. By default, the index and value are set in the `dialog.foreach` scope, which can be accessed as `dialog.Foreach.index` and `dialog.Foreach.value`. You can read more about the `Foreach` action in [Actions in adaptive dialogs][foreach-action].

## Turn scope

The turn scope contains _non-persistent_ data that is only scoped for the current turn. The turn scope provides a place to share data for the lifetime of the current turn.  

### This scope example

    turn.bookingConfirmation
    turn.activityProcessed

### Turn sub-scopes

#### turn.activity

Each incoming [activity][botframework-activity] to the bot is available via `turn.activity` scope.

For example, you might have something like this defined in our .lg file to respond to a user that entered an invalid value when prompted for their age:

```.lg
Sorry, I do not understand '${turn.activity.text}'. ${GetAge()}
```

Or setting property values in your dialogs source code:

```c#
ItemsProperty = "turn.activity.membersAdded"
```

#### turn.recognized

All intents and entities returned from a [recognizer][recognizers] on any given turn, are automatically set in the `turn.recognized` scope and remain available until the next turn occurs. the `turn.recognized` scope has three properties:

* `turn.recognized.intents.xxx`: A list of the top intents classified by the recognizer for that turn.
* `turn.recognized.entities.xxx`: A list of entities recognized that turn.
* `turn.recognized.score`: The _confidence score_ of the top scoring intent for that turn.

For example, a flight booking application might have a book flight intent with an entity for departure destinations and another entity for arrival destinations, the example below demonstrates how to capture the departure destination value before the turn ends.

```c#
Value = "=turn.recognized.entities.fromCity.location"
```

There is another way to accomplish this using a [Memory short-hand notation](#memory-short-hand-notations)

```c#
// Value is a property containing an expression. @entityName is shorthand to refer to the value of
// the entity recognized. @fromCity.location is same as turn.recognized.entities.fromCity.location
Value = "=@fromCity.location"
```

#### turn.dialogEvent

`turn.dialogEvent` contains the payload of an event raised either by the system or your code. You can access the information contained in the payload by accessing the turn.dialogEvent.\<eventName\>.value scope.

#### turn.lastResult

 You can access the results from the last dialog that was called from the `turn.lastResult` scope.

#### turn.activityProcessed

`turn.activityProcessed`, a boolean property which if set means that `turnContext.activity` has been consumed by some component in the system.

#### turn.interrupted

`turn.interrupted`, a Boolean property; `true` indicates that an interruption has occurred.

## Settings scope

This represents any settings that are made available to the bot via the platform specific settings configuration system, for example if you are developing your bot using C#, these settings will appear in the appsettings.json file, if you are developing your bot using JavaScript, these settings will appear in the .env file or the config.py file when developing with Python. Additionally, some settings are contained in the dynamic environment settings in Azure, all are available in the settings scope..

### Settings scope example
<!--TODO P2: rewrite this with language tabs for C#/JS. -->
This is an example of an appsettings.json file that holds configuration settings for your bot:

```json
{
    "MicrosoftAppId": "<yourMicrosoftAppId>",
    "MicrosoftAppPassword": "<yourMicrosoftAppPassword>",
    "QnAMaker": {
        "knowledgebaseId": "<yourQnAKnowledgebaseId>",
        "hostname": "https://<YourHostName>.azurewebsites.net/qnamaker",
        "endpointKey": "yourEndpointKey"
    }
}
```

This is an example of how you would refer to the configuration settings that are stored in your appsettings.json file using the settings memory scope:

```csharp
var recognizer = new QnAMakerRecognizer()
{
    HostName = settings.QnAMaker.hostname,
    EndpointKey = settings.QnAMaker:endpointKey,
    KnowledgeBaseId = settings.QnAMaker:knowledgebaseId,
};
```

## This scope

The `this` scope pertains the active action's property bag. This is helpful for input actions since their life type typically lasts beyond a single turn of the conversation.

* `this.value` holds the current recognized value for the input.
* `this.turnCount` holds the number of times the missing information has been prompted for this input.

This example shows a common usage in a bots startup class:

### This scope example

```csharp
new TextInput()
{
    property = "user.name",
    prompt = new ActivityTemplate("what is your name?"),
    defaultValue = "Human",
    defaultValueResponse = new ActivityTemplate("Sorry, I still am not getting it after ${this.turnCount} attempts. For now, I'm setting your name to '${class.defaultValue}' for now. You can always say 'My name is <your name>' to re-introduce yourself to me.")
}
```

## Class scope

This holds the instance properties of the active dialog. you reference this scope as follows: `${class.<propertyName>}`.

### Class scope example

```csharp
new TextInput()
{
  Property = "user.age"
  Prompt = new ActivityTemplate("What is your age?"),
  DefaultValue = "30",
  DefaultValueResponse = new ActivityTemplate("Sorry, I'm not getting it in spite of you trying ${this.turnCount} number of times. \n I'm going with ${class.defaultValue} for now.")
}
```

## Memory short-hand notations

There are few short-hand notations supported to access specific memory scopes.

| Symbol | Usage           | Expansion                             | Notes                                                                                                                   |
|--------|-----------------|---------------------------------------|------------------------------------------------------------------------------------------------------------------------ |
| $      | `$userName`     | `dialog.userName`                     | Short hand notation that represents the dialog scope.                                                                   |
| #      | `#intentName`   | `turn.recognized.intents.intentName`  | Short hand used to denote a named intent returned by the recognizer.                                                    |
| @      | `@entityName`   | `turn.recognized.entities.entityName` | `@entityName` returns the first and _only_ the first value found for the entity, immaterial of the value's cardinality. |
| @@     | `@@entityName`  | `turn.recognized.entities.entityName` | `@@entityName` will return the actual value of the entity, preserving the value's cardinality.                          |
| %      | `%propertyName` | `class.propertyName`                  | Used to refer to instance properties (e.g. `MaxTurnCount`, `DefaultValue` etc).                                         |

[bot-builder-basics]:bot-builder-basics.md
[introduction]:bot-builder-adaptive-dialog-introduction.md
[managing-state]:bot-builder-concept-state.md
[recognizers]:bot-builder-concept-adaptive-dialog-recognizers.md
[botframework-activity]:https://github.com/microsoft/botbuilder/blob/master/specs/botframework-activity/botframework-activity.md
[foreach-action]:../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#foreach
[setproperties-action]:../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#setproperties
[concept-dialog]:bot-builder-concept-dialog.md
