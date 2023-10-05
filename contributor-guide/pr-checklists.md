# PR checklists

- [PR Review](#pr-review)
- [New article](#new-article)
- [Retire an article](#retire-an-article)

## PR review

### Mandatory (blocking) - Mandatory checks before any PR gets reviewed. PRs that fail to pass these checks will not be reviewed

- [ ] Must not be a [draft PR](https://docs.github.com/en/github/collaborating-with-issues-and-pull-requests/about-pull-requests#draft-pull-requests). Do not take PRs out of draft mode unless the writer OKs it.
- [ ] Must have the **review requested** label. <!---One issue with our tagging is we have status: in review: review requested and status: in review: changes requested. However, each PR already indicates if there are changes requested. We should decide what the best practice is for this. Consider removing the label, or creating a new one.-->
- [ ] Related issues must be linked in the PR description. If there are no related issues, explicitly state that in the description.
- [ ] No build report errors. Ideally there will be no build suggestions or warnings.
- [ ] No locales in links.
- [ ] Only one H1 heading.
- [ ] Bullets and numbers, images, and code snippets are properly indented and ordered (quick pass is fine in most cases).
- [ ] Specific topic types (like quickstarts, tutorials, etc.) follow the Microsoft Learn guidance. See [Choose the correct topic type for your article](https://review.learn.microsoft.com/help/contribute/content-type-comparison) for more information about correctly formatting information.
- [ ] Re-request a review once you've finished addressing feedback and want a reviewer to take a second pass.

Specific scenarios:

- Media updates:
  - [ ] Media is clear.
  - [ ] Media contains no private information.
- Link additions/updates:
  - [ ] Confirm links work. Actually click them.
    - Site-relative links will generate a 404, most of the time. Remove **review** from the URL to check the link.
      - Example: change `https://review.learn.microsoft.com/azure/...` to `https://learn.microsoft.com/azure/...`.
- New article added to the docset:
  - [ ] All checks passed in the [New article](#new-article) section below.
- Article retired from the docset:
  - [ ] All checks passed in the [Retire an article](#retire-an-article) section below.

### Preferred - Optional checks before a PR gets reviewed. These should not be skipped, but they are not absolutely mandatory before merging in the event of a time crunch

- [ ] Check spelling, case, grammar, spacing, and punctuation. Don't use capitalization (or lack of) for emphasis.<!--add specific links here-->
- [ ] Text formatting follows [guidance](formatting-text.md) (TBD).
- [ ] Sentence case for headings and titles.<!--add specific links here-->
- [ ] Don't introduce new warning/suggestions. If you do, note them.
- [ ] If English isn't your first language, ask someone else on the team whose first language is English for a language review.
- [ ] If you're not comfortable with a particular programming language, ask a writer who is more familiar for a review.
- [ ] If making significant tech/code changes, ask a technical expert for a review.

Specific scenarios:

- How-Tos and sample based articles:
  - [ ] Test the sample and make sure it works as expected in all documented languages.
  - Channels:
    - [ ] Ask the channel's team point of contact to test the channel functionality.

### Nitpick - More nuanced checks. These should be done if time allows

- [ ] Good readability. Casual tone, but not too casual. Not wordy (less is more), doesn't use jargon/culture-specific language, and use passive voice judiciously.
- [ ] Bias free language.
- [ ] No curly quotes.

### Backlog - Checks we need to work on in the future

- [ ] All images have unique and descriptive alt text. Non-unique alt text generates a suggestion currently, which will eventually be promoted to a warning.
- [ ] Clear navigation - intro about topic, point to other places to look if this isn't the right topic (SEO), article content, additional information and/or next steps.

## New article

### Mandatory - Mandatory checks to make before adding a new article to the doc set

- [ ] New article name follows [naming conventions](file-names-and-locations.md), including the length.
- [ ] New article is in the TOC. The article name and TOC should map fairly easily.
  - Example: An article in the **Debug** section of the TOC may not need the word *debug* in the TOC title, but it can be included in the article title.  
- [ ] Correct [metadata](article-metadata.md). Be sure to update the date.
- [ ] Has the "Applies to" header.

Specific scenarios:

- How-Tos and sample based articles:
  - [ ] Test the sample and make sure it works as expected in all documented languages.

### Preferred - Optional checks before adding a new article. These should not be skipped, but they are not absolutely mandatory before merging in the event of a time crunch

TBD

## Merge PR

### Mandatory - Mandatory checks to make before merging a PR

- [ ] Must have the **ready** label.

### Preferred - Optional checks before merging a PR. These should not be skipped, but they are not absolutely mandatory before merging in the event of a time crunch

TBD

## Retire an article

### Mandatory - Mandatory checks to make before retiring an article

TBD

### Preferred - Optional checks before an article is retired. These should not be skipped, but they are not absolutely mandatory before merging in the event of a time crunch

TBD
