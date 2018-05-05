---
title: Tutorial - Save user state data | Microsoft Docs
description: Learn how to save user state data in the Bot Builder SDK.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 4/23/2018
monikerRange: 'azure-bot-service-4.0'
---

# Save user state data

When the bot is asking users for input, chances are that you would want to persist some of the information to storage of some form. The Bot Builder SDK allows you to store user inputs using *in-memory storage*, *file storage*, database storage such as *CosmosDB* or *SQL*. 

This tutorial will show you how to define your storage object in the middleware layer and how to save user into to the storage object so that it can be persisted.

## Prequisite 

This tutorial builds on the [Manage a conversation flow with waterfall](bot-builder-tutorial-waterfall.md) tutorial.

## Add storage to middleware layer


## Save user input to storage

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

??? 

> [!div class="nextstepaction"]
> [Save user state data](bot-builder-tutorial-save-data.md)
