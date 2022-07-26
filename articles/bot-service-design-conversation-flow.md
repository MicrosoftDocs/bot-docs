---
title: Design and control conversation flow
description: Learn how to provide a good user experience with bots. Understand procedural conversation flow, interruption handling, and other design concepts.
keywords: design, control, conversation flow, handle interruptions, overview
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: bot-service
ms.date: 07/26/2022
---

# Design and control conversation flow

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

In a traditional application, the user interface (UI) consists of a series of screens, and a single app or website can use one or more screens as needed to exchange information with the user.
Most applications start with a main screen where users initially land, and that screen provides navigation that leads to other screens for various functions like starting a new order, browsing products, or looking for help.

Like apps and websites, bots have a UI, but it's made up of _messages_ rather than screens. Messages may contain buttons, text, and other elements, or be entirely speech-based.

While a traditional application or website can request multiple pieces of information on a screen all at once, a bot will gather the same amount of information using multiple messages. In this way, the process of gathering information from the user is an active experience; one where the user is having an active conversation with the bot.

A well-designed bot will have a conversational flow that feels natural. The bot should be able to handle the core conversation seamlessly and be able to handle interruptions or switching topics gracefully.

## Procedural conversation flow

Conversations with a bot can focus on the task a bot is trying to achieve, which is called a procedural flow. The bot asks the user a series of questions to gather all the information it needs before processing the task.

In a procedural conversation flow, you define the order of the questions and the bot will ask the questions in the order you defined. You can organize the questions into logical groups to keep the code centralized while staying focused on guiding the conversation. For example, you may design one module to contain the logic that helps the user browse for products and a separate module to contain the logic that helps the user create a new order.

You can structure these modules to flow in any way you like, ranging from free form to sequential. The Bot Framework SDK provides a dialogs library that allows you to construct any conversational flow your bot needs. The library includes [waterfall dialogs](/azure/bot-service/bot-builder-concept-waterfall-dialogs) for creating a sequence of steps and prompts for asking users questions. For more information, see [Dialogs library](/azure/bot-service/bot-builder-concept-dialog).

:::image type="content" source="./media/designing-bots/core/dialogs-screens.png" alt-text="Diagram comparing application GUI flow against bot conversation flow.":::

In a traditional application, everything begins with the _main_ screen.
The main screen invokes the _new order_ screen.
The new order screen remains in control until it either closes or invokes other screens, such as the _product search_ screen.
If the new order screen closes, the user is returned to the main screen.

In a bot that uses dialogs, everything begins with the _root dialog_.
The root dialog invokes the _new order dialog_.
At that point, the new order dialog takes control of the conversation and remains in control until it either closes or invokes another dialog, such as the _product search dialog_.
If the new order dialog closes, control of the conversation returns back to the root dialog.

For an example of how to implement a conversational flow using the dialog libraries, see [Implement sequential conversation flow](./v4sdk/bot-builder-dialog-manage-conversation-flow.md).

## Handle interruptions

It may be tempting to assume that users will perform procedural tasks one by one in a neat and orderly way.
For example, in a procedural conversation flow using dialogs, the user will start at the root dialog and invoke the new order dialog. From the new order dialog, they invoke the product search dialog. Then, when selecting one of the results listed in product search dialog, they invoke the new order dialog. After completing the order, they arrive back at the root dialog.

Although it would be great if users always traveled such a linear, logical path, it seldom occurs.
People don't always communicate in sequential order. They tend to frequently change their minds.
Consider the following example:

:::image type="content" source="./media/bot-service-design-conversation-flow/stack-issue.png" alt-text="Example of a user asking a question in response to a question from the bot.":::

While your bot may be procedural centric, the user may decide to do something entirely different or ask a question that may be unrelated to the current topic.
In the example above, the user asks a question rather than providing the yes/no response that the bot expects.
How should your bot respond?

- Insist that the user answer the question first.
- Disregard everything that the user had done previously, reset the whole dialog stack, and start from the beginning by attempting to answer the user's question.
- Attempt to answer the user's question and then return to that yes/no question and try to resume from there.

There's no _right_ answer to this question, as the best solution will depend upon the specifics of your scenario and how the user would reasonably expect the bot to respond. See how to [Handle user interruptions](v4sdk/bot-builder-howto-handle-user-interrupt.md) for a bot that is designed to handle some types of interruptions.

## Expire a conversation

Sometimes it's useful to restart a conversation from the beginning. For instance, if a user doesn't respond after a certain period of time. Different methods for ending a conversation include:

- Track the last time a message was received from a user, and clear state if the time is greater than a preconfigured length upon receiving the next message from the user.
- Use a storage layer feature, such as Cosmos DB's [time-to-live](/azure/cosmos-db/how-to-time-to-live) feature, to clear state after a preconfigured length of time.

For more information, see how to [Expire a conversation](v4sdk/bot-builder-howto-expire-conversation.md).

## Next steps

Managing the user's navigation across dialogs and designing a conversational flow in a manner that enables users to achieve their goals (even in a non-linear fashion) is a fundamental challenge of bot design.
The [Design bot navigation](bot-service-design-navigation.md) article reviews some common pitfalls of poorly designed navigation and discusses strategies for avoiding those traps.
