---
title: Bot Framework REST APIs  - Bot Service
description: Get started with the Bot Framework REST APIs that can be used to build bots and clients that connect to bots.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: bot-service
ms.date: 11/01/2021
---

# Bot Framework REST APIs

Most Bot Framework bots are built using the Bot Framework SDK, which organizes your bot and handles all conversations for you. An alternative to using the SDK is to send messages directly to the bot using a REST API.

## Build a bot

By coding with Bot Framework REST APIs, you can send and receive messages with users on any channel configured in your bot's Azure AI Bot Service registration.

> [!TIP]
> The Bot Framework provides client libraries that can be used to build bots in either C# or Node.js.
> To build a bot using C#, use the [Bot Framework SDK for C#](../dotnet/bot-builder-dotnet-overview.md).
> To build a bot using Node.js, use the [Bot Framework SDK for Node.js](../nodejs/index.md).

Refer to the [Azure AI Bot Service](../bot-service-overview-introduction.md) docs to learn more about building bots using the service.

## Build a Direct Line client

Most channels such as Facebook, Teams, or Slack provide clients, but with Direct Line you can enable your own client application to communicate with your bot. [Web Chat](https://github.com/microsoft/BotFramework-WebChat) is an open source example of a Direct Line client, and it can be used as-is or modified or learned from when making your own client. The Direct Line API implements an authentication mechanism that uses standard secret/token patterns and provides a stable schema, even if your bot changes its protocol version. To learn more about using the Direct Line API to enable communication between a client and your bot, see [Key concepts](bot-framework-rest-direct-line-3-0-concepts.md).

Direct Line clients can be in different languages and locations (for example, a desktop app instead of a web page). For more information, see [About Direct Line](../bot-service-channel-directline.md).
