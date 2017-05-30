// <defineForm>
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;

// The SandwichOrder class represents the form that you want to complete 
// using information that is collected from the user. 
// It must be serializable so the bot can be stateless. 
// The order of fields defines the default sequence in which the user is asked questions.

// The enumerations define the valid options for each field in SandwichOrder, and the order
// of the values represents the sequence in which they are presented to the user in a conversation.

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
// </defineForm>




// <connectToFramework>
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
                break;
        }
    }
    ...
}
// </connectToFramework>




// <handleExceptionOrQuit>
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
                    reply = $"You quit on {e.Last} -- maybe you can finish next time!";
                }
                else
                {
                    reply = "Sorry, I've had a short circuit. Please try again.";
                }
                await context.PostAsync(reply);
            }
        });
}
// </handleExceptionOrQuit>

