---
title: Connect a bot to Slack | Microsoft Docs
description: Learn how to configure a bot's connection to Slack.
author: JaymeMPerlman
ms.author: v-jaype
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 05/24/2017
ms.reviewer:
---

# Connect a bot to Slack

You can configure your bot to communicate with people using the Slack messaging app.

## Log in to Slack and create a Slack Application for your bot

Log into Slack and [create a Slack application](https://api.slack.com/applications/new).

![Set up bot](~/media/channels/slack-NewApp.png)

## Create application and assign a Development Slack team

Enter an App Name, and select a Development Slack Team. If you are not already a member of a Slack Team, [create or join one here](https://slack.com/)

![Create app](~/media/channels/slack-CreateApp.png)

Your App will be created, and a Client ID and Client Secret generated.

## Add a new Redirect URL

Select the **OAuth & Permissions** tab, select **Add a new Redirect URL**, enter https://slack.botframework.com then select **Add** and **Save URLs**

![Add Redirect URL](~/media/channels/slack-RedirectURL.png)

## Create a Slack Bot User

Select the **Bot Users** tab, then select **Add a Bot User**

![Create bot](~/media/channels/slack-CreateBot.png)

Adding a Bot User allows you to assign a username for your bot, and choose whether it is always shown as online or not. Select **Add Bot User** to validate your settings, and then select **Save Changes**. 

![Create bot](~/media/channels/slack-CreateBot-AddBotUser.png)

## Add and Configure Interactive Messages (optional)

If your bot will use Slack-specific functionality such as buttons, select the **Interactive Messages** tab and enable interactive messages.

![Enable messages](~/media/channels/slack-EnableMessages.png)

Set the Request URL for Interactive Messages to be https://slack.botframework.com/api/Actions, then select the **Enable Interactive Messages** button and  **Save changes** once the URL is successfully validated.

![Enable messages](~/media/channels/slack-MessageURL.png)

## Gather credentials
Select the **Basic Information** tab, and scroll to the **App Credentials** section. The Client ID, Client Secret and (optional) Verification Token required for configuration of your Slack bot are displayed here.

![Gather credentials](~/media/channels/slack-AppCredentials.png)

## Submit credentials

In a separate browser window, return to the Bot Framework site at http://dev.botframework.com/ 
Select **My bots**, and choose the Bot that you want to connect to Slack
In the **Add a channel** section, select the Slack icon
In the **Submit your Credentials** section, paste the App Credentials from the Slack website into the appropriate fields. 
The **Landing Page URL** is optional. You may remove or change it.

![Submit credentials](~/media/channels/slack-SubmitCredentials.png)

Click **Submit Slack Credentials**, and follow the instructions to authorize your Slack app's access to your Development Slack Team. 

## Enable the bot
Check **Enable this bot on Slack**. Then click **I'm done configuring Slack**.

When you have completed these steps, your bot will be successfully configured to communicate with users in Slack.

