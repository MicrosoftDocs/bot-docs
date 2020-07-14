---
title: Add single sign on to Web Chat - Bot Service
description: Learn about adding single sign on to Web Chat in the Bot Framework.
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 07/10/2020
---

# Add single sign on to Web Chat

Single sign on (SSO) allows a client, such as a Web Chat control, to communicate with a bot or skill on behalf of the user. Currently, only the [Azure AD v2](~/v4sdk/bot-builder-concept-identity-providers.md#azure-active-directory-identity-provider) identity provider is supported.

Typically, a Web Chat is embedded in a website page. When the user sign on the website, the Web Chat invokes a bot on behalf of the user. The website client's token, based on the user's credentials, is exchanged for a different one to access the bot. In this way, the user does not have to sign on twice; the first time on the website, and the second time on the bot hence, the term SSO.

The following diagram shows the SSO flow when using a Web Chat client.

> [!NOTE]
> Fix the diagram to include normal flow (status 200).

![bot sso webchat](~/v4sdk/media/concept-bot-authentication/bot-auth-sso-webchat-time-sequence.PNG)

In the case of failure, SSO falls back to the existing behavior of showing the **OAuth** card. The failure may be caused for example if the user consent is required or if the token exchange fails.

Let's analyze the flow.

1. The client starts a conversation with the bot triggering an OAuth scenario.
1. The bot sends back an OAuth Card to the client.
1. The client intercepts the OAuth card before displaying it to the user and checks if it contains a `TokenExchangeResource` property.
1. If the property exisists, the client sends a `TokenExchangeInvokeRequest` to the bot. The client must have an exchangeable token for the user, which must be an Azure AD v2 token and whose audience must be the same as `TokenExchangeResource.Uri` property.  The client sends an Invoke activity to the bot with the body shown below.

    ```json
    {
        "type": "Invoke",
        "name": "signin/tokenExchange",
        "value": {
            "id": "<any unique Id>",
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
            "id":"<unique Id>",
            "connectionName": "<connection Name on the skill bot (from the OAuth Card)>",
            "failureDetail": "<failure reason if status code is not 200, null otherwise>"
        }
    }
    ```

1. If the `TokenExchangeInvokeResponse` has a `status` of `200`, then the client does not show the OAuth card. See the *normal flow* diagram. For any other `status` or if the `TokenExchangeInvokeResponse` is not received, then the client shows the OAuth card to the user. See the *fallback flow* diagram. This ensures that the SSO flow falls back to normal OAuthCard flow, in case of any errors or unmet dependencies like user consent.

> [!WARNING]
> For an example on how to get the user's exchangeable token and more, please refer to [Web Chat samples (TBD)](https://linkrequired).

<!--
## Website with secure access

Many websites support authentication. Once a user has signed on to a website, a token is typically stored in a cookie and/or in-memory in the user’s web browser. This token is most often included in the authorization header to API calls that the browser makes to backend services.

If you want your bot to use the same user's token as the client website, then the Web Chat can be configured to send the user's token with every message to the bot. This is preferable to sending the token once and having the bot cache it because the token could expire, and the bot cannot refresh it. Also, it takes a sizable amount of work to securely cache and store user tokens.

To configure the Web Chat to send user tokens with each outgoing message, you can use the Web Chat [BackChannel](https://github.com/Microsoft/BotFramework-WebChat#the-backchannel) capability. This allows to intercept every outgoing activity and augment it with any extra information, including the user token. (**Is this correct? Are we going to have an example?**)

> [!WARNING]
> ?? Example needed here ??

## Website with anonymous and secure access

In some cases, websites use bots that allow anonymous access to get basic information such as a bank opening hours, routing number, branch address information and so on. That same bot can perform secure tasks such as balance inquiries or transfer money, which require user's sign on.

In this case, it is often preferable to have the bot ask the website to prompt the user to sign on, and then have the resulting user token sent to the bot. In order for this to happen the bot must inform the website that it needs the user token. This can be done using a custom *EventActivity* from the bot to the Web Chat.

When the bot receives user's request to perform a secure task, it can send two replies:

- A *MessageActivity* asking the user to sign on.
- An *EventActivity* with a name such as *sign on request*.

The Web Chat can be configured to listen for events and perform special processing when it receives the *sign on request* event. In this case, it prompts the user to sign on to the website. Once the user has signed on, a token can be returned to the bot as another *EventActivity*.

> [!WARNING]
> ?? Example needed here ??
-->