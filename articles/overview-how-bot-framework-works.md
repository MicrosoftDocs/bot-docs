---
title: How the Bot Framework works | Microsoft Docs
description: The key features of the Bot Framework will help you build powerful and intelligent bots.
keywords:
author: RobStand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date:
ms.reviewer:

# Include the following line commented out
#ROBOTS: Index
---
>[!WARNING]
> Content is still under development.

# How the Bot Framework works
The Bot Framework provides tools and SDKs for building bots.

## Core concepts
Bots have dialogs, activities/messages, etc.

## Build with the SDK
SDK features

## Debug and test with the emulator
Use the emulator

## Connecting to channels
Channel features

## Publish with the portal

## Some content to leverage

### Bot Builder SDKs
These SDKs provide features such as dialogs and built-in prompts that make interacting with users much simpler. They provide access to Microsoft Cognitive Services APIs which give your bot more human-like intelligence. These APIs include LUIS for natural language understanding, Cortana for voice, and the Bing APIs for search. For more information about adding intelligence to your bot, see [Bot Intelligence](/en-us/bot-intelligence/getting-started/).
The Bot Builder SDKs are provided as open source on GitHub (see [BotBuilder](https://github.com/Microsoft/BotBuilder)).
- [Node.js SDK](https://github.com/Microsoft/BotBuilder/tree/master/Node)
- [.NET SDK](https://github.com/Microsoft/BotBuilder/tree/master/CSharp)

### Azure Bot Service
The Azure Bot Service provides an integrated environment that is purpose-built for bot development, enabling you to build, connect, test, deploy and manage intelligent bots, all from one place. You can write your bot in C# or Node.js directly in the browser using the Azure editor, without any need for a tool chain (local editor and source control).

For a walkthough of creating a bot using Azure Bot Service, see [Use Azure Bot Service](~/azure-bot-service/getstarted.md).

### Bot Framework REST API

As an alternative to using the Bot Builder SDK for .NET, the Bot Builder SDK for .NET, or the Azure Bot Service, you can create a bot with any programming language by using the Bot Framework REST API.
See the Bot Framework’s [REST API](/en-us/connector/overview/).


### Bot Framework Emulator
The Bot Framework Emulator is a desktop application that allows bot developers to test and debug their bots. The Bot Framework Emulator can communicate with your bot wherever it is running; on localhost, or remotely in the cloud.
For details on using the emulator, see [Bot Framework Emulator](~/debug-bots-emulator.md).

## Bot Connector
The Bot Connector service provides the connection from your bot to text/SMS, Skype, Slack, Facebook Messenger, Office 365 mail and other channels.
The bot you write exposes a Microsoft Bot Framework-compatible API on the Internet, which allows the Bot Framework Connector service to forward messages from your bot to a user, and send user messages back to your bot. The Connector also takes care of authentication messages.

<!--
There are different ways for your bot to communicate with the Connector.
The Node.JS or .NET SDKs provide built-in methods for connecting to the service. A simple bot using Node.JS demonstrates this in [Create a bot with the Bot Builder SDK for Node.js](~/nodejs/getstarted.md).
Bots built using .NET can also use the Bot Framework Connector SDK .NET template. -->

The Direct Line API enables you to host your bot in your app rather than using one of the automatically configured channels. The Direct Line API is intended for developers writing their own client applications, web chat controls, mobile apps, or service-to-service applications that will talk to their bot.
The framework also provides an embeddable Web chat control that lets you host from your website.

## Developer Portal
When you finish writing your bot, you need to register it, connect it to channels, and finally publish it, using the Developer Portal.
Registering your bot describes it to the framework, and it’s how you get the bot’s app ID and password that’s used for authentication.
Bots that you register are located at My bots in the portal.

For more information on publishing a bot, see [Publish a bot](https://review.docs.microsoft.com/en-us/botframework/~/deploy/overview).

## Bot Directory
The Bot Directory is a public directory of all bots that are registered and published with Microsoft Bot Framework. You are encouraged to always publish your bot because it makes it more discoverable.
For most channels, you can share your bot with users as soon as you configure the channel.
Users can select your bot in the directory and add it to one or more of the configured channels that they use, or use the Web chat control to try it.

<!-- If you configured your bot to work with Skype, you must publish your bot to the Bot Directory and Skype apps (see Publishing your bot) before users can start using it.
Although Skype is the only channel that requires you to publish your bot to the directory, you are encouraged to always publish your bot because it makes it more discoverable. -->

Publishing the bot submits it for review. For information about the review process, see Bot review guidelines. If your bot passes review, it’s added to the Bot Directory.