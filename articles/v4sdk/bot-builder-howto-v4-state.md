---
title: Manage conversation and user state | Microsoft Docs
description: Learn how to save and retrieve state data with the Bot Builder SDK for .NET.
keywords: conversation state, user state, conversation flow
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 09/18/18
monikerRange: 'azure-bot-service-4.0'
---

# Manage conversation and user state

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

A key to good bot design is to track the context of a conversation, so that your bot remembers things like the answers to previous questions. Depending on what your bot is used for, you may even need to keep track of state or store information for longer than the lifetime of the conversation. A bot's *state* is information it remembers in order to respond appropriately to incoming messages. The Bot Builder SDK provides two classes for storing and retrieving state data as an object associated with a user or a conversation.

- **Conversation state** help your bot keep track of the current conversation the bot is having with the user. If your bot needs to complete a sequence of steps or switch between conversation topics, you can use conversation properties to manage steps in a sequence or track the current topic. 

- **User state** can be used for many purposes, such as determining where the user's prior conversation left off or simply greeting a returning user by name. If you store a user's preferences, you can use that information to customize the conversation the next time you chat. For example, you might alert the user to a news article about a topic that interests her, or alert a user when an appointment becomes available. 

The `ConversationState` and `UserState` are state classes that are specializations of `BotState` class with policies that control the lifetime and scope of the objects stored in them. Components that need to store state create and register a property with a type, and use property accessor to access the state. The bot state manager can use memory storage, CosmosDB, and Blob Storage. 

> [!NOTE] 
> Use bot state manager to store preferences, user name, or the last thing they ordered, but do not use it to store critical business data. For critical data, create your own storage components or write directly to [storage](bot-builder-howto-v4-storage.md).
> In-memory data storage is intended for testing only. This storage is volatile and temporary. The data is cleared each time the bot is restarted.

## Using conversation state and user state to direct conversation flow
In designing a conversation flow, it is useful to define a state flag to direct the conversation flow. The flag can be a simple boolean type or a type that includes the name of the current topic. The flag can help you track where in a conversation you are. For example, a boolean type flag can tell you whether you are in a conversation or not, while a topic name property can tell you which conversation you are currently in.



# [C#](#tab/csharp)
### Conversation and User state
You can use [Echo Bot With Counter sample](https://aka.ms/EchoBot-With-Counter) as the start point for this how-to. First, create `TopicState` class to manage the current topic of conversation in `TopicState.cs` as shown below:

```csharp
public class TopicState
{
   public string Prompt { get; set; } = "askName";
}
``` 
Then create `UserProfile` class in `UserProfile.cs` to manage user state.
```csharp
public class UserProfile
{
    public string UserName { get; set; }
    public string TelephoneNumber { get; set; }
}
``` 
The `TopicState` class has a flag to keep track of where we are in the conversation and uses conversation state to store it. The Prompt is initialized as "askName" to initiate the conversation. Once the bot receives response from the user, Prompt will be redefined as "askNumber" to initiate the next conversation. `UserProfile` class tracks user name and phone number and stores it in user state.

### Property accessors
The `EchoBotAccessors` class in our example is created as a singleton and passed into the `class EchoWithCounterBot : IBot` constructor through dependency injection. The `EchoBotAccessors` class contains the `ConversationState`, `UserState`, and associated `IStatePropertyAccessor`. The `conversationState` object stores the topic state and `userState` object that stores the user profile information. The `ConversationState` and `UserState` objects will be created later in the Startup.cs file. The conversation and user state objects are where we persist anything at the conversation and user scope. 

Updated the constructor to include `UserState` as shown below:
```csharp
public EchoBotAccessors(ConversationState conversationState, UserState userState)
{
    ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
    UserState = userState ?? throw new ArgumentNullException(nameof(userState));
}
```
Create unique names for `TopicState` and `UserProfile` accessors.
```csharp
public static string UserProfileName { get; } = $"{nameof(EchoBotAccessors)}.UserProfile";
public static string TopicStateName { get; } = $"{nameof(EchoBotAccessors)}.TopicState";
```
Next, define two accessors. The first one stores the topic of the conversation, and the second stores user's name and phone number.
```csharp
public IStatePropertyAccessor<TopicState> TopicState { get; set; }
public IStatePropertyAccessor<UserProfile> UserProfile { get; set; }
```

Properties to get the ConversationState is already defined, but you'll need to add `UserState` as shown below:
```csharp
public ConversationState ConversationState { get; }
public UserState UserState { get; }
```
After making the changes, save the file. Next, we will update the Startup class to create `UserState` object to persist anything at the user-scope. The `ConversationState` already exists. 
```csharp

services.AddBot<EchoWithCounterBot>(options =>
{
    ...

    IStorage dataStore = new MemoryStorage();
    
    var conversationState = new ConversationState(dataStore);
    options.State.Add(conversationState);
        
    var userState = new UserState(dataStore);  
    options.State.Add(userState);
});
```
The line `options.State.Add(ConversationState);` and `options.State.Add(userState);` add the conversation state and user state, respectively. Next, create and register state accesssors. Accessors created here are passed into the IBot-derived class on every turn. Modify the code to inclue user state as shown below:
```csharp
services.AddSingleton<EchoBotAccessors>(sp =>
{
   ...
    var userState = options.State.OfType<UserState>().FirstOrDefault();
    if (userState == null)
    {
        throw new InvalidOperationException("UserState must be defined and added before adding user-scoped state accessors.");
    }
   ...
 });
```

Next, create the two accessors using `TopicState` and `UserProfile` and pass it into the `class EchoWithCounterBot : IBot` class through dependency injection.
```csharp
services.AddSingleton<EchoBotAccessors>(sp =>
{
   ...
    var accessors = new EchoBotAccessors(conversationState, userState)
    {
        TopicState = conversationState.CreateProperty<TopicState>(EchoBotAccessors.TopicStateName),
        UserProfile = userState.CreateProperty<UserProfile>(EchoBotAccessors.UserProfileName),
     };

     return accessors;
 });
```

The conversation and user state are linked to a singleton via the `services.AddSingleton` code block and saved via a state store accessor in the code starting with `var accessors = new EchoBotAccessor(conversationState, userState)`.

### Use conversation and user state properties 
In the `OnTurnAsync` handler of the `EchoWithCounterBot : IBot` class, modify the code to prompt for user name and then phone number. To track where we are in the conversation, we use the Prompt property defined in the TopicState. This property was initialized a "askName". Once we get the user name, we set it to "askNumber" and set the UserName to the name user typed in. After the phone number is received, you send a confirmation message and set the prompt to 'confirmation' because you are at the end of the conversation.

```csharp
if (turnContext.Activity.Type == ActivityTypes.Message)
{
    // Get the conversation state from the turn context.
    var convo = await _accessors.TopicState.GetAsync(turnContext, () => new TopicState());
    
    // Get the user state from the turn context.
    var user = await _accessors.UserProfile.GetAsync(turnContext, () => new UserProfile());
    
    // Ask user name. The Prompt was initialiazed as "askName" in the TopicState.cs file.
    if (convo.Prompt == "askName")
    {
        await turnContext.SendActivityAsync("What is your name?");
        
        // Set the Prompt to ask the next question for this conversation
        convo.Prompt = "askNumber";
        
        // Set the property using the accessor
        await _accessors.TopicState.SetAsync(turnContext, convo);
        
        //Save the new prompt into the conversation state.
        await _accessors.ConversationState.SaveChangesAsync(turnContext);
    }
    else if (convo.Prompt == "askNumber")
    {
        // Set the UserName that is defined in the UserProfile class
        user.UserName = turnContext.Activity.Text;
        
        // Use the user name to prompt the user for phone number
        await turnContext.SendActivityAsync($"Hello, {user.UserName}. What's your telephone number?");
        
        // Set the Prompt now that we have collected all the data
        convo.Prompt = "confirmation";
                 
        await _accessors.TopicState.SetAsync(turnContext, convo);
        await _accessors.ConversationState.SaveChangesAsync(turnContext);

        await _accessors.UserProfile.SetAsync(turnContext, user);
        await _accessors.UserState.SaveChangesAsync(turnContext);
    }
    else if (convo.Prompt == "confirmation")
    { 
        // Set the TelephoneNumber that is defined in the UserProfile class
        user.TelephoneNumber = turnContext.Activity.Text;

        await turnContext.SendActivityAsync($"Got it, {user.UserName}. I'll call you later.");

        // reset initial prompt state
        convo.Prompt = "askName"; // Reset for a new conversation.
        
        await _accessors.TopicState.SetAsync(turnContext, convo);
        await _accessors.ConversationState.SaveChangesAsync(turnContext);
    }
}
```   

# [JavaScript](#tab/js)

### Conversation and User state

You can use [Echo Bot With Counter sample](https://aka.ms/EchoBot-With-Counter-JS) as the starting point for this how-to. This sample already uses the `ConversationState` to store the message count. We will need to add a `TopicStates` object to track our conversation state and `UserState` to track user information in a `userProfile` object. 

In the main bot's `index.js` file, add the `UserState` to the require list:

**index.js**

```javascript
// Import required bot services. See https://aka.ms/bot-services to learn more about the different parts of a bot.
const { BotFrameworkAdapter, MemoryStorage, ConversationState, UserState } = require('botbuilder');
```

Next, create the `UserState` using `MemoryStorage` as the storage provider then pass it as the second argument to the `MainDialog` class.

**index.js**

```javascript
// Create conversation state with in-memory storage provider. 
const conversationState = new ConversationState(memoryStorage);
const userState = new UserState(memoryStorage);
// Create the main bot.
const bot = new EchBot(conversationState, userState);
```

In your `bot.js` file, update the constructor to accept the `userState` as the second argument. Then create a `topicState` property from the `conversationState` and create a `userProfile` property from the `userState`.

**bot.js**

```javascript
const TOPIC_STATE = 'topic';
const USER_PROFILE = 'user';

constructor (conversationState, userState) {
    // creates a new state accessor property.see https://aka.ms/about-bot-state-accessors to learn more about the bot state and state accessors 
    this.conversationState = conversationState;
    this.topicState = this.conversationState.createProperty(TOPIC_STATE);

    // User state
    this.userState = userState;
    this.userProfile = this.userState.createProperty(USER_PROFILE);
}
```

### Use conversation and user state properties

In the `onTurn` handler of the `MainDialog` class, modify the code to prompt for user name and then phone number. To track where we are in the conversation, we use the `prompt` property defined in the `topicState`. This property is initialized to "askName". Once we get the user name, we set it to "askNumber" and set the UserName to the name user typed in. After the phone number is received, you send a confirmation message and set the prompt to `undefined` because you are at the end of the conversation.

**dialogs/mainDialog/index.js**

```javascript
// see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
if (turnContext.activity.type === 'message') {
    // read from state and set default object if object does not exist in storage.
    let topicState = await this.topicState.get(turnContext, {
        //Define the topic state object
        prompt: "askName"
    });
    let userProfile = await this.userProfile.get(turnContext, {  
        // Define the user's profile object
        "userName": "",
        "telephoneNumber": ""
    });

    if(topicState.prompt == "askName"){
        await turnContext.sendActivity("What is your name?");

        // Set next prompt state
        topicState.prompt = "askNumber";

        // Update state
        await this.topicState.set(turnContext, topicState);
    }
    else if(topicState.prompt == "askNumber"){
        // Set the UserName that is defined in the UserProfile class
        userProfile.userName = turnContext.activity.text;

        // Use the user name to prompt the user for phone number
        await turnContext.sendActivity(`Hello, ${userProfile.userName}. What's your telephone number?`);

        // Set next prompt state
        topicState.prompt = "confirmation";

        // Update states
        await this.topicState.set(turnContext, topicState);
        await this.userProfile.set(turnContext, userProfile);
    }
    else if(topicState.prompt == "confirmation"){
        // Set the phone number
        userProfile.telephoneNumber = turnContext.activity.text;

        // Sent confirmation
        await turnContext.sendActivity(`Got it, ${userProfile.userName}. I'll call you later.`)

        // reset initial prompt state
        topicState.prompt = "askName"; // Reset for a new conversation

        // Update states
        await this.topicState.set(turnContext, topicState);
        await this.userProfile.set(turnContext, userProfile);
    }
    
    // Save state changes to storage
    await this.conversationState.saveChanges(turnContext);
    await this.userState.saveChanges(turnContext);
    
}
else {
    await turnContext.sendActivity(`[${turnContext.activity.type} event detected]`);
}
```

---

## Start your bot
Run your bot locally.

### Start the emulator and connect your bot
Next, start the emulator and then connect to your bot in the emulator:

1. Click the **Open Bot** link in the emulator "Welcome" tab. 
2. Select the .bot file located in the directory where you created the Visual Studio solution.

### Interact with your bot

Send a "Hi" message to your bot, and the bot will ask for your name and phone number. After you provide that informmation, the bot will send a confirmation messsage. If you continue after that, the bot will go through the same cycle again.
![Emulator running](../media/emulator-v4/emulator-running.png)

If you decide to manage state yourself, see [manage conversation flow with your own prompts](bot-builder-primitive-prompts.md). An alternative is to use the waterfall dialog. The dialog keeps track of the conversation state for you so you do not need to create flags to track your state. For more information, see [manage a simple conversation with dialogs](bot-builder-dialog-manage-conversation-flow.md).

## Next steps
Now that you know how to use state to help you read and write bot data to storage, lets take a look at how you can read and write directly to storage.

> [!div class="nextstepaction"]
> [How to write directly to storage](bot-builder-howto-v4-storage.md).
