---
title: Download the Bot Builder SDK and tools | Microsoft Docs
description: Get the Bot Builder SDKs and tools for developing bots with the Bot Framework.
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 06/27/2017
ms.reviewer:
---
# Bot Framework SDK and tools

The Bot Builder SDK provides libraries, samples, and tools to help you build and debug bots. To get started building bots, download the SDK for the platform you are using, either C# on .NET or Node.js. You can download the SDK as packages or you can get the source code.

## Get the Bot Builder SDK for .NET
The Bot Builder SDK for .NET leverages C# to provide a familiar way for .NET developers to write bots. It is a powerful framework for constructing bots that can handle both free-form interactions and more guided conversations where the user selects from possible values.

The Bot Builder SDK for .NET is available as a [NuGet package](https://www.nuget.org/packages/Microsoft.Bot.Builder/).

> [!IMPORTANT]
> The Bot Builder SDK for .NET requires .NET Framework 4.6 or newer. If you are adding the SDK to an existing project
> targeting a lower version of the .NET Framework, you will need to update your project to target .NET Framework 4.6 first.

To install the SDK in an existing Visual Studio project, complete the following steps:

1. In **Solution Explorer**, right-click the project name and select **Manage NuGet Packages...**.
2. On the **Browse** tab, type "Microsoft.Bot.Builder" into the search box.
3. Select **Microsoft.Bot.Builder** in the list of results, click **Install**, and accept the changes.

You can also download the Bot Builder SDK for .NET [source code](https://github.com/Microsoft/BotBuilder/tree/master/CSharp) from GitHub.

### Visual Studio project templates
Download Visual Studio project templates to accelerate bot development.

* [Bot template for Visual Studio][bot-template] for developing bots with C#
* [Cortana skill template for Visual Studio][cortana-template] for developing Cortana skills with C#

> [!TIP]
> <a href="https://docs.microsoft.com/en-us/visualstudio/ide/how-to-locate-and-organize-project-and-item-templates" target="_blank">Learn more</a> about how to install Visual Studio 2017 project templates.

## Bot Builder SDK for Node.js
The Bot Builder SDK for Node.js provides a familiar way for Node.js developers to write bots. You can use it to build a wide variety of conversational user interfaces, from simple prompts to free-form conversations. The Bot Builder SDK for Node.js uses restify, a popular framework for building web services, to create the bot's web server. The SDK is also compatible with Express.

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

## Bot Framework Emulator
The Bot Framework Emulator is a desktop application that allows you to test and debug you bots, either locally or remotely. Using the emulator, you can chat with your bot and inspect the messages that your bot sends and receives. [Learn more about the Bot Framework emulator](~/debug-bots-emulator.md) and [download the emulator](http://emulator.botframework.com) for your platform.

## Bot Framework Channel Inspector
Quickly see what bot features will look like on different channels with the the Bot Framework [Channel Inspector](https://docs.botframework.com/en-us/channel-inspector/channels/Skype/#navtitle).

[bot-template]: http://aka.ms/bf-bc-vstemplate
[cortana-template]: https://aka.ms/bf-cortanaskill-template
