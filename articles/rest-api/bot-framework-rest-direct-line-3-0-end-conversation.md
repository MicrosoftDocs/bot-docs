---
title: End a conversation in Bot Framework SDK
description: Learn how to use version 3.0 of the Direct Line API to end conversations between bots and Cortana channels. See how to set up and send endOfConversation events.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: reference
ms.date: 09/27/2021
ms.custom:
  - evergreen
---

# End a conversation in Direct Line API 3.0

The **endOfConversation** [activity](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md) means the channel or bot has ended the conversation.

> [!NOTE]
> The **endOfConversation** event is sent by very few channels, and few channels accept it. Some channels, including Direct Line, don't implement this functionality and instead drop or forward the activity on; each channel determines how to react to an endOfConversation activity.

## Send an endOfConversation activity

To request to end a conversation with Cortana channel, POST End of Conversation Activity to the channel's messaging endpoint.

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
