---
title: Connect a bot to Slack | Microsoft Docs
description: Learn how to configure a bot's connection to Slack.
author: RobStand
ms.author: rstand
manager: tomlm
ms.topic: article
ms.prod: bot-framework
ms.date: 06/12/2017
---

# Connect a bot to Slack
[!include[Channel Inspector intro](~/includes/snippet-channel-inspector.md)]

You can configure your bot to communicate with people using the Slack messaging app.

## Log in to Slack and create a Slack Application for your bot


## Log in to Slack and create a Slack Application

Log into Slack and [create a Slack application](https://api.slack.com/applications/new).

![Set up bot](~/media/channels/slack-NewApp.png)

## Create an app and assign a Development Slack team

Enter an App Name and select a Development Slack Team. If you are not already a member of a Development Slack Team, [create or join one](https://slack.com/).

![Create app](~/media/channels/slack-CreateApp.png)

Click Create App. Slack will create your app and generate a Client ID and Client Secret.

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

Select **Add Bot User** to validate your settings, and then select **Save Changes**.

![Create bot](~/media/channels/slack-CreateApp-AddBotUser.png)

## Add and Configure Interactive Messages (optional)

If your bot will use Slack-specific functionality such as buttons, select the **Interactive Messages** tab and enable interactive messages.

![Enable messages](~/media/channels/slack-EnableMessages.png)

1. Enter https://slack.botframework.com/api/Actions as the Request URL for Interactive Messages.
2. Click the **Enable Interactive Messages** button and **Save changes** once the URL is successfully validated.

![Enable messages](~/media/channels/slack-MessageURL.png)

## Gather credentials
Select the **Basic Information** tab, and scroll to the **App Credentials** section. The Client ID, Client Secret and optional Verification Token required for configuration of your Slack bot are displayed.

![Gather credentials](~/media/channels/slack-AppCredentials.png)

## Submit credentials


In a separate browser window, return to the Bot Framework site at http://dev.botframework.com/.

1. Select **My bots** and choose the Bot that you want to connect to Slack.
2. In the **Add a channel** section, click the Slack icon.
3. In the **Submit your Credentials** section, paste the App Credentials from the Slack website into the appropriate fields.
4. The **Landing Page URL** is optional. You may remove or change it.
5. Click **Submit Slack Credentials**.

![Submit credentials](~/media/channels/slack-SubmitCredentials.png)

Follow the instructions to authorize your Slack app's access to your Development Slack Team.

## Enable the bot
Check **Enable this bot on Slack**. Then click **I'm done configuring Slack**.

When you have completed these steps, your bot will be successfully configured to communicate with users in Slack.
