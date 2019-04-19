---
title: API reference - Direct Line API 3.0 | Microsoft Docs
description: Learn about headers, HTTP status codes, schema, operations, and objects in Direct Line API 3.0. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# API reference - Direct Line API 3.0

You can enable your client application to communicate with your bot by using Direct Line API 3.0. Direct Line API 3.0 uses industry-standard REST and JSON over HTTPS.

## Base URI

To access Direct Line API 3.0, use this base URI for all API requests:

`https://directline.botframework.com`

## Headers

In addition to the standard HTTP request headers, a Direct Line API request must include an `Authorization` header that specifies a secret or token to authenticate the client that is issuing the request. Specify the `Authorization` header using this format:

```http
Authorization: Bearer SECRET_OR_TOKEN
```

For details about how to obtain a secret or token that your client can use to authenticate its Direct Line API requests, see [Authentication](bot-framework-rest-direct-line-3-0-authentication.md).

## HTTP status codes

The <a href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html" target="_blank">HTTP status code</a> that is returned with each response indicates the outcome of the corresponding request. 

| HTTP status code | Meaning |
|----|----|
| 200 | The request succeeded. |
| 201 | The request succeeded. |
| 202 | The request has been accepted for processing. |
| 204 | The request succeeded but no content was returned. |
| 400 | The request was malformed or otherwise incorrect. |
| 401 | The client is not authorized to make the request. Often this status code occurs because the `Authorization` header is missing or malformed. |
| 403 | The client is not allowed to perform the requested operation. If the request specified a token that was previously valid but has expired, the `code` property of the [Error](bot-framework-rest-connector-api-reference.md#error-object) that is returned within the [ErrorResponse](bot-framework-rest-connector-api-reference.md#errorresponse-object) object is set to `TokenExpired`. |
| 404 | The requested resource was not found. Typically this status code indicates an invalid request URI. |
| 500 | An internal server error occurred within the Direct Line service. |
| 502 | The bot is unavailable or returned an error. **This is a common error code.** |

> [!NOTE]
> HTTP status code 101 is used in the WebSocket connection path, although this is likely handled by your WebSocket client.

### Errors

Any response that specifies an HTTP status code in the 4xx range or 5xx range will include an [ErrorResponse](bot-framework-rest-connector-api-reference.md#errorresponse-object) object in the body of the response that provides information about the error. If you receive an error response in the 4xx range, inspect the **ErrorResponse** object to identify the cause of the error and resolve your issue prior to resubmitting the request.

> [!NOTE]
> HTTP status codes and values specified in the `code` property inside the **ErrorResponse** object are stable. Values specified in the `message` property inside the **ErrorResponse** object may change over time.

The following snippets show an example request and the resulting error response.

#### Request

```http
POST https://directline.botframework.com/v3/directline/conversations/abc123/activities
[detail omitted]
```

#### Response
```http
HTTP/1.1 502 Bad Gateway
[other headers]
```
```json
{
    "error": {
        "code": "BotRejectedActivity",
        "message": "Failed to send activity: bot returned an error"
    }
}
```

## Token operations 
Use these operations to create or refresh a token that a client can use to access a single conversation.

| Operation | Description |
|----|----|
| [Generate Token](#generate-token) | Generate a token for a new conversation. | 
| [Refresh Token](#refresh-token) | Refresh a token. | 

### Generate Token
Generates a token that is valid for one conversation. 
```http 
POST /v3/directline/tokens/generate
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | A [Conversation](#conversation-object) object | 

### Refresh Token
Refreshes the token. 
```http 
POST /v3/directline/tokens/refresh
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | A [Conversation](#conversation-object) object | 

## Conversation operations 
Use these operations to open a conversation with your bot and exchange activities between client and bot.

| Operation | Description |
|----|----|
| [Start Conversation](#start-conversation) | Opens a new conversation with the bot. | 
| [Get Conversation Information](#get-conversation-information) | Gets information about an existing conversation. This operation generates a new WebSocket stream URL that a client may use to [reconnect](bot-framework-rest-direct-line-3-0-reconnect-to-conversation.md) to a conversation. |
| [Get Activities](#get-activities) | Retrieves activities from the bot. |
| [Send an Activity](#send-an-activity) | Sends an activity to the bot. | 
| [Upload and Send File(s)](#upload-send-files) | Uploads and sends file(s) as attachment(s). |

### Start Conversation
Opens a new conversation with the bot. 
```http 
POST /v3/directline/conversations
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | A [Conversation](#conversation-object) object | 

### Get Conversation Information
Gets information about an existing conversation and also generates a new WebSocket stream URL that a client may use to [reconnect](bot-framework-rest-direct-line-3-0-reconnect-to-conversation.md) to a conversation. You may optionally supply the `watermark` parameter in the request URI to indicate the most recent message seen by the client.
```http 
GET /v3/directline/conversations/{conversationId}?watermark={watermark_value}
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | A [Conversation](#conversation-object) object | 

### Get Activities
Retrieves activities from the bot for the specified conversation. You may optionally supply the `watermark` parameter in the request URI to indicate the most recent message seen by the client. 

```http 
GET /v3/directline/conversations/{conversationId}/activities?watermark={watermark_value}
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | An [ActivitySet](#activityset-object) object. The response contains `watermark` as a property of the `ActivitySet` object. Clients should page through the available activities by advancing the `watermark` value until no activities are returned. | 

### Send an Activity
Sends an activity to the bot. 
```http 
POST /v3/directline/conversations/{conversationId}/activities
```

| | |
|----|----|
| **Request body** | An [Activity](bot-framework-rest-connector-api-reference.md#activity-object) object |
| **Returns** | A [ResourceResponse](bot-framework-rest-connector-api-reference.md#resourceresponse-object) that contains an `id` property which specifies the ID of the Activity that was sent to the bot. | 

### <a id="upload-send-files"></a> Upload and Send File(s)
Uploads and sends file(s) as attachment(s). Set the `userId` parameter in the request URI to specify the ID of the user that is sending the attachment(s).
```http 
POST /v3/directline/conversations/{conversationId}/upload?userId={userId}
```

| | |
|----|----|
| **Request body** | For a single attachment, populate the request body with the file contents. For multiple attachments, create a multipart request body that contains one part for each attachment, and also (optionally) one part for the [Activity](bot-framework-rest-connector-api-reference.md#activity-object) object that should serve as the container for the specified attachment(s). For more information, see [Send an activity to the bot](bot-framework-rest-direct-line-3-0-send-activity.md). |
| **Returns** | A [ResourceResponse](bot-framework-rest-connector-api-reference.md#resourceresponse-object) that contains an `id` property which specifies the ID of the Activity that was sent to the bot. | 

> [!NOTE]
> Uploaded files are deleted after 24 hours.

## Schema

Direct Line 3.0 schema includes all of the objects that are defined by the [Bot Framework v3 schema](bot-framework-rest-connector-api-reference.md#objects) as well as the `ActivitySet` object and the `Conversation` object.

### ActivitySet object 
Defines a set of activities.<br/><br/>

| Property | Type | Description |
|----|----|----|
| **activities** | [Activity](bot-framework-rest-connector-api-reference.md#activity-object)[] | Array of **Activity** objects. |
| **watermark** | string | Maximum watermark of activities within the set. A client may use the `watermark` value to indicate the most recent message it has seen either when [retrieving activities from the bot](bot-framework-rest-direct-line-3-0-receive-activities.md#http-get) or when [generating a new WebSocket stream URL](bot-framework-rest-direct-line-3-0-reconnect-to-conversation.md). |

### Conversation object
Defines a Direct Line conversation.<br/><br/>

| Property | Type | Description |
|----|----|----|
| **conversationId** | string | ID that uniquely identifies the conversation for which the specified token is valid. |
| **expires_in** | number | Number of seconds until the token expires. |
| **streamUrl** | string | URL for the conversation's message stream. |
| **token** | string | Token that is valid for the specified conversation. |

### Activities

For each [Activity](bot-framework-rest-connector-api-reference.md#activity-object) that a client receives from a bot via Direct Line:

- Attachment cards are preserved.
- URLs for uploaded attachments are hidden with a private link.
- The `channelData` property is preserved without modification. 

Clients may [receive](bot-framework-rest-direct-line-3-0-receive-activities.md) multiple activities from the bot as part of an [ActivitySet](#activityset-object). 

When a client sends an [Activity](bot-framework-rest-connector-api-reference.md#activity-object) to a bot via Direct Line:

- The `type` property specifies the type activity it is sending (typically **message**).
- The `from` property must be populated with a user ID, chosen by the client.
- Attachments may contain URLs to existing resources or URLs uploaded through the Direct Line attachment endpoint.
- The `channelData` property is preserved without modification.
- The total size of the activity, when serialized to JSON and encrypted, must not exceed 256K characters. Therefore it is recommended that activities are kept under 150K. If more data is needed consider breaking the activity into multiple and/or consider using attachments.

Clients may [send](bot-framework-rest-direct-line-3-0-send-activity.md) a single activity per request. 
