---
title: Create a bot for Telegram - Azure Bot Service
description: Learn how to configure bots to use the Telegram messaging app to communicate with people.
keywords: configure bot, Telegram, bot channel, Telegram bot, access token
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: how-to
ms.service: bot-service
ms.custom: abs-meta-21q1
ms.date: 12/13/2017
---

# Connect a bot to Telegram

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

You can configure your bot to communicate with people using the Telegram messaging app.

[!INCLUDE [Channel Inspector intro](includes/snippet-channel-inspector.md)]

## Visit the Bot Father to create a new Telegram bot

Create a new [Telegram bot](https://telegram.me/botfather) using the Bot Father.

![Visit Bot Father](media/channels/tg-StepVisitBotFather.png)

## Create a new Telegram bot

To create a new Telegram bot, send command `/newbot`.

![Create new bot](media/channels/tg-StepNewBot.png)

### Specify a friendly name

Give the Telegram bot a friendly name.

![Give bot a friendly name](media/channels/tg-StepNameBot.png)

### Specify a username

Give the Telegram bot a unique username.

![Give bot a unique name](media/channels/tg-StepUsername.png)

### Copy the access token

Copy the Telegram bot's access token.

![Copy access token](media/channels/tg-StepBotCreated.png)

## Enter the Telegram bot's access token

Login to the Azure Portal.  Go to your bot or [create a new Azure Bot](/quickstart-create-bot-with-azure).

Go to your bot's **Channels** section in the Azure portal and select **Telegram**.

> [!NOTE]
> The Azure portal UI will look slightly different if you have already connected your bot to Telegram.

![Select Telegram in channels](media/channels/tg-connectBot-Azure.png)

Paste the token you copied previously into the **Access Token** field and click **Save**.

![Telegram access token](media/channels/tg-accessToken-Azure.png)

Your bot is now successfully configured to communicate with users in Telegram.

![Telegram bot enabled](media/channels/tg-botEnabled-Azure.png)
