---
title: Configure a bot to run on one or more channels using the Bot Framework Portal | Microsoft Docs
description: Learn how to configure a bot to run on one or more channels by using the Bot Framework Portal.
keywords: Bot Framework, Bot Builder, configure bot, Bot Framework Portal, channel, configure
author: kbrandl
manager: rstand
ms.topic: publish-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/06/2017
ms.reviewer:
#ROBOTS: Index
---

# Configure a bot to run on one or more channels

After you have [registered](~/portal-register-bot.md) your bot with the Bot Framework and [deployed](~/publish-bot-overview.md) your bot to the cloud, 
you can configure it to run on one or more channels. 

## Overview
For most channels, you must provide channel configuration information to the framework to run your bot on the channel. 
For example, most channels require that your bot have an account on the channel, and others require your bot to 
also have an application registered with the channel (for example, Facebook, GroupMe, and others).

When you register a bot with the Bot Framework, the following channels are automatically pre-configured:

- Skype
- Web Chat

> [!TIP]
> To take full advantage of Skype, you must [publish](~/portal-submit-bot-directory.md) your bot to Bot Directory.

## Configure your bot to run on another channel

To configure your bot to run on another channel, complete the following steps:

1. Sign in to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Portal</a>.
2. Click **My bots**. 
3. Select the bot that you want to configure.
4. Under **Add another channel** on the bot dashboard, click **Add** next to the channel on which you want to enable your bot.
5. Complete the configuration steps. After you have configured the channel, it will appear under **Channels** on the bot dashboard. 

After you've configured the channel, users on that channel can start using your bot.

> [!TIP]
> To change a bot's configuration for a channel or to disable a bot on a channel, click **Edit** within the channel's tile under **Channels**. 

## Next steps

After you've configured your bot to run on the channels where your users are, the next (and final) step in the bot publication process is to [publish](~/portal-submit-bot-directory.md) your bot to Bot Directory. 





