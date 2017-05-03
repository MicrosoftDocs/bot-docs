---
title: Add suggested actions to messages | Microsoft Docs
description: Learn how to add suggested actions to messages using the Bot Builder SDK for .NET.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer: 
---

# Add suggested actions to messages

[!include[Introduction to suggested actions](~/includes/snippet-suggested-actions-intro.md)] 

> [!TIP]
> To learn how various channels render suggested actions, see the [Channel Inspector][channelInspector].

## Send suggested actions

To add suggested actions to a message, set the `SuggestedActions` property of the activity to a list of [CardAction][cardAction] objects that represent the buttons to be presented to the user. 

This code example shows how to create a message that presents three suggested actions to the user:

[!code-csharp[Add suggested actions](~/includes/code/dotnet-add-suggested-actions.cs#addSuggestedActions)]

When the user taps one of the suggested actions, the bot will receive a message from the user that contains the `Value` of the corresponding action.

## Additional resources

- [Activities overview](~/dotnet/bot-builder-dotnet-activities.md)
- [Create messages](~/dotnet/bot-builder-dotnet-create-messages.md)

[channelInspector]: https://docs.botframework.com/en-us/channel-inspector/channels/Facebook/#navtitle

[cardAction]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/d1f/class_microsoft_1_1_bot_1_1_connector_1_1_card_action.html