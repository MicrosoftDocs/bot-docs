---
title: Connect a bot to GroupMe | Microsoft Docs
description: Learn how to configure a bot's connection to GroupMe.
author: JaymeMPerlman
ms.author: v-jaype
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 06/06/2017
---

# Connect a bot to GroupMe

You can configure a bot to communicate with people using the GroupMe group messaging app.

[!include[Channel Inspector intro](~/includes/snippet-channel-inspector.md)]

## Sign up for a GroupMe account

If you don't have a GroupMe account, [sign up for a new account](https://web.groupme.com/signup).

## Create a GroupMe application

[Create a GroupMe application](https://dev.groupme.com/applications/new) for your bot.

Use this callback URL: <code>https://groupme.botframework.com/Home/Login</code>

![Create app](~/media/channels/GM-StepApp.png)

## Gather credentials

Copy the **Client ID** section from the redirect URL.

![Copy client ID](~/media/channels/GM-StepClientId.png)

## Submit credentials

1. Paste the Client ID you just copied into the **Client ID** field.
2. Click **Submit GroupMe Credentials**.
![Enter credentials](~/media/channels/GM-StepApp.png)

## Enable the bot
Check **Enable this bot on GroupMe**. Then click **I'm done configuring GroupMe**.

When you have completed these steps, your bot will be successfully configured to communicate with users in GroupMe.


