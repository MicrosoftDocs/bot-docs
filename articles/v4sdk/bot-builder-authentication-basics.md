---
title: Bot Framework authentication basics - Bot Service
description: Learn about the authentication basics in the Bot Framework.
author: mmiele
ms.author: v-mimiel
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 07/07/2020
---

# Bot Framework authentication basics

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Often a bot must access protected resources, for example email account, on behalf of the user. In order to do that the bot must be **authorized** based on the user's credentials. Before that, the user must be **authenticated** first.
Moreover, the bot itself must be a known entity, that is it must be authenticated in the Azure Bot Service context. This happens prior to the bot being authorized to operate on behalf of the user.

Let's see if we can untangle this bundle by starting with a bird's eye view of the Bot Framework authentication context.

![bot authentication context.](./media/concept-bot-authentication\bot-auth-context.PNG)

- **Bot Channel Registration**. When you register a bot in Azure, for example via the **Bot Channels Registration**, Azure creates an Active Directory (AD) registration application. This application has an app id (`MicrosoftAppID`) and a client secret (`MicrosoftAppPassword`). You use these values in the bot configuration files as described below.
Notice that you can achieve similar results by creating a **Web App Bot**.

- **Azure AD Identity**. The Azure Active Directory (Azure AD) is a cloud identity service that allows you to build applications that securely sign in users using industry standard protocols like OAuth2.0. You create an AD application and use its **app Id** and **password** to select an **identity provider** and generate an **authentication** connection. You add this connection to the bot channel registration settings. You also add the connection name in the bot configuration files as described below.

- **Bot**. A bot is identified by its channels registration (or web app) **app Id** and **password**. You add the related values in the bot's configuration files (`appsettings.json` (.NET), `.env` (JavaScript), `config.py` (Python)) or in **Azure Key Vault**. You also add the connection name to the files.
The bot uses the **token** based on the app id and password to access protected resources. Also, the bot uses the **token** based on the authentication connection to access user's protected resources.

## Bot authentication and authorization

The following are the main steps to authenticate a bot and authorize it to access user's protected resources:

1. Create a bot channel registration application.
1. Add the registration app id and password to the bot configuration file. This allows the bot to be authenticated to access protected resources.
1. Create an Azure AD application to select an identity provider to authenticate the user.
1. Create an authentication connection and add it to the channel registration settings.
1. Add the connection name to the bot's configuration files. This allows the bot to be authorized to access user's protected resources.

For a complete example, see [Add authentication to a bot](bot-builder-authentication.md).

### Best practices

- Keep the AAD app registration restricted to its original purpose of service to service application.
- Create an additional AAD app for any user to service authentication, for more finite control over disabling authentication connections, rolling secrets, or reusing the AAD app with other applications.

Some of the problems you encounter if you also use the AAD registration app for authentication are:

- If the certificate attached to the AAD app registration needs to be renewed it would impact users that have authenticated with other AAD services using the certificate.
- In general, it creates a single point of failure and control for all authentication related activities with the bot.

## Related topics

The following articles provide in depth information and examples about the Bot Framework authentication. Start by looking at the [Authentication types](bot-builder-concept-authentication-types.md) and then [Identity providers](bot-builder-concept-identity-providers.md).

> [!div class="mx-tdBreakAll"]
> |Article|Description|
> |-------------|----------|
> |[Authentication types <img width=105px/>](bot-builder-concept-authentication-types.md)| Describes the two Bot Framework authentication types and the tokens they use.|
> |[Identity providers](bot-builder-concept-identity-providers.md)| Describes the use of identity providers. They allow you to build applications that securely sign in users using industry standard protocols like OAuth2.0.|
> |[User authentication](bot-builder-concept-authentication.md)| Describes user's authentication and the related token to authorize a bot to perform tasks on the user's behalf.|
> |[Single sign on](bot-builder-concept-sso.md)| Describes single user authentication for multiple protected resources access.|
> |[Bot channels registration](../bot-service-quickstart-registration.md)|Shows how to register a bot with the Azure Bot Service.|
> |[Bot Framework security guidelines](bot-builder-security-guidelines.md)| Describes security in general and as it applies to the Bot Framework.|
> |[Add authentication to a bot](bot-builder-authentication.md)| Shows how to create bot channel registration, authentication connection and preparing the code.|
> |[Add single sign on to a bot](bot-builder-authentication-sso.md)| Shows how to add single sign on authentication to a bot.|
