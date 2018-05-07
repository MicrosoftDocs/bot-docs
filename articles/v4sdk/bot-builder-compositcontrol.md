---
title: Create modular bot logic using dialog container | Microsoft Docs
description: Learn how to modularize your bot logic using dialog container in the Bot Builder SDK for Node.js and C#.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 4/27/2018
monikerRange: 'azure-bot-service-4.0'
---

# Create modular bot logic with Dialog Container

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

Imagine that you are creating a hotel bot that handles multiple tasks such as greeting the user, reserving a dinner table, ordering food, setting an alarm, displaying the current weather and many others. You can handle each of these tasks within your bot using one dialog object but this can make your dialog code too large and cluttered in your bot's main file. The best way to tackle this issue is through modularization. Modularization will streamline your code and make it easier to debug. Additionally, you can break it up into sections allowing multiple teams to simultaneously work on the bot. We can create a bot that manages multiple conversation flows by breaking them up into components using Dialog Container. We will create a few basic conversation flows and show how they can be combined together using Dialog Container.

In this example we will be creating a hotel bot that combines a check in, wake-up, and reserve table modules.

## Define a modular class

First, we start with a simple check in dialog that will ask the user for their name and what room they will be staying in. To modularize this task, we create a `CheckIn` class that extends `DialogContainer`. This class has a constructor that defines the root dialog as `checkIn`. The `checkIn` dialog is defined as a *waterfall* with three steps. The signature and construction of the dialog object is exactly the same as a standard waterfall. For more information about dialogs and waterfall, see [Use dialogs to manage conversation flow](bot-builder-dialog-manage-conversation-flow.md).


To keep track of the user's input, a `userState` object is passed in as a dialog parameter from the parent. In the `CheckIn` class the dialog is built into the constructor which allows you to save information to `userState`. Throughout this dialog we can write to a locale state object defined here as `dc.activeDialog.state.guestInfo` as the user inputs information. After this dialog is complete the locale state object will be disposed of. Therefore, we save the locale state object to the parent's `userState` that will persist information about the user across all the conversations you have with the user. For more information about state management, see [Save state using conversation and user properties](bot-builder-howto-v4-state.md). 

**checkIn.js**
```JavaScript
const { DialogContainer, DialogSet, TextPrompt, NumberPrompt } = require('botbuilder-dialogs');

class CheckIn extends DialogContainer {
    constructor(userState) {
        // Dialog ID of 'checkIn' will start when class is called in the parent
        super('checkIn');

        // Defining the conversation flow using a waterfall model
        this.dialogs.add('checkIn', [
            async function (dc) {
                // Create a new local guestInfo state object
                dc.activeDialog.state.guestInfo = {};
                await dc.context.sendActivity("What is your name?");
            },
            async function (dc, name){
                // Save the name 
                dc.activeDialog.state.guestInfo.userName = name;
                await dc.prompt('numberPrompt', `Hi ${name}. What room will you be staying in?`);
            },
            async function (dc, room){
                // Save the room number
                dc.activeDialog.state.guestInfo.room = room
                await dc.context.sendActivity(`Great! Enjoy your stay!`);

                // Save dialog's state object to the parent's state object
                const user = userState.get(dc.context);
                user.guestInfo = dc.activeDialog.state.guestInfo;
                await dc.end();
            }
        ]);
        // Defining the prompt used in this conversation flow
        this.dialogs.add('textPrompt', new TextPrompt());
        this.dialogs.add('numberPrompt', new NumberPrompt());
    }
}
exports.CheckIn = CheckIn;
```

## Define multiple modular classes

One major benefit of using Dialog Container is the ability to combine multiple dialogs together. Since each `DialogSet` maintains an exclusive set of `dialogs`, sharing or cross referencing `dialogs` cannot be done easily. This is where Dialog Container comes in. A Dialog Container will merge all the individual dialogs into one master dialog that makes cross referencing possible. To illustrate that, let's create two more dialogs that will ask the user which table they would like to reserve for dinner as well as create a wake up call. These dialogs will once again be defined in a new class that extends from Dialog Container and starts their root dialogs when their class is called. 

**reserveTable.js**
```JavaScript
const { DialogContainer, DialogSet, ChoicePrompt } = require('botbuilder-dialogs');

class ReserveTable extends DialogContainer {
    constructor(userState) {
        // Dialog ID of 'reserve_table' will start when class is called in the parent
        super('reserve_table'); 

        // Defining the conversation flow using a waterfall model
        this.dialogs.add('reserve_table', [
            async function (dc, args) {
                // Get the user state from context
                const user = userState.get(dc.context);

                // Create a new local reserveTable state object
                dc.activeDialog.state.reserveTable = {};

                const prompt = `Welcome ${user.guestInfo.userName}, which table would you like to reserve?`;
                const choices = ['1', '2', '3', '4', '5', '6'];
                await dc.prompt('choicePrompt', prompt, choices);
            },
            async function(dc, choice){
                // Save the table number
                dc.activeDialog.state.reserveTable.tableNumber = choice.value;
                await dc.context.sendActivity(`Sounds great, we will reserve table number ${choice.value} for you.`);
                
                // Get the user state from context
                const user = userState.get(dc.context);
                // Persist dialog's state object to the parent's state object
                user.reserveTable = dc.activeDialog.state.reserveTable;

                // End the dialog
                await dc.end();
            }
        ]);

        // Defining the prompt used in this conversation flow
        this.dialogs.add('choicePrompt', new ChoicePrompt());
    }
}
exports.ReserveTable = ReserveTable;
```

**wakeUp.js**
```JavaScript
const { DialogContainer, DialogSet, DatetimePrompt } = require('botbuilder-dialogs');

class WakeUp extends DialogContainer {
    constructor(userState) {
        // Dialog ID of 'wakeup' will start when class is called in the parent
        super('wakeUp');

        this.dialogs.add('wakeUp', [
            async function (dc, args) {
                // Get the user state from context
                const user = userState.get(dc.context); 

                // Create a new local reserveTable state object
                dc.activeDialog.state.wakeUp = {};  
                             
                await dc.prompt('datePrompt', `Hello, ${user.guestInfo.userName}. What time would you like your alarm to be set?`);
            },
            async function (dc, time){
                // Get the user state from context
                const user = userState.get(dc.context);

                // Save the time
                dc.activeDialog.state.wakeUp.time = time[0].value

                await dc.context.sendActivity(`Your alarm is set to ${time[0].value} for room ${user.guestInfo.room}`);
                
                // Save dialog's state object to the parent's state object
                user.wakeUp = dc.activeDialog.state.wakeUp;

                // End the dialog
                await dc.end();
            }]);

        // Defining the prompt used in this conversation flow
        this.dialogs.add('datePrompt', new DatetimePrompt());
    }
}
exports.WakeUp = WakeUp;
```

## Add modules to bot

Below is the main file that ties these three modularized dialogs into one bot.

The flow of the bot will be tracked by `dc.continue()`. The `dc.continue()` method will continue to the next step of the waterfall on the dialog stack.

**app.js**
```JavaScript
const {BotFrameworkAdapter, FileStorage, ConversationState, UserState, BotStateSet, MessageFactory} = require("botbuilder");
const {DialogSet} = require("botbuilder-dialogs");
const restify = require("restify");
var azure = require('botbuilder-azure'); 

// Create server
let server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log(`${server.name} listening to ${server.url}`);
});

// Create adapter
const adapter = new BotFrameworkAdapter({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

//Memory Storage
const storage = new FileStorage("c:/temp");
// ConversationState lasts for the entirety of a conversation then gets disposed of
const convoState = new ConversationState(storage);

// UserState persists information about the user across all of the conversations you have with that user
const userState  = new UserState(storage);

adapter.use(new BotStateSet(convoState, userState));

// Listen for incoming requests 
server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (context) => {
        const isMessage = context.activity.type === 'message';

        // State will store all of your information 
        const convo = convoState.get(context);
        const user = userState.get(context); // userState will not be used in this example

        const dc = dialogs.createContext(context, convo);
        // Continue the current dialog if one is currently active
        await dc.continue(); 

        // Default action
        if (!context.responded && isMessage) {

            // Getting the user info from the state
            const userinfo = userState.get(dc.context); 
            // If guest info is undefined prompt the user to check in
            if(!userinfo.guestInfo){
                await dc.begin('checkInPrompt');
            }else{
                await dc.begin('mainMenu'); 
            }           
        }
    });
});

const dialogs = new DialogSet();
dialogs.add('mainMenu', [
    async function (dc, args) {
        const menu = ["Reserve Table", "Wake Up"];
        await dc.context.sendActivity(MessageFactory.suggestedActions(menu));    
    },
    async function (dc, result){
        // Decide which module to start
        switch(result){
            case "Reserve Table":
                await dc.begin('reservePrompt');
                break;
            case "Wake Up":
                await dc.begin('wakeUpPrompt');
                break;
            default:
                await dc.context.sendActivity("Sorry, i don't understand that command. Please choose an option from the list below.");
                break;            
        }
    },
    async function (dc, result){
        await dc.replace('mainMenu'); // Show the menu again
    }

]);

// Importing the dialogs 
const checkIn = require("./checkIn");
dialogs.add('checkInPrompt', new checkIn.CheckIn(userState));

const reserve_table = require("./reserveTable");
dialogs.add('reservePrompt', new reserve_table.ReserveTable(userState));

const wake_up = require("./wake_up");
dialogs.add('wakeUpPrompt', new wake_up.WakeUp(userState));

```

As you can see, the modular classes are added to the master `dialogs` object in similar fashion as you would have added [prompts](bot-builder-prompts.md) to a dialog. In this way, you can add as many modules to your bot as you want. Each module would add additional capabilities and services the bot can offer to your users.


## Next steps

Now that you know how to modularize your bot logic by using dialogs, let's take a look at how to use Language Understanding (LUIS) to help your bot decide when to begin the dialogs.

> [!div class="nextstepaction"]
> [Use LUIS for Language Understanding](./bot-builder-howto-v4-luis.md)
