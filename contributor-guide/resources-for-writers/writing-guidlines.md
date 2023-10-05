# Writing guidelines for Azure AI Bot Service "conceptual" content

## Existing resources [for writers]

- **Microsoft style guide** (public)  
  https://learn.microsoft.com/style-guide/welcome/

- **Microsoft Docs contributor guide** (public)  
  https://learn.microsoft.com/contribute/

- **Validation reference** (public)  
  https://learn.microsoft.com/contribute/validation-reference/

- **Microsoft Docs contributor guide** (internal)  
  This includes Azure-specific guidance on overview, quickstart, and tutorial articles.  
  https://review.learn.microsoft.com/help/contribute/index?branch=main

- **Onboarding guide** (internal)  
  https://review.learn.microsoft.com/help/onboard/admin/reference?branch=main

- Our own **Bot Framework technical content contributors' guide** (semi-public)  
  Somewhat stale and in need of updating…  
  https://github.com/MicrosoftDocs/bot-docs-pr/blob/main/contributor-guide/contributor-guide-index.md

- Teams channels
  - **Docs support**

## Types of articles

Refer to the [Microsoft Docs contributor guide](https://learn.microsoft.com/contribute/) (**Create content** > **Write** section) for overall guidance on specific article types. The structure of overview, quickstart, and tutorial articles are highly constrained.

## General guidance

The Microsoft style guide has most of the general guidance, including:

- [Grammar and parts of speech](https://learn.microsoft.com/style-guide/grammar/grammar-and-parts-of-speech)
- [Word choice](https://learn.microsoft.com/style-guide/word-choice/)
- A-Z word list and term collections

## Links

### Linking to other articles

Use normal Markdown links [link text](in-repo link target).

### Linking to reference topics

Use xref links, `[link text](xref:UID)`. Note that UIDs are case-sensitive.

#### C#

To find the xref for C#, go to the [.NET API browser](https://learn.microsoft.com/dotnet/api/) and search for the member you want to link to. The xref will be the page's ms.assetID metadata value. For example, the xref for the BotAdapter.Use method is **Microsoft.Bot.Builder.BotAdapter.Use***.

```html
<meta name="ms.assetid" content="Microsoft.Bot.Builder.BotAdapter.Use*" />
```

#### JavaScript/TypeScript

To find the xref for JavaScript, go to the [JavaScript API browser](https://learn.microsoft.com/javascript/api/) and search for the member you want to link to. The xref will be the link text, as opposed to the link target. For example, the xref for the BotAdapter.use method is **botbuilder-core.BotAdapter.use**.

### For sample code snippets

Use the code macro, for instance:

New:

```md
:::code language="csharp" source="~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs" range="2-24,26":::
```

Old:

```md
[!code-csharp[Constructor snippet](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=22-41)]
```

### Linking to samples, other repos, and external pages

Use an aka link with a target at the most general level that makes sense.
