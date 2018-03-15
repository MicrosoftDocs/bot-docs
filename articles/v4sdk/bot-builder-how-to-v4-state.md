---
title: Save state using conversation and user properties | Microsoft Docs
description: Learn how to save and retrieve data with V4 of the Bot Builder SDK for .NET.
author: RobStand
ms.author: v-demak
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 2/16/17
monikerRange: 'azure-bot-service-4.0'
---

# Save state using conversation and user properties
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-state.md)
> - [Node.js](../nodejs/bot-builder-nodejs-state.md)

<!--
 Note for V4: You can write directly to storage without using the state manager. Therefore, this topic isn't called "managing state". State is in a separate topic.
-->

<!-- 
A bot may have to save state information, which is information it remembers in order to respond appropriately to incoming messages. The Bot Builder SDK provides classes for storing and retrieving state data as a set of properties associated with a user or a conversation. These properties are saved as key-value pairs. 

* Conversation properties help your bot keep track of the current conversation the bot is having with the user. If your bot completes a sequence of steps or switches between conversation topics, you can use conversation properties to manage steps in a sequence or track the current topic. Since conversation properties reflect the state of the current conversation, you typically clear them at the end of a session, when the bot receives an `endConversation` activity.
* User properties can be used for many purposes, such as determining where the user's prior conversation left off or simply greeting a returning user by name. If you store a user's preferences, you can use that information to customize the conversation the next time you chat. For example, you might alert the user to a news article about a topic that interests her, or alert a user when an appointment becomes available.  
-->

For your bot to save conversation and user state, first initialize state manager middleware, and then use the conversation and user state properties.

> [!NOTE]
> For background on why you use conversation and user properties, see [Concept: Saving state](./bot-builder-v4-concept-storage.md).
<!--or a specific user within the context of a specific conversation.--> 


## Initialize state manager middleware
In the V4 SDK, you need to initialize the bot adapter to use a state manager as middleware before you can use the Conversation and User property stores.  `ConversationState` is used for conversation properties, and `UserState` is used for user properties. The state manager middleware provides an abstraction that lets you access properties using a simple key-value store, independent of the type of underlying storage. The state manager takes care of writing data to storage and managing concurrency, whether the underlying storage type is in-memory, file storage, or Azure table storage.


# [C#](#tab/csharp)
To see how `ConversationState` is initialized, see `Startup.cs` in the Microsoft.Bot.Samples.EchoBot-AspNetCore sample.

```csharp

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBot<EchoBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);

                // initialize a conversation state manager to save conversation state to memory storage
                options.Middleware.Add(new ConversationState<EchoState>(new MemoryStorage()));                
                options.EnableProactiveMessages = true;
            });

            services.AddTransient<IMyService, MyService>();
        }
```
# [JavaScript](#tab/jsmemorymiddleware)
```javascript
const bot = new Bot(adapter)
    .use(new MemoryStorage())
    .use(new ConversationStateManager())
```    
---

> [!NOTE] 
> In-memory data storage is intended for testing only. This storage is volatile and temporary. The data is cleared each time the bot is restarted. 

<!-- 
To use the in-memory storage for testing purposes, create a new instance of the in-memory storage, and use it to instantiate `ConversationStateManagerMiddleware`:

```csharp
// In-memory storage
storage = new MemoryStorage();

// initialize a bot state manager
bot = new Builder.Bot(activityAdapter)
        .Use(new BotStateManager(storage))
```
-->

File storage is also intended for testing. This stores data in a file on the machine running the bot.

# [C#](#tab/csfileMiddleware)
```csharp
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBot<EchoBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);
                
                // initialize a conversation state manager to save conversation state to file storage
                options.Middleware.Add(new ConversationState<EchoState>(new FileStorage(System.IO.Path.GetTempPath())));                
                options.EnableProactiveMessages = true;
            });

            services.AddTransient<IMyService, MyService>();
        }

```        
# [JavaScript](#tab/jsfilemiddleware)
```javascript
const bot = new Bot(adapter)
    .use(new FileStorage("C:\temp"))
    .use(new ConversationStateManager())
```
---

You can use also use Azure Table storage.

# [C#](#tab/csharpazuremiddleware)


Go to `Startup.cs` in the Microsoft.Bot.Samples.EchoBot-AspNetCore sample, and edit the code in the `ConfigureServices` method.
```csharp
    services.AddBot<EchoBot>(options =>
    {
                options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);
                // "UseDevelopmentStorage=true" is the connection string and "conversationstatetable" is the table name.
                options.Middleware.Add(new ConversationState<EchoState>(new AzureTableStorage("UseDevelopmentStorage=true","conversationstatetable")));
                // you could also specify the cloud storage account instead of the connection string
                /* options.Middleware.Add(new ConversationState<EchoState>(
                    new AzureTableStorage(WindowsAzure.Storage.CloudStorageAccount.DevelopmentStorageAccount, "demo96b83"))); */
                options.EnableProactiveMessages = true;
    });
```
If the table doesn't exist, it is created.

<!-- 
TODO: step-by-step inspection of the stored table
-->

# [JavaScript](#tab/jsazuremiddleware)
```javascript
const bot = new Bot(adapter)
    // "UseDevelopmentStorage=true" is the connection string and "conversationstatetable" is the table name.
    .use(new AzureTableStorage("UseDevelopmentStorage=true","conversationstatetable"))
    .use(new ConversationStateManager())
```
---

### Configuring state manager middleware
When initializing the `ConversationState` and `UserState` middleware, an optional `StateSettings` parameter allows you to change the default behavior of how properties are saved. The default settings are:

* persistUserState/persistConversationState: Properties are persisted beyond the lifetime of the context object.
* lastWriterWins: If more than one instance of the bot writes to a property, allow the last instance of the bot to overwrite the previous one.
* writeBeforeSend: Write data to storage before the SendActivity step in the message pipeline.

> [!NOTE] 
> These are default values only for ConversationState and UserState. If you're creating your own middleware to write state information to `MemoryStorage`, `FileStorage`, or `AzureTableStorage`, your implementation should specify those values.

## Use conversation and user state properties 
<!-- middleware and message context -->

Once the state manager middleware has been configured, you can get the conversation state and user state properties from the context object.
<!-- Changes are written to storage before the `SendActivity()` pipeline completes. -->

# [C#](#tab/csharppropertysnippet)
You can try out this out using the `Microsoft.Bot.Samples.EchoBot` sample in the Bot Builder SDK. In `WebApiConfig.cs` in the sample, the following line initializes the `ConversationStateManagerMiddleWare`:


`.Use(new ConversationStateManagerMiddleware(new MemoryStorage()));`

In `EchoBot.cs`, the following class defines the data type to store in the conversation state: 

```csharp

    public class EchoState : StoreItem
    {

        public int TurnNumber { get; set; }
    }
```

In the `OnReceiveActivity` handler, `context.GetConversationState` gets the conversation state to access the data you defined:

```csharp

public async Task OnReceiveActivity(IBotContext context)
        {
            var msgActivity = context.Request.AsMessageActivity();
            
            if (msgActivity != null)
            {
                var conversationState = context.GetConversationState<EchoState>() ?? new EchoState();

                conversationState.TurnNumber++;

                // calculate something for us to return
                int length = (msgActivity.Text ?? string.Empty).Length;

                // simulate calling a dependent service that was injected
                await _myService.DoSomethingAsync();

                // return our reply to the user
                context.Reply($"[{conversationState.TurnNumber}] You sent {msgActivity.Text} which was {length} characters");
            }
            
            var convUpdateActivity = context.Request.AsConversationUpdateActivity();
            if (convUpdateActivity != null)
            {
                foreach (var newMember in convUpdateActivity.MembersAdded)
                {
                    if (newMember.Id != convUpdateActivity.Recipient.Id)
                    {
                        context.Reply("Hello and welcome to the echo bot.");
                    }
                }
            }
        }
```   

# [JavaScript](#tab/jspropertysnippet)

Paste the code below into a file called app.js:

```javascript
const { BotFrameworkAdapter, MemoryStorage, ConversationState } = require('botbuilder');
const restify = require('restify');

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

// Add conversation state middleware
const conversationState = new ConversationState(new MemoryStorage());

adapter.use(conversationState);

// Listen for incoming requests 
server.post('/api/messages', (req, res) => {
    // Route received request to adapter for processing
    adapter.processRequest(req, res, (context) => {
        if (context.request.type === 'message') {
            const state = conversationState.get(context);
            const count = state.count === undefined ? state.count = 0 : ++state.count;
            return context.sendActivity(`${count}: You said "${context.request.text}"`);
        } else {
            return context.sendActivity(`[${context.request.type} event detected]`);
        }
    });
});
```
---
In the example, the `promptedForName` property is set when the bot asks the user for their name. Then when the next message is received, the bot checks the property. If the property is set to true, the bot knows the user was just asked for their name, and interprets the incoming message as a name, to save in `context.State.UserProperties`.




## Next steps

- [Concept: Storage in the Bot Builder SDK](./bot-builder-v4-concept-storage.md)


## Additional resources

If you want to write directly to storage, instead of using the state manager, see [How to write directly to storage](./bot-builder-how-to-v4-storage.md).