---
title: About component and waterfall dialogs
description: Describes what component, waterfall, and prompt dialogs are and how they work within the Bot Framework SDK.
keywords: conversation flow, bot conversation, component dialog, waterfall dialog, prompt dialog, dialog set
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: azure-ai-bot-service
ms.date: 08/01/2022
monikerRange: 'azure-bot-service-4.0'
ms.custom:
  - evergreen
---

# About component and waterfall dialogs

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Dialogs come in a few different types. This article describes component, waterfall, and prompt dialogs.
For information about dialogs in general, see the [dialogs library](bot-builder-concept-dialog.md) article. For information about adaptive dialogs, see the [introduction to adaptive dialogs](bot-builder-adaptive-dialog-introduction.md).

A _waterfall dialog_ (or waterfall) defines a sequence of steps, allowing your bot to guide a user through a linear process.
These dialogs are designed to work within the context of a _component dialog_.

A component dialog is a type of container dialog that allows dialogs in the set to call other dialogs in the set, such as a waterfall dialog calling prompt dialogs or another waterfall dialog.
Component dialogs manage a set of _child_ dialogs, such as waterfall dialogs, prompts, and so on.
You can design a component dialog to handle specific tasks and reuse it, in the same bot or across multiple bots.

_Prompt dialogs_ (prompts) are dialogs designed to ask the user for specific types of information, such as a number, a date, or a name, and so on.
Prompts are designed to work with waterfall dialogs in a component dialog.

## Component dialogs

Sometimes you want to write a reusable dialog that you want to use in different scenarios, such as an address dialog that asks the user to provide values for street, city and zip code.

The _component dialog_ provides a strategy for creating independent dialogs to handle specific scenarios, breaking a large dialog set into more manageable pieces. Each of these pieces has its own dialog set, and avoids any name collisions with the dialog set that contains it. For more information, see the [component dialog how to](bot-builder-compositcontrol.md).

## Waterfall dialogs

A waterfall dialog is a specific implementation of a dialog that is commonly used to collect information from the user or guide the user through a series of tasks. Each step of the conversation is implemented as an asynchronous function that takes a _waterfall step context_ (`step`) parameter. At each step, the bot [prompts the user for input](bot-builder-prompts.md) (or can begin a child dialog, but that it's often a prompt), waits for a response, and then passes the result to the next step. The result of the first function is passed as an argument into the next function, and so on.

The following diagram shows a sequence of waterfall steps and the stack operations that take place. Details on the use of the dialog stack are below in the [using dialogs](#using-dialogs) section.

:::image type="content" source="media/bot-builder-dialog-concept.png" alt-text="Representation of how messages map to waterfall steps.":::

Within waterfall steps, the context of the waterfall dialog is stored in its _waterfall step context_. The step context is similar to the dialog context and provides access to the current turn context and state. Use the waterfall step context object to interact with a dialog set from within a waterfall step.

You can handle a return value from a dialog either within a waterfall step in a dialog or from your bot's on turn handler, although you generally only need to check the status of the dialog turn result from your bot's turn logic.
Within a waterfall step, the dialog provides the return value in the waterfall step context's _result_ property.

### Waterfall step context properties

The waterfall step context contains the following properties:

- _Options_: contains input information for the dialog.
- _Values_: contains information you can add to the context, and is carried forward into subsequent steps.
- _Result_: contains the result from the previous step.

Additionally, the _next_ method (**NextAsync** in C#, **next** in JavaScript and Python) continues to the next step of the waterfall dialog within the same turn, enabling your bot to skip a certain step if needed.

## Prompts

Prompts, within the dialogs library, provide an easy way to ask the user for information and evaluate their response. For example for a _number prompt_, you specify the question or information you're asking for, and the prompt automatically checks to see if it received a valid number response. If it did, the conversation can continue; if it didn't, it will reprompt the user for a valid answer.

Behind the scenes, prompts are a two-step dialog. First, the prompt asks for input; second, it returns the valid value, or starts from the top with a reprompt.

Prompts have _prompt options_ given when the prompt is called, which is where you can specify the text to prompt with, the retry prompt if validation fails, and choices to answer the prompt. In general, the prompt and retry prompt properties are activities, though there's some variation on how this is handled in different programming languages.

Additionally, you can choose to add some custom validation for your prompt when you create it. For example, say we wanted to get a party size using the number prompt, but that party size has to be more than 2 and less than 12. The prompt first checks to see if it received a valid number, then runs the custom validation if it's provided. If the custom validation fails, it will reprompt the user as above.

When a prompt completes, it explicitly returns the resulting value that was asked for. When that value is returned, we can be sure it has passed both the built-in prompt validation and any additional custom validation that may have been provided.

For examples on using various prompts, take a look at how to use the [dialogs library to gather user input](bot-builder-prompts.md).

### Prompt types

Behind the scenes, prompts are a two-step dialog. First, the prompt asks for input; second, it returns the valid value, or restarts from the top with a reprompt. The dialogs library offers various basic prompts, each used for collecting a different type of response. The basic prompts can interpret natural language input, such as "ten" or "a dozen" for a number, or "tomorrow" or "Friday at 10am" for a date-time.

| Prompt | Description | Returns |
|:----|:----|:----|
| _Attachment prompt_ | Asks for one or more attachments, such as a document or image. | A collection of _attachment_ objects. |
| _Choice prompt_ | Asks for a choice from a set of options. | A _found choice_ object. |
| _Confirm prompt_ | Asks for a confirmation. | A Boolean value. |
| _Date-time prompt_ | Asks for a date-time. | A collection of _date-time resolution_ objects. |
| _Number prompt_ | Asks for a number. | A numeric value. |
| _Text prompt_ | Asks for general text input. | A string. |

To prompt a user for input, define a prompt using one of the built-in classes, such as the _text prompt_, and add it to your dialog set. Prompts have fixed IDs that must be unique within a dialog set. You can have a custom validator for each prompt, and for some prompts, you can specify a _default locale_.

### Prompt locale

The locale is used to determine language-specific behavior of the _choice_, _confirm_, _date-time_, and _number_ prompts. For any given input from the user, if the channel provided a _locale_ property in user's message, then that is used. Otherwise, if the prompt's _default locale_ is set, by providing it when calling the prompt's constructor or by setting it later, then that is used. If neither of those locales are provided, English ("en-us") is used as the locale.

The locale is a two, three, or four character ISO 639 code that represents a language or language family.

### Prompt options

The second parameter of the step context's _prompt_ method takes a _prompt options_ object, which has the following properties.

| Property | Description |
| :--- | :--- |
| _Prompt_ | The initial activity to send the user, to ask for their input. |
| _Retry prompt_ | The activity to send the user if their first input didn't validate. |
| _Choices_ | A list of choices for the user to choose from, for use with a choice prompt. |
| _Validations_ | Additional parameters to use with a custom validator. |
| _Style_ | Defines how the choices for a choice prompt or confirm prompt will be presented to a user. |

You should always specify the initial prompt activity to send to the user, and a retry prompt for instances when the user's input doesn't validate.

If the user's input isn't valid, the retry prompt is sent to the user; if there was no retry specified, then the initial prompt is used. However, if an activity is sent back to the user from within the validator, no retry prompt is sent.

#### Prompt validation

You can validate a prompt response before returning the value to the next step of the waterfall. A validator function has a _prompt validator context_ parameter and returns a Boolean, indicating whether the input passes validation.
The prompt validator context includes the following properties:

| Property | Description |
| :--- | :--- |
| _Context_ | The current turn context for the bot. |
| _Recognized_ | A _prompt recognizer result_ that contains information about the user input, as processed by the recognizer. |
| _Options_ | Contains the _prompt options_ that were provided in the call to start the prompt. |

The prompt recognizer result has the following properties:

| Property | Description |
| :--- | :--- |
| _Succeeded_ | Indicates whether the recognizer was able to parse the input. |
| _Value_ | The return value from the recognizer. If necessary, the validation code can modify this value. |

## Using dialogs

Dialogs can be thought of as a programmatic stack, which we call the _dialog stack_, with the turn handler as the one directing it and serving as the fallback if the stack is empty. The top-most item on that stack is considered the _active dialog_, and the dialog context directs all input to the active dialog.

When a dialog begins, it's pushed onto the stack, and is now the active dialog. It remains the active dialog until it either ends, it's removed by the [replace dialog](#repeating-a-dialog) method, or another dialog is pushed onto the stack (by either the turn handler or active dialog itself) and becomes the active dialog. When that new dialog ends, it's popped off the stack and the next dialog down becomes the active dialog again. This allows for [repeating a dialog](#repeating-a-dialog) or [branching a conversation](#branch-a-conversation), discussed below.

You can begin or continue a root dialog using the _run_ dialog extension method. From the bot code, calling the dialog run extension method either continues the existing dialog, or starts a new instance of the dialog if the stack is currently empty. Control and user input goes to the active dialog on the stack.

The run method requires a _state property accessor_ to access the dialog state. The accessor is created and used the same way as other state accessors, but is created as it's own property based off of the conversation state. Details on managing state can be found in the [managing state topic](bot-builder-concept-state.md), and usage of dialog state is shown in the [sequential conversation flow](bot-builder-dialog-manage-conversation-flow.md) how-to.

From within a dialog, you have access to the dialog context and can use it to start other dialogs, end the current dialog, and perform other operations.

### To start a dialog

From within a waterfall dialog, pass the _dialog ID_ of the dialog you want to start into the waterfall dialog's context using either the _begin dialog_, _prompt_, or _replace dialog_ method.

- The prompt and begin dialog methods will push a new instance of the referenced dialog onto the top of the stack.
- The replace dialog method will pop the current dialog off the stack and push the replacing dialog onto the stack. The replaced dialog is canceled and any information that instance contained is disposed of.

Use the _options_ parameter to pass information to the new instance of the dialog.
The options passed into the new dialog can be accessed via the step context's _options_ property in any step of the dialog.
For more information, see how to [Create advanced conversation flow using branches and loops](bot-builder-dialog-manage-complex-conversation-flow.md).

### To continue a dialog

Within a waterfall dialog, use the step context's _values_ property to persist state between turns.
Any value added to this collection in a previous turn is available in subsequent turns.
For more information, see how to [Create advanced conversation flow using branches and loops](bot-builder-dialog-manage-complex-conversation-flow.md).

### To end a dialog

Within a waterfall dialog, use the _end dialog_ method to end a dialog by popping it off the stack. The _end dialog_ method can return an optional result to the parent context (such as the dialog that called it, or the bot's turn handler). This is most often called from within the dialog to end the current instance of itself.

You can call the end dialog method from anywhere you have a dialog context, but it will appear to the bot that it was called from the current active dialog.

> [!TIP]
> It's best practice to explicitly call the _end dialog_ method at the end of the dialog.

### To clear all dialogs

If you want to pop all dialogs off the stack, you can clear the dialog stack by calling the dialog context's _cancel all dialogs_ method.

### Repeating a dialog

You can replace a dialog with itself, creating a loop, by using the _replace dialog_ method.
This is a great way to handle [complex interactions](~/v4sdk/bot-builder-dialog-manage-complex-conversation-flow.md) and one technique for managing menus.

> [!NOTE]
> If you need to persist the internal state for the current dialog, you'll need to pass information to the new instance of the dialog in the call to the _replace dialog_ method, and then initialize the dialog appropriately.

### Branch a conversation

The dialog context maintains the dialog stack and for each dialog on the stack, tracks which step is next. Its _begin dialog_ method creates a child and pushes that dialog onto the top of the stack, and its _end dialog_ method pops the top dialog off the stack. _End dialog_ is usually called from within the dialog that's ending.

A dialog can start a new dialog within the same dialog set by calling the dialog context's _begin dialog_ method and providing the ID of the new dialog, which then makes the new dialog the currently active dialog. The original dialog is still on the stack, but calls to the dialog context's _continue dialog_ method are only sent to the dialog that is on top of the stack, the _active dialog_. When a dialog is popped off the stack, the dialog context will resume with the next step of the waterfall on the stack where it left off of the original dialog.

Therefore, you can create a branch within your conversation flow by including a step in one dialog that can conditionally choose a dialog to start out of a set of available dialogs.

## Additional information

- For more about adaptive dialogs, see the [introduction to adaptive dialogs](bot-builder-adaptive-dialog-Introduction.md).
- For information about skills, see [about skills](skills-conceptual.md).
