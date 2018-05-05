---
title: Add input hints to messages | Microsoft Docs
description: Learn how to add input hints to messages using the Bot Builder SDK.
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 04/04/2018
monikerRange: 'azure-bot-service-4.0'
---

# Add input hints to messages

By specifying an input hint for a message, you can indicate whether your bot is accepting, expecting, or ignoring user input after the message is delivered to the client. For many channels, this enables clients to set the state of user input controls accordingly. For example, if a message's input hint indicates that the bot is ignoring user input, the client may close the microphone and disable the input box to prevent the user from providing input.

Be sure the necessary libraries are included for input hints.

# [C#](#tab/cslibraries)
```cs
using Microsoft.Bot.Schema;
```

# [JavaScript](#tab/jslibraries)
```javascript
const {InputHints} = require('botbuilder');
```
---

## Accepting input

To indicate that your bot is passively ready for input but is not awaiting a response from the user, set the message's input hint to _accepting input_. On many channels, this will cause the client's input box to be enabled and microphone to be closed, but still accessible to the user. For example, Cortana will open the microphone to accept input from the user if the user holds down the microphone button. The following code creates a message that indicates the bot is accepting user input.

# [C#](#tab/csacceptinginput)
[!code-csharp[Accepting input](../includes/code/dotnet-input-hints.cs#InputHintAcceptingInput)]

# [JavaScript](#tab/jsacceptinginput)
```javascript
const basicMessage = MessageFactory.text('This is the text that will be displayed.', 'This is the text that will be spoken.', InputHints.ExpectingInput);
await context.sendActivity(basicMessage);
```
---

## Expecting input

To indicate that your bot is awaiting a response from the user, set the message's input hint to _expecting input_. On many channels, this will cause the client's input box to be enabled and microphone to be open. The following code example creates a message that indicates the bot is expecting user input.

# [C#](#tab/csexpectinginput)
[!code-csharp[Expecting input](../includes/code/dotnet-input-hints.cs#InputHintExpectingInput)]

# [JavaScript](#tab/jsexpectinginput)
```javascript
const basicMessage = MessageFactory.text('This is the text that will be displayed.', 'This is the text that will be spoken.', InputHints.ExpectingInput);
await context.sendActivity(basicMessage);
```
---

## Ignoring input
 
To indicate that your bot is not ready to receive input from the user, set the message's input hint to _ignorning input_. On many channels, this will cause the client's input box to be disabled and microphone to be closed. The following code example creates a message that indicates the bot is ignoring user input.

# [C#](#tab/csignoringinput)
[!code-csharp[Ignoring input](../includes/code/dotnet-input-hints.cs#InputHintIgnoringInput)]

# [JavaScript](#tab/jsignoringinput)
```javascript
const basicMessage = MessageFactory.text('This is the text that will be displayed.', 'This is the text that will be spoken.', InputHints.IgnoringInput);
await context.sendActivity(basicMessage);
```
---

## Default values for input hint

If you do not set the input hint for a message, the Bot Builder SDK will automatically set it for you by using this logic: 

- If your bot sends a prompt, the input hint for the message will specify that your bot is **expecting input**.</li>
- If your bot sends single message, the input hint for the message will specify that your bot is **accepting input**.</li>
- If your bot sends a series of consecutive messages, the input hint for all but the final message in the series will specify that your bot is **ignoring input**, and the input hint for the final message in the series will specify that your bot is **accepting input**.
