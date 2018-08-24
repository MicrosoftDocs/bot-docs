---
title: Send messages | Microsoft Docs
description: Learn about how to send messages within the Bot Builder SDK.
keywords: sending messages, message activities, simple text message, speech, spoken message  
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 04/04/2018
monikerRange: 'azure-bot-service-4.0'
---

# Send messages

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

The primary way your bot will communicate with users, and likewise receive communication, is through **message** activities. Some messages may simply consist of plain text, while others may contain richer content such as cards or attachments. Your bot's turn handler receives messages from the user, and you can send responses to the user from there. The turn context object provides methods for sending messages back to the user. For more information about activity processing in general, see [Activity processing](bot-builder-concept-activity-processing.md).

This article describes how to send simple text and speech messages. For sending richer content, see how to [add rich media attachments](bot-builder-howto-add-media-attachments.md). For information on how to use prompt objects, see how to [prompt users for input](bot-builder-prompts.md).

## Send a simple text message

To send a simple text message, specify the string you want to send as the activity:

# [C#](#tab/csharp)

In the bot's
**OnTurnAsync** <!-- <xref:microsoft.bot.ibot.onturnasync*> -->
method, use the turn context object's
**SendActivityAsync** <!-- <xref:microsoft.bot.builder.iturncontext.sendactivityasync*> -->
method to send a single message response. You can also use the object's
**SendActivitiesAsync** <!-- <xref:microsoft.bot.builder.iturncontext.sendactivitiesasync*> -->
method to send multiple responses at once.

```cs
await context.SendActivityAsync("Greetings from sample message.", cancellationToken: token);
```

# [JavaScript](#tab/javascript)

In the bot's turn handler, use the turn context object's **sendActivity** method to send a single message response. You can also use the object's **sendActivities** method to send multiple responses at once.

```javascript
await context.sendActivity("Greetings from sample message.");
```

---

## Send a spoken message

Certain channels support speech-enabled bots, allowing them to speak to the user. A message can have both written and spoken content.

> [!NOTE]
> For those channels that don't support speech, the speech content is ignored.

# [C#](#tab/csharp)

Use the optional **speak** parameter to provide text to be spoken as part of the response.

```cs
await context.SendActivityAsync(
    "This is text to display.",
    speak: "This is text to speak.",
    cancellationToken: token);
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
