---
title: Define code recognizer as task trigger | Microsoft Docs
description: Learn how to use a custom code recognizer as a task trigger.
author: vkannan
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/04/2017
ROBOTS: NoIndex, NoFollow
---

# Define code recognizer as task trigger
> [!IMPORTANT]
> Conversation Designer is not available to all customers yet. More details on
> availability of Conversation Designer will come later this year.

Code recognizers enable you to write custom scripts to help perform a task. Regex-based expression evaluation or calling into other services can be used to help determine the user's intent. The custom script will instruct the conversation runtime to trigger a task. 

## Create a script function
To use a script as a recognizer, in the task editor, choose "Script function" as the recognizer, specify the name of the function, and Click **Create/view function** to start editing a script. You can also click **Create/view function** without specifying a script name and an empty function will be created for you with the default name. 

## Script trigger function parameter

The specified script callback function will always be called with the [`context`](conversation-designer-context-object.md) object.

The `context` object includes both `taskEntities` and `contextEntities`. Task entities are entities defined or generated for this task and context entities are property bags where entities can be preserved across conversations with the user.

## Return value

**Script trigger** functions are expected to return a boolean.

## Sample regex-based recognizer
The following code sample uses regex to process the request and returns a boolean so the conversation runtime can determine which script trigger to execute.

```javascript
module.exports.fnFindPhoneTrigger = function(context) {
    if (context.request.text.includes("call") || context.request.text.includes("ring")) {
        return true;
    } else {
        return false;
    }
} 
```

## Next step
> [!div class="nextstepaction"]
> [Reply action](conversation-designer-reply.md)
