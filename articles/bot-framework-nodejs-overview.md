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

## Introduction

The Bot Builder SDK for Node.js is a powerful, easy-to-use framework that provides a familiar way for Node.js developers to write bots.
You can use it to build a wide variety of conversational user interfaces, from simple prompts to free-form conversations.

The conversational logic for your bot is hosted as a web service. The Bot Bulder SDK uses <a href="http://restify.com">restify</a>, a popular framework for building web services, to create the bot's web server. 
The SDK is also compatible with <a href="http://expressjs.com/">Express</a> and the use of other web app frameworks is possible with some adaption. 

Using the SDK, you can take advantage of the following SDK features: 

- Powerful system for building dialogs to encapsulate conversational logic.
- Built-in prompts for simple things such as Yes/No, strings, numbers, and enumerations, as well as support for messages containing images and attachments, 
and rich cards containing buttons.
- Built-in support for powerful AI frameworks such as <a href="http://luis.ai" target="_blank">LUIS</a>.
- Built-in recognizers and event handlers that guide the user through the 
conversation, providing help, navigation, clarification, and confirmation as needed.

## Get the SDK

Clone the <a href="https://github.com/Microsoft/BotBuilder" target="_blank">BotBuilder</a> GitHub repository. 

```
git clone https://github.com/Microsoft/BotBuilder.git
cd BotBuilder/Node
```

See [Get Started](bot-framework-nodejs-getstarted.md) for instructions on how to install the SDK and the necessary dependencies.

## Get code samples

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

## Next steps

Review the following articles in this section to learn more about building bots using the Bot Builder SDK for Node.js.

- [Get Started](bot-framework-nodejs-getstarted.md): Quickly build and test a simple bot by following instructions in this step-by-step tutorial.
- [Key concepts](bot-framework-nodejs-concepts.md): Learn about key concepts in the Bot Builder SDK for Node.js.
- **Tutorials**: See task-focused code samples that show how to use various features of the Bot Builder SDK for Node.js.


If you encounter problems or have suggestions regarding the Bot Builder SDK for Node.js, 
see [Support](resources-support.md) for a list of resources that provide opportunities 
for support and feedback. 
