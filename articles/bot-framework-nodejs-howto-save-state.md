---
title: Save user data | Microsoft Docs
description: Learn how to save user data from a conversational application (bot).
author: DeniseMak
manager: rstand
ms.topic: develop-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/24/2017
ms.reviewer: rstand
ROBOTS: Index, Follow
---

# Save user data using the Bot Builder SDK for Node.js

<!--
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-save-state.md)
> * [Node.js](bot-framework-nodejs-howto-save-state.md)
>
-->

## Introduction

The Bot Framework SDK supports a storage system which it uses to track information like which dialog is active for a conversation,
which step of a dialog is active, and data that the bot remembers about the user. 

The [ChatConnector][] class automatically provides an implementation of this storage system, that uses Bot Framework Service to store this data for you. 
In the example that follows, we’ll use [session.userData][session_userData] to remember the name of the user. 

## Save the user's name

The following example prompts the user for their name and then saves it in [session.userData][session_userData].  

The message handler takes a next() function as an argument which we use to manually advance to the next step in the sequence of message handlers.
This lets us check if we know the user's name and skips to the greeting if we do.


```javascript

// Create your bot with a waterfall to ask user their name
var bot = new builder.UniversalBot(connector, [
    function (session, args, next) {
        if (!session.userData.name) {
            // Ask user for their name
            builder.Prompts.text(session, "Hello... What's your name?");
        } else {
            // Skip to next step
        next();
        }
    },
    function (session, results) {
        // Update name if answered
        if (results.response) {
            session.userData.name = results.response;
        }

        // Greet the user
        session.send("Hi %s!", session.userData.name);
    }
]);


```


## Next steps

In this article, we discussed how to save user information by using the Bot Builder SDK for Node.js. 

To learn more, see:

> [!NOTE]
> To do: Add links to other articles about managing state.


[SendTyping]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#sendtyping
[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage
[ChatConnector]:
[session_userData]:https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#userdata