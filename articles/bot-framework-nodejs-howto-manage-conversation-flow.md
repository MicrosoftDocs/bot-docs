---
title: Manage conversation flow using dialogs with the Bot Builder SDK for Node.js | Microsoft Docs
description: Learn how to manage conversation flow using dialogs and the Bot Builder SDK for Node.js.
keywords: Bot Framework, dialog, conversation flow, conversation, node.js, node, Bot Builder, SDK
author: kbrandl
manager: rstand

# the ms.topic should be the section of the IA that the article is in, with the suffix -article. Some examples:
# get-started article, sdk-reference-article
ms.topic: develop-nodejs-article

ms.prod: botframework

# The ms.service should be the Bot Framework technology area covered by the article, e.g., Bot Builder, LUIS, Azure Bot Service
ms.service: Bot Builder

# Date the article was updated
ms.date: 02/17/2017

# Alias of the document reviewer. Change to the appropriate person.
ms.reviewer: rstand

# Include the following line commented out
#ROBOTS: Index
---
# Manage conversation flow using dialogs with the Bot Builder SDK for Node.js
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-manage-conversation-flow.md)
> * [Node.js](bot-framework-nodejs-howto-manage-conversation-flow.md)
>

## Introduction

In this tutorial, we'll walk through an example of managing conversation flow using dialogs and the Bot Builder SDK for Node.js. 

> [!NOTE]
> To do: add introductory content -- image of example bot dialog flow (from: media/designing-bots/core/dialogs-screens.png) 
> and text describing the steps of that dialog flow.

## Invoke the root dialog

First, the bot controller invokes the "root dialog". 
The following example shows how to wire the basic HTTP GET call to a controller and then invoke the root dialog. 

```javascript
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () { });

var connector = new builder.ChatConnector({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

var bot = new builder.UniversalBot(connector);
server.post('/api/messages', connector.listen());

// Root dialog
bot.dialog('/', ...
```

## Invoke the 'New Order' dialog

Next, the root dialog invokes the 'New Order' dialog. 

```javascript
bot.dialog('/', new builder.IntentDialog()
//Did the user type 'order'?
.matchesAny([/order/i], [ 
    function (session) {
        //Let's invoke then the new order dialog
        session.beginDialog('/newOrder');
    },

    function (session, result) {
        //This will get us whatever the new order dialog decided to return to us
        var resultFromNewOrder = result.response;

        session.send('New order dialog just told me this: %s', resultFromNewOrder');
        //We are now done with the root dialog
        session.endDialog(); 
    }
])
```
##<a id="dialog-lifecycle"></a> Dialog lifecycle

When a dialog is invoked, it takes control of the conversation flow. 
Every new message will be subject to processing by that dialog until until it either closes or redirects to another dialog. 

In Node, you can invoke one dialog from another by using `session.beginDialog()`. 
To close a dialog and remove it from the stack (sending the user to the next remaining dialog in the stack), use `session.endDialog()`. 

## Next steps

In this tutorial, we walked through an example of managing conversation flow using dialogs and the Bot Builder SDK for Node.js. 
To learn more, see:

> [!NOTE]
> To do: Add links to related articles
