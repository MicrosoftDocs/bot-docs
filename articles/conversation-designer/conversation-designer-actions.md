---
title: Task actions | Microsoft Docs
description: Learn how to set up actions for Conversation Designer bots
author: vkannan
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/04/2017
ROBOTS: NoIndex, NoFollow
---
# Task actions

Actions represent how a bot should respond to a specific task trigger. Available options are:

- [Simple response](#simple-response): Defines a simple response with an optional adaptive card. 
- [Script action](#script-action): Write custom script to call back-end service to complete task.
- [Dialog](conversation-designer-dialogs.md): Build and execute a conversation model using a dialog flow.

## Simple response

To set up a simple response as the action, select **Simple response** from **DO** action drop down. You can then enter simple responses into the **Bot's response to user** field.

You can optionally include an [adaptive card](conversation-designer-adaptive-cards.md) with the simple response. You can also optionally provide custom script function to execute before sending the response to the user. These options are available to you while you create or edit a task. 

## Script action

[Script action functions](conversation-designer-context-object.md#script-callback-functions) execute custom script you define to help complete the task. For example, calling into a service to actually set the thermostat to 74 degrees when the user says "Set my thermostat to 74 degrees." 

To use custom script as the action, select **Script action** as the **DO** action type in the task editor. Then enter the name of the function that implements the action. Click **Edit** to bring up the **Scripts** editor and write your implementation for this function. 

## Script action function parameter

The specified action callback function will always be called with the [`context`](conversation-designer-context-object.md) object.

The `context` object includes both `taskEntities` and `contextEntities`. Task entities are entities defined or generated for this task and context entities are a property bag where entities can be preserved across conversations with the user.
<!-- TODO: Do we really mean across all conversations (as defined by a unique activity conversation ID) with a given user for this specific bot? 
Would be good to include detail on lifetime management and how/when to clear these.-->

<!-- ## Do action function expected return values
TODO TBD -->

## Return value
**Script action** functions are expected to return a boolean.

## Sample script action function
The following sample function makes an HTTP call to get a response before returning a trigger matched.

```javascript
export function NewTask_do_onRun(context) {
    var options =  {
        host: 'HOST',
        path: 'PATH',
        method: 'post',
        headers: {
            "HEADER1" : "VALUE"
        }, 
        body: {
            "BODY": VALUE
        }
    };
    
    return http.request(options).then(function(response) {
      // parse response payload
      // Send a message to user
      context.responses.push({text: "Done", type: "message"});
    });
} 

```

## Next step
> [!div class="nextstepaction"]
> [Task Dialogs](conversation-designer-dialogs.md)
