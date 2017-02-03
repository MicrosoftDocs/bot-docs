---
layout: page
title: Sending and Receiving Messages
permalink: /en-us/connector/messages/
weight: 3030
parent1: Building your Bot Using the Bot Connector REST API
---

A conversation is a series of message sent between your bot and one or more users. Each message is an [Activity](../reference/#activity) object. When a user sends a message, the connector posts the message to your bot (web service). Your bot examines the message to determine its type and responds accordingly. The following are the types of messages that bots and users can exchange.

|**Message type**|**Description**
|contactRelationUpdate|The channel sends this message to indicate that the user added or removed your bot from their contacts list in the channel. If the user added your bot to their contacts list, the message's **action** property is set to _add_; otherwise, it's set to _remove_.
|conversationUpdate|The channel sends this message to indicate that one or more users joined or left the conversation, or the topic name changed. For a list of users that joined the conversation, see the message's **addedMembers** property; otherwise, see the **removedMembers** property. You can use this message to welcome new users to the conversation. 
|deleteUserData|The channel sends this message to indicate that the user wants the bot to delete all of their personally identifiable information (PII) that the bot may have saved using the [User State REST API](../reference/#endpoints). If you receive this message, you must delete the user's data. After you delete the user's data, you should send them a message indicating that it's been deleted.
|message|The bot or user sends this message to advance the conversation. For example, the user sends a message asking for information, and your bot replies with a message that answers the user's question. Most messages that you send and receive will be of this type.
|ping|The bot is sent this message to verify that it's URL is accessible. The bot should respond with HTTP status code 200 OK, and may respond with 401 Unauthorized or 403 Forbidden.
|typing|The channel or bot sends this message to indicate to the other that they're working on a reply. Not all channels support this message.

Most messages that your bot will send and receive are of type *message*. To reply to a user's message, create a new **Activity** object and set, at a minimum, the following properties:

|**Property**|**Description**
|conversation|Set this to the contents of the **conversation** property from the user's message.
|from|Set this to the contents of the **recipient** property from the user's message.
|locale|Set this to the contents of the **locale** property from the user's message, if specified.
|recipient|Set this to the contents of the **from** property from the user's message.
|replyToId|Set this to the contents of the **id** property from the user's message.
|type|Set this to *message*.

If your bot's reply is a simple text message, set the message's **text** property with your response. The message's text reply may include simple formatting. By default, the text reply may contain Markdown formatting. For a list of allowed Markdown formatting characters that you may use, see the [message's](../reference/#activity) **textFormat** property. You can also specify the language of the text string by setting the **locale** property (the default is English).

If your bot responds with images, videos, or rich cards, set the **attachments** and **attachmentLayout** properties accordingly. For details about using attachments, see [Adding attachments to a message](../attachments).

To reply to a message, send a POST request to https://api.botframework.com/v3/conversations/{conversationId}/activities/{activityId}. Set `{conversationId}` to the ID of your message's **conversation** property, and set `{activityId}` to the ID of your message's **replyToId** property. 

The body of the request must contain the [Activity](../reference/#activity) object. The following shows a simple text reply to a user's message. 

```cmd
POST https://api.botframework.com/v3/conversations/abcd1234/activities/5d5cdc723 HTTP/1.1
Authorization: Bearer eyJhbGciOiJIUzI1Ni...
Content-Type: application/json

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

You may send one or more replies to a single message. Some channels limit the number of messages that bots can send them. Each channel defines their own limits, and each may respond differently if your bot exceeds the limit. For example, the channel may return HTTP status code 429 or they may drop your bot from the conversation. Because neither option provides a great user experience, you should understand the channel's limits and make sure your bots stays within them. 

If you send multiple replies to a single user's message, the framework will preserve message ordering to the extent possible. For example, if you wait for the completion of message A before sending message B, we will respect the ordering that A comes before B. But, in general, because the channel manages message delivery, we cannot guarantee the delivery order. It is possible that the channel may reorder messages. For example, you have likely seen email and text messages that are delivered out of order. As a mitigation, you might consider putting a time delay between your messages.

Most of the time your bot will be send messages in reply to a user's message. But there may be times when your bot wants to send a message to the conversation that is not a reply to a user's message. For example, when your bot initiates the conversation, or wants to start a new topic of conversation, or the bot's sending a goodbye message at the end of the conversation. Basically, you send a message to the conversation anytime you're not replying directly to another message. These types of messages are not synchronized with other messages that are sent in the conversation.

To send a message to a conversation, send a POST request to https://api.botframework.com/v3/conversations/{conversationId}/activities. Set `{conversationId}` to the conversation's ID. If your bot initiates the conversation (see [Starting the conversation](../conversation)), the response contains the conversation ID. Otherwise, if you've already received a message from a user, you can get the ID from the [message](../reference/#activity)'s **conversation** property.

The body of the request must contain an [Activity](../reference/#activity) object. If your bot initiates the conversation, you can get the values of the required properties (for example, **from**, **recipient**, and **conversation**) using a previously cached message from the user, or if you're sending this message during a current conversation, you can build the message from a previous message as described above.

For information about adding attachments such as images, videos, or rich cards, see [Adding Attachments to a Message](../attachments).

For information about adding channel-specific data to a message, see [Adding Channel Data](../channeldata).