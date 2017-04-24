---
title: How the Bot Framework works | Microsoft Docs
description: The key features of the Bot Framework will help you build powerful and intelligent bots.
keywords:
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date:
ms.reviewer:
ROBOTS: Index, Follow
---
>[!WARNING]
> Content is still under development.

# How the Bot Framework works

In order to perform even the most basic tasks, a bot must be able to:

1. Accept the user's input and provide relevant output
2. Understand the user's language and intent
3. Communicate with the user via a preferred channel

Bots that are able to communicate with users in a comfortable, helpful way are considered intelligent.
The Bot Framework provides both a platform and comprehensive toolkit for building, connecting, and deploying intelligent bots.

## Before you begin, design

Think about what you want your bot to do and how. Whether it's simple text Q&A or a rich, interactive exchange, consider what value the bot will provide. To help, we've compiled some [design documentation](bot-design-best-practices.md) to help you plan for key user interactions.

## Choose your development environment

The Bot Framework provides a spectrum of development options.

### Build with the Bot Service

The Azure Bot Service provides an integrated environment purpose-built for bot development. You can write a bot in C# or Node.js, connect, test, deploy and manage it from your web broweser with no separate editor or source control required. For simple bots, you can instantiate an instance of a service without writing code at all.

### Build with the SDK

The SDKs provide features such as dialogs, APIs and built-in prompts to support more complex user interactions without having to write it from scratch.
The Bot Builder SDKs are provided as open source on GitHub (see [BotBuilder](https://github.com/Microsoft/BotBuilder)).
- [Node.js SDK](https://github.com/Microsoft/BotBuilder/tree/master/Node)
- [.NET SDK](https://github.com/Microsoft/BotBuilder/tree/master/CSharp)

### Build it yourself

As an alternative to using the Bot Builder SDK or the Azure Bot Service, you can create a bot with any programming language by using the Bot Framework's [REST API](/en-us/connector/overview/).

## Teach your bot

Use the Microsoft Cognitive Services APIs with the Bot Builder SDK to craft smart, helpful bots. 
[Intelligent bots](~/intelligent-bots.md) <!-- link was ~/en-us/bot-intelligence/getting-started/) but build said it was broken --> respond more naturally, understand speech, search for data, determine user location, and even recognize a user's intent when input isn't quite as clear as it could be.

## Connect your bot
Channel features

## Debug and test
### Use the emulator
The Bot Framework Emulator is a desktop application that allows bot developers to test and debug their bots. The Bot Framework Emulator can communicate with your bot wherever it is running; on localhost, or remotely in the cloud.
For details on using the emulator, see [Bot Framework Emulator](~/debug-bots-emulator.md).
### or don't

## Publish with the portal
When you finish writing your bot, use the Developer Portal to [register](~/portal-register-bot.md) and [publish](~/portal-submit-bot-directory.md) it. Registering your bot gets you the the bot’s app ID and password used for authentication.

Publishing the bot submits it for review. For information about the review process, see Bot review guidelines. If your bot passes review, it’s added to the Bot Directory. The Bot Directory is a public directory of all bots that are registered and published with Microsoft Bot Framework. 
### or don't

You are encouraged to publish your bot because it makes it discoverable.

<!--
## Bot Connector
The Bot Connector service provides the connection from your bot to text/SMS, Skype, Slack, Facebook Messenger, Office 365 mail and other channels.
The bot you write exposes a Microsoft Bot Framework-compatible API on the Internet, which allows the Bot Framework Connector service to forward messages from your bot to a user, and send user messages back to your bot. The Connector also takes care of authentication messages. -->

<!--
There are different ways for your bot to communicate with the Connector.
The Node.JS or .NET SDKs provide built-in methods for connecting to the service. A simple bot using Node.JS demonstrates this in [Create a bot with the Bot Builder SDK for Node.js](~/nodejs/bot-builder-nodejs-quickstart.md).
Bots built using .NET can also use the Bot Framework Connector SDK .NET template. -->

The Direct Line API enables you to host your bot in your app rather than using one of the automatically configured channels. The Direct Line API is intended for developers writing their own client applications, web chat controls, mobile apps, or service-to-service applications that will talk to their bot.
The framework also provides an embeddable Web chat control that lets you host from your website.

## Developer Portal


## Bot Directory
The Bot Directory is a public directory of all bots that are registered and published with Microsoft Bot Framework. You are encouraged to always publish your bot because it makes it more discoverable.
For most channels, you can share your bot with users as soon as you configure the channel.
Users can select your bot in the directory and add it to one or more of the configured channels that they use, or use the Web chat control to try it.

<!-- If you configured your bot to work with Skype, you must publish your bot to the Bot Directory and Skype apps (see Publishing your bot) before users can start using it.
Although Skype is the only channel that requires you to publish your bot to the directory, you are encouraged to always publish your bot because it makes it more discoverable. -->

