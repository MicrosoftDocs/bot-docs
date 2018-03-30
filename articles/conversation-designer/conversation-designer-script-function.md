---
title: Define a script function as a Do action | Microsoft Docs
description: Learn how to set up script action function as Do action.
author: vkannan
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 12/13/2017
ROBOTS: NoIndex, NoFollow
---

# Define a script function as a Do action

[Script action functions](conversation-designer-context-object.md#script-callback-functions) execute custom script you define to help complete the task. For example, calling into a service to actually set the thermostat to 74 degrees when the user says "Set my thermostat to 74 degrees." 

To use custom script as the action, select **Script action** as the **DO** action type in the task editor. Then enter the name of the function that implements the action. Click **Edit** to bring up the **Scripts** editor and write your implementation for this function. 

## Script action function parameter

The specified action callback function will always be called with the [`context`](conversation-designer-context-object.md) object.

The `context` object includes both `taskEntities` and `contextEntities`. Task entities are entities defined or generated for this task and context entities are a property bag where entities can be preserved across conversations with the user.

## Return value
**Script action** functions are expected to return a boolean.

## Sample script action function
The following sample function makes an HTTP call to get a response before returning a trigger matched.

```javascript
module.exports.NewTask_do_onRun = function(context) {
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
> [Dialogue action](conversation-designer-dialogues.md)
