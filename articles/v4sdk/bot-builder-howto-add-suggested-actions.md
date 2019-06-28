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
ms.date:  05/23/2019
monikerRange: 'azure-bot-service-4.0'
---

# Use button for input

[!INCLUDE[applies-to](../includes/applies-to.md)]

You can enable your bot to present buttons that the user can tap to provide input. Buttons enhance user experience by enabling the user to answer a question or make a selection with a simple tap of a button, rather than having to type a response with a keyboard. Unlike buttons that appear within rich cards (which remain visible and accessible to the user even after being tapped), buttons that appear within the suggested actions pane will disappear after the user makes a selection. This prevents the user from tapping stale buttons within a conversation and simplifies bot development (since you will not need to account for that scenario). 

## Suggest action using button

*Suggested actions* enable your bot to present buttons. You can create a list of suggested actions (also known as "quick replies") that will be shown to the user for a single turn of the conversation: 

# [C#](#tab/csharp)

The source code shown here is based on the [suggest actions sample](https://aka.ms/SuggestedActionsCSharp).

[!code-csharp[suggested actions](~/../botbuilder-samples/samples/csharp_dotnetcore/08.suggested-actions/Bots/SuggestedActionsBot.cs?range=87-101)]

# [JavaScript](#tab/javascript)

The source code shown here is based on the [suggested actions sample](https://aka.ms/SuggestActionsJS).

[!code-javascript[suggested actions](~/../botbuilder-samples/samples/javascript_nodejs/08.suggested-actions/bots/suggestedActionsBot.js?range=61-64)]

---

## Additional resources

You can access the complete source code shown here: the [CSharp sample](https://aka.ms/SuggestedActionsCSharp) or [JavaScript sample](https://aka.ms/SuggestActionsJS).

## Next steps

> [!div class="nextstepaction"]
> [Save user and conversation data](./bot-builder-howto-v4-state.md)
