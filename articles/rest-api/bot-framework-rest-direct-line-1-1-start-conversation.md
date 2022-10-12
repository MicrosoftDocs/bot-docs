---
title: Start a conversation using Direct Line API 1.1 - Bot Service
description: Learn how to use version 1.1 of the Direct Line API to start conversations with bots. Find out how the Start Conversation and Generate Token operations differ.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 11/01/2021
---

# Start a conversation in Direct Line API 1.1

> [!IMPORTANT]
> This article describes how to start a conversation using Direct Line API 1.1. If you're creating a new connection between your client application and bot, use [Direct Line API 3.0](bot-framework-rest-direct-line-3-0-start-conversation.md) instead.

Direct Line conversations are explicitly opened by clients and may run as long as the bot and client participate and have valid credentials. While the conversation is open, both the bot and client may send messages. More than one client may connect to a given conversation and each client may participate on behalf of multiple users.

## Open a new conversation

To open a new conversation with a bot, issue this request:

```http
POST https://directline.botframework.com/api/conversations
Authorization: Bearer SECRET_OR_TOKEN
```

The following snippets provide an example of the Start Conversation request and response.

### Request

```http
POST https://directline.botframework.com/api/conversations
Authorization: Bearer RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0y8qbOF5xPGfiCpg4Fv0y8qqbOF5x8qbOF5xn
```

### Response

If the request is successful, the response will contain an ID for the conversation, a token, and a value that indicates the number of seconds until the token expires.

```http
HTTP/1.1 200 OK
[other headers]
```

```json
{
  "conversationId": "abc123",
  "token": "RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0y8qbOF5xPGfiCpg4Fv0y8qqbOF5x8qbOF5xn",
  "expires_in": 1800
}
```

## Start Conversation versus Generate Token

The Start Conversation operation (`POST /api/conversations`) is similar to the [Generate Token](bot-framework-rest-direct-line-1-1-authentication.md#generate-token) operation (`POST /api/tokens/conversation`) in that both operations return a `token` that can be used to access a single conversation. However, the Start Conversation operation also starts the conversation and contacts the bot, whereas the Generate Token operation does neither of these things.

If you intend to start the conversation immediately, use the Start Conversation operation. If you plan to distribute the token to clients and want them to initiate the conversation, use the [Generate Token](bot-framework-rest-direct-line-1-1-authentication.md#generate-token) operation instead.

## Additional resources

- [Key concepts](bot-framework-rest-direct-line-1-1-concepts.md)
- [Authentication](bot-framework-rest-direct-line-1-1-authentication.md)
