---
title: Intercept messages | Microsoft Docs
description: Learn how to create logs or other records by intercepting and processing information exchanges using the Bot Builder SDK for Node.js.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 02/21/2017
ms.reviewer:
ROBOTS: Index, Follow
---

# Intercept messages

[!include[Introduction to message logging](~/includes/snippet-message-logging-intro.md)]

## Example

The following code sample shows how to intercept messages that are exchanged between user and bot by using the concept of **middleware** in the Bot Builder SDK for Node.js. 

First, configure the handler for incoming messages (`botbuilder`) and for outgoing messages (`send`).

```javascript
server.post('/api/messages', connector.listen());
var bot = new builder.UniversalBot(connector);
bot.use({
    botbuilder: function (session, next) {
        myMiddleware.logIncomingMessage(session, next);
    },
    send: function (event, next) {
        myMiddleware.logOutgoingMessage(event, next);
    }
})
```

Then, implement each of the handlers to define the action to take for each message that is intercepted.

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
In this example, the bot simply prints some information about each message, but you can 
update `logIncomingMessage` and `logOutgoingMessage` as necessary to define the actions that you want to take for each message. 


