---
title: Greet users when they join a conversation or add your bots to their contacts list using the Bot Builder SDK for Node.js | Microsoft Docs
description: Learn how your conversational application (bot) can handle events such as a user joining a conversation or adding a bot to their contacts list.
keywords: bot framework, handle conversation updates, join conversation, add to contacts, greet users, conversationUpdate, contactRelationUpdate
author: DeniseMak
manager: rstand
ms.topic: develop-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/24/2017
ms.reviewer: rstand
#ROBOTS: Index
---
<!-- This topic is about handling conversation update events, conversationUpdate and contactRelationUpdate
The title is "Greet users" because typically, you'll greet the users or do other first-run activities when a user joins a conversation -->

# Greet users who join a conversation or add your bot to their contacts using the Bot Builder SDK for Node.js

<!--
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-save-user-data.md)
> * [Node.js](bot-framework-nodejs-howto-save-user-data.md)
>
-->

## Introduction

This article demonstrates how your bot can handle events such as a user joining a conversation or adding a bot to their contacts list. 

 <!-- todo: Session and Converstaion and PrivateConversation --> 

## Greeting a user on conversation join
The Bot Framework provides the conversationUpdate event for notifying your bot when a user joins or leaves a conversation.

Your bot can greet the user or perform other first-run activities when a user joins the conversation. 

<!-- TODO: Reference code in snippet repository -->

```javascript


bot.on('conversationUpdate', function (message) {
    if (message.membersAdded && message.membersAdded.length > 0) {
        // Say hello
        var isGroup = message.address.conversation.isGroup;
        var txt = isGroup ? "Hello everyone!" : "Hello...";
        var reply = new builder.Message()
                .address(message.address)
                .text(txt);
        bot.send(reply);
    } else if (message.membersRemoved) {
        // See if bot was removed
        var botId = message.address.bot.id;
        for (var i = 0; i < message.membersRemoved.length; i++) {
            if (message.membersRemoved[i].id === botId) {
                // Say goodbye
                var reply = new builder.Message()
                        .address(message.address)
                        .text("Goodbye");
                bot.send(reply);
                break;
            }
        }
    }
});


```


## Next steps

In this article, we discussed how to handle the conversationUpdate event using the Bot Builder SDK for Node.js. 

To learn more, see:

> [!NOTE]
> To do: Add links to other articles about conversationUpdate.


<!-- TODO: UPDATE LINKS TO POINT TO NEW REFERENCE -->
[SendTyping]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#sendtyping
[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage
[ChatConnector]:https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.chatconnector.html
[session_userData]:https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#userdata