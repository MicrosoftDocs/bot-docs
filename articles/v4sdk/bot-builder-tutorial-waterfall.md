---
title: Ask the user questions | Microsoft Docs
description: Learn how to use the waterfall model to ask user for multiple inputs in the Bot Builder SDK.
keywords: waterfalls, dialogs, asking a question, prompts
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 5/10/2018
monikerRange: 'azure-bot-service-4.0'
---

# Ask the user questions

At its core, a bot is built around the conversation with a user. Conversation can take [many forms](bot-builder-conversations.md); they may be short or may be more complex, may be asking questions or may be answering questions. What shape the conversation takes depends on several factors, but they all involve a conversation.

This tutorial guides you through building up a conversation, from asking a simple question through a multi-turn bot. Our example will be around reserving a table, but you can imagine a bot that does a variety of things through a multi-turn conversation, such as placing an order, answering FAQs, making reservations, and so on.

An interactive chat bot can respond to user input or ask user for specific input. This tutorial will show you how to ask a user a question using the `Prompts` library, which is part of `Dialogs`. [Dialogs](../bot-service-design-conversation-flow.md) can be thought of as the container that defines a conversation structure, and prompts within dialogs is covered more in depth in its own [how-to article](bot-builder-prompts.md).

## Prerequisite

Code in this tutorial will build on the **basic bot** you created through the [Get Started](~/bot-service-quickstart.md) experience.

## Get the package

# [C#](#tab/cstab)

Install the **Microsoft.Bot.Builder.Dialogs** package from Nuget packet manager.

# [JavaScript](#tab/jstab)
Navigate to your bot's project folder and install the `botbuilder-dialogs` package from NPM:

```cmd
npm install --save botbuilder-dialogs@preview
```

---

## Import package to bot

# [C#](#tab/cstab)

Add reference to both dialogs and prompts in your bot code.

```cs
// ...
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Prompts;
// ...
```


# [JavaScript](#tab/jstab)

Open the **app.js** file and include the `botbuilder-dialogs` library in the bot code.

```javascript
const botbuilder_dialogs = require('botbuilder-dialogs');
```

---

This will give you access to the `DialogSet` and `Prompts` library that you will use to ask the user questions. `DialogSet` is just a collection of dialogs, which we structure in a **waterfall** pattern. This simply means that one dialog follows another.

## Instantiate a dialogs object

Instantiate a `dialogs` object. You will use this dialog object to manage the question and answer process.

# [C#](#tab/cstab)
Declare a member variable in your bot class and initialize it in the constructor for your bot. 
```cs
public class MyBot : IBot
{
    private readonly DialogSet dialogs;
    public MyBot()
    {
        dialogs = new DialogSet();
    }
    // The rest of the class definition is omitted here
}
```

# [JavaScript](#tab/jstab)

```javascript
const dialogs = new botbuilder_dialogs.DialogSet();
```
---

## Define a waterfall dialog

To ask a question, you will need at least a two step **waterfall** dialog. For this example, you will construct a two step **waterfall** dialog where the first step asks the user for their name and the second step greets the user by name. 

# [C#](#tab/cstab)

Modify your bot's constructor to add the dialog:
```csharp
public MyBot()
{
    dialogs = new DialogSet();
    dialogs.Add("greetings", new WaterfallStep[]
    {
        async (dc, args, next) =>
        {
            // Prompt for the guest's name.
            await dc.Prompt("textPrompt","What is your name?");
        },
        async(dc, args, next) =>
        {
            await dc.Context.SendActivity($"Hi {args["Text"]}!");
            await dc.End();
        }
    });
}
```

# [JavaScript](#tab/jstab)

```javascript
// Greet user:
// Ask for the user name and then greet them by name.
dialogs.add('greetings',[
    async function (dc){
        await dc.prompt('textPrompt', 'What is your name?');
    },
    async function(dc, results){
        var userName = results;
        await dc.context.sendActivity(`Hello ${userName}!`);
        await dc.end(); // Ends the dialog
    }
]);
```

---

The question is asked using a `textPrompt` method that came with the `Prompts` library. The `Prompts` library offers a set of prompts that allows you to ask users for various types of information. For more information about other prompt types, see [Prompt user for input](~/v4sdk/bot-builder-prompts.md).

For the prompting to work, you will need to add a prompt to the `dialogs` object with the dialogId `textPrompt` and create it with the `TextPrompt()` constructor.

# [C#](#tab/cstab)

```cs
public MyBot()
{
    dialogs = new DialogSet();
    dialogs.Add("greetings", new WaterfallStep[]
    {
        async (dc, args, next) =>
        {
            // Prompt for the guest's name.
            await dc.Prompt("textPrompt","What is your name?");
        },
        async(dc, args, next) =>
        {
            await dc.Context.SendActivity($"Hi {args["Text"]}!");
            await dc.End();
        }
    });
    // add the prompt, of type TextPrompt
    dialogs.Add("textPrompt", new Builder.Dialogs.TextPrompt());
}

```
Once the user answers the question, the response can be found in the `args` parameter of step 2.

# [JavaScript](#tab/jstab)

```javascript
dialogs.add('textPrompt', new botbuilder_dialogs.TextPrompt());
```
Once the user answers the question, the response can be found in the `results` parameter of step 2. In this case, the `results` is assigned to a local variable `userName`. In a later tutorial, we'll show you how to persist user inputs to a storage of your choice.

---


Now that you have defined your `dialogs` to ask a question, you need to call on the dialog to start the prompting process.

## Start the dialog

# [C#](#tab/cstab)

Modify your bot logic to something like this:

```cs

public async Task OnTurn(ITurnContext context)
{
    // We'll cover state later, in the next tutorial
    var state = ConversationState<Dictionary<string, object>>.Get(context);
    var dc = dialogs.CreateContext(context, state);
    if (context.Activity.Type == ActivityTypes.Message)
    {
        await dc.Continue();
        
        if(!context.Responded)
        {
            await dc.Begin("greetings");
        }
    }
}
```

Bot logic goes in the `OnTurn()` method. Once the user says "Hi" then the bot will start the `greetings` dialog. The first step of the `greetings` dialog prompts the user for their name. The user will send a reply with their name as a message activity, and the result is send to step two of the waterfall through the `dc.Continue()` method. The second step of the waterfall, as you have defined it, will greet the user by their name and ends the dialog. 

# [JavaScript](#tab/jstab)

Modify your **Basic** bot's `processActivity()` method to look like this:

```javascript
// Listen for incoming activity 
server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (context) => {
        const isMessage = (context.activity.type === 'message');
        // State will store all of your information 
        const convo = conversationState.get(context);
        const dc = dialogs.createContext(context, convo);

        if (isMessage) {
            // Check for valid intents
            if(context.activity.text.match(/hi/ig)){
                await dc.begin('greetings');
            }
        }

        if(!context.responded){
            // Continue executing the "current" dialog, if any.
            await dc.continue();

            if(!context.responded && isMessage){
                // Default message
                await context.sendActivity("Hi! I'm a simple bot. Please say 'Hi'.");
            }
        }
    });
});
```

Bot logic goes within the `processActivity()` method. Once the user says "Hi" then the bot will start the `greetings` dialog. The first step of the `greetings` dialog prompts the user for their name. The user will send a reply with their name as the activity's `text` message. Since the message didn't match any expected intents and the bot has not sent any reply to the user, the result is sent to step two of the waterfall through the `dc.continue()` method. The second step of the waterfall, as you have defined it, will greet the user by their name and ends the dialog. If, for example, step two did not send the user the greeting message, then `processActivity` method will end with the *default message* sent to the user.

---



## Define a more complex waterfall dialog

Now that we've covered how a waterfall dialog works and how to build one, let's try a more complex dialog aimed at reserving a table.

To manage the table reservation request, you will need to define a **waterfall** dialog with four steps. In this conversation, you will also be using a `DatetimePrompt` and `NumberPrompt` in additional to the `TextPrompt`.



# [C#](#tab/cstab)

Start with the Echo Bot template, and rename your bot to CafeBot. Add a `DialogSet` and some static member variables.

```cs

namespace CafeBot
{
    public class CafeBot : IBot
    {
        private readonly DialogSet dialogs;

        // Usually, we would save the dialog answers to our state object, which will be covered in a later tutorial.
        // For purpose of this example, let's use the three static variables to store our reservation information.
        static DateTime reservationDate;
        static int partySize;
        static string reservationName;

        // the rest of the class definition is omitted here
        // but is discussed in the rest of this article
    }
}
```

Then define your `reserveTable` dialog. You can add the dialog within the bot class constructor.
```cs
public CafeBot()
{
    dialogs = new DialogSet();

    // Define our dialog
    dialogs.Add("reserveTable", new WaterfallStep[]
    {
        async (dc, args, next) =>
        {
            // Prompt for the guest's name.
            await dc.Context.SendActivity("Welcome to the reservation service.");

            await dc.Prompt("dateTimePrompt", "Please provide a reservation date and time.");
        },
        async(dc, args, next) =>
        {
            var dateTimeResult = ((DateTimeResult)args).Resolution.First();

            reservationDate = Convert.ToDateTime(dateTimeResult.Value);
            
            // Ask for next info
            await dc.Prompt("partySizePrompt", "How many people are in your party?");

        },
        async(dc, args, next) =>
        {
            partySize = (int)args["Value"];

            // Ask for next info
            await dc.Prompt("textPrompt", "Whose name will this be under?");
        },
        async(dc, args, next) =>
        {
            reservationName = args["Text"];
            string msg = "Reservation confirmed. Reservation details - " +
            $"\nDate/Time: {reservationDate.ToString()} " +
            $"\nParty size: {partySize.ToString()} " +
            $"\nReservation name: {reservationName}";
            await dc.Context.SendActivity(msg);
            await dc.End();
        }
    });

     // Add a prompt for the reservation date
     dialogs.Add("dateTimePrompt", new Microsoft.Bot.Builder.Dialogs.DateTimePrompt(Culture.English));
     // Add a prompt for the party size
     dialogs.Add("partySizePrompt", new Microsoft.Bot.Builder.Dialogs.NumberPrompt<int>(Culture.English));
     // Add a prompt for the user's name
     dialogs.Add("textPrompt", new Microsoft.Bot.Builder.Dialogs.TextPrompt());
}
```


# [JavaScript](#tab/jstab)

The `reserveTable` dialog will look like this:

```javascript
// Reserve a table:
// Help the user to reserve a table
var reservationInfo = {};

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
        await dc.prompt('textPrompt', "Whose name will this be under?");
    },
    async function(dc, result){
        reservationInfo.reserveName = result;
        
        // Reservation confirmation
        var msg = `Reservation confirmed. Reservation details: 
            <br/>Date/Time: ${reservationInfo.dateTime} 
            <br/>Party size: ${reservationInfo.partySize} 
            <br/>Reservation name: ${reservationInfo.reserveName}`;
        await dc.context.sendActivity(msg);
        await dc.end();
    }
]);
```

---

The conversation flow of the `reserveTable` dialog will ask the user 3 questions through the first three steps of the waterfall. Step four processes the answer to the last question and sends the user the reservation confirmation.



# [C#](#tab/cstab)
Each waterfall step of the `reserveTable` dialog uses a prompt to ask the user for information. The following code was used to add the prompts to the dialog set.

```cs
dialogs.Add("dateTimePrompt", new Microsoft.Bot.Builder.Dialogs.DateTimePrompt(Culture.English));
dialogs.Add("partySizePrompt", new Microsoft.Bot.Builder.Dialogs.NumberPrompt<int>(Culture.English));
dialogs.Add("textPrompt", new Microsoft.Bot.Builder.Dialogs.TextPrompt());
```

# [JavaScript](#tab/jstab)

For this waterfall to work, you will also need to add these prompts to the `dialogs` object:

```javascript
// Define prompts
// Generic prompts
dialogs.add('textPrompt', new botbuilder_dialogs.TextPrompt());
dialogs.add('dateTimePrompt', new botbuilder_dialogs.DatetimePrompt());
dialogs.add('partySizePrompt', new botbuilder_dialogs.NumberPrompt());
```

---

Now, you are ready to hook this into the bot logic.

## Start the dialog

# [C#](#tab/cstab)
Modify your bot's `OnTurn` to contain the following code:
```cs
public async Task OnTurn(ITurnContext context)
{
    if (context.Activity.Type == ActivityTypes.Message)
    {
        // The type parameter PropertyBag inherits from 
        // Dictionary<string,object>
        var state = ConversationState<Dictionary<string, object>>.Get(context);
        var dc = dialogs.CreateContext(context, state);
        await dc.Continue();

        // Additional logic can be added to enter each dialog depending on the message received
        
        if(!context.Responded)
        {
            if (context.Activity.Text.ToLowerInvariant().Contains("reserve table"))
            {
                await dc.Begin("reserveTable");
            }
            else
            {
                await context.SendActivity($"You said '{context.Activity.Text}'");
            }
        }
    }
}
```


In **Startup.cs**, change the initialization of the ConversationState middleware to use a class deriving from `Dictionary<string,object>` instead of `EchoState`.

For example, in `Configure()`:
```cs
options.Middleware.Add(new ConversationState<Dictionary<string, object>>(dataStore));
```


# [JavaScript](#tab/jstab)

To capture the user intent for this request, add a few lines of code to the `processActivity()` method. Modify your bot's `processActivity()` method to look like this:

```javascript
// Listen for incoming activity 
server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (context) => {
        const isMessage = (context.activity.type === 'message');
        // State will store all of your information 
        const convo = conversationState.get(context);
        const dc = dialogs.createContext(context, convo);

        if (isMessage) {
            // Check for valid intents
            if(context.activity.text.match(/hi/ig)){
                await dc.begin('greetings');
            }
            else if(context.activity.text.match(/reserve table/ig)){
                await dc.begin('reserveTable');
            }
        }

        if(!context.responded){
            // Continue executing the "current" dialog, if any.
            await dc.continue();

            if(!context.responded && isMessage){
                // Default message
                await context.sendActivity("Hi! I'm a simple bot. Please say 'Hi' or 'Reserve table'.");
            }
        }
    });
});
```

At execution time, whenever the user sends the message containing the string `reserve table`, the bot will start the `reserveTable` conversation.

---



## Next steps

In this tutorial, the bot is saving the user's input to variables within our bot. If you want to store or persist this information, you need to add state to the middleware layer. Let's take a closer look at how to persist user state data in the next tutorial. 

> [!div class="nextstepaction"]
> [Persist user data](bot-builder-tutorial-persist-user-inputs.md)
