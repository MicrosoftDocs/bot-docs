---
title: Activities overview | Microsoft Docs
description: Learn about the different activity types available within the Bot Framework SDK for .NET.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Activities overview

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

[!INCLUDE [Activity concept overview](../includes/snippet-dotnet-concept-activity.md)]

## Activity types in the Bot Framework SDK for .NET

The following activity types are supported by the Bot Framework SDK for .NET.

| Activity.Type | Interface | Description |
|------|------|------|
| [message](#message) | IMessageActivity | Represents a communication between bot and user. |
| [conversationUpdate](#conversationupdate) | IConversationUpdateActivity | Indicates that the bot was added to a conversation, other members were added to or removed from the conversation, or conversation metadata has changed. |
| [contactRelationUpdate](#contactrelationupdate) | IContactRelationUpdateActivity | Indicates that the bot was added or removed from a user's contact list. |
| [typing](#typing) | ITypingActivity | Indicates that the user or bot on the other end of the conversation is compiling a response. | 
| [deleteUserData](#deleteuserdata) | n/a | Indicates to a bot that a user has requested that the bot delete any user data it may have stored. |
| [endOfConversation](#endofconversation) | IEndOfConversationActivity | Indicates the end of a conversation. |
| [event](#event) | IEventActivity | Represents a communication sent to a bot that is not visible to the user. |
| [invoke](#invoke) | IInvokeActivity | Represents a communication sent to a bot to request that it perform a specific operation. This activity type is reserved for internal use by the Microsoft Bot Framework. |
| [messageReaction](#messagereaction) | IMessageReactionActivity | Indicates that a user has reacted to an existing activity. For example, a user clicks the "Like" button on a message. |

## message

Your bot will send **message** activities to communicate information to and receive **message** activities from users. 
Some messages may simply consist of plain text, while others may contain richer content such as [text to be spoken](bot-builder-dotnet-text-to-speech.md), [suggested actions](bot-builder-dotnet-add-suggested-actions.md), [media attachments](bot-builder-dotnet-add-media-attachments.md), [rich cards](bot-builder-dotnet-add-rich-card-attachments.md), and [channel-specific data](bot-builder-dotnet-channeldata.md). 
For information about commonly-used message properties, see [Create messages](bot-builder-dotnet-create-messages.md).

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

## deleteUserData

A bot receives a **deleteUserData** activity when a user requests deletion of any data that the bot has previously persisted for him or her. If your bot receives this type of activity, it should delete any personally identifiable information (PII) that it has previously stored for the user that made the request.

## endOfConversation 

A bot receives an **endOfConversation** activity to indicate that the user has ended the conversation. A bot may send an **endOfConversation** activity to indicate to the user that the conversation is ending. 

## event

Your bot may receive an **event** activity from an external process or service that wants to 
communicate information to your bot without that information being visible to users. The 
sender of an **event** activity typically does not expect the bot to acknowledge receipt in any way.

## invoke

Your bot may receive an **invoke** activity that represents a request for it to perform a specific operation. 
The sender of an **invoke** activity typically expects the bot to acknowledge receipt via HTTP response. 
This activity type is reserved for internal use by the Microsoft Bot Framework.

## messageReaction

Some channels will send **messageReaction** activities to your bot when a user reacted to an existing activity. For example, a user clicks the "Like" button on a message. The **ReplyToId** property will indicate which activity the user reacted to.

The **messageReaction** activity may correspond to any number of **messageReactionTypes** that the channel defined. For example, "Like" or "PlusOne" as reaction types that a channel may send. 

## Additional resources

- [Send and receive activities](bot-builder-dotnet-connector.md)
- [Create messages](bot-builder-dotnet-create-messages.md)
- <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/dc/d2f/class_microsoft_1_1_bot_1_1_connector_1_1_activity.html" target="_blank">Activity class</a>
