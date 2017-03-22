---
title: What is the Bot Framework | Microsoft Docs
description: The Microsoft Bot Framework is a comprehensive offering that you use to build and deploy high quality bots for your users to enjoy wherever they are talking.
keywords: bots, introduction, framework
author: RobStand
manager: rstand

ms.topic: bot-framework-overview-article

ms.prod: botframework
ms.service: Bot Framework
ms.date: 03/07/2017

# Alias of the document reviewer. Change to the appropriate person.
ms.reviewer:

# Include the following line commented out
#ROBOTS: Index
#REVIEW
---
> [!WARNING]
> The content in this article is still under development. The article may have errors in content, formatting,
> and copy. The content may change dramatically as the article is developed.

#What is the Bot Framework?

The Microsoft Bot Framework provides what you need to build and deploy bots that interact naturally with users, wherever they have conversations. The framework consists of the following components:
- **Bot Builder** - SDKs and tools for writing bots, including an emulator for testing your bot.  
- **Bot Connector** - A service that allows your bot to communicate across multiple client channels such as Skype, Outlook 365, or Slack.  
- **Developer Portal** - A dashboard that allows you to configure the channels your bot uses, and register and publish your bot.
- **Bot Directory** - A public directory of bots registered and published with the Microsoft Bot Framework.  

##What is a bot?
A bot is a web service that interacts with users in a conversational format. Users start conversations with your bot from any channel that you've configured your bot to work on (for example, Text/SMS, Skype, Slack, Facebook Messenger, and other popular services). You can design conversations to be freeform, natural language interactions or more guided ones where you provide the user choices or actions. The conversation can use simple text strings or more complex rich cards that contain text, images, and action buttons.

The following conversation shows a bot that schedules salon appointments. The bot understands the user's intent, presents appointment options using action buttons, displays the user's selection when they tap an appointment, and then sends a thumbnail card that contains the appointment's specifics.

<div style="text-align:center" markdown="1">
![salon bot example](https://docs.botframework.com/en-us/images/connector/salon_bot_example.png)
</div>

Bots (or conversation agents) are rapidly becoming an integral part of one’s digital experience – they are as vital a way for users to interact with a service or application as a website or a mobile experience.

Developers writing bots all face the same problems: bots require basic I/O, they must have language and dialog skills, and they must connect to users – preferably in any conversation experience and language the user chooses. The Bot Framework provides tools to solve these problems. Features include automatic translation to more than 30 languages, user and conversation state management, debugging tools, an embeddable web chat control and a way for users to discover, try, and add bots to the conversation experiences they love.

Your bot, which uses methods provided by the Bot Builder SDK, communicates with the Bot Connector service, which handles communication with channels.
The Developer Portal is used to choose which channels are enabled for your bot. The Bot Directory allows users to find your bot.

