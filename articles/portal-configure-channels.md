---
title: Configure a bot to run on one or more channels | Microsoft Docs
description: Learn how to configure a bot to run on one or more channels using the Bot Framework Portal.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---

# Configure a bot to run on one or more channels

After you have [registered](~/portal-register-bot.md) your bot with the Bot Framework and [deployed](~/publish-bot-overview.md) your bot to the cloud, 
you can configure it to run on one or more channels. 

## Get started
For most channels, you must provide channel configuration information to the framework to run your bot on the channel. Most channels require that your bot have an account on the channel, and others, like Facebook Messenger, require your bot to have an application registered with the channel also.

When you register a bot with the Bot Framework, the following channels are automatically pre-configured:

- Skype
- Web Chat

## Configure your bot to connect to another channel

To configure your bot to connect to another channel, complete the following steps:

1. Sign in to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Portal</a>.
2. Click **My bots**. 
3. Select the bot that you want to configure.
4. Click the **Channels** tab.
5. Under **Add channel** on the bot dashboard, click the channel to add.

After you've configured the channel, users on that channel can start using your bot.

## Publish a bot
The publishing process is different for each channel. If the channel requires a review, ensure that the bot meets the [review guidelines](~/portal-bot-review-guidelines.md) before submitting it for publishing.


[!include[publishing](~/includes/snippet-publish-to-channel.md)]

