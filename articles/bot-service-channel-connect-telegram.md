---
title: Create a bot for Telegram | Microsoft Docs
description: Learn how to configure a bot's connection to Telegram.
keywords: configure bot, Telegram, bot channel, Telegram bot, access token
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# Connect a bot to Telegram

You can configure your bot to communicate with people using the Telegram messaging app.

[!INCLUDE [Channel Inspector intro](~/includes/snippet-channel-inspector.md)]

## Visit the Bot Father to create a new Telegram bot

<a href="https://telegram.me/botfather" target="_blank">Create a new Telegram bot</a> using the Bot Father.

![Visit Bot Father](~/media/channels/tg-StepVisitBotFather.png)

## Create a new Telegram bot
To create a new Telegram bot, send command `/newbot`.

![Create new bot](~/media/channels/tg-StepNewBot.png)

### Specify a friendly name

Give the Telegram bot a friendly name.

![Give bot a friendly name](~/media/channels/tg-StepNameBot.png)

### Specify a username

Give the Telegram bot a unique username.

![Give bot a unique name](~/media/channels/tg-StepUsername.png)

### Copy the access token

Copy the Telegram bot's access token.

![Copy access token](~/media/channels/tg-StepBotCreated.png)

## Enter the Telegram bot's access token

Paste the token you copied previously into the **Access Token** field and click **Submit**.

## Enable the bot
Check **Enable this bot on Telegram**. Then click **I'm done configuring Telegram**.

When you have completed these steps, your bot will be successfully configured to communicate with users in Telegram.
