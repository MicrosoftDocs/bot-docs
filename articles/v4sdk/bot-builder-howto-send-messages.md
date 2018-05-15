---
title: Sending messages with the Bot Builder SDK | Microsoft Docs
description: Learn about how to send messages within the Bot Builder SDK.
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 04/04/2018
monikerRange: 'azure-bot-service-4.0'
---

# Sending messages

The primary way your bot will communicate with users, and likewise receive communication, is through **message** activities. Some messages may simply consist of plain text, while others may contain richer content such as cards or attachments.

This article describes how to send simple text and speech messages, for sending richer content, see how to [add rich media attachments](bot-builder-howto-add-media-attachments.md).

## Send a simple text message

To send a simple text message, specify the string you want to send as the activity:

# [C#](#tab/csharp)
```cs
await context.SendActivity("Greetings from sample message");
```

# [JavaScript](#tab/javascript)
```javascript
await context.sendActivity("Greetings from sample message");
```
---

## Send a spoken message

Certain channels support a speech enabled bot, allowing it to speak to the user. A message has the ability to have both written text to be displayed, and text to be spoken.

> [!NOTE] 
> For those channels that don't support speech, the speech argument will be ignored.

# [C#](#tab/csharp)

For .NET, you can specify text to be spoken either in an individual activity message, or through a prompt when using the built in [prompts](bot-builder-prompts.md).

To add speech to a single activity message, specify the `Activity.Speak` property.

```cs
Activity reply = new Activity();
reply.Text = "This is text that will be displayed.";
reply.Speak = "This will be spoken.";
await context.SendActivity(reply);
```

When including it in a prompt, specify it as an optional parameter when sending the prompt. For more on prompts, see how to [prompt users for input](bot-builder-prompts.md)

```cs
TextPrompt textPrompt = new TextPrompt();
await textPrompt.Prompt(context, "Text to be displayed.", "Text to be spoken.");
```

# [JavaScript](#tab/javascript)

To add speech, you'll need the `Microsoft.Bot.Builder.MessageFactory` to build the message. `MessageFactory` is used more with [rich media](bot-builder-howto-add-media-attachments.md), where it is explained a bit more, but for now we'll just require and use it here.

```javascript
// Require MessageFactory from botbuilder
const {MessageFactory} = require('botbuilder');

const basicMessage = MessageFactory.text('This is the text that will be displayed.', 'This is the text that will be spoken.');
await context.sendActivity(basicMessage);
```
---
