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

As single sign on (SSO) allows a client, such as a Web Chat control, to communicate with a bot or skill on behalf of the user. Currently, only the [Azure AD v2](~/v4sdk/bot-builder-concept-identity-providers.md#azure-active-directory-identity-provider) identity provider is supported.

Typically, a Web Chat is embedded into a website. When the user sign on the website, the Web Chat invokes a bot or a skill on behalf of the user. This is because the client's token, based on the user's credentials, is exchanged for a different token to use with the same identity provider, but with a different application, the bot in this case. Hence, the term SSO, because the user does not have to sign on twice; the first time on the web site, and the second time on the bot.

The following shows a normal and a fallback flow when using a Web Chat client.

![bot sso webchat](~/v4sdk/media/concept-bot-authentication/bot-auth-sso-webchat-time-sequence.PNG)

In the case of failure, SSO falls back to the existing behavior of showing the OAuth card. The failure may be caused for example if the user consent is required or if the token exchange fails.

For more information, see [Single sign on](~/v4sdk/bot-builder-concept-sso.md)

Let's look at typical scenarios where Web Chat is used.

## Website with secure access

Many websites support authentication. Once a user has signed on to a website, a user token is typically stored in a cookie and/or in-memory in the user’s web browser. This token is most often included in the authorization header to API calls that the browser makes to backend services. It is best practice to use https protocol when making these calls and to verify the token authenticity on the backend service.

A similar approach applies to Web Chat and bots using the Bot Framework in that messages sent from Web Chat to the Bot can include the client’s user token in each message. Web Chat sends messages using https to the Azure Bot Service, which then signs them and passes them to the bot.

If you want your bot to use the same user token as the client website, then Web Chat can be set up to send the user token with every message to the bot. This is preferable to sending the token once and having the bot cache the token for the following reasons:

- The token could expire, and the bot cannot refresh it.
- It takes a decent amount of work to securely cache and store user tokens.

To configure WebChat to send user tokens with each outgoing message, you can use the Web Chat [BackChannel](https://github.com/Microsoft/BotFramework-WebChat#the-backchannel) capability. This allows to intercept every outgoing activity and augment it with any extra information, including the user token.

> [!WARNING]
> ?? Example needed here ??

## Website with anonymous and secure access

In some cases, websites use bots that allow anonymous access to get basic information that, for a bank, could be opening hours, the bank routing number, or branch address information. That same bot can perform secure tasks such as balance inquiries or transfer money, which require the user to be signed on.

In this case, it is often preferable to have the bot ask the website to prompt the user to sign on, and then have the resulting user token sent to the bot. In order for this to happen the bot must inform the website that it needs the user token. This can be done using a custom *EventActivity* from the bot to Web Chat.

When the bot receives user's request to perform a secure it can send two replies:

- A *MessageActivity* asking the user to sign on.
- An *EventActivity* with a name such as *sign on request*.

The Web Chat can be configured to listen for events and perform special processing when it receives this *sign on request* event. In this case, it prompts the user to sign on to the website using website controls. Once the user has signed on, the token can be returned to the bot as another *EventActivity* and in subsequent *MessageActivities*.

> [!WARNING]
> ?? Example needed here ??
> To configure WebChat to intercept incoming activities to ‘listen’ for the *sign on request* event, you can create your own instance of DirectLine and subscribe to the activity$ observable on the DirectLine object: