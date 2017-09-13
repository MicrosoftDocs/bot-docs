---
title: Add suggested actions to messages | Microsoft Docs
description: Learn how to add suggested actions to messages using the Bot Builder SDK for .NET.
author: kbrandl
ms.author: kibrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 06/06/2017
---

# Add suggested actions to messages
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-add-suggested-actions.md)
> - [Node.js](../nodejs/bot-builder-nodejs-send-suggested-actions.md)
> - [REST](../rest-api/bot-framework-rest-connector-add-suggested-actions.md)

[!include[Introduction to suggested actions](../includes/snippet-suggested-actions-intro.md)] 

> [!TIP]
> Use the [Channel Inspector][channelInspector] to see how suggested actions look and work on various channels.

## Send suggested actions

To add suggested actions to a message, set the `SuggestedActions` property of the activity to a list of [CardAction][cardAction] objects that represent the buttons to be presented to the user. 

This code example shows how to create a message that presents three suggested actions to the user:

[!code-csharp[Add suggested actions](../includes/code/dotnet-add-suggested-actions.cs#addSuggestedActions)]

When the user taps one of the suggested actions, the bot will receive a message from the user that contains the `Value` of the corresponding action.

## Additional resources

- [Preview features with the Channel Inspector][inspector]
- [Activities overview](bot-builder-dotnet-activities.md)
- [Create messages](bot-builder-dotnet-create-messages.md)
- <a href="https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.connector.activity?view=botbuilder-3.8" target="_blank">Activity class</a>
- <a href="https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.connector.imessageactivity?view=botbuilder-3.8" target="_blank">IMessageActivity interface</a>
- <a href="https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.connector.cardaction?view=botbuilder-3.8" target="_blank">CardAction class</a>
- <a href="https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.connector.suggestedactions?view=botbuilder-3.8" target="_blank">SuggestedActions class</a>

[cardAction]: https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.connector.cardaction?view=botbuilder-3.8

[inspector]: ../portal-channel-inspector.md

[channelInspector]: ../portal-channel-inspector.md
