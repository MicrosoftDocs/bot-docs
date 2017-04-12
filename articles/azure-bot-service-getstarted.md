---
title: Create a bot with the Azure Bot Service | Microsoft Docs
description: Learn how to create a bot with the Azure Bot Service.
keywords: Bot Framework, Bot Builder, Azure Bot Service, get started
author: kbrandl
manager: rstand
ms.topic: bot-service-get-started-article
ms.prod: bot-framework

ms.date: 
ms.reviewer: rstand@microsoft.com
ROBOTS: Index, Follow
---
> [!WARNING]
> This article is under development. The content will likely be rewritten.

# Create a bot with the Azure Bot Service

<!--
> [!div class="op_single_selector"]
> * [.NET](~/dotnet/getstarted.md)
> * [Node.js](~/nodejs/getstarted.md)
> * [Azure Bot Service](~/azure-bot-service/getstarted.md)
>
-->

The Azure Bot Service accelerates the process of developing a bot 
by providing an integrated environment that is purpose-built for bot development. 
This tutorial walks you through the process of creating and testing a bot by using the Azure Bot Service.

## Prerequisites

You must have a Microsoft Azure subscription before you can use the Azure Bot Service. 
If you do not already have a subscription, you can register for a <a href="https://azure.microsoft.com/en-us/free/" target="_blank">free trial</a>.

## Create your bot

To create a bot by using the Azure Bot Service, 
sign in to <a href="https://portal.azure.com" target="_blank">Azure</a> and complete the following steps. 

### Create a new bot service

1. Select **New** in the menu blade. 

2. In the **New** blade, navigate to the Intelligence + analytics category, and select Bot Service. 

3. In the Bot Service blade, provide the requested information, and click **Create** to create the bot service and deploy it to the cloud. 

    - Set **App name** to your bot’s name. The name is used as the subdomain when your bot is deployed to the cloud (for example, *mybasicbot*.azurewebsites.net). 
    - Select the subscription to use.  
    - Select the <a href="https://azure.microsoft.com/en-us/documentation/articles/resource-group-overview/" target="_blank">resource group</a> and <a href="https://azure.microsoft.com/en-us/regions/" target="_blank">location</a>.<br/>  
    ![Bot Service blade](~/media/azure-bot-service-create-bot.png)

4. Confirm that the bot service has been deployed.
    - Click **Notifications** (the bell icon that is located along the top edge of the Azure portal). The notification will change from **Deployment started** to **Deployment succeeded**. 
    - After the notification changes to **Deployment succeeded**, click that notification.<br/><br/>
    ![Azure notification](~/media/azure-bot-service-first-bot-notification.png)

### Create App ID and password  

Next, create an app ID and password for your bot, so that it will be able to authenticate with the Bot Framework.

1. Click **Create Microsoft App ID and password**.  

    ![create app id](~/media/azure-bot-service-create-app-id.png)  

2. On the page that opens in a new browser tab, click **Generate an app password to continue**.

3. Copy and securely store the password that is shown, and then click **Ok**.

4. Click **Finish and go back to Bot Framework**.

5. Back in the Azure Portal, the **app ID** field is now auto-populated for you. 
Paste the password that you copied (in step 3 above) into the password field.
> [!TIP]
> If the **app ID** field is not auto-populated, you can retrieve it by signing in to the 
> <a href="https://apps.dev.microsoft.com" target="_blank">Microsoft Application Registration Portal</a> 
> and copying the application ID from your application's registration settings.

    ![password](~/media/azure-bot-service-password.png)  

> [!NOTE]
> Click **Manage Microsoft App ID and password** only if you want to generate a secondary password for your bot now. 
> In the future, you can manage app ID and password at any time by using the Bot Framework Portal, as described [here](~/portal-register-bot.md#maintain). 

### Select your programming language 

Choose the programming language that you want to use to develop your bot.  

![language](~/media/azure-bot-service-coding-language.png)  

### Select a template and create the bot

Select the template to use as the starting point for developing your bot. 
For this tutorial, choose the **Basic** template. 

![template](~/media/azure-bot-service-template.png)  

Then, click **Create bot** to create the bot based upon the programming language and template that you've chosen. 

> [!IMPORTANT]
> When you click **Create bot**, there may be a slight delay before a splash screen renders to indicate that the bot service is generating your bot. *Do not* click **Create bot** again. Please wait for the splash screen to appear.

When the bot service finishes generating your bot, the Azure editor will contain the bot's source files. 
At this point, the bot has been created, registered with the Bot Framework, deployed to the cloud, and is fully functional. 
If you sign in to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Portal</a>, 
you'll see that your bot is now listed under **My bots**. 
Congratulations! You've successfully created a bot by using the Azure Bot Service! 

![bot settings in portal](~/media/azure-bot-service-bf-portal.png)

## Test your bot

Now that your bot is running in the cloud, try it out by typing a few messages into the built-in chat control 
that's located to the right of the code editor in Azure. 
You should see that the bot responds to each message you send by echoing back your message prefixed with the text *You said*. 

![azure editor](~/media/azure-bot-service-editor.png)  

## Next steps

In this tutorial, you created a simple bot by using the Azure Bot Service 
and verified the bot's functionality by using the built-in chat control within Azure. 
At this point, you may want to add more functionality to your bot or set up continuous integration. 
You can also [configure your bot](~/portal-configure-channels.md) to run on one or more channels, 
and [publish your bot](~/portal-submit-bot-directory.md) to the Bot Directory, without ever leaving 
the Azure portal. 

To learn more about building great bots with the Bot Framework, see the following articles:

- [Key concepts in the Bot Framework](~/bot-framework-concepts-overview.md)
- [Introduction to bot design](~/bot-design-principles.md)
