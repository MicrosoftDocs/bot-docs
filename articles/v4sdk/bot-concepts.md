---
title: Channels and the Bot Connector Service | Microsoft Docs
description: Bot Builder SDK key concepts.
keywords: activities, conversation
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 03/28/2018
monikerRange: 'azure-bot-service-4.0'
---

# Channels and the Bot Connector Service

Channels are the endpoint that a user is connecting to our bot from, such as Facebook, Skype, Email, Slack, etc. The Bot Connector Service, configured through the [Azure portal](https://portal.azure.com), connects your bot to these channels and facilitates communication between your bot and the user. 

In addition to standard channels provided with the Bot Connector Service, you can also connect your bot to your own client application using [Direct Line](bot-builder-howto-direct-line.md) as your channel.

The Bot Connector Service allows you to develop your bot in a channel-agnostic way by normalizing messages that the bot sends to a channel. This involves converting it from the bot builder schema into the channel’s schema. However, if the channel does not support all aspects of the bot builder schema, the service will try to convert the message to a format that the channel does support. For example, if the bot sends a message that contains a card with action buttons to the SMS channel, the connector may send the card as an image and include the actions as links in the message’s text.

## Activities and conversations

The Bot Connector Service uses JSON to exchange information between the bot and the user, and the Bot Builder SDK wraps this information in a language-specific activity object. [Activity types](../bot-service-activities-entities.md) were mentioned in when discussing [interaction with your bot](bot-builder-basics.md#interaction-with-your-bot), with the most common type of activity is a message, but the other activity types are important too. These include a conversation update, contact relation update, delete user data, end of conversation, typing, message reaction, and a couple other bot specific activites that the user will likely never see. Details on each of these and when they happen can be found in our activity reference content.

Every turn and it's associated activity belongs to a logical **conversation**, which represents an interaction between one or more bots and a specific user or group of users. A conversation is specific to a channel and has an ID that is unique to that channel.

## Next steps

Now that you're familiar with some key concepts of a bot, let's dive into the different forms of conversation our bot may use.

> [!div class="nextstepaction"]
> [Conversation forms](bot-builder-conversations.md)
