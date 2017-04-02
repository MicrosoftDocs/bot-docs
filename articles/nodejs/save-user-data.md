---
title: Save user data | Microsoft Docs
description: Learn how to save user data from a conversational application (bot).
keywords: bot framework, save state, store data, user data, track state, bot storage, remember answers
author: DeniseMak
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 02/24/2017
ms.reviewer:
#ROBOTS: Index
---

# Save user data using the Bot Builder SDK for Node.js

<!--
> [!div class="op_single_selector"]
> * [.NET](~/dotnet/howto-save-user-data.md)
> * [Node.js](~/nodejs/save-user-data.md)
>
-->



The Bot Framework SDK supports a storage system which it uses to track information like which dialog is active for a conversation,
which step of a dialog is active, and data that the bot remembers about the user. 

The [ChatConnector][ChatConnector] class automatically provides an implementation of this storage system, that uses Bot Framework Service to store this data for you. 
You can access stored data using [session.userData][session_userData]. <!-- todo: Session and Converstaion and PrivateConversation --> 

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


<!-- TODO: UPDATE LINKS TO POINT TO NEW REFERENCE -->
[SendTyping]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#sendtyping
[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage
[ChatConnector]:https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.chatconnector.html
[session_userData]:https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#userdata