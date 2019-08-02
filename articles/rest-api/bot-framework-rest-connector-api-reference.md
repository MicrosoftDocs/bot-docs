---
title: API reference | Microsoft Docs
description: Learn about headers, operations, objects, and errors in the Bot Connector service and Bot State service. 
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 10/25/2018
---

# API reference

> [!NOTE]
> The REST API is not equivalent to the SDK. The REST API is provided to allow standard REST communication, however the preferred method of interacting with the Bot Framework is the SDK. 

Within the Bot Framework, the Bot Connector service enables your bot to exchange messages with users on channels that are configured in the Bot Framework Portal. The service uses industry-standard REST and JSON over HTTPS.

## Base URI

When a user sends a message to your bot, the incoming request contains an [Activity](#bot-framework-activity-schema) object with a `serviceUrl` property that specifies the endpoint to which your bot should send its response. To access the Bot Connector service, use the `serviceUrl` value as the base URI for API requests. 

For example, assume that your bot receives the following activity when the user sends a message to the bot.

```json
{
    "type": "message",
    "id": "bf3cc9a2f5de...",
    "timestamp": "2016-10-19T20:17:52.2891902Z",
    "serviceUrl": "https://smba.trafficmanager.net/apis",
    "channelId": "channel's name/id",
    "from": {
        "id": "1234abcd",
        "name": "user's name"
    },
    "conversation": {
        "id": "abcd1234",
        "name": "conversation's name"
    },
    "recipient": {
        "id": "12345678",
        "name": "bot's name"
    },
    "text": "Haircut on Saturday"
}
```

The `serviceUrl` property within the user's message indicates that the bot should send its response to the endpoint `https://smba.trafficmanager.net/apis`; this will be the base URI for any subsequent requests that the bot issues in the context of this conversation. If your bot will need to send a proactive message to the user, be sure to save the value of `serviceUrl`.

The following example shows the request that the bot issues to respond to the user's message. 

```http
POST https://smba.trafficmanager.net/apis/v3/conversations/abcd1234/activities/bf3cc9a2f5de... 
Authorization: Bearer eyJhbGciOiJIUzI1Ni...
Content-Type: application/json
```

```json
{
    "type": "message",
    "from": {
        "id": "12345678",
        "name": "bot's name"
    },
    "conversation": {
        "id": "abcd1234",
        "name": "conversation's name"
    },
   "recipient": {
        "id": "1234abcd",
        "name": "user's name"
    },
    "text": "I have several times available on Saturday!",
    "replyToId": "bf3cc9a2f5de..."
}
```

## Headers

### Request headers

In addition to the standard HTTP request headers, every API request that you issue must include an `Authorization` header that specifies an access token to authenticate your bot. Specify the `Authorization` header using this format:

```http
Authorization: Bearer ACCESS_TOKEN
```

For details about how to obtain an access token for your bot, see [Authenticate requests from your bot to the Bot Connector service](bot-framework-rest-connector-authentication.md#bot-to-connector).

### Response headers

In addition to the standard HTTP response headers, every response will contain an `X-Correlating-OperationId` header. The value of this header is an ID that corresponds to the Bot Framework log entry which contains details about the request. Any time that you receive an error response, you should capture the value of this header. If you are not able to independently resolve the issue, include this value in the information that you provide to the Support team when you report the issue.

## HTTP status codes

The <a href="http://www.w3.org/Protocols/rfc2616/rfc2616-sec10.html" target="_blank">HTTP status code</a> that is returned with each response indicates the outcome of the corresponding request. 

| HTTP status code | Meaning |
|----|----|
| 200 | The request succeeded. |
| 201 | The request succeeded. |
| 202 | The request has been accepted for processing. |
| 204 | The request succeeded but no content was returned. |
| 400 | The request was malformed or otherwise incorrect. |
| 401 | The bot is not authorized to make the request. |
| 403 | The bot is not allowed to perform the requested operation. |
| 404 | The requested resource was not found. |
| 500 | An internal server error occurred. |
| 503 | The service is unavailable. |

### Errors

Any response that specifies an HTTP status code in the 4xx range or 5xx range will include an [ErrorResponse](#bot-framework-activity-schema) object in the body of the response that provides information about the error. If you receive an error response in the 4xx range, inspect the **ErrorResponse** object to identify the cause of the error and resolve your issue prior to resubmitting the request.

## Conversation operations 
Use these operations to create conversations, send messages (activities), and manage the contents of conversations.

| Operation | Description |
|----|----|
| [Create Conversation](#create-conversation) | Creates a new conversation. |
| [Send to Conversation](#send-to-conversation) | Sends an activity (message) to the end of the specified conversation. |
| [Reply to Activity](#reply-to-activity) | Sends an activity (message) to the specified conversation, as a reply to the specified activity. |
| [Get Conversations](#get-conversations) | Gets a list of conversations the bot has participated in. |
| [Get Conversation Members](#get-conversation-members) | Gets the members of the specified conversation. |
| [Get Conversation Paged Members](#get-conversation-paged-members) | Gets the members of the specified conversation one page at a time. |
| [Get Activity Members](#get-activity-members) | Gets the members of the specified activity within the specified conversation. |
| [Update Activity](#update-activity) | Updates an existing activity. |
| [Delete Activity](#delete-activity) | Deletes an existing activity. |
| [Delete Conversation Member](#delete-conversation-member) | Removes a member from a conversation. |
| [Send Conversation History](#send-conversation-history) | Uploads a transcript of past activities to the conversation. |
| [Upload Attachment to Channel](#upload-attachment-to-channel) | Uploads an attachment directly into a channel's blob storage. |

### Create Conversation
Creates a new conversation.
```http 
POST /v3/conversations
```

| | |
|----|----|
| **Request body** | A `ConversationParameters` object |
| **Returns** | A `ConversationResourceResponse` object |

### Send to Conversation
Sends an activity (message) to the specified conversation. The activity will be appended to the end of the conversation according to the timestamp or semantics of the channel. To reply to a specific message within the conversation, use [Reply to Activity](#reply-to-activity) instead.
```http
POST /v3/conversations/{conversationId}/activities
```

| | |
|----|----|
| **Request body** | An [Activity](#bot-framework-activity-schema) object |
| **Returns** | An [Identification](#bot-framework-activity-schema) object | 

### Reply to Activity
Sends an activity (message) to the specified conversation, as a reply to the specified activity. The activity will be added as a reply to another activity, if the channel supports it. If the channel does not support nested replies, then this operation behaves like [Send to Conversation](#send-to-conversation).
```http
POST /v3/conversations/{conversationId}/activities/{activityId}
```

| | |
|----|----|
| **Request body** | An [Activity](#bot-framework-activity-schema) object |
| **Returns** | An [Identification](#bot-framework-activity-schema) object | 

### Get Conversations
Gets a list of conversations the bot has participated in.
```http
GET /v3/conversations?continuationToken={continuationToken}
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | A [ConversationsResult](#bot-framework-activity-schema) object | 

### Get Conversation Members
Gets the members of the specified conversation.
```http
GET /v3/conversations/{conversationId}/members
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | An array of [ChannelAccount](#bot-framework-activity-schema) objects | 

### Get Conversation Paged Members
Gets the members of the specified conversation one page at a time.
```http
GET /v3/conversations/{conversationId}/pagedmembers?pageSize={pageSize}&continuationToken={continuationToken}
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | An array of [ChannelAccount](#bot-framework-activity-schema) objects and a continuation token that can be used to get more values |

### Get Activity Members
Gets the members of the specified activity within the specified conversation.
```http
GET /v3/conversations/{conversationId}/activities/{activityId}/members
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | An array of [ChannelAccount](#bot-framework-activity-schema) objects | 

### Update Activity
Some channels allow you to edit an existing activity to reflect the new state of a bot conversation. For example, you might remove buttons from a message in the conversation after the user has clicked one of the buttons. If successful, this operation updates the specified activity within the specified conversation. 
```http
PUT /v3/conversations/{conversationId}/activities/{activityId}
```

| | |
|----|----|
| **Request body** | An [Activity](#bot-framework-activity-schema) object |
| **Returns** | An [Identification](#bot-framework-activity-schema) object | 

### Delete Activity
Some channels allow you to delete an existing activity. If successful, this operation removes the specified activity from the specified conversation.
```http
DELETE /v3/conversations/{conversationId}/activities/{activityId}
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | An HTTP Status code that indicates the outcome of the operation. Nothing is specified in the body of the response. | 

### Delete Conversation Member
Removes a member from a conversation. If that member was the last member of the conversation, the conversation will also be deleted.
```http
DELETE /v3/conversations/{conversationId}/members/{memberId}
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | An HTTP Status code that indicates the outcome of the operation. Nothing is specified in the body of the response. | 

### Send Conversation History
Uploads a transcript of past activities to the conversation so that the client can render them.
```http
POST /v3/conversations/{conversationId}/activities/history
```

| | |
|----|----|
| **Request body** | A [Transcript](#bot-framework-activity-schema) object. |
| **Returns** | A [ResourceResponse](#bot-framework-activity-schema) object. | 

### Upload Attachment to Channel
Uploads an attachment for the specified conversation directly into a channel's blob storage. This enables you to store data in a compliant store.
```http 
POST /v3/conversations/{conversationId}/attachments
```

| | |
|----|----|
| **Request body** | An [AttachmentUpload](#bot-framework-activity-schema) object. |
| **Returns** | A [ResourceResponse](#bot-framework-activity-schema) object. The **id** property specifies the attachment ID that can be used with the [Get Attachments Info](#get-attachment-info) operation and the [Get Attachment](#get-attachment) operation. | 

## Attachment operations 
Use these operations to retrieve information about an attachment as well the binary data for the file itself.

| Operation | Description |
|----|----|
| [Get Attachment Info](#get-attachment-info) | Gets information about the specified attachment, including file name, file type, and the available views (e.g., original or thumbnail). |
| [Get Attachment](#get-attachment) | Gets the specified view of the specified attachment as binary content. | 

### Get Attachment Info 
Gets information about the specified attachment, including file name, type, and the available views (e.g., original or thumbnail).
```http
GET /v3/attachments/{attachmentId}
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | An [AttachmentInfo](#bot-framework-activity-schema) object | 

### Get Attachment
Gets the specified view of the specified attachment as binary content.
```http
GET /v3/attachments/{attachmentId}/views/{viewId}
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | Binary content that represents the specified view of the specified attachment | 

## State operations
The Microsoft Bot Framework State service is retired as of March 30, 2018. Previously, bots built on the Azure Bot Service or the Bot Builder SDK had a default connection to this service hosted by Microsoft to store bot state data. Bots will need to be updated to use their own state storage.

| Operation | Description |
|----|----|
| `Set User Data` | Stores state data for a specific user on a channel. |
| `Set Conversation Data` | Stores state data for a specific conversation on a channel. |
| `Set Private Conversation Data` | Stores state data for a specific user within the context of a specific conversation on a channel. |
| `Get User Data` | Retrieves state data that has previously been stored for a specific user across all conversations on a channel. |
| `Get Conversation Data` | Retrieves state data that has previously been stored for a specific conversation on a channel. |
| `Get Private Conversation Data` | Retrieves state data that has previously been stored for a specific user within the context of a specific conversation on a channel. |
| `Delete State For User` | Deletes state data that has previously been stored for a user. |

## Bot Framework Activity schema

See the [Bot Framework Activity schema](https://aka.ms/botSpecs-activitySchema) for the objects and properties that your bot can use to communicate with a user.
