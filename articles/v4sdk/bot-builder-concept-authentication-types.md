---
title: Bot framework authentication types in the Azure Bot Service - Bot Service
description: Learn about bot authentication types in the Azure Bot Service.
keywords: azure bot service, authentication, bot framework token service
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 08/14/2020
monikerRange: 'azure-bot-service-4.0'
---

# Authentication types

The Bot Framework provides several authentication types as described below.

## Bot authentication

A bot is identified by the `MicrosoftAppID` and `MicrosoftAppPassword` which are kept within the bot's settings files: `appsettings.json` (.NET), `.env` (JavaScript), `web.config` (Python), or the [Azure Key Vault](https://docs.microsoft.com/azure/key-vault/general/overview). For more information, see [MicrosoftAppID and MicrosoftAppPassword](~/bot-service-manage-overview.md#microsoftappid-and-microsoftapppassword).

When you register a bot with Azure, for example via the bot channels registration, an Active Directory registration application is created. This application has its own application ID (`MicrosoftAppID`) and client secret (`MicrosoftAppPassword`) needed to configure the bot for deployment. The application ID is also needed to secure the service to service communication between the bot and the [Bot Framework channels](~/bot-service-manage-channels.md).

Notice that a bot communicates with Azure **bot connector service** using HTTP over a secured channel (SSL/TLS). When the bot sends a request to the connector service, it must include information the connector service uses to verify its identity. Likewise, when the connector service sends a request to a bot, it must include information that the bot can use to verify its identity. For more information, see [Authentication](../rest-api/bot-framework-rest-connector-authentication.md).

<!-- Should the topic be named "Bot authentication"? -->

## Client authentication

This kind of authentication applies to client applications using Direct Line or any of other supported channels to communicate with a bot.

- A client application can authenticate requests to Direct Line API 3.0 either by using a **secret** that you obtain from the Direct Line channel configuration page in the Azure portal or by using a **token** that you obtain at runtime. The secret or token should be specified in the Authorization header of each request. For more information, see [Secrets and tokens](~/rest-api/bot-framework-rest-direct-line-3-0-authentication.md#secrets-and-tokens).
- Single Sign on (SSO) allows a client, such as virtual assistant, WebChat and so on, to communicate with a bot or skill on behalf of the user.
Currently, only the [Azure AD v2](~v4sdk/bot-builder-concept-identity-providers.md#azure-active-directory-identity-provider) identity provider is supported. For more information, see [Single sign on](~v4sdk/bot-builder-concept-sso.md).
- An identity provider authenticates user or client identities and issues consumable security tokens. It provides authentication as a service. Client applications, such as web applications, delegate authentication to a trusted identity provider. For more information, see [Identity providers](~/v4sdk/bot-builder-concept-identity-providers.md).

> [!NOTE]
> Both **Bot authentication** and **Client authentication** belong to the same logical authentication step.

## User authentication

At times a bot must access secured online resources on behalf of the user. To do that the bot must be authorized. This is because to perform certain operations such as checking email, checking on flight status, or placing an order, the bot will need to call an external service such as Microsoft Graph, GitHub, or a company's REST service. OAuth is used to authenticate the user and authorize the bot.

> [!NOTE]
> Two macro-steps are involved for a bot to access a user's resources.
>
> 1. **Authentication**. The process of verifying the user's identity.
> 1. **Authorization**. The process of verifying that the bot can access the user's resources.
>
> If the first step is successful then a token based on the user's credentials is issued. In the second step, the bot uses the token to access the user's resources.

For more information, see [User authentication](bot-builder-concept-authentication.md).