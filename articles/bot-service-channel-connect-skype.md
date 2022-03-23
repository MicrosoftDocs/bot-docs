---
title: Connect a Bot Framework bot to Skype
description: Learn how to configure bots to connect to Skype and communicate with users via Skype.
keywords: skype, bot channels, configure skype, publish, connect to channels
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: mainguy
ms.service: bot-service
ms.topic: how-to
ms-custom: abs-meta-21q1
ms.date: 03/22/2022
---

# Connect a bot to Skype

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to configure a bot already connected to Skype.

>[!NOTE]
> As of October 31, 2019 the Skype channel no longer accepts new bot publishing requests. This means that you can continue to develop existing bots connected to the Skype channel, but your bot will be limited to 100 users. You will not be able to publish your bot to a larger audience. Read more about [why some features are not available in Skype anymore](https://support.skype.com/faq/fa12091/why-are-some-features-not-available-in-skype-anymore).

## Prerequisites

- An Azure subscription. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- An existing bot published to Azure and previously connected to Skype.

## Configure your bot in Azure

1. Open the [Azure portal](https://portal.azure.com/).
1. Open the Azure Bot resource blade for your bot.
1. Open **Channels** and select **Skype**.
1. Use the tabs to configure the channel:
    1. **Messaging** controls how your bot sends and receives massages in Skype.
    1. **Calling** controls whether calling is enabled and whether to use IVR functionality or Real Time Media functionality for calls.
    1. **Groups** controls whether your bot can be added to a group.
    1. Select **Save** and accept the **Terms of Service**.

## Web control embed code

To embed the bot into your website, you need an embed code. To get your embed code:

1. Go to the **Channels** blade for your bot resource.
1. Select **Get bot embed codes**.
1. Azure displays a list of **Bot Embed Codes**. Copy the embed code for **Skype**.
