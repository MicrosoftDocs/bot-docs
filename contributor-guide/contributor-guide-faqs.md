---
title: Frequent asked questions.
description: Collection of frequent asked questions to make a writer's life easier.
topic: article
service: bot-service
date: 12/29/2020
---

# Frequent asked questions

Contains a list of frequent asked questions that writers might have when going about their daily tasks. It is meant to be a living document, as more questions and answers are created. A quick reference before redirecting the writers to more in depth answers.

<hr/>

[A](#a) &MediumSpace; [B](#b) &MediumSpace; [C](#c) &MediumSpace; [D](#d) &MediumSpace; [E](#e) &MediumSpace;
[F](#f) &MediumSpace; [G](#g) &MediumSpace; [H](#h) &MediumSpace; [I](#i) &MediumSpace; [J](#j) &MediumSpace;
[K](#k) &MediumSpace; [L](#l) &MediumSpace; [M](#m) &MediumSpace; [N](#n) &MediumSpace; [O](#o) &MediumSpace;
[P](#p) &MediumSpace; [Q](#q) &MediumSpace; [R](#r) &MediumSpace; [S](#s) &MediumSpace; [T](#t) &MediumSpace;
[U](#u) &MediumSpace; [V](#v) &MediumSpace; [W](#w) &MediumSpace; [X](#x) &MediumSpace; [Y](#y) &MediumSpace; [Z](#z)

<hr/>

<a id="a"></a>
<a id="b"></a>
<a id="c"></a>
<a id="d"></a>
<a id="e"></a>
<a id="f"></a>

<a id="g"></a>

## GitHub

### How to fix a local repository that's out of sync?

If your copy of the GitHub repo is out of sync with the remote, follow the steps described in this article: [To make sure your local copy is completely in sync with the remote](github-tips-and-tricks.md#to-make-sure-your-local-copy-is-completely-in-sync-with-the-remote).

### What can I do to show my PR isn't ready to be merged?

- Make the PR a [draft pull request](https://github.blog/2019-02-14-introducing-draft-pull-requests/). Draft pull requests cannot be merged.
- Use one of the following labels that indicate a PR isn't ready to be merged:
  - **status: writing** (work in progress)
  - **DO NOT MERGE**

<a id="h"></a>
<a id="i"></a>
<a id="j"></a>
<a id="k"></a>

<a id="l"></a>

## Links

### When do I create reference-style links?

You can use **reference-style links** to make your source content easier to read. They are most useful when you repeatedly link to the same target. They are for convenience and readability (especially if the repeated target is long).

If the link text and the link target ID are the same, you can shorten the Markdown even further:

```markdown
Blah blah [unique link text][] blah blah.

[unique link text]: link-target
```

For more information, see [Reference-style links](https://review.learn.microsoft.com/help/platform/links-how-to?branch=main#reference-style-links).

### How do I define link text?

- See link markdown rules described in this article: [Create-links-markdown](create-links-markdown.md)
- In the footnote links definitions such as `[1]:bot-builder-concept-dialog.md` do not use numbers in the square brackets, but short text.

### How do I check for duplicate alternate text descriptions?

In Markdown, links can be made using the following formats:

- Link to a page: `[link name to display](link to the page)`.
- Link to an image: `![hidden name](link to the image)` (old style).
- Link to an image: `:::image type="content" source="<folderPath>" alt-text="<alt text>" link="<https://link.com>":::` (new style)

The strings in the square brackets, that is `link name to display` and `hidden name` are called **alternate text**.

> The **alternate text** must be **unique in a page**.

To find duplicate alternate text follow these steps:

1. In an active *Pull Request*, click the **build report** link.
1. In the **Total Build Suggestions** row, click the number in the related column.

    ![build suggestions](media/contributor-guide-faqs/build-suggestions.png)

1. In the page, search for **duplicate-alt-text**.
1. You can copy all the related rows, with relevant information, in an Excel page.

    ![build suggestions 2](media/contributor-guide-faqs/build-suggestions-2.png)

1. Finally, edit the duplicate alt text in the linked article.

### How to verify that deprecated links are fixed?

Sometimes you may need to fix links, like when they're obsolete. The examples below show how to verify different kinds of links.

#### Example 1

1. Wrong format: `https://aka.ms/azure-bot-subscribe-to-conversation-events)`.
1. Correct format: `/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events`.

To verify that the links are fixed follow these steps:

1. In the PR page click the **Conversation** tab.
1. Search the most recent iteration of the `<article name>.md` file that has the links to be fixed.
1. Right click the **View** link and open the article in a new tab.
1. In the new tab, fix the link to the article by removing `review.` from the address. You get the current article containing the links to fix.
1. Find the link and verify that the fix you made works.

#### Example 2 - footnote links

1. Wrong format: `[prompts]:https://aka.ms/bot-builder-concept-dialog#prompts`.
1. Correct format: `[prompts]:bot-builder-concept-waterfall-dialogs.md#prompts`.

These kind of links, which are usually defined in the page footnote and are not visible, are slightly more difficult to verify. Follow these steps to verify footnote links:

1. In the PR page click the **Conversation** tab.
1. Search the most recent iteration of the `<article name>.md` file that has the links to be fixed.
1. Right click the **View** link and open the article in a new tab.
1. Right click the `<article name>.md` file and open the file in a new tab.
1. Click the **Raw** tab and search for the string that is in the square brackets. In the example it is `prompts`.
1. Find the place where it is used.
1. Go to the tab that contains the article find the place where the link is used and check if it works.

<a id="m"></a>

## Markdown

### When to include Markdown files?

Use include Markdown files *only if their content need to be repeated in multiple articles*. The include feature instructs the documentation build system to replace the reference with the content of the include file at build time. For more information, see [Included Markdown files](https://learn.microsoft.com/contribute/markdown-reference#included-markdown-files).

### How to remove unused include files?

When building your PR, you may notice that some include files do not have a *View* link. It's likely that they are not used anymore and have not been deleted from the repository.

![unused include files](media/contributor-guide-faqs/unused-include-files.png)

Go ahead and delete them and rebuild the PR. It should not happen, but if you get broken links errors, put the include files back and investigate further.

### How to create FAQ content?

When you have frequently asked questions (FAQ) and answers about products, features, and services, you can use a *structured YAML template* to present the content. The purpose of using a structured YAML template is to align with a specific schema that Google has defined for FAQ pages. That schema provides better crawlability, ranking, and relevance in the search engine. For more information, see [Create FAQ content](https://review.learn.microsoft.com/help/contribute/contribute-how-to-faq-guide?branch=main).

Writing in YAML format can be a little tricky because you have to obey to strict rules such as proper spacing, positioning of keywords and so on.

<a id="n"></a>
<a id="o"></a>
<a id="p"></a>
<a id="q"></a>
<a id="r"></a>

<a id="s"></a>

## Sentences

### How do I use the word "we"?

The use of **we** is discouraged. Instead, focus on the customer, and avoid making Microsoft the subject.
For more information, see [we](https://learn.microsoft.com/style-guide/a-z-word-list-term-collections/w/we).

### How do I use verbs?

Using verbs correctly helps you write clear and simple sentences. The present tense is often easier to read and understand than the past or future tense. It's the best choice for most content. For more information, see [Verbs](https://learn.microsoft.com/style-guide/grammar/verbs).

<a id="t"></a>
<a id="u"></a>
<a id="v"></a>
<a id="w"></a>
<a id="x"></a>
<a id="y"></a>
<a id="z"></a>

## References

- [How to Write Good](https://www.plainlanguage.gov/resources/humor/how-to-write-good/)
- [Federal plain language guidelines](https://www.plainlanguage.gov/guidelines/)
- [Contributor Guide](https://review.learn.microsoft.com/help/contribute/?branch=main)
- [Create FAQ content](https://review.learn.microsoft.com/help/contribute/contribute-how-to-faq-guide?branch=main)
