---
title: Handling interruptions in adaptive dialogs
description: describes the concept of handling interruptions when collecting user input in adaptive dialogs
keywords: bot, user, interruptions, consultation, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 07/27/2020
monikerRange: 'azure-bot-service-4.0'
---

# Handling interruptions in adaptive dialogs

[!INCLUDE [applies-to-v4](../includes/applies-to.md)]

When you build a conversation flow that prompts for user input, you need to decide what should happen when they respond in a way that takes the conversation in a different direction. It is natural in human conversations to temporarily discuss a different topic before returning to the original subject and being able to seamlessly handle scenarios where the user interrupts your conversation flow is an important aspect of a robust bot.

> [!IMPORTANT]
> Handling interruptions in adaptive dialogs is made possible using the Bot Framework SDK's _consultation_ mechanism which enables a dialog to consult its parent dialog. This consultation mechanism also enables [Flexible entity extraction](#flexible-entity-extraction) and [Confirmation and correction](#confirmation-and-correction), which will also be discussed in this article.

## Prerequisites

- [Introduction to adaptive dialogs][introduction]
- [Asking for user input in adaptive dialogs][inputs]
- A basic understanding of [Language Understanding][language-understanding] especially the [intent][intents] and [entity][entities] concepts.

## Building a conversational flow

Suppose you are building a bot to take orders for a local coffee shop.  You might build a conversational flow that looks something like this:

> **User**: I'd like to order a coffee.
>
> **Bot**: What type of coffee would you like?
>
> **User**: Espresso.
>
> **Bot**: What size would you like?
>
> **User**: Make it a large, please.
>
> **Bot**: No problem, a large espresso will be ready in 5 minutes.

What happens if, when you asked what type of coffee, you don't get an answer, but rather a question? This is one way that the user can interrupt your conversational flow. consider the following example:

> **User**: I'd like to order a coffee.
>
> **Bot**: What type of coffee would you like?
>
> **User**: What types of coffee do you have?

If you haven't anticipated user interruptions, assuming some basic level of error handling, the conversation might go something like this:

> **User**: I'd like to order a coffee.
>
> **Bot**: What type of coffee would you like?
>
> **User**: What types of coffee do you have?
>
> **Bot**: Sorry, I didn't understand your response.
>
> **Bot**:  What type of coffee would you like?

The Bot Framework SDK provides an answer to this dilemma using _interruptions_. Interruptions is a technique available in the Bot Framework SDK that enables the bot to understand and respond to user input that does not directly pertain to the specific piece of input that was requested.

### Examples of interruptions to a conversational flow

Unlike a form with a predefined set of fields that a user would fill in, a bot enables the user to enter anything into an input field and a robust bot will need to be able to figure out how to respond appropriately to a wide range of user inputs.  Interruptions is one tool that enables your bot to respond to unexpected user inputs, or input that do not fit directly with the conversational flow of a given dialog. Consider the following scenarios:

- A bot that does flight bookings
   - being asked the temperature forecast for the destination in the middle of booking a flight.
   - The user requesting to add a car rental in the final flight booking confirmation.
- Being asked what time the shop opens in the middle of ordering a coffee.
- Asking for a medical or legal definition then returning to complete the series of questions.
- A request for _help_ in the middle of any conversation.
- A request to _cancel_ in the middle of any conversation.
- Starting a process over from the beginning when in the middle of a set of steps.

## Interruptions

Interruptions can be handled locally within a dialog as well as global by re-routing to another dialog. You can use interruption as a technique to:

- Detect and handle user's response as a locally relevant intent within the scope of the active dialog, meaning the dialog that prompted the user for information.
- Detect that a different dialog would be better suited to handle the user input and then using the Bot Framework SDK's consultation mechanism to enable a different dialog handle the user input.

> [!NOTE]
> Need a concise definition for the Bot Framework SDK's consultation mechanism.

By default adaptive dialogs do this in response to all user inputs:

- Run the recognizer configured on the adaptive dialog that contains the input action
- Evaluate the `AllowInterruptions` expression.
   - If **true**, evaluate the triggers in the parent adaptive dialog and execute the actions of any triggers that match, then issue a re-prompt when the input action resumes.
   - If **false**, evaluate the value property and assign it as a value to the property. If null run the internal entity recognizer for that input action (e.g. number recognizer for number input etc) to resolve a value for that input action.

### The AllowInterruptions property

`AllowInterruptions` is a property of the `InputDialog` class, which all inputs derive from, so it is available when [Asking for user input in adaptive dialogs][inputs]. The `AllowInterruptions` property is a `BoolExpression` which can be either a boolean (_true/false_) or an [adaptive expression][adaptive-expressions] which resolves to a boolean.

The `AllowInterruptions` property is defined when you create your [inputs][inputs] and defaults to true. Some examples:

| `AllowInterruptions` property                       | Explanation                                                                                         |
| --------------------------------------------------- | --------------------------------------------------------------------------------------------------- |
| AllowInterruptions = true                           | Allow interruptions in this input.                                                                  |
| AllowInterruptions = false                          | Do not allow interruptions in this input.                                                           |
| AllowInterruptions = "turn.recognized.score <= 0.7" | Allow interruptions only if we do not have a high confidence classification of an intent.           |
| AllowInterruptions = "turn.recognized.score >= 0.9 \|\| !@personName" | Allow interruptions only if you have a high confidence classification of an intent, or when you do not get a value for the `personName` entity.  |

> [!TIP]
>
> `personName` is a prebuilt entity provided by [LUIS](https://www.luis.ai/) that detects people names. It is very useful because it is already trained, so you don't need to add example utterances containing `personName` to your bots intents. For more information on using prebuilt entities in in your bot see the [.lu file format](/file-format/bot-builder-lu-file-format.md).

### Handling interruptions locally

You can handle interruptions locally by adding `OnIntent` triggers to match the possible intents. Consider this example:

> **User**: I'd like to order a coffee.
>
> **Bot**: What type of coffee would you like?
>
> **User**: Espresso.
>
> **Bot**: What size would you like?
>
> **User**: What sizes do you have?
>
> **Bot**: We offer the following espresso sizes:    <br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
> Tall $2.95    <br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
> Grande $3.65  <br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
> Venti	$4.15
>
> **User**: Grande.
>
> **Bot**: No problem, a Grande espresso will be ready in 5 minutes.

### Handling interruptions globally

Adaptive dialogs have a _consultation_ mechanism which enables a user input that cannot be handled by the active dialog to be passed up to that dialogs parent dialog and if the parent cannot handle the user input it gets passed up to the parents parent dialog all the way up to the root dialog until it reaches a dialog that has a trigger that fires. If no triggers fire using this consultation mechanism, the active action gets the user input back to decide what to do next. Consider this example:

> **User**: I'd like to order a coffee.
>
> **Bot**: What type of coffee would you like?
>
> **User**: What types of coffee do you have?

The _coffee order_ dialog is interrupted and the _coffee types_ dialog executes to respond to the users question.

> **Bot**: We offer a variety of hot coffee drinks including Freshly Brewed Coffee, Caffe Latte, Caffe Mocha, White Chocolate Mocha, and flat white.

The _coffee types_ dialog responds to the users question and returns control back to the _coffee order_ dialog to complete the order.

> **Bot**:  What type of coffee would you like?
>
> **User**: A large flat white, please.
>
> **Bot**: No problem, a large flat white will be ready in 5 minutes.

## Flexible entity extraction

Whenever an adaptive dialog begins, any options passed into the dialog via `BeginDialog` become properties of that dialog and can be accessed as long as it is in [scope][dialog-scope]. You access these properties by name: `dialog.<propertyName>`.

You can design your bot to use these values when present, and prompt the user when they are not. If you have a property associated with your input action, and s value for that property is passed in via `BeginDialog`, the user will not be prompted for it. There are situations when you may need to prompt the user for that information even when the property is not `null`. In these situations you can set `AlwaysPrompt` to `true`.

_Flexible entity extraction_ enables you to take the first non-null value, using `coalesce`, a prebuilt function available as part of [Adaptive Expressions][adaptive-expressions].

## Confirmation and correction

_Confirmation and correction_ enables the scenario where you ask the user for confirmation, and they not only provide a confirmation, but they also include input that includes additional user intents in their confirmation response.

## Additional information

- To learn more about expressions see the article [Adaptive expressions][adaptive-expressions].

[introduction]:bot-builder-adaptive-dialog-introduction.md
[inputs]: bot-builder-concept-adaptive-dialog-Inputs.md
[language-understanding]: bot-builder-concept-adaptive-dialog-recognizers#language-understanding
[intents]: bot-builder-concept-adaptive-dialog-recognizers#intents
[entities]: bot-builder-concept-adaptive-dialog-recognizers#entities
[adaptive-expressions]: bot-builder-concept-adaptive-expressions.md
[dialog-scope]: ../adaptive-dialog/adaptive-dialog-prebuilt-memory-states.md#dialog-scope