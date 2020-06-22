# <a id="top"></a>C# ref doc guide

In this article:

- [Repos](#repos)
- [Tools (for local builds)](#tools-for-local-builds)
- [Cross-team coordination](#cross-team-coordination)
- [Markup and boilerplate](#markup-and-boilerplate)

## Repos

These repos are part of the C# reference doc pipeline.

| Repo | Role |
| :--- | :--- |
| [Microsoft/BotBuilder-dotnet](https://github.com/Microsoft/BotBuilder-dotnet) | Source libraries for the C# SDK. |
| [MicrosoftDocs/bot-docs-pr](https://github.com/MicrosoftDocs/bot-docs-pr) | The **\\dotnet** directory is the bucket for the intermediate files generated from the NuGet packages. |
| [MicrosoftDocs/bot-docs-pr](https://github.com/MicrosoftDocs/bot-docs-pr) | The larger Bot Framework doc set in which the ref docs are included. |

back to [top](#top)

## Package repositories

The main sources for NuGet packages are NuGet (stable/official) and [BotBuilder.MyGet](https://botbuilder.myget.org/gallery/) (daily).

## Tools (for local builds)

The [Onboarding guide][] has info on how to run local builds, see [How to Test a .NET-based API Reference Project Locally](https://review.docs.microsoft.com/help/onboard/admin/reference/dotnet/testing-locally?branch=master).

Tools:

- [nue](https://github.com/docascode/nue/)&mdash;optional, to extract assemblies from NuGet packages.
- [mdoc](https://github.com/mono/api-doc-tools/)&mdash;to create a reflected API representation from the assemblies.
- [ECMA2YAML](https://github.com/docascode/ECMA2Yaml)&mdash;optional, to convert from mdoc to YAML.
- [DocFX](https://github.com/dotnet/docfx/releases)&mdash;to generate a local version of the ref docs.

## Cross-team coordination

### Before an SDK release

- **Dev team** adds or updates code in the botbuilder-js repo.
- **Doc team** adds or updates code comments and submits PRs against the botbuilder-dotnet repo.

### Pushing an SDK release

- **Dev team** builds the NuGet packages and publishes them to the NuGet site.

  In theory, they update the list of [current C# packages](https://github.com/Microsoft/BotBuilder-dotnet#packages) on the botbuilder-dotnet README page.

### After an SDK release

1. **Docs team** submits an onboarding request. (If we have a hard release date, we can submit this a day or two early to get it on their calendar.)
1. **Pubs team** processes the request. They'll ask us to check the review site to verify that everything looks right.
   - At this point, we can only fix doc build and package omission issues, as the dev team does not like to republish packages.
   - If there is an egregious ref doc error, we can probably add an overwrite (in the bot-docs-pr repo) and have them rebuild the docs.
   - Once we approve the review build, they will push the changes live.

back to [top](#top)

## Markup and boilerplate

The [C# docXML guide][] and [recommended docXML tags][] describe how to use C# /// (docXML) comments.

See the public [Microsoft style guide][] [reference documentation](https://docs.microsoft.com/style-guide/developer-content/reference-documentation) section for general guidance on documenting specific types and members.
See the [.NET API docs wiki][] for more specific guidance and boilerplate.

<!-- Writing and onboarding guides -->

[Microsoft style guide]: https://docs.microsoft.com/en-us/style-guide/welcome/
[Docs contributor guide]: https://review.docs.microsoft.com/en-us/help/contribute/?branch=master
[Onboarding guide]: https://review.docs.microsoft.com/help/onboard/admin/reference?branch=master

<!-- local build tools -->

<!-- Language and markup reference ----- -->

[C# docXML guide]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/
[recommended docXML tags]: https://docs.microsoft.com/dotnet/csharp/programming-guide/xmldoc/recommended-tags-for-documentation-comments
[.NET API docs wiki]: https://github.com/dotnet/dotnet-api-docs/wiki
