---
title: .qna file format - Bot Service
description: .qna file format reference
keywords: qna file format, reference, qnamaker
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/16/2020
---


# .qna file format

.qna files contain Markdown-like text based definitions for [QnAmaker.ai](http://qnamaker.ai) concepts. This article covers the various concepts expressed via the .qna file format.

<!--
Supported concepts:

- [Comments](#Adding-comments)
- [QnA pairs](#Question-and-Answer-pairs)
- [Filters](#QnAMaker-Filters)
- [QnA Alterations](#QnA-Maker-alterations)
- [File references](#QnA-Maker-pdf-file-ingestion)
- [References](#External-references)
- [Model description](#Model-description)
- [Multi-turn content](#Multi-turn-content)-->

## Adding comments

You can add comments to your .lu document by prefixing the comment with **>**.

```.qna
> This is a comment and will be ignored
```

## Question and Answer pairs

The .qna file (and parser) supports question and answer definitions as well.

Here's an example of basic question and answer definition.

```.qna
	# ? Question
	[list of question variations]
	```.qna
	Answer
	```
```

Here's an example of question and answer definitions.


```.qna
> # QnA Definitions
### ? who is the ceo?
	```
	You can change the default message if you use the QnAMakerDialog.
	See [this link](https://docs.botframework.com/en-us/azure-bot-service/templates/qnamaker/#navtitle) for details.
	```


### ? How do I programmatically update my KB?
	```
	You can use our REST apis to manage your KB.
	\#1. See here for details: https://westus.dev.cognitive.microsoft.com/docs/services/58994a073d9e04097c7ba6fe/operations/58994a073d9e041ad42d9baa
	```
```

Note that the `markdown` type identifier for an `answer` is optional.

### Multiple questions

You can add multiple questions to the same answer by simply adding variations to questions.

```.qna
### ? Who is your ceo?
- get me your ceo info
	```.qna
		Vishwac
	```
```

## QnAMaker Filters

Filters in QnA Maker are simple key-value pairs that can be used to narrow search results, boost answers and store context.

Use the following notation to add filters.

```.qna
***Filters:***
- name = value
- name = value
```

Here's an example of how a filter could be used.

```.qna
### ? Where can I get coffee?
- I need coffee

**Filters:**
- location = seattle

    ```.qna
    You can get coffee in our Seattle store at 1 pike place, Seattle, WA
    ```

### ? Where can I get coffee?
- I need coffee

**Filters:**
- location = portland

    ```.qna
    You can get coffee in our Portland store at 52 marine drive, Portland, OR
    ```
```

## QnA Maker alterations

QnA Maker supports [word alterations](https://docs.microsoft.com/azure/cognitive-services/qnamaker/concepts/best-practices#use-synonyms) as a way to improve the likelihood that a given user query is answered with an appropriate response. You can use this feature to add synonyms to keywords that take different form.

You can describe word alterations/synonyms lists in .qna files using the following notation.
```.qna
$<synonym word>:qna-alteration=
- <list of synonyms>
```

Here's an example:
```.qna
$botframework : qna-alterations=
- bot framework
- Microsoft bot framework
```

## QnA Maker PDF file ingestion

QnA Maker also supports ingesting PDF files during KB creation. You can add files for QnA Maker to ingest using the URL reference scheme. If the URI's content type is not text/html, then the parser will add it to files collection for QnA Maker to ingest.

```.qna
[SurfaceManual.pdf](https://download.microsoft.com/download/2/9/B/29B20383-302C-4517-A006-B0186F04BE28/surface-pro-4-user-guide-EN.pdf)
```

## External references

External references are supported in the .qna file and Markdown link syntax.

### Reference another .qna file

Reference to another .qna file using `\[link name](\<.qna file name\>)`. References can be an absolute path or a relative path from the containing .qna file.

### Reference to a folder containing .qna files

Reference to a folder with other .qna files is supported through:

- `\[link name](\<.qna file path\>/*)` will look for .qna files under the specified absolute or relative path.
- `\[link name](\<.qna file path\>/**)` will recursively look for .qna files under the specified absolute or relative path including sub-folders.

### Reference a URL

Reference a URL for QnAMaker to ingest during KB creation via `\[link name](\<URL\>)`

### Reference from a specific file

You can also add references to utterances defined in a specific file under an Intent section or as QnA pairs.

- `[link name](<.lu file path>#<INTENT-NAME>)` will find all utterances found under <INTENT-NAME> in the .lu file and add them to the list of questions where the reference is specified.
- `[link name](\<.lu file path>#*utterances*)` will find all utterances in the .lu file and add them to the list of questions where the reference is specified.
- `\[link name](\<.qna file path\>#?)` will find questions from all QnA pairs defined in the .qna file and add them to the list of utterances where this reference is specified.
- `\[link name](\<.qna folder\>/*#?)` will find all questions from all .qna files in the specified folder and add them to the list of utterances where this reference is specified.

Here's an example of those references.

```.qna
> QnA URL reference
[QnaURL](https://docs.microsoft.com/en-in/azure/cognitive-services/qnamaker/faqs)

> Include all content in ./kb1.qna
[KB1](./kb1.qna)

> Look for all .qna files under a path
[ChitChat](./chitchat/*)

> Recursively look for .qna files under a path including sub-folders.
[Chit chat](../chitchat/resources/**)
```

## Model description

You can include configuration information for your LUIS application or QnA Maker KB in the qna file to help direct the parser to handle the LU content correctly.

Here's how to add configuration information.

```.qna
> !# @\<property> = \<value>
> !# @\<scope>-\<property> = \<value>
> !# @\<scope>-\<property> = \<semicolon-delimited-key-value-pairs>
```

Note that any information explicitly passed in via CLI arguments will override information in the .qna file.

```.qna
> Parser instruction - this is optional; unless specified, parser will default to the latest version.
> !# @version = 1.0

> QnA Maker KB description
> !# @kb.name = my qna maker kb name

> Source for a specific QnA pair
> !# @qna.pair.source = <source value>
```

## Multi-turn content

Multi-turn content is represented in .qna format using markdown link notation. Links are specified via the following semantics.

```
- [display text](#<ID or question>)
```

You can optionally include `context-only` for any prompts that are only contextually available for this question. Read  the section about [adding an existing question-and-answer pair as a follow-up prompt][1] to learn more about use of `context`.

```.qna
- [tell me a joke](#?joke) `context-only`
```

### Follow-up prompts

Developers have two options for creating follow-up prompts: using a question as a follow-up prompt directly , or assigning an explicit ID to a QnA pair.

### Use a question directly

1. The first QnA pair that has the link text as a `question` will be added as the prompt. If you need more explicit control, use [IDs][assign-an-explicit-id-for-a-qna-pair] instead.
2. When you are directly using a question, use markdown convention and replace spaces with hyphen. e.g. `#?when-is-the-portland-store-open` .vs. `#?when is the portland store open`. The parser will do its best to find the link but this would not actually render as a clickable link in most Markdown renderers.

```.qna
# ?store hours
	```
		Most our stores are open M-F 9AM-10PM
	```
**Prompts:**
- [Seattle store](#?seattle)
- [Portland store](#?when-is-the-portland-store-open)

# ?seattle
	```
		Seattle store is open M-F 9AM-10PM
	```

# ?when is the portland store open
- portland store hours
	```
		Portland store is open 24x7.
	```
```

### Assign an explicit ID to a QnA pair

Assign IDs for each prompt with a number. You can see in the example below the prompt for each store has been assigned a different numeric value.

```.qna
# ?store hours
	```
		Most our stores are open M-F 9AM-10PM
	```
**Prompts:**
- [Seattle store](#1)
- [Portland store](#2)

<a id = "1"></a>

# ?seattle
	```
		Seattle store is open M-F 9AM-10PM
	```

<a id = "2"></a>

# ?when is the portland store open
- portland store hours
	```
		Portland store is open 24x7.
	```
```

## Additional Resources
- See [.lu file format](bot-builder-lu-file-format.md) for information about the .lu file format.

[1]:https://docs.microsoft.com/azure/cognitive-services/qnamaker/how-to/multiturn-conversation#add-a-new-question-and-answer-pair-as-a-follow-up-prompt
