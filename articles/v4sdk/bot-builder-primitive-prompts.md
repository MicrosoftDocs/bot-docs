---
title: Manage a conversation flow with primitive prompts | Microsoft Docs
description: Learn how to manage a conversation flow with primitive prompts in the Bot Builder SDK.
keywords: conversation flow, prompts
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 7/20/2018
monikerRange: 'azure-bot-service-4.0'
---

# Prompt users for input using your own prompts
[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

A conversation between a bot and a user often involves asking (prompting) the user for information, parsing the user's response, and then acting on that information. In the topic about [prompting users using the Dialogs library](bot-builder-prompts.md), it details how to prompt users for input using the **Dialogs** library. Among other things, the **Dialogs** library takes care of tracking the current conversation and the current question being asked. It also provides methods for requesting different types of information such as text, number, date and time, and so on. 

In certain situations, the built in **Dialogs** may not be the right solution for your bot; **Dialogs** might add too much overhead for simple bots, be too rigid, or otherwise not achieve what you need your bot to do. In those cases, you can skip the library and implement your own prompting logic. This topic will show you how to setup your basic *Echo bot* so that you can manage conversation using your own prompts.

## Track prompt states

In a prompt-driven conversation, you need to track where in the conversation you currently are, and what question is currently being asked. In code, this translates to managing a couple of flags. 

For example, you can create a couple of properties you want to track. 

# [C#](#tab/csharp)

Here we have a user profile class nested in our prompt information, allowing all of these to be stored in our bot [state](bot-builder-howto-v4-state.md) together.

```csharp
// Where user information will be stored
public class UserProfile
{
    public string userName = null;
    public string workPlace = null;
    public int age = 0;
}

// Flags to keep track of our prompts, and the 
// user profile object for this conversation
public class PromptInformation
{
    public string topicTitle = null;
    public string prompt = null;
    public UserProfile userProfile = null;
}
```

# [JavaScript](#tab/javascript)

These states just keep track of which topic and which prompt we are currently on. To ensure these flags function as expected when deployed to the cloud, we store them in the [conversation state](bot-builder-howto-v4-state.md). Place this code within your main bot logic.

**app.js**
```javascript
// Define a topicStates object if it doesn't exist in the convoState.
if(!convo.topicStates){
    convo.topicStates = {
        "topicTitle": undefined, // Current conversation topic in progress
        "prompt": undefined      // Current prompt state in progress - indicate what question is being asked.
    };
}
```

---

## Manage a topic of conversation

In a sequential conversation, such as one where you gather information from the user, you need to know when the user has entered the conversation and where in the conversation the user is. You can track this in the main bot turn handler by setting and checking the prompt flags defined above, then acting accordingly. The sample below shows how you can use these flags to gather the user's profile information through the conversation.

# [C#](#tab/csharp)

```csharp
// Get our current state, as defined above
var convoState = context.GetConversationState<PromptInformation>();

if (convoState.userProfile == null)
{
    await context.SendActivity("Welcome new user, please fill out your profile information.");
    convoState.topicTitle = "profileTopic"; // Start the userProfile topic
    convoState.userProfile = new UserProfile();
}

// Start or continue a conversation if we are in one
if (convoState.topicTitle == "profileTopic")
{
    if (convoState.userProfile.userName == null && convoState.prompt == null)
    {
        convoState.prompt = "askName";
        await context.SendActivity("What is your name?");
    }
    else if (convoState.prompt == "askName")
    {
        // Save the user's response
        convoState.userProfile.userName = context.Activity.Text;

        // Ask next question
        convoState.prompt = "askAge";
        await context.SendActivity("How old are you?");
    }
    else if (convoState.prompt == "askAge")
    {
        // Save user's response
        if (!Int32.TryParse(context.Activity.Text, out convoState.userProfile.age))
        {
            // Failed to convert to int
            await context.SendActivity("Failed to convert string to int");
        }
        else
        {
            // Ask next question
            convoState.prompt = "workPlace";
            await context.SendActivity("Where do you work?");
        }

    }
    else if (convoState.prompt == "workPlace")
    {
        // Save user's response
        convoState.userProfile.workPlace = context.Activity.Text;

        // Done
        convoState.topicTitle = null; // Reset conversation flag
        convoState.prompt = null;     // Reset the prompt flag
        
        await context.SendActivity("Thank you. Your profile is complete.");
    }
    else // Somehow our flags got inappropriately set
    {
        await context.SendActivity("Looks like something went wrong, let's start over");
        convoState.userProfile = null;
        convoState.prompt = null;
        convoState.topicTitle = null;
    }
}
```

# [JavaScript](#tab/javascript)

**app.js**
```javascript
server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (context) => {
        const isMessage = (context.activity.type === 'message');
        // State will store all of your information 
        const convo = conversationState.get(context);

        // Defined flags to manage the conversation flow and prompt states
        // convo.topicStates.topicTitle - Current conversation topic in progress
        // convo.topicStates.prompt - Current prompt state in progress - indicate what question is being asked.
        
        if (isMessage) {
            // Define a topicStates object if it doesn't exist in the convoState.
            if(!convo.topicStates){
                convo.topicStates = {
                    "topicTitle": undefined, // Current conversation topic in progress
                    "prompt": undefined      // Current prompt state in progress - indicate what question is being asked.
                };
            }
            
            // If user profile is not defined then define it.
            if(!convo.userProfile){
                
                await context.sendActivity(`Welcome new user, please fill out your profile information.`);
                convo.topicStates.topicTitle = "profileTopic"; // Start the userProfile topic
                convo.userProfile = { // Define the user's profile object
                    "userName": undefined,
                    "age": undefined,
                    "workPlace": undefined
                }; 
            }

            // Start or continue a conversation if we are in one
            if(convo.topicStates.topicTitle == "profileTopic"){
                if(!convo.userProfile.userName && !convo.topicStates.prompt){
                    convo.topicStates.prompt = "askName";
                    await context.sendActivity("What is your name?");
                }
                else if(convo.topicStates.prompt == "askName"){
                    // Save the user's response
                    convo.userProfile.userName = context.activity.text; 

                    // Ask next question
                    convo.topicStates.prompt = "askAge";
                    await context.sendActivity("How old are you?");
                }
                else if(convo.topicStates.prompt == "askAge"){
                    // Save user's response
                    convo.userProfile.age = context.activity.text;

                    // Ask next question
                    convo.topicStates.prompt = "workPlace";
                    await context.sendActivity("Where do you work?");
                }
                else if(convo.topicStates.prompt == "workPlace"){
                    // Save user's response
                    convo.userProfile.workPlace = context.activity.text;

                    // Done
                    convo.topicStates.topicTitle = undefined; // Reset conversation flag
                    convo.topicStates.prompt = undefined;     // Reset the prompt flag
                    await context.sendActivity("Thank you. Your profile is complete.");
                }
            }

            // Check for valid intents
            else if(context.activity.text && context.activity.text.match(/hi/ig)){
                await context.sendActivity(`Hi ${convo.userProfile.userName}.`);
            }
            else {
                // Default message
                await context.sendActivity("Hi. I'm the Contoso bot.");
            }
        }

    });
});

```

---

The sample code above checks if a user's profile is defined in memory. If not and this is a new user, then set the flag to start that topic automatically. Then, it shows how you can move forward through your conversation by setting the `prompt` flag to the value of the current question being asked. With this flag set to the proper value, the conditional clause will catch the user's response on each turn and process it accordingly. 

Lastly, you must reset these flags when you are done asking for information so your bot is not trapped in a loop. You can extend this basic setup to manage more complex conversation flows as your bot requires simply by defining other flags or branching the conversation based on user input.

## Manage multiple topics of conversations

Once you are able to handle one topic of conversation, you can extend the bot logic to handle multiple topics of conversation. Just like a single topic of conversation, multiple topics can be handled simply by checking for conditions that trigger a particular topic, and then taking the appropriate path.

In our example above, you can refactor it to allow for other functions and topics, such as reserving a table or ordering dinner. Additional information can be added to the user profile or topic state flags as needed to keep track of the conversation.

To help better manage multiple topics of conversation, one method could be to provide a main menu. Using [suggested actions](bot-builder-howto-add-suggested-actions.md), you can give your user a choice of which topic they could to engage in, and then diving into that topic. You may also find it helpful to split code into independent functions, making it easier to follow the flow of conversation.

## Validate user input

The **Dialogs** library provides built in ways to validate the user's input, but we can also do so with our own prompts. For example, if we ask for the user's age we want to make sure we get a number, not something like "Bob" as a response.

Parsing a number or a date and time is a complex task that's beyond the scope of this topic. Fortunately, there is a library we can leverage. To properly parse this information, we use the [Microsoft's Text Recognizer](https://github.com/Microsoft/Recognizers-Text) library. This package is available through NuGet, or downloading it from the repository. Additionally, it's included in the **Dialogs** library, which is worth noting but we are not using that library here.

This library is particularly useful for parsing common language or complex responses about things like date, time, or phone numbers. In this sample, we're only validating a number for a dinner reservation party size, but the same idea can be extended for much more in depth validations. If you are not familiar with this library, review the content found on that site to see what it has to offer.

In this sample we only show the use of `RecognizeNumber`. Details on how to use other Recognizer methods from the library can be found in that [repository's documentation](https://github.com/Microsoft/Recognizers-Text/blob/master/README.md).

# [C#](#tab/csharp)

To use the recognizers library, add it to your using statements.

```csharp
using Microsoft.Recognizers.Text.Number;
using Microsoft.Recognizers.Text;
using System.Linq; // Used to get the first result from the recognizer
```

Then, define a function that actually does the validation.

```csharp
private async Task<bool> ValidatePartySize(ITurnContext context, string value)
{
    try
    {
        // Recognize the input as a number. This works for responses such as
        // "twelve" as well as "12"
        var result = NumberRecognizer.RecognizeNumber(input, Culture.English);

        // Attempt to convert the Recognizer result to an integer
        Int32.TryParse(result.First().Text, out int partySize);
        
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
        await context.SendActivity("Error with your party size. < br /> Please specify a number between 6 - 20.");
        return false;
    }
}
```

# [JavaScript](#tab/javascript)

To use the recognizers library, require it in **app.js**:

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

        if(value < 6) {
            throw new Error(`Party size too small.`);
        }
        else if(value > 20){
            throw new Error(`Party size too big.`);
        }
        return true; // Return the valid value
    }
    catch (err){
        await context.sendActivity(`${err.message} <br/>Please specify a number between 6 - 20.`);
        return false;
    }
}
```

---

Within the prompt step that we want to validate, call the validation function before moving on to the next prompt. If the validation fails, ask the question again.

# [C#](#tab/csharp)

```csharp
if (convoState.prompt == "partySize")
{
    if (await ValidatePartySize(context, context.Activity.Text))
    {
        // Save user's response in our state, ReservationInfo, which 
        // is a new class we've added to our state
        convoState.ReservationInfo.partySize = context.Activity.Text;

        // Ask next question
        convoState.prompt = "reserveName";
        await context.SendActivity("Who's name will this be under?");
    }
    else
    {
        // Ask again
        await context.SendActivity("How many people are in your party?");
    }
}
```

# [JavaScript](#tab/javascript)

**app.js**

```javascript
// ...
if(convo.topicStates.prompt == "partySize"){
    if(await validatePartySize(context, context.activity.text)){
        // Save user's response
        convo.reservationInfo.partySize = context.activity.text;
        
        // Ask next question
        topicStates.prompt = "reserveName";
        await context.sendActivity("Who's name will this be under?");
    }
    else {
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
