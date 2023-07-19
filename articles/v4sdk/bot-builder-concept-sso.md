---
title: Single sign-on in the Bot Framework SDK
description: Learn about single sign-on (SSO) to allow apps to share access to user resources.
keywords: Azure AI Bot Service, authentication, bot framework token service
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: conceptual
ms.date: 08/08/2022
monikerRange: 'azure-bot-service-4.0'
---

# About single sign-on

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Single sign-on (SSO) allows access to resources to be shared across independent applications.
For instance, a user could sign in to a service in a root bot, and the root bot could share the access token with a skill bot.
Currently, only the [Azure Active Directory v2](./bot-builder-concept-identity-providers.md#azure-active-directory-identity-provider) identity provider is supported.

SSO applies to the following scenarios:

- A root bot and one or more skill bots. The user signs in from the root bot. The bot then invokes multiple skills on behalf of the user.
- A Web Chat control embedded on a website. The user signs in from the website. The website then invokes a bot or a skill on behalf of the user.

SSO provides the following advantages:

- The user doesn't have to sign in multiple times.
- The root bot or website doesn't need to know the user's permissions.

> [!NOTE]
> SSO is a available in Bot Framework SDK version 4.8 and later.

## SSO components interaction

The following time sequence diagrams show the interactions between the SSO various components.

- The following diagram illustrates the flow for a root bot.

    :::image type="content" source="media/concept-bot-authentication/bot-auth-sso-va-time-sequence.PNG" alt-text="SSO sequence diagram for a root bot.":::

- The following diagram illustrates the flow, and fallback flow, for a Web Chat control.

    :::image type="content" source="media/concept-bot-authentication/bot-auth-sso-webchat-time-sequence.PNG" alt-text="SSO sequence diagram for a Web Chat control.":::

    If the token exchange fails, the fall-back is to prompt the user to sign in.
    Such failures can happen when extra permissions are required or the token is for the wrong service.

Let's analyze the flow.

1. The client starts a conversation with the bot triggering an OAuth scenario.
1. The bot sends back an OAuth Card to the client.
1. The client intercepts the OAuth card before displaying it to the user and checks if it contains a `TokenExchangeResource` property.
1. If the property exists, the client sends a `TokenExchangeInvokeRequest` to the bot. The client must have an exchangeable token for the user, which must be an Azure Active Directory v2 token and whose audience must be the same as `TokenExchangeResource.Uri` property. The client sends an Invoke activity to the bot with the body shown below.

    ```json
    {
        "type": "Invoke",
        "name": "signin/tokenExchange",
        "value": {
            "id": "<any unique ID>",
            "connectionName": "<connection Name on the skill bot (from the OAuth Card)>",
            "token": "<exchangeable token>"
        }
    }
    ```

1. The bot processes the `TokenExchangeInvokeRequest` and returns a `TokenExchangeInvokeResponse` back to the client. The
client should wait until it receives the `TokenExchangeInvokeResponse`.

    ```json
    {
        "status": "<response code>",
        "body": {
            "id":"<unique ID>",
            "connectionName": "<connection Name on the skill bot (from the OAuth Card)>",
            "failureDetail": "<failure reason if status code isn't 200, null otherwise>"
        }
    }
    ```

1. If the `TokenExchangeInvokeResponse` has a `status` of `200`, then the client doesn't show the OAuth card. See the _normal flow_ diagram. For any other `status` or if the `TokenExchangeInvokeResponse` isn't received, then the client shows the OAuth card to the user. See the _fallback flow_ diagram. This ensures that the SSO flow falls back to the normal OAuthCard flow, if there are any errors or unmet dependencies, like user consent.

## Next steps

> [!div class="nextstepaction"]
> [Add single sign-on to a bot](bot-builder-authentication-sso.md)
