---
title: Triggering actions | Microsoft Docs
description: Learn how to implement trigger actions using global message handlers by using the Bot Builder SDK for Node.js.
keywords: Bot Framework, trigger actions, node.js, Bot Builder, SDK, global handler, global message handler, message handler
author: kbrandl
manager: rstand
ms.topic: develop-nodejs-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/21/2017
ms.reviewer:
#ROBOTS: Index
---

# Triggering actions


[!include[Introduction to global message handlers](~/includes/snippet-global-handlers-intro.md)]
This article describes how to implement actions, which are global message handlers. 

## Triggering a help dialog

With the Bot Builder SDK for Node.js, you can use `triggerAction` to specify the triggers that will cause the 
invokation of specific dialogs. 


For example, the `triggerAction` in the following code sample invokes the **help** dialog 
if the user's message is "help", thereby adding the **help** dialog to the top of the dialog stack and 
putting it in control of the conversation. When the **help** dialog completes, it will be removed from 
the dialog stack and the dialog from which the user sent the message "help" will regain control of the conversation.

```javascript
bot.dialog('help', (session, args, next) => {
    // Send message to the user and end this dialog
    session.endDialog('This is a simple bot that collects a name and age.');
}).triggerAction({
    matches: /^help$/,
    onSelectAction: (session, args, next) => {
        // Add the help dialog to the dialog stack 
        // (override the default behavior of replacing the stack)
        session.beginDialog(args.action, args);
    }
});
```

## Trigger a cancel action

Another type of action that can triggered globally is cancellation. In this example, the bot uses a **cancelAction** to end the conversation if the user's message is "cancel".

<!--
```javascript
bot.dialog('cancel', (session, args, next) => {
    session.endConversation('Operation canceled');
}).triggerAction({
    matches: /^cancel$/
});
```
--> 

```javascript
// Add dialog for creating a list
bot.dialog('listBuilderDialog', function (session) {
    if (!session.dialogData.list) {
        // Start a new list 
        session.dialogData.list = [];
        session.send("Each message will added as a new item to the list.\nSay 'end list' when finished or 'cancel' to discard the list.\n")
    } else if (/end.*list/i.test(session.message.text)) {
        // Return current list
        session.endDialogWithResult({ response: session.dialogData.list });
    } else {
        // Add item to list and save() change to dialogData
        session.dialogData.list.push(session.message.text);
        session.save();
    }
}).cancelAction('cancelList', "List canceled", { 
    matches: /^cancel/i,
    confirmPrompt: "Are you sure?"
});
```


## Confirming interruptions

By default, when the user says something that triggers a dialog it will automatically interrupt any dialogs that are active. 
In most cases you’ll find that this is desirable, but they’re may be times where you’re doing something large or complex and you’d like to just confirm that 
canceling the active dialog is what the user wants to do.
This can be easily accomplished by adding a [confirmPrompt](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.itriggeractionoptions#confirmprompt) 
option to your triggerAction() or cancelAction(). 

```javascript
// Add dialog to handle 'Buy' button click
bot.dialog('buyButtonClick', [ ... waterfall steps ... ])
    .triggerAction({ 
        matches: /(buy|add)\s.*shirt/i
        confirmPrompt: "This will cancel adding the current item. Are you sure?" 
    })
    .cancelAction('cancelBuy', "Ok... Item canceled", { 
        matches: /^cancel/i,
        confirmPrompt: "are you sure?" 
    });

```


The cancel action's confirmPrompt will be used anytime the user says “cancel” and the trigger action's confirmPrompt will be used anytime another dialog 
is triggered (including the ‘buyButtonClick’ dialog itself) and attempts to interrupt the dialog. 

## Trigger the reload of a dialog

The [reloadAction][reloadAction] handler binds an action to a dialog that causes the dialog to reload when the action is triggered. This is useful for implementing logic to handle user utterances like "start over".

```javascript
// Add dialog to handle 'Buy' button click
bot.dialog('buyButtonClick', [ ... waterfall steps ... ])
    .triggerAction({ 
        matches: /(buy|add)\s.*shirt/i
        confirmPrompt: "This will cancel adding the current item. Are you sure?" 
    })
    .cancelAction('cancelBuy', "Ok... Item canceled", { 
        matches: /^cancel/i,
        confirmPrompt: "are you sure?" 
    })
    .reloadAction('reloadBuy', "Restarting order.", { 
        matches: /^start over/i,
        confirmPrompt: "are you sure?" 
    });

```
## Start another dialog

From within the scope of a dialog, you can bind an action to the dialog that will start another dialog when it is triggered. The new dialog is pushed onto the stack so it does not automatically end the current task. The current task is continued once the new dialog ends. The built-in prompts will automatically re-prompt the user once this happens but that behavior can be disabled by setting the **promptAfterAction** flag when calling a built-in prompt.

The following example demonstrates how to use **beginDialogAction** to create actions that are
only in scope when a particular dialog is on the stack. The '/orderPizza' adds
actions that let the user view their cart and checkout but those actions can 
only be taken while the user is actually ordering a pizza.

<!-- 
This sample also shows how support multi-level cancel within a bot. When 
ordering a pizza you can cancel either an item you're adding or the entire 
order.  The user can say "cancel order" at anytime to cancel the order but 
saying just "cancel" will intelligently cancel either the current item being 
added or the order depending on where the user is in the flow. -->

```javascript
// Add dialog to manage ordering a pizza
bot.dialog('orderPizzaDialog', [
    function (session, args) {
        if (!args.continueOrder) {
            session.userData.cart = [];
            session.send("At anytime you can say 'cancel order', 'view cart', or 'checkout'.")
        }
        builder.Prompts.choice(session, "What would you like to add?", "Pizza|Drinks|Extras");
    },
    function (session, results) {
        session.beginDialog('add' + results.response.entity);
    },
    function (session, results) {
        if (results.response) {
            session.userData.cart.push(results.response);
        }
        session.replaceDialog('orderPizzaDialog', { continueOrder: true });
    }
]).triggerAction({ 
        matches: /order.*pizza/i,
        confirmPrompt: "This will cancel the current order. Are you sure?"
  })
  .cancelAction('cancelOrderAction', "Order canceled.", { 
      matches: /(cancel.*order|^cancel)/i,
      confirmPrompt: "Are you sure?"
  })
  .beginDialogAction('viewCartAction', 'viewCartDialog', { matches: /view.*cart/i })
  .beginDialogAction('checkoutAction', 'checkoutDialog', { matches: /checkout/i });

// Dialog for showing the users cart
bot.dialog('viewCartDialog', function (session) {
    var msg;
    var cart = session.userData.cart;
    if (cart.length > 0) {
        msg = "Items in your cart:";
        for (var i = 0; i < cart.length; i++) {
            msg += "\n* " + cart[i];
        }
    } else {
        msg = "Your cart is empty.";
    }
    session.endDialog(msg);
});

// Dialog for checking out
bot.dialog('checkoutDialog', function (session) {
    var msg;
    var cart = session.userData.cart;
    if (cart.length > 0) {
        msg = "Your order is on its way.";
    } else {
        msg = "Your cart is empty.";
    }
    delete session.userData.cart;
    session.endConversation(msg);
});
```

<!--
View the "feature-onDisambiguateRoute" example to see how you'd prompt the user
to disambiguate between "cancel item" and "cancel order".  
-->


## Additional resources

- [Designing conversation flow](~/design/core-dialogs.md)
- [Bot capabilities](~/design/capabilities.md)

<!--
- [reloadAction][reloadAction]
-->

[matches]: (https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.idialogactionoptions#matches)
[reloadAction]: (https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialog.html#reloadaction)