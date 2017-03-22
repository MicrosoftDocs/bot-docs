---
title: Azure Bot Service overview | Microsoft Docs
description: Learn about Azure Bot Service.
keywords: Bot Framework, Azure Bot Service, Overview
author: Toney001
manager: rstand
ms.topic: bot-service-article
ms.prod: botframework
ms.service: Azure Bot Service
ms.date: 3/20/2017
ms.reviewer: 
#ROBOTS: Index

---

# Overview

## Introduction

Use the Azure Bot Service to accelerate bot development by working in an integrated environment that’s purpose-built for bot development. This integrated environment lets you build, connect, deploy and manage intelligent bots that interact naturally wherever your users are talking, whether it's with an app or website, Text/SMS, Skype, Slack, Facebook Messenger, Kik, Office 365 mail, and other popular services. 

Azure Bot Service is powered by Microsoft Bot Framework. By using <a href="https://docs.microsoft.com/en-us/azure/azure-functions/">Azure Functions</a>, your bot will run in a serverless environment on Azure that will scale based on demand. You can also increase the value of your bots with a few lines of code by plugging into <a href="https://www.microsoft.com/cognitive-services/en-US/sign-up?ReturnUrl=/cognitive-services/en-us/subscriptions" target="_blank">Cognitive Services</a> to enable your bots to see, hear, interpret and interact in more human ways.

Using the Azure editor, you can write your bot in C# or Node.js without any need for a tool chain (local editor and source control). The integrated chat window sits side-by-side with the Azure editor so that you can test your bot on the fly as you write the code in the browser.

The Azure editor does not allow you to manage the files by adding new files or renaming or deleting existing files. If you need to manage the files, you should set up [continuous integration](bot-framework-azure-continuousintegration.md), which would let you use the IDE and source control of your choice (for example, Visual Studio Team, GitHub, and Bitbucket). Continuous integration will automatically deploy to Azure the changes that you commit to source control. Once you have setup continuous integration, you can [debug your bot locally](bot-framework-azure-debug.md).

> [!NOTE]
> After configuring continuous integration, you will no longer be able to update the bot in the Azure editor.

## Get sample bots
The following are the Bot App templates that you can create.

* Basic—A simple bot that uses dialogs to respond to user input. Read [Create a bot with the Azure Bot Service](bot-framework-azure-basicbot.md) to learn how to build a basic bot.
* Form—A bot that uses a guided conversation to collect user input. The C# template uses FormFlow to collect user input, and the Node.js template uses waterfalls to collect user input. See  [Form bot template](bot-framework-azure-formbot.md).
* Language understanding—A bot that uses natural language models (LUIS) to understand user intent. See [Natural langugae bot template](bot-framework-azure-naturallanguagebot.md).
* Proactive—A bot that uses Azure Functions to alert bot users of events. See [Proactive bot template](bot-framework-azure-proactivebot.md).
* Question and answer—A bot that creates question and answer pairs based upon a link you supply to your company's FAQ. See [Question and Answer template](bot-framework-azure-questionandanswerbot.md).


Azure Bot Service is powered by the serveless infrastructure of Azure Functions, and it shares its runtime concepts, which you should become familiar with.

All Azure Bot Service bots include the following files, depending on whether you want to use .NET or Node.js.

### .NET \#

| File | Description |
|------|-------------|
| Bot.sln | The Microsoft Visual Studio solutions file. Used locally if you set up [continuous integration](bot-framework-azure-continuousintegration.md). |
|Commands.json | This file contains the commands that start debughost in Task Runner Explorer when you open the Bot.sln file. If you don't install Task Runner Explorer, you can delete this file.
| debughost.cmd | This file contains the commands to load and run your bot. Used locally if you set up continuous integration and want to debug your bot locally. For more information, see Debugging C# bots built using the Azure Bot Service on Windows. The file also contains your app ID and password. You would set the ID and password if you want to debug authentication. If you set these, you must provide the ID and password in the emulator, too. |
| function.json | This file contains the function’s bindings. You should not modify this file. |
| host.json | A metadata file that contains the global configuration options affecting the function. |
| project.json | This file contains the project’s NuGet references. You should only have to change this file if you add a new reference. |
| readme.md | This file contains notes that you should read before using or modifying the bot. |


###Node.js

| File | Description |
|------|-------------|
| function.json | This file contains the function’s bindings. You should not modify this file. |
| host.json | A metadata file that contains the global configuration options affecting the function |
| package.json | This file contains the project’s NuGet references. You should only have to change this file if you add a new reference. |

## Next steps

Learn more about building bots using Azure Bot Service by
reviewing articles and tutorials throughout this section.

Read [Get Started](bot-framework-azure-getstarted.md) so that you can quickly build and test a simple bot by following instructions in this step-by-step tutorial.

If you encounter problems or have suggestions regarding Azure Bot Service, 
see [Support](bot-framework-resources-support.md) for a list of available resources. 
