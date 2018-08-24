---
title: Add suggested actions to messages | Microsoft Docs
description: Learn how to send suggested actions within messages using the Bot Builder SDK for JavaScript.
keywords: suggested actions, buttons, extra input
author: Kaiqb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date:  03/13/2018
monikerRange: 'azure-bot-service-4.0'
---

# Add suggested actions to messages

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

[!include[Introduction to suggested actions](../includes/snippet-suggested-actions-intro.md)] 

## Send suggested actions

You can create a list of suggested actions (also known as "quick replies") that will be shown to the user for a single turn of the conversation:

# [C#](#tab/csharp)

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
```

```csharp
// Create the activity and add suggested actions.
IMessageActivity activity = MessageFactory.SuggestedActions(
    actions: new string[] { "red", "green", "blue" },
    text: "Choose a color");

// Send the activity as a reply to the user.
await context.SendActivityAsync(activity, token);
```

# [JavaScript](#tab/javascript)

```javascript
// Require MessageFactory from botbuilder.
const {MessageFactory} = require('botbuilder');

//  Initialize the message object.
const basicMessage = MessageFactory.suggestedActions(['red', 'green', 'blue'], 'Choose a color');

await context.sendActivity(basicMessage);
```

---

## Additional resources

To preview features, you can use the [Channel Inspector](../bot-service-channel-inspector.md)
