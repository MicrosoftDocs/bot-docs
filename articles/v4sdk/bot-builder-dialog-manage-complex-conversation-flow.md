---
title: Create advance conversation flow using branches and loops | Microsoft Docs
description: Learn how to manage a complex conversation flow with dialogs in the Bot Builder SDK for Node.js.
keywords: complex conversation flow, repeat, loop, menu, dialogs, prompts, waterfalls, dialog set
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 11/03/2018
monikerRange: 'azure-bot-service-4.0'
---

# Create advance conversation flow using branches and loops

[!INCLUDE [pre-release-label](~/includes/pre-release-label.md)]

In the last article, we demonstrated using the dialogs library to manage simple conversations. In [sequential conversation flows](bot-builder-dialog-manage-conversation-flow.md), the user starts from the first step of a *waterfall*, continues through to the last step, and the conversational exchange finishes. In this article we will use dialogs to manage more complex conversations with portions that can branch and loop. To do so, we'll use various methods defined on the dialog context and waterfall step context, and we'll pass arguments between different parts of the dialog.

See [Dialogs library](bot-builder-concept-dialog.md) for more background information about dialogs.

To give you more control over the *dialog stack*, the **Dialogs** library provides a _replace dialog_ method. This method allows you swap the currently active dialog for another one while maintaining the state and flow of the conversation. The _begin dialog_ and _replace dialog_ methods allow you to branch and loop as necessary to create more complex interactions. Should your conversation complexity increase to where your waterfall dialogs become difficult to manage, investigate [dialog reuse](bot-builder-compositcontrol.md) or build a custom dialog management class based on the base `Dialog` class.

In this article we'll create sample dialogs for a hotel concierge bot that a guest could use to access common services: reserving a table at the hotel restaurant, and ordering a meal from room service.  Each one of these features, along with a menu connecting them together, will be created as dialogs in a dialog set.

The bot's top-level dialog provides the guest with these two options. If the guest wants to reserve a table, the top-level dialog uses the _begin dialog_ async method to start the table reservation dialog. If the guest wants to order room service, the top-level dialog instead starts the dinner ordering dialog.

The dinner ordering dialog first asks the guest to select food items off a menu, and then asks for their room number. The selection of food items is _also_ a dialog - it is called into play multiple times as a guest selects items from the menu before submitting the dinner order.

This diagram illustrates the dialogs we'll be creating in this article and their relationship to each other.

![Illustration of the dialogs used in this article](~/media/complex-conversation-flows.png)

## How to branch

The dialog context maintains a _dialog stack_ and for each dialog on the stack, tracks which step is next. Its _begin dialog_ method pushes a dialog onto the top of the stack, and its _end dialog_ method pops the top dialog off the stack.

To branch, choose one from a set of child dialogs to start. For more information about this concept, see [branch a conversation](bot-builder-concept-dialog.md#branch-a-conversation).

## How to loop

The dialog context's _replace dialog_ method allows you to replace the dialog that is on top of the stack. The state of the old dialog is discarded and the new dialog starts from the beginning. You can use this method to create a loop by replacing a dialog with itself. However, to [persist any information the bot has already collected](bot-builder-howto-v4-state.md), you will need to pass that information to the new instance of the dialog in the call to the _replace dialog_ method.

In the following example, you will see that the room service order is stored on the dialog state, and when the `orderPrompt` dialog is repeated, the current dialog state is passed in so that the new dialog's state can continue adding items to it. If you prefer to store this information in a bot state outside of the dialog, see how to [persist user data](bot-builder-tutorial-persist-user-inputs.md).

## Create the dialogs for the hotel bot

In this section we'll create the dialogs to manage a conversation for the hotel bot we described.

### Install the dialogs library

# [C#](#tab/csharp)

We'll start from a basic EchoBot template. For instructions, see the [quickstart for .NET](../dotnet/bot-builder-dotnet-sdk-quickstart.md).

To use dialogs, install the `Microsoft.Bot.Builder.Dialogs` NuGet package for your project or solution.
Then reference the dialogs library in using statements in your code files as necessary.

```csharp
using Microsoft.Bot.Builder.Dialogs;
```

# [JavaScript](#tab/javascript)

We'll start from a EchoBot template. For instructions, see the [quickstart for JavaScript](../javascript/bot-builder-javascript-quickstart.md).



The `botbuilder-dialogs` library can be downloaded from npm. To install the `botbuilder-dialogs` library, run the following npm command:

```cmd
npm install --save botbuilder-dialogs
```

To use **dialogs** in your bot, include it in the bot code. For example, add this to your **index.js** file:

```javascript
const { DialogSet } = require('botbuilder-dialogs');
```

And this to your **bot.js** file:

```javascript
const { DialogSet, NumberPrompt, ChoicePrompt, WaterfallDialog } = require('botbuilder-dialogs');
```

---

### Create a dialog set

Create a _dialog set_ to which we'll add all of the dialogs for this example.

# [C#](#tab/csharp)

Create a **HotelDialogs** class, and add the using statements we'll need.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
```

Derive the class from **DialogSet**. Include a constructor that takes an `IStatePropertyAccessor<DialogState>` parameter to use to manage the internal state of an instance of the dialog set. Also, define the IDs and keys we'll use to identify the dialogs, prompts, and state information for this dialog set.

```csharp
/// <summary>Contains the set of dialogs and prompts for the hotel bot.</summary>
public class HotelDialogs : DialogSet
{
    /// <summary>The ID of the top-level dialog.</summary>
    public const string MainMenu = "mainMenu";

    public HotelDialogs(IStatePropertyAccessor<DialogState> dialogStateAccessor)
        : base(dialogStateAccessor)
    {
    }

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

In the **index.js** file, add code to create a state property accessor for managing dialog state, and use that to create the dialog set we'll use for the bot.

```javascript
// Create conversation state with in-memory storage provider.
const conversationState = new ConversationState(memoryStorage);
const dialogStateAccessor = conversationState.createProperty('dialogState');

// Create a dialog set for the bot.
const dialogSet = new DialogSet(dialogStateAccessor);

// Create the bot.
const bot = new MyBot(conversationState, dialogSet)
```

Then update the activity processing call to use the bot object.

```javascript
// Listen for incoming requests.
server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (context) => {
        // Route to the bot's turn handler.
        await bot.onTurn(context);
    });
});
```

---

### Add the prompts to the set

We'll use a **ChoicePrompt** to ask guests whether to order dinner or to reserve a table, and also which option off the dinner menu to select. And, we'll use a **NumberPrompt** to ask for the guest's room number when they do order dinner.

# [C#](#tab/csharp)

In the **HotelDialogs** constructor, add the two prompts.

```csharp
// Add the prompts.
Add(new ChoicePrompt(Inputs.Choice));
Add(new NumberPrompt<int>(Inputs.Number));
```

# [JavaScript](#tab/javascript)

Update the bot constructor, and add two prompts to the dialog set.

```javascript
constructor(conversationState, dialogSet) {
    // Creates a new state accessor property.
    // See https://aka.ms/about-bot-state-accessors to learn more about the bot state and state accessors.
    this.countProperty = conversationState.createProperty(TURN_COUNTER_PROPERTY);
    this.conversationState = conversationState;
    this.dialogSet = dialogSet;

    this.dialogSet.add(new ChoicePrompt('choicePrompt'));
    this.dialogSet.add(new NumberPrompt('numberPrompt'));
}
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
    /// <summary>Gets or sets the text to show the guest for this option.</summary>
    public string Description { get; set; }

    /// <summary>Gets or sets the ID of the associated dialog for this option.</summary>
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

    /// <summary>Gets or sets the name of the meal item or the request.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the price of the meal item; or NaN for a request.</summary>
    public double Price { get; set; }

    /// <summary>Gets the text to show the guest for this option.</summary>
    public string Description => double.IsNaN(Price) ? Name : $"{Name} - ${Price:0.00}";
}
```

```csharp
/// <summary>Contains the lists used to present options to the guest.</summary>
private static class Lists
{
    /// <summary>Gets the options for the top-level dialog.</summary>
    public static List<WelcomeChoice> WelcomeOptions { get; } = new List<WelcomeChoice>
    {
        new WelcomeChoice { Description = "Order dinner", DialogName = Dialogs.OrderDinner },
        new WelcomeChoice { Description = "Reserve a table", DialogName = Dialogs.ReserveTable },
    };

    private static readonly List<string> _welcomeList = WelcomeOptions.Select(x => x.Description).ToList();

    /// <summary>Gets the choices to present in the choice prompt for the top-level dialog.</summary>
    public static IList<Choice> WelcomeChoices { get; } = ChoiceFactory.ToChoices(_welcomeList);

    /// <summary>Gets the reprompt action for the top-level dialog.</summary>
    public static Activity WelcomeReprompt
    {
        get
        {
            var reprompt = MessageFactory.SuggestedActions(_welcomeList, "Please choose an option");
            reprompt.AttachmentLayout = AttachmentLayoutTypes.List;
            return reprompt as Activity;
        }
    }

    /// <summary>Gets the options for the food-selection dialog.</summary>
    public static List<MenuChoice> MenuOptions { get; } = new List<MenuChoice>
    {
        new MenuChoice { Name = "Potato Salad", Price = 5.99 },
        new MenuChoice { Name = "Tuna Sandwich", Price = 6.89 },
        new MenuChoice { Name = "Clam Chowder", Price = 4.50 },
        new MenuChoice { Name = MenuChoice.Process, Price = double.NaN },
        new MenuChoice { Name = MenuChoice.Cancel, Price = double.NaN },
    };

    private static readonly List<string> _menuList = MenuOptions.Select(x => x.Description).ToList();

    /// <summary>Gets the choices to present in the choice prompt for the food-selection dialog.</summary>
    public static IList<Choice> MenuChoices { get; } = ChoiceFactory.ToChoices(_menuList);

    /// <summary>Gets the reprompt action for the food-selection dialog.</summary>
    public static Activity MenuReprompt
    {
        get
        {
            var reprompt = MessageFactory.SuggestedActions(_menuList, "Please choose an option");
            reprompt.AttachmentLayout = AttachmentLayoutTypes.List;
            return reprompt as Activity;
        }
    }
}
```

# [JavaScript](#tab/javascript)

In the **bot.js** file, create a **dinnerMenu** constant to contain this information.

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

This dialog uses a `ChoicePrompt` to display the menu and then wait for the user to choose an option. When the user chooses either `Order dinner` or `Reserve a table`, it starts the appropriate dialog. When the child dialog is done, instead of just ending the dialog in the last step and leaving the user wondering what's happening, the `mainMenu` dialog loops by starting the `mainMenu` dialog again. With this simple structure, the bot will always show the menu and the user will always know what their choices are.

The `mainMenu` dialog contains these waterfall steps:

* In the first step of the waterfall, we initialize or clear the dialog state, greet the guest, and ask them to choose from the available options: `Order dinner` or `Reserve a table`.
* In the second step, we retrieve their selection and then start the child dialog that is associated with that selection. Once the child dialog ends, this dialog will resume with the following step.
* In the last step, we use the _replace dialog_ method to replace this dialog with a new instance of itself, which effectively turns the welcome dialog into a looping menu.

# [C#](#tab/csharp)

Within the constructor, add the waterfall dialog. Then define the waterfall steps. Here, we've defined them in a nested class.

Within the **HotelDialogs** constructor.

```csharp
// Define the steps for and add the main welcome dialog.
WaterfallStep[] welcomeDialogSteps = new WaterfallStep[]
{
    MainDialogSteps.PresentMenuAsync,
    MainDialogSteps.ProcessInputAsync,
    MainDialogSteps.RepeatMenuAsync,
};

Add(new WaterfallDialog(MainMenu, welcomeDialogSteps));
```

Within the **HotelDialogs** class, add the step definitions.

```csharp
/// <summary>
/// Contains the waterfall dialog steps for the order-dinner dialog.
/// </summary>
private static class MainDialogSteps
{
    public static async Task<DialogTurnResult> PresentMenuAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken)
    {
        // Greet the guest and ask them to choose an option.
        await stepContext.Context.SendActivityAsync(
            "Welcome to Contoso Hotel and Resort.",
            cancellationToken: cancellationToken);
        return await stepContext.PromptAsync(
            Inputs.Choice,
            new PromptOptions
            {
                Prompt = MessageFactory.Text("How may we serve you today?"),
                RetryPrompt = Lists.WelcomeReprompt,
                Choices = Lists.WelcomeChoices,
            },
            cancellationToken);
    }

    public static async Task<DialogTurnResult> ProcessInputAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken)
    {
        // Begin a child dialog associated with the chosen option.
        var choice = (FoundChoice)stepContext.Result;
        var dialogId = Lists.WelcomeOptions[choice.Index].DialogName;

        return await stepContext.BeginDialogAsync(dialogId, null, cancellationToken);
    }

    public static async Task<DialogTurnResult> RepeatMenuAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken)
    {
        // Start this dialog over again.
        return await stepContext.ReplaceDialogAsync(MainMenu, null, cancellationToken);
    }
}
```

# [JavaScript](#tab/javascript)

In the bot constructor, add the `mainMenu` waterfall dialog.

```javascript
// Display a menu and ask user to choose a menu item. Direct user to the item selected otherwise, show
// the menu again.
this.dialogSet.add(new WaterfallDialog('mainMenu', [
    async function (step) {
        // Welcome the user and send a prompt.
        await step.context.sendActivity("Welcome to Contoso Hotel and Resort.");
        return await step.prompt('choicePrompt', "How may we serve you today?", ['Order Dinner', 'Reserve a table']);
    },
    async function (step) {
        // Handle the user's response to the previous prompt and branch the dialog.
        if (step.result.value.match(/order dinner/ig)) {
            return await step.beginDialog('orderDinner');
        } else if (step.result.value.match(/reserve a table/ig)) {
            return await step.beginDialog('reserveTable');
        }
    },
    async function (step) {
        // Calling replaceDialog will loop the main menu
        return await step.replaceDialog('mainMenu');
    }
]));
```

---

### Create the order dinner dialog

In the order-dinner dialog, we'll welcome the guest to the "dinner order service" and immediately start the food-selection dialog, which we'll cover in the next section. Importantly, if the guest asks the service to process their order, the food-selection dialog returns the list of items on the order. To complete the process, this dialog then asks for a room number to which to deliver the food, and then sends a confirmation message. If the guest cancels their order, this dialog doesn't ask for a room number and ends immediately.

# [C#](#tab/csharp)

To the **HotelDialog** class, add a data structure we can use to track the guest's dinner order. Since this will be persisted in the dialog state, the class needs the default constructor for serialization.

```csharp
/// <summary>Contains the guest's dinner order.</summary>
private class OrderCart : List<MenuChoice>
{
    public OrderCart() : base() { }

    public OrderCart(OrderCart other) : base(other) { }
}
```

Within the constructor, add the waterfall dialog. Then define the waterfall steps. Here, we've defined them in a nested class.

Within the **HotelDialogs** constructor.

```csharp
// Define the steps for and add the order-dinner dialog.
WaterfallStep[] orderDinnerDialogSteps = new WaterfallStep[]
{
    OrderDinnerSteps.StartFoodSelectionAsync,
    OrderDinnerSteps.GetRoomNumberAsync,
    OrderDinnerSteps.ProcessOrderAsync,
};

Add(new WaterfallDialog(Dialogs.OrderDinner, orderDinnerDialogSteps));
```

Within the **HotelDialogs** class, add the step definitions.

* In the first step, we send a welcome message and start the food-selection dialog.
* In the second step, we check whether the food-selection dialog returned an order cart.
  * If so, prompt them for their room number and save the cart information.
  * If not, assume they cancelled their order, and call the _end dialog_ method to end this dialog.
* In the last step, record their room number and send them a confirmation message before ending. This is the step in which your bot would forward the order to a processing service.

```csharp
/// <summary>
/// Contains the waterfall dialog steps for the order-dinner dialog.
/// </summary>
private static class OrderDinnerSteps
{
    public static async Task<DialogTurnResult> StartFoodSelectionAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken)
    {
        await stepContext.Context.SendActivityAsync(
            "Welcome to our Dinner order service.",
            cancellationToken: cancellationToken);

        // Start the food selection dialog.
        return await stepContext.BeginDialogAsync(Dialogs.OrderPrompt, null, cancellationToken);
    }

    public static async Task<DialogTurnResult> GetRoomNumberAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken)
    {
        if (stepContext.Result != null && stepContext.Result is OrderCart cart)
        {
            // If there are items in the order, record the order and ask for a room number.
            stepContext.Values[Outputs.OrderCart] = cart;
            return await stepContext.PromptAsync(
                Inputs.Number,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("What is your room number?"),
                    RetryPrompt = MessageFactory.Text("Please enter your room number."),
                },
                cancellationToken);
        }
        else
        {
            // Otherwise, assume the order was cancelled by the guest and exit.
            return await stepContext.EndDialogAsync(null, cancellationToken);
        }
    }

    public static async Task<DialogTurnResult> ProcessOrderAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken)
    {
        // Get and save the guest's answer.
        var roomNumber = (int)stepContext.Result;
        stepContext.Values[Outputs.RoomNumber] = roomNumber;

        // Process the dinner order using the collected order cart and room number.

        await stepContext.Context.SendActivityAsync(
            $"Thank you. Your order will be delivered to room {roomNumber} within 45 minutes.",
            cancellationToken: cancellationToken);
        return await stepContext.EndDialogAsync(null, cancellationToken);
    }
}
```

# [JavaScript](#tab/javascript)

In the bot constructor, add the `orderDinner` waterfall dialog.

```javascript
// Order dinner:
// Help user order dinner from a menu
this.dialogSet.add(new WaterfallDialog('orderDinner', [
    async function (step) {
        await step.context.sendActivity("Welcome to our dinner order service.");

        return await step.beginDialog('orderPrompt', step.values.orderCart = {
            orders: [],
            total: 0
        }); // Prompt for orders
    },
    async function (step) {
        if (step.result == "Cancel") {
            return await step.endDialog();
        } else {
            return await step.prompt('numberPrompt', "What is your room number?");
        }
    },
    async function (step) {
        await step.context.sendActivity(`Thank you. Your order will be delivered to room ${step.result} within 45 minutes.`);
        return await step.endDialog();
    }
]));
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

Within the constructor, add the waterfall dialog. Then define the waterfall steps. Here, we've defined them in a nested class.

Within the **HotelDialogs** constructor.

```csharp
// Define the steps for and add the order-prompt dialog.
WaterfallStep[] orderPromptDialogSteps = new WaterfallStep[]
{
    OrderPromptSteps.PromptForItemAsync,
    OrderPromptSteps.ProcessInputAsync,
};

Add(new WaterfallDialog(Dialogs.OrderPrompt, orderPromptDialogSteps));
```

* In the first step, we initialize the dialog state. If the input arguments to the dialog contain cart information, we save that to our dialog state; otherwise, we create an empty cart and add that. We then prompt the guest for a choice from the dinner menu.
* In the next step, we look at the option the guest picked:
  * If it is a request to process the order, check whether the cart contains any items.
    * If so, end the dialog, returning the cart contents.
    * Otherwise, send an error message to the guest and start over from the beginning of the dialog.
  * If it is a request to cancel the order, end the dialog, returning an empty dictionary.
  * If it is a dinner item, add it to the cart, send a status message, and start the dialog over, passing in the current order state.

Within the **HotelDialogs** class, add the step definitions.

```csharp
/// <summary>
/// Contains the waterfall dialog steps for the order-prompt dialog.
/// </summary>
private static class OrderPromptSteps
{
    public static async Task<DialogTurnResult> PromptForItemAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken)
    {
        // First time through, options will be null.
        var cart = (stepContext.Options is OrderCart oldCart && oldCart != null)
            ? new OrderCart(oldCart) : new OrderCart();

        stepContext.Values[Outputs.OrderCart] = cart;
        stepContext.Values[Outputs.OrderTotal] = cart.Sum(item => item.Price);

        return await stepContext.PromptAsync(
            Inputs.Choice,
            new PromptOptions
            {
                Prompt = MessageFactory.Text("What would you like?"),
                RetryPrompt = Lists.MenuReprompt,
                Choices = Lists.MenuChoices,
            },
            cancellationToken);
    }

    public static async Task<DialogTurnResult> ProcessInputAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken)
    {
        // Get the guest's choice.
        var choice = (FoundChoice)stepContext.Result;
        var menuOption = Lists.MenuOptions[choice.Index];

        // Get the current order from dialog state.
        var cart = (OrderCart)stepContext.Values[Outputs.OrderCart];

        if (menuOption.Name is MenuChoice.Process)
        {
            if (cart.Count > 0)
            {
                // If there are any items in the order, then exit this dialog,
                // and return the list of selected food items.
                return await stepContext.EndDialogAsync(cart, cancellationToken);
            }
            else
            {
                // Otherwise, send an error message and restart from
                // the beginning of this dialog.
                await stepContext.Context.SendActivityAsync(
                    "Your cart is empty. Please add at least one item to the cart.",
                    cancellationToken: cancellationToken);
                return await stepContext.ReplaceDialogAsync(Dialogs.OrderPrompt, null, cancellationToken);
            }
        }
        else if (menuOption.Name is MenuChoice.Cancel)
        {
            await stepContext.Context.SendActivityAsync(
                "Your order has been cancelled.",
                cancellationToken: cancellationToken);

            // Exit this dialog, returning null.
            return await stepContext.EndDialogAsync(null, cancellationToken);
        }
        else
        {
            // Add the selected food item to the order and update the order total.
            cart.Add(menuOption);
            var total = (double)stepContext.Values[Outputs.OrderTotal] + menuOption.Price;
            stepContext.Values[Outputs.OrderTotal] = total;

            await stepContext.Context.SendActivityAsync(
                $"Added {menuOption.Name} (${menuOption.Price:0.00}) to your order." +
                    Environment.NewLine + Environment.NewLine +
                    $"Your current total is ${total:0.00}.",
                cancellationToken: cancellationToken);

            // Present the order options again, passing in the current order state.
            return await stepContext.ReplaceDialogAsync(Dialogs.OrderPrompt, cart);
        }
    }
}
```

# [JavaScript](#tab/javascript)

In the bot constructor, add the `orderPrompt` waterfall dialog.

```javascript
// Helper dialog to repeatedly prompt user for orders
this.dialogSet.add(new WaterfallDialog('orderPrompt', [
    async function (step) {
        // Define a new cart of one does not exists
        step.values.orderCart = step.options;

        return await step.prompt('choicePrompt', "What would you like?", dinnerMenu.choices);
    },
    async function (step) {
        const choice = step.result;
        if (choice.value.match(/process order/ig)) {
            if (step.values.orderCart.orders.length > 0) {
                // Process the order
                await step.context.sendActivity("Processing your order.");
                // ...
                step.values.orderCart = undefined; // Reset cart
                return await step.endDialog();
            } else {
                await step.context.sendActivity("Your cart was empty. Please add at least one item to the cart.");
                // Ask again
                return await step.replaceDialog('orderPrompt', step.values.orderCart);
            }
        } else if (choice.value.match(/cancel/ig)) {
            await step.context.sendActivity("Your order has been canceled.");
            return await step.endDialog(choice.value);
        } else {
            var item = dinnerMenu[choice.value];

            // Only proceed if user chooses an item from the menu
            if (!item) {
                await step.context.sendActivity("Sorry, that is not a valid item. Please pick one from the menu.");

                // Ask again
                return await step.replaceDialog('orderPrompt', step.values.orderCart);
            } else {
                // Add the item to cart
                step.values.orderCart.orders.push(item);
                step.values.orderCart.total += item.Price;

                await step.context.sendActivity(`Added to cart: ${choice.value}. <br/>Current total: $${step.values.orderCart.total}`);

                // Ask again
                return await step.replaceDialog('orderPrompt', step.values.orderCart);
            }
        }
    }
]));
```

The sample code above shows that the main `orderDinner` dialog uses a helper dialog named `orderPrompt` to handle user choices. The `orderPrompt` dialog displays the menu of food items, asks the user to choose an item, add the item to cart and prompts again in a loop. This allows the user to add multiple items to their order. The dialog loops until the user chooses `Process order` or `Cancel`. At which point, execution is handed back to the parent dialog (the `orderDinner` dialog). The `orderDinner` dialog does some last minute house keeping if the user wants to process the order; otherwise, it ends and returns execution back to its parent dialog (e.g.: `mainMenu`). The `mainMenu` dialog in turn continues executing the last step which is to simply redisplay the main menu choices.

---

### Create the reserve table dialog

<!-- TODO: Update the "Manage simple conversation flows" topic to use a reserveTable dialog, and then reference that from here. -->

To keep this example shorter, we show only a minimal implementation of the `reserveTable` dialog here.

# [C#](#tab/csharp)

Within the constructor, add the waterfall dialog. Then define the waterfall steps. Here, we've defined them in a nested class.

Within the **HotelDialogs** constructor.

```csharp
// Define the steps for and add the reserve-table dialog.
WaterfallStep[] reserveTableDialogSteps = new WaterfallStep[]
{
    ReserveTableSteps.StubAsync,
};

Add(new WaterfallDialog(Dialogs.ReserveTable, reserveTableDialogSteps));
```

Within the **HotelDialogs** class, add the step definitions.

```csharp
/// <summary>
/// Contains the waterfall dialog steps for the reserve-table dialog.
/// </summary>
private static class ReserveTableSteps
{
    public static async Task<DialogTurnResult> StubAsync(
        WaterfallStepContext stepContext,
        CancellationToken cancellationToken)
    {
        await stepContext.Context.SendActivityAsync(
            "Your table has been reserved.",
            cancellationToken: cancellationToken);

        return await stepContext.EndDialogAsync(null, cancellationToken);
    }
}
```

# [JavaScript](#tab/javascript)

In the bot constructor, add the placeholder `reserveTable` waterfall dialog.

```javascript
// Reserve a table:
// Help the user to reserve a table
this.dialogSet.add(new WaterfallDialog('reserveTable', [
    // Replace this waterfall with your reservation steps.
    async function(step){
        await step.context.sendActivity("Your table has been reserved");
        await step.endDialog();
    }
]));
```

---

### Update the bot code to call the dialogs

Update your bot's turn handler code to call the dialog.

# [C#](#tab/csharp)

Rename **EchoBotAccessors.cs** to **BotAccessors.cs**, and rename the class from `EchoBotAccessors` to `BotAccessors`. Update the using statements and the class definition to provide the state property accessor we need for this bot.

```csharp
using System;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
```

```csharp
/// <summary>
/// This class is created as a Singleton and passed into the IBot-derived constructor.
///  - See <see cref="EchoWithCounterBot"/> constructor for how that is injected.
///  - See the Startup.cs file for more details on creating the Singleton that gets
///    injected into the constructor.
/// </summary>
public class BotAccessors
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BotAccessors"/> class.
    /// Contains the <see cref="ConversationState"/> and associated <see cref="IStatePropertyAccessor{T}"/>.
    /// </summary>
    /// <param name="conversationState">The state object that stores the counter.</param>
    public BotAccessors(ConversationState conversationState)
    {
        ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
    }

    /// <summary>
    /// Gets the <see cref="IStatePropertyAccessor{T}"/> name used for the <see cref="DialogState"/> accessor.
    /// </summary>
    /// <remarks>Accessors require a unique name.</remarks>
    /// <value>The accessor name for the dialog state accessor.</value>
    public static string DialogStateAccessorName { get; } = $"{nameof(BotAccessors)}.DialogState";

    /// <summary>
    /// Gets or sets the DialogState property accessor.
    /// </summary>
    /// <value>
    /// The DialogState property accessor.
    /// </value>
    public IStatePropertyAccessor<DialogState> DialogStateAccessor { get; set; }

    /// <summary>
    /// Gets the <see cref="ConversationState"/> object for the conversation.
    /// </summary>
    /// <value>The <see cref="ConversationState"/> object.</value>
    public ConversationState ConversationState { get; }
}
```

Update the **Startup.cs** file to configure the `BotAccessors` singleton.

1. Update the using statements.

    ```csharp
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Integration;
    using Microsoft.Bot.Builder.Integration.AspNet.Core;
    using Microsoft.Bot.Configuration;
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    ```

1. Update the part of the `ConfigureServices` method that registers the bot state property accessors.

    ```csharp
    // Create and register state accesssors.
    // Acessors created here are passed into the IBot-derived class on every turn.
    services.AddSingleton<BotAccessors>(sp =>
    {
        var options = sp.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;
        var conversationState = options.State.OfType<ConversationState>().FirstOrDefault();

        // Create the custom state accessor.
        // State accessors enable other components to read and write individual properties of state.
        var accessors = new BotAccessors(conversationState)
        {
            DialogStateAccessor = conversationState.CreateProperty<DialogState>(BotAccessors.DialogStateAccessorName),
        };

        return accessors;
    });
    ```

Rename the EchoWithCounterBot.cs file to HotelBot.cs, and rename the class from EchoWithCounterBot to HotelBot.

1. Update the initialization code for the bot.

    ```csharp
    private readonly BotAccessors _accessors;
    private readonly HotelDialogs _dialogs;
    private readonly ILogger _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="HotelBot"/> class.
    /// </summary>
    /// <param name="accessors">A class containing <see cref="IStatePropertyAccessor{T}"/> used to manage state.</param>
    /// <param name="loggerFactory">A <see cref="ILoggerFactory"/> that is hooked to the Azure App Service provider.</param>
    public HotelBot(BotAccessors accessors, ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<HotelBot>();
        _logger.LogTrace("EchoBot turn start.");
        _accessors = accessors;
        _dialogs = new HotelDialogs(_accessors.DialogStateAccessor);
    }
    ```

1. Update the turn handler for the bot, so that it runs the dialog.

    ```csharp
    public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
    {
        var dc = await _dialogs.CreateContextAsync(turnContext, cancellationToken);

        if (turnContext.Activity.Type == ActivityTypes.Message)
        {
            await dc.ContinueDialogAsync(cancellationToken);
            if (!turnContext.Responded)
            {
                await dc.BeginDialogAsync(HotelDialogs.MainMenu, null, cancellationToken);
            }
        }
        else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
        {
            var activity = turnContext.Activity.AsConversationUpdateActivity();
            if (activity.MembersAdded.Any(member => member.Id != activity.Recipient.Id))
            {
                await dc.BeginDialogAsync(HotelDialogs.MainMenu, null, cancellationToken);
            }
        }

        await _accessors.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
    }
    ```

# [JavaScript](#tab/javascript)

```javascript
async onTurn(turnContext) {
    let dc = await this.dialogSet.createContext(turnContext);

    // See https://aka.ms/about-bot-activity-message to learn more about the message and other activity types.
    if (turnContext.activity.type === ActivityTypes.Message) {

        await dc.continueDialog();

        if (!turnContext.responded) {
            await dc.beginDialog('mainMenu');
        }
    } else if (turnContext.activity.type === ActivityTypes.ConversationUpdate) {
        // Do we have any new members added to the conversation?
        if (turnContext.activity.membersAdded.length !== 0) {
            // Iterate over all new members added to the conversation
            for (var idx in turnContext.activity.membersAdded) {
                // Greet anyone that was not the target (recipient) of this message.
                // Since the bot is the recipient for events from the channel,
                // context.activity.membersAdded === context.activity.recipient.Id indicates the
                // bot was added to the conversation, and the opposite indicates this is a user.
                if (turnContext.activity.membersAdded[idx].id !== turnContext.activity.recipient.id) {
                    // Start the dialog.
                    await dc.beginDialog('mainMenu');
                }
            }
        }
    }

    // Save state changes
    await this.conversationState.saveChanges(turnContext);
}
```

---

## Next steps

You can enhance this bot by offering other options like "more info" or "help" to the menu choices. For more information on implementing these types of interruptions, see [Handle user interruptions](bot-builder-howto-handle-user-interrupt.md).

Now that you have learned how to use dialogs, prompts, and waterfalls to manage complex conversation flows, let's take a look at how we can break our dialogs (such as the `orderDinner` and `reserveTable` dialogs) into separate objects, instead of lumping them all together in one large dialog set.

> [!div class="nextstepaction"]
> [Create modular bot logic with Composite Control](bot-builder-compositcontrol.md)
