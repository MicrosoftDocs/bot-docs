---
title: API Reference for the context object | Microsoft Docs
description: Learn how to reference the context object in your Conversation Designer bot.
author: vkannan
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/11/2017
ROBOTS: NoIndex, NoFollow
---
# API Reference
> [!IMPORTANT]
> Conversation Designer is not available to all customers yet. More details on
> availability of Conversation Designer will come later this year.

Conversation Designer gives you the ability to add custom business logic to your bot. These script functions are implemented in the **Scripts** editor. The functions are hooked up in various places like the conditional response templates, the task's *triggers* or *actions*, or the dialogue states and they all receives a `context` object that is of type **[IConversationContext]** in their function parameters.

The following code sample shows the signature for a conditional response function with the **context** object passed in.

```javascript
/**
* @param {IConversationContext} context
*/
module.exports.NewConditionalResponse_onRun = function(context) {
    // Business logic here
    return true; // Returns a boolean
}

```

Through the `context` object, you can access information regarding the conversation between the user and the bot.

## Script callback functions

The custom script callback functions you create may take many forms. While you can give them different names, functionally, they take on one of the following forms.

| Form | Parameter | Return type | Description |
| ---- | ---- | ---- | ---- |
| Before response function | context | **void** or **promise** | Function that is executed before a response is given. |
| Process function | context | **void** or **promise** | Function that performs business logic. |
| Decision function | context | **string** or **promise** | Function that makes decisions based on business logic. The return string should match a condition from a [decision](conversation-designer-dialogues.md#decision-state) block. |
| Code recognizer function | context | **boolean** or **promise** | Custom business logic that gets run when a **Script trigger** occurs. Return `true` to indicate a match. Otherwise, return `false` to cancel the match. |
| onRecognize function | context | **boolean** or **promise** | Function that executes only if there is a match from a LUIS recognizer. Use this callback function to process LUIS entities and return an appropriate **boolean** value. Return `true` to indicate a match. Otherwise, return `false` to cancel the match. |

## IConversationContext interface

The `IConversationContext` interface tracks conversation information between the user and the bot. All custom functions that are hooked up through the Conversation Designer will receive a `context` object as parameter argument.

## Context properties
The `context` object exposes the following properties.

| Name |  Code | Description |
| ---- | ---- | ---- |
| `request` | `context.request` | Get the request object that contains the bot's activity.  |
| | `context.request.attachment` | An attachment activity that may contains an adaptive card. |
| | `context.request.text` | A text activity that contains the incoming text message from the client. |
| | `context.request.speak` | A speak activity that contains the spoken text (if available) from the client. |
| | `context.request.type` | Specifies the activity type (Default: "message"). |
| `responses` | `context.responses` | Maintains an array of activities that will be sent back to the client at the end of the current state or code behind execution. |
| | `context.responses.push` | Add an activity to the response. |
| `global` | `context.global` | A JavaScript object that contains conversational data you defined. This object persists throughout the conversation. |
| `local` | `context.local` | A JavaScript object that contains task data you defined. This object persists for the duration of a specific task. LUIS intents are always returned to the local context. If you want to persist LUIS results, consider copying it to the `context.global` context. |
| | `context.local['@description']` | Returns the raw Entities received from LUIS. |
| `sticky` | `context.sticky` | Indicates the current task name |
| `currentTemplate` | `context.currentTemplate` | A [conditional response template](conversation-designer-response-templates.md#conditional-response-templates) that is called for both the display and speak evaluations. This object contains three properties: <br/>1. **name**: The name of the current template. <br/>2. **modalityDisplay**: A boolean that indicates the modality is associated with a display evaluation. <br/>3. **modalitySpeak**: A boolean that indicates the modality is associated with a speak evaluation. |

## Context methods
The `context` object exposes the following methods.
| Name | Return type | Code | Description |
| ---- | ---- | ---- | ---- |
| `getCurrentTurn` | **number** | `context.getCurrentTurn();` | Get the turn from the Frame on top of the stack if you are executing a reprompt. |

## Next step
> [!div class="nextstepaction"]
> [Create a bot](conversation-designer-create-bot.md)
