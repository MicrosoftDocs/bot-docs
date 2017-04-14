---
title: Display a typing indicator | Microsoft Docs
description: Teach your bot to display a "please wait" indicator while it is processing a request using the Bot Builder SDK for Node.js
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 02/24/2017
ms.reviewer:
ROBOTS: Index, Follow
---

# Display a typing indicator 

<!--
> [!div class="op_single_selector"]
> * [.NET](~/dotnet/howto-send-receive-attachments.md)
> * [Node.js](~/nodejs/send-receive-attachments.md)
>
-->


In this article, we'll discuss how to send a typing indicator by using the Bot Builder SDK for Node.js.

Users of your bot will expect a timely response to their message. If your bot performs some long running task like calling a server or executing a query, without giving the user some indication that the bot heard them, the user could get impatient and send additional messages to the bot.
Many channels support the sending of a typing indication to simply show the user that their message was received and is being processed.


<!-- TODO: Channels that support typing include: -->

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

Typing indicators are also useful when inserting a message delay to prevent messages that contain images from being sent out of order.

To learn more, see [How to send a rich card](~/nodejs/send-card-buttons.md).


## Additional resources

* [sendTyping][SendTyping]


[SendTyping]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#sendtyping
[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage