---
title: Overview of Bot templates for Azure Bot Service | Microsoft Docs
description: Learn about Bot templates for Azure Bot Service.
keywords: Bot Framework, Azure Bot Service, continuous integration
author: Toney001
manager: rstand
ms.topic: bot-service-article
ms.prod: botframework
ms.service: Azure Bot Service
ms.date: 3/21/2017
ms.reviewer:
#ROBOTS: Index
---

#Overview of templates

Azure Bot Service is powered by the serveless infrastructure of Azure Functions, and it shares its <a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-reference" target="_blank">runtime concepts</a>, which you should become familiar with.

The Azure editor does not allow you to manage the files by adding new files or renaming or deleting existing files. If you need to manage the files, you should set up [continuous integration](bot-framework-azure-continuous-integration.md), which would let you use the IDE and source control of your choice (for example, Visual Studio Team, GitHub, and Bitbucket). Continuous integration will automatically deploy to Azure the changes that you commit to source control. Once you have setup continuous integration, you can [debug your bot locally](bot-framework-azure-debug.md).

> [!NOTE]
> After configuring continuous integration, you will no longer be able to update the bot in the Azure editor.

The following are the Bot App templates that you can create.

* Basic bot—A simple bot that uses dialogs to respond to user input. Read [Create a bot with the Azure Bot Service](bot-framework-azure-basic-bot.md) to learn how to build a basic bot.
* Form bot—A bot that uses a guided conversation to collect user input. The C# template uses FormFlow to collect user input, and the Node.js template uses waterfalls to collect user input. See  [Form bot template](bot-framework-azure-form-bot.md).
* Language understanding bot—A bot that uses natural language models (LUIS) to understand user intent. See [Natural langugae bot template](bot-framework-azure-natural-language-bot.md).
* Proactive bot—A bot that uses Azure Functions to alert bot users of events. See [Proactive bot template](bot-framework-azure-proactive-bot.md).
* Question and answer bot—A bot that creates question and answer pairs based upon a link you supply to your company's FAQ. See [Question and Answer template](bot-framework-azure-question-and-answer-bot.md).

## Next steps

Learn more about building bots using Azure Bot Service by
reviewing articles and tutorials throughout this section.

- Read [Get Started](bot-framework-azure-getstarted.md) so that you can quickly build and test a simple bot by following instructions in this step-by-step tutorial.

- If you encounter problems or have suggestions regarding Azure Bot Service, 
see [Support](bot-framework-resources-support.md) for a list of available resources. 

