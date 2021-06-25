---
title:  Connect a bot to Office 365 email - Bot Service
description: Learn how to configure bots to send and receive email messages by connecting them to Microsoft 365 email. See how to customize messages.
keywords: Office 365, bot channels, email, email credentials, azure portal, custom email
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/09/2019
---

# Connect a bot to Office 365 email

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

Bots can communicate with users via Office 365 email in addition to other [channels](bot-service-manage-channels.md). When a bot is configured to access an email account, it receives a message when a new email arrives. The bot can then respond as indicated by its business logic. For example, the bot could send an email reply acknowledging an email was received with the message, "Hi! Thanks for your order! We will begin processing it immediately."

> [!WARNING]
> It's a violation of the Bot Framework [Code of Conduct](https://www.botframework.com/Content/Developer-Code-of-Conduct-for-Microsoft-Bot-Framework.htm) to create "spambots", including bots that send unwanted or unsolicited bulk email.

> [!NOTE]
> If you are using Microsoft Exchange Server, make sure you have enabled [Autodiscover](/exchange/client-developer/exchange-web-services/autodiscover-for-exchange) first before configuring email channel.

## Configure email credentials

You can connect a bot to the Email channel by entering Office 365 credentials in the Email channel configuration.
Federated authentication using any vendor that replaces AAD is not supported.

> [!NOTE]
> You should not use your own personal email accounts for bots, as every message sent to that email account will be forwarded to the bot. This can result in the bot inappropriately sending a response to a sender. For this reason, bots should only use dedicated O365 email accounts.

To add the Email channel, open the bot in the [Azure Portal](https://portal.azure.com/), click the **Channels** blade, and then click **Email**. Enter your valid email credentials and click **Save**.

![Enter email credentials](media/bot-service-channel-connect-email/bot-service-channel-connect-email-credentials.png)

The Email channel currently works with Office 365 only. Other email services are not currently supported.

## Customize emails

The Email channel supports sending custom values to create more advanced, customized emails by using the activity `channelData` property.
The snippet below shows an example of the `channelData` for an incoming custom email message, from the bot to the user.

[!INCLUDE [email channelData json](includes/snippet-channelData-email.md)]

For more information about the activity `channelData` property, see [Create a custom Email message](v4sdk/bot-builder-channeldata.md#create-a-custom-email-message).

## Other considerations

If your bot does not return a 200 OK HTTP status code within 15 seconds in response to an incoming email message, the email channel will try to resend the message, and your bot may receive the same email message activity a few times. For more information, see the [HTTP details](v4sdk/bot-builder-basics.md#http-details) section in **How bots work** and the how to [troubleshooting timeout errors](https://github.com/daveta/analytics/blob/master/troubleshooting_timeout.md) article.

> [!NOTE]
> If you are using an Office 365 account with MFA enabled on it, make sure you disable MFA for the specified account first, then you can configure the account for the email channel. Otherwise, the connection will fail.

## Additional resources

- Connect a bot to [channels](bot-service-manage-channels.md)
- [Implement channel-specific functionality](v4sdk/bot-builder-channeldata.md) with the Bot Framework SDK for .NET
- Read the [channels reference](bot-service-channels-reference.md) article for more information about which features are supported on each channel