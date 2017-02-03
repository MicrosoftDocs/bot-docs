---
layout: page
title: Localizing Form Content
permalink: /en-us/csharp/builder/formflow-localization/
weight: 2490
parent1: Building your Bot Using Bot Builder for .NET
parent2: Chat Bots
---

<span style="color:red">This needs to be under FormFlow</span>


The form uses the current thread's CurrentUICulture and CurrentCulture to determine the language. By default, the culture comes from the Language field of the current message, but you may change it. Depending on your bot, a localized string may come from the following sources:

- The built-in localization for PromptDialog and FormFlow
- A resource file that's generated from the static strings in your form
- A resource file that you create with strings for dynamically computed fields, messages or confirmations

<span style="color:red">What languages do PromptDialog and formflow support?</span>

The static strings in a form include strings that the form generates from the information in your C# class and from the strings you supply as prompts, templates, messages or confirmations. It does not include strings generated from built-in templates since those are already localized. Since many strings are automatically generated, it is not easy to use normal C# resource strings directly. For this reason, FormFlow provides the code to easily extract all of the static strings in a form by either:

<span style="color:red">If the bot supports multiple languages, do they include a Prompt attribute for each locale?</span>

1. Calling IFormBuilder.SaveResource on your form to save the strings to a .resx file
2. Using the **rview** tool that's included in the BotBuilder NuGet package to generate a resource file from your .dll or .exe file. To generate the .resx file, pass the assembly that contains your static form building method and the path to that method. The following example shows how to generate the Microsoft.Bot.Sample.AnnotatedSandwichBot.SandwichOrder.resx resource file. 

```
rview -g Microsoft.Bot.Sample.AnnotatedSandwichBot.dll Microsoft.Bot.Sample.AnnotatedSandwichBot.SandwichOrder.BuildForm
```

The following shows a few lines from the generated .resx file.

{% highlight xml %}
  <data name="Specials_description;VALUE" xml:space="preserve">
    <value>Specials</value>
  </data>
  <data name="DeliveryAddress_description;VALUE" xml:space="preserve">
    <value>Delivery Address</value>
  </data>
  <data name="DeliveryTime_description;VALUE" xml:space="preserve">
    <value>Delivery Time</value>
  </data>
  <data name="PhoneNumber_description;VALUE" xml:space="preserve">
    <value>Phone Number</value>
  </data>
  <data name="Rating_description;VALUE" xml:space="preserve">
    <value>your experience today</value>
  </data>
  <data name="message0;LIST" xml:space="preserve">
    <value>Welcome to the sandwich order bot!</value>
  </data>
  <data name="Sandwich_terms;LIST" xml:space="preserve">
    <value>sandwichs?</value>
  </data>
{% endhighlight %}

Add the resource file to your project and then follow these steps to set the neutral language:

1. Right-click on your project and select **Application**
2. Click **Assembly Information**
3. Select the language you developed your bot in from the Neutral Language drop down list.

When your form is created, the IFormBuilder.Build method will automatically look for resources that contain your form type name and use them to localize all of the static strings in your form.

<span style="color:red">Does it pick the locale based on the locale sent in the user's message?</span>

Note that dynamic fields defined using Advanced.Field.SetDefine (see [Using Dynamic Fields](/en-us/csharp/builder/formflow-formbuilder/#dynamicfields) cannot be localized because the field's string is dynamically constructed at the time the form is populated. To localize these fields, use normal C# localization where a C# class for string resources is automatically generated for you.

<span style="color:red">Is there a topic that we can link to that describes "normal C# localization"?</span>


After you add the resource files to your project, you need to localize them. The easiest way to do this to install and use the [Multilingual App Toolkit (MAT)](https://developer.microsoft.com/en-us/windows/develop/multilingual-app-toolkit). To enable MAT for your project:

1. Select your project in the Visual Studio Solution Explorer
2. Click **Tools**, **Multilingual App Toolkit**, and **Enable** to enable your project
3. Right-click the project and select **Multilingual App Toolkit**, **Add Translations** to select the translations. This will create industry standard XLF files which you can then automatically or manually translate.

The following shows what the Annotated Sandwich Bot form builder would look like when you put this all together.


{% highlight csharp %}
        public static IForm<SandwichOrder> BuildLocalizedForm()
        {
            var culture = Thread.CurrentThread.CurrentUICulture;
            IForm<SandwichOrder> form;
            if (!_forms.TryGetValue(culture, out form))
            {
                OnCompletionAsyncDelegate<SandwichOrder> processOrder = async (context, state) =>
                                {
                                    await context.PostAsync(DynamicSandwich.Processing);
                                };
                // Form builder uses the thread culture to automatically switch framework strings
                // and also your static strings as well.  Dynamically defined fields must do their own localization.
                var builder = new FormBuilder<SandwichOrder>()
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
                                        .AddDescription("cookie", DynamicSandwich.FreeCookie)
                                        .AddTerms("cookie", Language.GenerateTerms(DynamicSandwich.FreeCookie, 2))
                                        .AddDescription("drink", DynamicSandwich.FreeDrink)
                                        .AddTerms("drink", Language.GenerateTerms(DynamicSandwich.FreeDrink, 2));
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
                                return new PromptAttribute(string.Format(DynamicSandwich.Cost, cost) + "{||}");
                            })
                        .Field(nameof(SandwichOrder.DeliveryAddress),
                            validate: async (state, response) =>
                            {
                                var result = new ValidateResult { IsValid = true, Value = response };
                                var address = (response as string).Trim();
                                if (address.Length > 0 && address[0] < '0' || address[0] > '9')
                                {
                                    result.Feedback = DynamicSandwich.BadAddress;
                                    result.IsValid = false;
                                }
                                return result;
                            })
                        .Field(nameof(SandwichOrder.DeliveryTime), "What time do you want your sandwich delivered? {||}")
                        .Confirm("Do you want to order your {Length} {Sandwich} on {Bread} {&Bread} with {[{Cheese} {Toppings} {Sauces}]} to be sent to {DeliveryAddress} {?at {DeliveryTime:t}}?")
                        .AddRemainingFields()
                        .Message("Thanks for ordering a sandwich!")
                        .OnCompletion(processOrder);
                builder.Configuration.DefaultPrompt.ChoiceStyle = ChoiceStyleOptions.Auto;
                form = builder.Build();
                _forms[culture] = form;
            }
            return form;
        }
    };
{% endhighlight %}


<span style="color:red">I might have missed this but which fields uses localized strings? I know the dynamic ones don't but what about .confirm and .message?</span>

The following shows what the form looks like when the CurrentUICulture is French.

```
Bienvenue sur le bot d'ordre "sandwich" !
Quel genre de "sandwich" vous souhaitez sur votre "sandwich"?
 1. BLT
 2. Jambon Forêt Noire
 3. Poulet Buffalo
 4. Faire fondre le poulet et Bacon Ranch
 5. Combo de coupe à froid
 6. Boulette de viande Marinara
 7. Poulet rôti au four
 8. Rôti de boeuf
 9. Rotisserie poulet
 10. Italienne piquante
 11. Bifteck et fromage
 12. Oignon doux Teriyaki
 13. Thon
 14. Poitrine de dinde
 15. Veggie
> 2

Quel genre de longueur vous souhaitez sur votre "sandwich"?
 1. Six pouces
 2. Pied Long
> ?
* Vous renseignez le champ longueur.Réponses possibles:
* Vous pouvez saisir un numéro 1-2 ou des mots de la description. (Six pouces, ou Pied Long)
* Retourner à la question précédente.
* Assistance: Montrez les réponses possibles.
* Abandonner: Abandonner sans finir
* Recommencer remplir le formulaire. (Vos réponses précédentes sont enregistrées.)
* Statut: Montrer le progrès en remplissant le formulaire jusqu'à présent.
* Vous pouvez passer à un autre champ en entrant son nom. ("Sandwich", Longueur, Pain, Fromage, Nappages, Sauces, Adresse de remise, Délai de livraison, ou votre expérience aujourd'hui).
Quel genre de longueur vous souhaitez sur votre "sandwich"?
 1. Six pouces
 2. Pied Long
> 1

Quel genre de pain vous souhaitez sur votre "sandwich"?
 1. Neuf grains de blé
 2. Neuf grains miel avoine
 3. Italien
 4. Fromage et herbes italiennes
 5. Pain plat
> neuf
Par pain "neuf" vouliez-vous dire (1. Neuf grains miel avoine, ou 2. Neuf grains de blé)
```

<span style="color:red">So is it okay that the example switches the numbers in the bread confirmation?</span>
