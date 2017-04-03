---
title: Designing conversation flow within a bot | Microsoft Docs
description: Understand the fundamental concepts of designing conversation flow by using dialogs in the Microsoft Bot Framework.
keywords: Bot Framework, bot design, dialog, conversation flow, conversation
author: matvelloso
manager: rstand
ms.topic: design-ui-and-ux-article
ms.prod: bot-framework

ms.date:
ms.reviewer: rstand
#ROBOTS: Index
---
# Designing conversation flow

In a traditional application, the user interface (UI) is typically comprises screens. 
A single app or website can use one or more screens as needed to exchange information with the user. 
Most applications start with a main screen where users initially land and provide navigation that leads to other screens for various functions like starting a new order, browsing products, or looking for help.

Like apps and websites, bots have a UI, but it comprises **dialogs**, rather than screens. 
Dialogs enable the bot developer to logically separate various areas of bot functionality, much like screens do for traditional apps and websites. 
For example, you may design one dialog to contain the logic that helps the user browse for products, and a separate dialog to contain the logic that helps the user create a new order. 

Dialogs may or may not have graphical interfaces. 
They may contain buttons, text, and other elements, or be entirely speech-based. 
Additionally, one dialog can invoke another, just like one screen can invoke another in an app or website.

## Using dialogs to manage conversation flow

[!include[Dialog flow example](~/includes/snippet-dotnet-manage-conversation-flow-intro.md)]

For a detailed walkthrough of managing conversation flow using dialogs and the Bot Builder SDK, see:

- [Manage conversation flow using dialogs with the Bot Builder SDK for .NET](~/dotnet/manage-conversation-flow.md)
- [Manage conversation flow using dialogs with the Bot Builder SDK for Node.js](~/nodejs/manage-conversation-flow.md)

##<a id="stack"></a> Dialog stack

When one dialog invokes another, the Bot Builder adds the new dialog to the top of the dialog stack. 
The dialog that is on top of the stack at any given time is in control of the conversation. 
Every new message sent by the user will be subject to processing by that dialog until it either closes or redirects to another dialog. 
When a dialog closes, it's removed from the stack, and the prior dialog in the stack assumes control of the conversation. 

> [!IMPORTANT]
> Understanding the concept of how the dialog stack is constructed and deconstructed 
> by the Bot Builder as dialogs invoke one another, close, and so on 
> is critical to being able to effectively design the conversation flow of a bot. 

##<a id="dialogs-stacks-and-humans"></a> Dialogs, stacks and humans

It may be tempting to assume that users will navigate across dialogs, creating a dialog stack, 
and at some point will navigate back in the direction they came from, unstacking the dialogs one by one in a neat and orderly way. 
For example, the user will start at root dialog, invoke the new order dialog from there, and then invoke the product search dialog. 
Then the user will select a product and confirm, exiting the product search dialog, complete the order, exiting the new order dialog, and arrive back at the root dialog. 

Although it would be great if users always traveled such a linear, logical path through an application, website, or bot, it seldom occurs. 
Humans do not communicate in "stacks." They tend to change their minds, a lot. 
Consider the following example: 

![bot](~/media/designing-bots/core/stack-issue.png)

While your bot may have logically constructed a stack of dialogs, 
 the user may decide to do something entirely different or ask a question that may be unrelated to the current topic. 
In the example, the user asks a question rather than providing the yes/no response that the dialog expects. 
How should your dialog respond?

- Insist that the user should answer the question first. 
- Disregard everything that the user had done previously, reset the whole dialog stack, and start from the beginning by attempting to answer the user's question. 
- Attempt to answer the user's question and then return to that yes/no question and try to resume from there. 

There is no *right* answer to this question, as
the best solution will depend upon the specifics of your scenario, 
such as the likelihood of this happening or how the user would reasonably expect the bot to respond. 

## Next steps

Managing the user's navigation across dialogs and designing conversation flow in a manner that enables 
users to achieve their goals (even in a non-linear fashion) is a fundamental challenge of bot design. 
In the [next article](~/design/bot-design-navigation.md), we'll 
review some common pitfalls of poorly designed navigation and discuss strategies for avoiding those traps. 
