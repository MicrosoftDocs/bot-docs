---
title: Page title that displays in the browser tab and search results
description: Article description that will be displayed on landing pages and in most search results
author: GitHub-alias-of-only-one-author
ms.author: writing-lead-ms-alias
manager: writing-manager-ms-alias
ms.reviewer: editor-ms-alias
ms.topic: overview
ms.service: bot-service
ms.date: mm/dd/yyyy # date the article was updated in mm/dd/yyyy format
---

# Markdown template for Bot Framework on Microsoft Docs (WIP)

Your article should have only one H1 heading, which you create with a single # sign. The H1 heading should always be followed by a descriptive paragraph that helps the customer understand what the article is about. It should contain keywords you think customers would use to search for this piece of content. Do not start the article with a note or tip - always start with an introductory paragraph.

## Headings

Two ## signs create an H2 heading - if your article needs to be structured with headings below the H1, you need to have at least TWO H2 headings.

H2 headings are rendered on the page as an automatic on-page TOC. Do not hand-code article navigation in an article. Use the H2 headings to do that.

Within an H2 section, you can use three ### signs to create H3 headings. In our content, try to avoid going deeper than 3 heading layers - the heading levels are often hard to distinguish on the rendered page.

For more information, see [Markdown/Headings](https://review.learn.microsoft.com/help/platform/markdown-reference?branch=main#headings).

## Images

Use the `:::image` macro for images.

For more information, see [Markdown/Images](https://review.learn.microsoft.com/help/platform/markdown-reference?branch=main#images).

## Linking

Your article will most likely contain links. Use the right type of link, based on the link target: file in repo, site-relative, full URL.

For more details, see [Links](https://review.learn.microsoft.com/help/platform/links-how-to?branch=main).

## Include files

Whenever the same piece of content exists in multiple places throughout the docs (ex: the same few sentences describing a term, or the same tip/note/warning/etc.),
you can create that content inside a 'snippet' file (in the /includes folder), then simply reference that file in markdown to dynamically inject its contents into any article.

For more details, see [Includes](https://review.learn.microsoft.com/help/platform/includes-best-practices?branch=main).

## Including code from a GitHub repo

Use the `:::code` macro for code snippets from a GitHub samples repo.

For more information, see [How to include code in docs](https://review.learn.microsoft.com/help/platform/code-in-docs?branch=main).

## Notes and tips

Use notes and tips judiciously. A little bit goes a long way.

```md
> [!NOTE]
> Note text.

> [!TIP]
> Tip text.

> [!IMPORTANT]
> Important text.
```

For more information, see [Markdown/Alerts](https://review.learn.microsoft.com/help/platform/markdown-reference?branch=main#alerts-note-tip-important-caution-warning)

## Lists

A simple numbered list in markdown creates a numbered list on your published page.

1. First step.
1. Second step.
1. Third step.

Use hyphens to create unordered lists:

- Item
- Item
- Item

## Next steps

Every topic should end with 1 to 3 concrete, action oriented next steps and links to the next logical piece of content to keep the customer engaged.
