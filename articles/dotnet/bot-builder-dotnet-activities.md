---
title: Activities overview | Microsoft Docs
description: Learn about the different activity types available within the Bot Builder SDK for .NET.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---

# Activities overview

[!include[Activity concept overview](~/includes/snippet-dotnet-concept-activity.md)]

## Activity types in the Bot Builder SDK for .NET

The following activity types are supported by the Bot Builder SDK for .NET.

| Activity.Type | Interface | Description |
|------|------|------|
| message | IMessageActivity | Represents a communication between bot and user. |
| conversationUpdate | IConversationUpdateActivity | Indicates that the bot was added to a conversation, other members were added to or removed from the conversation, or conversation metadata has changed. |
| contactRelationUpdate | IContactRelationUpdateActivity | Indicates that the bot was added or removed from a user's contact list. |
| typing | ITypingActivity | Indicates that the user or bot on the other end of the conversation is compiling a response. | 
| ping | n/a | Represents an attempt to determine whether a bot's endpoint is accessible. | 
| deleteUserData | n/a | Indicates to a bot that a user has requested that the bot delete any user data it may have stored. |
| endOfConversation | IEndOfConversationActivity | Indicates the end of a conversation. |

## message

Your bot will send **message** activities to communicate information to and receive **message** activities from users. 
Some messages may be plain text, while others may contain richer content such as 
[media attachments](~/dotnet/bot-builder-dotnet-add-media-attachments.md), [buttons, and cards](~/dotnet/bot-builder-dotnet-add-rich-card-attachments.md) or 
[channel-specific data](~/dotnet/bot-builder-dotnet-channeldata.md). 
For information about commonly-used message properties, see [Create messages](~/dotnet/bot-builder-dotnet-create-messages.md).

## conversationUpdate

A bot receives a **conversationUpdate** activity whenever it has been added to a conversation, 
other members have been added to or removed from a conversation, 
or conversation metadata has changed. 

If members have been added to the conversation, the activity's `MembersAdded` property will contain an array of 
`ChannelAccount` objects to identify the new members. 

To determine whether your bot has been added to the conversation (i.e., is one of the new members), evaluate whether the `Recipient.Id` value for the activity (i.e., your bot's id) 
matches the `Id` property for any of the accounts in the `MembersAdded` array.

If members have been removed from the conversation, the `MembersRemoved` property will contain an array of `ChannelAccount` objects to identify the removed members. 

> [!TIP]
> If your bot receives a **conversationUpdate** activity indicating that a user has joined the conversation, 
> you may choose to have it respond by sending a welcome message to that user. 

## contactRelationUpdate

A bot receives a **contactRelationUpdate** activity whenever it is added to or removed from a user's contact list. The value of the activity's `Action` property (add | remove) indicates whether the bot has been added or removed from the user's contact list.

## typing

A bot receives a **typing** activity to indicate that the user is typing a response. 
A bot may send a **typing** activity to indicate to the user that it is working to fulfill a request or compile a response. 

## ping

A bot receives a **ping** activity to determine whether its endpoint is accessible. The bot should respond with HTTP status code 200 (OK), 403 (Forbidden), or 401 (Unauthorized).

## deleteUserData

A bot receives **deleteUserData** activity when a user requests deletion of any data that the bot has previously persisted for him or her. 
If your bot receives this type of activity, it should delete any personally identifiable information (PII) 
for the user that made the request.

## endOfConversation 

A bot receives an **endOfConversation** activity to indicate that the user has ended the conversation. 
A bot may send an **endOfConversation** activity to indicate to the user that the conversation is ending. 

## Additional resources

- [Send and receive activities](~/dotnet/bot-builder-dotnet-connector.md)
- [Create messages](~/dotnet/bot-builder-dotnet-create-messages.md)
- [Add media attachments to messages](~/dotnet/bot-builder-dotnet-add-media-attachments.md)
- [Add rich cards to messages](~/dotnet/bot-builder-dotnet-add-rich-card-attachments.md)
- [Implement channel-specific functionality](~/dotnet/bot-builder-dotnet-channeldata.md)