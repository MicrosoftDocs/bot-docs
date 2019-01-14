---
title: Intercept messages | Microsoft Docs
description: Learn how to create logs or other records by intercepting and processing information exchanges using the Bot Framework SDK for Node.js.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 11/02/2018
monikerRange: 'azure-bot-service-3.0'
---
# Intercept messages

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-middleware.md)
> - [Node.js](../nodejs/bot-builder-nodejs-intercept-messages.md)

[!INCLUDE [Introduction to message logging](../includes/snippet-message-logging-intro.md)]

## Example

The following code sample shows how to intercept messages that are exchanged between user and bot by using the concept of **middleware** in the Bot Framework SDK for Node.js. 

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

## Sample code

For a complete sample that shows how to intercept and log messages using the Bot Framework SDK for Node.js, see the <a href="https://aka.ms/v3-js-capability-middlewareLogging" target="_blank">Middleware and Logging sample</a> in GitHub.
