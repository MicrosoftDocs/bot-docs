---
title: Implement channel-specific functionality with the Bot Builder SDK for .NET | Microsoft Docs
description: Learn how to implement channel-specific functionality by using the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, channel data, channel-specific functionality, custom message
author: kbrandl
manager: rstand
ms.topic: develop-dotnet-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 03/13/2017
ms.reviewer:
#ROBOTS: Index
---

# Implement channel-specific functionality

By using the **Activity** object's **ChannelData** property 
to pass native metadata to a channel, 
you can implement channel-specific functionality that is not possible to achieve
by using [message text and attachments](bot-framework-dotnet-compose-messages.md) alone. 
This article describes how to use a message activity's **ChannelData** property to implement the following channel-specific functionality.

| Channel | Functionality |
|----|----|
| Email | Send and receive an email that contains body, subject, and importance metadata |
| Slack | Send full fidelity Slack messages |
| Facebook | Send Facebook notifications natively |
| Telegram | Perform Telegram-specific actions, such as sharing a voice memo or a sticker |
| Kik | Send and receive native Kik messages | 

> [!NOTE]
> The value of an **Activity** object's **ChannelData** property is a JSON object. 
> Therefore, the examples in this article show the expected format of the 
> **channelData** JSON property in various scenarios. 
> To create a JSON object using .NET, use the **JObject** (.NET) class. 

## Create a custom Email message

To create an email message, set the **Activity** object's **ChannelData** property 
to a JSON object that contains the following properties. 

| Property | Description |
|----|----|
| htmlBody | An HTML document that specifies the body of the email message. See the channel's documentation for information about supported HTML elements and attributes. |
| importance | The email's importance level. Valid values are **high**, **normal**, and **low**. The default value is **normal**. |
| subject | The email's subject. See the channel's documentation for information about field requirements. |

> [!NOTE]
> Messages that your bot receives from users via the Email channel may 
> contain a **ChannelData** property that is populated with a JSON object like the one described above.

The following snippet shows an example of the **channelData** property for a custom email message.

[!code-javascript[Email channelData](../includes/code/dotnet-channelData.json#emailChannelData)]

## Create a full-fidelity Slack message

To create a full-fidelity Slack message, 
set the **Activity** object's **ChannelData** property to a JSON object that specifies 
<a href="https://api.slack.com/docs/messages" target="_blank">Slack messages</a>, 
<a href="https://api.slack.com/docs/message-attachments" target="_blank">Slack attachments</a>, and/or 
<a href="https://api.slack.com/docs/message-buttons" target="_blank">Slack buttons</a>. 

> [!NOTE]
> To support buttons in Slack messages, you must enable **Interactive Messages** when you 
> [configure your bot](bot-framework-publish-configure.md) for the Slack channel.

The following snippet shows an example of the **channelData** property for a custom Slack message.

[!code-javascript[Slack channelData](../includes/code/dotnet-channelData.json#slackChannelData1)]

When a user clicks a button within a Slack message, your bot will receive a response message
in which the **ChannelData** property is populated with a **payload** JSON object. 
The **payload** object specifies contents of the original message, 
identifies the button that was clicked, and identifies the user who clicked the button. 

The following snippet shows an example of the **channelData** property in the message that a bot receives 
when a user clicks a button in the Slack message.

[!code-javascript[Slack channelData](../includes/code/dotnet-channelData.json#slackChannelData2)]

Your bot can reply to this message in the [normal manner](bot-framework-dotnet-send-and-receive.md#create-reply), 
or it can post its response directly to the endpoint that is specified by 
the **payload** object's **response_url** property.
For information about when and how to post a response to the **response_url**, see 
<a href="https://api.slack.com/docs/message-buttons" target="_blank">Slack Buttons</a>. 

## Create a Facebook notification

To create a Facebook notification, 
set the **Activity** object's **ChannelData** property to a JSON object that specifies the following properties. 

| Property | Description |
|----|----|
| notification_type | The type of notification (e.g., **REGULAR**, **SILENT_PUSH**, **NO_PUSH**).
| attachment | An attachment that specifies an image, video, or other multimedia type, or a templated attachment such as a receipt. |

> [!NOTE]
> For details about format and contents of the **notification_type** property and **attachment** property, see the 
> <a href="https://developers.facebook.com/docs/messenger-platform/send-api-reference#guidelines"target="_blank">Facebook API documentation</a>. 

The following snippet shows an example of the **channelData** property for a Facebook receipt attachment.

[!code-javascript[Facebook channelData](../includes/code/dotnet-channelData.json#facebookChannelData)]

## Create a Telegram message

To create a message that implements Telegram-specific actions, 
such as sharing a voice memo or a sticker, 
set the **Activity** object's **ChannelData** property to a JSON object that specifies the following properties. 

| Property | Description |
|----|----|
| method | The Telegram Bot API method to call. |
| parameters | The parameters of the specified method. |

The following Telegram methods are supported: 

- answerInlineQuery
- editMessageCaption
- editMessageReplyMarkup
- editMessageText
- forwardMessage
- kickChatMember
- sendAudio
- sendChatAction
- sendContact
- sendDocument
- sendLocation
- sendMessage
- sendPhoto
- sendSticker
- sendVenue
- sendVideo
- sendVoice
- unbanChateMember

For details about these Telegram methods and their parameters, see the 
<a href="https://core.telegram.org/bots/api#available-methods" target="_blank">Telegram Bot API documentation</a>.

> [!NOTE]
> <ul><li>The **chat_id** parameter is common to all Telegram methods. If you do not specify **chat_id** as a parameter, the framework will provide the ID for you.</li>
<li>Instead of passing file contents inline, specify the file using a URL and media type as shown in the example below.</li>
<li>Within each message that your bot receives from the Telegram channel, the **ChannelData** property will include the message that your bot sent previously.</li></ul>

The following snippet shows an example of a **channelData** property that specifies a single Telegram method.

[!code-javascript[Telegram channelData](../includes/code/dotnet-channelData.json#telegramChannelData1)]

The following snippet shows an example of a **channelData** property that specifies an array of Telegram methods.

[!code-javascript[Telegram channelData](../includes/code/dotnet-channelData.json#telegramChannelData2)]

## Create a native Kik message

To create a native Kik message, 
set the **Activity** object's **ChannelData** property to a JSON object that specifies the following property.

| Property | Description |
|----|----|
| messages | An array of Kik messages. For details about Kik message format, see <a href="https://dev.kik.com/#/docs/messaging#message-formats" target="_blank">Kik Message Formats</a>. |

The following snippet shows an example of the **channelData** property for a native Kik message.

[!code-javascript[Telegram channelData](../includes/code/dotnet-channelData.json#kikChannelData)]

## Additional resources

- [Send and receive activities](bot-framework-dotnet-send-and-receive.md)
- [Compose messages](bot-framework-dotnet-compose-messages.md)
- [Add attachments to messages](bot-framework-dotnet-add-attachments.md)
