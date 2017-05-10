---
title: Register a bot with the Bot Framework | Microsoft Docs
description: Learn how to register a bot with the Bot Framework.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---

# Register a bot with the Bot Framework

Before others can use your bot, you must register it with the Bot Framework.
Registration is a simple process. You are prompted to provide some information about your bot and then the portal generates the app ID and password that your bot will use to authenticate with the Bot Framework.

> [!NOTE]
> Bots created with the Azure Bot Service are automatically registered as part of the creation process.
 
## Register your bot

To register your bot, sign in to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Portal</a>, click **Register**, and complete the following steps.
  
### Complete the **Bot profile** section of the form.  

1. Upload an icon that will represent your bot in the conversation.  
2. Provide your bot's **Display Name**. When users search for this bot, this is the name that will appear in the search results.  
3. Provide a **Description** of your bot. This is the description that will appear in search results, so it should accurately describe what the bot does.  

### Complete the **Configuration** section of the form.  

1. Provide your bot's **HTTPS** endpoint. This is the endpoint where your bot will receive HTTP POST messages from Bot Connector. If you built your bot by using the Bot Builder SDK, the endpoint should end with `/api/messages`.
    - If you have already deployed your bot to the cloud, specify the endpoint generated from that deployment.
    - If you have not yet deployed your bot to the cloud, leave the endpoint blank for now. You will return to the Bot Framework Portal later and specify the endpoint after you've deployed your bot.  

2. Click **Manage Microsoft App ID and password**.  
    - On the next page, click **Generate an app password to continue**.
    - Copy and securely store the password that is shown, and then click **Ok**.  
    - Click **Finish and go back to Bot Framework**.  
    - Back in the Bot Framework Portal, the **App ID** field is now populated.  

3. Check to indicate that you have read and accepted the the [Terms of Use][terms], [Privacy Statement][privacy], and [Code of Conduct][code]. 

4. Click **Register** to complete the registration process.

##<a id="updateConfigSettings"></a> Update application configuration settings

After you've registered your bot, update the Microsoft App Id and Microsoft App Password values in your application's configuration settings to specify the **app ID** and **password** values that were generated for your bot during the registration process.

> [!TIP]
> If you're using the Bot Builder SDK for Node.js, set the following environment variables:
> <ul><li>MICROSOFT_APP_ID</li><li>MICROSOFT_APP_PASSWORD</li></ul>
> If you're using the Bot Builder SDK for .NET, set the following key values in the web.config file:
> <ul><li>MicrosoftAppId</li><li>MicrosoftAppPassword</li></ul>

## Update or delete registration

<a id="maintain"></a>
To update or delete the bot's registration data:

1. Sign in to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Portal</a>.
2. Click **My Bots**.
3. Select the bot that you want to configure and click **Settings**.
    - To generate a new password, click **Manage Microsoft App ID and password**.
    - To delete a bot, click **Delete bot**.

## Next steps

After you have registered your bot with the Bot Framework,
the next step in the bot publication process will depend upon whether or not you've already deployed your bot to the cloud.

### If you have not yet deployed your bot to the cloud:

1. Deploy your bot to the cloud by following the instructions found in [Deploy a bot to the cloud](~/deploy-bot-overview.md).

2. Return to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Portal</a> and [update your bot's registration data](~/portal-register-bot.md#maintain) to specify the **HTTPS** endpoint for the bot.

3. [Configure the bot to run on one or more channels](~/portal-configure-channels.md).

### If you have already deployed your bot to the cloud:

1. Update the Microsoft App Id and Microsoft App Password values in your deployed application's configuration settings to specify the **app ID** and **password** values that were generated for your bot during the registration process, as described [here](#updateConfigSettings). 

2. [Configure the bot to run on one or more channels](~/portal-configure-channels.md). 

[terms]: https://aka.ms/bf-terms
[code]: https://aka.ms/bf-conduct
[privacy]: https://aka.ms/bf-privacy
