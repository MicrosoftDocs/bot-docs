---
title: Register a bot with the Bot Framework | Microsoft Docs
description: Learn how to register a bot with the Bot Framework.
keywords: Bot Framework, Bot Builder, register bot, Bot Framework Portal
author: kbrandl
manager: rstand
ms.topic: publish-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/06/2017
ms.reviewer: rstand
#ROBOTS: Index
---
# Register a bot with the Bot Framework

Before others can use your bot, you must register it with the Bot Framework.
Registration is a simple process wherein you provide some information about your bot and then generate the app ID and password that your bot will use to authenticate with the Bot Framework.

## Register your bot

To register your bot, sign in to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Developer Portal</a>, click **Register a bot**, and complete the following steps.
  
###Complete the **Bot profile** section of the form.  

1. Upload an icon that will represent your bot in the conversation.  
2. Provide your bot's name. If you publish the bot to the <a href="https://bots.botframework.com/" target="_blank">Bot Directory</a>, this is the name that will appear in the directory.  
3. Provide your bot's handle. This is the name that will represent your bot in the conversation. Choose this value carefully, because you cannot change it after you register the bot.  
4. Provide a description of your bot. If you publish the bot to the <a href="https://bots.botframework.com/" target="_blank">Bot Directory</a>, this is the description that will appear directory, so it should describe what your bot does.  

###Complete the **Configuration** section of the form.  

1. Provide your bot's **HTTPS** endpoint. This is the endpoint where your bot will receive HTTP POST messages from Bot Connector. (If you built your bot by using the Bot Builder SDK, the endpoint should end with `/api/messages`.)
    - If you have already deployed your bot to the cloud, specify the endpoint generated from that deployment.
    - If you have not yet deployed your bot to the cloud, leave the endpoint blank for now, and return later (to the Bot Framework Developer Portal) to specify the endpoint after you've deployed your bot.  

2. Click **Manage Microsoft App ID and password**.  
    - On the page that you’re redirected to, click **Generate an app password to continue**.
    - Copy and securely store the password that is shown, and then click **Ok**.  
    - Click **Finish and go back to Bot Framework**.  
    - Back in the Bot Framework Developer Portal, the **app ID** field is now auto-populated for you.  

###Complete the **Admin** section of the form.  

1. Provide a comma-delimited list of email addresses corresponding to of the owners of the bot.
You must provide monitored emails because the framework will send all communications to these emails.  
    - If you host your bot in Azure and use Azure App Insights, provide your insights key.  

2. Click **Register** to complete the registration process.

<a id="maintain"></a>
> [!NOTE]
> In the future, to update the bot's registration data (or to delete the bot's registration altogether):
> <ol><li>Sign in to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Developer Portal</a>.</li><li>Click **My Bots**.</li><li>Select the bot that you want to configure, and in the Details section, click **Edit**.</li><li>Update settings.<ul><li>To generate a new password, click **Manage Microsoft App ID and password** (in the Configuration section) to access the Microsoft Application Registration Portal, where the bot's password is managed.</li></ul></li><li>Click **Save changes** (or click **Delete bot** to delete the bot registration altogether).</li></ol>

##<a id="updateConfigSettings"></a> Update application configuration settings

After you've registered your bot, update the Microsoft App Id and Microsoft App Password values in your application's configuration settings to specify the **app ID** and **password** values that were generated for your bot during the registration process.

> [!TIP]
[!include[Application configuration settings](../includes/snippet-tip-bot-config-settings.md)]

## Next steps

After you have registered your bot with the Bot Framework,
the next step in the bot publication process will depend upon whether or not you've already deployed your bot to the cloud.

### If you have not yet deployed your bot to the cloud:
1. Deploy your bot to the cloud by following the instructions found in [Deploy a bot to the cloud](bot-framework-publish-deploy.md).

2. Return to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Developer Portal</a> and [update your bot's registration data](bot-framework-publish-register.md#maintain) to specify the **HTTPS** endpoint for the bot.

3. [Configure the bot to run on one or more channels](bot-framework-publish-configure.md).

### If you have already deployed your bot to the cloud:
1. Update the Microsoft App Id and Microsoft App Password values in your deployed application's configuration settings to specify the **app ID** and **password** values that were generated for your bot during the registration process, as described [here](#updateConfigSettings).

2. [Configure the bot to run on one or more channels](bot-framework-publish-configure.md).
