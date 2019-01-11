---
title: Create a Bot Channels Registration with Bot Service | Microsoft Docs
description: Learn how to register an existing bot with Bot Service.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: abs
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Register a bot with Bot Service



If you already have a bot hosted elsewhere and you would like to use the Bot Service to connect it to other channels, you will need to register your bot with the Bot Service. In this topic, learn how to register your bot with the Bot Service by creating a **Bot Channels Registration** bot service.

> [!IMPORTANT] 
> You only need to register your bot if it is not hosted in Azure. If you [created a bot](bot-service-quickstart.md) through the Azure portal then your bot is already registered with the Bot Service.

## Log in to Azure
Log in to the [Azure portal](http://portal.azure.com).

> [!TIP]
> If you do not already have a subscription, you can register for a <a href="https://azure.microsoft.com/en-us/free/" target="_blank">free account</a>.

## Create a Bot Channels Registration
You need a **Bot Channels Registration** bot service to be able to use Bot Service functionality. A registration bot lets you connect your bot to channels.

To create a **Bot Channels Registration**, do the following:

1. Click the **New** button found on the upper left-hand corner of the Azure portal, then select **AI + Cognitive Services > Bot Channels Registration**. 

2. A new blade will open with information about the **Bot Channels Registration**. Click the **Create** button to start the creation process. 

3. In the **Bot Service** blade, provide the requested information about your bot as specified in the table below the image.  <br/>
   ![Create registration bot blade](~/media/azure-bot-quickstarts/registration-create-bot-service-blade.png)


   |                    Setting                     |         Suggested value         |                                                                                                  Description                                                                                                  |
   |------------------------------------------------|---------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
   |           <strong>Bot name</strong>            |     Your bot's display name     |                                                  The display name for the bot that appears in channels and directories. This name can be changed at anytime.                                                  |
   |         <strong>Subscription</strong>          |        Your subscription        |                                                                                Select the Azure subscription you want to use.                                                                                 |
   |        <strong>Resource Group</strong>         |         myResourceGroup         |                                 You can create a new [resource group](/azure/azure-resource-manager/resource-group-overview#resource-groups) or choose from an existing one.                                  |
   |                    Location                    |             West US             |                                                        Choose a location near where your bot is deployed or near other services your bot will access.                                                         |
   |         <strong>Pricing tier</strong>          |               F0                |             Select a pricing tier. You may update the pricing tier at any time. For more information, see [Bot Service pricing](https://azure.microsoft.com/en-us/pricing/details/bot-service/).              |
   |      <strong>Messaging endpoint</strong>       |               URL               |                                                                               Enter the URL for your bot's messaging endpoint.                                                                                |
   |     <strong>Application Insights</strong>      |               On                | Decide if you want to turn [Application Insights](bot-service-manage-analytics.md) <strong>On</strong> or <strong>Off</strong>. If you select <strong>On</strong>, you must also specify a regional location. |
   | <strong>Microsoft App ID and password</strong> | Auto create App ID and password |              Use this option if you need to manually enter a Microsoft App ID and password. Otherwise, a new Microsoft App ID and password will be created for you in the bot creation process.               |


4. Click **Create** to create the service and register your bot's messaging end point.

Confirm that the registration has been created by checking the **Notifications**. The notifications will change from **Deployment in progress...** to **Deployment succeeded**. Click **Go to resource** button to open the bot's resources blade. 

## Bot Channels Registration password

**Bot Channels Registration** bot service does not have an app service associated with it. Because of that, this bot service only has a *MicrosoftAppID*. You need to generate the password manually and save it yourself. You will need this password if you want to test your bot using the [emulator](bot-service-debug-emulator.md).

To generate a MicrosoftAppPassword, do the following:

1. From the **Settings** blade, click **Manage**. This is the link appearing by the **Microsoft App ID**. This link will open a window where you can generate a new password. <br/>
  ![Manage link in Settings blade](~/media/azure-bot-quickstarts/registration-settings-manage-link.png)

2. Click **Generate New Password**. This will generate a new password for your bot. Copy this password and save it to a file. This is the only time you will see this password. If you do not have the full password saved, you will need to repeat the process to create a new password should you need it later. <br/>
  ![Generate Microsoft App Password](~/media/azure-bot-quickstarts/registration-generate-app-password.png)

## Update the bot

If you're using the Bot Framework SDK for Node.js, set the following environment variables:

* MICROSOFT_APP_ID
* MICROSOFT_APP_PASSWORD

If you're using the Bot Framework SDK for .NET, set the following key values in the web.config file:

* MicrosoftAppId
* MicrosoftAppPassword

## Test the bot

Now that your bot service is created, [test it in Web Chat](bot-service-manage-test-webchat.md). Enter a message and your bot should respond.

## Next steps

In this topic, you learned how to register your hosted bot with the Bot Service. The next step is to learn how to manage your Bot Service.

> [!div class="nextstepaction"]
> [Manage a bot](bot-service-manage-overview.md)

