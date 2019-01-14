---
title: Replace dialogs | Microsoft Docs
description: Learn how to replace dialogs to re-prompt for input and manage conversation flow using the Bot Framework SDK for Node.js.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Replace dialogs

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

The ability to replace a dialog can be useful when you need to validate user input or repeat an action during the course of a conversation. With the Bot Framework SDK for Node.js, you can replace a dialog by using the [`session.replaceDialog`](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#replacedialog) method. This method enables you to end the current dialog and replace it with a new dialog without returning to the caller. 

## Create custom prompts to validate input

The Bot Framework SDK for Node.js includes input validation for some types of [prompts](bot-builder-nodejs-dialog-prompt.md) such as `Prompts.time` and `Prompts.choice`. To validate text input that you receive in response to `Prompts.text`, you must create your own validation logic and custom prompts. 

You may want to validate an input if the input must comply with a certain value, pattern, range, or criteria that you define. If an input fails validation, the bot can prompt the user for that information again by using the `session.replaceDialog` method.

The following code sample shows how to create a custom prompt to validate user input for a phone number.

```javascript
// This dialog prompts the user for a phone number. 
// It will re-prompt the user if the input does not match a pattern for phone number.
bot.dialog('phonePrompt', [
    function (session, args) {
        if (args && args.reprompt) {
            builder.Prompts.text(session, "Enter the number using a format of either: '(555) 123-4567' or '555-123-4567' or '5551234567'")
        } else {
            builder.Prompts.text(session, "What's your phone number?");
        }
    },
    function (session, results) {
        var matched = results.response.match(/\d+/g);
        var number = matched ? matched.join('') : '';
        if (number.length == 10 || number.length == 11) {
            session.userData.phoneNumber = number; // Save the number.
            session.endDialogWithResult({ response: number });
        } else {
            // Repeat the dialog
            session.replaceDialog('phonePrompt', { reprompt: true });
        }
    }
]);
```

In this example, the user is initially prompted to provide their phone number. The validation logic uses a regular expression that matches a range of digits from the user input. If the input contains 10 or 11 digits, then that number is returned in the response. Otherwise, the `session.replaceDialog` method is executed to repeat the `phonePrompt` dialog, which prompts the user for input again, this time providing more specific guidance regarding the format of input that is expected.

When you call the `session.replaceDialog` method, you specify the name of the dialog to repeat and an arguments list. In this example, the arguments list contains `{ reprompt: true }`, which enables the bot to provide different prompt messages depending on whether it is an initial prompt or a reprompt, but you can specify whatever arguments your bot may require. 

## Repeat an action

There may be times in the course of a conversation where you want to repeat a dialog to enable the user to complete a certain action multiple times. For example, if your bot offers a variety of services, you might initially display the menu of services, walk the user through the process of requesting a service, and then display the menu of services again, thereby enabling the user to request another service. To achieve this, you can use the [`session.replaceDialog`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#replacedialog) method to display the menu of services again, rather than ending the conversation with the ['session.endConversation`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#endconversation) method. 

The following example shows how to use the `session.replaceDialog` method to implement this type of scenario. First, the menu of services is defined:

```javascript
// Main menu
var menuItems = { 
    "Order dinner": {
        item: "orderDinner"
    },
    "Dinner reservation": {
        item: "dinnerReservation"
    },
    "Schedule shuttle": {
        item: "scheduleShuttle"
    },
    "Request wake-up call": {
        item: "wakeupCall"
    },
}
```

The `mainMenu` dialog is invoked by the default dialog, so the menu will be presented to the user at the beginning of the conversation. Additionally, a [`triggerAction`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialog.html#triggeraction) is attached to the `mainMenu` dialog so that the menu will also be presented any time the user input is "main menu". When the user is presented with the menu and selects an option, the dialog that corresponds to the user's selection is invoked by using the `session.beginDialog` method.

```javascript
var inMemoryStorage = new builder.MemoryBotStorage();

// This is a reservation bot that has a menu of offerings.
var bot = new builder.UniversalBot(connector, [
    function(session){
        session.send("Welcome to Contoso Hotel and Resort.");
        session.beginDialog("mainMenu");
    }
]).set('storage', inMemoryStorage); // Register in-memory storage 

// Display the main menu and start a new request depending on user input.
bot.dialog("mainMenu", [
    function(session){
        builder.Prompts.choice(session, "Main Menu:", menuItems);
    },
    function(session, results){
        if(results.response){
            session.beginDialog(menuItems[results.response.entity].item);
        }
    }
])
.triggerAction({
    // The user can request this at any time.
    // Once triggered, it clears the stack and prompts the main menu again.
    matches: /^main menu$/i,
    confirmPrompt: "This will cancel your request. Are you sure?"
});
```

In this example, if the user chooses option 1 to order dinner to be delivered to their room, the `orderDinner` dialog will be invoked and will walk the user through the process of ordering dinner. At the end of the process, the bot will confirm the order and display the main menu again by using the `session.replaceDialog` method.

```javascript
// Menu: "Order dinner"
// This dialog allows user to order dinner to be delivered to their hotel room.
bot.dialog('orderDinner', [
    function(session){
        session.send("Lets order some dinner!");
        builder.Prompts.choice(session, "Dinner menu:", dinnerMenu);
    },
    function (session, results) {
        if (results.response) {
            var order = dinnerMenu[results.response.entity];
            var msg = `You ordered: %(Description)s for a total of $${order.Price}.`;
            session.dialogData.order = order;
            session.send(msg);
            builder.Prompts.text(session, "What is your room number?");
        } 
    },
    function(session, results){
        if(results.response){
            session.dialogData.room = results.response;
            var msg = `Thank you. Your order will be delivered to room #${results.response}.`;
            session.send(msg);
            session.replaceDialog("mainMenu"); // Display the menu again.
        }
    }
])
.reloadAction(
    "restartOrderDinner", "Ok. Let's start over.",
    {
        matches: /^start over$/i
    }
)
.cancelAction(
    "cancelOrder", "Type 'Main Menu' to continue.", 
    {
        matches: /^cancel$/i,
        confirmPrompt: "This will cancel your order. Are you sure?"
    }
);
```

Two triggers are attached to the `orderDinner` dialog to enable the user to "start over" or "cancel" the order at any time during the ordering process. 

The first trigger is [`reloadAction`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialog.html#reloadaction), which allows the user to start the order process over again by sending the input "start over". When the trigger matches the utterance "start over", the `reloadAction` restarts the dialog from the beginning. 

The second trigger is [`cancelAction`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialog.html#cancelaction), which allows the user to abort the order process completely by sending the input "cancel". This trigger does not automatically display the main menu again, but instead sends a message that tells the user what to do next by saying "Type 'Main Menu' to continue."

### Dialog loops

In the example above, the user can only select one item per order. That is, if the user wanted to order two items from the menu, they would have to complete the entire ordering process for the first item and then repeat the entire ordering process again for the second item. 

The following example shows how to improve upon the previous bot by refactoring the dinner menu into a separate dialog. Doing so enables the bot to repeat the dinner menu in a loop and therefore allows the user to select multiple items within a single order.

First, add a "Check out" option to the menu. This option will allow the user to exit the item selection process and continue with the check out process.

```javascript
// The dinner menu
var dinnerMenu = { 
    //...other menu items...,
    "Check out": {
        Description: "Check out",
        Price: 0 // Order total. Updated as items are added to order.
    }
};
```

Next, refactor the order prompt into its own dialog so that the bot can repeat the menu and allow user to add multiple items to their order.

```javascript
// Add dinner items to the list by repeating this dialog until the user says `check out`. 
bot.dialog("addDinnerItem", [
    function(session, args){
        if(args && args.reprompt){
            session.send("What else would you like to have for dinner tonight?");
        }
        else{
            // New order
            // Using the conversationData to store the orders
            session.conversationData.orders = new Array();
            session.conversationData.orders.push({ 
                Description: "Check out",
                Price: 0
            })
        }
        builder.Prompts.choice(session, "Dinner menu:", dinnerMenu);
    },
    function(session, results){
        if(results.response){
            if(results.response.entity.match(/^check out$/i)){
                session.endDialog("Checking out...");
            }
            else {
                var order = dinnerMenu[results.response.entity];
                session.conversationData.orders[0].Price += order.Price; // Add to total.
                var msg = `You ordered: ${order.Description} for a total of $${order.Price}.`;
                session.send(msg);
                session.conversationData.orders.push(order);
                session.replaceDialog("addDinnerItem", { reprompt: true }); // Repeat dinner menu
            }
        }
    }
])
.reloadAction(
    "restartOrderDinner", "Ok. Let's start over.",
    {
        matches: /^start over$/i
    }
);
```

In this example, orders are stored in a bot data store that is scoped to the current conversation,  `session.conversationData.orders`. For every new order, the variable is re-initialized with a new array and for every item the user chooses, the bot adds that item to the `orders` array and adds the price to the total, which is stored in the checkout's `Price` variable. When the user finishes selecting items for their order, they can say "check out" and then continue with the remainder of the order process.

> [!NOTE]
> For more information about bot data storage, see [Manage state data](bot-builder-nodejs-state.md). 

Finally, update the second step of the waterfall within the `orderDinner` dialog to process the order with confirmation. 

```javascript
// Menu: "Order dinner"
// This dialog allows user to order dinner and have it delivered to their room.
bot.dialog('orderDinner', [
    function(session){
        session.send("Lets order some dinner!");
        session.beginDialog("addDinnerItem");
    },
    function (session, results) {
        if (results.response) {
            // Display itemize order with price total.
            for(var i = 1; i < session.conversationData.orders.length; i++){
                session.send(`You ordered: ${session.conversationData.orders[i].Description} for a total of $${session.conversationData.orders[i].Price}.`);
            }
            session.send(`Your total is: $${session.conversationData.orders[0].Price}`);

            // Continue with the check out process.
            builder.Prompts.text(session, "What is your room number?");
        } 
    },
    function(session, results){
        if(results.response){
            session.dialogData.room = results.response;
            var msg = `Thank you. Your order will be delivered to room #${results.response}`;
            session.send(msg);
            session.replaceDialog("mainMenu");
        }
    }
])
//...attached triggers...
;
```

## Cancel a dialog

While the `session.replaceDialog` method can be used to replace the *current* dialog with a new one, it cannot be used to replace a dialog that is located further down the dialog stack. To replace a dialog within the dialog stack that is not the *current* dialog, use the [`session.cancelDialog`](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#canceldialog) method instead. 

The `session.cancelDialog` method can be used to end a dialog regardless of where it exists in the dialog stack and optionally invoke a new dialog in its place. To call the `session.cancelDialog` method, specify the ID of the dialog to cancel and optionally, specify the ID of the dialog to invoke in its place. For example, this code snippet cancels the `orderDinner` dialog and replaces it with the `mainMenu` dialog:

```javascript
session.cancelDialog('orderDinner', 'mainMenu'); 
```

When the `session.cancelDialog` method is called, the dialog stack will be searched backwards and the first occurrence of that dialog will be canceled, causing that dialog and its child dialogs (if any) to be removed from the dialog stack. Control will then be returned to the calling dialog, which can check for a [`results.resumed`](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptresult.html#resumed) code equal to [`ResumeReason.notCompleted`](http://docs.botframework.com/en-us/node/builder/chat-reference/enums/_botbuilder_d_.resumereason.html#notcompleted) to detect the cancellation.

As an alternative to specifying the ID of the dialog to cancel when you call the `session.cancelDialog` method, you can instead specify the zero-based index of the dialog to cancel, where the index represents the dialog's position in the dialog stack. For example, the following code snippet terminates the currently active dialog (index = 0) and starts the `mainMenu` dialog in its place. The `mainMenu` dialog is invoked at position 0 of the dialog stack, thereby becoming the new default dialog.

```javascript
session.cancelDialog(0, 'mainMenu');
```

Consider the sample that is discussed in [dialog loops](#dialog-loops) above. When the user reaches the item selection menu, that dialog (`addDinnerItem`) is the fourth dialog in the dialog stack: `[default dialog, mainMenu, orderDinner, addDinnerItem]`. How can you enable the user to cancel their order from within the `addDinnerItem` dialog? If you attach a `cancelAction` trigger to the `addDinnerItem` dialog, it will only return the user back to the previous dialog (`orderDinner`), which will send the user right back into the `addDinnerItem` dialog.

This is where the `session.cancelDialog` method is useful. Starting with the [dialog loops example](#dialog-loops), add "Cancel order" as an explicit option within the dinner menu.

```javascript
// The dinner menu
var dinnerMenu = { 
    //...other menu items...,
    "Check out": {
        Description: "Check out",
        Price: 0      // Order total. Updated as items are added to order.
    },
    "Cancel order": { // Cancel the order and back to Main Menu
        Description: "Cancel order",
        Price: 0
    }
};
```

Then, update the `addDinnerItem` dialog to check for a "cancel order" request. If "cancel" is detected, use the `session.cancelDialog` method to cancel the default dialog (i.e., the dialog at index 0 of the stack) and invoke the `mainMenu` dialog in its place. 

```javascript
// Add dinner items to the list by repeating this dialog until the user says `check out`. 
bot.dialog("addDinnerItem", [
    //...waterfall steps...,
    // Last step
    function(session, results){
        if(results.response){
            if(results.response.entity.match(/^check out$/i)){
                session.endDialog("Checking out...");
            }
            else if(results.response.entity.match(/^cancel/i)){
                // Cancel the order and start "mainMenu" dialog.
                session.cancelDialog(0, "mainMenu");
            }
            else {
                //...add item to list and prompt again...
                session.replaceDialog("addDinnerItem", { reprompt: true }); // Repeat dinner menu.
            }
        }
    }
])
//...attached triggers...
;
```

By using the `session.cancelDialog` method in this way, you can implement whatever conversation flow your bot requires.

## Next steps

As you can see, to replace dialogs on the stack, you use various types of **actions** to accomplish that task. Actions gives you great flexibilities in managing conversation flow. Let's take a closer look at **actions** and how to better handle user actions.

> [!div class="nextstepaction"]
> [Handle user actions](bot-builder-nodejs-dialog-actions.md)
