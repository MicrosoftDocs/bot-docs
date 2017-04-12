---
title: Get started building bots with the Bot Framework | Microsoft Docs
description: Learn how to get started building powerful bots with the Bot Framework.
keywords:
author: kbrandl
manager: rstand
ms.topic: get-started-article
ms.prod: bot-framework
ms.date: 04/01/2017
ms.reviewer: rstand
#ROBOTS: Index
---
# Get started building bots with Bot Builder
Bot Builder is an open-source SDK for building bots with support for .NET, Node.js, and REST. 

## Bot Builder SDK for .NET
The Bot Builder SDK for .NET leverages C# to provide a familiar way for .NET developers to write bots. The [detailed walkthrough](~/dotnet/getstarted.md) will guide you through creating a bot with the Bot Builder SDK for .NET.

## Bot Builder SDK for Node.js
The Bot Builder SDK for Node.js leaverages frameworks like Express & Restify to provide a familiar way for JavaScript developers to write bots. The [detailed walkthrough](~/nodejs/getstarted.md) guide you through creating a bot with the Bot Builder SDK for Node.js. 

## REST API
You can create a bot with any programming language by using the Bot Framework REST API. The Bot Framework REST API has three offerings to cover REST scenarios.

The [Bot Connector REST API][connectorAPI] enables your bot to send and receive messages to channels configured in the [Bot Framework Developer Portal](https://dev.botframework.com/). 

The [Bot State REST API][stateAPI] enables your bot to store and retrieve state associated with the conversations that are conducted through the Bot Connector REST API.

The [Direct Line REST API][directLineAPI] enables you to connect your own application, such as a client application, web chat control, or mobile app, directly to a single bot.

## Azure Bot Service
The Azure Bot Service provides an integrated environment that is purpose-built for bot development, 
enabling you to build, connect, test, deploy, and manage intelligent bots, all from one place. 
You can write your bot in C# or Node.js directly in the browser using the Azure editor.

The [detailed walkthrough](~/azure-bot-service-getstarted.md) will guide you through building a bot with the Azure Bot Service.

[connectorAPI]: https://docs.botframework.com/en-us/restapi/connector/#navtitle
 
[stateAPI]: https://docs.botframework.com/en-us/restapi/state/#navtitle

[directLineAPI]: https://docs.botframework.com/en-us/restapi/directline3/#navtitle