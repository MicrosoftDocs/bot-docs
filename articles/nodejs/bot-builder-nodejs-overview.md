---
title: Bot Builder SDK for Node.js | Microsoft Docs
description: Explore the Bot Builder SDK for Node.js, a powerful, easy-to-use bot building framework.
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer: 
---

# Bot Builder SDK for Node.js
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-overview.md)
> - [Node.js](../nodejs/bot-builder-nodejs-overview.md)
> - [REST](../rest-api/bot-framework-rest-overview.md)

The Bot Builder SDK for Node.js is a powerful, easy-to-use framework that provides a familiar way for Node.js developers to write bots.
You can use it to build a wide variety of conversational user interfaces, from simple prompts to free-form conversations.

The conversational logic for your bot is hosted as a web service. The Bot Builder SDK uses <a href="http://restify.com">restify</a>, a popular framework for building web services, to create the bot's web server. 
The SDK is also compatible with <a href="http://expressjs.com/">Express</a> and the use of other web app frameworks is possible with some adaption. 

Using the SDK, you can take advantage of the following SDK features: 

- Powerful system for building dialogs to encapsulate conversational logic.
- Built-in prompts for simple things such as Yes/No, strings, numbers, and enumerations, as well as support for messages containing images and attachments, 
and rich cards containing buttons.
- Built-in support for powerful AI frameworks such as <a href="http://luis.ai" target="_blank">LUIS</a>.
- Built-in recognizers and event handlers that guide the user through the 
conversation, providing help, navigation, clarification, and confirmation as needed.

## Get the SDK


To install the SDK and its dependencies, first create a folder for your bot, navigate to it, and run the following **npm** command:

```
npm init
```

Next, install the Bot Builder SDK and <a href="http://restify.com/" target="_blank">Restify</a> modules by running the following **npm** commands:

```
npm install --save botbuilder
npm install --save restify
```

## Get started

If you aren't ready to build a bot on your own, you can quickly [get started](~/nodejs/bot-builder-nodejs-quickstart.md) with building a bot with the Bot Builder SDK for Node.js.

For information on concepts that help you understand the rest of the documentation, see [Key concepts](~/nodejs/bot-builder-nodejs-concepts.md).

[Learn the principles][DesignGuide] of designing bots and [explore patterns][DesignPatterns] for guidance on what features your bot needs in order to best address the top user scenarios.

Once you understand basic principles, the [How-to][HowTo] articles give you step-by-step instructions on adding features to your bot.

## Additional resources

The following articles get you started building bots using the Bot Builder SDK for Node.js.

* [Get Started](~/nodejs/bot-builder-nodejs-quickstart.md): Quickly build and test a simple bot by following instructions in this step-by-step tutorial.
* [Key concepts](~/nodejs/bot-builder-nodejs-concepts.md): Learn about key concepts in the Bot Builder SDK for Node.js.

The following task-focused how-to guides demonstrate various features of the Bot Builder SDK for Node.js.

* [Respond to messages](~/nodejs/bot-builder-nodejs-use-default-message-handler.md)
* [Triggering actions](~/nodejs/bot-builder-nodejs-global-handlers.md)
* [Recognize user intent](~/nodejs/bot-builder-nodejs-recognize-intent.md)
* [Send a rich card](~/nodejs/bot-builder-nodejs-send-rich-cards.md)
* [Send attachments](~/nodejs/bot-builder-nodejs-send-receive-attachments.md)
* [Saving user data](~/nodejs/bot-builder-nodejs-save-user-data.md)


If you encounter problems or have suggestions regarding the Bot Builder SDK for Node.js, 
see [Support](~/resources-support.md) for a list of resources that provide opportunities 
for support and feedback. 


[DesignGuide]: ~/bot-design-principles.md 
[DesignPatterns]: ~/bot-design-pattern-task-automation.md 
[HowTo]: ~/nodejs/bot-builder-nodejs-use-default-message-handler.md 
