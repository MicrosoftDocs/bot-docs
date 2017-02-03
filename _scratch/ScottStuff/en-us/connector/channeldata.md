---
layout: page
title: Adding Channel-specific Data
permalink: /en-us/connector/channeldata/
weight: 3060
parent1: Building your Bot Using the Bot Connector REST API
---

Some channels provide features that can't be described by using Bot Connector's text and attachment schema. To use some channels, or to use channel-specific features, you need to set the [message](../reference/#activity)'s **channelData** property to an object that contains the channel-specific data. For example, to use the Office 365 email channel, you'd set the **channelData** property to the email's body and subject. For all requirements about exchanging messages, and a description of the channel-specific data, see the channel's documentation.

The following list contains some of the channels where you'd use **channelData** to provide channel-specific attachments.

|**Channel**|**Feature**
|[Email](#email)|Use **channelData** to send and receive an email's body, subject, and importance metadata. 
|[Slack](#slack)|Use **channelData** to send full fidelity Slack messages.
|[Facebook](#facebook)|Use **channelData** to send Facebook notifications natively.
|[Telegram](#telegram)|Use **channelData** to  perform Telegram-specific actions, such as sharing a voice memo or a sticker. 
|[Kik](#kik)|Use **channelData** to send and receive native Kik messages. 

For information about adding attachments, see [Adding Attachments to a Message](../attachments).

<a id="email" />

### Microsoft Office 365 Email

<span style="color:red"><< Doc says, "optionally supports the ability to send custom properties", so you can send email without using channelData? >></span>

To send an email message, create an object with the following properties and set the **channelData** property to it. Messages that you receive will also contain an object with the same properties.

<span style="color:red"><< Are these the only email properties that you can set? >></span>

|**Property**|**Description**
|htmlBody|An HTML document that contains the body of the email message. For supported HTML elements and attributes, see the channel's documentation.
|importance|The email's importance level. The options are **high**, **normal**, and **low**. The default is **Normal**.
|subject|The email's subject. For limits, see the channel's documentation.

The following shows a simple email message that the bot sends to the user.

```cmd
{
    "type": "message",
    "from": {
        "id": "mybot@gmail.com",
        "name": "My bot"
    },
    "conversation": {
        "id": "abcd1234",
        "name": "conversation's name"
    },
    "recipient": {
        "id": "john@gmail.com",
        "name": "John Doe"
    },
    "channelData": {
       "htmlBody" : "<html><body style=\"font-family: Calibri; font-size: 11pt;\">email message goes here</body></html>",
       "subject":"email subject goes here",
       "importance":"high"
    },
    "replyToId": "5d5cdc723"
}
```

<a id="slack" />

### Slack

<span style="color:red"><< Do you have to use channelData to send a slack message? The doc talks about using channelData to send custom slack messages, so what can they send using Activity and what can they do if the use channelData? >></span>

To send a slack message, use slack messages and attachments. For details about the format of the Slack messages that you set **channelData** to, see [Slack Messages](https://api.slack.com/docs/messages), [Slack Attachments](https://api.slack.com/docs/message-attachments), and [Slack Buttons](https://api.slack.com/docs/message-buttons). To support action buttons, when you configure your bot for the Slack channel, you must follow the instructions to enable Interactive Messages.

If the Slack message contains action buttons, and a user clicks one of the buttons, the response message that Slack sends to your bot will contain a payload object that contains the Slack message. The following shows a snippet of an example response that contains the payload. 

<span style="color:red"><< The Slack docs don't show a "payload" object, so why do we include it? >></span>

<span style="color:red"><< The example in the doc doesn't look like the example in the Slack doc - ours shows the original message inline while the Slack doc show it in an "original_message" >></span>


```cmd
    "channelData": {
        "payload": {
            "actions": [
                {
                    "name": "recommend",
                    "value": "yes"
                }
            ],
            . . .
            "original_message": "{…}"
            "response_url": "https://hooks.slack.com/actions/T47... "
        }
    }
```

You can reply to this message normally or you can use the URL in the **response_url** property to reply. For information about when you would use each reply option, see [Slack Buttons](https://api.slack.com/docs/message-buttons).  

<span style="color:red"><< The slack docs talk about verifying the token to ensure that it's coming from slack. Is the connector doing that or do they still have to do it? >></span>

<a id="facebook" />

### Facebook

To send Facebook notifications natively, set the message's **channelData** property to an object that contains the following properties: 

|**Property**|**Description**
|notification_type|The type of notification. For example, REGULAR (emits a sound/vibration and a phone notification), SILENT_PUSH (emits a phone notification), NO_PUSH (does not emit either). The default is REGULAR.
|attachment|The attachment that contains the image, video, or other multimedia types, or a templated attachment such as a receipt.

For details about these properties, see the [Facebook Send API Reference](https://developers.facebook.com/docs/messenger-platform/send-api-reference#guidelines). Note that the **attachment** property is defined as a property of Facebook's **message** object; do not include **message** object as shown in the Facebook documentation.

The following shows a snippet of the channel data that contains a receipt attachment.

```cmd
    "channelData": {
        "notification_type": "NO_PUSH",
        "attachment": {
            "type": "template"
            "payload": {
                "template_type": "receipt",
                . . .
            }
        }
    }
```

<span style="color:red"><< Why don't we just set channelData to the same object that Facebook uses, so we can just point them to the facebook doc and not have to mention anything about "message"? 
  "message":{
    "attachment":{
      "type":"image",
      "payload":{
        "attachment_id":"1745504518999123"
      }
    }
  }
>></span>

<<span style="color:red">< Do bots have to use channelData to send facebook messages? >></span>

<span style="color:red"><< If we can include images, videos, audio, etc., why do they need to use facebook's attachments? >></span>

<span style="color:red"><< Can't bots use our cards instead of using facebook's generic template or button template? >></span> 


<a id="telegram" />

### Telegram

To send Telegram-specific actions, such as sharing a voice memo or a sticker, set the message's **channelData** property to an object that contains the following properties:

|**Property**|**Description**
|method|The Telegram Bot API method to call.
|parameters|The parameters of the specified method.

The following are the methods that you may specify. For details about these methods, and their parameters and types, see [Telegram Bot API](https://core.telegram.org/bots/api#available-methods).

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


Notes:

- The **chat_id** parameter is common to all Telegram methods. If you do not provide it, the framework will provide the ID for you.
- Instead of passing file contents inline, specify the file using a URL and media type as shown in the example below.
- For messages that you receive from the Telegram channel, the **channelData** property will include the previous message that you sent.

The following shows a snippet of the channel data that contains a single method.

```cmd
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

The following shows a snippet of the channel data that contains an array of methods.

```cmd
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


<a id="kik" />

### Kik

To send Kik messages natively, set the message's **channelData** property to an object that contains the following properties:

|**Property**|**Description**
|messages|An array of Kik messages. For details about Kik messages that you send and receive through **channelData**, see [Kik Message Formats](https://dev.kik.com/#/docs/messaging#message-formats).


The following shows a snippet of the channel data that contains a Kik message.

```cmd
    "channelData": {
        "messages": [
            {
                "chatId": "c6dd8165…",
                "type": "link",
                "to": "kikhandle",
                "title": "My Webpage",
                "text": "Some text to display",
                "url": "http://botframework.com",
                "picUrl": "http://lorempixel.com/400/200/",
                "attribution": {
                    "name": "My App",
                    "iconUrl": "http://lorempixel.com/50/50/"
                },
                "noForward": true,
         "kikJsData": {
                    "key": "value"
                }
            }
        ]
    }
```

