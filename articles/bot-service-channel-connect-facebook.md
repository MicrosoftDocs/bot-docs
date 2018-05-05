---
title: Connect a bot to Facebook Messenger | Microsoft Docs
description: Learn how to configure a bot's connection to Facebook Messenger.
author: RobStand
ms.author: RobStand
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 12/13/2017
---

# Connect a bot to Facebook Messenger
To learn more about developing for Facebook Messenger, see the [Messenger platform documentation](https://developers.facebook.com/docs/messenger-platform). You may wish to review Facebook's [pre-launch guidelines](https://developers.facebook.com/docs/messenger-platform/product-overview/launch#app_public), [quick start](https://developers.facebook.com/docs/messenger-platform/guides/quick-start), and [setup guide](https://developers.facebook.com/docs/messenger-platform/guides/setup).

To configure a bot to communicate using Facebook Messenger, enable Facebook Messenger on a Facebook page and then connect the bot to the app.

[!INCLUDE [Channel Inspector intro](~/includes/snippet-channel-inspector.md)]

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

Set the "Allow API Access to App Settings" slider to "Yes".

![App settings](~/media/bot-service-channel-connect-facebook/api_settings.png)

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
2. Under **Subscription Fields**, select *message\_deliveries*, *messages*, *messaging\_optins*, and *messaging_postbacks*.
3. Click **Verify and Save**. 

![Configure webhook](~/media/channels/FB-webhookConfig.png)

## Provide Facebook credentials
On the Bot Framework Portal, paste the **Page ID**, **App ID**, **App Secret**, and **Page Access Token** values copied from Facebook Messenger previously.

![Enter credentials](~/media/channels/fb-credentials2.png)

## Submit for review

Facebook requires a Privacy Policy URL and Terms of Service URL on its basic app settings page. The [Code of Conduct](https://aka.ms/bf-conduct) page contains third party resource links to help create a privacy policy. The [Terms of Use](https://aka.ms/bf-terms) page contains sample terms to help create an appropriate Terms of Service document.

After the bot is finished, Facebook has its own [review process](https://developers.facebook.com/docs/messenger-platform/app-review) for apps that are published to Messenger. The bot will be tested to ensure it is compliant with Facebook's [Platform Policies](https://developers.facebook.com/docs/messenger-platform/policy-overview).

## Make the App public and publish the Page
> [!NOTE]
> Until an app is published, it is in [Development Mode](https://developers.facebook.com/docs/apps/managing-development-cycle). Plugin and API functionality will only work for admins, developers, and testers.

After the review is successful, in the App Dashboard under App Review, set the app to Public.
Ensure that the Facebook Page associated with this bot is published. Status appears in Pages settings.
