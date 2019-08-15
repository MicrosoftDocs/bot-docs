# [internal] Dialogs cheat sheet

## Terms, concepts, or artifacts to define

### [Bot Framework] dialogs library

1. The dialogs library provides a few built-in features, such as
    prompts and waterfall dialogs to make your bot conversation easier to manage.

### dialog

1. A conversational abstraction that encapsulates its own states. Each dialog is designed to perform a specific task and can break a conversation into smaller pieces. [example: waterfall]

    ...Dialogs are a central concept in the Bot Framework SDK, and provide a way to manage a conversation with the user. The Bot Framework V4 SDK Dialogs library offers waterfall dialogs, prompts and component dialogs as built-in constructs to model conversations via dialogs.

    ...A dialog guides the conversation, sometimes in response user input or other stimuli. Logically, a dialog can be thought of as composed of _turns_.

    ...This makes things more portable making each dialog piece adhere to the single responsibility principle.

    ...[In terms of a state machine]: initial state, continuation, final/accepting state.

    ...[In terms of the strategy pattern]

    - simple dialog
    - complex dialog

### _dialog_ class

1. The _dialog_ class defines the base abstraction for a dialog, which can be used to control dialogs "from the outside" (from a dialog context) in a consistent way. Each derived class can add additional semantics that affect how the dialog proceeds "from the inside". For example, a waterfall dialog describes a linear sequence of steps, a prompt describes a way to ask the user for a specific type of information, and a composite dialog encapsulates an inner dialog set.
    - Developers can derive from any of the _dialog_ classes to add their own internal semantics.
1. A dialog is controlled from a dialog context: begin, continue, end, resume, re-prompt.
1. At design-time, a dialog object describes the behavior of the dialog.
1. At run time, state for the dialog stack is managed by the _DialogContext_, which maintains a _stack_ property that stores state for each dialog on the stack.

### dialog set

1. A related set of dialogs that can all call each other.

    ...A hub, a way of tracking dialogs as a whole and a managing associated state.

### _dialog set_ class

1. Represents a collection of dialogs, such as prompts, waterfall dialogs, and other dialogs. (Each dialog in the set has a unique ID and can call each other.)
1. Used to generate a dialog context....

### dialog state

1. all state information for a specific dialog
1. as part of the dialog stack
1. the current state of a dialog [FSM-meaning]

### _dialog state_ class

1. Contains state information for all dialogs for a specific dialog context.
    - When you create a dialog set, you must provide a state property accessor that can access state for the dialog set.
    - When you create a dialog context from a dialog state, the state accessor retrieves dialog state from the turn context.

### dialog stack

1. _dialog stack_ property of the _DialogState_ class (state information for all active dialogs).
1. _stack_ property of the _DialogContext_ class.

### context

The data used by a bot that allows it to "pick up where it left off" each turn. Bots are stateless, so each turn context is reestablished.

1. turn context: Includes the bot's state plus information about the incoming activity.
1. dialog context: Includes the turn context, information about the dialog set in question, and the state of the dialog stack.
1. waterfall step context: Includes dialog context, plus

### _dialog context_ class

1. The context for the current turn with respect to the dialog stack.
1. Used to control dialogs and the dialog stack.

See also:
<a href="https://en.wikipedia.org/wiki/Context_(computing)" target="_blank">context</a>,

### _turn context_ class

### _waterfall step context_ class

### active dialog

### dialog step

1. Describes a possible processing state of the dialog.
1. In any given dialog turn, the dialog step defines how the dialog responds to input.

### dialog turn

1. The portion of a turn that is processed by a given dialog.
1. The dialog can be thought of as performing one or more steps during any given turn.

### dialog instance

### _dialog instance_ class

### dialog options

### calling context (parent context)

### parent dialog

### child dialog

### dialog result

### _component dialog_ class

1. Encapsulates a dialog set, defining a type of composite dialog.
    - When a component dialog is a member of an _outer_ dialog set, it can be thought of as a subroutine...
    - A component dialog defines an _inner_ dialog set at design-time. At run-time, this is managed as an _inner_ dialog context.

### _waterfall dialog_ class

1. Define [at design-time] a sequence of steps and pass information along to the next step.
    - Are good for simple linear tasks.
    - Can be combined with prompt dialogs to ask the user for information.
    - Can be combined with other waterfalls to create more complex flow, such as branches and loops.

### _prompt_ class

1. Encapsulates logic for asking the user for specific types of information, such as text, a number, or a date.
    - Attempts to parse, or otherwise interpret, user input as the desired type of information.
    - Can include additional validation criteria.
    - Will repeat until it can parse and validate the input.

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
