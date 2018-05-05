---
title: Handle user interrupt | Microsoft Docs
description: Learn how to handle user interrupt and direct conversation flow.
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

There is no right answer to these questions as each situation is unique to the scenario your bot is designed to do. In this topic, we will explore some common ways to handle user interruptions and suggest some ways to implement them in your bot.

> [!NOTE]
> The full code sample used in this article can be found at [Dialog flow](#)

## Handle expected interruptions

Procedural conversation flows have a core set of steps that you want to lead the user through. Any user actions that break this flow are potential interruptions. In a normal flow, there are interruptions that you can anticipate. Take the following situations for example.

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

For example, in the order dinner flow, you can provide expected interruptions along with the menu items. In this case, the menu items are send as an array of `choices`.

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

Then, in your ordering logic, you can check for them using string matching or regular expressions.

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

## Handle unexpected interruptions

There are interruptions that are out of scope of what your bot is designed to do. While you cannot anticipate all interruptions, there are pattern of interruptions in which you can program your bot to handle.

### Switching topic of conversations
What if the user is in the middle of one conversation and wants to switch to another conversation? Assuming your bot supports both conversations. For example, your bot can reserve a table and order dinner. While the user is in the reserve a table flow, instead of answering the question for "How many people is in your party?", the user sends the message "order dinner". The user, in this case, changed their mind and wants to engage in a dinner ordering conversation instead. How should you handle this interruption? 

The choice is up to you. You can switch topic to the order dinner flow or you can make it a sticky issue by telling the user that you are expecting a number and reprompt them. If you do allow them to switch topic, you then have to decide if you will save the progress so that the user can pick up from where they left off or you could delete all the information you have collected so that they will have to start that process all over next time they want to reserve a table. For more information about managing user state data, see [Save state using conversation and user properties](bot-builder-howto-v4-state.md).

### Apply artificial intellgence
For interruptions that are not in scope, you can try to guess what the user intent is. You can do this using AI services such as QnAMaker, LUIS, or your custom logic. Then offer up suggestions for what the bot thinks the user wants. For example, while in the middle of the reserve table flow, the user say, "I want to order a burger". This is not something the bot knows how to handle from this conversation flow. Since the current flow has nothing to do with ordering, and the bot's other conversation command is "order dinner", so the bot does not know what to do with this input. If you apply LUIS, for example, you could train the model to recognize that they want to order food (e.g.: LUIS can return an "orderFood" intent). Thus, the bot could response with, "It seems you want to order food. Would you like to switch to our order dinner process instead?" For more information on training LUIS and detecting user intents, see [User LUIS for language understanding](bot-builder-howto-v4-luis.md).

### Default response
If all else fails, you can send a generic default response instead of doing nothing and leaving the user wondering what is going on. The default response should tell the user what commands the bot understand so the user can get back on track.

You can check against the `context.responded` object at the end of the `adapter.processActivity` call to see if the bot had send anything back to the user during the turn. If the bot processes the user's input but does not respond, chances are that the bot does not know what to do with the input. In that case, you can catch it and send the user a default message.

The default for this bot is to give the user the `mainMenu` again. It's up to you to decide what experience that user will have in this situation for your bot.

```javascript
// Check to see if anyone replied. If not then clear all the stack and present the main menu
if (!context.responded) {
    await dc.endAll().begin('mainMenu');
}
```

## Next step
