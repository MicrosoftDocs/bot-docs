---
title: Send and receive attachments | Microsoft Docs
description: Learn how to send a typing indicator from a conversational application (bot).
keywords: bot framework, bot, typing indicator, indicate typing
author: DeniseMak
manager: rstand
ms.topic: develop-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/24/2017
ms.reviewer:
#ROBOTS: Index
---

# Send a typing indicator using the Bot Builder SDK for Node.js

<!--
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-send-receive-attachments.md)
> * [Node.js](bot-framework-nodejs-howto-send-receive-attachments.md)
>
-->


In this article, we'll discuss how to send a typing indicator by using the Bot Builder SDK for Node.js.

Users of your bot will expect a timely response to their message. If your bot performs some long running task like calling a server or executing a query, without giving the user some indication that the bot heard them, the user could get impatient and send additional messages to the bot.
Many channels support the sending of a typing indication to simply show the user that their message was received and is being processed.


<!-- TODO: Channels that support typing include: -->
> [!NOTE]
> To do: Add links to info on channels that support typing.

## Send a typing indicator

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


## Next steps

In this article, we discussed how to send a typing indicator by using the Bot Builder SDK for Node.js. 

Typing indicators are useful when inserting a message delay to avoid messages being sent out of order.

To learn more, see:

> [!NOTE]
> To do: Add links to Guarantee message order


[SendTyping]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#sendtyping
[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage