---
title: End a conversation | Microsoft Docs
description: Learn how to end a conversation using Direct Line API v3.0. 
author: kbrandl
ms.author: kibrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 06/13/2017
---

# End a conversation

Either a client or a bot may signal the end of a Direct Line conversation by sending an **endOfConversation** [activity](bot-framework-rest-connector-activities.md). 

## Send an endOfConversation activity

An **endOfConversation** activity ends communication between bot and client. After an **endOfConversation** activity has been sent, the client may still [retrieve messages](bot-framework-rest-direct-line-3-0-receive-activities.md#http-get) using `HTTP GET`, but neither the client nor the bot can send any additional messages to the conversation. 

To end a conversation, simply issue a POST request to send an **endOfConversation** activity.

### Request

```http
POST https://directline.botframework.com/v3/directline/conversations/abc123/activities
Authorization: Bearer RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0
[other headers]
```

```json
{
    "type": "endOfConversation",
    "from": {
        "id": "user1"
    }
}
```

### Response

If the request is successful, the response will contain an ID for the activity that was sent.

```http
HTTP/1.1 200 OK
[other headers]
```

```json
{
  "id": "0004"
}
```

## Additional resources

- [Key concepts](bot-framework-rest-direct-line-3-0-concepts.md)
- [Authentication](bot-framework-rest-direct-line-3-0-authentication.md)
- [Send an activity to the bot](bot-framework-rest-direct-line-3-0-send-activity.md)
