---
title: Handle user and conversation events  | Microsoft Docs
description: Learn how to handle events such as a user joining a conversation using the Bot Builder SDK for Node.js.
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 08/04/2017
ms.reviewer:
---

# Handle user and conversation events

This article demonstrates how your bot can handle events such as a user joining a conversation, adding a bot to a contacts list, or saying **Goodbye** when a bot is being removed from a conversation.


## Greet a user on conversation join
The Bot Framework provides the [conversationUpdate][conversationUpdate] event for notifying your bot whenever a member joins or leaves a conversation. A conversation member can be a user or a bot.

The following code snippet allows the bot to greet new member(s) to a conversation or say **Goodbye** if it is being removed from the conversation.

[!include[conversationUpdate sample Node.js](../includes/snippet-code-node-conversationupdate-1.md)]

## Acknowledge add to contacts list

The [contactRelationUpdate][contactRelationUpdate] event notifies your bot that a user has added you to their contacts list.

[!include[contactRelationUpdate sample Node.js](../includes/snippet-code-node-contactrelationupdate-1.md)]

## Add a first-run dialog

Since the **conversationUpdate** and the **contactRelationUpdate** event are not supported by all channels, a universal way to greet a user who joins a conversation is to add a first-run dialog.

In the following example we’ve added a function that triggers the dialog any time we’ve never seen a user before. You can customize the way an action is triggered by providing an [onFindAction][onFindAction] handler for your action. 

[!include[first-run sample Node.js](../includes/snippet-code-node-first-run-dialog-1.md)]


You can also customize what an action does after its been triggered by providing an [onSelectAction][onSelectAction] handler. For trigger actions you can provide an [onInterrupted][onInterrupted] handler to intercept an interruption before it occurs. For more information, see [Handle user actions](bot-builder-nodejs-dialog-actions.md)

## Additional resources

* [conversationUpdate][conversationUpdate]
* [contactRelationUpdate][contactRelationUpdate]

[conversationUpdate]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iconversationupdate.html
[contactRelationUpdate]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.icontactrelationupdate.html

[onFindAction]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.itriggeractionoptions#onfindaction
[onSelectAction]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.itriggeractionoptions#onselectaction
[onInterrupted]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.itriggeractionoptions#oninterrupted

[SendTyping]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#sendtyping
[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage
[ChatConnector]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.chatconnector.html
[session_userData]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#userdata
