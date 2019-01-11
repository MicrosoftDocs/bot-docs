---
title: Dialogs overview | Microsoft Docs
description: Learn how to use dialogs within the Bot Framework SDK for Node.js to model conversations and manage conversation flow.
author: DucVo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Dialogs in the Bot Framework SDK for Node.js

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-dialogs.md)
> - [Node.js](../nodejs/bot-builder-nodejs-dialog-overview.md)

Dialogs in the Bot Framework SDK for Node.js allow you to model conversations and manage conversation flow. A bot communicates with a user via conversations. Conversations are organized into dialogs. Dialogs can contain waterfall steps, and prompts. As the user interacts with the bot, the bot will start, stop, and switch between various dialogs in response to user messages. Understanding how dialogs work is key to successfully designing and creating great bots. 

This article introduces dialog concepts. After you read this article, then follow the links in the [Next steps](#next-steps) section to dive deeper into these concepts.

## Conversations through dialogs

Bot Framework SDK for Node.js defines a conversation as the communication between a bot and a user through one or more dialogs. A dialog, at its most basic level, is a reusable module that performs an operation or collects information from a user. You can encapsulate the complex logic of your bot in reusable dialog code.

A conversation can be structured and changed in many ways:

- It can originate from your [default dialog](#default-dialog).
- It can be redirected from one dialog to another.
- It can be resumed.
- It can follow a [waterfall](bot-builder-nodejs-dialog-waterfall.md) pattern, which guides the user through a series of steps or [prompts](bot-builder-nodejs-dialog-prompt.md) the user with a series of questions.
- It can use [actions](bot-builder-nodejs-dialog-actions.md) that listen for words or phrases that trigger a different dialog. 

You can think of a conversation like a parent to dialogs. As such, a conversation contains a *dialog stack* and maintain its own set of state data; namely, the `conversationData` and the `privateConversationData`. A dialog, on the other hand, maintains the `dialogData`. For more information on state data, see [Manage state data](bot-builder-nodejs-state.md).

## Dialog stack

A bot interacts with a user through a series of dialogs that are maintained on a dialog stack. Dialogs are pushed on and popped off the stack in the course of a conversation. The stack works like a normal LIFO stack; meaning, the last dialog added will be the first one to complete. Once a dialog completes then control is returned to the previous dialog on the stack.

When a bot conversation first starts or when a conversation ends, the dialog stack is empty. At this point, when the a user sends a message to the bot, the bot will respond with the *default dialog*.

## Default dialog

Prior to Bot Framework version 3.5, a *root* dialog is defined by adding a dialog named `/`, which lead to naming conventions similar to that of URLs. This naming convention wasn't appropriate to naming dialogs. 

> [!NOTE]
> Starting with version 3.5 of the Bot Framework, the *default dialog* is registered as the second parameter in the constructor of [`UniversalBot`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot.html#constructor).  

The following code snippet shows how to define the default dialog when creating the `UniversalBot` object.

```javascript
var bot = new builder.UniversalBot(connector, [
    //...Default dialog waterfall steps...
    ]);
```

The default dialog runs whenever the dialog stack is empty and no other dialog is [triggered](bot-builder-nodejs-dialog-actions.md) via LUIS or another [recognizer](bot-builder-nodejs-recognize-intent-messages.md). As the default dialog is the bot's first response to the user, the default dialog should provide some contextual information to the user, such as a list of available commands or an overview of what the bot can do.

## Dialog handlers

The dialog handler manages the flow of a conversation. To progress through a conversation, the dialog handler directs the flow by starting and ending dialogs. 

## Starting and ending dialogs

To start a new dialog (and push it onto the stack), use [`session.beginDialog()`](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#begindialog). To end a dialog (and remove it from the stack, returning control to the calling dialog), use either [`session.endDialog()`](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#enddialog) or [`session.endDialogWithResult()`](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#enddialogwithresult). 

## Using waterfalls and prompts

[Waterfall](bot-builder-nodejs-dialog-waterfall.md) is a simple way to model and manage conversation flow. A waterfall contains a sequence of steps. In each step, you can either complete an action on behalf of the user or [prompt](bot-builder-nodejs-dialog-prompt.md) the user for information.

A waterfall is implemented using a dialog that's made up of a collection of functions. Each function defines a step in the waterfall. The following code sample shows a simple conversation that uses a two step waterfall to prompt the user for their name and greet them by name.

```javascript
// Ask the user for their name and greet them by name.
bot.dialog('greetings', [
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    },
    function (session, results) {
        session.endDialog(`Hello ${results.response}!`);
    }
]);
```

When a bot reaches the end of the waterfall without ending the dialog, the next message from the user will restart that dialog at step one of the waterfall. This may lead to frustrations as the user may feel like they are trapped in a loop. To avoid this situation, when a conversation or dialog has come to an end, it is best practice to explicitly call `endDialog`, `endDialogWithResult`, or `endConversation`.

## Next steps

To dive deeper into dialogs, it is important to understand how waterfall pattern works and how to use it to guide users through a process.

> [!div class="nextstepaction"]
> [Define conversation steps with waterfalls](bot-builder-nodejs-dialog-waterfall.md)
