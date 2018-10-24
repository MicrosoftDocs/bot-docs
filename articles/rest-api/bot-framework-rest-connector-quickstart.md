---
title: Create a bot with the Bot Connector service | Microsoft Docs
description: Create a bot with the Bot Connector service. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# Create a bot with the Bot Connector service
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-quickstart.md)
> - [Node.js](../nodejs/bot-builder-nodejs-quickstart.md)
> - [Bot Service](../bot-service-quickstart.md)
> - [REST](../rest-api/bot-framework-rest-connector-quickstart.md)

The Bot Connector service enables your bot to exchange messages with channels that are configured in the <a href="https://dev.botframework.com/" target="_blank">Bot Framework Portal</a>, by using industry-standard REST and JSON over HTTPS. This tutorial walks you through the process of obtaining an access token from the Bot Framework and using the Bot Connector service to exchange messages with the user.

## <a id="get-token"></a> Get an access token

> [!IMPORTANT]
> If you have not already done so, you must [register your bot](../bot-service-quickstart-registration.md) with the Bot Framework to obtain its App ID and password. You will need the bot's AppID and password to get an access token.

To communicate with the Bot Connector service, you must specify an access token in the `Authorization` header of each API request, using this format: 

```http
Authorization: Bearer ACCESS_TOKEN
```

You can obtain the access token for your bot by issuing an API request.

### Request

To request an access token that can be used to authenticate requests to the Bot Connector service, issue the following request, replacing **MICROSOFT-APP-ID** and **MICROSOFT-APP-PASSWORD** with the App ID and password that you obtained when you [registered](../bot-service-quickstart-registration.md) your bot with the Bot Framework.

```http
POST https://login.microsoftonline.com/botframework.com/oauth2/v2.0/token
Host: login.microsoftonline.com
Content-Type: application/x-www-form-urlencoded

grant_type=client_credentials&client_id=MICROSOFT-APP-ID&client_secret=MICROSOFT-APP-PASSWORD&scope=https%3A%2F%2Fapi.botframework.com%2F.default
```

### Response

If the request succeeds, you will receive an HTTP 200 response that specifies the access token and information about its expiration. 

```json
{
    "token_type":"Bearer",
    "expires_in":3600,
    "ext_expires_in":3600,
    "access_token":"eyJhbGciOiJIUzI1Ni..."
}
```

> [!TIP]
> For more details about authentication in the Bot Connector service, see [Authentication](bot-framework-rest-connector-authentication.md).

## Exchange messages with the user

A conversation is a series of messages exchanged between a user and your bot. 

### Receive a message from the user

When the user sends a message, the Bot Framework Connector POSTs a request to the endpoint that you specified when you [registered](../bot-service-quickstart-registration.md) your bot. The body of the request is an [Activity][Activity] object. The following example shows the request body that a bot receives when the user sends a simple message to the bot. 

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

### Reply to the user's message

When your bot's endpoint receives a `POST` request that represents a message from the user (i.e., `type` = **message**), use the information in that request to create the [Activity][Activity] object for your response.

1. Set the **conversation** property to the contents of the **conversation** property in the user's message.
2. Set the **from** property to the contents of the **recipient** property in the user's message.
3. Set the **recipient** property to the contents of the **from** property in the user's message.
4. Set the **text** and **attachments** properties as appropriate.

Use the `serviceUrl` property in the incoming request to [identify the base URI](bot-framework-rest-connector-api-reference.md#base-uri) that your bot should use to issue its response. 

To send the response, `POST` your [Activity][Activity] object to `/v3/conversations/{conversationId}/activities/{activityId}`, as shown in the following example. The body of this request is an [Activity][Activity] object that prompts the user to select an available appointment time.

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
    "text": "I have these times available:",
    "replyToId": "bf3cc9a2f5de..."
}
```

In this example request, `https://smba.trafficmanager.net/apis` represents the base URI; the base URI for requests that your bot issues may be different. For details about setting the base URI, see [API Reference](bot-framework-rest-connector-api-reference.md#base-uri). 

> [!IMPORTANT]
> As shown in this example, the `Authorization` header of each API request that you send must contain the word **Bearer** followed by the access token that you [obtained from the Bot Framework](#get-token).

To send another message that enables a user to select an available appointment time by clicking a button, `POST` another request to the same endpoint:

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
    "attachmentLayout": "list",
    "attachments": [
      {
        "contentType": "application/vnd.microsoft.card.thumbnail",
        "content": {
          "buttons": [
            {
              "type": "imBack",
              "title": "10:30",
              "value": "10:30"
            },
            {
              "type": "imBack",
              "title": "11:30",
              "value": "11:30"
            },
            {
              "type": "openUrl",
              "title": "See more",
              "value": "http://www.contososalon.com/scheduling"
            }
          ]
        }
      }
    ],
    "replyToId": "bf3cc9a2f5de..."
}
```   

## Next steps

In this tutorial, you obtained an access token from the Bot Framework and used the Bot Connector service to exchange messages with the user. 
You can use the [Bot Framework Emulator](../bot-service-debug-emulator.md) to test and debug your bot. 
If you'd like to share your bot with others, you'll need to [configure](../bot-service-manage-channels.md) it to run on one or more channels.


[Activity]: bot-framework-rest-connector-api-reference.md#activity-object