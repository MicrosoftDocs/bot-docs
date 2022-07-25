---
title: Connect a Bot Framework bot to Telegram
description: Learn how to configure your bot to use the Telegram messaging app to communicate with people.
keywords: configure bot, Telegram, bot channel, Telegram bot, access token
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: mainguy
ms.service: bot-service
ms.topic: how-to
ms.custom: abs-meta-21q1
ms.date: 03/22/2022
---

# Connect a bot to Telegram

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

You can configure your bot to communicate with people using the Telegram messaging app. This article describes how create a Telegram bot and connect it to your bot in the Azure portal.

[!INCLUDE [Channel Inspector intro](includes/snippet-channel-inspector.md)]

## Prerequisites

- An Azure account. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- An existing bot published to Azure.
- A device with [Telegram](https://telegram.org/) installed and a Telegram account.

## Create a new Telegram bot with BotFather

Create a Telegram bot with BotFather before connecting your bot to Telegram.

1. Start a new conversation with the [BotFather](https://telegram.me/botfather).

    :::image type="content" source="media/channels/tg-StepVisitBotFather.png" alt-text="Visit Bot Father":::

1. Send `/newbot` to create a new Telegram bot.
1. When asked, enter a name for the bot.
1. Give the Telegram bot a unique username. Note that the bot name must end with the word "bot" (case-insensitive).
1. Copy and save the Telegram bot's access token for later steps.

## Configure Telegram in the Azure portal

Now that you have an access token, you can configure your bot in the Azure portal to communicate with Telegram.

1. Log in to the [Azure](https://portal.azure.com) portal.
1. Go to your bot. Then select **Channels** from **Settings**.
1. Select **Telegram** from the list of **Available Channels**.
1. Enter the token you copied previously into the **Access Token** field and select **Apply**.

Your bot's now successfully configured to communicate with users in Telegram.

## Additional information

For information about using Telegram-specific actions in messages, see how to [Implement channel-specific functionality](v4sdk/bot-builder-channeldata.md).
