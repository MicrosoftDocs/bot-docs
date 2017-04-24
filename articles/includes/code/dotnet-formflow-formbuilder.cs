// <fieldDefinition>
[Optional]
[Template(TemplateUsage.NoPreference, "None")]
public string Specials;
// </fieldDefinition>


// <defineValue>
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
// </defineValue>


// <defineConfirmation>
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
// </defineConfirmation>


// <formBuilderForm>
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
// </formBuilderForm>
