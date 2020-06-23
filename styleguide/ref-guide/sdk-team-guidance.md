# Reference documentation guidance

Contains guidance for the SDK team when adding reference documentation to the code.

## C\#

uses docXML

---

## JavaScript/TypeScript

- [JSDoc](#jsdoc)
- [TypeDoc](#typedoc)
- [JavaScript API Browser](#[javascript-api-browser])
- [Tags](#tags)
- [Files](#files)
- [Namespaces](#namespaces)
- [Reference primitive types](#reference-primitive-types)
- [Reference SDK-defined types](#reference-sdk-defined-types)
- [Additional information](additional-information)

---

The following recommendations are for developers and writers working on JavaScript/TypeScript reference (comment) documentation.

### JSDoc

Standard **JSDoc** conventions apply for **JavaScript** documentation. You can read more about them here: [JsDoc](https://jsdoc.app/).

### TypeDoc

**TypeDoc** conventions apply to **TypeScript** documentation (please, read [platform breakdown](https://review.docs.microsoft.com/en-us/help/onboard/admin/reference/concepts/platforms)). You can read more about them here: [TypeDoc](http://typedoc.org/guides/doccomments/).

### JavaScript API Browser

All JavaScript and TypeScript API documentation on *docs.microsoft.com* is indexed in the [JavaScript API Browser](https://review.docs.microsoft.com/en-us/javascript/api). This ensures that the customers have a single entry point to discover all possible JavaScript APIs.

## Tags

Tags are special words in comment descriptions that start with `@`. For example, when describing a function,
you must describe elements like parameters and return value in the comment section.
You use tags for identifying each elements.


### @param \<name>

Documents a method parameter specified by the name.

```javascript
/**
* Creates a new property accessor for reading and writing an individual
* property to the bot states storage object.
* @param T (Optional) type of property to create. Defaults to `any` type.
* @param name Name of the property to add.
* @return prop Property accessor.
*/
public createProperty<T = any>(name: string): StatePropertyAccessor<T> {
    const prop: BotStatePropertyAccessor<T> = new BotStatePropertyAccessor<T>(this, name);
    return prop;
}
```

### @remarks

Communicates important information about a type or a method.

```javascript
/**
 * Base class for the frameworks state persistance scopes.
 *
 * @remarks
 * This class will read and write state, to a provided storage provider,
 * for each turn of conversation with a user. Derived classes, like `ConversationState`
 * and `UserState`, provide a `StorageKeyFactory` which is  used to determine the key
 * used to persist a given storage object.
 *
 * The state object thats loaded will be automatically cached on the context object for the
 * lifetime of the turn and will only be written to storage if it has been modified.
 */
export class BotState implements PropertyManager {
    ......
}
```

### @return \<name>

Documents the object returned by the method.

```javascript
/**
* Asynchronously lists the members of the current conversation.
*
* @param context The context object for the turn.
*
* @returns An array of [ChannelAccount](xref:botframework-schema.ChannelAccount) objects for
* all users currently involved in a conversation.
*
* @remarks
* Returns an array of [ChannelAccount](xref:botframework-schema.ChannelAccount) objects for
* all users currently involved in a conversation.
*
* This is different from [getActivityMembers](xref:botbuilder.BotFrameworkAdapter.getActivityMembers)
* in that it will return all members of the conversation, not just those directly involved in a specific activity.
*/
public async getConversationMembers(context: TurnContext): Promise<ChannelAccount[]> {
if (!context.activity.serviceUrl) { throw new Error(`BotFrameworkAdapter.getConversationMembers(): missing serviceUrl`); }
if (!context.activity.conversation || !context.activity.conversation.id) {
    throw new Error(`BotFrameworkAdapter.getConversationMembers(): missing conversation or conversation.id`);
}
const serviceUrl: string = context.activity.serviceUrl;
const conversationId: string = context.activity.conversation.id;
const client: ConnectorClient = this.getOrCreateConnectorClient(context, serviceUrl, this.credentials);

return await client.conversations.getConversationMembers(conversationId);
}
```

### @typeparam \<name>

Documents a generic type parameter for the subsequent symbol specified by the param name.

```javascript
/**
 * Contains state information for an instance of a dialog on the stack.
 *
 * @typeparam T Optional. The type that represents state information for the dialog.
 *
 * @remarks
 * This contains information for a specific instance of a dialog on a dialog stack.
 * The dialog stack is associated with a specific dialog context and dialog set.
 * Information about the dialog stack as a whole is persisted to storage using a dialog state object.
 *
 * **See also**
 * - [DialogState](xref:botbuilder-dialogs.DialogState)
 * - [DialogContext](xref:botbuilder-dialogs.DialogContext)
 * - [DialogSet](xref:botbuilder-dialogs.DialogSet)
 * - [Dialog](xref:botbuilder-dialogs.Dialog)
 */
export interface DialogInstance<T = any> {
    /**
     * ID of this dialog
     *
     * @remarks
     * Dialog state is associated with a specific dialog set.
     * This ID is the the dialog's [id](xref:botbuilder-dialogs.Dialog.id) within that dialog set.
     *
     * **See also**
     * - [DialogState](xref:botbuilder-dialogs.DialogState)
     * - [DialogSet](xref:botbuilder-dialogs.DialogSet)
     */
    id: string;

    /**
     * The state information for this instance of this dialog.
     */
    state: T;

    /**
     * Hash code used to detect that a dialog has changed since the curent instance was started.
     */
    version?: string;
}
```

### @ignore

Keeps the subsequent from being documented.

```javascript
/**
* @ignore
* @private
* Returns the actual ClaimsIdentity from the JwtTokenValidation.authenticateRequest() call.
* @remarks
* This method is used instead of authenticateRequest() in processActivity() to obtain the ClaimsIdentity for caching in the TurnContext.turnState.
*
* @param request Received request.
* @param authHeader Received authentication header.
*/
private authenticateRequestInternal(request: Partial<Activity>, authHeader: string): Promise<ClaimsIdentity> {
    return JwtTokenValidation.authenticateRequest(
        request as Activity, authHeader,
        this.credentialsProvider,
        this.settings.channelService,
        this.authConfiguration
    );
}

```

<!--
I do not remember why we did this:

```javascript
 /**
     * Gets or sets an error handler that can catch exceptions in the middleware or application.
     *
     * @remarks
     * The error handler is called with these parameters:
     *
     * | Name | Type | Description |
     * | :--- | :--- | :--- |
     * | `context` | [TurnContext](xref:botbuilder-core.TurnContext) | The context object for the turn. |
     * | `error` | `Error` | The Node.js error thrown. |
     */
    public get onTurnError(): (context: TurnContext, error: Error) => Promise<void> {
        return this.turnError;
    }

```
-->

## Files

The comment describing a file must be placed before any code in the file.
It should be annotated with the `@packageDocumentation` tag so *TypeDoc* knows it is
the documentation for the file itself.

```javascript
// file.ts
/**
 * This is the doc comment for file.ts
 * @packageDocumentation
 */

```

## Namespaces

Namespaces (previously referred to as *modules*) can be commented like any other elements in *TypeScript*.
Namespaces can be defined in multiple files. TypeDoc selects the longest comment by default.
You can override this behavior with the special `@preferred` comment tag.

```javascript
/**
 * This the actual preferred namespace comment.
 * @preferred
 */
namespace MyModule { }
/**
 * This is the dismissed namespace comment.
 * This is the longer comment but will be dismissed in favor of the preferred comment.
 */
namespace MyModule { }

```

## Reference primitive types

You can find a list ot the TypeScript primitive types [here](https://www.typescriptlang.org/docs/handbook/basic-types.html).

### boolean

The following code sample shows how to refer to the primitive `boolean` type in the `@returns` comment tag.

```javascript
/**
* Checks if the service url is for a trusted host or not.
* @param  {string} serviceUrl The service url
* @returns {boolean} True if the host of the service url is trusted; False otherwise.
*/
public static isTrustedServiceUrl(serviceUrl: string): boolean {
    try {
        const uri: url.Url = url.parse(serviceUrl);
        if (uri.host) {
            return AppCredentials.isTrustedUrl(uri.host);
        }
    } catch (e) {
        // tslint:disable-next-line:no-console
        console.error('Error in isTrustedServiceUrl', e);
    }

    return false;
}

```

### string

The following code sample shows how to refer to the primitive `string` type in the `@param` comment tag.

```javascript
/**
 * Authenticate with username and password
 * @param {string} username - Username
 * @param {string} password - Password
 * @param {function} callback - The callback to handle the error and result.
 */
exports.authenticateWithUsernamePassword = function (username, password, callback) {
  var authorityUrl = authenticationParameters.authorityHostUrl + '/' + authenticationParameters.tenant;
  var resource = 'https://management.core.windows.net/';
  var context = new AuthenticationContext(authorityUrl);
  context.acquireTokenWithUsernamePassword(resource, authenticationParameters.username,
  authenticationParameters.password, authenticationParameters.clientId, function (err, tokenResponse) {
    if (err) {
      callback('Authentication failed. The error is: ' + util.inspect(err));
    } else {
      callback(null, tokenResponse);
    }
  });
};

```

## Reference SDK-defined types

The SDK-defined types are those defined in the Bot Framerk SDK libraries.
To link to auto-generated API reference pages, use XRef links with the **unique ID** (UID) of the type or member.
The following is the syntax to create a link:

```xml
<xref:UID>
<xref:UID?displayProperty=nameWithType>
```

> [!NOTE]
> By default, link text shows only the member or type name. The optional `displayProperty=nameWithType` query parameter produces fully qualified link text, that is, *namespace.type* for types, and *type.member* for type members, including enumeration type members.

You can use a markdown style if you want to change the text of the link itself as follows:

```markdown
[link custom text](xref:UID)
```

To find the UID for the API on `docs.microsoft.com`, type all or some of its full name in the  [JavaScript API Browser](https://review.docs.microsoft.com/en-us/javascript/api) search box. The UDI are displayed on the left side. The following picture shows an example, where the UIDs are in the red box:

![JS UIDs](../media/js-udis.PNG)

To test the UID is correct, enter the following in yor browser:

```http
    https://xref.docs.microsoft.com/query?uid=<UID to test>
```

If the UID is correct you will get a long string withe the `href ` of the reference documentation page. For example, if you enter this in your browser:

https://xref.docs.microsoft.com/query?uid=botbuilder-core.ConversationState.getStorageKey

You obtain this string:

```json
[{"uid":"botbuilder-core.ConversationState.getStorageKey","name":"getStorageKey(TurnContext)","href":"https://docs.microsoft.com/javascript/api/botbuilder-core/conversationstate#getstoragekey-turncontext-","tags":["/javascript","public"]}]
```

And if you enter the `href` value in your browser:

https://docs.microsoft.com/javascript/api/botbuilder-core/conversationstate#getstoragekey-turncontext-

You get the documentation page for `getStorageKey(TurnContext)`.


### Examples

> [!div class="mx-tdBreakAll"]
> |Type|Example|Link|Comments|
> |------|-----|------|-----|
> |Class|`[ConversationState](xref:botbuilder-core.ConversationState)`|[ConversationState](https://review.docs.microsoft.com/en-us/javascript/api/botbuilder-core/conversationstate?view=botbuilder-ts-latest&branch=master)|
> |Method|`[clear()](xref:botbuilder-core.ConversationState.clear)`|[ConversationState.clear](https://docs.microsoft.com/javascript/api/botbuilder-core/conversationstate#clear-turncontext-)| |
> |Interface|`[ChannelAccount](xref:botframework-schema.ChannelAccount)`|[ChannelAccount](https://docs.microsoft.com/javascript/api/botframework-schema/channelaccount)||
> |Method|`[getActivityMembers](xref:botbuilder.BotFrameworkAdapter.getActivityMembers)`|[BotFrameworkAdapter.getActivityMembers](https://docs.microsoft.com/javascript/api/botbuilder/botframeworkadapter#getactivitymembers-turncontext--string-)||

For more information on XRef links, see [XRef (cross reference) links](https://review.docs.microsoft.com/en-us/help/contribute/links-how-to?branch=master#xref-cross-reference-links).


## Additional information

### Errors
> [!div class="mx-tdBreakAll"]
> |Error Type|Error Example|Error Message|Solution|
> |-------------|----------|---------|---------|
> |Invalid link warning|`<a href=""#add"">add()</a>`|The file `docs-ref-autogen/botbuilder-dialogs/DialogSet.yml` doesn't contain a bookmark named `add`.|Replace the link `<a href=""#add"">add()</a>` with `[add()](xref:botbuilder-dialogs.DialogSet.add)`.|

### Good practices

- Add snippet code in comment example.

    \```JavaScript <br/>
    const { ConversationState, MemoryStorage } = require('botbuilder'); <br/>
    \```
- Do use `@remarks` tags.
- Do use `[<link-text>](xref:<link-uid>)` links.
- The UIDs are in the generated .yaml files.
  Or, search for the target using the [JavaScript API Browser](https://review.docs.microsoft.com/en-us/javascript/api).
- Don't use `[[ ]]` links. :stuck_out_tongue_closed_eyes:
- Don't use `@see` tags.    :stuck_out_tongue_closed_eyes:

---

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