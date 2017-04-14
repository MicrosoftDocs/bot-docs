---
title: Customize user experience with pattern language | Microsoft Docs
description: Learn how to override default settings to provide a customized user experience using Formflow and the Bot Builder SDK for .NET.
author: kbrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/31/2017
ms.reviewer:
ROBOTS: Index, Follow
---

# Customize user experience with pattern language

When you customize a prompt or override a default template, you can 
use pattern language to specify the contents and/or format of the prompt. 

## Prompts and templates

A [prompt][promptAttribute] defines the message that is sent to the user to request a piece of information or ask for confirmation. You can customize a prompt by using the [Prompt attribute](~/dotnet/formflow-advanced.md) or implicitly through [IFormBuilder<T>.Field][field]. 

Forms use templates to automatically construct prompts and other things such as help. 
You can override the default template of a class or field by using the [Template attribute](~/dotnet/formflow-advanced.md). 

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

[!code-csharp[Patterns example](~/includes/code/dotnet-formflow-pattern-language.cs#patterns1)]

This is the resulting prompt:

```
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

[!code-csharp[Patterns example](~/includes/code/dotnet-formflow-pattern-language.cs#patterns2)]

This is the resulting prompt:

```
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

## Additional resources

- [FormFlow](~/dotnet/formflow.md)
- [Advanced features of FormFlow](~/dotnet/formflow-advanced.md)
- [Customize a form using FormBuilder](~/dotnet/formflow-formbuilder.md)
- [Localize form content](~/dotnet/formflow-localize.md)
- [Define a form using JSON schema](~/dotnet/formflow-json-schema.md)

[promptAttribute]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d0/d34/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_prompt_attribute.html

[field]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d4/d1c/interface_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_i_form_builder.html#ad7881ee9b6a31bcd4acf2033eca8d097

[formConfiguration]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/de/db8/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_form_configuration.html#a40fe1b7c1bb62d7ae2accb8501a597a5

[separator]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d7/d48/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_advanced_1_1_template_base_attribute.html#aa55384cb431ff75190efe7abd2fdb6a7

[lastSeparator]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d7/d48/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_advanced_1_1_template_base_attribute.html#a72a59330fcdc04cf70b922c6d217d44c

[templateUsage]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/dd/df7/namespace_microsoft_1_1_bot_1_1_builder_1_1_form_flow.html#a28ef6a551a3611e4a6abe06659797313

[caseNormalization]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/dd/df7/namespace_microsoft_1_1_bot_1_1_builder_1_1_form_flow.html#a4b3fef3ebd0b6d6f84591f4430ae2fd5

[choiceStyleOptions]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/dd/df7/namespace_microsoft_1_1_bot_1_1_builder_1_1_form_flow.html#ac6396117e96818e92b4892d316e326d9

[feedbackOptions]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/dd/df7/namespace_microsoft_1_1_bot_1_1_builder_1_1_form_flow.html#ac4ead01e3c8eeb1424f886479af0adf5
