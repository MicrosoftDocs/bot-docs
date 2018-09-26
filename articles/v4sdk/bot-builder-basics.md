---
title: Bot Activity in the Bot Builder SDK | Microsoft Docs
description: Describes how activity and http work within the Bot Builder SDK.
keywords: conversation flow, turn, bot conversation, dialogs, prompts, waterfalls, dialog set
author: johnataylor
ms.author: johtaylo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 9/12/2018
monikerRange: 'azure-bot-service-4.0'
---

# Understanding how bots work

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

A bot is an app that users interact with in a conversational way, using text, graphics (cards), or speech. Every interaction between the user and the bot generates an Activity. Depending on the application used to connect to the bot (Facebook, Skype, Slack, etc.), certain information is sent between the user and your bot as part of the Activity. Before creating bots, it is important to undertand how a bot uses Activity object to communicate with its users. Let's first take a look at Activities that are exchanged when we run a simple echo bot and its relationship to HTTP.

![activity diagram](media/bot-builder-activity.png)

Two Activity types illustrated here are: ConversationUpdate and Message.

Activities belonging to the same conversation all include the same ConversationReference details along with a few other tracking identifiers. Without these fields correctly set in the Activity, the Bot Framework Service won’t know what to do with it, and it will get dropped or rejected.

ConversationUpdate is sent from the Bot Framework Service when a party joins the conversation. For example, on starting a conversation with the Bot Framework Emulator, you will see two ConversationUpdate Activities. One for the user joining the conversation and one for the bot joining. These ConversationUpdate Activities are easily distinguished because the MembersAdded property will contain an Id that matches the Recipient Id for the ConversationUpdate Activity corresponding to the bot joining.

The Message Activity carries conversation information between the parties. In an echo bot example, the Message Activities are carrying simple text. The client will render this text content. Alternatively, the Message Activity might carry text to be spoken, suggested actions or cards to be displayed. 

Activities arrive at the bot from the Bot Framework Service via an HTTP POST request. The bot responds to the inbound POST request with a 200 HTTP status code. Activities sent from the bot to the channel are sent on a separate HTTP POST to the Bot Framework Service. This, in turn is, acknowledged with a 200 HTTP status code.

The protocol doesn’t specify the order in which these POST requests and their acknowledgments are made. However, to fit with common HTTP service frameworks, typically these requests are nested, meaning that the outbound HTTP request is made from the bot within the scope of the inbound HTTP request. This pattern is illustrated in the diagram above. As there are two distinct HTTP connections back to back the security model must provide for both. 

## The Activity processing stack
Let's drill into the previous diagram with a focus on the arrival of a Message Activity.

![activity processing stack](media/bot-builder-activity-processing-stack.png)

In the echo example, the bot replied to the Message Activity with another Message Activity containing the same text message. The Message Activity generated in response must be correctly addressed. Otherwise, it won’t arrive at its intended destination. Correctly addressing an Activity means including the appropriate ConversationReference details along with details about the sender and the recipient. In this particular example, the Message Activity is sent in response to one that had arrived. Therefore, the addressing details can be taken from the inbound Activity. In general, this is not something application need be concerned about as it handled by the SDK behind the scenes. However, it can be useful to know when debugging and examining traces or audit logs.

In the echo bot example, the bot created and sent a Message Activity on an HTTP POST in response to the inbound Message Activity it had received on the HTTP POST it was handling. However, there is nothing in the protocol that mandates this exchange. It’s common for a bot to respond to a ConversationUpdate Activity by sending some welcome text in a Message Activity.  

In the example, the processing starts with the HTTP POST request arriving at the Web Server. In C# this will typically be an ASP.NET project, in a JavaScript NodeJS project this is likely to be one of the popular frameworks such as Express or Restify.

Integrated into the Web Service framework is the Adapter. The Adapter is the core of the Bot SDK runtime.  The Activity is carried as JSON in the HTTP POST body. This JSON is deserialized to create the Activity object that is then handed to the Adapter with a call to ProcessActivity. On receiving the Activity, the Adapter creates a TurnContext and calls the Middleware. The name “TurnContext” follows from the use of the word “turn” to describe all the processing associated with the arrival of an Activity. The TurnContext is one of the most important abstractions in the SDK, not only does it carry the inbound Activity to all the Middleware components and the application logic but it also provides the mechanism whereby the Middleware components and the application logic can send outbound Activities.

## Middleware

Middleware is much like any other messaging middleware, comprising a linear set of components that are each executed in order, giving each a chance to operate on the Activity. The final stage of the middleware pipeline is a callback to invoke the OnTurn function on the bot class the application has registered with the Adapter. The OnTurn function takes a TurnContext as its argument, typically the application logic running inside the OnTurn function will process the inbound Activity’s content and generate one or more Activities in response, sending these out using the SendActivity function on the TurnContext. Calling SendActivity on the TurnContext will cause the middleware components to be invoked on the outbound Activities. Middleware components execute before and after the bot’s OnTurn function. The execution is inherently nested and, as such, sometimes referred to being like a Russian Doll.

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

// Import required bot services. See https://aka.ms/bot-services to learn more about the different parts of a bot.
const { BotFrameworkAdapter, MemoryStorage, ConversationState } = require('botbuilder');

// This bot's main dialog.
const { EchoBot } = require('./bot');

// Import required bot configuration.
const { BotConfiguration } = require('botframework-config');

```

### Conversation state

Within **index.js**, we created a state object that uses `MemoryStorage` as the storage provider. This state is defined as `ConversationState`, which just means it's keeping the state of your conversation. `ConversationState` will store the information you're interested in, which in this case is simply a turn counter, in memory.

```javascript
// Create conversation state with in-memory storage provider. 
const conversationState = new ConversationState(memoryStorage);

// Create the main dialog.
const bot = new EchoBot(conversationState);
```

### Bot Logic

The third parameter within *processActivity* is a function handler that will be called to perform the bot’s logic after the received [activity](bot-builder-concept-activity-processing.md) has been pre-processed by the adapter and routed through any middleware. The [context](./bot-builder-concept-activity-processing.md#turn-context) variable, passed as an argument to the function handler, can be used to provide information about the incoming activity, the sender and receiver, the channel, the conversation, etc. Activity processing is routed to the `mainDlg` object.

```javascript
// Listen for incoming requests 
server.post('/api/messages', (req, res) => {
    // Route received request to adapter for processing
    adapter.processActivity(req, res, (context) => {
        // Route to main dialog.
        await bot.onTurn(context);
    });
});
```


### EchoBot

All Activity processing is routed to this class's `onTurn` handler. When the class is created, a state object is passed in. Using this state object, the constructor creates a `this.countProperty` accessor to persist the turn counter for this bot.

On each turn, we first check to see if the bot has received a message. If the bot did not receive a message, we will echo back the activity type received. Next, we create a state variable that holds the information of your bot's conversation. If the count variable is `undefined`, it is set to 1 (which will occur when your bot first starts) or increment it with every new message. We echo back to the user the count along with the message they sent. Finally, we set the count and save the changes to state.

```javascript
const { ActivityTypes } = require('botbuilder');

// Turn counter property
const TURN_COUNTER_PROPERTY = 'turnCounterProperty';

class EchoBot {
    /**
     *
     * @param {ConversationState} conversation state object
     */
    constructor(conversationState) {
        // Creates a new state accessor property.
        // See https://aka.ms/about-bot-state-accessors to learn more about the bot state and state accessors
        this.countProperty = conversationState.createProperty(TURN_COUNTER_PROPERTY);
        this.conversationState = conversationState;
    }
    /**
     *
     * Use onTurn to handle an incoming activity, received from a user, process it, and reply as needed
     *
     * @param {TurnContext} on turn context object.
     */
    async onTurn(turnContext) {
        // Handle message activity type. User's responses via text or speech or card interactions flow back to the bot as Message activity.
        // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
        // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
        if (turnContext.activity.type === ActivityTypes.Message) {
            // read from state.
            let count = await this.countProperty.get(turnContext);
            count = count === undefined ? 1 : ++count;
            await turnContext.sendActivity(`${ count }: You said "${ turnContext.activity.text }"`);
            // increment and set turn counter.
            await this.countProperty.set(turnContext, count);
        } else { 
            // Generic handler for all other activity types.
            await turnContext.sendActivity(`[${ turnContext.activity.type } event detected]`);
        }
        // Save state changes
        await this.conversationState.saveChanges(turnContext);
    }
}

exports.EchoBot = EchoBot;
```


---

### The bot file

The `.bot` file contains information, including the endpoint, app ID, and password, and references to services that are used by the bot. This file gets created for you when you start building a bot from a template, but you can create your own through the emulator or other tools. You can specify the .bot file to use when testing your bot with the [emulator](../bot-service-debug-emulator.md).

```json
{
    "name": "EchoBotWithCounter",
    "description": "Echo bot with counter sample.",
    "padlock": "",
    "services": [
      {
        "id": "1",
        "name": "development",
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
