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

- The [type2docfx][type2docfx-npm] package
- The [typedoc][typedoc-npm] package
- The [docfx][docfx-tool] tool

back to [top](#top)

## Cross-team coordination

### Doc team

Before a release:

- Add or update code comments and submit PRs against the botbuilder-js repo.

### Dev team

Pushing a release:

- The dev team builds the npm packages and publishes them to the npm site.
- In theory, they update the list of [current JavaScript packages](https://github.com/Microsoft/botbuilder-js#packages) on the botbuilder-js README page.

### Pubs team

After a release:

1. We submit an onboarding request. (If we have a hard release date, we can submit this a day or two early to get it on their calendar.)
1. Once they process the request, they'll ask us to check the review site to verify that everything looks right.
   - At this point, we can only fix doc build and package omission issues, as the dev team does not like to republish packages.
   - If there is an egregious ref doc error, we can probably add an overwrite (repo tbd: either bot-docs-pr or botbuilder-docs-sdk-typescript) and have them rebuild the docs.
1. Once we approve the review build, they will push the changes live.

back to [top](#top)

## Markup and boilerplate

We should only use [typeDoc][typeDoc-comments] supported markup, though these sections can include Markdown.

See the [TypeScript handbook][] for an official TypeScript language reference.

Sections:

- [Code blocks](#code-blocks)
- [Symbol references](#symbol-references)
- [Summary](#summary)
- [@remarks](#remarks)
- [@typeparam \<param-name>](#typeparam-param-name)
- [@param \<param-name>](#param-param-name)
- [@returns](#returns)

### Code blocks

Fenced code blocks default to JavaScript syntax highlighting.

Add short code examples as desireable to the @remarks section.
- At the type-level (class, interface, object, enum, and so on), only include a code example if we expect it to apply to 80% or more of the typical use cases.
- At the method-level, consider adding code examples to the most commonly hit topics.

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

### Symbol references

Use `[link-text](xref:uid)` style links.

To find the xref for JavaScript, go to https://docs.microsoft.com/en-us/javascript/api/ and search for the member you want to link to. The xref will be the link text, as opposed to the link target.
For example, the xref for the BotAdapter.use method is __botbuilder-core.BotAdapter.use__.

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

### Summary

Every public element should have a description, including protected members of public classes or interfaces.

> [jf] When in doubt, open with a verb phrase. Include one or two more short sentences as necessary, but leave most of the explanation for the remarks section. The following are suggestions and based off of updates as I make them.

> [!NOTE] If a public element is _internal_, include the following sentence as part of the summary:
>
> `This method supports the framework and is not intended to be called directly for your code.`

| Element | Wording |
| :--- | :--- |
| File | TBD |
| General class or interface | \<_Verb phrase_>. _Such as:_ Represents..., Provides..., _or_ Contains..., _etc._ |
| Abstract base class | Defines the core behavior of \<_class name or feature_> and provides a base for \<_derived classes or derivations_>.
| Derived class or interface (extends or implements) | _This information is automatically included in the build. **Don't mention** the base class or interface unless there is something important to say about it. **However**, try to describe the class or interface in terms similar to how the base class or interface is described._ |
| Generic |  |
| Property | _Use a noun phrase, such as,_ The tenant to acquire the bot-to-channel token from. _or_ The ID assigned to you bot. |
| Optional property | Optional. \<_Noun phrase_>. |
| Function property |  |
| Constructor | Creates an new instance of the \[\<_blah_>](xref:\<_module.blah_>) class|structure. |
| Constructor in abstract class | Called from constructors in derived classes to initialize the \[\<_blah_>](xref:\<_module.blah_>) class. |
| Method or function | \<_A verb phrase that describes the method behavior_>. |
| `async` method or function | Asynchronously \<_verb phrase that describes the method behavior_>. |
| Generator function |  |
| Overloaded method | \<Include in the summary what makes this overload distinct from the others_>. |
| Interface method | When implemented, \<_verb phrase_>. |

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

### @remarks

| Element | Wording |
| :--- | :--- |
| in general |  |
| Method that can throw an exception |  |
| in general |  |

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
| default-initialized |  |
| [**rest**](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Functions/rest_parameters) |  |
| this |  |

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

### @returns

| Element | Wording |
| :--- | :--- |
| void |  |
| null or undefined |  |
| never |  |
|  |  |

back to [top](#top) > [Markup and boilerplate](#markup-and-boilerplate)

<!-- ----- Footnote links ----- -->

[Onboarding guide]: https://review.docs.microsoft.com/help/onboard/admin/reference?branch=master

[type2docfx-npm]: https://www.npmjs.com/package/type2docfx
[typedoc-npm]: https://www.npmjs.com/package/typedoc
[docfx-tool]: https://dotnet.github.io/docfx/index.html

[TypeScript handbook]: http://www.typescriptlang.org/docs/handbook/basic-types.html

[typeDoc-comments]: https://typedoc.org/guides/doccomments
