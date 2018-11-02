---
title: Create your own prompts to gather user input | Microsoft Docs
description: Learn how to manage a conversation flow with primitive prompts in the Bot Builder SDK.
keywords: conversation flow, prompts
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 10/31/2018
monikerRange: 'azure-bot-service-4.0'
---

# Create your own prompts to gather user input

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

A conversation between a bot and a user often involves asking (prompting) the user for information, parsing the user's response, and then acting on that information. The topic about [prompting users using the Dialogs library](bot-builder-prompts.md) details how to prompt users for input using the **Dialogs** library. Among other things, the **Dialogs** library takes care of tracking the current conversation and the current question being asked. It also provides methods for requesting and validating different types of information such as text, number, date and time, and so on.

In certain situations, the built in **Dialogs** may not be the right solution for your bot; **Dialogs** might add too much overhead for simple bots, be too rigid, or otherwise not achieve what you need your bot to do. In those cases, you can skip the library and implement your own prompting logic. This topic will show you how to setup your basic *Echo bot* so that you can manage conversation using your own prompts.

## Track prompt states

In a prompt-driven conversation, you need to track where in the conversation you currently are, and what question is currently being asked. In code, this translates to managing a couple of flags.

For example, you can create a couple of properties you want to track.

These states keep track of which topic and which prompt we are currently on. To ensure these flags function as expected when deployed to the cloud, we store them in the [conversation state](bot-builder-howto-v4-state.md). 

# [C#](#tab/csharp)

We are creating two classes to track state. **TopicState** to track the progress of the conversational prompts, and **UserProfile** to track the information the user provides. We'll be storing this information in our bot [state](bot-builder-howto-v4-state.md) in a later step.

```csharp
/// <summary>
/// Contains conversation state information about the conversation in progress.
/// </summary>
public class TopicState
{
    /// <summary>
    /// Identifies the current "topic" of conversation.
    /// </summary>
    public string Topic { get; set; }

    /// <summary>
    /// Indicates whether we asked the user a question last turn, and
    /// if so, what it was.
    /// </summary>
    public string Prompt { get; set; }
}
```

```csharp
/// <summary>
/// Contains user state information for the user's profile.
/// </summary>
public class UserProfile
{
    public string UserName { get; set; }

    public int? Age { get; set; }

    public string WorkPlace { get; set; }
}
```

# [JavaScript](#tab/javascript)

In **index.js**, update the require statement to include `UserState`.

```javascript
const { BotFrameworkAdapter, MemoryStorage, ConversationState, UserState } = require('botbuilder');
```

Then create a user state management object and pass it in when creating your bot.

```javascript
// Create conversation and user state with in-memory storage provider.
const conversationState = new ConversationState(memoryStorage);
const userState = new UserState(memoryStorage);

// Create the bot.
const myBot = new MyBot(conversationState, userState);
```

In **bot.js**, define identifiers for the state property accessors we will use to manage the bot's [state](bot-builder-howto-v4-state.md). Also define the prompts to use for the information we want to collect from the user.

Add this code outside of the `MyBot` class.

```javascript
// Define identifiers for our state property accessors.
const TOPIC_STATE_PROPERTY = 'topicStateProperty';
const USER_PROFILE_PROPERTY = 'userProfileProperty';

// Define the prompts to use to ask for user profile information.
const fields = {
    userName: "What is your name?",
    age: "How old are you?",
    workPlace: "Where do you work?"
}
```

---

## Manage a topic of conversation

In a sequential conversation, such as one where you gather information from the user, you need to know when the user has entered the conversation and where in the conversation the user is. You can track this in the main bot's turn handler by setting and checking the prompt flags defined above, then acting accordingly. The sample below shows how you can use these flags to gather the user's profile information through the conversation.

The bot code is presented here, and discussed in the next section.

# [C#](#tab/csharp)

For ASP.NET Core, we need to setup our bot and dependency injection first.

Rename the file **EchoWithCounterBot.cs** to **PrimitivePromptsBot.cs**, and also update the class name. This class holds our bot logic, and we'll get back to updating this shortly.

Rename the file **EchoBotAccessors.cs** to **BotAccessors.cs**, and also update the class name. This class holds the state management objects and the state property accessors for our bot. Update the definition to the following.

```csharp
using Microsoft.Bot.Builder;
using System;

/// <summary>
/// Contains the state and state property accessors for the primitive prompts bot.
/// </summary>
public class BotAccessors
{
    public const string TopicStateName = "PrimitivePrompts.TopicStateAccessor";

    public const string UserProfileName = "PrimitivePrompts.UserProfileAccessor";

    public ConversationState ConversationState { get; }

    public UserState UserState { get; }

    public IStatePropertyAccessor<TopicState> TopicStateAccessor { get; set; }

    public IStatePropertyAccessor<UserProfile> UserProfileAccessor { get; set; }

    public BotAccessors(ConversationState conversationState, UserState userState)
    {
        if (conversationState is null)
        {
            throw new ArgumentNullException(nameof(conversationState));
        }

        if (userState is null)
        {
            throw new ArgumentNullException(nameof(userState));
        }

        this.ConversationState = conversationState;
        this.UserState = userState;
    }
}
```

Update the **Startup.cs** file's `ConfigureServices` method, starting from where you set up the `IStorage` object.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddBot<PrimitivePromptsBot>(options =>
    {
        // ...

        // The Memory Storage used here is for local bot debugging only. When the bot
        // is restarted, everything stored in memory will be gone.
        IStorage dataStore = new MemoryStorage();

        var conversationState = new ConversationState(dataStore);
        options.State.Add(conversationState);

        var userState = new UserState(dataStore);
        options.State.Add(userState);
    });

    // Create and register state accessors.
    // Accessors created here are passed into the IBot-derived class on every turn.
    services.AddSingleton<BotAccessors>(sp =>
    {
        var options = sp.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;
        var conversationState = options.State.OfType<ConversationState>().FirstOrDefault();
        var userState = options.State.OfType<UserState>().FirstOrDefault();

        // Create the custom state accessor.
        // State accessors enable other components to read and write individual properties of state.
        var accessors = new BotAccessors(conversationState, userState)
        {
            TopicStateAccessor = conversationState.CreateProperty<TopicState>(BotAccessors.TopicStateName),
            UserProfileAccessor = userState.CreateProperty<UserProfile>(BotAccessors.UserProfileName),
        };

        return accessors;
    });
}
```

Finally, update the bot logic in your **PrimitivePromptsBot.cs** file.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

public class PrimitivePromptsBot : IBot
{
    public const string ProfileTopic = "profile";

    /// <summary>
    /// Describes a field in the user profile.
    /// </summary>
    private class UserFieldInfo
    {
        /// <summary>
        /// The ID to use for this field.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The prompt to use to ask for a value for this field.
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// Gets the value of the corresponding field.
        /// </summary>
        public Func<UserProfile, string> GetValue { get; set; }

        /// <summary>
        /// Sets the value of the corresponding field.
        /// </summary>
        public Action<UserProfile, string> SetValue { get; set; }
    }

    /// <summary>
    /// The prompts for the user profile, indexed by field name.
    /// </summary>
    private static List<UserFieldInfo> UserFields { get; } = new List<UserFieldInfo>
    {
        new UserFieldInfo {
            Key = nameof(UserProfile.UserName),
            Prompt = "What is your name?",
            GetValue = (profile) => profile.UserName,
            SetValue = (profile, value) => profile.UserName = value,
        },
        new UserFieldInfo {
            Key = nameof(UserProfile.Age),
            Prompt = "How old are you?",
            GetValue = (profile) => profile.Age.HasValue? profile.Age.Value.ToString() : null,
            SetValue = (profile, value) =>
            {
                if (int.TryParse(value, out int age))
                {
                    profile.Age = age;
                }
            },
        },
        new UserFieldInfo {
            Key = nameof(UserProfile.WorkPlace),
            Prompt = "Where do you work?",
            GetValue = (profile) => profile.WorkPlace,
            SetValue = (profile, value) => profile.WorkPlace = value,
        },
    };

    /// <summary>
    /// The state and state accessors for the bot.
    /// </summary>
    private BotAccessors Accessors { get; }

    public PrimitivePromptsBot(BotAccessors accessors)
    {
        Accessors = accessors ?? throw new ArgumentNullException(nameof(accessors));
    }

    public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
    {
        if (turnContext.Activity.Type is ActivityTypes.Message)
        {
            // Use the state property accessors to get the topic state and user profile.
            TopicState topicState = await Accessors.TopicStateAccessor.GetAsync(
                turnContext,
                () => new TopicState { Topic = ProfileTopic, Prompt = null },
                cancellationToken);
            UserProfile userProfile = await Accessors.UserProfileAccessor.GetAsync(
                turnContext,
                () => new UserProfile(),
                cancellationToken);

            // Check whether we need more information.
            if (topicState.Topic is ProfileTopic)
            {
                // If we're expecting input, record it in the user's profile.
                if (topicState.Prompt != null)
                {
                    UserFieldInfo field = UserFields.First(f => f.Key.Equals(topicState.Prompt));
                    field.SetValue(userProfile, turnContext.Activity.Text.Trim());
                }

                // Determine which fields are not yet set.
                List<UserFieldInfo> emptyFields = UserFields.Where(f => f.GetValue(userProfile) is null).ToList();

                if (emptyFields.Any())
                {
                    // If all the fields are empty, send a welcome message.
                    if (emptyFields.Count == UserFields.Count)
                    {
                        await turnContext.SendActivityAsync("Welcome new user, please fill out your profile information.");
                    }

                    // We have at least one empty field. Prompt for the next empty field,
                    // and update the prompt flag to indicate which prompt we just sent,
                    // so that the response can be captured at the beginning of the next turn.
                    UserFieldInfo field = emptyFields.First();
                    await turnContext.SendActivityAsync(field.Prompt);
                    topicState.Prompt = field.Key;
                }
                else
                {
                    // Our user profile is complete!
                    await turnContext.SendActivityAsync($"Thank you, {userProfile.UserName}. Your profile is complete.");
                    topicState.Prompt = null;
                    topicState.Topic = null;
                }
            }
            else if (turnContext.Activity.Text.Trim().Equals("hi", StringComparison.InvariantCultureIgnoreCase))
            {
                await turnContext.SendActivityAsync($"Hi. {userProfile.UserName}.");
            }
            else
            {
                await turnContext.SendActivityAsync("Hi. I'm the Contoso cafe bot.");
            }

            // Use the state property accessors to update the topic state and user profile.
            await Accessors.TopicStateAccessor.SetAsync(turnContext, topicState, cancellationToken);
            await Accessors.UserProfileAccessor.SetAsync(turnContext, userProfile, cancellationToken);

            // Save any state changes to storage.
            await Accessors.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await Accessors.UserState.SaveChangesAsync(turnContext, false, cancellationToken);
        }
    }
}
```

# [JavaScript](#tab/javascript)

In **bot.js**, update the `MyBot` class definition.

We setup the state property accessors in the bot's constructor: `topicStateAccessor` and `userProfileAccessor`. The topic state tracks the topic of conversation, and the user profile tracks the information we've collected for the user.

```javascript
constructor(conversationState, userState) {
    // Create state property accessors.
    this.topicStateAccessor = conversationState.createProperty(TOPIC_STATE_PROPERTY);
    this.userProfileAccessor = userState.createProperty(USER_PROFILE_PROPERTY);

    // Track the conversation and user state management objects.
    this.conversationState = conversationState;
    this.userState = userState;
}
```

Then update the turn handler to use the bot state to control the conversation flow and save the collected user information.

```javascript
async onTurn(turnContext) {
    // Handle only message activities from the user.
    if (turnContext.activity.type === ActivityTypes.Message) {
        // Get state properties using their accessors, providing default values.
        const topicState = await this.topicStateAccessor.get(turnContext, {
            prompt: undefined,
            topic: 'profile'
        });
        const userProfile = await this.userProfileAccessor.get(turnContext, {
            "userName": undefined,
            "age": undefined,
            "workPlace": undefined
        });

        if (topicState.topic === 'profile') {
            // If a prompt flag is set in the conversation state, use it to capture the incoming value
            // into the appropriate field of the user profile.
            if (topicState.prompt) {
                userProfile[topicState.prompt] = turnContext.activity.text;
            }

            // Determine which fields are not yet set.
            const empty_fields = [];
            Object.keys(fields).forEach(function (key) {
                if (!userProfile[key]) {
                    empty_fields.push({
                        key: key,
                        prompt: fields[key]
                    });
                }
            });

            if (empty_fields.length) {

                // If all the fields are empty, send a welcome message.
                if (empty_fields.length == Object.keys(fields).length) {
                    await turnContext.sendActivity('Welcome new user, please fill out your profile information.');
                }

                // We have at least one empty field. Prompt for the next empty field.
                await turnContext.sendActivity(empty_fields[0].prompt);

                // update the flag to indicate which prompt we just sent
                // so that the response can be captured at the beginning of the next turn.
                topicState.prompt = empty_fields[0].key;

            } else {
                // Our user profile is complete!
                await turnContext.sendActivity('Thank you. Your profile is complete.');
                topicState.prompt = null;
                topicState.topic = null;

            }
        } else if (turnContext.activity.text && turnContext.activity.text.match(/hi/ig)) {
            // Check to see if the user said "hi" and respond with a greeting
            await turnContext.sendActivity(`Hi ${userProfile.userName}.`);
        } else {
            // Default message
            await turnContext.sendActivity("Hi. I'm the Contoso bot.");
        }

        // Save state changes
        await this.conversationState.saveChanges(turnContext);
        await this.userState.saveChanges(turnContext);
    }
}
```

---

The sample code above initializes the _topic_ flag to `profile` in order to start the profile collection conversation. The bot moves forward through the conversation by updating the _prompt_ flag to a value representing the current question being asked. With this flag set to the proper value, the bot will know what to do with the next message received from the user and process it accordingly.

Lastly, the flags are reset when the bot is done asking for information. Otherwise, your bot will be trapped in a loop, never moving on from the final question.

You can extend this pattern to manage more complex conversation flows by defining other flags, or branching the conversation based on user input.

## Manage multiple topics of conversations

Once you are able to handle one topic of conversation, you can extend the bot's logic to handle multiple topics of conversation. Multiple topics can be handled by checking for additional conditions, then taking the appropriate path.

You can extend the example above to allow for other features and conversation topics, such as reserving a table or placing an order. Add additional flags to the topic state as needed to keep track of the conversation.

You may also find it helpful to split code into independent functions or methods, making it easier to follow the flow of conversation. A common pattern is to have the bot evaluate the message and state, and then delegate control to functions which implement details of the feature.

To help your users better navigate multiple topics of conversation, consider providing a main menu. For example, using [suggested actions](bot-builder-howto-add-suggested-actions.md), you can give your user a choice of which topic they could to engage in, rather than guessing at what your bot can do.

## Validate user input

The **Dialogs** library provides built in ways to validate the user's input, but we can also do so with our own prompts. For example, if we ask for the user's age, we want to make sure we get a number, not something like "Bob" as a response.

Parsing a number or a date and time is a complex task that's beyond the scope of this topic. Fortunately, there is a library we can leverage. To parse this information, we use the [Microsoft's Text Recognizer](https://github.com/Microsoft/Recognizers-Text) library. This package is available through NuGet and npm. You can also downloading it directly from the repository. (It's included in the **Dialogs** library too, which is worth noting even though we are not using it here.)

This library is particularly useful for parsing complex input like dates, times, or phone numbers. In this sample, we're  validating a number for a dinner reservation party size, but the same idea can be extended for more complex validation operations.

In the following sample we only show the use of `RecognizeNumber`. Details on how to use other Recognizer methods from the library can be found in that [repository's documentation](https://github.com/Microsoft/Recognizers-Text/blob/master/README.md).

# [C#](#tab/csharp)

To use the **Microsoft.Recognizers.Text.Number** library, include the NuGet package, and add these using statements to your bot file.

```csharp
using System.Linq;
using Microsoft.Recognizers.Text;
using Microsoft.Recognizers.Text.Number;
```

There are many ways we could handle validation. Here, we'll update our helper class to include validation.

Add the following members to the bot's inner `UserFieldInfo` class.

```csharp
/// <summary>Delegate for validating input.</summary>
/// <param name="turnContext">The current turn context. turnContext.Activity.Text contains the input to validate.</param>
/// <returns><code>true</code> if the input is valid; otherwise, <code>false</code>.</returns>
public delegate Task<bool> ValidatorDelegate(
    ITurnContext turnContext,
    CancellationToken cancellationToken = default(CancellationToken));

/// <summary>By default, evaluate all input as valid.</summary>
private static readonly ValidatorDelegate NoValidator =
    async (ITurnContext turnContext, CancellationToken cancellationToken) => true;

/// <summary>Gets or sets the validation function to use.</summary>
public ValidatorDelegate ValidateInput { get; set; } = NoValidator;
```

Then, update the _age_ entry in the bot's `UserFields` to define the validation to use.
Since we will validate the input before setting the value for age, we can simplify the `SetValue` function a little and take advantage of the text recognizers library.

```csharp
private static List<UserFieldInfo> UserFields { get; } = new List<UserFieldInfo>
{
    // ...
    new UserFieldInfo {
        Key = nameof(UserProfile.Age),
        Prompt = "How old are you?",
        GetValue = (profile) => profile.Age.HasValue? profile.Age.Value.ToString() : null,
        SetValue = (profile, value) =>
        {
            // As long as the input validates, this should work.
            List<ModelResult> result = NumberRecognizer.RecognizeNumber(value, Culture.English);
            profile.Age = int.Parse(result.First().Text);
        },
        ValidateInput = async (turnContext, cancellationToken) =>
        {
            try
            {
                // Recognize the input as a number. This works for responses such as
                // "twelve" as well as "12".
                List<ModelResult> result = NumberRecognizer.RecognizeNumber(
                    turnContext.Activity.Text, Culture.English);

                // Attempt to convert the Recognizer result to an integer
                int.TryParse(result.First().Text, out int age);

                if (age < 18)
                {
                    await turnContext.SendActivityAsync(
                        "You must be 18 or older.",
                        cancellationToken: cancellationToken);
                    return false;
                }
                else if (age > 120)
                {
                    await turnContext.SendActivityAsync(
                        "You must be 120 or younger.",
                        cancellationToken: cancellationToken);
                    return false;
                }
            }
            catch
            {
                await turnContext.SendActivityAsync(
                    "I couldn't understand your input. Please enter your age in years.",
                    cancellationToken: cancellationToken);
                return false;
            }

            // If we got through this, the number is valid.
            return true;
        },
    },
    // ...
};
```

Finally, we update our turn handler to validate all input before saving a value to the property.
Validation defaults to our NoValidator function, which accepts any input. So, the behavior for the age prompt is the only one that will change. If the input fails to validate, we don't set the field, and the bot will prompt for input for this field again next turn.

Here, we're just looking at the part of the turn handler we need to update.

```csharp
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
{
    if (turnContext.Activity.Type is ActivityTypes.Message)
    {
        // ...
        // Check whether we need more information.
        if (topicState.Topic is ProfileTopic)
        {
            // If we're expecting input, record it in the user's profile.
            if (topicState.Prompt != null)
            {
                UserFieldInfo field = UserFields.First(f => f.Key.Equals(topicState.Prompt));
                if (await field.ValidateInput(turnContext, cancellationToken))
                {
                    field.SetValue(userProfile, turnContext.Activity.Text.Trim());
                }
            }

            // ...
        }
        //...
    }
}
```

# [JavaScript](#tab/javascript)

To use the **Recognizers** library, add the package and require it in your bot code (in **bot.js**):

```bash
npm i @microsoft/recognizers-text-suite --save
```

```javascript
// Required packages for this bot.
const Recognizers = require('@microsoft/recognizers-text-suite');
```

Then, update the `fields` metadata to include text recognition and validation code:

```javascript
// Define the prompts to use to ask for user profile information.
const fields = {
    userName: { prompt: "What is your name?" },
    age: {
        prompt: "How old are you?",
        recognize: (turnContext) => {
            var result = Recognizers.recognizeNumber(
                turnContext.activity.text, Recognizers.Culture.English);
            return parseInt(result[0].resolution.value);
        },
        validate: async (turnContext) => {
            try {
                // Recognize the input as a number. This works for responses such as
                // "twelve" as well as "12".
                var result = Recognizers.recognizeNumber(
                    turnContext.activity.text, Recognizers.Culture.English);
                var age = parseInt(result[0].resolution.value);
                if (age < 18) {
                    await turnContext.sendActivity("You must be 18 or older.");
                    return false;
                }
                if (age > 120 ) {
                    await turnContext.sendActivity("You must be 120 or younger.");
                    return false;
                }
            } catch (_) {
                await turnContext.sendActivity(
                    "I couldn't understand your input. Please enter your age in years.");
                return false;
            }
            return true;
        }
    },
    workPlace: { prompt: "Where do you work?" }
}
```

In our bot's turn handler, update the following blocks, where we record the user's input, and where we prompt the user. We need to update these sections to account for the changes to the field metadata.

```javascript
async onTurn(turnContext) {
    // Handle only message activities from the user.
    if (turnContext.activity.type === ActivityTypes.Message) {
        // ...

        if (topicState.topic === 'profile') {
            // If a prompt flag is set in the conversation state, use it to capture the incoming value
            // into the appropriate field of the user profile.
            if (topicState.prompt) {
                const field = fields[topicState.prompt];
                // If the prompt has validation, check whether the input validates.
                if (!field.validate || await field.validate(turnContext)) {
                    // Set the field, using a recognizer if one is defined.
                    userProfile[topicState.prompt] = (field.recognize)
                        ? field.recognize(turnContext)
                        : turnContext.activity.text;
                }
            }

            // ...

            if (empty_fields.length) {

                // ...

                // We have at least one empty field. Prompt for the next empty field.
                await turnContext.sendActivity(empty_fields[0].prompt.prompt);

                // ...

            } // ...
        } // ...

        // Save state changes
        await this.conversationState.saveChanges(turnContext);
        await this.userState.saveChanges(turnContext);
    }
}
```

---

## Next step

Now that you have a handle on how to manage the prompt states yourself, let's take a look at how you leverage the **Dialogs** library to prompt users for input.

> [!div class="nextstepaction"]
> [Prompt users for input using Dialogs](bot-builder-prompts.md)
