---
title: Send a message the bot | Microsoft Docs
description: Learn how to send a message to the bot using Direct Line API v1.1. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# Send a message to the bot

> [!IMPORTANT]
> This article describes how to send a message to the bot using Direct Line API 1.1. If you are creating a new connection between your client application and bot, use [Direct Line API 3.0](bot-framework-rest-direct-line-3-0-send-activity.md) instead.

Using the Direct Line 1.1 protocol, clients can exchange messages with bots. These messages are converted to the schema that the bot supports (Bot Framework v1 or Bot Framework v3). A client may send a single message per request. 

## Send a message

To send a message to the bot, the client must create a [Message](bot-framework-rest-direct-line-1-1-api-reference.md#message-object) object to define the message and then issue a `POST` request to `https://directline.botframework.com/api/conversations/{conversationId}/messages`, specifying the Message object in the body of the request.

The following snippets provide an example of the Send Message request and response.

### Request

```http
POST https://directline.botframework.com/api/conversations/abc123/messages
Authorization: Bearer RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0
[other headers]
```

```json
{
  "text": "hello",
  "from": "user1"
}
```

### Response

When the message is delivered to the bot, the service responds with an HTTP status code that reflects the bot's status code. If the bot generates an error, an HTTP 500 response ("Internal Server Error") is returned to the client in response to its Send Message request. If the POST is successful, the service returns an HTTP 204 status code. No data is returned in body of the response. The client's message and any messages from the bot can be obtained via [polling](bot-framework-rest-direct-line-1-1-receive-messages.md). 

```http
HTTP/1.1 204 No Content
[other headers]
```

### Total time for the Send Message request/response

The total time to POST a message to a Direct Line conversation is the sum of the following:

- Transit time for the HTTP request to travel from the client to the Direct Line service
- Internal processing time within Direct Line (typically less than 120ms)
- Transit time from the Direct Line service to the bot
- Processing time within the bot
- Transit time for the HTTP response to travel back to the client

## Send attachment(s) to the bot

In some situations, a client may need to send attachments to the bot such as images or documents. A client may send attachments to the bot either by [specifying the URL(s)](#send-by-url) of the attachment(s) within the [Message](bot-framework-rest-direct-line-1-1-api-reference.md#message-object) object that it sends using `POST /api/conversations/{conversationId}/messages` or by [uploading attachment(s)](#upload-attachments) using `POST /api/conversations/{conversationId}/upload`.

## <a id="send-by-url"></a> Send attachment(s) by URL

To send one or more attachments as part of the [Message](bot-framework-rest-direct-line-1-1-api-reference.md#message-object) object using `POST /api/conversations/{conversationId}/messages`, specify the attachment URL(s) within the message's `images` array and/or `attachments` array.

## <a id="upload-attachments"></a> Send attachment(s) by upload

Often, a client may have image(s) or document(s) on a device that it wants to send to the bot, but no URLs corresponding to those files. In this situation, a client can can issue a `POST /api/conversations/{conversationId}/upload` request to send attachments to the bot by upload. The format and contents of the request will depend upon whether the client is [sending a single attachment](#upload-one-attachment) or [sending multiple attachments](#upload-multiple-attachments).

### <a id="upload-one-attachment"></a> Send a single attachment by upload

To send a single attachment by upload, issue this request: 

```http
POST https://directline.botframework.com/api/conversations/{conversationId}/upload?userId={userId}
Authorization: Bearer SECRET_OR_TOKEN
Content-Type: TYPE_OF_ATTACHMENT
Content-Disposition: ATTACHMENT_INFO
[other headers]

[file content]
```

In this request URI, replace **{conversationId}** with the ID of the conversation and **{userId}** with the ID of the user that is sending the message. In the request headers, set `Content-Type` to specify the attachment's type and set `Content-Disposition` to specify the attachment's filename.

The following snippets provide an example of the Send (single) Attachment request and response.

#### Request

```http
POST https://directline.botframework.com/api/conversations/abc123/upload?userId=user1
Authorization: Bearer RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0
Content-Type: image/jpeg
Content-Disposition: name="file"; filename="badjokeeel.jpg"
[other headers]

[JPEG content]
```

#### Response

If the request is successful, a message is sent to the bot when the upload completes and the service returns an HTTP 204 status code.

```http
HTTP/1.1 204 No Content
[other headers]
```

### <a id="upload-multiple-attachments"></a> Send multiple attachments by upload

To send multiple attachments by upload, `POST` a multipart request to the `/api/conversations/{conversationId}/upload` endpoint. Set the `Content-Type` header of the request to `multipart/form-data` and include the `Content-Type` header and `Content-Disposition` header for each part to specify each attachment's type and filename. In the request URI, set the `userId` parameter to the ID of the user that is sending the message. 

You may include a [Message](bot-framework-rest-direct-line-1-1-api-reference.md#message-object) object within the request by adding a part that specifies the `Content-Type` header value `application/vnd.microsoft.bot.message`. This allows the client to customize the message that contains the attachment(s). If the request includes a Message, the attachments that are specified by other parts of the payload are added as attachments to that Message before it is sent. 

The following snippets provide an example of the Send (multiple) Attachments request and response. In this example, the request sends a message that contains some text and a single image attachment. Additional parts could be added to the request to include multiple attachments in this message.

#### Request

```http
POST https://directline.botframework.com/api/conversations/abc123/upload?userId=user1
Authorization: Bearer RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0
Content-Type: multipart/form-data; boundary=----DD4E5147-E865-4652-B662-F223701A8A89
[other headers]

----DD4E5147-E865-4652-B662-F223701A8A89
Content-Type: image/jpeg
Content-Disposition: form-data; name="file"; filename="badjokeeel.jpg"
[other headers]

[JPEG content]

----DD4E5147-E865-4652-B662-F223701A8A89
Content-Type: application/vnd.microsoft.bot.message
[other headers]

{
  "text": "Hey I just IM'd you\n\nand this is crazy\n\nbut here's my webhook\n\nso POST me maybe",
  "from": "user1"
}

----DD4E5147-E865-4652-B662-F223701A8A89
```

#### Response

If the request is successful, a message is sent to the bot when the upload completes and the service returns an HTTP 204 status code.

```http
HTTP/1.1 204 No Content
[other headers]
```

## Additional resources

- [Key concepts](bot-framework-rest-direct-line-1-1-concepts.md)
- [Authentication](bot-framework-rest-direct-line-1-1-authentication.md)
- [Start a conversation](bot-framework-rest-direct-line-1-1-start-conversation.md)
- [Receive messages from the bot](bot-framework-rest-direct-line-1-1-receive-messages.md)