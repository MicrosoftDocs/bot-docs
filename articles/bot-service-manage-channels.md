---
title: Configure a bot to run on one or more channels | Microsoft Docs
description: Learn how to configure a bot to run on one or more channels using the Bot Framework Portal.
keywords: bot channels, configure, cortana, facebook messenger, kik, slack, skype, azure portal
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 12/13/2017

---

# Connect a bot to channels

A channel is the connection between the Bot Framework and communication apps. You configure a bot to connect to the channels you want it to be available on. For example, a bot connected to the Skype channel can be added to a contact list and people can interact with it in Skype. 

The channels include many popular services, such as [Cortana](bot-service-channel-connect-cortana.md), [Facebook Messenger](bot-service-channel-connect-facebook.md), [Kik](bot-service-channel-connect-kik.md), and [Slack](bot-service-channel-connect-slack.md), as well as several others. [Skype](https://dev.skype.com/bots) and Web Chat are pre-configured for you. 

Connecting to channels is quick and easy in the [Azure Portal](https://portal.azure.com).

## Get started

For most channels, you must provide channel configuration information to run your bot on the channel. Most channels require that your bot have an account on the channel, and others, like Facebook Messenger, require your bot to have an application registered with the channel also.

To configure your bot to connect to a channel, complete the following steps:

1. Sign in to the <a href="https://portal.azure.com" target="_blank">Azure Portal</a>.
1. Select the bot that you want to configure.
3. In the Bot Service blade, click **Channels** under **Bot Management**.
4. Click the icon of the channel you want to add to your bot.

![Connect to channels](~/media/channels/connect-to-channels.png)

After you've configured the channel, users on that channel can start using your bot.

## Publish a bot

The publishing process is different for each channel.

[!INCLUDE [publishing](~/includes/snippet-publish-to-channel.md)]

