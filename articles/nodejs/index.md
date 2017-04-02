---
title: Bot Builder SDK for Node.js | Microsoft Docs
description: Learn about the Bot Builder SDK for Node.js.
keywords: Bot Framework, Node.js, Bot Builder, SDK
author: DeniseMak
manager: rstand
ms.topic: develop-nodejs-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 03/08/2017
ms.reviewer: rstand
#ROBOTS: Index
---

# Bot Builder SDK for Node.js


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

## Install the SDK


To install the SDK and its dependencies, first create a folder for your bot, navigate to it, and run the following **npm** command:

```
npm init
```

Next, install the Bot Builder SDK and <a href="http://restify.com/" target="_blank">Restify</a> modules by running the following **npm** commands:

```
npm install --save botbuilder
npm install --save restify
```

## How to get started

<!-- TODO: Make sure this section is consistent with Resources and doesn't duplicate it -->
Once you've installed the SDK, see [Get Started](~/nodejs/getstarted.md) for instructions on how to quickly build and test a simple bot.

For information on concepts that help you understand the rest of the documentation, see [Key concepts](~/nodejs/concepts.md).

<!--
* [Glossary](~/bot-framework-glossary.md): Covers terminology that is used throughout the SDK documentation. -->

Before you start building your bot, read [Designing bots][DesignGuide] to learn about principles of bot design and [Patterns][DesignPatterns] for guidance on what features your bot needs in order to best address the top user scenarios. 


To learn about how your bot processes user messages using dialogs and handlers, see [Managing conversation flow](~/nodejs/manage-conversation-flow.md).


Once you understand basic conversational mechanics, the [How-to guides][HowToFirstArticle] give you instructions on adding more advanced features to your bot.

## Get samples

The <a href="https://github.com/Microsoft/BotBuilder" target="_blank">BotBuilder</a> GitHub repository 
contains numerous code samples that show how to build bots using the Bot Builder SDK for Node.js. 
To access these code samples, clone the repository and navigate to the **Samples** folder.

```
git clone https://github.com/Microsoft/BotBuilder.git
cd BotBuilder/Node/Samples
```

The <a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">BotBuilder-Samples</a> GitHub repository 
contains numerous task-focused code samples that show how to use various features of the Bot Builder SDK for Node.js. 
To access these code samples, clone the repository and navigate to the **Node** folder.

```
git clone https://github.com/Microsoft/BotBuilder-Samples.git
cd BotBuilder-Samples/Node
```

### Basic samples

The following [examples](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples) demonstrate basic techniques for building a great bot.

| Sample | Description |
|------|------|
| hello-ChatConnector | Shows how to get started with the **ChatConnector** class. |
| basics-waterfall | Shows how to use a *waterfall*, which is a sequential series of message handlers associated with a dialog, to prompt the user with a series of questions. |
| basics-loops | Shows how to use **session.replaceDialog** to create loops. |
| basics-menus | Shows how to create a simple menu system for a bot. |
| basics-naturalLanguage | Shows how to use a **LuisDialog** to add natural language support to a bot. | 
| basics-multiTurn | Shows how to implement a multi-turn conversation, in which the user asks a question that is followed by a series of follow-up questions, using waterfalls. | 
| basics-firstRun | Shows how to create a first-run experience using a piece of middleware. |
| basics-logging| Shows how to add logging/filtering of incoming messages using middleware. |
| basics-localization | Shows how to implement support for multiple languages. | 
| basics-customPrompt | Shows how to create a custom prompt. |
| basics-libraries | Shows how to package a set of dialogs as a library that can be shared across multiple bots. |

### Demo bots

The demo bots are samples designed to showcase what's possible on specific channels. They’re good sources of example code for highlighting features of a channel.

| Sample | Description |
|------|------|
| demo-skype | A bot designed to showcase what’s possible on Skype. | 
| demo-skype-calling | A bot designed to show how to build a calling bot for Skype. |
| demo-slack | A bot designed to showcase what’s possible on Skype. | 
| demo-facebook | A bot designed to showcase what’s possible on Facebook. |

### Other samples

You can find other samples in the [Bot Builder SDK Samples repository](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node). 

## Additional resources

The following articles get you started building bots using the Bot Builder SDK for Node.js.

* [Get Started](~/nodejs/getstarted.md): Quickly build and test a simple bot by following instructions in this step-by-step tutorial.
* [Key concepts](~/nodejs/concepts.md): Learn about key concepts in the Bot Builder SDK for Node.js.

The following task-focused how-to guides demonstrate various features of the Bot Builder SDK for Node.js.

* [Manage conversation flow](~/nodejs/manage-conversation-flow.md)
* [Triggering actions](~/nodejs/global-handlers.md)
* [Recognize user intent](~/nodejs/recognize-intent.md)
* [Send a rich card](~/nodejs/send-card-buttons.md)
* [Send attachments](~/nodejs/send-receive-attachments.md)
* [Saving user data](~/nodejs/save-user-data.md)


If you encounter problems or have suggestions regarding the Bot Builder SDK for Node.js, 
see [Support](~/resources-support.md) for a list of resources that provide opportunities 
for support and feedback. 

<<<<<<< HEAD:articles/bot-framework-nodejs-overview.md
[HowToFirstArticle]: bot-framework-nodejs-howto-localization.md
[SendFirstArticle]: bot-framework-nodejs-howto-use-default-message-handler.md
[AskFirstArticle]: bot-framework-nodejs-howto-prompts.md
[ListenFirstArticle]: bot-framework-nodejs-howto-global-handlers.md
[DesignGuide]: bot-framework-design-overview.md
[DesignPatterns]: bot-framework-design-patterns-task.md
=======
[HowToFirstArticle]: ~/nodejs/localization.md
[DesignGuide]: ~/design/principles.md
[DesignPatterns]: ~/design/patterns-task.md
>>>>>>> 939dce2500428da8c3bf34414264c64d6df08959:articles/nodejs/index.md
