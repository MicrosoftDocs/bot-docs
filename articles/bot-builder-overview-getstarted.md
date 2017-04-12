---
title: Get started building bots with the Bot Framework | Microsoft Docs
description: Learn about the different features that Bot Builder provides for building a bot using the Bot Framework.
keywords:
author: kbrandl
manager: rstand
ms.topic: get-started-article
ms.prod: bot-framework
ms.date: 04/01/2017
ms.reviewer: rstand
#ROBOTS: Index
---
> [!WARNING]
> The content in this article is currently under development.

# Get started with the Bot Framework
Intro text here.

## Building bots
The Bot Framework includes the Bot Builder to help you build and connect intelligent bots. Bot Builder comprises three open-source SDKs that support .NET, Node.js, and REST.

### Bot Builder SDK for .NET
The Bot Builder SDK for .NET is a powerful framework for constructing bots that can handle both freeform interactions and more guided ones where the possibilities are explicitly shown to the user. It is easy to use and leverages C# to provide a familiar way for .NET developers to write bots. 

The [detailed walkthrough](~/dotnet/getstarted.md) using the Bot Builder SDK for .NET will get you started toward building your own bot.

### Bot Builder SDK for Node.js
The Bot Builder SDK for Node.js is a powerful framework for constructing bots that can handle both freeform interactions and more guided ones where the possibilities are explicitly shown to the user. It is easy to use and models frameworks like Express & Restify to provide a familiar way for JavaScript developers to write bots. 

The [detailed walkthrough](~/nodejs/getstarted.md) using the Bot Builder SDK for Node.js will get you started toward building your own bot.

### REST API
You can create a bot with any programming language by using the Bot Framework REST API. The Bot Framework REST API has three offerings to cover REST scenarios.

The [Bot Connector REST API](https://docs.botframework.com/en-us/restapi/connector/#navtitle) enables your bot to send and receive messages to channels configured in the [Bot Framework Developer Portal](https://dev.botframework.com/). 

The [Bot State REST API](https://docs.botframework.com/en-us/restapi/state/#navtitle) enables your bot to store and retrieve state associated with the conversations that are conducted through the Bot Connector REST API.

The [Direct Line REST API](https://docs.botframework.com/en-us/restapi/directline3/#navtitle) enables you to connect your own application, such as a client application, web chat control, or mobile app, directly to a single bot.

## Azure Bot Service
The Azure Bot Service provides an integrated environment that is purpose-built for bot development, 
enabling you to build, connect, test, deploy, and manage intelligent bots, all from one place. 
You can write your bot in C# or Node.js directly in the browser using the Azure editor, without any need for separate tools. If you want to use your own tools, like code editors and source control, you can. 

The [detailed walkthrough](~/azure-bot-service-getstarted.md) using the Azure Bot Service will get you started toward building your own bot.

## Debugging bots
To help you test and debug your bots, the Bot Framework includes the [Bot Framework Emulator](debug-bots-emulator.md). You can also debug and test your bots with Visual Studio, VS Code, or your own test harness.

## Deploying bots
After you have built and tested your bots, you register your bot, deploy it to the cloud, and configure it to work with channels.

> [!NOTE]
> If you built your bot using the Azure Bot Service, you don't need to register or deploy your bot.
> Bot registration and deployment are handled as part of the Azure Bot Service bot creation process.

### Register your bot with the Bot Framework

[Registering a bot](~/portal-register-bot.md) is a simple process. You provide some information about your bot and then generate the app ID and password that your bot will use to authenticate with the Bot Framework.

### Deploy your bot to the cloud

Before others can use your bot, you must deploy it to the cloud. You have a few different options:

- [Deploy from a local git repository](~/deploy-bot-local-git.md) using continuous integration
- [Deploy from Github](~/deploy-bot-github.md) using continous integration
- [Deploy from Visual Studio](~/deploy-bot-visual-studio.md)

## Configure your bot to run on one or more conversation channels
After you have registered your bot with the Bot Framework and deployed your bot to the cloud, you can [configure a bot to run on one or more channels](~/portal-configure-channels.md).

## Next steps
Add next steps content here.