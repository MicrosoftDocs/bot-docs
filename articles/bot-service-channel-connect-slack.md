---
title: Connect a bot to Slack | Microsoft Docs
description: Learn how to configure a bot's connection to Slack.
keywords: connect a bot, bot channel, Slack bot, Slack messaging app
author: JonathanFingold
ms.author: v-jofing
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 01/09/2019
---

# Connect a bot to Slack

You can configure your bot to communicate with people using the Slack messaging app.

## Create a Slack Application for your bot

Log into [Slack](https://slack.com/signin) and then go to [create a Slack application](https://api.slack.com/apps) channel.

![Set up bot](~/media/channels/slack-NewApp.png)

## Create an app and assign a Development Slack team

Enter an App Name and select a Development Slack Team. If you are not already a member of a Development Slack Team, [create or join one](https://slack.com/).

![Create app](~/media/channels/slack-CreateApp.png)

Click **Create App**. Slack will create your app and generate a Client ID and Client Secret.

## Add a new Redirect URL

Next you will add a new Redirect URL.

1. Select the **OAuth & Permissions** tab.
2. Click **Add a new Redirect URL**.
3. Enter https://slack.botframework.com.
4. Click **Add**.
5. Click **Save URLs**.

![Add Redirect URL](~/media/channels/slack-RedirectURL.png)

## Create a Slack Bot User

Adding a Bot User allows you to assign a username for your bot and choose whether it is always shown as online.

1. Select the **Bot Users** tab.
2. Click **Add a Bot User**.

![Create bot](~/media/channels/slack-CreateBot.png)

Click **Add Bot User** to validate your settings, click **Always Show My Bot as Online** to **On**, and then click **Save Changes**.

![Create bot](~/media/channels/slack-CreateApp-AddBotUser.png)

## Subscribe to Bot Events

Follow these steps to subscribe to six particular bot events. By subscribing to bot events, your app will be notified of user activities at the URL you specify.

> [!TIP]
> Your bot handle is the name of your bot. To find a bot's handle,
> visit [https://dev.botframework.com/bots](https://dev.botframework.com/bots),
> choose a bot, and record the name of the bot.

1. Select the **Event Subscriptions** tab.
2. Click **Enable Events** to **On**.
3. In **Request URL**, enter `https://slack.botframework.com/api/Events/{YourBotHandle}`, where `{YourBotHandle}` is your bot handle, without the braces. The bot handle used in this example is **ContosoBot**.

   ![Subscribe Events: top](~/media/channels/slack-SubscribeEvents-a.png)

4. In **Subscribe to Bot Events**, click **Add Bot User Event**.
5. In the list of events, select these six event types:
    * `member_joined_channel`
    * `member_left_channel`
    * `message.channels`
    * `message.groups`
    * `message.im`
    * `message.mpim`

   ![Subscribe Events: middle](~/media/channels/slack-SubscribeEvents-b.png)

6. Click **Save Changes**.

   ![Subscribe Events: bottom](~/media/channels/slack-SubscribeEvents-c.png)

## Add and Configure Interactive Messages (optional)

If your bot will use Slack-specific functionality such as buttons, follow these steps:

1. Select the **Interactive Components** tab and click **Enable Interactive Components**.
2. Enter `https://slack.botframework.com/api/Actions` as the **Request URL**.
3. Click the **Save changes** button.

![Enable messages](~/media/channels/slack-MessageURL.png)

## Gather credentials

Select the **Basic Information** tab and scroll to the **App Credentials** section.
The Client ID, Client Secret, and Verification Token required for configuration of your Slack bot are displayed.

![Gather credentials](~/media/channels/slack-AppCredentials.png)

## Submit credentials

In a separate browser window, return to the Bot Framework site at `https://dev.botframework.com/`.

1. Select **My bots** and choose the Bot that you want to connect to Slack.
2. In the **Channels** section, click the Slack icon.
3. In the **Enter your Slack credentials** section, paste the App Credentials from the Slack website into the appropriate fields.
4. The **Landing Page URL** is optional. You may omit or change it.
5. Click **Save**.

![Submit credentials](~/media/channels/slack-SubmitCredentials.png)

Follow the instructions to authorize your Slack app's access to your Development Slack Team.

## Enable the bot

On the Configure Slack page, confirm the slider by the Save button is set to **Enabled**.
Your bot is configured to communicate with users in Slack.

## Create an Add to Slack button

Slack provides HTML you can use to help Slack users find your bot in the
*Add the Slack button* section of [this page](https://api.slack.com/docs/slack-button).
To use this HTML with your bot, replace the href value (begins with `https://`) with the URL found in your bot's Slack channel settings.
Follow these steps to get the replacement URL.

1. On [https://dev.botframework.com/bots](https://dev.botframework.com/bots), click your bot.
2. Click **Channels**, right-click the entry named **Slack**, and click **Copy link**. This URL is now in your clipboard.
3. Paste this URL from your clipboard into the HTML provided for the Slack button. This URL replaces the href value provided by Slack for this bot.

Authorized users can click the **Add to Slack** button provided by this modified HTML to reach your bot on Slack.

## Also available as an adapter

This channel is also [available as an adapter](https://botkit.ai/docs/v4/platforms/slack.html). To help you choose between an adapter and a channel, see [Currently available adapters](bot-service-channel-additional-channels.md#currently-available-adapters).
