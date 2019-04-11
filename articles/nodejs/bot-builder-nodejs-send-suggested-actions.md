---
title: Add suggested actions to messages | Microsoft Docs
description: Learn how to send suggested actions within messages using the Bot Framework SDK for Node.js.
author: v-ducvo
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date:  02/19/2019
monikerRange: 'azure-bot-service-3.0'
---

# Add suggested actions to messages

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-add-suggested-actions.md)
> - [Node.js](../nodejs/bot-builder-nodejs-send-suggested-actions.md)
> - [REST](../rest-api/bot-framework-rest-connector-add-suggested-actions.md)

[!INCLUDE [Introduction to suggested actions](../includes/snippet-suggested-actions-intro.md)]

## Suggested actions example

To add suggested actions to a message, set the `suggestedActions` property of the message to a list of [card actions][ICardAction] that represent the buttons to be presented to the user.

This code example shows how to send a message that presents three suggested actions to the user:

[!code-javascript[Send suggested actions](../includes/code/node-send-suggested-actions.js#sendSuggestedActions)]

When the user taps one of the suggested actions, the bot will receive a message from the user that contains the `value` of the corresponding action.

Be aware that the `imBack` method will post the `value` to the chat window of the channel you are using. If this is not the desired effect, you can use the `postBack` method, which will still post the selection back to your bot, but will not display the selection in the chat window. Some channels do not support `postBack`, however, and in those instances the method will behave like `imBack`.

## Additional resources

- [Samples][samples]
- [IMessage][IMessage]
- [ICardAction][ICardAction]
- [session.send][SessionSend]

[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage

[SessionSend]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#send

[ICardAction]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.icardaction.html

<!-- The inspector is no longer supported: we're redirecting to the samples for now. -->
[samples]: https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples
