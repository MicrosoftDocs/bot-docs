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


<!-- 
A bot may have to save state information, which is information it remembers in order to respond appropriately to incoming messages. The Bot Builder SDK provides classes for storing and retrieving state data as a set of properties associated with a user or a conversation. These properties are saved as key-value pairs. 

* Conversation properties help your bot keep track of the current conversation the bot is having with the user. If your bot completes a sequence of steps or switches between conversation topics, you can use conversation properties to manage steps in a sequence or track the current topic. Since conversation properties reflect the state of the current conversation, you typically clear them at the end of a session, when the bot receives an `endConversation` activity.
* User properties can be used for many purposes, such as determining where the user's prior conversation left off or simply greeting a returning user by name. If you store a user's preferences, you can use that information to customize the conversation the next time you chat. For example, you might alert the user to a news article about a topic that interests her, or alert a user when an appointment becomes available.  
-->


For your bot to save conversation and user state, first initialize state manager middleware, and then use the conversation and user state properties. If you need more background on why you use conversation and user properties, see [Saving state](./bot-builder-storage-concept.md) concept topic

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

In the line `options.Middleware.Add(new ConversationState<EchoState>(new MemoryStorage()));`, `ConversationState` is the conversation state manager object, which is added to the bot as middleware. The `EchoState` type parameter is the type representing how you want conversation state info to be stored. A bot can use any class type for conversation or user state data.

The implementation of `EchoState` is in `EchoBot.cs`:
```csharp

    public class EchoState : StoreItem
    {
        public int TurnNumber { get; set; }
    }
``` 

# [JavaScript](#tab/jsmemorymiddleware)

```javascript
const { BotFrameworkAdapter, FileStorage, MemoryStorage, ConversationState } = require('botbuilder');

// ...

// Create adapter
const adapter = new BotFrameworkAdapter({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

// Add conversation state middleware
const conversationState = new ConversationState(new MemoryStorage());


const conversationState = new ConversationState(fs);
adapter.use(conversationState);
```    
---

> [!NOTE] 
> In-memory data storage is intended for testing only. This storage is volatile and temporary. The data is cleared each time the bot is restarted. See [File storage](#file-storage) and [Azure table storage](#azure-table-storage) later in this article to set up other underlying storage mediums for conversation state and user state. 

---

### Configuring state manager middleware

When initializing the `ConversationState` and `UserState` middleware, an optional `StateSettings` parameter allows you to change the default behavior of how properties are saved. The default settings are:

* persistUserState/persistConversationState: Properties are persisted beyond the lifetime of the turn context.
* lastWriterWins: If more than one instance of the bot writes to a property, allow the last instance of the bot to overwrite the previous one.


## Use conversation and user state properties 
<!-- middleware and message context -->

Once the state manager middleware has been configured, you can get the conversation state and user state properties from the context object.
<!-- Changes are written to storage before the `SendActivity()` pipeline completes. -->

# [C#](#tab/csharppropertysnippet)

You can see how this works using the `Microsoft.Bot.Samples.EchoBot` sample in the Bot Builder SDK. 

In the `OnReceiveActivity` handler, `context.GetConversationState` gets the conversation state to access the data you defined, and you can modify the state by using the properties (in this case, incrementing `TurnNumber`).

```csharp


public async Task OnReceiveActivity(IBotContext context)
        {
            var msgActivity = context.Request.AsMessageActivity();
            
            if (msgActivity != null)
            {

                // The type parameter to GetConversationState specifies the type of the property to retrieve

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

// Listen for incoming activity 
server.post('/api/messages', (req, res) => {
    // Route received activity to adapter for processing
    adapter.processActivity(req, res, async (context) => {
        if (context.activity.type === 'message') {
            const state = conversationState.get(context);
            const count = state.count === undefined ? state.count = 0 : ++state.count;
            await context.sendActivity(`${count}: You said "${context.activity.text}"`);
        } else {
            await context.sendActivity(`[${context.activity.type} event detected]`);
        }
    });
});
```
---

## Using conversation state to direct conversation flow

<!-- add example of Username and Prompted for conversation flow -->
A simple way to track such a conversation is to use a flag to mark where you are in the conversation. When the flag is false, the question has not been asked yet; when true, the question has been asked, and the next response should contain the answer.

In the following example, the `promptedForName` property is set when the bot asks the user for their name. When the next message is received, the bot checks the property. If the property is set to true, the bot knows the user was just asked for their name, and interprets the incoming message as a name to save as a user property.


# [C#](#tab/csFlag)
```csharp
// haveAskedNameFlag is a state property telling
// whether we have asked for the user's name yet.

public class StateInfo 
{
    public bool haveAskedNameFlag  { get; set; }
    public bool haveAskedNumberFlag  { get; set; }
}

public class UserInfo
{
    public string name { get; set; }
    public string telephoneNumber { get; set; }
    public bool done { get; set; }
}

public async Task OnReceiveActivity(IBotContext context)
{            
    if (context.Request.Type == ActivityTypes.Message)
    {        
        
        var conversationState = context.GetConversationState<StateInfo>() ?? new StateInfo();
        var userState = context.GetUserState<UserInfo>();
        
        if (conversationState.haveAskedNameFlag == false && String.IsNullOrEmpty(userState.name))
        {
            // Ask for the name.
            await context.SendActivity("Hello. What's your name?");
                                            
            // Set flag to show we've asked for the name. We save this out so the
            // context object for the next turn of the conversation can check haveAskedName
            conversationState.haveAskedNameFlag = true;
        }
        else if (conversationState.haveAskedNumberFlag == false && !userState.done)
        {
            // Save the name.
            var name = context.Request.AsMessageActivity().Text;                    
            userState.name = name;

            // Ask for the phone number. You might want a flag to track this, too.
            await context.SendActivity($"Hello, {name}. What's your telephone number?");
            conversationState.haveAskedNumberFlag = true;
        } else if (conversationState.haveAskedNumberFlag == true && !userState.done)
        {
            // save the telephone number
            var telephonenumber = context.Request.AsMessageActivity().Text;
            
            userState.telephoneNumber = telephonenumber;
            userState.done = true;
            
        }
    }

// ...
}
```

To set up user state so that it can be returned by `context.GetConversationState`, you add user state middleware. For example, in `Startup.cs` of the ASP .NET Core EchoBot, changing the code in ConfigureServices.cs:

```csharp
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddBot<EchoBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);                

                options.Middleware.Add(new ConversationState<StateInfo>(new MemoryStorage()));
                options.Middleware.Add(new UserState<UserInfo>(new MemoryStorage()));

                options.EnableProactiveMessages = true;
            });
            
            
            services.AddTransient<IMyService, MyService>();
        }
```


# [JavaScript](#tab/jsflag)

```js
const { BotFrameworkAdapter, MemoryStorage, ConversationState } = require('botbuilder');
const restify = require('restify');

// Create server
let server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log(`${server.name} listening to ${server.url}`);
});

// Create adapter (it's ok for MICROSOFT_APP_ID and MICROSOFT_APP_PASSWORD to be blank for now)  
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
    adapter.processActivity(req, res, async (context) => {
        if (context.activity.type === 'message') {
            const state = conversationState.get(context);

            if(conversationState.haveAskedNameFlag === undefined){
                // Ask for the name.
                await context.sendActivity("What is your name?")
                // Set flag to show we've asked for the name. We save this out so the
                // context object for the next turn of the conversation can check haveAskedName
                conversationState.haveAskedNameFlag = true;
            } else if(conversationState.haveAskedNameFlag === true && state.name === undefined){
                // Save the name.
                var name = context.activity.text;                    
                state.name = name;

                // Ask for the phone number. You might want a flag to track this, too.
                await context.sendActivity(`Hello, ${name}. What's your telephone number?`);
                conversationState.haveAskedNumberFlag = true;
            } else if(conversationState.haveAskedNumberFlag === true){
                // save the phone number
                var telephonenumber = context.activity.text;

                state.telephonenumber = telephonenumber
            }
        }
    });
});

```

---

## File storage

If you're using `FileStorage` you can see how the conversation state data is serialized to JSON and saved to a file.

# [C#](#tab/csfileMiddleware)

Go to `Startup.cs` in the Microsoft.Bot.Samples.EchoBot-AspNetCore sample, and edit the code in the `ConfigureServices` method.
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


Run the code, and let the echobot echo back your input a few times.

Then go to the directory specified by `System.IO.Path.GetTempPath()`. You should see a file with a name starting with "conversation". Open it and look at the JSON it contains.
```json
{
  "$type": "Microsoft.Bot.Samples.Echo.EchoState, Microsoft.Bot.Samples.EchoBot",
  "TurnNumber": 3,
  "eTag": "ecfe2a23566b4b52b2fe697cffc59385"
}
```

The `$type` specifies the type of the data structure you're using in your bot to store conversation state. The `TurnNumber` field corresponds to the `TurnNumber` property in the `EchoState` class. The `eTag` field is inherited from `IStoreItem`, is a unique value that automatically gets updated each time your bot updates conversation state.  The eTag field enables your bot to enable optimistic concurrency.

# [JavaScript](#tab/jsfilemiddleware)
```javascript
const { BotFrameworkAdapter, FileStorage, MemoryStorage, ConversationState } = require('botbuilder');

// ...

// Create adapter
const adapter = new BotFrameworkAdapter({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

// Add conversation state middleware
var fs = new FileStorage({ path: "C:/temp" });
// note - constructor doesn't throw if path doesn't exist or no permissions

const conversationState = new ConversationState(fs);
adapter.use(conversationState);
```
---

## Azure table storage

---

You can also use Azure Table storage as your storage medium.

# [C#](#tab/csharpazuremiddleware)

In the Microsoft.Bot.Samples.EchoBot-AspNetCore sample, add a reference to `Microsoft.Bot.Builder.Azure`.

Then, go to `Startup.cs`, and edit the code in the `ConfigureServices` method.
```csharp
    services.AddBot<EchoBot>(options =>
    {
        options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);
        // The parameters are the connection string and table name.
        // "UseDevelopmentStorage=true" is the connection string to use if you are using the Azure Storage Emulator.
        // Replace it with your own connection string if you're not using the emulator
        options.Middleware.Add(new ConversationState<EchoState>(new AzureTableStorage("UseDevelopmentStorage=true","conversationstatetable")));
        // you could also specify the cloud storage account instead of the connection string
        /* options.Middleware.Add(new ConversationState<EchoState>(
            new AzureTableStorage(WindowsAzure.Storage.CloudStorageAccount.DevelopmentStorageAccount, "conversationstatetable"))); */
        options.EnableProactiveMessages = true;
    });
```
`UseDevelopmentStorage=true` is the connection string you can use with the [Azure Storage Emulator][AzureStorageEmulator]. Replace it with your own connection string if you're not using the emulator.

If the table with the name you specify in the constructor to `AzureTableStorage` doesn't exist, it is created.

<!-- 
TODO: step-by-step inspection of the stored table
-->

# [JavaScript](#tab/jsazuremiddleware)

In `app.js` of the echobot sample, you can create ConversationState using `AzureTableStorage`

```javascript
const { BotFrameworkAdapter, FileStorage, MemoryStorage, ConversationState } = require('botbuilder');
const { TableStorage } = require{'botbuilder-azure'};

// ...

// Create adapter
const adapter = new BotFrameworkAdapter({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

// Add conversation state middleware
// The parameters are the connection string and table name.
// "UseDevelopmentStorage=true" is the connection string to use if you are using the Azure Storage Emulator.
// Replace it with your own connection string if you're not using the emulator
var azureStorage = new TableStorage({ tableName: "TestAzureTable1", storageAccountOrConnectionString: "UseDevelopmentStorage=true"})

// You can alternatively use your account name and table name
// ar azureStorage = new TableStorage({tableName: "TestAzureTable2", storageAccessKey: "V3ah0go4DLkMtQKUPC6EbgFeXnE6GeA+veCwDNFNcdE6rqSVE/EQO/kjfemJaitPwtAkmR9lMKLtcvgPhzuxZg==", storageAccountOrConnectionString: "storageaccount"});


const conversationState = new ConversationState(azureStorage);
adapter.use(conversationState);

```

`UseDevelopmentStorage=true` is the connection string you can use with the [Azure Storage Emulator][AzureStorageEmulator].
If the table with the name you specify in the constructor to `AzureTableStorage` doesn't exist, it is created.

---

You can take a look at the conversation state data that is saved, by first running the sample and then opening the table using [Azure Storage explorer][AzureStorageExplorer]:

![echobot conversation state data in Azure Storage Explorer](media/how-to-state/echostate-azure-storage-explorer.png)


The **Partition Key** is a uniquely-generated key specific to the current conversation. If you restart the bot or start a new conversation, the new conversation will get its own row with its own partition key. The `EchoState` data structure is serialized to JSON and saved in the **Json** column of the Azure Table. 

```json
{
    "$type":"Microsoft.Bot.Samples.Echo.AspNetCore.EchoState, Microsoft.Bot.Samples.EchoBot-AspNetCore",
    "TurnNumber":2,
    "LastMessage":"second message",
    "eTag":"*"
}
```

The `$type` and `eTag` fields are added by the BotBuilder SDK. For more about eTags, see [Managing optimistic concurrency](bot-builder-how-to-v4-storage.md#manage-concurrency-using-etags)



## Next steps

To learn about how to write directly to underlying storage, see [Writing directly to storage](bot-builder-how-to-v4-storage.md).


## Additional resources

If you want to write directly to storage, instead of using the state manager, see [How to write directly to storage](./bot-builder-how-to-v4-storage.md).
For more background on storage, see [Storage in the Bot Builder SDK](./bot-builder-storage-concept.md)

<!-- Links -->
[AzureStorageEmulator]: https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator
[AzureStorageExplorer]: https://azure.microsoft.com/en-us/features/storage-explorer/
