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

The Microsoft Bot Framework provides what you need to build and deploy bots that interact naturally with users, wherever they have conversations. The framework consists of the following components:
- Bot Builder -- SDKs and tools for writing bots, including an emulator for testing your bot.  
- Bot Connector -- A service that allows your bot to communicate across multiple client channels such as Skype, Outlook 365, or Slack.  
- Developer Portal -- A dashboard a for connecting your bot to channels, registering and publishing your bot.
- Bot Directory -- A public directory of bots registered and published with the Microsoft Bot Framework. 

##What is a bot?
A bot is a web service that interacts with users in a conversational format. Users start conversations with your bot from any channel that you've configured your bot to work on (for example, Text/SMS, Skype, Slack, Facebook Messenger, and other popular services). You can design conversations to be freeform, natural language interactions or more guided ones where you provide the user choices or actions. The conversation can use simple text strings or more complex rich cards that contain text, images, and action buttons.

The following conversation shows a bot that schedules salon appointments. The bot understands the user's intent, presents appointment options using action buttons, displays the user's selection when they tap an appointment, and then sends a thumbnail card that contains the appointment's specifics.

<div style="text-align:center" markdown="1">
![salon bot example](https://docs.botframework.com/en-us/images/connector/salon_bot_example.png)
</div>

Bots (or conversation agents) are rapidly becoming an integral part of one’s digital experience – they are as vital a way for users to interact with a service or application as a website or a mobile experience. 

Developers writing bots all face the same problems: bots require basic I/O, they must have language and dialog skills, and they must connect to users – preferably in any conversation experience and language the user chooses. The Bot Framework provides tools to solve these problems. Features include automatic translation to more than 30 languages, user and conversation state management, debugging tools, an embeddable web chat control and a way for users to discover, try, and add bots to the conversation experiences they love.


## Bot Builder

The Bot Framework provides SDKs for building your bot and an emulator for testing and debugging it.

### Bot Builder SDKs
These SDKs provide features such as dialogs and built-in prompts that make interacting with users much simpler. They provide access to Microsoft Cognitive Services APIs which give your bot more human-like intelligence. These APIs include LUIS for natural language understanding, Cortana for voice, and the Bing APIs for search. For more information about adding intelligence to your bot, see [Bot Intelligence](/en-us/bot-intelligence/getting-started/).
The Bot Builder SDKs are provided as open source on GitHub (see [BotBuilder](https://github.com/Microsoft/BotBuilder)).
- [Node.js SDK](/en-us/node/builder/overview/). 
- [.NET SDK](/en-us/csharp/builder/sdkreference/) 

### Azure Bot Service
The Azure Bot Service provides an integrated environment that is purpose-built for bot development, enabling you to build, connect, test, deploy and manage intelligent bots, all from one place. You can write your bot in C# or Node.js directly in the browser using the Azure editor, without any need for a tool chain (local editor and source control). 

For a walkthough of creating a bot using Azure Bot Service, see [Use Azure Bot Service](bot-framework-azure-getstarted.md).

### Bot Framework REST API

As an alternative to using the Bot Builder SDK for .NET, the Bot Builder SDK for .NET, or the Azure Bot Service, you can create a bot with any programming language by using the Bot Framework REST API.
See the Bot Framework’s [REST API](/en-us/connector/overview/). 


### Bot Framework Channel Emulator
The Bot Framework Emulator is a desktop application that allows bot developers to test and debug their bots. The Bot Framework Emulator can communicate with your bot wherever it is running; on localhost, or remotely in the cloud.
For details on using the emulator, see [Bot Framework Emulator](bot-framework-emulator.md).

## Bot Connector
The Bot Connector service provides the connection to text/SMS, Skype, Slack, Facebook Messenger, Office 365 mail and other channels. The Node.JS or .NET SDKs provide built-in methods for connecting to the service.
The Direct Line API enables you to host your bot in your app rather than using one of the automatically configured channels. This API is intended for developers writing their own client applications, web chat controls, mobile apps, or service-to-service applications that will talk to their bot.
The framework also provides an embeddable Web chat control that lets you host from your website. 

## Developer Portal
The Developer Portal allows you to register, connect, publish and manage your bot through your bot’s dashboard. This is where you configure the channels on which your bot communicates.

## Bot Directory
The Bot Directory is a public directory of bots registered and published with Microsoft Bot Framework. Users can try your bot from the directory by using the Web chat control. Users can discover and add your bot to the channels on which it is configured when the directory is made public to end users. 