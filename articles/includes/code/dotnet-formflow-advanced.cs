// <validationFunction>
public static IForm<SandwichOrder> BuildForm()
{
    ...
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
        ...
        .Build();
}
// </validationFunction>


// <toppingsTerms>
public enum ToppingOptions
{
    // This starts at 1 because 0 is the "no value" value
    [Terms("except", "but", "not", "no", "all", "everything")]
    Everything = 1,
    ...
}
// </toppingsTerms>


// <promptAttribute>
[Prompt("What kind of {&} would you like? {||}")]
public SandwichOptions? Sandwich;
// </promptAttribute>


// <promptChoice>
[Prompt("What kind of {&} would you like? {||}", ChoiceFormat="{1}")]
public SandwichOptions? Sandwich;
// </promptChoice>


// <templateAttribute>
[Template(TemplateUsage.EnumSelectOne, "What kind of {&} would you like on your sandwich? {||}", ChoiceStyle = ChoiceStyleOptions.PerLine)]
public class SandwichOrder
// </templateAttribute>


// <templateMessages>
[Template(TemplateUsage.NotUnderstood, "I do not understand \"{0}\".", "Try again, I don't get \"{0}\".")]
[Template(TemplateUsage.EnumSelectOne, "What kind of {&} would you like on your sandwich? {||}")]
public class SandwichOrder
// </templateMessages>


// <optionalAttribute>
[Optional]
public CheeseOptions? Cheese;
// </optionalAttribute>


// <termsAttribute>
[Terms(@"rotis\w* style chicken", MaxPhrase = 3)]
RotisserieStyleChicken, SpicyItalian, SteakAndCheese, SweetOnionTeriyaki, Tuna,...
// </termsAttribute>


// <numericAttribute>
[Numeric(1, 5)]
public double? Rating;
// </numericAttribute>


// <patternAttribute>
[Pattern(@"(<Undefined control sequence>\d)?\s*\d{3}(-|\s*)\d{4}")]
public string PhoneNumber;
// </patternAttribute>
