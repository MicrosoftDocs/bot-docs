---
title: Triggering actions | Microsoft Docs
description: Learn how to implement trigger actions using global message handlers by using the Bot Builder SDK for Node.js.
keywords: Bot Framework, trigger actions, node.js, Bot Builder, SDK, global handler, global message handler, message handler
author: kbrandl
manager: rstand
ms.topic: develop-nodejs-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/21/2017
ms.reviewer:
#ROBOTS: Index
---

# Triggering actions


[!include[Introduction to global message handlers](../includes/snippet-global-handlers-intro.md)]
This article describes how to implement actions, which are global message handlers. 

## Triggering a help dialog

With the Bot Builder SDK for Node.js, you can use `triggerAction` to specify the triggers that will cause the 
invokation of specific dialogs. 


For example, the `triggerAction` in the following code sample invokes the **help** dialog 
if the user's message is "help", thereby adding the **help** dialog to the top of the dialog stack and 
putting it in control of the conversation. When the **help** dialog completes, it will be removed from 
the dialog stack and the dialog from which the user sent the message "help" will regain control of the conversation.

```javascript
bot.dialog('help', (session, args, next) => {
    // Send message to the user and end this dialog
    session.endDialog('This is a simple bot that collects a name and age.');
}).triggerAction({
    matches: /^help$/,
    onSelectAction: (session, args, next) => {
        // Add the help dialog to the dialog stack 
        // (override the default behavior of replacing the stack)
        session.beginDialog(args.action, args);
    }
});
```

## Trigger a cancel action

Another type of action that can triggered globally is cancellation. In this example, the bot uses a **cancelAction** to end the conversation if the user's message is "cancel".

<!--
```javascript
bot.dialog('cancel', (session, args, next) => {
    session.endConversation('Operation canceled');
}).triggerAction({
    matches: /^cancel$/
});
```
--> 

```javascript
// Add dialog for creating a list
bot.dialog('listBuilderDialog', function (session) {
    if (!session.dialogData.list) {
        // Start a new list 
        session.dialogData.list = [];
        session.send("Each message will added as a new item to the list.\nSay 'end list' when finished or 'cancel' to discard the list.\n")
    } else if (/end.*list/i.test(session.message.text)) {
        // Return current list
        session.endDialogWithResult({ response: session.dialogData.list });
    } else {
        // Add item to list and save() change to dialogData
        session.dialogData.list.push(session.message.text);
        session.save();
    }
}).cancelAction('cancelList', "List canceled", { 
    matches: /^cancel/i,
    confirmPrompt: "Are you sure?"
});
```

## Additional resources

- [Designing conversation flow](bot-framework-design-core-dialogs.md)
- [Bot capabilities](bot-framework-design-capabilities.md)

[matches]: (https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.idialogactionoptions#matches)