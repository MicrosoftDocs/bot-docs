---
title: What's new - Bot Service
description: Learn about improvements and new features in the August 2020 release of the Bot Framework SDK, including new functionality in Skills, Teams, and other areas.
keywords: bot framework, azure bot service
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 11/13/2020
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

## Additional information

- You can see previous announcements in the [archived information](what-is-new-archive.md).
- See the Bot Framework SDK [release notes](https://github.com/microsoft/botframework-sdk/releases/) for more information about the changes made to the SDK in the 4.11 release.

[1]:https://github.com/Microsoft/botbuilder-dotnet/#packages
[2]:https://github.com/Microsoft/botbuilder-js#packages
[3]:https://github.com/Microsoft/botbuilder-python#packages
[3a]:https://github.com/Microsoft/botbuilder-java#packages
[5]:index.yml
[6]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore
[7]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs
[8]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/typescript_nodejs
[9]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_es6
[10]:https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_webapi
[11a]:https://aka.ms/python-sample-repo
