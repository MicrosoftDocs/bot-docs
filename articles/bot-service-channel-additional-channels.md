---
title:  Additional channels - Bot Service
description: Learn how to connect bots to Webex Teams, Websocket, Webhooks, Google Hangouts, Google Assistant, and Amazon Alexa. Compare the use of adapters and channels.
keywords: bot channels, hangouts, Twilio, facebook, azure portal
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 02/08/2019
---

# Additional channels

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

Besides the channels listed in within these docs, there are a few additional channels available as an adapter, both through our [provided platforms](https://botkit.ai/docs/v4/platforms/) via Botkit, or accessible through the [community repositories](https://github.com/BotBuilderCommunity/). Below are the additional channels provided:

- [Webex Teams](https://botkit.ai/docs/v4/platforms/webex.html)
- [Websocket and Webhooks](https://botkit.ai/docs/v4/platforms/web.html)
- [Google Hangouts and Google Assistant](https://github.com/BotBuilderCommunity/) (available through community)
- [Amazon Alexa](https://github.com/BotBuilderCommunity/) (available through community)

## Currently available adapters

See [Platform Adapters](https://botkit.ai/docs/v4/platforms/) for complete list of available adapters. You'll notice some channels are available also as an adapter, and it's up to you when to use a channel versus an adapter.

### When to use an adapter

1. The service does not support the channel you want.
2. Security and compliance requirements of your deployment dictate that you cannot rely on an outside service.
3. Depth of features that you need in a particular channel may not be supported.

### When to use a channel

1. You require cross-channel compatibility: your bot should work on more than one of the available channels.
2. Built in support: Microsoft maintains, patches, and seamlessly services each channel for you each time a third-party makes updates.
3. Allows access to additional exclusive Microsoft channels, like quickly growing Microsoft Teams.
4. If you want to rely on a GUI interface to enable additional channels for your bot.
