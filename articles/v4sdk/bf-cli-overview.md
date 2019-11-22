---
title: Azure Bot Framework Command-Line Interface (CLI) overview | Microsoft Docs
description: Learn about the Bot Framework Command-Line Interface (CLI).
keywords: Bot Framework Command-Line Interface, Bot Framework CLI
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 11/01/2019
monikerRange: 'azure-bot-service-4.0'
---

<!--TODO:
- [?] Add to TOC: Reference/Bot Framework CLI/Reference
- [?] Add other topics to the same node for each of the command groups
-->
# Bot Framework CLI overview

[!INCLUDE[applies-to](../includes/applies-to.md)]

Bot Framework Command-Line Interface (CLI) is a cross-platform tool that allows you to manage bots and related services. It replaces a collection of older standalone CLI tools by aggregating them into a single tool. 

## Prerequisites

* [Node.js](https://nodejs.org/), version 10.14.1 or later.

## Installation

Install the BF CLI from the command line.

~~~cmd
npm i -g @microsoft/botframework-cli
~~~

## Available commands

The following commands are currently available.

| Old tool | BF command set | Description |
| :--- | :--- | :--- |
| ChatDown | [`bf chatdown`](bf-cli-reference.md#bf-chatdown) | Commands for working with chat dialog (**.chat**) files. |
| na | [`bf config`](bf-cli-reference.md#bf-config) | Configures various settings within the CLI. |
| LuDown, LuisGen | [`bf luis`](bf-cli-reference.md#bf-luis) | Commands for working with LUIS resource files and managing LUIS models. |
| QnAMaker | [`bf qnamaker`](bf-cli-reference.md#bf-qnamaker) | Commands for working with QnA Maker resource files and managing knowledge bases. |

The following tools will be ported in upcoming releases:
- LUIS (API)
- Dispatch

See [Porting Map](https://github.com/microsoft/botframework-cli/blob/master/PortingMap.md) for a mapping reference between the old and new tools.

_Note: The older CLI tools will be deprecated in upcoming releases and support for them will end in the future.
All new investments, bug fixes, and new features in this area will target only the BF CLI._

## Overview

The BF CLI manages bots and related services. It is part of the Microsoft Bot Framework, a comprehensive framework for building enterprise-grade conversational AI experiences. In addition to managing bot related resources, the BF CLI can be used as part of continuous integration and continuous deployment (CI/CD) pipelines. As you build your bot, you might also need to integrate AI services like LUIS for language understanding, QnAMaker for your bot to respond to simple questions in a Q&A format, and more. To integrate AI service in your bot, use:

* [`bf luis`](bf-cli-reference.md#bf-luis) command to work with LUIS **.lu** resource files and manage LUIS models. It can also generate corresponding source (C# or JavaScript) code.
* [LUIS APIs tool](https://github.com/microsoft/botbuilder-tools/tree/master/packages/LUIS/readme.md) to deploy the local files, train, test, and publish them as Language Understanding models within the LUIS service.
* [`bf qnamaker`](bf-cli-reference.md#bf-qnamaker) command to work with QnAMaker knowledge bases. It can create and manage QnA Maker assets both locally, and on the QnA Maker service.

* Refer to the lu library [documentation](https://github.com/microsoft/botframework-cli/tree/master/packages/lu/README.md) for how to work with **.lu** and **.qna** file formats.

As your bot becomes more sophisticated, use the [Dispatch](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/Dispatch) CLI tool to create, evaluate, and dispatch intent across multiple LUIS models and QnA Maker knowledge bases.

To test and refine your bot, you can use the new [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/releases). The Emulator enables you to test and debug your bots on local machine or in the cloud.

During early design stages you might want to create mock conversations between the user and the bot for the specific scenarios your bot will support. Use [`bf chatdown`](bf-cli-reference.md#bf-chatdown) command to author conversation mockup **.chat** files, convert them into rich transcripts, and view the conversations in the the Emulator.

Lastly, with the [Azure CLI](https://github.com/microsoft/botframework-cli/blob/master/AzureCli.md) (`az bot` command), you can create, download, publish, and configure channels with the [Azure Bot Service](https://azure.microsoft.com/services/bot-service/). It is a plugin that extends the functionality of the [Azure CLI](https://docs.microsoft.com/cli/azure/install-azure-cli?view=azure-cli-latest) to manage your Azure Bot Service assets.

## Privacy and instrumentation
BF CLI contains instrumentation options that are designed to help us improve the tool based on **anonymous** usage patterns. __It is disabled, opted-out by default__. If you elect to opt-in, Microsoft gathers some usage data:

* Command group calls
* Flags used, **excluding** specific values. For example, for the parameter `--folder:name`, only the use of `--folder` is gathered, the folder name is not gathered.

To modify data collection behavior, use the [`bf config`](bf-cli-reference.md#bf-config) command.

Please refer to [Microsoft Privacy Statement](https://privacy.microsoft.com/privacystatement) for more details.  

## Issues and feature requests
- You can file issues and feature requests [here](https://github.com/microsoft/botframework-cli/issues).
- You can find known issues [here](https://github.com/microsoft/botframework-cli/labels/known-issues).

## Next steps
- [BF cli reference](bf-cli-reference.md)
