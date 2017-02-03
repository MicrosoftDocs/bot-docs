---
layout: page
title: Using FormFlow to Manage the Conversation
permalink: /en-us/csharp/builder/formflow/
weight: 2400
parent1: Building your Bot Using Bot Builder for .NET
parent2: Chat Bots
---


[Dialogs](/en-us/csharp/builder/dialogs/) are very powerful and flexible, but handling a guided conversation such as ordering a sandwich can require a lot of effort. At each point in the dialog, there are many possibilities of what happens next. For example, you may need to clarify an ambiguity, provide help, go back, or show progress. In order to simplify building guided conversations, the framework provides a powerful dialog building block known as FormFlow. 

FormFlow sacrifices some of the flexibility that dialogs provide, but in a way that requires much less effort. Even better, you can combine FormFlow generated dialogs and other kinds of dialogs like a [LuisDialog](/en-us/csharp/builder/dialogs-luis/) to get the best of both worlds. A FormFlow dialog guides the user through filling in the form while providing help and guidance along the way.


### Forms and Fields

A form is a C# class that contains one or more public properties. The properties represent the information that you want to collect from the user. The property must be one of the following types:

* Integral (sbyte, byte, short, ushort, int, uint, long, ulong)
* Floating point (float, double)
* String
* DateTime
* Enumeration
* List of enumerations

<span style="color:red">List of enum?</span>

Any of the data types may also be nullable (for example, int?), which you use to model that the field does not have a value. If the field is an enumeration, and is not nullable, then the 0 value in the enumeration is considered to be null and you should start your enumeration values at 1. FormFlow ignores all other property types and methods.

For complex objects, you need to create a form for the top-level C# class and another form for the complex object. You can use the dialogs system to compose the forms together. It is also possible to define a form directly by implementing **Advanced.IField** or using **Advanced.Field** and populating the dictionaries within it. 

### Simple sandwich bot

The following shows a simple sandwich order form. The SandwichOrder class defines the form, and the enumerations define the options for building a sandwich. The class also includes the static BuildForm method that uses **FormBuilder** to build your form. There are lots of things you can do with the form builder, which we'll cover later, but this example simply defines a welcome message.

To use FormFlow, you must first import the Microsoft.Bot.Builder.FormFlow namespace.

{% highlight csharp %}

using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
#pragma warning disable 649

// The SandwichOrder class is the simple form you want to fill out.  It must be 
// serializable so the bot can be stateless. The order of fields defines the default
// order in which the user is asked questions.

// The enumerations show the legal options of each field in SandwichOrder, and the order
// of the values is the order that they're presented to the user in a conversation.

namespace Microsoft.Bot.Sample.SimpleSandwichBot
{
    public enum SandwichOptions
    {
        BLT, BlackForestHam, BuffaloChicken, ChickenAndBaconRanchMelt, ColdCutCombo, MeatballMarinara,
        OvenRoastedChicken, RoastBeef, RotisserieStyleChicken, SpicyItalian, SteakAndCheese, SweetOnionTeriyaki, Tuna,
        TurkeyBreast, Veggie
    };
    public enum LengthOptions { SixInch, FootLong };
    public enum BreadOptions { NineGrainWheat, NineGrainHoneyOat, Italian, ItalianHerbsAndCheese, Flatbread };
    public enum CheeseOptions { American, MontereyCheddar, Pepperjack };
    public enum ToppingOptions
    {
        Avocado, BananaPeppers, Cucumbers, GreenBellPeppers, Jalapenos,
        Lettuce, Olives, Pickles, RedOnion, Spinach, Tomatoes
    };
    public enum SauceOptions
    {
        ChipotleSouthwest, HoneyMustard, LightMayonnaise, RegularMayonnaise,
        Mustard, Oil, Pepper, Ranch, SweetOnion, Vinegar
    };

    [Serializable]
    public class SandwichOrder
    {
        public SandwichOptions? Sandwich;
        public LengthOptions? Length;
        public BreadOptions? Bread;
        public CheeseOptions? Cheese;
        public List<ToppingOptions> Toppings;
        public List<SauceOptions> Sauce;

        public static IForm<SandwichOrder> BuildForm()
        {
            return new FormBuilder<SandwichOrder>()
                    .Message("Welcome to the simple sandwich order bot!")
                    .Build();
        }
    };
}

{% endhighlight %}

The following examples shows how to connect the form to the framework. The **Conversation.SendAsync** method calls the static **MakeRootDialog** method which calls the **FormDialog.FromForm** method to create the SandwichOrder form. The combination of your C# class and connecting it to the framework is enough to automatically create a conversation.


{% highlight csharp %}

        internal static IDialog<SandwichOrder> MakeRootDialog()
        {
            return Chain.From(() => FormDialog.FromForm(SandwichOrder.BuildForm));
        }

        [ResponseType(typeof(void))]
        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            if (activity != null)
            {
                switch (activity.GetActivityType())
                {
                    case ActivityTypes.Message:
                        await Conversation.SendAsync(activity, MakeRootDialog);
                        break;

                    case ActivityTypes.ConversationUpdate:
                    case ActivityTypes.ContactRelationUpdate:
                    case ActivityTypes.Typing:
                    case ActivityTypes.DeleteUserData:
                    default:
                        Trace.TraceError($"Unknown activity type ignored: {activity.GetActivityType()}");

                    . . .
{% endhighlight %}

The following shows an example interaction that demonstrates some of the features offered by FormFlow. A '>' symbol shows where the user enters their response. 

```
Please select a sandwich
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

This form populates the SandwichOrder.Sandwich property. The form automatically generates the prompt, "Please select a sandwich". The word "sandwich" in the prompt comes from the Sandwhich property's name. The SandwichOptions enumeration provides the choices that make up the list. Each enumeration value was broken into words based on changes in case and underscores.

Users can ask for help on any form to get guidance with filling out the form. To get help on a form, the user enters "help".

```
> help
* You are filling in the sandwich field. Possible responses:
* You can enter a number 1-15 or words from the descriptions. (BLT, Black Forest Ham, Buffalo Chicken, Chicken And Bacon Ranch Melt, Cold Cut Combo, Meatball Marinara, Oven Roasted Chicken, Roast Beef, Rotisserie Style Chicken, Spicy Italian, Steak And Cheese, Sweet Onion Teriyaki, Tuna, Turkey Breast, and Veggie)
* Back: Go back to the previous question.
* Help: Show the kinds of responses you can enter.
* Quit: Quit the form without completing it.
* Reset: Start over filling in the form. (With defaults from your previous entries.)
* Status: Show your progress in filling in the form so far.
* You can switch to another field by entering its name. (Sandwich, Length, Bread, Cheese, Toppings, and Sauce).
```

As described in help, you can respond to this prompt by responding with the number of the choice you want, or you can also use the words that are found in the choice's description. There are also a number of commands that let you back up a step, get help, quit, start over or get the progress so far. 

Let's enter "2" to select "Black Forest Ham". 

```
Please select a sandwich
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
> 2
Please select a length(1. Six Inch, 2. Foot Long)
> 
```

<span style="color:red">Do we really need to show how form input/commands work? Isn't there a FormFlow (whatever this is built on) that we can link to? Or just list the commands in a table.</span>

The next prompt gets the value for the SandwichOrder.Length property. If you wanted to back up to check on your change, you could enter 'back' at the prompt.

```
> back
Please select a sandwich(current choice: Black Forest Ham)
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
```

The prompt shows the user's current selection, "Black Forest Ham". The user can change the selection or enter 'c' to keep the current choice. 

```
Please select a sandwich (current choice: Black Forest Ham)
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
> c
Please select a length(1. Six Inch, 2. Foot Long)
> 1
```

As an alternative to entering the number associated with the choice, the user can enter the text itself. If the text is ambiguous, the form automatically asks for clarification.

```
Please select a bread
 1. Nine Grain Wheat
 2. Nine Grain Honey Oat
 3. Italian
 4. Italian Herbs And Cheese
 5. Flatbread
> nine grain
By "nine grain" bread did you mean(1. Nine Grain Honey Oat, 2. Nine Grain Wheat)
> 1
```

The following example shows how the form responds to input that is not understood. 

```
Please select a cheese (1. American, 2. Monterey Cheddar, 3. Pepperjack)
> amercan
"amercan" is not a cheese option.
> american smoked
For cheese I understood American. "smoked" is not an option.
```

The following shows the experience when providing values for properties that allow multiple choices. 

```
Please select one or more toppings
 1. Banana Peppers
 2. Cucumbers
 3. Green Bell Peppers
 4. Jalapenos
 5. Lettuce
 6. Olives
 7. Pickles
 8. Red Onion
 9. Spinach
 10. Tomatoes
> peppers, lettuce and tomato
By "peppers" toppings did you mean(1. Green Bell Peppers, 2. Banana Peppers)
> 1
```

To get the current state of the order, enter "status". It shows the values that were already specified and those that are remaining.

```
Please select one or more sauce
 1. Honey Mustard
 2. Light Mayonnaise
 3. Regular Mayonnaise
 4. Mustard
 5. Oil
 6. Pepper
 7. Ranch
 8. Sweet Onion
 9. Vinegar
> status
* Sandwich: Black Forest Ham
* Length: Six Inch
* Bread: Nine Grain Honey Oat
* Cheese: American
* Toppings: Lettuce, Tomatoes, and Green Bell Peppers
* Sauce: Unspecified  
```

When the user completes the form, they're asked to confirm the order.

```
Please select one or more sauce
 1. Honey Mustard
 2. Light Mayonnaise
 3. Regular Mayonnaise
 4. Mustard
 5. Oil
 6. Pepper
 7. Ranch
 8. Sweet Onion
 9. Vinegar
> 1
Is this your selection?
* Sandwich: Black Forest Ham
* Length: Six Inch
* Bread: Nine Grain Honey Oat
* Cheese: American
* Toppings: Lettuce, Tomatoes, and Green Bell Peppers
* Sauce: Honey Mustard
>
```

If the user enters "no", they're given the option to change any part of the form. This example changes the sandwich's length, and then enters "y" to complete the order, which returns the completed form to the calling dialog.

```
Is this your selection?
* Sandwich: Black Forest Ham
* Length: Six Inch
* Bread: Nine Grain Honey Oat
* Cheese: American
* Toppings: Lettuce, Tomatoes, and Green Bell Peppers
* Sauce: Honey Mustard
> no
What do you want to change?
 1. Sandwich(Black Forest Ham)
 2. Length(Six Inch)
 3. Bread(Nine Grain Honey Oat)
 4. Cheese(American)
 5. Toppings(Lettuce, Tomatoes, and Green Bell Peppers)
 6. Sauce(Honey Mustard)
> 2
Please select a length(current choice: Six Inch) (1. Six Inch, 2. Foot Long)
> 2
Is this your selection?
* Sandwich: Black Forest Ham
* Length: Foot Long
* Bread: Nine Grain Honey Oat
* Cheese: American
* Toppings: Lettuce, Tomatoes, and Green Bell Peppers
* Sauce: Honey Mustard

> y
```

From the above interactions, you can see that the form:

* Automatically generates and manages the conversation
* Provides clear guidance and help  
* Understands both numbers and textual entries  
* Gives feedback on what is understood and what is not  
* Asks clarifying questions when needed  
* Allows navigating between the steps  

All of this is pretty amazing for not having to do any of the work! However, not every interaction is as good as you might want it to be. That is why there are easy ways to provide:

* Messages during the process of filling in a form  
* Custom prompts per field  
* Templates to use when automatically generating prompts or help  
* Terms to match on  
* Options for showing choices and numbers  
* Fields that are optional  
* Conditional fields  
* Value validation  
* and much more...

<span style="color:red">What's the difference between bullets 5, 6, and 7?</span>




For information about how to improve the sandwich bot by adding attributes, business logic, and the FormBuilder, see [FormFlow Advanced Features](/en-us/csharp/builder/formflow-advanced/).



### Handling Quit and Exceptions

When the user enters 'quit' in the form, or an exception occurs, it may be useful to know:

- Which step the 'quit' or exception happened on
- The state of the form
- What steps were successfully completed. 

The form passes back all of this information through the **FormCanceledException<T>** class. The following example shows how to catch the exception and send a message after:

- Successfully processing the order
- When the user quits
- When there is an exception 

{% highlight csharp %}
        internal static IDialog<SandwichOrder> MakeRootDialog()
        {
            return Chain.From(() => FormDialog.FromForm(SandwichOrder.BuildLocalizedForm))
                .Do(async (context, order) =>
                {
                    try
                    {
                        var completed = await order;
                        // Actually process the sandwich order...
                        await context.PostAsync("Processed your order!");
                    }
                    catch (FormCanceledException<SandwichOrder> e)
                    {
                        string reply;
                        if (e.InnerException == null)
                        {
                            reply = $"You quit on {e.Last}--maybe you can finish next time!";
                        }
                        else
                        {
                            reply = "Sorry, I've had a short circuit.  Please try again.";
                        }
                        await context.PostAsync(reply);
                    }
                });
        }
{% endhighlight %}


