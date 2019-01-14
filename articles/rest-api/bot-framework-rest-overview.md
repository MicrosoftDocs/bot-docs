---
title: Bot Framework REST APIs  | Microsoft Docs
description: Get started with the Bot Framework REST APIs that can be used to build bots and clients that connect to bots.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# Bot Framework REST APIs
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-overview.md)
> - [Node.js](../nodejs/bot-builder-nodejs-overview.md)
> - [REST](../rest-api/bot-framework-rest-overview.md)

The Bot Framework REST APIs enable you to build bots that exchange messages with channels configured in the <a href="https://dev.botframework.com/" target="_blank">Bot Framework Portal</a>, store and retrieve state data, and connect your own client applications to your bots. All Bot Framework services use industry-standard REST and JSON over HTTPS.

## Build a bot

You can create a bot with any programming language by using the Bot Connector service to exchange messages with channels configured in the Bot Framework Portal. 

> [!TIP]
> The Bot Framework provides client libraries that can be used to build bots in either C# or Node.js. 
> To build a bot using C#, use the [Bot Framework SDK for C#](../dotnet/bot-builder-dotnet-overview.md). 
> To build a bot using Node.js, use the [Bot Framework SDK for Node.js](../nodejs/index.md). 

To learn more about building bots using the Bot Connector service, see [Key concepts](bot-framework-rest-connector-concepts.md).

## Build a client

You can enable your own client application to communicate with your bot by using the Direct Line API. The Direct Line API implements an authentication mechanism that uses standard secret/token patterns and provides a stable schema, even if your bot changes its protocol version. To learn more about using the Direct Line API to enable communication between a client and your bot, see [Key concepts](bot-framework-rest-direct-line-3-0-concepts.md). 