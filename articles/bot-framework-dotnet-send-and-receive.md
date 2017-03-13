---
title: Send and receive activities using the Bot Framework Connector service and .NET | Microsoft Docs
description: Learn how to send and receive activities using the Bot Framework Connector service via the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, Connector, Connector service, activity, send activity, receive activity
author: kbrandl
manager: rstand
ms.topic: develop-dotnet-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 03/09/2017
ms.reviewer:
#ROBOTS: Index
---

# Send and receive activities

This article describes how to use the [Connector service](bot-framework-dotnet-connector.md) 
via the Bot Builder SDK for .NET to exchange information between bot and user 
on a channel. 

##<a id="activity-types"></a> Activity types

[!include[Activity concept overview](../includes/snippet-dotnet-concept-activity.md)]

The following table lists and describes the various activity types.

| Activity.Type | Interface | Description |
|------|------|------|
| message | IMessageActivity | Represents a communication between bot and user. |
| conversationUpdate | IConversationUpdateActivity | Indicates that the bot was added to a conversation, other members were added to or removed from the conversation, or conversation metadata has changed. |
| contactRelationUpdate | IContactRelationUpdateActivity | Indicates that the bot was added or removed from a user's contact list. |
| typing | ITypingActivity | Indicates that the user or bot on the other end of the conversation is compiling a response. | 
| ping | n/a | Represents an attempt to determine whether it has implemented security correctly. | 
| deleteUserData | n/a | Indicates to a bot that a user has requested that the bot delete any user data it may have stored. |
| endOfConversation | IEndOfConversationActivity | Indicates the end of a conversation. |

The following sections explain each activity type in greater detail 
and describe the situations in which your bot may expect to send or receive such activities.

### message

Your bot will send **message** activities to communicate information to users, and likewise, 
will also receive **message** activities from users. 
Some messages may simply consist of plain text, while others may contain richer content such as 
[media attachments, buttons, and cards](bot-framework-dotnet-add-attachments.md). 
For information about commonly-used message properties, see [Compose messages](bot-framework-dotnet-compose-messages.md).

### conversationUpdate

Your bot will receive a **conversationUpdate** activity whenever it has been added to a conversation, 
other members have been added to or removed from a conversation, 
or conversation metadata has changed. 

If members have been added to the conversation, the activity's **MembersAdded** property will contain an array of **ChannelAccount** 
objects to identify the new members. 
You can determine whether your bot has been added to the conversation (i.e., is one of the new members), 
by evaluating whether the **Recipient.Id** value for the activity (i.e., your bot's id) 
matches the **Id** property for any of the accounts in the **MembersAdded** array.

If members have been removed from the conversation, the **MembersRemoved** property will contain an array of **ChannelAccount** 
objects to identify the removed members. 

> [!TIP]
> If your bot receives a **conversationUpdate** activity to indicate that a user has joined the conversation, 
> you may choose to have it respond by sending a welcome message to that user. 

### contactRelationUpdate

For channels that enable your bot to be a member of a user's contact list (Skype, for example), 
your bot will receive a **contactRelationUpdate** activity whenever it is added or removed from a user's contact list. 
The value of the activity's **Action** property indicates whether the bot has been added to the user's contact list ("add") 
or removed from the user's contact list ("remove").

### typing

Your bot may send a **typing** activity to indicate to the user that it is working to fulfill a request or compile a response. 
Likewise, your bot may receive a **typing** activity to indicate that the user is typing a response. 

### ping

Your bot will receive a **ping** activity to test that it has implemented security correctly. 
It should simply respond with HTTP status code 200 (OK), 403 (Forbidden), or 401 (Unauthorized).

### deleteUserData

Your bot will receive a **deleteUserData** activity when a user requests that the bot delete any 
data that it has previously persisted for him or her. 
If your bot receives this type of activity, it should delete any personally identifiable information (PII) 
for the user that made the request.

### endOfConversation 

Your bot may send an **endOfConversation** activity to indicate to the user that the conversation is ending. 
Likewise, your bot may receive an **endOfConversation** activity to indidate that the user has ended the conversation. 

##<a id="create-client"></a> Creating a connector client

The **ConnectorClient** class contains the methods that a bot uses to communicate with a user on a channel. 
When your bot receives an [Activity](bot-framework-dotnet-concepts.md#activity) object from the Connector service, 
it should use the **ServiceUrl** specified for that activity to create the connector client that it'll subsequently 
use to generate a response. 

[!code-csharp[Create connector client](../includes/code/dotnet-send-and-receive.cs#createConnectorClient)]

> [!TIP]
> Because a channel's endpoint may not be stable, your bot should direct communications to the endpoint 
> that the Connector service specifies in the **Activity** object, whenever possible (rather than relying upon a cached endpoint). 
>
> If your bot needs to initiate the conversation, it can use a cached endpoint for the specified channel 
> (since there will be no incoming **Activity** object in that scenario), but it should refresh cached endpoints often. 

## Creating a reply

The Connector uses an [Activity](bot-framework-dotnet-concepts.md#activity) object to pass information back and forth between bot and channel (user). 
Every activity contains information used for routing the message to the appropriate destination 
along with information about who created the message (**From** property), 
the context of the message, and the recipient of the message (**Recipient** property).

When your bot receives an activity from the Connector service, the incoming activity's **Recipient** property specifies 
the bot's identity in that conversation. 
Because some channels (for example, Slack) assign the bot a new identity when it's added to a conversation, 
the bot should always use the value of the incoming activity's **Recipient** property as the value of 
the **From** property in its response.

Although you can create and initialize the outgoing **Activity** object yourself from scratch, 
the Bot Builder SDK provides an easier way of creating a reply. 
By using the incoming activity's **CreateReply** method, 
you simply specify the message text for the response, and the outgoing activity is created 
with the **Recipient**, **From**, and **Conversation** property automatically populated.

[!code-csharp[Create reply](../includes/code/dotnet-send-and-receive.cs#createReply)]

## Sending a reply

Once you've created a reply, you can send it by calling the connector client's **ReplyToActivity** method. 
The Connector service will deliver the reply using the appropriate channel semantics. 

[!code-csharp[Send reply](../includes/code/dotnet-send-and-receive.cs#sendReply)]

> [!TIP]
> If your bot is replying to a user's message, always use the **ReplyToActivity** method.

## Sending a (non-reply) message 

If your bot is part of a conversation, it can send a message that is not a direct reply to 
any message from the user by calling the **SendToConversation** method. 

[!code-csharp[Send non-reply message](../includes/code/dotnet-send-and-receive.cs#sendNonReplyMessage)]

> [!TIP]
> You may use the **CreateReply** method to initialize the new message (which would automatically set 
> the **Recipient**, **From**, and **Conversation** properties for the message). 
> Alternatively, you could use the **CreateMessageActivity** method to create the new message 
> and set all property values yourself.

> [!NOTE]
> The Bot Framework does not impose any restrictions on the number of messages that a bot may send. 
> However, most channels enforce throttling limits to restrict bots from sending a large number of messages in a short period of time. 
> Additionally, if the bot sends multiple messages in quick succession, 
> the channel may not always render the messages in the proper sequence.

## Starting a conversation

There may be times when your bot needs to initiate a conversation with one or more users. 
You can start a conversation by calling either the **CreateDirectConversation** method (for a private conversation with a single user) 
or the **CreateConversation** method (for a group conversation with multiple users) 
to retrieve a **ConversationAccount** object. 
Then, create the message and send it by calling the **SendToConversation** method.

> [!NOTE]
> To use either the **CreateDirectConversation** method or the **CreateConversation** method,
> you must first [create the connector client](#create-client) by using the target channel's service URL 
> (which you may retrieve from cache, if you've persisted it from previous messages). 

> [!NOTE]
> Not all channels support group conversations. 
> To determine whether a channel supports group conversations, consult the channel's documentation.

The following code example uses the **CreateDirectConversation** method to create a private conversation with a single user.

[!code-csharp[Start private conversation](../includes/code/dotnet-send-and-receive.cs#startPrivateConversation)]

The following code example uses the **CreateConversation** method to create a group conversation with multiple users.

[!code-csharp[Start group conversation](../includes/code/dotnet-send-and-receive.cs#startGroupConversation)]

## Next steps

In this article, we discussed activity types and 
learned how to exchange information between bot and user on a channel. 
For more information about using the Connector service via the Bot Builder SDK for .NET, 
explore the remaining articles in this section. 