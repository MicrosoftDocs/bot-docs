# Reference documentation - templates and boilerplate

- [Summaries](#summaries)
- [Parameters](#parameters)
- [Return values](#return-values)
- [Property values](#property-values)
- [Errors and exceptions](#errors-and-exceptions)
- [Remarks](#remarks)

## Summaries

- [Types](#summary-types)
- [Constructors](#summary-constructors)
- [Fields](#summary-fields)
- [Methods](#summary-methods)
- [Operators](#summary-operators)
- [Properties](#summary-properties)
- [Abstract and virtual members](#summary-abstract-and-virtual-members)
- [Overloaded members](#summary-overloaded-members)
- [Enumeration-related](#summary-enumeration-related)
- [Event-related](#summary-event-related)

The summary succinctly states what a type or member does or represents. Every public element should have a description, including protected members of public classes or interfaces, that conveys the importance of the class or member.

1. If a public element is _internal_ but still visible, include the following note as part or the whole of the summary.

    > This _\<element-type>_ supports the framework and is not intended to be called directly from your code.

1. Begin with a present-tense, third-person verb, except for exception classes, enum members, abstract members, and virtual members (see the links in the next section).

    - Do not merely repeat the wording of the member name in the summary; provide a meaningful description and context. For example, instead of describing the String.Format method by saying Formats a string, you can use a description like this: Replaces each format item in a specified string with the string representation of a specified object.
    - Use programming language-neutral text. For example, don't include Visual Basic or C#-specific terms in your summary.
    - Avoid using special formatting such as lists that might cause problems for IntelliSense builds.
    - Avoid using parameter names or self-referential type or member names in the summary. There are exceptions; for example, you can use the type name in summaries for constructors and for dispose methods.
    - For **overloaded** constructors and methods, provide a general summary that is broad enough to apply to all the overloads, and write more specific summaries for the individual overloads. For the individual overloads, use wording that differentiates each overload from the others, and provide enough information to help users select the overload they'd like to call.  
    This requires that the code comment language allows you to add overload topics. There may be a way to do this in YAML after the fact, but we probably don't want to introduce that headache.
    - When in doubt, open with a verb phrase. Include one or two more short sentences as necessary, but leave most of the explanation for the remarks section.

### Summary: types

| Item | Wording | Example |
| :--- | :--- | :--- |
| In general | _begin with a present-tense third-person verb._ | Displays XDO data in a scrollable grid.<br>&mdash;<br>Represents a time interval. |
| Abstract or interface | _begin with_ `Defines`, `Provides`, or `Represents`. | Defines a generalized type-specific comparison method that a value type or class implements to order or sort its instances.<br>&mdash;<br>Provides functionality to format the value of an object into a string representation. |
| Generic type | _If relevant, mention the generic type parameter as part of the overall summary._ | Defines a generalized method that a value type or class implements to create a type-specific method for determining equality of instances.<br>&mdash;<br>Encapsulates a method that has two parameters and returns a value of the type specified by the `TResult` parameter. |
| Exception or error class | The exception/error that is thrown when/for/by _\<condition>_. | The exception that is thrown when the value of an argument is outside the allowable range of values as defined by the invoked method.<br>&mdash;<br>The exception that is thrown for a Win32 error code.<br>&mdash;<br>The exception that is thrown by methods invoked through reflection. |
| Delegates | Represents/Encapsulates a/the method that will _\<verb phrase>_. | Represents the method that will handle an event that has no event data.<br>&mdash;<br>Encapsulates a method that has two parameters and returns a value of the type specified by the `TResult` parameter. |
| Sealed or static class | _\<Typical summary, followed by...>_ This class cannot be inherited.  | Represents an application domain, which is an isolated environment where applications execute. This class cannot be inherited. |

For **derived** types, try to use wording similar to that of the base/parent class/interface.

#### Summary: control-like types

For types that represent a _standard_ conversational UI elements, such as dialogs, prompts, suggested actions, cards, and so on, we can borrow constructions from GUI interface descriptions. For example, we could use something like the following:

| Class | Summary |
| :-- | :-- |
| _dialog_ | Defines the base class for dialogs, which guide the user through a series of steps. |
| _waterfall dialog_ | A dialog that guides the user through an ordered sequence of steps. |
| _prompt_ | Defines the base class for prompts, which prompt the user for specific types of information. |
| _text prompt_ | A prompt that gathers text input from the user. |

### Summary: constructors

See [overloaded member summaries](#summary-overloaded-members) for how to word constructor overloads.

| Item | Wording | Examples |
| :--- | :--- | :--- |
| Constructor, concrete class/struct | Initializes a new instance of the _\<class/struct>_ class/struct. | Initializes a new instance of the `WebProxy` class.<br>&mdash;<br>Initializes a new instance of the `SqlInt64` struct using the supplied long integer. |

### Summary: fields

### Summary: methods

### Summary: operators

### Summary: properties

### Summary: abstract and virtual members

<dl>
<dt>abstract type</dt>
<dd>A type in a nominative type system that cannot be instantiated directly, as opposed to a <i>concrete</i> type.</dd>
<dd>Interfaces (and their members) are inherently abstract.</dd>
<dt>abstract method</dt>
<dd>A pure abstract method is one with only a signature and no implementation body.</dd>
<dd>Abstract methods are implicitly virtual (at least in C#).</dd>
<dt>virtual method</dt>
<dd>An inheritable and overridable function/method in which the implementation in derived types is used (where provided).</dd>
</dl>

| Item | Wording | Examples |
| :--- | :--- | :--- |
| Abstract method | When implemented in a derived class, _\<followed by what would normally go in the method summary>_. | When implemented in a bot, handles an incoming activity. |
| Virtual method | Override this in a derived class to _\<followed by what would normally go in the method summary>_. | Override this in a derived class to provide logic specific to `EndOfConversation` activities, such as the conversational logic. |

### Summary: overloaded members

| Item | Wording | Examples |
| :--- | :--- | :--- |
|  |  |  |

### Summary: enumeration-related

| Item | Wording | Example |
| :--- | :--- | :--- |
| Enum type | _Begin with a present-tense third-person verb, such as Specifies, Identifies, Defines, or Describes._ | Specifies the application elements on which it is valid to apply an attribute.<br>&mdash;<br>Describes the action that caused a `CollectionChanged` event. |
| A _flags_ enum type | _Include a second paragraph that reads something like:_<br><br>This enumeration allows bitwise combination of its member values. | Specifies the application elements on which it is valid to apply an attribute.<br><br>This enumeration has a `FlagsAttribute` attribute that allows a bitwise combination of its member values. |
| Enum member | _Noun (or verb) phrase. Begin with an introductory article, if appropriate.<br><br>Each member of a given enumeration should use similar construction and make sense in the context of the enumeration type's summary._ | Cultures that are specific to a country/region.<br>&mdash;<br>Compare strings using culture-sensitive sort rules and the invariant culture. |
| Enum member used as a mask | A mask used to retrieve _\<noun phrase>_. | A mask used to retrieve the scope of a method. |

### Summary: event-related

The C# SDK doesn't use traditional C#-style events; however, it has event-like classes and constructs. The JavaScript SDK uses an event emitter pattern in a few places, similar to emitters in Node.js.

Some _\<on X>_ methods are now typical event handlers, even in this broader sense. For instance _TurnContext.OnSendActivities_ adds a _SendActivitiesHandler_ to an internal list of such handlers.

| Item | Wording | Examples |
| :--- | :--- | :--- |
| _\<On X>_ or _\<Handle X>_ methods | Handles _\<whatever the event-like thing is>_. | Override this in a derived class to handle an incoming activity.<br>&mdash;<br>Handles user input and attempts to parse it. |

## Parameters

## Return values

## Property values

## Errors and exceptions

## Remarks
