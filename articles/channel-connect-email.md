---
title:  Connect a bot to Office 365 email | Microsoft Docs
description: Learn how to configure a bot to send and receive email with Office 365.
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 06/05/2017
ms.reviewer:
---
# Connect a bot to Office 365 email

Bots can communicate with users via Office 365 email in addition to other [channels](~/portal-configure-channels.md). When a bot is configured to access an email account, it receives a message when a new email arrives. The bot can then respond as indicated by its business logic. For example, the bot could send an email reply acknowledging an email was received with the message, "Hi! Thanks for your order! We will begin processing it immediately." 

> [!WARNING]
> It is a violation of the Bot Framework [Code of Conduct](https://www.botframework.com/Content/Microsoft-Bot-Framework-Preview-Online-Services-Agreement.htm) to create "spambots", including bots that send unwanted or unsolicited bulk email.

## Configure email credentials

You can connect a bot to the Email channel by entering Office 365 credentials in the Email channel configuration.

1. Sign in to the <a href="https://dev.botframework.com/" target="_blank">Bot Framework Portal</a>.
2. Click **My bots**.
3. Select your bot.
4. Click the **Email** channel.  
5. Enter valid email credentials.
    * Email address
    * Password
6. Click **Save**.

![Enter email credentials](~/media/channel-connect-email/channel-connect-email-credentials.png)

The Email channel currently works with Office 365 only. Other email services are not currently supported.

## Customize emails

The Email channel supports sending custom properties to create more advanced, customized emails using the `channelData` property.

| Property | Description |
|---------|  -----|
| HtmlBody   | The HTML to use for the body of the message. |
| Subject    | The subject to use for the message.|
| Importance | The importance flag to use for the message.(Low/Normal/High) |

The following example message shows a JSON file that includes the `channelData` properties.

```json
    {
        "type": "message",
        "locale": "en-Us",
        "channelID":"email",
        "from": { "id":"mybot@mydomain.com", "name":"My bot"},
        "recipient": { "id":"joe@otherdomain.com", "name":"Joe Doe"},
        "conversation": { "id":"123123123123", "topic":"awesome chat" },
        "channelData":
        {
            "htmlBody" : "<html><body style = /"font-family: Calibri; font-size: 11pt;/" >This is more than awesome</body></html>",
            "subject":"Super awesome message subject",
            "importance":"high"
        }
    }
```
For more information about using `channelData`, see the [Node.js](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/core-ChannelData) sample or [.NET](~/dotnet/bot-builder-dotnet-channeldata.md) documentation.

## Additional resources

* Connect a bot to [channels](~/portal-configure-channels.md)
* [Implement channel-specific functionality](dotnet/bot-builder-dotnet-channeldata.md) with the Bot Builder SDK for .NET
* Use the [Channel Inspector](portal-channel-inspector.md) to see how a channel renders a particular feature of your bot application
