---
title: Create a bot | Microsoft Docs
description: Learn how to create a new bot using Conversation Designer.
author: vkannan
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/04/2017
ROBOTS: NoIndex, NoFollow
---

# Create a new Conversation Designer bot


This tutorial walks you through step-by-step instructions for creating a new bot. 

## Prerequisites

- An [Azure subscription](https://azure.microsoft.com/en-us/)
- Familiarity with JavaScript programming. Custom script functions are written in JavaScript.
- Microsoft Edge, Google Chrome

## Setting up the environment 

To set up the environment, use the email address you provided to Microsoft Corporation to participate in this private preview release.
1. Go to https://dev.botframework.com/, and sign in.
2. Click **CREATE A BOT OR SKILL** in the top navigation panel. 
3. Click **Create** to create a bot with the Conversation Designer.
4. On the next page, complete all fields, and then click **Create bot** – it takes about 2 minutes for bot provisioning to complete. 

## Bot Provisioning

The following Azure features are automatically provisioned: 

1. Azure resource group with the bot name you specified 
2. Azure App service
3. Azure App Service plan 
4. Azure Storage account
5. Application Insights 
6. Cognitive Services subscription for [LUIS.ai](http://luis.ai)
7. Microsoft Account single app. [Learn more](https://apps.dev.microsoft.com/#/appList)

## Next step
> [!div class="nextstepaction"]
> [Save bot](conversation-designer-save-bot.md)

## Additional resources
* Learn about [tasks](conversation-designer-tasks.md)
* [Register](../portal-register-bot.md) bots
* Learn more about the [Bot Builder SDK for Node](../nodejs/index.md) 
