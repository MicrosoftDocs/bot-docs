---
title: Send and receive activities | Microsoft Docs
description: Learn how to exchange information with a user across various channels by using the Connector service via the Bot Framework SDK for .NET.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Send and receive activities

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

The Bot Framework Connector provides a single REST API that enables a bot to communicate across multiple 
channels such as Skype, Email, Slack, and more. 
It facilitates communication between bot and user, by relaying messages from bot to channel 
and from channel to bot. 

This article describes how to use the Connector via the Bot Framework SDK for .NET to 
exchange information between bot and user on a channel. 

> [!NOTE]
> While it is possible to construct a bot by exclusively using the techniques that are described
> in this article, the Bot Framework SDK provides additional features like 
> [dialogs](bot-builder-dotnet-dialogs.md) and [FormFlow](bot-builder-dotnet-formflow.md) that 
> can streamline the process of managing conversation flow and state and 
> make it simpler to incorporate cognitive services such as language understanding.

## Create a connector client

The [ConnectorClient][ConnectorClient] class contains the methods that a bot uses to communicate with a user on a channel. 
When your bot receives an <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/dc/d2f/class_microsoft_1_1_bot_1_1_connector_1_1_activity.html" target="_blank">Activity</a> object from the Connector, 
it should use the `ServiceUrl` specified for that activity to create the connector client that it'll 
subsequently use to generate a response. 

[!code-csharp[Create connector client](../includes/code/dotnet-send-and-receive.cs#createConnectorClient)]

> [!TIP]
> Because a channel's endpoint may not be stable, your bot should direct communications to the endpoint 
> that the Connector specifies in the `Activity` object, whenever possible (rather than relying upon a cached endpoint). 
>
> If your bot needs to initiate the conversation, it can use a cached endpoint for the specified channel 
> (since there will be no incoming `Activity` object in that scenario), but it should refresh cached endpoints often. 

## <a id="create-reply"></a> Create a reply

The Connector uses an [Activity](bot-builder-dotnet-activities.md) object to pass information back and forth between bot and channel (user). 
Every activity contains information used for routing the message to the appropriate destination 
along with information about who created the message (`From` property), 
the context of the message, and the recipient of the message (`Recipient` property).

When your bot receives an activity from the Connector, the incoming activity's `Recipient` property specifies 
the bot's identity in that conversation. 
Because some channels (e.g., Slack) assign the bot a new identity when it's added to a conversation, 
the bot should always use the value of the incoming activity's `Recipient` property as the value of 
the `From` property in its response.

Although you can create and initialize the outgoing `Activity` object yourself from scratch, 
the Bot Framework SDK provides an easier way of creating a reply. 
By using the incoming activity's `CreateReply` method, 
you simply specify the message text for the response, and the outgoing activity is created 
with the `Recipient`, `From`, and `Conversation` properties automatically populated.

[!code-csharp[Create reply](../includes/code/dotnet-send-and-receive.cs#createReply)]

## Send a reply

Once you've created a reply, you can send it by calling the connector client's `ReplyToActivity` method. 
The Connector will deliver the reply using the appropriate channel semantics. 

[!code-csharp[Send reply](../includes/code/dotnet-send-and-receive.cs#sendReply)]

> [!TIP]
> If your bot is replying to a user's message, always use the `ReplyToActivity` method.

## Send a (non-reply) message 

If your bot is part of a conversation, it can send a message that is not a direct reply to 
any message from the user by calling the `SendToConversation` method. 

[!code-csharp[Send non-reply message](../includes/code/dotnet-send-and-receive.cs#sendNonReplyMessage)]

You may use the `CreateReply` method to initialize the new message (which would automatically set 
the `Recipient`, `From`, and `Conversation` properties for the message). 
Alternatively, you could use the `CreateMessageActivity` method to create the new message 
and set all property values yourself.

> [!NOTE]
> The Bot Framework does not impose any restrictions on the number of messages that a bot may send. 
> However, most channels enforce throttling limits to restrict bots from sending a large number of messages in a short period of time. 
> Additionally, if the bot sends multiple messages in quick succession, 
> the channel may not always render the messages in the proper sequence.

## Start a conversation

There may be times when your bot needs to initiate a conversation with one or more users. 
You can start a conversation by calling either the `CreateDirectConversation` method (for a private conversation with a single user) 
or the `CreateConversation` method (for a group conversation with multiple users) 
to retrieve a `ConversationAccount` object. 
Then, create the message and send it by calling the `SendToConversation` method. 
To use either the `CreateDirectConversation` method or the `CreateConversation` method,
you must first [create the connector client](#create-a-connector-client) by using the target channel's service URL 
(which you may retrieve from cache, if you've persisted it from previous messages). 

> [!NOTE]
> Not all channels support group conversations. 
> To determine whether a channel supports group conversations, consult the channel's documentation.

This code example uses the `CreateDirectConversation` method to create a private conversation with a single user.

[!code-csharp[Start private conversation](../includes/code/dotnet-send-and-receive.cs#startPrivateConversation)]

This code example uses the `CreateConversation` method to create a group conversation with multiple users.

[!code-csharp[Start group conversation](../includes/code/dotnet-send-and-receive.cs#startGroupConversation)]

## Additional resources

- [Activities overview](bot-builder-dotnet-activities.md)
- [Create messages](bot-builder-dotnet-create-messages.md)
- <a href="/dotnet/api/?view=botbuilder-3.11.0" target="_blank">Bot Framework SDK for .NET Reference</a>
- <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/dc/d2f/class_microsoft_1_1_bot_1_1_connector_1_1_activity.html" target="_blank">Activity class</a>
- <a href="/dotnet/api/microsoft.bot.connector.connectorclient" target="_blank">ConnectorClient class</a>

[ConnectorClient]: /dotnet/api/microsoft.bot.connector.connectorclient
