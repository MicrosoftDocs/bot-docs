---
title: Receive activities from the bot | Microsoft Docs
description: Learn how to receive activities from the bot using Direct Line API v3.0. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 06/13/2019
---

# Receive activities from the bot

Using the Direct Line 3.0 protocol, clients can receive activities via `WebSocket` stream or retrieve activities by issuing `HTTP GET` requests.

## WebSocket vs HTTP GET

A streaming WebSocket efficiently pushes messages to clients, whereas the GET interface enables clients to explicitly request messages. Although the WebSocket mechanism is often preferred due to its efficiency, the GET mechanism can be useful for clients that are unable to use WebSockets.

The service allows only 1 WebSocket connection per conversation. Direct Line may close additional WebSocket connections with a reason value of `collision`.

Not all [activity types](bot-framework-rest-connector-activities.md) are available both via WebSocket and via HTTP GET. The following table describes the availability of the various activity types for clients that use the Direct Line protocol.

| Activity type | Availability | 
|----|----|
| message | HTTP GET and WebSocket |
| typing | WebSocket only |
| conversationUpdate | Not sent/received via client |
| contactRelationUpdate | Not supported in Direct Line |
| endOfConversation | HTTP GET and WebSocket |
| all other activity types | HTTP GET and WebSocket |

## <a id="connect-via-websocket"></a> Receive activities via WebSocket stream

When a client sends a [Start Conversation](bot-framework-rest-direct-line-3-0-start-conversation.md) request to open a conversation with a bot, the service's response includes a `streamUrl` property that the client can subsequently use to connect via WebSocket. The stream URL is preauthorized and therefore the client's request to connect via WebSocket does NOT require an `Authorization` header.

The following example shows a request that uses a `streamUrl` to connect via WebSocket.

```http
-- connect to wss://directline.botframework.com --
GET /v3/directline/conversations/abc123/stream?t=RCurR_XV9ZA.cwA..."
Upgrade: websocket
Connection: upgrade
[other headers]
```

The service responds with status code HTTP 101 ("Switching Protocols").

```http
HTTP/1.1 101 Switching Protocols
[other headers]
```

### Receive messages

After it connects via WebSocket, a client may receive these types of messages from the Direct Line service:

- A message that contains an [ActivitySet](bot-framework-rest-direct-line-3-0-api-reference.md#activityset-object) that includes one or more activities and a watermark (described below).
- An empty message, which the Direct Line service uses to ensure the connection is still valid.
- Additional types, to be defined later. These types are identified by the properties in the JSON root.

An `ActivitySet` contains messages sent by the bot and by all users in the conversation. The following example shows an `ActivitySet` that contains a single message.

```json
{
    "activities": [
        {
            "type": "message",
            "channelId": "directline",
            "conversation": {
                "id": "abc123"
            },
            "id": "abc123|0000",
            "from": {
                "id": "user1"
            },
            "text": "hello"
        }
    ],
    "watermark": "0000a-42"
}
```

### Process messages

A client should keep track of the `watermark` value that it receives in each [ActivitySet](bot-framework-rest-direct-line-3-0-api-reference.md#activityset-object), so that it may use the watermark to guarantee that no messages are lost if it loses its connection and needs to [reconnect to the conversation](bot-framework-rest-direct-line-3-0-reconnect-to-conversation.md). If the client receives an `ActivitySet` wherein the `watermark` property is `null` or missing, it should ignore that value and not overwrite the prior watermark that it received.

A client should ignore empty messages that it receives from the Direct Line service.

A client may send empty messages to the Direct Line service to verify connectivity. The Direct Line service will ignore empty messages that it receives from the client.

The Direct Line service may forcibly close the WebSocket connection under certain conditions. If the client has not received an `endOfConversation` activity, it may [generate a new WebSocket stream URL](bot-framework-rest-direct-line-3-0-reconnect-to-conversation.md) that it can use to reconnect to the conversation. 

The WebSocket stream contains live updates and very recent messages (since the call to connect via WebSocket was issued) but it does not include messages that were sent prior to the most recent `POST` to `/v3/directline/conversations/{id}`. To retrieve messages that were sent earlier in the conversation, use `HTTP GET` as described below.

## <a id="http-get"></a> Retrieve activities with HTTP GET

Clients that are unable to use WebSockets can retrieve activities by using `HTTP GET`.

To retrieve messages for a specific conversation, issue a `GET` request to the `/v3/directline/conversations/{conversationId}/activities` endpoint, optionally specifying the `watermark` parameter to indicate the most recent message seen by the client. 

The following snippets provide an example of the Get Conversation Activities request and response. The Get Conversation Activities response contains `watermark` as a property of the [ActivitySet](bot-framework-rest-direct-line-3-0-api-reference.md#activityset-object). Clients should page through the available activities by advancing the `watermark` value until no activities are returned.

### Request

```http
GET https://directline.botframework.com/v3/directline/conversations/abc123/activities?watermark=0001a-94
Authorization: Bearer RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0
```

### Response

```http
HTTP/1.1 200 OK
[other headers]
```

```json
{
    "activities": [
        {
            "type": "message",
            "channelId": "directline",
            "conversation": {
                "id": "abc123"
            },
            "id": "abc123|0000",
            "from": {
                "id": "user1"
            },
            "text": "hello"
        }, 
        {
            "type": "message",
            "channelId": "directline",
            "conversation": {
                "id": "abc123"
            },
            "id": "abc123|0001",
            "from": {
                "id": "bot1"
            },
            "text": "Nice to see you, user1!"
        }
    ],
    "watermark": "0001a-95"
}
```

## Timing considerations

Most clients wish to retain a complete message history. Even though Direct Line is a multi-part protocol with potential timing gaps, the protocol and service is designed to make it easy to build a reliable client.

- The `watermark` property that is sent in the WebSocket stream and Get Conversation Activities response is reliable. A client will not miss any messages as long as it replays the watermark verbatim.

- When a client starts a conversation and connects to the WebSocket stream, any activities that are sent after the POST but before the socket is opened are replayed before new activities.

- When a client issues a Get Conversation Activities request (to refresh history) while it is connected to the WebSocket stream, activities may be duplicated across both channels. Clients should keep track of all known activity IDs so that they are able to reject duplicate activities, should they occur.

Clients that poll using `HTTP GET` should choose a polling interval that matches their intended use.

- Service-to-service applications often use a polling interval of 5s or 10s.

- Client-facing applications often use a polling interval of 1s, and issue a single additional request shortly after every message that the client sends (to rapidly retrieve a bot's response). This delay can be as short at 300ms but should be tuned based on the bot's speed and transit time. Polling should not be more frequent than once per second for any extended period of time.

## Additional resources

- [Key concepts](bot-framework-rest-direct-line-3-0-concepts.md)
- [Authentication](bot-framework-rest-direct-line-3-0-authentication.md)
- [Start a conversation](bot-framework-rest-direct-line-3-0-start-conversation.md)
- [Reconnect to a conversation](bot-framework-rest-direct-line-3-0-reconnect-to-conversation.md)
- [Send an activity to the bot](bot-framework-rest-direct-line-3-0-send-activity.md)
- [End a conversation](bot-framework-rest-direct-line-3-0-end-conversation.md)
