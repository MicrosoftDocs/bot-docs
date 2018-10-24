---
title: Send an activity the bot | Microsoft Docs
description: Learn how to send an activity to the bot using Direct Line API v3.0. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# Send an activity to the bot

Using the Direct Line 3.0 protocol, clients and bots may exchange several different types of [activities](bot-framework-rest-connector-activities.md), including **message** activities, **typing** activities, and custom activities that the bot supports. A client may send a single activity per request. 

## Send an activity

To send an activity to the bot, the client must create an [Activity](bot-framework-rest-connector-api-reference.md#activity-object) object to define the activity and then issue a `POST` request to `https://directline.botframework.com/v3/directline/conversations/{conversationId}/activities`, specifying the Activity object in the body of the request.

The following snippets provide an example of the Send Activity request and response.

### Request

```http
POST https://directline.botframework.com/v3/directline/conversations/abc123/activities
Authorization: Bearer RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0
Content-Type: application/json
[other headers]
```

```json
{
    "type": "message",
    "from": {
        "id": "user1"
    },
    "text": "hello"
}
```

### Response

When the activity is delivered to the bot, the service responds with an HTTP status code that reflects the bot's status code. If the bot generates an error, an HTTP 502 response ("Bad Gateway") is returned to the client in response to its Send Activity request. If the POST is successful, the response contains a JSON payload that specifies the ID of the Activity that was sent to the bot.

```http
HTTP/1.1 200 OK
[other headers]
```

```json
{
    "id": "0001"
}
```

### Total time for the Send Activity request/response

The total time to POST a message to a Direct Line conversation is the sum of the following:

- Transit time for the HTTP request to travel from the client to the Direct Line service
- Internal processing time within Direct Line (typically less than 120ms)
- Transit time from the Direct Line service to the bot
- Processing time within the bot
- Transit time for the HTTP response to travel back to the client

## Send attachment(s) to the bot

In some situations, a client may need to send attachments to the bot such as images or documents. A client may send attachments to the bot either by [specifying the URL(s)](#send-by-url) of the attachment(s) within the [Activity](bot-framework-rest-connector-api-reference.md#activity-object) object that it sends using `POST /v3/directline/conversations/{conversationId}/activities` or by [uploading attachment(s)](#upload-attachments) using `POST /v3/directline/conversations/{conversationId}/upload`.

## <a id="send-by-url"></a> Send attachment(s) by URL

To send one or more attachments as part of the [Activity](bot-framework-rest-connector-api-reference.md#activity-object) object using `POST /v3/directline/conversations/{conversationId}/activities`, simply include one or more [Attachment](bot-framework-rest-connector-api-reference.md#attachment-object) objects within the Activity object and set the `contentUrl` property of each Attachment object to specify the HTTP, HTTPS, or `data` URI of the attachment.

## <a id="upload-attachments"></a> Send attachment(s) by upload

Often, a client may have image(s) or document(s) on a device that it wants to send to the bot, but no URLs corresponding to those files. In this situation, a client can can issue a `POST /v3/directline/conversations/{conversationId}/upload` request to send attachments to the bot by upload. The format and contents of the request will depend upon whether the client is [sending a single attachment](#upload-one-attachment) or [sending multiple attachments](#upload-multiple-attachments).

### <a id="upload-one-attachment"></a> Send a single attachment by upload

To send a single attachment by upload, issue this request: 

```http
POST https://directline.botframework.com/v3/directline/conversations/{conversationId}/upload?userId={userId}
Authorization: Bearer SECRET_OR_TOKEN
Content-Type: TYPE_OF_ATTACHMENT
Content-Disposition: ATTACHMENT_INFO
[other headers]

[file content]
```

In this request URI, replace **{conversationId}** with the ID of the conversation and **{userId}** with the ID of the user that is sending the message. The `userId` parameter is required. In the request headers, set `Content-Type` to specify the attachment's type and set `Content-Disposition` to specify the attachment's filename.

The following snippets provide an example of the Send (single) Attachment request and response.

#### Request

```http
POST https://directline.botframework.com/v3/directline/conversations/abc123/upload?userId=user1
Authorization: Bearer RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0
Content-Type: image/jpeg
Content-Disposition: name="file"; filename="badjokeeel.jpg"
[other headers]

[JPEG content]
```

#### Response

If the request is successful, a **message** Activity is sent to the bot when the upload completes and the response that the client receives will contain the ID of the Activity that was sent.

```http
HTTP/1.1 200 OK
[other headers]
```

```json
{
  "id": "0003"
}
```

### <a id="upload-multiple-attachments"></a> Send multiple attachments by upload

To send multiple attachments by upload, `POST` a multipart request to the `/v3/directline/conversations/{conversationId}/upload` endpoint. Set the `Content-Type` header of the request to `multipart/form-data` and include the `Content-Type` header and `Content-Disposition` header for each part to specify each attachment's type and filename. In the request URI, set the `userId` parameter to the ID of the user that is sending the message. 

You may include an [Activity](bot-framework-rest-connector-api-reference.md#activity-object) object within the request by adding a part that specifies the `Content-Type` header value `application/vnd.microsoft.activity`. If the request includes an Activity, the attachments that are specified by other parts of the payload are added as attachments to that Activity before it is sent. If the request does not include an Activity, an empty Activity is created to serve as the container in which the specified attachments are sent.

The following snippets provide an example of the Send (multiple) Attachments request and response. In this example, the request sends a message that contains some text and a single image attachment. Additional parts could be added to the request to include multiple attachments in this message.

#### Request

```http
POST https://directline.botframework.com/v3/directline/conversations/abc123/upload?userId=user1
Authorization: Bearer RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0
Content-Type: multipart/form-data; boundary=----DD4E5147-E865-4652-B662-F223701A8A89
[other headers]

----DD4E5147-E865-4652-B662-F223701A8A89
Content-Type: image/jpeg
Content-Disposition: form-data; name="file"; filename="badjokeeel.jpg"
[other headers]

[JPEG content]

----DD4E5147-E865-4652-B662-F223701A8A89
Content-Type: application/vnd.microsoft.activity
[other headers]

{
  "type": "message",
  "from": {
    "id": "user1"
  },
  "text": "Hey I just IM'd you\n\nand this is crazy\n\nbut here's my webhook\n\nso POST me maybe"
}

----DD4E5147-E865-4652-B662-F223701A8A89
```

#### Response

If the request is successful, a message Activity is sent to the bot when the upload completes and the response that the client receives will contain the ID of the Activity that was sent.

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
- [Start a conversation](bot-framework-rest-direct-line-3-0-start-conversation.md)
- [Reconnect to a conversation](bot-framework-rest-direct-line-3-0-reconnect-to-conversation.md)
- [Receive activities from the bot](bot-framework-rest-direct-line-3-0-receive-activities.md)
- [End a conversation](bot-framework-rest-direct-line-3-0-end-conversation.md)
