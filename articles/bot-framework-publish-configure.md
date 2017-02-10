---
title: Bot Framework Configure Bot | Microsoft Docs
description: Detailed walkthrough of how to configure a bot to run on one or more channels using the Bot Framework Developer Portal.
services: Bot Framework
documentationcenter: BotFramework-Docs
author: kbrandl
manager: rstand

ms.service: Bot Framework
ms.topic: article
ms.workload: Cognitive Services
ms.date: 02/06/2017
ms.author: v-kibran@microsoft.com

---
# Configure a bot to run on one or more channels

After you have [registered](bot-framework-publish-register.md) your bot with the Bot Framework and [deployed](bot-framework-publish-deploy.md) your bot to the cloud, 
you can configure it to run on one or more channels. 

> [!NOTE]
> For detailed information about Channels in the Bot Framework, see [TBD-TOPIC-IN-KEY-CONCEPTS-SECTION].

## Overview
For most channels, you must provide channel configuration information to the framework in order to run your bot on the channel. 
For example, most channels require that your bot have an account on the channel, and others require your bot to 
also have an application registered with the channel (for example, Facebook, GroupMe, and others).

When you register a bot with the Bot Framework, the following channels are automatically pre-configured:

- Skype
- Web Chat

> [!TIP]
> To take full advantage of Skype, you must [publish](bot-framework-publish-add-to-directory.md) your bot to Bot Directory.

## Configure your bot to run on another channel

To configure your bot to run on another channel, complete the following steps:

1. Sign in to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Developer Portal</a>.
2. Click **My bots**. 
3. Select the bot that you want to configure.
4. Under **Add another channel** on the bot dashboard, click **Add** next to the channel on which you want to enable your bot.
5. Complete the configuration steps. After you have configured the channel, it will appear under **Channels** on the bot dashboard. 

After you've configured the channel, users on that channel can start using your bot.

> [!TIP]
> To change a bot's configuration for a channel or to disable a bot on a channel, click **Edit** within the channel's tile under **Channels**. 

## Next steps

After you've configured your bot to run on the channels where your users are, the next (and final) step in the bot publication process is to [publish](bot-framework-publish-add-to-directory.md) your bot to Bot Directory. 





