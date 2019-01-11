---
title: Manage resources with a .bot file | Microsoft Docs
description: Describes the purpose and use of bot file.
keywords: bot file, .bot, .bot file, msbot, bot resources, manage bot resources
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 11/23/2018
monikerRange: 'azure-bot-service-4.0'
---

# Manage resources with a .bot file

Bots usually consume lots of different services, such as [LUIS.ai](https://luis.ai) or [QnaMaker.ai](https://qnamaker.ai). When you are developing a bot, there is no uniform place to store the metadata about the services that are in use.  This prevents us from building tooling that looks at a bot as a whole.

To address this problem, we have created a **.bot file** to act as the place to bring all service references together in one place to 
enable tooling.  For example, the Bot Framework Emulator ([V4](https://aka.ms/Emulator-wiki-getting-started)) uses a  .bot file to create a unified view over the connected services your bot consumes.  

With a .bot file, you can register services like:

* **Localhost** local debugger endpoints
* [**Azure Bot Service**](https://azure.microsoft.com/en-us/services/bot-service/) Azure Bot Service registrations.
* [**LUIS.AI**](https://www.luis.ai/) LUIS gives your bot the ability to communicate with people using natural language.. 
* [**QnA Maker**](https://qnamaker.ai/) Build, train and publish a simple question and answer bot based on FAQ URLs, structured documents or editorial content in minutes.
* [**Dispatch**](https://github.com/Microsoft/botbuilder-tools/tree/master/Dispatch) models for dispatching across multiple services.
* [**Azure Application Insights**](https://azure.microsoft.com/en-us/services/application-insights/) for insights and bot analytics.
* [**Azure Blob Storage**](https://azure.microsoft.com/en-us/services/storage/blobs/) for bot state persistence. 
* [**Azure Cosmos DB**](https://azure.microsoft.com/en-us/services/cosmos-db/) - globally distributed, multi-model database service to persist bot state.

Apart from these, your bot might rely on other custom services. You can leverage the [generic service](https://github.com/Microsoft/botbuilder-tools/blob/master/packages/MSBot/docs/add-services.md) capability to connect a generic service configuration.

## When is a .bot file created? 
- If you create a bot using [Azure Bot Service](https://ms.portal.azure.com/#blade/Microsoft_Azure_Marketplace/GalleryResultsListBlade/selectedSubMenuItemId/%7B%22menuItemId%22%3A%22gallery%2FCognitiveServices_MP%2FBotService%22%2C%22resourceGroupId%22%3A%22%22%2C%22resourceGroupLocation%22%3A%22%22%2C%22dontDiscardJourney%22%3Afalse%2C%22launchingContext%22%3A%7B%22source%22%3A%5B%22GalleryFeaturedMenuItemPart%22%5D%2C%22menuItemId%22%3A%22CognitiveServices_MP%22%2C%22subMenuItemId%22%3A%22BotService%22%7D%7D), a .bot file is automatically created for you with list of connected services provisioned. The .bot is encrypted by default.
- If you create a bot using Bot Framework V4 SDK [Template](https://marketplace.visualstudio.com/items?itemName=BotBuilder.botbuilderv4) for Visual Studio or using Bot Builder [Yeoman Generator](https://www.npmjs.com/package/generator-botbuilder), a .bot file is automatically created. No connected services are provisioned in this flow and the bot file is not encrypted.
- If you are starting with [BotBuilder-samples](https://github.com/Microsoft/botbuilder-samples), every sample for Bot Framework V4 SDK includes a .bot file and the .bot file is not encrypted. 
- You can also create a bot file using the [MSBot](https://github.com/Microsoft/botbuilder-tools/blob/master/packages/MSBot/README.md) tool.

## What does a bot file look like? 
Take a look at a sample [.bot](https://github.com/Microsoft/botbuilder-tools/blob/master/packages/MSBot/docs/sample-bot-file.json) file.
To learn about encrypting and decrypting the .bot file, see [Bot Secrets](https://github.com/Microsoft/botbuilder-tools/blob/master/packages/MSBot/docs/bot-file-encryption.md).

## Why do I need a .bot file?
A .bot file is **not** a requirement to build bots with Bot Framework SDK. You can continue to use appsettings.json, web.config, env, 
keyvault or any mechanism you see fit to keep track of service references and keys that your bot depends on. However, to test
the bot using the Emulator, you'll need a .bot file. The good news is that Emulator can create a .bot file for testing. To do that, 
start the Emulator, click on the **create a new bot configuration** link on the Welcome page. In the dialog box that appears, type a **Bot name** and an **Endpoint URL**. Then connect.

The advantages of using .bot file are:
- Provides a standard way of storing resources regardless of the language/platform you use.   
- Bot Framework Emulator and CLI tools rely on and work great with tracking connected services in a consistent format (in a .bot file) 
- Elegant tooling solutions around services creation and management is harder without a well defined schema (.bot file).  

## Using .bot file in your Bot Framework SDK bot
You can use the .bot file to get service configuration information in your bot's code. The BotFramework-Configuration library available 
for [C#](https://www.nuget.org/packages/Microsoft.Bot.Configuration) and [JS](https://www.npmjs.com/package/botframework-config) helps you load a bot file and supports several methods to query and get the appropriate service configuration information.

## Additional resources
Refer to [MSBot](https://github.com/Microsoft/botbuilder-tools/blob/master/packages/MSBot/README.md) readme file for more information on using a bot file.
