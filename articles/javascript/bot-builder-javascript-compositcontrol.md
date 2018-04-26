---
title: Create modular bots logic using composit control | Microsoft Docs
description: Learn how to modularize your bot logic using composit control in the Bot Builder SDK for Node.js.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 3/1/2018
monikerRange: 'azure-bot-service-4.0'
---

# Create modular bot logic with Composite Control

Imagine that you are creating a hotel bot that handles multiple tasks such as greeting the user, reserving a dinner table, ordering food, setting an alarm, displaying the current weather and many others. You can handle each of these tasks within your bot using one `dialogs` object but this can make your `dialogs` code too large and cluttered in your `app.js` file. The best way to tackle this issue is through modularization. Modularization will streamline your code and make it easier to debug. Additionally, you can break it up into sections allowing multiple teams to simultaneously work on the bot. We can create a bot that manages multiple conversation flows by breaking them up into components using Composite Control. We will create a few basic conversation flows and show how they can be combined together using Composite Control.

In this example we will be creating a hotel bot that combines a check in, wake-up, and reserve table modules.

## Define a modular class

First, we start with a simple check in dialog that will ask the user for their name and what room they will be staying in. To modularize this task, we create a `CheckIn` class that extends `CompositeControl`. This class has a constructor that defines the root dialog as `checkIn`. The `checkIn` dialog is defined as a *waterfall* with three steps. The signature and construction of the dialog object is exactly the same as a standard waterfall. For more information about dialogs and waterfall, see [Use dialogs to manage conversation flow](bot-builder-javascript-dialog-manage-conversation-flow.md).

To keep track of the user's input, a `state` object is passed in as a dialog parameter from the parent. In the root dialg, the `state` object is passed into the `args` parameter. Throughout this dialog we can write to the state object as the user inputs information. The state object, in this case defined as `guestInfo`, will be returned to the parent when `dc.end(guestInfo)` is called.  For more information about state management, see [Manage conversation and user state](../v4sdk/bot-builder-how-to-v4-state.md). More details about saving state data is discussed below.


**checkIn.js**
```JavaScript
const botbuilder_dialogs = require("botbuilder-dialogs");

class CheckIn extends botbuilder_dialogs.CompositeControl {
    constructor() {
        // Dialog ID of 'checkIn' will start when class is called in the parent
        super(dialogs, 'checkIn');
    }
}
exports.CheckIn = CheckIn;

// Create a variable that will hold the parent's state object
var guestInfo = {
    userName: undefined,
    room: undefined
};

// Defining the conversation flow using a waterfall model
const dialogs = new botbuilder_dialogs.DialogSet();
dialogs.add('checkIn', [
    async function (dc, args) {
        // Set guestInfo to args, the state object passed in from the parent
        guestInfo = args;
        await dc.context.sendActivity("What is your name?");
    },
    async function (dc, name){
        // Save the name 
        guestInfo.userName = name;
        await dc.prompt('numberPrompt', `Hi ${name}. What room will you be staying in?`);
    },
    async function (dc, room){
        // Save the room number
        guestInfo.room = room
        await dc.context.sendActivity(`Great! Enjoy your stay!`);
        // Return the updated state object back to the parent 
        return await dc.end(guestInfo);
    }
]);
// Defining the prompt used in this conversation flow
dialogs.add('textPrompt', new botbuilder_dialogs.TextPrompt());
dialogs.add('numberPrompt', new botbuilder_dialogs.NumberPrompt());
```

## Define multiple modular classes

One major benefit of using Composit Control is the ability to combine multiple dialogs together. Since each `DialogSet` maintains an exclusive set of `dialogs`, sharing or cross referencing `dialogs` cannot be done easily. This is where Composit Control comes in, `CompositControl` will merge all the individual dialogs into one master dialog that makes cross referencing possible. To illustrate that, let’s create two more dialogs that will ask the user which table they would like to reserve for dinner as well as create a wake up call. These dialogs will once again be defined in a new class that extends from Composite Control and starts their root dialogs when their class is called. 

**reserveTable.js**
```JavaScript
const botbuilder_dialogs = require("botbuilder-dialogs");

class ReserveTable extends botbuilder_dialogs.CompositeControl {
    constructor() {
        // Dialog ID of 'reserve_table' will start when class is called in the parent
        super(dialogs, 'reserve_table');
    }
}

// Create a variable that will hold the parent's state object
var guestInfo = {
    tableNumber: undefined
};

exports.ReserveTable = ReserveTable;

const dialogs = new botbuilder_dialogs.DialogSet();

// Defining the conversation flow using a waterfall model
dialogs.add('reserve_table', [
    async function (dc, args) {
        // Set guestInfo to args, the state object passed in from the parent
        guestInfo = args;
        const prompt = `Welcome ${guestInfo.userName}, which table would you like to reserve?`;
        const choices = ['1', '2', '3', '4', '5', '6'];
        await dc.prompt('choicePrompt', prompt, choices);
    },
    async function(dc, choice){
        // Save the table number
        guestInfo.tableNumber = choice.value;
        await dc.context.sendActivity(`Sounds great, we will reserve table number ${choice.value} for you.`);
        // Return the updated state object back to the parent 
        return dc.end(guestInfo);
    }
]);
// Defining the prompt used in this conversation flow
dialogs.add('choicePrompt', new botbuilder_dialogs.ChoicePrompt());
```

**wakeUp.js**
```JavaScript
const botbuilder_dialogs = require("botbuilder-dialogs");

class WakeUp extends botbuilder_dialogs.CompositeControl {
    constructor() {
        // Dialog ID of 'wakeup' will start when class is called in the parent
        super(dialogs, 'wakeUp');
    }
}
exports.WakeUp = WakeUp;
// Create a variable that will hold the parent's state object
var guestInfo = {
    time: undefined
};

// Defining the conversation flow using a waterfall model
const dialogs = new botbuilder_dialogs.DialogSet();
dialogs.add('wakeUp', [
    async function (dc, args) {
        // Set guestInfo to args, the state object passed in from the parent
        guestInfo = args;
        await dc.prompt('datePrompt', `Hello, ${guestInfo.userName}. What time would you like your alarm to be set?`);
    },
    async function (dc, time){
        // Save the time
        guestInfo.time = time[0].value
        await dc.context.sendActivity(`Your alarm is set to ${time[0].value} for room ${guestInfo.room}`);
        // Return the updated state object back to the parent 
        return dc.end(guestInfo);
    }
]);
// Defining the prompt used in this conversation flow
dialogs.add('datePrompt', new botbuilder_dialogs.DatetimePrompt());
```

## Add modules to bot

Below is the complete `app.js` file that ties these three modularized dialogs into one bot. The flow of the bot will be tracked by `state.topic`. The `state.topic` parameter tracks whether the bot is engaging in a topic of conversation with a user. In this case, the `state.topic` is implemented as a boolean parameter. If you want to, you could implement it as a *name* parameter by setting `state.topic = 'checkIn'` and resetting it to `state.topic = undefined`.

**app.js**
```JavaScript
const botbuilder = require("botbuilder");
const botbuilder_dialogs = require("botbuilder-dialogs");
const restify = require("restify");

// Create server
let server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log(`${server.name} listening to ${server.url}`);
});

// Create adapter
const adapter = new botbuilder.BotFrameworkAdapter({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

//Memory Storage
const conversationState = new botbuilder.ConversationState(new botbuilder.MemoryStorage());

adapter.use(conversationState);

// Listen for incoming requests 
server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (context) => {
        if (context.activity.type === 'message') {

            // State will store all of your information 
            const state = conversationState.get(context);
            const dc = dialogs.createContext(context, state);

            // Route received request
            if (state.topic) {
                // We are in the middle of a topic of conversation
                var result = await dc.continue()
            
                if(!result.active){
                    // If dialog finished, save the result 
                    state.guestInfo = result.result;
                    // Reset the conversation topic
                    state.topic = false;
                }

                // Default response if none was given during the turn.
                if(!context.responded){
                    dc.context.sendActivity("Sorry I don't understand");
                }
            }
            else if(!state.guestInfo){
                // To track current topic of conversation
                state.topic = true;
                // Get user info and greet the user by name with state passed in as a parameter
                await dc.begin("checkInModule", state.guestInfo); 
            }
            else {
                // Check for user intent
                const utterance = (context.activity.text || '').trim().toLowerCase();

                if(utterance.includes('reserve table')){
                    // To track current topic of conversation
                    state.topic = true;
                    // Calling the dialog with state passed in as a parameter
                    await dc.begin('reserveModule', state.guestInfo)

                } else if(utterance.includes('wake up')){
                    // To track current topic of conversation
                    state.topic = true
                    // Calling the dialog with state passed in as a parameter
                    await dc.begin('wakeUpModule', state.guestInfo);
                } 
                else {
                    // No valid intents, provide some guidence/hints as to what commands the bot understands.
                    await context.sendActivity(`Hi ${state.guestInfo.userName}. How may we serve you today? Request a "wake up" call or "reserve table"?`);
                }
            }
        }
    });
});

const dialogs = new botbuilder_dialogs.DialogSet();

// Importing the dialogs 
const checkIn = require("./checkIn");
dialogs.add('checkInModule', new checkIn.CheckIn());

const reserve_table = require("./reserveTable");
dialogs.add('reserveModule', new reserve_table.ReserveTable());

const wake_up = require("./wake_up");
dialogs.add('wakeUpModule', new wake_up.WakeUp());
```

As you can see, the modular classes are added to the master `dialogs` object in similar fashion as you would have added *prompts* to a dialog. In this way, you can add as many modules to your bot as you want. Each module would add additional capabilities and services the bot can offer to your users.

## Saving State to Storage
When using modular control, it is important to pass state to each class. Passing in the state object gives the bot the ability to read and write information to storage while keeping the module generic. The state object will be automatically cached on the context object for the lifetime of the turn and will only be written to storage if they have been modified. When a bot's state is used as middleware its state object will be automatically read in before your bot logic runs and written back out upon completion of your bot's logic. 

## Next step

<!----
> > [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-manage-conversation-flow.md)
> - [Node.js](../nodejs/bot-builder-nodejs-dialog-manage-conversation-flow.md)
---->
