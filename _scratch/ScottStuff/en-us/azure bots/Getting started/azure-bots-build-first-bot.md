---
layout: page
title: Creating your first bot
permalink: /en-us/azure-bots/build/first-bot/
weight: 11000
parent1: Azure Bot Service
parent2: Get Started
---

To create a bot using Azure Bot Service:

1. Sign in to [Azure](https://portal.azure.com). 
2. Select **New** in the menu blade.  
3. In the **New** blade, navigate to the Intelligence + Analytics category, and select Bot Service.  
4. In the Bot Service blade, provide the requested information and click **Create**. When the deployment completes, Azure adds a notification to the **Notifications** group.  

     ![Bot Service blade](/en-us/images/azure-bots/first-bot-create.png)  
   
  - Set **App name** to your bot’s name. The name is used as the subdomain of your deployment (for example, mybasicbot.azurewebsites.net). The name may contain alphanumeric characters and a dash (-).  
  - Select the subscription to use.  
  - Select the resource group and location. See the Azure documentation for information about [Resource Group]( https://azure.microsoft.com/en-us/documentation/articles/resource-group-overview/), and [Location](https://azure.microsoft.com/en-us/regions/).   
  
5. Check whether the bot has been deployed.  
  - To determine whether the bot has been deployed, click **Notifications** (see the bell icon at the top right). The notification will change from **Deployments started** to **Deployments succeeded**.  
  - Click on the notification, and then on the resource name in the resulting notification blade.

    ![notifications](/en-us/images/azure-bots/first-bot-notification.png)  

    - Alternatively, you can click All Resources in the menu blade, and then select the bot from the list of resources.    
  
6. Create an app ID and password by clicking **Create Microsoft App ID and password**.  

    ![create app id](/en-us/images/azure-bots/create_app_id.png)  
  
    - When you click Create, you’re taken to another page where you click **Generate an app password to continue** to get your password. Copy and securely store the password.  
    - Click **Finish and go back to Bot Framework**.  
    - When you come back, the app ID field is populated for you. Paste the password that you copied into the password field.  

    ![password](/en-us/images/azure-bots/password.png)  
  
    - You should only click **Manage Microsoft App ID and password** if you want to get a secondary password for your bot. Note that you can manage passwords anytime in the Bot Framework portal. Click **My bots**, select the bot, and in the Details section, click **Edit**. Under Configuration, click **Manage Microsoft App ID and password**.  
  
7. Select the programming language that you want to use to develop your bot.  

    ![language](/en-us/images/azure-bots/coding_language.png)  
  
8. Select the template to use as the starting point for writing your bot.  

    ![template](/en-us/images/azure-bots/template.png)  
  
10. Click **Create bot** to create the bot. After the Bot Service generates your bot, the Azure editor will contains your bot’s source files. At this point, the bot is functional. Try it out in the chat control.  

    ![azure editor](/en-us/images/azure-bots/azure_editor.png)  
  
    (Note that there may be a slight delay before the splash screen displays indicating that the Bot Service is generating your bot; don’t click **Create bot** again.)

What’s left to do? Besides adding functionality to your bot, you’ll need to configure your bot to work on the channels that your users use, and you’ll need to publish the bot to the Bot Framework’s Bot Directory. For information about configuring channels, see [Connecting to channels](/en-us/azure-bots/manage/channels/). For information about publishing your bot to the Bot directory, see [Publishing your bot](/en-us/azure-bot-service/manage/publish/).

For information about setting up continuous integration, see [Setting up continuous integration](/en-us/azure-bots/manage/setting-up-continuous-integration/).


