---
title: Explore activity types | Microsoft Docs
description: Learn about the different activity types available within the Bot Builder SDK for .NET and when to use them.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/15/2017
ms.reviewer:
ROBOTS: Index, Follow
---

# Explore activity types

[!include[Activity concept overview](~/includes/snippet-dotnet-concept-activity.md)]

This article describes activity types in the Bot Builder SDK for .NET and explores 
the situations in which your bot may expect to send or receive each type of activity.

## Activity types in the Bot Builder SDK for .NET

The following table lists the activity types that are supported by the Bot Builder SDK for .NET.

| Activity.Type | Interface | Description |
|------|------|------|
| message | IMessageActivity | Represents a communication between bot and user. |
| conversationUpdate | IConversationUpdateActivity | Indicates that the bot was added to a conversation, other members were added to or removed from the conversation, or conversation metadata has changed. |
| contactRelationUpdate | IContactRelationUpdateActivity | Indicates that the bot was added or removed from a user's contact list. |
| typing | ITypingActivity | Indicates that the user or bot on the other end of the conversation is compiling a response. | 
| ping | n/a | Represents an attempt to determine whether it has implemented security correctly. | 
| deleteUserData | n/a | Indicates to a bot that a user has requested that the bot delete any user data it may have stored. |
| endOfConversation | IEndOfConversationActivity | Indicates the end of a conversation. |

## message

Your bot will send **message** activities to communicate information to users, and likewise, 
will also receive **message** activities from users. 
Some messages may simply consist of plain text, while others may contain richer content such as 
[media attachments](~/dotnet/add-media-attachments.md), [buttons, and cards](~/dotnet/add-rich-card-attachments.md) or 
[channel-specific data](~/dotnet/channeldata.md). 
For information about commonly-used message properties, see [Create messages](~/dotnet/create-messages.md).

## conversationUpdate

Your bot will receive a **conversationUpdate** activity whenever it has been added to a conversation, 
other members have been added to or removed from a conversation, 
or conversation metadata has changed. 

If members have been added to the conversation, the activity's `MembersAdded` property will contain an array of 
`ChannelAccount` objects to identify the new members. 
You can determine whether your bot has been added to the conversation (i.e., is one of the new members), 
by evaluating whether the `Recipient.Id` value for the activity (i.e., your bot's id) 
matches the `Id` property for any of the accounts in the `MembersAdded` array.

If members have been removed from the conversation, the `MembersRemoved` property will contain an array of `ChannelAccount` objects to identify the removed members. 

> [!TIP]
> If your bot receives a **conversationUpdate** activity to indicate that a user has joined the conversation, 
> you may choose to have it respond by sending a welcome message to that user. 

## contactRelationUpdate

For channels that enable your bot to be a member of a user's contact list (Skype, for example), 
your bot will receive a **contactRelationUpdate** activity whenever it is added or removed from a user's contact list. 
The value of the activity's `Action` property indicates whether the bot has been added to the user's contact list ("add") or removed from the user's contact list ("remove").

## typing

Your bot may send a **typing** activity to indicate to the user that it is working to fulfill a request or compile a response. 
Likewise, your bot may receive a **typing** activity to indicate that the user is typing a response. 

## ping

Your bot will receive a **ping** activity to test that it has implemented security correctly. 
It should simply respond with HTTP status code 200 (OK), 403 (Forbidden), or 401 (Unauthorized).

## deleteUserData

Your bot will receive a **deleteUserData** activity when a user requests that the bot delete any 
data that it has previously persisted for him or her. 
If your bot receives this type of activity, it should delete any personally identifiable information (PII) 
for the user that made the request.

## endOfConversation 

Your bot may send an **endOfConversation** activity to indicate to the user that the conversation is ending. 
Likewise, your bot may receive an **endOfConversation** activity to indidate that the user has ended the conversation. 

## Additional resources

- [Send and receive activities](~/dotnet/connector.md)
- [Create messages](~/dotnet/create-messages.md)
- [Add media attachments to messages](~/dotnet/add-media-attachments.md)
- [Add rich cards to messages](~/dotnet/add-rich-card-attachments.md)
- [Implement channel-specific functionality](~/dotnet/channeldata.md)