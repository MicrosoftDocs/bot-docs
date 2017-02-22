---
title: Implement global message handlers by using the Bot Builder SDK for Node.js | Microsoft Docs
description: Learn how to implement global message handlers by using the Bot Builder SDK for Node.js.
keywords: Bot Framework, node.js, Bot Builder, SDK, global handler, global message handler, message handler
author: kbrandl
manager: rstand
ms.topic: develop-nodejs-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/21/2017
ms.reviewer: rstand
#ROBOTS: Index
---

# Implement global message handlers using the Bot Builder SDK for Node.js
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-global-handlers.md)
> * [Node.js](bot-framework-nodejs-howto-global-handlers.md)
>

## Introduction

[!include[Introduction to global message handlers](../includes/snippet-global-handlers-intro.md)]
In this article, we'll discuss how to implement global message handlers by using the Bot Builder SDK for Node.js. 

## Implement global message handlers

With the Bot Builder SDK for Node.js, use `triggerActions` to specify the triggers that will cause the 
invokation of specific dialogs. 

For example, the following code sample invokes the **cancel** dialog (which ends the conversation) 
if the user's message is "cancel".

```javascript
bot.dialog('cancel', (session, args, next) => {
    // end the conversation to cancel the operation
    session.endConversation('Operation canceled');
}).triggerAction({
    matches: /^cancel$/
});
```

As another example, the `triggerAction` in the following code sample invokes the **help** dialog 
if the user's message is "help", thereby adding the **help** dialog to the top of the dialog stack and 
putting it in control of the conversation. When the **help** dialog completes, it will be removed from 
the dialog stack and the dialog from which the user sent the message "help" will regain control of the conversation.

```javascript
bot.dialog('help', (session, args, next) => {
    // send help message to the user and end this dialog
    session.endDialog('This is a simple bot that collects a name and age.');
}).triggerAction({
    matches: /^help$/,
    onSelectAction: (session, args, next) => {
        // overrides default behavior of replacing the dialog stack
        // This will add the help dialog to the stack
        session.beginDialog(args.action, args);
    }
});
```

## Additional resources

In this article, we discussed how to implement global message handlers by using the Bot Builder SDK for Node.js. 
To learn more, see:

> [!NOTE]
> To do: Add links to related content (link to 'detailed readme' and 'full Node.js code' that Matt refers to)