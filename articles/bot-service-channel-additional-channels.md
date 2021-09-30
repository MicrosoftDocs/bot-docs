---
title:  Additional channels in Bot Framework SDK
description: Learn about the various modalities to access bots via channels; specifically channel adapters and Azure channels. 
keywords: bot channels, hangouts, Twilio, facebook, azure portal
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 09/29/2021
---

# Additional channels

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

You can make your bot accessible in channels in two ways:

1. **Azure Bot Service channels**: make your bot available in channels with the Azure Bot Service. This works for all supported languages.
1. **Channel adapters**: make your bot available in channels with an adapter. The channel adapter translates between the Bot Framework activity schema and the native schema of a channel. The SDK, Botkit, and Bot Builder Community repos provide various channel adapters, which are language specific.

    1. The Bot Framework SDK repo lists many of the [available adapters](https://github.com/microsoft/botframework-sdk#channels-and-adapters), including Azure Bot Service channels and channel adapters. 
    1. The Botkit repo includes channel adapters, which they call [platform adapters](https://botkit.ai/docs/v4/platforms/). Botkit is an open source developer tool for building chat bots, apps and custom integrations for major messaging platforms.
    1. The [Bot Builder Community](https://github.com/BotBuilderCommunity/) repositories include channel adapters. View the README for each repo to see which channel adapters have been developed.

Some channels are accessible through Azure Bot Service or through an adapter. It's up to you when to use a channel versus an adapter.

## Currently available adapters

Each repository is responsible for maintaining the list of adapters and channels they support.


## When to use a channel adapter

1. Azure Bot Service does not support the channel you want.
1. Security and compliance requirements of your deployment dictate that you cannot rely on an outside service.
1. Depth of features that you need in a particular channel may not be supported.

## When to use an Azure channel

1. You require cross-channel compatibility, such that your bot should work on more than one of the available channels.
1. Built-in support. Microsoft maintains, patches, and seamlessly services each channel for you each time a third-party makes updates.
1. You want access to additional exclusive Microsoft channels, like quickly growing Microsoft Teams.
1. If you want to rely on a GUI interface to enable additional channels for your bot.
