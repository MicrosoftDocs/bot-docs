---
title: Add speech to messages - Bot Service
description: Learn how to add speech to bot messages. See how to specify and format the text that bots use on speech-enabled channels and how to include input hints.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 02/20/2020
---

# Add speech to messages with the Bot Connector API

If you are building a bot for a speech-enabled channel, you can construct messages that specify the text to be spoken by your bot. You can also attempt to influence the state of the client's microphone by specifying an [input hint](bot-framework-rest-connector-add-input-hints.md) to indicate whether your bot is accepting, expecting, or ignoring user input.

You can configure your bot to allow client applications to communicate with it through [Direct Line Speech channel](../bot-service-channel-connect-directlinespeech.md).

## Specify text to be spoken by your bot

To specify text to be spoken by your bot on a speech-enabled channel, set the `speak` property within the [Activity][Activity] object that represents your message. You can set the `speak` property to either a plain text string or a string that is formatted as [Speech Synthesis Markup Language (SSML)](/azure/cognitive-services/speech-service/speech-synthesis-markup), an XML-based markup language that enables you to control various characteristics of your bot's speech such as voice, rate, volume, pronunciation, pitch, and more. If the channel does not support, the message is delivered as text.


The following request sends a message that specifies text to be displayed and text to be spoken and indicates that the bot is [expecting user input](bot-framework-rest-connector-add-input-hints.md). It specifies the `speak` property using  [SSML](/azure/cognitive-services/speech-service/speech-synthesis-markup) format to indicate that the word "sure" should be spoken with a moderate amount of emphasis. In this example request, Direct Line represents the base URI; the base URI for requests that your bot issues may be different. For details about setting the base URI, see [API Reference](bot-framework-rest-connector-api-reference.md#base-uri).

```http
POST https://smba.trafficmanager.net/apis/v3/conversations/abcd1234/activities/5d5cdc723
Authorization: Bearer ACCESS_TOKEN
Content-Type: application/json
```

```json
{
    "type": "message",
    "from": {
        "id": "12345678",
        "name": "sender's name"
    },
    "conversation": {
        "id": "abcd1234",
        "name": "conversation's name"
   },
   "recipient": {
        "id": "1234abcd",
        "name": "recipient's name"
    },
    "text": "Are you sure that you want to cancel this transaction?",
    "speak": "<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"en-US\">Are you <emphasis level=\"moderate\">sure</emphasis> that you want to cancel this transaction?</speak>",
    "inputHint": "expectingInput",
    "replyToId": "5d5cdc723"
}
```

## Input hints

When you send a message on a speech-enabled channel, you can express the intended state of the client's microphone by also including an input hint to indicate whether your bot is accepting, expecting, or ignoring user input. For more information, see [Add input hints to messages](bot-framework-rest-connector-add-input-hints.md).

## Additional resources

- [Create messages](bot-framework-rest-connector-create-messages.md)
- [Send and receive messages](bot-framework-rest-connector-send-and-receive-messages.md)
- [Add input hints to messages](bot-framework-rest-connector-add-input-hints.md)
- [Speech Synthesis Markup Language (SSML)](/azure/cognitive-services/speech-service/speech-synthesis-markup)

[Activity]: bot-framework-rest-connector-api-reference.md#activity-object
