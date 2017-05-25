---
title: Connect a bot to Facebook Messenger | Microsoft Docs
description: Learn how to configure a bot's connection to Facebook Messenger.
author: JaymeMPerlman
ms.author: v-jaype
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 05/24/2017
ms.reviewer:
---

# Connect a bot to Facebook Messenger
To learn more about developing for Facebook Messenger, see the [Messenger platform documentation](https://developers.facebook.com/docs/messenger-platform). 

To configure a bot to communicate using Facebook Messenger, enable Facebook Messenger on a Facebook page and then connect the bot to the app.

> [!NOTE]
> The Facebook UI may appear slightly different depending on which version you are using. 

## Copy the Page ID
The bot is accessed through a Facebook Page. [Create a new Facebook Page](https://www.facebook.com/bookmarks/pages) or go to an existing Page.

* Open the Facebook Page's **About** page and then copy and save the **Page ID**.

## Create a Facebook app
[Create a new Facebook App](https://developers.facebook.com/quickstarts/?platform=web) on the Page and generate an App ID and App Secret for it.

![Create an App ID](~/media/channels/FB-CreateAppId.png)

* Copy and save the **App ID** and the **App Secret**.

![Save App ID and secret](~/media/channels/FB-get-appid.png)

## Enable messenger

Enable Facebook Messenger in the new Facebook App. 
* On the **Product Setup** page of the app, click **Get Started** and then click **Get Started** again. 

![Enable messenger](~/media/channels/FB-AddMessaging1.png)

## Generate a Page Access Token
In the **Token Generation** panel of the Messenger section, select the target Page. A Page Access Token will be generated. 

* Copy and save the **Page Access Token**. 

![Generate token](~/media/channels/FB-generateToken.png)

## Enable webhooks
Click **Set up Webhooks** to forward messaging events from Facebook Messenger to the bot.

![Enable webhook](~/media/channels/FB-webhook.png)

## Provide webhook callback URL and verify token
Return to the [Bot Framework Portal](https://dev.botframework.com/). Open the bot, click the **Channels** tab, and then click **Facebook Messenger**.

* Copy the **Callback URL** and **Verify Token** values from the portal.

![Copy values](~/media/channels/fb-callbackVerify.png)

1. Return to Facebook Messenger and paste the **Callback URL** and **Verify Token** values.
2. Under **Subscription Fields**, select *message\_deliveries*, *messages*, *messaging\_options*, and *messaging_postbacks*.
3. Click **Verify and Save**. 

![Configure webhook](~/media/channels/FB-webhookConfig.png)

## Provide Facebook credentials
On the Bot Framework Portal, paste the **Page ID**, **App ID**, **App Secret**, and **Page Access Token** values copied from Facebook Messenger previously.

![Enter credentials](~/media/channels/fb-credentials2.png)

## Enable the bot
If the app is not ready for release, leave the toggle set to *Disabled* and click **Save**. The bot can be finished and enabled later.

To release the bot to the Facebook Messenger App Directory, click the toggle to set it to *Enabled* and then click **Save**. 








