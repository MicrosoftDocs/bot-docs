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

## Bot Builder

The Bot Framework provides the following SDKs for building your bot as well as an emulator for testing and debugging it.

### Bot Builder SDKs
These SDKs provide features such as dialogs and built-in prompts that make interacting with users much simpler. They provide access to Microsoft Cognitive Services APIs which give your bot more human-like intelligence. These APIs include LUIS for natural language understanding, Cortana for voice, and the Bing APIs for search. For more information about adding intelligence to your bot, see [Bot Intelligence](/en-us/bot-intelligence/getting-started/).
The Bot Builder SDKs are provided as open source on GitHub (see [BotBuilder](https://github.com/Microsoft/BotBuilder)).
- [Node.js SDK](https://github.com/Microsoft/BotBuilder/tree/master/Node)
- [.NET SDK](https://github.com/Microsoft/BotBuilder/tree/master/CSharp)

### Azure Bot Service
The Azure Bot Service provides an integrated environment that is purpose-built for bot development, enabling you to build, connect, test, deploy and manage intelligent bots, all from one place. You can write your bot in C# or Node.js directly in the browser using the Azure editor, without any need for a tool chain (local editor and source control).

For a walkthough of creating a bot using Azure Bot Service, see [Use Azure Bot Service](bot-framework-azure-getstarted.md).

### Bot Framework REST API

As an alternative to using the Bot Builder SDK for .NET, the Bot Builder SDK for .NET, or the Azure Bot Service, you can create a bot with any programming language by using the Bot Framework REST API.
See the Bot Framework’s [REST API](/en-us/connector/overview/).


### Bot Framework Emulator
The Bot Framework Emulator is a desktop application that allows bot developers to test and debug their bots. The Bot Framework Emulator can communicate with your bot wherever it is running; on localhost, or remotely in the cloud.
For details on using the emulator, see [Bot Framework Emulator](bot-framework-resources-emulator.md).

## Bot Connector
The Bot Connector service provides the connection from your bot to text/SMS, Skype, Slack, Facebook Messenger, Office 365 mail and other channels.
The bot you write exposes a Microsoft Bot Framework-compatible API on the Internet, which allows the Bot Framework Connector service to forward messages from your bot to a user, and send user messages back to your bot. The Connector also takes care of authentication messages.

<!--
There are different ways for your bot to communicate with the Connector.
The Node.JS or .NET SDKs provide built-in methods for connecting to the service. A simple bot using Node.JS demonstrates this in [Create a bot with the Bot Builder SDK for Node.js](bot-framework-nodejs-getstarted.md).
Bots built using .NET can also use the Bot Framework Connector SDK .NET template. -->

The Direct Line API enables you to host your bot in your app rather than using one of the automatically configured channels. The Direct Line API is intended for developers writing their own client applications, web chat controls, mobile apps, or service-to-service applications that will talk to their bot.
The framework also provides an embeddable Web chat control that lets you host from your website.

## Developer Portal
When you finish writing your bot, you need to register it, connect it to channels, and finally publish it, using the Developer Portal.
Registering your bot describes it to the framework, and it’s how you get the bot’s app ID and password that’s used for authentication.
Bots that you register are located at My bots in the portal.

For more information on publishing a bot, see [Publish a bot](https://review.docs.microsoft.com/en-us/botframework/bot-framework-publish-overview).

## Bot Directory
The Bot Directory is a public directory of all bots that are registered and published with Microsoft Bot Framework. You are encouraged to always publish your bot because it makes it more discoverable.
For most channels, you can share your bot with users as soon as you configure the channel.
Users can select your bot in the directory and add it to one or more of the configured channels that they use, or use the Web chat control to try it.

<!-- If you configured your bot to work with Skype, you must publish your bot to the Bot Directory and Skype apps (see Publishing your bot) before users can start using it.
Although Skype is the only channel that requires you to publish your bot to the directory, you are encouraged to always publish your bot because it makes it more discoverable. -->

Publishing the bot submits it for review. For information about the review process, see Bot review guidelines. If your bot passes review, it’s added to the Bot Directory.

## Next Steps
Get started with [building a bot](bot-framework-botbuilder-overview.md) and learn more about [designing great bots](designing-bots/index.md).

[NodeGetStarted]:bot-framework-nodejs-getstarted.md
[DotNETGetStarted]:bot-framework-dotnet-getstarted.md
