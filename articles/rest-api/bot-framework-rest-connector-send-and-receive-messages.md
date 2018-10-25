---
title: Send and receive messages | Microsoft Docs
description: Learn how to send and receive messages using the Bot Connector service. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# Send and receive messages

The Bot Connector service enables a bot to communicate across multiple channels such as Skype, Email, Slack, and more. It facilitates communication between bot and user, by relaying [activities](bot-framework-rest-connector-activities.md) from bot to channel and from channel to bot. Every activity contains information used for routing the message to the appropriate destination along with information about who created the message, the context of the message, and the recipient of the message. This article describes how to use the Bot Connector service to exchange **message** activities between bot and user on a channel. 

## <a id="create-reply"></a> Reply to a message

### Create a reply 

When the user sends a message to your bot, your bot will receive the message as an [Activity][Activity] object of type **message**. To create a reply to a user's message, create a new [Activity][Activity] object and start by setting these properties:

| Property | Value |
|----|----|
| conversation | Set this property to the contents of the `conversation` property in the user's message. |
| from | Set this property to the contents of the `recipient` property in the user's message. |
| locale | Set this property to the contents of the `locale` property in the user's message, if specified. |
| recipient | Set this property to the contents of the `from` property in the user's message. |
| replyToId | Set this property to the contents of the `id` property in the user's message. |
| type | Set this property to **message**. |

Next, set the properties that specify the information that you want to communicate to the user. For example, you can set the `text` property to specify the text to be displayed in the message, set the `speak` property to specify text to be spoken by your bot, and set the `attachments` property to specify media attachments or rich cards to include in the message. For detailed information about commonly-used message properties, see [Create messages](bot-framework-rest-connector-create-messages.md).

### Send the reply

Use the `serviceUrl` property in the incoming activity to [identify the base URI](bot-framework-rest-connector-api-reference.md#base-uri) that your bot should use to issue its response. 

To send the reply, issue this request: 

```http
POST /v3/conversations/{conversationId}/activities/{activityId}
```

In this request URI, replace **{conversationId}** with the value of the `conversation` object's `id` property within your (reply) Activity and replace **{activityId}** with the value of the `replyToId` property within your (reply) Activity. Set the body of the request to the [Activity][Activity] object that you created to represent your reply.

The following example shows a request that sends a simple text-only reply to a user's message. In this example request, `https://smba.trafficmanager.net/apis` represents the base URI; the base URI for requests that your bot issues may be different. For details about setting the base URI, see [API Reference](bot-framework-rest-connector-api-reference.md#base-uri).

```http
POST https://smba.trafficmanager.net/apis/v3/conversations/abcd1234/activities/5d5cdc723 
Authorization: Bearer ACCESS_TOKEN 
Content-Type: application/json 
```

```json
{
    "type": "message",
    "from": {
        "id": "12345678",
        "name": "Pepper's News Feed"
    },
    "conversation": {
        "id": "abcd1234",
        "name": "Convo1"
   },
   "recipient": {
        "id": "1234abcd",
        "name": "SteveW"
    },
    "text": "My bot's reply",
    "replyToId": "5d5cdc723"
}
```

## <a id="send-message"></a> Send a (non-reply) message

A majority of the messages that your bot sends will be in reply to messages that it receives from the user. However, there may be times when your bot needs to send a message to the conversation that is not a direct reply to any message from the user. For example, your bot may need to start a new topic of conversation or send a goodbye message at the end of the conversation. 

To send a message to a conversation that is not a direct reply to any message from the user, issue this request: 

```http
POST /v3/conversations/{conversationId}/activities
```

In this request URI, replace **{conversationId}** with the ID of the conversation. 
    
Set the body of the request to an [Activity][Activity] object that you create to represent your reply.

> [!NOTE]
> The Bot Framework does not impose any restrictions on the number of messages that a bot may send. 
> However, most channels enforce throttling limits to restrict bots from sending a large number of messages in a short period of time. 
> Additionally, if the bot sends multiple messages in quick succession, the channel may not always render the messages in the proper sequence.

## Start a conversation

There may be times when your bot needs to initiate a conversation with one or more users. 
To start a conversation with a user on a channel, your bot must know its account information and the user's account information on that channel. 

> [!TIP]
> If your bot may need to start conversations with its users in the future, cache user account information, other relevant information such as user preferences and locale, and the service URL (to use as the base URI in the Start Conversation request). 

To start a conversation, issue this request: 

```http
POST /v3/conversations
```

Set the body of the request to a [Conversation][Conversation] object that specifies your bot's account information and the account information of the user(s) that you want to include in the conversation.

> [!NOTE]
> Not all channels support group conversations. Consult the channel's documentation to determine whether a channel supports group conversations and to identify the maximum number of participants that a channel allows in a conversation.

The following example shows a request that starts a conversation. In this example request, `https://smba.trafficmanager.net/apis` represents the base URI; the base URI for requests that your bot issues may be different. For details about setting the base URI, see [API Reference](bot-framework-rest-connector-api-reference.md#base-uri).

```http
POST https://smba.trafficmanager.net/apis/v3/conversations 
Authorization: Bearer ACCESS_TOKEN
Content-Type: application/json
```

```json
{
    "bot": {
        "id": "12345678",
        "name": "bot's name"
    },
    "isGroup": false,
    "members": [
        {
            "id": "1234abcd",
            "name": "recipient's name"
        }
    ],
    "topicName": "News Alert"
}
```

If the conversation is established with the specified users, the response will contain an ID that identifies the conversation. 

```json
{
    "id": "abcd1234"
}
```

Your bot can then use this conversation ID to [send a message](#send-message) to the user(s) within the conversation.

## Additional resources

- [Activities overview](bot-framework-rest-connector-activities.md)
- [Create messages](bot-framework-rest-connector-create-messages.md)

[Activity]: bot-framework-rest-connector-api-reference.md#activity-object
[ConversationAccount]: bot-framework-rest-connector-api-reference.md#conversationaccount-object
[Conversation]: bot-framework-rest-connector-api-reference.md#conversation-object

