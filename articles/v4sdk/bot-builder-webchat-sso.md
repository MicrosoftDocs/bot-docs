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

With signing on, the bot identifies the user who is part of a conversation to perform actions on the user's behalf against identity providers.
For example, a bot can ask a user to sign on to Office 365 to check the user’s recent email or calendar.

The Bot Framework currently support the following sign on options:

1. Sharing the client’s user token directly with the bot via channel data.
1. Using an OAuthCard to drive a sign on experience to any OAuth provider.
1. Single sign on (SSO), where the client UI takes the client’s user token for the client application and exchanges it for a different token that can be used with the same identity provider, but with a different application and scope. It is possible to create a similar user experience using Web Chat by using technique 1 shown above.