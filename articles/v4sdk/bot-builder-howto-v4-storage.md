---
title: Write directly to storage | Microsoft Docs
description: Learn how to write directly to storage with the Bot Framework SDK for .NET.
keywords: storage, read and write, memory storage, eTag
author: DeniseMak
ms.author: v-demak
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 4/13/19
monikerRange: 'azure-bot-service-4.0'
---

# Write directly to storage

[!INCLUDE[applies-to](../includes/applies-to.md)]

You can read and write directly to your storage object without using middleware or context object. This can be appropriate to data that your bot uses, that comes from a source outside your bot's conversation flow. For example, let's say your bot allows the user to ask for the weather report, and your bot retrieves the weather report for a specified date, by reading it from an external database. The content of the weather database isn't dependent on user information or the conversation context, so you could just read it directly from storage instead of using the state manager. The code examples in this article show you how to read and write data to storage using **Memory Storage**, **Cosmos DB**, **Blob Storage**, and **Azure Blob Transcript Store**.

## Prerequisites
- If you don't have an Azure subscription, create a [free](https://azure.microsoft.com/en-us/free/) account before you begin.
- Install the Bot Framework [Emulator](https://aka.ms/Emulator-wiki-getting-started)

## Memory storage

We will first create a bot that will read and write data to Memory Storage. Memory storage is used for testing purposes only and is not intended for production use. Be sure to set storage to Cosmos DB or Blob Storage before publishing your bot.

#### Build a basic bot

The rest of this topic builds off of an Echo bot. The Echo bot sample code for creating this project can be found here, [C# Sample](https://aka.ms/cs-echobot-sample) or here [JS Sample](https://aka.ms/js-echobot-sample). You can use the Bot Framework Emulator to connect to, converse with, and test your bot. The following sample adds every message from the user to a list. The data structure containing this list is then saved to your storage.

# [C#](#tab/csharp)

**MyBot.cs**
```csharp
using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

// Create local Memory Storage.
private static readonly MemoryStorage _myStorage = new MemoryStorage();

// Create cancellation token (used by Async Write operation).
public CancellationToken cancellationToken { get; private set; }

// Class for storing a log of utterances (text of messages) as a list.
public class UtteranceLog : IStoreItem
{
     // A list of things that users have said to the bot
     public List<string> UtteranceList { get; } = new List<string>();

     // The number of conversational turns that have occurred
     public int TurnNumber { get; set; } = 0;

     // Create concurrency control where this is used.
     public string ETag { get; set; } = "*";
}

// Every Conversation turn for our Bot calls this method.
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
{
   if (turnContext.Activity.Type == ActivityTypes.Message)
   {
      // Replace the two lines of code from original MyBot code with the following:

      // preserve user input.
      var utterance = turnContext.Activity.Text;
      // make empty local logitems list.
      UtteranceLog logItems = null;

      // see if there are previous messages saved in storage.
      try
      {
         string[] utteranceList = { "UtteranceLog" };
         logItems = _myStorage.ReadAsync<UtteranceLog>(utteranceList).Result?.FirstOrDefault().Value;
      }
      catch
      {
         // Inform the user an error occured.
         await turnContext.SendActivityAsync("Sorry, something went wrong reading your stored messages!");
      }

      // If no stored messages were found, create and store a new entry.
      if (logItems is null)
      {
            // add the current utterance to a new object.
            logItems = new UtteranceLog();
            logItems.UtteranceList.Add(utterance);
            // set initial turn counter to 1.
            logItems.TurnNumber++;

            // Show user new user message.
            await turnContext.SendActivityAsync($"{logItems.TurnNumber}: The list is now: {string.Join(", ", logItems.UtteranceList)}");

            // Create Dictionary object to hold received user messages.
            var changes = new Dictionary<string, object>();
            {
               changes.Add("UtteranceLog", logItems);
            }
         try
         {
            // Save the user message to your Storage.
            await _myStorage.WriteAsync(changes, cancellationToken);
         }
         catch
         {
            // Inform the user an error occured.
            await turnContext.SendActivityAsync("Sorry, something went wrong storing your message!");
         }
      }
      // Else, our Storage already contained saved user messages, add new one to the list.
      else
      {
         // add new message to list of messages to display.
         logItems.UtteranceList.Add(utterance);
         // increment turn counter.
         logItems.TurnNumber++;

         // show user new list of saved messages.
         await turnContext.SendActivityAsync($"{logItems.TurnNumber}: The list is now: {string.Join(", ", logItems.UtteranceList)}");

         // Create Dictionary object to hold new list of messages.
         var changes = new Dictionary<string, object>();
         {
            changes.Add("UtteranceLog", logItems);
         };

         try
         {
            // Save new list to your Storage.
            await _myStorage.WriteAsync(changes,cancellationToken);
         }
         catch
         {
            // Inform the user an error occured.
            await turnContext.SendActivityAsync("Sorry, something went wrong storing your message!");
         }
      }
   }
}

```

# [JavaScript](#tab/javascript)

**index.js**
```javascript
const { BotFrameworkAdapter, ConversationState, MemoryStorage } = require('botbuilder');
const restify = require('restify');

// Add memory storage.
var storage = new MemoryStorage();

// const conversationState = new ConversationState(storage);
// adapter.use(conversationState);

// Listen for incoming requests - adds storage for messages.
server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (context) => {

        if (context.activity.type === 'message') {
            // Route to main dialog.
            await myBot.onTurn(context);
            // Save updated utterance inputs.
            await logMessageText(storage, context);
        }
        else {
            // Just route to main dialog.
            await myBot.onTurn(context);
        }
    });
});

// This function stores new user messages. Creates new utterance log if none exists.
async function logMessageText(storage, context) {
    let utterance = context.activity.text;
    try {
        // Read from the storage.
        let storeItems = await storage.read(["UtteranceLogJS"])
        // Check the result.
        var UtteranceLogJS = storeItems["UtteranceLogJS"];

        if (typeof (UtteranceLogJS) != 'undefined') {
            // The log exists so we can write to it.
            storeItems["UtteranceLogJS"].turnNumber++;
            storeItems["UtteranceLogJS"].UtteranceList.push(utterance);
            // Gather info for user message.
            var storedString = storeItems.UtteranceLogJS.UtteranceList.toString();
            var numStored = storeItems.UtteranceLogJS.turnNumber;

            try {
                await storage.write(storeItems)
                context.sendActivity(`${numStored}: You stored: ${storedString}`);
            } catch (err) {
                context.sendActivity(`Write failed of UtteranceLogJS: ${err}`);
            }

         } else {
            context.sendActivity(`Creating and saving new utterance log`);
            var turnNumber = 1;
            storeItems["UtteranceLogJS"] = { UtteranceList: [`${utterance}`], "eTag": "*", turnNumber }
            // Gather info for user message.
            var storedString = storeItems.UtteranceLogJS.UtteranceList.toString();
            var numStored = storeItems.UtteranceLogJS.turnNumber;

            try {
                await storage.write(storeItems)
                context.sendActivity(`${numStored}: You stored: ${storedString}`);
            } catch (err) {
                context.sendActivity(`Write failed: ${err}`);
            }
        }
    } catch (err) {
        context.sendActivity(`Read rejected. ${err}`);
    };
}

```

---

### Start your bot
Run your bot locally.

### Start the emulator and connect your bot
Next, start the emulator and then connect to your bot in the emulator:

1. Click the **Open Bot** link in the emulator "Welcome" tab.
2. Select the .bot file located in the directory where you created the project.

### Interact with your bot
Send a message to your bot, and the bot will list the messages it received.
![Emulator running](../media/emulator-v4/emulator-running.png)


## Using Cosmos DB
Now that you've used memory storage, we'll update the code to use Azure Cosmos DB. Cosmos DB is Microsoft's globally distributed, multi-model database. Azure Cosmos DB enables you to elastically and independently scale throughput and storage across any number of Azure's geographic regions. It offers throughput, latency, availability, and consistency guarantees with comprehensive service level agreements (SLAs).

### Set up
To use Cosmos DB in your bot, you'll need to get a few things set up before getting into the code.

#### Create your database account
1. In a new browser window, sign in to the [Azure portal](http://portal.azure.com).
2. Click **Create a resource > Databases > Azure Cosmos DB**
3. In the **New account page**, provide a unique name in the **ID** field. For **API**, select **SQL**, and provide **Subscription**, **Location**, and **Resource group** information.
4. Then click **Create**.

The account creation takes a few minutes. Wait for the portal to display the Congratulations! Your Azure Cosmos DB account was created page.

##### Add a collection
1. Click **Settings > New Collection**. The **Add Collection** area is displayed on the far right, you may need to scroll right to see it. Due to recent updates to Cosmos DB, be sure to add a single Partition key: _/id_. This key will avoid cross partition query errors.

![Add Cosmos DB collection](./media/add_database_collection.png)

2. Your new database collection, "bot-cosmos-sql-db" with a collection id of "bot-storage." We will use these values in our coding example that follows below.
 -
![Cosmos DB](./media/cosmos-db-sql-database.png)

3. The endpoint URI and key are available within the **Keys** tab of your database settings. These values will be needed to configure your code further down in this article.

![Cosmos DB Keys](./media/comos-db-keys.png)

#### Add configuration information
Our configuration data to add Cosmos DB storage is short and simple, you can add additional configuration settings using these same methods as your bot gets more complex. This example uses the Cosmos DB database and collection names from the example above.

# [C#](#tab/csharp)

**MyBot.cs**
```csharp
private const string CosmosServiceEndpoint = "<your-cosmos-db-URI>";
private const string CosmosDBKey = "<your-cosmos-db-account-key>";
private const string CosmosDBDatabaseName = "bot-cosmos-sql-db";
private const string CosmosDBCollectionName = "bot-storage";
```


# [JavaScript](#tab/javascript)

Add the following information to your `.env` file.

**.env**
```javascript
ACTUAL_SERVICE_ENDPOINT=<your database URI>
ACTUAL_AUTH_KEY=<your database key>
DATABASE=bot-cosmos-sql-db
COLLECTION=bot-storage
```
---

#### Installing packages
Make sure you have the packages necessary for Cosmos DB

# [C#](#tab/csharp)

```powershell
Install-Package Microsoft.Bot.Builder.Azure
```

# [JavaScript](#tab/javascript)

You can add references to botbuilder-azure in your project via npm: -->


```powershell
npm install --save botbuilder-azure
```

To use the .env configuration file, the bot needs an extra package included. First, get the dotnet package from npm:

```powershell
npm install --save dotenv
```

---

### Implementation

# [C#](#tab/csharp)

The following sample code runs using the same bot code as the [memory storage](#memory-storage) sample provided above.
The code snippet below shows an implementation of Cosmos DB storage for '_myStorage_' that replaces local Memory storage.

**MyBot.cs**
```csharp
using Microsoft.Bot.Builder.Azure;

// Create local Memory Storage - commented out.
// private static readonly MemoryStorage _myStorage = new MemoryStorage();

// Replaces Memory Storage with reference to Cosmos DB.
private static readonly CosmosDbStorage _myStorage = new CosmosDbStorage(new CosmosDbStorageOptions
{
   AuthKey = CosmosDBKey,
   CollectionId = CosmosDBCollectionName,
   CosmosDBEndpoint = new Uri(CosmosServiceEndpoint),
   DatabaseId = CosmosDBDatabaseName,
});
```

# [JavaScript](#tab/javascript)

The following sample code is similar to [memory storage](#memory-storage) but with some slight changes.

Require `CosmosDbStorage` from botbuilder-azure and configure dotenv to read the `.env` file.

**index.js**
```javascript
const { CosmosDbStorage } = require("botbuilder-azure");
```
Comment out Memory Storage, replace with reference to Cosmos DB.

**index.js**
```javascript
// Create local Memory Storage - commented out.
// var storage = new MemoryStorage();

// Create access to Cosmos DB storage.
//Add CosmosDB
const storage = new CosmosDbStorage({
    serviceEndpoint: process.env.ACTUAL_SERVICE_ENDPOINT,
    authKey: process.env.ACTUAL_AUTH_KEY,
    databaseId: process.env.DATABASE,
     collectionId: process.env.COLLECTION
})

```

---

## Start your bot
Run your bot locally.

## Start the emulator and connect your bot
Next, start the emulator and then connect to your bot in the emulator:

1. Click the **Open Bot** link in the emulator "Welcome" tab.
2. Select the .bot file located in the directory where you created the project.

## Interact with your bot
Send a message to your bot, and the bot will list the messages it received.
![Emulator running](../media/emulator-v4/emulator-running.png)


### View your data
After you have run your bot and saved your information, we can view the data stored in the Azure portal under the **Data Explorer** tab.

![Data Explorer example](./media/data_explorer.PNG)

### Manage concurrency using eTags
In our bot code example we set the `eTag` property of each `IStoreItem` to `*`. The `eTag` (entity tag) member of your store object is used within Cosmos DB to manage concurrency. The `eTag` tells your database what to do if another instance of the bot has changed the object in the same storage that your bot is writing to.

<!-- define optimistic concurrency -->

#### Last write wins - allow overwrites
An `eTag` property value of asterisk (`*`) indicates that the last writer wins. When creating a new data store, you can set `eTag` of a property to `*` to indicate that you have not previously saved the data that you are writing, or that you want the last writer to overwrite any previously saved property. If concurrency is not an issue for your bot, setting the `eTag` property to `*` for any data that you are writing enables overwrites.

#### Maintain concurrency and prevent overwrites
When storing your data into Cosmos DB, use a value other than `*` for the `eTag` if you want to prevent concurrent access to a property and avoid overwriting changes from another instance of the bot. The bot receives an error response with the message `etag conflict key=` when it attempts to save state data and the `eTag` is not the same value as the `eTag` in storage. <!-- To control concurrency of data that is stored using `IStorage`, the BotBuilder SDK checks the entity tag (ETag) for `Storage.Write()` requests. -->

By default, the Cosmos DB store checks the `eTag` property of a storage object for equality every time a bot writes to that item, and then updates it to a new unique value after each write. If the `eTag` property on write doesn't match the `eTag` in storage, it means another bot or thread changed the data.

For example, let's say you want your bot to edit a saved note, but you don't want your bot to overwrite changes that another instance of the bot has done. If another instance of the bot has made edits, you want the user to edit the version with the latest updates.

# [C#](#tab/csharp)

First, create a class that implements `IStoreItem`.

```csharp
public class Note : IStoreItem
{
    public string Name { get; set; }
    public string Contents { get; set; }
    public string ETag { get; set; }
}
```

Next, create an initial note by creating a storage object, and add the object to your store.

```csharp
// create a note for the first time, with a non-null, non-* ETag.
var note = new Note { Name = "Shopping List", Contents = "eggs", ETag = "x" };

var changes = Dictionary<string, object>();
{
    changes.Add("Note", note);
};
await NoteStore.WriteAsync(changes, cancellationToken);
```

Then, access and update the note later, keeping its `eTag` that you read from the store.

```csharp
var note = NoteStore.ReadAsync<Note>("Note").Result?.FirstOrDefault().Value;

if (note != null)
{
    note.Contents += ", bread";
    var changes = new Dictionary<string, object>();
    {
         changes.Add("Note1", note);
    };
    await NoteStore.WriteAsync(changes, cancellationToken);
}
```

If the note was updated in the store before you write your changes, the call to `Write` will throw an exception.


# [JavaScript](#tab/javascript)

Add a helper function to the end of your bot that will write a sample note to a data store.
First create `myNoteData` object.

```javascript
// Helper function for writing a sample note to a data store
async function createSampleNote(storage, context) {
    var myNoteData = {
        name: "Shopping List",
        contents: "eggs",
        // If any Note file is already stored, the eTag field
        // must be set to "*" in order to allow writing without first reading the stored eTag
        // otherwise you'll likely get an exception indicating an eTag conflict.
        eTag: "*"
    }
}
```

Within the `createSampleNote` helper function initialize a `changes` object and add your *notes* to it, then write it to storage.

```javascript
// Write the note data to the "Note" key
var changes = {};
changes["Note"] = myNoteData;
// Creates a file named Note, if it doesn't already exist.
// specifying eTag= "*" will overwrite any existing contents.
// The act of writing to the file automatically updates the eTag property
// The first time you write to Note, the eTag is changed from *, and file contents will become:
//    {"name":"Shopping List","contents":"eggs","eTag":"1"}
try {
    await storage.write(changes);
    await context.sendActivity('Successful created a note.');
} catch (err) {
    await context.sendActivity(`Could not create note: ${err}`);
}
```

Then to access and update the note later we create another helper function that can be accessed when the user types in "update note".

```javascript
async function updateSampleNote(storage, context) {
    try {
        // Read in a note
        var note = await storage.read(["Note"]);
        console.log(`note.eTag=${note["Note"].eTag}\n note=${JSON.stringify(note)}`);
        // update the note that we just read
        note["Note"].contents += ", bread";
        console.log(`Updated note=${JSON.stringify(note)}`);

        try {
            await storage.write(note); // Write the changes back to storage
            await context.sendActivity('Successfully updated to note.');
        } catch (err) {            console.log(`Write failed: ${err}`);
        }
    }
    catch (err) {
        await context.sendActivity(`Unable to read the Note: ${err}`);
    }
}
```

If the note was updated in the store before you write your changes, the call to `write` will throw an exception.

---

To maintain concurrency, always read a property from storage, then modify the property you read, so that the `eTag` is maintained. If you read user data from the store, the response will contain the eTag property. If you change the data and write updated data to the store, your request should include the eTag property that specifies the same value as you read earlier. However, writing an object with its `eTag` set to `*` will allow the write to overwrite any other changes.

## Using Blob storage
Azure Blob storage is Microsoft's object storage solution for the cloud. Blob storage is optimized for storing massive amounts of unstructured data, such as text or binary data.

### Create your Blob storage account
To use Blob storage in your bot, you'll need to get a few things set up before getting into the code.
1. In a new browser window, sign in to the [Azure portal](http://portal.azure.com).
2. Click **Create a resource > Storage > Storage account - blob, file, table, queue**
3. In the **New account page**, enter **Name** for the storage account, select **Blob storage** for **Account kind**, provide **Location**, **Resource group** and **Subscription** infomration.
4. Then click **Create**.

![Create Blob storage](./media/create-blob-storage.png)

#### Add configuration information

Find the Blob Storage keys you need to configure Blob Storage for your bot as shown above:
1. In the Azure portal, open your Blob Storage account and select **Settings > Access keys**.

![Find Blob Storage Keys](./media/find-blob-storage-keys.png)

We will now use two of these keys to provide our code with access to our Blob Storage account.

# [C#](#tab/csharp)

```csharp
using Microsoft.Bot.Builder.Azure;
```
Update the line of code that points "_myStorage_" to your existing Blob Storage account.

```csharp


private static readonly AzureBlobStorage _myStorage = new AzureBlobStorage("<your-blob-storage-account-string>", "<your-blob-storage-container-name>");
```

# [JavaScript](#tab/javascript)
```javascript
const mystorage = new BlobStorage({
   <youy_containerName>,
   <your_storageAccountOrConnectionString>,
   <your_storageAccessKey>
})
```
---

Once "_myStorage_" is set to point to your Blob Storage account, your bot code will now store and retrieve data from Blob Storage.

## Start your bot
Run your bot locally.

## Start the emulator and connect your bot
Next, start the emulator and then connect to your bot in the emulator:

1. Click the **Open Bot** link in the emulator "Welcome" tab.
2. Select the .bot file located in the directory where you created the project.

## Interact with your bot
Send a message to your bot, and the bot will list the messages it received.
![Emulator running](../media/emulator-v4/emulator-running.png)

### View your data
After you have run your bot and saved your information, we can view it in under the **Storage Explorer** tab in the Azure portal.

## Blob transcript storage
Azure blob transcript storage provides a specialized storage option that allows you to easily save and retrieve user conversations in the form of a recorded transcript. Azure blob transcript storage is particularly useful for automatically capturing user inputs to examine when debugging your bot's performance.

### Set up
Azure blob transcript storage can use the same blob storage account created following the steps detailed in sections "_Create your blob storage account_" and "_Add configuration information_" above. For this discussion, we have added a new blob container, "_mybottranscripts_."

### Implementation
The following code connects transcript storage pointer "_transcriptStore_" to your new Azure blob transcript storage account. The source code to store user conversations shown here is based on the [Conversation History](https://aka.ms/bot-history-sample-code) sample.

```csharp
// In Startup.cs
using Microsoft.Bot.Builder.Azure;

// Enable the conversation transcript middleware.
blobStore = new AzureBlobTranscriptStore(blobStorageConfig.ConnectionString, storageContainer);
var transcriptMiddleware = new TranscriptLoggerMiddleware(blobStore);
options.Middleware.Add(transcriptMiddleware);

// In ConversationHistoryBot.cs
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;

private readonly AzureBlobTranscriptStore _transcriptStore;

/// <param name="transcriptStore">Injected via ASP.NET dependency injection.</param>
public ConversationHistoryBot(AzureBlobTranscriptStore transcriptStore)
{
    _transcriptStore = transcriptStore ?? throw new ArgumentNullException(nameof(transcriptStore));
}

```

### Store user conversations in azure blob transcripts
After the TranscriptLoggerMiddleware is added, the Transcript Store will automatically begin to preserve your users' conversations with your bot. These conversations can later be used as a debugging tool to see how users are interacting with your bot. The following code retrieves the transcript, then sends it to the current conversation when activity.text receives the input message _!history_. Note: the SendConversationHistoryAsync method is supported in the Direct Line, Web Chat and Emulator channels.


```csharp
/// <summary>
/// Every Conversation turn for our EchoBot will call this method.
/// </summary>
/// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
/// for processing this conversation turn. </param>
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
{
    var activity = turnContext.Activity;
    if (activity.Type == ActivityTypes.Message)
    {
        if (activity.Text == "!history")
        {
           // Download the activities from the Transcript (blob store) when a request to upload history arrives.
           var connectorClient = turnContext.TurnState.Get<ConnectorClient>(typeof(IConnectorClient).FullName);
           // Get all the message type activities from the Transcript.
           string continuationToken = null;
           var count = 0;
           do
           {
               var pagedTranscript = await _transcriptStore.GetTranscriptActivitiesAsync(activity.ChannelId, activity.Conversation.Id, continuationToken);
               var activities = pagedTranscript.Items
                  .Where(a => a.Type == ActivityTypes.Message)
                  .Select(ia => (Activity)ia)
                  .ToList();

               var transcript = new Transcript(activities);

               await connectorClient.Conversations.SendConversationHistoryAsync(activity.Conversation.Id, transcript, cancellationToken: cancellationToken);

               continuationToken = pagedTranscript.ContinuationToken;
           }
           while (continuationToken != null);
```

### Find all stored transcripts for your channel
To see what data you have stored, you can use the following code to programmatically find the "_ConversationIDs_" for all transcripts that you have stored. When using the bot emulator to test your code, selecting "_Start Over_" begins a new transcript with a new "_ConversationID_."

```csharp
List<string> storedTranscripts = new List<string>();
PagedResult<Transcript> pagedResult = null;
var pageSize = 0;
do
{
    pagedResult = await _transcriptStore.ListTranscriptsAsync("emulator", pagedResult?.ContinuationToken);

    // transcript item contains ChannelId, Created, Id.
    // save the converasationIds (Id) found by "ListTranscriptsAsync" to a local list.
    foreach (var item in pagedResult.Items)
    {
         // Make sure we store an unescaped conversationId string.
         var strConversationId = item.Id;
         storedTranscripts.Add(Uri.UnescapeDataString(strConversationId));
    }
} while (pagedResult.ContinuationToken != null);
```

### Retrieve user conversations from azure blob transcript storage
Once bot interaction transcripts have been stored into your Azure blob transcript store, you can programmatically retrieve them for testing or debugging using the AzureBlobTranscriptStorage method, "_GetTranscriptActivities_". The following code snippet retrieves all user inputs transcripts that were received and stored within the previous 24 hours from each stored transcript.


```csharp
var numTranscripts = storedTranscripts.Count();
for (int i = 0; i < numTranscripts; i++)
{
    PagedResult<IActivity> pagedActivities = null;
    do
    {
        string thisConversationId = storedTranscripts[i];
        // Find all inputs in the last 24 hours.
        DateTime yesterday = DateTime.Now.AddDays(-1);
        // Retrieve iActivities for this transcript.
        pagedActivities = await _myTranscripts.GetTranscriptActivitiesAsync("emulator", thisConversationId, pagedActivities?.ContinuationToken, yesterday);
        foreach (var item in pagedActivities.Items)
        {
            // View as message and find value for key "text" :
            var thisMessage = item.AsMessageActivity();
            var userInput = thisMessage.Text;
         }
    } while (pagedActivities.ContinuationToken != null);
}
```

### Remove stored transcripts from azure blob transcript storage
Once you have finished using user input data to test or debug you bot, stored transcripts can be programmatically removed from your Azure blob transcript store. The following code snippet removes all stored transcripts from your bot transcript store.


```csharp
for (int i = 0; i < numTranscripts; i++)
{
   // Remove all stored transcripts except the last one found.
   if (i > 0)
   {
       string thisConversationId = storedTranscripts[i];
       await _transcriptStore.DeleteTranscriptAsync("emulator", thisConversationId);
    }
}
```


This following link provides more information concerning [Azure Blob Transcript Storage](https://docs.microsoft.com/dotnet/api/microsoft.bot.builder.azure.azureblobtranscriptstore)

## Next steps
Now that you know how to read read and write directly from storage, lets take a look at how you can use the state manager to do that for you.

> [!div class="nextstepaction"]
> [Save state using conversation and user properties](bot-builder-howto-v4-state.md)

