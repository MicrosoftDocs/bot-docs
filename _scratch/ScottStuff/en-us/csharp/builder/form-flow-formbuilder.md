---
layout: page
title: Using FormBuilder to Customize a Form
permalink: /en-us/csharp/builder/formflow-formbuilder/
weight: 2420
parent1: Building your Bot Using Bot Builder for .NET
parent2: Chat Bots
---

<span style="color:red">This needs to be under FormFlow</span>


In addition to using attributes to customize the user experience, you can use FormBuilder to specify the order of the form's steps, add validation, return custom messages, and create dynamic fields. By default, the steps specified in the builder are executed in order. (Steps might be skipped if there is already a value, or if there is explicit navigation.) 

The following shows a more complex usage of FormBuilder.

{% highlight csharp %}
        public static IForm<SandwichOrder> BuildForm()
        {
            OnCompletionAsyncDelegate<SandwichOrder> processOrder = async (context, state) =>
            {
                await context.PostAsync("We are currently processing your sandwich. We will message you the status.");
            };

            return new FormBuilder<SandwichOrder>()
                        .Message("Welcome to the sandwich order bot!")
                        .Field(nameof(Sandwich))
                        .Field(nameof(Length))
                        .Field(nameof(Bread))
                        .Field(nameof(Cheese))
                        .Field(nameof(Toppings),
                            validate: async (state, value) =>
                            {
                                var values = ((List<object>)value).OfType<ToppingOptions>();
                                var result = new ValidateResult { IsValid = true, Value = values };
                                if (values != null && values.Contains(ToppingOptions.Everything))
                                {
                                    result.Value = (from ToppingOptions topping in Enum.GetValues(typeof(ToppingOptions))
                                                    where topping != ToppingOptions.Everything && !values.Contains(topping)
                                                    select topping).ToList();
                                }
                                return result;
                            })
                        .Message("For sandwich toppings you have selected {Toppings}.")
                        .Field(nameof(SandwichOrder.Sauces))
                        .Field(new FieldReflector<SandwichOrder>(nameof(Specials))
                            .SetType(null)
                            .SetActive((state) => state.Length == LengthOptions.FootLong)
                            .SetDefine(async (state, field) =>
                            {
                                field
                                    .AddDescription("cookie", "Free cookie")
                                    .AddTerms("cookie", "cookie", "free cookie")
                                    .AddDescription("drink", "Free large drink")
                                    .AddTerms("drink", "drink", "free drink");
                                return true;
                            }))
                        .Confirm(async (state) =>
                        {
                            var cost = 0.0;
                            switch (state.Length)
                            {
                                case LengthOptions.SixInch: cost = 5.0; break;
                                case LengthOptions.FootLong: cost = 6.50; break;
                            }
                            return new PromptAttribute($"Total for your sandwich is {cost:C2} is that ok?");
                        })
                        .Field(nameof(SandwichOrder.DeliveryAddress),
                            validate: async (state, response) =>
                            {
                                var result = new ValidateResult { IsValid = true, Value = response };
                                var address = (response as string).Trim();
                                if (address.Length > 0 && (address[0] < '0' || address[0] > '9'))
                                {
                                    result.Feedback = "Address must start with a number.";
                                    result.IsValid = false;
                                }
                                return result;
                            })
                        .Field(nameof(SandwichOrder.DeliveryTime), "What time do you want your sandwich delivered? {||}")
                        .Confirm("Do you want to order your {Length} {Sandwich} on {Bread} {&Bread} with {[{Cheese} {Toppings} {Sauces}]} to be sent to {DeliveryAddress} {?at {DeliveryTime:t}}?")
                        .AddRemainingFields()
                        .Message("Thanks for ordering a sandwich!")
                        .OnCompletion(processOrder)
                        .Build();
        }
{% endhighlight %}

The above example defines the steps of the form and shows how to validate selections and create [dynamically defined fields](#dynamicallydefinedfields). The following outlines what the example does.

1. Shows the welcome message.
1. Fills in SandwichOrder.Sandwich
2. Fills in SandwichOrder.Length
3. Fills in SandwichOrder.Bread
4. Fills in SandwichOrder.Cheese
5. Fills in SandwichOrder.Toppings and transforms adds missing values if the user selected Everything
6. Shows a message confirming the selected toppings
7. Fills in SandwichOrder.Sauces
8. Dynamically defines the field for SandwichOrder.Specials
9. Dynamically defines the confirmation for the cost
10. Fills in SandwichOrder.DeliveryAddress and verifies the resulting string. If the address does not start with a number, the form returns a message.
11. Fills in SandwichOrder.DeliveryTime with a custom prompt
12. Confirms the order
13. Adds any remaining fields that were defined in the class but not explicitly referenced by Field. (If the example did not include AddRemainingField, the form would not include those fields not referenced by Field.)
14. Shows a final thank you message
15. Defines an OnCompletionAsync handler to process the order


### Adding business logic

If you have complex interdependencies between fields, or you need to add logic when setting or getting a value, you can include a validation function. The validation function lets you manipulate the state and return a **ValidateResult** object. The result can return:

- A feedback string describing why a value is not valid
- A transformed value
- A set of choices for clarifying a value

The following shows the validation function from the above example. If the Toppings field includes the Everything enumeration value, the function adds all of the missing toppings that were not selected. 

{% highlight csharp %}
                        .Field(nameof(Toppings),
                            validate: async (state, value) =>
                            {
                                var values = ((List<object>)value).OfType<ToppingOptions>();
                                var result = new ValidateResult { IsValid = true, Value = values };
                                if (values != null && values.Contains(ToppingOptions.Everything))
                                {
                                    result.Value = (from ToppingOptions topping in Enum.GetValues(typeof(ToppingOptions))
                                                    where topping != ToppingOptions.Everything && !values.Contains(topping)
                                                    select topping).ToList();
                                }
                                return result;
                            })
                        .Message("For sandwich toppings you have selected {Toppings}.")
{% endhighlight %}

<span style="color:red">Yes, this shows the validate function but it doesn't show the context for where you can include the function. Also, where is .Message displayed?</span>

<span style="color:red">What does state and value contain? Does value contain everything and jalapenos?</span>

To handle cases where the user wants everthing except for a few choices, add the following Terms attribute.

{% highlight csharp %}
    public enum ToppingOptions
    {
        // This starts at 1 because 0 is the "no value" value
        [Terms("except", "but", "not", "no", "all", "everything")]
        Everything = 1,
        . . .
    }
{% endhighlight %}

The following shows the interaction when the user requests everything except Jalapenos. 

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
<span style="color:red">What part takes care of "but jalapenos"?</span>


### Dynamically Defined Fields, Confirmations and Messages

With FormBuilder you can dynamically switch on and off parts of your form based on the state of your object, or dynamically define fields. To define a dynamic field, you can implement Advanced.IField yourself, but it is easier to use the Advanced.FieldReflector class. 

The sandwich example creates specials for free drinks and cookies if the user orders a foot-long sandwich. The specials are treated as dynamic fields because they depend on the user ordering a foot-long sandwich. The first step is to define the underlying field that will contain your dynamic value.

{% highlight csharp %}
        [Optional]
        [Template(TemplateUsage.NoPreference, "None")]
        public string Specials;
{% endhighlight %}

Because the field is optional, add the Optional attribute. Also change the NoPreference template to use None instead of No Preference.

The following shows how to add the dynamic part to FormBuilder. 

{% highlight csharp %}
                        .Field(new FieldReflector<SandwichOrder>(nameof(Specials))
                            .SetType(null)
                            .SetActive((state) => state.Length == LengthOptions.FootLong)
                            .SetDefine(async (state, field) =>
                            {
                                field
                                    .AddDescription("cookie", "Free cookie")
                                    .AddTerms("cookie", "cookie", "free cookie")
                                    .AddDescription("drink", "Free large drink")
                                    .AddTerms("drink", "drink", "free drink");
                                return true;
                            }))
{% endhighlight %}

- The Advanced.Field.SetType method sets the type of the field (null means enumeration). 
- The Advanced.Field.SetActive method enables the field only when the length is a foot long. 
- The Advanced.Field.SetDefine method provides an async delegate that defines the field. The delegate is passed the current state object and the Advanced.Field that is being dynamically defined. The delegate uses the fluent methods found on the field to dynamically define values. In this case, the values are strings and the AddDescription and AddTerms methods provide the descriptions and terms for the value.

You can also use FormBuilder to dynically define messages and confirmations. Messages and confirmations only run when the prior steps are inactive or completed. The following shows the confirmation that computes the cost of the sandwich. 

{% highlight csharp %}
                        .Confirm(async (state) =>
                        {
                            var cost = 0.0;
                            switch (state.Length)
                            {
                                case LengthOptions.SixInch: cost = 5.0; break;
                                case LengthOptions.FootLong: cost = 6.50; break;
                            }
                            return new PromptAttribute($"Total for your sandwich is {cost:C2} is that ok?");
                        })
{% endhighlight %}


It is possible to create a FormFlow experience without a C# class at all. The easiest way to do that is to derive a class from Advanced.Field and implement the Advanced.IFieldState methods to get and set values and unknown values.

<span style="color:red">I think if you're going to give the above option, you need to work through the usage.</span>
