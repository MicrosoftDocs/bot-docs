---
title: Design and control conversation flow | Microsoft Docs
description: Learn how to design and control conversation flow in your bot to provide a good user experience.
author: matvelloso
ms.author: mateusv
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 12/13/2017
 
---

# Design and control conversation flow

In a traditional application, the user interface (UI) is a series of screens. 
A single app or website can use one or more screens as needed to exchange information with the user. 
Most applications start with a main screen where users initially land and provide navigation that leads to other screens for various functions like starting a new order, browsing products, or looking for help.

Like apps and websites, bots have a UI, but it is made up of **dialogs**, rather than screens. 
Dialogs enable the bot developer to logically separate various areas of bot functionality and guide conversational flow. For example, you may design one dialog to contain the logic that helps the user browse for products and a separate dialog to contain the logic that helps the user create a new order. 

Dialogs may or may not have graphical interfaces. They may contain buttons, text, and other elements, or be entirely speech-based. Dialogs also contain actions to perform tasks such as invoking other dialogs or processing user input.

## Using dialogs to manage conversation flow

[!INCLUDE [Dialog flow example](~/includes/snippet-dotnet-manage-conversation-flow-intro.md)]

For a detailed walkthrough of managing conversation flow using dialogs and the Bot Builder SDK, see:

- [Manage conversation flow with dialogs (.NET)](~/dotnet/bot-builder-dotnet-manage-conversation-flow.md)
- [Manage conversation flow with dialogs (Node.js)](~/nodejs/bot-builder-nodejs-manage-conversation-flow.md)

## Dialog stack

When one dialog invokes another, the Bot Builder adds the new dialog to the top of the dialog stack. 
The dialog that is on top of the stack is in control of the conversation. 
Every new message sent by the user will be subject to processing by that dialog until it either closes or redirects to another dialog. 
When a dialog closes, it's removed from the stack, and the previous dialog in the stack assumes control of the conversation. 

> [!IMPORTANT]
> Understanding the concept of how the dialog stack is constructed and deconstructed 
> by the Bot Builder as dialogs invoke one another, close, and so on 
> is critical to being able to effectively design the conversation flow of a bot. 

## Dialogs, stacks and humans

It may be tempting to assume that users will navigate across dialogs, creating a dialog stack, 
and at some point will navigate back in the direction they came from, unstacking the dialogs one by one in a neat and orderly way. 
For example, the user will start at root dialog, invoke the new order dialog from there, and then invoke the product search dialog. 
Then the user will select a product and confirm, exiting the product search dialog, complete the order, exiting the new order dialog, and arrive back at the root dialog. 

Although it would be great if users always traveled such a linear, logical path, it seldom occurs. 
Humans do not communicate in "stacks." They tend to frequently change their minds. 
Consider the following example: 

![bot](~/media/bot-service-design-conversation-flow/stack-issue.png)

While your bot may have logically constructed a stack of dialogs, 
 the user may decide to do something entirely different or ask a question that may be unrelated to the current topic. 
In the example, the user asks a question rather than providing the yes/no response that the dialog expects. 
How should your dialog respond?

- Insist that the user answer the question first. 
- Disregard everything that the user had done previously, reset the whole dialog stack, and start from the beginning by attempting to answer the user's question. 
- Attempt to answer the user's question and then return to that yes/no question and try to resume from there. 

There is no *right* answer to this question, as the best solution will depend upon the specifics of your scenario and how the user would reasonably expect the bot to respond. 

## Next steps

Managing the user's navigation across dialogs and designing conversation flow in a manner that enables 
users to achieve their goals (even in a non-linear fashion) is a fundamental challenge of bot design. 
The [next article](~/bot-service-design-navigation.md) reviews some common pitfalls of poorly designed navigation and discusses strategies for avoiding those traps. 
