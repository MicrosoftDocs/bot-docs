---
title: Authentication | Microsoft Docs
description: Learn how to authenticate API requests in Direct Line API v1.1. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# Authentication

> [!IMPORTANT]
> This article describes authentication in Direct Line API 1.1. If you are creating a new connection between your client application and bot, use [Direct Line API 3.0](bot-framework-rest-direct-line-3-0-authentication.md) instead.

A client can authenticate requests to Direct Line API 1.1 either by using a **secret** that you [obtain from the Direct Line channel configuration page](../bot-service-channel-connect-directline.md) in the Bot Framework Portal or by using a **token** that you obtain at runtime.

The secret or token should be specified in the `Authorization` header of each request, using either the "Bearer" scheme or the "BotConnector" scheme. 

**Bearer scheme**:
```http
Authorization: Bearer SECRET_OR_TOKEN
```

**BotConnector scheme**:
```http
Authorization: BotConnector SECRET_OR_TOKEN
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
POST https://directline.botframework.com/api/tokens/conversation
Authorization: Bearer SECRET
```

In the `Authorization` header of this request, replace **SECRET** with the value of your Direct Line secret.

The following snippets provide an example of the Generate Token request and response.

### Request

```http
POST https://directline.botframework.com/api/tokens/conversation
Authorization: Bearer RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0
```

### Response

If the request is successful, the response contains a token that is valid for one conversation. The token will expire in 30 minutes. For the token to remain useful, you must [refresh the token](#refresh-token) before it expires.

```http
HTTP/1.1 200 OK
[other headers]

"RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0y8qbOF5xPGfiCpg4Fv0y8qqbOF5x8qbOF5xn"
```

### Generate Token versus Start Conversation

The Generate Token operation (`POST /api/tokens/conversation`) is similar to the [Start Conversation](bot-framework-rest-direct-line-1-1-start-conversation.md) operation (`POST /api/conversations`) in that both operations return a `token` that can be used to access a single conversation. However, unlike the Start Conversation operation, the Generate Token operation does not start the conversation or contact the bot. 

If you plan to distribute the token to clients and want them to initiate the conversation, use the Generate Token operation. If you intend to start the conversation immediately, use the [Start Conversation](bot-framework-rest-direct-line-1-1-start-conversation.md) operation instead.

## <a id="refresh-token"></a> Refresh a Direct Line token

A Direct Line token is valid for 30 minutes from the time it is generated and can be refreshed an unlimited amount of times, as long as it has not expired. An expired token cannot be refreshed. To refresh a Direct Line token, issue this request:

```http
POST https://directline.botframework.com/api/tokens/{conversationId}/renew
Authorization: Bearer TOKEN_TO_BE_REFRESHED
```

In the URI of this request, replace **{conversationId}** with the ID of the conversation for which the token is valid and in the `Authorization` header of this request, replace **TOKEN_TO_BE_REFRESHED** with the Direct Line token that you want to refresh.

The following snippets provide an example of the Refresh Token request and response.

### Request

```http
POST https://directline.botframework.com/api/tokens/abc123/renew
Authorization: Bearer CurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0y8qbOF5xPGfiCpg4Fv0y8qqbOF5x8qbOF5xn
```

### Response

If the request is successful, the response contains a new token that is valid for the same conversation as the previous token. The new token will expire in 30 minutes. For the new token to remain useful, you must [refresh the token](#refresh-token) before it expires.

```http
HTTP/1.1 200 OK
[other headers]

"RCurR_XV9ZA.cwA.BKA.y8qbOF5xPGfiCpg4Fv0y8qqbOF5x8qbOF5xniaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0"
```

## Additional resources

- [Key concepts](bot-framework-rest-direct-line-1-1-concepts.md)
- [Connect a bot to Direct Line](../bot-service-channel-connect-directline.md)