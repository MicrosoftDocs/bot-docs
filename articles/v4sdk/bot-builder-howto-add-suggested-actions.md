---
title: Use button for input | Microsoft Docs
description: Learn how to send suggested actions within messages using the Bot Framework SDK for JavaScript.
keywords: suggested actions, buttons, extra input
author: Kaiqb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date:  4/18/2019
monikerRange: 'azure-bot-service-4.0'
---

# Use button for input

[!INCLUDE[applies-to](../includes/applies-to.md)]

You can enable your bot to present buttons that the user can tap to provide input. Buttons enhance user experience by enabling the user to answer a question or make a selection with a simple tap of a button, rather than having to type a response with a keyboard. Unlike buttons that appear within rich cards (which remain visible and accessible to the user even after being tapped), buttons that appear within the suggested actions pane will disappear after the user makes a selection. This prevents the user from tapping stale buttons within a conversation and simplifies bot development (since you will not need to account for that scenario). 

## Suggest action using button

*Suggested actions* enable your bot to present buttons. You can create a list of suggested actions (also known as "quick replies") that will be shown to the user for a single turn of the conversation: 

# [C#](#tab/csharp)

You can access the source code used here from [GitHub](https://aka.ms/SuggestedActionsCSharp)

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

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
await turnContext.SendActivityAsync(reply, cancellationToken: cancellationToken);
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
