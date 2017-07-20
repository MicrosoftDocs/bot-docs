---
title: Create messages with the Bot Connector API  | Microsoft Docs
description: Learn about commonly-used message properties within the Bot Connector API. 
author: kbrandl
ms.author: kibrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 05/12/2017
---

# Create messages

Your bot will send [Activity][Activity] objects of type **message** to communicate information to users, and likewise, will also receive **message** activities from users. Some messages may simply consist of plain text, while others may contain richer content such as [text to be spoken](bot-framework-rest-connector-text-to-speech.md), [suggested actions](bot-framework-rest-connector-add-suggested-actions.md), [media attachments](bot-framework-rest-connector-add-media-attachments.md), [rich cards](bot-framework-rest-connector-add-rich-cards.md), and [channel-specific data](bot-framework-rest-connector-channeldata.md). This article describes some of the commonly-used message properties.

## Message text and format

To create a basic message that contains only plain text, set the `text` property of the [Activity][Activity] object to the contents of the message and set the `locale` property to the locale of the sender. 

The `textFormat` property the [Activity][Activity] object can be used to specify the format of the text. The value of the `textFormat` property defaults to **markdown** and interprets text using Markdown formatting standards, 
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

The `attachments` property of the [Activity][Activity] object can be used to send simple media attachments 
(image, audio, video, file) and rich cards. For details, see [Add media attachments to messages](bot-framework-rest-connector-add-media-attachments.md) and [Add rich cards to messages](bot-framework-rest-connector-add-rich-cards.md).

## Entities

The `entities` property of the [Activity][Activity] object is an array of open-ended <a href="http://schema.org/" target="_blank">schema.org</a> objects that allows the exchange of common contextual metadata between the channel and bot.

### Mention entities

Many channels support the ability for a bot or user to "mention" someone within the context of a conversation. 
To mention a user in a message, populate the message's `entities` property with a [Mention][Mention] object. 

### Place entities

To convey <a href="https://schema.org/Place" target="_blank">location-related information</a> within a message, populate the message's `entities` property with [Place][Place] object. 

## Channel data

The `channelData` property of the [Activity][Activity] object can be used to implement channel-specific functionality. 
For details, see [Implement channel-specific functionality](bot-framework-rest-connector-channeldata.md).

## Text to speech

The `speak` property of the [Activity][Activity] object can be used to specify the text to be spoken by your bot on a speech-enabled channel and the `inputHint` property of the [Activity][Activity] object can be used to influence the state of the client's microphone. For details, see [Add speech to messages](bot-framework-rest-connector-text-to-speech.md) and [Add input hints to messages](bot-framework-rest-connector-add-input-hints.md).

## Suggested actions

The `suggestedActions` property of the [Activity][Activity] object can be used to present buttons that the user can tap to provide input. Unlike buttons that appear within rich cards (which remain visable and accessible to the user even after being tapped), buttons that appear within the suggested actions pane will disappear after the user makes a selection. For details, see [Add suggested actions to messages](bot-framework-rest-connector-add-suggested-actions.md).

## Additional resources

- [Activities overview](bot-framework-rest-connector-activities.md)
- [Send and receive messages](bot-framework-rest-connector-send-and-receive-messages.md)
- [Add media attachments to messages](bot-framework-rest-connector-add-media-attachments.md)
- [Add rich cards to messages](bot-framework-rest-connector-add-rich-cards.md)
- [Add speech to messages](bot-framework-rest-connector-text-to-speech.md)
- [Add input hints to messages](bot-framework-rest-connector-add-input-hints.md)
- [Add suggested actions to messages](bot-framework-rest-connector-add-suggested-actions.md)
- [Implement channel-specific functionality](bot-framework-rest-connector-channeldata.md)

[Mention]: bot-framework-rest-connector-api-reference.md#mention-object
[Place]: bot-framework-rest-connector-api-reference.md#place-object
[Activity]: bot-framework-rest-connector-api-reference.md#activity-object