---
title: Bot Builder SDK concepts | Microsoft Docs
description: Bot Builder SDK key concepts.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 03/28/2018
monikerRange: 'azure-bot-service-4.0'
---

# Key concepts

On top of the fundamentals [already presented](bot-builder-basics.md), Azure Bot Service and the Bot Builder SDK v4 involve a few other ideas that warrant explanation. These concepts still apply regardless of whether you're developing in C#, JavaScript, Java, or Python.

## Channels and the Bot Connector Service

Channels are the endpoint that a user is connecting to our bot from, such as Facebook, Skype, Email, Slack, etc. The Bot Connector Service, configured through the [Azure portal](https://portal.azure.com), connects your bot to these channels and facilitates communication between your bot and the user. 

In addition to standard channels provided with the Bot Connector Service, you can also connect your bot to your own client application using [Direct Line]() as your channel.

The Bot Connector Service allows you to develop your bot in a channel-agnostic way by normalizing messages that the bot sends to a channel. This involves converting it from the bot builder schema into the channel’s schema. However, if the channel does not support all aspects of the bot builder schema, the service will try to convert the message to a format that the channel does support. For example, if the bot sends a message that contains a card with action buttons to the SMS channel, the connector may send the card as an image and include the actions as links in the message’s text.

## Activities and conversations

The Bot Connector Service uses JSON to exchange information between the bot and the user, and the Bot Builder SDK wraps this information in a language-specific activity object. [Activity types]() were mentioned in when discussing [interaction with your bot](bot-builder-basics.md#interaction-with-your-bot), with the most common type of activity is a message, but the other activity types are important too. These include a conversation update, contact relation update, delete user data, end of conversation, typing, message reaction, and a couple other bot specific activites that the user will likely never see. Details on each of these and when they happen can be found in our activity reference content.

Every turn and it's associated activity belongs to a logical **conversation**, which represents an interaction between one or more bots and a specific user or group of users. A conversation is specific to a channel and has an ID that is unique to that channel.

## State and storage

Depending on what your bot is used for, you may need to keep track of state or store information for longer than the lifetime of the bot. Knowing certain state, such as user state or conversation state, allows your bot to remember the questions it's been asked or give information tailored to that specific user. Having that knowledge available gives our bot a more natural way of conversing, and provides a way to have more interesting conversations.

[Storage](bot-builder-storage-concept.md) is available in multiple ways including a database, Azure Table storage, and to a local file. Being able to read and write from persistent storage enables your bot to do things such as update shared resources, record RSVPs or votes, or read historical weather data. In the same way an app uses storage to achieve it's objectives, your bot can do so within the conversation with your user.

## Next steps

Now that you're familiar with some key concepts of a bot, let's dive into the different forms of conversation our bot may use.

> [!div class="nextstepaction"]
> [Conversation forms](bot-builder-conversations.md)
