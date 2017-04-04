// <buildLocalizedForm>
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
        // FormBuilder uses the thread culture to automatically switch framework strings and static strings.
        // Dynamically defined fields must do their own localization.
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
// </buildLocalizedForm>