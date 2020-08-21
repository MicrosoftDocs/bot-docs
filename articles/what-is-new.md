---
title: What's new - Bot Service
description: Learn about improvements and new features in the May 2020 release of the Bot Framework SDK, including new functionality in Skills, Teams, and other areas.
keywords: bot framework, azure bot service
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 05/18/2020
monikerRange: 'azure-bot-service-4.0'
---

# What's new May 2020

[!INCLUDE[applies-to](includes/applies-to.md)]

The Bot Framework SDK v4 is an [Open Source SDK](https://github.com/microsoft/botframework-sdk/#readme) that enable developers to model and build sophisticated conversation 
using their favorite programming language.

This article summarizes key new features and improvements in Bot Framework and Azure Bot Service.

|   | C#  | JS  | Python |  Java | 
|---|:---:|:---:|:------:|:-----:|
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

## Skills 
[Skills](v4sdk/skills-conceptual.md) have been updated to work with adaptive dialogs, and both adaptive and traditional dialogs will now accept all types of activities.
 
The skill manifest schema has been updated to [version 2.1](https://github.com/microsoft/botframework-sdk/tree/master/schemas/skills). Improvements in this version include the ability to declare & share your language models, and define any type of activity that your skill can receive.
 
This release also includes authentication improvements with skills, including using SSO with dialogs, and OAuth without needing a magic code in WebChat and DirectLine.

## Microsoft Teams
We continue to focus on making sure all the Teams-specific APIs are fully supported across our SDKs. This release brings full support for Microsoft Teams APIs in the preview [Java SDK](https://github.com/microsoft/botbuilder-java), including [samples](https://github.com/microsoft/botbuilder-java/tree/master/samples).
 
The `OnTeamsMemberAdded` event in the activity handler has been updated to use the get single member endpoint under the covers, which should significantly reduce latency and reliability of this event in large teams.
 
The `TeamsChannelAccount` object has been updated to include `userRole` (one of owner, member, or guest) and `tenantId` (for the user's tenantId).

## Bot Telemetry 
Bots now capture Page View events, native to Application Insights, whenever a dialog is started. This allows you to use the User Flows dashboard in Application Insights to see how users move through your bot, between dialogs and where they drop out.

![Telemetry In AppInsights](https://raw.githubusercontent.com/microsoft/botframework-sdk/master/docs/media/UserFlowsAppInsights.jpg?raw=true)

## Adaptive Dialogs 
We’re also excited to make [Adaptive Dialogs](v4sdk/bot-builder-adaptive-dialog-introduction.md) generally available in C# and as a preview release in JavaScript! 

Adaptive Dialogs, which underpin the dialog design and management authoring features found in Bot Framework Composer, enable developers to dynamically update conversation flow based on context and events. This is especially useful when dealing with more sophisticated conversation requirements, such as context switches and interruptions.  Bot Framework Skills can now also leverage Adaptive Dialogs.

Adaptive Dialogs also now support Telemetry. Data from Adaptive Dialogs, including  triggers, actions and recognizers now flow into your Azure Application Insights instance.

## CLI tools for Adaptive Dialogs
[CLI tools](https://github.com/Microsoft/botframework-cli) for Adaptive Dialogs, Language Generation, QnaMaker and Luis Cross-train - new ability to merge and validate adaptive schema assets, augment qna and lu files, create/ update/ replace/ train/ publish LUIS and or QnA maker application and Language Generation templates manipulation.

New CLI Tools were added for management of Adaptive Dialogs.
- [bf-dialog](https://github.com/microsoft/botframework-cli/tree/master/packages/dialog#relevant-docs) supports merging dialog schema files and verify file format correctness.
- [bf-luis](https://github.com/microsoft/botframework-cli/tree/master/packages/luis#relevant-docs) Adds commands to augment lu files and create/ update/ replace/ train/ publish LUIS
- [bf-qnamaker](https://github.com/microsoft/botframework-cli/tree/master/packages/qnamaker#relevant-docs) Adds commands to augment qna files and create/ update/ replace/ train/ publish QnAMaker
- [bf-lg](https://github.com/microsoft/botframework-cli/tree/master/packages/lg#relevant-docs) Parse, collate, expand and translate lg files.

## Language Generation

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

## Adaptive Expressions
[Adaptive Expressions](v4sdk/bot-builder-concept-adaptive-expressions.md) are Generally Available (GA) on both the C# and JS Platforms. 

Bots use expressions to evaluate the outcome of a condition based on runtime information available in memory to the dialog or the Language Generation system. These evaluations determine how your bot reacts to user input and other factors that impact bot functionality.

Adaptive expressions were created to address this core need as well as provide an adaptive expression language that can used with the Bot Framework SDK and other conversational AI components, like [Bot Framework Composer](https:/docs.microsoft.com/composer/), Language Generation, Adaptive dialogs, and [Adaptive Cards](https://docs.microsoft.com/adaptive-cards/).

An adaptive expression can contain one or more explicit values, pre-built functions or custom functions. Consumers of adaptive expressions also have the capability to inject additional supported functions. For example, all Language Generation templates are available as functions as well as additional functions that are only available within that component's use of adaptive expressions.

## Authentication Improvements
We added support for single sign-on while using Expect Replies. This applies to SSO performed between a pair of bots: host and a skill.

For Bot Identification we've added the ability to specify `sendx5c` parameter for certificate authentication. This feature was requested by customers and allows for more flexibility when using cert auth. 

Additional Sovereign Clouds are supported.

## Generated Dialogs - Early Preview

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

## VS Code Debugger - Early Preview
[Adaptive tools](https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-tool) is a brand new Visual studio code extension you can use to create/ validate .lu and .lg documents as well as debug declaratively defined adaptive dialogs. This extension provides rich authoring & editing capabilities for .lu and .lg file formats including syntax highlighting, auto-suggest and auto-complete.

We anticipate adding an early preview to the VS Marketplace shortly after this release. 

## Bot Builder Community
During this release, the Bot Builder Community has further raised the bar by adding more features, more adapters, and fixing more bugs.

1. A revised C# [Alexa Adapter](https://github.com/BotBuilderCommunity/botbuilder-community-dotnet/tree/develop/libraries/Bot.Builder.Community.Adapters.Alexa) and [Google Home Adapter]() Re-built from the ground up, starting with Alexa, to allow the adapters to be consumed by Azure Bot Service and made available as channels. Improvements include better native activity type mapping, improved markdown rendering and support for more complex scenarios (such as merging multiple outgoing activities).

2. A new C# [Zoom Adapter](https://github.com/BotBuilderCommunity/botbuilder-community-dotnet/tree/develop/libraries/Bot.Builder.Community.Adapters.Zoom) that supports Zoom 1:1 and channel chat capabilities and converts them to native BF activity types. With it, you can subscribe to any event that Zoom supports, with full support for Zoom interactive messages and rich message templates. (The adapter translates Zoom events into BF event activities.)

3. A [RingCentral Adapter](https://github.com/BotBuilderCommunity/botbuilder-community-dotnet/tree/develop/libraries/Bot.Builder.Community.Adapters.RingCentral). The [RingCentral](https://www.ringcentral.com/) Engage adapter allows you to add an additional endpoint to your bot for [RingCentral Engage Digital Platform](https://www.ringcentral.com/digital-customer-engagement.html) integration. The RingCentral endpoint can be used in conjunction with other channels meaning, for example, you can have a bot exposed on out of the box channels such as Facebook and Teams, but also integrated as an [RingCentral Engage Digital Source SDK](https://support.ringcentral.com/s/article/RingCentral-Engage-Digital-Introduction?language=en_US) into RingCentral.

## Bot Framework Composer

[Bot Framework Composer](https://docs.microsoft.com/composer/) is Generally Available (GA) on the [Windows](https://aka.ms/bf-composer-download-win) | [macOS](https://aka.ms/bf-composer-download-mac) | [Linux](https://aka.ms/bf-composer-download-linux) platforms.

Bot Framework Composer is a visual authoring canvas for developers and multi-disciplinary teams to build bots. It is an open source conversational application based on the Microsoft Bot Framework SDK. Within Composer, you will find everything you need to build a sophisticated conversational experience:

* A visual editing canvas for conversation flow.
* In-context editing for language understanding.
* Tools to train and manage language understanding (such as LUIS and QnA Maker) components.
* Powerful language generation and templating systems.
* A ready-to-use bot run-time executable.

[1]:https://github.com/Microsoft/botbuilder-dotnet/#packages
[2]:https://github.com/Microsoft/botbuilder-js#packages
[3]:https://github.com/Microsoft/botbuilder-python#packages
[3a]:https://github.com/Microsoft/botbuilder-java#packages
[6]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore
[7]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs
[8]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/typescript_nodejs
[9]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_es6
[10]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_webapi
[11a]:https://aka.ms/python-sample-repo

## Additional information
- You can see previous announcements [here](what-is-new-archive.md).
