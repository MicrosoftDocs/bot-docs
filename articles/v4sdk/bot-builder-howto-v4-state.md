---
title: Save user and conversation data | Microsoft Docs
description: Learn how to save and retrieve state data with the Bot Builder SDK.
keywords: conversation state, user state, conversation, saving state, managing bot state
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 11/26/18
monikerRange: 'azure-bot-service-4.0'
---

# Save user and conversation data

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

A bot is inherently stateless. Once your bot is deployed, it may not run in the same process or on the same machine from one turn to the next. However, your bot may need to track the context of a conversation, so that it can manage its behavior and remember answers to previous questions. The state and storage features of the SDK allow you to add state to your bot.

## Prerequisites

- Knowledge of [bot basics](bot-builder-basics.md) and how bots [manage state](bot-builder-concept-state.md) is required.
- The code in this article is based on the **StateBot** sample. You'll need a copy of the sample in either [C#](https://github.com/Microsoft/BotFramework-Samples/tree/master/SDKV4-Samples/dotnet_core/StateBot) or [JS]().
- The [Bot Framework Emulator](https://aka.ms/Emulator-wiki-getting-started), to test the bot locally.

## About the sample code

This article discusses the configuration aspects of managing your bot's state. To add state, we configure state properties, state management, and storage, and then use those in the bot.

- Each _state property_ contains state information for your bot.
- Each state property accessor allows you to get or set the value of the associated state property.
- Each state management object automates the reading and writing of associated state information to storage.
- The storage layer connects to the backing storage for state, such as in-memory (for testing), or Azure Cosmos DB Storage (for production).

We need to configure the bot with state property accessors with which it can get and set state at run-time, when it is handling an activity. A state property accessor is created using a state management object, and a state management object is created using a storage layer. So, we'll start at the storage level and work up from there.

## Configure storage

Since we don't plan to deploy this bot, we'll use _memory storage_, which we'll use this to configure both user and conversation state in the next step.

### [C#](#tab/csharp)

In **Startup.cs**, configure the storage layer.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // ...
    IStorage storage = new MemoryStorage();
    // ...
}
```

### [JavaScript](#tab/javascript)

In your **index.js** file, configure the storage layer.

```javascript
// Define state store for your bot.
const memoryStorage = new MemoryStorage();
```

---

## Create state management objects

We track both _user_ and _conversation_ state, and use these to create  _state property accessors_ in the next step.

### [C#](#tab/csharp)

In **Startup.cs**, reference the storage layer when you create your state management objects.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // ...
    ConversationState conversationState = new ConversationState(storage);
    UserState userState = new UserState(storage);
    // ...
}
```

### [JavaScript](#tab/javascript)

In your **index.js** file, add `UserState` to your require statement.

```javascript
const { BotFrameworkAdapter, MemoryStorage, ConversationState, UserState } = require('botbuilder');
```

Then, reference the storage layer when you create your conversation and user state management objects.

```javascript
// Create conversation and user state with in-memory storage provider.
const conversationState = new ConversationState(memoryStorage);
const userState = new UserState(memoryStorage);
```

---

## Create state property accessors

To _declare_ a state property, first create a state property accessor, using one of our state management objects. We configure the bot to track the following information:

- The user's name, which we'll define in user state.
- Whether we've just asked the user for their name and some additional information about the message they just sent.

The bot uses the accessor to get the state property from the turn context.

### [C#](#tab/csharp)

We first define classes to contain all the information that we want to manage in each type of state.

- A `UserProfile` class for the user information that the bot will collect.
- A `ConversationData` class to tack information about when a message arrived and who sent the message.

```csharp
// Defines a state property used to track information about the user.
public class UserProfile
{
    public string Name { get; set; }
}
```

```csharp
// Defines a state property used to track conversation data.
public class ConversationData
{
    // The time-stamp of the most recent incoming message.
    public string Timestamp { get; set; }

    // The ID of the user's channel.
    public string ChannelId { get; set; }

    // Track whether we have already asked the user's name
    public bool PromptedUserForName { get; set; } = false;
}
```

Next, we define a class that contains the state management information we'll need to configure our bot instance.

```csharp
public class StateBotAccessors
{
    public StateBotAccessors(ConversationState conversationState, UserState userState)
    {
        ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
        UserState = userState ?? throw new ArgumentNullException(nameof(userState));
    }
  
    public static string UserProfileName { get; } = "UserProfile";

    public static string ConversationDataName { get; } = "ConversationData";

    public IStatePropertyAccessor<UserProfile> UserProfileAccessor { get; set; }

    public IStatePropertyAccessor<ConversationData> ConversationDataAccessor { get; set; }
  
    public ConversationState ConversationState { get; }
  
    public UserState UserState { get; }
}
```

### [JavaScript](#tab/javascript)

We pass the state management objects directly to the bot's constructor, and let the bot create the state property accessors itself.

In **index.js**, provide the state management objects when you create the bot.

```javascript
// Create the bot.
const myBot = new MyBot(conversationState, userState);
```

In **bot.js**, define the identifiers you'll need for managing and tracking state.

```javascript
// The accessor names for the conversation data and user profile state property accessors.
const CONVERSATION_DATA_PROPERTY = 'conversationData';
const USER_PROFILE_PROPERTY = 'userProfile';
```

---

## Configure your bot

Now we're ready to define the state property accessors and configure our bot.
We'll use the conversation state management object for the conversation flow state property accessor.
We'll use the user state management object for the user profile state property accessor.

### [C#](#tab/csharp)

In **Startup.cs**, we configure ASP.NET to provide the bundled state property and management objects. This will be retrieved from the bot's constructor through the dependency injection framework in ASP.NET Core.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // ...
    services.AddSingleton<StateBotAccessors>(sp =>
    {
        // Create the custom state accessor.
        return new StateBotAccessors(conversationState, userState)
        {
            ConversationDataAccessor = conversationState.CreateProperty<ConversationData>(StateBotAccessors.ConversationDataName),
            UserProfileAccessor = userState.CreateProperty<UserProfile>(StateBotAccessors.UserProfileName),
        };
    });
}
```

In the bot's constructor, the `CustomPromptBotAccessors` object is provided when ASP.NET creates the bot.

```csharp
// Defines a bot for filling a user profile.
public class CustomPromptBot : IBot
{
    private readonly StateBotAccessors _accessors;

    public StateBot(StateBotAccessors accessors, ILoggerFactory loggerFactory)
    {
        // ...
        accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));
    }

    // The bot's turn handler and other supporting code...
}
```

### [JavaScript](#tab/javascript)

In the bot's constructor (in the **bot.js** file), we create the state property accessors and add them to the bot. We also add references to the state management objects, as we need these to save any state changes we make.

```javascript
constructor(conversationState, userState) {
    // Create the state property accessors for the conversation data and user profile.
    this.conversationData = conversationState.createProperty(CONVERSATION_DATA_PROPERTY);
    this.userProfile = userState.createProperty(USER_PROFILE_PROPERTY);

    // The state management objects for the conversation and user state.
    this.conversationState = conversationState;
    this.userState = userState;
}
```

---

## Access state from your bot

The preceding sections cover the initialization-time steps to add our state property accessors to our bot.
Now, we can use those accessors at run-time to read and write state information.

1. Before we use our state properties, we use each accessor to load the property from storage and get it from the state cache.
   - Whenever you get a state property via its accessor, you should provide a default value. Otherwise, you can get a null value error.
1. Before we exit the turn handler:
   1. We use the accessors' _set_ method to push changes to the bot state.
   1. We use the state management objects' _save changes_ method to write those changes to storage.

### [C#](#tab/csharp)

```csharp
// The bot's turn handler.
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
{
    if (turnContext.Activity.Type == ActivityTypes.Message)
    {
        // Get the state properties from the turn context.
        UserProfile userProfile =
            await _accessors.UserProfileAccessor.GetAsync(turnContext, () => new UserProfile());
        ConversationData conversationData =
            await _accessors.ConversationDataAccessor.GetAsync(turnContext, () => new ConversationData());

        if (string.IsNullOrEmpty(userProfile.Name))
        {
            // First time around this is set to false, so we will prompt user for name.
            if (conversationData.PromptedUserForName)
            {
                // Set the name to what the user provided
                userProfile.Name = turnContext.Activity.Text?.Trim();

                // Acknowledge that we got their name.
                await turnContext.SendActivityAsync($"Thanks {userProfile.Name}.");

                // Reset the flag to allow the bot to go though the cycle again.
                conversationData.PromptedUserForName = false;
            }
            else
            {
                // Prompt the user for their name.
                await turnContext.SendActivityAsync($"What is your name?");

                // Set the flag to true, so we don't prompt in the next turn.
                conversationData.PromptedUserForName = true;
            }

            // Save user state and save changes.
            await _accessors.UserProfileAccessor.SetAsync(turnContext, userProfile);
            await _accessors.UserState.SaveChangesAsync(turnContext);
        }
        else
        {
            // Add message details to the conversation data.
            conversationData.Timestamp = turnContext.Activity.Timestamp.ToString();
            conversationData.ChannelId = turnContext.Activity.ChannelId.ToString();

            // Display state data
            await turnContext.SendActivityAsync($"{userProfile.Name} sent: {turnContext.Activity.Text}");
            await turnContext.SendActivityAsync($"Message received at: {conversationData.Timestamp}");
            await turnContext.SendActivityAsync($"Message received from: {conversationData.ChannelId}");
        }

        // Update conversation state and save changes.
        await _accessors.ConversationDataAccessor.SetAsync(turnContext, conversationData);
        await _accessors.ConversationState.SaveChangesAsync(turnContext);
    }
}
```

### [JavaScript](#tab/javascript)

```javascript
// The bot's turn handler.
async onTurn(turnContext) {
    if (turnContext.activity.type === ActivityTypes.Message) {
        // Get the state properties from the turn context.
        const userProfile = await this.userProfile.get(turnContext, {});
        const conversationData = await this.conversationData.get(
            turnContext, { promptedForUserName: false });

        if (!userProfile.name) {
            // First time around this is undefined, so we will prompt user for name.
            if (conversationData.promptedForUserName) {
                // Set the name to what the user provided.
                userProfile.name = turnContext.activity.text;

                // Acknowledge that we got their name.
                await turnContext.sendActivity(`Thanks ${userProfile.name}.`);

                // Reset the flag to allow the bot to go though the cycle again.
                conversationData.promptedForUserName = false;
            } else {
                // Prompt the user for their name.
                await turnContext.sendActivity('What is your name?');

                // Set the flag to true, so we don't prompt in the next turn.
                conversationData.promptedForUserName = true;
            }
            // Save user state and save changes.
            await this.userProfile.set(turnContext, userProfile);
            await this.userState.saveChanges(turnContext);
        } else {
            // Add message details to the conversation data.
            conversationData.timestamp = turnContext.activity.timestamp.toLocaleString();
            conversationData.channelId = turnContext.activity.channelId;

            // Display state data.
            await turnContext.sendActivity(`${userProfile.name} sent: ${turnContext.activity.text}`);
            await turnContext.sendActivity(`Message received at: ${conversationData.timestamp}`);
            await turnContext.sendActivity(`Message received from: ${conversationData.channelId}`);
        }
        // Update conversation state and save changes.
        await this.conversationData.set(turnContext, conversationData);
        await this.conversationState.saveChanges(turnContext);
    }
}
```

---

## Test the bot

1. Run the sample locally on your machine. If you need instructions, refer to the README file for [C#](https://github.com/Microsoft/BotFramework-Samples/tree/master/SDKV4-Samples/dotnet_core/StateBot) or [JS](https://github.com/Microsoft/BotFramework-Samples/tree/master/SDKV4-Samples/js/stateBot) sample.
1. Use the emulator to test the bot as shown below.

![test state bot sample](media/state-bot-testing-emulator.png)

## Additional resources

**Privacy:** If you intend to store user's personal data, you should ensure compliance with [General Data Protection Regulation](https://blog.botframework.com/2018/04/23/general-data-protection-regulation-gdpr).

**State management:** All of the state management calls are asynchronous, and last-writer-wins by default. In practice, you should get, set, and save state as close together in your bot as possible.

**Critical business data:** Use bot state to store preferences, user name, or the last thing they ordered, but do not use it to store critical business data. For critical data, [create your own storage components](bot-builder-custom-storage.md) or write directly to [storage](bot-builder-howto-v4-storage.md).

**Recognizer-Text:** The sample uses the Microsoft/Recognizers-Text libraries to parse and validate user input. For more information, see the [overview](https://github.com/Microsoft/Recognizers-Text#microsoft-recognizers-text-overview) page.

## Next step

Now that you know how to configure state to help you read and write bot data to storage, let's learn how ask the user a series of questions, validate their answers, and save their input.

> [!div class="nextstepaction"]
> [Create your own prompts to gather user input](bot-builder-primitive-prompts.md).
