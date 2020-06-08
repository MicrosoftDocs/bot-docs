---
title: Bot Service Frequently Asked Questions Availability - Bot Service
description: Frequently Asked Questions about Bot Framework and when availability.
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 06/08/2020
---

# Background and availability

- [Why did Microsoft develop the Bot Framework?](#Why-did-Microsoft-develop-the-Bot-Framework?)
- [What is the v4 SDK?](#What-is-the-v4-SDK?)
- [How to run a bot offline?](#How-to-run-a-bot-offline?)
- [Bot Framework SDK Version 3 Lifetime Support and Deprecation Notice](#Bot-Framework-SDK-Version-3-Lifetime-Support-and-Deprecation-Notice)
- [How can I migrate Azure Bot Service from one region to another?](#How-can-I-migrate-Azure-Bot-Service-from-one-region-to-another?)

---

## Why did Microsoft develop the Bot Framework?

We created the Bot Framework to make it easier for developers to build and connect great bots to users, wherever they converse, including on Microsoft's premier channels.

## What is the v4 SDK?
Bot Framework v4 SDK builds on the feedback and learnings from the prior Bot Framework SDKs. It introduces the right levels of abstraction while enabling rich componentization of the bot building blocks. You can start with a simple bot and grow your bot in sophistication using a modular and extensible framework. You can find [FAQ](https://github.com/Microsoft/botbuilder-dotnet/wiki/FAQ) for the SDK on GitHub.

## How to run a bot offline?

<!-- WIP -->
Before talking about the use of a bot offline, meaning a bot not deployed on Azure or on some other host services but on premises, let's clarify a few points.

- A bot is a web service that does not have a UI, so the user must interact with it via other means, in the form of channels, which use the [Bot Framework Service](rest-api/bot-framework-rest-connector-concepts.md). The connector functions as a *proxy* to relay messages between a client and the bot.
- The **connector** is a global application hosted on Azure nodes and spread geographically for availability and scalability.
- You use the [Bot Channel Registration](bot-service-quickstart-registration.md) to register the bot with the connector.
    >[!NOTE]
    > The bot must have its endpoint publicly reachable by the connector.

You can run a bot offline with limited capabilities. For example, if you want to use a bot offline that has LUIS capability, you must build a container for the bot, and required tools, and a container for LUIS. Both connected via Docker Compose bridged network.

This is a "partial" offline solution because a Cognitive Services container needs periodic online connection.

> [!NOTE]
> The QnA service is not supported in a bot running offline.

For more information, see:

- [Deploy the Language Understanding (LUIS) container to Azure Container Instances](https://docs.microsoft.com/azure/cognitive-services/luis/deploy-luis-on-container-instances)
- [Container support in Azure Cognitive Services](https://docs.microsoft.com/azure/cognitive-services/cognitive-services-container-support)

## Bot Framework SDK Version 3 Lifetime Support and Deprecation Notice

Microsoft Bot Framework SDK V4 was released in September 2018, and since then we have shipped a few dot-release improvements. As announced previously, the V3 SDK is being retired. Accordingly, there will be no more development in V3 repositories. **Existing V3 bot workloads will continue to run without interruption. We have no plans to disrupt any running workloads**.

As mentioned, Bot Builder SDK V3 bots continue to run and be supported by Azure Bot Service. Bot Builder SDK V3 will only be supported  for critical security bug fixes, connector, and protocol layer compatibility updates.

All new features and capabilities are developed exclusively on [Bot Framework SDK V4](https://github.com/microsoft/botframework-sdk).  Customers are encouraged to migrate their bots to V4 as soon as possible.

We highly recommend that you start migrating your V3 bots to V4. In order to support this migration we have produced migration documentation and will provide extended support for migration initiatives (via standard channels such as Stack Overflow and Microsoft Customer Support).


For more information please refer to the following references:
* [Essential Migration Guidance](https://aka.ms/bf-migration-overview)
* Primary V4 Repositories to develop Bot Framework bots
  * [Botbuilder for dotnet](https://github.com/microsoft/botbuilder-dotnet)
  * [Botbuilder for JS](https://github.com/microsoft/botbuilder-js)
* QnA Maker Libraries were replaced with the following V4 libraries:
  * [Libraries for dotnet](https://github.com/Microsoft/botbuilder-dotnet/tree/master/libraries/Microsoft.Bot.Builder.AI.QnA)
  * [Libraries for JS](https://github.com/Microsoft/botbuilder-js/blob/master/libraries/botbuilder-ai/src/qnaMaker.ts)
* Azure Libraries were replaced with the following V4 libraries:
  * [Botbuilder for JS Azure](https://github.com/Microsoft/botbuilder-js/tree/master/libraries/botbuilder-azure)
  * [Botbuilder for dotnet Azure](https://github.com/Microsoft/botbuilder-dotnet/tree/master/libraries/Microsoft.Bot.Builder.Azure)

### V3 Status Summary

#### ABS Service
1. The ABS service side will continue to support running V3 bots with no planned end of life and any running bots will not be disrupted.
1. Channels will remain compatible with V3 with no disruption or end of life plan.
1. Creation of new V3 bots is disabled on the portal; however, expert users who wish to deploy their V3 bots independently, not on ABS (e.g. as webapp service) can do so.

#### SDK and Tools
1.    We are not investing in V3 from SDK side, and will only apply critical security fixes to the SDK branches for the foreseeable future (Exception: We plan to add a Skills connector to allow V4 bots to call legacy V3 bots).
2.    SDKs and tools development is exclusively on V4 with no V3 work done or planned (hence we're already "there").
3.    We do not prevent anyone from running old tools to manage their V3 bots.

## How can I migrate Azure Bot Service from one region to another?

Azure Bot Service does not support region move. It's a global service that is not tied to any specific region.
