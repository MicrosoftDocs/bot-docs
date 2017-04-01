---
title: What is the Bot Framework | Microsoft Docs
description: The Microsoft Bot Framework is a comprehensive offering that you use to build and deploy high quality bots.
keywords: bots, introduction, framework
author: RobStand
manager: rstand
ms.topic: bot-framework-overview-article
ms.prod: botframework
ms.service: Bot Framework
ms.date: 03/07/2017
ms.reviewer:

# Include the following line commented out
#ROBOTS: Index
#REVIEW
---

# What is the Bot Framework?

The Microsoft Bot Framework provides a platform for building and connecting powerful and intelligent bots. With the Bot Framework,you can build bots to interact with your users naturally in text/SMS, Skype, Slack, Facebook Messenger, and other popular services.

## What is a bot?
A bot is a web service that interacts with users in a conversational format. Users start conversations with your bot from any channel that you've configured your bot to work on (for example, Text/SMS, Skype, Slack, Facebook Messenger, and other popular services). You can design conversations to be freeform, natural language interactions or more guided ones where you provide the user choices or actions. The conversation can use simple text strings or more complex rich cards that contain text, images, and action buttons.

The following conversation shows a bot that schedules salon appointments. The bot understands the user's intent, presents appointment options using action buttons, displays the user's selection when they tap an appointment, and then sends a thumbnail card that contains the appointment's specifics.

![salon bot example](https://docs.botframework.com/en-us/images/connector/salon_bot_example.png)

Bots (or conversation agents) are rapidly becoming an integral part of one’s digital experience – they are as vital a way for users to interact with a service or application as a website or a mobile experience.

## Building and publishing bots
Developers writing bots all face the same problems: bots require basic I/O, they must have language and dialog skills, and they must connect to users, preferably in any conversation experience and language the user chooses. The Bot Framework provides tools to help solve these problems.

### Bot Builder
To help you build your bot, the Bot Framework includes [Bot Builder](bot-framework-botbuilder-overview.md), which provides rich and full-featured SDKs for the .NET and Node.js platforms. The SDKs provide features such as dialogs
and built-in prompts that make interacting with users much simpler. Bot Builder also includes an emulator and several sample bots. In addition to Bot Builder, you can create bots using the [Azure Bot Service](bot-framework-azure-overview.md) or the REST API.

### Developer Portal
When you [register your bot](bot-framework-publish-register.md) with the Developer Portal, you can connect your bots to text/SMS, Skype, Slack, Facebook Messenger, or other channels. You can
register, connect, publish, and manage your bot through your bot's dashboard. 

### Bot Connector
[Configure your bot](bot-framework-publish-configure.md) to  easily connect with users on channels, such Facebook
Messenger or Skype, when you register your bot in the Developer Portal. The Connector service sits between your bot and the channels, 
and passes the messages between them. The Connector also normalizes the messages that the bot sends the channel, 
if necessary.

### Bot Directory
After you finish building and configuring your bot, you [publish your bot](bot-framework-publish-add-to-directory.md) to the Bot Directory. The Bot Directory is a public directory of bots registered and published with the Bot Framework. Users can discover and add your bot to channels, and they can try your bot from the Bot Directory.

## Making bots smarter
To give your bot more human-like senses, you can incorporate LUIS for natural language understanding, 
Cortana for voice, and the Bing APIs for search. [Learn more](intelligent-bots.md) about adding intelligence to your bot.

## Next Steps
Get a [deeper look at the features](bot-framework-overview-how-it-works.md) of the Bot Framework. Then get started with [building a bot](bot-framework-botbuilder-overview.md) and learn more about [designing great bots](design-principles.md).

[NodeGetStarted]:bot-framework-nodejs-getstarted.md
[DotNETGetStarted]:bot-framework-dotnet-getstarted.md

