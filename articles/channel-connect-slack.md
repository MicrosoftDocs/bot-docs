---
title: Connect a bot to Slack | Microsoft Docs
description: Learn how to configure a bot's connection to Slack.
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 06/06/2017
---

# Connect a bot to Slack

You can configure your bot to communicate with people using the Slack messaging app.

[!include[Channel Inspector intro](~/includes/snippet-channel-inspector.md)]

## Log in to Slack and create a Slack Application

Log into Slack and [create a Slack application](https://api.slack.com/applications/new).

![Set up bot](~/media/channels/slack-NewApp.png)

## Create application and set redirect URI

![Create app](~/media/channels/slack-CreateApp.png)

## Create a Slack Bot

![Create bot](~/media/channels/slack-CreateBot.png)

## Add Interactive Messages (optional)

If your bot will use buttons, select the **Interactive Messages** tab and enable interactive messages.

![Enable messages](~/media/channels/slack-EnableMessages.png)

## Configure Interactive Messages (optional)

Set the Request URL for Interactive Messages.

![Enable messages](~/media/channels/slack-MessageURL.png)

## Gather credentials
1. Select the **Add Credentials** tab 
2. Copy the **Client Id** and **Client Secret**.
3. If you enabled buttons, also copy the **Verification Token for Buttons**.

![Gather credentials](~/media/channels/slack-StepAuth.png)

## Submit credentials

Paste the credentials you copied previously into the appropriate field.
The **Landing Page URL** is optional. You may remove or change it.

Click **Submit Slack Credentials**.

## Enable the bot
Check **Enable this bot on Slack**. Then click **I'm done configuring Slack**.

When you have completed these steps, your bot will be successfully configured to communicate with users in Slack.


