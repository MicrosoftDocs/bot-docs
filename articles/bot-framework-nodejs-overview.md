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
- Built-in support powerful AI frameworks such as <a href="http://luis.ai" target="_blank">LUIS</a>.
- Built-in recognizers and event handlers that guide the user through the 
conversation, providing help, navigation, clarification, and confirmation as needed.

## Get the SDK

Clohe the <a href="https://github.com/Microsoft/BotBuilder" target="_blank">BotBuilder</a> GitHub repository. 

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

## Next steps

Review the following articles in this section to learn more about building bots using the Bot Builder SDK for Node.js.

- [Get Started](bot-framework-nodejs-getstarted.md): Quickly build and test a simple bot by following instructions in this step-by-step tutorial.
- [Key concepts](bot-framework-nodejs-concepts.md): Learn about key concepts in the Bot Builder SDK for Node.js.
- **How to**: See task-focused code samples that show how to use various features of the Bot Builder SDK for Node.js.
<!-- 
- Debugging: Learn how to debug a bot that was built using the Bot Builder SDK for Node.js.
-->

If you encounter problems or have suggestions regarding the Bot Builder SDK for Node.js, 
see [Support](bot-framework-resources-support.md) for a list of resources that provide opportunities 
for support and feedback. 
