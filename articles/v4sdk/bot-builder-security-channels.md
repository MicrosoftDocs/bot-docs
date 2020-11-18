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

The attacker makes the bot thinks he is someone else. For example, in Web Chat, the attacker can impersonate someone else by **changing the user ID** of his Web Chat instance.

#### Impersonation mitigation

- Make the **user ID unguessable**.
- [Connect a bot to Direct Line](../bot-service-channel-connect-directline.md).
- Enable the Direct Line channel's [enhanced authentication](../bot-service-channel-connect-directline.md#configure-settings) option to allow the Azure Bot Service to further detect and reject any user ID change. This means the user ID (`Activity.From.Id`) on messages from Direct Line to the bot will always be the same as the one you used to initialize the Web Chat control.

    This is because Direct Line creates a **token** based on the Direct Line secret and embeds the *User.Id* in the token. Direct Line makes sure the messages sent to the bot have that *User id* as the activity's *From.Id*. If a client sends a message to Direct Line having a different *From.Id*, it will be changed to the **Id embedded in the token** before forwarding the message to the bot. So you cannot use another user ID after a channel secret is initialized with a user ID.

    This feature requires the user ID to start with `dl_`.

    See the following [code samples](../rest-api/bot-framework-rest-direct-line-3-0-authentication.md#code-examples).

### User identity spoofing

Identity spoofing refers to the action of an attacker that assumes the identity of a legitimate user and then uses that identity to accomplish a malicious goal.

You must be aware that there are two user identities:

1. The channel user’s identity.
1. The user’s identity from the identity provider that the bot uses to authenticated the user.

When a bot asks the channel user A to sign-in to an identity provider, the sign-in process must assure that user A is the only one that signs into the provider. If another user B is also allowed to sign-in the provider, he would have access to user A resources through the bot.

#### User identity  mitigation

In the Web Chat channel, there are two mechanisms to assure that the proper user is signed in.

1. **Magic code**. At the end of the sign-in process, the user is presented with a randomly generated 6-digit code (*magic code*). The user must type this code in the conversation to complete the sign-in process. This tends to result in a bad user's experience. Additionally, it is still susceptible to phishing attacks. A malicious user can trick another user to sign-in and obtain the magic code through phishing. This approach is deprecated. Instead, it is recommended to use the **Direct Line enhanced authentication** approach described below.

1. **Direct Line enhanced authentication**. Because of the issues with the *magic code* approach, Azure Bot Service removed its need. Azure Bot Service guarantees that the sign-in process can only be completed in the **same browser session** as the Web Chat itself.
To enable this protection, you must start Web Chat with a **Direct Line token** that contains a **list of trusted domains that can host the bot’s Web Chat client**. With enhanced authentication options, you can statically specify the trusted domain (origin) list in the Direct Line configuration page. See [enhanced authentication settings](../bot-service-channel-connect-directline.md#configure-settings).
