---
title: Single sign on with a Web Chat in Bot Framework SDK
description: Learn about the workflow when single sign on is used in a bot and with a Web Chat client. 
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.custom: abs-meta-21q1
ms.date: 09/01/2021
---

# Single sign with a Web Chat

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Single sign on (SSO) allows a client, such as a Web Chat control, to communicate with a bot on behalf of the user. Currently, only the [Azure AD v2](~/v4sdk/bot-builder-concept-identity-providers.md#azure-active-directory-identity-provider) identity provider is supported.

Typically, a Web Chat is embedded in a website page. When the user sign on the website, the Web Chat invokes a bot on behalf of the user. The website client's token, based on the user's credentials, is exchanged for a different one to access the bot. In this way, the user does not have to sign on twice; the first time on the website, and the second time on the bot, hence the term SSO.

The following diagram shows the SSO flow when using a Web Chat client.

![bot sso webchat](~/v4sdk/media/concept-bot-authentication/bot-auth-webchat-sso.PNG)

In the case of failure, SSO falls back to the existing behavior of showing the **OAuth** card. The failure may be caused for example if the user consent is required or if the token exchange fails.

Let's analyze the flow.

1. The user sign on the website.
1. An OAuth trigger activity is received by the Web Chat.
1. The Web Chat starts a conversation with the bot via an OAuth trigger activity.
1. The bot sends back an OAuth Card to the Web Chat.
1. The Web Chat intercepts the OAuth card before displaying it to the user and checks if it contains a `TokenExchangeResource` property.
1. If the property exists, the Web Chat must get an exchangeable token for the user, which must be an Azure AD v2 token.
1. The Web Chat sends an Invoke activity to the bot with the body shown below.
    ```json
    {
        "type": "Invoke",
        "name": "signin/tokenExchange",
        "value": {
            "id": "<any unique ID>",
            "connectionName": "<connection name on the bot (from the OAuth Card)>",
            "token": "<exchangeable token>"
        }
    }
    ```

1. The bot processes the `TokenExchangeInvokeRequest` by issuing a request to the Azure Bot Service to obtain an exchangeable token.

1. The Azure Bot Service sends the token to the bot.

1. The bot returns a `TokenExchangeInvokeResponse` back to the Web Chat. The Web Chat waits till it receives the `TokenExchangeInvokeResponse`.

    ```json
    {
        "status": "<response code>",
        "body": {
            "id":"<unique ID>",
            "connectionName": "<connection Name on the bot (from the OAuth Card)>",
            "failureDetail": "<failure reason if status code is not 200, null otherwise>"
        }
    }
    ```

1. If the `TokenExchangeInvokeResponse` has a `status` of `200`, then the Web Chat does not show the OAuth card. For any other `status` or if the `TokenExchangeInvokeResponse` is not received, then the Web Chat shows the OAuth card to the user. This ensures that the SSO flow falls back to normal OAuthCard flow, in case of any errors or unmet dependencies like user consent.

For an implementation example, please refer to this [SSO sample](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/07.advanced-web-chat-apps/e.sso-on-behalf-of-authentication).
