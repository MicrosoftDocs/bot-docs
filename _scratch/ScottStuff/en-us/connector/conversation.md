---
layout: page
title: Starting a Conversation
permalink: /en-us/connector/conversation/
weight: 3020
parent1: Building your Bot Using the Bot Connector REST API
---

Conversations can take many forms. For example, your bot can have a private conversation with a single user, or a group conversation with multiple users including other bots. Most channels support private conversations but not all channels support group conversations. To determine whether the channel supports group conversations, see the channel's documentation. 

Most of the time, users will start the conversation. If the user starts the conversation, your bot simply responds to messages that the user sends (see [Sending and receiving messages](../messages)). But sometimes your bot may want to start the conversation. For example, if your bot knows about the user's interests, and it learns of a news event or article that's related to one of their interests, your bot can start the conversation with the user to alert them to the article.

to start a conversation, your bot needs to know its account information on that channel as well as the user's account information. If you're going to start conversations, make sure you cache the account information along with any other relevant information such as user preferences and locale (so the message uses the language of the user).

To start the conversation, send a POST request to https://api.botframework.com/v3/conversations. The body of the request must contain a [Conversation](../reference/#conversation-object) object. You must specify your bot's account information and the account information of the users that you want to join the conversation. To determine whether the channel supports group conversations, and the maximum number of participants that it allows in a conversation, see the channel's documentation. 

The following example shows a request to start a conversation.

```cmd
POST https://api.botframework.com/v3/conversations HTTP/1.1
Authorization: Bearer eyJhbGciOiJIUzI1Ni...
Content-Type: application/json

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

If the Connector is able to establish the conversation with the specified users, the response will contain an ID that identifies the conversation. Make sure that you capture the ID because you'll need it to send messages to the users. The following example shows a response that contains a conversation ID.

```cmd
{
    "id": "abcd1234"
}
```
<span style="color:red"><<
If the channel doesn't support letting the bot start the conversation, the response's status code will be set to ???.

If the channel doesn't support group conversations, the response's status code will be set to ???.

What steps transpire when the bot tries to start the conversation? What does the Connector do? How does the channel "wake up" on the user's device? Does the user have to be signed into the channel? What if the user uses the bot on multiple devices, which does it wake up on?

What status code does the Connector return if it's unable to establish the conversation, 503?

If I specify 4 users but only 2 users are available (join to the conversation), does the bot get any indication as to which users joined or which users didn't join?

If there's no indication as to who joined, do I send messages to the entire list of users and the connector will deliver the message to whoever has joined and ignore the others?
>></span>


After initiating the conversation, your bot sends a message to the users by posting a request to https://api.botframework.com/v3/conversations/abcd1234/activities. For details about sending and receiving messages, see [Sending and receiving messages](../messages).

For details about starting a conversation using the .NET or Node SDKs, see [Alarm Bot](/en-us/csharp/builder/sdkreference/dialogs.html#alarmBot) and [Proactive Messaging](en-us/node/builder/chat/UniversalBot/#proactive-messaging). 
