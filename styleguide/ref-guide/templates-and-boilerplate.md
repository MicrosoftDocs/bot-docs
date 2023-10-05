# Reference documentation - templates and boilerplate

- [Summaries](#summaries)
- [Fields](#fields)
- [Property values](#property-values)
- [Parameters](#parameters)
- [Return values](#return-values)
- [Errors and exceptions](#errors-and-exceptions)
- [Remarks](#remarks)

## Summaries

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

- [Reference documentation - templates and boilerplate](#reference-documentation---templates-and-boilerplate)
  - [Summaries](#summaries)
    - [Summary: types](#summary-types)
      - [Feature implementation across languages](#feature-implementation-across-languages)
      - [Summary: control-like types](#summary-control-like-types)
    - [Summary: constructors](#summary-constructors)
    - [Summary: fields](#summary-fields)
    - [Summary: methods](#summary-methods)
    - [Summary: operators](#summary-operators)
    - [Summary: properties](#summary-properties)
    - [Summary: abstract and virtual members](#summary-abstract-and-virtual-members)
    - [Summary: overloaded members](#summary-overloaded-members)
    - [Summary: enumeration-related](#summary-enumeration-related)
    - [Summary: event-related](#summary-event-related)
  - [Fields](#fields)
  - [Property values](#property-values)
  - [Parameters](#parameters)
  - [Return values](#return-values)
  - [Errors and exceptions](#errors-and-exceptions)
  - [Remarks](#remarks)

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

#### Feature implementation across languages

| Feature | C# | Java | JavaScript/TypeScript | Python |
| :-- | :-- | :-- | :-- | :-- |
| Abstract base classes | [Abstract Classes and Class Members](https://learn.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/abstract-and-sealed-classes-and-class-members#abstract-classes-and-class-members) | | [Abstract classes](https://www.typescriptlang.org/docs/handbook/classes.html#abstract-classes) | [PEP 3119](https://www.python.org/dev/peps/pep-3119/) |
| Delegates | | | | |
| Exceptions/errors | [Exceptions and Exception Handling](https://learn.microsoft.com/dotnet/csharp/programming-guide/exceptions/) | | [MDN>Error](https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Error) [Node.js>Errors](https://nodejs.org/dist/latest-v12.x/docs/api/errors.html) | [PEP 3109](https://www.python.org/dev/peps/pep-3109/) |
| Eventing | | | [Node.js>Events](https://nodejs.org/dist/latest-v12.x/docs/api/events.html) | |
| Generics | [Generics](https://learn.microsoft.com/dotnet/csharp/programming-guide/generics/) | | [Generics](https://www.typescriptlang.org/docs/handbook/generics.html) | [PEP 560](https://www.python.org/dev/peps/pep-0560/) |

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

| Item | Wording | Examples |
|------|---------|----------|
| Field | Begin with a present-tense third-person verb, such as *Specifies* or *Represents*. | Specifies that the message box should display Yes and No buttons<br>&mdash;<br>Represents the HKEY_CLASSES_ROOT registry key.|

### Summary: methods

For guidelines about **On**_\<Event>_ methods, see the [event-related](#summary-event-related) section. For guidelines about documenting overloaded methods, see [overloaded members](#summary-overloaded-members).

| Item | Wording | Examples |
|------|---------|----------|
| General method  | Begin with a present-tense third-person verb. | Displays XDO data in a scrollable grid.<br>&mdash;<br>Processes Windows messages that are currently in the message queue. |
| **Dispose** method, general overload | Releases the resources used by the current instance of the *\<class>* class. | Releases the resources used by the current instance of the *ComponentDesigner* class. |
| **Dispose()** method | Releases the resources used by the current instance of the *\<class>* class. | Releases the resources used by the current instance of the *Timer* class. |
| **Dispose(Boolean)** method | Called by the **Dispose()** and **Finalize()** methods to release the managed and unmanaged resources used by the current instance of the *\<class>* class.  | Called by the **Dispose()** and **Finalize()** methods to release the managed and unmanaged resources used by the current instance of the *DocumentDesigner* class. |
| <a name="exception"></a> Method that always throws an exception | Throws a/an *\<ExceptionType>* exception in all cases.<br /><br />**Note:**<br />In the Remarks section, explain why the member is not supported. | Throws a *NotSupportedException* exception in all cases. |
| Explicit interface method implementation | \<Copy from the interface member if appropriate> |

### Summary: operators

Read [overloaded members](#summary-overloaded-members) for guidelines about documenting overloaded operators.

| Item | Wording | Examples |
|------|---------|----------|
| Unary/binary operator; for example, + operator | Begin with a present-tense verb. | Adds a value to a *Unit*. |
| Conversion operator | Converts a *\<Type>* to a *\<Type>*.<br /><br />**Note:**<br />If one type is a primitive, use the language-neutral phrase for it. | Converts a decimal to a 32-bit signed integer. |

### Summary: properties

| Item | Wording | Examples |
|------|---------|----------|
| Read/write property or indexed property | *Boolean:*<br />Gets or sets a value that indicates whether *\<condition>*.<br /><br />*Other:*<br />Gets or sets *\<summary without specifying the type>*.  | Gets or sets a value that indicates whether the control can accept data that the user drags onto it.<br>&mdash;<br>Gets or sets the background color for the control. |
| Read-only property or indexed property | *Boolean:*<br />Gets a value that indicates whether *\<condition>*.<br /><br />Other:<br />Gets *\<summary without specifying the type>*.<br /><br />**Note:**<br />It isn't necessary to say "This property is read-only." | Gets a value that indicates whether the type is passed by reference.<br>&mdash;<br>Gets the default binder used by the system. |
| Explicit interface property implementation | \<Copy from the interface member if appropriate> | |

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

## Fields

## Property values

## Parameters

## Return values

<!-- continuation methods -->

## Errors and exceptions

## Remarks
