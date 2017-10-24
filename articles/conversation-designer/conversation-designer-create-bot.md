---
title: Create a Conversation Designer bot | Microsoft Docs
description: Learn how to create a new bot using Conversation Designer.
author: v-ducvo
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/04/2017
ROBOTS: NoIndex, NoFollow
---

# Create a new Conversation Designer bot
> [!IMPORTANT]
> Conversation Designer is not available to all customers yet. More details on
> availability of Conversation Designer will come later this year.

This tutorial walks you through step-by-step instructions for creating a new Conversation Designer bot. 

## Prerequisites

- Conversation Designer requires an Azure subscription. You can get started <a href="https://azure.microsoft.com/en-us/" target="_blank">here</a>
- If you haven't done so already, make sure you sign in to the [LUIS portal](https://luis.ai) at least once after you created an account with them.
- Familiarity with JavaScript programming. Custom script functions are written in JavaScript.
- Microsoft Edge or Google Chrome

## Create a Conversation Designer bot

To create a Conversation Designer bot, follow these steps:
1. Go to https://dev.botframework.com/, and sign in. Use the email address you provided to Microsoft Corporation to participate in this private preview release.
2. Click **Create a bot** in the top-right navigation panel. 
3. Click **Create** to *Create a bot with the Conversation Designer*.
4. Select from one of the many [sample bots](conversation-designer-sample-bots.md) to start with. Click **Next**. If you are not sure which **sample bot** to use, just choose the one you think is the closest to a bot you want to build. You can later switch to a different **sample bot**.
5. Complete all fields and click **Create bot** – it takes about 2 minutes for bot provisioning to complete. 

## Bot Provisioning

The following Azure features are automatically provisioned when you create a Conversation Designer bot: 

1. Azure resource group with the bot name you specified
2. Azure App service
3. Azure App Service plan 
4. Azure Storage account
5. Application Insights 
6. Cognitive Services subscription for [LUIS.ai](https://luis.ai). A LUIS app is created with the **Bot handle** (plus a randomly generated string) as the app name.
7. Microsoft Account single app. [Learn more](https://apps.dev.microsoft.com/#/appList)

## Welcome message

Once the bot is provisioned, Conversation Designer will open the bot's **Build** page. A welcome message appears with information to help you get started. Explore those options or close the message and start working on your bot. You can get back to the welcome message by clicking the ellipses (**...**) from the top-left navigation panel, and then choosing the **Welcome...** option.

## Next step
> [!div class="nextstepaction"]
> [Save bot](conversation-designer-save-bot.md)

## Additional resources
* Learn about [tasks](conversation-designer-tasks.md)
* [Register](../portal-register-bot.md) bots
* Learn more about the [Bot Builder SDK for Node](../nodejs/index.md) 
