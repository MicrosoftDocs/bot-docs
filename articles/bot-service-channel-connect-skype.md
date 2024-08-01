---
title: Connect a Bot Framework bot to Skype
description: Learn how to configure bots to connect to Skype and communicate with users via Skype.
keywords: skype, bot channels, configure skype, publish, connect to channels
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: azure-ai-bot-service
ms.topic: how-to
ms-custom: abs-meta-21q1
ms.date: 03/30/2022
ms.custom:
  - evergreen
---

# Connect a bot to Skype

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to configure a bot already connected to Skype.

>[!NOTE]
> As of May 12, 2023 Skype bots are again supported and will be going forward.

## Prerequisites

- An Azure subscription. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- An existing bot published to Azure and previously connected to Skype.

## Configure your bot in Azure

1. Open the [Azure portal](https://portal.azure.com/).
1. Open the Azure Bot resource blade for your bot.
1. Open **Channels** and select **Skype**.
1. Use the tabs to configure the channel:
    1. **Messaging** controls how your bot sends and receives massages in Skype.
    1. **Calling** controls whether calling is enabled. <!--Requires a webhook.-->
    1. **Groups** controls whether your bot can be added to a group.
    1. Select **Save** and accept the **Terms of Service** to connect the Skype channel to your bot.

## Test your bot in Skype

1. Open the [Azure portal](https://portal.azure.com/).
1. Open the Azure Bot resource blade for your bot.
1. Open **Channels** and select the **Add to Skype** link. An **Add Bot to Contacts** page will open in your browser.
    1. Review the information on the page.
    1. Select **Add to Contacts**.
    1. If prompted, sign-in to Skype and select **Add to Contacts** again.
    1. If prompted, allow the site to open Skype.
1. Skype will open and display a page that describes your bot.
    1. Select **Get Started**. On the next page, select **Send message**.
    1. You can now interact with your bot in Skype.

## Get a Web control embed code

To embed the bot into your website, you need an embed code. To get your embed code:

1. Go to the **Channels** blade for your bot resource.
1. Select **Get bot embed codes**.
1. Azure displays a list of **Bot Embed Codes**. Copy the embed code for **Skype**.
