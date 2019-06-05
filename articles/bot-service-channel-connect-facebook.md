---
title: Connect a bot to Facebook Messenger | Microsoft Docs
description: Learn how to configure a bot's connection to Facebook Messenger.
keywords: Facebook Messenger, bot channel, Facebook App, App ID, App Secret, Facebook bot, credentials
author: RobStand
ms.author: RobStand
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 10/12/2018
---

# Connect a bot to Facebook

Your bot can be connected to both Facebook Messenger and Facebook Workplace, so that it can communicate with users on both platforms. The following tutorial shows how to connect a bot to these two channels step by step.

> [!NOTE]
> The Facebook UI may appear slightly different depending on which version you are using.

## Connect a bot to Facebook Messenger

To learn more about developing for Facebook Messenger, see the [Messenger platform documentation](https://developers.facebook.com/docs/messenger-platform). You may wish to review Facebook's [pre-launch guidelines](https://developers.facebook.com/docs/messenger-platform/product-overview/launch#app_public), [quick start](https://developers.facebook.com/docs/messenger-platform/guides/quick-start), and [setup guide](https://developers.facebook.com/docs/messenger-platform/guides/setup).

To configure a bot to communicate using Facebook Messenger, enable Facebook Messenger on a Facebook page and then connect the bot to the app.

### Copy the Page ID

The bot is accessed through a Facebook Page. [Create a new Facebook Page](https://www.facebook.com/bookmarks/pages) or go to an existing Page.

* Open the Facebook Page's **About** page and then copy and save the **Page ID**.

### Create a Facebook app

[Create a new Facebook App](https://developers.facebook.com/quickstarts/?platform=web) on the Page and generate an App ID and App Secret for it.

![Create an App ID](~/media/channels/FB-CreateAppId.png)

* Copy and save the **App ID** and the **App Secret**.

![Save App ID and secret](~/media/channels/FB-get-appid.png)

Set the "Allow API Access to App Settings" slider to "Yes".

![App settings](~/media/bot-service-channel-connect-facebook/api_settings.png)

### Enable messenger

Enable Facebook Messenger in the new Facebook App.

![Enable messenger](~/media/channels/FB-AddMessaging1.png)

### Generate a Page Access Token

In the **Token Generation** panel of the Messenger section, select the target Page. A Page Access Token will be generated.

* Copy and save the **Page Access Token**.

![Generate token](~/media/channels/FB-generateToken.png)

### Enable webhooks

Click **Set up Webhooks** to forward messaging events from Facebook Messenger to the bot.

![Enable webhook](~/media/channels/FB-webhook.png)

### Provide webhook callback URL and verify token

In the [Azure portal](https://portal.azure.com/), open the bot, click the **Channels** tab, and then click **Facebook Messenger**.

* Copy the **Callback URL** and **Verify Token** values from the portal.

![Copy values](~/media/channels/fb-callbackVerify.png)

1. Return to Facebook Messenger and paste the **Callback URL** and **Verify Token** values.

2. Under **Subscription Fields**, select *message\_deliveries*, *messages*, *messaging\_optins*, and *messaging\_postbacks*.

3. Click **Verify and Save**.

![Configure webhook](~/media/channels/FB-webhookConfig.png)

4. Subscribe the webhook to the Facebook page.

![Subscribe webhook](~/media/bot-service-channel-connect-facebook/subscribe-webhook.png)


### Provide Facebook credentials

In Azure portal, paste the **Facebook App ID**, **Facebook App Secret**, **Page ID**,and **Page Access Token** values copied from Facebook Messenger previously. You can use the same bot on multiple facebook pages by adding additional page ids and access tokens.

![Enter credentials](~/media/channels/fb-credentials2.png)

### Submit for review

Facebook requires a Privacy Policy URL and Terms of Service URL on its basic app settings page. The [Code of Conduct](https://investor.fb.com/corporate-governance/code-of-conduct/default.aspx) page contains third party resource links to help create a privacy policy. The [Terms of Use](https://www.facebook.com/terms.php) page contains sample terms to help create an appropriate Terms of Service document.

After the bot is finished, Facebook has its own [review process](https://developers.facebook.com/docs/messenger-platform/app-review) for apps that are published to Messenger. The bot will be tested to ensure it is compliant with Facebook's [Platform Policies](https://developers.facebook.com/docs/messenger-platform/policy-overview).

### Make the App public and publish the Page

> [!NOTE]
> Until an app is published, it is in [Development Mode](https://developers.facebook.com/docs/apps/managing-development-cycle). Plugin and API functionality will only work for admins, developers, and testers.

After the review is successful, in the App Dashboard under App Review, set the app to Public.
Ensure that the Facebook Page associated with this bot is published. Status appears in Pages settings.

## Connect a bot to Facebook Workplace

See the [Workplace Help Center](https://workplace.facebook.com/help/work/) to learn more about Facebook Workplace and [Workplace Developer Documentation](https://developers.facebook.com/docs/workplace) for guidelines about developing for Facebook Workplace.

To configure a bot to communicate using Facebook Workplace, create a custom integration and connect the bot to it.

### Create a Facebook Workplace Premium account

Following the instructions [here](https://www.facebook.com/workplace) to create a Facebook Workplace Premium account and set yourself as the system administrator. Please keep in mind that only system administrator of a Workplace can create custom integrations.

### Create a custom integration

When you create a custom integration, an app with defined permissions and a page of type 'Bot' only visible within your Workplace community are created.

Create a [custom integration](https://developers.facebook.com/docs/workplace/custom-integrations-new) for your Workplace following the steps below:

- In the **Admin Panel**, open the **Integrations** tab.
- Click on the **Create your own custom App** button.

![Workplace Integration](~/media/channels/fb-integration.png)

- Choose a display name and a profile picture for the app. Such information will be shared with the page of type 'Bot'.
- Set the **Allow API Access to App Settings** to "Yes".
- Copy and safely store the App ID, App Secret and App Token that's shown to you.

![Workplace Keys](~/media/channels/fb-keys.png)

Now you have finished creating a custom integration. You can find the page of type 'Bot' in your Workplace community, as shown below.

![Workplace page](~/media/channels/fb-page.png)

### Provide Facebook credentials

In Azure portal, paste the **Facebook App ID**, **Facebook App Secret** and **Page Access Token** values copied from the Facebook Workplace previously. Instead of a traditional pageID, use the numbers following the integrations name on its **About** page. Similar to connecting a bot to Facebook Messenger, the webhooks can be connected with the credentials shown in Azure.

### Submit for review
Please refer to the **Connect a bot to Facebook Messenger** section and [Workplace Developer Documentation](https://developers.facebook.com/docs/workplace) for details.

### Make the App public and publish the Page
Please refer to the **Connect a bot to Facebook Messenger** section for details.

## Setting the API version

If you receive a notification from Facebook about deprecation of a certain version of the Graph API, go to [Facebook developers page](https://developers.facebook.com). Navigate to your bot’s **App Settings** and go to **Settings > Advanced > Upgrade API version**, then switch **Upgrade All Calls** to 3.0.

![API version upgrade](~/media/channels/facebook-version-upgrade.png)

## Sample code

For further reference the <a href="https://aka.ms/facebook-events" target="_blank">Facebook-events</a> sample bot can be used to explore the bot communication with Facebook Messenger.

## Also available as an adapter

This channel is also [available as an adapter](https://botkit.ai/docs/v4/platforms/facebook.html). To help you choose between an adapter and a channel, see [Currently available adapters](bot-service-channel-additional-channels.md#currently-available-adapters).
