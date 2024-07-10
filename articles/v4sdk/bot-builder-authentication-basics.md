---
title: Bot Framework SDK authentication basics
description: Learn about the authentication basics in the Bot Framework SDK. Learn how a bot can access protected resources on behalf of a user.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: conceptual
ms.date: 06/06/2022
ms.custom:
  - evergreen
---

# Bot Framework authentication basics

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Often a bot must access protected resources, for example email account, on behalf of the user. In order to do that the bot must be **authorized** based on the user's credentials. Before that, the user must be **authenticated** first.
The bot must be a known entity, that is, it must be authenticated within the Azure AI Bot Service context. This happens prior to the bot being authorized to operate on behalf of the user.

Let's see if we can untangle this bundle by starting with a bird's eye view of the Bot Framework authentication context.

:::image type="content" source="media/concept-bot-authentication/bot-auth-context2.png" alt-text="Bot authentication context":::

- When you register a bot in Azure via a **Azure Bot** resource, Azure creates an Microsoft Entra ID registration application. This application has an app ID (`MicrosoftAppId`) and a client secret (`MicrosoftAppPassword`). You use these values in the bot configuration files as described below.

- Microsoft Entra ID is a cloud identity service that allows you to build applications that securely sign in _users_ using industry standard protocols like OAuth 2.0. You create an Active Directory application and use its _app ID_ and _password_ to select an _identity provider_ and generate an _authentication_ connection. You add this connection to your bot resource. You also add the connection name in the bot configuration files as described below.

- A bot is identified by its Azure Bot resource _app ID_ and _password_. You add the related values in the bot's configuration file or to a secrets or key manager. You also add the connection name. The bot uses a token based on the app ID and password to access protected resources. The bot uses different tokens, based on the authentication connection, to access the user's protected resources.

## Bot authentication and authorization

The following are the main steps to authenticate a bot and authorize it to access user's protected resources:

1. Create a bot channel registration application.
1. Add the registration app ID and password to the bot configuration file. This allows the bot to be authenticated to access protected resources.
1. Create an Microsoft Entra ID application to select an identity provider to authenticate the user.
1. Create an authentication connection and add it to the channel registration settings.
1. Add the connection name to the bot's configuration files. This allows the bot to be authorized to access user's protected resources.

For a complete example, see [Add authentication to a bot](bot-builder-authentication.md).

### Best practices

- Keep the Microsoft Entra ID app registration restricted to its original purpose of service to service application.
- Create an additional Microsoft Entra ID app for any user to service authentication, for more finite control over disabling authentication connections, rolling secrets, or reusing the Microsoft Entra ID app with other applications.

Some of the problems you encounter if you also use the Microsoft Entra ID registration app for authentication are:

- If the certificate attached to the Microsoft Entra ID app registration needs to be renewed, it would impact users that have authenticated with other Microsoft Entra ID services using the certificate.
- In general, it creates a single point of failure and control for all authentication-related activities with the bot.

## Related topics

The following articles provide in-depth information and examples about authentication within the Bot Framework. Start by looking at the [Authentication types](bot-builder-concept-authentication-types.md) and then [Identity providers](bot-builder-concept-identity-providers.md).

| Article | Description |
|--|--|
| [Authentication types](bot-builder-concept-authentication-types.md) | Describes the two Bot Framework authentication types and the tokens they use. |
| [Identity providers](bot-builder-concept-identity-providers.md) | Describes the use of identity providers. They allow you to build applications that securely sign in users using industry standard protocols like OAuth2.0. |
| [User authentication](bot-builder-concept-authentication.md) | Describes user's authentication and the related token to authorize a bot to perform tasks on the user's behalf. |
| [Single sign on](bot-builder-concept-sso.md) | Describes single user authentication for multiple protected resources access. |
| [Register a bot with Azure](../bot-service-quickstart-registration.md) | Shows how to register a bot with the Azure AI Bot Service. |
| [Bot Framework security guidelines](bot-builder-security-guidelines.md) | Describes security in general and as it applies to the Bot Framework. |
| [Add authentication to a bot](bot-builder-authentication.md) | Shows how to create bot channel registration, create an authentication connection, and prepare the code. |
| [Add single sign on to a bot](bot-builder-authentication-sso.md) | Shows how to add single sign-on authentication to a bot. |
