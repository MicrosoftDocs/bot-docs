---
title: Bot Builder REST APIs  | Microsoft Docs
description: Get started with the Bot Framework REST APIs that can be used to build bots and clients that connect to bots.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer: 
---

# Bot Framework REST APIs

The Bot Framework REST APIs enable you to build bots that exchange messages with channels configured in the <a href="https://dev.botframework.com/" target="_blank">Bot Framework Portal</a>, store and retrieve state data, and connect your own client applications to your bots. All Bot Framework services use industry-standard REST and JSON over HTTPS.

## Build a bot

You can create a bot with any programming language by using the Bot Connector service to exchange messages with channels configured in the Bot Framework Developer Portal. You can use the Bot State service to store and retrieve state data that is related to conversations that your bot conducts using the Bot Connector service. 

> [!TIP]
> The Bot Framework provides client libraries that can be used to build bots in either C# or Node.js. 
> To build a bot using C#, use the [Bot Builder SDK for C#](~/dotnet/bot-builder-dotnet-overview.md). 
> To build a bot using Node.js, use the [Bot Builder SDK for Node.js](~/nodejs/index.md). 

To learn more about building bots using the Bot Connector service and the Bot State service, see [Key concepts](~/rest-api/bot-framework-rest-connector-concepts.md).

## Connect your bot to a client

You can enable your own client application to communicate with your bot by using the Direct Line API. 
The Direct Line API implements an authentication mechanism that uses standard secret/token patterns and provides a stable schema, even if your bot changes its protocol version. Your client can use the Direct Line API to send messages to your bot via an `HTTP POST` request. Your bot can use the Direct Line API to receive messages from a client either via `WebSocket` stream or by polling using `HTTP GET`. To learn more about connecting a client to your bot using the Direct Line API, see [Key concepts](~/rest-api/bot-framework-rest-direct-line-concepts.md). 


