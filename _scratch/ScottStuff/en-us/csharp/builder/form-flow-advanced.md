---
layout: page
title: FormFlow Advanced Features
permalink: /en-us/csharp/builder/formflow-advanced/
weight: 2410
parent1: Building your Bot Using Bot Builder for .NET
parent2: Chat Bots
---

<span style="color:red">This needs to be under FormFlow</span>


The example FormFlow dialog shown in [FormFlow](/en-us/csharp/builder/formflow-advanced/) is a basic FormFlow implementation that doesn't take advantage of FormFlow features that let you customize the user experience. The example in this section shows how you might update the example by:

* Adding some new field types including string and DateTime  
* Using attributes to add descriptions, terms, prompts and templates  
* Switching from fields to properties to incorporate business logic  
* Adding messages, flow control and confirmations  

<span style="color:red">What's the difference between fields and properties?</span>

### FormFlow attributes

FormFlow includes the following C# attributes that you can add to your class to better control the dialog.  

Attribute | Purpose
----------| -------
[Describe] | Changes how a field or a value is shown in a template or card
[Numeric] | Restricts the accepted values of a numeric field
[Optional]| Marks a field as optional
[Pattern] | Defines a regular expression to validate a string field
[Prompt]| Defines the prompt to use for a field
[Template] | Defines the template to use to generate prompts or values in prompts
[Terms] | Defines the input terms that match a field or value

<span style="color:red">Describe: "in a card?"</span>

### Using the Prompt attribute

The form automatically generates prompts for each field in your form. For example, the default prompt for SandwichOrder.Sandwich is "Please select a sandwich". To provide your own prompt for this field and others, use the Prompt attribute.

{% highlight csharp %}
        [Prompt("What kind of {&} would you like? {||}")]
        public SandwichOptions? Sandwich;
{% endhighlight %}

The prompt uses [Pattern Language](#patternlanguage) which populates pattern elements such as {&} and {\|\|} with form data at runtime. In this example, {&} is replaced with the description of the field and {\|\|} is replaced with the list of choices in the enumeration. By default, the description of a field is generated from the field's name, but you could also use a Describe attribute to specify your own description. The following shows the prompt for the above example. 

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

A prompt may also include parameters that affect how the form displays the prompt. For example, adding the ChoiceFormat parameter changes the format of the list of choices.

{% highlight csharp %}
        [Prompt("What kind of {&} would you like? {||}", ChoiceFormat="{1}")]
        public SandwichOptions? Sandwich;
{% endhighlight %}

In this case, the list of choices is not numbered.

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


You can use the Prompt attribute to override a single prompt or you can replace the templates that are used to automatically generate the prompts. The following example redefines the default template that controls how the form handles enumeration fields. In this case, the user may select only one enumeration value, and the form displays the values one per line. All enumeration fields will use the same prompt text which substitutes {&} with the field's name. 

{% highlight csharp %}
[Template(TemplateUsage.EnumSelectOne, "What kind of {&} would you like on your sandwich? {||}", ChoiceStyle = ChoiceStyleOptions.PerLine)]
public class SandwichOrder
{% endhighlight %}

The following shows the prompts for the cheese and bread fields. 

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

If you have a field that is optional, you could include a "None" value in the enumeration, but a better option is to use the Optional attribute. For example, not everyone may want cheese on their sandwich. The following shows how to include the Optional attribute.


{% highlight csharp %}
        [Optional]
        public CheeseOptions? Cheese;
{% endhighlight %}

An optional field includes a special No Preference choice. 

```
What kind of cheese would you like on your sandwich? (current choice: No Preference)
  1. American
  2. Monterey Cheddar
  3. Pepperjack
 >
```

And if the user has selected a value, the prompt includes the No Preference choice in the list.

```
What kind of cheese would you like on your sandwich? (current choice: American)
 1. American
 2. Monterey Cheddar
 3. Pepperjack
 4. No Preference
>
```

To interject some variation in the prompts and messages that the form generates, you can define multiple Pattern Language patterns to randomly select between. The following example redefines the TemplateUsage.NotUnderstood template to randomly select between two patterns when responding to unknown text.

{% highlight csharp %}
    [Template(TemplateUsage.NotUnderstood, "I do not understand \"{0}\".", "Try again, I don't get \"{0}\".")]
    [Template(TemplateUsage.EnumSelectOne, "What kind of {&} would you like on your sandwich? {||}")]
    public class SandwichOrder
{% endhighlight %}

Now when the user types something that is not understood, one of the two patterns will be randomly selected.

```
What size of sandwich do you want? (1. Six Inch, 2. Foot Long)
> two feet
I do not understand "two feet".
> two feet
Try again, I don't get "two feet"
> 
```


### Matching user input

When matching user input, terms are used to identify possible meanings for what was typed. By default, terms are generated by taking the field or value name and applying the following steps:

- Break on case changes and underscore (_).
- Generate each n-gram up to a maximum length.
- Add "s?" to the end of each word to support plurals. For example, the value AngusBeefAndGarlicPizza would generate: 'angus?', 'beefs?', 'garlics?', 'pizzas?', 'angus? beefs?', 'garlics? pizzas?' and 'angus beef and garlic pizza'. 

<span style="color:red">What's an "n-gram"?</span>

To override the terms used to match user input to a field or a value in a field, use the Terms attribute. The word "rotisserie" is a word that users are likely to misspell, so the following example shows how to use a regular expression to make it more likely that a match will occur. The example also includes the Terms.MaxPhrase parameter, which causes the Language.GenerateTerms to also generate variations. The example applies the Terms attribute to the RotisserieStyleChicken enumeration value.

{% highlight csharp %}
    [Terms(@"rotis\w* style chicken", MaxPhrase = 3)]
    RotisserieStyleChicken, SpicyItalian, SteakAndCheese, SweetOnionTeriyaki, Tuna,...
{% endhighlight %}

The following shows how Terms can improve matching.

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

You can also use attributes to validate values. For example, you can use the Numeric attribute to restrict the range of allowed numbers. The following example shows restricting the Rating field to numbers between 1 through 5. 

{% highlight csharp %}
        [Numeric(1, 5)]
        [Optional]
        [Describe("your experience today")]
        public double? Rating;
{% endhighlight %}

Similarly, you can use the Pattern attribute to specify a regular expression that validates a string field like PhoneNumber. 

{% highlight csharp %}
        [Pattern(@"(<Undefined control sequence>\d)?\s*\d{3}(-|\s*)\d{4}")]
        public string PhoneNumber;
{% endhighlight %}

### Passing in Initial Form State and Entities

When you launch a FormDialog, you can optionally pass in an instance of your state. If you do, any step (field) that contains a value will be skipped by default. To force the form to prompt the user for all fields, including those with values, pass FormOptions.PromptFieldsWithValues. If the field contains a value, the prompt will use the current value as the default value.

You can also pass in LUIS entities to bind to the state. If the EntityRecommendation.Type is a path to a field in your C# class, the EntityRecommendation.Entity will be passed through the recognizer to bind to your field. If the field is bound to an entity, the form will skip the step (just like with the initial state case).

### More advanced features

For more control over the user experience, including specifying the order of the form steps, adding validation, returning custom messages, and creating dynamic fields, see [Using FormBuilder to Customize a Form](/en-us/csharp/builder/formflow-formbuilder/).