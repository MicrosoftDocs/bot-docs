---
title: Intercept messages using the Bot Builder SDK for Node.js | Microsoft Docs
description: Learn how to intercept messages that are exchanged between user and bot by using the Bot Builder SDK for Node.js.
keywords: Bot Framework, node.js, Bot Builder, SDK, message logging, intercept message, inspect message
author: kbrandl
manager: rstand
ms.topic: develop-nodejs-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/21/2017
ms.reviewer: rstand
#ROBOTS: Index
---

# Intercept messages using the Bot Builder SDK for Node.js
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-middleware.md)
> * [Node.js](bot-framework-nodejs-howto-middleware.md)
>

## Introduction

[!include[Application configuration settings](../includes/snippet-message-logging-intro.md)]
In this article, we'll discuss how to intercept messages that are exchanged between user and bot by using the Bot Builder SDK for Node.js. 

## Intercept messages

The following code sample shows how to intercept messages that are exchanged between user and bot, 
by using the concept of **middleware** in the Bot Builder SDK for Node.js. 

First, configure the handler for incoming messages (`botbuilder`) and for outgoing messages (`send`).

```javascript
server.post('/api/messages', connector.listen());
var bot = new builder.UniversalBot(connector);
// Middleware for logging
bot.use({
    botbuilder: function (session, next) {
        myMiddleware.logIncomingMessage(session, next);
    },
    send: function (event, next) {
        myMiddleware.logOutgoingMessage(event, next);
    }
})
```

Then, implement each of the handlers to define the action to take for each message that is intercepted:

```javascript
module.exports = {
    logIncomingMessage: function (session, next) {
        console.log(session.message.text);
        next();
    },
    logOutgoingMessage: function (event, next) {
        console.log(event.text);
        next();
    }
}
```

Now, every inbound message (from user to bot) will trigger `logIncomingMessage`, 
and every outbound message (from bot to user) will trigger `logOutgoingMessage`.
In this example, we're simply printing some information about each message, but you can 
update `logIncomingMessage` and `logOutgoingMessage` as necessary to define the actions that you want to take for each message. 


## Additional resources

In this article, we discussed how to intercept the messages that are exchanged between user and bot by using the Bot Builder SDK for Node.js. 
To learn more, see:

> [!NOTE]
> To do: Add links to related content (link to 'detailed readme' and 'full C# code' that Matt refers to)