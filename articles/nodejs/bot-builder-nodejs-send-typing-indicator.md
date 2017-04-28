---
title: Send a typing indicator | Microsoft Docs
description: Learn how to add a "please wait" indicator to tell a user a bot is processing a request using the Bot Builder SDK for Node.js
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---

# Send a typing indicator 


Users expect a timely response to their messages. If your bot performs some long-running task like calling a server or executing a query without giving the user some indication that the bot heard them, the user could get impatient and send additional messages or just assume the bot is broken.
Many channels support the sending of a typing indication to show the user that the message was received and is being processed.


## Typing indicator example

The following example demonstrates how to send a typing indication using [session.sendTyping()][SendTyping].  You can test this with the Bot Framework Emulator.


```javascript

// Create bot and default message handler
var bot = new builder.UniversalBot(connector, function (session) {
    session.sendTyping();
    setTimeout(function () {
        session.send("Hello there...");
    }, 3000);
});


```

Typing indicators are also useful when inserting a message delay to prevent messages that contain images from being sent out of order.

To learn more, see [How to send a rich card](~/nodejs/bot-builder-nodejs-send-rich-cards.md).


## Additional resources

* [sendTyping][SendTyping]


[SendTyping]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#sendtyping
[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage