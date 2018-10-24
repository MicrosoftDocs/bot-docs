---
title: Receive messages from the bot | Microsoft Docs
description: Learn how to receive messages from the bot using Direct Line API v1.1. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# Receive messages from the bot

> [!IMPORTANT]
> This article describes how to receive messages from the bot using Direct Line API 1.1. If you are creating a new connection between your client application and bot, use [Direct Line API 3.0](bot-framework-rest-direct-line-3-0-receive-activities.md) instead.

Using the Direct Line 1.1 protocol, clients must poll an `HTTP GET` interface to receive messages. 

## Retrieve messages with HTTP GET

To retrieve messages for a specific conversation, issue a `GET` request to the `api/conversations/{conversationId}/messages` endpoint, optionally specifying the `watermark` parameter to indicate the most recent message seen by the client. An updated `watermark` value will be returned in the JSON response, even if no messages are included.

The following snippets provide an example of the Get Messages request and response. The Get Messages response contains `watermark` as a property of the [MessageSet](bot-framework-rest-direct-line-1-1-api-reference.md#messageset-object). Clients should page through the available messages by advancing the `watermark` value until no messages are returned. 

### Request

```http
GET https://directline.botframework.com/api/conversations/abc123/messages?watermark=0001a-94
Authorization: Bearer RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0
```

### Response

```http
HTTP/1.1 200 OK
[other headers]
```

```json
{
    "messages": [
        {
            "conversation": "abc123",
            "id": "abc123|0000",
            "text": "hello",
            "from": "user1"
        }, 
        {
            "conversation": "abc123",
            "id": "abc123|0001",
            "text": "Nice to see you, user1!",
            "from": "bot1"
        }
    ],
    "watermark": "0001a-95"
}
```

## Timing considerations

Even though Direct Line is a multi-part protocol with potential timing gaps, the protocol and service is designed to make it easy to build a reliable client. The `watermark` property that is sent in the Get Messages response is reliable. A client will not miss any messages as long as it replays the watermark verbatim.

Clients should choose a polling interval that matches their intended use.

- Service-to-service applications often use a polling interval of 5s or 10s.

- Client-facing applications often use a polling interval of 1s, and issue an additional request ~300ms after every message that the client sends (to rapidly retrieve a bot's response). This 300ms delay should be adjusted based on the bot's speed and transit time.

## Additional resources

- [Key concepts](bot-framework-rest-direct-line-1-1-concepts.md)
- [Authentication](bot-framework-rest-direct-line-1-1-authentication.md)
- [Start a conversation](bot-framework-rest-direct-line-1-1-start-conversation.md)
- [Send a message to the bot](bot-framework-rest-direct-line-1-1-send-message.md)