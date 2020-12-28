---
title: Bot Framework Frequently Asked Questions Availability - Bot Service
description: Frequently Asked Questions about Bot Framework background and availability.
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/28/2020
---

# Background and availability

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

<!-- Attention writers!!
     1 - This article contains FAQs regarding Bot Framework background and availability.
     1 - When you create a new FAQ, please add the related link to the proper section in bot-service-resources-bot-framework-faq.md.-->

## Why did Microsoft develop the Bot Framework?

We created the Bot Framework to make it easier for developers to build and connect great bots to users, wherever they converse, including on Microsoft's premier channels.

## How can I migrate Azure Bot Service from one region to another?

Azure Bot Service does not support region move. It's a global service that is not tied to any specific region.

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

## What is the v4 SDK?

Bot Framework v4 SDK builds on the feedback and learnings from the prior Bot Framework SDK versions. It introduces the right levels of abstraction while enabling rich set of components as the bot building blocks. You can start with a simple bot and grow it in sophistication using a modular and extensible framework. See also [What's new with Bot Framework](https://github.com/Microsoft/botbuilder-dotnet/wiki/FAQ) on GitHub.

## Bot Framework SDK Version 3 Lifetime Support and Deprecation Notice

Microsoft Bot Framework SDK V4 was released in September 2018, and since then we have shipped a few dot-release improvements. As announced previously, the V3 SDK is being retired. Accordingly, there will be no more development in V3 repositories. **Existing V3 bot workloads will continue to run without interruption. We have no plans to disrupt any running workloads**.

As mentioned, Bot Builder SDK V3 bots continue to run and be supported by Azure Bot Service. Bot Builder SDK V3 will only be supported  for critical security bug fixes, connector, and protocol layer compatibility updates.

All new features and capabilities are developed exclusively on [Bot Framework SDK V4](https://github.com/microsoft/botframework-sdk).  Customers are encouraged to migrate their bots to V4 as soon as possible.

We highly recommend that you start migrating your V3 bots to V4. In order to support this migration we have produced migration documentation and will provide extended support for migration initiatives (via standard channels such as Stack Overflow and Microsoft Customer Support).

### V3 Status Summary

#### ABS Service
1. The ABS service side will continue to support running V3 bots with no planned end of life and any running bots will not be disrupted.
1. Channels will remain compatible with V3 with no disruption or end of life plan.
1. Creation of new V3 bots is disabled on the portal; however, expert users who wish to deploy their V3 bots independently, not on ABS (e.g. as webapp service) can do so.

#### SDK and Tools
1.    We are not investing in V3 from SDK side, and will only apply critical security fixes to the SDK branches for the foreseeable future (Exception: We plan to add a Skills connector to allow V4 bots to call legacy V3 bots).
2.    SDKs and tools development is exclusively on V4 with no V3 work done or planned (hence we're already "there").
3.    We do not prevent anyone from running old tools to manage their V3 bots.

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

## My current website integrates Web Chat using an `<iframe>` element obtained from Azure Bot Services. I want to upgrade to v4.

Starting from May 2019, we are rolling out v4 to websites that integrate Web Chat using `<iframe>` element. Please refer to [the embed documentation](https://github.com/Microsoft/BotFramework-WebChat/tree/master/packages/embed) for information on integrating Web Chat using `<iframe>`.

## My website is integrated with Web Chat v3 and uses customization options provided by Web Chat, no customization at all, or very little of my own customization that was not available with Web Chat.

Please follow the implementation of sample [`00.migration/a.v3-to-v4`](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/00.migration/a.v3-to-v4) to convert your webpage from v3 to v4 of Web Chat.

## My website is integrated with a fork of Web Chat v3. I have implemented a lot of customization in my version of Web Chat, and I am concerned v4 is not compatible with my needs.

One of our team's favorite things about v4 of Web Chat is the ability to add customization **without the need to fork Web Chat**. Although this creates additional overhead for v3 users who forked Web Chat previously, we will do our best to support customers making the jump. Please use the following suggestions:

-  Take a look at the implementation of sample [`00.migration/a.v3-to-v4`](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/00.migration/a.v3-to-v4). This is a great starting place to get Web Chat up and running.
-  Next, please go through the [samples list](https://aka.ms/botframework-webchat-samples) to compare your customization requirements to what Web Chat has already provided support for. These samples are made up of commonly asked-for features for Web Chat.
-  If one or more of your features is not available in the samples, please look through our [open and closed issues](https://github.com/Microsoft/BotFramework-WebChat/issues?utf8=%E2%9C%93&q=is%3Aissue+), [Samples label](https://github.com/Microsoft/BotFramework-WebChat/issues?utf8=%E2%9C%93&q=is%3Aissue+is%3Aopen+label%3ASample), and the [Migration Support label](https://github.com/Microsoft/BotFramework-WebChat/issues?q=is%3Aissue+migrate+label%3A%22Migration+Support%22) to search for sample requests and/or customization support for a feature you are looking for. Adding your comment to open issues will help the team prioritize requests that are in high demand, and we encourage participation in our community.
-  If you did not find your feature in the list of open requests, please feel free to [file your own request](https://github.com/Microsoft/BotFramework-WebChat/issues/new). Just like the item above, other customers adding comments to your open issue will help us prioritize which features are most commonly needed across Web Chat users.
-  Finally, if you need your feature as soon as possible, we welcome [pull requests](https://github.com/Microsoft/BotFramework-WebChat/compare) to Web Chat. If you have the coding experience to implement the feature yourself, we would very much appreciate the additional support! Creating the feature yourself will mean that it is available for your use on Web Chat more quickly, and that other customers looking for the same or similar feature may utilize your contribution.
-  Make sure to check out the rest of this `README` to learn more about v4.




