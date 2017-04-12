---
title: Advanced features of FormFlow in the Bot Builder SDK for .NET | Microsoft Docs
description: Learn how to use advanced features of FormFlow with the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, FormFlow, attributes, business logic, initial state, entities
author: kbrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/29/2017
ms.reviewer:
#ROBOTS: Index
---

# Advanced features of FormFlow

[FormFlow](~/dotnet/formflow.md) describes a basic FormFlow 
implementation that delivers a fairly generic user experience. 
To deliver a more customized user experience using FormFlow, you can specify initial form state, 
add business logic to manage interdependencies between fields and process user input, 
and use attributes to customize prompts, override templates, 
designate optional fields, match user input, and validate user input. 

## Specify initial form state and entities

When you launch a [FormDialog][formDialog], you may optionally pass in an instance of your state. 
If you do pass in an instance of your state, then by default, 
FormFlow will skip steps for any fields that already contain values; 
the user will not be prompted for those fields. 
To force the form to prompt the user for all fields (including those fields that already contain values in the 
initial state), pass in [FormOptions.PromptFieldsWithValues][promptFieldsWithValues] when you launch 
the `FormDialog`. If a field contains an initial value, the prompt will use that value as the default value.

You can also pass in [LUIS](http://luis.ai/) entities to bind to the state. 
If the `EntityRecommendation.Type` is a path to a field in your C# class, 
the `EntityRecommendation.Entity` will be passed through the recognizer to bind to your field. 
FormFlow will skip steps for any fields that are bound to an entity; 
the user will not be prompted for those fields. 

##<a id="business-logic"></a> Add business logic 

To handle interdependencies between form fields or apply specific logic during the process of getting or setting 
a field value, you can specify business logic within a validation function. 
A validation function lets you manipulate the state and return a [ValidateResult][validateResult] object that can contain: 

- a feedback string that describes the reason that a value is invalid
- a transformed value
- a set of choices for clarifying a value

This code example shows a validation function for the `Toppings` field. 
If input for the field contains the `ToppingOptions.Everything` enumeration value, the function 
ensures that the `Toppings` field value contains the full list of toppings.

[!code-csharp[Validation function](~/includes/code/dotnet-formflow-advanced.cs#validationFunction)]

In addition to the validation function, you can add the [Term](#terms-attribute) attribute 
to match user expressions such as "everything" or "not".

[!code-csharp[Terms for Toppings](~/includes/code/dotnet-formflow-advanced.cs#toppingsTerms)]

Using the validation function shown above, this snippet shows the 
interaction between bot and user when the user requests "everything but Jalapenos." 

```
Please select one or more toppings (current choice: No Preference)
 1. Everything
 2. Avocado
 3. Banana Peppers
 4. Cucumbers
 5. Green Bell Peppers
 6. Jalapenos
 7. Lettuce
 8. Olives
 9. Pickles
 10. Red Onion
 11. Spinach
 12. Tomatoes
> everything but jalapenos
For sandwich toppings you have selected Avocado, Banana Peppers, Cucumbers, Green Bell Peppers, Lettuce, Olives, Pickles, Red Onion, Spinach, and Tomatoes.
```

## FormFlow attributes

You can add these C# attributes to your class to customize behavior of a FormFlow dialog.

| Attribute | Purpose |
|----|----| 
| [Describe][describeAttribute] | Alter how a field or a value is shown in a template or card |
| [Numeric][numericAttribute] | Restrict the accepted values of a numeric field |
| [Optional][optionalAttribute] | Mark a field as optional |
| [Pattern][patternAttribute] | Define a regular expression to validate a string field |
| [Prompt][promptAttribute] | Define the prompt for a field |
| [Template][templateAttribute] | Define the template to use to generate prompts or values in prompts |
| [Terms][termsAttribute] | Define the input terms that match a field or value |

##<a id="prompt-attribute"></a> Customize prompts using the Prompt attribute

Default prompts are automatically generated for each field in your form, 
but you can specify a custom prompt for any field by using the `Prompt` attribute. 
For example, if the default prompt for the `SandwichOrder.Sandwich` field is "Please select a sandwich", 
you can add the `Prompt` attribute to specify a custom prompt for that field.

[!code-csharp[Prompt attribute](~/includes/code/dotnet-formflow-advanced.cs#promptAttribute)]

This example uses [pattern language](~/dotnet/formflow-pattern-language.md) 
to dynamically populate the prompt with form data at runtime: `{&}` is replaced with the description 
of the field and `{||}` is replaced with the list of choices in the enumeration. 

> [!NOTE]
> By default, the description of a field is generated from the field's name. 
> To specify a custom description for a field, add the `Describe` attribute.

This snippet shows the customized prompt that is specified by the example above.

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

A `Prompt` attribute may also specify parameters that affect how the form displays the prompt. 
For example, the `ChoiceFormat` parameter determines how the form renders the list of choices.

[!code-csharp[Prompt attribute ChoiceFormat parameter](~/includes/code/dotnet-formflow-advanced.cs#promptChoice)]

In this example, the value of the `ChoiceFormat` parameter indicates that the choices should be 
displayed as a bulleted list (instead of a numbered list).

```
What kind of sandwich would you like?
- BLT
- Black Forest Ham
- Buffalo Chicken
- Chicken And Bacon Ranch Melt
- Cold Cut Combo
- Meatball Marinara
- Oven Roasted Chicken
- Roast Beef
- Rotisserie Style Chicken
- Spicy Italian
- Steak And Cheese
- Sweet Onion Teriyaki
- Tuna
- Turkey Breast
- Veggie
>
```

##<a id="template-attribute"></a> Customize prompts using the Template attribute

While the `Prompt` attribute enables you to customize the prompt for a single field, 
the `Template` attribute enables you to replace the default templates that FormFlow uses to automatically 
generate prompts. 
This code example uses the `Template` attribute to redefine how the form handles 
all enumeration fields. The attribute indicates that the user may select only one item, 
sets the prompt text by using [pattern language](~/dotnet/formflow-pattern-language.md), 
and specifies that the form should display only one item per line. 

[!code-csharp[Template attribute](~/includes/code/dotnet-formflow-advanced.cs#templateAttribute)]

This snippet shows the resulting prompts for the `Bread` field and `Cheese` field.

```
What kind of bread would you like on your sandwich?
 1. Nine Grain Wheat
 2. Nine Grain Honey Oat
 3. Italian
 4. Italian Herbs And Cheese
 5. Flatbread
> 

What kind of cheese would you like on your sandwich? 
 1. American
 2. Monterey Cheddar
 3. Pepperjack
> 
```

If you use the `Template` attribute to replace the default templates that FormFlow uses to 
generate prompts, you may want to interject some variation into the prompts and messages 
that the form generates. 
To do so, you can define multiple text strings using 
[pattern language](~/dotnet/formflow-pattern-language.md), and the form will randomly choose 
from the available options each time it needs to display a prompt or message.

This code example redefines the [TemplateUsage.NotUnderstood][notUnderstood] 
template to specify two different variations of 
message. When the bot needs to communicate that it does not understand a user's input, it will 
determine message contents by randomly selecting one of the two text strings. 

[!code-csharp[Template variations of message](~/includes/code/dotnet-formflow-advanced.cs#templateMessages)]

This snippet shows an example of the resulting the interaction between bot and user. 

```
What size of sandwich do you want? (1. Six Inch, 2. Foot Long)
> two feet
I do not understand "two feet".
> two feet
Try again, I don't get "two feet"
> 
```

## Designate a field as optional using the Optional attribute

To designate a field as optional, use the `Optional` attribute. 
This code example specifies that the `Cheese` field is optional.

[!code-csharp[Optional attribute](~/includes/code/dotnet-formflow-advanced.cs#optionalAttribute)]

If a field is optional and no value has been specified, 
the current choice will be displayed as "No Preference".

```
What kind of cheese would you like on your sandwich? (current choice: No Preference)
  1. American
  2. Monterey Cheddar
  3. Pepperjack
 >
```

If a field is optional and the user has specified a value, 
"No Preference" will be displayed as the last choice in the list.

```
What kind of cheese would you like on your sandwich? (current choice: American)
 1. American
 2. Monterey Cheddar
 3. Pepperjack
 4. No Preference
>
```

##<a id="terms-attribute"></a> Match user input using the Terms attribute

When a user sends a message to a bot that is built using FormFlow, 
the bot attempts to identify the meaning of the user's input by matching the input to a list of terms. 
By default, the list of terms is generated by applying these steps to the field or value: 

1. Break on case changes and underscore (_).
2. Generate each <a href="https://en.wikipedia.org/wiki/N-gram" target="_blank">n-gram</a> up to a maximum length.
3. Add "s?" to the end of each word (to support plurals). 

For example, the value "AngusBeefAndGarlicPizza" would generate these terms: 

- 'angus?'
- 'beefs?'
- 'garlics?'
- 'pizzas?'
- 'angus? beefs?'
- 'garlics? pizzas?'
- 'angus beef and garlic pizza'

To override this default behavior and define the list of terms that are used to match 
user input to a field or a value in a field, use the `Terms` attribute. 
For example, you may use the `Terms` attribute (with a regular expression) to account for the fact that users are 
likely to misspell the word "rotisserie." 

[!code-csharp[Terms attribute](~/includes/code/dotnet-formflow-advanced.cs#termsAttribute)]

By using the `Terms` attribute, you increase the likelihood of 
being able to match user input with one of the valid choices. 
The `Terms.MaxPhrase` parameter in this example causes the `Language.GenerateTerms` to generate additional variations of terms. 

This snippet shows the resulting interaction between bot and user when the user misspells "Rotisserie."

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
> rotissary checkin
For sandwich I understood Rotisserie Style Chicken. "checkin" is not an option.
```

## Validate user input using the Numeric attribute or Pattern attribute

To restrict the range of allowed values for a numeric field, use the `Numeric` attribute. 
This code example uses the `Numeric` attribute to specify that input for the `Rating` field 
must be a number between 1 and 5. 

[!code-csharp[Numeric attribute](~/includes/code/dotnet-formflow-advanced.cs#numericAttribute)]

To specify the required format for the value of a particular field, use the `Pattern` attribute. 
This code example uses the `Pattern` attribute to specify the required format for the value of the 
`PhoneNumber` field.

[!code-csharp[Pattern attribute](~/includes/code/dotnet-formflow-advanced.cs#patternAttribute)]

## Summary

This article has described how to deliver a customized user experience with FormFlow 
by specifying initial form state, 
adding business logic to manage interdependencies between fields and process user input, 
and using attributes to customize prompts, override templates, 
designate optional fields, match user input, and validate user input. 
For information about additional ways to customize the user experience with FormFlow, 
see [Customize a form using FormBuilder](~/dotnet/formflow-formbuilder.md).

## Additional Resources

- [FormFlow](~/dotnet/formflow.md)
- [Customize a form using FormBuilder](~/dotnet/formflow-formbuilder.md)
- [Localize form content](~/dotnet/formflow-localize.md)
- [Define a form using JSON schema](~/dotnet/formflow-json-schema.md)
- [Customize user experience with pattern language](~/dotnet/formflow-pattern-language.md)

[formDialog]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/db/de5/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_form_dialog.html

[promptFieldsWithValues]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/dd/df7/namespace_microsoft_1_1_bot_1_1_builder_1_1_form_flow.html#a27be39918296ad3536d6a5c8d6ed5394aa796e574d8cf50f288b2e44ecd64764a

[validateResult]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d4/d7b/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_validate_result.html

[describeAttribute]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/dd/d38/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_describe_attribute.html

[numericAttribute]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/df/d31/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_numeric_attribute.html

[optionalAttribute]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d7/d6f/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_optional_attribute.html

[patternAttribute]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/da/d2b/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_pattern_attribute.html

[promptAttribute]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d0/d34/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_prompt_attribute.html

[templateAttribute]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d7/d0a/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_template_attribute.html

[termsAttribute]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d2/d27/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_terms_attribute.html

[notUnderstood]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/dd/df7/namespace_microsoft_1_1_bot_1_1_builder_1_1_form_flow.html#a28ef6a551a3611e4a6abe06659797313aca296fd71c8deadb1bccdad8e097bc50