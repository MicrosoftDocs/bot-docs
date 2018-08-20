---
title: Manage a complex conversation flow with dialogs | Microsoft Docs
description: Learn how to manage a complex conversation flow with dialogs in the Bot Builder SDK for Node.js.
keywords: complex conversation flow, repeat, loop, menu, dialogs, prompts, waterfalls, dialog set
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 7/27/2018
monikerRange: 'azure-bot-service-4.0'
---

# Manage complex conversation flows with dialogs

[!INCLUDE [pre-release-label](~/includes/pre-release-label.md)]

You can manage both simple and complex conversation flows using the dialogs library. In [simple conversation flows](bot-builder-dialog-manage-conversation-flow.md), the user starts from the first step of a *waterfall*, continues through to the last step, and the conversational exchange finishes. In this article we will use dialogs to manage more complex conversations with portions that can branch and loop. To do so, we'll use the dialog context's _replace_ method, as well as passing arguments between different parts of the dialog.

<!-- TODO: We need a dialogs conceptual topic to link to, so we can reference that here, in place of describing what they are and what their features are in a how-to topic. -->

To give you more control over the *dialog stack*, the **Dialogs** library provides a _replace_ method. This method allows you to pop the current dialog off the stack, push a new dialog onto the top of the stack, and start the new dialog. This feature allows you to provide a more complex conversation. These techniques can be used to create arbitrarily complex conversations. Should your conversation complexity increase to where dialogs become difficult to manage, it is possible to create your own flow of control logic to keep track of your user's conversation.

<!-- TODO: This is probably a good place to add a link to the modular/composite dialogs topic. -->

In this article we'll create the dialogs for a hotel bot that a guest could use to reserve a table or order a meal to be delivered to their room. The top-level dialog provides the guest with these two options. If they want to reserve a table the top-level dialog starts a table reservation dialog. If the guest wants to order dinner, the top-level dialog starts a dinner ordering dialog. The dinner ordering dialog first asks the guest to select food items off a menu and then asks for their room number. The selection of food items is also a dialog, so that we can allow the guest to select multiple items before processing their dinner order.

This diagram illustrates the dialogs we'll be creating in this article and their relationship to each other.

![Illustration of the dialogs used in this article](~/media/complex-conversation-flows.png)

## How to branch

The dialog context maintains a _dialog stack_ and for each dialog on the stack, tracks which step is next. Its _begin_ method pushes a dialog onto the top of the stack, and its _end_ method pops the top dialog off the stack.

A dialog can start a new dialog (aka: branching) within the same dialog set by calling the dialog context's _begin_ method and providing the ID of the new dialog. The original dialog is still on the stack, but calls to the dialog context's _continue_ method are only sent to the dialog that is on top of the stack, the _active dialog_. When a dialog is popped off the stack, the dialog context will resume with the next step of the waterfall on the stack where it left off.

Thus, you can create a branch within your conversation flow by including a step in one dialog that can conditionally choose a dialog to start out of a set of potential dialogs.

## How to loop

The dialog context's _replace_ method allows you to replace the dialog that is on top of the stack. The state of the old dialog is discarded and the new dialog starts from the beginning. You can use this method to create a loop by replacing a dialog with itself. However, to [persist state](bot-builder-howto-v4-state.md) you will need to pass information to the new instance of the dialog in the call to the _replace_ method. In the following example, you will see that the dinner order cart is stored on the dialog state and when the `orderPrompt` dialog is repeated, the current dialog state is passed in so that the new dialog's state can continue adding items to it. If you prefer to store these information in the other states, see [Persist user data](bot-builder-tutorial-persist-user-inputs.md).

## Create the dialogs for the hotel bot

In this section we'll create the dialogs to manage a conversation for the hotel bot we described.

### Install the dialogs library

# [C#](#tab/csharp)

We'll start from a basic EchoBot template. For instructions, see the [quickstart for .NET](~/dotnet/bot-builder-dotnet-quickstart.md).

To use dialogs, install the `Microsoft.Bot.Builder.Dialogs` NuGet package for your project or solution.
Then reference the dialogs library in using statements in your code files as necessary.

```csharp
using Microsoft.Bot.Builder.Dialogs;
```

# [JavaScript](#tab/javascript)

The `botbuilder-dialogs` library can be downloaded from NPM. To install the `botbuilder-dialogs` library, run the following NPM command:

```cmd
npm install --save botbuilder-dialogs
```

To use **dialogs** in your bot, include it in the bot code. For example, add this to your **app.js** file:

```javascript
const botbuilder_dialogs = require('botbuilder-dialogs');
```

---

### Create a dialog set

Create a _dialog set_ to which we'll add all of the dialogs for this example.

# [C#](#tab/csharp)

Create a **HotelDialog** class, and add the using statements we'll need.

```csharp
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Prompts.Choices;
using Microsoft.Bot.Schema;
using Microsoft.Recognizers.Text;
using System;
using System.Collections.Generic;
using System.Linq;
```

Derive the class from **DialogSet**, and define the IDs and keys we'll use to identify the dialogs, prompts, and state information for this dialog set.

```csharp
/// <summary>Contains the set of dialogs and prompts for the hotel bot.</summary>
public class HotelDialog : DialogSet
{
    /// <summary>The ID of the top-level dialog.</summary>
    public const string MainMenu = "mainMenu";

    /// <summary>Contains the IDs for the other dialogs in the set.</summary>
    private static class Dialogs
    {
        public const string OrderDinner = "orderDinner";
        public const string OrderPrompt = "orderPrompt";
        public const string ReserveTable = "reserveTable";
    }

    /// <summary>Contains the IDs for the prompts used by the dialogs.</summary>
    private static class Inputs
    {
        public const string Choice = "choicePrompt";
        public const string Number = "numberPrompt";
    }

    /// <summary>Contains the keys used to manage dialog state.</summary>
    private static class Outputs
    {
        public const string OrderCart = "orderCart";
        public const string OrderTotal = "orderTotal";
        public const string RoomNumber = "roomNumber";
    }
}
```

# [JavaScript](#tab/javascript)

To use **dialogs** in your bot, include it in the bot code.

Reference the library from your **app.js** file.

```javascript
const botbuilder_dialogs = require('botbuilder-dialogs');
```

And then create the dialog set.

```javascript
const dialogs = new botbuilder_dialogs.DialogSet();
```

---

### Add the prompts to the set

We'll use a **ChoicePrompt** to ask guests whether to order dinner or to reserve a table, and also which option off the dinner menu to select. And, we'll use a **NumberPrompt** to ask for the guest's room number when they do order dinner.

# [C#](#tab/csharp)

In the **HotelDialog** constructor, add the two prompts.

```csharp
// Add the prompts.
this.Add(Inputs.Choice, new ChoicePrompt(Culture.English));
this.Add(Inputs.Number, new NumberPrompt<int>(Culture.English));
```

# [JavaScript](#tab/javascript)

Add these two prompts to the dialog set.

```javascript
dialogs.add('choicePrompt', new botbuilder_dialogs.ChoicePrompt());
dialogs.add('numberPrompt', new botbuilder_dialogs.NumberPrompt());
```

---

### Define some of the supporting information

Since we'll need information about each option on the dinner menu, let's set that up now.

# [C#](#tab/csharp)

Create an inner static **Lists** class to contain this information. We'll also create inner **WelcomeChoice** and **MenuChoice** classes to contain information about each option.

While we're at it, we'll add information for the list of options in the top-level welcome dialog, and we'll also create supporting lists that we'll use later when prompting the guests for this information. It's a little added work up front, but it will make the dialog code simpler.

```csharp
/// <summary>Describes an option for the top-level dialog.</summary>
private class WelcomeChoice
{
    /// <summary>The text to show the guest for this option.</summary>
    public string Description { get; set; }

    /// <summary>The ID of the associated dialog for this option.</summary>
    public string DialogName { get; set; }
}

/// <summary>Describes an option for the food-selection dialog.</summary>
/// <remarks>We have two types of options. One represents meal items that the guest
/// can add to their order. The other represents a request to process or cancel the
/// order.</remarks>
private class MenuChoice
{
    /// <summary>The request text for cancelling the meal order.</summary>
    public const string Cancel = "Cancel order";

    /// <summary>The request text for processing the meal order.</summary>
    public const string Process = "Process order";

    /// <summary>The name of the meal item or the request.</summary>
    public string Name { get; set; }

    /// <summary>The price of the meal item; or NaN for a request.</summary>
    public double Price { get; set; }

    /// <summary>The text to show the guest for this option.</summary>
    public string Description => (double.IsNaN(Price)) ? Name : $"{Name} - ${Price:0.00}";
}

/// <summary>Contains the lists used to present options to the guest.</summary>
private static class Lists
{
    /// <summary>The options for the top-level dialog.</summary>
    public static List<WelcomeChoice> WelcomeOptions { get; } = new List<WelcomeChoice>
    {
        new WelcomeChoice { Description = "Order dinner", DialogName = Dialogs.OrderDinner },
        new WelcomeChoice { Description = "Reserve a table", DialogName = Dialogs.ReserveTable },
    };

    private static List<string> WelcomeList { get; } = WelcomeOptions.Select(x => x.Description).ToList();

    /// <summary>The choices to present in the choice prompt for the top-level dialog.</summary>
    public static List<Choice> WelcomeChoices { get; } = ChoiceFactory.ToChoices(WelcomeList);

    /// <summary>The reprompt action for the top-level dialog.</summary>
    public static Activity WelcomeReprompt
    {
        get
        {
            var reprompt = MessageFactory.SuggestedActions(WelcomeList, "Please choose an option");
            reprompt.AttachmentLayout = AttachmentLayoutTypes.List;
            return reprompt as Activity;
        }
    }

    /// <summary>The options for the food-selection dialog.</summary>
    public static List<MenuChoice> MenuOptions { get; } = new List<MenuChoice>
    {
        new MenuChoice { Name = "Potato Salad", Price = 5.99 },
        new MenuChoice { Name = "Tuna Sandwich", Price = 6.89 },
        new MenuChoice { Name = "Clam Chowder", Price = 4.50 },
        new MenuChoice { Name = MenuChoice.Process, Price = double.NaN },
        new MenuChoice { Name = MenuChoice.Cancel, Price = double.NaN },
    };

    private static List<string> MenuList { get; } = MenuOptions.Select(x => x.Description).ToList();

    /// <summary>The choices to present in the choice prompt for the food-selection dialog.</summary>
    public static List<Choice> MenuChoices { get; } = ChoiceFactory.ToChoices(MenuList);

    /// <summary>The reprompt action for the food-selection dialog.</summary>
    public static Activity MenuReprompt
    {
        get
        {
            var reprompt = MessageFactory.SuggestedActions(MenuList, "Please choose an option");
            reprompt.AttachmentLayout = AttachmentLayoutTypes.List;
            return reprompt as Activity;
        }
    }
}
```

# [JavaScript](#tab/javascript)

Create a **dinnerMenu** constant to contain this information.

```javascript
const dinnerMenu = {
    choices: ["Potato Salad - $5.99", "Tuna Sandwich - $6.89", "Clam Chowder - $4.50", 
        "Process order", "Cancel"],
    "Potato Salad - $5.99": {
        Description: "Potato Salad",
        Price: 5.99
    },
    "Tuna Sandwich - $6.89": {
        Description: "Tuna Sandwich",
        Price: 6.89
    },
    "Clam Chowder - $4.50": {
        Description: "Clam Chowder",
        Price: 4.50
    }
}
```

---

### Create the welcome dialog

This dialog uses a `ChoicePrompt` to display the menu and waits for the user to choose an option. When the user chooses either `Order dinner` or `Reserve a table`, it starts the dialog for the appropriate choice and when that dialog is done, instead of just ending the dialog in the last step and leaving the user wondering what's happening, this `mainMenu` dialog repeats itself by starting the `mainMenu` dialog again. With this simple structure, the bot will always show the menu and the user will always know what their choices are.

The **MainMenu** dialog contains these waterfall steps.

* In the first step of the waterfall, we initialize or clear the dialog state, greet the guest, and ask them to choose from the available options: `Order dinner` or `Reserve a table`.
* In the second step, we're retrieving their selection and then starting the child dialog that is associated with that selection. Once the child dialog ends, this dialog will resume with the following step.
* In the last step, we use the **DialogContext.Replace** method to replace this dialog with a new instance of itself, which effectively turns the welcome dialog into a looping menu.

# [C#](#tab/csharp)

```csharp
// Add the main welcome dialog.
this.Add(MainMenu, new WaterfallStep[]
{
    async (dc, args, next) =>
    {
        // Greet the guest and ask them to choose an option.
        await dc.Context.SendActivity("Welcome to Contoso Hotel and Resort.");
        await dc.Prompt(Inputs.Choice, "How may we serve you today?", new ChoicePromptOptions
        {
            Choices = Lists.WelcomeChoices,
            RetryPromptActivity = Lists.WelcomeReprompt,
        });
    },
    async (dc, args, next) =>
    {
        // Begin a child dialog associated with the chosen option.
        var choice = (FoundChoice)args["Value"];
        var dialogId = Lists.WelcomeOptions[choice.Index].DialogName;

        await dc.Begin(dialogId, dc.ActiveDialog.State);
    },
    async (dc, args, next) =>
    {
        // Start this dialog over again.
        await dc.Replace(MainMenu);
    },
});
```

# [JavaScript](#tab/javascript)

```javascript
// Display a menu and ask user to choose a menu item. Direct user to the item selected otherwise, show
// the menu again.
dialogs.add('mainMenu', [
    async function(dc){
        await dc.context.sendActivity("Welcome to Contoso Hotel and Resort.");
        await dc.prompt('choicePrompt', "How may we serve you today?", ['Order Dinner', 'Reserve a table']);
    },
    async function(dc, result){
        if(result.value.match(/order dinner/ig)){
            await dc.begin('orderDinner');
        }
        else if(result.value.match(/reserve a table/ig)){
            await dc.begin('reserveTable');
        }
    },
    async function(dc, result){
        // Start over
        await dc.endAll().begin('mainMenu');
    }
]);
```

---

### Create the order dinner dialog

In the order-dinner dialog, we'll welcome the guest to the "dinner order service" and immediately start the food-selection dialog, which we'll cover in the next section. Importantly, if the guest asks the service to process their order, the food-selection dialog returns the list of items on the order. To complete the whole process, this dialog then asks for a room number to which to deliver the food, and then sends a confirmation message before ending. If the guest cancels their order, this dialog doesn't ask for a room number and ends immediately.

# [C#](#tab/csharp)

To the **HotelDialog** class, add a data structure we can use to track the guest's dinner order.

```csharp
/// <summary>Contains the guest's dinner order.</summary>
private class OrderCart : List<MenuChoice> { }
```

In the **HotelDialog** constructor, add the order-dinner dialog.

* In the first step, we send a welcome message and start the food-selection dialog.
* In the second step, we check whether the food-selection dialog returned an order cart.
  * If so, prompt them for their room number and save the cart information.
  * If not, assume they cancelled their order, and call **DialogContext.End** to end this dialog.
* In the last step, record their room number and send them a confirmation message before ending. This is the step in which your bot would forward the order to a processing service.

```csharp
// Add the order-dinner dialog.
this.Add(Dialogs.OrderDinner, new WaterfallStep[]
{
    async (dc, args, next) =>
    {
        await dc.Context.SendActivity("Welcome to our Dinner order service.");

        // Start the food selection dialog.
        await dc.Begin(Dialogs.OrderPrompt);
    },
    async (dc, args, next) =>
    {
        if (args.TryGetValue(Outputs.OrderCart, out object arg) && arg is OrderCart cart)
        {
            // If there are items in the order, record the order and ask for a room number.
            dc.ActiveDialog.State[Outputs.OrderCart] = cart;
            await dc.Prompt(Inputs.Number, "What is your room number?", new PromptOptions
            {
                RetryPromptString = "Please enter your room number."
            });
        }
        else
        {
            // Otherwise, assume the order was cancelled by the guest and exit.
            await dc.End();
        }
    },
    async (dc, args, next) =>
    {
        // Get and save the guest's answer.
        var roomNumber = args["Text"] as string;
        dc.ActiveDialog.State[Outputs.RoomNumber] = roomNumber;

        // Process the dinner order using the collected order cart and room number.

        await dc.Context.SendActivity($"Thank you. Your order will be delivered to room {roomNumber} within 45 minutes.");
        await dc.End();
    },
});
```

# [JavaScript](#tab/javascript)

```javascript
// Order dinner:
// Help user order dinner from a menu
dialogs.add('orderDinner', [
    async function (dc){
        await dc.context.sendActivity("Welcome to our Dinner order service.");
        
        await dc.begin('orderPrompt', dc.activeDialog.state.orderCart); // Prompt for orders
    },
    async function (dc, result) {
        if(result == "Cancel"){
            await dc.end();
        }
        else { 
            await dc.prompt('numberPrompt', "What is your room number?");
        }
    },
    async function(dc, result){
        await dc.context.sendActivity(`Thank you. Your order will be delivered to room ${result} within 45 minutes.`);
        await dc.end();
    }
]);
```

---

### Create the order prompt dialog

In the food-selection dialog, we'll present the guest with a list of options that includes both the dinner items they can order and the two order processing requests. This dialog loops until the guest chooses to have the bot process or cancel their order.

* When the guest selects a dinner item, we add it to their _cart_.
* If the guest chooses to process their order, we first check whether the cart is empty.
  * If it's empty, we send an error message and continue looping.
  * Otherwise, we end this dialog, returning the cart information to the parent dialog.
* If the guest chooses to cancel their order, we end the dialog, returning no cart information.

# [C#](#tab/csharp)

In the **HotelDialog** constructor, add the food-selection dialog.

* In the first step, we initialize the dialog state. If the input arguments to the dialog contain cart information, we save that to our dialog state; otherwise, we create an empty cart and add that. We then prompt the guest for a choice from the dinner menu.
* In the next step, we look at the option the guest picked:
  * If it is a request to process the order, check whether the cart contains any items.
    * If so, end the dialog, returning the cart contents.
    * Otherwise, send an error message to the guest and start over from the beginning of the dialog.
  * If it is a request to cancel the order, end the dialog, returning an empty dictionary.
  * If it is a dinner item, add it to the cart, send a status message, and start the dialog over, passing in the current order state.

```csharp
// Add the food-selection dialog.
this.Add(Dialogs.OrderPrompt, new WaterfallStep[]
    {
        async (dc, args, next) =>
        {
            if (args is null || !args.ContainsKey(Outputs.OrderCart))
            {
                // First time through, initialize the order state.
                dc.ActiveDialog.State[Outputs.OrderCart] = new OrderCart();
                dc.ActiveDialog.State[Outputs.OrderTotal] = 0.0;
            }
            else
            {
                // Otherwise, set the order state to that of the arguments.
                dc.ActiveDialog.State = new Dictionary<string, object>(args);
            }

            await dc.Prompt(Inputs.Choice, "What would you like?", new ChoicePromptOptions
            {
                Choices = Lists.MenuChoices,
                RetryPromptActivity = Lists.MenuReprompt,
            });
        },
        async (dc, args, next) =>
        {
            // Get the guest's choice.
            var choice = (FoundChoice)args["Value"];
            var option = Lists.MenuOptions[choice.Index];

            // Get the current order from dialog state.
            var cart = (OrderCart)dc.ActiveDialog.State[Outputs.OrderCart];

            if (option.Name is MenuChoice.Process)
            {
                if (cart.Count > 0)
                {
                    // If there are any items in the order, then exit this dialog,
                    // and return the list of selected food items.
                    await dc.End(new Dictionary<string, object>
                    {
                        [Outputs.OrderCart] = cart
                    });
                }
                else
                {
                    // Otherwise, send an error message and restart from
                    // the beginning of this dialog.
                    await dc.Context.SendActivity(
                        "Your cart is empty. Please add at least one item to the cart.");
                    await dc.Replace(Dialogs.OrderPrompt);
                }
            }
            else if (option.Name is MenuChoice.Cancel)
            {
                await dc.Context.SendActivity("Your order has been cancelled.");

                // Exit this dialog, returning an empty property bag.
                dc.ActiveDialog.State.Clear();
                await dc.End(new Dictionary<string, object>());
            }
            else
            {
                // Add the selected food item to the order and update the order total.
                cart.Add(option);
                var total = (double)dc.ActiveDialog.State[Outputs.OrderTotal] + option.Price;
                dc.ActiveDialog.State[Outputs.OrderTotal] = total;

                await dc.Context.SendActivity($"Added {option.Name} (${option.Price:0.00}) to your order." +
                    Environment.NewLine + Environment.NewLine +
                    $"Your current total is ${total:0.00}.");

                // Present the order options again, passing in the current order state.
                await dc.Replace(Dialogs.OrderPrompt, dc.ActiveDialog.State);
            }
        },
    });
```

# [JavaScript](#tab/javascript)

```javascript
// Helper dialog to repeatedly prompt user for orders
dialogs.add('orderPrompt', [
    async function(dc, orderCart){
        // Define a new cart of one does not exists
        if(!orderCart){
            // Initialize a new cart
            // convoState = conversationState.get(dc.context);
            dc.activeDialog.state.orderCart = {
                orders: [],
                total: 0
            };
        }
        else {
            dc.activeDialog.state.orderCart = orderCart;
        }
        await dc.prompt('choicePrompt', "What would you like?", dinnerMenu.choices);
    },
    async function(dc, choice){
        // Get state object
        // convoState = conversationState.get(dc.context);

        if(choice.value.match(/process order/ig)){
            if(dc.activeDialog.state.orderCart.orders.length > 0) {
                // Process the order
                // ...
                dc.activeDialog.state.orderCart = undefined; // Reset cart
                await dc.context.sendActivity("Processing your order.");
                await dc.end();
            }
            else {
                await dc.context.sendActivity("Your cart was empty. Please add at least one item to the cart.");
                // Ask again
                await dc.replace('orderPrompt');
            }
        }
        else if(choice.value.match(/cancel/ig)){
            //dc.activeDialog.state.orderCart = undefined; // Reset cart
            await dc.context.sendActivity("Your order has been canceled.");
            await dc.end(choice.value);
        }
        else {
            var item = dinnerMenu[choice.value];

            // Only proceed if user chooses an item from the menu
            if(!item){
                await dc.context.sendActivity("Sorry, that is not a valid item. Please pick one from the menu.");
                
                // Ask again
                await dc.replace('orderPrompt');
            }
            else {
                // Add the item to cart
                dc.activeDialog.state.orderCart.orders.push(item);
                dc.activeDialog.state.orderCart.total += item.Price;

                await dc.context.sendActivity(`Added to cart: ${choice.value}. <br/>Current total: $${dc.activeDialog.state.orderCart.total}`);

                // Ask again
                await dc.replace('orderPrompt', dc.activeDialog.state.orderCart);
            }
        }
    }
]);
```

The sample code above shows that the main `orderDinner` dialog uses a helper dialog named `orderPrompt` to handle user choices. The `orderPrompt` dialog displays the menu of food items, asks the user to choose an item, add the item to cart and prompts again in a loop. This allows the user to add multiple items to their order. The dialog loops until the user chooses `Process order` or `Cancel`. At which point, execution is handed back to the parent dialog (the `orderDinner` dialog). The `orderDinner` dialog does some last minute house keeping if the user wants to process the order; otherwise, it ends and returns execution back to its parent dialog (e.g.: `mainMenu`). The `mainMenu` dialog in turn continues executing the last step which is to simply redisplay the main menu choices.

---
### Create the reserve table dialog

<!-- TODO: Update the "Manage simple conversation flows" topic to use a reserveTable dialog, and then reference that from here. -->

To keep this example shorter, we show only a minimal implementation of the `reserveTable` dialog here.

# [C#](#tab/csharp)

```csharp
// Add the table-reservation dialog.
this.Add(Dialogs.ReserveTable, new WaterfallStep[]
    {
        // Replace this waterfall with your reservation steps.
        async (dc, args, next) =>
        {
            await dc.Context.SendActivity("Your table has been reserved.");
            await dc.End();
        }
    });
```

# [JavaScript](#tab/javascript)

```javascript
// Reserve a table:
// Help the user to reserve a table

dialogs.add('reserveTable', [
    // Replace this waterfall with your reservation steps.
    async function(dc, args, next){
        await dc.context.sendActivity("Your table has been reserved");
        await dc.end();
    }
]);
```

---

## Next steps

You can enhance this bot by offering other options like "more info" or "help" to the menu choices. For more information on implementing these types of interruptions, see [Handle user interruptions](bot-builder-howto-handle-user-interrupt.md).

Now that you have learned how to use dialogs, prompts, and waterfalls to manage complex conversation flows, let's take a look at how we can break our dialogs (such as the `orderDinner` and `reserveTable` dialogs) into separate objects, instead of lumping them all together in one large dialog set.

> [!div class="nextstepaction"]
> [Create modular bot logic with Composite Control](bot-builder-compositcontrol.md)
