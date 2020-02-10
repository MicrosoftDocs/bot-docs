---
title: What's new (Archive) - Bot Service
description: Learn what is new in Bot Framework.
keywords: bot framework, azure bot service
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 11/28/2019
monikerRange: 'azure-bot-service-4.0'
---

# What's new November 2019

[!INCLUDE[applies-to](includes/applies-to.md)]

The Bot Framework SDK v4 is an [Open Source SDK](https://github.com/microsoft/botframework-sdk/#readme) that enable developers to model and build sophisticated conversation 
using their favorite programming language.

This article summarizes key new features and improvements in Bot Framework and Azure Bot Service.


|   | C#  | JS  | Python |  Java | 
|---|:---:|:---:|:------:|:-----:|
|Release |[4.6 GA][1] | [4.6 GA][2] | [Beta 4][3] | [Preview 3][3a]|
|Docs | [docs][5] |[docs][5] |  | |
|Samples |[.NET Core][6], [WebAPI][10] |[Node.js][7], [TypeScript][8], [es6][9]  | | | 

#### Bot Framework SDK for Microsoft Teams (GA)
The Bot Framework SDK v4.6 release fully integrates support for building Teams bots allowing users to use them in channel or group chat conversations. By adding a bot to a team or chat, all users of the conversation can take advantage of the bot functionality right in the conversation.  [[Docs](https://docs.microsoft.com/azure/bot-service/bot-builder-basics-teams)]

#### Bot Framework for Power Virtual Agent (Preview)

Power Virtual Agent is designed to enable business users to create bots within a UI-based bot building SaaS experience, without having to code or manage specific AI services. 
Power Virtual Agents can be extended with the Microsoft Bot Framework, allowing developers and business users to collaborate in building bots for their 
organizations. [[Docs](https://docs.microsoft.com/dynamics365/ai/customer-service-virtual-agent/overview)]


#### Bot Framework SDK for Skills (Preview)

- **Skills for bots**: Create reusable conversational skills to add functionality to a bot. Leverage pre-built skills, such as Calendar, Email, Task, Point of Interest, Automotive, Weather and News skills. Skills include language models, dialogs, QnA, and integration code delivered to customize and extend as required. [[Docs](https://microsoft.github.io/botframework-solutions/overview/skills/)]

- **Skills for Power Virtual Agent - Coming!**: For bots built with Power Virtual Agents, you can build new skills for these bots using Bot Framework and Azure Cognitive Services without needing to build a new bot from scratch. 

#### Adaptive Dialogs (Preview)
Adaptive Dialogs enable developers to dynamically update conversation flow based on context and events. This is especially handy when dealing with conversation context switches and interruptions in the middle of a conversation. [[Docs][48] | [C# samples][49]] 

#### Language Generation (Preview)
Language Generation enables developers to separate logic used to generate bot's respones including the ability to define multiple variations on a phrase, execute simple expressions based on context, refer to conversational memory. [[Docs][44] | [C# samples][45]]

#### Common Expression Language (Preview)
Common Expression Language allows you to evaluate the outcome of a condition-based logic at runtime. Common language can be used across the Bot Framework SDK and conversational AI components, such as Adaptive Dialogs and Language Generation. [[Docs][40] | [API][41]]

## What's new (July 2019)

[!INCLUDE[applies-to](includes/applies-to.md)]

The Bot Framework SDK v4 is an [Open Source SDK][1a] that enable developers to model and build sophisticated conversation 
using their favorite programming language.

This article summarizes key new features and improvements in Bot Framework and Azure Bot Service.

|   | C#  | JS  | Python |   
|---|:---:|:---:|:------:|
|SDK |[4.5][1] | [4.5][2] | [4.4.0b2 (preview)][3] | 
|Docs | [docs][5] |[docs][5] |  | |
|Samples |[.NET Core][6], [WebAPI][10] |[Node.js][7] , [TypeScript][8], [es6][9]  | [Python][11] | | 


### Bot Framework Channels
- [Direct Line Speech (public preview)](https://aka.ms/streaming-extensions) | [docs](https://docs.microsoft.com/azure/bot-service/directline-speech-bot?view=azure-bot-service-4.0): Bot Framework and Microsoft's Speech Services provide a channel that enables streamed speech and text bi-directionally from the client to the bot application using WebSockets.  

- [Direct Line App Service Extension (public preview)](https://portal.azure.com) | [docs](https://aka.ms/directline-ase): A version of Direct Line 
that allows clients to connect directly to bots using the Direct Line API. This offers many benefits, including increased performance and more isolation. Direct Line App Service Extension is available on all Azure App Services, including those hosted within an Azure App Service Environment. An Azure App Service Environment provides isolation and is ideal for working within a VNet. A VNet lets you create your own private space in Azure and is crucial to your cloud network as it offers isolation, segmentation, and other key benefits. 

### Bot Framework SDK
- [Adaptive Dialog (SDK v4.6 preview)](https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog#readme) | [docs](https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog/docs) | [C# samples](https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog/csharp_dotnetcore): 
Adaptive Dialog now allow developers to dynamically update conversation flow based on context and events. This is especially useful when dealing with conversation context switches and interruptions in the middle of a conversation. 
  
- [Bot Framework Python SDK (preview 2)](https://github.com/microsoft/botbuilder-python) | [samples](https://github.com/Microsoft/botbuilder-python/tree/master/samples): The Python SDK now supports OAuth, Prompts, CosmosDB, and includes all major functionality in SDK 4.5. Plus, samples to help you learn about the new features in the SDK.

### Bot Framework Testing
- [Docs](https://aka.ms/testing-framework) | Unit testing packages ([C#](https://aka.ms/nuget-botbuilder-testing)/ [JavaScript](https://aka.ms/npm-botbuilder-testing)) | [C# sample](https://aka.ms/cs-core-test-sample) | [JS sample](https://aka.ms/js-core-test-sample): Addressing customers’ and developers’ ask for better testing tools, the July version of the SDK introduces a new unit testing capability. The Microsoft.Bot.Builder.testing package simplifies the process of unit testing dialogs in your bot.  

- [Channel Testing](https://github.com/Microsoft/BotFramework-Emulator/releases) | [docs](https://aka.ms/channel-testing): 

Introduced at Microsoft Build 2019, the Bot Inspector is a new feature in the Bot Framework Emulator which lets you debug and test bots on channels like Microsoft Teams, Slack, Cortana, and more. As you use the bot on specific channels, messages will be mirrored to the Bot Framework Emulator where you can inspect the message data that the bot received. Additionally, a snapshot of the bot memory state for any given turn between the channel and the bot is rendered as well.

### Web Chat
- Based on enterprise customers asks, we've added a [web chat sample](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/19.a.single-sign-on-for-enterprise-apps#single-sign-on-demo-for-enterprise-apps-using-oauth) that shows how to authorize a user to access resources on an enterprise app with a bot. Two types of resources are used to demonstrate the interoperability of OAuth with Microsoft Graph and GitHub API.

### Solutions
- [Virtual Assistant Solution Acclerator](https://github.com/Microsoft/botframework-solutions#readme) : Provides a set of templates, solution accelerators and skills to help build sophisticated conversational experiences. New Android app client for Virtual Assistant that integrates with Direct-Line Speech and Virtual Assistant demonstrating how a device client can interact with your Virtual Assistant and render Adaptive Cards. Updates also include support for Direct-Line Speech and Microsoft Teams.
  
- [Dynamics 365 Virtual Agent for Customer Service (public preview)](https://dynamics.microsoft.com/en-us/ai/virtual-agent-for-customer-service/): With the public preview, you can provide exceptional customer service with intelligent, adaptable virtual agents. Customer service experts can easily create and enhance bots with AI-driven insights.
  
- [Chat for Dynamics 365](https://www.powerobjects.com/powerpacks/powerchat/): Chat for Dynamics 365 offers several capabilities to ensure the support agents and end users can interact effectively and remain highly productive. Live chat and track conversations from visitors on your website within Microsoft Dynamics 365.

## What's new (May 2019)

|   | C#  | JS  | Python |  Java | 
|---|:---:|:---:|:------:|:-----:|
|SDK |[4.4.3][1] | [4.4.0][2] | [4.4.0b1 (preview)][3] | [4.0.0a6 (preview)][3a]|
|Docs | [docs][5] |[docs][5] |  | |
|Samples |[.NET Core][6], [WebAPI][10] |[Node.js][7] , [TypeScript][8], [es6][9]  | [Python][11] | | 

<a name="V4-whats-new"></a>
## Bot Framework SDK (New! In preview)

- [Adaptive Dialog][47] | [docs][48] | [C# samples][49]: Adaptive Dialogs enable developers to build conversations that can be dynamically changed as the conversation progresses.  Traditionally developers have mapped out the entire flow of a conversation up front, which limits the flexibility of the conversation.  Adaptive dialogs allow them to be more flexible, to respond to changes in context and insert new steps or entire sub-dialogs into the conversation as it progresses. 

- [Language Generation][43] | [docs][44] | [C# samples][45]: Language Generation; which allows the developer to extract the embedded strings from their code and resource files and manage them through a Language Generation runtime and file format.  Language Generation enable customers to define multiple variations on a phrase, execute simple expressions based on context, refer to conversational memory, and over time will enable us to bring additional capabilities all leading to a more natural conversational experience.

- [Common Expression Language][40] | [api][41]: Both Adaptive dialogs and Language Generation rely on and use a common expression language to power bot conversations.

## Botkit
[Botkit][100] is a developer tool and SDK for building chat bots, apps and custom integrations for major messaging platforms. Botkit bots `hear()` triggers, `ask()` questions and `say()` replies. Developers can use this syntax to build dialogs - now cross compatible with the latest version of Bot Framework SDK. 

In addition, Botkit brings with it 6 platform adapters allowing Javascript bot applications to communicate directly with messaging platforms: [Slack][102], [Webex Teams][103], [Google Hangouts][104], [Facebook Messenger][105], [Twilio][106], and [Web chat][107].

Botkit is part of Microsoft Bot Framework and is released under the [MIT Open Source license][101]

## Bot Framework Solutions (New! In preview)

The [Bot Framework Solutions repository](https://github.com/Microsoft/AI#readme) is the home for a set of templates, solution accelerators and skills to help build advanced, assistant-like conversational experiences.

| Name | Description |  
|:------------|:------------| 
|[**Virtual Assistant**](https://github.com/Microsoft/AI/tree/master/docs#virtual-assistant) | Customers have a significant need to deliver a conversational assistant tailored to their brand, personalized to their users, and made available across a broad range of canvases and devices. <br/><br/> The Enterprise Template greatly simplifies the creation of a new bot project including: basic conversational intents, Dispatch integration, QnA Maker, Application Insights and an automated deployment.|
|[**Skills**](https://github.com/Microsoft/AI/blob/master/docs/overview/skills.md)| Developers can compose conversational experiences by stitching together re-usable conversational capabilities, known as Skills. Skills are themselves Bots, invoked remotely and a Skill developer template (.NET, TS) is available to facilitate creation of new Skills. 
|[**Analytics**](https://github.com/Microsoft/AI/blob/master/docs/readme.md#analytics)| Gain key insights into your bot’s health and behavior with the Conversational AI Analytics solutions. Review available telemetry, sample Application Insights queries, and Power BI dashboards to understand the full breadth of your bot’s conversations with users. |

## Azure Bot Service
Azure Bot Service enables you to host intelligent, enterprise-grade bots with complete ownership and control of your data. 
Developers can register and connect their bots to users on Skype, Microsoft Teams, Cortana, Web Chat, 
and more. [Azure][27]  |  [docs][28] | [connect to channels][29] 

* **Direct Line JS Client**: If you want to use the Direct Line channel in Azure Bot Service and are not using the WebChat client, 
the Direct Line JS client can be used in your custom application. Go to [Github][30] for more information.

<a name="ABS-whats-new"></a>

* **New! Direct Line Speech Channel**: We are bringing together the Bot Framework and Microsoft's Speech Services to provide a channel that enables streamed speech and text bi-directionally from the client to the bot application.  For more information, see how to add [speech channel to your bot](https://docs.microsoft.com/azure/bot-service/directline-speech-bot?view=azure-bot-service-4.0).


## Bot Framework Emulator
The [Bot Framework Emulator][60] is a  cross-platform desktop application that allows bot developers to test and debug bots built using the Bot Framework SDK. You can use the Bot Framework Emulator to test bots running locally on your machine or to connect to bots running remotely.

- [Downlad latest][61] | [Docs][62]

<a name="Emulator-whats-new"></a>
### Bot Inspector (New! In preview)

The Bot Framework Emulator has released a beta of the new Bot Inspector feature. It provides a way to debug and test your 
Bot Framework SDK v4 bots on channels like Microsoft Teams, Slack, Cortana, Facebook Messenger, Skype, etc. 
As you have the conversation, messages will be mirrored to the Bot Framework Emulator where you can inspect the 
message data that the bot received. Additionally, a snapshot of the bot state for any given turn between the 
channel and the bot is rendered as well. Read more about 
[Bot Inspector](https://github.com/Microsoft/BotFramework-Emulator/blob/master/content/CHANNELS.md).


## Related Services

### Language Understanding 
A machine learning-based service to build natural language experiences. Quickly create enterprise-ready, custom models that continuously improve. [Language Understanding Service(LUIS)][30] allows your application to understand what a person wants in their own words.

<a name="LUIS-whats-new"></a>

- **New! Roles, External Entities and Dynamic Entities**: LUIS has added several features that let developers extract more detailed information from text, so users can now build more intelligent solutions with less effort. LUIS also extended roles to all entity types, which allows the same entities to be classified with different subtypes based on context. Developers now have more granular control of what they can do with LUIS, including being able to identify and update models at runtime through dynamic lists and external entities. Dynamic lists are used to append to list entities at prediction time, permitting user-specific information to get matched exactly. Separate supplementary entity extractors are run with external entities, and that information can be appended to LUIS as strong signals for other models.

- **New! Analytics dashboard**: LUIS is releasing a more detailed, visually-rich comprehensive analytics dashboard. Its user-friendly design highlights common issues most users face when designing applications, by providing simple explanations on how to resolve them to help users gain more insight into their models’ quality, potential data problems, and guidance to adopt best practices.

[Docs][31] | [Add language understanding to your bot][32] 

### QnA Maker
[QnA Maker][33] is a cloud-based API service that creates a conversational, question-and-answer layer over your data. With QnA Maker, you can build, train and publish a simple question and answer bot based on FAQ URLs, structured documents, product manuals or editorial content in minutes.

<a name="QnA-whats-new"></a>

- **New! Extraction pipeline**: Now you can extract hierarchical information from URLs, files and sharepoint
- **New! Intelligence**: Contextual ranking models, active learning suggestions
- **New! Conversation**: Multi-turn conversations in QnA Maker.

[Docs][34]  | [add qnamaker to your bot][35] 

### Speech Services
[Speech Services](https://docs.microsoft.com/azure/cognitive-services/speech-service/) convert audio to text, perform speech translation and text-to-speech with the unified Speech services. With the speech services, you can integrate speech into your bot, create custom wake words, and author in multiple languages.

### Adaptive Cards
[Adaptive Cards](https://adaptivecards.io) are an open standard for developers to exchange card content in a common and consistent way, and are used by Bot Framework developers to create great cross-channel conversational experiences.

## Additional information
- Visit [GitHub page](https://github.com/Microsoft/botframework/blob/master/whats-new.md#whats-new) for more information.

[1]:https://github.com/Microsoft/botbuilder-dotnet/#packages
[1a]:https://github.com/microsoft/botframework-sdk/#readme
[2]:https://github.com/Microsoft/botbuilder-js#packages
[3]:https://github.com/Microsoft/botbuilder-python#packages
[3a]:https://github.com/Microsoft/botbuilder-java#packages
[5]:https://docs.microsoft.com/azure/bot-service/?view=azure-bot-service-4.0
[6]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore
[7]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs
[8]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/typescript_nodejs
[9]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_es6
[10]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_webapi
[11]:https://github.com/Microsoft/botbuilder-python/tree/master/samples

[18]:https://github.com/Microsoft/botbuilder-tools/tree/master/packages/LUIS#readme
[19]:https://github.com/Microsoft/botbuilder-tools/tree/master/packages/QnAMaker#readme

[27]:https://azure.microsoft.com/services/bot-service/
[28]:https://docs.microsoft.com/azure/bot-service/bot-service-overview-introduction?view=azure-bot-service-4.0
[29]:https://docs.microsoft.com/azure/bot-service/bot-service-manage-channels?view=azure-bot-service-4.0

[30]:https://www.luis.ai
[31]:https://docs.microsoft.com/azure/cognitive-services/LUIS/Home
[32]:https://docs.microsoft.com/azure/bot-service/bot-builder-howto-v4-luis?view=azure-bot-service-4.0&branch=pr-en-us-1325&tabs=csharp
[33]:https://www.qnamaker.ai/
[34]:https://aka.ms/what-is-qnamaker
[35]:https://docs.microsoft.com/azure/bot-service/bot-builder-howto-qna?view=azure-bot-service-4.0&branch=pr-en-us-1325&tabs=cs

[40]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/common-expression-language#readme
[41]:https://github.com/Microsoft/BotBuilder-Samples/blob/master/experimental/common-expression-language/api-reference.md
[43]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/language-generation#readme
[44]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/language-generation/docs
[45]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/language-generation/csharp_dotnetcore
[46]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/language-generation/javascript_nodejs/13.core-bot
[47]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog#readme
[48]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog/docs
[49]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog/csharp_dotnetcore
[50]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog/declarative

[60]:https://github.com/Microsoft/BotFramework-Emulator#readme
[61]:https://github.com/Microsoft/BotFramework-Emulator/releases/latest
[62]:https://docs.microsoft.com/azure/bot-service/bot-service-debug-emulator?view=azure-bot-service-4.0

[100]:https://github.com/howdyai/botkit#readme
[101]:https://github.com/howdyai/botkit/blob/master/LICENSE.md
[102]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-slack#readme
[103]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-webex#readme
[104]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-hangouts#readme
[105]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-facebook#readme
[106]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-twilio-sms#readme
[107]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-web#readme
