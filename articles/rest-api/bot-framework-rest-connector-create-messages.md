---
title: Create messages with the Bot Connector API  | Microsoft Docs
description: Learn about commonly-used message properties within the Bot Connector API. 
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer: 
---

# Create messages

Your bot will send **message** [activities](~/rest-api/bot-framework-rest-connector-activities.md) to communicate information to users, and likewise, will also receive **message** activities from users. Some messages may simply consist of plain text, while others may contain richer content such as [media attachments](~/rest-api/bot-framework-rest-connector-add-media-attachments.md), [rich cards](~/rest-api/bot-framework-rest-connector-add-rich-cards.md), and [channel-specific data](~/rest-api/bot-framework-rest-connector-channeldata.md). This article describes some of the commonly-used message properties.

## Message text and format

To create a basic message that contains only plain text, simply specify the `text` property (as contents of the message) 
and the `locale` property (as the locale of the sender). 

The `textFormat` property of a message can be used to specify the format of the text. The value of the `textFormat` property defaults to **markdown** and interprets text using Markdown formatting standards, 
but you can also specify **plain** to interpret text as plain text or **xml** to interpret text as XML markup.

If a channel is not capable of rendering the specified formatting, the message will be rendered using reasonable approximation. For example, Markdown **bold** text for a message sent via text messaging will be rendered as \*bold\*. Only channels that support fixed width formats and HTML can render standard table markdown.

These styles are supported when `textFormat` is set to **markdown**:  

| Style | Markdown | Example | 
| ---- | ---- | ---- | 
| bold | \*\*text\*\* | **text** |
| italic | \*text\* | *text* |
| header (1-5) | # H1 | #H1 |
| strikethrough | \~\~text\~\~ | ~~text~~ |
| horizontal rule | --- |  |
| unordered list | \* text |  <ul><li>text</li></ul> |
| ordered list | 1. text | 1. text |
| preformatted text | \`text\` | `text`  |
| blockquote | \> text | <blockquote>text</blockquote> |
| hyperlink | \[bing](http://www.bing.com) | [bing](http://www.bing.com) |
| image link| !\[duck](http://aka.ms/Fo983c) | ![duck](http://aka.ms/Fo983c) |

These styles are supported when `textFormat` is set to **xml**:  

| Style | Markdown | Example | 
|----|----|----|----|
| bold | \<b\>text\</b\> | **text** | 
| italic | \<i\>text\</i\> | *text* |
| underline | \<u\>text\</u\> | <u>text</u> |
| strikethrough | \<s\>text\</s\> | <s>text</s> |
| hyperlink | \<a href="http://www.bing.com"\>bing\</a\> | <a href="http://www.bing.com">bing</a> |

> [!NOTE]
> The `textFormat` **xml** is supported only by the Skype channel. 

## Attachments

The `attachments` property of a message activity can be used to send simple media attachments 
(image, audio, video, file) and rich cards. For details, see [Add media attachments to messages](~/rest-api/bot-framework-rest-connector-add-media-attachments.md) and [Add rich cards to messages](~/rest-api/bot-framework-rest-connector-add-rich-cards.md).

## Entities

The `entities` property of a message is an array of open-ended <a href="http://schema.org/" target="_blank">schema.org</a> objects that allows the exchange of common contextual metadata between the channel and bot.

### Mention entities

Many channels support the ability for a bot or user to "mention" someone within the context of a conversation. 
To mention a user in a message, populate the message's `entities` property with a [Mention][Mention] object. 

### Place entities

To convey <a href="https://schema.org/Place" target="_blank">location-related information</a> within a message, populate the message's `entities` property with [Place][Place] object. 

## Channel data

The `channelData` property of a message activity can be used to implement channel-specific functionality. 
For details, see [Implement channel-specific functionality](~/rest-api/bot-framework-rest-connector-channeldata.md).

## Additional resources

- [Activities overview](~/rest-api/bot-framework-rest-connector-activities.md)
- [Send and receive messages](~/rest-api/bot-framework-rest-connector-send-and-receive-messages.md)
- [Add media attachments to messages](~/rest-api/bot-framework-rest-connector-add-media-attachments.md)
- [Add rich cards to messages](~/rest-api/bot-framework-rest-connector-add-rich-cards.md)
- [Implement channel-specific functionality](~/rest-api/bot-framework-rest-connector-channeldata.md)

[Mention]: ~/rest-api/bot-framework-rest-connector-api-reference.md#mention-object
[Place]: ~/rest-api/bot-framework-rest-connector-api-reference.md#place-object