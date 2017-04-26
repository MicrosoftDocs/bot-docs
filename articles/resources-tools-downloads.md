---
title: Bot Framework SDKs and tools | Microsoft Docs
description: Get the Bot Builder SDKs and tools for developing bots with the Bot Framework.
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 03/01/2017
ms.reviewer:
---
# Bot Framework SDKs and tools

The Bot Framework provides SDKs, tools, and resources to help you develop great bots.



## Bot Builder SDK for .NET

The Bot Builder SDK for .NET is available as a NuGet package. 
To install the SDK within a Visual Studio project, complete the following steps:

1. In **Solution Explorer**, right-click the project name and select **Manage NuGet Packages...**.
2. On the **Browse** tab, type "Microsoft.Bot.Builder" into the search box.
3. Select **Microsoft.Bot.Builder** in the list of results, click **Install**, and accept the changes.

You can also [download the Bot Builder SDK for .NET source code from Github](https://github.com/Microsoft/BotBuilder/tree/master/CSharp).

## Get the Bot Builder SDK for Node.js
To install the Bot Builder for Node.js SDK and its dependencies, first create a folder for your bot, navigate to it, and run the following **npm** command:

```
npm init
```

Next, install the Bot Builder SDK for Node.js and <a href="http://restify.com/" target="_blank">Restify</a> modules by running the following **npm** commands:

```
npm install --save botbuilder
npm install --save restify
```

You can also [download the Bot Builder SDK for Node.js source code from Github](https://github.com/Microsoft/BotBuilder/tree/master/Node).

## Downloads
- [Bot Framework Emulator (Mac and Windows)](https://emulator.botframework.com/)
- [Visual Studio Template - C#](http://aka.ms/bf-bc-vstemplate)

## Additinoal tools

- [Vorlon.js](http://vorlonjs.io) is a remote debugging tool for JavaScript code. You can use it to inspect any bot created in Node.js ([read why and how](http://aka.ms/botinspector)). You can easily see your dialog stacks and understand what is happening inside of your bot.

