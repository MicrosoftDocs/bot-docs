---
title: What's new (Archive) - Bot Service
description: Learn about improvements and new features in successive 2019 releases of the Bot Framework SDK, including new functionality in Skills, Teams, and other areas.
keywords: bot framework, azure bot service
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 03/22/2021
---

# What's new November 2020

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

The Bot Framework SDK v4 is an [Open Source SDK](https://github.com/microsoft/botframework-sdk/#readme) that enable developers to model and build sophisticated conversations using their favorite programming language.

This article summarizes key new features and improvements in Bot Framework and Azure Bot Service.

|Item | C#  | JS  | Python | Java
|:----|:---:|:---:|:------:|:-----:
|Release |[4.11 (GA)][1] | [4.11 (GA)][2] | [4.11 (GA)][3] | [4.7 Preview][3a]
|Samples |[.NET Core][6], [WebAPI][10] |[Node.js][7], [TypeScript][8], [es6][9]  | [Python][11a] |

Welcome to the November 2020 release of the Bot Framework SDK. There are a number of updates in this version that we hope you will like; some of the key highlights include:

- [Documentation](#documentation-updates): improvements to existing documentation, including READMEs for the code and samples repositories.
- Teams: added support for the Participant Meeting API, and other general improvements.
- Skills: can now be run and tested locally in the Emulator without an app ID and password, improved support for skills in adaptive dialogs.
- Orchestrator (preview): a Language Understanding technology for routing incoming user utterances to an appropriate skill or to subsequent language processing service such as LUIS or QnA Maker.
- Cloud adapter (preview, .NET only): a bot adapter that supports hosting a bot in any cloud environment.

**Insiders**: Want to try new features as soon as possible? You can download the nightly Insiders build [[C#](https://github.com/microsoft/botbuilder-dotnet/blob/main/UsingMyGet.md)] [[JS](https://github.com/microsoft/botbuilder-js/blob/main/UsingMyGet.md)] [[Python](https://github.com/microsoft/botbuilder-python/blob/main/UsingTestPyPI.md)] [[CLI](https://github.com/Microsoft/botframework-cli#nightly-builds)] and try the latest updates as soon as they are available. And for the latest Bot Framework news, updates, and content, follow us on Twitter @msbotframework!

## Documentation updates

Following feedback from customers and the Bot Framework Support team, a number of documents were created or updated. These are helpful towards providing answers and information relating to recurring issues from bot developers.

- Expanded code comment documentation in the SDK repositories.
- Improved READMEs in the samples and SDK repositories.
- New and updated documents addressing recurring bot developer issues:
  - Updated and expanded the [conceptual](v4sdk/bot-builder-adaptive-dialog-Introduction.md) and [how-to](v4sdk/bot-builder-adaptive-dialog-setup.md) articles for adaptive dialogs.
  - Updated and reorganized the [authentication and security](v4sdk/bot-builder-authentication-basics.md) articles.
  - Updates to the [Bot Framework Composer](/composer/) documentation.
  - Issue fixes and general documentation improvements to response generation, Cognitive Services, adaptive expressions, skills, channels, and other topics.
  - The SDK v3 documentation has been removed from the main doc set and is available on the [previous versions](/previous-versions) site.

### Additional information

- You can see previous announcements in the [archived information](what-is-new-archive.md).
- See the Bot Framework SDK [release notes](https://github.com/microsoft/botframework-sdk/releases/) for more information about the changes made to the SDK in the 4.11 release.

## What's new August 2020

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

The Bot Framework SDK v4 is an [Open Source SDK](https://github.com/microsoft/botframework-sdk/#readme) that enable developers to model and build sophisticated conversations using their favorite programming language.

This article summarizes key new features and improvements in Bot Framework and Azure Bot Service.

|Item | C#  | JS  | Python | Java
|:----|:---:|:---:|:------:|:-----:
|Release |[4.10 (GA)][1] | [4.10 (GA)][2] | [4.10 (GA)][3] | [4.6 Preview][3a]
|Samples |[.NET Core][6], [WebAPI][10] |[Node.js][7], [TypeScript][8], [es6][9]  | [Python][11a] |

Welcome to the August 2020 release of the Bot Framework SDK. There are a number of updates in this version that we hope you will like; some of the key highlights include:

- [Documentation](#documentation): improvements to existing documentation, including READMEs for the code and samples repositories.
- [Customer supportability](#customer-supportability): improvements focused on developers seeking assistance using the Bot Framework, tools and SDKs.
- [Customer requests](#customer-requests): improvements focused on feature requests from the developer community and 3rd parties using the Bot Framework SDK and tools.
- [Code quality](#code-quality): improvements focused on unit and functional test coverage and on the reference documentation.
- [New SDK features](#new-sdk-features): new preview features added in this release.
- [Other improvements](#other-improvements): other improvements to the SDK.

**Insiders**: Want to try new features as soon as possible? You can download the nightly Insiders build [[C#](https://github.com/microsoft/botbuilder-dotnet/blob/main/UsingMyGet.md)] [[JS](https://github.com/microsoft/botbuilder-js/blob/main/UsingMyGet.md)] [[Python](https://github.com/microsoft/botbuilder-python/blob/main/UsingTestPyPI.md)] [[CLI](https://github.com/Microsoft/botframework-cli#nightly-builds)] and try the latest updates as soon as they are available. And for the latest Bot Framework news, updates, and content, follow us on Twitter @msbotframework!

### Documentation

Following feedback from customers and the Bot Framework Support team, a number of documents were created or updated. These are helpful towards providing answers and information relating to recurring issues from bot developers.

- Expanded code comment documentation in the SDK repositories.
- Improved READMEs in the samples and SDK repositories.
- New and updated documents addressing recurring bot developer issues:
  - Addition of an Azure Bot Service](/azure/bot-service/) hub page that links to both the Bot Framework Composer and the Bot Framework SDK documentation.
  - Updates to [language generation](v4sdk/bot-builder-concept-language-generation.md), [adaptive expressions](v4sdk/bot-builder-concept-adaptive-expressions.md), and [adaptive dialog](v4sdk/bot-builder-adaptive-dialog-introduction.md) articles.
  - Updates to the [Bot Framework Composer](/composer/) documentation.
  - Issue fixes and general documentation improvements to authentication, skills, channels, and other topics.

### Customer supportability

Developers using the Microsoft Bot Framework have many [resources](bot-service-resources-links-help.md) for getting help. Internal tools have been improved to increase the responsiveness of the engineering team in areas of most interest to developers.

- Creation of internal bots and improved tools for customer support.
- Improved analytics of trends in customer reported feature requests and issues.
- Coordination of labels across GitHub repositories.

### Customer requests

- Additional Teams channel lifecycle events.
- Improved Application Insights integration.
- Coordination of labels across GitHub repositories.
- Addition of a locale to the conversation update activity.
- Added alt-text support to card actions for images on buttons.
- Updated the skill handler to return a resource response object.
- Included support for the latest version of Azure Blobs storage.
- Improvements to the OAuth prompt dialog.
- Various bug fixes and telemetry improvements.

### Code quality

- Enforcing code style and format rules.
- Improved unit test code coverage and quality.
- Increased profiling of the code base.
- REST API Swagger files unified across SDK repositories. Introduced a version to the files.
- Added a settings object pattern for C# adapters.
- Added dependency policing in the JavaScript SDK.
- Added integration tests for Adaptive cards and Direct Line JavaScript.

### New SDK features

- [Orchestrator (preview)](https://github.com/microsoft/BotBuilder-Samples/blob/main/experimental/orchestrator/README.md): a transformer-based solution that runs locally with your bot to dispatch across one or more Bot Builder skills, LUIS applications, or QnA Maker knowledge bases.
- [Bot Builder Azure Queues (C# preview)](https://www.nuget.org/packages/Microsoft.Bot.Builder.Azure.Queues): better integration with Azure Queues and the _continue conversation later_ dialog.
- [Bot Builder Azure Blobs (C# preview)](https://www.nuget.org/packages/Microsoft.Bot.Builder.Azure.Blobs): new implementations of Azure Blob storage. This library is a replacement for the older Azure Blob storage implementation.

### Other improvements

- Continued improvements to Microsoft Teams API support.
- Bot Framework CLI tools added `lg` as a core plugin and included other overall tool improvements.
- Updated the README files in the samples and added new Teams Typescript samples.
- Composer improved support for skills and improved integration for Cognitive Services.
- Web Chat added many accessibility improvements.
- Emulator added bug fixes and updates.

## What's new May 2020

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

The Bot Framework SDK v4 is an [Open Source SDK](https://github.com/microsoft/botframework-sdk/#readme) that enable developers to model and build sophisticated conversation
using their favorite programming language.

This article summarizes key new features and improvements in Bot Framework and Azure Bot Service.

|Item | C#  | JS  | Python | Java
|:----|:---:|:---:|:------:|:-----:
|Release |[4.9.1 (GA)][1] | [4.9.0 (GA)][2] | [4.9.0 (GA)][3] | [4.6 Preview][3a]|
|Samples |[.NET Core][6], [WebAPI][10] |[Node.js][7], [TypeScript][8], [es6][9]  | [Python][11a] | |

Welcome to the May 2020 release of the Bot Framework SDK. There are a number of updates in this version that we hope you will like; some of the key highlights include:

* [Skills](#skills) - Skills now support adaptive dialogs and all activity types, and have improved support for SSO and OAuth. The v2.1 skill manifest is now GA.  We also added Bot Framework Composer support for building and consuming Skills.
* [Microsoft Teams](#microsoft-teams) - Improvements in Microsoft Teams API support, including support in Java!
* [Bot Telemetry](#bot-telemetry) - Mapping of Dialogs into Azure AppInsights Page View Events.
* [Adaptive Dialogs](#adaptive-dialogs) - A more flexible, event driven dialog system for implementing multi-turn conversational patterns.
* [CLI tools for Adaptive Dialogs](#cli-tools-for-adaptive-dialogs) - new ability to merge and validate adaptive schema assets.
* [Language Generation](#language-generation) - Add language and personality responses to your bot conversations.
* [Adaptive Expressions](#adaptive-expressions) -  Use bot aware expressions to react to user input and drive bot functionality.
* [Authentication Improvements](#authentication-improvements) - SSO between Bots and Skills and improvements to X.509 auth.
* [Generated Dialogs](#generated-dialogs---early-preview) (Early Preview) - Automatically create robust Bot Framework Composer assets from JSON or JSON Schema that leverage Adaptive Dialogs.
* [VS Code debugger for Adaptive Dialogs](#vs-code-debugger---early-preview) (Early Preview) - Create & validate .lu and .lg documents as well as debug declaratively defined adaptive dialogs.
* [Bot Framework Composer](#bot-framework-composer) - A visual authoring canvas for developers and multi-disciplinary teams to build bots.

**Insiders**: Want to try new features as soon as possible? You can download the nightly Insiders build [[C#](https://github.com/microsoft/botbuilder-dotnet/blob/master/UsingMyGet.md)] [[JS](https://github.com/microsoft/botbuilder-js/blob/master/UsingMyGet.md)] [[Python](https://github.com/microsoft/botbuilder-python/blob/master/UsingTestPyPI.md)] [[CLI](https://github.com/Microsoft/botframework-cli#nightly-builds)] and try the latest updates as soon as they are available. And for the latest Bot Framework news, updates, and content, follow us on Twitter @msbotframework!

### Skills
[Skills](v4sdk/skills-conceptual.md) have been updated to work with adaptive dialogs, and both adaptive and traditional dialogs will now accept all types of activities.

The skill manifest schema has been updated to [version 2.1](https://github.com/microsoft/botframework-sdk/tree/master/schemas/skills). Improvements in this version include the ability to declare & share your language models, and define any type of activity that your skill can receive.

This release also includes authentication improvements with skills, including using SSO with dialogs, and OAuth without needing a magic code in WebChat and DirectLine.

### Microsoft Teams
We continue to focus on making sure all the Teams-specific APIs are fully supported across our SDKs. This release brings full support for Microsoft Teams APIs in the preview [Java SDK](https://github.com/microsoft/botbuilder-java), including [samples](https://github.com/microsoft/botbuilder-java/tree/master/samples).

The `OnTeamsMemberAdded` event in the activity handler has been updated to use the get single member endpoint under the covers, which should significantly reduce latency and reliability of this event in large teams.

The `TeamsChannelAccount` object has been updated to include `userRole` (one of owner, member, or guest) and `tenantId` (for the user's tenantId).

### Bot Telemetry
Bots now capture Page View events, native to Application Insights, whenever a dialog is started. This allows you to use the User Flows dashboard in Application Insights to see how users move through your bot, between dialogs and where they drop out.

![Telemetry In AppInsights](https://raw.githubusercontent.com/microsoft/botframework-sdk/master/docs/media/UserFlowsAppInsights.jpg?raw=true)

### Adaptive Dialogs
We're also excited to make [Adaptive Dialogs](v4sdk/bot-builder-adaptive-dialog-introduction.md) generally available in C# and as a preview release in JavaScript!

Adaptive Dialogs, which underpin the dialog design and management authoring features found in Bot Framework Composer, enable developers to dynamically update conversation flow based on context and events. This is especially useful when dealing with more sophisticated conversation requirements, such as context switches and interruptions.  Bot Framework Skills can now also leverage Adaptive Dialogs.

Adaptive Dialogs also now support Telemetry. Data from Adaptive Dialogs, including  triggers, actions and recognizers now flow into your Azure Application Insights instance.

### CLI tools for Adaptive Dialogs
[CLI tools](https://github.com/Microsoft/botframework-cli) for Adaptive Dialogs, Language Generation, QnaMaker and Luis Cross-train - new ability to merge and validate adaptive schema assets, augment qna and lu files, create/ update/ replace/ train/ publish LUIS and or QnA maker application and Language Generation templates manipulation.

New CLI Tools were added for management of Adaptive Dialogs.
- [bf-dialog](https://github.com/microsoft/botframework-cli/tree/master/packages/dialog#relevant-docs) supports merging dialog schema files and verify file format correctness.
- [bf-luis](https://github.com/microsoft/botframework-cli/tree/master/packages/luis#relevant-docs) Adds commands to augment lu files and create/ update/ replace/ train/ publish LUIS
- [bf-qnamaker](https://github.com/microsoft/botframework-cli/tree/master/packages/qnamaker#relevant-docs) Adds commands to augment qna files and create/ update/ replace/ train/ publish QnAMaker
- [bf-lg](https://github.com/microsoft/botframework-cli/tree/master/packages/lg#relevant-docs) Parse, collate, expand and translate lg files.

### Language Generation

LG is Generally Available (GA) on both the C# and JS Platforms.

[Language Generation (LG)](v4sdk/bot-builder-concept-language-generation.md) enables you to define multiple variations of a phrase, execute simple expressions based on context, and refer to conversational memory. At the core of language generation lies template expansion and entity substitution. You can provide one-of variation for expansion as well as conditionally expanding a template. The output from language generation can be a simple text string or multi-line response or a complex object payload that a layer above language generation will use to construct a complete activity. The Bot Framework Composer natively supports language generation to produce output activities using the LG templating system.

You can use Language Generation to:
* Achieve a coherent personality, tone of voice for your bot.
* Separate business logic from presentation.
* Include variations and sophisticated composition for any of your bot's replies.
* Construct cards, suggested actions and attachments using a structured response template.

Language Generation is achieved through:

* A markdown based .lg file that contains the templates and their composition.
Full access to the current bot's memory so you can data bind language to the state of memory.
* Parser and runtime libraries that help achieve runtime resolution.

### Adaptive Expressions
[Adaptive Expressions](v4sdk/bot-builder-concept-adaptive-expressions.md) are Generally Available (GA) on both the C# and JS Platforms.

Bots use expressions to evaluate the outcome of a condition based on runtime information available in memory to the dialog or the Language Generation system. These evaluations determine how your bot reacts to user input and other factors that impact bot functionality.

Adaptive expressions were created to address this core need as well as provide an adaptive expression language that can used with the Bot Framework SDK and other conversational AI components, like [Bot Framework Composer](/composer/), Language Generation, Adaptive dialogs, and [Adaptive Cards](/adaptive-cards/).

An adaptive expression can contain one or more explicit values, pre-built functions or custom functions. Consumers of adaptive expressions also have the capability to inject additional supported functions. For example, all Language Generation templates are available as functions as well as additional functions that are only available within that component's use of adaptive expressions.

### Authentication Improvements
We added support for single sign-on while using Expect Replies. This applies to SSO performed between a pair of bots: host and a skill.

For Bot Identification we've added the ability to specify `sendx5c` parameter for certificate authentication. This feature was requested by customers and allows for more flexibility when using cert auth.

Additional Sovereign Clouds are supported.

### Generated Dialogs - Early Preview

The Bot Framework has a rich collection of conversational building blocks, but
creating a bot that feels natural to converse with requires understanding and
coordinating across language understanding, language generation and dialog
management. To simplify this process and capture best practices, we've created
the [bf-generate](https://github.com/microsoft/BotBuilder-Samples/blob/master/experimental/generation/generator/README.md) plugin for the [BotFramework CLI tool](https://github.com/microsoft/botframework-cli). The
generated dialogs make use of event-driven adaptive dialogs with a rich and
evolving set of capabilities including:

- Handle out of order and multiple responses for simple and array properties.
- Add, remove, clear and show properties.
- Support for choosing between ambiguous entity values and entity property mappings.
- Recognizing and mapping for all LUIS prebuilt entities.
- Help function, including auto-help on multiple retries.
- Cancel
- Confirmation

### VS Code Debugger - Early Preview
[Adaptive tools](https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-tool) is a brand new Visual studio code extension you can use to create/ validate .lu and .lg documents as well as debug declaratively defined adaptive dialogs. This extension provides rich authoring & editing capabilities for .lu and .lg file formats including syntax highlighting, auto-suggest and auto-complete.

We anticipate adding an early preview to the VS Marketplace shortly after this release.

### Bot Builder Community
During this release, the Bot Builder Community has further raised the bar by adding more features, more adapters, and fixing more bugs.

1. A revised C# [Alexa Adapter](https://github.com/BotBuilderCommunity/botbuilder-community-dotnet/tree/develop/libraries/Bot.Builder.Community.Adapters.Alexa) and [Google Home Adapter]() Re-built from the ground up, starting with Alexa, to allow the adapters to be consumed by Azure Bot Service and made available as channels. Improvements include better native activity type mapping, improved markdown rendering and support for more complex scenarios (such as merging multiple outgoing activities).

2. A new C# [Zoom Adapter](https://github.com/BotBuilderCommunity/botbuilder-community-dotnet/tree/develop/libraries/Bot.Builder.Community.Adapters.Zoom) that supports Zoom 1:1 and channel chat capabilities and converts them to native BF activity types. With it, you can subscribe to any event that Zoom supports, with full support for Zoom interactive messages and rich message templates. (The adapter translates Zoom events into BF event activities.)

3. A [RingCentral Adapter](https://github.com/BotBuilderCommunity/botbuilder-community-dotnet/tree/develop/libraries/Bot.Builder.Community.Adapters.RingCentral). The [RingCentral](https://www.ringcentral.com/) Engage adapter allows you to add an additional endpoint to your bot for [RingCentral Engage Digital Platform](https://www.ringcentral.com/digital-customer-engagement.html) integration. The RingCentral endpoint can be used in conjunction with other channels meaning, for example, you can have a bot exposed on out of the box channels such as Facebook and Teams, but also integrated as an [RingCentral Engage Digital Source SDK](https://support.ringcentral.com/s/article/RingCentral-Engage-Digital-Introduction?language=en_US) into RingCentral.

### Bot Framework Composer

[Bot Framework Composer](https://docs.microsoft.com/composer/) is Generally Available (GA) on the [Windows](https://aka.ms/composer-windows-download) | [macOS](https://aka.ms/composer-mac-download) | [Linux](https://aka.ms/composer-linux-download) platforms.

Bot Framework Composer is a visual authoring canvas for developers and multi-disciplinary teams to build bots. It is an open source conversational application based on the Microsoft Bot Framework SDK. Within Composer, you will find everything you need to build a sophisticated conversational experience:

* A visual editing canvas for conversation flow.
* In-context editing for language understanding.
* Tools to train and manage language understanding (such as LUIS and QnA Maker) components.
* Powerful language generation and templating systems.
* A ready-to-use bot run-time executable.

## What's new November 2019

The Bot Framework SDK v4 is an [Open Source SDK](https://github.com/microsoft/botframework-sdk/#readme) that enable developers to model and build sophisticated conversation
using their favorite programming language.

This article summarizes key new features and improvements in Bot Framework and Azure Bot Service.

|Item | C#  | JS  | Python | Java  |
|:----|:---:|:---:|:------:|:-----:|
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

The Bot Framework SDK v4 is an [Open Source SDK][1a] that enable developers to model and build sophisticated conversation
using their favorite programming language.

This article summarizes key new features and improvements in Bot Framework and Azure Bot Service.

| Item    | C#                           | JS                                       | Python                 |
|:--------|:----------------------------:|:----------------------------------------:|:----------------------:|
| SDK     | [4.5][1]                     | [4.5][2]                                 | [4.4.0b2 (preview)][3] |
| Docs    | [docs][5]                    | [docs][5]                                |                        |
| Samples | [.NET Core][6], [WebAPI][10] | [Node.js][7] , [TypeScript][8], [es6][9] | [Python][11]           |

### Bot Framework Channels

- [Direct Line Speech (public preview)](https://aka.ms/streaming-extensions) | [docs](directline-speech-bot.md): Bot Framework and Microsoft's Speech Services provide a channel that enables streamed speech and text bi-directionally from the client to the bot application using WebSockets.

- [Direct Line App Service Extension (public preview)](https://portal.azure.com) | [docs](https://aka.ms/directline-ase): A version of Direct Line
that allows clients to connect directly to bots using the Direct Line API. This offers many benefits, including increased performance and more isolation. Direct Line App Service Extension is available on all Azure App Services, including those hosted within an Azure App Service Environment. An Azure App Service Environment provides isolation and is ideal for working within a VNet. A VNet lets you create your own private space in Azure and is crucial to your cloud network as it offers isolation, segmentation, and other key benefits.

### Bot Framework SDK
- [Adaptive Dialog (SDK v4.6 preview)](https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog#readme) | [docs](https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog/docs) | [C# samples](https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog/csharp_dotnetcore):
Adaptive Dialog now allow developers to dynamically update conversation flow based on context and events. This is especially useful when dealing with conversation context switches and interruptions in the middle of a conversation.

- [Bot Framework Python SDK (preview 2)](https://github.com/microsoft/botbuilder-python) | [samples](https://github.com/Microsoft/botbuilder-python/tree/master/samples): The Python SDK now supports OAuth, Prompts, CosmosDB, and includes all major functionality in SDK 4.5. Plus, samples to help you learn about the new features in the SDK.

### Bot Framework Testing
- [Docs](./v4sdk/unit-test-bots.md) | Unit testing packages ([C#](https://aka.ms/nuget-botbuilder-testing)/ [JavaScript](https://aka.ms/npm-botbuilder-testing)) | [C# sample](https://aka.ms/cs-core-test-sample) | [JS sample](https://aka.ms/js-core-test-sample): Addressing customers' and developers' ask for better testing tools, the July version of the SDK introduces a new unit testing capability. The Microsoft.Bot.Builder.testing package simplifies the process of unit testing dialogs in your bot.

- [Channel Testing](https://github.com/Microsoft/BotFramework-Emulator/releases) | [docs](bot-service-debug-inspection-middleware.md):

Introduced at Microsoft Build 2019, the Bot Inspector is a new feature in the Bot Framework Emulator which lets you debug and test bots on channels like Microsoft Teams, Slack, and more. As you use the bot on specific channels, messages will be mirrored to the Bot Framework Emulator where you can inspect the message data that the bot received. Additionally, a snapshot of the bot memory state for any given turn between the channel and the bot is rendered as well.

### Web Chat
- Based on enterprise customers asks, we've added a [web chat sample](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/19.a.single-sign-on-for-enterprise-apps#single-sign-on-demo-for-enterprise-apps-using-oauth) that shows how to authorize a user to access resources on an enterprise app with a bot. Two types of resources are used to demonstrate the interoperability of OAuth with Microsoft Graph and GitHub API.

### Solutions
- [Virtual Assistant Solution Acclerator](https://github.com/Microsoft/botframework-solutions#readme) : Provides a set of templates, solution accelerators and skills to help build sophisticated conversational experiences. New Android app client for Virtual Assistant that integrates with Direct-Line Speech and Virtual Assistant demonstrating how a device client can interact with your Virtual Assistant and render Adaptive Cards. Updates also include support for Direct-Line Speech and Microsoft Teams.

- [Dynamics 365 Virtual Agent for Customer Service (public preview)](https://dynamics.microsoft.com/en-us/ai/virtual-agent-for-customer-service/): With the public preview, you can provide exceptional customer service with intelligent, adaptable virtual agents. Customer service experts can easily create and enhance bots with AI-driven insights.

- [Chat for Dynamics 365](https://www.powerobjects.com/powerpacks/powerchat/): Chat for Dynamics 365 offers several capabilities to ensure the support agents and end users can interact effectively and remain highly productive. Live chat and track conversations from visitors on your website within Microsoft Dynamics 365.

## What's new (May 2019)

|Item | C#  | JS  | Python | Java  |
|:----|:---:|:---:|:------:|:-----:|
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
|[**Analytics**](https://github.com/Microsoft/AI/blob/master/docs/readme.md#analytics)| Gain key insights into your bot's health and behavior with the Conversational AI Analytics solutions. Review available telemetry, sample Application Insights queries, and Power BI dashboards to understand the full breadth of your bot's conversations with users. |

## Azure Bot Service
Azure Bot Service enables you to host intelligent, enterprise-grade bots with complete ownership and control of your data.
Developers can register and connect their bots to users on Microsoft Teams, Web Chat,
and more. [Azure][27]  |  [docs][28] | [connect to channels][29]

* **Direct Line JS Client**: If you want to use the Direct Line channel in Azure Bot Service and are not using the WebChat client,
the Direct Line JS client can be used in your custom application. Go to [Github][30] for more information.

<a name="ABS-whats-new"></a>

* **New! Direct Line Speech Channel**: We are bringing together the Bot Framework and Microsoft's Speech Services to provide a channel that enables streamed speech and text bi-directionally from the client to the bot application.  For more information, see how to add [speech channel to your bot](directline-speech-bot.md).


## Bot Framework Emulator
The [Bot Framework Emulator][60] is a  cross-platform desktop application that allows bot developers to test and debug bots built using the Bot Framework SDK. You can use the Bot Framework Emulator to test bots running locally on your machine or to connect to bots running remotely.

- [Download latest][61] | [Docs][62]

<a name="Emulator-whats-new"></a>
### Bot Inspector (New! In preview)

The Bot Framework Emulator has released a beta of the new Bot Inspector feature. It provides a way to debug and test your
Bot Framework SDK v4 bots on channels like Microsoft Teams, Slack, Facebook Messenger,etc.
As you have the conversation, messages will be mirrored to the Bot Framework Emulator where you can inspect the
message data that the bot received. Additionally, a snapshot of the bot state for any given turn between the
channel and the bot is rendered as well. Read more about
[Bot Inspector](https://github.com/Microsoft/BotFramework-Emulator/blob/master/content/CHANNELS.md).


## Related Services

### Language Understanding
A machine learning-based service to build natural language experiences. Quickly create enterprise-ready, custom models that continuously improve. [Language Understanding Service(LUIS)][30] allows your application to understand what a person wants in their own words.

<a name="LUIS-whats-new"></a>

- **New! Roles, External Entities and Dynamic Entities**: LUIS has added several features that let developers extract more detailed information from text, so users can now build more intelligent solutions with less effort. LUIS also extended roles to all entity types, which allows the same entities to be classified with different subtypes based on context. Developers now have more granular control of what they can do with LUIS, including being able to identify and update models at runtime through dynamic lists and external entities. Dynamic lists are used to append to list entities at prediction time, permitting user-specific information to get matched exactly. Separate supplementary entity extractors are run with external entities, and that information can be appended to LUIS as strong signals for other models.

- **New! Analytics dashboard**: LUIS is releasing a more detailed, visually-rich comprehensive analytics dashboard. Its user-friendly design highlights common issues most users face when designing applications, by providing simple explanations on how to resolve them to help users gain more insight into their models' quality, potential data problems, and guidance to adopt best practices.

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
[5]:index.yml
[6]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore
[7]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs
[8]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/typescript_nodejs
[9]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_es6
[10]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_webapi
[11]:https://github.com/Microsoft/botbuilder-python/tree/master/samples
[11a]:https://aka.ms/python-sample-repo

[18]:https://github.com/Microsoft/botbuilder-tools/tree/master/packages/LUIS#readme
[19]:https://github.com/Microsoft/botbuilder-tools/tree/master/packages/QnAMaker#readme

[27]:https://azure.microsoft.com/services/bot-service/
[28]:bot-service-overview-introduction.md
[29]:bot-service-manage-channels.md

[30]:https://www.luis.ai
[31]:https://docs.microsoft.com/azure/cognitive-services/LUIS/Home
[32]:v4sdk/bot-builder-howto-v4-luis.md
[33]:https://www.qnamaker.ai/
[34]:/azure/cognitive-services/qnamaker/overview/overview
[35]:v4sdk/bot-builder-howto-qna.md

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
[62]:bot-service-debug-emulator.md

[100]:https://github.com/howdyai/botkit#readme
[101]:https://github.com/howdyai/botkit/blob/master/LICENSE.md
[102]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-slack#readme
[103]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-webex#readme
[104]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-hangouts#readme
[105]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-facebook#readme
[106]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-twilio-sms#readme
[107]:https://github.com/howdyai/botkit/tree/master/packages/botbuilder-adapter-web#readme
