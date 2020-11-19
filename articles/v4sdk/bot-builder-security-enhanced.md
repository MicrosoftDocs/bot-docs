---
title: Bot Framework channels security - Bot Service
description: Learn about potential security risks when users connect to a bot using the allowed channels
author: mmiele
ms.author: v-mimiel
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 11/19/2020
---

# Bot Framework enhanced authentication

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article describes potential security risks when the users connect to a bot, for example using the [Web Chat](~/bot-service-channel-connect-webchat.md#embed-the-web-chat-control-in-a-web-page) control. It shows mitigating solutions using the Direct Line [enhanced authentication settings](../bot-service-channel-connect-directline.md#configure-settings) and secure *user ID* handling.

You must be aware that there are two user identities:

- The channel user’s identity. An attacker can use it for [Impersonation](#Impersonation).
- The user’s identity from the identity provider that the bot uses to authenticated the user. An attacker can use it for [Identity spoofing](#Identity-spoofing).

## Impersonation

Impersonation refers to the action of an attacker that makes the bot think he is someone else. For example, in Web Chat, the attacker can impersonate someone else by **changing the user ID** of the Web Chat instance.

### Impersonation mitigation

- Make the **user ID unguessable**.
- [Connect a bot to Direct Line](../bot-service-channel-connect-directline.md).
- Enable the Direct Line channel's [enhanced authentication](../bot-service-channel-connect-directline.md#configure-settings) option to allow the Azure Bot Service to further detect and reject any user ID change. This means the user ID (`Activity.From.Id`) on messages from Direct Line to the bot will always be the same as the one you used to initialize the Web Chat control.

    [!NOTE]
    > Direct Line creates a **token** based on the Direct Line secret and embeds the *User.Id* in the token.
    > It assures that the messages sent to the bot have that *User.Id* as the activity's *From.Id*. If a client sends a message to Direct Line having a different *From.Id*, it will be changed to the **Id embedded in the token** before forwarding the message to the bot. So you cannot use another user ID after a channel secret is initialized with that ID.

    This feature requires the user ID to start with `dl_` as shown below:

    [!code-csharp[specify user ID](~/../botbuilder-samples/experimental/DirectLineTokenSite/Bot_Auth_DL_Secure_Site_MVC/Controllers/HomeController.cs?range=15-50&highlight=9)]

    The generated token, based on the Direct Line secret, is then used in the Web Chat control as shown below:

    [!code-csharp[specify token](~/../botbuilder-samples/experimental/DirectLineTokenSite/Bot_Auth_DL_Secure_Site_MVC/Views/Home/Index.cshtml?range=1-16&highlight=11-14)]

## Identity spoofing

Identity spoofing refers to the action of an attacker that assumes the identity of a legitimate user and then uses that identity to accomplish a malicious goal.

When a bot asks the channel user A to sign-in to an identity provider, the sign-in process must assure that user A is the only one that signs into the provider. If another user B is also allowed to sign-in the provider, he would have access to user A resources through the bot.

### User identity spoofing mitigation

In the Web Chat, there are two mechanisms to assure that the proper user is signed in.

1. **Magic code**. At the end of the sign-in process, the user is presented with a randomly generated 6-digit code (*magic code*). The user must type this code in the conversation to complete the sign-in process. This tends to result in a bad user's experience. Additionally, it is still susceptible to phishing attacks. A malicious user can trick another user to sign-in and obtain the magic code through phishing. This approach is deprecated. Instead, it is recommended to use the **Direct Line enhanced authentication** approach described below.

1. **Direct Line enhanced authentication**. Because of the issues with the *magic code* approach, Azure Bot Service removed its need. Azure Bot Service guarantees that the sign-in process can only be completed in the **same browser session** as the Web Chat itself.
To enable this protection, you must start Web Chat with a **Direct Line token** that contains a **list of trusted domains that can host the bot’s Web Chat client**. With enhanced authentication options, you can statically specify the trusted domain (origin) list in the Direct Line configuration page. See [Impersonation](#Impersonation) section.

## Example

The code in this article is based on the sample: [MVC DirectLine token controller](https://github.com/microsoft/BotBuilder-Samples/tree/main/experimental/DirectLineTokenSite).

To run the example, perform the following steps:

1. If you do not have a bot, create a basic one as shown in this article: [Tutorial: Create a basic bot](bot-builder-tutorial-create-basic-bot.md).
1. Connect the Direct Line channel to the bot. Follow the steps described in this article: [Connect a bot to Direct Line](../bot-service-channel-connect-directline.md).
1. When connecting the bot to the Direct Line, enable the [enhanced authentication](../bot-service-channel-connect-directline.md#configure-settings) option.
1. Copy and securely store the secret key.
1. Finally, assign the secret key in the example `HomeController` class, as shown below.

    [!code-csharp[cs sample](~/../botbuilder-samples/experimental/DirectLineTokenSite/Bot_Auth_DL_Secure_Site_MVC/Controllers/HomeController.cs?range=15-19&highlight=3-4)]

    The example (client application) will use the secret key to ask Direct Line to issue a token. This token, along with the user ID, uniquely and securely identifies the user to allow the communication with the bot using the Web Chat control.