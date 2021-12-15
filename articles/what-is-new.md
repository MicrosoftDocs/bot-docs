---
title: What's new in the Bot Service SDKs for C#, Java, JavaScript, and Python
description: Learn about improvements and new features in the July 2021 release of the Bot Framework SDK for C#, Java, JavaScript, and Python.
keywords: bot framework, azure bot service
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: conceptual
ms.date: 07/23/2021
ms.custom: tab-zone-seo
---

# What's new for the Bot Framework SDKs

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

The Bot Framework SDK v4.14 is an [Open Source SDK](https://github.com/microsoft/botframework-sdk/#readme) that enables developers to model and build sophisticated conversations using C#, Java, JavaScript, or Python.

This article summarizes key new features and improvements in Bot Framework and Azure Bot Service.

## Insiders

Want to try new features as soon as possible? You can download the nightly Insiders build [[C#](https://github.com/microsoft/botbuilder-dotnet/blob/main/dailyBuilds.md)] [[JS](https://github.com/microsoft/botbuilder-js/blob/main/dailyBuilds.md)] [[Python](https://github.com/microsoft/botbuilder-python/blob/master/UsingTestPyPI.md)] [[CLI](https://github.com/Microsoft/botframework-cli#nightly-builds)] and try the latest updates as soon as they are available. And for the latest Bot Framework news, updates, and content, follow us on Twitter @msbotframework!

## July 2021 - v4.14

Welcome to the July 2021 release of the Bot Framework SDK. Here are the key highlights for this release.

| Item    | C#                           | JS                                      | Python         | Java            |
|:--------|:----------------------------:|:---------------------------------------:|:--------------:|:---------------:|
| Release | [4.14 (GA)][1]               | [4.14 (GA)][2]                          | [4.14 (GA)][3] | [4.14 (GA)][3a] |
| Samples | [.NET Core][6], [WebAPI][10] | [Node.js][7], [TypeScript][8], [es6][9] | [Python][11a]  | [Java][12]      |

- Added support for `Action.Execute` in Adaptive Cards.
- Added support for the Outlook channel. For more information, see [Connect a bot to the Outlook channel for Actionable Messages (Preview)](bot-service-channel-connect-actionable-email.md).
- Added Microsoft Teams activity handler support for meeting start and end events. For more information, see [How Microsoft Teams bots work](v4sdk/bot-builder-basics-teams.md).
- Added a _get meeting info_ method to the _Teams info_ object.
- Added information for v2.2 of the skill manifest schema. For more information, see how to [Write a skill manifest](v4sdk/skills-write-manifest.md).
- Various bug fixes and quality improvements in the docs and the SDK.

For more information, see [Bot Framework SDK v.4.14 release notes](https://github.com/microsoft/botframework-sdk/releases/).

## May 2021 - v4.13

Welcome to the May 2021 release of the Bot Framework SDK. Some of the key highlights include:

- The Orchestrator recognizer has been released to general availability (GA). See how to [Use multiple LUIS and QnA models with Orchestrator](v4sdk/bot-builder-tutorial-orchestrator.md) for more information.
- Added support for the new  `command` and `commandResult` activities that support requests to perform specific actions. See the [Bot Framework Activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md) for more about these activities.
- Added C# and JavaScript support for component registration for the [Bot Framework Composer](/composer). See the documentation for the 2.0 release of Composer for more information.
- The cloud adapter is GA in C#, and various skill classes were updated to use the cloud adapter's authentication abstractions.
- Single-sign-on middleware was introduced for Microsoft Teams.

For more information, see [Bot Framework SDK v4.13 release notes](https://github.com/microsoft/botframework-sdk/releases/tag/4.13.0).

## March 2021 - v4.12

Welcome to the March 2021 release of the Bot Framework SDK. Some of the key highlights include:

- The Telephony channel is now available with samples in early preview.
- Microsoft Teams - new and improved samples, Adaptive Card tabs, Action.Execute (preview, C#) and Composer support (preview).
- The cloud adapter (preview 2, C#) has improved platform support with increased functionality.
- Orchestrator (preview 3) now supports more languages, and documentation has been improved.
- Bot Framework CLI Tools - LUIS applications neural training technology support, and more!
- Azure Health Bot - Microsoft Healthcare Bot service is moving to Azure, further empowering organizations to benefit from Azure's enhanced tooling, security, and compliance offerings.
- Power Virtual Agents - Bot creation, editing and publishing made easy!

For more information, see [Bot Framework SDK v4.12 release notes](https://github.com/microsoft/botframework-sdk/releases/tag/4.12.0).

## November 2020 - v4.11

Welcome to the November 2020 release of the Bot Framework SDK. There are a number of updates in this version that we hope you'll enjoy:

- Teams: added support for the Participant Meeting API, and other general improvements.
- Skills: can now be run and tested locally in the Emulator without an app ID and password, improved support for skills in adaptive dialogs.
- Orchestrator (preview): a Language Understanding technology for routing incoming user utterances to an appropriate skill or to subsequent language processing service such as LUIS or QnA Maker.
- Cloud adapter (preview, .NET only): a bot adapter that supports hosting a bot in any cloud environment.

For more information, see [Bot Framework SDK v4.11 release notes](https://github.com/microsoft/botframework-sdk/releases/tag/4.11.0).

### Documentation updates

Following feedback from customers and the Bot Framework Support team, a number of documents were created or updated. These are helpful towards providing answers and information relating to recurring issues from bot developers.

- Expanded code comment documentation in the SDK repositories.
- Improved `README` files in the samples and SDK repositories.
- New and updated documents addressing recurring bot developer issues:
  - Updated and expanded the [conceptual](v4sdk/bot-builder-adaptive-dialog-Introduction.md) and [how-to](v4sdk/bot-builder-adaptive-dialog-setup.md) articles for adaptive dialogs.
  - Updated and reorganized the [authentication and security](v4sdk/bot-builder-authentication-basics.md) articles.
  - Updates to the [Bot Framework Composer](/composer/) documentation.
  - Issue fixes and general documentation improvements to response generation, Cognitive Services, adaptive expressions, skills, channels, and other topics.
  - The SDK v3 documentation has been removed from the main doc set and is available on the [previous versions](/previous-versions) site.

## August 2020 - v4.10

Welcome to the August 2020 release of the Bot Framework SDK. There are a number of updates in this version that we hope you'll:

- [Customer supportability](#customer-supportability): improvements focused on developers seeking assistance using the Bot Framework, tools and SDKs.
- [Customer requests](#customer-requests): improvements focused on feature requests from the developer community and 3rd parties using the Bot Framework SDK and tools.
- [Code quality](#code-quality): improvements focused on unit and functional test coverage and on the reference documentation.
- [New SDK features](#new-sdk-features): new preview features added in this release.
- [Other improvements](#other-improvements): other improvements to the SDK.

For more information, see [Bot Framework SDK v4.10 release notes](https://github.com/microsoft/botframework-sdk/releases/tag/4.10).

### Documentation

Following feedback from customers and the Bot Framework Support team, a number of documents were created or updated. These are helpful towards providing answers and information relating to recurring issues from bot developers.

- Expanded code comment documentation in the SDK repositories.
- Improved `README` files in the samples and SDK repositories.
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
- Updated the `README` files in the samples and added new Teams Typescript samples.
- Composer improved support for skills and improved integration for Cognitive Services.
- Web Chat added many accessibility improvements.
- Emulator added bug fixes and updates.

## Additional information

For complete release notes for all Bot Framework SDK releases, see [release notes on GitHub](https://github.com/microsoft/botframework-sdk/releases).

[1]: https://github.com/Microsoft/botbuilder-dotnet/#packages
[2]: https://github.com/Microsoft/botbuilder-js#packages
[3]: https://github.com/Microsoft/botbuilder-python#packages
[3a]: https://github.com/Microsoft/botbuilder-java#packages
[5]: index.yml
[6]: https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore
[7]: https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs
[8]: https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/typescript_nodejs
[9]: https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_es6
[10]: https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_webapi
[11a]: https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python
[12]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot
