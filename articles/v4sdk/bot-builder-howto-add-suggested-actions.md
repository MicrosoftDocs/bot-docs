---
title: Use button for input - Bot Service
description: Learn how to send suggested actions within messages using the Bot Framework SDK for JavaScript.
keywords: suggested actions, buttons, extra input
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 11/01/2021
monikerRange: 'azure-bot-service-4.0'
---

# Use button for input

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Buttons enhance the conversational experience by letting the user answer a question or select the desired button, rather than having to type a response with a keyboard. Unlike buttons that appear within rich cards (which remain visible and accessible to the user even after being selected), buttons that appear within the suggested actions pane will disappear after the user makes a selection. This prevents the user from selecting stale buttons within a conversation and simplifies bot development since you won't need to account for that scenario.

## Suggest action using button

*Suggested actions* enable your bot to present buttons. You can create a list of suggested actions (also known as _quick replies_) that will be shown to the user for a single turn of the conversation.

# [C#](#tab/csharp)

The source code shown here is based on the [Suggested actions](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/08.suggested-actions) sample.

[!code-csharp[suggested actions](~/../botbuilder-samples/samples/csharp_dotnetcore/08.suggested-actions/Bots/SuggestedActionsBot.cs?range=80-98)]

# [JavaScript](#tab/javascript)

The source code shown here is based on the [Suggested actions](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/08.suggested-actions) sample.

[!code-javascript[suggested actions](~/../botbuilder-samples/samples/javascript_nodejs/08.suggested-actions/bots/suggestedActionsBot.js?range=58-89)]

# [Java](#tab/java)

The source code shown here is based on the [Suggested actions](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/08.suggested-actions) sample.

[!code-java[suggested actions](~/../botbuilder-samples/samples/java_springboot/08.suggested-actions/src/main/java/com/microsoft/bot/sample/suggestedactions/SuggestedActionsBot.java?range=102-136)]

# [Python](#tab/python)

The source code shown here is based on the [Suggested actions](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/08.suggested-actions) sample.

[!code-python[suggested actions](~/../botbuilder-samples/samples/python/08.suggested-actions/bots/suggested_actions_bot.py?range=63-81)]

---

## Additional resources

You can access the complete source code for the **Suggested actions** sample in [C#](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/08.suggested-actions), [JavaScript](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/08.suggested-actions), [Java](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/08.suggested-actions) and [Python](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/08.suggested-actions).

## Next steps

> [!div class="nextstepaction"]
> [Save user and conversation data](./bot-builder-howto-v4-state.md)
