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

In the Bot Framework, two broad authentication categories exist: **bot authentication** and **user authentication**. Each has an associated **token** to allow access to secured resources. The following figure shows the elements involved in both bot and user authentication.

![bot framework authentication context](media/concept-bot-authentication/bot-framework-auth-context.png)

To help understanding the previous figure, notice the following:

 - **Host Platform**. It is the bot hosting platform. It can be Azure or any host platform chosen by the customer. In the picture the host platform is Azure.
 - **Bot Connector Service**. It converts messages received from channels into activity objects, and send them to the bot's messaging endpoint. Likewise, it converts activity objects received from the bot into messages and sent them to the channels. See also [Create a bot with the Bot Connector service](~/rest-api/bot-framework-rest-connector-quickstart.md).
- **Bot Adapter**. This is the default Bot Framework adapter. It performs these tasks:
    - Converts the JSON payload into an object. At this point, it is already an activity object, thanks to the Bot Connector Service.
    - Creates a turn context and adds the activity object to it.
    - Runs middleware, if any.
    - Forwards the turn context to the bot.

> [!NOTE]
> When a custom channel adapter is used, the adapter itself performs the tasks that the Bot Connector Service and the default Bot Adapter do. Also, it provides the authentication mechanism for the related web hook API. For an example,
see [Add Slack app settings to your bot's configuration file](~/bot-service-channel-connect-slack.md#add-slack-app-settings-to-your-bots-configuration-file).

## Bot authentication

A bot is identified by the **MicrosoftAppID** and **MicrosoftAppPassword** which are kept within the bot's settings files: `appsettings.json` (.NET), `.env` (JavaScript), `config.py` (Python), or the **Azure Key Vault**.
For more information, see [MicrosoftAppID and MicrosoftAppPassword](~/bot-service-manage-overview.md#microsoftappid-and-microsoftapppassword).

When you register a bot with Azure, for example via the **Bot Channels Registration**, Azure creates an Active Directory registration application if you perform the registration using the Azure portal. If you use the CLI, you must specifically create it. The registration application has an application ID (`MicrosoftAppID`) and client secret (`MicrosoftAppPassword`). These values are used to generate a **token** to allow the bot to access secure resources.

When a channel sends a request to a bot, via the Bot Connector service, it specifies a **token** in the **Authorization header** of the request. The bot authenticates calls from the Bot Connector service by verifying the authenticity of the token.

For more information, see [Authenticate requests from the Bot Connector service to your bot](~/rest-api/bot-framework-rest-connector-authentication.md#connector-to-bot).

When the bot sends a request to a channel via the **Bot Connector service**, it must specify the **token** in the **Authorization header** of the request.
All requests must include the access token which is verified by the Bot Connector service to authorize the request.

For more information, see [Authenticate requests from your bot to the Bot Connector service](~/rest-api/bot-framework-rest-connector-authentication.md#bot-to-connector).

The operations described are automatically performed by the Bot Framework SDK.

### Channels

Channels communicate with a bot via the **Bot Connector service** this means that the previous authentication principles generally apply. You may want to notice the specific characteristics of the channels described next.

#### Direct Line

Besides the standard supported channels, a client application can communicate with a bot using the Direct Line channel.

The client application authenticates requests to Direct Line (version 3.0) either by using a **secret** obtained from the [Direct Line channel configuration](~/bot-service-channel-connect-directline.md) page in the Azure portal or, better, by using a **token** that obtained at runtime. The secret or token are specified in the Authorization header of each request.

> [!NOTE]
> A Direct Line secret is a master key that can be used to **access any conversation** that belongs to the associated bot. A secret can also be used to obtain a token. **Secrets do not expire**.
> A Direct Line token is a key that can be used to **access a single conversation**. **A token expires but can be refreshed**.

For more information, see [Authentication](~/rest-api/bot-framework-rest-direct-line-3-0-authentication.md).

#### Web Chat

When embedding a Web Chat control in a web page, you can use a either a **secret** or, better, a **token** obtained at runtime.
For more information, see [Connect a bot to Web Chat](~/bot-service-channel-connect-webchat.md).

Notice that when you register a bot with Azure, the Web Chat channel is automatically configured to allow testing of the bot.

![bot web chat testing](media/concept-bot-authentication/bot-webchat-testing.PNG)


### Skills

TBD

### Emulator

The emulator has its own authentication flow, and its own tokens.  Emulator tokens, and the validation path for them, are a little different from channel tokens, and skill tokens

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

### Identity providers

An identity provider authenticates user or client identities and issues consumable security tokens. It provides user authentication as a service.

Client applications, such as web applications, delegate authentication to a trusted identity provider.

A trusted identity provider does the following:

- Enables single sign-on (SSO) features, allowing an application to access multiple secured resources.
- Facilitates connections between cloud computing resources and users, decreasing the need for users to re-authenticate.

Notice that user's authentication is performed by a channel using an identity provider specific to the channel. For more information, see [Identity providers](bot-builder-concept-identity-providers.md).

> [!NOTE]
> The token issued during **Bot authentication** is not the same token issued during **User authentication**. The first is used to establish secure communication between a bot, channels and, ultimately, client applications. The second is used to authorize the bot to access secured resource on behalf of the user.

