---
title: Design and control conversation flow | Microsoft Docs
description: Learn how to design and control conversation flow in your bot to provide a good user experience.
keywords: design, control, conversation flow, handle interruptions, overview
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/19/2018
---

::: moniker range="azure-bot-service-3.0"

# Design and control conversation flow

[!INCLUDE [pre-release-label](./includes/pre-release-label-v3.md)]

In a traditional application, the user interface (UI) is a series of screens. 
A single app or website can use one or more screens as needed to exchange information with the user. 
Most applications start with a main screen where users initially land and provide navigation that leads to other screens for various functions like starting a new order, browsing products, or looking for help.

Like apps and websites, bots have a UI, but it is made up of **dialogs**, rather than screens. Dialogs help preserve your place within a conversation, prompt users when needed, and execute input validation. They are useful for managing multi-turn conversations and simple "forms-based" collections of information to accomplish activities such as booking a flight.

Dialogs enable the bot developer to logically separate various areas of bot functionality and guide conversational flow. For example, you may design one dialog to contain the logic that helps the user browse for products and a separate dialog to contain the logic that helps the user create a new order. 

Dialogs may or may not have graphical interfaces. They may contain buttons, text, and other elements, or be entirely speech-based. Dialogs also contain actions to perform tasks such as invoking other dialogs or processing user input.

## Using dialogs to manage conversation flow

[!INCLUDE [Dialog flow example](./includes/snippet-dotnet-manage-conversation-flow-intro.md)]

For a detailed walkthrough of managing conversation flow using dialogs and the Bot Framework SDK, see:

- [Manage conversation flow with dialogs (.NET)](./dotnet/bot-builder-dotnet-manage-conversation-flow.md)
- [Manage conversation flow with dialogs (Node.js)](./nodejs/bot-builder-nodejs-manage-conversation-flow.md)

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

![bot](./media/bot-service-design-conversation-flow/stack-issue.png)

While your bot may have logically constructed a stack of dialogs, 
 the user may decide to do something entirely different or ask a question that may be unrelated to the current topic. 
In the example, the user asks a question rather than providing the yes/no response that the dialog expects. 
How should your dialog respond?

- Insist that the user answer the question first. 
- Disregard everything that the user had done previously, reset the whole dialog stack, and start from the beginning by attempting to answer the user's question. 
- Attempt to answer the user's question and then return to that yes/no question and try to resume from there. 

There is no one *right* answer to this question, as the best solution will depend upon the specifics of your scenario and how the user would reasonably expect the bot to respond. However, as your conversation complexity increases **dialogs** become harder to manage. For complex branchings situations, it may be easier to create your own flow of control logic to keep track of your user's conversation.

## Next steps

Managing the user's navigation across dialogs and designing conversation flow in a manner that enables 
users to achieve their goals (even in a non-linear fashion) is a fundamental challenge of bot design. 
The [next article](./bot-service-design-navigation.md) reviews some common pitfalls of poorly designed navigation and discusses strategies for avoiding those traps. 

::: moniker-end

::: moniker range="azure-bot-service-4.0"

# Design and control conversation flow

In a traditional application, the user interface (UI) consists of a series of screens, and a single app or website can use one or more screens as needed to exchange information with the user.
Most applications start with a main screen where users initially land, and that screen provides navigation that leads to other screens for various functions like starting a new order, browsing products, or looking for help.

Like apps and websites, bots have a UI, but it is made up of **messages** rather than screens. Messages may contain buttons, text, and other elements, or be entirely speech-based.

While a traditional application or website can request multiple pieces of information on a screen all at once, a bot will gather the same amount of information using multiple messages. In this way, the process of gathering information from the user is an active experience; one where the user is having an active conversation with the bot.

A well designed bot will have a conversation flow that feels natural. The bot should be able to handle the core conversation seamlessly, and be able to handle interruptions or switching topic of conversations gracefully.

## Procedural conversation flow

Conversation with a bot is generally focused around the task a bot is trying to achieve, which is called a procedural flow. This is where the bot asks the user a series of questions to gather all the information it needs before processing the task.

In a procedural conversation flow, you define the order of the questions and the bot will ask the questions in the order you defined. You can organize the questions into logical *modules* to keep the code centralized while staying focused on guiding the conversational. For example, you may design one module to contain the logic that helps the user browse for products and a separate module to contain the logic that helps the user create a new order.

You can structure these modules to flow in any way you like, ranging from free form to sequential. The Bot Framework SDK provides several libraries that allows you to construct any conversational flow your bot needs. For example, the `prompts` library allows you to ask users for input, the `waterfall` library allows you to define a sequence of question/answer pair, the `dialog control` library allows you to modularized your conversational flow logic, etc. All of these libraries are tied together through a `dialogs` object. Let's take a closer look at how modules are implemented as `dialogs` to design and manage conversation flows and see how that flow is similar to the traditional application flow.

![bot](./media/designing-bots/core/dialogs-screens.png)

In a traditional application, everything begins with the **main screen**.
The **main screen** invokes the **new order screen**.
The **new order screen** remains in control until it either closes or invokes other screens, such as the **product search screen**.
If the **new order screen** closes, the user is returned to the **main screen**.

In a bot, everything begins with the **root dialog**.
The **root dialog** invokes the **new order dialog**.
At that point, the **new order dialog** takes control of the conversation and remains in control until it either closes or invokes other dialogs, such as the **product search dialog**.
If the **new order dialog** closes, control of the conversation is returned back to the **root dialog**.

For examples of how implement conversation flow, see how to [create your own prompts to gather user input](./v4sdk/bot-builder-primitive-prompts.md) and use dialogs to [implement sequential conversation flow](./v4sdk/bot-builder-dialog-manage-conversation-flow.md).

## Handle interruptions

It may be tempting to assume that users will perform procedural tasks one by one in a neat and orderly way.
For example, in a procedural conversation flow using `dialogs`, the user will start at root dialog, invoke the new order dialog from there, and then invoke the product search dialog. Then the user will select a product and confirm, exiting the product search dialog, complete the order, exiting the new order dialog, and arrive back at the root dialog.

Although it would be great if users always traveled such a linear, logical path, it seldom occurs.
Humans do not communicate in sequential `dialogs`. They tend to frequently change their minds.
Consider the following example:

![bot](./media/bot-service-design-conversation-flow/stack-issue.png)

While your bot may be procedural centric, the user may decide to do something entirely different or ask a question that may be unrelated to the current topic.
In the example above, the user asks a question rather than providing the yes/no response that the bot expects.
How should your bot respond?

- Insist that the user answer the question first.
- Disregard everything that the user had done previously, reset the whole dialog stack, and start from the beginning by attempting to answer the user's question.
- Attempt to answer the user's question and then return to that yes/no question and try to resume from there.

There is no *right* answer to this question, as the best solution will depend upon the specifics of your scenario and how the user would reasonably expect the bot to respond. For more information see [Handle user interruption](v4sdk/bot-builder-howto-handle-user-interrupt.md).

## Next steps

Managing the user's navigation across dialogs and designing conversation flow in a manner that enables
users to achieve their goals (even in a non-linear fashion) is a fundamental challenge of bot design.
The [next article](~/bot-service-design-navigation.md) reviews some common pitfalls of poorly designed navigation and discusses strategies for avoiding those traps.

::: moniker-end
