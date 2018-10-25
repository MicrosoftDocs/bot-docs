---
title: Manage state data | Microsoft Docs
description: Learn how to store and retrieve state data using the Bot State service. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# Manage state data

The Bot State service enables your bot to store and retrieve state data that is associated with a user, a conversation, or a specific user within the context of a specific conversation. You may store up to 32 kilobytes of data for each user on a channel, each conversation on a channel, and each user within the context of a conversation on a channel. State data can be used for many purposes, such as determining where a prior conversation left off or simply greeting a returning user by name. If you store a user's preferences, you can use that information to customize the conversation the next time you chat. For example, you might alert the user to a news article about a topic that interests her, or alert a user when an appointment becomes available. 

> [!IMPORTANT]
> The Bot Framework State Service API is not recommended for production environments, and may be deprecated in a future release. It is recommended that you update your bot code to use the in-memory storage for testing purposes or use one of the **Azure Extensions** for production bots. For more information, see the **Manage state data** topic for [.NET](~/dotnet/bot-builder-dotnet-state.md) or [Node](~/nodejs/bot-builder-nodejs-state.md) implementation.

## <a id="concurrency"></a> Data concurrency

To control concurrency of data that is stored using the Bot State service, the framework uses the entity tag (ETag) for `POST` requests. The framework does not use the standard headers for ETags. Instead, the body of the request and response specifies the ETag using the `eTag` property. 

For example, if you issue a `GET` request to retrieve user data from the store, the response will contain the `eTag` property. If you change the data and issue a `POST` request to save the updated data to the store, your request may include the `eTag` property that specifies the same value as you received earlier in the `GET` response. If the ETag specified in your `POST` request matches the current value in the store, the server will save the user's data and respond with **HTTP 200 Success** and specify a new `eTag` value in the body of the response. If the ETag specified in your `POST` request does not match the current value in the store, the server will respond with **HTTP 412 Precondition Failed** to indicate that the user's data in the store has changed since you last saved or retrieved it.

> [!TIP]
> A `GET` response will always include an `eTag` property, but you do not need to specify the `eTag` property 
> in `GET` requests. 
> An `eTag` property value of asterisk (`*`) indicates that you have not previously saved data for the specified 
> combination of channel, user, and conversation.
>
> Specifying the `eTag` property in `POST` requests is optional. 
> If concurrency is not an issue for your bot, do not include the `eTag` property in `POST` requests. 

## <a id="save-user-data"></a> Save user data

To save state data for a user on a specific channel, issue this request:

```http
POST /v3/botstate/{channelId}/users/{userId}
```

In this request URI, replace **{channelId}** with the channel’s ID and replace **{userId}** with the user's ID on that channel. The `channelId` and `from` properties within any message that your bot has previously received from the user will contain these IDs. You may also choose to cache these values in a secure location so that you can access the user's data in the future without having to extract them from a message.

Set the body of the request to a [BotData][BotData] object, where the `data` property specifies the data that you want to save. If you use entity tags for [concurrency control](#concurrency), set the `eTag` property to the ETag that you received in the response the last time you saved or retrieved the user's data (whichever is the most recent). If you do not use entity tags for concurrency, then do not include the `eTag` property in the request.

> [!NOTE]
> This `POST` request will overwrite user data in the store only if the specified ETag matches the server's ETag, or if no ETag is specified in the request.

### Request 

The following example shows a request that saves data for a user on a specific channel. In this example request, `https://smba.trafficmanager.net/apis` represents the base URI; the base URI for requests that your bot issues may be different. For details about setting the base URI, see [API Reference](bot-framework-rest-connector-api-reference.md#base-uri).

```http
POST https://smba.trafficmanager.net/apis/v3/botstate/abcd1234/users/12345678
Authorization: Bearer ACCESS_TOKEN
Content-Type: application/json
```

```json
{
    "data": [
        {
            "trail": "Lake Serene",
            "miles": 8.2,
            "difficulty": "Difficult",
        },
        {
            "trail": "Rainbow Falls",
            "miles": 6.3,
            "difficulty": "Moderate",
        }
    ],
    "eTag": "a1b2c3d4"
}
```

### Response 

The response will contain a [BotData][BotData] object with a new `eTag` value.

## Get user data

To get state data that has previously been saved for the user on a specific channel, issue this request:

```http
GET /v3/botstate/{channelId}/users/{userId}
```

In this request URI, replace **{channelId}** with the channel’s ID and replace **{userId}** with the user's ID on that channel. The `channelId` and `from` properties within any message that your bot has previously received from the user will contain these IDs. You may also choose to cache these values in a secure location so that you can access the user's data in the future without having to extract them from a message.

### Request

The following example shows a request that gets data that has previously been saved for the user. In this example request, `https://smba.trafficmanager.net/apis` represents the base URI; the base URI for requests that your bot issues may be different. For details about setting the base URI, see [API Reference](bot-framework-rest-connector-api-reference.md#base-uri).

```http
GET https://smba.trafficmanager.net/apis/v3/botstate/abcd1234/users/12345678
Authorization: Bearer ACCESS_TOKEN
Content-Type: application/json
```

### Response

The response will contain a [BotData][BotData] object, where the `data` property contains the data that you have previously saved for the user and the `eTag` property contains the ETag that you may use within a subsequent request to save the user's data. If you have not previously saved data for the user, the `data` property will be `null` and the `eTag` property will contain an asterisk (`*`).

This example shows the response to the `GET` request:

```json
{
    "data": [
        {
            "trail": "Lake Serene",
            "miles": 8.2,
            "difficulty": "Difficult",
        },
        {
            "trail": "Rainbow Falls",
            "miles": 6.3,
            "difficulty": "Moderate",
        }
    ],
    "eTag": "xyz12345"
}
```

## Save conversation data

To save state data for a conversation on a specific channel, issue this request:

```http
POST /v3/botstate/{channelId}/conversations/{conversationId}
```

In this request URI, replace **{channelId}** with the channel’s ID, and replace **{conversationId}** with ID of the conversation. The `channelId` and `conversation` properties within any message that your bot has previously sent to or received in the context of the conversation will contain these IDs. You may also choose to cache these values in a secure location so that you can access the converation's data in the future without having to extract them from a message.

Set the request body to a [BotData][BotData] object, as described in [Save user data](#save-user-data).

> [!IMPORTANT]
> Because the [Delete user data](#delete-user-data) operation does not delete data that has been stored using the 
> **Save conversation data** operation, you must NOT use this operation to store a user's 
> personally identifiable information (PII).

### Response

The response will contain a [BotData][BotData] object with a new `eTag` value.

## Get conversation data

To get state data that has previously been saved for a conversation on a specific channel, issue this request:

```http
GET /v3/botstate/{channelId}/conversations/{conversationId}
```

In this request URI, replace **{channelId}** with the channel’s ID and replace **{conversationId}** with ID of the conversation. The `channelId` and `conversation` properties within any message that your bot has previously sent or received in the context of the conversation will contain these IDs. You may also choose to cache these values in a secure location so that you can access the converation's data in the future without having to extract them from a message.

### Response

The response will contain a [BotData][BotData] object with a new `eTag` value.

## Save private conversation data

To save state data for a user within the context of a specific conversation, issue this request:

```http
POST /v3/botstate/{channelId}/conversations/{conversationId}/users/{userId}
```

In this request URI, replace **{channelId}** with the channel’s ID, replace **{conversationId}** with ID of the conversation, and replace **{userId}** with the user's ID on that channel. The `channelId`, `conversation`, and `from` properties within any message that your bot has previously received from the user in the context of the conversation will contain these IDs. You may also choose to cache these values in a secure location so that you can access the converation's data in the future without having to extract them from a message.

Set the request body to a [BotData][BotData] object, as described in [Save user data](#save-user-data).

### Response

The response will contain a [BotData][BotData] object with a new `eTag` value.

## Get private conversation data

To get state data that has previously been saved for a user within the context of a specific conversation, issue this request: 

```http
GET /v3/botstate/{channelId}/conversations/{conversationId}/users/{userId}
```

In this request URI, replace **{channelId}** with the channel’s ID, replace **{conversationId}** with ID of the conversation, and replace **{userId}** with the user's ID on that channel. The `channelId`, `conversation`, and `from` properties within any message that your bot has previously received from the user in the context of the conversation will contain these IDs. You may also choose to cache these values in a secure location so that you can access the converation's data in the future without having to extract them from a message.

### Response

The response will contain a [BotData][BotData] object with a new `eTag` value.

## Delete user data

To delete state data for a user on a specific channel, issue this request:

```http
DELETE /v3/botstate/{channelId}/users/{userId}
```
In this request URI, replace **{channelId}** with the channel’s ID and replace **{userId}** with the user's ID on that channel. The `channelId` and `from` properties within any message that your bot has previously received from the user will contain these IDs. You may also choose to cache these values in a secure location so that you can access the user's data in the future without having to extract them from a message.

> [!IMPORTANT]
> This operation deletes data that has previously been stored for the user by using either the [Save user data](#save-user-data) operation or the [Save private conversation data](#save-private-conversation-data) operation. 
> It does NOT delete data that has previously been stored by using the [Save conversation data](#save-conversation-data) operation. 
> Therefore, you must NOT use the **Save conversation data** operation to store a user's personally identifiable information (PII).

Your bot should execute the **Delete user data** operation when it receives an [Activity][Activity] of type [deleteUserData](bot-framework-rest-connector-activities.md#deleteuserdata) or an activity of type [contactRelationUpdate](bot-framework-rest-connector-activities.md#contactrelationupdate) that indicates the bot has been removed from the user's contact list.

## Additional resources

- [Key concepts](bot-framework-rest-connector-concepts.md)
- [Activities overview](bot-framework-rest-connector-activities.md)

[BotData]: bot-framework-rest-connector-api-reference.md#botdata-object
[Activity]: bot-framework-rest-connector-api-reference.md#activity-object
