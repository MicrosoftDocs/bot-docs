---
title: What's new - Bot Service
description: Learn about improvements and new features in the August 2020 release of the Bot Framework SDK, including new functionality in Skills, Teams, and other areas.
keywords: bot framework, azure bot service
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 05/17/2021
---

# What's new May 2021

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

The Bot Framework SDK v4 is an [Open Source SDK](https://github.com/microsoft/botframework-sdk/#readme) that enable developers to model and build sophisticated conversations using their favorite programming language.

This article summarizes key new features and improvements in Bot Framework and Azure Bot Service.

| Item    | C#                           | JS                                      | Python         | Java            |
|:--------|:----------------------------:|:---------------------------------------:|:--------------:|:---------------:|
| Release | [4.13 (GA)][1]               | [4.13 (GA)][2]                          | [4.13 (GA)][3] | [4.13 (GA)][3a] |
| Samples | [.NET Core][6], [WebAPI][10] | [Node.js][7], [TypeScript][8], [es6][9] | [Python][11a]  | [Java][12]      |

Welcome to the May 2021 release of the Bot Framework SDK. Some of the key highlights include:

- The Orchestrator recognizer has been released to general availability (GA). See how to [Use multiple LUIS and QnA models with Orchestrator](v4sdk/bot-builder-tutorial-orchestrator.md) for more information.
- Added support for the new  `command` and `commandResult` activities that support requests to perform specific actions. See the [Bot Framework Activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md) for more about these activities.
- Added C# and JavaScript support for component registration for the [Bot Framework Composer](/composer). See the documentation for the 2.0 release of Composer for more information.
- The cloud adapter is GA in C#, and various skill classes were updated to use the cloud adapter's authentication abstractions.
- Single-sign-on middleware was introduced for Microsoft Teams.

See the Bot Framework SDK [release notes](https://github.com/microsoft/botframework-sdk/releases/) for more information about the changes made to the SDK in the 4.13 release.

**Insiders**: Want to try new features as soon as possible? You can download the nightly Insiders build [[C#](https://github.com/microsoft/botbuilder-dotnet/blob/main/dailyBuilds.md)] [[JS](https://github.com/microsoft/botbuilder-js/blob/main/dailyBuilds.md)] [[Python](https://github.com/microsoft/botbuilder-python/blob/master/UsingTestPyPI.md)] [[CLI](https://github.com/Microsoft/botframework-cli#nightly-builds)] and try the latest updates as soon as they are available. And for the latest Bot Framework news, updates, and content, follow us on Twitter @msbotframework!

## Additional information

You can see previous announcements in the [archived information](what-is-new-archive.md).

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
[11a]: https://aka.ms/python-sample-repo
[12]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot
