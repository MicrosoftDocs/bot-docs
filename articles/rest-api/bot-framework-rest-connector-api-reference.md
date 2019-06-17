---
title: API reference | Microsoft Docs
description: Learn about headers, operations, objects, and errors in the Bot Connector service and Bot State service. 
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 10/25/2018
---

# API reference

> [!NOTE]
> The REST API is not equivalent to the SDK. The REST API is provided to allow standard REST communication, however the preferred method of interacting with the Bot Framework is the SDK. 

Within the Bot Framework, the Bot Connector service enables your bot to exchange messages with users on channels that are configured in the Bot Framework Portal and the Bot State service enables your bot to store and retrieve state data that is related to the conversations that your bot conducts using the Bot Connector service. Both services use industry-standard REST and JSON over HTTPS.

> [!IMPORTANT]
> The Bot Framework State Service API is not recommended for production environments, and may be deprecated in a future release. It is recommended that you update your bot code to use the in-memory storage for testing purposes or use one of the **Azure Extensions** for production bots. For more information, see the **Manage state data** topic for [.NET](~/dotnet/bot-builder-dotnet-state.md) or [Node](~/nodejs/bot-builder-nodejs-state.md) implementation.

## Base URI

When a user sends a message to your bot, the incoming request contains an [Activity](#activity-object) object with a `serviceUrl` property that specifies the endpoint to which your bot should send its response. To access the Bot Connector service or the Bot State service, use the `serviceUrl` value as the base URI for API requests. 

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

Any response that specifies an HTTP status code in the 4xx range or 5xx range will include an [ErrorResponse](#errorresponse-object) object in the body of the response that provides information about the error. If you receive an error response in the 4xx range, inspect the **ErrorResponse** object to identify the cause of the error and resolve your issue prior to resubmitting the request.

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
| **Request body** | A [Conversation](#conversation-object) object |
| **Returns** | A [ConversationResourceResponse](#conversationresourceresponse-object) object | 

### Send to Conversation
Sends an activity (message) to the specified conversation. The activity will be appended to the end of the conversation according to the timestamp or semantics of the channel. To reply to a specific message within the conversation, use [Reply to Activity](#reply-to-activity) instead.
```http
POST /v3/conversations/{conversationId}/activities
```

| | |
|----|----|
| **Request body** | An [Activity](#activity-object) object |
| **Returns** | An [Identification](#identification-object) object | 

### Reply to Activity
Sends an activity (message) to the specified conversation, as a reply to the specified activity. The activity will be added as a reply to another activity, if the channel supports it. If the channel does not support nested replies, then this operation behaves like [Send to Conversation](#send-to-conversation).
```http
POST /v3/conversations/{conversationId}/activities/{activityId}
```

| | |
|----|----|
| **Request body** | An [Activity](#activity-object) object |
| **Returns** | An [Identification](#identification-object) object | 

### Get Conversations
Gets a list of conversations the bot has participated in.
```http
GET /v3/conversations?continuationToken={continuationToken}
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | A [ConversationsResult](#conversationsresult-object) object | 

### Get Conversation Members
Gets the members of the specified conversation.
```http
GET /v3/conversations/{conversationId}/members
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | An array of [ChannelAccount](#channelaccount-object) objects | 

### Get Conversation Paged Members
Gets the members of the specified conversation one page at a time.
```http
GET /v3/conversations/{conversationId}/pagedmembers?pageSize={pageSize}&continuationToken={continuationToken}
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | An array of [ChannelAccount](#channelaccount-object) objects and a continuation token that can be used to get more values |

### Get Activity Members
Gets the members of the specified activity within the specified conversation.
```http
GET /v3/conversations/{conversationId}/activities/{activityId}/members
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | An array of [ChannelAccount](#channelaccount-object) objects | 

### Update Activity
Some channels allow you to edit an existing activity to reflect the new state of a bot conversation. For example, you might remove buttons from a message in the conversation after the user has clicked one of the buttons. If successful, this operation updates the specified activity within the specified conversation. 
```http
PUT /v3/conversations/{conversationId}/activities/{activityId}
```

| | |
|----|----|
| **Request body** | An [Activity](#activity-object) object |
| **Returns** | An [Identification](#identification-object) object | 

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
| **Request body** | A [Transcript](#transcript-object) object. |
| **Returns** | A [ResourceResponse](#resourceresponse-object) object. | 

### Upload Attachment to Channel
Uploads an attachment for the specified conversation directly into a channel's blob storage. This enables you to store data in a compliant store.
```http 
POST /v3/conversations/{conversationId}/attachments
```

| | |
|----|----|
| **Request body** | An [AttachmentUpload](#attachmentupload-object) object. |
| **Returns** | A [ResourceResponse](#resourceresponse-object) object. The **id** property specifies the attachment ID that can be used with the [Get Attachments Info](#get-attachment-info) operation and the [Get Attachment](#get-attachment) operation. | 

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
| **Returns** | An [AttachmentInfo](#attachmentinfo-object) object | 

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
Use these operations to store and retrieve state data.

| Operation | Description |
|----|----|
| [Set User Data](#set-user-data) | Stores state data for a specific user on a channel. | 
| [Set Conversation Data](#set-conversation-data) | Stores state data for a specific conversation on a channel. | 
| [Set Private Conversation Data](#set-private-conversation-data) | Stores state data for a specific user within the context of a specific conversation on a channel. | 
| [Get User Data](#get-user-data) | Retrieves state data that has previously been stored for a specific user across all conversations on a channel. | 
| [Get Conversation Data](#get-conversation-data) | Retrieves state data that has previously been stored for a specific conversation on a channel. | 
| [Get Private Conversation Data](#get-private-conversation-data) | Retrieves state data that has previously been stored for a specific user within the context of a specific conversation on a channel. | 
| [Delete State For User](#delete-state-for-user) | Deletes state data that has previously been stored for a user by using the [Set User Data](#set-user-data) operation or the [Set Private Conversation Data](#set-private-conversation-data) operation. | 

### Set User Data
Stores state data for the specified user on the specified channel.
```http
POST /v3/botstate/{channelId}/users/{userId} 
```

| | |
|----|----|
| **Request body** | A [BotData](#botdata-object) object |
| **Returns** | A [BotData](#botdata-object) object | 

### Set Conversation Data
Stores state data for the specified conversation on the specified channel.
```http
POST /v3/botstate/{channelId}/conversations/{conversationId}
```

| | |
|----|----|
| **Request body** | A [BotData](#botdata-object) object |
| **Returns** | A [BotData](#botdata-object) object | 

### Set Private Conversation Data
Stores state data for the specified user within the context of the specified conversation on the specified channel.
```http
POST /v3/botstate/{channelId}/conversations/{conversationId}/users/{userId} 
```

| | |
|----|----|
| **Request body** | A [BotData](#botdata-object) object |
| **Returns** | A [BotData](#botdata-object) object | 

### Get User Data
Retrieves state data that has previously been stored for the specified user across all conversations on the specified channel.
```http
GET /v3/botstate/{channelId}/users/{userId} 
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | A [BotData](#botdata-object) object | 

### Get Conversation Data
Retrieves state data that has previously been stored for the specified conversation on the specified channel.
```http
GET /v3/botstate/{channelId}/conversations/{conversationId} 
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | A [BotData](#botdata-object) object | 

### Get Private Conversation Data
Retrieves state data that has previously been stored for the specified user within the context of the specified conversation on the specified channel.
```http
GET /v3/botstate/{channelId}/conversations/{conversationId}/users/{userId} 
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | A [BotData](#botdata-object) object | 

### Delete State For User
Deletes state data that has previously been stored for the specified user on the specified channel by using either the [Set User Data](#set-user-data) operation or the [Set Private Conversation Data](#set-private-conversation-data) operation.
```http
DELETE /v3/botstate/{channelId}/users/{userId} 
```

| | |
|----|----|
| **Request body** | n/a |
| **Returns** | An array of strings (IDs) | 

## <a id="objects"></a> Schema

Schema defines the object and its properties that your bot can use to communicate with a user. 

| Object | Description |
| ---- | ---- |
| [Activity object](#activity-object) | Defines a message that is exchanged between bot and user. |
| [AnimationCard object](#animationcard-object) | Defines a card that can play animated GIFs or short videos. |
| [Attachment object](#attachment-object) | Defines additional information to include in the message. An attachment may be a media file (e.g., audio, video, image, file) or a rich card. |
| [AttachmentData object](#attachmentdata-object) | Describes an attachment data. |
| [AttachmentInfo object](#attachmentinfo-object) | Describes an attachment. |
| [AttachmentView object](#attachmentview-object) | Defines a attachment view. |
| [AttachmentUpload object](#attachmentupload-object) | Defines an attachment to be uploaded. |
| [AudioCard object](#audiocard-object) | Defines a card that can play an audio file. |
| [BotData object](#botdata-object) | Defines state data for a user, a conversation, or a user in the context of a specific conversation that is stored using the Bot State service. |
| [CardAction object](#cardaction-object) | Defines an action to perform. |
| [CardImage object](#cardimage-object) | Defines an image to display on a card. |
| [ChannelAccount object](#channelaccount-object) | Defines a bot or user account on the channel. |
| [Conversation object](#conversation-object) | Defines a conversation, including the bot and users that are included within the conversation. |
| [ConversationAccount object](#conversationaccount-object) | Defines a conversation in a channel. |
| [ConversationMembers object](#conversationmembers-object) | Defines the members of a conversation. |
| [ConversationParameters object](#conversationparameters-object) | Define parameters for creating a new conversation |
| [ConversationReference object](#conversationreference-object) | Defines a particular point in a conversation. |
| [ConversationResourceResponse object](#conversationresourceresponse-object) | Defines a response to [Create Conversation](#create-conversation). |
| [ConversationsResult object](#conversationsresult-object) | Defines the result of a call to [Get Conversations](#get-conversations). |
| [Entity object](#entity-object) | Defines an entity object. |
| [Error object](#error-object) | Defines an error. |
| [ErrorResponse object](#errorresponse-object) | Defines an HTTP API response. |
| [Fact object](#fact-object) | Defines a key-value pair that contains a fact. |
| [GeoCoordinates object](#geocoordinates-object) | Defines a geographical location using World Geodetic System (WSG84) coordinates. |
| [HeroCard object](#herocard-object) | Defines a card with a large image, title, text, and action buttons. |
| [Identification object](#identification-object) | Identifies a resource. |
| [MediaEventValue object](#mediaeventvalue-object) | Supplementary parameter for media events. |
| [MediaUrl object](#mediaurl-object) | Defines the URL to a media file's source. |
| [Mention object](#mention-object) | Defines a user or bot that was mentioned in the conversation. |
| [MessageReaction object](#messagereaction-object) | Defines a reaction to a message. |
| [Place object](#place-object) | Defines a place that was mentioned in the conversation. |
| [ReceiptCard object](#receiptcard-object) | Defines a card that contains a receipt for a purchase. |
| [ReceiptItem object](#receiptitem-object) | Defines a line item within a receipt. |
| [ResourceResponse object](#resourceresponse-object) | Defines a resource. |
| [SemanticAction object](#semanticaction-object) | Defines a reference to a programmatic action. |
| [SignInCard object](#signincard-object) | Defines a card that lets a user sign in to a service. |
| [SuggestedActions object](#suggestedactions-object) | Defines the options from which a user can choose. |
| [ThumbnailCard object](#thumbnailcard-object) | Defines a card with a thumbnail image, title, text, and action buttons. |
| [ThumbnailUrl object](#thumbnailurl-object) | Defines the URL to an image's source. |
| [Transcript object](#transcript-object) | A collection of activities to be uploaded using [Send Conversation History](#send-conversation-history). |
| [VideoCard object](#videocard-object) | Defines a card that can play videos. |

### Activity object
Defines a message that is exchanged between bot and user.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **action** | string | The action to apply or that was applied. Use the **type** property to determine context for the action. For example, if **type** is **contactRelationUpdate**, the value of the **action** property would be **add** if the user added your bot to their contacts list, or **remove** if they removed your bot from their contacts list. |
| **attachments** | [Attachment](#attachment-object)[] | Array of **Attachment** objects that defines additional information to include in the message. Each attachment may be either a media file (e.g., audio, video, image, file) or a rich card. |
| **attachmentLayout** | string | Layout of the rich card **attachments** that the message includes. One of these values: **carousel**, **list**. For more information about rich card attachments, see [Add rich card attachments to messages](bot-framework-rest-connector-add-rich-cards.md). |
| **channelData** | object | An object that contains channel-specific content. Some channels provide features that require additional information that cannot be represented using the attachment schema. For those cases, set this property to the channel-specific content as defined in the channel's documentation. For more information, see [Implement channel-specific functionality](bot-framework-rest-connector-channeldata.md). |
| **channelId** | string | An ID that uniquely identifies the channel. Set by the channel. | 
| **conversation** | [ConversationAccount](#conversationaccount-object) | A **ConversationAccount** object that defines the conversation to which the activity belongs. |
| **code** | string | Code indicating why the conversation has ended. |
| **entities** | object[] | Array of objects that represents the entities that were mentioned in the message. Objects in this array may be any <a href="http://schema.org/" target="_blank">Schema.org</a> object. For example, the array may include [Mention](#mention-object) objects that identify someone who was mentioned in the conversation and [Place](#place-object) objects that identify a place that was mentioned in the conversation. |
| **from** | [ChannelAccount](#channelaccount-object) | A **ChannelAccount** object that specifies the sender of the message. |
| **historyDisclosed** | boolean | Flag that indicates whether or not history is disclosed. Default value is **false**. |
| **id** | string | ID that uniquely identifies the activity on the channel. | 
| **inputHint** | string | Value that indicates whether your bot is accepting, expecting, or ignoring user input after the message is delivered to the client. One of these values: **acceptingInput**, **expectingInput**, **ignoringInput**. |
| **locale** | string | Locale of the language that should be used to display text within the message, in the format `<language>-<country>`. The channel uses this property to indicate the user's language, so that your bot may specify display strings in that language. Default value is **en-US**. |
| **localTimestamp** | string | Date and time that the message was sent in the local time zone, expressed in <a href="https://en.wikipedia.org/wiki/ISO_8601" target="_blank">ISO-8601</a> format. |
| **membersAdded** | [ChannelAccount](#channelaccount-object)[] | Array of **ChannelAccount** objects that represents the list of users that joined the conversation. Present only if activity **type** is "conversationUpdate" and users joined the conversation. | 
| **membersRemoved** | [ChannelAccount](#channelaccount-object)[] | Array of **ChannelAccount** objects that represents the list of users that left the conversation. Present only if activity **type** is "conversationUpdate" and users left the conversation. | 
| **name** | string | Name of the operation to invoke or the name of the event. |
| **recipient** | [ChannelAccount](#channelaccount-object) | A **ChannelAccount** object that specifies the recipient of the message. |
| **relatesTo** | [ConversationReference](#conversationreference-object) | A **ConversationReference** object that defines a particular point in a conversation. |
| **replyToId** | string | The ID of the message to which this message replies. To reply to a message that the user sent, set this property to the ID of the user's message. Not all channels support threaded replies. In these cases, the channel will ignore this property and use time ordered semantics (timestamp) to append the message to the converasation. | 
| **serviceUrl** | string | URL that specifies the channel's service endpoint. Set by the channel. | 
| **speak** | string | Text to be spoken by your bot on a speech-enabled channel. To control various characteristics of your bot's speech such as voice, rate, volume, pronunciation, and pitch, specify this property in <a href="https://msdn.microsoft.com/en-us/library/hh378377(v=office.14).aspx" target="_blank">Speech Synthesis Markup Language (SSML)</a> format. |
| **suggestedActions** | [SuggestedActions](#suggestedactions-object) | A **SuggestedActions** object that defines the options from which the user can choose. |
| **summary** | string | Summary of the information that the message contains. For example, for a message that is sent on an email channel, this property may specify the first 50 characters of the email message. |
| **text** | string | Text of the message that is sent from user to bot or bot to user. See the channel's documentation for limits imposed upon the contents of this property. |
| **textFormat** | string | Format of the message's **text**. One of these values: **markdown**, **plain**, **xml**. For details about text format, see [Create messages](bot-framework-rest-connector-create-messages.md). |
| **timestamp** | string | Date and time that the message was sent in the UTC time zone, expressed in <a href="https://en.wikipedia.org/wiki/ISO_8601" target="_blank">ISO-8601</a> format. |
| **topicName** | string | Topic of the conversation to which the activity belongs. |
| **type** | string | Type of activity. One of these values: **contactRelationUpdate**, **conversationUpdate**, **deleteUserData**, **message**, **typing**, **event**, and **endOfConversation**. For details about activity types, see [Activities overview](bot-framework-rest-connector-activities.md). |
| **value** | object | Open-ended value. |
| **semanticAction** |[SemanticAction](#semanticaction-object) | A **SemanticAction** object that represents a reference to a programmatic action. |

<a href="#objects">Back to Schema table</a>

### AnimationCard object
Defines a card that can play animated GIFs or short videos.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **autoloop** | boolean | Flag that indicates whether to replay the list of animated GIFs when the last one ends. Set this property to **true** to automatically replay the animation; otherwise, **false**. The default value is **true**. |
| **autostart** | boolean | Flag that indicates whether to automatically play the animation when the card is displayed. Set this property to **true** to automatically play the animation; otherwise, **false**. The default value is **true**. |
| **buttons** | [CardAction](#cardaction-object)[] | Array of **CardAction** objects that enable the user to perform one or more actions. The channel determines the number of buttons that you may specify. |
| **duration** | string | The length of the media content, in [ISO 8601 duration format](https://www.iso.org/iso-8601-date-and-time-format.html). |
| **image** | [ThumbnailUrl](#thumbnailurl-object) | A **ThumbnailUrl** object that specifies the image to display on the card. |
| **media** | [MediaUrl](#mediaurl-object)[] | Array of **MediaUrl** objects that specifies the list of animated GIFs to play. |
| **shareable** | boolean | Flag that indicates whether the animation may be shared with others. Set this property to **true** if the animation may be shared; otherwise, **false**. The default value is **true**. |
| **subtitle** | string | Subtitle to display under the card's title. |
| **text** | string | Description or prompt to display under the card's title or subtitle. |
| **title** | string | Title of the card. |
| **value** | object | Supplementary parameter for this card |

<a href="#objects">Back to Schema table</a>

### Attachment object
Defines additional information to include in the message. An attachment may be a media file (e.g., audio, video, image, file) or a rich card.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **contentType** | string | The media type of the content in the attachment. For media files, set this property to known media types such as **image/png**, **audio/wav**, and **video/mp4**. For rich cards, set this property to one of these vendor-specific types:<ul><li>**application/vnd.microsoft.card.adaptive**: A rich card that can contain any combination of text, speech, images, buttons, and input fields. Set the **content** property to an <a href="http://adaptivecards.io/documentation/#create-cardschema" target="_blank">AdaptiveCard</a> object.</li><li>**application/vnd.microsoft.card.animation**: A rich card that plays animation. Set the **content** property to an [AnimationCard](#animationcard-object) object.</li><li>**application/vnd.microsoft.card.audio**: A rich card that plays audio files. Set the **content** property to an [AudioCard](#audiocard-object) object.</li><li>**application/vnd.microsoft.card.video**: A rich card that plays videos. Set the **content** property to a [VideoCard](#videocard-object) object.</li><li>**application/vnd.microsoft.card.hero**: A Hero card. Set the **content** property to a [HeroCard](#herocard-object) object.</li><li>**application/vnd.microsoft.card.thumbnail**: A Thumbnail card. Set the **content** property to a [ThumbnailCard](#thumbnailcard-object) object.</li><li>**application/vnd.microsoft.com.card.receipt**: A Receipt card. Set the **content** property to a [ReceiptCard](#receiptcard-object) object.</li><li>**application/vnd.microsoft.com.card.signin**: A user Sign In card. Set the **content** property to a [SignInCard](#signincard-object) object.</li></ul> |
| **contentUrl** | string | URL for the content of the attachment. For example, if the attachment is an image, set **contentUrl** to the URL that represents the location of the image. Supported protocols are: HTTP, HTTPS, File, and Data. |
| **content** | object | The content of the attachment. If the attachment is a rich card, set this property to the rich card object. This property and the **contentUrl** property are mutually exclusive. |
| **name** | string | Name of the attachment. |
| **thumbnailUrl** | string | URL to a thumbnail image that the channel can use if it supports using an alternative, smaller form of **content** or **contentUrl**. For example, if you set **contentType** to **application/word** and set **contentUrl** to the location of the Word document, you might include a thumbnail image that represents the document. The channel could display the thumbnail image instead of the document. When the user clicks the image, the channel would open the document. |

<a href="#objects">Back to Schema table</a>

### AttachmentData object 
Describes an attachment data.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **name** | string | Name of the attachment. |
| **originalBase64** | string | Attachment content. |
| **thumbnailBase64** | string | Attachment thumbnail content. |
| **type** | string | Content-type of the attachment. |

<a href="#objects">Back to Schema table</a>

### AttachmentInfo object
Describes an attachment.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **name** | string | Name of the attachment. |
| **type** | string | ContentType of the attachment. |
| **views** | [AttachmentView](#attachmentview-object)[] | Array of **AttachmentView** objects that represent the available views for the attachment. |

<a href="#objects">Back to Schema table</a>

### AttachmentView object
Defines a attachment view.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **viewId** | string | View ID. |
| **size** | number | Size of the file. |

<a href="#objects">Back to Schema table</a>

<!-- TODO - can't find in swagger file -->
### AttachmentUpload object
Defines an attachment to be uploaded.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **type** | string | ContentType of the attachment. | 
| **name** | string | Name of the attachment. | 
| **originalBase64** | string | Binary data that represents the contents of the original version of the file. |
| **thumbnailBase64** | string | Binary data that represents the contents of the thumbnail version of the file. |

<a href="#objects">Back to Schema table</a>

### AudioCard object
Defines a card that can play an audio file.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **aspect** | string | Aspect ratio of the thumbnail that is specified in the **image** property. Valid values are **16:9** and **9:16**. |
| **autoloop** | boolean | Flag that indicates whether to replay the list of audio files when the last one ends. Set this property to **true** to automatically replay the audio files; otherwise, **false**. The default value is **true**. |
| **autostart** | boolean | Flag that indicates whether to automatically play the audio when the card is displayed. Set this property to **true** to automatically play the audio; otherwise, **false**. The default value is **true**. |
| **buttons** | [CardAction](#cardaction-object)[] | Array of **CardAction** objects that enable the user to perform one or more actions. The channel determines the number of buttons that you may specify. |
| **duration** | string | The length of the media content, in [ISO 8601 duration format](https://www.iso.org/iso-8601-date-and-time-format.html). |
| **image** | [ThumbnailUrl](#thumbnailurl-object) | A **ThumbnailUrl** object that specifies the image to display on the card. |
| **media** | [MediaUrl](#mediaurl-object)[] | Array of **MediaUrl** objects that specifies the list of audio files to play. |
| **shareable** | boolean | Flag that indicates whether the audio files may be shared with others. Set this property to **true** if the audio may be shared; otherwise, **false**. The default value is **true**. |
| **subtitle** | string | Subtitle to display under the card's title. |
| **text** | string | Description or prompt to display under the card's title or subtitle. |
| **title** | string | Title of the card. |
| **value** | object | Supplementary parameter for this card |

<a href="#objects">Back to Schema table</a>

<!-- TODO - can't find in swagger file -->
### BotData object
Defines state data for a user, a conversation, or a user in the context of a specific conversation that is stored using the Bot State service.<br/><br/>

| Property | Type | Description |
|----|----|----|
| **data** | object | In a request, a JSON object that specifies the properties and values to store using the Bot State service. In a response, a JSON object that specifies the properties and values that have been stored using the Bot State service. | 
| **eTag** | string | The entity tag value that you can use to control data concurrency for the data that you store using the Bot State service. For more information, see [Manage state data](bot-framework-rest-state.md). | 

<a href="#objects">Back to Schema table</a>

### CardAction object
Defines an action to perform.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **image** | string | URL of an image to display on the | 
| **text** | string | Text for the action |
| **title** | string | Text of the button. Only applicable for a button's action. |
button. Only applicable for a button's action. |
| **type** | string | Type of action to perform. For a list of valid values, see [Add rich card attachments to messages](bot-framework-rest-connector-add-rich-cards.md). |
| **value** | object | Supplementary parameter for the action. The value of this property will vary according to the action **type**. For more information, see [Add rich card attachments to messages](bot-framework-rest-connector-add-rich-cards.md). |

<a href="#objects">Back to Schema table</a>

### CardImage object
Defines an image to display on a card.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **alt** | string | Description of the image. You should include the description to support accessibility. |
| **tap** | [CardAction](#cardaction-object) | A **CardAction** object that specifies the action to perform if the user taps or clicks the image. |
| **url** | string | URL to the source of the image or the base64 binary of the image (for example, `data:image/png;base64,iVBORw0KGgo...`). |

<a href="#objects">Back to Schema table</a>

### ChannelAccount object
Defines a bot or user account on the channel.<br/><br/>

| Property | Type | Description |
|----|----|----|
| **id** | string | ID that uniquely identifies the bot or user on the channel. |
| **name** | string | Name of the bot or user. |

<a href="#objects">Back to Schema table</a>

<!--TODO can't find-->
### Conversation object
Defines a conversation, including the bot and users that are included within the conversation.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **bot** | [ChannelAccount](#channelaccount-object) | A **ChannelAccount** object that identifies the bot. |
| **isGroup** | boolean | Flag to indicate whether or not this is a group conversation. Set to **true** if this is a group conversation; otherwise, **false**. The default is **false**. To start a group conversation, the channel must support group conversations. |
| **members** | [ChannelAccount](#channelaccount-object)[] | Array of **ChannelAccount** objects that identify the members of the conversation. This list must contain a single user unless **isGroup** is set to **true**. This list may include other bots. |
| **topicName** | string | Title of the conversation. |
| **activity** | [Activity](#activity-object) | In a [Create Conversation](#create-conversation) request, an **Activity** object that defines the first message to post to the new conversation. |

<a href="#objects">Back to Schema table</a>

### ConversationAccount object
Defines a conversation in a channel.<br/><br/>

| Property | Type | Description |
|----|----|----|
| **id** | string | The ID that identifies the conversation. The ID is unique per channel. If the channel starts the conversation, it sets this ID; otherwise, the bot sets this property to the ID that it gets back in the response when it starts the conversation (see Starting a conversation). |
| **isGroup** | boolean | Flag to indicate whether the conversation contains more than two participants at the time the activity was generated. Set to **true** if this is a group conversation; otherwise, **false**. The default is **false**. |
| **name** | string | A display name that can be used to identify the conversation. |
| **conversationType** | string | Indicates the type of the conversation in channels that distingish between conversation types (e.g.: group, personal). |

<a href="#objects">Back to Schema table</a>

### ConversationMembers object
Defines the members of a conversation.<br/><br/>

| Property | Type | Description |
|----|----|----|
| **id** | string | The conversation ID. |
| **members** | array | An array of [ChannelAccount](#channelaccount-object) objects. |

<a href="#objects">Back to Schema table</a>

### ConversationParameters object
Define parameters for creating a new conversation.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **isGroup** | boolean | Indicates if this is a group conversation. |
| **bot** | [ChannelAccount](#channelaccount-object) | Address of the bot in the conversation. |
| **members** | array | List of members to add to the conversation. |
| **topicName** | string | Topic title of a conversation. This property is only used if a channel supports it. |
| **activity** | [Activity](#activity-object) | (optional) Use this activity as the initial message to the conversation when creating a new conversation. |
| **channelData** | object | Channel specific payload for creating the conversation. |

<a href="#objects">Back to Schema table</a>

### ConversationReference object
Defines a particular point in a conversation.<br/><br/>

| Property | Type | Description |
|----|----|----|
| **activityId** | string | ID that uniquely identifies the activity that this object references. | 
| **bot** | [ChannelAccount](#channelaccount-object) | A **ChannelAccount** object that identifies the bot in the conversation that this object references. |
| **channelId** | string | An ID that uniquely identifies the channel in the conversation that this object references. | 
| **conversation** | [ConversationAccount](#conversationaccount-object) | A **ConversationAccount** object that defines the conversation that this object references. |
| **serviceUrl** | string | URL that specifies the channel's service endpoint in the conversation that this object references. | 
| **user** | [ChannelAccount](#channelaccount-object) | A **ChannelAccount** object that identifies the user in the conversation that this object references. |

<a href="#objects">Back to Schema table</a>

### ConversationResourceResponse object
Defines a response to [Create Conversation](#create-conversation).<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **activityId** | string | ID of the activity. |
| **id** | string | ID of the resource. |
| **serviceUrl** | string | Service endpoint. |

<a href="#objects">Back to Schema table</a>

### ConversationsResult object
Defines the result of [Get Conversations](#get-conversations).<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **continuationToken** | string | The continuation token that can be used in subsequent calls to [Get Conversations](#get-conversations). |
| **conversations** | array | An array of [ConversationMembers](#conversationmembers-object) objects |

<a href="#objects">Back to Schema table</a>

### Error object
Defines an error.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **code** | string | Error code. |
| **message** | string | A description of the error. |

<a href="#objects">Back to Schema table</a>

### Entity object
Defines an entity object.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **type** | string | Entity type. Typically contain types from schema.org. |

<a href="#objects">Back to Schema table</a>

### ErrorResponse object
Defines an HTTP API response.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **error** | [Error](#error-object) | An **Error** object that contains information about the error. |

<a href="#objects">Back to Schema table</a>

### Fact object
Defines a key-value pair that contains a fact.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **key** | string | Name of the fact. For example, **Check-in**. The key is used as a label when displaying the fact's value. |
| **value** | string | Value of the fact. For example, **10 October 2016**. |

<a href="#objects">Back to Schema table</a>

### GeoCoordinates object
Defines a geographical location using World Geodetic System (WSG84) coordinates.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **elevation** | number | Elevation of the location. |
| **name** | string | Name of the location. |
| **latitude** | number | Latitude of the location. |
| **longitude** | number | Longitude of the location. |
| **type** | string | The type of this object. Always set to **GeoCoordinates**. |

<a href="#objects">Back to Schema table</a>

### HeroCard object
Defines a card with a large image, title, text, and action buttons.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **buttons** | [CardAction](#cardaction-object)[] | Array of **CardAction** objects that enable the user to perform one or more actions. The channel determines the number of buttons that you may specify. |
| **images** | [CardImage](#cardimage-object)[] | Array of **CardImage** objects that specifies the image to display on the card. A Hero card contains only one image. |
| **subtitle** | string | Subtitle to display under the card's title. |
| **tap** | [CardAction](#cardaction-object) | A **CardAction** object that specifies the action to perform if the user taps or clicks the card. This can be the same action as one of the buttons or a different action. |
| **text** | string | Description or prompt to display under the card's title or subtitle. |
| **title** | string | Title of the card. |

<a href="#objects">Back to Schema table</a>

<!--TODO can't find-->
### Identification object
Identifies a resource.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **id** | string | ID that uniquely identifies the resource. |

<a href="#objects">Back to Schema table</a>

### MediaEventValue object 
Supplementary parameter for media events.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **cardValue** | object | Callback parameter specified in the **Value** field of the media card that originated this event. |

<a href="#objects">Back to Schema table</a>

### MediaUrl object
Defines the URL to a media file's source.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **profile** | string | Hint that describes the media's content. |
| **url** | string | URL to the source of the media file. |

<a href="#objects">Back to Schema table</a>

<!--TODO can't find-->
### Mention object
Defines a user or bot that was mentioned in the conversation.<br/><br/> 


|          Property          |                   Type                   |                                                                                                                                                                                                                           Description                                                                                                                                                                                                                            |
|----------------------------|------------------------------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| <strong>mentioned</strong> | [ChannelAccount](#channelaccount-object) | A <strong>ChannelAccount</strong> object that specifies the user or the bot that was mentioned. Note that some channels such as Slack assign names per conversation, so it is possible that your bot's mentioned name (in the message's <strong>recipient</strong> property) may be different from the handle that you specified when you [registered](../bot-service-quickstart-registration.md) your bot. However, the account IDs for both would be the same. |
|   <strong>text</strong>    |                  string                  |                                                                                                                         The user or bot as mentioned in the conversation. For example, if the message is "@ColorBot pick me a new color," this property would be set to <strong>@ColorBot</strong>. Not all channels set this property.                                                                                                                          |
|   <strong>type</strong>    |                  string                  |                                                                                                                                                                                                   This object's type. Always set to <strong>Mention</strong>.                                                                                                                                                                                                    |

<a href="#objects">Back to Schema table</a>

### MessageReaction object
Defines a reaction to a message.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **type** | string | Type of reaction. |

<a href="#objects">Back to Schema table</a>

### Place object
Defines a place that was mentioned in the conversation.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **address** | object |  Address of a place. This property can be a `string` or a complex object of type `PostalAddress`. |
| **geo** | [GeoCoordinates](#geocoordinates-object) | A **GeoCoordinates** object that specifies the geographical coordinates of the place. |
| **hasMap** | object | Map to the place. This property can be a `string` (URL) or a complex object of type `Map`. |
| **name** | string | Name of the place. |
| **type** | string | This object's type. Always set to **Place**. |

<a href="#objects">Back to Schema table</a>

### ReceiptCard object
Defines a card that contains a receipt for a purchase.<br/><br/>

| Property | Type | Description |
|----|----|----|
| **buttons** | [CardAction](#cardaction-object)[] | Array of **CardAction** objects that enable the user to perform one or more actions. The channel determines the number of buttons that you may specify. |
| **facts** | [Fact](#fact-object)[] | Array of **Fact** objects that specify information about the purchase. For example, the list of facts for a hotel stay receipt might include the check-in date and check-out date. The channel determines the number of facts that you may specify. |
| **items** | [ReceiptItem](#receiptitem-object)[] | Array of **ReceiptItem** objects that specify the purchased items |
| **tap** | [CardAction](#cardaction-object) | A **CardAction** object that specifies the action to perform if the user taps or clicks the card. This can be the same action as one of the buttons or a different action. |
| **tax** | string | A currency-formatted string that specifies the amount of tax applied to the purchase. |
| **title** | string | Title displayed at the top of the receipt. |
| **total** | string | A currency-formatted string that specifies the total purchase price, including all applicable taxes. |
| **vat** | string | A currency-formatted string that specifies the amount of value added tax (VAT) applied to the purchase price. |

<a href="#objects">Back to Schema table</a>

### ReceiptItem object
Defines a line item within a receipt.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **image** | [CardImage](#cardimage-object) | A **CardImage** object that specifies thumbnail image to display next to the line item.  |
| **price** | string | A currency-formatted string that specifies the total price of all units purchased. |
| **quantity** | string | A numeric string that specifies the number of units purchased. |
| **subtitle** | string | Subtitle to be displayed under the line item’s title. |
| **tap** | [CardAction](#cardaction-object) | A **CardAction** object that specifies the action to perform if the user taps or clicks the line item. |
| **text** | string | Description of the line item. |
| **title** | string | Title of the line item. |

<a href="#objects">Back to Schema table</a>

### ResourceResponse object
Defines a response that contains a resource ID.<br/><br/>

|      Property       |  Type  |                Description                |
|---------------------|--------|-------------------------------------------|
| <strong>id</strong> | string | ID that uniquely identifies the resource. |

<a href="#objects">Back to Schema table</a>

### SemanticAction object
Defines a reference to a programmatic action.<br/><br/>

| Property | Type | Description |
|----|----|----|
| **id** | string | ID of this action |
| **entities** | [Entity](#entity-object) | Entities associated with this action |

<a href="#objects">Back to Schema table</a>

### SignInCard object
Defines a card that lets a user sign in to a service.<br/><br/>

| Property | Type | Description |
|----|----|----|
| **buttons** | [CardAction](#cardaction-object)[] | Array of **CardAction** objects that enable the user to sign in to a service. The channel determines the number of buttons that you may specify. |
| **text** | string | Description or prompt to include on the sign in card. |

<a href="#objects">Back to Schema table</a>

### SuggestedActions object
Defines the options from which a user can choose.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **actions** | [CardAction](#cardaction-object)[] | Array of **CardAction** objects that define the suggested actions. |
| **to** | string[] | Array of strings that contains the IDs of the recipients to whom the suggested actions should be displayed. |

<a href="#objects">Back to Schema table</a>

### ThumbnailCard object
Defines a card with a thumbnail image, title, text, and action buttons.<br/><br/>

| Property | Type | Description |
|----|----|----|
| **buttons** | [CardAction](#cardaction-object)[] | Array of **CardAction** objects that enable the user to perform one or more actions. The channel determines the number of buttons that you may specify. |
| **images** | [CardImage](#cardimage-object)[] | Array of **CardImage** objects that specify thumbnail images to display on the card. The channel determines the number of thumbnail images that you may specify. |
| **subtitle** | string | Subtitle to display under the card's title. |
| **tap** | [CardAction](#cardaction-object) | A **CardAction** object that specifies the action to perform if the user taps or clicks the card. This can be the same action as one of the buttons or a different action. |
| **text** | string | Description or prompt to display under the card's title or subtitle. |
| **title** | string | Title of the card. |

<a href="#objects">Back to Schema table</a>

### ThumbnailUrl object
Defines the URL to an image's source.<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **alt** | string | Description of the image. You should include the description to support accessibility. |
| **url** | string | URL to the source of the image or the base64 binary of the image (for example, `data:image/png;base64,iVBORw0KGgo...`). |

<a href="#objects">Back to Schema table</a>

### Transcript object
A collection of activities to be uploaded using [Send Conversation History](#send-conversation-history).<br/><br/> 

| Property | Type | Description |
|----|----|----|
| **activities** | array | An array of [Activity](#activity-object) objects. They should each have a unique ID and timestamp. |

<a href="#objects">Back to Schema table</a>

### VideoCard object
Defines a card that can play videos.<br/><br/>

| Property | Type | Description |
|----|----|----|
| **aspect** | string | Aspect ratio of a video (e.g.: 16:9, 4:3).|
| **autoloop** | boolean | Flag that indicates whether to replay the list of videos when the last one ends. Set this property to **true** to automatically replay the videos; otherwise, **false**. The default value is **true**. |
| **autostart** | boolean | Flag that indicates whether to automatically play the videos when the card is displayed. Set this property to **true** to automatically play the videos; otherwise, **false**. The default value is **true**. |
| **buttons** | [CardAction](#cardaction-object)[] | Array of **CardAction** objects that enable the user to perform one or more actions. The channel determines the number of buttons that you may specify. |
| **duration** | string | The length of the media content, in [ISO 8601 duration format](https://www.iso.org/iso-8601-date-and-time-format.html). |
| **image** | [ThumbnailUrl](#thumbnailurl-object) | A **ThumbnailUrl** object that specifies the image to display on the card. |
| **media** | [MediaUrl](#mediaurl-object)[] | Array of **MediaUrl** objects that specifies the list of videos to play. |
| **shareable** | boolean | Flag that indicates whether the videos may be shared with others. Set this property to **true** if the videos may be shared; otherwise, **false**. The default value is **true**. |
| **subtitle** | string | Subtitle to display under the card's title. |
| **text** | string | Description or prompt to display under the card's title or subtitle. |
| **title** | string | Title of the card. |
| **value** | object | Supplementary parameter for this card|

<a href="#objects">Back to Schema table</a>
