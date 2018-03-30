---
title: Add suggested actions to messages | Microsoft Docs
description: Learn how to send suggested actions within messages using the Bot Builder SDK for Node.js.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date:  06/06/2017
---

# Add suggested actions to messages
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-add-suggested-actions.md)
> - [Node.js](../nodejs/bot-builder-nodejs-send-suggested-actions.md)
> - [REST](../rest-api/bot-framework-rest-connector-add-suggested-actions.md)

[!INCLUDE [Introduction to suggested actions](../includes/snippet-suggested-actions-intro.md)]

> [!TIP]
> Use the [Channel Inspector][channelInspector] to see how suggested actions look and work on various channels.

## Suggested actions example

To add suggested actions to a message, set the `suggestedActions` property of the message to a list of [card actions][ICardAction] that represent the buttons to be presented to the user.

This code example shows how to send a message that presents three suggested actions to the user:

[!code-javascript[Send suggested actions](../includes/code/node-send-suggested-actions.js#sendSuggestedActions)]

When the user taps one of the suggested actions, the bot will receive a message from the user that contains the `value` of the corresponding action.

Be aware that the `imBack` method will post the `value` to the chat window of the channel you are using. If this is not the desired effect, you can use the `postBack` method, which will still post the selection back to your bot, but will not display the selection in the chat window. Some channels do not support `postBack`, however, and in those instances the method will behave like `imBack`.

## Additional resources

* [Preview features with the Channel Inspector][inspector]
* [IMessage][IMessage]
* [ICardAction][ICardAction]
* [session.send][SessionSend]

[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage

[SessionSend]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#send

[ICardAction]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.icardaction.html

[inspector]: ../bot-service-channel-inspector.md

[channelInspector]: ../bot-service-channel-inspector.md
