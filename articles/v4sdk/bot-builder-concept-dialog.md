---
title: Dialogs in the Bot Framework SDK
description: Learn about Bot Framework SDK dialogs. Understand dialog classes and features, different types of dialogs, and dialog design patterns.
keywords: conversation flow, dialogs, dialog state, bot conversation, dialog set, dialog context, dialog stack
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: bot-service
ms.date: 09/01/2022
monikerRange: 'azure-bot-service-4.0'
---

# Dialogs library

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

_Dialogs_ are a central concept in the SDK, providing ways to manage a long-running conversation with the user.
A dialog performs a task that can represent part of or a complete conversational thread.
It can span just one turn or many, and can span a short or long period of time.

This article describes the core classes and features of the dialog library.

- You should be familiar with [how bots work](bot-builder-basics.md) (including [what a turn is](bot-builder-basics.md#defining-a-turn)) and [managing state](bot-builder-concept-state.md).
- Each dialog represents a conversational task that can run to completion and return collected information.
- Each dialog represents a basic unit of control flow: it can begin, continue, and end; pause and resume; or be canceled.
- Dialogs are similar to a method or function in a programming language. You can pass in arguments or parameters when you start a dialog, and the dialog can later produce a return value when it ends.

> [!TIP]
> If you are new to developing bots with the Bot Framework or creating a new conversational experience, start with  the [Bot Framework Composer](/composer/).
> For existing SDK-first bots, not created in Composer, consider exposing your bot as a [skill](skills-conceptual.md) and using Composer for future bot development.

## Dialog state

Dialogs can implement a _multi-turn conversation_, and as such, they rely on _persisted state_ across turns. Without state in dialogs, your bot wouldn't know where it was in the conversation or what information it had already gathered.

To retain a dialog's place in the conversation, the dialog's state must be retrieved from and saved to memory each turn. This is handled via a dialog state property accessor defined on the bot's conversation state. This dialog state manages information for all active dialogs, and children of active dialogs.
This allows the bot to pick up where it left off last and to handle a variety of conversation models.

At run time, the dialog state property includes information on where the dialog is in its logical process, including any internally collected information in the form of a _dialog instance_ object. Again, this needs to be read into the bot and saved out to memory each turn.

## Dialog infrastructure

Along with various types of dialogs, the following classes are involved in the design and control of conversations.
While you don't usually need to interact with these classes directly, being aware of them and their purpose is useful when designing dialogs for a bot.

| Class | Description
| :--   | :--
| _Dialog set_ | Defines a collection of dialogs that can reference each other and work in concert.
| _Dialog context_ | Contains information about all active dialogs.
| _Dialog instance_ | Contains information about one active dialog.
| _Dialog turn result_ | Contains status information from an active, or recently active, dialog. If the active  dialog ended, this contains its return value.

## Dialog types

The dialogs library provides a few types of dialogs to make your bot's conversations easier to manage. Some of these types are described in more detail later in this article.

| Type | Description |
|:-|:-|
| dialog | The base class for all dialogs. |
| [container dialog](#container-dialogs) | The base class for all _container_ dialogs, such as component and adaptive dialogs. It maintains an inner dialog set and allows you to treat a collection of dialogs as a unit. |
| [component dialog](#component-dialogs) | A general-purpose type of container dialog that encapsulates a set of dialogs, allowing for the reuse of the set as a whole. When a component dialog starts, it begins with a designated dialog in its collection. When the inner process completes, the component dialog ends. |
| waterfall dialog | Defines a sequence of steps, allowing your bot to guide a user through a linear process. These are typically designed to work within the context of a component dialog. |
| prompt dialogs | Ask the user for input and return the result. A prompt will repeat until it gets valid input or it is canceled. They are designed to work with waterfall dialogs. |
| adaptive dialog | A type of container dialog used by Composer to provide more natural conversational flows. _Not_ intended to be used directly in an SDK-first bot. |
| action dialogs | A type of dialog that supports the implementation of actions in Composer. _Not_ intended to be used directly in an SDK-first bot. |
| input dialogs | A type of dialog that supports the implementation of input actions in Composer. _Not_ intended to be used directly in an SDK-first bot. |
| [skill dialog](#skill-dialog) | Automates the management of one or more skill bots from a skill consumer. Composer directly supports skills as actions. |
| [QnA Maker dialog](#qna-maker-dialog) | Automates access to a QnA Maker knowledge base. This dialog is designed to also work as an action within Composer. |

> [!IMPORTANT]
> _Adaptive dialogs_ were first added in version 4.9 of the C# SDK.
> Adaptive dialogs support the [Bot Framework Composer](/composer/) and are not intended to be used directly in an SDK-first bot.

## Dialog patterns

There are two main patterns for starting and managing dialogs from a bot.

1. We recommend using Bot Framework Composer to author conversational dialogs, to benefit from more natural, free-flowing conversational capabilities. For more information, see the [Introduction to Bot Framework Composer](/composer/introduction). Such bots can still be extended with code where needed.
1. Develop your bot in one of the SDK languages and use your root dialog's _run_ extension method. For information on using the run method with a component dialog, see [about component and waterfall dialogs](bot-builder-concept-waterfall-dialogs.md) and how to [implement sequential conversation flow](bot-builder-dialog-manage-conversation-flow.md).

### The dialog stack

A dialog context contains information about all active dialogs and includes a _dialog stack_, which acts as a _call stack_ for all the active dialogs.
Each container dialog has an inner set of dialogs that it is controlling, and so each active container dialog introduces an inner dialog context and dialog stack as part of its state.

While you will not access the stack directly, understanding that it exists and its function will help you understand how various aspects of the dialogs library work.

## Container dialogs

A container dialog <!--acts as individual dialog and--> can be part of a larger dialog set. Each container has an inner dialog set that is also managed.

- Each dialog set creates a scope for resolving dialog IDs.
- The SDK currently implements two types of container dialogs: component dialogs and adaptive dialogs.

  The conceptual structure of the two are quite different. However, a Composer bot can make use of both.

### Dialog IDs

When you add a dialog to a dialog set, you assign it an unique ID within that set. Dialogs within a set reference each other by their IDs.

When one dialog references another dialog at run time, it does so by the dialog's ID. The dialog context tries to resolve the ID based on the other dialogs in the immediate dialog set. If there is no match, it looks for a match in the containing or outer dialog set, and so on. If no match is found, an exception or error is generated.

### Component dialogs

Component dialogs use a sequence model for conversations, and each dialog in the container is responsible for calling other dialogs in the container. When the component dialog's inner dialog stack is empty, the component ends.

Consider using component and waterfall dialogs if your bot has a relatively simple control flow that does not require more dynamic conversation flow.

[About component and waterfall dialogs](bot-builder-concept-waterfall-dialogs.md) describes component, waterfall, and prompt dialogs in more detail.

## Other dialogs

The QnA Maker and skill dialogs can be used as stand-alone dialogs or as part of a collection of dialogs in a container.

### QnA Maker dialog

[!INCLUDE [qnamaker-sunset-alert](../includes/qnamaker-sunset-alert.md)]

The QnA Maker dialog accesses a QnA Maker knowledge base and supports QnA Maker's follow-up prompt and active learning features.

- Follow-up prompts, also known as multi-turn prompts, allow a knowledge base to ask the user for more information before answering their question.
- Active learning suggestions allow the knowledge base to improve over time. The QnA Maker dialog supports explicit feedback for the active learning feature.

For more information, see:

- [What is QnA Maker?](/azure/cognitive-services/qnamaker/overview/overview).
- In the SDK, how to [use QnA Maker to answer questions](bot-builder-howto-qna.md).
- In Composer, how to [Add a QnA Maker knowledge base to your bot](/composer/how-to-add-qna-to-bot).

### Skill dialog

A skill dialog accesses and manages one or more skills.
The skill dialog posts activities from the parent bot to the skill bot and returns the skill responses to the user.

For more information, see:

- In the SDK, the [Skills overview](skills-conceptual.md).
- In Composer, [About skills](/composer/concept-skills).

## Next steps

> [!div class="nextstepaction"]
> [About middleware](bot-builder-concept-middleware.md)
