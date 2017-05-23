---
title: Add input hints to messages | Microsoft Docs
description: Learn how to add input hints to messages using the Bot Connector service.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 05/12/2017
ms.reviewer: 
---

# Add input hints to messages
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-add-input-hints.md)
> - [Node.js](../nodejs/bot-builder-nodejs-send-input-hints.md)
> - [REST](../rest-api/bot-framework-rest-connector-add-input-hints.md)

By specifying an input hint for a message, you can indicate whether your bot is accepting, expecting, or ignoring user input after the message is delivered to the client. For many channels, this enables clients to set the state of user input controls accordingly. For example, if a message's input hint indicates that the bot is ignoring user input, the client may close the microphone and disable the input box to prevent the user from providing input.

## Accepting input

To indicate that your bot is passively ready for input but is not awaiting a response from the user, set the `inputHint` property to **acceptingInput** within the [Activity][Activity] object that represents your message. On many channels, this will cause the client's input box to be enabled and microphone to be closed, but still accessible to the user. For example, Cortana will open the microphone to accept input from the user if the user holds down the microphone button. 

The following example shows a request that sends a message and specifies that the bot is accepting input, where **[baseURI]** should be replaced with the value of the `serviceUrl` property in a message that the bot previously received from the user.

```http
POST [baseURI]/v3/conversations/abcd1234/activities/5d5cdc723
Authorization: Bearer ACCESS_TOKEN
Content-Type: application/json
```

```json
{
    "type": "message",
    "from": {
        "id": "12345678",
        "name": "sender's name"
    },
    "conversation": {
        "id": "abcd1234",
        "name": "conversation's name"
   },
   "recipient": {
        "id": "1234abcd",
        "name": "recipient's name"
    },
    "text": "Here's a picture of the house I was telling you about.",
    "inputHint": "acceptingInput",
    "replyToId": "5d5cdc723"
}
```

## Expecting input

To indicate that your bot is awaiting a response from the user, set the `inputHint` property to **expectingInput** within the [Activity][Activity] object that represents your message. On many channels, this will cause the client's input box to be enabled and microphone to be open. 

The following example shows a request that sends a message and specifies that the bot is expecting input, where **[baseURI]** should be replaced with the value of the `serviceUrl` property in a message that the bot previously received from the user.

```http
POST [baseURI]/v3/conversations/abcd1234/activities/5d5cdc723
Authorization: Bearer ACCESS_TOKEN
Content-Type: application/json
```

```json
{
    "type": "message",
    "from": {
        "id": "12345678",
        "name": "sender's name"
    },
    "conversation": {
        "id": "abcd1234",
        "name": "conversation's name"
   },
   "recipient": {
        "id": "1234abcd",
        "name": "recipient's name"
    },
    "text": "What is your favorite color?",
    "inputHint": "expectingInput",
    "replyToId": "5d5cdc723"
}
```

## Ignoring input
 
To indicate that your bot is not ready to receive input from the user, set the `inputHint` property to **ignoringInput** within the [Activity][Activity] object that represents your message. On many channels, this will cause the client's input box to be disabled and microphone to be closed. 

The following example shows a request that sends a message and specifies that the bot is ignoring input, where **[baseURI]** should be replaced with the value of the `serviceUrl` property in a message that the bot previously received from the user.

```http
POST [baseURI]/v3/conversations/abcd1234/activities/5d5cdc723
Authorization: Bearer ACCESS_TOKEN
Content-Type: application/json
```

```json
{
    "type": "message",
    "from": {
        "id": "12345678",
        "name": "sender's name"
    },
    "conversation": {
        "id": "abcd1234",
        "name": "conversation's name"
   },
   "recipient": {
        "id": "1234abcd",
        "name": "recipient's name"
    },
    "text": "Please hold while I perform the calculation.",
    "inputHint": "ignoringInput",
    "replyToId": "5d5cdc723"
}
```

## Additional resources

- [Create messages](~/rest-api/bot-framework-rest-connector-create-messages.md)
- [Send and receive messages](~/rest-api/bot-framework-rest-connector-send-and-receive-messages.md)

[Activity]: ~/rest-api/bot-framework-rest-connector-api-reference.md#activity-object