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
# Connect a bot to channels
A channel is the connection between the Bot Framework and communication apps. You configure a bot to connect to the channels you want it to be available on. For example, a business bot connected to the Bing channel will show up in Bing search results and people can interact with it on Bing. Connecting to channels is quick and easy in the [Bot Framework Portal](https://dev.botframework.com).

![Connect to channels](~/media/channels/connect-to-channels.png)

The channels include many popular services, such as [Bing](~/channels/channel-bing.md), [Cortana](~/channels/channel-cortana.md), [Facebook Messenger](~/thirdparty-channels/channel-facebook.md), [Kik](~/thirdparty-channels/channel-kik.md), and [Slack](~/thirdparty-channels/channel-slack.md), as well as several others. [Skype](https://dev.skype.com/bots) and Web Chat are pre-configured for you. 

You configure your bot to run on channels after you have [registered](~/portal-register-bot.md) your bot with the Bot Framework and [deployed](~/publish-bot-overview.md) it to the cloud. 

## Get started
For most channels, you must provide channel configuration information to run your bot on the channel. Most channels require that your bot have an account on the channel, and others, like Facebook Messenger, require your bot to have an application registered with the channel also.

To configure your bot to connect to a channel, complete the following steps:

1. Sign in to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Portal</a>.
2. Click **My bots**. 
3. Select the bot that you want to configure.
4. Click the **Channels** tab.
5. Under **Add channel** on the bot dashboard, click the channel to add.

After you've configured the channel, users on that channel can start using your bot.

## Publish a bot
The publishing process is different for each channel. If the channel requires a review, ensure that the bot meets the [review guidelines](~/portal-bot-review-guidelines.md) before submitting it for publishing.

[!include[publishing](~/includes/snippet-publish-to-channel.md)]

