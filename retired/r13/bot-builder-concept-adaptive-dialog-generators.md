---
title: Generators in adaptive dialogs
description: Providing Language Generation (LG) to your adaptive dialogs using Generators.
keywords: bot, generators, adaptive dialogs, language generation, lg
author: emgrol
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 05/16/2020
monikerRange: 'azure-bot-service-4.0'
---

# Generators in adaptive dialogs

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Generators tie a specific [language generation (LG)](bot-builder-concept-language-generation.md) system to an adaptive dialog.
In this article you'll learn about the LG templates that add variety and personality to bot responses, and how to call those templates in your root dialog using the `TemplateEngineLanguageGenerator`.

## Prerequisites

- Knowledge of [adaptive dialogs](bot-builder-adaptive-dialog-Introduction.md)
- Knowledge of [recognizers](bot-builder-concept-adaptive-dialog-recognizers.md) in adaptive dialogs
- Knowledge of [adaptive expressions](bot-builder-concept-adaptive-expressions.md)

## Language generation

Language Generation (LG) allows developers to extract embedded strings from their code and resource files and manage them through an LG runtime and file format. With LG, developers can create a more natural conversational experience by defining multiple variations on a phrase, executing simple expressions based on context, and referring to conversational memory.

LG, along with [recognizers](bot-builder-concept-adaptive-dialog-recognizers.md), enables clean separation and encapsulation of a dialog's Language Understanding and language generation assets. Recognizers give your bot the ability to understand input and LG lets your bot respond to that input in an intelligent way.

## LG templates

Templates are the core concept of the language generation system. Variations in templates are used by your bot to respond to user input. Each template has a name and one of the following:

- a list of one-of variation text values
- a structured content definition
- a collection of conditions, each with:
    - an [adaptive expression](bot-builder-concept-adaptive-expressions.md)
    - a list of one-of variation text values per condition

Templates are defined in [.lg](../file-format/bot-builder-lg-file-format.md) files, which can contain one or more templates. There are three types of templates:

- [simple response](../file-format/bot-builder-lg-file-format.md#simple-response-template)
- [conditional response](../file-format/bot-builder-lg-file-format.md#conditional-response-template)
- [structured response](../file-format/bot-builder-lg-file-format.md#structured-response-template)

### Simple response template

A simple response template includes one or more variations of text that are used for composition and expansion. When this template is evaluated, the LG runtime will randomly pick one of these variations.

Here's an example of a simple response template with two variations:

```lg
> Greeting template with 2 variations.
# GreetingPrefix
- Hi
- Hello
```

### Conditional response template

Conditional response templates let you author content that's selected based on a condition. All conditions are expressed using [adaptive expressions](bot-builder-concept-adaptive-expressions.md), both [prebuilt](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md) and [custom](../language-generation/bot-builder-howto-use-lg-custom-functions.md).

There are two types of conditional response templates: [if-else](../file-format/bot-builder-lg-file-format.md#if-else-template) and [switch](../file-format/bot-builder-lg-file-format.md#switch-template).

#### If-else template

The if-else template lets you build a template that picks a collection based on a cascading order of conditions. Like simple response templates, can have inline variations in IF or ELSE blocks. Nesting is achieved through [composition](#structured-response-template).

Here's an example of an if-else conditional response template:

```lg
> time of day greeting reply template with conditions.
# timeOfDayGreeting
- IF: ${timeOfDay == 'morning'}
    - Good morning!
    - Wake up.
- ELSE:
    - Good evening
    - Sleep well!
    - Goodnight.
```

#### Switch template

The switch template lets you design a template that matches an expression's value to a CASE clause and produces output based on that case.

Here's an example of a switch conditional response template:

```lg
# TestTemplate
- SWITCH: ${condition}
- CASE: ${case-expression-1}
    - output1
- CASE: ${case-expression-2}
    - output2
- DEFAULT:
   - final output
```

### Structured response template

Structured response templates let you define a complex structure that supports major LG functionality, like templating, composition, and substitution, while leaving the interpretation of the structured response up to the caller of the LG library. Activity and card definitions are currently supported for bot applications.

Here's the definition of a structured response template:

```lg
# TemplateName
> this is a comment
[Structure-name
    Property1 = <plain text> .or. <plain text with template reference> .or. <expression>
    Property2 = list of values are denoted via '|'. e.g. a | b
> this is a comment about this specific property
    Property3 = Nested structures are achieved through composition
]
```

Read [structured response template](../language-generation/language-generation-structured-response-template.md) for more information and examples of complex templates.

## Call templates in your root dialog

After creating templates for your bot you can add them to your adaptive dialog. You can set the generator to an .lg file or set the generator to a `TemplateEngineLanguageGenerator` instance where you explicitly manage the one or more .lg files. The example below shows the latter approach.

<!--### [C#](#tab/csharp)-->

Say you want to add templates from **RootDialog.lg** to an adaptive dialog. Add the following packages to your code:

```csharp
using Microsoft.Bot.Builder.LanguageGeneration;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Templates;
using Microsoft.Bot.Builder.Dialogs.Adaptive.Generators;

```

Then you need to resolve the path of the .lg file:

```csharp
string[] paths = { ".", "Dialogs", "RootDialog.lg" };
string fullPath = Path.Combine(paths);
```

This will ensure that you're calling the correct template files for you bot.

Now you can create the `TemplateEngineLanguageGenerator` in your adaptive dialog that manages the templates in **RootDialog.lg**:

```csharp
var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog)){
.
.
.
Generator = new TemplateEngineLanguageGenerator(Templates.ParseFile(fullPath))
}
```

You can now call templates in your bot by name, using the format `${<template-name>}`.

```csharp
new SendActivity("${FinalUserProfileReadOut()}")
```

In the example above, the bot calls the `FinalUserProfileReadOut` template and responds with the contents of the template.

<!--### [Javascript](#tab/javascript)

Say you want to add templates from **RootDialog.lg** to an adaptive dialog. Add the following lines to your code to require the necessary packages:

```javascript
const { NumberInput, AttachmentInput, ConfirmInput, IfCondition, ActivityTemplate, AdaptiveDialog, TextInput, SendActivity, TemplateEngineLanguageGenerator, OnBeginDialog } = require('botbuilder-dialogs-adaptive');
const { Templates } = require('botbuilder-lg');
```

Resolve the path using `path.join()` and use the `Templates` constant to parse the **rootDialog.lg** file:

```javascript
const lgFile = Templates.parseFile(path.join(__dirname, 'rootDialog.lg'));
```

By joining the path you ensure that you are calling the correct template files for your bot.

Now you can create the `TemplateEngineLanguageGenerator` to manage the templates in **rootDialog.lg**:

```javascript
const rootDialog = new AdaptiveDialog(ROOT_DIALOG).configure(
.
.
.
generator: new TemplateEngineLanguageGenerator(lgFile)
```

You can now call templates in your bot by name, using the format `${<template-name>}`.

```javascript
new SendActivity('${FinalUserProfileReadOut()}')
```

In the example above, the bot calls the `FinalUserProfileReadOut` template and responds with the contents of the template.

---    -->

## Additional Information

- [.lg file format](../file-format/bot-builder-lg-file-format.md)
- [Structured response template](../language-generation/language-generation-structured-response-template.md)
- [Adaptive expression prebuilt functions](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md)
