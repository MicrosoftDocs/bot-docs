---
title: Connect a bot to Facebook Messenger | Microsoft Docs
description: Learn how to configure a bot's connection to Facebook Messenger.
author: JaymeMPerlman
ms.author: v-jaype
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date:
ms.reviewer:
ROBOTS: Index, Follow
---
# Connect a bot to Facebook Messenger

Configure your bot to communicate using the Facebook Messenger app.
You will create a Facebook page, enable Facebook Messenger on that page, and then connect your bot to that app.

To learn more about developing for Facebook Messenger, see the [Messenger platform documentation](https://developers.facebook.com/docs/messenger-platform). 

## Create a Facebook page
Your bot is accessed through a Facebook Page.
The Facebook Page ID can be found on your Facebook Page's **About** page. 

[Create a new Facebook Page](https://www.facebook.com/bookmarks/pages) or go to an existing Page and copy the Facebook Page ID.

## Create a Facebook app
Connecting a bot also requires a Facebook App. 
[Create a new Facebook App](https://developers.facebook.com/quickstarts/?platform=web)

> [!NOTE]
> The Facebook UI may appear slightly different depending on which version you are using. 

![Create an App ID](~/media/channels/FB-CreateAppId.png)

## Copy your App ID and App Secret and save them for later
<<<<<<< HEAD:articles/thirdparty-channels/channel_facebook.md

![Save App ID and secret](~/media/channels/FB_get-appid.png)
=======
![Save App ID and secret](~/media/channels/FB-get-appid.png)
>>>>>>> f0a86db874863bd39da27c71980a927d4b329cbf:articles/thirdparty-channels/channel-facebook.md

## Enable messenger

Enable Facebook Messenger in your new Facebook App. 
On the **Product Setup** page of the app, click **Get Started** and then click **Get Started** again. 

![Enable messenger](~/media/channels/FB-AddMessaging_1.png)

## Generate page access token
In the **Token Generation** panel of the Messenger section, select your Page.
A Page Access Token will be generated for you. Copy this Access Token. 

![Enable messenger](~/media/channels/FB-generateToken.png)

## Set up webhook
Enable webhooks to forward messaging events from Facebook Messenger to your bot.

![Enable webhook](~/media/channels/FB-webhook.png)

## Configure webhook callback URL and verify token

Configure the webhook. 
<!-- on the origial page, (https://dev.botframework.com/ConfigChannel.aspx?botId=<botname>&channelId=facebook)this immediately precedes the fields where the callback and verify tokens are actually generated -->
1. Enter the URL below for the Callback URL
2. Enter the Verify Token. 
3. Under **Subscription Fields**, select message\_deliveries, messages, messaging\_options, and messaging_postbacks 
4. Click **Verify** and then click **Save**. 

![Configure webhook](~/media/channels/FB-webhookConfig.png)

## Enter your credentials
<<<<<<< HEAD:articles/thirdparty-channels/channel_facebook.md

Enter the Page ID, App ID, Page Secret, and Access Token that you copied earlier.
![Enter credentials](~/media/channels/FB_credentials.png)
=======
Enter the credentials that you copied earlier.
![Enter credentials](~/media/channels/FB-credentials.png)
>>>>>>> f0a86db874863bd39da27c71980a927d4b329cbf:articles/thirdparty-channels/channel-facebook.md

Check **Enable this bot on Facebook Messenger**.

Click **I'm done configuring Facebook Messenger**

