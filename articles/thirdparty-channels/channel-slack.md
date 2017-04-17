---
title: Connect a bot to Slack | Microsoft Docs
description: Learn how to configure a bot's connection to Slack.
author: JaymeMPerlman
ms.author: v-jaype
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date:
ms.reviewer:
ROBOTS: Index, Follow
---
# Connect a bot to Slack

Configure a bot to communicate using the Slack messaging app.

## Log in to Slack and create a Slack Application

https://api.slack.com/applications/new 

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

![Paste credentials](~/media/channels/slack-creds.png)

Click **Submit Slack Credentials**.

Check **Enable this bot on Slack**.

Click **I'm done configuring Slack**

