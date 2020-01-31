---
title: What's new - Bot Service
description: Learn what is new in Bot Framework.
keywords: bot framework, azure bot service
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 12/10/2019
monikerRange: 'azure-bot-service-4.0'
---

# What's new December 2019

[!INCLUDE[applies-to](includes/applies-to.md)]

The Bot Framework SDK v4 is an [Open Source SDK](https://github.com/microsoft/botframework-sdk/#readme) that enable developers to model and build sophisticated conversation 
using their favorite programming language.

This article summarizes key new features and improvements in Bot Framework and Azure Bot Service.

|   | C#  | JS  | Python |  Java | 
|---|:---:|:---:|:------:|:-----:|
|Release |[GA][1] | [GA][2] | [GA][3] | [Preview 3][3a]|
|Docs | [docs][5] |[docs][5] |[docs][5]  | |
|Samples |[.NET Core][6], [WebAPI][10] |[Node.js][7], [TypeScript][8], [es6][9]  | [Python][11a] | | 

#### Python SDK (GA)
We are pleased to announce the general availability of the Python SDK. The SDK supports core bot runtime, connectors, middleware, dialogs, prompts, Language Understanding (LUIS), and QnA Maker. 

#### Bot Framework SDK for Skills (GA)

- **Skills for bots**: Create reusable conversational skills to add functionality to a bot. Leverage pre-built skills, such as Calendar, Email, Task, Point of Interest, Automotive, Weather, and News skills. Skills include language models, dialogs, QnA, and integration code delivered to customize and extend as required. [[Docs](https://aka.ms/skills-docs)]

- **Skills for Power Virtual Agent - Coming!**: For bots built with Power Virtual Agents, you can build new skills for these bots using Bot Framework and Azure Cognitive Services without needing to build a new bot from scratch. 

#### Bot Framework for Power Virtual Agent (GA)

Power Virtual Agent is designed to enable business users to create bots within a UI-based bot building SaaS experience, without having to code or manage specific AI services. 
Power Virtual Agents can be extended with the Microsoft Bot Framework, allowing developers and business users to collaborate in building bots for their organizations. [[Docs](https://docs.microsoft.com/dynamics365/ai/customer-service-virtual-agent/overview)]

#### Adaptive Dialogs (Preview)
Adaptive Dialogs enable developers to dynamically update conversation flow based on context and events. This is especially handy when dealing with conversation context switches and interruptions in the middle of a conversation. [[Docs][48] | [C# samples][49]] 

#### Language Generation (Preview)
Language Generation enables developers to separate logic used to generate bot's respones including the ability to define multiple variations on a phrase, execute simple expressions based on context, refer to conversational memory. [[Docs][44] | [C# samples][45]]

#### Common Expression Language (Preview)
Common Expression Language allows you to evaluate the outcome of a condition-based logic at runtime. Common language can be used across the Bot Framework SDK and conversational AI components, such as Adaptive Dialogs and Language Generation. [[Docs][40] | [API][41]]

[1]:https://github.com/Microsoft/botbuilder-dotnet/#packages
[2]:https://github.com/Microsoft/botbuilder-js#packages
[3]:https://github.com/Microsoft/botbuilder-python#packages
[3a]:https://github.com/Microsoft/botbuilder-java#packages
[5]:https://docs.microsoft.com/azure/bot-service/?view=azure-bot-service-4.0
[6]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore
[7]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs
[8]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/typescript_nodejs
[9]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_es6
[10]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_webapi
[11a]:https://aka.ms/python-sample-repo


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

## Additional information
- You can see previous announcements [here](what-is-new-archive.md).
