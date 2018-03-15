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

<!--or a specific user within the context of a specific conversation.--> 


## Initialize state manager middleware
In the V4 SDK, you need to initialize the bot adapter to use a state manager as middleware before you can use the Conversation and User property stores.  `ConversationStateManagerMiddleware` is used for conversation properties, and `UserStateManagerMiddleware` is used for user properties. The state manager middleware provides an abstraction that lets you access properties using a simple key-value store, independent of the type of underlying storage. The state manager takes care of writing data to storage and managing concurrency, whether the underlying storage type is in-memory, file storage, or Azure table storage.

# [C#](#tab/csharp)
```csharp
// initialize a bot adapter to save conversation state to memory storage
var botadapter = new Bot.Builder.Adapters.BotFrameworkAdapter("", "") // blank AppID and password
      .Use(new Bot.Builder.Middleware.ConversationStateManagerMiddleware(new MemoryStorage()));
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
// initialize a bot adapter to save conversation state to file storage
var botadapter = new Bot.Builder.Adapters.BotFrameworkAdapter("", "") // blank AppID and password
        .Use(new ConversationStateManagerMiddleware(new FileStorage(System.IO.Path.GetTempPath())));
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
```csharp
// initialize a bot adapter to save conversation state to Azure Table storage
var botadapter = new Bot.Builder.Adapters.BotFrameworkAdapter("", "") // blank AppID and password
        .Use(new ConversationStateManagerMiddleware(new Bot.Builder.Azure.AzureTableStorage(
            ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString, ConfigurationManager.TableName))));
```
# [JavaScript](#tab/jsazuremiddleware)
```javascript
const bot = new Bot(adapter)
    .use(new AzureTableStorage("connection string", "table name"))
    .use(new ConversationStateManager())
```
---

### Configuring state manager middleware
When initializing the middleware, an optional `StateManagerMiddlewareSettings` parameter allows you to change the default behavior of how properties are saved. The default settings are:

* persistUserState/persistConversationState: Properties are persisted beyond the lifetime of the context object.
* lastWriterWins: If more than one instance of the bot writes to a property, allow the last instance of the bot to overwrite the previous one.
* writeBeforeSend: Write data to storage before the SendActivity step in the message pipeline.

## Use conversation and user state properties 
<!-- middleware and message context -->

Once the state manager middleware has been configured, you can access properties using the context object: `context.State.UserProperties` or `context.State.ConversationProperties`.
<!-- Changes are written to storage before the `SendActivity()` pipeline completes. -->

# [C#](#tab/csharppropertysnippet)
You can try out this out using the `Microsoft.Bot.Samples.EchoBot_AspNet461` sample in the Bot Builder SDK. In `WebApiConfig.cs` in the sample, the following line initializes the `ConversationStateManagerMiddleWare`:


`.Use(new ConversationStateManagerMiddleware(new MemoryStorage()));`

In `MessagesController.cs`, replace the `OnReceiveActivity` method with the following code: 

```csharp

protected async override Task OnReceiveActivity(IBotContext context)
{
    var msgActivity = context.Request.AsMessageActivity();
    if (msgActivity != null)
    {

        // State.ConversationProperties["promptedForName"] is a flag for indicating that the bot just prompted the user for their name.
        // If the flag is null, the user hasn't been prompted before, so ask them for their name.
        if (context.State.ConversationProperties["promptedForName"] == null)
        {
            // Prompt user for name
            context.Reply("Hi. What's your name?");
            // Set flag to show that the bot has prompted for the user's name
            context.State.ConversationProperties["promptedForName"] = true;
        }
        else if (context.State.ConversationProperties["promptedForName"] == true)
        {
            // If the "promptedForName" property is true, the bot interprets the current message text 
            // as a response to the name prompt            
            var name = context.Request.AsMessageActivity().Text;
            context.State.UserProperties["name"] = name;

            // Greet user by name
            context.Reply("Nice to meet you, ${name}.");

            // set the "promptedForName" property to false, so the next message that arrives won't be treated as a name response.
            context.State.ConversationProperties["promptedForName"] = false;
        }

    }

}
            
```
# [JavaScript](#tab/jspropertysnippet)

Paste the code below into a file called app.js:

```javascript
// Node.js snippet updates are TBD

const { Bot } = require('botbuilder');
const { BotFrameworkAdapter } = require('botbuilder-services');
const restify = require('restify');

// Create server
let server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log(`${server.name} listening to ${server.url}`);
});

// Create adapter and listen to servers '/api/messages' route.
const adapter = new BotFrameworkAdapter({ 
    appId: process.env.MICROSOFT_APP_ID, 
    appPassword: process.env.MICROSOFT_APP_PASSWORD 
});
server.post('/api/messages', adapter.listen());

// Initialize bot and define the bots onReceive message handler
const bot = new Bot(adapter)
    .use(new MemoryStorage())
    .use(new BotStateManager())
    .onReceive((context) => {
        if (context.request.type === 'message') {
            if (context.state.conversation.promptedForName === undefined) {
                // Prompt user for their name
                context.state.conversation.promptedForName = true;
                context.reply(`Hi. What's your name?`);
            } else {
                // Save their answer
                const name = context.request.text;
                context.state.user.name = name;
                context.state.conversation.promptForName = undefined;
                context.reply(`Nice to meet you ${name}`);
            }
        }
    });
```
---
In the example, the `promptedForName` property is set when the bot asks the user for their name. Then when the next message is received, the bot checks the property. If the property is set to true, the bot knows the user was just asked for their name, and interprets the incoming message as a name, to save in `context.State.UserProperties`.




## Next steps

- [Concept: Storage in the Bot Builder SDK](./bot-builder-v4-concept-storage.md)


## Additional resources

If you want to write directly to storage, instead of using the state manager, see [How to write directly to storage](./bot-builder-how-to-v4-storage.md).