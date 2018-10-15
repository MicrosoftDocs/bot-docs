---
title: Page title that displays in the browser tab and search results | Microsoft Docs
description: Article description that will be displayed on landing pages and in most search results
author: GitHub-alias-of-only-one-author
ms.author: your-alias
manager: rstand
ms.topic: article
ms.prod: bot-framework
# date the article was updated in mm/dd/yyyy format
ms.date: 05/01/2017
---
# Markdown template for Bot Framework on Microsoft Docs (WIP)

Your article should have only one H1 heading, which you create with a single # sign. The H1 heading should always be followed by a descriptive paragraph that helps the customer understand what the article is about. It should contain keywords you think customers would use to search for this piece of content. Do not start the article with a note or tip - always start with an introductory paragraph.

## Headings

Two ## signs create an H2 heading - if your article needs to be structured with headings below the H1, you need to have at least TWO H2 headings.

H2 headings are rendered on the page as an automatic on-page TOC. Do not hand-code article navigation in an article. Use the H2 headings to do that.

Within an H2 section, you can use three ### signs to create H3 headings. In our content, try to avoid going deeper than 3 heading layers - the headings are often hard to distinguish on the rendered page.

## Images
You can use images throughout a technical article. Make sure you include alt text for all your images. This helps accessibility and discoverability.

## Linking
Your article will most likely contain links. Here's sample markdown for a link to a target that is not on the docs.microsoft.com site:

    [link text](url)
    [Scott Guthrie's blog](http://weblogs.asp.net/scottgu)

Launch an (external) link in a new tab by using an HTML anchor tag as shown in this example:

    <a href="url" target="_blank">link text</a>

    <a href="http://weblogs.asp.net/scottgu" target="_blank">Scott Guthrie's blog</a>

Here's sample markdown for a link to another technical article in the botframework-docs repository:

    [link text](../articles/article-name.md)

You can also use so-called reference style links where you define the links at the bottom of the article, and reference them like this:

    I get 10 times more traffic from [Google][gog] than from [Yahoo][yah] or [MSN][msn].

## Using 'Include' files

Whenever the same piece of content exists in multiple places throughout the docs (ex: the same few sentences describing a term, or the same tip/note/warning/etc.),
you can create that content inside a 'snippet' file (in the /includes folder), then simply reference that file in markdown to dynamically inject its contents into any article.

For example, the following 'include' statement injects the contents that's defined in the `includes/snippet-tip-bot-config-settings.md` file.

    [!include[Application configuration settings](~/includes/snippet-tip-bot-config-settings.md)]

Syntax for the include statement is:

    [!include[description-of-the-include-contents](path-to-md-file-that-contains-the-markdown-to-include)]

## Including code from a GitHub repo

You can inject code from a file that resides in GitHub by using a statement like the following:

    [!code-JavaScript[_samples/echobot](../_samples/echobot/server.js "Echo Bot in Node.js")]

For details about how to configure this functionality, see the [OPS documenation](https://opsdocs.azurewebsites.net/en-us/opsdocs/partnerdocs/codesnippets?branch=master).

## Notes and tips
You should use notes and tips judiciously. A little bit goes a long way. Put the text of the note or tip on the line after the custom markdown extension.

```
> [!NOTE]
> Note text.

> [!TIP]
> Tip text.

> [!IMPORTANT]
> Important text.
```

## Lists

A simple numbered list in markdown creates a numbered list on your published page.

1. First step.
2. Second step.
3. Third step.

Use hyphens to create unordered lists:

- Item
- Item
- Item


## Next steps
Every topic should end with 1 to 3 concrete, action oriented next steps and links to the next logical piece of content to keep the customer engaged.

<!--Reference style links - using these makes the source content way more readable than using inline links-->
[gog]: http://google.com/        
[yah]: http://search.yahoo.com/  
[msn]: http://search.msn.com/    
