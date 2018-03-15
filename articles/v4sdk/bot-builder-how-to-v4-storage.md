---
title: Store data | Microsoft Docs
description: Learn how to write directly to storage with V4 of the Bot Builder SDK for .NET.
author: RobStand
ms.author: v-demak
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/16/18
monikerRange: 'azure-bot-service-4.0'
---

# Save data directly to storage

<!--
 Note for V4: You can write directly to storage without using the state manager. Therefore, this topic isn't called "managing state". State is in a separate topic.
 ## Manage state data by writing directly to storage
-->

You can write directly to storage without using the context object or middleware, by using `IStorage.Write` and `IStorage.Read`.

This code example shows you how to read and write data to storage. In this example the storage is a file, but you can easily change the code to initialze the storage object to use `MemoryStorage` or `AzureTableStorage` instead. 

# [C#](#tab/csharpechorproperty)
To store data that you access using `Storage.Write` and `Storage.Read`, you can use the `StoreItems` and `StoreItem` classes.

```csharp
// Start with the EchoBot sample in the BotBuilder V4 SDK and edit the code to match the following

// In the constructor initialize file storage
    public class EchoBot : IBot
    {
        private readonly FileStorage _myStorage;

        public EchoBot()
        {
            _myStorage = new FileStorage(System.IO.Path.GetTempPath());
        }

// Add a class for storing a log of utterances (text of messages) as a list
        public class UtteranceLog : StoreItem
        {
            // A list of things that users have said to the bot
            public List<string> UtteranceList { get; private set; } = new List<string>();    
            // The number of conversational turns that have occurred        
            public int TurnNumber { get; set; } = 0;
        }

// Replace the OnReceiveActivity in EchoBot.cs with the following:
        // added 'async'
        public async Task OnReceiveActivity(IBotContext context)
        {
            var activityType = context.Request.Type;

            context.Reply($"Activity type: {context.Request.Type}.");

            if (activityType == ActivityTypes.Message)
            {
                // *********** begin (create or add to log of messages)
                var utterance = context.Request.Text;
                bool restartList = false;

                if (utterance.Equals("restart"))
                {
                    restartList = true;
                }

                // Attempt to read the existing property bag
                StoreItems<Bot.UtteranceLog> logItems = null;
                try
                {
                    logItems = await _myStorage.Read<Bot.UtteranceLog>("UtteranceLog");
                    // returns a new StoreItems if no log found.
                }
                catch (System.Exception ex)
                {
                    context.Reply(ex.Message);
                   
                }
                
                // If the property bag wasn't found, create a new one
                if (!logItems.ContainsKey("UtteranceLog"))
                {
                    try
                    {
                        logItems = new StoreItems<Bot.UtteranceLog>();
                        var newLog = new UtteranceLog();
                        // add the current utterance to the new list.
                        newLog.UtteranceList.Add(utterance);
                        logItems.Add("UtteranceLog", newLog);
                        context.Reply($"creating log with \"{utterance}\"");
                        await _myStorage.Write(logItems);
                    }
                    catch (System.Exception ex)
                    {
                        context.Reply(ex.Message);
                    }


                }
                // logItems.ContainsKey("UtteranceLog") == true, we were able to read a log from storage
                else 
                {
                    // Modify its property
                    if (restartList)
                    {
                        logItems["UtteranceLog"].UtteranceList = new List<String>();
                    } else
                    {
                        logItems["UtteranceLog"].UtteranceList.Add(utterance);
                        logItems["UtteranceLog"].TurnNumber++;
                    }

                    context.Reply($"logging \"{utterance}\"");
                    await _myStorage.Write(logItems);


                }
            }

            return;
        }
    }
```
# [JavaScript](#tab/jsweatherproperty)

``` javascript
// paste the following into app.js
const { BotFrameworkAdapter, FileStorage, MemoryStorage, ConversationState } = require('botbuilder');
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
// const conversationState = new ConversationState(new MemoryStorage());
var fs = new FileStorage({ path: "C:/Users/v-demak/Documents/a-temp" });
// note - constructor doesn't throw if path DNE or no permissions

const conversationState = new ConversationState(fs);
adapter.use(conversationState);

// Listen for incoming requests 
server.post('/api/messages', (req, res) => {
    // Route received request to adapter for processing
    adapter.processRequest(req, res, (context) => {
        if (context.request.type === 'message') {
            const state = conversationState.get(context);
            const count = state.count === undefined ? state.count = 0 : ++state.count;


            let utterance = context.request.text;
            let logItems = fs.read(["UtteranceLog2"])
                .then((storeItems) => {
                    // check result
                    console.log(`Just read StoreItems[UtteranceLog] from file: ${JSON.stringify(storeItems)}`);
                    var utteranceLog = storeItems["UtteranceLog2"];
                    console.log(`utteranceLog: ${JSON.stringify(utteranceLog)}`);

                    if (typeof (utteranceLog) != 'undefined') {
                        // log exists so we can write to it

                        storeItems["UtteranceLog2"].UtteranceList.push(utterance);
                        console.log(`Updated StoreItems[UtteranceLog] with additional utterance: ${JSON.stringify(storeItems)}`);

                        fs.write(storeItems) 
                            .then(() => { console.log('successful write.') },
                                (err) => { console.log(`write failed of UtteranceLog: ${err.message}`) }
                            );

                    } else {
                        console.log(`need to create new utterance log`);
                        storeItems["UtteranceLog2"] = { UtteranceList: [`${utterance}`], "eTag": "*" }
                        fs.write(storeItems)
                            .then(() => { console.log('successful write.') },
                                (err) => { console.log(`write failed: ${err.errno}`) }
                            );
                    }
                }, (reason) => {
                    console.log("Read rejected.")
                    // rejected
                });

            return context.sendActivity(`${count}: You said "${context.request.text}"`);
        } else {
            return context.sendActivity(`[${context.request.type} event detected]`);
        }
    });
});
```
---
<!-- 
# [C#](#tab/csharpweatherproperty)
To store data that you access using `Storage.Write` and `Storage.Read`, you use the `StoreItems` and `StoreItem` classes.

```csharp
// Start with the EchoBot_AspNet461 sample in the BotBuilder V4 SDK,
// and replace the code in MessagesController.cs with the following:
namespace Microsoft.Bot.Samples.EchoBot_AspNet461
{
    public class MessagesController : BotController
    {
        IStorage storage;

        public MessagesController(BotFrameworkAdapter adapter) : base(adapter)
        {
            // initialize the storage
            storage = new FileStorage(System.IO.Path.GetTempPath());
        }

        protected async override Task OnReceiveActivity(IBotContext context)
        {
            var msgActivity = context.Request.AsMessageActivity();
            if (msgActivity != null)
            {

                // get the text of the message 
                var utterance = context.Request.AsMessageActivity().Text.Trim().ToLower();



                if (string.Equals("get weather", utterance))
                {
                    // read weather report out of storage
                    StoreItems weatherProperties = await storage.Read("WeatherReports");
                    
                    if  (weatherProperties.ContainsKey("WeatherReports"))
                    {
                        var todaysWeather = weatherProperties["WeatherReports"]["today"];
                        context.Reply($"{todaysWeather}");
                    }
                    else
                    { // Create key-value store weatherReports[WeatherReports][today], storing today's weather
                        StoreItem weatherReports = new StoreItem
                        {
                            eTag = "*"
                        };
                        weatherReports.Add("today", "No weather report yet."); // hard code placeholder string for weather report                        
                        weatherProperties.Add("WeatherReports", weatherReports);

                        await storage.Write(weatherProperties);
                        context.Reply("We don't have data for today's weather yet. Can you look out the window and tell me, what the weather looks like now?");
                        context.State.ConversationProperties["promptedForWeather"] = true;
                    }
                }
                else if (string.Equals("set weather", utterance))
                {
                    // read weather report out of storage
                    StoreItems weatherProperties = await storage.Read("WeatherReports");
                    context.State.ConversationProperties["promptedForWeather"] = true;
                    context.Reply("ok, what's the weather look like?");
                }

                else
                {
                    if (context.State.ConversationProperties["promptedForWeather"]??false)
                    {
                        context.State.ConversationProperties["promptedForWeather"] = false;
                        // read weather report out of storage
                        StoreItems weatherProperties = await storage.Read("WeatherReports");
                        weatherProperties["WeatherReports"]["today"] = utterance;
                        await storage.Write(weatherProperties);
                    }
                }

            }

            var convUpdateActivity = context.Request.AsConversationUpdateActivity();
            if (convUpdateActivity != null)
            {
                foreach (var newMember in convUpdateActivity.MembersAdded)
                {
                    if (newMember.Id != convUpdateActivity.Recipient.Id)
                    {
                        context.Reply("Hello and welcome to the weather bot. Say 'get weather' to get the weather report.");
                    }
                }

            }

        }
    }
}
```
# [JavaScript](#tab/jsweatherproperty)
```
// Node.js snippet TBD
```
---
-->

## Manage concurrency using entity tags (eTags)

In the previous example, you set the `eTag` property to `*`. The `eTag` (entity tag) member of the `StoreItem` class is for managing concurrency. The `eTag` indicates what to do if another instance of the bot has changed the `StoreItem` that your bot is writing to. 

<!-- define optimistic concurrency -->

### Last write wins - allow overwrites

Set the eTag to `*` to allow other instances of the bot to overwrite previously written data. An `eTag` property value of asterisk (`*`) indicates that the last writer wins. When creating a new data store, you can set `eTag` of a property to `*` to indicate that you have not previously saved the data that you are writing, or that you want the last writer to overwrite any previously saved property. If concurrency is not an issue for your bot, setting the `eTag` property to `*` for any data that you are writing will allow overwrites.

This is shown in the following code example.

# [C#](#tab/csetagoverwrite)
```csharp
StoreItems dataStore = new StoreItems();

// create a set of properties for the first time
StoreItem note = new StoreItem();
// Set the eTag of the property to *
note.eTag("*");
note.Add("NoteName", "Shopping List");
note.Add("NoteContents", "eggs");
dataStore["Note1"] = note;
await storage.Write(dataStore).ConfigureAwait(false);

```
# [JavaScript](#tab/jstagoverwrite)
```csharp
// Node.js snippet TBD
// In Node.js, etag=* by default

```
---

### Maintain concurrency and prevent overwrites
If you want to prevent concurrent access to a property, so that if another instance of the bot has changed the data, your bot doesn't overwrite the changes. Instead the bot receives an error response with the message `etag conflict key=` when it attempts to save state data. <!-- To control concurrency of data that is stored using `IStorage`, the BotBuilder SDK checks the entity tag (ETag) for `Storage.Write()` requests. -->


By default, the `eTag` property of a `StoreItem` is checked it for equality every time a bot writes to that item, and then updates it to a new unique value it after each write. If the `eTag` property on `Write` doesn't match the `eTag` in the dataStore, it means another bot changed the data. 

For example, let's say you want your bot to edit a saved note, but you don't want your bot to overwrite changes that another instance of the bot has done. If another instance of the bot has made edits, you want the user to edit the version with the latest updates.

You can first create the note by creating a `StoreItems` object that represents a data store, and add items for the properties of the note.

# [C#](#tab/csetag)

```csharp
StoreItems dataStore = new StoreItems();

// create a note for the first time
StoreItem note = new StoreItem();
note.Add("NoteName", "Shopping List");
note.Add("NoteContents", "eggs");
dataStore["Note1"] = note;
await storage.Write(dataStore).ConfigureAwait(false);
```

Then to access the note later you can use `Read()`:
```csharp
String noteName = "";
String noteContents = "";
StoreItems noteStore = await storage.Read("Note");

// if there were more than one note you can iterate through each of the items in the store
foreach (var item in noteStore)
{
    StoreItem value = item.Value as StoreItem;
    noteName = value["NoteName"];
    noteContents = value["NoteContents"];


}

    // To make a change to the item, use the item you read from storage and modify it.
noteStore["Note1"]["NoteContents"]= "bread";
await storage.Write(noteStore).ConfigureAwait(false);
```

# [JavaScript](#tab/jsetag)
```
// Node.js snippet TBD
```
---



To maintain concurrency, always read a property from storage, then modify the property you read, so that the `eTag` is maintained. If you issue a `Storage.Read()` request to retrieve user data from the store, the response will contain the eTag property. If you change the data and issue a `Storage.Write()` request to save the updated data to the store, your request may include the eTag property that specifies the same value as you received earlier in the `Storage.Read()` response, so that they will match. 

<!-- If the ETag specified in your `Storage.Write()` request matches the current value in the store, the server will save the data and specify a new eTag value in the body of the response, that indicates that the data has been updated. If the ETag specified in your Storage.Write() request does not match the current value in the store, the bot responds with an error indicating an eTag conflict, to indicate that the user's data in the store has changed since you last saved or retrieved it. -->

<!-- TODO: new snippet -->

## Save a conversation reference using storage

You can use IStorage to save BotBuilder SDK objects as well as user-defined data. This code snippet uses `Storage.Write()` to save a ConversationReference object for use in sending a proactive message.

# [C#](#tab/csharpwriteconvref)
```csharp
                // If the user says "subscribe"
                if (utterance.CompareTo("subscribe") == 0)
                {
                    var reference = context.ConversationReference;
                    var userId = reference.User.Id;

                    // save the ConversationReference to a global variable of this class
                    conversationReference = reference;

                    StoreItems storeItems = new StoreItems();
                    StoreItem conversationReferenceToStore = new StoreItem();
                    // set the eTag to "*" to indicate you're overwriting previous data
                    conversationReferenceToStore.eTag = "*";
                    conversationReferenceToStore.Add("ref", reference);
                    storeItems[$"ConversationReference/{userId}"] = conversationReferenceToStore;

                    await storage.Write(storeItems).ConfigureAwait(false);

                    SubscribeUser(userId);

                    context.Reply("Thank You! We will message you shortly.");
                }

        public void SubscribeUser(string userId)
        {
            CreateContextForUserAsync(userId, async (IBotContext context) =>
            {
                context.Reply("You've been notified.");
                await Task.Delay(2000);
            });
                        
        }
```
# [JavaScript](#tab/jswriteconvref)
```javascript
const { MemoryStorage } = require('botbuilder');

const storage = new MemoryStorage();
const bot = new Bot(adapter)
    .onReceive((context) => {
        const utterance = (context.request.text || '').trim().toLowerCase();
        if (utterances === 'subscribe') {
            const reference = context.conversationReference;
            const userId = reference.user.id;
            const changes = {};
            changes['reference/' + userId] = reference;
            return storage.write(changes)
                .then(( => subscribeUser(userId)))
                .then(() => {
                    context.reply(`Thank You! We will message you shortly.`);
                });
        }
    });
```
---

To read the saved object from storage, call `Storage.Read()`.

# [C#](#tab/csharpread)
```csharp


        private async void CreateContextForUserAsync(String userId,Func<IBotContext, Task> onReady)
        {
            var referenceKey = $"ConversationReference/{userId}";
            
            ConversationReference localRef = null;
            StoreItems conversationReferenceFromStorage = await storage.Read(referenceKey);
            foreach (var item in conversationReferenceFromStorage)
            {
                StoreItem value = item.Value as StoreItem;
                localRef = value["ref"];
            }
            
            await bot.CreateContext(this.conversationReference, onReady);

        }
```
# [JavaScript](#tab/jsread)
```javascript
function subscribeUser(userId) {
    setTimeout(() => {
        createContextForUser(userId, (context) => {
            context.reply(`You've been notified!`);
        });
    }, 2000);
}

function createContextForUser(userId, onReady) {
    const referenceKey = 'reference/' + userId;
    return storage.read([referenceKey])
        .then((rows) => rows[referenceKey])
        .then((reference) => bot.createContext(reference, onReady));
}
```
---




## Additional resources

- [Concept: Storage in the Bot Builder SDK](./bot-builder-v4-concept-storage.md)


