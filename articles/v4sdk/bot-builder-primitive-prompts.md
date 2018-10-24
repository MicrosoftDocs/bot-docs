---
title: Manage a conversation flow with primitive prompts | Microsoft Docs
description: Learn how to manage a conversation flow with primitive prompts in the Bot Builder SDK.
keywords: conversation flow, prompts
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 7/20/2018
monikerRange: 'azure-bot-service-4.0'
---

# Prompt users for input using your own prompts

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

**app.js**

```javascript
const storage = new MemoryStorage(); // Volatile memory
const conversationState = new ConversationState(storage);
const userState = new UserState(storage);
const dialogState = conversationState.createProperty('dialogState');
const userProfile = userState.createProperty('userProfile');
```

Place this code within your main bot logic.

```javascript
// Pull the state of the dialog out of the conversation state manager.
const convo = await dialogState.get(context, {
    prompt: undefined,
    topic: 'profile'
});

// Pull the user profile out of the user state manager.
const userProfile = await userProfile.get(context, {  // Define the user's profile object
        "userName": undefined,
        "age": undefined,
        "workPlace": undefined
    }
);
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

**app.js**

```javascript

server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (context) => {
        const isMessage = (context.activity.type === ActivityTypes.Message);

        // Set up a list of fields that we need to collect from the user.
        const fields = {
            userName: "What is your name?",
            age: "How old are you?",
            workPlace: "Where do you work?"
        }

        // Pull the state of the dialog out of the conversation state manager.
        const convo = await dialogState.get(context, {
            topic: 'profile',
            prompt: undefined
        });

        // Pull the user profile out of the user state manager.
        const userProfile = await userProfile.get(context, {  // Define the user's profile object
                "userName": undefined,
                "age": undefined,
                "workPlace": undefined
            }
        );

        if (isMessage) {

            if (convo.topic === 'profile') {
                // If a prompt flag is set in the conversation state, use it to capture the incoming value
                // into the appropriate field of the user profile.
                if (convo.prompt) {
                    userProfile[convo.prompt] = context.activity.text;
                }

                 // Determine which fields are not yet set.
                 const empty_fields = [];
                 Object.keys(fields).forEach(function(key) {
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
                        await context.sendActivity('Welcome new user, please fill out your profile information.');
                    }

                    // We have at least one empty field. Prompt for the next empty field.
                    await context.sendActivity(empty_fields[0].prompt);

                    // update the flag to indicate which prompt we just sent
                    // so that the response can be captured at the beginning of the next turn.
                    convo.prompt = empty_fields[0].key;

                 } else {
                    // Our user profile is complete!
                    await context.sendActivity('Thank you. Your profile is complete.');
                    convo.prompt = null;
                    convo.topic = null;

                 }
            } else if (context.activity.text && context.activity.text.match(/hi/ig)) {
                // Check to see if the user said "hi" and respond with a greeting
                await context.sendActivity(`Hi ${ userProfile.userName }.`);
            } else {
                // Default message
                await context.sendActivity("Hi. I'm the Contoso bot.");
            }
        }

        // End the turn by writing state changes back to storage
        await conversationState.saveChanges(context);
        await userState.saveChanges(context);
    });
});

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

Parsing a number or a date and time is a complex task that's beyond the scope of this topic. Fortunately, there is a library we can leverage. To parse this information, we use the [Microsoft's Text Recognizer](https://github.com/Microsoft/Recognizers-Text) library. This package is available through NuGet, or downloading it from the repository. (It's included in the **Dialogs** library too, which is worth noting even though we are not using it here.)

This library is particularly useful for parsing complex input like dates, times, or phone numbers. In this sample, we're  validating a number for a dinner reservation party size, but the same idea can be extended for more complex validation operations.

In the following sample we only show the use of `RecognizeNumber`. Details on how to use other Recognizer methods from the library can be found in that [repository's documentation](https://github.com/Microsoft/Recognizers-Text/blob/master/README.md).

# [C#](#tab/csharp)

To use the **Microsoft.Recognizers.Text.Number** library, include the NuGet package and add it to your using statements.

```csharp
using Microsoft.Recognizers.Text.Number;
using Microsoft.Recognizers.Text;
using System.Linq;
```

Then, define a function that actually does the validation.

```csharp
private async Task<bool> ValidatePartySize(ITurnContext context, string value)
{
    try
    {
        // Recognize the input as a number. This works for responses such as
        // "twelve" as well as "12"
        var result = NumberRecognizer.RecognizeNumber(value, Culture.English);

        // Attempt to convert the Recognizer result to an integer
        int.TryParse(result.First().Text, out int partySize);

        if (partySize < 6)
        {
            throw new Exception("Party size too small.");
        }
        else if (partySize > 20)
        {
            throw new Exception("Party size too big.");
        }

        // If we got through this, the number is valid
        return true;
    }
    catch (Exception)
    {
        await context.SendActivityAsync("Error with your party size. < br /> Please specify a number between 6 - 20.");
        return false;
    }
}
```

# [JavaScript](#tab/javascript)

To use the **Recognizers** library, require it in **app.js**:

```javascript
// Required packages for this bot
var Recognizers = require('@microsoft/recognizers-text-suite');
```

Then, define a function that actually does the validation.

```javascript
// Support party size between 6 and 20 only
async function validatePartySize(context, input){
    try {
        // Recognize the input as a number. This works for responses such as
        // "twelve" as well as "12"
        var result = Recognizers.recognizeNumber(input, Recognizers.Culture.English);
        var value = parseInt(results[0].resolution.value);

        if (value < 6) {
            throw new Error(`Party size too small.`);
        } else if(value > 20){
            throw new Error(`Party size too big.`);
        }
        return true; // Return the valid value
    } catch (err){
        await context.sendActivity(`${err.message} <br/>Please specify a number between 6 - 20.`);
        return false;
    }
}
```

---

While processing the user's response to the prompt, call the validation function before moving on to the next prompt. If the validation fails, ask the question again.

# [C#](#tab/csharp)

```csharp
if (topicState.Prompt == "partySize")
{
    if (await ValidatePartySize(turnContext, turnContext.Activity.Text))
    {
        // Save user's response in our state, ReservationInfo, which
        // is a new class we've added to our state
        // UserFieldInfo partySize;
        partySize.SetValue(userProfile, turnContext.Activity.Text);

        // Ask next question.
        topicState.Prompt = "reserveName";
        await turnContext.SendActivityAsync("Who's name will this be under?");
    }
    else
    {
        // Ask again.
        await turnContext.SendActivityAsync("How many people are in your party?");
    }
}
```

# [JavaScript](#tab/javascript)

**app.js**

```javascript
// ...
if (convo.prompt == "partySize") {
    if (await validatePartySize(context, context.activity.text)) {
        // Save user's response
        reservationInfo.partySize = context.activity.text;

        // Ask next question
        convo.prompt = "reserveName";
        await context.sendActivity("Who's name will this be under?");
    } else {
        // Ask again
        await context.sendActivity("How many people are in your party?");
    }
}
// ...
```

---

## Next step

Now that you have a handle on how to manage the prompt states yourself, let's take a look at how you leverage the **Dialogs** library to prompt users for input.

> [!div class="nextstepaction"]
> [Prompt users for input using Dialogs](bot-builder-prompts.md)
