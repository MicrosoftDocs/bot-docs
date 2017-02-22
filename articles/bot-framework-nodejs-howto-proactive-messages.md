---
title: Send proactive messages by using the Bot Builder SDK for Node.js | Microsoft Docs
description: Learn how to send proactive messages by using the Bot Builder SDK for Node.js.
keywords: Bot Framework, node.js, Bot Builder, SDK, proactive message, ad hoc message, dialog-based message
author: kbrandl
manager: rstand
ms.topic: develop-nodejs-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/22/2017
ms.reviewer: rstand
#ROBOTS: Index
---
# Send proactive messages using the Bot Builder SDK for Node.js
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-proactive-messages.md)
> * [Node.js](bot-framework-nodejs-howto-proactive-messages.md)
>

## Introduction

[!include[Introduction to proactive messages - part 1](../includes/snippet-proactive-messages-intro-1.md)] 

[!include[Introduction to proactive messages - part 2](../includes/snippet-proactive-messages-intro-2.md)] 

In this article, we'll discuss how to send proactive messages by using the Bot Builder SDK for Node.js. 

## Send an ad hoc proactive message

The following code samples show how to send an ad hoc proactive message by using the Bot Builder SDK for Node.js.

To be able to send an ad hoc message to a user, the bot must first collect (and save) information about the user from the current conversation. 
The **address** property of the message includes all of the information that the bot will need to send an ad hoc message to the user later. 

```javascript
bot.dialog('/', function(session, args) {
    var savedAddress = session.message.address;

     // (Save this information somewhere that it can be accessed later, such as in a database.)

    var message = 'Hello user, good to meet you! I now know your address and can send you notifications in the future.';
    session.send(message);
})
```

> [!NOTE]
> For simplicity, this example does not specify how to store the user data. 
> The bot can store the user data in any manner, so long as it can be accessed later when the bot is ready to send the ad hoc message.

After the bot has collected information about the user, it can send an ad hoc proactive message to the user at any time. 
To do so, it simply retrieves the user data it stored previously, constructs the message, and sends it.

```javascript
var bot = new builder.UniversalBot(connector);

function sendProactiveMessage(address) {
    var msg = new builder.Message().address(address);
    msg.text('Hello, this is a notification');
    msg.textLocale('en-US');
    bot.send(msg);
}
```

> [!TIP]
> An ad hoc proactive message can be initiated like from 
> asynchronous triggers such as http requests, timers, queues or from anywhere else that the developer chooses.

## Send a dialog-based proactive message

The following code samples show how to send a dialog-based proactive message by using the Bot Builder SDK for Node.js.

To be able to send a dialog-based message to a user, the bot must first collect (and save) information from the current conversation. 

```javascript
var bot = new builder.UniversalBot(connector);

function sendProactiveMessage(address) {
    var msg = new builder.Message().address(address);
    msg.text('Hello, this is a notification');
    msg.textLocale('en-US');
    bot.send(msg);
}
```


## Additional resources

In this article, we discussed how to send proactive messages by using the Bot Builder SDK for Node.js. 
To learn more, see:

> [!NOTE]
> To do: Add links to related content (link to 'detailed readme' and 'full Node.js code' that Matt refers to)