---
title: Connect a bot to Direct Line | Microsoft Docs
description: Learn how to configure a bot's connection to Direct Line.
keywords: direct line, bot channels, custom client, connect to channels, configure
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 10/11/2018
---

# Connect a bot to Direct Line

You can enable your own client application to communicate with your bot by using the Direct Line channel. 

## Add the Direct Line channel

To add the Direct Line channel, open the bot in the [Azure Portal](https://portal.azure.com/), click the **Channels** blade, and then click **Direct Line**.

![Add Direct Line channel](~/media/bot-service-channel-connect-directline/directline-addchannel.png)

## Add new site

Next, add a new site that represents the client application that you want to connect to your bot. Click **Add new site**, enter a name for your site, and click **Done**.

![Add Direct Line site](~/media/bot-service-channel-connect-directline/directline-addsite.png)

## Manage secret keys

When your site is created, the Bot Framework generates secret keys that your client application can use to [authenticate](~/rest-api/bot-framework-rest-direct-line-3-0-authentication.md) the Direct Line API requests that it issues to communicate with your bot. To view a key in plain text, click **Show** for the corresponding key.

![Show Direct Line key](~/media/bot-service-channel-connect-directline/directline-showkey.png)

Copy and securely store the key that is shown. Then use the key to [authenticate](~/rest-api/bot-framework-rest-direct-line-3-0-authentication.md) the Direct Line API requests that your client issues to communicate with your bot.
Alternatively, use the Direct Line API to [exchange the key for a token](~/rest-api/bot-framework-rest-direct-line-3-0-authentication.md#generate-token) that your client can use to authenticate its subsequent requests within the scope of a single conversation.

![Copy Direct Line key](~/media/bot-service-channel-connect-directline/directline-copykey.png)

## Configure settings

Finally, configure settings for the site.

- Select the Direct Line protocol version that your client application will use to communicate with your bot.

> [!TIP]
> If you are creating a new connection between your client application and bot, use Direct Line API 3.0.

When finished, click **Done** to save the site configuration. You can repeat this process, beginning with [Add new site](#add-new-site), for each client application that you want to connect to your bot.
