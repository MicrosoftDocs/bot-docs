# Formatting text

This article attempts to bring together the sometimes contradictory guidance from the Docs contributors guide and Microsoft Style Guide, and when applicable, our own specific take on it.

## Referenced articles

These are the articles referenced in rough order of relevance

| Last checked | Article | Shorthand |
|:-|:-|:-|
| 1/1/2021 | Docs contributors guide / Resources / [Format text](https://review.docs.microsoft.com/help/contribute/text-formatting-guidelines?branch=main) | Format text |
|  | MS Style Guide / Developer content / [Formatting developer text elements](https://docs.microsoft.com/style-guide/developer-content/formatting-developer-text-elements) | Dev text |
|  | MS Style Guide / Procedures and instructions / [Describing interactions with UI](https://docs.microsoft.com/style-guide/procedures-instructions/describing-interactions-with-ui) | Describing UI |
|  | MS Style Guide / Procedures and instructions / [Formatting text in instructions](https://docs.microsoft.com/style-guide/procedures-instructions/formatting-text-in-instructions) | Instructions |
|  | MS Style Guide / Text formatting / [Formatting common text elements](https://docs.microsoft.com/style-guide/text-formatting/formatting-common-text-elements) | Common text |
| na | None. Agreed upon within the Bot Framework writing team, or based on input from the **Docs Support** Teams team. | The rough date of the last discussion on a specific element. |

<a id="guidance"></a>

## Typography guidance

TL;DR, skip to [Typography quick reference](#typography-quick-reference).

| Element | Convention | Example | Source and notes |
|:-|:-|:-|:-|
| <a id="code-element-agnostic"></a>Code element [language-agnostic] | _Italic_ (when the meaning might be ambiguous) or normal. Use lower case (except where the element name includes a proper noun) and separate words with spaces. Treat this a little bit like a [new term](#new-term). | To receive a simple text message, use the _text_ property of the _activity_ object. | 2018: Decided to use descriptive terms in italic for SDK elements because the names are not identical across languages. However, this means that such text may get localized with formatting. |
| <a id="code-element-specific"></a>Code element [language-specific] | `Code`. Match the exact form of the element. In general, qualify what type of element you're talking about, such as namespace, class, method, property, keyword, and so on. Don't add () to the end of a method to indicate that it's a method, unless you need to include the full signature. | The `Attachments` property of the `Activity` object contains an array of `Attachment` objects that represent the media attachments and rich cards attached to the message. | Format text > [Code style](https://review.docs.microsoft.com/help/contribute/text-formatting-guidelines?branch=main#code-style) |
| <a id="cli-commands"></a>CLI commands | `Code`, inline or fenced. | Use `dotnet run` to start the bot. | Format text > [Code style](https://review.docs.microsoft.com/help/contribute/text-formatting-guidelines?branch=main#code-style)<br/>(Fenced code blocks include a copy button but take up more visual space.) |
| <a id="emphasis"></a>Emphasis | _Italic_. Use sparingly. Do not capitalize a word just for emphasis. | Cybercriminals might call you and claim to be from Microsoft. Be aware that Microsoft will _never_ call you to charge for security or software fixes. | [Common text](https://docs.microsoft.com/style-guide/text-formatting/formatting-common-text-elements) |
| <a id="file-names"></a>File names, folder names, and paths | **Bold**. Match the casing of the path. | Update **run.csx** with the following code: | 2018: We wanted some visual distinction that wasn't as jarring as the `code` format. Locallization shouldn't be an issue, since most file names and paths are not translatable. |
| <a id="file-extensions"></a>File extensions | Normal. Lower case. Include the . before the extension name. | An .lu file contains Markdown-like, simple text based definitions for language understanding resources. | [Instructions](https://docs.microsoft.com/style-guide/procedures-instructions/formatting-text-in-instructions)<br/>When choosing between "a" or "an", assume the dot (.) is unvoiced. |
|<a id="headings"></a>Headings|Normal, sentence case||Format > [Headings and link text](https://review.docs.microsoft.com/help/contribute/text-formatting-guidelines?branch=main#headings-and-link-text)|
| <a id="literals"></a>Literals | `Code`, **bold**, or _italic_ (TBD) | TBD | TBD |
| <a id="new-term"></a>New term | _Italic_. Lower case, unless it is a proper noun. Only italicize on first mention, and include a description or example. | An app runs in an _App Service plan_. An App Service plan defines a set of compute resources for a web app to run on. | Format text > [Italics](https://review.docs.microsoft.com/help/contribute/text-formatting-guidelines?branch=main#italics) |
| <a id="package-names"></a>Package names - TBD | **Bold**. Match the casing of the package name as it appears on the package repository site, such as on [NuGet](https://www.nuget.org/), [npm](https://www.npmjs.com/), and so on. Qualify the name with what type of package it is. | To use dialogs, install the **Microsoft.Bot.Builder.Dialogs** NuGet package. <br><br>OR<br><br> To use dialogs, install the *Microsoft.Bot.Builder.Dialogs* NuGet package. | 2018: Same reasoning as for [file names](#file-names). There is guidance suggesting [italics](https://review.docs.microsoft.com/help/contribute/text-formatting-guidelines?branch=main#code-style).|
| <a id="sample-names"></a>Sample names | **Bold** on first use. Match the sample name (use the name in the `README`). Consider adding the word "adaptive" in normal text before adaptive dialogs samples. | A copy of the **Proactive messages** sample in [**C#**](#), [**JavaScript**](#), or [**Python**](#). | 2018: See [Localization](#localization) note. |
| <a id="ui-element-named"></a>UI elements [with a label or hover text] | **Bold**. Match the casing of the element's label or hover text. | In **Solution Explorer**, right-click the project node, and then select **Add** > **New Item**. | Format text > [Bold](https://review.docs.microsoft.com/help/contribute/text-formatting-guidelines?branch=main#bold) |
| <a id="ui-element-unnamed"></a>UI elements [without a label or hover text] | _Italic_ on first use, then normal. Lower case. Treat this like a [new term](#new-term). | Both Azure DevOps Services and Azure DevOps Server 2019 use the new navigation user interface, with a vertical sidebar to go to the main service areas: **Boards**, **Repos**, **Pipelines**, and more. To learn more, see [Web portal navigation](#) in Azure DevOps. | 01/2021: See MS Style guide > [pane](https://styleguides.azurewebsites.net/Styleguide/Read?id=2700&topicid=35548). 12/2020: The UI element without UI text in this example is the _sidebar_, as opposed to the service areas listed in the sidebar. |
| <a id="url-live"></a>URLs [clickable] | Use a [link](https://review.docs.microsoft.com/help/contribute/links-how-to?branch=main#link-text) with link text that is descriptive [within the context] and does not include additional formatting. Do not use "here" as the link text. Use the title case if using the exact section, article, or TOC title. Consider notifying users if they are leaving a docset. | Knowledge of [Bot basics](https://docs.microsoft.com/azure/bot-service/bot-builder-basics). | [Links](https://review.docs.microsoft.com/help/contribute/links-how-to?branch=main#link-text) |
| <a id="url-live-exception"></a>URLs [clickable, exception] | When including a list of language-specific links to samples, we have been using the language name in **bold** for the link text. Don't include sample after the language name. | A copy of the **Proactive messages** sample in [**C#**](#), [**JavaScript**](#), or [**Python**](#). | 2018: Here, even for screen readers, the context should make the meaning of these links obvious. |
| <a id="url-literal"></a>URLs [non-clickable] | `Code`. | Enter your bot's URL, which is the URL of the local port, with /api/messages added to the path, typically `http://localhost:3978/api/messages`. | Format text > [Code style](https://review.docs.microsoft.com/help/contribute/text-formatting-guidelines?branch=main#code-style) |
| <a id="user-input"></a>User input | TBD | TBD | TBD |

### Typography quick reference

#### Bold

- [UI elements [with a label or hover text]](#ui-element-named)
- May need to revisit
  - [File names, folder names, and paths](#file-names)
  - [Package names](#package-names)
  - [Sample names](#sample-names)

#### Italic

- [Code element [language-agnostic]](#code-element-agnostic) - to address unique Bot Framework concerns
- [Emphasis](#emphasis)
- [New term](#new-term)
- [UI elements [without a label or hover text]](#ui-element-unnamed)

#### Literal/code

- [Code element [language-specific]](#code-element-specific), in-line
- [CLI commands](#cli-commands), in-line or fenced
- [URLs [non-clickable]](#url-literal)

#### Normal

- [File extensions](#file-extensions)
- [URLs [clickable]](#url-live), as a live link
  - [URLs [clickable, exception]](#url-live-exception), + bold for now. This may be "wrong".

#### TBD

- [Literals](#literals)
- [User input](#user-input)

## Capitalization

### Normal (lower) case

- Feature names, unless there's an exception for the term. See [Feature names](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=44733) in the Cloud guide.
- An instance of a product. See [Product names vs. instances](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=44732) in the Cloud guide.

### Sentence case

- Blog posts and press releases. See [Capitalization](https://styleguides.azurewebsites.net/Styleguide/Read?id=2700&topicid=33685) and [Formatting titles](https://styleguides.azurewebsites.net/Styleguide/Read?id=2700&topicid=36416) in the Writing guide.
- Topic titles and headings. See [Headings and titles](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=28110) in the Cloud guide.
- TOC entries.
- UI/UX text. See [Capitalization by channel](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=44731) in the Cloud guide.

### Title case

- Blog, book, song, and white paper titles. See [Capitalization](https://styleguides.azurewebsites.net/Styleguide/Read?id=2700&topicid=33685) and [Formatting titles](https://styleguides.azurewebsites.net/Styleguide/Read?id=2700&topicid=36416) in the Writing guide.
- A product or service name. See [Product names vs. instances](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=44732) in the Cloud guide.
- Proper nouns. See [Capitalization](https://styleguides.azurewebsites.net/Styleguide/Read?id=2700&topicid=33685) in the Writing guide.
- A team or group name, not including the word "team" or "group". See [Team and group names](https://styleguides.azurewebsites.net/Styleguide/Read?id=2696&topicid=44143) in the Cloud guide.

## Notes

### Icons

See:

- Microsoft Style Guide / A&ndash;Z word list and term collections / I / [icon](https://docs.microsoft.com/style-guide/a-z-word-list-term-collections/i/icon)
- [Icons Guidance](https://review.docs.microsoft.com/help/contribute/markdown-reference?branch=main#icons)
- [Test themes and transparency](https://review.docs.microsoft.com/help/contribute/contribute-how-to-create-conceptual-art?branch=main#test-multiple-themes-and-transparency)

Avoid using the word _icon_ for icons that include a name or hover text.
To refer to the graphic itself, if there's no other identifying label, use _symbol_, as in _warning symbol_.

#### Examples: icons

- Select **Resources** 📄.
- Select the warning symbol ⚠.
- Most apps have their own settings. Look for the gear icon ⚙ in the app.

### Localization

If you choose an alternate text style where code is normally called for, make sure it's okay for the text to be translated in localized versions of the article. Code is the only style that automatically prevents translation. For scenarios where you want to prevent localization without using code style, see [Non-localized strings](https://review.docs.microsoft.com/help/contribute/markdown-reference?branch=main#non-localized-strings) in the Contributors guide.
