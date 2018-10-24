---
title: Add suggested actions to messages | Microsoft Docs
description: Learn how to send suggested actions within messages using the Bot Builder SDK for JavaScript.
keywords: suggested actions, buttons, extra input
author: Kaiqb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date:  03/13/2018
monikerRange: 'azure-bot-service-4.0'
---

# Add suggested actions to messages

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

[!include[Introduction to suggested actions](../includes/snippet-suggested-actions-intro.md)] 

## Send suggested actions

You can create a list of suggested actions (also known as "quick replies") that will be shown to the user for a single turn of the conversation: 

# [C#](#tab/csharp)

You can access the source code used here from [GitHub](https://aka.ms/SuggestedActionsCSharp)

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

```csharp
var reply = turnContext.Activity.CreateReply("What is your favorite color?");

reply.SuggestedActions = new SuggestedActions()
{
    Actions = new List<CardAction>()
    {
        new CardAction() { Title = "Red", Type = ActionTypes.ImBack, Value = "Red" },
        new CardAction() { Title = "Yellow", Type = ActionTypes.ImBack, Value = "Yellow" },
        new CardAction() { Title = "Blue", Type = ActionTypes.ImBack, Value = "Blue" },
    },

};
await turnContext.SendActivityAsync(reply, cancellationToken);
```

# [JavaScript](#tab/javascript)
You can access the source code used here from [GitHub](https://aka.ms/SuggestActionsJS).

```javascript
const { ActivityTypes, MessageFactory, TurnContext } = require('botbuilder');

async sendSuggestedActions(turnContext) {
    var reply = MessageFactory.suggestedActions(['Red', 'Yellow', 'Blue'], 'What is the best color?');
    await turnContext.sendActivity(reply);
}
```

---

## Additional resources

You can access the complete source code shown here from GitHub [[C#](https://aka.ms/SuggestedActionsCSharp) | [JS](https://aka.ms/SuggestActionsJS)].
