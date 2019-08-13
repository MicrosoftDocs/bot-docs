# [internal] Dialogs cheat sheet

## Terms, concepts, or artifacts to define

### [Bot Framework] dialogs library

1. The dialogs library provides a few built-in features, such as
    prompts and waterfall dialogs to make your bot's conversation easier to manage.

### dialog

1. A conversational abstraction that encapsulates its own states. Each dialog is designed to perform a specific task and can break a conversation into smaller pieces. [example: waterfall]

    ...Dialogs are a central concept in the Bot Framework SDK, and provide a way to manage a conversation with the user. The Bot Framework V4 SDK Dialogs library offers waterfall dialogs, prompts and component dialogs as built-in constructs to model conversations via dialogs.

    ...A dialog guides the conversation, sometimes in response user input or other stimuli. Logically, a dialog can be thought of as composed of _turns_.

    ...This makes things more portable making each dialog piece adhere to the single responsibility principle.

    ...[In terms of a state machine]: initial state, continuation, final/accepting state.

    ...[In terms of the strategy pattern]

    - simple dialog
    - complex dialog

### dialog set

1. A related set of dialogs that can all call each other.

    ...A hub, a way of tracking dialogs as a whole and a managing associated state.

### _DialogSet_ class

1. Represents a collection of dialogs, such as prompts, waterfall dialogs, and other dialogs. (Each dialog in the set has a unique ID and can call each other.)
1. Used to generate a dialog context....

### _Dialog_ class

1. Base, abstract implementation of a dialog that allows it to be controlled from a dialog context: begin, continue, end, resume, re-prompt.
    - Base class for waterfall, composite, prompts, adaptive dialogs, and so on.
        ...Each derived type of dialog can (and often does) add additional internal semantics.
    - Developers can derive from any of the _dialog_ classes to add their own internal semantics.
1. At design-time, a dialog object describes the behavior of the dialog.
1. At run time, state for the dialog stack is managed by the _DialogContext_, which maintains a _stack_ property that stores state for each dialog on the stack.

### _DialogState_ class

1. Contains state information for all dialogs for a specific dialog context.
    - When you create a dialog set, you must provide a state property accessor that can access state for the dialog set.
    - When you create a dialog context from a dialog state, the state accessor retrieves dialog state from the turn context.

### dialog stack

1. _dialog stack_ property of the _DialogState_ class (state information for all active dialogs).
1. _stack_ property of the _DialogContext_ class.

### dialog state

1. all state information for a specific dialog
1. as part of the dialog stack
1. the current state of a dialog [FSM-meaning]

### _DialogContext_ class

1. The context for the current turn with respect to the dialog stack.
1. Used to control dialogs and the dialog stack.

    See also:
    - <a href="https://en.wikipedia.org/wiki/Context_(computing)" target="_blank">context</a>
    - [turn context](#turn-context)
    - [dialog context](#dialog-context)
    - [waterfall step context](#waterfall-step-context)

### turn context

### dialog context

### waterfall step context

### active dialog

### inactive dialog

### _DialogInstance_ class

### dialog step

### dialog instance

### dialog options

### calling context (parent context)

### parent dialog

### child dialog

### dialog result

### dialog turn

### _ComponentDialog_ class

1. Encapsulates a dialog set, defining a type of composite dialog.
    - When a component dialog is a member of an _outer_ dialog set, it can be thought of as a subroutine...
    - A component dialog defines an _inner_ dialog set at design-time. At run-time, this is managed as an _inner_ dialog context.

### _WaterFallDialog_ class

1. Define [at design-time] a sequence of steps and pass information along to the next step.
    - Are good for simple linear tasks.
    - Can be combined with prompt dialogs to ask the user for information.
    - Can be combined with other waterfalls to create more complex flow, such as branches and loops.

### _Prompt\<T>_ class

1. Encapsulates logic for asking the user for specific types of information, such as text, a number, or a date.
    - Attempts to parse, or otherwise interpret, user input as the desired type of information.
    - Can include additional validation criteria.
    - Will repeat until it can parse and validate the input.

### prompt dialog

### prompt validator

### interrupt

1. In the context of the design of a bot or dialog, any activity that is allowed to "interrupt" the normal flow of the conversation. For example, if the phrase "cancel" could be used to _interrupt_ the conversation and reset it to a starting state.
    - cancel, pause, interject

### iterruptable dialog

1. A derived dialog class designed to check for interrupts before continuing.

...and other terms...

## Interactions and relationships

Composite dialogs, as relates to the "outer" dialog set, of which it's a member, and to the "inner" dialog set, which it encapsulates.

- Examples: _ComponentDialog_, _ContainerDialog_, and _AdaptiveDialog_ all encapsulate an "inner" dialog set.

Dialog sets, as relates to its member dialogs

Dialog stacks, as relates to the active and "inactive" dialogs on the stack

Dialog context, as relates to other dialog contexts (they can be nested).

- This happens for component dialogs, and probably for adaptive dialogs, too.

...other relationships...
