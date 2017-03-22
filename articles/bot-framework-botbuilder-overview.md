---
title: About Bot Builder | Microsoft Docs
description: Learn about the different means that Bot Builder provides for building a bot using the Bot Framework.
keywords: Bot Framework, Bot Builder, SDK, REST, Azure Bot Service
author: kbrandl
manager: rstand
ms.topic: get-started-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/17/2017
ms.reviewer:
#ROBOTS: Index
---
# Get started with the Bot Framework

The Bot Framework enables you to build and connect intelligent bots to interact with your users naturally through the applications that they use, including SMS, Facebook Messenger, Skype, and Slack, among others. 

The Bot Framework provides four ways to provides to build bots depending on your language of choice

- [Bot Builder SDK for .NET](#dotnet) 
- [Bot Builder SDK for Node.js](#node)
- [Azure Bot Service](#azure)

## <a id="dotnet"></a>Bot Builder SDK for .NET
The Bot Builder SDK for .NET is a powerful framework for constructing bots that can handle both freeform interactions and more guided ones where the possibilities are explicitly shown to the user. It is easy to use and leverages C# to provide a familiar way for .NET developers to write bots. 

The [detailed walkthrough](bot-framework-dotnet-getstarted.md) using the Bot Builder SDK for .NET will get you started toward building your own bot.

## <a id="node"></a>Bot Builder SDK for Node.js
The Bot Builder SDK for Node.js is a powerful framework for constructing bots that can handle both freeform interactions and more guided ones where the possibilities are explicitly shown to the user. It is easy to use and models frameworks like Express & Restify to provide a familiar way for JavaScript developers to write bots. 

The [detailed walkthrough](bot-framework-nodejs-getstarted.md) using the Bot Builder SDK for Node.js will get you started toward building your own bot.

## <a id="azure"></a>Azure Bot Service
The Azure Bot Service provides an integrated environment that is purpose-built for bot development, 
enabling you to build, connect, test, deploy and manage intelligent bots, all from one place. 
You can write your bot in C# or Node.js directly in the browser using the Azure editor, without any need for a tool chain (local editor and source control). 

The [detailed walkthrough](bot-framework-azure-getstarted.md) using the Azure Bot Service will get you started toward building your own bot.

## REST API
You can create a bot with any programming language by using the Bot Framework REST API. The Bot Framework REST API has three offerings to cover REST scenarios.

The [Bot Connector REST API](https://docs.botframework.com/en-us/restapi/connector/#navtitle) enables your bot to send and receive messages to channels configured in the [Bot Framework Developer Portal](https://dev.botframework.com/). 

The [Bot State REST API](https://docs.botframework.com/en-us/restapi/state/#navtitle) enables your bot to store and retrieve state associated with the conversations that are conducted through the Bot Connector REST API.

The [Direct Line REST API](https://docs.botframework.com/en-us/restapi/directline3/#navtitle) enables you to connect your own application, such as a client application, web chat control, or mobile app, directly to a single bot.