---
title: Persist user data | Microsoft Docs
description: Learn how to save user state data to storage in the Bot Builder SDK.
keywords: persist user data, storage, conversation data 
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 4/23/2018
monikerRange: 'azure-bot-service-4.0'
---

# Persist user data

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

When the bot ask users for input, chances are that you would want to persist some of the information to storage of some form. The Bot Builder SDK allows you to store user inputs using *in-memory storage*, *file storage*, or database storage such as *CosmosDB* or *SQL*, where local storage types are mainly used for testing or prototyping and the later storage types are best for production bots.

This tutorial will show you how to define your storage object and save user inputs to the storage object so that it can be persisted. 

> [!NOTE]
> Regardless of the storage type you choose to use, the process for hooking it up and persisting data is the same. This tutorial uses the `FileStorage` as the storage to persist data.
> For more information about state and other storage types, see [Manage conversation and user state](bot-builder-howto-v4-state.md).

## Prequisite 

This tutorial builds on the [Reserve a table](bot-builder-tutorial-waterfall.md) tutorial. In that tutorial, you build a bot that asks the user for three pieces of information about their table reservation at your restaurant. However, the user's input wasn't persisted. This tutorial will add data storage persistance to that bot.

## Add storage to middleware layer

The Bot Builder V4 SDK handles state and storage through a state manager middleware. The middleware provides an abstraction layer that lets you access properties using a simple key-value store, independent of the type of underlying storage. The state manager takes care of writing data to storage and managing concurrency, whether the underlying storage type is in-memory, file storage, or Azure table storage. In this tutorial, we will be using `FileStorage` to persist the user inputs.

The `FileStorage` provider comes with the `bot-builder` package. The sample you started with uses the `MemoryStorage` provider. That storage type is using the bot's volitile *in-memory* which gets disposed when the bot is restarted. The `FileStorage` library, on the other hand, behave similar to a database. That is, this library writes the storage information to a file on your local computer. You can specify where to put this storage file so that you can inspect it later.

To use the `FileStorage`, find the statement in your bot where the `conversationState` object is defined and update it to create a `new botbuilder.FileStorage("c:/temp")`. Also, you can define the location where this storage file should be written out to. That way, you can easily find it to inspect the content of what got persisted.

# [C#](#tab/cstab)
```cs
var storage = new FileStorage("c:/temp");

// These two classes are simply Dictionaries to store state
options.Middleware.Add(new ConversationState<MyBot.convoState>(storage));
options.Middleware.Add(new UserState<MyBot.userState>(storage));
```

The Bot Builder SDK provide three state objects with different scopes that you can choose from.

| State | Scope | Description |
| ---- | ---- | ---- |
| `dc.ActiveDialog.State` | dialog | State available to the steps of the waterfall dialog. |
| `ConversationState` | conversation | State available to current conversation. |
| `UserState` | user | State available accross multiple conversations. |

# [JavaScript](#tab/jstab)

**app.js**
```javascript
// Storage
const storage = new FileStorage("c:/temp");
const convoState = new ConversationState(storage);
const userState  = new UserState(storage);
adapter.use(new BotStateSet(convoState, userState));
```

The `BotStateSet` can manage both the `ConversationState` and `UserState` at the same time. When it comes time to save user data, you can choose. The Bot Builder SDK provide three state objects with different scopes that you can choose from.

| State | Scope | Description |
| ---- | ---- | ---- |
| `dc.activeDialog.state` | dialog | State available to the steps of the waterfall dialog. |
| `ConversationState` | conversation | State available to current conversation. |
| `UserState` | user | State available accross multiple conversations. |

---
 

## Persist state

For your bot you can write to any of these three state locations, depending on what you're saving and how long it needs to persist.  

# [C#](#tab/cstab)

The *state manager middleware* handles writing state for you at the end of each turn. So, all you need to do in your bot is assign the data to the state object of your choice. In this example, we use the `dc.ActiveDialog.State` to track the user input for our reservation. This way, instead of saving the user input in a global variable, you can store it in a temporary state object scope to the dialog. This object only exists as long as the dialog is active; if you want to persist it longer, you must transfer it to one of the other state objects. In this case, we assign the reservation `msg` to the conversation state in the last step of the waterfall.

```cs
dialogs.Add("reserveTable", new WaterfallStep[]
{
    async (dc, args, next) =>
    {
        // Prompt for the guest's name.
        await dc.Context.SendActivity("Welcome to the reservation service.");

        dc.ActiveDialog.State = new Dictionary<string, object>();

        await dc.Prompt("dateTimePrompt", "Please provide a reservation date and time.");
    },
    async(dc, args, next) =>
    {
        var dateTimeResult = ((DateTimeResult)args).Resolution.First();

        dc.ActiveDialog.State["date"] = Convert.ToDateTime(dateTimeResult.Value);
        
        // Ask for next info
        await dc.Prompt("partySizePrompt", "How many people are in your party?");

    },
    async(dc, args, next) =>
    {
        dc.ActiveDialog.State["partySize"] = (int)args["Value"];

        // Ask for next info
        await dc.Prompt("textPrompt", "Who's name will this be under?");
    },
    async(dc, args, next) =>
    {
        dc.ActiveDialog.State["name"] = args["Text"];
        string msg = "Reservation confirmed. Reservation details - " +
        $"\nDate/Time: {dc.ActiveDialog.State["date"].ToString()} " +
        $"\nParty size: {dc.ActiveDialog.State["partySize"].ToString()} " +
        $"\nReservation name: {dc.ActiveDialog.State["name"]}";

        var convo = ConversationState<convoState>.Get(dc.Context);

        // In production, you may want to store something more helpful
        convo[$"{dc.ActiveDialog.State["name"]} reservation"] = msg;

        await dc.Context.SendActivity(msg);
        await dc.End();
    }
});
```

# [JavaScript](#tab/jstab)

The *state manager middleware* handles writing state to file for you at the end of each turn. So, all you need to do in your bot is assign the data to the state object of your choice. In this example, we use the `dc.activeDialog.state` to track the user input in a `reservervationInfo` object. This way, instead of saving the user input in a global variable, you can store it in a temporary state object scope to the dialog. Because this object only exists as long as the dialog is active, if you want to persist it, you must transfer it to one of the other state objects. In this case, we assign the `reservationInfo` to the `convo` state in the last step of the waterfall.

```javascript
// Reserve a table:
// Help the user to reserve a table

dialogs.add('reserveTable', [
    async function(dc, args, next){
        await dc.context.sendActivity("Welcome to the reservation service.");

        dc.activeDialog.state.reservationInfo = {}; // Clears any previous data
        await dc.prompt('dateTimePrompt', "Please provide a reservation date and time.");
    },
    async function(dc, result){
        dc.activeDialog.state.reservationInfo.dateTime = result[0].value;

        // Ask for next info
        await dc.prompt('partySizePrompt', "How many people are in your party?");
    },
    async function(dc, result){
        dc.activeDialog.state.reservationInfo.partySize = result;

        // Ask for next info
        await dc.prompt('textPrompt', "Who's name will this be under?");
    },
    async function(dc, result){
        dc.activeDialog.state.reservationInfo.reserveName = result;
        
        // Persist data
        var convo = conversationState.get(dc.context);; // conversationState.get(dc.context);
        convo.reservationInfo = dc.activeDialog.state.reservationInfo;

        // Confirm reservation
        var msg = `Reservation confirmed. Reservation details: 
            <br/>Date/Time: ${dc.activeDialog.state.reservationInfo.dateTime} 
            <br/>Party size: ${dc.activeDialog.state.reservationInfo.partySize} 
            <br/>Reservation name: ${dc.activeDialog.state.reservationInfo.reserveName}`;
            
        await dc.context.sendActivity(msg);
        await dc.end();
    }
]);
```

---

Now, you are ready to hook this into the bot logic.

## Start the dialog

There is no code changes you need to make here. Simply run the bot and send the message to start the `reserveTable` conversation.

## Check file storage content

After you run the bot and have gone through the `reserveTable` conversation, find the information saved to a file in the location you specified (e.g.: "C:/temp"). The file name is prepended with either "conversation!" or "user!". It may help to sort the files by date so you can find the lastest one easier.

## Next steps

Now that you know how to save user inputs, lets take a look at what type of input you can ask from the user using the prompts library.

> [!div class="nextstepaction"]
> [Prompt user for input](~/v4sdk/bot-builder-prompts.md)
