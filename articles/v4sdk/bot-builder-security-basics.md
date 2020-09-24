---
title: Bot Framework security basics - Bot Service
description: Learn about the security basics in the Bot Framework.
author: kamrani
ms.author: v-mimiel
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 07/07/2020
---

# Bot Framework security basics

Often a bot must access protected resources, for example a bank account, on behalf of the user. In order to do that it must be authorized based on the user's credentials. Before that, the user must be authenticated first.
Moreover, the bot itself must be a known entity, that is it must be authenticated in the Azure Bot Service context. This comes prior it is authorized to operate on behalf of the user. Let's see if we can untangle the bundle.

The following picture is a bird's eye view of the bot authentication context.

![bot authentication context.](./media/concept-bot-authentication\bot-auth-context.PNG)

- **Bot Channel Registration**. When you register a bot in the Azure portal, for example via the **Bot Channels Registration**, Azure creates an Active Directory (AD) registration application. This registration has an application ID (`MicrosoftAppID`) and client secret (`MicrosoftAppPassword`).

- **Azure AD Identity**. The Azure Active Directory (Azure AD) is a cloud identity service that allows you to build applications that securely sign in users using industry standard protocols like OAuth2.0. You create an AD application and use its **app id** and **password** to select an identity provider and generate an authentication connection. You add this connection to the bot channel registration settings.

- **Bot**. A bot is identified by its channels registration **MicrosoftAppID** and **MicrosoftAppPassword**, which are kept within the bot's configuration files (`appsettings.json` (.NET), `.env` (JavaScript), `config.py` (Python)) or in **Azure Key Vault**. Azure uses these values to generate a **token** that the bot uses to access secure resources.  Also, the bot is **authorized** to access user's protected resources using the token issued based on the authentication connection. You add the connection name to the bot's configuration files mentioned before.

## Bot authentication and authorization in a nutshell

To summarize, to authenticate a bot and authorize it to access user's protected resources, you perform these steps:

1. Create a bot channel registration application.
1. Add the registration app id and password to the bot configuration file. This allows the bot to be authenticated to access protected resources.
1. Create an Azure AD application to select an identity provider to authenticate the user.
1. Create an authentication connection and add it to the channel registration settings. Also add its name to the bot's configuration files. This allow the bot to be authorized to access user's protected resources.

## Related topics

The following articles provide in depth information and examples of the Bot Framework security.

> [!div class="mx-tdBreakAll"]
> |Article|Description|
> |-------------|----------|
> |[Bot Framework security guidelines](bot-builder-security-guidelines.md)| Security in general and as it applies to the Bot Framework.|
> |[Authentication types](bot-builder-concept-authentication-types.md)| Bot Framework authentication types.|
> |[User authentication](bot-builder-concept-authentication.md)| User authentication and bot authorization to perform tasks on user's behalf.|
> |[Identity providers](bot-builder-concept-identity-providers.md)| Authentication as a service.|
> |[Single sign on](bot-builder-concept-sso.md)| Single user authentication for multiple access.|
> |[Add authentication to a bot](bot-builder-authentication.md)| How to add authentication to a bot.|
> |[Add single sign on to a bot](bot-builder-authentication-sso.md)| How to add single sign on to a bot.|
