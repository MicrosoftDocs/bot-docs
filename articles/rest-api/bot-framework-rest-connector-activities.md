---
title: Activities overview  | Microsoft Docs
description: Learn about the different activity types available within the Bot Connector service. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# Activities overview

The Bot Connector service exchanges information between bot and channel (user) by passing an [Activity][Activity] object. The most common type of activity is **message**, but there are other activity types that can be used to communicate various types of information to a bot or channel. 

## Activity types in the Bot Connector service

The following activity types are supported by the Bot Connector service.

| Activity type | Description |
|------|------|------|
| message | Represents a communication between bot and user. |
| conversationUpdate | Indicates that the bot was added to a conversation, other members were added to or removed from the conversation, or conversation metadata has changed. |
| contactRelationUpdate | Indicates that the bot was added or removed from a user's contact list. |
| typing | Indicates that the user or bot on the other end of the conversation is compiling a response. | 
| deleteUserData | Indicates to a bot that a user has requested that the bot delete any user data it may have stored. |
| endOfConversation | Indicates the end of a conversation. |

## message

Your bot will send **message** activities to communicate information to and receive **message** activities from users. 
Some messages may be plain text, while others may contain richer content such as 
[media attachments](bot-framework-rest-connector-add-media-attachments.md), [buttons, and cards](bot-framework-rest-connector-add-rich-cards.md) or 
[channel-specific data](bot-framework-rest-connector-channeldata.md). 
For information about commonly-used message properties, see [Create messages](bot-framework-rest-connector-create-messages.md). For information about how to send and receive messages, see [Send and receive messages](bot-framework-rest-connector-send-and-receive-messages.md). 

## conversationUpdate

A bot receives a **conversationUpdate** activity whenever it has been added to a conversation, 
other members have been added to or removed from a conversation, 
or conversation metadata has changed. 

If members have been added to the conversation, the activity's `addedMembers` property will identify the new members. If members have been removed from the conversation, the `removedMembers` property will identify the removed members. 

> [!TIP]
> If your bot receives a **conversationUpdate** activity indicating that a user has joined the conversation, 
> you may choose to respond by sending a welcome message to that user. 

## contactRelationUpdate

A bot receives a **contactRelationUpdate** activity whenever it is added to or removed from a user's contact list. The value of the activity's `action` property (add | remove) indicates whether the bot has been added or removed from the user's contact list.

## typing

A bot receives a **typing** activity to indicate that the user is typing a response. A bot may send a **typing** activity to indicate to the user that it is working to fulfill a request or compile a response. 

## deleteUserData

A bot receives a **deleteUserData** activity when a user requests deletion of any data that the bot has previously persisted for him or her. If your bot receives this type of activity, it should delete any personally identifiable information (PII) that it has previously stored for the user that made the request.

## endOfConversation 

A bot receives an **endOfConversation** activity to indicate that the user has ended the conversation. A bot may send an **endOfConversation** activity to indicate to the user that the conversation is ending. 

## Additional resources

- [Create messages](bot-framework-rest-connector-create-messages.md)
- [Send and receive messages](bot-framework-rest-connector-send-and-receive-messages.md)

[Activity]: bot-framework-rest-connector-api-reference.md#activity-object
