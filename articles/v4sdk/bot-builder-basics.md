---
title: Bot basics | Microsoft Docs
description: Bot Builder SDK basic.
keywords: turn context, receiving input, bot basic, bot
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 09/16/2018
monikerRange: 'azure-bot-service-4.0'
---

# Bot basics

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

Intelligent bots can help you get things done without involving humans. Bots can perform tasks such as, setting an alarm, giving you a weather report, finding an airline ticket or ordering pizza.

A bot is a messaging app that can be developed in C# or JavaScript using the Bot Builder SDK v4. You can leverage samples and templates in the SDK to create bots that have the ability to understand natural language and answer questions. Bots developed with the SDK can be easily deployed to Azure or another cloud service.

## Interaction with your bot

A bot is an app that users interact with in a conversational way, using text, graphics (cards), or speech. Every interaction between the user and the bot generates an Activity. Depending on the application used to connect to the bot (Facebook, Skype, Slack, etc.), certain information is sent between the user and your bot as part of the Activity.

An Activity can come in various forms. Activities include both communication from the user, which is referred to as a message, or additional information wrapped up in a handful of other [activity types](~/bot-service-activities-entities.md). This additional information can include when a new party joins or leaves the conversation, when a conversation ends, etc. Those types of communication from the user's connection are sent by the underlying system, without the user needing to do anything.

The SDK receives the communication and wraps it up in an Activity object, with the correct type, to give it to your bot code. An inbound Activity arrives at the bot via an HTTP POST request, and an outbound Activitiy is sent as an outbound HTTP POST from the bot.

## Defining a turn

The process of receiving an activity and sending an activity in response is called a turn. The bot execution follows this general pattern:

- Receive an Activity.
- Perform business logic and send one or more Activities in response.

Activity processing is managed by the **adapter**. When the adapter receives an Activity, it creates a **turn context** to provide information about the Activity and give context to the current turn we are processing. That turn context exists for the duration of the turn, and then is disposed of, marking the end of the turn.

The turn context contains a handful of information, and the same object is used through all the layers of your bot. This is helpful because this turn context object can be, and should be, used to store information that might be needed later in the turn. See [activity processing](~/v4sdk/bot-builder-concept-activity-processing.md) for more information.

> [!IMPORTANT]
> All **turns** are independent of each other, executing on their own, and have the potential to overlap. The bot may be processing multiple turns at a time, from various users on various channels. Each turn will have it's own turn context, but it's worth considering the complexity that introduces in some situations.

## Bot structure

Let's look at the Echo Bot With Counter [[C#](https://aka.ms/EchoBotWithStateCSharp) | [JS](https://aka.ms/EchoBotWithStateJS)] sample, and examine key pieces of the bot.

# [C#](#tab/cs)

A bot is a type of [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.1) web application. If you look at the [ASP.NET](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/index?view=aspnetcore-2.1&tabs=aspnetcore2x) fundamentals, you'll see similar code in files such as Program.cs and Startup.cs. These files are required for all web apps and are not bot specific. Code in some of these files won't be copied here, but you can refer to the Echo Bot With Counter sample.

### EchoWithCounterBot.cs

The main bot logic is defined in `public class EchoWithCounterBot : IBot`. `IBot` defines a single method `OnTurnAsync`. Your application must implement this method. `OnTurnAsync` has turnContext that provides information about the incoming Activity. The incoming Activity corresponds to the inbound HTTP request. Activities can be of various types, so we first check to see if your bot has received a message. If it is a message, we  get the conversation state from the turn context, increment the turn counter, and then persist the new turn counter value into the conversation state. And then send a message back to the user using SendActivityAsync call. The outgoing Activity corresponds to the outbound HTTP request.

```cs
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
{
    if (turnContext.Activity.Type == ActivityTypes.Message)
    {
        // Get the conversation state from the turn context.
        var state = await _accessors.CounterState.GetAsync(turnContext, () => new CounterState());

        // Bump the turn count for this conversation.
        state.TurnCount++;

        // Set the property using the accessor
        await _accessors.CounterState.SetAsync(turnContext, state);

        // Save the new turn count into the conversation state.
        await _accessors.ConversationState.SaveChangesAsync(turnContext);

        // Echo back to the user whatever they typed.
        var responseMessage = $"Turn {state.TurnCount}: You sent '{turnContext.Activity.Text}'\n";
        await turnContext.SendActivityAsync(responseMessage);
    }
}
```

### Startup.cs

The `ConfigureServices` method loads the connected services from [.bot](bot-builder-basics.md#the-bot-file) file, catches any errors that occur during a conversation turn and logs them, sets up your credential provider and creates a conversation state object to store conversation data in memory.

```csharp
IStorage dataStore = new MemoryStorage();
var conversationState = new ConversationState(dataStore);
options.State.Add(conversationState);
```

It also creates and registers `EchoBotAccessors` that are defined in the EchoBotStateAccessors.cs file and are passed into the `public class EchoWithCounterBot : IBot` class using dependency injection framework in ASP.NET Core.

```csharp
services.AddSingleton<EchoBotAccessors>(sp =>
{
    // ...

    // Create the custom state accessor.
    // State accessors enable other components to read and write individual properties of state.
    var accessors = new EchoBotAccessors(conversationState)
    {
        CounterState = conversationState.CreateProperty<CounterState>(EchoBotAccessors.CounterStateName),
    };

    return accessors;
});
```

The `Configure` method finishes the configuration of your app by specifying that the app use the Bot Framework and a few other files. All bots using the Bot Framework will need that configuration call. `ConfigureServices` and `Configure` are called by the runtime when the app starts.

### CounterState.cs

This file contains a simple class that our bot uses to maintain the current state. It contains only an `int` that we use to increment your the counter.

```cs
public class CounterState
{
    public int TurnCount { get; set; } = 0;
}
```

### EchoBotAccessors.cs

The `EchoBotAccessors` class is created as a singleton in the `Startup` class and passed into the IBot derived class. In this case, `public class EchoWithCounterBot : IBot`. The bot uses the accessor to persist conversation data. The constructor of `EchoBotAccessors` is passed in a conversation object that is created in the Startup.cs file.

```cs
public class EchoBotAccessors
{
    public EchoBotAccessors(ConversationState conversationState)
    {
        ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
    }

    public static string CounterStateName { get; } = $"{nameof(EchoBotAccessors)}.CounterState";

    public IStatePropertyAccessor<CounterState> CounterState { get; set; }

    public ConversationState ConversationState { get; }
}
```

# [JavaScript](#tab/js)

The system section mainly contains **package.json**, the **.env** file, **app.js** and **README.md**. Code in some files won't be copied here, but you will see it when you run the bot.

### package.json

**package.json** specifies dependencies and their associated versions for your bot. This is all set up by the template and your system.

### .env file

The **.env** file specifies the configuration information for your bot, such as the port number, app ID, and password among other things. If using certain technologies or using this bot in production, you will need to add your specific keys or URL to this configuration. For this Echo bot, however, you don't need to do anything here right now; the app ID and password may be left undefined at this time. 

To use the **.env** configuration file, the template needs an extra package included.  First, get the `dotenv` package from npm:

`npm install dotenv`

Then add the following line to your bot with the other required libraries:

```javascript
const dotenv = require('dotenv');
```

### index.js

The top of your `index.js` file has the server and adapter that allow your bot to communicate with the user and send responses. The server will listen on the specified port from the **.env** configuration, or fall back to _3978_ for connection with your emulator. The adapter will act as the conductor for your bot, directing incoming and outgoing communication, authentication, and so on. 

```javascript
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
``` 

### Required libraries

At the very top of your `index.js` file you will find a series of modules or libraries that are being required. These modules will give you access to a set of functions that you may want to include in your application. 

```javascript
const path = require('path');
const restify = require('restify');

const CONFIG_ERROR = 1;

// Import required bot services. See https://aka.ms/bot-services to learn more about the different parts of a bot.
const { BotFrameworkAdapter, MemoryStorage, ConversationState } = require('botbuilder');

// This bot's main dialog.
const MainDialog = require('./dialogs/mainDialog');

// Import required bot configuration.
const { BotConfiguration } = require('botframework-config');

```

### Conversation state

Within **index.js**, we created a state object that uses `MemoryStorage` as the storage provider. This state is defined as `ConversationState`, which just means it's keeping the state of your conversation. `ConversationState` will store the information you're interested in, which in this case is simply a turn counter, in memory.

```javascript
// Create conversation state with in-memory storage provider. 
const conversationState = new ConversationState(memoryStorage);

// Create the main dialog.
const mainDlg = new MainDialog(conversationState);
```

### Bot Logic

The third parameter within *processActivity* is a function handler that will be called to perform the bot’s logic after the received [activity](bot-builder-concept-activity-processing.md) has been pre-processed by the adapter and routed through any middleware. The [context](./bot-builder-concept-activity-processing.md#turn-context) variable, passed as an argument to the function handler, can be used to provide information about the incoming activity, the sender and receiver, the channel, the conversation, etc. Activity processing is routed to the `mainDlg` object.

```javascript
// Listen for incoming requests 
server.post('/api/messages', (req, res) => {
    // Route received request to adapter for processing
    adapter.processActivity(req, res, (context) => {
        // Route to main dialog.
        await mainDlg.onTurn(context);
    });
});
```


### MainDialog

All Activity processing is routed to this class's `onTurn` handler. When the class is created, a state object is passed in. Using this state object, the constructor creates a `this.countProperty` accessor to persist the turn counter for this bot.

On each turn, we first check to see if the bot has received a message. If the bot did not receive a message, we will echo back the activity type received. Next, we create a state variable that holds the information of your bot's conversation. If the count variable is `undefined`, it is set to 1 (which will occur when your bot first starts) or increment it with every new message. We echo back to the user the count along with the message they sent. Finally, we set the count and save the changes to state.

```javascript
// Turn counter property
const TURN_COUNTER = 'turnCounter';

class MainDialog {
    /**
     * 
     * @param {Object} conversationState 
     */
    constructor (conversationState) {
        // creates a new state accessor property.see https://aka.ms/about-bot-state-accessors to learn more about the bot state and state accessors
        this.conversationState = conversationiState;
        this.countProperty = this.conversationState.createProperty(TURN_COUNTER);
    }
    /**
     * 
     * @param {Object} context on turn context object.
     */
    async onTurn(context) {
        // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
        if (context.activity.type === 'message') {
            // read from state.
            let count = await this.countProperty.get(context);
            count = count === undefined ? 1 : ++count;
            await context.sendActivity(`${count}: You said "${context.activity.text}"`);
            await this.countProperty.set(context, count);

            // End this turn by saving changes to the conversation state.
            await this.conversationState.saveChanges(context);
        }
        else {
            await context.sendActivity(`[${context.activity.type} event detected]`);
        }
    }
}

module.exports = MainDialog;
```


---

### The bot file

The `.bot` file contains information, including the endpoint, app ID, and password, and references to services that are used by the bot. This file gets created for you when you start building a bot from a template, but you can create your own through the emulator or other tools. You can specify the .bot file to use when testing your bot with the [emulator](../bot-service-debug-emulator.md).

```json
{
    "name": "EchoBotWithCounter",
    "description": "Echo bot with counter sample.",
    "secretKey": "",
    "services": [
      {
        "id": "http://localhost:3978/api/messages",
        "name": "EchoBotWithCounter",
        "type": "endpoint",
        "appId": "",
        "appPassword": "",
        "endpoint": "http://localhost:3978/api/messages"
      }
    ]
}
```

## Next steps

> [!div class="nextstepaction"]
> [Create a bot](~/bot-service-quickstart.md)
