---
title: Conversations within the Bot Builder SDK | Microsoft Docs
description: Describes what a conversation is within the Bot Builder SDK.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/15/2018
monikerRange: 'azure-bot-service-4.0'

---

A conversation contains a series of activities sent between a bot and a user and represents an interaction between one or more bots and either a _direct_ conversation with a specific user or a _group_ conversation with multiple users.
A bot communicates with a user by receiving activities from a user on a channel, sending activities to a user on a channel.

- Not all channels support group conversations. See [channels] for more information.
- Users are identified by a unique ID per channel, but the SDK does not provide greater identification. See [user identification] for more information.

The Bot Framework SDK defines a _conversation reference_ object, which identifies the conversastion, the bot and the user participating, and additional information.
The SDK also defines an _activiity_ object that fully describes the activity.
Every activity contains routing information, along with information about the channel, the conversation, the sender, the receiver, and more.

Effectively, a conversation is a series of activities between your bot and a specific user (or group) on a specific channel.
The conversation reference contains a _conversation_ object with an ID that identifies the conversation. This identifier is unique per channel. 
- The channel sets the ID when it starts the conversion.
- The bot cannot start a conversation; however, once it has a conversation ID, it can resume the existing conversation.

Your bot can send activities back to the user, either _proactively_, in response to internal logic, or _reactively_, in response to an activity from the user or channel.

## Types of conversations

On many levels, your bot is defined byt the types of conversations it can have with users. 
For information about bot design, see the topics in the [Design] section.

## See also

- Activities
- Bot Connector REST API
- Channels
- Context
- Conversation flow
- Proactive messaging
- State
- User identification
- Users
