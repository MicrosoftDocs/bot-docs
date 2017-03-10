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
ms.reviewer: rstand
#ROBOTS: Index
---

# Send and receive activities

This article describes how to use the [Connector service](bot-framework-dotnet-connector.md) 
via the Bot Builder SDK for .NET to exchange information back and forth between bot and channel (user). 

## Creating a connector client

The **ConnectorClient** class contains the methods that a bot uses to communicate with a user on a channel. 
When your bot receives an [Activity](bot-framework-dotnet-concepts.md#activity) object from the Connector service, 
it should use the **ServiceUrl** specified for that activity to create the connector client that it'll subsequently 
use to generate a response. 

```cs
public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
{
    var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
    . . .
}
```

> [!TIP]
> Because a channel's endpoint may not be stable, your bot should direct communications to the endpoint 
> that the Connector service specifies in the **Activity** object, whenever possible (rather than relying upon a cached endpoint). 
>
> If your bot needs to initiate the conversation, it can use a cached endpoint for the specified channel 
> (since there will be no incoming **Activity** object in that scenario), but it should refresh cached endpoints often. 

## Creating a reply

The Connector uses an [Activity](bot-framework-dotnet-concepts.md#activity) object to pass information back and forth between bot and channel (user). 
Every activity contains information used for routing the message to the appropriate destination 
along with information about who created the message (**From** field), 
the context of the message, and the recipient of the message (**Recipient** field).

When your bot receives an activity from the Connector service, the incoming activity's **Recipient** field specifies 
the bot's identity in that conversation. 
Because some channels (for example, Slack) assign the bot a new identity when it's added to a conversation, 
the bot should always use the value of the incoming activity's **Recipient** field as the value of 
the **From** field in its response.

Although you can create and initialize the outgoing **Activity** object in a step-by-step manner, 
the Bot Builder SDK provides an easier way of creating a reply. 
By using the incoming activity's **CreateReply** method, 
you simply specify the message text for the response, and the outgoing activity is created 
with the **Recipient**, **From**, and **Conversation** fields automatically populated.

```cs
Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");
```

## Sending a reply

Once you've created a reply, you can send it by calling the connector client's **ReplyToActivity** method. 
The Connector service will deliver the reply using the appropriate channel semantics. 

```cs
await connector.Conversations.ReplyToActivityAsync(reply);
```
> [!TIP]
> If your bot is replying to user's message, always use the **ReplyToActivity** method.

## Sending a (non-reply) message 

If your bot is part of a conversation, it can send a message that is not a direct reply to 
any message from the user by calling the **SendToConversation** method. 

```cs
await connector.Conversations.SendToConversationAsync((Activity)newMessage);
```

> [!TIP]
> You may use the **CreateReply** method to initialize the new message (which would automatically set 
> the **Recipient**, **From**, and **Conversation** fields for the message). 
> Alternatively, you could use the **CreateMessageActivity** method to create the new message in a step-by-step manner.

> [!NOTE]
> The Bot Framework does not impose any restrictions on the number of messaages that a bot may send. 
> However, most channels enforce throttling limits to restrict bots from sending large volumes of messages in a short period of time. 
> Additionally, if the bot sends multiple messages in quick succession, 
> the channel may not always render the messages in the proper sequence.

