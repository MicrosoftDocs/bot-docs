---
title: End a conversation | Microsoft Docs
description: Learn how to end a conversation using Direct Line API v3.0. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# End a conversation

Either a client or a bot may signal the end of a Direct Line conversation by sending an **endOfConversation** [activity](bot-framework-rest-connector-activities.md). 

> [!NOTE] 
> The endOfConversation event is only supported in the Cortana channel, other channels do not implement this functionality. Each channel determines how to react to an endOfConversation activity. If you are designing a DirectLine client, you would update the client to behave appropriately, such as generating an error if the bot sent an activity to a conversation that has already ended.

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
