---
title: Connect a bot to GroupMe | Microsoft Docs
description: Learn how to configure a bot's connection to GroupMe.
keywords: bot channel, GroupMe, create GroupMe, credentials
author: RobStand
ms.author: RobStand
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# Connect a bot to GroupMe

You can configure a bot to communicate with people using the GroupMe group messaging app.

[!INCLUDE [Channel Inspector intro](~/includes/snippet-channel-inspector.md)]

## Sign up for a GroupMe account

If you don't have a GroupMe account, [sign up for a new account](https://web.groupme.com/signup).

## Create a GroupMe application

[Create a GroupMe application](https://dev.groupme.com/applications/new) for your bot.

Use this callback URL: `https://groupme.botframework.com/Home/Login`

![Create app](~/media/channels/GM-StepApp.png)

## Gather credentials

1. In the **Redirect URL** field, copy the value after **client_id=**.
2. Copy the **Access Token** value.

![Copy client ID and access token](~/media/channels/GM-StepClientId.png)


## Submit credentials

1. On dev.botframework.com, paste the **client_id** value you just copied into the **Client ID** field.
2. Paste the **Access Token** value into the **Access Token** field.
2. Click **Save**.

![Enter credentials](~/media/channels/GM-StepClientIDToken.png)
