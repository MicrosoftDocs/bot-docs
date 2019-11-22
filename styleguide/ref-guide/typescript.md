# <a id="top"></a>TypeScript ref doc guide

In this article:

- [Repos](#repos)
- [Tools (for local builds)](#tools-for-local-builds)
- [Cross-team coordination](#cross-team-coordination)
- [Markup and boilerplate](#markup-and-boilerplate)

## Repos

These repos are part of the JavaScript reference doc pipeline.

| Repo | Role |
| :--- | :--- |
| [Microsoft/BotBuilder-js](https://github.com/Microsoft/BotBuilder-js) | Source libraries for the JavaScript/TypeScript SDK. |
| [MicrosoftDocs/botbuilder-docs-sdk-typescript](https://github.com/MicrosoftDocs/botbuilder-docs-sdk-typescript) | Bucket for the intermediate files generated from the npm packages. |
| [MicrosoftDocs/bot-docs-pr](https://github.com/MicrosoftDocs/bot-docs-pr) | The larger Bot Framework doc set in which the ref docs are included. |

back to [top](#top)

## Tools (for local builds)

The [Onboarding guide][] has info on how to run local builds. For us, this currently requires:

- Globally install the [type2docfx][type2docfx-npm] package.

  ```cmd
  npm i -g type2docfx --save-dev
  ```

- Globally install the [typedoc][typedoc-npm] package.

  ```cmd
  npm i -g typedoc
  ```

- Download the [docfx][docfx-tool] tool from https://github.com/dotnet/docfx/releases

  This downloads a .zip file. Extract the contents to a directory you can easily find, such as **C:\\Program Files\\docfx\\**.

back to [top](#top)

## Cross-team coordination

### Before a release

- **Dev team** adds or updates code in the botbuilder-js repo.
- **Doc team** adds or updates code comments and submits PRs against the botbuilder-js repo.

### Pushing a release

- **Dev team** builds the npm packages and publishes them to the npm site.

  In theory, they update the list of [current JavaScript packages](https://github.com/Microsoft/botbuilder-js#packages) on the botbuilder-js README page.

### After a release

1. **Docs team** submits an onboarding request. (If we have a hard release date, we can submit this a day or two early to get it on their calendar.)
1. **Pubs team** processes the request. They'll ask us to check the review site to verify that everything looks right.
   - At this point, we can only fix doc build and package omission issues, as the dev team does not like to republish packages.
   - If there is an egregious ref doc error, we can probably add an overwrite (repo tbd: either bot-docs-pr or botbuilder-docs-sdk-typescript) and have them rebuild the docs.
   - Once we approve the review build, they will push the changes live.

back to [top](#top)

## Markup and boilerplate

We should only use [typeDoc][typeDoc docs] supported markup, though these sections can include Markdown. The only JSDoc markup that _should_ work that isn't covered by typeDoc is the `@remarks` tag. See [JSDoc usage][] and the [jsdoc repo][] for info for JSDoc.

See these resources for TypeScript and JavaScript:
- [TypeScript docs][]&mdash;an official TypeScript language reference.
- [MDN JavaScript docs][]&mdash;Mozilla developer network resources for JavaScript.
- [Node.js docs][]&mdash;Node.js documentation.

The rest of this sections covers style and wording guidance:

- [Symbol references](#symbol-references)
- [@module](#module)
- [Summary](#summary)
- [Object types](#object-types)
- [Modifiers](#modifiers)
- [@typeparam \<param-name>](#typeparam-param-name)
- [@param \<param-name>](#param-param-name)
- [@returns](#returns)
- [@remarks](#remarks)
- [Code blocks](#code-blocks)
- [General guidance](#general-guidance)

### Symbol references

Use `[link-text](xref:uid)` style links to link to other Bot Framework and Microsoft TypeScript ref docs.

To find the xref for JavaScript, go to https://docs.microsoft.com/en-us/javascript/api/ and search for the member you want to link to. The xref will be the link text, as opposed to the link target.
For example, the xref for the BotAdapter.use method is __botbuilder-core.BotAdapter.use__.

For links into our docs, link "directly" to the article, such as https://docs.microsoft.com/azure/bot-service/bot-builder-howto-send-messages for the **Send and receive text messages** how to.

For links to other docs, use an aka link as you would in the conceptual topics.

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

### @module

A `module` tag at the top of a code file should generate module-level documentation. Only one file should be used to document each module. In other words, choose one file per module to use for module-level comments.

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

### Summary

Every public element should have a description, including protected members of public classes or interfaces.

> [jf] When in doubt, open with a verb phrase. Include one or two more short sentences as necessary, but leave most of the explanation for the remarks section. The following are suggestions and based off of updates as I make them.

> [!NOTE] If a public element is _internal_, include the following note as part of the summary:
>
> ```text
> This <_element-type_> supports the framework and is not intended to be called directly for your code.
> ```

| Element | Wording |
| :--- | :--- |
| File                | TBD |
| Concrete class      | \<_Noun or verb phrase_>. _Such as,_ Provides context for a turn of a bot. _or_ A storage layer that uses an in-memory dictionary. |
| Interface           | \<_Verb phrase_>. _Such as,_ Contains settings used to configure a \[\<_class-name_>](xref:\<_class-uid_>) instance. |
| Abstract base class | Defines the core behavior of \<_class name or feature_> and provides a base for \<_derived classes or derivations_>. |
| Derived class or interface (extends or implements) | _The unlinked [bug] base class is included, but the interfaces are not in the build. **Do mention** all base classes and interfaces. Also, try to describe the class or interface in terms similar to how the base class or interface is described._<br/>_Such as,_ A \[\<_class-name_>](xref:\<_class-uid_>) that can connect a bot to a service endpoint. Implements \[\<interface-name_>](xref:\<interface-uid_>). |
| Generic class or interface | _If relevant, mention the generic type parameter as part of the overall summary._ |
| Constructor         | Creates an new instance of the \[\<_class-name_>](xref:\<_class-uid_>) class. |
| Constructor in abstract class | Called from constructors in derived classes to initialize the \[\<_class-name_>](xref:\<_class-uid_>) class. |
| Property            | \<_Noun or verb phrase_>. _Such as,_ The tenant to acquire the bot-to-channel token from. _or_ The ID assigned to you bot. |
| Function property, method, or function | \<_A verb phrase that describes the behavior_>. |
| `async` method/function | An asynchronous method/function that \<_verb phrase that describes the method behavior_>. |
| Generator function  | TBD: _haven't seen one in the framework...yet._ |
| Overloaded method   | _Include in the summary what makes this overload distinct from the others_. |
| Abstract or interface method | When implemented in a derived class, \<_verb phrase_>. |

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

### Object types

| Type | Notes |
| :--- | :--- |
| boolean        | _Summary like:_ Indicates whether \<_to do a thing_ or _a condition holds_>.<br/>_Remarks like:_ `true` \<_to do x_ or _when y_>; otherwise, `false`.<br/>_If the false condition requires more explanation, can say,_ ...; or `false` \<_if/when/to z_>. |
| number         |  |
| string         |  |
| array          |  |
| tuple          |  |
| enum           |  |
| any            |  |
| void           |  |
| null/undefined |  |
| never          |  |
| object         |  |
| interface      |  |
| class          |  |
| function       |  |
| async function |  |
| generic        | _Include a link to the generic type [for now], as links are not auto-generated in the docs._ |
| union          | _The object can have members that **any** of the `\|`'d types have. Call out how the different types are reconciled or what they represent._ |
| intersection   | _The object can have members that **all** of the `&`'d types have._ |
| optional       | _Effectively `\|`'d with `undefined`. Start with_ Optional. _Call out the default behavior in the absence of a value._ |
| default init   | _Call out what the default value is._ |
| mapped         |  |
| Partial\<T>    | _All members of T are optional members of Partial\<T>. Call out any members that need to be present._ |
| Readonly\<T>   | _All members of T are readonly members of Readonly\<T>._ |
| Symbol         |  |

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

### Modifiers

| Modifier | Notes |
| :--- | :--- |
| public     |  |
| private    |  |
| protected  |  |
| const      |  |
| readonly   |  |
| static     |  |
| instance   |  |
| optional   |  |
| abstract   |  |
| indexable  |  |
| accessors  |  |
| constant (enum) |  |
| ambient (enum)  |  |

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

### @typeparam \<param-name>

| Element | Wording |
| :--- | :--- |
|  |  |

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

### @param \<param-name>

| Element | Wording |
| :--- | :--- |
| [a mutated copy of another object] |  |
| [an object that will be mutated by the method] |  |
| object |  |
| boolean |  |
| number |  |
| string |  |
| array |  |
| tuple |  |
| enum |  |
| any |  |
| Partial<T> |  |
| union type |  |
| function |  |
| optional |  |
| default  |  |
| [**rest**](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions/rest_parameters) |  |
| this |  |
| parameter property |  |

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

### @returns

Currently, the doc build does not include a description along with the return value.

However, if a value is returned, make sure to describe it in the @remarks section.

| Element | Wording |
| :--- | :--- |
| void, null, undefined, or never or a Promise for any of these |  |
| this | For _cascading/fluent_ semantics, use:<br/>A reference to the \<_whatever_> object. |

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

### @remarks

| Element | Wording |
| :--- | :--- |
| in general |  |
| Method that can throw an exception |  |
| in general |  |

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

### Code blocks

Fenced code blocks default to JavaScript syntax highlighting, but it doesn't hurt to be explicit.

Add short code examples as desireable to the @remarks section.

- Introduce each example, even if it is just with the phrase, "For example:".
- At the type-level (class, interface, object, enum, and so on), include a code example that will apply to the most common use cases.
- At the method-level, consider adding code examples to the most commonly hit topics.

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

### General guidance

#### Event emitters and events

#### Continuation functions

#### Chaining semantics

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

<!-- Footnote links: Onboarding & ref doc refresh ----- -->

[Onboarding guide]: https://review.docs.microsoft.com/help/onboard/admin/reference?branch=master

<!-- Footnote links: local build tools ----- -->

[type2docfx-npm]: https://www.npmjs.com/package/type2docfx
[typedoc-npm]: https://www.npmjs.com/package/typedoc
[docfx-tool]: https://dotnet.github.io/docfx/index.html

<!-- Footnote links: style ----- -->

[Microsoft style guide]: https://docs.microsoft.com/en-us/style-guide/welcome/

<!-- Footnote links: Language and markup reference ----- -->

[TypeScript docs]: http://www.typescriptlang.org/docs/home.html
[MDN JavaScript docs]: https://developer.mozilla.org/en-US/docs/Web/JavaScript
[Node.js docs]: https://nodejs.org/en/docs/

[typeDoc docs]: https://typedoc.org/guides/doccomments
[JSDoc usage]: https://jsdoc.app/
[jsdoc repo]: https://github.com/jsdoc/jsdoc
