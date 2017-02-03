---
title: What is the Bot Framework? | Microsoft Docs
description: Overview of the Bot Framework and its capabilities.
services: service-name
documentationcenter: BotFramework-Docs
author: DeniseMak
manager: rstand

ms.service: Bot Framework
ms.topic: article
ms.workload: Cognitive Services
ms.date: 01/20/2017
ms.author: v-demak@microsoft.com; rstand@microsoft.com

---
#What is the Bot Framework?

The Microsoft Bot Framework provides what you need to build and deploy bots that interact naturally with users, wherever they have conversations. The framework consists of the Bot Builder SDK, Bot Connector, Developer Portal, and Bot Directory. There's also an emulator that you can use to test your bot. 

##What is a bot?
A bot is a web service that interacts with users in a conversational format. Users start conversations with your bot from any channel that you've configured your bot to work on (for example, Text/SMS, Skype, Slack, Facebook Messenger, and other popular services). You can design conversations to be freeform, natural language interactions or more guided ones where you provide the user choices or actions. The conversation can use simple text strings or more complex rich cards that contain text, images, and action buttons.

The following conversation shows a bot that schedules salon appointments. The bot understands the user's intent, presents appointment options using action buttons, displays the user's selection when they tap an appointment, and then sends a thumbnail card that contains the appointment's specifics.

<div style="text-align:center" markdown="1">
![salon bot example](https://docs.botframework.com/en-us/images/connector/salon_bot_example.png)
</div>

Bots (or conversation agents) are rapidly becoming an integral part of one’s digital experience – they are as vital a way for users to interact with a service or application as a website or a mobile experience. 

##How the Bot Framework helps you
Developers writing bots all face the same problems: bots require basic I/O, they must have language and dialog skills, and they must connect to users – preferably in any conversation experience and language the user chooses. The Bot Framework provides tools to solve these problems. Features include automatic translation to more than 30 languages, user and conversation state management, debugging tools, an embeddable web chat control and a way for users to discover, try, and add bots to the conversation experiences they love.

![Bot Framework Diagram](http://docs.botframework.com/en-us/images/faq-overview/botframework_overview_july.png)

To build your bot, the Framework provides a [.NET SDK](/en-us/csharp/builder/sdkreference/) and [Node.js SDK](/en-us/node/builder/overview/). These SDKs provide features such as dialogs and built-in prompts that make interacting with users much simpler. For those using other languages, see the framework’s [REST API](/en-us/connector/overview/). The Bot Builder SDK is provided as open source on GitHub (see [BotBuilder](https://github.com/Microsoft/BotBuilder)).

To give your bot more human-like senses, you can incorporate LUIS for natural language understanding, Cortana for voice, and the Bing APIs for search. For more information about adding intelligence to your bot, see [Bot Intelligence](/en-us/bot-intelligence/getting-started/).