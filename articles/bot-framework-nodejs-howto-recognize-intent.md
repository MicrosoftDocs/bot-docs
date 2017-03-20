---
title: Recognize intent using a custom recognizer | Microsoft Docs
description: Learn how to recognize the user's intent in a conversational application (bot).
keywords: bot framework, bot, intent, recognize, recognizer
author: DeniseMak
manager: rstand
ms.topic: develop-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/24/2017
ms.reviewer: rstand
#ROBOTS: Index
---

# Recognize user intent using the Bot Builder SDK for Node.js

<!-- Need to create NET stub.
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-send-card-buttons.md)  
> * [Node.js](bot-framework-nodejs-howto-send-card-buttons.md)
>
--> 
## Introduction

When users ask your bot for something, like "help" or "find news", your bot needs to understand what the user is asking for. 
You can design your bot to recognize a set of intents that interpret the user’s input in terms of the intention it conveys.
This article demonstrates how to register a custom intent recognizer that runs each time the bot receives a message from the user. 
Custom recognizers return a named intent that can be used to trigger actions and dialogs within the bot.


## Example: Register a custom intent recognizer
This example adds a recognizer that determines if the user says 'help' or 'goodbye'. A bot can also have a 
recognizer that determines if the user sends an image, or calls an external web service to determine the user's intent.


[!code-js[Register a custom intent recognizer](../includes/code/node-howto-recognize-intent.js#addCustomRecognizer)]

Once you've registered a recognizer, you can associate the recognizer with an action using a `matches` clause.

[!code-js[Bind intents to actions](../includes/code/node-howto-recognize-intent.js#bindIntentsToActions)]

## Additional resources

In this article, we discussed how to register a custom intent recognizer using the Bot Builder SDK for Node.js. 

To learn more about the actions you can associate with a recognized intent, see [Global handlers](bot-framework-nodejs-howto-global-handlers.md).

> [!NOTE]
> To do: Add additional links to related content  

[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage