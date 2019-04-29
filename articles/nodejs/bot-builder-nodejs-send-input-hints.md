---
title: Add input hints to messages | Microsoft Docs
description: Learn how to add input hints to messages using the Bot Framework SDK for .NET.
author: v-ducvo
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Add input hints to messages

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-add-input-hints.md)
> - [Node.js](../nodejs/bot-builder-nodejs-send-input-hints.md)
> - [REST](../rest-api/bot-framework-rest-connector-add-input-hints.md)

By specifying an input hint for a message, you can indicate whether your bot is accepting, expecting, or ignoring user input after the message is delivered to the client. For many channels, this enables clients to set the state of user input controls accordingly. For example, if a message's input hint indicates that the bot is ignoring user input, the client may close the microphone and disable the input box to prevent the user from providing input.

## Accepting input

To indicate that your bot is passively ready for input but is not awaiting a response from the user, set the message's input hint to `builder.InputHint.acceptingInput`. On many channels, this will cause the client's input box to be enabled and microphone to be closed, but still accessible to the user. For example, Cortana will open the microphone to accept input from the user if the user holds down the microphone button. The following code example creates a message that indicates the bot is accepting user input.

[!code-javascript[IMessage.speak](../includes/code/node-input-hints.js#InputHintAcceptingInput)]

## Expecting input

To indicate that your bot is awaiting a response from the user, set the message's input hint to `builder.InputHint.expectingInput`. On many channels, this will cause the client's input box to be enabled and microphone to be open. The following code example creates a prompt that indicates the bot is expecting user input.

[!code-javascript[Prompt](../includes/code/node-input-hints.js#InputHintExpectingInput)]

## Ignoring input

To indicate that your bot is not ready to receive input from the user, set the message's input hint to `builder.InputHint.ignoringInput`. 
On many channels, this will cause the client's input box to be disabled and microphone to be closed. The following code example uses the `session.say()` method to send a message that indicates the bot is ignoring user input.

[!code-javascript[Session.say()](../includes/code/node-input-hints.js#InputHintIgnoringInput)]

## Default values for input hint

If you do not set the input hint for a message, the Bot Framework SDK will automatically set it for you by using this logic: 

- If your bot sends a prompt, the input hint for the message will specify that your bot is **expecting input**.</li>
- If your bot sends single message, the input hint for the message will specify that your bot is **accepting input**.</li>
- If your bot sends a series of consecutive messages, the input hint for all but the final message in the series will specify that your bot is **ignoring input**, and the input hint for the final message in the series will specify that your bot is **accepting input**.

## Additional resources

- [Add speech to messages](bot-builder-nodejs-text-to-speech.md)
