---
title: Dialogs within the Bot Builder SDK | Microsoft Docs
description: Describes what a dialog is and how it work within the Bot Builder SDK.
keywords: conversation flow, recognize intent, single turn, multiple turn, bot conversation, dialogs, prompts, waterfalls, dialog set
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 9/17/2018
monikerRange: 'azure-bot-service-4.0'
---

# Dialog library

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

Dialogs enable the bot developer to guide conversational flow using a predetermined sequence of operations. A dialog is similar to a function in a program, and can be invoked as often as it is needed. You can also connect multiple dialogs together to handle all the interactions that you want your bot to offer.

The **Dialog** library in the Bot Builder SDK includes built-in features such as _prompts_, _waterfall dialogs_, and _component dialogs_ to help you manage your bot's conversation. You can use prompts to ask users for different types of information, a waterfall to combine multiple steps together in a sequence, and component dialogs to package your dialog logic into separate classes that can then be integrated into multiple bots.

For more information about conversation flow, see [conversation flow](bot-builder-conversations.md) and [how to design and control conversation flow](../bot-service-design-conversation-flow.md).

## Dialog sets

A _dialog set_ object contains dialogs that the bot can call during the course of a conversation. When designing your conversation flow, you should add any dialogs required to a dialog set. Dialogs contained within the set may include any number of prompts, waterfall, and component dialogs. It is important to note that dialogs within a given set can only invoke other dialogs in that same set. It is possible to have multiple dialog sets in a single bot.

## Dialog state

When defining a dialog set, you must also specify the _state property accessor_ with which to track the _dialog state_. Since a user's position in a dialog is tied directly to the conversation, the dialog state accessor should be created off of _conversation state_. The dialog set uses the dialog state accessor to manage data specific to the active dialog and the _dialog stack_, which keeps track of the bot's active dialogs. When a dialog ends or the stack is cleared, the associated data is deleted.

For more information about state and storage, see [how to manage conversation and user state](bot-builder-howto-v4-state.md).

## Dialog stack and dialog context

When you add a dialog to the _dialog set_, you are defining a possible dialog that the bot can call on during a conversation. The dialog is not active until it is directly invoked and pushed onto the dialog stack, at which point it becomes the active dialog. The dialog remains active until it completes - either because it runs of of steps to process, or because _endDialog_ is called. When a dialog ends, it is popped off the stack, and control is returned to parent dialog or the bot's main turn handler. Though all dialogs on the stack are considered _active_, only the dialog on the top of the stack gets to process user input.

Before you can interact with the dialog stack and the dialogs in a dialog set, the bot's turn handler needs to create a _dialog context_ for the dialog set. You can use this dialog context object to start or continue a dialog, and it is also available to the currently running dialog. For more information, see [how to manage simple conversation flow with dialogs](bot-builder-dialog-manage-conversation-flow.md).

## Prompts

The **Dialog** library comes with a set of prompt types you can use to collect various types of user input. For example, to ask user for text input, you can use the **TextPrompt**; to ask user for a number, you can use the **NumberPrompt**; to ask for a date and time, you can use the **DateTimePrompt**. Prompts are a special type of dialog.

To use a prompt from a [waterfall dialog](#waterfall-dialogs), add both the waterfall and the prompt to the same dialog set. When a _waterfall step_ calls a prompt, the following operations occur:

1. The prompt is started and pushed onto the top of the stack, becoming the active dialog.
1. The prompt's prompt message is sent to the user.
1. The dialog context pauses and returns control to the bot's turn handler.
1. When the dialog continues on the next turn, the prompt tries to interpret the user's input, ensuring it matches the appropriate data type such as text, an number, or a date and time.
   - If the prompt can not interpret the input, or the prompt includes a validator and the validation fails, the user is re-prompted for input, and this step is repeated on the next turn.
   - Otherwise, the prompt exits and the next step of the waterfall begins, with the result of the prompt passed in as input.

Because of the nature of the prompt-response interaction, implementing a prompt requires at least two steps in a waterfall dialog - one to send the prompt, and a second to capture and process the response.  If you have an additional prompt, you can sometimes combine these by using a single function to first processing the user's response and then start the next prompt.

For examples of prompt types and how to use them in a waterfall dialog, see [how to prompt users for input using the Dialog library](bot-builder-prompts.md).

## Waterfall dialogs

A `WaterfallDialog` is a specific implementation of a dialog that is used to collect information from the user or guide the user through a series of tasks. The tasks are implemented as an array of functions where the result of the first function is passed as an argument into the next function, and so on. Each function typically represents one step in the overall process. At each step, the bot prompts the user for input, waits for a response, and then passes the results to the next step.

When you invoke a dialog, you can pass in options to the dialog, These options are available to the dialog's first step, and can be used to initialize the dialog.

When you end a dialog, you can return a value. If the dialog was started from within another waterfall step, then the value returned from the child dialog is available to the next step of the waterfall. If the dialog was started or continued using the dialog context, then the value is returned as part of the result to the calling code. For more information, see [how to manage simple conversation flow with dialogs](bot-builder-dialog-manage-conversation-flow.md).

### Waterfall step arguments

A _waterfall dialog_ is composed of a sequence of _waterfall steps_. Each step is an asynchronous delegate that takes a _waterfall step context_ (`step`) parameter.

A waterfall step context object includes the following properties:

- A _context_ that contains the current turn context.
- An _options_ object that contains any input passed in when the dialog was started.
- A _reason_ that indicates how control passed to this step of the dialog.
- A _result_ that contains the return value from the previous step.
- A _values_ collection in which you can persist dialog information between turns.

A waterfall step context object includes the following methods:

- A _begin dialog_ method for pushing a dialog onto the top of the _dialog stack_ and starting it.
- A _cancel all dialogs_ method to cancel all dialogs on the stack and clearing the stack.
- A _continue dialog_ method to continue the active dialog in the stack. In general, you would only call this from a bot's turn handler and not from within a waterfall step.
- An _end dialog_ method to end the current dialog and pop it from the stack.
- A _next_ method to move to the next step of the waterfall without pausing for input.
- A _prompt_ method to use a prompt to collect information from the user.
- A _replace dialog_ method to replace to dialog on the top of the stack. The old dialog is canceled and popped from the stack, and the new dialog is started and push onto the stack. You can replace a dialog with itself, which has the effect of starting over from the beginning.

### Waterfall step return values

Each waterfall step must return a _dialog turn result_ object. Except for the _cancel all dialogs_ method, all of the methods outlined for the step object return a _dialog turn result_ that you can use as the return value for that step.

You can also return the _end of turn_ object as a return value to end the current step and proceed with the next step on the next turn.

> [!IMPORTANT]
> If a step does not return an appropriate object, then the dialog may repeat the step indefinitely or throw an error.

How you exit the previous step of the waterfall determines how the result value is exposed in the subsequent step.

| The value returned by the previous step | The value of this step's _result_ property |
| :--- | :--- |
| The result of the _next_ method | The _result_ parameter from the call to the _next_ method. |
| The result of the _begin dialog_ method | The _result_ parameter from the child dialog's _end dialog_ method. |
| The result of the _prompt_ method | The object the prompt or the prompt's validator returns. |
| An _end of turn_ value | The user's input text for this turn. |

The _next_, _begin dialog_, _end dialog_, and _prompt_ methods are defined on the waterfall step context object as described in the [waterfall step arguments](#waterfall-step-arguments) section.

## Dialog results

When a dialog ends, you can optionally return a value. The _dialog turn result_ object contains the following members:

- *status*: This enumeration indicates the status of the dialog stack, including whether there are any _active dialogs_ and whether there is a return value.
  - _cancelled_ indicates that the dialog was cancelled and the stack is empty.
  - _complete_ indicates that the dialog completed successfully, the result is available, and the stack is empty.
  - _empty_ indicates that there is currently nothing on the dialog stack.
  - _waiting_ indicates that the dialog on top is waiting for a response from the user.
- *result*: When _status_ is _complete_, this property contains the value returned by the _end_ method of the final dialog that was on the stack.

> [!TIP]
> Prompts are implemented as dialogs, and their return values are handled in the same way as return values from other dialogs. However, if you are using a validator with your prompt, the validator may alter the return value.

You can handle a return value from a dialog either within a waterfall step in a dialog or from your bot's on turn handler.
Within a waterfall step, the dialog provides the return value in the waterfall step context's _result_ property.
You generally only need to check the status of the dialog turn result from your bot's turn logic.

### In your bot's on turn handler

Both the dialog context's _begin dialog_ and _continue dialog_ async methods return a dialog turn result value, for single-step dialogs and multi-step dialogs, respectively.

In a multi-step waterfall, you would capture the returned results from the dialog context's _continue dialog_ method, instead of from its _begin dialog_ method.

If your bot can start a number of different dialogs, you will to reconcile the returned information with the original process that you started. You could do this by setting a state flag in your bot or by checking the return type or type of information contained in the return value.

## Repeating a dialog

To repeat a dialog, use the *replace dialog* method. The dialog context's *replace dialog* method will pop the current dialog off the stack and push the replacing dialog onto the top of the stack and begin that dialog. You can use this method to create a loop by replacing a dialog with itself. Note that if you need to persist the internal state for the current dialog, you will need to pass information to the new instance of the dialog in the call to the _replace dialog_ method, and then initialize the dialog appropriately. The options passed into the new dialog can be accessed via the step context's _options_ property in any step of the dialog.

This is a great way to handle a complex conversation flow or to manage menus.

## Branch a conversation

The dialog context maintains a _dialog stack_ and for each dialog on the stack, tracks which step is next. Its _begin dialog_ method pushes a dialog onto the top of the stack, and its _end dialog_ method pops the top dialog off the stack.

A dialog can start a new dialog within the same dialog set by calling the dialog context's _begin dialog_ method and providing the ID of the new dialog, which then makes the new dialog the currently active dialog. The original dialog is still on the stack, but calls to the dialog context's _continue dialog_ method are only sent to the dialog that is on top of the stack, the _active dialog_. When a dialog is popped off the stack, the dialog context will resume with the next step of the waterfall on the stack where it left off of the original dialog.

Therefore, you can create a branch within your conversation flow by including a step in one dialog that can conditionally choose a dialog to start out of a set of available dialogs.

## Component dialog

The _component dialog_ class allows you to encapsulate one or more dialogs, and break some of your bot logic into separate classes that can then be shared between different bots or packaged for distribution. A component dialog class defines an internal _dialog set_ of its own, as well as one or more interlinked dialogs in that set. By defining dialogs as part of a component dialog, developers create a new scope in which dialogs are interpreted that is separate from the bot's main dialog set scope. This prevents dialogs from different components from colliding or interfering with one another.

The combined unit can be added into any bot's _dialog set_. Once it is added to the set, it can be invoked or popped off the stack just like any other dialogs in the set.

<!--TODO: Add this or another appropriate link when we know what it is.

For more information, see [Create an integrated set of dialogs](bot-builder-compositcontrol.md).

-->

## Next steps

> [!div class="nextstepaction"]
> [Use dialog library to gather user input](bot-builder-prompts.md)
