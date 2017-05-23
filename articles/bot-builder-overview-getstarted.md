---
title: Start building bots with the Bot Framework | Microsoft Docs
description: Get started building powerful bots with the Bot Framework and Bot Builder SDKs.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: get-started-article
ms.prod: bot-framework
ms.date: 
ms.reviewer: 
---

# Start building bots with the Bot Framework

The Bot Framework includes Bot Builder to give you the tools you need to develop bots. Bot Builder is an open-source SDK with support for .NET, Node.js, and REST. Using Bot Builder, you can get started and have a working bot in just a few minutes.

## Bot Builder SDK for .NET

The Bot Builder SDK for .NET leverages C# to provide a familiar way for .NET developers to write bots. The [detailed walkthrough](~/dotnet/bot-builder-dotnet-quickstart.md) will guide you through creating a bot with the Bot Builder SDK for .NET.

## Bot Builder SDK for Node.js

The Bot Builder SDK for Node.js leverages frameworks like Express & Restify to provide a familiar way for JavaScript developers to write bots. The [detailed walkthrough](~/nodejs/bot-builder-nodejs-quickstart.md) guides you through creating a bot with the Bot Builder SDK for Node.js. 

## Azure Bot Service

The Azure Bot Service provides an integrated environment that is purpose-built for bot development, 
enabling you to build, connect, test, deploy, and manage intelligent bots, all from one place. 
You can write your bot in C# or Node.js directly in the browser using the Azure editor. Your bot is automatically
deployed to Azure.

The [detailed walkthrough](~/azure/azure-bot-service-quickstart.md) will guide you through building a bot with the Azure Bot Service.

## REST API

You can create a bot with any programming language by using the Bot Framework REST API. There are three REST APIs in the Bot Framework:

 - The [Bot Connector REST API][connectorAPI] enables your bot to send and receive messages to channels configured in the [Bot Framework Portal](https://dev.botframework.com/). 

- The [Bot State REST API][stateAPI] enables your bot to store and retrieve state associated with the conversations that are conducted through the Bot Connector REST API.

- The [Direct Line REST API][directLineAPI] enables you to connect your own application, such as a client application, web chat control, or mobile app, directly to a single bot.

[connectorAPI]: https://docs.botframework.com/en-us/restapi/connector/#navtitle
 
[stateAPI]: https://docs.botframework.com/en-us/restapi/state/#navtitle

[directLineAPI]: https://docs.botframework.com/en-us/restapi/directline3/#navtitle