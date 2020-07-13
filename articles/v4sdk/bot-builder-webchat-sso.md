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

Typically, a Web Chat is embedded in website page. When the user sign on the website, the Web Chat invokes a bot on behalf of the user. The website client's token, based on the user's credentials, is exchanged for a different one to access the bot. Hence, the term SSO, because the user does not have to sign on twice; the first time on the website, and the second time on the bot.

The following shows the SSO flow when using a Web Chat client.

![bot sso webchat](~/v4sdk/media/concept-bot-authentication/bot-auth-sso-webchat-time-sequence.PNG)

In the case of failure, SSO falls back to the existing behavior of showing the OAuth card. The failure may be caused for example if the user consent is required or if the token exchange fails.

For more information, see [Single sign on](~/v4sdk/bot-builder-concept-sso.md)

Let's look at a couple of typical website scenarios where the Web Chat is used.

## Website with secure access

Many websites support authentication. Once a user has signed on to a website, a token is typically stored in a cookie and/or in-memory in the user’s web browser. This token is most often included in the authorization header to API calls that the browser makes to backend services.

If you want your bot to use the same user's token as the client website, then the Web Chat can be set up to send the user's token with every message to the bot. This is preferable to sending the token once and having the bot cache it because the token could expire, and the bot cannot refresh it. Also it takes some work to securely cache and store user tokens.

To configure the Web Chat to send user tokens with each outgoing message, you can use the Web Chat [BackChannel](https://github.com/Microsoft/BotFramework-WebChat#the-backchannel) capability. This allows to intercept every outgoing activity and augment it with any extra information, including the user token.

> [!WARNING]
> ?? Example needed here ??

## Website with anonymous and secure access

In some cases, websites use bots that allow anonymous access to get basic information such as a bank opening hours, routing number, branch address information and so on. That same bot can perform secure tasks such as balance inquiries or transfer money, which require user's sign on.

In this case, it is often preferable to have the bot ask the website to prompt the user to sign on, and then have the resulting user token sent to the bot. In order for this to happen the bot must inform the website that it needs the user token. This can be done using a custom *EventActivity* from the bot to the Web Chat.

When the bot receives user's request to perform a secure task, it can send two replies:

- A *MessageActivity* asking the user to sign on.
- An *EventActivity* with a name such as *sign on request*.

The Web Chat can be configured to listen for events and perform special processing when it receives this *sign on request* event. In this case, it prompts the user to sign on to the website. Once the user has signed on, the token can be returned to the bot as another *EventActivity*.

> [!WARNING]
> ?? Example needed here ??
