---
title: Reconnect to a conversation in Bot Framework SDK
description: Learn how to reconnect to a conversation after losing contact. See how to use Direct Line API version 3.0 to generate new WebSocket stream URLs.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: reference
ms.date: 10/04/2021
---

# Reconnect to a conversation in Direct Line API 3.0

If a client is using the [WebSocket interface](bot-framework-rest-direct-line-3-0-receive-activities.md#connect-via-websocket) to receive messages but loses its connection, it may need to reconnect. In this scenario, the client must generate a new WebSocket stream URL that it can use to reconnect to the conversation.

## Generate a new WebSocket stream URL

To generate a new WebSocket stream URL that can be used to reconnect to an existing conversation, issue this request:

```http
GET https://directline.botframework.com/v3/directline/conversations/{conversationId}?watermark={watermark_value}
Authorization: Bearer SECRET_OR_TOKEN
```

In this request URI, replace **{conversationId}** with the conversation ID and replace **{watermark_value}** with the watermark value (if the `watermark` parameter is available). The `watermark` parameter is optional. If the `watermark` parameter is specified in the request URI, the conversation replays from the watermark, guaranteeing that no messages are lost. If the `watermark` parameter is omitted from the request URI, only messages received after the reconnection request are replayed.

The following snippets provide an example of the Reconnect request and response.

### Request

```http
GET https://directline.botframework.com/v3/directline/conversations/abc123?watermark=0000a-42
Authorization: Bearer RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0y8qbOF5xPGfiCpg4Fv0y8qqbOF5x8qbOF5xn
```

### Response

If the request is successful, the response will contain an ID for the conversation, a token, and a new WebSocket stream URL.

```http
HTTP/1.1 200 OK
[other headers]
```

```json
{
  "conversationId": "abc123",
  "token": "RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0y8qbOF5xPGfiCpg4Fv0y8qqbOF5x8qbOF5xn",
  "streamUrl": "https://directline.botframework.com/v3/directline/conversations/abc123/stream?watermark=000a-4&amp;t=RCurR_XV9ZA.cwA..."
}
```

## Reconnect to the conversation

The client must use the new WebSocket stream URL to [reconnect to the conversation](bot-framework-rest-direct-line-3-0-receive-activities.md#connect-via-websocket) within 60 seconds. If the connection can't be established during this time, the client must issue another Reconnect request to generate a new stream URL.

If you have "Enhanced authentication option" enabled in the Direct Line settings, you might get a 400 "MissingProperty" error if you don't have a correctly configured token attached to the request.

## Additional resources

- [Key concepts](bot-framework-rest-direct-line-3-0-concepts.md)
- [Authentication](bot-framework-rest-direct-line-3-0-authentication.md)
- [Receive activities via WebSocket stream](bot-framework-rest-direct-line-3-0-receive-activities.md#connect-via-websocket)
