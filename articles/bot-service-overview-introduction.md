---
title: About Bot Service | Microsoft Docs
description: Learn about Bot Service, a service for building, connecting, testing, deploying, monitoring, and managing bots.
author: Kaiqb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 04/13/2018
---

::: moniker range="azure-bot-service-3.0"

# About Bot Service

Bot Service provides what you need to build, connect, test, deploy, monitor, and manage bots. Bot Service provides the core components for creating bots, including the Bot Builder SDK for developing bots and the Bot Framework for connecting bots to channels.

Bot Service provides an integrated environment purpose-built for bot development. You can write a bot, connect, test, deploy, and manage it from your web browser with no separate editor or source control required. For simple bots, you may not need to write code at all. It is powered by the Bot Framework and it provides two [hosting plans](bot-service-overview-readme.md#hosting-plans):

- With the App Service plan, a bot is a standard Azure web app you can set to allocate a predefined capacity with predictable costs and scaling. 
- With a Consumption plan, a bot is a serverless bot that runs on Azure Functions and uses the pay-per-run Azure Functions pricing.

Bot Service accelerates bot development with [five bot templates](bot-service-concept-templates.md) you can choose from when you create a bot. You can further modify your bot directly in the browser using the Azure editor or in an Integrated Development Environment (IDE), such as Visual Studio and Visual Studio Code.

## What is a bot?
Think of a bot as an app that users interact with in a conversational way. Bots can communicate conversationally with text, cards, or speech. A bot may be as simple as basic pattern matching with a response, or it may be a sophisticated weaving of artificial intelligence techniques with complex conversational state tracking and integration to existing business services.

Bot Service enables you to build bots that support different types of interactions with users. You can design conversations in your bot to be freeform. Your bot can also have more guided interactions where it provides the user choices or actions. The conversation can use simple text strings or more complex rich cards that contain text, images, and action buttons. And you can add natural language interactions, which let your users interact with your bots in a natural and expressive way.

## Why use Bot Service?
Here are some key features of Bot Service:

- **Multiple language support**. Bot Service leverages Bot Builder with support for .NET and Node.js. 
- **Bot templates**. Bot Service templates allow you to quickly create a bot with the code and features you need. Choose from a Basic bot, a Forms bot for collecting user input, a Language understanding bot that leverages LUIS to understand user intent, a QnA bot to handle FAQs, or a Proactive bot that alerts users of events.
- **Bring your own dependencies**. Bots support NuGet and NPM, so you can use your favorite packages in your bot.
- **Flexible development**. Code your bot right in the Azure portal or set up continuous integration and deploy your bot through GitHub, Visual Studio Team Services, and other supported development tools. You can also publish from Visual Studio.
- **Connect to channels**. Bot Service supports several popular channels for connecting your bots and the people that use them. Users can start conversations with your bot on any channel that you've configured your bot to work with, including Skype, Facebook, Teams, Slack, SMS, and several others.
- **Tools and services**. Test your bot with the Bot Framework Emulator and preview your bot on different channels with the Channel Inspector.
- **Open source**. The Bot Builder SDK is open-source and available on [GitHub](https://github.com/microsoft/botbuilder).

## What is Bot Builder?
[Bot Builder](bot-builder-overview-getstarted.md) provides an SDK, libraries, samples, and tools to help you build and debug bots. When you build a bot with Bot Service, your bot is backed by the Bot Builder SDK. You can also use the Bot Builder SDK to create a bot from scratch using C# or Node.js. Bot Builder includes the Bot Framework Emulator for testing your bots and the Channel Inspector for previewing your bot's user experience on different channels.

## Next steps
Now that you have an overview of Bot Service, the next step is to dive in and create a bot.

> [!div class="nextstepaction"]
> [Create a bot with Bot Service](bot-service-quickstart.md)
::: moniker-end

::: moniker range="azure-bot-service-4.0"

# Azure Bot Service

Azure Bot Service provides tools to build, test, deploy, and manage intelligent bots all in one place. Through the modular and extensible framework provided by the SDK, developers can leverage templates to create bots that provide speech, language understanding, question and answer, and more.  

## What is a bot?
A bot is an app that users interact with in a conversational way using text, graphics (cards), or speech. It may be a simple question and answer dialog, or a sophisticated bot that allows people to interact with services in an intelligent manner using pattern matching, state tracking and artificial intelligence techniques well-integrated with existing business services. Check out [case studies](https://azure.microsoft.com/services/bot-service/) of bots.  

## Building a bot 
You can choose to use your favorite development environment or command line tools to create your bot in C#, JavaScript, Java, and Python. We provide tools for various stages of bot development that you can use to build your bot to get you started.    

![Bot Overview](~/media/bot-service-overview.png)

## Plan 
Before writing code, review the bot [design guidelines](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-design-principles) for best practices and identify the needs for your bot. You can create a simple bot or include more sophisticated capabilities, such as speech, language understanding, QnA, or the ability to extract knowledge from different sources and provide intelligent answers.  

> [!TIP] 
>
> Install the template you need:
>  - [Bot builder VSIX](https://marketplace.visualstudio.com/) for SDK v3 or v4
>  - [botbuilder-v3](https://www.npmjs.com/search?q=botbuilder-v3) for Node.js SDK v3, or [botbuilder-v4](https://www.npmjs.com/search?q=botbuilder-v4) for JavaScript SDK v4 
>  - [botbuilder-java](https://www.npmjs.com/search?q=botbuilder-java)  (Java SDK v4 only)
>  - [botbuilder-python](https://www.npmjs.com/search?q=botbuilder-python)  (Python SDK v4 only)
>
> Install tools:
> - [Chatdown](https://github.com/Microsoft/botbuilder-tools/tree/master/Chatdown) to create mockups of bot conversations
> - [MSBot](https://github.com/Microsoft/botbuilder-tools/tree/master/MSBot) to create and manage your bot configuration file
> - [LUDown](https://github.com/Microsoft/botbuilder-tools/tree/master/Ludown) to bootstrap language understanding for your bot from command-line
> - [Emulator](https://github.com/Microsoft/botbuilder-tools/) to test your bot
>
> If needed, use bot components, such as:  
> - [LUIS](https://www.luis.ai/) to add natural language to bots
> - [QnA Maker](https://qnamaker.ai/) to respond to user's questions in a more natural, conversational way
> - [Speech](https://azure.microsoft.com/services/cognitive-services/speech/) to convert audio to text, understand intent, and convert text back to speech  
> - [Spelling](https://azure.microsoft.com/services/cognitive-services/spell-check/) to correct spelling errors, recognize the difference among names, brand names, and slang 


## Build your bot 
You can create a bot in the programming language of your choice using tools, such as Yeoman, or Visual Studio or Azure. 

> [!TIP]
>
> Create a bot using [SDK](http://docs.microsoft.com/azure/bot-service/dotnet/bot-builder-dotnet-quickstart),  [Azure portal](https://docs.microsoft.com/azure/bot-service/bot-service-quickstart), or use [CLI tools](https://docs.microsoft.com/azure/bot-service/bot-service-build-botbuilder-templates)
>
> Add components: 
> - Add language understanding model [LUIS](https://docs.microsoft.com/en-us/azure/cognitive-services/luis/home). 
> - Add [QnA Maker](https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/home) knowledge base to answer questions users ask.  
> - If you are using more than model, for example QnA with LUIS or multiple LUIS models, your bot will need to know which one to use when. See how to use the [dispatch tool to manage multiple models](https://docs.microsoft.com/azure/bot-service).  
> - Enhace user experience with cards, speech, or translation 
> - Add logic to your bot using the Bot Builder SDK.   

## Test your bot 
Before publishing, test and debug your bot with an emulator, either locally or remotely. 

> [!TIP] 
>
> [Review and test the interaction with your bot](https://docs.microsoft.com/azure/bot-service) 

## Publish 
When you are ready to publish your bot to Azure or to your own web service or data center. 

> [!Tip]
>
> [Deploy to Azure](https://docs.microsoft.com/azure/bot-service/bot-service-build-continuous-deployment)

## Connect          
Connect your bot to channels such as Facebook, Messenger, Kik, Skype, Slack, Microsoft Teams, Telegram, text/SMS, Twilio, Cortana, and Skype to increase interactions and reach more customers.  
  
> [!TIP]
>
> [Choose the channels to be added](https://docs.microsoft.com/azure/bot-service/bot-service-manage-channels)


## Evaluate 
Use the data collected in Azure portal to identify opportunities to improve the capabilities and performance of your bot. 

> [!Tip]
>
> [Gather analytics](https://docs.microsoft.com/azure/bot-service/bot-service-manage-analytics) 

::: moniker-end
