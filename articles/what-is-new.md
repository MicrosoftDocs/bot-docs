---
title: What's new | Microsoft Docs
description: Learn what is new in Bot Framework.
keywords: bot framework, azure bot service
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 07/17/2019
monikerRange: 'azure-bot-service-4.0'
---

# What's new in Bot Framework (July 2019)

[!INCLUDE[applies-to](includes/applies-to.md)]

The Bot Framework SDK v4 is an [Open Source SDK][1a] that enable developers to model and build sophisticated conversation 
using their favorite programming language.

This article summarizes key new features and improvements in Bot Framework and Azure Bot Service.

|   | C#  | JS  | Python |   
|---|:---:|:---:|:------:|
|SDK |[4.5][1] | [4.5][2] | [4.4.0b2 (preview)][3] | 
|Docs | [docs][5] |[docs][5] |  | |
|Samples |[.NET Core][6], [WebAPI][10] |[Node.js][7] , [TypeScript][8], [es6][9]  | [Python][111] | | 

[1a]:https://github.com/microsoft/botframework-sdk/#readme
[1]:https://github.com/Microsoft/botbuilder-dotnet/#packages
[2]:https://github.com/Microsoft/botbuilder-js#packages
[3]:https://github.com/Microsoft/botbuilder-python#packages
[5]:https://docs.microsoft.com/azure/bot-service/?view=azure-bot-service-4.0
[6]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore
[7]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs
[8]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_typescript
[9]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_es6
[10]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_webapi
[111]:https://github.com/Microsoft/botbuilder-python/tree/master/samples


## Bot Framework Channels
- [Direct Line Speech (public preview)](https://aka.ms/streaming-extensions) | [docs](https://docs.microsoft.com/azure/bot-service/directline-speech-bot?view=azure-bot-service-4.0): Bot Framework and Microsoft's Speech Services provide a channel that enables streamed speech and text bi-directionally from the client to the bot application using WebSockets.  

- [Direct Line App Service Extension (public preview)](https://portal.azure.com) | [docs](https://aka.ms/directline-ase): A version of Direct Line 
that allows clients to connect directly to bots using the Direct Line API. This offers many benefits, including increased performance and more isolation. Direct Line App Service Extension is available on all Azure App Services, including those hosted within an Azure App Service Environment. An Azure App Service Environment provides isolation and is ideal for working within a VNet. A VNet lets you create your own private space in Azure and is crucial to your cloud network as it offers isolation, segmentation, and other key benefits. 

## Bot Framework SDK
- [Adaptive Dialog (SDK v4.6 preview)](https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog#readme) | [docs](https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog/docs) | [C# samples](https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog/csharp_dotnetcore): 
Adaptive Dialog now allow developers to dynamically update conversation flow based on context and events. This is especially useful when dealing with conversation context switches and interruptions in the middle of a conversation. 
  
- [Bot Framework Python SDK (preview 2)](https://github.com/microsoft/botbuilder-python) | [samples](https://github.com/Microsoft/botbuilder-python/tree/master/samples): The Python SDK now supports OAuth, Prompts, CosmosDB, and includes all major functionality in SDK 4.5. Plus, samples to help you learn about the new features in the SDK.

## Bot Framework Testing
- [Docs](https://aka.ms/testing-framework) | Unit testing packages ([C#](https://aka.ms/nuget-botbuilder-testing)/ [JavaScript](https://aka.ms/npm-botbuilder-testing)) | [C# sample](https://aka.ms/cs-core-test-sample) | [JS sample](https://aka.ms/js-core-test-sample): Addressing customers’ and developers’ ask for better testing tools, the July version of the SDK introduces a new unit testing capability. The Microsoft.Bot.Builder.testing package simplifies the process of unit testing dialogs in your bot.  

- [Channel Testing](https://github.com/Microsoft/BotFramework-Emulator/releases) | [docs](https://aka.ms/channel-testing): 

Introduced at Microsoft Build 2019, the Bot Inspector is a new feature in the Bot Framework Emulator which lets you debug and test bots on channels like Microsoft Teams, Slack, Cortana, and more. As you use the bot on specific channels, messages will be mirrored to the Bot Framework Emulator where you can inspect the message data that the bot received. Additionally, a snapshot of the bot memory state for any given turn between the channel and the bot is rendered as well.

## Web Chat
- Based on enterprise customers asks, we've added a [web chat sample](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/19.a.single-sign-on-for-enterprise-apps#single-sign-on-demo-for-enterprise-apps-using-oauth) that shows how to authorize a user to access resources on an enterprise app with a bot. Two types of resources are used to demonstrate the interoperability of OAuth with Microsoft Graph and GitHub API.

## Solutions
- [Virtual Assistant Solution Acclerator](https://github.com/Microsoft/botframework-solutions#readme) : Provides a set of templates, solution accelerators and skills to help build sophisticated conversational experiences. New Android app client for Virtual Assistant that integrates with Direct-Line Speech and Virtual Assistant demonstrating how a device client can interact with your Virtual Assistant and render Adaptive Cards. Updates also include support for Direct-Line Speech and Microsoft Teams.
  
- [Dynamics 365 Virtual Agent for Customer Service (public preview)](https://dynamics.microsoft.com/en-us/ai/virtual-agent-for-customer-service/): With the public preview, you can provide exceptional customer service with intelligent, adaptable virtual agents. Customer service experts can easily create and enhance bots with AI-driven insights.
  
- [Chat for Dynamics 365](https://www.powerobjects.com/powerpacks/powerchat/): Chat for Dynamics 365 offers several capabilities to ensure the support agents and end users can interact effectively and remain highly productive. Live chat and track conversations from visitors on your website within Microsoft Dynamics 365.

## Additional information
- You can see previous announcements [here](what-is-new-archive.md).
