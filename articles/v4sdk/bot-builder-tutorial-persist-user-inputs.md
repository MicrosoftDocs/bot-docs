---
title: Persist user data | Microsoft Docs
description: Learn how to save user state data to storage in the Bot Builder SDK.
keywords: persist user data, storage, conversation data 
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 09/19/2018
monikerRange: 'azure-bot-service-4.0'
---

# Persist user data

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

When the bot ask users for input, chances are that you would want to persist some of the information to storage of some form. The Bot Builder SDK allows you to store user inputs using *in-memory storage* or database storage such as *CosmosDB*. Local storage types are mainly used during testing or prototyping of your bot. However, persistent storage types, such as database storage, are best for production bots.

This topic shows you how to define your storage object and save user inputs to the storage object so that it can be persisted. We'll use a dialog to ask the user for their name, if we don't already have it. Regardless of the storage type you choose to use, the process for hooking it up and persisting data is the same. The code in this topic uses `CosmosDB` as the storage to persist data.

## Prerequisites

Certain resources are required, based on the development environment you want to use.

# [C#](#tab/csharp)

* [Install Visual Studio 2015 or greater](https://www.visualstudio.com/downloads/).
* [Install the BotBuilder V4 Template](https://marketplace.visualstudio.com/items?itemName=BotBuilder.botbuilderv4).

# [JavaScript](#tab/javascript)

* [Install Visual Studio Code](https://www.visualstudio.com/downloads/).
* [Install Node.js v8.5 or greater](https://nodejs.org/en/).
* [Install Yeoman](http://yeoman.io/).
* Install the Node.js v4 Botbuilder template generator.

    ```shell
    npm install generator-botbuilder
    ```

---

This tutorial makes use of the following packages.

# [C#](#tab/csharp)

We'll start from a basic EchoBot template. For instructions, see the [quickstart for .NET](~/dotnet/bot-builder-dotnet-quickstart.md).

Install these additional packages from the NuGet packet manager.

* **Microsoft.Bot.Builder.Azure**
* **Microsoft.Bot.Builder.Dialogs**

# [JavaScript](#tab/javascript)

We'll start from a basic EchoBot template. For instructions, see the [quickstart for JavaScript](~/javascript/bot-builder-javascript-quickstart.md).

Install these additional npm packages.

```cmd
npm install --save botbuilder-dialogs
npm install --save botbuilder-azure
```

---

To test the bot you create in this tutorial, you will need to install the [BotFramework Emulator](https://github.com/Microsoft/BotFramework-Emulator).

## Create a CosmosDB service and update your application settings

To set up a CosmosDB service and a database, follow the instructions for [using CosmosDB](bot-builder-howto-v4-storage.md#using-cosmos-db). The steps are summarized here:

1. In a new browser window, sign in to the <a href="http://portal.azure.com/" target="_blank">Azure portal</a>.
1. Click **Create a resource > Databases > Azure Cosmos DB**.
1. In the **New account** page, provide a unique name in the **ID** field. For **API**, select **SQL**, and provide **Subscription**, **Location**, and **Resource group** information.
1. Click **Create**.

Then, add a collection to that service for use with this bot.

Record the database ID and collection ID you used to add the collection, and also the URI and primary key from the collection's keys settings, as we will need these to connect our bot to the service.

### Update your application settings

# [C#](#tab/csharp)

Update your **appsettings.json** file to include the connection information for CosmosDB .

```csharp
{
  // Settings for CosmosDB.
  "CosmosDB": {
    "DatabaseID": "<your-database-identifier>",
    "CollectionID": "<your-collection-identifier>",
    "EndpointUri": "<your-CosmosDB-endpoint>",
    "AuthenticationKey": "<your-primary-key>"
  }
}
```

# [JavaScript](#tab/javascript)

In your project folder, locate the **.env** file and add these entries with your Cosmos specific data to it.

**.env**

```text
DB_SERVICE_ENDPOINT=<your-CosmosDB-endpoint>
AUTH_KEY=<authentication key>
DATABASE=<your-primary-key>
COLLECTION=<your-collection-identifier>
```

Then, in your bot's main **index.js** file, replace `storage` to use `CosmosDbStorage` instead of `MemoryStorage`. During run time, the environment variables will be pulled in and populate these fields.

```javascript
const storage = new CosmosDbStorage({
    serviceEndpoint: process.env.DB_SERVICE_ENDPOINT,
    authKey: process.env.AUTH_KEY, 
    databaseId: process.env.DATABASE,
    collectionId: process.env.COLLECTION
});
```

---

## Create storage, state manager, and state property accessor objects

Bots use state management and storage objects to manage and persist state. The manager provides an abstraction layer that lets you access state properties using state property accessors, independent of the type of underlying storage. Use the state manager to write data to storage.

# [C#](#tab/csharp)

### Define a class for your user data

Rename the file **CounterState.cs** to **UserData.cs**, and rename the **CounterState** class to **UserData**.

Update this class to hold the data you will collect.

```csharp
/// <summary>
/// Class for storing persistent user data.
/// </summary>
public class UserData
{
    public string Name { get; set; }
}
```

### Define a class for your state and state property accessor objects

Rename the file **EchoBotAccessors.cs** to **BotAccessors.cs**, and rename the **EchoBotAccessors** class to **BotAccessors**.

Update this class to store the state objects and state property accessors your bot will need.

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using System;

public class BotAccessors
{
    public UserState UserState { get; }

    public ConversationState ConversationState { get; }

    public IStatePropertyAccessor<DialogState> DialogStateAccessor { get; set; }

    public IStatePropertyAccessor<UserData> UserDataAccessor { get; set; }

    public BotAccessors(UserState userState, ConversationState conversationState)
    {
        this.UserState = userState
            ?? throw new ArgumentNullException(nameof(userState));

        this.ConversationState = conversationState
            ?? throw new ArgumentNullException(nameof(conversationState));
    }
}
```

### Update the startup code for your bot

In your **Startup.cs** file, update your using statements.

```csharp
using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Integration;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Configuration;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
```

In your `ConfigureServices` method, update the add bot call, starting from where you create the backing storage object, and then register your bot accessors object.

We need conversation state for the `DialogState` object to track the dialog state. We're registering singletons for the dialog state property accessor and the dialog set that our bot will use. The bot will create its own state property accessor for the user state.

The `BotAccessors` accessor is an efficient way to manage storage for multiple state objects of your bot.

```cs
public void ConfigureServices(IServiceCollection services)
{
    // Register your bot.
    services.AddBot<UserDataBot>(options =>
    {
        // ...

        // Use persistent storage and create state management objects.
        var cosmosSettings = Configuration.GetSection("CosmosDB");
        IStorage storage = new CosmosDbStorage(
            new CosmosDbStorageOptions
            {
                DatabaseId = cosmosSettings["DatabaseID"],
                CollectionId = cosmosSettings["CollectionID"],
                CosmosDBEndpoint = new Uri(cosmosSettings["EndpointUri"]),
                AuthKey = cosmosSettings["AuthenticationKey"],
            });
        options.State.Add(new ConversationState(storage));
        options.State.Add(new UserState(storage));
    });

    // Register the bot's state and state property accessor objects.
    services.AddSingleton<BotAccessors>(sp =>
    {
        var options = sp.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;
        var userState = options.State.OfType<UserState>().FirstOrDefault();
        var conversationState = options.State.OfType<ConversationState>().FirstOrDefault();

        return new BotAccessors(userState, conversationState)
        {
            UserDataAccessor = userState.CreateProperty<UserData>("UserDataBot.UserData"),
            DialogStateAccessor = conversationState.CreateProperty<DialogState>("UserDataBot.DialogState"),
        };
    });
}
```

# [JavaScript](#tab/javascript)

### Update your server code

In your project's **index.js** file, update the following require statements.

```javascript
// Import required bot services.
const { BotFrameworkAdapter, ConversationState, UserState } = require('botbuilder');
const { CosmosDbStorage } = require('botbuilder-azure');
```

We will be using the `UserState` to store data for this tutorial. We need to create a new `userState` object and update this line of code to pass in a second parameter to the `MainDialog` class.

```javascript
// Create conversation state with in-memory storage provider.
const conversationState = new ConversationState(storage);
const userState = new UserState(storage);

// Create the main dialog.
const bot = new MyBot(conversationState, userState);
```

If we encounter a general error, clear out both conversation and user state.

```javascript
// Catch-all for errors.
adapter.onTurnError = async (context, error) => {
    // This check writes out errors to console log .vs. app insights.
    console.error(`\n [onTurnError]: ${error}`);
    // Send a message to the user
    context.sendActivity(`Oops. Something went wrong!`);
    // Clear out state
    await conversationState.load(context);
    await conversationState.clear(context);
    await userState.load(context);
    await userState.clear(context);
    // Save state changes.
    await conversationState.saveChanges(context);
    await userState.saveChanges(context);
};
```

And update the HTTP server loop to call our bot object.

```javascript
// Listen for incoming requests.
server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (context) => {
        // Route to main dialog.
        await bot.onTurn(context);
    });
});
```

### Update your bot logic

In the `MyBot` class, require the necessary libraries for your bot to operate. In this tutorial, we will be using the **Dialogs** library.

```javascript
// Required packages for this bot
const { ActivityTypes } = require('botbuilder');
const { DialogSet, WaterfallDialog, TextPrompt, NumberPrompt } = require('botbuilder-dialogs');

```

Update the `MyBot` class's constructor to accept a second parameter, `userState`. Also, update the constructor to define the states, dialogs, and prompts we need for this tutorial. In this case, we are defining a two step waterfall where _step 1_ asks the user for their name and _step 2_ returns the user's response. It's up to the bot's main logic to persist that information.

```javascript
constructor(conversationState, userState) {

    // creates a new state accessor property.
    this.conversationState = conversationState;
    this.userState = userState;

    this.dialogState = this.conversationState.createProperty('dialogState');
    this.userDataAccessor = this.userState.createProperty('userData');

    this.dialogs = new DialogSet(this.dialogState);

    // Add prompts
    this.dialogs.add(new TextPrompt('textPrompt'));

    // Add a waterfall dialog to collect and return the user's name.
    this.dialogs.add(new WaterfallDialog('greetings', [
        async function (step) {
            return await step.prompt('textPrompt', "What is your name?");
        },
        async function (step) {
            return await step.endDialog(step.result);
        }
    ]));
}
```

---

When it comes time to save user data, you have some choices. The SDK provides a few state objects with different scopes that you can choose from. Here, we're using conversation state to manage the dialog state object and user state to manage user data.

For more information about storage and state in general, see [storage](bot-builder-howto-v4-storage.md) and [how to manage conversation and user state](bot-builder-howto-v4-state.md).

## Create a greeting dialog

We'll use a dialog to collect the user's name. To keep this scenario simple, the dialog will return the user's name, and the bot will manage the user data object and state.

Create a **GreetingsDialog** class and include the following code.

# [C#](#tab/csharp)

```cs
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

/// <summary>Defines a dialog for collecting a user's name.</summary>
public class GreetingsDialog : DialogSet
{
    /// <summary>The ID of the main dialog.</summary>
    public const string MainDialog = "main";

    /// <summary>The ID of the the text prompt to use in the dialog.</summary>
    private const string TextPrompt = "textPrompt";

    /// <summary>Creates a new instance of this dialog set.</summary>
    /// <param name="dialogState">The dialog state property accessor to use for dialog state.</param>
    public GreetingsDialog(IStatePropertyAccessor<DialogState> dialogState)
        : base(dialogState)
    {
        // Add the text prompt to the dialog set.
        Add(new TextPrompt(TextPrompt));

        // Define the main dialog and add it to the set.
        Add(new WaterfallDialog(MainDialog, new WaterfallStep[]
        {
            async (stepContext, cancellationToken) =>
            {
                // Ask the user for their name.
                return await stepContext.PromptAsync(TextPrompt, new PromptOptions
                {
                    Prompt = MessageFactory.Text("What is your name?"),
                }, cancellationToken);
            },
            async (stepContext, cancellationToken) =>
            {
                // Assume that they entered their name, and return the value.
                return await stepContext.EndDialogAsync(stepContext.Result, cancellationToken);
            },
        }));
    }
}
```

# [JavaScript](#tab/javascript)

See the section above where the dialog is created within the `MainDialog`'s constructor.

---

For more information about dialogs, see [how to prompt for input](bot-builder-prompts.md) and [how to manage simple conversation flow](bot-builder-dialog-manage-conversation-flow.md).

## Update your bot to use the dialog and user state

We'll discuss bot construction and managing user input separately.

### Add the dialog and a user state accessor

We need to track the dialog instance and the state property accessor for user data.

# [C#](#tab/csharp)

Add code to initialize our bot.

```cs
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;

/// <summary>Defines the bot for the persisting user data tutorial.</summary>
public class UserDataBot : IBot
{
    /// <summary>The bot's state and state property accessor objects.</summary>
    private BotAccessors Accessors { get; }

    /// <summary>The dialog set that has the dialog to use.</summary>
    private GreetingsDialog GreetingsDialog { get; }

    /// <summary>Create a new instance of the bot.</summary>
    /// <param name="options">The options to use for our app.</param>
    /// <param name="greetingsDialog">An instance of the dialog set.</param>
    public UserDataBot(BotAccessors botAccessors)
    {
        // Retrieve the bot's state and accessors.
        Accessors = botAccessors;

        // Create the greetings dialog.
        GreetingsDialog = new GreetingsDialog(Accessors.DialogStateAccessor);
    }
}
```

# [JavaScript](#tab/javascript)

See the section above where these state accessors are defined within the `MainDialog`'s constructor.

---

### Update the turn handler

The turn handler will greet the user when they first join the conversation, and respond to them whenever they send the bot a message. If at any point the bot doesn't know the user's name already, it will start the greeting dialog to ask for their name. When the dialog completes, we'll save their name to user state using a state object generated by our state property accessor. When the turn ends, the accessor and state manager will write changes to the object out to storage.

We'll also add support for the _delete user data_ activity.

# [C#](#tab/csharp)

Update the bot's `OnTurnAsync` method.

```cs
/// <summary>Handles incoming activities to the bot.</summary>
/// <param name="turnContext">The context object for the current turn.</param>
/// <param name="cancellationToken">A cancellation token that can be used by other objects
/// or threads to receive notice of cancellation.</param>
/// <returns>A task that represents the work queued to execute.</returns>
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
{
    // Retrieve user data from state.
    UserData userData = await Accessors.UserDataAccessor.GetAsync(turnContext, () => new UserData());

    // Establish context for our dialog from the turn context.
    DialogContext dc = await GreetingsDialog.CreateContextAsync(turnContext);

    // Handle conversation update, message, and delete user data activities.
    switch (turnContext.Activity.Type)
    {
        case ActivityTypes.ConversationUpdate:

            // Greet any user that is added to the conversation.
            IConversationUpdateActivity activity = turnContext.Activity.AsConversationUpdateActivity();
            if (activity.MembersAdded.Any(member => member.Id != activity.Recipient.Id))
            {
                if (userData.Name is null)
                {
                    // If we don't already have their name, start a dialog to collect it.
                    await turnContext.SendActivityAsync("Welcome to the User Data bot.");
                    await dc.BeginDialogAsync(GreetingsDialog.MainDialog);
                }
                else
                {
                    // Otherwise, greet them by name.
                    await turnContext.SendActivityAsync($"Hi {userData.Name}! Welcome back to the User Data bot.");
                }
            }

            break;

        case ActivityTypes.Message:

            // If there's a dialog running, continue it.
            if (dc.ActiveDialog != null)
            {
                var dialogTurnResult = await dc.ContinueDialogAsync();
                if (dialogTurnResult.Status == DialogTurnStatus.Complete
                    && dialogTurnResult.Result is string name
                    && !string.IsNullOrWhiteSpace(name))
                {
                    // If it completes successfully and returns a valid name, save the name and greet the user.
                    userData.Name = name;
                    await turnContext.SendActivityAsync($"Pleased to meet you {userData.Name}.");
                }
            }
            else if (userData.Name is null)
            {
                // Else, if we don't have the user's name yet, ask for it.
                await dc.BeginDialogAsync(GreetingsDialog.MainDialog);
            }
            else
            {
                // Else, echo the user's message text.
                await turnContext.SendActivityAsync($"{userData.Name} said, '{turnContext.Activity.Text}'.");
            }

            break;

        case ActivityTypes.DeleteUserData:

            // Delete the user's data.
            userData.Name = null;
            await turnContext.SendActivityAsync("I have deleted your user data.");

            break;
    }

    // Update the user data in the turn's state cache.
    await Accessors.UserDataAccessor.SetAsync(turnContext, userData, cancellationToken);

    // Persist any changes to storage.
    await Accessors.UserState.SaveChangesAsync(turnContext, false, cancellationToken);
    await Accessors.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
}
```

# [JavaScript](#tab/javascript)

Update the bot's `onTurn` handler.

```javascript
async onTurn(turnContext) {
    const dc = await this.dialogs.createContext(turnContext); // Create dialog context
    const userData = await this.userDataAccessor.get(turnContext, {});

    switch (turnContext.activity.type) {
        case ActivityTypes.ConversationUpdate:
            if (turnContext.activity.membersAdded[0].name !== 'Bot') {
                if (userData.name) {
                    await turnContext.sendActivity(`Hi ${userData.name}! Welcome back to the User Data bot.`);
                }
                else {
                    // send a "this is what the bot does" message
                    await turnContext.sendActivity('Welcome to the User Data bot.');
                    await dc.beginDialog('greetings');
                }
            }
            break;
        case ActivityTypes.Message:
            // If there is an active dialog running, continue it
            if (dc.activeDialog) {
                var turnResult = await dc.continueDialog();
                if (turnResult.status == "complete" && turnResult.result) {
                    // If it completes successfully and returns a value, save the name and greet the user.
                    userData.name = turnResult.result;
                    await this.userDataAccessor.set(turnContext, userData);
                    await turnContext.sendActivity(`Pleased to meet you ${userData.name}.`);
                }
            }
            // Else, if we don't have the user's name yet, ask for it.
            else if (!userData.name) {
                await dc.beginDialog('greetings');
            }
            // Else, echo the user's message text.
            else {
                await turnContext.sendActivity(`${userData.name} said, ${turnContext.activity.text}.`);
            }
            break;
        case ActivityTypes.DeleteUserData:
            // Delete the user's data.
            // Note: You can use the Emulator to send this activity.
            userData.name = null;
            await this.userDataAccessor.set(turnContext, userData);
            await turnContext.sendActivity("I have deleted your user data.");
            break;
    }

    // Save changes to the conversation and user states.
    await this.conversationState.saveChanges(turnContext);
    await this.userState.saveChanges(turnContext);
}
```

---

## Start your bot in Visual Studio

Build and run your application.

## Start the emulator and connect your bot

Next, start the emulator and then connect to your bot in the emulator:

1. Click the **Open Bot** link in the emulator "Welcome" tab.
2. Select the .bot file located in the directory where you created the Visual Studio solution.

## Interact with your bot

Send a message to your bot, and the bot will respond back with a message.
![Emulator running](../media/emulator-v4/emulator-running.png)

## Next steps

> [!div class="nextstepaction"]
> [Manage conversation and user state](bot-builder-howto-v4-state.md)
