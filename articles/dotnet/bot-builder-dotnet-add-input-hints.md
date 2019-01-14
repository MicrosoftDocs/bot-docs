---
title: Add input hints to messages | Microsoft Docs
description: Learn how to add input hints to messages using the Bot Framework SDK for .NET.
author: RobStand
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

To indicate that your bot is passively ready for input but is not awaiting a response from the user, set the message's input hint to `InputHints.AcceptingInput`. On many channels, this will cause the client's input box to be enabled and microphone to be closed, but still accessible to the user. For example, Cortana will open the microphone to accept input from the user if the user holds down the microphone button. The following code example creates a message that indicates the bot is accepting user input.

```cs
Activity reply = activity.CreateReply("This is the text that will be displayed.");
reply.Speak = "This is the text that will be spoken.";
reply.InputHint = InputHints.AcceptingInput;
await connector.Conversations.ReplyToActivityAsync(reply);
```

## Expecting input

To indicate that your bot is awaiting a response from the user, set the message's input hint to `InputHints.ExpectingInput`. On many channels, this will cause the client's input box to be enabled and microphone to be open. The following code example creates a message that indicates the bot is expecting user input.

```cs
Activity reply = activity.CreateReply("This is the text that will be displayed.");
reply.Speak = "This is the text that will be spoken.";
reply.InputHint = InputHints.ExpectingInput;
await connector.Conversations.ReplyToActivityAsync(reply);
```

## Ignoring input

To indicate that your bot is not ready to receive input from the user, set the message's input hint to `InputHints.IgnorningInput`. On many channels, this will cause the client's input box to be disabled and microphone to be closed. The following code example creates a message that indicates the bot is ignoring user input.

```cs
Activity reply = activity.CreateReply("This is the text that will be displayed.");
reply.Speak = "This is the text that will be spoken.";
reply.InputHint = InputHints.IgnoringInput;
await connector.Conversations.ReplyToActivityAsync(reply);
```

## Default values for input hint

If you do not set the input hint for a message, the Bot Framework SDK will automatically set it for you by using this logic:

- If your bot sends a prompt, the input hint for the message will specify that your bot is **expecting input**.</li>
- If your bot sends single message, the input hint for the message will specify that your bot is **accepting input**.</li>
- If your bot sends a series of consecutive messages, the input hint for all but the final message in the series will specify that your bot is **ignoring input**, and the input hint for the final message in the series will specify that your bot is **accepting input**.

## Additional resources

- [Create messages](bot-builder-dotnet-create-messages.md)
- [Add speech to messages](bot-builder-dotnet-text-to-speech.md)
- <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/dc/d2f/class_microsoft_1_1_bot_1_1_connector_1_1_activity.html" target="_blank">Activity class</a>
- <a href="/dotnet/api/microsoft.bot.connector.inputhints" target="_blank">InputHints class</a>
