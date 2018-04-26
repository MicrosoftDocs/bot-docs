---
title: Manage a conversation flow with dialogs | Microsoft Docs
description: Learn how to manage a conversation between a bot and a user with dialogs in the Bot Builder SDK for Node.js.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 3/1/2018
monikerRange: 'azure-bot-service-4.0'
---

# Manage conversation flow with dialogs
<!----
> > [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-manage-conversation-flow.md)
> - [Node.js](../nodejs/bot-builder-nodejs-dialog-manage-conversation-flow.md)
---->

Managing conversation flow is an essential task in building bots. With the Bot Builder SDK, you can manage conversation flow using **dialogs**.

A dialog is like a function in a program. It is generally designed to perform a specific operation and it can be invoked as often as it is needed. You can chain multiple dialogs together to handle just about any conversation flow that you want your bot to handle. The `botbuilder-dialogs` library in Bot Builder SDK includes built-in features such as **prompts** and **waterfalls** to help you manage conversation flow through dialog. The **prompts** library provide various prompts you can use to ask user for different types of information. The **waterfall** library provide a way for you to combine multiple steps together in a sequence.

This article will show you how to create a `dialogs` object and add prompts and waterfall steps into the `DialogSet` to manage both simple conversation flows and complex conversation flows. 

## Install botbuilder-dialogs library

The `botbuilder-dialogs` library can be downloaded from NPM. To install the `botbuilder-dialogs` library, run the following NPM command:

```cmd
npm install --save botbuilder-dialogs
```

To use **dialogs** in your bot, include it in the bot code. For example:

**app.js**

```javascript
const botbuilder_dialogs = require('botbuilder-dialogs');
```

## Create a dialog stack

To use **dialogs**, you must first create a *dialog set*. The `botbuilder-dialogs` library provides a class call `DialogSet`. The **DialogSet** class defines a **dialog stack** and gives you a simple interface to manage the stack. The Bot Builder SDK implement the stack as an array.

To create a **DialogSet**, do the following:

```javascript
const dialogs = new botbuilder_dialogs.DialogSet();
```

The call above will create a **DialogSet** with a default **dialog stack** name `dialogStack`. If you want to name your stack, you can pass it in as a parameter to **DialogSet()**. For example:

```javascript
const dialogs = new botbuilder_dialogs.DialogSet("myStack");
```

## Create a dialog

The **DialogSet** allows you to create three types of dialogs:
1. A simple dialog where the dialog is defined with only one function.
2. A **prompt** dialog where the dialog uses at least two functions, one to prompt the user for input and the other to process the input. You can string these together using the **waterfall** model.
3. A **waterfall** dialog defines a dialog with an array of functions and the functions are executed sequentially based on the array order.

To create a dialog, use the **add** method. For example, the following code snippet defines a simple dialog named `addTwoNumbers` that belongs to the `dialogs` set:

```javascript
// Show the sum of two numbers.
dialogs.add('addTwoNumbers', async function (dc, numbers){
        var sum = numbers[0] + numbers[1];
        await dc.context.sendActivity(`${numbers[0]} + ${numbers[1]} = ${sum}`);
        await dc.end();
    }
);
```

Creating a dialog only adds the dialog definition to the set, the dialog is not executed until it is pushed onto the stack by calling the **begin()** or **replace()** method. The *dialogId* (e.g.: `addTwoNumers`) must be unique for each dialog set. You can define as many dialogs for each set as necessary.

## Create a dialog with waterfall steps

A conversation consists of a series of messages exchanged between user and bot. When the bot's objective is to lead the user through a series of steps, you can use a **waterfall** model to define the steps of the conversation.

A **waterfall** is a specific implementation of a dialog that is most commonly used to collect information from the user or guide the user through a series of tasks. The tasks are implemented as an array of functions where the result of the first function is passed as arguments into the next function, and so on. Each function typically represents one step in the overall process. At each step, the bot [prompts the user for input](bot-builder-javascript-prompts.md), waits for a response, and then passes the result to the next step.

For example, the following code sample defines three functions in an array that represents the three steps of a **waterfall**:

```javascript
// Greet user:
// Ask for the user name and then greet them by name.
// Ask them where they work.
dialogs.add('greetings',[
    async function (dc){
        await dc.prompt('textPrompt', 'What is your name?');
    },
    async function(dc, results){
        var userName = results;
        await dc.context.sendActivity(`Hello ${userName}!`);
        await dc.prompt('textPrompt', 'Where do you work?');
    },
    async function(dc, results){
        var workPlace = results;
        await dc.context.sendActivity(`${workPlace} is a fun place to work.`);
        await dc.end(); // Ends the dialog
    }
]);

dialogs.add('textPrompt', new botbuilder_dialogs.TextPrompt());
```

The function signature for a **waterfall** step is as follows:

| Type | Value | Description |
| ---- | ----- | ----- |
| Parameters | *context*, <br/>*args* (optional), <br/>*next* (optional) | The *args* parameter contains argument(s) passed into the function. The *next* parameter is a method that allows you to proceed to the next step of the waterfall. The *next* method can optionally accept an *args* argument that allows you to pass argument(s) to the next function. |
| Return | *next()*, <br/>*dialogs.prompt()*, <br/>*dialogs.end()*, <br/>*dialogs.begin()*, <br/>*Promise.resolve()* | Each function must return one of these methods otherwise the bot will be stuck in that function. That is, if a function does not return one of these methods then all user input will cause this function to be re-executed each time the user sent the bot a message. |

When you reached the end of the waterfall, it is best practice to return with the `end()` method so that the dialog can be popped off the stack. See [End a dialog](#end-a-dialog) section for more information. Likewise, to proceed from one step to the next, the waterfall step must end with either a prompt or explicitly call the `next()` function to advance the waterfall. 

## Start a dialog

To start a dialog, pass the *dialogId* you want to start into the `begin()`, `prompt()`, or `replace()` methods. The **begin** method will push the dialog onto the top of the stack while the **replace** method will pop the current dialog off the stack and pushes the replacing dialog onto the stack.

To start a dialog without arguments:

```javascript
// Start the 'greetings' dialog.
await dc.begin('greetings');
```

To start a dialog dialog with arguments:

```javascript
// Start the 'greetings' dialog with the 'userName' passed in. 
await dc.begin('greetings', userName);
```

To start a **prompt** dialog:

```javascript
// Start a 'choicePrompt' dialog with choices passed in as an array of colors to choose from.
await dc.prompt('choicePrompt', `choice: select a color`, ['red', 'green', 'blue']);
```

Depending on the type of prompt you are starting, the prompt's argument signature may be different. The **DialogSet.prompt** method is a helper method. This method takes in arguments and construct the appropriate options for the prompt; then, it calls the **begin** method to start the prompt dialog.

To replace a dialog on the stack:

```javascript
// End the current dialog and start the 'mainMenu' dialog.
await dc.replace('mainMenu'); // Can optionally passed in an 'args' as the second argument.
```

## End a dialog

End a dialog by popping it off the stack and returns an optional result to the parent dialog. The parent dialog will have its **Dialog.resume()** method invoked with any returned result.

It is best practice to explicitly call the `end()` method at the end of the dialog; however, it is not required because the dialog will automatically be popped off the stack for you.

To end a dialog:

```javascript
// End the current dialog by popping it off the stack
await dc.end();
```

To end a dialog with optional argument(s) passed to the parent dialog:

```javascript
// End the current dialog and pass a result to the parent dialog
await dc.end(result);
```

Alternatively, you may also end the dialog by returning a resolved promise:

```javascript
await Promise.resolve();
```

The call to `Promise.resolve()` will result in the dialog ending and popping off the stack. However, this method does not call the parent dialog to resume execution. After the call to `Promise.resolve()`, execution stops. The bot will resume where the parent dialog left off when the user send the bot a message. This may not be the ideal user experience to end a dialog. Consider ending a dialog with either `end()` or `replace()` so your bot can continue interacting with the user.

## Clear the dialog stack

If you want to pop all dialogs off the stack, you can clear the dialog stack by calling the `dc.endAll()` method.

```javascript
// Pop all dialogs from the current stack.
await dc.endAll();
```

## Repeat a dialog

To repeat a dialog, use the `dialogs.replace()` method.

```javascript
// End the current dialog and start the 'mainMenu' dialog.
await dc.replace('mainMenu'); 
```

If you want to show the main menu by default, you can create a `mainMenu` dialog with the following steps:

```javascript
// Display a menu and ask user to choose a menu item. Direct user to the item selected.
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
        else {
            // Repeat the menu
            await dc.replace('mainMenu');
        }
    },
    async function(dc, result){
        // Start over
        await dc.endAll().begin('mainMenu');
    }
]);

dialogs.add('choicePrompt', new botbuilder_dialogs.ChoicePrompt());
```

This dialog uses a `ChoicePrompt` to display the menu and waits for the user to choose an option. When the user choose either `Order Dinner` or `Reserve a table`, it starts the dialog for the appropriate choice and when that task is done, instead of just ending the dialog in the last step, this dialog repeat itself.

## Dialog loops

Another way to use the `replace()` method is by emulating loops. Take this scenario for example. If you want to allow the user to add multiple menu item to a cart, you can loop the menu choices until the user is done ordering.

```javascript
// Order dinner:
// Help user order dinner from a menu

var dinnerMenu = {
    choices: ["Potato Salad - $5.99", "Tuna Sandwich - $6.89", "Clam Chowder - $4.50", 
        "More info", "Process order", "Cancel", "Help"],
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

// The order cart
var orderCart = {
    orders: [],
    total: 0,
    clear: function(dc) {
        this.orders = [];
        this.total = 0;
        dc.context.activity.conversation.orderCart = null;
    }
};

dialogs.add('orderDinner', [
    async function (dc){
        await dc.context.sendActivity("Welcome to our Dinner order service.");
        orderCart.clear(dc); // Clears the cart.

        await dc.begin('orderPrompt'); // Prompt for orders
    },
    async function (dc, result) {
        if(result == "Cancel"){
            //return Promise.resolve();
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

// Define prompts
// Generic prompts
dialogs.add('textPrompt', new botbuilder_dialogs.TextPrompt());
dialogs.add('numberPrompt', new botbuilder_dialogs.NumberPrompt());
dialogs.add('dateTimePrompt', new botbuilder_dialogs.DatetimePrompt());
dialogs.add('choicePrompt', new botbuilder_dialogs.ChoicePrompt());

```

> [!NOTE]
> To get the full source code used in this section, see [C#](#) or [JavaScript](#).

The sample code above shows that the main `orderDinner` dialog uses a helper dialog named `orderPrompt` to handle user choices. The `orderPrompt` dialog displays the menu, ask the user to choose an item, add the item to cart and prompt again. This allows the user to add multiple items to their order. The dialog loops until the user chooses `Process order` or `Cancel`. At which point, execution is handed back to the parent dialog (e.g.: `orderDinner`). The `orderDinner` dialog does some last minute house keeping if the user wants to process the order otherwise it ends and return execution back to its parent dialog (e.g.: `mainMenu`). The `mainMenu` dialog in turn continue executing the last step which is to simply redisplay the main menu choices.

## Next steps

Now that you learn how to use **dialogs**, **prompts**, and **waterfall** to manage conversation flow, let's take a look at how we can break our dialogs into modular tasks instead of lumping them all together in the main bot logic's `dialogs` object.

> [!div class="nextstepaction"]
> [Create modular bot logic with Composite Control](bot-builder-javascript-compositcontrol.md)
