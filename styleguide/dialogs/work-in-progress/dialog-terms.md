# [internal] Dialogs cheat sheet

## Terms, concepts, or artifacts to define

### [Bot Framework] dialogs library

1. Provides a few built-in features to make your bot conversation easier to manage. For example, prompts and waterfall dialogs, component dialogs, and more in preview.

### dialog

1. Abstracts a conversation and encapsulates its own states. Each dialog is designed to perform a specific task and can break a conversation into smaller pieces. [example: waterfall]
    - A [relatively] _simple dialog_ is one that might ask a series of questions and then reply with an answer or acknowledgement, without branching or other control logic.
    - A [relatively] _complex dialog_ is one that can progress in multiple ways, such as spawning child dialogs, and so on.

    ...Dialogs are a central concept in the Bot Framework SDK, and provide a way to manage a conversation with the user.

    ...A dialog guides the conversation, sometimes in response user input or other stimuli. Logically, a dialog can be thought of as composed of _turns_.

    ...This makes things more portable making each dialog piece adhere to the single responsibility principle.

    ...[In terms of a [state machine (wikipedia)](https://en.wikipedia.org/wiki/Finite-state_machine)]: initial state, continuation, final/accepting state.

    ...[In terms of the [strategy pattern (wikipedia)](https://en.wikipedia.org/wiki/Strategy_pattern)]: using a choice or a LUIS intent to trigger a dialog.

### _dialog_ class

1. Defines the base abstraction for a [dialog](#dialog). This _contract_ allows dialogs to be controlled "from the outside" (from a [dialog context](#dialog-context)) in a consistent way. Each derived class can add additional semantics that affect how the dialog proceeds "from the inside".
    - A dialog is controlled from a dialog context using these operations: **begin**, **continue**, **end**, **resume**, and **re-prompt**.
    - For example, a [_waterfall dialog_](#waterfall-dialog-class) describes a linear sequence of steps, a [_prompt_](#prompt-class) describes a way to ask the user for a specific type of information, and a [component dialog_](#component-dialog-class) encapsulates an inner dialog set.
    - Developers can derive from any of the _dialog_ classes to add their own internal semantics.
1. At design time, a dialog object describes the behavior of the dialog.
1. At run time, state for the [dialog stack](#dialog-stack) is managed by a [_dialog context_](#dialog-context-class) object, which maintains a _stack_ property that stores state for each dialog on the stack.

### dialog set

1. Contains a related set of [dialogs](#dialog) that can all call each other.

    ...A hub, a way of tracking dialogs as a whole and a managing associated state.

### _dialog set_ class

1. Represents a collection of [_dialog_](#dialog-class) objects, such as prompts, waterfall dialogs, and other dialogs. (Each dialog in the set has a unique ID and can call each other.)
1. Used to generate a [_dialog context_](#dialog-context-class) that can be used to control the dialogs in the [dialog set](#dialog-set).

### dialog state

1. Represents all state information for a specific [dialog set](#dialog-set).
1. Represents the [dialog stack](#dialog-stack).
1. [In terms of a [state machine (wikipedia)](https://en.wikipedia.org/wiki/Finite-state_machine)]: The current state of a dialog [set].

### _dialog state_ class

1. Contains state information for all [_dialogs_](#dialog-class) for a specific [_dialog context_](#dialog-context-class).
    - When you create a [_dialog set_](#dialog-set-class), you must provide a state property accessor that can access state for the dialog set.
    - When you create a dialog context from a dialog state, the state accessor retrieves dialog state from the turn [context](#context).

### dialog stack

1. Represents state information for all "active" dialogs, similar to a [call stack(wikipedia)](https://en.wikipedia.org/wiki/Call_stack).
    - The _dialog stack_ property of the [_dialog state_](#dialog-state-class) class.
    - The _stack_ property of the [_dialog context_](#dialog-context-class) class.
1. Contains a list of [_dialog instance_](#dialog-instance-class) objects.

### context

The data used by a bot that allows it to "pick up where it left off". Bots are stateless, so context has to be reestablished each turn. See [context (wikipedia)](https://en.wikipedia.org/wiki/Context_(computing))].

1. turn context: Includes the bot's state plus information about the incoming activity.
1. dialog context: Includes the turn context, information about the [dialog set](#dialog-set) in question, and the state of the [dialog stack](#dialog-stack).
1. waterfall step context: Includes dialog context, plus context established as part of being a waterfall: step index, reason, result [from previous step], options [the dialog was started with], and values ["local" to the waterfall].

### _turn context_ class

1. The [context](#context) for the current bot turn.

### _dialog context_ class

1. The [context](#context) for the current [dialog turn](#dialog-turn) with respect to the dialog stack.
1. Used to control [_dialogs_](#dialog-class) and the [dialog stack](#dialog-stack).

### _waterfall step context_ class

1. The [context](#context) for the current step of a [_waterfall dialog_](#waterfall-dialog-class).

### active dialog

1. The [dialog instance](#dialog-instance) on the top of the [dialog stack](#dialog-stack), that is the one that will most immediately handle a turn if the [dialog context](#dialog-context) is instructed to continue.
    ...This would be **the** active dialog, the one on top of the stack, the one that will be invoked when the dialog context gets a call to _continue dialog_.
1. Any dialog instance on the dialog stack. Each dialog on the stack is active, in that it will eventually handle a turn once the dialogs above it on the stack complete, unless they are all cancelled.
    ...This would **an** active dialog, one that has not yet completed and is still on the stack, just no the one on top.

### dialog step

1. Describes a possible processing state of a [dialog](#dialog).
1. In any given [dialog turn](#dialog-turn), the dialog step defines how the dialog responds to input.

### dialog turn

1. The portion of a turn that is processed by a given dialog.
1. The [dialog](#dialog) can be thought of as performing one or more [dialog steps](#dialog-step) during any given turn.

### dialog instance

1. As opposed to a [dialog](#dialog) which describes its overall behavior, a _dialog instance_ contains state information for an [active dialog](#active-dialog).
    - This allows the bot to manage (save and restore) state for the [dialog stack](#dialog-stack) each turn.

### _dialog instance_ class

### dialog options

### calling context

1. See [parent context](#parent-context).

### parent context

### parent dialog

### child dialog

### dialog result

### _component dialog_ class

1. Encapsulates a [_dialog set_](#dialog-set-class), defining a type of composite [_dialog_](#dialog-class).
    - When a component dialog is a member of an _outer_ dialog set, it can be thought of as a subroutine...
    - At design time, a component dialog defines an _inner_ dialog set.
    - At run time, this is managed via an _inner_ [_dialog context_](#dialog-context-class).

### _waterfall dialog_ class

1. Defines a [_dialog_](#dialog-class) that's composed of a linear series of _waterfall steps_.
1. At design time, defines a sequence of [steps](#dialog-step) and how information is passed along to the next step.
    - Are good for simple linear tasks.
    - Can be combined with [_prompts_](#prompt-class) to ask the user for information.
    - Can be combined with other waterfalls to create more complex flow, such as branches and loops.

### _prompt_ class

1. Defines a two-step [_dialog_](#dialog-class) designed to collect and validate user input.
1. Encapsulates logic for asking the user for specific types of information, such as text, a number, or a date.
    - Attempts to parse, or otherwise interpret, user input as the desired type of information.
    - Can include additional validation criteria.
    - Will repeat until it can parse and validate the input.

### _adaptive dialog_ class

### prompt validator

### interrupt

1. In the context of the design of a bot or [dialog](#dialog), any activity that is allowed to "interrupt" the normal flow of the conversation. For example, the phrase "cancel" could be used to _interrupt_ the conversation and reset it to a starting state.
    ...Three broad types of interrupt:
    - cancel: terminate part or all of the current processing.
    - pause: begin some new process wherein the old one will continue upon the new one's completion.
    - interject: do a one-turn operation (such as providing help) and then continue with the original process on the next turn.

### iterruptable dialog

1. A derived dialog class designed to check for [interrupts](#interrupt) before continuing.

...and other terms...

## Interactions and relationships

Composite dialogs, as relates to the "outer" dialog set, of which it's a member, and to the "inner" dialog set, which it encapsulates.

- Examples: _ComponentDialog_, _ContainerDialog_, and _AdaptiveDialog_ all encapsulate an "inner" dialog set.

Dialog sets, as relates to its member dialogs

Dialog stacks, as relates to the active and "inactive" dialogs on the stack

Dialog context, as relates to other dialog contexts (they can be nested).

- This happens for component dialogs, and probably for adaptive dialogs, too.

...other relationships...
