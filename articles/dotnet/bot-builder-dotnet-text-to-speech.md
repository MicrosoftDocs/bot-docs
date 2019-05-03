---
title: Add speech to messages | Microsoft Docs
description: Learn how to add speech to messages using the Bot Framework SDK for .NET.
author: RobStand
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

If you are building a bot for a speech-enabled channel such as Cortana, you can construct messages that specify the text to be spoken by your bot. You can also attempt to influence the state of the client's microphone by specifying an [input hint](bot-builder-dotnet-add-input-hints.md) to indicate whether your bot is accepting, expecting, or ignoring user input.

## Specify text to be spoken by your bot

Using the Bot Framework SDK for .NET, there are multiple ways to specify the text to be spoken by your bot on a speech-enabled channel. You can set the `Speak` property of the [message][IMessageActivity], call the `IDialogContext.SayAsync()` method, or specify prompt options `speak` and `retrySpeak` when sending a message using a built-in prompt.

### <a id="message-speak"></a> IMessageActivity.Speak

If you are creating a [message][IMessageActivity] and setting its individual properties, you can set the `Speak` property of the message to specify the text to be spoken by your bot. The following code example creates a message that specifies text to be displayed and text to be spoken and indicates that the bot is [accepting user input](bot-builder-dotnet-add-input-hints.md).

[!code-csharp[Set speak property](../includes/code/dotnet-text-to-speech.cs#Speak1)]

### <a id="say-async"></a> IDialogContext.SayAsync()

If you are using [dialogs](bot-builder-dotnet-dialogs.md), you can call the `SayAsync()` method to create and send a message that specifies the text to be spoken, in addition to the text to be displayed and other options. The following code example creates a message that specifies text to be displayed and text to be spoken.

[!code-csharp[Call SayAsync()](../includes/code/dotnet-text-to-speech.cs#Speak2)]

### <a id="prompt-options"></a> Prompt options

Using any of the built-in prompts, you can set the options `speak` and `retrySpeak` to specify the text to be spoken by your bot. The following code example creates a prompt that specifies text to be displayed, text to be spoken initially, and text to be spoken after waiting a while for user input. It uses [SSML](#ssml) formatting to indicate that the word "sure" should be spoken with a moderate amount of emphasis.

[!code-csharp[Set Prompt options](../includes/code/dotnet-text-to-speech.cs#Speak3)]

## <a id="ssml"></a> Speech Synthesis Markup Language (SSML)

To specify text to be spoken by your bot, you can give it a string that is formatted as Speech Synthesis Markup Language (SSML). SSML is an XML-based markup language (and therefore must be valid XML) that enables you to control various characteristics of your bot's speech such as voice, rate, volume, pronunciation, pitch, and more. For details about SSML, see <a href="https://msdn.microsoft.com/en-us/library/hh378377(v=office.14).aspx" target="_blank">Speech Synthesis Markup Language Reference</a>.

When providing the SSML formatted string, the outer SSML wrapper element may be omitted.

## Input hints

When you send a message on a speech-enabled channel, you can attempt to influence the state of the client's microphone by also including an input hint to indicate whether your bot is accepting, expecting, or ignoring user input. For more information, see [Add input hints to messages](bot-builder-dotnet-add-input-hints.md).

## Sample code 

For a complete sample that shows how to create a speech-enabled bot using the Bot Framework SDK for .NET, see the <a href="https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/CSharp" target="_blank">Roller Skill sample</a> in GitHub.

## Additional resources

- [Create messages](bot-builder-dotnet-create-messages.md)
- [Add input hints to messages](bot-builder-dotnet-add-input-hints.md)
- <a href="https://msdn.microsoft.com/en-us/library/hh378377(v=office.14).aspx" target="_blank">Speech Synthesis Markup Language (SSML)</a>
- <a href="https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/CSharp/demo-RollerSkill" target="_blank">Roller Skill sample (GitHub)</a>
- <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/dc/d2f/class_microsoft_1_1_bot_1_1_connector_1_1_activity.html" target="_blank">Activity class</a>
- <a href="/dotnet/api/microsoft.bot.connector.imessageactivity" target="_blank">IMessageActivity interface</a>
- <a href="/dotnet/api/microsoft.bot.builder.dialogs.internals.dialogcontext" target="_blank">DialogContext class</a>
- <a href="/dotnet/api/microsoft.bot.builder.dialogs.internals.prompt-2" target="_blank">Prompt class</a>

[IMessageActivity]: /dotnet/api/microsoft.bot.connector.imessageactivity

