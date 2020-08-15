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

When you register a bot with Azure, for example via the bot channels registration, an Active Directory registration application is created. This application has its own application ID (app ID) and client secret (password) needed to configure the bot for deployment. The app ID is also needed to secure the service to service communication between the bot and the Bot Framework Channel Services.

## Client authentication

A client can authenticate requests to Direct Line API 3.0 either by using a secret that you obtain from the Direct Line channel configuration page in the Bot Framework Portal or by using a token that you obtain at runtime. The secret or token should be specified in the Authorization header of each request. For more information, see [Secrets and tokens](~/rest-api/bot-framework-rest-direct-line-3-0-authentication.md#secrets-and-tokens).

## User authentication

There are times when a bot must access secured online resources on behalf of the user, and to do that the bot must be authorized. This is because to perform certain operations such as checking email, checking on flight status, or placing an order, the bot will need to call an external service such as Microsoft Graph, GitHub, or a company's REST service. OAuth is used to authenticate the user and authorize the bot.

> [!NOTE]
> Two macro-steps are involved for a bot to access a user's resources.
>
> 1. **Authentication**. The process of verifying the user's identity.
> 1. **Authorization**. The process of verifying that the bot can access the user's resources.
>
> If the first step is successful then a token based on the user's credentials is issued. In the second step, the bot uses the token to access the user's resources.

For more information, see [User authentication](bot-builder-concept-authentication.md).