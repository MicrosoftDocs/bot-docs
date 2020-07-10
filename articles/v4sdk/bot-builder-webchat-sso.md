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

Single sign on (SSO) allows a client, such as Web Chat control to communicate with a bot or skill on behalf of the user. Currently, only the [Azure AD v2](~v4sdk/bot-builder-concept-identity-providers.md#azure-active-directory-identity-provider) identity provider is supported.

A typical scenario is the one where a Web Chat is embedded into a website. When the user sign in the website, the Web Chat invokes a bot or a skill on behalf of the user. This is because the client’s user token, based on the user's credentials, is exchanged for a different token to be used with the same identity provider, but with a different application, the bot in this case. Hence, the term SSO, because the user does not have to sign on twice; the first time on the web site, and the second time nn the bot.

The following shows a normal and a fallback flow when using a Web Chat client.

![bot sso webchat](~/v4sdk/media/concept-bot-authentication/bot-auth-sso-webchat-time-sequence.PNG)

In the case of failure, SSO falls back to the existing behavior of showing the OAuth card. The failure may be caused for example if the user consent is required or if the token exchange fails.

For more information, see [Single sign on](~/v4sdk/bot-builder-concept-sso.md)