---
title: Connect a bot to GroupMe | Microsoft Docs
description: Learn how to configure a bot's connection to GroupMe.
author: JaymeMPerlman
ms.author: v-jaype
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date:
ms.reviewer:

---
# Connect a bot to GroupMe

Configure a bot to communicate using the GroupMe group messaging app.

## Sign up for a GroupMe account

https://web.groupme.com/signup 

## Create a GroupMe application

https://dev.groupme.com/applications/new 

Use this callback URL: <code>https://groupme.botframework.com/Home/Login</code>

<!-- verify that this is the correct callback for everyone or if they need to specify something specific because reasons-->

![Create app](~/media/channels/GM-StepApp.png)

## Gather credentials

Copy the **Client ID** section from the redirect URL.

![Copy client ID](~/media/channels/GM-StepClientId.png)

## Submit credentials

1. Paste the Client ID you just copied into the **Client ID** field.
2. Click **Submit GroupMe Credentials**.
![Enter credentials](~/media/channels/GM-StepApp.png)

## Enable the bot
Check **Enable this bot on GroupMe**. Click **I'm done configuring GroupMe**.

When you have completed these steps, your bot will be successfully configured to communicate with users in GroupMe.

