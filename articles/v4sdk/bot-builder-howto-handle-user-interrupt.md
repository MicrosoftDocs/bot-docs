---
title: Handle user interrupt | Microsoft Docs
description: Learn how to handle user interrupt and direct conversation flow.
keywords: interrupt, interruptions, switching topic, break
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 04/17/2018
ms.reviewer:
monikerRange: 'azure-bot-service-4.0'
---

# Handle user interrupt

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

Handling user interrupt is an important aspect of a robust bot. While you may think that your users will follow your defined conversation flow step by step, chances are good that they will change their minds or ask a question in the middle of the process instead of answering the question. In these situations, how would your bot handle the user's input? What would the user experience be like? How would you maintain user state data?

There is no right answer to these questions as each situation is unique to the scenario your bot is designed to handle. In this topic, we will explore some common ways to handle user interruptions and suggest some ways to implement them in your bot.

## Handle expected interruptions

A procedural conversation flow has a core set of steps that you want to lead the user through, and any user actions that vary from those steps are potential interruptions. In a normal flow, there are interruptions that you can anticipate.

**Table reservation**
In a table reservation bot, the core steps may be to ask the user for a date and time, the size of the party, and the reservation name. In that process, some expected interruptions you could anticipate may include: 
 * `cancel`: To exit the process.
 * `help`: To provide additional guidance about this process.
 * `more info`: To give hints and suggestions or provide alternative ways of reserving a table (e.g.: an email address or phone number to contact).
 * `show list of available tables`: If that is an option; show a list of tables available for the date and time the user wanted.

**Order dinner**
In an order dinner bot, the core steps would be to provide a list of menu items and allow the user to add items to their cart. In this process, some expected interruptions you could anticipate may include: 
 * `cancel`: To exit the ordering process.
 * `more info`: To provide dietary detail about each menu item.
 * `help`: To provide help on how to use the system.
 * `process order`: To process the order.

You could provide these to the user as a list of **suggested actions** or as a hint so the user is at least aware of what commands they can send that the bot would understand.

For example, in the order dinner flow, you can provide expected interruptions along with the menu items. In this case, the menu items are sent as an array of `choices`.

# [C#](#tab/csharptab)

```cs
public class dinnerItem
{
    public string Description;
    public double Price;
}

public class dinnerMenu
{
    static public Dictionary<string, dinnerItem> dinnerChoices = new Dictionary<string, dinnerItem>
    {
        { "potato salad", new dinnerItem { Description="Potato Salad", Price=5.99 } },
        { "tuna sandwich", new dinnerItem { Description="Tuna Sandwich", Price=6.89 } },
        { "clam chowder", new dinnerItem { Description="Clam Chowder", Price=4.50 } }
    };

    static public string[] choices = new string[] {"Potato Salad", "Tuna Sandwich", "Clam Chowder", "more info", "Process order", "help", "Cancel"};
}
```

# [JavaScript](#tab/jstab)

```javascript
var dinnerMenu = {
    choices: ["Potato Salad - $5.99", "Tuna Sandwich - $6.89", "Clam Chowder - $4.50", 
            "more info", "Process order", "Cancel"],
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

In your ordering logic, you can check for them using string matching or regular expressions.

# [C#](#tab/csharptab)

First, we need to define a helper to keep track of our orders

```cs
// Helper class for storing the order in the dictionary
public class Orders
{
    public double total;
    public string order;
    public bool processOrder;

    // Initialize order values
    public Orders()
    {
        total = 0;
        order = "";
        processOrder = false;
    }
}
```

Then, add the dialog to your bot.

```cs
dialogs.Add("orderPrompt", new WaterfallStep[]
{
    async (dc, args, next) =>
    {
        // Prompt the user
        await dc.Prompt("choicePrompt",
            "What would you like for dinner?",
            new ChoicePromptOptions
            {
                Choices = dinnerMenu.choices.Select( s => new Choice { Value = s }).ToList(),
                RetryPromptString = "I'm sorry, I didn't understand that. What would you " +
                    "like for dinner?"
            });
    },
    async(dc, args, next) =>
    {
        var convo = ConversationState<Dictionary<string,object>>.Get(dc.Context);

        // Get the user's choice from the previous prompt
        var response = (args["Value"] as FoundChoice).Value.ToLower();

        if(response == "process order")
        {
            try 
            {
                var order = convo["order"];

                await dc.Context.SendActivity("Order is on it's way!");
                
                // In production, you may want to store something more helpful, 
                // such as send order off to be made
                (order as Orders).processOrder = true;

                // Once it's submitted, clear the current order
                convo.Remove("order");
                await dc.End();
            }
            catch
            {
                await dc.Context.SendActivity("Your order is empty, please add your order choice");
                // Ask again
                await dc.Replace("orderPrompt");
            }
        }
        else if(response == "cancel" )
        {
            // Get rid of current order
            convo.Remove("order");
            await dc.Context.SendActivity("Your order has been canceled");
            await dc.End();
        }
        else if(response == "more info")
        {
            // Send more information about the options
            var msg = "More info: <br/>" +
                "Potato Salad: contains 330 calaries per serving. Cost: 5.99 <br/>"
                + "Tuna Sandwich: contains 700 calaries per serving. Cost: 6.89 <br/>"
                + "Clam Chowder: contains 650 calaries per serving. Cost: 4.50";
            await dc.Context.SendActivity(msg);

            // Ask again
            await dc.Replace("orderPrompt");
        }
        else if(response == "help")
        {
            // Provide help information
            await dc.Context.SendActivity("To make an order, add as many items to your cart as " +
                "you like then choose the \"Process order\" option to check out.");

            // Ask again
            await dc.Replace("orderPrompt");
        }
        else
        {
            // Unlikely to get past the prompt verification, but this will catch 
            // anything that isn't a valid menu choice
            if(!dinnerMenu.dinnerChoices.ContainsKey(response))
            {
                await dc.Context.SendActivity("Sorry, that is not a valid item. " +
                    "Please pick one from the menu.");
    
                // Ask again
                await dc.Replace("orderPrompt");
            }
            else {
                // Add the item to cart
                Orders currentOrder;

                // If there is a current order going, add to it. If not, start a new one
                try
                {
                    currentOrder = convo["order"] as Orders;
                }
                catch
                {
                    convo["order"] = new Orders();
                    currentOrder = convo["order"] as Orders;
                }

                // Add to the current order
                currentOrder.order += (dinnerMenu.dinnerChoices[$"{response}"].Description) + ", ";
                currentOrder.total += (double)dinnerMenu.dinnerChoices[$"{response}"].Price;

                // Save back to the conversation state
                convo["order"] = currentOrder;

                await dc.Context.SendActivity($"Added to cart. Current order: " +
                    $"{currentOrder.order} " +
                    $"<br/>Current total: ${currentOrder.total}");

                // Ask again to allow user to add more items or process order
                await dc.Replace("orderPrompt");
            }
        }
    }
});
```

# [JavaScript](#tab/jstab)

```javascript
// Helper dialog to repeatedly prompt user for orders
dialogs.add('orderPrompt', [
    async function(dc){
        await dc.prompt('choicePrompt', "What would you like?", dinnerMenu.choices);
    },
    async function(dc, choice){
        if(choice.value.match(/process order/ig)){
            if(orderCart.orders.length > 0) {
                // Process the order
                dc.context.activity.conversation.dinnerOrder = orderCart;

                await dc.end();
            }
            else {
                await dc.context.sendActivity("Your cart was empty. Please add at least one item to the cart.");
                // Ask again
                await dc.replace('orderPrompt');
            }
        }
        else if(choice.value.match(/cancel/ig)){
            orderCart.clear(context);
            await dc.context.sendActivity("Your order has been canceled.");
            await dc.end(choice.value);
        }
        else if(choice.value.match(/more info/ig)){
            var msg = "More info: <br/>Potato Salad: contains 330 calaries per serving. <br/>"
                + "Tuna Sandwich: contains 700 calaries per serving. <br/>" 
                + "Clam Chowder: contains 650 calaries per serving."
            await dc.context.sendActivity(msg);
            
            // Ask again
            await dc.replace('orderPrompt');
        }
        else if(choice.value.match(/help/ig)){
            var msg = `Help: <br/>To make an order, add as many items to your cart as you like then choose the "Process order" option to check out.`
            await dc.context.sendActivity(msg);
            
            // Ask again
            await dc.replace('orderPrompt');
        }
        else {
            var choice = dinnerMenu[choice.value];

            // Only proceed if user chooses an item from the menu
            if(!choice){
                await dc.context.sendActivity("Sorry, that is not a valid item. Please pick one from the menu.");
                
                // Ask again
                await dc.replace('orderPrompt');
            }
            else {
                // Add the item to cart
                orderCart.orders.push(choice);
                orderCart.total += dinnerMenu[choice.value].Price;

                await dc.context.sendActivity(`Added to cart: ${choice.value}. <br/>Current total: $${orderCart.total}`);

                // Ask again
                await dc.replace('orderPrompt');
            }
        }
    }
]);
```

---

## Handle unexpected interruptions

There are interruptions that are out of scope of what your bot is designed to do.
While you cannot anticipate all interruptions, there are patterns of interruptions that you can program your bot to handle.

### Switching topic of conversations
What if the user is in the middle of one conversation and wants to switch to another conversation? For example, your bot can reserve a table and order dinner.
While the user is in the _reserve a table_ flow, instead of answering the question for "How many people are in your party?", the user sends the message "order dinner". In this case, the user changed their mind and wants to engage in a dinner ordering conversation instead. How should you handle this interruption? 

You can switch topics to the order dinner flow or you can make it a sticky issue by telling the user that you are expecting a number and reprompt them. If you do allow them to switch topics, you then have to decide if you will save the progress so that the user can pick up from where they left off or you could delete all the information you have collected so that they will have to start that process all over next time they want to reserve a table. For more information about managing user state data, see [Save state using conversation and user properties](bot-builder-howto-v4-state.md).

### Apply artificial intelligence
For interruptions that are not in scope, you can try to guess what the user intent is. You can do this using AI services such as QnAMaker, LUIS, or your custom logic, then offer up suggestions for what the bot thinks the user wants. 

For example, while in the middle of the reserve table flow, the user says, "I want to order a burger". This is not something the bot knows how to handle from this conversation flow. Since the current flow has nothing to do with ordering, and the bot's other conversation command is "order dinner", the bot does not know what to do with this input. If you apply LUIS, for example, you could train the model to recognize that they want to order food (e.g.: LUIS can return an "orderFood" intent). Thus, the bot could response with, "It seems you want to order food. Would you like to switch to our order dinner process instead?" For more information on training LUIS and detecting user intents, see [User LUIS for language understanding](bot-builder-howto-v4-luis.md).

### Default response
If all else fails, you can send a generic default response instead of doing nothing and leaving the user wondering what is going on. The default response should tell the user what commands the bot understands so the user can get back on track.

You can check against the context **responded** flag at the end of the bot logic to see if the bot sent anything back to the user during the turn. If the bot processes the user's input but does not respond, chances are that the bot does not know what to do with the input. In that case, you can catch it and send the user a default message.

The default for this bot is to give the user the `mainMenu` dialog. It's up to you to decide what experience your user will have in this situation for your bot.

# [C#](#tab/csharptab)

```cs
if(!context.Responded)
{
    await dc.EndAll().Begin("mainMenu");
}
```

# [JavaScript](#tab/jstab)

```javascript
// Check to see if anyone replied. If not then clear all the stack and present the main menu
if (!context.responded) {
    await dc.endAll().begin('mainMenu');
}
```

---

