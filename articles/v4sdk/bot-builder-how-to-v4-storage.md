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

This code example shows you how to read and write text for weather a report to storage. In this example the storage is a file, but you can easily change the code to initialze the storage object to use `MemoryStorage` or `AzureTableStorage` instead. 

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
<!-- 
`StoreItems` contains one or more key-value pairs.

For example, the following snippet demonstrates how to write to a custom storage object that contains a flag that indicates if the user is logged in or not. The bot writes to the storage object's `loggedIn` value if the user says "log in" or "log out". If the user says "status", the bot reads from storage and replies based on the `loggedIn` value.

```csharp
using Microsoft.Bot.Builder.Storage;

// initialize an object to store data
this.CustomStore = new StoreItems();

// initialize a storage object
storage = new FileStorage(System.IO.Path.GetTempPath());  

// ...

private async Task BotReceiveHandler(IBotContext context, MiddlewareSet.NextDelegate next)
{
    if (context.Request.Type == ActivityTypes.Message)
    {
        // get the text of the message 
        var utterance = context.Request.AsMessageActivity().Text.Trim().ToLower();
        bool loggedIn;
        var flagsKey = "Flags";

        if (String.Equals("log in", utterance))
        {
                    //change loggedIn to false
                    loginFlagToStore = new StoreItem
                    {
                        eTag = "*"
                    };
                    loginFlagToStore.Add("loggedIn", false);
                    CustomStore[flagsKey] = loginFlagToStore;
                    await storage.Write(CustomStore);
        }
        if (String.Equals("log out", utterance))
        {
                    //change loggedIn to true
                    loginFlagToStore = new StoreItem
                    {
                        eTag = "*"
                    };
                    loginFlagToStore.Add("loggedIn", true);
                    CustomStore[flagsKey] = loginFlagToStore;
                    await storage.Write(CustomStore);
        }
        if (String.Equals("status", utterance))
        {
            this.CustomStore = await storage.Read(flagsKey);
            if (this.CustomStore != null && CustomStore["Flags"].ContainsKey("loggedIn"))
            {
                    loggedIn = CustomStore["Flags"]["loggedIn"];                    
            }
            else
            {
                    loggedIn = false;
            }
            // reply based on flag             
            if (loggedIn)
            {
                        context.Reply("Logged in.");
            } else
            {
                        context.Reply("Not logged in.");
            }
        }
```
-->
<!--
```csharp

storage = new Microsoft.Bot.Builder.Storage.FileStorage(System.IO.Path.GetTempPath());

[...]
var utterance = context.Request.AsMessageActivity().Text.Trim().ToLower();

[...]

StoreItems storeItems = new StoreItems();
StoreItem itemToStore = new StoreItem();
// set the eTag to "*" to allow other instances of the bot to overwrite data
itemToStore.eTag = "*";

// Add the user's name to a StoreItem
itemToStore.Add("name", utterance);
// Create a key named "UserInfo" and set its value to the item
storeItems["UserInfo"] = itemToStore;

await storage.Write(storeItems).ConfigureAwait(false);
```

The following code reads the data:
```csharp
            StoreItems userInfoFromStorage = await storage.Read(referenceKey);
            foreach (var item in userInfoFromStorage)
            {
                StoreItem value = item.Value as StoreItem;
                userName = value["name"];
            }
```
-->

## Manage concurrency using entity tags (eTags)

In the previous example, you set the `eTag` property to `*`. The `eTag` (entity tag) member of the `StoreItem` class is for managing concurrency. The `eTag` indicates what to do if another instance of the bot has changed the `StoreItem` that your bot is writing to. 

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


By default, the `eTag` property of a `StoreItem` object is initialized to `0` when created. If you haven't set it to '*' to allow overwrites, the Storage object checks it for equality every time a bot writes to that item, and then increments it after each write. If the `eTag` property on `Write` doesn't match the `eTag` in the dataStore, it means another bot changed the data. 

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


