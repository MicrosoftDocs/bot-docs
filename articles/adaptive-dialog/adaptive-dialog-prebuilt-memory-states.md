---
title: Managing state in adaptive dialogs - reference guide
description: Describing memory scopes in adaptive dialogs
keywords: bot, managing state, memory scopes, user scope, conversation scope, dialog scope, settings scope, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 06/12/2020
monikerRange: 'azure-bot-service-4.0'
---

# Managing state in adaptive dialogs - reference guide

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article provides technical details that will help you work with memory scopes in adaptive dialogs. For an introduction to memory scopes and managing state in adaptive dialogs, see the [Conversation flow and memory][managing-state] Composer concept article.

> [!TIP]
> All property paths are case-insensitive. For example, `user.name` is the same as `user.Name`. Also, if you do not have a property named `user.name` and you create a property named `user.name.first` the `user.name` object will automatically be created for you.

## User scope

User scope is persistent data scoped to the ID of the user you are conversing with.  

Examples:

- `user.name`
- `user.address.city`

## Conversation scope

Conversation scope is persistent data scoped to the ID of the conversation you are having.  

Examples:

- `conversation.hasAccepted`
- `conversation.dateStarted`
- `conversation.lastMaleReference`
- `conversation.lastFemaleReference`
- `conversation.lastLocationReference`

## Dialog scope

Dialog scope persists data for the life of the associated dialog, providing memory space for each dialog to have internal persistent bookkeeping. Dialog scope is cleared when the associated dialog ends.

Dialog scope shorthand examples:

- The shorthand for `dialog.orderStarted` is `$orderStarted`.
- The shorthand for `dialog.shoppingCart` is `$shoppingCart`.

All options passed into `BeginDialog` when creating a new adaptive dialog become properties of that dialog and can be accessed as long as it is in scope. You access these properties by name: dialog.\<propertyName>. For example, if the caller passed {a : '1', b: '2'} then they will be set as dialog.a and dialog.b.

### Dialog sub-scopes

All trigger actions in an adaptive dialog have their own sub-scopes and are accessed by name, for example the `Foreach` action is accessed as `dialog.Foreach`. By default, the index and value are set in the `dialog.foreach` scope, which can be accessed as `dialog.Foreach.index` and `dialog.Foreach.value`.

## Turn scope

The turn scope contains _non-persistent_ data that is only scoped for the current turn. The turn scope provides a place to share data for the lifetime of the current turn.  

Examples:

- `turn.bookingConfirmation`
- `turn.activityProcessed`

### Turn sub-scopes

#### turn.activity

Each incoming [activity][botframework-activity] to the bot is available via `turn.activity` scope.

For example, you might have something like this defined in our .lg file to respond to a user that entered an invalid value when prompted for their age:

```lg
Sorry, I do not understand '${turn.activity.text}'. ${GetAge()}
```

#### turn.recognized

All intents and entities returned from a [recognizer][recognizers] on any given turn, are automatically set in the `turn.recognized` scope and remain available until the next turn occurs. the `turn.recognized` scope has three properties:

- `turn.recognized.intents.xxx`: A list of the top intents classified by the recognizer for that turn.
- `turn.recognized.entities.xxx`: A list of entities recognized that turn.
- `turn.recognized.score`: The _confidence score_ of the top scoring intent for that turn.

#### turn.dialogEvent

`turn.dialogEvent` contains the payload of an event raised either by the system or your code. You can access the information contained in the payload by accessing the turn.dialogEvent.\<eventName\>.value scope.

#### turn.lastResult

 You can access the results from the last dialog that was called from the `turn.lastResult` scope.

#### turn.activityProcessed

`turn.activityProcessed`, a boolean property which if set means that `turnContext.activity` has been consumed by some component in the system.

#### turn.interrupted

`turn.interrupted`, a Boolean property; `true` indicates that an interruption has occurred.

## Settings scope

This represents any settings that are made available to the bot via the platform specific settings configuration system, for example if you are developing your bot using C#, these settings will appear in the appsettings.json file<!--, if you are developing your bot using JavaScript, these settings will appear in the .env file or the config.py file when developing with Python. Additionally, some settings are contained in the dynamic environment settings in Azure, all are available in the settings scope-->.

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

## This scope

The `this` scope pertains the active action's property bag. This is helpful for input actions since their life type typically lasts beyond a single turn of the conversation.

- `this.value` holds the current recognized value for the input.
- `this.turnCount` holds the number of times the missing information has been prompted for this input.

## Class scope

This holds the instance properties of the active dialog. you reference this scope as follows: `${class.<propertyName>}`.

## Additional information

- For an introduction to managing state in Composer, see the [Conversation flow and memory][managing-state] Composer concept article.

[managing-state]: /composer/concept-memory
[foreach-action]: ./adaptive-dialog-prebuilt-actions.md#foreach
[botframework-activity]: https://github.com/microsoft/botframework-sdk/blob/master/specs/botframework-activity/botframework-activity.md
[recognizers]: ../v4sdk/bot-builder-concept-adaptive-dialog-recognizers.md
