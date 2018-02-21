---
title: Conversations within the Bot Builder SDK | Microsoft Docs
description: Describes what a conversation is within the Bot Builder SDK.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/20/2018
monikerRange: 'azure-bot-service-4.0'

---

A conversation contains a series of activities sent between a bot and a user and represents an interaction between one or more bots and either a _direct_ conversation with a specific user or a _group_ conversation with multiple users.
A bot communicates with a user on a channel by receiving activities from, and sending activities to the user.
The underlying structure of conversations and activities is defined by the Bot Connector and Direct Line REST APIs.

- Not all channels support group conversations.
- Each user has an ID that is unique per channel.
- Each conversation has an ID that is unique per channel.
- The channel sets the conversation ID when it starts the conversion.
- The bot cannot start a conversation; however, once it has a conversation ID, it can resume that conversation.

The SDK defines _conversation reference_ and _activity_ objects.
The conversation reference identifies the conversastion (via a _conversation account_ object), the bot and the user participating, and additional information.
The activiity object describes the activity, including routing information, along with information about the channel, the conversation, the sender, the receiver, and more.

## Conversation lifetime

A bot receives a `conversationUpdate` activity whenever it has been added to a conversation, other members have been added to or removed from a conversation, or conversation metadata has changed.
A bot receives an `endOfConversation` activity to indicate that the user has ended the conversation.
A bot may send an `endOfConversation` activity to indicate to the user that the conversation is ending.

## Types of conversations

Your bot can send activities back to the user, either _proactively_, in response to internal logic, or _reactively_, in response to an activity from the user or channel.

On many levels, your bot is defined byt the types of conversations it can have with users. 
For information about bot design, see [Conversation flow] and other topics in the [Design] section.

## See also

- Activities
- Bot Connector REST API
- Channels
- Context
- Conversation flow
- Proactive messaging
- State
- Users
