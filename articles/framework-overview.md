---
title: Bot Framework Overview | Microsoft Docs
description: The Microsoft Bot Framework is a comprehensive offering used to build and deploy high quality bots.
author: RobStand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 04/01/2017

# Include the following line commented out
ROBOTS: Index, Follow
#REVIEW
---

# Bot Framework Overview

The Bot Framework is a platform and set of tools for building, connecting, and deploying powerful and intelligent bots. With the Bot Framework, you can build bots to interact with users in a natural way. You can connect your bot to the channels that are most useful to your users, including text/SMS, Skype, Slack, Facebook Messenger, and many other popular services. With Microsoft Cognitive Services, you can add rich artificial intelligence capabilities to your bot and provide compelling and powerful experiences to users.

With the Bot Framework, you can design conversations in your bot to be freeform. Your bot can also have more guided interactions where it provides the user choices or actions. The conversation can use simple text strings or more complex rich cards that contain text, images, and action buttons. And you can add natural language interactions, which let your users interact with your bots in a natural and expressive way.

> [!TIP]
> The type of bots that you can build with the Bot Framework are also commonly called <a href="https://en.wikipedia.org/wiki/Chatbot">chatbots</a>.

Let's look at an example of a bot that schedules salon appointments. The bot understands the user's intent, presents appointment options using action buttons, displays the user's selection when they tap an appointment, and then sends a thumbnail card that contains the appointment's specifics.
<p>
<div style="text-align: center" markdown="1">
![salon bot example](~/media/salon_bot_example.png)
</div>

Bots are rapidly becoming an integral part of digital experiences. They are becoming as essential as a website or a mobile experience for users to interact with a service or application.

## Why use the Bot Framework?
Developers writing bots all face the same problems: bots require basic I/O, they must have language and dialog skills, and they must connect to users, preferably in any conversation experience and language the user chooses. The Bot Framework provides a powerful set of tools to help solve these problems.

### Bot Builder
To help you build your bot, the Bot Framework includes [Bot Builder](bot-builder-overview-getstarted.md), which provides rich and full-featured SDKs for the .NET and Node.js platforms. The SDKs provide features that make interactions between bots and users much simpler. Bot Builder also includes an emulator for debugging your bots, as well as a large set of sample bots you can use as building blocks. 

In addition to Bot Builder, you can create bots using the [Azure Bot Service](~/azure/index.md) or the REST API.

### Developer portal
Managing your bot is easy with the developer portal The developer portal gives you one convenient place to register, connect, publish, and manage your bot. It also provides diagnostic tools and a web chat control you can use to embed your bot on a web page.

### Connect to channels
The developer portal is also where you configure channels, such as Facebook
Messenger or Skype, that your bot will connect to. The Connector service sits between your bot and the channels, and passes the messages between them.

![List of channels on the portal](~/media/portal-channels-list.png) 

## Build smart bots
To give your bot more human-like senses, you can take advantage of  Microsoft Cognitive Services to add smart features like natural language understanding, image recognition, speech, and more. [Learn more](~/intelligent-bots.md) about adding intelligence to your bot.

## Next Steps
Dive deeper into [the capabilities](overview-how-bot-framework-works.md) of the Bot Framework. Get started  [building your first bot](bot-builder-overview-getstarted.md) and learn more about [designing great bots](~/bot-design-principles.md).

[NodeGetStarted]:~/nodejs/getstarted.md
[DotNETGetStarted]:~/dotnet/getstarted.md

