---
title:  Connect a bot to email - Bot Service
description: Learn how to configure bots to send and receive email messages by connecting them to Microsoft 365 email. See how to customize messages.
keywords: Office 365, bot channels, email, email credentials, azure portal, custom email
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: bot-service
ms.topic: how-to
ms.date: 03/10/2022
---

# Connect a bot to email

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

You can configure your bot to communicate with users via Microsoft 365 email. When you configure your bot to access an email account, it receives a message when a new email arrives. The bot can then use email to respond. For example, in response to a user's email message, the bot could send an email reply with the message, "Hi! Thanks for your order! We will begin processing it immediately."

The Email channel currently works with Office 365 only. Other email services aren't currently supported.

> [!WARNING]
>
> - For security reasons, Exchange Online will disable _basic authentication_ on October 1st, 2022. The Email channel now supports the new Exchange Online _modern authentication_ model. Bots that use the basic authentication model will experience failures after the October 2022 change; or earlier if your tenant administrator disables basic authentication before that date. For more information, see [Basic Authentication and Exchange Online – September 2021 Update](https://techcommunity.microsoft.com/t5/exchange-team-blog/basic-authentication-and-exchange-online-september-2021-update/ba-p/2772210).
> - It's a violation of the Bot Framework [Code of Conduct](https://www.botframework.com/Content/Developer-Code-of-Conduct-for-Microsoft-Bot-Framework.htm) to create "spambots", including bots that send unwanted or unsolicited bulk email.

## Prerequisites

- If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A dedicated Office 365 email account for the bot.
- Permission to grant the bot `Mail.ReadWrite` and `Mail.Send` access. For more information, see [Understanding Azure AD application consent experiences](/azure/active-directory/develop/application-consent-experience).

> [!NOTE]
> You shouldn't use your own personal email accounts for bots, as every message sent to that email account will be forwarded to the bot. This can result in the bot inappropriately sending a response to a sender. For this reason, bots should only use dedicated M365 email accounts.

## Configure email to use modern authentication

1. Open the [Azure portal](https://portal.azure.com/).
1. Open the Azure Bot resource blade for your bot.
1. In the left pane, select **Channels**.
1. Select **Email** to open the **Configure Email** blade.

    :::image type="content" source="media/bot-service-channel-connect-email/bot-service-channel-connect-modern-authentication.png" alt-text="Configure email settings":::

    1. Set **Authentication type** to **Modern authentication (OAUTH)**.
    1. In **Email Address**, enter the dedicated Office 365 email account for the bot.
    1. Select **Authorize**.
        1. When prompted, sign in to the email account and grant read/write and send permissions to the bot.
        1. On success, a page opens with a validation code. Copy the validation code.

            :::image type="content" source="media/bot-service-channel-connect-email/bot-service-channel-validation-code.png" alt-text="Interaction with validation code":::

    1. In **Authentication code**, enter the validation code you just copied.
    1. Select **Apply** to complete email configuration.

## Configure email to use basic authentication

> [!NOTE]
>
> - Federated authentication using any vendor that replaces Azure AD isn't supported.
> - For security reasons, usage of _basic authentication_ in Exchange Online is being disabled on October 1st, 2022.
>   You should migrate all of your bots to use _modern authentication_ before the deadline.
> - If you use Microsoft Exchange Server, make sure you've enabled [Autodiscover](/exchange/client-developer/exchange-web-services/autodiscover-for-exchange) first, before configuring email to use basic authentication.
> - If you're using an Office 365 account with MFA enabled on it, make sure you disable MFA for the specified account first; then you can configure the account for the email channel. Otherwise, the connection will fail.

1. Open the [Azure portal](https://portal.azure.com/).
1. Open the Azure Bot resource blade for your bot.
1. In the left pane, select **Channels (preview)** to open the **Channels** blade.
1. Select **Email** to open the **Configure Email** blade.

    :::image type="content" source="media/bot-service-channel-connect-email/bot-service-channel-connect-basic-authentication.png" alt-text="Enter email credentials":::

    1. Set **Authentication type** to **Basic authentication (disabling staring October, 2022)**.
    1. In **Email Address**, enter the dedicated Office 365 email account for the bot.
    1. In **Password**, enter the password for the email account.
    1. Select **Apply** to complete email configuration.

## Customize emails

The Email channel supports sending custom values to create more advanced, customized emails by using the activity `channelData` property.
The snippet below shows an example of the `channelData` for an incoming custom email message, from the bot to the user.

[!INCLUDE [email channelData json](includes/snippet-channelData-email.md)]

For more information about the activity `channelData` property, see [Create a custom Email message](v4sdk/bot-builder-channeldata.md#create-a-custom-email-message).

## Troubleshoot

For errors that can occur while processing consent to an application, see [Understanding Azure AD application consent experiences](/azure/active-directory/develop/application-consent-experience) and [Unexpected error when performing consent to an application](/azure/active-directory/manage-apps/application-sign-in-unexpected-user-consent-error).

If your bot doesn't return a 200 OK HTTP status code within 15 seconds in response to an incoming email message, the email channel will try to resend the message, and your bot may receive the same email message activity a few times. For more information, see the [HTTP details](v4sdk/bot-builder-basics.md#http-details) section in **How bots work** and the [troubleshooting timeout errors](https://github.com/daveta/analytics/blob/master/troubleshooting_timeout.md) article.

## Additional resources

- Connect a bot to [channels](bot-service-manage-channels.md)
- [Implement channel-specific functionality](v4sdk/bot-builder-channeldata.md) with the Bot Framework SDK for .NET
- Read the [channels reference](bot-service-channels-reference.md) article for more information about which features are supported on each channel
