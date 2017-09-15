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

Conversation Designer gives you the ability to add custom business logic to your bot. These script functions are implemented in the **Scripts** editor. The functions are hooked up in various places like the conditional response templates, the task's *triggers* or *actions*, or the dialog states and they all receives a `context` object that is of type **[IConversationContext]** in their function parameters.

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
| Before response function | context | **void** | Function that is executed before a response is given. |
| Process function | context | **void** | Function that performs business logic. |
| Decision function | context | **string** | Function that makes decisions and conditional response based on business logic. |
| Code recognizer function | context | **boolean** | Custom business logic that gets run when a **Script trigger** occurs. |
| Prompt function | context | **boolean** | Function that executes as part of a prompt. |


## IConversationContext interface

The `IConversationContext` interface tracks conversation information between the user and the bot. All custom functions that are hooked up through the Conversation Designer will receive a `context` object as parameter argument.

## Context properties
The `context` object exposes the following properties.

| Name |  Code | Description |
| ---- | ---- | ---- |
| `contextEntities` | `context.contextEntities["entityName"][index].value` | Read-only. The **contextEntities** property persists until explicitly cleared or until the bot receives a new conversation (conversation update activity). |
| `taskEntities` | `context.taskEntities["entityName"][index].value` | Read-only. The **taskEntities** property are cleared at the end of the current task execution. |
| `flowState` | // Get the name of the  current task being executed. <br/> `context.flowState.task` <br/> // Get a handle to the stack for the current conversation. <br/> `context.flowState.stack` | The **flowState** maintains the *task* and *stack* state information for the current task. The *task* and *stack* states are of type [Entity](#entity-interface).  |
| | `context.flowState.stack.type` | Get the type of current dialog state being executed (e.g.: process, decision, feedback, etc.) |
| | `context.flowState.stack.dialogFlowName` | Get the name of the parent dialog flow if the current conversational state is in a dialog; otherwise, this property will be empty. |
| | `context.flowState.stack.stateName` | Get the name of the current sate being executed. |
| `output` | `context.output` | Read-only. The output from the code behind function. |
| `request` | `context.request` | Get the request object that contains the bot's activity.  |
| | `context.request.attachment` | An attachment activity that may contains an adaptive card. |
| | `context.request.text` | A text activity that contains the incoming text message from the client. |
| | `context.request.speak` | A speak activity that contains the spoken text (if available) from the client. |
| | `context.request.type` | Specifies the activity type (Default: "message"). |
| `response` | `context.response` | Maintains an array of activities that will be sent back to the client at the end of the current state or code behind execution. |
| | `context.response.push` | Add an activity to the response. |
| | `context.response.push` | Remove an activity from the top of the response array. |

## Context methods
The `context` object exposes the following methods.
| Name | Return type | Code | Description |
| ---- | ---- | ---- | ---- |
| `addContextEntity` | **IConversationContext** | `context.addContextEntity("entityName", "entityValue");` | Add a context entity. Context entities persists until explicitly cleared or the bot receives a new conversation (conversation update activity).|
| `addTaskEntity` | **IConversationContext** | `context.addTaskEntity("entityName", "entityValue");` | Add a task entity. Task entities are cleared at the end of the current task execution. |
| `containsContextEntity` | **boolean** | `context.containsContextEntity("entityName", "entityValue");` | Indicates if **contextEntities** contains the entity. |
| `containsTaskEntity` | **boolean** | `context.containsTaskEntity("entityName", "entityValue");` | Indicates if **taskEntities** contains the entity. |
| `getCurrentTurn` | **number**| `context.getCurrentTurn();` | Get the turn from the Frame on top of the stack if you are executing a reprompt. |
| `removeContextEntity` | **IConversationContext** | `context.removeContextEntity("entityName");` | Remove a context entity. |
| `removeTaskEntity` | **IConversationContext** | `context.removeTaskEntity("entityName");` | Remove a task entity. |

## Entity interface

The **Entity** interface exposes the following **read-only** properties.

| Name | Return type | Description |
| ---- | ---- | ---- |
| `type` | **string** | The entity type (e.g.: color, city, sport, etc...). |
| `value` | **string** | The value of the entity extracted from the utterance (e.g.: red, Seattle, Football). |
| `startIndex` | **number** | The starting index of the entity extracted from the utterance. |
| `endIndex` | **number** | The ending index of the entity extracted from the utterance. |
| `score` | **number** | The confidence score of an entity (e.g.: range between 0 to 1). |
| `resolution` | **EntityResolution** | A property bag with more information about an entity. Look at [builtin.datetime](https://docs.microsoft.com/en-us/azure/cognitive-services/luis/pre-builtentities) for an example of how the **score** is used. |

## Next step
> [!div class="nextstepaction"]
> [Create a bot](conversation-designer-create-bot.md)
