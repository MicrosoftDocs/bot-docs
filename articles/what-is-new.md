---
title: What's new | Microsoft Docs
description: Learn what is new in Bot Framework.
keywords: bot framework, azure bot service
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: get-started-article
ms.service: bot-service
ms.subservice: abs
ms.date: 05/23/2019
monikerRange: 'azure-bot-service-4.0'
---

# What's new in Bot Framework

[!INCLUDE[applies-to](includes/applies-to.md)]

The Bot Framework SDK v4 is an [Open Source SDK][1a] that enable developers to model and build sophisticated conversation 
using their favorite programming language.

This article summarizes key new features and improvements in Bot Framework and Azure Bot Service.

|   | C#  | JS  | Python |  Java | 
|---|:---:|:---:|:------:|:-----:|
|SDK |[4.4.3][1] | [4.4.0][2] | [4.4.0b1 (preview)][3] | [4.0.0a6 (preview)][3a]|
|Docs | [docs][5] |[docs][5] |  | |
|Samples |[.NET Core][6], [WebAPI][10] |[Node.js][7] , [TypeScript][8], [es6][9]  | [Python][111] | | 

[1a]:https://github.com/microsoft/botframework-sdk/#readme
[1]:https://github.com/Microsoft/botbuilder-dotnet/#packages
[2]:https://github.com/Microsoft/botbuilder-js#packages
[3]:https://github.com/Microsoft/botbuilder-python#packages
[3a]:https://github.com/Microsoft/botbuilder-java#packages
[4]:https://github.com/Microsoft/botbuilder-java#packages
[5]:https://docs.microsoft.com/en-us/azure/bot-service/?view=azure-bot-service-4.0
[6]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore
[7]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs
[8]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_typescript
[9]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_es6
[10]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_webapi
[111]:https://github.com/Microsoft/botbuilder-python/tree/master/samples

<a name="V4-whats-new"></a>
## Bot Framework SDK (New! In preview)

- [Adaptive Dialog][47] | [docs][48] | [C# samples][49]: Adaptive Dialogs enable developers to build conversations that can be dynamically changed as the conversation progresses.  Traditionally developers have mapped out the entire flow of a conversation up front, which limits the flexibility of the conversation.  Adaptive dialogs allow them to be more flexible, to respond to changes in context and insert new steps or entire sub-dialogs into the conversation as it progresses. 

- [Language Generation][43] | [docs][44] | [C# samples][45]: Language Generation; which allows the developer to extract the embedded strings from their code and resource files and manage them through a Language Generation runtime and file format.  Language Generation enable customers to define multiple variations on a phrase, execute simple expressions based on context, refer to conversational memory, and over time will enable us to bring additional capabilities all leading to a more natural conversational experience.

- [Common Expression Language][40] | [api][41]: Both Adaptive dialogs and Language Generation rely on and use a common expression language to power bot conversations.

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

## Botkit
[Botkit][100] is a developer tool and SDK for building chat bots, apps and custom integrations for major messaging platforms. Botkit bots `hear()` triggers, `ask()` questions and `say()` replies. Developers can use this syntax to build dialogs - now cross compatible with the latest version of Bot Framework SDK. 

In addition, Botkit brings with it 6 platform adapters allowing Javascript bot applications to communicate directly with messaging platforms: [Slack][102], [Webex Teams][103], [Google Hangouts][104], [Facebook Messenger][105], [Twilio][106], and [Web chat][107].

Botkit is part of Microsoft Bot Framework and is released under the [MIT Open Source license][101]

[100]:https://github.com/howdyai/botkit#readme
[101]:https://github.com/howdyai/botkit/blob/master/LICENSE.md
[102]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-slack#readme
[103]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-webex#readme
[104]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-hangouts#readme
[105]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-facebook#readme
[106]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-twilio-sms#readme
[107]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-web#readme

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

* **New! Direct Line Speech Channel**: We are bringing together the Bot Framework and Microsoft's Speech Services to provide a channel that enables streamed speech and text bi-directionally from the client to the bot application.  For more information, see how to add [speech channel to your bot](https://docs.microsoft.com/en-us/azure/bot-service/directline-speech-bot?view=azure-bot-service-4.0).

[27]:https://azure.microsoft.com/en-us/services/bot-service/
[28]:https://docs.microsoft.com/en-us/azure/bot-service/bot-service-overview-introduction?view=azure-bot-service-4.0
[29]:https://docs.microsoft.com/en-us/azure/bot-service/bot-service-manage-channels?view=azure-bot-service-4.0
[30]:https://github.com/Microsoft/BotFramework-DirectLineJS/blob/master/README.md


## Bot Framework Emulator
The [Bot Framework Emulator][60] is a  cross-platform desktop application that allows bot developers to test and debug bots built using the Bot Framework SDK. You can use the Bot Framework Emulator to test bots running locally on your machine or to connect to bots running remotely.

- [Downlad latest][61] | [Docs][62]

<a name="Emulator-whats-new"></a>
### Bot Inspector (New! In preview)

The Bot Framework Emulator has released a beta of the new Bot Inspector feature. It provides a way to debug and test your Bot Framework SDK v4 bots on channels like Microsoft Teams, Slack, Cortana, Facebook Messenger, Skype, etc. As you have the conversation, messages will be mirrored to the Bot Framework Emulator where you can inspect the message data that the bot received. Additionally, a snapshot of the bot state for any given turn between the channel and the bot is rendered as well. Read more about [Bot Inspector](https://github.com/Microsoft/BotFramework-Emulator/blob/master/content/CHANNELS.md)

[60]:https://github.com/Microsoft/BotFramework-Emulator#readme
[61]:https://github.com/Microsoft/BotFramework-Emulator/releases/latest
[62]:https://docs.microsoft.com/en-us/azure/bot-service/bot-service-debug-emulator?view=azure-bot-service-4.0


## Related Services

### Language Understanding 
A machine learning-based service to build natural language experiences. Quickly create enterprise-ready, custom models that continuously improve. [Language Understanding Service(LUIS)][30] allows your application to understand what a person wants in their own words.

<a name="LUIS-whats-new"></a>

- **New! Roles, External Entities and Dynamic Entities**: LUIS has added several features that let developers extract more detailed information from text, so users can now build more intelligent solutions with less effort. LUIS also extended roles to all entity types, which allows the same entities to be classified with different subtypes based on context. Developers now have more granular control of what they can do with LUIS, including being able to identify and update models at runtime through dynamic lists and external entities. Dynamic lists are used to append to list entities at prediction time, permitting user-specific information to get matched exactly. Separate supplementary entity extractors are run with external entities, and that information can be appended to LUIS as strong signals for other models.

- **New! Analytics dashboard**: LUIS is releasing a more detailed, visually-rich comprehensive analytics dashboard. Its user-friendly design highlights common issues most users face when designing applications, by providing simple explanations on how to resolve them to help users gain more insight into their models’ quality, potential data problems, and guidance to adopt best practices.

[Docs][31] | [Add language understanding to your bot][32] 

[18]:https://github.com/Microsoft/botbuilder-tools/tree/master/packages/LUIS#readme
[19]:https://github.com/Microsoft/botbuilder-tools/tree/master/packages/QnAMaker#readme
[30]:https://www.luis.ai
[31]:https://docs.microsoft.com/en-us/azure/cognitive-services/LUIS/Home
[32]:https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-v4-luis?view=azure-bot-service-4.0&branch=pr-en-us-1325&tabs=csharp

### QnA Maker
[QnA Maker][33] is a cloud-based API service that creates a conversational, question-and-answer layer over your data. With QnA Maker, you can build, train and publish a simple question and answer bot based on FAQ URLs, structured documents, product manuals or editorial content in minutes.

<a name="QnA-whats-new"></a>

- **New! Extraction pipeline**: Now you can extract hierarchical information from URLs, files and sharepoint
- **New! Intelligence**: Contextual ranking models, active learning suggestions
- **New! Conversation**: Multi-turn conversations in QnA Maker.

[Docs][34]  | [add qnamaker to your bot][35] 

[33]:https://www.qnamaker.ai/
[34]:https://aka.ms/qnamaker-docs-home
[35]:https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-qna?view=azure-bot-service-4.0&branch=pr-en-us-1325&tabs=cs

### Speech Services
[Speech Services](https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/) convert audio to text, perform speech translation and text-to-speech with the unified Speech services. With the speech services, you can integrate speech into your bot, create custom wake words, and author in multiple languages.

### Adaptive Cards
[Adaptive Cards](https://adaptivecards.io) are an open standard for developers to exchange card content in a common and consistent way, and are used by Bot Framework developers to create great cross-channel conversatational experiences.

## Additional information
- Visit GitHub page for more [information](https://github.com/Microsoft/botframework/blob/master/whats-new.md#whats-new).
