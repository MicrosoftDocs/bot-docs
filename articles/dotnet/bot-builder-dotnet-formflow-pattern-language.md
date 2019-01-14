---
title: Customize user experience with pattern language | Microsoft Docs
description: Learn how to customize FormFlow prompts and override FormFlow templates by using pattern language with the Bot Framework SDK for .NET.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Customize user experience with pattern language

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

When you customize a prompt or override a default template, you can 
use pattern language to specify the contents and/or format of the prompt. 

## Prompts and templates

A [prompt][promptAttribute] defines the message that is sent to the user to request a piece of information or ask for confirmation. You can customize a prompt by using the [Prompt attribute](bot-builder-dotnet-formflow-advanced.md#customize-prompts-using-the-prompt-attribute) or implicitly through [IFormBuilder<T>.Field][field]. 

Forms use templates to automatically construct prompts and other things such as help. 
You can override the default template of a class or field by using the [Template attribute](bot-builder-dotnet-formflow-advanced.md#customize-prompts-using-the-template-attribute). 

> [!TIP]
> The [FormConfiguration.Templates][formConfiguration] 
> class defines a set of built-in templates that provide good examples of how to use pattern language.

## Elements of pattern language

Pattern language uses curly braces (`{}`) to identify elements that will be replaced at runtime with actual 
values. This table lists the elements of pattern language.

| Element | Description |
|----|----|
| `{<format>}` | Shows the value of the current field (the field that the attribute applies to). |
| `{&}` | Shows the description of the current field (unless otherwise specified, this is the name of the field). |
| `{<field><format>}` | Shows the value of the named field. | 
| `{&<field>}` | Shows the description of the named field. |
| <code>{&#124;&#124;}</code> | Shows the current choice(s), which could be the current value of a field, "no preference" or the values of an enumeration. |
| `{[{<field><format>} ...]}` | Shows a list of values from the named fields using [Separator][separator] and [LastSeparator][lastSeparator] to separate the individual values in the list. |
| `{*}` | Shows one line for each active field; each line contains the field description and current value. | 
| `{*filled}` | Shows one line for each active field that contains an actual value; each line contains the field description and current value. |
| `{<nth><format>}` | A regular C# format specifier that applies to the nth argument of a template. For the list of available arguments, see [TemplateUsage][templateUsage]. |
| `{?<textOrPatternElement>...}` | Conditional substitution. If all referred to pattern elements have values, the values are substituted and the whole expression is used. |

For the elements listed above:

- The `<field>` placeholder is the name of a field in your form class. For example, if your class contains a field with the name `Size`, you could specify `{Size}` as the pattern element. 

- An ellipses (`"..."`) within a pattern element indicates that the element may contain multiple values.

- The `<format>` placeholder is a C# format specifier. For example, if the class contains a field with the name `Rating` and of type `double`, you could specify `{Rating:F2}` as the pattern element to show two digits of precision.

- The `<nth>` placeholder references the nth argument of a template.

### Pattern language within a Prompt attribute

This example uses the `{&}` element to show the description of the `Sandwich` field and the 
`{||}` element to show the list of choices for that field.

[!code-csharp[Patterns example](../includes/code/dotnet-formflow-pattern-language.cs#patterns1)]

This is the resulting prompt:

```console
What kind of sandwich would you like?
1. BLT
2. Black Forest Ham
3. Buffalo Chicken
4. Chicken And Bacon Ranch Melt
5. Cold Cut Combo
6. Meatball Marinara
7. Oven Roasted Chicken
8. Roast Beef
9. Rotisserie Style Chicken
10. Spicy Italian
11. Steak And Cheese
12. Sweet Onion Teriyaki
13. Tuna
14. Turkey Breast
15. Veggie
>
```

## Formatting parameters

Prompts and templates support these formatting parameters.

| Usage | Description |
|----|----|
| `AllowDefault` | Applies to <code>{&#124;&#124;}</code> pattern elements. Determines whether the form should show the current value of the field as a possible choice. If `true`, the current value is shown as a possible value. The default is `true`. |
| `ChoiceCase` | Applies to <code>{&#124;&#124;}</code> pattern elements. Determines whether the text of each choice is normalized (e.g., whether the first letter of each word is capitalized). The default is `CaseNormalization.None`. For possible values, see [CaseNormalization][caseNormalization]. |
| `ChoiceFormat` | Applies to <code>{&#124;&#124;}</code> pattern elements. Determines whether to show a list of choices as a numbered list or a bulleted list. For a numbered list, set `ChoiceFormat` to `{0}` (default). For a bulleted list list, set `ChoiceFormat` to `{1}`. |
| `ChoiceLastSeparator` | Applies to <code>{&#124;&#124;}</code> pattern elements. Determines whether an inline list of choices includes a separator before the last choice. |
| `ChoiceParens` | Applies to <code>{&#124;&#124;}</code> pattern elements. Determines whether an inline list of choices is shown within parentheses. If `true`, the list of choices is shown within parentheses. The default is `true`. |
| `ChoiceSeparator` | Applies to <code>{&#124;&#124;}</code> pattern elements. Determines whether an inline list of choices includes a separator before every choice except the last choice. | 
| `ChoiceStyle` | Applies to <code>{&#124;&#124;}</code> pattern elements. Determines whether the list of choices is shown inline or per line. The default is `ChoiceStyleOptions.Auto` which determines at runtime whether to show the choice inline or in a list. For possible values, see [ChoiceStyleOptions][choiceStyleOptions]. |
| `Feedback` | Applies to prompts only. Determines whether the form echoes the user's choice to indicate that the form understood the selection. The default is `FeedbackOptions.Auto` which echoes the user's input only if part of it is not understood. For possible values, see [FeedbackOptions][feedbackOptions]. |
| `FieldCase` | Determines whether the text of the field's description is normalized (e.g., whether the first letter of each word is capitalized). The default is `CaseNormalization.Lower` which converts the description to lowercase. For possible values, see [CaseNormalization][caseNormalization]. |
| `LastSeparator` | Applies to `{[]}` pattern elements. Determines whether an array of items includes a separator before the last item. |
| `Separator` | Applies to `{[]}` pattern elements. Determines whether an array of items includes a separator before every item in the array except the last item. |
| `ValueCase` | Determines whether the text of the field's value is normalized (e.g., whether the first letter of each word is capitalized)_. The default is `CaseNormalization.InitialUpper` which converts the first letter of each word to uppercase. For possible values, see [CaseNormalization][caseNormalization]. |

### Prompt attribute with formatting parameter 

This example uses the `ChoiceFormat` parameter to specify that the list of choices should be displayed 
as a bulleted list.

[!code-csharp[Patterns example](../includes/code/dotnet-formflow-pattern-language.cs#patterns2)]

This is the resulting prompt:

```console
What kind of sandwich would you like?
* BLT
* Black Forest Ham
* Buffalo Chicken
* Chicken And Bacon Ranch Melt
* Cold Cut Combo
* Meatball Marinara
* Oven Roasted Chicken
* Roast Beef
* Rotisserie Style Chicken
* Spicy Italian
* Steak And Cheese
* Sweet Onion Teriyaki
* Tuna
* Turkey Breast
* Veggie
>
```

## Sample code

[!INCLUDE [Sample code](../includes/snippet-dotnet-formflow-samples.md)]

## Additional resources

- [Basic features of FormFlow](bot-builder-dotnet-formflow.md)
- [Advanced features of FormFlow](bot-builder-dotnet-formflow-advanced.md)
- [Customize a form using FormBuilder](bot-builder-dotnet-formflow-formbuilder.md)
- [Localize form content](bot-builder-dotnet-formflow-localize.md)
- [Define a form using JSON schema](bot-builder-dotnet-formflow-json-schema.md)
- <a href="/dotnet/api/?view=botbuilder-3.11.0" target="_blank">Bot Framework SDK for .NET Reference</a>

[promptAttribute]: /dotnet/api/microsoft.bot.builder.formflow.promptattribute

[field]: /dotnet/api/microsoft.bot.builder.formflow.iformbuilder-1.field

[formConfiguration]: /dotnet/api/microsoft.bot.builder.formflow.formconfiguration

[separator]: /dotnet/api/microsoft.bot.builder.formflow.advanced.templatebaseattribute.separator

[lastSeparator]: /dotnet/api/microsoft.bot.builder.formflow.advanced.templatebaseattribute.lastseparator

[templateUsage]: /dotnet/api/microsoft.bot.builder.formflow.templateusage

[caseNormalization]: /dotnet/api/microsoft.bot.builder.formflow.casenormalization

[choiceStyleOptions]: /dotnet/api/microsoft.bot.builder.formflow.choicestyleoptions

[feedbackOptions]: /dotnet/api/microsoft.bot.builder.formflow.feedbackoptions
