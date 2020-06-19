# Reference documentation guidance

Contains guidance for the SDK team when adding reference documentation to the code.

## C\#

uses docXML

## JavaScript/TypeScript

uses a mix of TypeDoc and jsdoc

## Python

uses Sphinx

Here are recommendations for developers and writers working on Python reference comment content:

- Follow the syntax in [how to document a Python API](https://review.docs.microsoft.com/help/onboard/admin/reference/python/documenting-api).
  Failing to do so will possibly result in malformed code comments.
- When linking to methods, do not add () to the end of function names. Function names should be documented in the form:
- Do not add `:param _x_:` without adding a `:type _x_:`
- Do not add a list of `:param _x_:`, `:type _x_`:, `:return:`, or `:rtype:` blocks without descriptions. This will result in malformed method/class summaries.

Here's an example:

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