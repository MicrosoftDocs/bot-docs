---
title: Preview bot features with the Channel Inspector | Microsoft Docs
description: Learn how various Bot Framework features look and work on different channels by using the Channel Inspector.
keyword: bot, features, channel, display
author: Kbrandl
ms.author: kibrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 06/06/2017
---

# Preview bot features with the Channel Inspector

The Bot Framework enables you to create bots with a variety of features such as buttons, images, rich cards displayed in carousel or list format, and more. However, each channel ultimately controls how features are rendered by its messaging clients. 

Even when a feature is supported by multiple channels, each channel may render the feature in a slightly different way. In cases where a message contains feature(s) that a channel does not natively support, the channel may attempt to down-render message contents as text or as a static image, which can significantly impact the message's appearance on the client. In some cases, a channel may not support a particular feature at all. For example, GroupMe clients cannot display a typing indicator.

Use the [Channel Inspector][inspector] to see how various Bot Framework features look and work on different channels. By understanding how features are rendered by various channels, you'll be able to design your bot to deliver an exceptional user experience on the channels where it communicates. The Channel Inspector also provides a great way to learn about and visually explore Bot Framework features.

> [!NOTE]
> Rich cards are a developing standard for bot information exchange to ensure consistent display across multiple channels. See the [.NET][netcard] or [Node.js][nodecard] documentation for more information about rich cards.

## Preview features across various channels

To see how a channel renders a particular feature, select the channel from **Channel** list and the feature from the **Feature** list. For example, to see how Skype renders a Hero Card, set **Channel** to *Skype* and **Feature** to *HeroCard*.

![Channel Inspector showing Skype channel and Hero Card](~/media/portal-channel-inspector.png)

The Channel Inspector shows a preview of the selected feature as it will be rendered by the specified channel. The **Notes** section conveys important information about message limitations and/or display changes. For example, some types of rich cards support only one image and some features may be down-rendered on certain channels. 

> [!NOTE]
> The Channel Inspector currently supports these channels: Email, Facebook, GroupMe, Kik, Skype, Slack, SMS, Microsoft Teams, Telegram, WebChat. Additional channels may be added in the future.

## Features that can be previewed

The Channel Inspector currently allows you to preview the following features. 

|Feature | Description|
| --- | ----|
 Buttons| Buttons that the user can click. Buttons appear on the conversation canvas with the message they belong to. |
| Carousel| A compact, scrollable horizontal list of cards. For a vertical layout, use List.|
| ChannelData| A way to pass metadata to access channel-specific functionality beyond cards, text, and attachments.|
| Codesample| A multi-line, pre-formatted block of text designed to display computer code.|
| DirectMessage| A message sent to a single member of a group conversation. 
| Document| Send and receive non-media attachments. |
| Emoji| Display supported emoji. 
| GroupChat| Bot sends messages to participate in group conversations. |
| HeroCard| A formatted attachment typically containing a single large image, a tap action, and buttons (optional), along with descriptive text content. |
| Images| Display of image attachments. |
| List| A vertical list of cards. For a horizontal, scrollable layout, use Carousel.|
| Location| Share the user's physical location with the bot. |
| Markdown| Renders text formatted with Markdown.|
| Members| Shares the list of members in the conversation with the bot during a group chat. |
| ReceiptCard| Display a receipt to the user. |
| SigninCard| Request the user to enter authentication credentials.|
| ThumbnailCard| A formatted attachment containing a single small image (thumbnail), a tap action, and buttons (optional), along with descriptive text content. |
| Typing| Displays a typing indicator. This is helpful to inform the user that the bot is still functioning, but performing some action in the background.|
|  Video| Displays video attachments and play controls.|

## Additional resources

* [Channel Inspector][inspector]
* Rich cards in [Node.js][nodecard] and [.NET][netcard]
* Media attachments in [Node.js][nodemedia] and [.NET][netmedia]
* Suggested actions in [Node.js][nodebutton] and [.NET][netbutton]

[inspector]: https://docs.botframework.com/en-us/channel-inspector/channels/Skype/

[syntax]: https://daringfireball.net/projects/markdown/syntax

[netcard]: ~\dotnet\bot-builder-dotnet-add-rich-card-attachments.md
[nodecard]: ~\nodejs\bot-builder-nodejs-send-rich-cards.md

[netmedia]: ~\dotnet\bot-builder-dotnet-add-media-attachments.md
[nodemedia]: ~\nodejs\bot-builder-nodejs-send-receive-attachments.md

[netbutton]: ~\dotnet\bot-builder-dotnet-add-suggested-actions.md
[nodebutton]: ~\nodejs\bot-builder-nodejs-send-suggested-actions.md
