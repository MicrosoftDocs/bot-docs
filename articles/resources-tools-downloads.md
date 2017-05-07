---
title: Download and use the Bot Framework SDKs and tools | Microsoft Docs
description: Get the Bot Builder SDKs and tools for developing bots with the Bot Framework.
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---
# Bot Framework SDKs and tools

Get started building bots with the Bot Builder SDK. With the Bot Builder SDK, you can build bots using .NET or Node.js.

## Get the Bot Builder SDK for .NET

The Bot Builder SDK for .NET is available as a [NuGet package](https://www.nuget.org/packages/Microsoft.Bot.Builder/).
To install the SDK in a Visual Studio project, complete the following steps:

1. In **Solution Explorer**, right-click the project name and select **Manage NuGet Packages...**.
2. On the **Browse** tab, type "Microsoft.Bot.Builder" into the search box.
3. Select **Microsoft.Bot.Builder** in the list of results, click **Install**, and accept the changes.

You can also download the Bot Builder SDK for .NET [source code](https://github.com/Microsoft/BotBuilder/tree/master/CSharp) from GitHub.

### Visual Studio project templates
Download Visual Studio project templates to accelerate bot development.

* [Bot template for Visual Studio][bot-template] for developing bots with C#
* [Cortana skill template for Visual Studio][cortana-template] for developing Cortana skills with C#

## Bot Builder SDK for Node.js

The Bot Builder SDK for Node.js is available as an npm package. 
To install the Bot Builder for Node.js SDK and its dependencies, first create a folder for your bot, navigate to it, and run the following **npm** command:

```
npm init
```

Next, install the Bot Builder SDK for Node.js and <a href="http://restify.com/" target="_blank">Restify</a> modules by running the following **npm** commands:

```
npm install --save botbuilder
npm install --save restify
```

You can also download the Bot Builder SDK for Node.js [source code](https://github.com/Microsoft/BotBuilder/tree/master/Node) from GitHub.

## Bot Framework Emulator
The Bot Framework Emulator is a desktop application that allows you to test and debug you bots, either locally or remotely. Using the emulator, you can chat with your bot and inspect the messages that your bot sends and receives. [Learn more about the Bot Framework emulator](~/debug-bots-emulator.md) and [download the emulator](http://emulator.botframework.com) for your platform.

## Bot Framework Channel Inspector
Quickly see what bot features will look like on different channels with the the Bot Framework [Channel Inspector](https://docs.botframework.com/en-us/channel-inspector/channels/Skype/#navtitle).

[bot-template]: http://aka.ms/bf-bc-vstemplate
[cortana-template]: https://aka.ms/bf-cortanaskill-template