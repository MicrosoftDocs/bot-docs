# Reference documentation guidance

Contains guidance for the SDK team when adding reference documentation to the code.

Please add a spell check for code comments to the check-in process.

## C\#

[C# docXML guide]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/
[recommended docXML tags]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/recommended-tags-for-documentation-comments

The C# projects use docXML for reference documentation.
The [C# docXML guide][] and [recommended docXML tags][] describe how to use C# /// (docXML) comments.

See the public **Microsoft style guide** [reference documentation](https://docs.microsoft.com/style-guide/developer-content/reference-documentation) section for general guidance on documenting specific types and members.

### Markdown within docXML

You can use Markdown within docXML.

- Don't use Markdown headings in docXML.

### Markup guidance

Use `///`-style comments (and not `/** */`-style comments)  for docXML markup.

[c]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/code-inline
[code]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/code
[cref]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/cref-attribute
[example]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/example
[exception]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/exception
[inheritdoc]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/inheritdoc
[list]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/list
[paramref]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/paramref
[remarks]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/remarks
[see]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/see
[seealso]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/seealso
[typeparamref]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/typeparamref

| Task | Resolution
| :-- | :--
| Add a code sample | Use one or more docXML [`<code>`][code] tags within an [`<example>`][example] tag within the [`<remarks>`][remarks] section.
| Clone the documentation from an interface or base class | Use the [`<inheritdoc>`][inheritdoc] tag.
| Create a list | Use a docXML [`<list>`][list] tag or Markdown-style list interchangeably.
| Create a table | Use Markdown to create a table.
| Document an exception | Use a docXML [`<exception>`][exception] tag for each different type of exception thrown directly by the member.
| Link to other members | Use a docXML [`<see>`][see] or [`<seealso>`][seealso] tag to link to another member. The tag's [cref attribute][cref] identifies the link target. Links to targets within the SDK (and many within Microsoft) will render as hyperlinks. All other see- and seealso-links will render as plain text.
| Link to other targets | Use Markdown links.
| Reference a parameter | Use a docXML [`<paramref>`][paramref] tag.
| Reference a generic type parameter | Use a docXML [`<typeparamref>`][typeparamref] tag.
| Style text as code | Use docXML [`<c>`][c] tags or Markdown in-line code styling interchangeably.

### Style guidance

- In general, keep summaries to 1-3 sentences. Put any additional guidance in the remarks section.
- Document exceptions thrown directly from the member.
- Don't document exceptions thrown as a result of calling other members.

## JavaScript/TypeScript

uses a mix of TypeDoc and jsdoc

## Python

\<include a mini toc>

Here are recommendations for developers and writers working on Python reference comment content.

Follow the Sphinx syntax in [how to document a Python API](https://review.docs.microsoft.com/help/onboard/admin/reference/python/documenting-api). Failing to do so will possibly result in malformed code comments.

### To reference functions

To reference `sys.exc_info()`:
~~~python
    :param trace: the traceback information as returned by :func:`sys.exc_info`.
~~~

### To reference types

#### To reference built-in types

#### To reference SDK-defined types

When referencing a type in the same package, you can leave off the package name:
~~~python
    :param storage: The storage layer this state management object will use to store and retrieve state
    :type storage:  :class:`Storage`
~~~

When referencing a type in a different package, you need to include the package name:
~~~python
    :param storage: The storage layer this state management object will use to store and retrieve state
    :type storage:  :class:`botbuilder.core.Storage`
~~~

### Common errors

#### When linking to methods, do not add () to the end of function names within the `:func:` tag.

**good**
~~~python
    :param start_time: the start time of the request. The value should look the
    same as the one returned by :func:`datetime.isoformat` (defaults to: None)
~~~

**bad**
~~~python
    :param start_time: the start time of the request. The value should look the
    same as the one returned by :func:`datetime.isoformat()` (defaults to: None)
~~~

#### Do not add `:param x:` without adding a `:type x:`

**good**
~~~python
    :param name: The name for this request. All requests with the same name will be grouped together.
    :type name: str
    :param url: The actual URL for this request (to show in individual request instances).
    :type url: str
~~~
**bad**
~~~python
    :param name: The name for this request. All requests with the same name will be grouped together.
    :param url: The actual URL for this request (to show in individual request instances).
~~~

#### Do not add a list of `:param x:`, `:type x`:, `:return:`, or `:rtype:` blocks without descriptions.

  This will result in malformed method/class summaries.

### Samples

See the OPS Onboarding Guide's **How to document a Python API** for a good sample of how to use a bunch of the Sphinx markup together, [Fully formatted code file](https://review.docs.microsoft.com/en-us/help/onboard/admin/reference/python/documenting-api?branch=master#fully-formatted-code-file).

Here are a few more examples:

~~~python
class BotState(PropertyManager):
    """
    Defines a state management object and automates the reading and writing of
    associated state properties to a storage layer.

    .. remarks::
        Each state management object defines a scope for a storage layer.
        State properties are created within a state management scope, and the Bot Framework
        defines these scopes: :class:`ConversationState`, :class:`UserState`, and :class:`PrivateConversationState`.
        You can define additional scopes for your bot.
    """
~~~
~~~python
    def __init__(self, storage: Storage, context_service_key: str):
        """
        Initializes a new instance of the :class:`BotState` class.

        :param storage: The storage layer this state management object will use to store and retrieve state
        :type storage:  :class:`bptbuilder.core.Storage`
        :param context_service_key: The key for the state cache for this :class:`BotState`
        :type context_service_key: str

        .. remarks::
            This constructor creates a state management object and associated scope. The object uses
            the :param storage: to persist state property values and the :param context_service_key: to cache state
            within the context for each turn.

        :raises: It raises an argument null exception.
        """
        self.state_key = "state"
        self._storage = storage
        self._context_service_key = context_service_key
~~~
~~~python
    def create_property(self, name: str) -> StatePropertyAccessor:
        """
        Creates a property definition and registers it with this :class:`BotState`.

        :param name: The name of the property
        :type name: str
        :return: If successful, the state property accessor created
        :rtype: :class:`StatePropertyAccessor`
        """
        if not name:
            raise TypeError("BotState.create_property(): name cannot be None or empty.")
        return BotStatePropertyAccessor(self, name)
~~~

## Java

uses javaDoc