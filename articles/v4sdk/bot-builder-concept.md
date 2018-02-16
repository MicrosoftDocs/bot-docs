---
title: Bot Builder SDK concepts | Microsoft Docs
description: Bot Builder SDK key concepts.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/14/2018
monikerRange: 'azure-bot-service-4.0'

---

# Key concepts in the Bot Builder SDK
This article introduces key concepts in the Bot Builder SDK.

## Connector
The connector uses the Bot Connector REST API to enable a bot to communicate across multiple channels such as Skype, Email, Slack, and more. It facilitates communication between bot and user by relaying messages from bot to channel. Your bot receives messages from users through a web service, and your bot's replies are sent to the channel through the connector.

The connector also normalizes the messages that the bot sends to channels so that you can develop your bot in a platform-agnostic way. Normalizing a message involves converting it from the bot schema into the channel’s schema. In cases where the channel does not support all aspects of the framework’s schema, the connector will try to convert the message to a format that the channel supports. For example, if the bot sends a message that contains a card with action buttons to the SMS channel, the connector may render the card as an image and include the actions as links in the message’s text. The Channel Inspector is a web tool that shows you how the connector renders messages on various channels.

## Adapter

The SDK provides an adapter that facilitates conversations between the bot and users and that mediates authentication of your bot with the Azure Bot Service. The adapter uses a connector to send REST API calls to the channels.
The bot's adapter receives incoming activities on its post method.

## Activity

The connector uses an activity object to pass information from your bot to the user. The most common type of activity is message, but there are other activity types that can be used to communicate various types of information to a channel. [Overview tbd]

## State

A key to good bot design is to track the context of a conversation, so that your bot remembers things like the last question the user asked.
If you store a user's preferences, you can use that information to customize the conversation the next time you chat. For example, you might alert the user to a news article about a topic that interests her, or alert a user when an appointment becomes available. 

Bots built using Bot Builder SDK are designed to be stateless so that they can easily be scaled to run across multiple compute nodes.
The SDK provides bot state manager middleware to persist data, and includes Azure Table Storage, file storage, and memory storage that you can use for data storage.
You can also create your own storage components for your bot.

![NOTE] File and memory storage won't scale across nodes.
You should generally avoid saving state using a global variable or function closures.
Doing so will create issues when you want to scale out your bot.
Instead, use the state property of your bot's context object to save data relative to a user or conversation.

## Context

When the bot receives an activity, it generates a context object that captures various parameters of the conversation. Each received activity is processed independently of other incoming activities. You can use the bot state manager to persist state between activities.

The context's state property provides user and conversation data. 
User data: tbd. 
Conversation data: tbd.

## Conversation object

Defines a conversation, including the bot and users that are included within the conversation.
The Conversation object is defined in the Bot Connector REST API.
When the bot generates context for an incoming activity, it includes the conversation information.



## Security

See[ authentication](#) and [user identification](#).

## Next steps

tbd.
