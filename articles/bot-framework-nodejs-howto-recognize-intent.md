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
recognizer that looks for the user to send an image or calls an external web service to determine the user's intent. Your bot can register than one recognizer.


[!code-js[Add a custom recognizer (Javascript)](../includes/code/node-howto-recognize-intent.js#addCustomRecognizer)]

Once you've registered a recognizer, you can associate the recognizer with an action using a `matches` clause.

[!code-js[Bind intents to actions (Javascript)](../includes/code/node-howto-recognize-intent.js#bindIntentsToActions)]


## Example: Use the built-in regular expression recognizer
Use [RegExpRecognizer](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.regexprecognizer.html) to detect the user's intent using a regular expression. Multiple expressions can be passed in to support recognizing across multiple languages. 

> [!NOTE]
> Code sample coming soon...

## Example: Use an external web service
 A bot can be configured to use cloud based intent recognition services like LUIS through an extensible set of recognizer plugins. Out of the box, Bot Builder comes with a LuisRecognizer that can be used to call a machine learning model you’ve trained via their web site. You can create a LuisRecognizer that’s pointed at your model and then pass that recognizer into your IntentDialog at creation time using the options structure.

> [!NOTE]
> Code sample coming soon...

## Disambiguate between multiple intents

Notice that the custom recognizer example involves assigning a numerical score to each intent. This is done since your bot may have more than one recognizer, and the Bot Builder SDK provides built-in logic to disambiguate between intents returned by multiple recognizers. The score assigned to an intent is typically between 0.0 and 1.0, but a custom recognizer may define an intent greater than 1.1 to ensure that that intent will always be chosen by the Bot Builder SDK's disambiguation logic. 

By default, recognizers run in parallel, but you can set recognizeOrder in [IIntentRecognizerSetOptions][IntentRecognizerSetOptions] such that the process quits as soon as your bot finds one that gives a score of 1.0.

You can provide custom disambiguation logic in your bot by implementing [IDisambiguateRouteHandler](https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.idisambiguateroutehandler.html).

> [!NOTE]
> Code sample of custom disambiguator coming soon...

## Additional resources

To learn more about the actions you can associate with a recognized intent, see [Managing conversation flow](bot-framework-nodejs-howto-manage-conversation-flow.md) and [Trigger actions using global handlers](bot-framework-nodejs-howto-global-handlers.md).

[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage
[IntentRecognizerSetOptions]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iintentrecognizersetoptions.html