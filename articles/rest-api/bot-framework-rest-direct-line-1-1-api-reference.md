---
title: API reference - Direct Line API 1.1 | Microsoft Docs
description: Learn about headers, HTTP status codes, schema, operations, and objects in Direct Line API 1.1. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# API reference - Direct Line API 1.1

> [!IMPORTANT]
> This article contains reference information for Direct Line API 1.1. If you are creating a new connection between your client application and bot, use [Direct Line API 3.0](bot-framework-rest-direct-line-3-0-api-reference.md) instead.

You can enable your client application to communicate with your bot by using Direct Line API 1.1. Direct Line API 1.1 uses industry-standard REST and JSON over HTTPS.

## Base URI

To access Direct Line API 1.1, use this base URI for all API requests:

`https://directline.botframework.com`

## Headers

In addition to the standard HTTP request headers, a Direct Line API request must include an `Authorization` header that specifies a secret or token to authenticate the client that is issuing the request. You can specify the `Authorization` header using either the "Bearer" scheme or the "BotConnector" scheme. 

**Bearer scheme**:
```http
Authorization: Bearer SECRET_OR_TOKEN
```

**BotConnector scheme**:
```http
Authorization: BotConnector SECRET_OR_TOKEN
```

For details about how to obtain a secret or token that your client can use to authenticate its Direct Line API requests, see [Authentication](bot-framework-rest-direct-line-1-1-authentication.md).

## HTTP status codes

The <a href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html" target="_blank">HTTP status code</a> that is returned with each response indicates the outcome of the corresponding request. 

| HTTP status code | Meaning |
|----|----|
| 200 | The request succeeded. |
| 204 | The request succeeded but no content was returned. |
| 400 | The request was malformed or otherwise incorrect. |
| 401 | The client is not authorized to make the request. Often this status code occurs because the `Authorization` header is missing or malformed. |
| 403 | The client is not allowed to perform the requested operation. Often this status code occurs because the `Authorization` header specifies an invalid token or secret. |
| 404 | The requested resource was not found. Typically this status code indicates an invalid request URI. |
| 500 | An internal server error occurred within the Direct Line service |
| 502 | A failure occurred within the bot; the bot is unavailable or returned an error.  **This is a common error code.** |

## Token operations 
Use these operations to create or refresh a token that a client can use to access a single conversation.

| Operation | Description |
|----|----|
| [Generate Token](#generate-token) | Generate a token for a new conversation. | 
| [Refresh Token](#refresh-token) | Refresh a token. | 

### Generate Token
Generates a token that is valid for one conversation. 
```http 
POST /api/tokens/conversation
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | A string that represents the token | 

### Refresh Token
Refreshes the token. 
```http 
GET /api/tokens/{conversationId}/renew
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | A string that represents the new token | 

## Conversation operations 
Use these operations to open a conversation with your bot and exchange messages between client and bot.

| Operation | Description |
|----|----|
| [Start Conversation](#start-conversation) | Opens a new conversation with the bot. | 
| [Get Messages](#get-messages) | Retrieves messages from the bot. |
| [Send a Message](#send-a-message) | Sends a message to the bot. | 
| [Upload and Send File(s)](#upload-send-files) | Uploads and sends file(s) as attachment(s). |

### Start Conversation
Opens a new conversation with the bot. 
```http 
POST /api/conversations
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | A [Conversation](#conversation-object) object | 

### Get Messages
Retrieves messages from the bot for the specified conversation. Set the `watermark` parameter in the request URI to indicate the most recent message seen by the client. 

```http
GET /api/conversations/{conversationId}/messages?watermark={watermark_value}
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | A [MessageSet](#messageset-object) object. The response contains `watermark` as a property of the `MessageSet` object. Clients should page through the available messages by advancing the `watermark` value until no messages are returned. | 

### Send a Message
Sends a message to the bot. 
```http 
POST /api/conversations/{conversationId}/messages
```

| | |
|----|----|
| **Request body** | A [Message](#message-object) object |
| **Returns** | No data is returned in body of the response. The service responds with an HTTP 204 status code if the message was sent successfully. The client may obtain its sent message (along with any messages that the bot has sent to the client) by using the [Get Messages](#get-messages) operation. |

### <a id="upload-send-files"></a> Upload and Send File(s)
Uploads and sends file(s) as attachment(s). Set the `userId` parameter in the request URI to specify the ID of the user that is sending the attachments.
```http 
POST /api/conversations/{conversationId}/upload?userId={userId}
```

| | |
|----|----|
| **Request body** | For a single attachment, populate the request body with the file contents. For multiple attachments, create a multipart request body that contains one part for each attachment, and also (optionally) one part for the [Message](#message-object) object that should serve as the container for the specified attachment(s). For more information, see [Send a message to the bot](bot-framework-rest-direct-line-1-1-send-message.md). |
| **Returns** | No data is returned in body of the response. The service responds with an HTTP 204 status code if the message was sent successfully. The client may obtain its sent message (along with any messages that the bot has sent to the client) by using the [Get Messages](#get-messages) operation. | 

> [!NOTE]
> Uploaded files are deleted after 24 hours.

## Schema

Direct Line 1.1 schema is a simplified copy of the Bot Framework v1 schema that includes the following objects.

### Message object

Defines a message that a client sends to a bot or receives from a bot.

| Property | Type | Description |
|----|----|----|
| **id** | string | ID that uniquely identifies the message (assigned by Direct Line). | 
| **conversationId** | string | ID that identifies the conversation.  | 
| **created** | string | Date and time that the message was created, expressed in <a href="https://en.wikipedia.org/wiki/ISO_8601" target="_blank">ISO-8601</a> format. | 
| **from** | string | ID that identifies the user that is the sender of the message. When creating a message, clients should set this property to a stable user ID. Although Direct Line will assign a user ID if none is supplied, this typically results in unexpected behavior. | 
| **text** | string | Text of the message that is sent from user to bot or bot to user. | 
| **channelData** | object | An object that contains channel-specific content. Some channels provide features that require additional information that cannot be represented using the attachment schema. For those cases, set this property to the channel-specific content as defined in the channel's documentation. This data is sent unmodified between client and bot. This property must either be set to a complex object or left empty. Do not set it to a string, number, or other simple type. | 
| **images** | string[] | Array of strings that contains the URL(s) for the image(s) that the message contains. In some cases, strings in this array may be relative URLs. If any string in this array does not begin with either "http" or "https", prepend `https://directline.botframework.com` to the string to form the complete URL. | 
| **attachments** | [Attachment](#attachment-object)[] | Array of **Attachment** objects that represent the non-image attachments that the message contains. Each object in the array contains a `url` property and a `contentType` property. In messages that a client receives from a bot, the `url` property may sometimes specify a relative URL. For any `url` property value that does not begin with either "http" or "https", prepend `https://directline.botframework.com` to the string to form the complete URL. | 

The following example shows a Message object that contains all possible properties. In most cases when creating a message, the client only needs to supply the `from` property and at least one content property (e.g., `text`, `images`, `attachments`, or `channelData`).

```json
{
    "id": "CuvLPID4kDb|000000000000000004",
    "conversationId": "CuvLPID4kDb",
    "created": "2016-10-28T21:19:51.0357965Z",
    "from": "examplebot",
    "text": "Hello!",
    "channelData": {
        "examplefield": "abc123"
    },
    "images": [
        "/attachments/CuvLPID4kDb/0.jpg?..."
    ],
    "attachments": [
        {
            "url": "https://example.com/example.docx",
            "contentType": "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
        }, 
        {
            "url": "https://example.com/example.doc",
            "contentType": "application/msword"
        }
    ]
}
```

### MessageSet object 
Defines a set of messages.<br/><br/>

| Property | Type | Description |
|----|----|----|
| **messages** | [Message](#message-object)[] | Array of **Message** objects. |
| **watermark** | string | Maximum watermark of messages within the set. A client may use the `watermark` value to indicate the most recent message it has seen when [retrieving messages from the bot](bot-framework-rest-direct-line-1-1-receive-messages.md). |

### Attachment object
Defines a non-image attachment.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **contentType** | string | The media type of the content in the attachment. |
| **url** | string | URL for the content of the attachment. |

### Conversation object
Defines a Direct Line conversation.<br/><br/>

| Property | Type | Description |
|----|----|----|
| **conversationId** | string | ID that uniquely identifies the conversation for which the specified token is valid. |
| **token** | string | Token that is valid for the specified conversation. |
| **expires_in** | number | Number of seconds until the token expires. |

### Error object
Defines an error.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **code** | string | Error code. One of these values: **MissingProperty**, **MalformedData**, **NotFound**, **ServiceError**, **Internal**, **InvalidRange**, **NotSupported**, **NotAllowed**, **BadCertificate**. |
| **message** | string | A description of the error. |
| **statusCode** | number | Status code. |

### ErrorMessage object
A standardized message error payload.<br/><br/> 


|        Property        |          Type          |                                 Description                                 |
|------------------------|------------------------|-----------------------------------------------------------------------------|
| <strong>error</strong> | [Error](#error-object) | An <strong>Error</strong> object that contains information about the error. |

