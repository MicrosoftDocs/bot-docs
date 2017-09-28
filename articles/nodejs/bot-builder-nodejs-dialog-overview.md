---
title: 對話框概述 | Microsoft Docs
description: 了解如何在Bot Builder SDK中使用Node.js中的對話框來建模會話和管理會話流程。
author: 
ms.author: 
manager: 
ms.topic: article
ms.prod: bot-framework
ms.date: 
---

# Node.js的Bot Builder SDK中的對話框
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-dialogs.md)
> - [Node.js](../nodejs/bot-builder-nodejs-dialog-overview.md)

Node.js的Bot Builder SDK中的對話框允許您對對話進行建模和管理會話流程。機器人通過對話與用戶進行溝通。對話組織成對話框。對話框可以包含瀑布步驟和提示。當用戶與機器人交互時，機器人將會因應用戶輸入的內容來啟動、停止或各種對話之間切換。了解對話框的工作原理是成功設計和創建優秀機器人的關鍵。

本文介紹對話框概念。閱讀本文後，請按照[後續步驟](#next-steps)部分中的鏈接深入了解這些概念。

## 通過對話框進行對話

Node.js的Bot Builder SDK通過一個或多個對話將會話合併為聊天機器人和用戶之間的溝通。最基本的對話框是可重複使用的模塊，用於執行操作或從用戶收集信息。您可以將您的機器人的複雜邏輯封裝在可重複使用的對話框程式碼中。

會話可以通過多種方式進行結構化和更改：

- 它可以來自您的[預設對話框](#default-dialog)。
- 它可以從一個對話框重定向到另一個對話框。
- 可以重新開始。
- 它可以遵循[瀑布式](bot-builder-nodejs-dialog-waterfall.md)，它引導用戶通過一系列步驟或[提示](bot-builder-nodejs-dialog-prompt.md)引導用戶回答的問題。
- 它可以使用[action](bot-builder-nodejs-dialog-actions.md)來監聽觸發不同對話框的單詞或短語。

You can think of a conversation like a parent to dialogs. As such, a conversation contains a *dialog stack* and maintain its own set of state data; namely, the `conversationData` and the `privateConversationData`. A dialog, on the other hand, maintains the `dialogData`. For more information on state data, see [Manage state data](bot-builder-nodejs-state.md).

## Dialog stack

A bot interacts with a user through a series of dialogs that are maintained on a dialog stack. Dialogs are pushed on and popped off the stack in the course of a conversation. The stack works like a normal LIFO stack; meaning, the last dialog added will be the first one to complete. Once a dialog completes then control is returned to the previous dialog on the stack.

When a bot conversation first starts or when a conversation ends, the dialog stack is empty. At this point, when the a user sends a message to the bot, the bot will respond with the *default dialog*.

## <a name="default-dialog"></a>預設對話框

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
// 向用戶詢問他們的姓名，並按名稱打招呼。
bot.dialog('greetings', [
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    },
    function (session, results) {
        session.endDialog(`Hello ${results.response}!`);
    }
]);
```

當機器人到達瀑布的盡頭而不結束對話時，用戶的下一條消息將在瀑布的第一步重新啟動該對話。這可能會導致疑問，因為用戶可能會覺得他們被困在一個循環中。為了避免這種情況，當對話或對話已經結束時，最好的做法是明確地調用`endDialog`、`endDialogWithResult`或`endConversation`來結束對話。

## <a name="next-steps"></a>下一步

要深入了解對話框，了解瀑布式的工作原理以及如何使用它來引導用戶進行過程很重要。

> [!div class="nextstepaction"]
> [定義瀑布式談話步驟](bot-builder-nodejs-dialog-waterfall.md)
