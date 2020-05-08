---
title: Memory scopes and managing state in Adaptive Dialogs
description: Collecting user input using adaptive dialogs
keywords: bot, Managing state, User scope, Conversation scope, Dialog scope, Settings scope, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 05/08/2020
---

# Memory scopes and managing state in Adaptive Dialogs

State within a bot follows the same paradigms as modern web applications, and the Bot Framework SDK provides some abstractions to make state management easier. This topic is covered in detail in the bot framework SDK [Managing state][1] article, you need to read and understand the information covered in that article to make the best use of this article which covers additioanl information that you will need to know when creating adaptive dialogs.

## Prerequisites

* A general understanding of [how bots work][1] in the Bot Framework V4 SDK is helpful.
* A general understanding of adaptive dialogs in the Bot Framework V4 SDK is helpful. For more information, see [Introduction to adaptive dialogs][2] and [Dialog libraries][3].
* See the bot framework SDK [Managing state][1] article for an overview of state management in BotBuilder V4 SDK.

## Managing state

As with web apps, a bot is inherently stateless; a different instance of your bot may handle any given turn of the conversation. For some bots, this simplicity is preferred - the bot can either operate without additional information, or the information required is guaranteed to be within the incoming message. For others, state (such as where in the conversation we are or previously received data about the user) is necessary for the bot to have a useful conversation.

[Adaptive dialogs][2] provides a way to access and manage memory. All adaptive dialogs by default use this model, meaning that all components that read from or write to memory have a common way to access and write information to the appropriate scopes.

All memory properties, in all memory scopes, are property bags, meaning you can store arbitrary information on them.

See [here][3] for guidance on when to use each scope.

Here are the different memory scopes available:

* [Managing state](#managing-state)
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

## User scope

User scope is persistent data scoped to the id of the user you are conversing with.  

Examples:

    user.name
    user.address.city

## Conversation scope

Conversation scope is persistent data scoped to the id of the conversation you are having.  

Examples:

    conversation.hasAcceptedTOU
    conversation.dateStarted
    conversation.lastMaleReference
    conversation.lastFemaleReference
    conversation.lastLocationReference

## Dialog scope

Dialog scope is persistent data scoped for a giving executing dialog, providing an memory space for each dialog to have internal persistent bookkeeping. Dialog scope is cleared when dialog ends. You can access properties available under the `dialog.XXX` scope via `$XXX`. `$` is a shorthand for `dialog` memory scope.

Examples:

```markdown
    dialog.orderStarted

    > same as
    $orderStarted

    dialog.shoppingCart

    > same as
    $shoppingCart
```

### Dialog sub-scopes

* `dialog.options` scope by default carry parameters/ input to the specific dialog being executed. 
* `dialog.foreach` scope by default carry dialog.foreach.value and dialog.foreach.index and are available to actions within a `forEach` action. 

## Turn scope

Turn scope is *non-persistent* data scoped for *only the current turn*, providing a place to share data for the lifetime of the current turn.  

Examples:

    turn.bookingConfirmation
    turn.activityProcessed

### Turn sub-scopes

* `turn.activity` - Each incoming [activity][5] to the bot is available via turn.activity scope.
* `turn.recognized` For each turn of the conversation, if a [recognizer][4] is run, then the output intents and entities from that recognizer are automatically set.
  * `turn.recognized.intents.xxx` - path to the top intent classified by the recognizer for that turn.
  * `turn.recognized.entities.xxx` - path to entities recognized for that turn.
  * `turn.recognized.score` - path to the top scoring intent's confidence score.
* `turn.dialogEvent` - The payload of an event raised by the system (or via user code) is available under turn.dialogEvent.\<eventName\>.value scope.
* `turn.lastResult` - The result from the last dialog that was called.
* `turn.activityProcessed` - Bool property which if set means that the turnContext.activity has been consumed by some component in the system.
* `turn.interrupted` - set to true if an interruption has occurred.

## Settings scope

This represents any settings that are made available to the bot via the platform specific settings configuration system - e.g. appsettings.json, .env, dynamic environment setting in Azure etc.

### Settings scope example

This is an example of an appsettings.json that holds configuration settings for your bot:

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

`This` scope pertains the active action's property bag. This is helpful for input actions since their life type typically lasts beyond a single turn of the conversation.

* `this.value` holds the current recognized value for the input.
* `this.turnCount` holds the number of times the missing information has been prompted for this input.

This example shows a common usage in a bots startup class:

```csharp
public Startup(IConfiguration configuration, IHostingEnvironment env)
{
    this.Configuration = configuration;
    this.HostingEnvironment = env;
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

| Symbol | Usage         | Expansion                           | Notes                                                                                                               |
|--------|---------------|-------------------------------------|---------------------------------------------------------------------------------------------------------------------|
| $      | $userName     | dialog.userName                     | Short hand notation that can be used to simply get or set a value on the dialog scope                               |
| #      | #intentName   | turn.recognized.intents.intentName  | Short hand used to denote a named intent returned by the recognizer                                                 |
| @      | @entityName   | turn.recognized.entities.entityName | @entityName will always **only** return the first value found for the entity immaterial of the value's cardinality  |
| @@     | @@entityName  | turn.recognized.entities.entityName | @@entityName will return the actual value of the entity, preserving the value's cardinality                         |
| %      | %propertyName | class.propertyName                  | Used to refer to instance properties (e.g. MaxTurnCount, DefaultValue etc)                                          |

[1]:https://docs.microsoft.com/azure/bot-service/bot-builder-concept-state?view=azure-bot-service-4.0
[2]:../README.md
[3]:https://docs.microsoft.com/azure/bot-service/bot-builder-concept-state?view=azure-bot-service-4.0#when-to-use-each-type-of-state
[4]:bot-builder-concept-adaptive-dialog-recognizers.md
[5]:https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md
