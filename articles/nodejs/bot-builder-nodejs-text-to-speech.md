---
title: Add speech to messages | Microsoft Docs
description: Learn how to add speech to messages using the Bot Framework SDK for Node.js.
author: v-ducvo
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Add speech to messages

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-text-to-speech.md)
> - [Node.js](../nodejs/bot-builder-nodejs-text-to-speech.md)
> - [REST](../rest-api/bot-framework-rest-connector-text-to-speech.md)

If you are building a bot for a speech-enabled channel such as Cortana, you can construct messages that specify the text to be spoken by your bot. You can also attempt to influence the state of the client's microphone by specifying an [input hint](bot-builder-nodejs-send-input-hints.md) to indicate whether your bot is accepting, expecting, or ignoring user input.

## Specify text to be spoken by your bot

Using the Bot Framework SDK for Node.js, there are multiple ways to specify the text to be spoken by your bot on a speech-enabled channel. You can set the `IMessage.speak` property and send the message using the `session.send()` method, send the message using the `session.say()` method (passing parameters that specify display text, speech text, and options), or send the message using a built-in prompt (specifying options `speak` and `retrySpeak`).

### <a id="message-speak"></a> IMessage.speak

If you are creating a message that will be sent using the `session.send()` method, set the `speak` property to specify the text to be spoken by your bot. The following code example creates a message that specifies text to be spoken and indicates that the bot is [accepting user input](bot-builder-nodejs-send-input-hints.md).

[!code-javascript[IMessage.speak](../includes/code/node-text-to-speech.js#IMessageSpeak)]

### <a id="session-say"></a> session.say()

As an alternative to using `session.send()`, you can call the `session.say()` method to create and send a message that specifies the text to be spoken, in addition to the text to be displayed and other options. The method is defined as follows:

`session.say(displayText: string, speechText: string, options?: object)`

| Parameter | Description |
|----|----|
| `displayText` | The text to be displayed. |
| `speechText` | The text (in plain text or <a href="https://msdn.microsoft.com/en-us/library/hh378377(v=office.14).aspx" target="_blank">SSML</a> format) to be spoken. |
| `options` | An `IMessage` object that can contain an attachment or [input hint](bot-builder-nodejs-send-input-hints.md). |

The following code example sends a message that specifies text to be displayed and text to be spoken and indicates that the bot is [ignoring user input](bot-builder-nodejs-send-input-hints.md).

[!code-javascript[Session.say()](../includes/code/node-text-to-speech.js#SessionSay)]

### <a id="prompt-options"></a> Prompt options

Using any of the built-in prompts, you can set the options `speak` and `retrySpeak` to specify the text to be spoken by your bot. The following code example creates a prompt that specifies text to be displayed, text to be spoken initially, and text to be spoken after waiting a while for user input. It indicates that the bot is [expecting user input](bot-builder-nodejs-send-input-hints.md) and uses [SSML](#ssml) formatting to specify that the word "sure" should be spoken with a moderate amount of emphasis.

[!code-javascript[Prompt](../includes/code/node-text-to-speech.js#Prompt)]

## <a id="ssml"></a> Speech Synthesis Markup Language (SSML)

To specify text to be spoken by your bot, you can use either a plain text string or a string that is formatted as Speech Synthesis Markup Language (SSML), an XML-based markup language that enables you to control various characteristics of your bot's speech such as voice, rate, volume, pronunciation, pitch, and more. For details about SSML, see <a href="https://msdn.microsoft.com/en-us/library/hh378377(v=office.14).aspx" target="_blank">Speech Synthesis Markup Language Reference</a>.

> [!TIP]
> Use an <a href="https://www.npmjs.com/search?q=ssml" target="_blank">SSML library</a> to create well-formatted SSML.

## Input hints

When you send a message on a speech-enabled channel, you can attempt to influence the state of the client's microphone by also including an input hint to indicate whether your bot is accepting, expecting, or ignoring user input. For more information, see [Add input hints to messages](bot-builder-nodejs-send-input-hints.md).

## Sample code 

For a complete sample that shows how to create a speech-enabled bot using the Bot Framework SDK for .NET, see the <a href="https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/demo-RollerSkill" target="_blank">Roller sample</a> in GitHub.

## Additional resources

- <a href="https://msdn.microsoft.com/en-us/library/hh378377(v=office.14).aspx" target="_blank">Speech Synthesis Markup Language (SSML)</a>
- <a href="https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/demo-RollerSkill" target="_blank">Roller sample (GitHub)</a>
