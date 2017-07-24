---
title: Code recognizer | Microsoft Docs
description: Learn how to use a custom code recognizer as a task trigger.
author: vkannan
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/04/2017
ROBOTS: NoIndex, NoFollow
---

# Code recognizer as task trigger

Code recognizers enable you to write custom scripts to help perform a task. Regex-based expression evaluation or calling into other services can be used to help determine the user's intent. The custom script will instruct the conversation runtime to trigger a task. 

To use a script as a task trigger, choose "Script trigger" as the trigger type in the **Task** editor and then specify the name of the function that implements the custom script function. Click **Edit** to start editing the function. 

## Script trigger function parameter

The specified script callback function will always be called with the [`context`](conversation-designer-context-object.md) object.

The `context` object includes both `taskEntities` and `contextEntities`. Task entities are entities defined or generated for this task and context entities are property bags where entities can be preserved across conversations with the user.

## Return value

**Script trigger** functions are expected to return a boolean.

## Sample regex-based recognizer
The following code sample uses regex to process the request and returns a boolean so the conversation runtime can determine which script trigger to execute.

```javascript
export function fnFindPhoneTrigger(context) {
    if (context.request.text.includes("call") || context.request.text.includes("ring")) {
        return true;
    } else {
        return false;
    }
} 
```

## Next step
> [!div class="nextstepaction"]
> [Task actions](conversation-designer-actions.md)
