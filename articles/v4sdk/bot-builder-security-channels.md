---
title: Bot Framework channels security - Bot Service
description: Learn about potential security risks when users connect to a bot using the allowed channels
author: mmiele
ms.author: v-mimiel
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 11/18/2020
---

# Bot Framework channels security

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article describes potential security risks when the users connect to a bot using the allowed channels, in particular the [Web Chat](~/bot-service-channel-connect-webchat.md) channel. It also shows mitigating solutions using the [Direct Line](../bot-service-channel-directline.md) channel with **enhanced authentication** enabled and ....

## Security risks

### Impersonation

The attacker makes the bot thinks they are someone else. For example, in Web Chat, attackers can impersonate someone else by **changing the user ID** of their Web Chat instance. To prevent this, it is recommend to bot developers to make the **user ID unguessable**.

#### Impersonation mitigation

- [Connect a bot to Direct Line](../bot-service-channel-connect-directline.md).
- Enable the Direct Line channel's [enhanced authentication](../bot-service-channel-connect-directline.md#configure-settings) options to allow the Azure Bot Service to further detect and reject any user ID change. This means the user ID (`Activity.From.Id`) on messages from Direct Line to your bot will always be the same as the one you initialized the Web Chat control with. Note that this feature requires the user ID to start with `dl_`. See the following [code samples](../rest-api/bot-framework-rest-direct-line-3-0-authentication.md#code-examples).

> [!NOTE]
> When a *User.Id* is provided while exchanging a secret for a token, that *User.Id* is embedded in the token. Direct Line makes sure the messages sent to the bot have that id as the activity's *From.Id*. If a client sends a message to Direct Line having a different *From.Id*, it will be changed to the **Id in the token** before forwarding the message to the bot. So you cannot use another user ID after a channel secret is initialized with a user ID.

### User identity

You must be aware that you deal with two user identities:

1. The channel user’s identity.
1. The user’s identity in the identity provider the bot uses to authenticated the user.

When a bot asks user A in a channel to sign in to an identity provider, the sign in process must assure that user A is the only one that signs into the provider. If another user B is allowed to sign in in the provider, then user A would have access to user B resources through the bot.

#### User identity  mitigation

In the Web Chat channel, there are 2 mechanisms for ensuring that the right user signed in.

1. At the end of sign-in, in the past, the user was presented with a randomly generated 6-digit code (aka magic code). The user must type this code in the conversation that initiated the sign-in to complete the sign-in process. This mechanism tends to result in a bad user experience. Additionally, it is still susceptible to phishing attacks. A malicious user can trick another user to sign-in and obtain the magic code through phishing.

1. Because of the issues with the previous approach, Azure Bot Service removed the need for the magic code. Azure Bot Service guarantees that the sign-in process can only be completed in the **same browser session** as the Web Chat itself.
To enable this protection, as a bot developer, you must start Web Chat with a **Direct Line token** that contains a **list of trusted domains that can host the bot’s Web Chat client**. Before, you could only obtain this token by passing an undocumented optional parameter to the Direct Line token API. Now, with enhanced authentication options, you can statically specify the trusted domain (origin) list in the Direct Line configuration page.
