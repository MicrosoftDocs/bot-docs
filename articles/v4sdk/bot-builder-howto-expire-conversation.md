---
title: Expire a conversation - Bot Service
description: Learn how to expire a user's conversation with a bot.
keywords: expire, timeout
author: ericdahlvang
ms.author: erdahlva
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 07/14/2020
monikerRange: 'azure-bot-service-4.0'
---

# Expire a conversation

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

A bot sometimes needs to restart a conversation from the beginning.  For instance, if a user does not respond after a certain period of time.  This article describes two methods for expiring a conversation:

- Track the last time a message was received from a user, and clear state if the time is greater than a preconfigured length upon receiving the next message from the user. For more information, see the [user interaction expiration](#user-interaction-expiration) section.
- Use a storage layer feature, such as Cosmos DB Time To Live (TTL), to automatically clear state after a preconfigured length of time. For more information, see the [storage expiration](#storage-expiration) section.

<!-- 
NOTE: in the future, provide guidance on an azure function queue or time trigger

- Track the last time a message was received from a user, and run a Web Job or Azure Function to clear the state and/or proactively message the user. See [Proactive Expiration](#proactive-expiration)
-->

## Prerequisites

- If you don't have an Azure subscription, create a [free](https://azure.microsoft.com/free/) account before you begin.
- Knowledge of [bot basics][concept-basics], [managing state][concept-state], and the [dialogs library][concept-dialogs].
- A copy of the **multi-turn prompt** sample in either [**C#**][cs-sample], [**JavaScript**][js-sample], [**Java**][java-sample], or [**Python**][python-sample].

## About this sample

The sample code in this article begins with the structure of a multi-turn bot, and extends that bot's functionality by adding additional code (provided in the following sections). This extended code demonstrates how to clear conversation state after a certain time period has passed.

## User Interaction Expiration

This type of expiring conversation is accomplished by adding a _last accessed time_ property to the bot's conversation state. This property value is then compared to the current time within the _activity handler_ before processing activities.

> [!NOTE]
> This example uses a 30 second timeout for ease of testing this pattern.

# [C#](#tab/csharp)

**appsettings.json**

First, add an `ExpireAfterSeconds` setting to appsettings.json:

```json
{
  "MicrosoftAppId": "",
  "MicrosoftAppPassword": "",
  "ExpireAfterSeconds": 30
}
```

**Bots\DialogBot.cs**

Next, add `ExpireAfterSeconds`, `LastAccessedTimeProperty`, and `DialogStateProperty` fields to the bot class and initialize them in the bot's constructor. Also add an `IConfiguration` parameter to the constructor with which to retrieve the `ExpireAfterSeconds` value.

Note that instead of creating the dialog state property accessor inline in the `OnMessageActivityAsync` method, you are creating and recording it at initialization time. The bot will need the state property accessor not only to run the dialog, but also to clear the dialog state.

```csharp
protected readonly int ExpireAfterSeconds;
protected readonly IStatePropertyAccessor<DateTime> LastAccessedTimeProperty;
protected readonly IStatePropertyAccessor<DialogState> DialogStateProperty;

// Existing fields omitted...

public DialogBot(IConfiguration configuration, ConversationState conversationState, UserState userState, T dialog, ILogger<DialogBot<T>> logger)
{
    ConversationState = conversationState;
    UserState = userState;
    Dialog = dialog;
    Logger = logger;

    TimeoutSeconds = configuration.GetValue<int>("ExpireAfterSeconds");
    DialogStateProperty = ConversationState.CreateProperty<DialogState>(nameof(DialogState));
    LastAccessedTimeProperty = ConversationState.CreateProperty<DateTime>(nameof(LastAccessedTimeProperty));
}
```

Finally, add code to the bot's `OnTurnAsync` method to clear the dialog state if the conversation is too old.

```csharp
public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
{
    // Retrieve the property value, and compare it to the current time.
    var lastAccess = await LastAccessedTimeProperty.GetAsync(turnContext, () => DateTime.UtcNow, cancellationToken).ConfigureAwait(false);
    if ((DateTime.UtcNow - lastAccess) >= TimeSpan.FromSeconds(ExpireAfterSeconds))
    {
        // Notify the user that the conversation is being restarted.
        await turnContext.SendActivityAsync("Welcome back!  Let's start over from the beginning.").ConfigureAwait(false);

        // Clear state.
        await ConversationState.ClearStateAsync(turnContext, cancellationToken).ConfigureAwait(false);
    }

    await base.OnTurnAsync(turnContext, cancellationToken).ConfigureAwait(false);

    // Set LastAccessedTime to the current time.
    await LastAccessedTimeProperty.SetAsync(turnContext, DateTime.UtcNow, cancellationToken).ConfigureAwait(false);

    // Save any state changes that might have occurred during the turn.
    await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken).ConfigureAwait(false);
    await UserState.SaveChangesAsync(turnContext, false, cancellationToken).ConfigureAwait(false);
}
```

# [JavaScript](#tab/javascript)

**.env**

First, add an `ExpireAfterSeconds` setting to .env:

```json
MicrosoftAppId=
MicrosoftAppPassword=
ExpireAfterSeconds=30
```

**bots\dialogBot.js**

Next, add fields to `DialogBot` and update the constructor. Add local fields for `expireAfterSeconds` and `lastAccessedTimeProperty`.

Add `expireAfterSeconds` as a parameter to the constructor and create the required `StatePropertyAccessor`:

```javascript
constructor(expireAfterSeconds, conversationState, userState, dialog) {

    // Existing code omitted...

    this.lastAccessedTimeProperty = this.conversationState.createProperty('LastAccessedTime');
    this.expireAfterSeconds = expireAfterSeconds;

    // Existing code omitted...
}
```

Add code to the bot's `run` method:

```javascript
async run(context) {
    // Retrieve the property value, and compare it to the current time.
    const now = new Date();
    const lastAccess = new Date(await this.lastAccessedTimeProperty.get(context, now.toISOString()));
    if (now !== lastAccess && ((now.getTime() - lastAccess.getTime()) / 1000) >= this.expireAfterSeconds) {
        // Notify the user that the conversation is being restarted.
        await context.sendActivity("Welcome back!  Let's start over from the beginning.");

        // Clear state.
        await this.conversationState.clear(context);
    }

    await super.run(context);

    // Set LastAccessedTime to the current time.
    await this.lastAccessedTimeProperty.set(context, now.toISOString());

    // Save any state changes. The load happened during the execution of the Dialog.
    await this.conversationState.saveChanges(context, false);
    await this.userState.saveChanges(context, false);
}
```

**index.js**

Lastly, update `index.js` to send the `ExpireAfterSeconds` parameter to `DialogBot`:

```javascript
const bot = new DialogBot(process.env.ExpireAfterSeconds, conversationState, userState, dialog);
```

# [Java](#tab/java)

**application.properties**

First, add an `ExpireAfterSeconds` setting to application.properties:

``` txt
MicrosoftAppId=
MicrosoftAppPassword=
server.port=3978
ExpireAfterSeconds=30
```

**DialogBot.java**

Next, add `expireAfterSeconds`, `lastAccessedTimeProperty`, and `dialogStateProperty` fields to the bot class and initialize them in the bot's constructor. Also add a `Configuration` parameter to the constructor to retrieve the `ExpireAfterSeconds` value.

Note that instead of creating the dialog state property accessor inline in the `onMessageActivity` method, you create and record it at initialization time. The bot will need the state property accessor not only to run the dialog, but also to clear the dialog state.

```java
    protected final int expireAfterSeconds;
    protected final StatePropertyAccessor<LocalTime> lastAccessedTimeProperty;
    protected final StatePropertyAccessor<DialogState> dialogStateProperty;

// Existing fields omitted...

    public DialogBot(
        Configuration configuration,
        ConversationState withConversationState,
        UserState withUserState,
        Dialog withDialog
    ) {
        dialog = withDialog;
        conversationState = withConversationState;
        userState = withUserState;

        expireAfterSeconds = configuration.getProperty("ExpireAfterSeconds") != null ?
                             Integer.parseInt(configuration.getProperty("ExpireAfterSeconds")) :
                             30;
        lastAccessedTimeProperty = conversationState.createProperty("LastAccessedTimeProperty");
        dialogStateProperty = conversationState.createProperty("DialogStateProperty");

    }
```

Finally, add code to the bot's `onTurn` method to clear the dialog state if the conversation is too old.

```java
    @Override
    public CompletableFuture<Void> onTurn(
        TurnContext turnContext
    ) {
        LocalTime lastAccess = lastAccessedTimeProperty.get(turnContext).join();
        if (lastAccess != null 
            && (java.time.temporal.ChronoUnit.SECONDS.between(lastAccess, LocalTime.now()) >= expireAfterSeconds)) {
            turnContext.sendActivity("Welcome back!  Let's start over from the beginning.").join();
            conversationState.clearState(turnContext).join();
        }
        return lastAccessedTimeProperty.set(turnContext, LocalTime.now()).thenCompose(setResult -> {
            return super.onTurn(turnContext)
            .thenCompose(result -> conversationState.saveChanges(turnContext))
            // Save any state changes that might have occurred during the turn.
            .thenCompose(result -> userState.saveChanges(turnContext));
        });
    }
```


## [Python](#tab/python)

**config.py**

First, add an `ExpireAfterSeconds` setting to config.py:

```python
PORT = 3978
APP_ID = os.environ.get("MicrosoftAppId", "")
APP_PASSWORD = os.environ.get("MicrosoftAppPassword", "")
EXPIRE_AFTER_SECONDS = os.environ.get("ExpireAfterSeconds", 30)
```

**bots\dialog_bot.py**

Next, add fields to `DialogBot` and update the constructor. Add local fields for `expire_after_seconds` and `last_accessed_time_property`.

Add `expire_after_seconds` as a parameter to the constructor and create the required `StatePropertyAccessor`:

```python
def __init__(
    self,
    expire_after_seconds: int,
    conversation_state: ConversationState,
    user_state: UserState,
    dialog: Dialog,
):
    # Existing code omitted...

    self.expire_after_seconds = expire_after_seconds
    self.dialog_state_property = conversation_state.create_property("DialogState")
    self.last_accessed_time_property = conversation_state.create_property("LastAccessedTime")
    self.conversation_state = conversation_state
    self.user_state = user_state
    self.dialog = dialog
```

Change `on_message_activity` so it uses the `dialog_state_property`:

```python
async def on_message_activity(self, turn_context: TurnContext):
    await DialogHelper.run_dialog(
        self.dialog,
        turn_context,
        self.dialog_state_property,
    )
```

Add code to the bot's `on_turn` method:

```python
async def on_turn(self, turn_context: TurnContext):
    # Retrieve the property value, and compare it to the current time.
    now_seconds = int(time.time())
    last_access = int(
        await self.last_accessed_time_property.get(turn_context, now_seconds)
    )
    if now_seconds != last_access and (
        now_seconds - last_access >= self.expire_after_seconds
    ):
        # Notify the user that the conversation is being restarted.
        await turn_context.send_activity(
            "Welcome back!  Let's start over from the beginning."
        )

        # Clear state.
        await self.conversation_state.clear_state(turn_context)
        await self.conversation_state.save_changes(turn_context, True)

    await super().on_turn(turn_context)

    # Set LastAccessedTime to the current time.
    await self.last_accessed_time_property.set(turn_context, now_seconds)

    # Save any state changes that might have occurred during the turn.
    await self.conversation_state.save_changes(turn_context)
    await self.user_state.save_changes(turn_context)
```

**app.py**

Lastly, update `app.py` to send the `EXPIRE_AFTER_SECONDS` parameter to `DialogBot`:

```python
BOT = DialogBot(CONFIG.EXPIRE_AFTER_SECONDS, CONVERSATION_STATE, USER_STATE, DIALOG)
```

---

## Storage Expiration

Cosmos DB provides a Time To Live (TTL) feature which allows you to delete items automatically from a container after a certain time period.  This can be configured from within the Azure portal or during container creation (using the language-specific Cosmos DB SDKs).

The Bot Framework SDK does not expose a TTL configuration setting.  However, container initialization can be overridden and the Cosmos DB SDK can be used to configure TTL prior to Bot Framework storage initialization.

# [C#](#tab/csharp)

Start with a fresh copy of the **multi-turn prompt** sample, and add the `Microsoft.Bot.Builder.Azure` NuGet package to the project.

**appsettings.json**

Update appsettings.json to include Cosmos DB storage options:

```json
{
  "MicrosoftAppId": "",
  "MicrosoftAppPassword": "",

  "CosmosDbTimeToLive": 30,
  "CosmosDbEndpoint": "<endpoint-for-your-cosmosdb-instance>",
  "CosmosDbAuthKey": "<your-cosmosdb-auth-key>",
  "CosmosDbDatabaseId": "<your-database-id>",
  "CosmosDbUserStateContainerId": "<no-ttl-container-id>",
  "CosmosDbConversationStateContainerId": "<ttl-container-id>"
}
```

Notice the two ContainerIds, one for `UserState` and one for `ConversationState`.  This is because we are setting a default Time To Live on the `ConversationState` container, but not on `UserState`.

**CosmosDbStorageInitializerHostedService.cs**

Next, create a `CosmosDbStorageInitializerHostedService` class, which will create the container with the configured Time To Live.

```csharp
// Add required using statements...

public class CosmosDbStorageInitializerHostedService : IHostedService
{
    readonly CosmosDbPartitionedStorageOptions _storageOptions;
    readonly int _cosmosDbTimeToLive;

    public CosmosDbStorageInitializerHostedService(IConfiguration config)
    {
        _storageOptions = new CosmosDbPartitionedStorageOptions()
        {
            CosmosDbEndpoint = config["CosmosDbEndpoint"],
            AuthKey = config["CosmosDbAuthKey"],
            DatabaseId = config["CosmosDbDatabaseId"],
            ContainerId = config["CosmosDbConversationStateContainerId"]
        };

        _cosmosDbTimeToLive = config.GetValue<int>("CosmosDbTimeToLive");
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var client = new CosmosClient(
            _storageOptions.CosmosDbEndpoint,
            _storageOptions.AuthKey,
            _storageOptions.CosmosClientOptions ?? new CosmosClientOptions()))
        {
            // Create the contaier with the provided TTL
            var containerResponse = await client
                .GetDatabase(_storageOptions.DatabaseId)
                .DefineContainer(_storageOptions.ContainerId, "/id")
                .WithDefaultTimeToLive(_cosmosDbTimeToLive)
                .WithIndexingPolicy().WithAutomaticIndexing(false).Attach()
                .CreateIfNotExistsAsync(_storageOptions.ContainerThroughput)
                .ConfigureAwait(false);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
```

**Startup.cs**

Lastly, update `Startup.cs` to use the storage initializer, and Cosmos Db for state:

```csharp
// Existing code omitted...

// commented out MemoryStorage, since we are using CosmosDbPartitionedStorage instead
// services.AddSingleton<IStorage, MemoryStorage>();

// Add the Initializer as a HostedService (so it is called during the app service startup)
services.AddHostedService<CosmosDbStorageInitializerHostedService>();

// Create the storage options for User state
var userStorageOptions = new CosmosDbPartitionedStorageOptions()
{
    CosmosDbEndpoint = Configuration["CosmosDbEndpoint"],
    AuthKey = Configuration["CosmosDbAuthKey"],
    DatabaseId = Configuration["CosmosDbDatabaseId"],
    ContainerId = Configuration["CosmosDbUserStateContainerId"]
};

// Create the User state. (Used in this bot's Dialog implementation.)
services.AddSingleton(new UserState(new CosmosDbPartitionedStorage(userStorageOptions)));

// Create the storage options for Conversation state
var conversationStorageOptions = new CosmosDbPartitionedStorageOptions()
{
    CosmosDbEndpoint = Configuration["CosmosDbEndpoint"],
    AuthKey = Configuration["CosmosDbAuthKey"],
    DatabaseId = Configuration["CosmosDbDatabaseId"],
    ContainerId = Configuration["CosmosDbConversationStateContainerId"]
};

// Create the Conversation state. (Used by the Dialog system itself.)
services.AddSingleton(new ConversationState(new CosmosDbPartitionedStorage(conversationStorageOptions)));

// Existing code omitted...
```

# [JavaScript](#tab/javascript)

Start with a fresh copy of the **multi-turn prompt** sample.

**.env**

Update .env to include Cosmos DB storage options:

```txt
MicrosoftAppId=
MicrosoftAppPassword=

CosmosDbTimeToLive=30
CosmosDbEndpoint=<endpoint-for-your-cosmosdb-instance>
CosmosDbAuthKey=<your-cosmosdb-auth-key>
CosmosDbDatabaseId=<your-database-id>
CosmosDbUserStateContainerId=<no-ttl-container-id>
CosmosDbConversationStateContainerId=<ttl-container-id>
```

Notice the two ContainerIds, one for `UserState` and one for `ConversationState`.  This is because we are setting a default Time To Live on the `ConversationState` container, but not `UserState`.

**project.json**

Next, add the `botbuilder-azure` npm package to project.json.

```json
"dependencies": {
    "botbuilder": "~4.9.2",
    "botbuilder-dialogs": "~4.9.2",
    "botbuilder-azure": "~4.9.2",
    "dotenv": "^8.2.0",
    "path": "^0.12.7",
    "restify": "~8.5.1"
},
```

**index.js**

Add the necessary require statements to index.js:

```javascript
const { CosmosDbPartitionedStorage } = require('botbuilder-azure');
const { CosmosClient } = require('@azure/cosmos');
```

Replace `MemoryStorage`, `ConversationState` and `UserState` creation with:

```javascript
// const memoryStorage = new MemoryStorage();

// Storage options for Conversation State
const conversationStorageOptions = {
    cosmosDbEndpoint: process.env.CosmosDbEndpoint,
    authKey: process.env.CosmosDbAuthKey,
    databaseId: process.env.CosmosDbDatabaseId,
    containerId: process.env.CosmosDbConversationStateContainerId
};

// Create a cosmosClient, and set defaultTtl (with other properties)
var cosmosClient = new CosmosClient({
    endpoint: conversationStorageOptions.cosmosDbEndpoint,
    key: conversationStorageOptions.authKey,
    ...conversationStorageOptions.cosmosClientOptions,
});

// Create the container with the provided TTL.
Promise.resolve(cosmosClient
    .database(conversationStorageOptions.databaseId)
    .containers.createIfNotExists({
        id: conversationStorageOptions.containerId,
        partitionKey: {
            paths: ['/id']
        },
        defaultTtl: parseInt(process.env.CosmosDbTimeToLive, 10)
    }));

// Storage options for User State
const userStorageOptions = {
    cosmosDbEndpoint: process.env.CosmosDbEndpoint,
    authKey: process.env.CosmosDbAuthKey,
    databaseId: process.env.CosmosDbDatabaseId,
    containerId: process.env.CosmosDbUserStateContainerId
};

// Create state instances.
const conversationState = new ConversationState(new CosmosDbPartitionedStorage(conversationStorageOptions));
const userState = new UserState(new CosmosDbPartitionedStorage(userStorageOptions));
```

Finally, run npm install before starting your bot.

```cmd
npm install
```

# [Java](#tab/java)

Start with a fresh copy of the **multi-turn prompt** sample, and add the following dependencies to the pom.xml file:

```xml
<dependency>
    <groupId>com.microsoft.bot</groupId>
    <artifactId>bot-azure</artifactId>
    <version>4.13.0</version>
</dependency>
    <dependency>
    <groupId>com.azure</groupId>
    <artifactId>azure-cosmos</artifactId>
</dependency>
```

**application.properties**

Update application.properties to include Cosmos DB storage options:

```txt
MicrosoftAppId=
MicrosoftAppPassword=
server.port=3978

CosmosDbTimeToLive = 30
CosmosDbEndpoint = <endpoint-for-your-cosmosdb-instance>
CosmosDbAuthKey = <your-cosmosdb-auth-key>
CosmosDbDatabaseId = <your-database-id>
CosmosDbUserStateContainerId = <no-ttl-container-id>
CosmosDbConversationStateContainerId = <ttl-container-id>
```

Notice the two ContainerIds, one for `UserState` and one for `ConversationState`.  This is because we are setting a default Time To Live on the `ConversationState` container, but not on `UserState`.

**CosmosDbStorageInitializer.java**

Next, create a `CosmosDbStorageInitializer` class, which will create the container with the configured Time To Live.

```java
package com.microsoft.bot.sample.multiturnprompt;

import com.azure.cosmos.CosmosAsyncClient;
import com.azure.cosmos.CosmosClientBuilder;
import com.azure.cosmos.models.CosmosContainerProperties;
import com.microsoft.bot.azure.CosmosDbPartitionedStorageOptions;
import com.microsoft.bot.integration.Configuration;

public class CosmosDbStorageInitializer {

    final CosmosDbPartitionedStorageOptions storageOptions;
    final int cosmosDbTimeToLive;

    public CosmosDbStorageInitializer(Configuration configuration) {
        storageOptions = new CosmosDbPartitionedStorageOptions();
        storageOptions.setCosmosDbEndpoint(configuration.getProperty("CosmosDbEndpoint"));
        storageOptions.setAuthKey(configuration.getProperty("CosmosDbAuthKey"));
        storageOptions.setDatabaseId(configuration.getProperty("CosmosDbDatabaseId"));
        storageOptions.setContainerId(configuration.getProperty("CosmosDbConversationStateContainerId"));
        cosmosDbTimeToLive = configuration.getProperty("CosmosDbTimeToLive") != null
                ? Integer.parseInt(configuration.getProperty("CosmosDbTimeToLive"))
                : 30;
    }

    public void initialize() {

        CosmosAsyncClient client = new CosmosClientBuilder().endpoint(storageOptions.getCosmosDbEndpoint())
                .key(storageOptions.getAuthKey()).buildAsyncClient();

        client.createDatabaseIfNotExists(storageOptions.getDatabaseId()).block();
        CosmosContainerProperties cosmosContainerProperties = new CosmosContainerProperties(
                storageOptions.getContainerId(), "/id");
        cosmosContainerProperties.setDefaultTimeToLiveInSeconds(cosmosDbTimeToLive);
        client.getDatabase(storageOptions.getDatabaseId()).createContainerIfNotExists(cosmosContainerProperties)
                .block();
        client.close();
    }
}

```

**Application.java**

Lastly, update `Application.java` to use the storage initializer, and Cosmos Db for state:

```java
// Existing code omitted...

@Override
public ConversationState getConversationState(Storage storage) {
    Configuration configuration = getConfiguration();
    CosmosDbStorageInitializer initializer = new CosmosDbStorageInitializer(configuration);
    initializer.initialize();

    CosmosDbPartitionedStorageOptions storageOptions = new CosmosDbPartitionedStorageOptions();
    storageOptions.setCosmosDbEndpoint(configuration.getProperty("CosmosDbEndpoint"));
    storageOptions.setAuthKey(configuration.getProperty("CosmosDbAuthKey"));
    storageOptions.setDatabaseId(configuration.getProperty("CosmosDbDatabaseId"));
    storageOptions.setContainerId(configuration.getProperty("CosmosDbConversationStateContainerId"));
    return new ConversationState(new CosmosDbPartitionedStorage(storageOptions));
}

/**
 * Returns a UserState object. Default scope of Singleton.
 *
 * @param storage The Storage object to use.
 * @return A UserState object.
 */
@Override
public UserState getUserState(Storage storage) {
    Configuration configuration = getConfiguration();
    CosmosDbPartitionedStorageOptions storageOptions = new CosmosDbPartitionedStorageOptions();
    storageOptions.setCosmosDbEndpoint(configuration.getProperty("CosmosDbEndpoint"));
    storageOptions.setAuthKey(configuration.getProperty("CosmosDbAuthKey"));
    storageOptions.setDatabaseId(configuration.getProperty("CosmosDbDatabaseId"));
    storageOptions.setContainerId(configuration.getProperty("CosmosDbUserStateContainerId"));
    return new UserState(new CosmosDbPartitionedStorage(storageOptions));
}
```

## [Python](#tab/python)

Start with a fresh copy of the **multi-turn prompt** sample.

**config.py**

Update `config.py` to include Cosmos DB storage options:

```python
PORT = 3978
APP_ID = os.environ.get("MicrosoftAppId", "")
APP_PASSWORD = os.environ.get("MicrosoftAppPassword", "")

COSMOSDB_TTL = os.environ.get("CosmosDbTimeToLive", 30)
COSMOSDB_ENDPOINT = os.environ.get("CosmosDbEndpoint", "<endpoint-for-your-cosmosdb-instance>")
COSMOSDB_AUTH_KEY = os.environ.get("CosmosDbAuthKey", "<your-cosmosdb-auth-key>")
COSMOSDB_DATABASE_ID = os.environ.get("CosmosDbDatabaseId", "<your-database-id>")
COSMOSDB_USER_STATE_CONTAINER_ID = os.environ.get("CosmosDbUserStateContainerId", "<no-ttl-container-id>")
COSMOSDB_CONVERSATION_STATE_CONTAINER_ID = os.environ.get("CosmosDbConversationStateContainerId", "<ttl-container-id>")
```

Notice the two ContainerIds, one for `UserState` and one for `ConversationState`.  This is because we are setting a default Time To Live on the `ConversationState` container, but not `UserState`.

**requirements.txt**

Next, add the `botbuilder-azure` package to requirements.txt.

```txt
botbuilder-integration-aiohttp>=4.10.0
botbuilder-dialogs>=4.10.0
botbuilder-ai>=4.10.0
botbuilder-azure>=4.10.0
```

Then, run pip install:

```cmd
pip install -r requirements.txt
```

**app.py**

```python
client = cosmos_client.CosmosClient(
    CONFIG.COSMOSDB_ENDPOINT, {"masterKey": CONFIG.COSMOSDB_AUTH_KEY},
)

containers = list(client.QueryContainers("dbs/" + CONFIG.COSMOSDB_DATABASE_ID, {
     "query": "SELECT * FROM r WHERE r.id=@id",
        "parameters": [
            {"name": "@id", "value": CONFIG.COSMOSDB_CONVERSATION_STATE_CONTAINER_ID}
        ],
    }))

if len(containers) < 1:
    new_container = client.CreateContainer(
        "dbs/" + CONFIG.COSMOSDB_DATABASE_ID,
        {
            "defaultTtl": CONFIG.COSMOSDB_TTL,
            "id": CONFIG.COSMOSDB_CONVERSATION_STATE_CONTAINER_ID,
            "partitionKey": {"paths": ["/id"], "kind": "Hash",},
        },
    )
```

---

Cosmos DB will now automatically delete conversation state records after 30 seconds of inactivity.

<!-- 
## Proactive Expiration
add Azure Function with ConversationReference solution 
-->

For more information, see [Configure time to live in Azure Cosmos DB][cosmos-ttl]

## To test the bot

1. If you have not done so already, install the [Bot Framework Emulator][emulator-readme].
1. Run the sample locally on your machine.
1. Start the Emulator, connect to your bot, and send a message to it.
1. After one of the prompts, wait 30 seconds before responding.

<!-- Footnote-style links -->

[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md

[cs-sample]: https://aka.ms/cs-multi-prompts-sample
[js-sample]: https://aka.ms/js-multi-prompts-sample
[java-sample]: https://aka.ms/java-multi-prompts-sample
[python-sample]: https://aka.ms/python-multi-prompts-sample

[cosmos-ttl]: /azure/cosmos-db/how-to-time-to-live
[emulator-readme]: https://aka.ms/bot-framework-emulator-readme
