---
title: Manage conversation flow using dialogs with the Bot Builder SDK for Node.js | Microsoft Docs
description: Learn how to manage conversation flow using dialogs and the Bot Builder SDK for Node.js.
keywords: Bot Framework, dialog, messages, conversation flow, conversation, node.js, node, Bot Builder, SDK
author: kbrandl
manager: rstand
ms.topic: develop-nodejs-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/17/2017
ms.reviewer: rstand
#ROBOTS: Index
---
# Manage conversation flow using dialogs with the Bot Builder SDK for Node.js
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-manage-conversation-flow.md)
> * [Node.js](bot-framework-nodejs-howto-manage-conversation-flow.md)
>

## Introduction

In this article, we'll walk through examples of sending messages and managing conversation flow using the Bot Builder SDK for Node.js. 

<!--
> [!NOTE]
> To do: add introductory content -- image of example bot dialog flow (from: media/designing-bots/core/dialogs-screens.png) 
> and text describing the steps of that dialog flow.
-->
## Using the default message handler

The simplest way to start sending and receiving messages is by using the default message handler. 
To do this, create a new UniversalBot with a function to handle the messages received from a user, and pass this object to your ChatConnector.

```javascript
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () { });

var connector = new builder.ChatConnector({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

server.post('/api/messages', connector.listen());

// Create your bot with a function to receive messages from the user
var bot = new builder.UniversalBot(connector, function (session) {
    session.send("You said: %s", session.message.text);
});

```
Your message handler takes a session object which can be used to read the user's message and compose replies. 
The session.send() method, which sends a reply to the user who sent the message, supports a flexible template syntax for formatting strings.
Refer to the sprintf library documentation for details about the template syntax. 

## Invoke a root dialog

Dialogs help you encapsulate your bot's conversational logic in manageable components. 
The Bot Builder SDK provides Dialog objects that help you manage conversation flow. The following example shows 
demonstrates how a bot invokes a "root dialog", instead of using the message handler passed to the bot's constructor. 
<!-- The following example shows how to wire the basic HTTP GET call to a controller and then invoke the root dialog. -->

```javascript
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () { });

var connector = new builder.ChatConnector({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

var bot = new builder.UniversalBot(connector);
server.post('/api/messages', connector.listen());

// root dialog
bot.dialog('/', ...
```

## Invoke a child dialog from the root dialog

Next, the root dialog invokes a 'New Order' dialog. 

```javascript
bot.dialog('/', new builder.IntentDialog()
// Did the user type 'order'?
.matchesAny([/order/i], [ 
    function (session) {
        // Invoke the new order dialog
        session.beginDialog('/newOrder');
    },

    function (session, result) {
        // Store the value that the new order dialog returns
        var resultFromNewOrder = result.response;

        session.send('New order dialog just told me this: %s', resultFromNewOrder);
        // Close the root dialog
        session.endDialog(); 
    }
])
```
##<a id="dialog-lifecycle"></a> Dialog lifecycle

When a dialog is invoked, it takes control of the conversation flow. 
Every new message will be subject to processing by that dialog until it either closes or redirects to another dialog. 

In Node, you can invoke one dialog from another by using `session.beginDialog()`. 
To close a dialog and remove it from the stack (thereby sending the user back to the prior dialog in the stack), use `session.endDialog()`. 

## Additional resources

In this article, we walked through an example of managing conversation flow using dialogs and the Bot Builder SDK for Node.js. 
To learn more, see:

> [!NOTE]
> To do: Add links to related articles
