---
title: Tutorial - Prompt user for multiple inputs using waterfall model | Microsoft Docs
description: Learn how to use the waterfall model to ask user for multiple inputs in the Bot Builder SDK.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 4/23/2018
monikerRange: 'azure-bot-service-4.0'
---

# Manage a conversation flow with waterfall

Imagine you are running a hotel and you want to build a bot that can help your guests reserve a table in your hotel restaurant. This tutorial will show you how to create a bot that prompt the user for the necessary information before reserving a table.

## Prequisite 

This tutorial builds on the [Prompt user for input](bot-builder-tutorial-prompts.md) tutorial where you learn how to ask user a question. In this tutorial, you will enhance that bot by creating a new **waterfall** dialog that asks the user for three pieces of information.

## Define a waterfall dialog

To manage the table reservation conversaion, you will need to define a **waterfall** dialog with four steps. In this conversation, you will also be using a `DatetimePrompt` and `NumberPrompt` in additional to the `TextPrompt`.

The `reserveTable` dialog will look like this:

```javascript
// Reserve a table:
// Help the user to reserve a table

var reservationInfo = {
    dateTime: '',
    partySize: '',
    reserveName: ''
}

dialogs.add('reserveTable', [
    async function(dc, args, next){
        await dc.context.sendActivity("Welcome to the reservation service.");
        reservationInfo = {}; // Clears any previous data
        await dc.prompt('dateTimePrompt', "Please provide a reservation date and time.");
    },
    async function(dc, result){
        reservationInfo.dateTime = result[0].value;

        // Ask for next info
        await dc.prompt('partySizePrompt', "How many people are in your party?");
    },
    async function(dc, result){
        reservationInfo.partySize = result;

        // Ask for next info
        await dc.prompt('textPrompt', "Who's name will this be under?");
    },
    async function(dc, result){
        reservationInfo.reserveName = result;

        // Confirm reservation
        var msg = `Reservation confirmed. Reservation details: 
            <br/>Date/Time: ${reservationInfo.dateTime} 
            <br/>Party size: ${reservationInfo.partySize} 
            <br/>Reservation name: ${reservationInfo.reserveName}`;
        await dc.context.sendActivity(msg);
        await dc.end();
    }
]);

```

The conversation flow of the `reserveTable` will ask the user 3 questions. Question 1 is asked in the first step. The user's response will be given in the `result` parameter of the second step. The second step saves the input and immediately ask the user the second question. The user's response will be given to the `result` parameter of the third step. The third step saves the user's input and, again, it ask the user for the third piece of information. In the fourth step, the bot saves the response and send the user a confirmation of their reservation information.

For this waterfall to work, you will also need to add the prompts to the `dialogs` object:

```javascript
// Define prompts
// Generic prompts
dialogs.add('textPrompt', new botbuilder_dialogs.TextPrompt());
dialogs.add('dateTimePrompt', new botbuilder_dialogs.DatetimePrompt());
dialogs.add('partySizePrompt', new botbuilder_dialogs.NumberPrompt());
```

Now, you are ready to hook this into the bot logic.

## Start the dialog

Modify your bot's `processActivity()` method to look like this:

```javascript
// Listen for incoming activity 
server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (context) => {
        if (context.activity.type === 'message') {
            // State will store all of your information 
            const state = conversationState.get(context);
            const dc = dialogs.createContext(context, state);

            if(context.activity.text.match(/hi/ig)){
                await dc.begin('greeting');
            }
            if(context.activity.text.match(/reserve table/ig)){
                await dc.begin('reserveTable');
            }
            else{
                // Continue executing the "current" dialog, if any.
                await dc.continue();
            }
        }
    });
});
```

At execution time, whenever the user sends the message containing the string `reserve table`, the bot will start the `reserveTable` conversation.

## Next steps

In this tutorial, the bot is saving the user's input to a global variable call `reservationInfo`. If you want to store or persist this information, you need to add state to the middleware layer. Lets take a closer look at how to persist user state data in the next tutorial. 

> [!div class="nextstepaction"]
> [Save user state data](bot-builder-tutorial-save-data.md)
