---
title: About the Bot Framework | Microsoft Docs
description: Learn about the Microsoft Bot Framework, a comprehensive framework of tools and services to build and deploy high quality bots.
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 10/03/2017
---

# About the Bot Framework

The Bot Framework is a platform for building, connecting, testing, and deploying powerful and intelligent bots. You can quickly [start building bots](bot-builder-overview-getstarted.md) with the [Azure Bot Service](azure-bot-service-quickstart.md). If you prefer building a bot from scratch, the Bot Framework provides the Bot Builder SDK for [.NET](dotnet/bot-builder-dotnet-quickstart.md) and [Node.js](nodejs/bot-builder-nodejs-quickstart.md).

## What is a bot?
Think of a bot as an app that users interact with in a conversational way. Bots can communicate conversationally with text, cards, or speech. A bot may be as simple as basic pattern matching with a response, or it may be a sophisticated weaving of artificial intelligence techniques with complex conversational state tracking and integration to existing business services.

The Bot Framework enables you to build bots that support different types of interactions with users. You can design conversations in your bot to be freeform. Your bot can also have more guided interactions where it provides the user choices or actions. The conversation can use simple text strings or more complex rich cards that contain text, images, and action buttons. And you can add natural language interactions, which let your users interact with your bots in a natural and expressive way.

Let's look at an example of a bot that schedules salon appointments. The bot understands the user's intent, presents appointment options using action buttons, displays the user's selection when they tap an appointment, and then sends a thumbnail card that contains the appointment's specifics.
<p>
<div style="text-align: center" markdown="1">
![salon bot example](~/media/salon_bot_example.png)
</div>  

Bots are rapidly becoming an integral part of digital experiences. They are becoming as essential as a website or a mobile experience for users to interact with a service or application.

## Why use the Bot Framework?
Developers writing bots all face the same problems: bots require basic I/O, they must have language and dialog skills, and they must connect to users, preferably in any conversation experience and language the user chooses. The Bot Framework provides powerful tools and features to help solve these problems.

### Azure Bot Service
The Azure Bot Service accelerates the process of developing a bot. It provisions a resource and Azure gives you five bot templates you can choose from when you create a bot. You can further modify your bot directly in the browser using the Azure editor or in an Integrated Development Environment (IDE), such as Visual Studio and Visual Studio Code.

### Bot Builder
The Bot Framework includes [Bot Builder](bot-builder-overview-getstarted.md), which provides a rich and full-featured SDK for the .NET and Node.js platforms. The SDK provides features that make interactions between bots and users much simpler. Bot Builder also includes an emulator for debugging your bots, as well as a large set of sample bots you can use as building blocks.

### Bot Framework portal
The Bot Framework portal gives you one convenient place to register, connect, and manage your bot. It also provides diagnostic tools and a web chat control you can use to embed your bot in a web page.

![Configure a bot in the developer portal](~/media/portal-configure-bot.png)

### Channels
The Bot Framework supports several popular channels for connecting your bots and people. Users can start conversations with your bot on any channel that you've configured your bot to work with, including email, Facebook, Skype, Slack, and SMS. You can use the [Channel Inspector](portal-channel-inspector.md) to preview features you want to use on these targeted channels.

![List of channels on the portal](~/media/portal-channels-list.png)

## Next steps
Dive deeper into [the capabilities](overview-how-bot-framework-works.md) of the Bot Framework. Get started  [building your first bot](bot-builder-overview-getstarted.md) and learn more about [designing great bots](~/bot-design-principles.md).

[NodeGetStarted]:~/nodejs/bot-builder-nodejs-quickstart.md
[DotNETGetStarted]:~/dotnet/bot-builder-dotnet-quickstart
