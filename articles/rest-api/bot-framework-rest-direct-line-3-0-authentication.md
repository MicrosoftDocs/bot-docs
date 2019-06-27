---
title: Authentication | Microsoft Docs
description: Learn how to authenticate API requests in Direct Line API v3.0. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 04/10/2019    
---

# Authentication

A client can authenticate requests to Direct Line API 3.0 either by using a **secret** that you [obtain from the Direct Line channel configuration page](../bot-service-channel-connect-directline.md) in the Bot Framework Portal or by using a **token** that you obtain at runtime. The secret or token should be specified in the `Authorization` header of each request, using this format: 

```http
Authorization: Bearer SECRET_OR_TOKEN
```

## Secrets and tokens

A Direct Line **secret** is a master key that can be used to access any conversation that belongs to the associated bot. A **secret** can also be used to obtain a **token**. Secrets do not expire. 

A Direct Line **token** is a key that can be used to access a single conversation. A token expires but can be refreshed. 

If you're creating a service-to-service application, specifying the **secret** in the `Authorization` header of Direct Line API requests may be simplest approach. If you're writing an application where the client runs in a web browser or mobile app, you may want to exchange your secret for a token (which only works for a single conversation and will expire unless refreshed) and specify the **token** in the `Authorization` header of Direct Line API requests. Choose the security model that works best for you.

> [!NOTE]
> Your Direct Line client credentials are different from your bot's credentials. This enables you to revise your keys independently and lets you share client tokens without disclosing your bot's password. 

## Get a Direct Line secret

You can [obtain a Direct Line secret](../bot-service-channel-connect-directline.md) via the Direct Line channel configuration page for your bot in the <a href="https://dev.botframework.com/" target="_blank">Bot Framework Portal</a>:

![Direct Line configuration](../media/direct-line-configure.png)

## <a id="generate-token"></a> Generate a Direct Line token

To generate a Direct Line token that can be used to access a single conversation, first obtain the Direct Line secret from the Direct Line channel configuration page in the <a href="https://dev.botframework.com/" target="_blank">Bot Framework Portal</a>. Then issue this request to exchange your Direct Line secret for a Direct Line token:

```http
POST https://directline.botframework.com/v3/directline/tokens/generate
Authorization: Bearer SECRET
```

In the `Authorization` header of this request, replace **SECRET** with the value of your Direct Line secret.

The following snippets provide an example of the Generate Token request and response.

### Request

```http
POST https://directline.botframework.com/v3/directline/tokens/generate
Authorization: Bearer RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0
```

The request payload, which contains the token parameters, is optional but recommended. When generating a token that can be sent back to the Direct Line service, provide the following payload to make the connection more secure. By including these values, Direct Line can perform additional security validation of the user ID and name, inhibiting tampering of these values by malicious clients. Including these values also improves Direct Line's ability to send the _conversation update_ activity, allowing it to generate the conversation update immediately upon the user joining the conversation. When this information is not provided, the user must send content before Direct Line can send the conversation update.

```json
{
  "user": {
    "id": "string",
    "name": "string"
  },
  "trustedOrigins": [
    "string"
  ]
}
```

| Parameter | Type | Description |
| :--- | :--- | :--- |
| `user.id` | string | Optional. Channel-specific ID of the user to encode within the token. For a Direct Line user, this must begin with `dl_`. You can create a unique user ID for each conversation, and for better security, you should make this ID unguessable. |
| `user.name` | string | Optional. The display-friendly name of the user to encode within the token. |
| `trustedOrigins` | string array | Optional. A list of trusted domains to embed within the token. These are the domains that can host the bot's Web Chat client. This should match the list in the Direct Line configuration page for your bot. |

### Response

If the request is successful, the response contains a `token` that is valid for one conversation and an `expires_in` value that indicates the number of seconds until the token expires. For the token to remain useful, you must [refresh the token](#refresh-token) before it expires.

```http
HTTP/1.1 200 OK
[other headers]
```

```json
{
  "conversationId": "abc123",
  "token": "RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0y8qbOF5xPGfiCpg4Fv0y8qqbOF5x8qbOF5xn",
  "expires_in": 1800
}
```

### Generate Token versus Start Conversation

The Generate Token operation (`POST /v3/directline/tokens/generate`) is similar to the [Start Conversation](bot-framework-rest-direct-line-3-0-start-conversation.md) operation (`POST /v3/directline/conversations`) in that both operations return a `token` that can be used to access a single conversation. However, unlike the Start Conversation operation, the Generate Token operation does not start the conversation, does not contact the bot, and does not create a streaming WebSocket URL. 

If you plan to distribute the token to clients and want them to initiate the conversation, use the Generate Token operation. If you intend to start the conversation immediately, use the [Start Conversation](bot-framework-rest-direct-line-3-0-start-conversation.md) operation instead.

## <a id="refresh-token"></a> Refresh a Direct Line token

A Direct Line token can be refreshed an unlimited amount of times, as long as it has not expired. An expired token cannot be refreshed. To refresh a Direct Line token, issue this request: 

```http
POST https://directline.botframework.com/v3/directline/tokens/refresh
Authorization: Bearer TOKEN_TO_BE_REFRESHED
```

In the `Authorization` header of this request, replace **TOKEN_TO_BE_REFRESHED** with the Direct Line token that you want to refresh.

The following snippets provide an example of the Refresh Token request and response.

### Request

```http
POST https://directline.botframework.com/v3/directline/tokens/refresh
Authorization: Bearer CurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0y8qbOF5xPGfiCpg4Fv0y8qqbOF5x8qbOF5xn
```

### Response

If the request is successful, the response contains a new `token` that is valid for the same conversation as the previous token and an `expires_in` value that indicates the number of seconds until the new token expires. For the new token to remain useful, you must [refresh the token](#refresh-token) before it expires.

```http
HTTP/1.1 200 OK
[other headers]
```

```json
{
  "conversationId": "abc123",
  "token": "RCurR_XV9ZA.cwA.BKA.y8qbOF5xPGfiCpg4Fv0y8qqbOF5x8qbOF5xniaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0",
  "expires_in": 1800
}
```

## Additional resources

- [Key concepts](bot-framework-rest-direct-line-3-0-concepts.md)
- [Connect a bot to Direct Line](../bot-service-channel-connect-directline.md)