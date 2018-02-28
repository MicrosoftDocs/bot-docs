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
On many levels, your bot is defined by the types of conversations it can have with users.

A conversation identifies a series of activities sent between a bot and a user on a specific channel and represents an interaction between one or more bots and either a _direct_ conversation with a specific user or a _group_ conversation with multiple users.
A bot communicates with a user on a channel by receiving activities from, and sending activities to the user.

- Each user has an ID that is unique per channel.
- Each conversation has an ID that is unique per channel.
- The channel sets the conversation ID when it starts the conversion.
- The bot cannot start a conversation; however, once it has a conversation ID, it can resume that conversation.
- Not all channels support group conversations.

When it processes an incoming activity, the _bot adapter_ uses a _context_ object to pass the conversation information to your middleware and your application's receive callback method.
- The context's _conversation reference_ property identifies the conversastion, and includes information about the bot and the user participating in the conversation.
- The context's _activity_ property describes the activity, including routing information, along with information about the channel, the conversation, the sender, and the receiver.

## Conversation lifetime

A bot receives a _conversation update_ activity whenever it has been added to a conversation, other members have been added to or removed from a conversation, or conversation metadata has changed.
You may want to have your bot react to converstation update activities by greeting users or introducing itself.

A bot receives an _end of conversation_ activity to indicate that the user has ended the conversation. A bot may send an _end of conversation_ activity to indicate to the user that the conversation is ending. 
If you are storing information about the conversation, you may want to clear that information when the conversation ends.

## Types of conversations

Your bot can support multi-turn interactions where it prompts users for multiple peices of information. It can be focused on a very specific task or support multiple types of tasks. 
The Bot Builder SDK has some built-in support for Language Understatnding (LUIS) and QnA Maker for adding natural language "question and answer" features to your bot.

<!--TODO: Add with links when these topics are available:
[Conversation flow] and other design articles.
[Using recognizers] [Using state and storage] and other how tos.
-->

In addition, your bot can send activities back to the user, either _proactively_, in response to internal logic, or _reactively_, in response to an activity from the user or channel.
<!--TODO: Link to messaging how tos.-->

## See also

- Activities
- Adapter
- Bot Connector Service
- Channels
- Context
- Conversation flow
- Proactive messaging
- State
- Users
