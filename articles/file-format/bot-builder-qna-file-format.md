---
title: .qna file format
description: .qna file format reference
keywords: qna file format, reference, qnamaker
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: reference
ms.date: 10/11/2022
monikerRange: 'azure-bot-service-4.0'
ms.custom:
  - evergreen
---

# .qna file format

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

[!INCLUDE [qnamaker-sunset-alert](../includes/qnamaker-sunset-alert.md)]

.qna files contain Markdown-like text based definitions for [QnAmaker.ai](http://qnamaker.ai) concepts. This article covers the various concepts expressed via the .qna file format.

## Adding comments

Use **>** to create a comment. Here's an example:

```qna
> This is a comment and will be ignored
```

## Question and Answer pairs

The .qna file and parser support question and answer definitions.

Here's the syntax of a basic question and answer definition:

````qna
# ? Question
[list of question variations]
```
Answer
```
````

Here's are examples of question and answer definitions:

````qna
> # QnA Definitions
### ? who is the ceo?
```
You can change the default message if you use the QnAMakerDialog.
For details, see [Azure AI Bot Service documentation](/articles/adaptive-dialog/adaptive-dialog-prebuilt-actions.md).
```


### ? How do I programmatically update my KB?
```
You can use our REST apis to manage your KB.
\#1. See here for details: https://westus.dev.cognitive.microsoft.com/docs/services/58994a073d9e04097c7ba6fe/operations/58994a073d9e041ad42d9baa
```
````

Note that the `markdown` type identifier for an `answer` is optional.

### Multiple questions

You can add multiple questions to the same answer by simply adding variations to questions.

````qna
### ? Aren't you feeling happy today?
- Feeling cheerful?
```markdown
I'm quite happy, thank you.
```
````

## QnAMaker Filters

Filters in QnA Maker are simple key-value pairs that can be used to narrow search results, boost answers and store context.

Use the following syntax to add filters:

```qna
***Filters:***
- name = value
- name = value
```

Here's an example of how a filter could be used:

````qna
### ? Where can I get coffee?
- I need coffee

**Filters:**
- location = seattle

```markdown
You can get coffee in our Seattle store at 1 pike place, Seattle, WA
```

### ? Where can I get coffee?
- I need coffee

**Filters:**
- location = portland

```markdown
You can get coffee in our Portland store at 52 marine drive, Portland, OR
```
````

## QnA Maker PDF file ingestion

QnA Maker also supports ingesting PDF files during KB creation. You can add files for QnA Maker to ingest using the URL reference scheme. If the URI's content type isn't text or HTML, then the parser will add it to files collection for QnA Maker to ingest.

```qna
[SurfaceManual.pdf](https://download.microsoft.com/download/2/9/B/29B20383-302C-4517-A006-B0186F04BE28/surface-pro-4-user-guide-EN.pdf)
```

## External references

External references are supported in the .qna file and use Markdown link syntax.

### Reference another .qna file

Reference to another .qna file using `[link name](<.qna file name>)`. References can be an absolute path or a relative path from the containing .qna file.

### Reference to a folder containing .qna files

Reference to a folder with other .qna files is supported through:

- `[link name](<.qna file path>/*)`: looks for .qna files under the specified absolute or relative path.
- `[link name](<.qna file path>/**)`: recursively looks for .qna files under the specified absolute or relative path including subfolders.

### Reference a URL

Reference a URL for QnAMaker to ingest during KB creation via `[link name](<URL>)`.

### Reference from a specific file

You can also add references to utterances defined in a specific file under an intent section or as QnA pairs.

- `[link name](<.lu file path>#<INTENT-NAME>)`: finds all utterances found under \<INTENT-NAME> in the .lu file and adds them to the list of questions where the reference is specified.
- `[link name](<.lu file path>#*utterances*)`: finds all utterances in the .lu file and adds them to the list of questions where the reference is specified.
- `[link name](<.qna file path>#?)`: finds questions from all QnA pairs defined in the .qna file and adds them to the list of utterances where this reference is specified.
- `[link name](<.qna folder>/*#?)`: finds all questions from all .qna files in the specified folder and adds them to the list of utterances where this reference is specified.

Here's an example of the above references:

```qna
> QnA URL reference
[QnaURL](/azure/ai-services/qnamaker/)

> Include all content in ./kb1.qna
[KB1](./kb1.qna)

> Look for all .qna files under a path
[ChitChat](./chitchat/*)

> Recursively look for .qna files under a path including subfolders.
[ChitChat](../chitchat/resources/**)
```

## Model description

You can include configuration information for your LUIS application or QnA Maker KB in the .qna file to help direct the parser to handle the LU content correctly.

[!INCLUDE [luis-sunset-alert](../includes/luis-sunset-alert.md)]

Here's how to add configuration information sing **> !#**:

```qna
> !# @<property> = <value>
> !# @<scope>-<property> = <value>
> !# @<scope>-<property> = <semicolon-delimited-key-value-pairs>
```

Note that any information explicitly passed in via CLI arguments will override information in the .qna file.

```qna
> Parser instruction - this is optional; unless specified, the parser will default to the latest version.
> !# @version = 1.0

> QnA Maker KB description
> !# @kb.name = my qna maker kb name

> Source for a specific QnA pair
> !# @qna.pair.source = <source value>
```

## Multiturn content

Multiturn content is represented in .qna format using Markdown link notation. Links are specified using the following way:

```qna
- [display text](#<ID or question>)
```

You can optionally include `context-only` for any prompts that are only contextually available for a question. Read  the section about [adding an existing question-and-answer pair as a follow-up prompt](/azure/ai-services/language-service/question-answering/tutorials/guided-conversations#add-question-pair-with-follow-up-prompts) to learn more about use of `context`.

```qna
- [tell me a joke](#?joke) `context-only`
```

### Follow-up prompts

Developers have two options for creating follow-up prompts: using a question as a follow-up prompt directly, or assigning an explicit ID to a QnA pair.

### Use a question directly

The first QnA pair that has the link text as a `question` will be added as the prompt. If you need more explicit control, use [IDs](#question-and-answer-pairs) instead.

When you're directly using a question, use Markdown convention and replace spaces with hyphens (for example, use `#?when-is-the-portland-store-open` instead of `#?when is the portland store open`). The parser will do its best to find the link.

````qna
# ?store hours
```
Most our stores are open M-F 9AM-10PM.
```
**Prompts:**
- [Seattle store](#?seattle)
- [Portland store](#?when-is-the-portland-store-open)

# ?seattle
```
The Seattle store is open M-F 9AM-10PM.
```

# ?when is the portland store open
- portland store hours
```
The Portland store is open 24/7.
```
````

> [!TIP]
> The link won't actually render as a selectable link in most Markdown renderers.

### Assign an explicit ID to a QnA pair

Assign IDs for each prompt with a number. You can see in the example below the prompt for each store has been assigned a different numeric value.

````qna
# ?store hours
```
Most our stores are open M-F 9AM-10PM.
```
**Prompts:**
- [Seattle store](#1)
- [Portland store](#2)

<a id = "1"></a>

# ?seattle
```
The Seattle store is open M-F 9AM-10PM.
```

<a id = "2"></a>

# ?when is the portland store open
- portland store hours
```
The Portland store is open 24/7.
```
````

## Additional Resources

- See [.lu file format](bot-builder-lu-file-format.md) for information about the .lu file format.
