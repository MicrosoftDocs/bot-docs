---
layout: page
title: Pattern Language
permalink: /en-us/csharp/builder/formflow-pattern-language/
weight: 2490
parent1: Building your Bot Using Bot Builder for .NET
parent2: Chat Bots
---

<span style="color:red">This needs to be under FormFlow</span>

The framework supports a pattern language that you can use in [Prompt]() and [Template]() attributes to customize the user experience.
The language uses curly braces ({}) to identify the pattern elements whose values are determined at runtime. The following are the possible pattern elements.


|Pattern Element | Description
|--------------- | -----------
|{\<format\>} | Shows the value of the current field (the field that the attribute applies to)
|{&} | Shows the description of the current field (unless otherwise specified, this is the name of the field)
|{\<field\>\<format\>} | Shows the value of the named field 
|{&\<field\>} | Shows the description of the named field
|{\|\|} | Shows the current choices, which are typically the values of an enumeration
|{[{\<field\>\<format\>} ...]} | Shows a list of values from the named fields using [Separator]() and [LastSeparator]() to separate the individual values
|{*} | Shows on separate lines the description and current value of each active field 
|{*filled} | Shows on separate lines the description and current value of each active field that contains an actual value
|{\<nth\>\<format\>} | A regular C# format specifier that applies to the nth argument of a template (see TemplateUsage for a list of available arguments)
|{?\<textOrPatternElement\>...} | Conditional substitution. If all referred to pattern elements have values, the values are substituted and the whole expression is used

<span style="color:red">For {*}, what does "active" field mean?</span>

<span style="color:red">?<textOrPatternElement>...} needs more explanation or an example.</span>

- The \<field\> placeholder is the name of a field in your form class. For example, if your class contains a field named, Size, you would use {Size} as the pattern element. 

<span style="color:red">Does the named field have to be public?</span>

- An ellipses ("...") within a pattern element indicates that the element may contain multiple values.

- The \<format\> placeholder is a C# format specifier. For example, if the class contains a field named, Rating, that is a double, you could use {Rating:F2} as the pattern element to show two digits of precision.

- The \<nth\> placeholder references the nth argument of a template.

The following example shows using the description and choice pattern elements.

{% highlight csharp %}
        [Prompt("What kind of {&} would you like? {||}")]
        public SandwichOptions? Sandwich;
{% endhighlight %}

The following shows the rendered prompt of the above example.

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

The **FormConfiguration.Templates** class defines a set of built-in templates. Forms use templates to automatically construct prompts and other things such as help. It is worthwhile to review the built-in templates to familiarize yourself with pattern language usage. You can specify a Prompt as an attribute of a field or implicitly through IFormBuilder<T>.Field. You can override the default template of a class or field. 

<span style="color:red">Need to further define IFormBuilder<T>.Field usage.</span>

<span style="color:red">Templates need more explanation - I have no idea where or how to use templates based on what I've read.</span>

Both prompts and templates support the following formatting parameters.

|Usage | Description
|------|------------
|AllowDefault | Applies to {\|\|} patten elements. Determines whether the form should show the current value of the field as a possible choice. If true, the current value is shown as a possible value. The default is true. 
|ChoiceCase | Applies to {\|\|} patten elements. Determines whether the text of choice is normalized. For example, whether the first letter of each word is capitalized. The default is CaseNormalization.None. For possible values, see [CaseNormalization]().
|ChoiceFormat | Applies to {\|\|} patten elements. Determines whether to use a numbered list of values or simply a list of values. To number each choice, set ChoiceFormat to {0} (default). To use an unnumbered list of the choice's, set ChoiceFormat to {1}. 
|ChoiceLastSeparator | Applies to {\|\|} patten elements. Determines whether an inline list of choices includes a separator before the last choice. 
|ChoiceParens | Applies to {\|\|} patten elements. Determines whether an inline list of choices is shown within parentheses. If true, the list of choices is shown within parentheses. The default is true. 
|ChoiceSeparator | Applies to {\|\|} patten elements. Determines whether an inline list of choices includes a separator before every choice except the last choice. 
|ChoiceStyle | Applies to {\|\|} patten elements. Determines whether the list of choices is shown inline or per line. The default is ChoiceStyleOptions.Auto which determines at runtime whether to show the choice inline or in a list. For possible values, see [ChoiceStyleOptions]().  
|Feedback | Determines whether the form echoes the user's choice to indicate that the form understood the selection. The default is FeedbackOptions.Auto which echoes the user's input only if part of it is not understood. For possible values, see [FeedbackOptions]().
|FieldCase | Determines whether the text of the field's description is normalized. For example, whether the first letter of each word is capitalized. The default is CaseNormalization.Lower which converts the description to lowercase. For possible values, see [CaseNormalization]().
|LastSeparator | Determines whether an array of items includes a separator before the last item. Applies to {[]} patten elements.
|Separator | Determines whether an array of items includes a separator before every item in the array except the last item. Applies to {[]} patten elements.
|ValueCase | Determines whether the text of the field's value is normalized. For example, whether the first letter of each word is capitalized. The default is CaseNormalization.InitialUpper which converts the first letter of each word to uppercase. For possible values, see [CaseNormalization]().

<span style="color:red">Is AllowDefault a boolean?</span>

<span style="color:red">For ChoiceCase, what is case normalization? What are the possible values?</span>

<span style="color:red">Is ChoiceLastSeparator a boolean?</span>

<span style="color:red">Is ChoiceParens a boolean? Does this apply only if ChoiceStyle is inline? What happens if you set this to true and ChoiceStyle is inlineNoParen?</span>

<span style="color:red">Is ChoiceSeparator a boolean?</span>

<span style="color:red">Feedback needs more information</span>

<span style="color:red">For FieldCase and ValueCase, what is case normalization?</span>

The following shows how to use the ChoiceFormat parameter to change how the form renders the list of choices.

{% highlight csharp %}
        [Prompt("What kind of {&} would you like? {||}", ChoiceFormat="{1}")]
        public SandwichOptions? Sandwich;
{% endhighlight %}

The following shows how the ChoiceFormat parameter affects the rendered list (compare this list to the previous example).

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

