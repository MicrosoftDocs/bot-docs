# Writing guidelines for Azure Bot Service “conceptual” content

## Existing resources [for writers]

- **Microsoft style guide** (public)<br/>
  https://docs.microsoft.com/en-us/style-guide/welcome/

- **Microsoft docs contributor's guide** (public)<br/>
  https://docs.microsoft.com/en-us/contribute/

- **Validation reference** (public)<br/>
  https://docs.microsoft.com/en-us/contribute/validation-reference/

- **Docs contributor's guide** (internal)<br/>
  This includes Azure-specific guidance on overview, quickstart, and tutorial articles.<br/>
  https://review.docs.microsoft.com/help/contribute/index?branch=master

- **Onboarding guide** (internal)<br/>
  https://review.docs.microsoft.com/help/onboard/admin/reference?branch=master

- Our own **Bot Framework technical content contributors' guide** (semi-public)<br/>
  Somewhat stale and in need of updating…<br/>
  https://github.com/MicrosoftDocs/bot-docs-pr/blob/master/contributor-guide/contributor-guide-index.md

- Teams channels
  - **Docs support**

## Types of articles

Refer to the [Docs contributor's guide](https://docs.microsoft.com/en-us/contribute/) (**Create content** > **Write** section) for overall guidance on specific article types. The structure of overview, quickstart, and tutorial articles are highly constrained.

## General guidance

The Microsoft style guide has most of the general guidance, including:

- [Grammar and parts of speech](https://docs.microsoft.com/en-us/style-guide/grammar/grammar-and-parts-of-speech)
- [Word choice](https://docs.microsoft.com/en-us/style-guide/word-choice/)
- A-Z word list and term collections

## Links

### Linking to other articles

Use normal Markdown links [link text](in-repo link target).

### Linking to reference topics

Use xref links, `[link text](xref:UID)`. Note that UIDs are case-sensitive.

#### C\#

To find the xref for C#, go to https://docs.microsoft.com/en-us/dotnet/api/ and search for the member you want to link to. The xref will be the page’s ms.assetID metadata value.
For example, the xref for the BotAdapter.Use method is __Microsoft.Bot.Builder.BotAdapter.Use*__.

```html
<meta name="ms.assetid" content="Microsoft.Bot.Builder.BotAdapter.Use*" />
```

#### JavaScript/TypeScript

To find the xref for JavaScript, go to https://docs.microsoft.com/en-us/javascript/api/ and search for the member you want to link to. The xref will be the link text, as opposed to the link target.
For example, the xref for the BotAdapter.use method is __botbuilder-core.BotAdapter.use__.

### For sample code snippets

Use the code macro, for instance:

```markdown
[!code-csharp[Constructor snippet](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=22-41)]
```

### Linking to samples, other repos, and external pages

Use an aka link with a target at the most general level that makes sense.
