---
title: Single sign on - Bot Service
description: Learn about single sign on (SSO) to allow a client to communicate with a bot or skill in the Azure Bot Service.
keywords: azure bot service, authentication, bot framework token service
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 03/19/2020
monikerRange: 'azure-bot-service-4.0'
---

# Single sign on

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Single Sign on (SSO) allows a client, such as virtual assistant, WebChat and so on, to communicate with a bot or skill on behalf of the user.
Currently, only the [Azure AD v2](./bot-builder-concept-identity-providers.md#azure-active-directory-identity-provider) identity provider is supported.

SSO applies to the following scenarios:

- Virtual assistant and one or more skill bots. The user can sign in once into the virtual assistant. The assistant then invokes multiple skills on behalf of the user. See also [Virtual assistant](./bot-builder-virtual-assistant-introduction.md).
- Webchat embedded into a website. The user sign in the website. The website then invokes a bot or a skill on behalf of the user.

SSO provides the following advantages:

- The user does not have to login over again, if already signed in a virtual assistant or website.
- The virtual assistant or website do not have knowledge of user permissions.

> [!NOTE]
> SSO is a new feature in Bot Framework SDK v4.8.

## SSO components interaction

The following time sequence diagrams show the interactions between the SSO various components.


- The following diagram shows a normal flow when using a virtual assistant client.

    ![bot sso va](media/concept-bot-authentication/bot-auth-sso-va-time-sequence.PNG)


- The following shows a normal and a fallback flow when using a WebChat client.

    ![bot sso webchat](media/concept-bot-authentication/bot-auth-sso-webchat-time-sequence.PNG)

    In the case of failure, SSO falls back to the existing behavior of showing the OAuth card.
    The failure may be caused for example if the user consent is required or if the token exchange fails.

Let's analyze the flow.

1. The client starts a conversation with the bot triggering an OAuth scenario.
1. The bot sends back an OAuth Card to the client.
1. The client intercepts the OAuth card before displaying it to the user and checks if it contains a `TokenExchangeResource` property.
1. If the property exisists, the client sends a `TokenExchangeInvokeRequest` to the bot. The client must have an exchangeable token for the user, which must be an Azure AD v2 token and whose audience must be the same as `TokenExchangeResource.Uri` property. <!-- For an example on how to get the user's exchangeable token, please refer to this [Webchat Sample (TBD)](https://linkrequired). --> The client sends an Invoke activity to the bot with the body shown below.

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
client should wait till it receives the `TokenExchangeInvokeResponse`.

    ```json
    {
        "status": "<response code>",
        "body": {
            "id":"<unique ID>",
            "connectionName": "<connection Name on the skill bot (from the OAuth Card)>",
            "failureDetail": "<failure reason if status code is not 200, null otherwise>"
        }
    }
    ```

1. If the `TokenExchangeInvokeResponse` has a `status` of `200`, then the client does not show the OAuth card. See the *normal flow* diagram. For any other `status` or if the `TokenExchangeInvokeResponse` is not received, then the client shows the OAuth card to the user. See the *fallback flow* diagram. This ensures that the SSO flow falls back to normal OAuthCard flow, in case of any errors or unmet dependencies like user consent.

## Next steps

> [!div class="nextstepaction"]
> [Add single sign on to a bot](bot-builder-authentication-sso.md)
