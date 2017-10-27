---
title: Start building bots with the Bot Framework | Microsoft Docs
description: Get started building powerful bots with the Bot Framework with the tools and resources you need.
author: robstand
ms.author: rstand
manager: rstand
ms.topic: get-started-article
ms.prod: bot-framework
ms.date: 10/25/2017
---
# Build bots with the Bot Framework
The Bot Framework provides services, libraries, samples, and tools to help you build and debug bots. The Azure Bot Service is the best way to get started with building bots. With the Azure Bot Service, you can create and deploy a bot in just a few minutes. You can use the Bot Builder SDK to create a bot from scratch using C# or Node.js. And if you want to create a bot using any programming language you want, you can use the REST API.

## Azure Bot Service
Azure Bot Service provides an integrated environment that is purpose-built for bot development, 
enabling you to build, connect, test, deploy, and manage intelligent bots, all from one place. 
You can write your bot in C# or Node.js directly in the browser using the Azure editor. Your bot is automatically
deployed to Azure.

The [Azure Bot Service Quickstart]((~/azure-bot-service-quickstart.md) will guide you through creating a bot with the Azure Bot Service.

## Bot Builder
The Bot Framework includes Bot Builder to help you develop bots. Bot Builder is an open-source SDK with support for .NET and Node.js. Using Bot Builder, you can get started and have a working bot in just a few minutes.

### Bot Builder SDK for .NET
The Bot Builder SDK for .NET leverages C# to provide a familiar way for .NET developers to write bots. It is a powerful framework for constructing bots that can handle both free-form interactions and more guided conversations where the user selects from possible values. 

The [.NET Quickstart](~/dotnet/bot-builder-dotnet-quickstart.md) will guide you through creating a bot with the Bot Builder SDK for .NET.

The Bot Builder SDK for .NET is available as a [NuGet package](https://www.nuget.org/packages/Microsoft.Bot.Builder/).

> [!IMPORTANT]
> The Bot Builder SDK for .NET requires .NET Framework 4.6 or newer. If you are adding the SDK to an existing project
> targeting a lower version of the .NET Framework, you will need to update your project to target .NET Framework 4.6 first.

To install the SDK in an existing Visual Studio project, complete the following steps:

1. In **Solution Explorer**, right-click the project name and select **Manage NuGet Packages...**.
2. On the **Browse** tab, type "Microsoft.Bot.Builder" into the search box.
3. Select **Microsoft.Bot.Builder** in the list of results, click **Install**, and accept the changes.

You can also download the Bot Builder SDK for .NET [source code](https://github.com/Microsoft/BotBuilder/tree/master/CSharp) from GitHub.

#### Visual Studio project templates
Download Visual Studio project templates to accelerate bot development.

* [Bot template for Visual Studio][bot-template] for developing bots with C#
* [Cortana skill template for Visual Studio][cortana-template] for developing Cortana skills with C#

> [!TIP]
> <a href="https://docs.microsoft.com/en-us/visualstudio/ide/how-to-locate-and-organize-project-and-item-templates" target="_blank">Learn more</a> about how to install Visual Studio 2017 project templates.

### Bot Builder SDK for Node.js
The Bot Builder SDK for Node.js provides a familiar way for Node.js developers to write bots. You can use it to build a wide variety of conversational user interfaces, from simple prompts to free-form conversations. The Bot Builder SDK for Node.js uses restify, a popular framework for building web services, to create the bot's web server. The SDK is also compatible with Express. 

The [Node.js Quickstart](~/nodejs/bot-builder-nodejs-quickstart.md) will guide you through creating a bot with the Bot Builder SDK for Node.js. 

The Bot Builder SDK for Node.js is available as an npm package. 
To install the Bot Builder for Node.js SDK and its dependencies, first create a folder for your bot, navigate to it, and run the following **npm** command:

```nodejs
npm init
```

Next, install the Bot Builder SDK for Node.js and <a href="http://restify.com/" target="_blank">Restify</a> modules by running the following **npm** commands:

```nodejs
npm install --save botbuilder
npm install --save restify
```

You can also download the Bot Builder SDK for Node.js [source code](https://github.com/Microsoft/BotBuilder/tree/master/Node) from GitHub.

## REST API

You can create a bot with any programming language by using the Bot Framework REST API. The [REST Quickstart](rest-api/bot-framework-rest-connector-quickstart.md) will guide you through creating a bot with REST.

There are three REST APIs in the Bot Framework:

 - The [Bot Connector REST API][connectorAPI] enables your bot to send and receive messages to channels configured in the [Bot Framework Portal](https://dev.botframework.com/). 

- The [Bot State REST API][stateAPI] enables your bot to store and retrieve state associated with the conversations that are conducted through the Bot Connector REST API.

- The [Direct Line REST API][directLineAPI] enables you to connect your own application, such as a client application, web chat control, or mobile app, directly to a single bot.

## Bot Framework Emulator
The Bot Framework Emulator is a desktop application that allows you to test and debug you bots, either locally or remotely. Using the emulator, you can chat with your bot and inspect the messages that your bot sends and receives. [Learn more about the Bot Framework emulator](~/debug-bots-emulator.md) and [download the emulator](http://emulator.botframework.com) for your platform.

## Bot Framework Channel Inspector
Quickly see what bot features will look like on different channels with the Bot Framework [Channel Inspector](portal-channel-inspector.md).

[bot-template]: http://aka.ms/bf-bc-vstemplate
[cortana-template]: https://aka.ms/bf-cortanaskill-template


[connectorAPI]: https://docs.botframework.com/en-us/restapi/connector/#navtitle
 
[stateAPI]: https://docs.botframework.com/en-us/restapi/state/#navtitle

[directLineAPI]: https://docs.botframework.com/en-us/restapi/directline3/#navtitle
