---
title: Azure Bot Service | Microsoft Docs
description: Learn about Azure Bot Service, a service for building, connecting, testing, deploying, monitoring, and managing bots.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/14/2018
monikerRange: 'azure-bot-service-4.0'
---

# Azure Bot Service
You can think of a bot as an app that users interact with in a conversational way. Azure Bot Service enables you to build bots that support different types of interactions with users. You can design conversations in your bot to be freeform. Your bot can also have more guided interactions where it provides the user choices or actions. The conversation can use simple text strings or more complex rich cards that contain text, images, and action buttons. And you can add natural language interactions, which let your users interact with your bots in a natural and expressive way. 

Azure Bot Service provides the core components for creating bots, including the Bot Builder SDK for developing bots and the Bot Connector Service for connecting bots to channels. You can write a bot, connect, test, deploy, and manage it from your web browser with no separate editor or source control required. Azure Bot Service provides two [hosting plans](bot-service-overview-readme.md#hosting-plans):

- With the App Service plan, a bot is a standard Azure web app you can set to allocate a predefined capacity with predictable costs and scaling. 
- With a Consumption plan, a bot is a serverless bot that runs on Azure Functions and uses the pay-per-run Azure Functions pricing.

Azure Bot Service accelerates bot development with [five bot templates](bot-service-templates.md) you can choose from when you create a bot. You can further modify your bot directly in the browser using the Azure editor or in an Integrated Development Environment (IDE), such as Visual Studio and Visual Studio Code.

## Why use Azure Bot Service?
Here are some key features of Azure Bot Service:

- **Open source**. The Bot Builder SDK is open-source and available on GitHub. It supports [.NET](https://github.com/microsoft/botbuilder-dotnet), [JavaScript](https://github.com/microsoft/botbuilder-js), [Python](https://github.com/microsoft/botbuilder-python), and [Java](https://github.com/microsoft/botbuilder-java).  

- **Bring your own dependencies**. Bots support NuGet, NPM, PyPI, and JPM so you can use your favorite packages in your bot.

- **Flexible development**. Code your bot right in the Azure portal or set up continuous integration and deploy your bot through GitHub, Visual Studio Team Services, and other supported development tools. You can also publish from Visual Studio.

- **Bot templates**. Templates allow you to quickly create a bot with the code and features you need. Choose from a Basic bot, a Forms bot for collecting user input, a Language understanding bot that leverages LUIS to understand user intent, a QnA bot to handle FAQs, or a Proactive bot that alerts users of events.

- **Connect to channels**. The bot communicates with the user using channels, such as Facebook, Slack, GroupMe, etc. You can also have _your bot_ connect to _your own client application_ by using Direct Line as a channel. A channel is the connection between the bot and communication apps. You configure channels you want your bot to be available on using the [Azure portal](https://portal.azure.com). The Bot Connector Service then connects your bot to those channels, and facilitates communication between bot and user by relaying messages from bot to channel and from channel to bot. 

- **Tools and services**. Test your bot with the Bot Framework Emulator and preview your bot on different channels with the Channel Inspector.

## What is Bot Builder SDK?
Bot Builder SDK provides libraries, samples, and tools to help you build and debug bots. When you build a bot with Azure Bot Service, your bot is backed by the Bot Builder SDK. You can also use the Bot Builder SDK to create a bot from scratch using C#, JavaScript, Python or Java. Bot Builder includes the Bot Framework Emulator for testing your bots and the Channel Inspector for previewing your bot's user experience on different channels.

## Next steps
Now that you have an overview of Azure Bot Service, the next step is to dive in and create a bot.

> [!div class="nextstepaction"]
> [Create a bot with Bot Service](#)
