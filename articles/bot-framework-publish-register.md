---
title: Bot Framework Register Bot | Microsoft Docs
description: Detailed walkthrough how to register a bot with the Bot Framework.
services: Bot Framework
documentationcenter: BotFramework-Docs
author: kbrandl
manager: rstand

ms.service: Bot Framework
ms.topic: article
ms.workload: Cognitive Services
ms.date: 02/06/2017
ms.author: v-kibran@microsoft.com

---
# Register a bot with the Bot Framework

Before others can use your bot, you must register it with the Bot Framework. 
Registration is a simple process wherein you provide some information about your bot and then generate the app ID and password that your bot will use to authenticate with the Bot Framework.

> [!NOTE]
> You can run your bot in the [Bot Framework Emulator](bot-framework-emulator.md) without supplying an app ID and password, but in order for others to use your bot, it must be registered and authenticate by using the app ID and password that are generated during the registration process.

## Register your bot

To register your bot, complete the following steps:  

1. Sign in to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Developer Portal</a>.  
2. Click **Register a bot**.  
3. Complete the **Bot profile** section of the form.  
  
    - Upload an icon that will represent your bot in the conversation.  
    - Provide your bot's name. If you publish the bot to the <a href="https://bots.botframework.com/" target="_blank">Bot Directory</a>, this is the name that will appear in the directory.  
    - Provide your bot's handle. This is the name that will represent your bot in the conversation. Choose this value carefully, because you cannot change it after you register the bot.  
    - Provide a description of your bot. If you publish the bot to the <a href="https://bots.botframework.com/" target="_blank">Bot Directory</a>, this is the description that will appear directory, so it should describe what your bot does.  

4. Complete the **Configuration** section of the form.  
  
    - Provide your bot's HTTPS endpoint. This is the endpoint where your bot will receive HTTP POST messages from Bot Connector. If you have already [deployed](bot-framework-publish-deploy.md) your bot to the cloud, specify the endpoint generated from that deployment. If you have not yet deployed your bot to the cloud, you can leave the endpoint blank for now, and return later (to the Bot Framework Developer Portal) to specify the endpoint after you've deployed your bot.  
    - Click **Manage Microsoft App ID and password**.  
        - On the page that you’re redirected to, click **Generate an app password to continue**. 
        - Copy and securely store the password that is shown, and then click **Ok**.  
        - Click **Finish and go back to Bot Framework**.  
        - Back in the Bot Framework Developer Portal, the **app ID** field is now auto-populated for you.  

5. Complete the **Admin** section of the form.  
  
    - Provide a comma-delimited list of email addresses corresponding to of the owners of the bot. 
You must provide monitored emails because the framework will send all communications to these emails.  
    - If you host your bot in Azure and use Azure App Insights, provide your insights key.  

6. Click **Register** to complete the registration process. 

### Update application configuration settings

> [!IMPORTANT]
> After you've registered your bot, update the Microsoft App Id and Microsoft App Password values in your application's configuration settings to specify the **app ID** and **password** values that were generated for your bot during the registration process.

##<a id="maintain"></a> Update bot registration data

To update a bot's registration data in the future (or to delete a bot's registration altogether), complete the following steps:

1. Sign in to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Developer Portal</a>.  

2. Click **My bots**.  

3. Select the bot that you want to configure, and in the Details section, click **Edit**.  

4. Update settings and click **Save changes** (or click **Delete bot** to delete the bot registration altogether).

> [!TIP]
> To generate a new password, click **Manage Microsoft App ID and password** (in the Configuration section) to access the Microsoft Application Registration Portal, where the bot's password is managed.

## Next steps

After you have registered your bot with the Bot Framework, 
the next step in the bot publication process will depend upon whether or not you've already deployed your bot to the cloud.

### If you have already deployed your bot to the cloud: 
Update the Microsoft App Id and Microsoft App Password values in your application's configuration settings to specify the **app ID** and **password** values that were generated for your bot during the registration process, and then re-deploy the bot. 
Verify the new deployment by testing the bot using the Bot Framework Emulator (supplying App ID and Password that were generated during the registration process).

Next, you can [configure the bot to run on one or more channels](bot-framework-publish-configure.md).

### If you have not yet deployed your bot to the cloud: 
Deploy your bot to the cloud by following the instructions found in [Deploy a bot to Microsoft Azure](bot-framework-publish-deploy.md). 

After you've deployed your bot to the cloud, return to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Developer Portal</a> to specify the HTTPS endpoint for your bot, as described [here](bot-framework-publish-register.md#maintain).

Next, you can [configure the bot to run on one or more channels](bot-framework-publish-configure.md).



 