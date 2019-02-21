---
title:  Connect a bot to Office 365 email | Microsoft Docs
description: Learn how to configure a bot to send and receive email with Office 365.
keywords: Office 365, bot channels, email, email credentials, azure portal, custom email
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 02/08/2019
---
# Connect a bot to Office 365 email

Bots can communicate with users via Office 365 email in addition to other [channels](~/bot-service-manage-channels.md). When a bot is configured to access an email account, it receives a message when a new email arrives. The bot can then respond as indicated by its business logic. For example, the bot could send an email reply acknowledging an email was received with the message, "Hi! Thanks for your order! We will begin processing it immediately."

> [!WARNING]
> It is a violation of the Bot Framework [Code of Conduct](https://www.botframework.com/Content/Microsoft-Bot-Framework-Preview-Online-Services-Agreement.htm) to create "spambots", including bots that send unwanted or unsolicited bulk email.

## Configure email credentials

You can connect a bot to the Email channel by entering Office 365 credentials in the Email channel configuration.
Federated authentication using any vendor that replaces AAD is not supported.

> [!NOTE]
> You should not use your own personal email accounts for bots, as every message sent to that email account will be forwarded to the bot. This can result in the bot inappropriately sending a response to a sender. For this reason, bots should only use dedicated O365 email accounts.

To add the Email channel, open the bot in the [Azure Portal](https://portal.azure.com/), click the **Channels** blade, and then click **Email**. Enter your valid email credentials and click **Save**.

![Enter email credentials](~/media/bot-service-channel-connect-email/bot-service-channel-connect-email-credentials.png)

The Email channel currently works with Office 365 only. Other email services are not currently supported.

## Customize emails

The Email channel supports sending custom properties to create more advanced, customized emails using the `channelData` property.

[!INCLUDE [Email channelData table](~/includes/snippet-channelData-email.md)]

The following example message shows a JSON file that includes these `channelData` properties.

```json
{
    "type": "message",
    "locale": "en-Us",
    "channelID": "email",
    "from": { "id": "mybot@mydomain.com", "name": "My bot"},
    "recipient": { "id": "joe@otherdomain.com", "name": "Joe Doe"},
    "conversation": { "id": "123123123123", "topic": "awesome chat" },
    "channelData":
    {
        "htmlBody": "<html><body style = /"font-family: Calibri; font-size: 11pt;/" >This is more than awesome.</body></html>",
        "subject": "Super awesome message subject",
        "importance": "high",
        "ccRecipients": "Yasemin@adatum.com;Temel@adventure-works.com"
    }
}
```

::: moniker range="azure-bot-service-3.0"
For more information about using `channelData`, see the [Node.js](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/core-ChannelData) sample or [.NET](~/dotnet/bot-builder-dotnet-channeldata.md) documentation.
::: moniker-end

::: moniker range="azure-bot-service-4.0"
For more information about using `channelData`,
see [how to implement channel-specific functionality](~/v4sdk/bot-builder-channeldata.md).
::: moniker-end

## Other considerations

If your bot does not return a 200 OK HTTP status code within 15 seconds in response to an incoming email message, the email channel will try to resend the message, and your bot may receive the same email message activity a few times. For more information, see the [HTTP details](v4sdk/bot-builder-basics.md#http-details) section in **How bots work** and the how to [troubleshooting timeout errors](https://github.com/daveta/analytics/blob/master/troubleshooting_timeout.md) article.

## Additional resources

<!-- Put whole list in monikers, even though it's just the second item that needs to be different. -->
::: moniker range="azure-bot-service-3.0"
* Connect a bot to [channels](~/bot-service-manage-channels.md)
* [Implement channel-specific functionality](dotnet/bot-builder-dotnet-channeldata.md) with the Bot Framework SDK for .NET
* Use the [Channel Inspector](bot-service-channel-inspector.md) to see how a channel renders a particular feature of your bot application
::: moniker-end
::: moniker range="azure-bot-service-4.0"
* Connect a bot to [channels](~/bot-service-manage-channels.md)
* [Implement channel-specific functionality](~/v4sdk/bot-builder-channeldata.md) with the Bot Framework SDK for .NET
* Use the [Channel Inspector](bot-service-channel-inspector.md) to see how a channel renders a particular feature of your bot application
::: moniker-end
