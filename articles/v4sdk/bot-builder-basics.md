---
title: Bot Activity in the Bot Builder SDK | Microsoft Docs
description: Describes how activity and http work within the Bot Builder SDK.
keywords: conversation flow, turn, bot conversation, dialogs, prompts, waterfalls, dialog set
author: johnataylor
ms.author: johtaylo
manager: kamrani\
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 11/02/2018
monikerRange: 'azure-bot-service-4.0'
---

# Understanding how bots work

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

A bot is an app that users interact with in a conversational way, using text, graphics (such as cards or images), or speech. Every interaction between the user and the bot generates an *activity*. The Bot Service sends information between the user's bot-connected app (such as Facebook, Skype, Slack, etc. which we call the *channel*) and the bot. Each channel may include additional information in the activities they send. Before creating bots, it is important to understand how a bot uses activity objects to communicate with its users. Let's first take a look at activities that are exchanged when we run a simple echo bot.

![activity diagram](media/bot-builder-activity.png)

Two activity types illustrated here are: *conversation update* and *message*.

The Bot Framework Service may send a conversation update when a party joins the conversation. For example, on starting a conversation with the Bot Framework Emulator, you will see two conversation update activities (one for the user joining the conversation and one for the bot joining). To distinguish these conversation update activities, check whether the *members added* property includes a member other than the bot. 

The message activity carries conversation information between the parties. In an echo bot example, the message activities are carrying simple text and the channel will render this text. Alternatively, the message activity might carry text to be spoken, suggested actions or cards to be displayed.

In this example, the bot created and sent a message activity in response to the inbound message activity it had received. However, a bot can respond in other ways to a received message activity; it’s not uncommon for a bot to respond to a conversation update activity by sending some welcome text in a message activity. More information can be found in [welcoming the user](bot-builder-welcome-user.md).

### HTTP Details

Activities arrive at the bot from the Bot Framework Service via an HTTP POST request. The bot responds to the inbound POST request with a 200 HTTP status code. Activities sent from the bot to the channel are sent on a separate HTTP POST to the Bot Framework Service. This, in turn, is acknowledged with a 200 HTTP status code.

The protocol doesn’t specify the order in which these POST requests and their acknowledgments are made. However, to fit with common HTTP service frameworks, typically these requests are nested, meaning that the outbound HTTP request is made from the bot within the scope of the inbound HTTP request. This pattern is illustrated in the diagram above. Since there are two distinct HTTP connections back to back, the security model must provide for both.

### Defining a turn

In a conversation, people often speak one-at-a-time, taking turns speaking. With a bot, it generally reacts to user input. Within the Bot Builder SDK, a _turn_ consists of the user's incoming activity to the bot and any activity the bot sends back to the user as an immediate response. You can think of a turn as the processing associated with the arrival of a given activity.

The *turn context* object provides information about the activity such as the sender and receiver, the channel, and other data needed to process the activity. It also allows for the addition of information during the turn across various layers of the bot.

The turn context is one of the most important abstractions in the SDK. Not only does it carry the inbound activity to all the middleware components and the application logic but it also provides the mechanism whereby the middleware components and the application logic can send outbound activities.

## The activity processing stack

Let's drill into the previous diagram with a focus on the arrival of a message activity.

![activity processing stack](media/bot-builder-activity-processing-stack.png)

In the example above, the bot replied to the message activity with another message activity containing the same text message. Processing starts with the HTTP POST request, with the activity information carried as a JSON payload, arriving at the web server. In C# this will typically be an ASP.NET project, in a JavaScript Node.js project this is likely to be one of the popular frameworks such as Express or Restify.

The *adapter*, an integrated component of the SDK, serves as the conductor of the framework. The service uses the activity information to create an activity object, and then calls the adapter's *process activity* method while passing in the activity object and authentication information (this call is wrapped inside the libraries for C#, but you will see it in JavaScript). Upon receiving the activity, the adapter creates a turn context object and calls the [middleware](#middleware). Processing continues to the bot logic after middleware, the pipeline completes, and the adapter disposes of the turn context object.

The bot's *turn handler*, which makes up most of the application logic, takes a turn context as its argument. The turn handler will typically process the inbound activity’s content and generate one or more activities in response, sending these out using the turn context's *send activity* method. Calling the send activity method will send an activity to the user's channel, unless processing otherwise gets interrupted. The activity will pass through any registered [event handlers](#response-event-handlers) before being sent to the channel.

## Middleware

Middleware is a linear set of components that are each added and executed in order, giving each a chance to operate on the activity both before and after the bot's turn handler and have access to the turn context for that activity. Unless middleware [short circuited](~/v4sdk/bot-builder-concept-middleware.md#short-circuiting), the final stage of the middleware pipeline is a callback to invoke the turn handler of the bot, before returning up the stack. For more in depth information about middleware, see the [middleware topic](~/v4sdk/bot-builder-concept-middleware.md).

## Generating responses

The turn context provides activity response methods to allow code to respond to an activity:

* The _send activity_ and _send activities_ methods send one or more activities to the conversation.
* If supported by the channel, the _update activity_ method updates an activity within the conversation.
* If supported by the channel, the _delete activity_ method removes an activity from the conversation.

Each response method runs in an asynchronous process. When it is called, the activity response method clones the associated [event handler](#response-event-handlers) list before starting to call the handlers, which means it will contain every handler added up to this point but will not contain anything added after the process starts.

This also means the order of your responses for independent activity calls is not guaranteed, particularly when one task is more complex than another. If your bot can generate multiple responses to an incoming activity, make sure that they make sense in whatever order they are received by the user. The only exception to this is the *send activities* method, which allows you to send an ordered set of activities.

[!INCLUDE [alert-await-send-activity](../includes/alert-await-send-activity.md)]

## Response event handlers

In addition to the application and middleware logic, response handlers (also sometimes referred to as event handlers, or activity event handlers) can be added to the context object. These handlers are called when the associated [response](#generating-responses) happens on the current context object, before executing the actual response. These handlers are useful when you know you'll want to do something, either before or after the actual event, for every activity of that type for the rest of the current response.

> [!WARNING]
> Be careful to not call an activity response method from within it's respective response event handler, for example, calling the send activity method from within an _on send activity_ handler. Doing so can generate an infinite loop.

Remember, each new activity gets a new thread to execute on. When the thread to process the activity is created, the list of handlers for that activity is copied to that new thread. No handlers added after that point will be executed for that specific activity event.

The handlers registered on a context object are handled very similarly to how the adapter manages the [middleware pipeline](~/v4sdk/bot-builder-concept-middleware.md#the-bot-middleware-pipeline). Namely, handlers get called in the order they're added, and calling the _next_ delegate passes control to the next registered event handler. If a handler doesn’t call the next delegate, none of the subsequent event handlers are called, the event [short circuits](~/v4sdk/bot-builder-concept-middleware.md#short-circuiting), and the adapter does not send the response to the channel.

## Bot structure

Let's look at the Echo Bot With Counter [[C#](https://aka.ms/EchoBotWithStateCSharp) | [JS](https://aka.ms/EchoBotWithStateJS)] sample, and examine key pieces of the bot.

[!INCLUDE [alert-await-send-activity](../includes/alert-await-send-activity.md)]

# [C#](#tab/cs)

A bot is a type of [ASP.NET Core](https://docs.microsoft.com/aspnet/core/?view=aspnetcore-2.1) web application. If you look at the [ASP.NET](https://docs.microsoft.com/aspnet/core/fundamentals/index?view=aspnetcore-2.1&tabs=aspnetcore2x) fundamentals, you'll see similar code in files such as **Program.cs** and **Startup.cs**. These files are required for all web apps and are not bot specific. Code in some of these files won't be copied here, but you can refer to the [C# echobot-with-counter](https://aka.ms/EchoBot-With-Counter) sample.

### EchoWithCounterBot.cs

The main bot logic is defined in the `EchoWithCounterBot` class that derives from the `IBot` interface. `IBot` defines a single method `OnTurnAsync`. Your application must implement this method. `OnTurnAsync` has turnContext that provides information about the incoming activity. The incoming activity corresponds to the inbound HTTP request. Activities can be of various types, so we first check to see if your bot has received a message. If it is a message, we  get the conversation state from the turn context, increment the turn counter, and then persist the new turn counter value into the conversation state. And then send a message back to the user using SendActivityAsync call. The outgoing activity corresponds to the outbound HTTP request.

```cs
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
{
    if (turnContext.Activity.Type == ActivityTypes.Message)
    {
        // Get the conversation state from the turn context.
        var oldState = await _accessors.CounterState.GetAsync(turnContext, () => new CounterState());

        // Bump the turn count for this conversation.
        var newState = new CounterState { TurnCount = oldState.TurnCount + 1 };

        // Set the property using the accessor.
        await _accessors.CounterState.SetAsync(turnContext, newState);

        // Save the new turn count into the conversation state.
        await _accessors.ConversationState.SaveChangesAsync(turnContext);

        // Echo back to the user whatever they typed.
        var responseMessage = $"Turn {newState.TurnCount}: You sent '{turnContext.Activity.Text}'\n";
        await turnContext.SendActivityAsync(responseMessage);
    }
    else
    {
        await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
    }
}
```

### Startup.cs

The `ConfigureServices` method loads the connected services from [.bot](bot-builder-basics.md#the-bot-file) file, catches any errors that occur during a conversation turn and logs them, sets up your credential provider and creates a conversation state object to store conversation data in memory.

```csharp
services.AddBot<EchoWithCounterBot>(options =>
{
    // Creates a logger for the application to use.
    ILogger logger = _loggerFactory.CreateLogger<EchoWithCounterBot>();

    var secretKey = Configuration.GetSection("botFileSecret")?.Value;
    var botFilePath = Configuration.GetSection("botFilePath")?.Value;

    // Loads .bot configuration file and adds a singleton that your Bot can access through dependency injection.
    BotConfiguration botConfig = null;
    try
    {
        botConfig = BotConfiguration.Load(botFilePath ?? @".\BotConfiguration.bot", secretKey);
    }
    catch
    {
        //...
    }

    services.AddSingleton(sp => botConfig);

    // Retrieve current endpoint.
    var environment = _isProduction ? "production" : "development";
    var service = botConfig.Services.Where(s => s.Type == "endpoint" && s.Name == environment).FirstOrDefault();
    if (!(service is EndpointService endpointService))
    {
        throw new InvalidOperationException($"The .bot file does not contain an endpoint with name '{environment}'.");
    }

    options.CredentialProvider = new SimpleCredentialProvider(endpointService.AppId, endpointService.AppPassword);

    // Catches any errors that occur during a conversation turn and logs them.
    options.OnTurnError = async (context, exception) =>
    {
        logger.LogError($"Exception caught : {exception}");
        await context.SendActivityAsync("Sorry, it looks like something went wrong.");
    };

    // The Memory Storage used here is for local bot debugging only. When the bot
    // is restarted, everything stored in memory will be gone.
    IStorage dataStore = new MemoryStorage();

    // ...

    // Create Conversation State object.
    // The Conversation State object is where we persist anything at the conversation-scope.
    var conversationState = new ConversationState(dataStore);

    options.State.Add(conversationState);
});
```

It also creates and registers `EchoBotAccessors` that are defined in the **EchoBotStateAccessors.cs** file and are passed into the public `EchoWithCounterBot` constructor using dependency injection framework in ASP.NET Core.

```csharp
// Create and register state accessors.
// Accessors created here are passed into the IBot-derived class on every turn.
services.AddSingleton<EchoBotAccessors>(sp =>
{
    var options = sp.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;
    // ...
    var conversationState = options.State.OfType<ConversationState>().FirstOrDefault();
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

The Yeoman generator creates a type of [restify](http://restify.com/) web application. If you look at the restify quickstart in their docs, you'll see an app similar to the generated **index.js** file. This section mainly describes the **package.json**, **.env** , **index.js**, **bot.js**, and **echobot-with-counter.bot** files. Code in some files won't be copied here, but you will see it when you run the bot, and you can refer to the [Node.js echobot-with-counter](https://aka.ms/js-echobot-with-counter) sample.

### package.json

**package.json** specifies dependencies and their associated versions for your bot. This is all set up by the template and your system.

### .env file

The **.env** file specifies the configuration information for your bot, such as the port number, app ID, and password among other things. If using certain technologies or using this bot in production, you will need to add your specific keys or URL to this configuration. For this Echo bot, however, you don't need to do anything here right now; the app ID and password may be left undefined at this time.

To use the **.env** configuration file, the template needs an extra package included.  First, get the `dotenv` package from npm:

`npm install dotenv`

### index.js

The `index.js` sets up your bot and the hosting service that will forward activities to your bot logic.

#### Required libraries

At the very top of your `index.js` file you will find a series of modules or libraries that are being required. These modules will give you access to a set of functions that you may want to include in your application.

```javascript
// Import required packages
const path = require('path');
const restify = require('restify');

// Import required bot services. See https://aka.ms/bot-services to learn more about the different parts of a bot.
const { BotFrameworkAdapter, ConversationState, MemoryStorage } = require('botbuilder');
// Import required bot configuration.
const { BotConfiguration } = require('botframework-config');

const { EchoBot } = require('./bot');

// Read botFilePath and botFileSecret from .env file
// Note: Ensure you have a .env file and include botFilePath and botFileSecret.
const ENV_FILE = path.join(__dirname, '.env');
const env = require('dotenv').config({ path: ENV_FILE });
```

#### Bot configuration

The next part loads information from your bot configuration file.

```javascript
// Get the .bot file path
// See https://aka.ms/about-bot-file to learn more about .bot file its use and bot configuration.
const BOT_FILE = path.join(__dirname, (process.env.botFilePath || ''));
let botConfig;
try {
    // Read bot configuration from .bot file.
    botConfig = BotConfiguration.loadSync(BOT_FILE, process.env.botFileSecret);
} catch (err) {
    console.error(`\nError reading bot file. Please ensure you have valid botFilePath and botFileSecret set for your environment.`);
    console.error(`\n - The botFileSecret is available under appsettings for your Azure Bot Service bot.`);
    console.error(`\n - If you are running this bot locally, consider adding a .env file with botFilePath and botFileSecret.`);
    console.error(`\n - See https://aka.ms/about-bot-file to learn more about .bot file its use and bot configuration.\n\n`);
    process.exit();
}

// For local development configuration as defined in .bot file
const DEV_ENVIRONMENT = 'development';

// Define name of the endpoint configuration section from the .bot file
const BOT_CONFIGURATION = (process.env.NODE_ENV || DEV_ENVIRONMENT);

// Get bot endpoint configuration by service name
// Bot configuration as defined in .bot file
const endpointConfig = botConfig.findServiceByNameOrId(BOT_CONFIGURATION);
```

#### Bot adapter, HTTP server, and bot state

The next parts set up the server and adapter that allow your bot to communicate with the user and send responses. The server will listen on the specified port from the **BotConfiguration.bot** configuration file, or fall back to _3978_ for connection with your emulator. The adapter will act as the conductor for your bot, directing incoming and outgoing communication, authentication, and so on.

We also create a state object that uses `MemoryStorage` as the storage provider. This state is defined as `ConversationState`, which just means it's keeping the state of your conversation. `ConversationState` will store the information you're interested in, which in this case is simply a turn counter, in memory.

```javascript
// Create bot adapter.
// See https://aka.ms/about-bot-adapter to learn more about bot adapter.
const adapter = new BotFrameworkAdapter({
    appId: endpointConfig.appId || process.env.microsoftAppID,
    appPassword: endpointConfig.appPassword || process.env.microsoftAppPassword
});

// Catch-all for any unhandled errors in your bot.
adapter.onTurnError = async (context, error) => {
    // This check writes out errors to console log .vs. app insights.
    console.error(`\n [onTurnError]: ${ error }`);
    // Send a message to the user
    context.sendActivity(`Oops. Something went wrong!`);
    // Clear out state
    await conversationState.clear(context);
    // Save state changes.
    await conversationState.saveChanges(context);
};

// Define a state store for your bot. See https://aka.ms/about-bot-state to learn more about using MemoryStorage.
// A bot requires a state store to persist the dialog and user state between messages.
let conversationState;

// For local development, in-memory storage is used.
// CAUTION: The Memory Storage used here is for local bot debugging only. When the bot
// is restarted, anything stored in memory will be gone.
const memoryStorage = new MemoryStorage();
conversationState = new ConversationState(memoryStorage);

// Create the main dialog.
const bot = new EchoBot(conversationState);

// Create HTTP server
let server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function() {
    console.log(`\n${ server.name } listening to ${ server.url }`);
    console.log(`\nGet Bot Framework Emulator: https://aka.ms/botframework-emulator`);
    console.log(`\nTo talk to your bot, open echoBot-with-counter.bot file in the Emulator`);
});
```

#### Bot Logic

The adapter's `processActivity` sends incoming activities to the bot logic.
The third parameter within `processActivity` is a function handler that will be called to perform the bot’s logic after the received [activity](#the-activity-processing-stack) has been pre-processed by the adapter and routed through any middleware. The turn context variable, passed as an argument to the function handler, can be used to provide information about the incoming activity, the sender and receiver, the channel, the conversation, etc. Activity processing is routed to the EchoBot's `onTurn`.

```javascript
// Listen for incoming requests.
server.post('/api/messages', (req, res) => {
    // Route received request to adapter for processing
    adapter.processActivity(req, res, (context) => {
        // Route to main dialog.
        await bot.onTurn(context);
    });
});
```

### EchoBot

All activity processing is routed to this class's `onTurn` handler. When the class is created, a state object is passed in. Using this state object, the constructor creates a `this.countProperty` accessor to persist the turn counter for this bot.

On each turn, we first check to see if the bot has received a message. If the bot did not receive a message, we will echo back the activity type received. Next, we create a state variable that holds the information of your bot's conversation. If the count variable is `undefined`, it is set to 1 (which will occur when your bot first starts) or increment it with every new message. We echo back to the user the count along with the message they sent. Finally, we set the count and save the changes to state.

```javascript
const { ActivityTypes } = require('botbuilder');

// Turn counter property
const TURN_COUNTER_PROPERTY = 'turnCounterProperty';

class EchoBot {

    constructor(conversationState) {
        // Creates a new state accessor property.
        // See https://aka.ms/about-bot-state-accessors to learn more about the bot state and state accessors
        this.countProperty = conversationState.createProperty(TURN_COUNTER_PROPERTY);
        this.conversationState = conversationState;
    }

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

The **.bot** file contains information, including the endpoint, app ID, and password, and references to services that are used by the bot. This file gets created for you when you start building a bot from a template, but you can create your own through the emulator or other tools. You can specify the .bot file to use when testing your bot with the [emulator](../bot-service-debug-emulator.md).

```json
{
    "name": "echobot-with-counter",
    "services": [
        {
            "type": "endpoint",
            "name": "development",
            "endpoint": "http://localhost:3978/api/messages",
            "appId": "",
            "appPassword": "",
            "id": "1"
        }
    ],
    "padlock": "",
    "version": "2.0"
}
```

## Additional resources

For more information about state management, see [how to manage conversation and user state](bot-builder-howto-v4-state.md)

## Next steps

> [!div class="nextstepaction"]
> [Create a bot](~/bot-service-quickstart.md)
