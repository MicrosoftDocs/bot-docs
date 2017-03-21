---
title: Recognize intent | Microsoft Docs
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

When the users ask your bot for something, like "help" or "find news", your bot needs to understand what the user is asking for. 
You can design your bot to recognize a set of intents that interpret the user’s input in terms of the intention it conveys.
This article demonstrates how to register a custom intent recognizer that will be run for every message received from the user. 
Custom recognizers can return a named intent that can be used to trigger actions and dialogs within the bot.


## Example: Register a custom intent recognizer
This specific example adds a recognizer that looks for the user to say 'help' or 'goodbye' but you could easily add a 
recognizer that looks for the user to send an image or calls an external web service to determine the user's intent.


[!code-js[Add a custom recognizer (Javascript)](../includes/code/node-howto-recognize-intent.js#addCustomRecognizer)]

Once you've registered a recognizer, you can associate the recognizer with an action using a `matches` clause.

[!code-js[Bind intents to actions (Javascript)](../includes/code/node-howto-recognize-intent.js#bindIntentsToActions)]

## Additional resources

To learn more about the actions you can associate with a recognized intent, see [Global handlers](bot-framework-nodejs-howto-global-handlers.md).

[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage