---
title: Dialogs within the Bot Framework SDK - Bot Service
description: Describes what a dialog is and how it work within the Bot Framework SDK.
keywords: conversation flow, dialogs, dialog state, bot conversation, dialog set, dialog context, dialog stack
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/14/2020
monikerRange: 'azure-bot-service-4.0'
---

<!-- Working notes:
  - [x] Pare down to generic dialog information and link off to "scenario-specific" topics.
  - [x] Pull component/waterfall specific content into a child article.
  - [x] Link off to the adaptive dialog concept article for that stuff.
  - [ ] Fix all resulting broken links in articles that refer to removed/renamed sections in this one.
-->

# Dialogs library

[!INCLUDE [applies-to-v4](../includes/applies-to.md)]

*Dialogs* are a central concept in the SDK, providing ways to manage a long-running conversation with the user.
A dialog performs a task that can represent part of or a complete conversational thread.
It can span just one turn or many, and can span a short or long period of time.

This article describes the core classes and features of the dialog library.
You should be familiar with [how bots work](bot-builder-basics.md) (including [what a turn is](bot-builder-basics.md#defining-a-turn)) and [managing state](bot-builder-concept-state.md).

Each dialog represents a conversational task that can run to completion and return collected information.
Each dialog represents a basic unit of control flow: it can begin, continue, and end; pause and resume; or be canceled.

You manage a dialog through its inputs and outputs. When the dialog starts, it receives a dialog option, as an optional argument. On each turn, the dialog is passed the current turn context, which includes the current activity. When the dialog ends, it can return a value. In effect, a bot can give control to a dialog, and a dialog can give control to another dialog, and so on.

## Dialog state

Dialogs are a way of implementing a _multi-turn conversation_, and as such, they rely on _persisted state_ across turns. Without state in dialogs, your bot wouldn't know where it was in the conversation or what information it had already gathered.

<!--A bot is inherently stateless.-->
To retain a dialog's place in the conversation, the dialog's state must be retrieved from and saved to memory each turn. This is handled via a dialog state property accessor defined on the bot's conversation state. This dialog state collects information for all active dialogs, and children of active dialogs.
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

> [!NOTE]
> As part of the introduction of adaptive dialogs, version 4.9 of the C# SDK introduced a _dialog manager_ class that automates many of the dialog management tasks for you.
> (Adaptive dialogs are in preview in version 4.10 of the JavaScript SDK.)
> For more information, see the [introduction to adaptive dialogs](bot-builder-adaptive-dialog-Introduction.md).
<!-- You can use a dialog manager to run any type of dialog. -->

## Dialog types

The dialogs library provides a few types of dialogs to make your bot's conversations easier to manage.

| Type | Description
| :--  | :--
| _dialog_ | The base class for all dialogs.
| _container dialog_ | The base class for all _container_ dialogs. It maintains an inner dialog set and allows you to treat a collection of dialogs as a unit.
| _component dialog_ | A general-purpose type of container dialog. When a component dialog starts, it begins with a designated dialog in its collection. When the inner process completes, the component dialog ends.
| _waterfall dialog_ | Defines a sequence of steps, allowing your bot to guide a user through a linear process. These are designed to work within the context of a component dialog.
| _prompt dialogs_ | Ask the user for input and return the result. A prompt will repeat until it gets valid input or it is canceled. They are designed to work with waterfall dialogs.
| _adaptive dialog_ | A type of container dialog that allows for flexible conversation flow. It includes built-in support for language recognition, language generation, and memory scoping features. To run an adaptive dialog (or another dialog that contains an adaptive dialog), you must start the dialog from a dialog manager.
| _action dialogs_ | Represent programmatic structures within an adaptive dialog. These let you design conversation flow much like expressions and statements in a traditional programming language let you design procedural flow in an application.
| _input dialogs_ | Ask the user for input, much like prompt dialogs, but can interact with other structures in an adaptive dialog.
| _skill dialog_ | Automates the management of one or more skill bots from a skill consumer.
| _QnA Maker dialog_ | Automates access to a QnA Maker knowledge base.

## Dialog patterns

There are two main patterns for starting and managing dialogs from a bot.

1. For adaptive dialogs, or any set of dialogs that contains an adaptive dialog, you need to create an instance of the dialog manager for your root dialog, and make your conversation state (and optionally your user state) available to the manager. For more information, see the [introduction to adaptive dialogs](bot-builder-adaptive-dialog-introduction.md) and how to [create a bot using adaptive dialogs](bot-builder-dialogs-adaptive.md).
1. For other dialogs, you can use the dialog manager or just use the root dialog's _run_ extension method. For information on using the run method with a component dialog, see [about component and waterfall dialogs](bot-builder-concept-waterfall-dialogs.md) and how to [implement sequential conversation flow](bot-builder-dialog-manage-conversation-flow.md).

### The dialog stack

A dialog context contains information about all active dialogs and includes a _dialog stack_, which acts as a _call stack_ for all the active dialogs.
Each container dialog has an inner set of dialogs that it is controlling, and so each active container dialog introduces an inner dialog context and dialog stack as part of its state.

While you will not access the stack directly, understanding that it exists and its function will help you understand how various aspects of the dialogs library work.

## Container dialogs

A container dialog acts as individual dialog and can be part of a larger dialog set. However, each container has an inner dialog set that is managed separately.

The SDK currently implements two types of container dialogs: component dialogs and adaptive dialogs.
While the conceptual structure of the two are quite different, they can be used together.

### Dialog IDs

When you add a dialog to a dialog set, you assign it an unique ID within that set. Dialogs within a set reference each other by their IDs.

When one dialog references another dialog at run time, it does so by the dialog's ID. The dialog context tries to resolve the ID based on the other dialogs in the immediate dialog set. If there is no match, it looks for a match in the containing or outer dialog set, and so on. If no match is found, an exception or error is generated.

### Component dialogs

Component dialogs use a sequence model for conversations, and each dialog in the container is responsible for calling other dialogs in the container. When the component dialog's inner dialog stack is empty, the component ends.

Consider using component and waterfall dialogs if your bot:

- Has a relatively simple control flow.
- Does not require a more flexible conversation model.

[About component and waterfall dialogs](bot-builder-concept-waterfall-dialogs.md)  describes component, waterfall, and prompt dialogs in more detail.

### Adaptive dialogs

Adaptive dialogs use a flexible model for conversations.
An adaptive dialog can be designed to end or remain active when its inner dialog stack is empty.
They offer several built-in capabilities, including interruption handling, attaching a recognizer to each dialog, using the language generation system, and more.
With adaptive dialogs, you can focus more on modeling the conversation and less on dialog mechanics.

An adaptive dialog is part of the dialogs library and works with all of the other dialog types.
You can easily build a bot that uses many dialog types.

Consider using adaptive dialogs if your bot:

- Requires the conversation flow to branch or loop based on business logic, user input, or other factors.
- Needs to adjust to context, accept information in an arbitrary order, or allow one conversational thread to pause for a side-conversation and then pick back up again.
- Needs context-specific language understanding models or needs to extract entity information from user input.
- Would benefit from custom input processing or response generation.

The [introduction to adaptive dialogs](bot-builder-adaptive-dialog-introduction.md) and the other adaptive dialog topics describe the features supported by adaptive dialogs: language recognition and language generation support, use of triggers and actions to model conversation flow, access to memory scopes, and so on.

## Other dialogs

The QnA Maker and skill dialogs can be used as stand-alone dialogs or as part of a collection of dialogs in a container.

### QnA Maker dialog

The QnA Maker dialog accesses a QnA Maker knowledge base and supports QnA Maker's follow-up prompt and active learning features.

- Follow-up prompts, also known as multi-turn prompts, allow a knowledge base to ask the user for more information before answering their question.
- Active learning suggestions allow the knowledge base to improve over time. The QnA Maker dialog supports explicit feedback for the active learning feature.

For information about QnA Maker, see how to [use QnA Maker to answer questions](bot-builder-howto-qna.md).

### Skill dialog

A skill dialog accesses and manages one or more skills.
The skill dialog posts activities from the parent bot to the skill bot and returns the skill responses to the user.

For information about skills, see the [skills overview](skills-conceptual.md).

## Next steps

> [!div class="nextstepaction"]
> [About middleware](bot-builder-concept-middleware.md)
