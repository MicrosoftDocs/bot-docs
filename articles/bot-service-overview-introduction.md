---
title: Azure Bot Service Introduction | Microsoft Docs
description: Learn about Bot Service, a service for building, connecting, testing, deploying, monitoring, and managing bots.
keywords: overview, introduction, SDK, outline
author: Kaiqb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 09/20/2018
---

::: moniker range="azure-bot-service-3.0"

# About Azure Bot Service

[!INCLUDE [pre-release-label](includes/pre-release-label-v3.md)]

Azure Bot Service provides tools to build, test, deploy, and manage intelligent bots all in one place. Through the modular and extensible framework provided by the SDK, developers can leverage templates to create bots that provide speech, language understanding, question and answer, and more.  

## What is a bot?
A bot is an app that users interact with in a conversational way using text, graphics (cards), or speech. It may be a simple question and answer dialog, or a sophisticated bot that allows people to interact with services in an intelligent manner using pattern matching, state tracking and artificial intelligence techniques well-integrated with existing business services. Check out [case studies](https://azure.microsoft.com/services/bot-service/) of bots.  

## Building a bot 
You can choose to use your favorite development environment or command line tools to create your bot in C# or Node.js. We provide tools for various stages of bot development that you can use to build your bot to get you started.    

![Bot Overview](media/bot-service-overview.png) 

## Plan 
Before writing code, review the bot [design guidelines](bot-service-design-principles.md) for best practices and identify the needs for your bot. You can create a simple bot or include more sophisticated capabilities, such as speech, language understanding, QnA, or the ability to extract knowledge from different sources and provide intelligent answers.  

> [!TIP] 
>
> Install the template you need:
>  - Bot builder .NET SDK v3 [VSIX template](https://marketplace.visualstudio.com/items?itemName=BotBuilder.BotBuilderV3) 
>  - Bot builder Node.js SDK v3 [Yeoman template](https://www.npmjs.com/package/generator-botbuilder) 
>
> Install tools:
> - Download [CLI tools](https://github.com/Microsoft/botbuilder-tools) to create and manage bot assests. These tools help you manage bot  configuration file, LUIS app, QnA knowledge base, and more from the command-line. You can find more details in the [readme](https://github.com/Microsoft/botbuilder-tools/blob/master/README.md).
> - [Emulator](https://github.com/Microsoft/BotFramework-Emulator/releases) to test your bot
>
> If needed, use bot components, such as:  
> - [LUIS](https://www.luis.ai/) to add language understanding to bots
> - [QnA Maker](https://qnamaker.ai/) to respond to user's questions in a more natural, conversational way
> - [Speech](https://azure.microsoft.com/services/cognitive-services/speech/) to convert audio to text, understand intent, and convert text back to speech  
> - [Spelling](https://azure.microsoft.com/services/cognitive-services/spell-check/) to correct spelling errors, recognize the difference among names, brand names, and slang 
> - [Cognitive Services](bot-service-concept-intelligence.md) for various other intelligent components 


## Build your bot 
Your bot is a web service that implements a conversational interface and communicates with the Bot Service. You can create this solution in any number of environments and languages and we offer easy getting started tools for Visual Studio or Yeoman or directly within the Azure portal. Look below for some of the tools and services you can use.

> [!TIP]
>
> Create a bot using [SDK](~/dotnet/bot-builder-dotnet-quickstart.md),  [Azure portal](bot-service-quickstart.md), or use [CLI tools](~/bot-builder-tools.md).
>
> Add components: 
> - Add language understanding model [LUIS](https://docs.microsoft.com/en-us/azure/cognitive-services/luis/home). 
> - Add [QnA Maker](https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/home) knowledge base to answer questions users ask.  
> - Enhace user experience with cards, speech, or translation. 
> - Add logic to your bot using the Bot Builder SDK.   

## Test your bot 
Bots are complex apps, with a lot of different parts working together. Like any other complex app, this can lead to some interesting bugs or cause your bot to behave differently than expected. Before publishing, test your bot.

> [!TIP] 
>
> - [Test bot with the emulator](bot-service-debug-emulator.md)
> - [Test bot in Web Chat](bot-service-manage-test-webchat.md)

## Publish 
When you are ready, publish your bot to Azure or to your own web service or data center. You can set up continuous deployment that allows you to develop your bot locally and is useful if your bot is checked into a source control like GitHub or Visual Studio Team Services. As you check your changes back into your source repository, your changes will automatically be deployed to Azure.

> [!Tip]
>
> - [Deploy to Azure](bot-service-build-continuous-deployment.md)

## Connect          
Connect your bot to channels such as Facebook, Messenger, Kik, Skype, Slack, Microsoft Teams, Telegram, text/SMS, Twilio, Cortana, and Skype to increase interactions and reach more customers.  
  
> [!TIP]
>
> - [Choose the channels to be added](bot-service-manage-channels.md)


## Evaluate 
Use the data collected in Azure portal to identify opportunities to improve the capabilities and performance of your bot. You can get service-level and instrumentation data like traffic, latency, and integrations. Analytics also provides conversation-level reporting on user, message, and channel data.

> [!Tip]
>
> - [Gather analytics](bot-service-manage-analytics.md) 


::: moniker-end

::: moniker range="azure-bot-service-4.0"

# About Azure Bot Service

[!INCLUDE [pre-release-label](includes/pre-release-label.md)]

Azure Bot Service provides tools to build, test, deploy, and manage intelligent bots all in one place. Through the modular and extensible framework provided by the SDK, developers can leverage templates to create bots that provide speech, understand natural language, handle questions and answers, and more.

Included within the Azure Bot Service are a variety of bot building tools, including:

* The Bot Builder SDK is the programming library (available in multiple languages) used to write the code of your bot.
* The Bot Framework service is a cloud API used to connect bots to popular messaging channels.
* The Bot Framework Emulator is a stand-alone chat client useful for designing, testing, debugging and designing your bot

With the Bot Builder SDK, developers can connect their bot to an array of useful Azure services tuned to work in the context
of conversational software, such as:

* [LUIS](https://luis.ai) adds natural language understanding to your bot.
* [QnaMaker](https://qnamaker.ai) helps your bot provide answers to common questions.
* Your bot can store data in [Azure Blob Storage](https://azure.microsoft.com/en-us/services/storage/blobs/) or [Cosmos DB](https://azure.microsoft.com/en-us/services/cosmos-db/).


## What is a bot?
A bot is an app that users interact with in a conversational way using text, interactive cards, and speech. A bot interaction can be quick a question and answer, or it can be a sophisticated conversation that provides access to services in an intelligent manner.
Bots can use simple pattern matching, but they also take advantage of A.I. and other technologies to allow users to interact with features in an open-ended, natural way.

Bots can do the same things other types of software can do - read and write files, use databases and APIs, and do the normal computational tasks that computers do. What makes bots special is their use of mechanisms normally reserved for human-to-human communication. Bots live alongside us in our chat rooms and speak to us in natural language through a connected speakers. Whatever tasks a bot is built to achieve, its nature as a bot brings it closer to its users, avalable instantly at a mention.

Bots enable computers to provide an experience that feels less like using a machine and more like dealing with a person - or at least an intelligent robot. They can be used to shift simple, repetitive tasks, such as taking a dinner reservation or gathering profile information, on to automated systems that may no longer require direct human intervention.

[Check out these case studies of bots](https://azure.microsoft.com/services/bot-service/).

## What is in a bot?
Under the hood, bots look a lot like modern web application software. They live on the internet, and use APIs to send and receive messages.

What's in a bot varies widely depending on what kind of bot it is. A simple bot could do nothing but receive a message and echo it back to the user with very little code involved. But modern bot software relies on a stack of technology and tools to deliver increasingly complex experiences on a wide variety of platforms.

Bots built with Azure Bot Service normally consist of the following components:

* A web server, in most cases one that is available on the public internet
* The Bot Framework SDK, a library of tools that provides an interface for developing custom features
* Custom software implementing the bot's features
* A database to store information
* Natural language tools used to extract information and meaning from messages


## Delivering a bot 
Creating a bot is both a technical and a creative process. In addition to writing a new type of software, designers and developers must create a user experience using an extremely limited palette of tools - in many cases, only words. 

Every word counts!

Azure Bot Service offers an integrated set of tools and services to facilitate this process. Choose your favorite development environment or command line tools to create your bot in C#, JavaScript, or Typescript. (Java and Python are coming too!) We provide tools for various stages of bot development that you can use to build your bot to get you started.    

### Design
As with any type of software, having a thorough understanding of the goals, processes and user needs is important to the process of creating a successful bot.

Before writing code, review the bot [design guidelines](bot-service-design-principles.md) for best practices and identify the needs for your bot. You can create a simple bot or include more sophisticated capabilities such as speech, natural language understanding,and question answering.


### Build
Your bot is a web service that implements a conversational interface and communicates with the Bot Service to send and receive messages and events. You can create this service in any number of environments and languages. You can start your bot development in the [Azure portal](bot-service-quickstart.md), or use the templates listed below for local development in the language of your choice to start with a generic app scaffold.

We have also created [a variety of sample applications](https://github.com/microsoft/botbuilder-samples) that provide example implementations of many of the capabilities available through the SDK and network of services. These are great for developers looking for a more feature rich starting point.

| .NET Template | JavaScript Template | 
| --- | --- | 
| [VSIX template](https://marketplace.visualstudio.com/items?itemName=BotBuilder.botbuilderv4) | [Yeoman template](https://www.npmjs.com/package/generator-botbuilder). Use @preview to get v4 template. |


As part of the Azure Bot Service, we offer additional components you can use to extend your bot's functionality:

| Feature | Description | Link |
| --- | --- | --- |
| Add natural language processing | Enable your bot to understand natural language, understand spelling errors, use speech, and recognize the user's intent | [How to use LUIS](~/v4sdk/bot-builder-howto-v4-luis.md) 
| Answer questions | Add a knowledge base to answer questions users ask in a more natural, conversational way | [How to use QnA Maker](~/v4sdk/bot-builder-howto-qna.md) 
| Manage multiple models | If using more than one model, such as for LUIS and QnA Maker, intelligently determine when to use which one during your bot's conversation | [Dispatch tool](~/v4sdk/bot-builder-tutorial-dispatch.md) |
| Add cards and buttons | Enhance the user experience with media other than text, such as graphics, menus, and cards | [How to add cards](v4sdk/bot-builder-howto-add-media-attachments.md) |

> [!NOTE]
> The table above is not a comprehensive list. Explore the articles on the left, starting with [sending messages](~/v4sdk/bot-builder-howto-send-messages.md), for more bot functionality.

Additionally we provide command line tools to help you to create, manage, and test bot assets. These tools can manage a bot configuration file, configure LUIS apps, build a QnA knowledge base, mock a conversation, and more - all from the command line. You can find more details in the [command line tools readme](https://github.com/Microsoft/botbuilder-tools/blob/master/README.md).

### Test 
Bots are complex apps, with a lot of different parts working together. Like any other complex app, this can lead to some interesting bugs or cause your bot to behave differently than expected. Before publishing, test your bot. 

We provide several ways to test bots before they are launched into the wild:

 [Test your bot locally with the emulator](bot-service-debug-emulator.md). The **Bot Framework Emulator** is a stand-alone app that not only provides a chat interface, but also debugging and interrogation tools to help understand how and why your bot does what it does.  The emulator can be run on a locally alongside your in-development bot application. 
 
 [Test your bot on the web](bot-service-manage-test-webchat.md). Once configured through the Azure portal your bot can also be reached through sample **web chat interface**. The web chat interface is a great way to grant access to your bot to testers and other people who do not have direct access to the bot's running code.

### Publish 
When you are ready for your bot to be available on the web, [publish your bot to Azure](bot-builder-howto-deploy-azure.md) or to your own web service or data center. Having an address on the public internet is the first step to your bot coming to life on your site, or inside chat channels.

### Connect          
Connect your bot to channels such as Facebook, Messenger, Kik, Skype, Slack, Microsoft Teams, Telegram, text/SMS, Twilio, Cortana, and Skype. Bot Framework does most of the work necessary to send and receive messages from all of these different platforms - your bot application receives a unified, normalized stream of messages regardless of the number and type of channels it is connected to.

[Choose channels to be added](bot-service-manage-channels.md).

### Evaluate 
Use the data collected in Azure portal to identify opportunities to improve the capabilities and performance of your bot. You can get service-level and instrumentation data like traffic, latency, and integrations. Analytics also provides conversation-level reporting on user, message, and channel data. 

Explore how to [gather analytics](bot-service-manage-analytics.md).


## Next steps

> [!div class="nextstepaction"]
> [Create a bot](bot-service-quickstart.md)

::: moniker-end
