---
title: Create messages with the Bot Connector API  - Bot Service
description: Become familiar with the messages that bots use to communicate with users. Learn about properties used to format text, attach files, and specify other behavior.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/13/2017
---

# Create messages with the Bot Connector API

Your bot will send [Activity][] objects of type **message** to communicate information to users, and likewise, will also receive **message** activities from users. Some messages may simply consist of plain text, while others may contain richer content such as [text to be spoken](bot-framework-rest-connector-text-to-speech.md), [suggested actions](bot-framework-rest-connector-add-suggested-actions.md), [media attachments](bot-framework-rest-connector-add-media-attachments.md), [rich cards](bot-framework-rest-connector-add-rich-cards.md), and [channel-specific data](bot-framework-rest-connector-channeldata.md). This article describes some of the commonly-used message properties.

## Message text and formatting

Message text can be formatted using **plain**, **markdown**, or **xml**. The default format for the `textFormat` property is **markdown** and interprets text using Markdown formatting standards. The level of text format support varies across channels. 

[!INCLUDE [Channel Inspector intro](~/includes/snippet-channel-inspector.md)]

The `textFormat` property of the [Activity][] object can be used to specify the format of the text. For example, to create a basic message that contains only plain text, set the `textFormat` property of the `Activity` object to **plain**, set the `text` property to the contents of the message and set the `locale` property to the locale of the sender.

## Attachments

The `attachments` property of the [Activity][] object can be used to send simple media attachments (image, audio, video, file) and rich cards. For details, see [Add media attachments to messages](bot-framework-rest-connector-add-media-attachments.md) and [Add rich cards to messages](bot-framework-rest-connector-add-rich-cards.md).

## Entities

The `entities` property of the [Activity][] object is an array of open-ended [schema.org](https://schema.org/) objects that allows the exchange of common contextual metadata between the channel and bot.

### Mention entities

Many channels support the ability for a bot or user to "mention" someone within the context of a conversation. To mention a user in a message, populate the message's `entities` property with a [Mention][] object.

### Place entities

To convey location-related information within a message, populate the message's`entities` property with [Place][] object.

## Channel data

The `channelData` property of the [Activity][] object can be used to implement channel-specific functionality. For details, see [Implement channel-specific functionality](bot-framework-rest-connector-channeldata.md).

## Text to speech

The `speak` property of the [Activity][] object can be used to specify the text to be spoken by your bot on a speech-enabled channel and the `inputHint` property of the `Activity` object can be used to influence the state of the client's microphone. For details, see [Add speech to messages](bot-framework-rest-connector-text-to-speech.md) and [Add input hints to messages](bot-framework-rest-connector-add-input-hints.md).

## Suggested actions

The `suggestedActions` property of the [Activity][] object can be used to present buttons that the user can tap to provide input. Unlike buttons that appear within rich cards (which remain visible and accessible to the user even after being tapped), buttons that appear within the suggested actions pane will disappear after the user makes a selection. For details, see [Add suggested actions to messages](bot-framework-rest-connector-add-suggested-actions.md).

## Additional resources

- [Channels reference][ChannelInspector]
- [Activities overview](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md)
- [Send and receive messages](bot-framework-rest-connector-send-and-receive-messages.md)
- [Add media attachments to messages](bot-framework-rest-connector-add-media-attachments.md)
- [Add rich cards to messages](bot-framework-rest-connector-add-rich-cards.md)
- [Add speech to messages](bot-framework-rest-connector-text-to-speech.md)
- [Add input hints to messages](bot-framework-rest-connector-add-input-hints.md)
- [Add suggested actions to messages](bot-framework-rest-connector-add-suggested-actions.md)
- [Implement channel-specific functionality](bot-framework-rest-connector-channeldata.md)

[ChannelInspector]: ../bot-service-channels-reference.md
[textFormating]: ../bot-service-channel-inspector.md#text-formatting

[Activity]: bot-framework-rest-connector-api-reference.md#activity-object
[Mention]: bot-framework-rest-connector-api-reference.md#mention-object
[Place]: bot-framework-rest-connector-api-reference.md#place-object
