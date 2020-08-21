---
title: What's new - Bot Service
description: Learn about improvements and new features in the August 2020 release of the Bot Framework SDK, including new functionality in Skills, Teams, and other areas.
keywords: bot framework, azure bot service
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 08/21/2020
monikerRange: 'azure-bot-service-4.0'
---

# What's new August 2020

[!INCLUDE[applies-to](includes/applies-to.md)]

The Bot Framework SDK v4 is an [Open Source SDK](https://github.com/microsoft/botframework-sdk/#readme) that enable developers to model and build sophisticated conversations using their favorite programming language.

This article summarizes key new features and improvements in Bot Framework and Azure Bot Service.

|   | C#  | JS  | Python |  Java
|---|:---:|:---:|:------:|:-----:
|Release |[4.10.0 (GA)][1] | [4.10.0 (GA)][2] | [4.10.0 (GA)][3] | [4.6 Preview][3a]
|Samples |[.NET Core][6], [WebAPI][10] |[Node.js][7], [TypeScript][8], [es6][9]  | [Python][11a] |

Welcome to the August 2020 release of the Bot Framework SDK. There are a number of updates in this version that we hope you will like; some of the key highlights include:

- [Documentation](#documentation): improvements to existing documentation, including READMEs for the code and samples repositories.
- [Customer supportability](#customer-supportability): improvements focused on developers seeking assistance using the Bot Framework, tools and SDKs.
- [Customer requests](#customer-requests): improvements focused on feature requests from the developer community and 3rd parties using the Bot Framework SDK and tools.
- [Code quality](#code-quality): improvements focused on unit and functional test coverage and on the reference documentation.
- [New SDK features](#new-sdk-features): new preview features added in this release.
- [Other improvements](#other-improvements): other improvements to the SDK.

**Insiders**: Want to try new features as soon as possible? You can download the nightly Insiders build [[C#](https://github.com/microsoft/botbuilder-dotnet/blob/main/UsingMyGet.md)] [[JS](https://github.com/microsoft/botbuilder-js/blob/main/UsingMyGet.md)] [[Python](https://github.com/microsoft/botbuilder-python/blob/main/UsingTestPyPI.md)] [[CLI](https://github.com/Microsoft/botframework-cli#nightly-builds)] and try the latest updates as soon as they are available. And for the latest Bot Framework news, updates, and content, follow us on Twitter @msbotframework!

## Documentation

Following feedback from customers and the Bot Framework Support team, a number of documents were created or updated. These are helpful towards providing answers and information relating to recurring issues from bot developers.

- Expanded code comment documentation in the SDK repositories.
- Improved READMEs in the samples and SDK repositories.
- New and updated documents addressing recurring bot developer issues:
  - Addition of an Azure Bot Service](/azure/bot-service/) hub page that links to both the Bot Framework Composer and the Bot Framework SDK documentation.
  - Updates to [language generation](v4sdk/bot-builder-concept-language-generation.md), [adaptive expressions](v4sdk/bot-builder-concept-adaptive-expressions.md), and [adaptive dialog](v4sdk/bot-builder-adaptive-dialog-introduction.md) articles.
  - Updates to the [Bot Framework Composer](/composer/) documentation.
  - Issue fixes and general documentation improvements to authentication, skills, channels, and other topics.

## Customer supportability

Developers using the Microsoft Bot Framework have many [resources](bot-service-resources-links-help.md) for getting help. Internal tools have been improved to increase the responsiveness of the engineering team in areas of most interest to developers.

- Creation of internal bots and improved tools for customer support.
- Improved analytics of trends in customer reported feature requests and issues.
- Coordination of labels across GitHub repositories.

## Customer requests

- Additional Teams channel lifecycle events.
- Improved Application Insights integration.
- Coordination of labels across GitHub repositories.
- Addition of a locale to the conversation update activity.
- Added alt-text support to card actions for images on buttons.
- Updated the skill handler to return a resource response object.
- Included support for the latest version of Azure Blobs storage.
- Improvements to the OAuth prompt dialog.
- Various bug fixes and telemetry improvements.

## Code quality

- Enforcing code style and format rules.
- Improved unit test code coverage and quality.
- Increased profiling of the code base.
- REST API Swagger files unified across SDK repositories. Introduced a version to the files.
- Added a settings object pattern for C# adapters.
- Added dependency policing in the JavaScript SDK.
- Added integration tests for Adaptive cards and Direct Line JavaScript.

## New SDK features

- [Orchestrator (preview)](https://github.com/microsoft/BotBuilder-Samples/blob/main/experimental/orchestrator/README.md): a transformer-based solution that runs locally with your bot to dispatch across one or more Bot Builder skills, LUIS applications, or QnA Maker knowledge bases.
- [Bot Builder Azure Queues (C# preview)](https://www.nuget.org/packages/Microsoft.Bot.Builder.Azure.Queues): better integration with Azure Queues and the _continue conversation later_ dialog.
- [Bot Builder Azure Blobs (C# preview)](https://www.nuget.org/packages/Microsoft.Bot.Builder.Azure.Blobs): new implementations of Azure Blob storage. This library is a replacement for the older Azure Blog storage implementation.

## Other improvements

- Continued improvements to Microsoft Teams API support.
- Bot Framework CLI tools added `lg` as a core plugin and included other overall tool improvements.
- Updated the READMEs in the samples and added new Teams Typescript samples.
- Composer improved support for skills and improved integration for Cognitive Services.
- Web Chat added many accessibility improvements.
- Emulator added bug fixes and updates.

[1]:https://github.com/Microsoft/botbuilder-dotnet/#packages
[2]:https://github.com/Microsoft/botbuilder-js#packages
[3]:https://github.com/Microsoft/botbuilder-python#packages
[3a]:https://github.com/Microsoft/botbuilder-java#packages
[6]:https://github.com/Microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore
[7]:https://github.com/Microsoft/BotBuilder-Samples/tree/main/samples/javascript_nodejs
[8]:https://github.com/Microsoft/BotBuilder-Samples/tree/main/samples/typescript_nodejs
[9]:https://github.com/Microsoft/BotBuilder-Samples/tree/main/samples/javascript_es6
[10]:https://github.com/Microsoft/BotBuilder-Samples/tree/main/samples/csharp_webapi
[11a]:https://aka.ms/python-sample-repo

## Additional information

You can see previous announcements [here](what-is-new-archive.md).
