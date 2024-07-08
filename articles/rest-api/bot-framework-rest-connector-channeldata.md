---
title: Implement channel-specific functionality using REST API - Bot Service
description: Learn how to implement channel-specific functionality using the Bot Connector API. 
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 09/01/2022
ms.custom:
  - evergreen
---

# Implement channel-specific functionality with the Bot Connector API

Some channels provide features that can't be implemented by using only [message text and attachments](bot-framework-rest-connector-create-messages.md). To implement channel-specific functionality, you can pass native metadata to a channel in the [Activity][] object's `channelData` property. For example, your bot can use the `channelData` property to instruct Telegram to send a sticker or to instruct Office365 to send an email.

[Activity]: bot-framework-rest-connector-api-reference.md#activity-object

This article describes how to use a message activity's `channelData` property to implement this channel-specific functionality:

| Channel  |                                 Functionality                                  |
| -------- | ------------------------------------------------------------------------------ |
| Email    | Send and receive an email that contains body, subject, and importance metadata |
| Slack    | Send full fidelity Slack messages                                              |
| Facebook | Send Facebook notifications natively                                           |
| Telegram | Perform Telegram-specific actions, such as sharing a voice memo or a sticker   |

> [!NOTE]
> The value of an `Activity` object's `channelData` property is a JSON object.
> The structure of the JSON object will vary according to the channel and the functionality being implemented, as described below.

## Create a custom email message

To create an email message, set the `Activity` object's `channelData` property to a JSON object that contains these properties:

[!INCLUDE [email channelData table](~/includes/snippet-channelData-email.md)]

This snippet shows an example of the `channelData` property for a custom email message.

```json
"channelData":
{
    "htmlBody": "<html><body style = \"font-family: Calibri; font-size: 11pt;\" >This is more than awesome.</body></html>",
    "importance": "high",
    "ccRecipients": "Yasemin@adatum.com;Temel@adventure-works.com"
}
```

## Create a full-fidelity Slack message

To create a full-fidelity Slack message, set the `Activity` object's `channelData` property to a JSON object that specifies
[Slack messages](https://api.slack.com/docs/messages), [Slack attachments](https://api.slack.com/docs/message-attachments), and/or [Slack buttons](https://api.slack.com/docs/message-buttons).

> [!NOTE]
> To support buttons in Slack messages, you must enable **Interactive Messages** when you
> [connect your bot](../bot-service-manage-channels.md) to the Slack channel.

This snippet shows an example of the `channelData` property for a custom Slack message.

```json
"channelData": {
   "text": "Now back in stock! :tada:",
   "attachments": [
        {
            "title": "The Further Adventures of Slackbot",
            "author_name": "Stanford S. Strickland",
            "author_icon": "https://api.slack.com/img/api/homepage_custom_integrations-2x.png",
            "image_url": "http://i.imgur.com/OJkaVOI.jpg?1"
        },
        {
            "fields": [
                {
                    "title": "Volume",
                    "value": "1",
                    "short": true
                },
                {
                    "title": "Issue",
                    "value": "3",
                    "short": true
                }
            ]
        },
        {
            "title": "Synopsis",
            "text": "After @episod pushed exciting changes to a devious new branch back in Issue 1, Slackbot notifies @don about an unexpected deploy..."
        },
        {
            "fallback": "Would you recommend it to customers?",
            "title": "Would you recommend it to customers?",
            "callback_id": "comic_1234_xyz",
            "color": "#3AA3E3",
            "attachment_type": "default",
            "actions": [
                {
                    "name": "recommend",
                    "text": "Recommend",
                    "type": "button",
                    "value": "recommend"
                },
                {
                    "name": "no",
                    "text": "No",
                    "type": "button",
                    "value": "bad"
                }
            ]
        }
    ]
}
```

When a user clicks a button within a Slack message, your bot will receive a response message in which the `channelData` property is populated with a `payload` JSON object. The `payload` object specifies contents of the original message,
identifies the button that was clicked, and identifies the user who clicked the button.

This snippet shows an example of the `channelData` property in the message that a bot receives when a user clicks a button in the Slack message.

```json
"channelData": {
    "payload": {
        "actions": [
            {
                "name": "recommend",
                "value": "yes"
            }
        ],
        //...
        "original_message": "{...}",
        "response_url": "https://hooks.slack.com/actions/..."
    }
}
```

Your bot can reply to this message in the [normal manner](bot-framework-rest-connector-send-and-receive-messages.md#create-reply), or it can post its response directly to the endpoint that is specified by the `payload` object's `response_url` property. For information about when and how to post a response to the `response_url`, see [Slack Buttons](https://api.slack.com/docs/message-buttons).

## Create a Facebook notification

To create a Facebook notification, set the `Activity` object's `channelData` property to a JSON object that specifies these properties:

| Property | Description |
|----|----|
| notification_type | The type of notification (such as, **REGULAR**, **SILENT_PUSH**, or **NO_PUSH**).
| attachment | An attachment that specifies an image, video, or other multimedia type, or a templated attachment such as a receipt. |

> [!NOTE]
> For details about format and contents of the `notification_type` property and `attachment` property, see the
> [Facebook API documentation](https://developers.facebook.com/docs/messenger-platform/send-api-reference#guidelines).

This snippet shows an example of the `channelData` property for a Facebook receipt attachment.

```json
"channelData": {
    "notification_type": "NO_PUSH",
    "attachment": {
        "type": "template",
        "payload": {
            "template_type": "receipt",
            //...
        }
    }
}
```

## Create a Telegram message

To create a message that implements Telegram-specific actions,
such as sharing a voice memo or a sticker,
set the `Activity` object's `channelData` property to a JSON object that specifies these properties:

| Property | Description |
|----|----|
| method | The Telegram Bot API method to call. |
| parameters | The parameters of the specified method. |

These Telegram methods are supported:

- answerInlineQuery
- editMessageCaption
- editMessageReplyMarkup
- editMessageText
- forwardMessage
- banChatMember
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
- unbanChatMember

For details about these Telegram methods and their parameters, see the [Telegram Bot API documentation](https://core.telegram.org/bots/api#available-methods).

> [!NOTE]
>
> - The `chat_id` parameter is common to all Telegram methods. If you don't specify `chat_id` as a parameter, the framework will provide the ID for you.
> - Instead of passing file contents inline, specify the file using a URL and media type as shown in the example below.
> - Within each message that your bot receives from the Telegram channel, the `channelData` property will include the message that your bot sent previously.

This snippet shows an example of a `channelData` property that specifies a single Telegram method.

```json
"channelData": {
    "method": "sendSticker",
    "parameters": {
        "sticker": {
            "url": "https://domain.com/path/gif",
            "mediaType": "image/gif",
        }
    }
}
```

This snippet shows an example of a `channelData` property that specifies an array of Telegram methods.

```json
"channelData": [
    {
        "method": "sendSticker",
        "parameters": {
            "sticker": {
                "url": "https://domain.com/path/gif",
                "mediaType": "image/gif",
            }
        }
    },
    {
        "method": "sendMessage",
        "parameters": {
            "text": "<b>This message is HTML formatted.</b>",
            "parse_mode": "HTML"
        }
    }
]
```

## Additional resources

- [Create messages](bot-framework-rest-connector-create-messages.md)
- [Send and receive messages](bot-framework-rest-connector-send-and-receive-messages.md)
- [Bot Framework Activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md)
- [Channels reference](../bot-service-channels-reference.md)
