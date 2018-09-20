---
title: Manage a simple conversation flow with dialogs | Microsoft Docs
description: Learn how to manage a simple conversation flow with dialogs in the Bot Builder SDK for Node.js.
keywords: simple conversation flow, dialogs, prompts, waterfalls, dialog set
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 8/2/2018
monikerRange: 'azure-bot-service-4.0'
---

# Manage simple conversation flow with dialogs

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

You can manage simple and complex conversation flows using the dialogs library. In a simple conversation flow, the user starts from the first step of a *waterfall*, continues through to the last step, and the conversation finishes. [Complex conversation flows](~/v4sdk/bot-builder-dialog-manage-complex-conversation-flow.md) include branches and loop.

<!-- TODO: We need a dialogs conceptual topic to link to, so we can reference that here, in place of describing what they are and what their features are in a how-to topic. -->

<!-- TODO: This paragraph belongs in a conceptual topic. -->

Dialogs are structures in your bot that act like a functions in your bot's program. Dialogs build the messages your bot sends, and carry out the computational tasks required. They are designed to perform a specific operations, in a specific order. They can be invoked in different ways - sometimes in response to a user, sometimes in response to some outside stimuli, or by other dialogs.

Using dialogs enables the bot developer to guide the conversational flow. You can create multiple dialogs and link them together to create any conversation flow that you want your bot to handle. The **Dialogs** library in the Bot Builder SDK includes built-in features such as _prompts_, _waterfall dialogs_ and _component dialogs_ to help you manage conversation flow. You can use prompts to ask users for different types of information. You can use a waterfall to combine multiple steps together in a sequence. And you can use component dialogs to create modular dialog systems containing multiple sub-dialogs.

In this article, we use _dialog sets_ to create a conversation flow that contains both prompts and waterfalls. We have two example dialogs. The first is a one-step dialog that performs an operation that requires no user input. The second is a multi-step dialog that prompts the user for some information.

## Install the dialogs library

# [C#](#tab/csharp)

We'll start from a basic EchoBot template. For instructions, see the [quickstart for .NET](~/dotnet/bot-builder-dotnet-quickstart.md).

To use dialogs, install the `Microsoft.Bot.Builder.Dialogs` NuGet package for your project or solution.
Then reference the dialogs library in using statements in your code files as necessary.

```csharp
using Microsoft.Bot.Builder.Dialogs;
```

# [JavaScript](#tab/javascript)

The `botbuilder-dialogs` library can be downloaded from NPM. To install the `botbuilder-dialogs` library, run the following NPM command:

```cmd
npm install --save botbuilder-dialogs
```

To use **dialogs** in your bot, include this in your **app.js** file.

```javascript
const { DialogSet } = require('botbuilder-dialogs');

// Import ConversationState and MemoryStorage from botbuilder. These will be used to store
// state information for the dialogs.
const { ConversationState, MemoryStorage } = require('botbuilder');

const storage = new MemoryStorage();
const conversationState = new ConversationState(storage);

// Create a state property used by the dialog set.
const dialogState = conversationState.createProperty('dialogState');
```

---

## Create a dialog set

In this first example, we'll create a one-step dialog that can add two numbers together and display the result.

To use dialogs, you must first create a *dialog set*. Dialog sets are objects that group together one or more dialogs for use together. Your bot might have multiple dialog sets to group related dialogs together - but we'll start with just one.

# [C#](#tab/csharp)

The `Microsoft.Bot.Builder.Dialogs` library provides a `DialogSet` class.
Create an **AdditionDialog** class, and add the using statements we'll need.
You can add named dialogs and sets of dialogs to a dialog set, and then access them by name later.

```csharp
using Microsoft.Bot.Builder.Dialogs;
```

Derive the class from **DialogSet**, and define the IDs and keys we'll use to identify the dialogs and input information for this dialog set.

```csharp
/// <summary> Defines a simple dialog for adding two numbers together. </summary>
public class AdditionDialogSet : DialogSet
{
    /// <summary>The name of the main dialog in the set.</summary>
    public const string Main = "additionDialog";

    /// <summary>Contains the IDs for the prompts used in the dialog set.</summary>
    private struct Inputs
    {
        public const string First = "first";
        public const string Second = "second";
    }
}
```

# [JavaScript](#tab/javascript)

The `botbuilder-dialogs` library provides a `DialogSet` class.
The **DialogSet** class defines a **dialog stack** and gives you a simple interface to manage the stack.
The Bot Builder SDK implements the stack as an array.

To create a **DialogSet**, do the following:

```javascript
// Create the dialog set, passing in a state accessor.
const dialogs = new DialogSet(dialogState);
```

---

After creating a dialog set, add dialogs to the set to make them available to the bot to use. The dialog does not run immediately - adding it to a dialog set only prepares it to run later.  

The ID of each dialog (for example, `addTwoNumbers`) must be unique within each dialog set. You can define as many dialogs as necessary within each set. If you want to create multiple dialog sets and have them work seemlessly together, see [Create modular bot logic](bot-builder-compositcontrol.md).

The dialog library defines the following types of dialogs:

* A **prompt** dialog is a 2-turn dialog that requires at least two step functions, one to prompt the user for input and the other to process the input. You can string these together using the **waterfall** model.
* A **waterfall** dialog defines a sequence of _waterfall steps_, which run in order. A waterfall dialog can have a single step, in which case it can be thought of as a single-turn dialog.

## Create a single-turn dialog

Single-turn dialog are useful for implementing interactions where the bot only sends a single message. This example creates a bot that can detect if the user says something like "1 + 2", and starts an `addTwoNumbers` dialog to reply with "1 + 2 = 3".

# [C#](#tab/csharp)

Values are passed into and returned from dialogs as `objects` property bags.

To create a simple dialog within a dialog set, use the `Add` method. The following adds a one-step waterfall named `addTwoNumbers`.

This step assumes that the dialog arguments getting passed in contain `first` and `second` properties that represent the numbers to be added.

Add the following constructor to the **AdditionDialog** class.

```csharp
/// <summary>Defines the steps of the dialog.</summary>
public AdditionDialogSet(IStatePropertyAccessor<DialogState> dialogState)
    : base(dialogState)
{
    Add(new WaterfallDialog(Main, new WaterfallStep[]
    {
        async (step, cancellationToken) =>
        {
            // Get the input from the options to the dialog and add them.
            var options = step.Options as TwoNumbersClass;
            var sum = options.First + options.Second;

            // Display the result to the user.
            await step.Context.SendActivityAsync($"{options.First} + {options.Second} = {sum}");

            // End the dialog.
            return await step.EndDialogAsync();
        },
    }));
}
```

Next add the class that we need to hold our input.

```csharp
public class TwoNumbersClass
{
    public double First { get; set; }
    public double Second { get; set; }
}
```

### Pass arguments to the dialog

In your bot code, update your using statements.

```cs
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
```

Add a dialog accessors to the class for the addition dialog.

```cs
private readonly BotAccessors _accessors;
private readonly AdditionDialogSet _dialogs;
private readonly ILogger _logger;

public AdditionBot(BotAccessors accessors, ILoggerFactory loggerFactory)
{
    if (loggerFactory == null)
    {
        throw new System.ArgumentNullException(nameof(loggerFactory));
    }

    _logger = loggerFactory.CreateLogger<AdditionBot>();
    _logger.LogTrace("EchoBot turn start");
    _accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));
    _dialogs = new AdditionDialogSet(_accessors.DialogStateAccessor);
}
```

To call the dialog from within your bot's `OnTurn` method, modify `OnTurn` to contain the following:

```cs
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
{
    // Handle any message activity from the user.
    if (turnContext.Activity.Type == ActivityTypes.Message)
    {
        // Generate a dialog context for the addition dialog.
        var dc = await _dialogs.CreateContextAsync(turnContext, cancellationToken);

        // Call a helper function that identifies if the user says something
        // like "2 + 3" or "1.25 + 3.28" and extract the numbers to add.
        if (TryParseAddingTwoNumbers(turnContext.Activity.Text, out double first, out double second))
        {
            await dc.BeginDialogAsync(AdditionDialogSet.Main, new AdditionDialogSet.TwoNumbersClass { First = first, Second = second }, cancellationToken);
        }
        else
        {
            // Echo back to the user whatever they typed.
            await turnContext.SendActivityAsync($"You said '{turnContext.Activity.Text}'");
        }
    }
}
```

Add a **TryParseAddingTwoNumbers** helper function to the bot class. The helper function just uses a simple regex to detect if the user's message is a request to add 2 numbers.

```cs
// Recognizes if the message is a request to add 2 numbers, in the form: number + number,
// where number may have optionally have a decimal point.: 1 + 1, 123.99 + 45, 0.4+7.
// For the sake of simplicity it doesn't handle negative numbers or numbers like 1,000 that contain a comma.
// If you need more robust number recognition, try System.Recognizers.Text
public static bool TryParseAddingTwoNumbers(string message, out double first, out double second)
{
    // captures a number with optional -/+ and optional decimal portion
    const string NUMBER_REGEXP = "([-+]?(?:[0-9]+(?:\\.[0-9]+)?|\\.[0-9]+))";

    // matches the plus sign with optional spaces before and after it
    const string PLUSSIGN_REGEXP = "(?:\\s*)\\+(?:\\s*)";

    const string ADD_TWO_NUMBERS_REGEXP = NUMBER_REGEXP + PLUSSIGN_REGEXP + NUMBER_REGEXP;

    var regex = new Regex(ADD_TWO_NUMBERS_REGEXP);
    var matches = regex.Matches(message);

    first = 0;
    second = 0;
    if (matches.Count > 0)
    {
        var matched = matches[0];
        if (double.TryParse(matched.Groups[1].Value, out first)
            && double.TryParse(matched.Groups[2].Value, out second))
        {
            return true;
        }
    }

    return false;
}
```

If you're using the EchoBot template, change the name of the **EchoBotAccessors** class to **BotAccessors** and modify it to contain the following.

```cs
public class BotAccessors
{
    /// <summary> Gets the ConversationState object for the conversation. </summary>
    public ConversationState ConversationState { get; }

    /// <summary> Gets the IStatePropertyAccessor{T} name used for the DialogState accessor. </summary>
    public static string DialogStateName { get; } = $"{nameof(BotAccessors)}.DialogState";

    /// <summary> Gets or sets the IStatePropertyAccessor{T} for DialogState. </summary>
    public IStatePropertyAccessor<DialogState> DialogStateAccessor { get; set; }

    public BotAccessors(ConversationState conversationState)
    {
        this.ConversationState = conversationState
            ?? throw new ArgumentNullException(nameof(conversationState));
    }
}
```

# [JavaScript](#tab/javascript)

Start with the JS template described in [Create a bot with the Bot Builder SDK v4](../javascript/bot-builder-javascript-quickstart.md). In **app.js**, add a statement to require `botbuilder-dialogs`.

```js
const { DialogSet, WaterfallDialog } = require('botbuilder-dialogs');
const { ActivityTypes, ConversationState, MemoryStorage } = require('botbuilder');

const storage = new MemoryStorage();
const conversationState = new ConversationState(storage);
const dialogState = conversationState.createProperty('dialogState');
```

In **app.js**, add the following code that defines a simple dialog named `addTwoNumbers` that belongs to the `dialogs` set:

```javascript
const dialogs = new DialogSet(dialogState);

// Show the sum of two numbers.
dialogs.add(new WaterfallDialog('addTwoNumbers', [
    async function (step, numbers) {
        var sum = Number.parseFloat(numbers[0]) + Number.parseFloat(numbers[1]);
        await step.context.sendActivity(`${numbers[0]} + ${numbers[1]} = ${ sum }`);
        return await step.endDialog();
    }
]));
```

Replace the code in **app.js** for processing the incoming activity with the following. The bot calls a helper function to check if the incoming message looks like a request for adding two numbers. If it does, it passes the numbers in the argument to `dc.beginDialog`.

```js
// Listen for incoming activity 
server.post('/api/messages', (req, res) => {
    // Route received activity to adapter for processing
    adapter.processActivity(req, res, async (context) => {
        const isMessage = context.activity.type === ActivityTypes.Message;
        // Create a DialogContext object.
        const dc = dialogs.createContext(context);

        if (isMessage) {
            // TryParseAddingTwoNumbers checks if the message matches a regular expression
            // and if it does, returns an array of the numbers to add
            var numbers = TryParseAddingTwoNumbers(context.activity.text); 
            if (numbers != null && numbers.length >=2 ) {
                await dc.beginDialog('addTwoNumbers', numbers);
            } else {
                // Just echo back the user's message if they're not adding numbers
                await context.sendActivity(`You said "${ context.activity.text }"`);
            }
        } else {
            await context.sendActivity(`[${context.activity.type} event detected]`);
        }

        // If the bot hasn't yet responded...
        if (!context.responded) {
            // Continue any active dialog, which might send a response...
            await dc.continueDialog();
            
            // Finally, if the bot still hasn't sent a response, send instructions.
            if (!context.responded && isMessage) {
                await dc.context.sendActivity(`Hi! I'm the add 2 numbers bot. Say something like "What's 2+3?"`);
            }
        }
    });
});
```

Add the helper function to **app.js**. The helper function just uses a regular expression to detect if the user's message is a request to add 2 numbers. If the regular expression matches, it returns an array that contains the numbers to add.

```javascript
function TryParseAddingTwoNumbers(message) {
    const ADD_NUMBERS_REGEXP = /([-+]?(?:[0-9]+(?:\.[0-9]+)?|\.[0-9]+))(?:\s*)\+(?:\s*)([-+]?(?:[0-9]+(?:\.[0-9]+)?|\.[0-9]+))/i;
    let matched = ADD_NUMBERS_REGEXP.exec(message);
    if (!matched) {
        // message wasn't a request to add 2 numbers
        return null;
    }
    else {
        var numbers = [matched[1], matched[2]];
        return numbers;
    }
}
```

---

### Run the bot

Try running the bot in the Bot Framework Emulator, and say things like "what's 1+1?" to it.

![run the bot](./media/how-to-dialogs/bot-output-add-numbers.png)

## Using dialogs to guide the user through steps

In this example, we create a multi-step dialog to prompt the user for information utilizing the dialogSet we created above.

### Create a dialog with waterfall steps

A **WaterfallDialog** is a specific implementation of a dialog that is commonly used to collect information from the user or guide the user through a series of tasks. Each step of the conversation is implemented as a function. At each step, the bot [prompts the user for input](bot-builder-prompts.md), waits for a response, and then passes the result to the next step. The result of the first function is passed as an argument into the next function, and so on.

For example, the following code sample defines three functions in an array that represents the three steps of a **waterfall**. After each prompt, the bot acknowledges the user's input but doesn't store it in any way.  If you want to persist user input, see [Persist user data](bot-builder-tutorial-persist-user-inputs.md) for more details.

# [C#](#tab/csharp)

This shows a constructor for a greeting dialog, where **GreetingDialog** derives from **DialogSet**, **Inputs.Text** contains the ID we're using for the **TextPrompt** object, and **Main** contains the ID for the greeting dialog itself.

```csharp
public GreetingDialog()
{
    // Include a text prompt.
    Add(new TextPrompt(Inputs.Text));

    // Define the dialog logic for greeting the user.
    Add(new WaterfallDialog(Main, WaterfallStep[]
    {
        async (step, cancellationToken) =>
        {
            // Ask for their name.
            return await step.PromptAsync(Inputs.Text, new PromptOptions
            {
                Prompt = MessageFactory.Text("What is your name?"),
            });
        },

        async (step, cancellationToken) =>
        {
            // Save the prompt result in dialog state.
            step.Values[Values.Name] = step.Result;

            // Acknowledge their input.
            await step.Context.SendActivityAsync($"Hi, {step.Result}!");

            // Ask where they work.
            return await step.PromptAsync(Inputs.Text, new PromptOptions
            {
                Prompt = MessageFactory.Text("Where do you work?"),
            });
        },

        async (step, cancellationToken) =>
        {
            // Save the prompt result in dialog state.
            step.Values[Values.WorkPlace] = step.Result;

            // Acknowledge their input.
            await step.Context.SendActivityAsync($"{step.Result} is a fun place.");

            // End the dialog and return the collected information.
            return await step.EndDialogAsync(new Output
            {
                Name = step.Values[Values.Name] as string,
                WorkPlace = step.Values[Values.WorkPlace] as string,
            });
        },
    }));
}
```

# [JavaScript](#tab/javascript)

```javascript
// Greet user:
// Ask for the user name and then greet them by name.
// Ask them where they work.
dialogs.add(new WaterfallDialog('greetings', [
    async function (step) {
        return await step.prompt('textPrompt', 'What is your name?');
    },
    async function(step) {
        var userName = step.result;
        await step.context.sendActivity(`Hi ${ userName }!`);
        return await step.prompt('textPrompt', 'Where do you work?');
    },
    async function(step) {
        var workPlace = step.result;
        await step.context.sendActivity(`${ workPlace } is a fun place.`);
        return await step.endDialog(); // Ends the dialog
    }
]);

dialogs.add(new TextPrompt('textPrompt'));
```

---

The signature for a **waterfall** step is as follows:

| Parameter | Description |
| :---- | :----- |
| `step` | The step context. |
| `options` | Optional, contains the arguments passed into the step. |
| `next` | Optional, a method that allows you to proceed to the next step of the waterfall without prompting. You can provide an *option* argument when you call this method, This allows you to pass arguments to the next step in the waterfall. |

Each step must call the *next()* delegate, or one of other the dialog context methods: *beginDialog*, *endDialog*, *prompt*, or *replaceDialog*. Otherwise, the bot will get stuck: the waterfall will not move forward to the next step, and the same step will be re-executed each time the user sends the bot a message. 

When you reached the end of the waterfall, it is best practice to return with the _endDialog_ method. See [End a dialog](#end-a-dialog) section below for more information. 

## Start a dialog

To start a dialog, pass the *dialogId* you want to start into the dialog context's _beginDialog_, _prompt_, or _replaceDialog_ method. The _beginDialog_ method will push the dialog onto the top of the stack, while the _replaceDialog_ method will pop the current dialog off the stack and push the replacing dialog onto the stack.

To start a dialog without arguments:

# [C#](#tab/csharp)

```csharp
// Start the greetings dialog.
await step.BeginDialogAsync(GreetingDialogSet.Main);
```

# [JavaScript](#tab/javascript)

```javascript
// Start the 'greetings' dialog.
await dc.beginDialog('greetings');
```

---

To start a dialog with arguments:

# [C#](#tab/csharp)

```csharp
// Start the greetings dialog with the 'userName' passed in.
await step.BeginDialogAsync(GreetingDialogSet.Main, userName);
```

# [JavaScript](#tab/javascript)

```javascript
// Start the 'greetings' dialog with the 'userName' passed in.
await dc.beginDialog('greetings', userName);
```

---

To start a **prompt** dialog:

# [C#](#tab/csharp)

Here, **Inputs.Text** contains the ID of a **TextPrompt** that is in the same dialog set.

```csharp
// Ask a user for their name.
return await step.PromptAsync(Inputs.Text, new PromptOptions
{
    Prompt = MessageFactory.Text("What is your name?"),
});
```

# [JavaScript](#tab/javascript)

```javascript
// Ask a user for their name.
await dc.prompt('textPrompt', "What is your name?");
```

---

Depending on the type of prompt you are starting, the prompt's argument signature may be different. The **DialogSet.prompt** method is a helper method. This method takes in arguments and constructs the appropriate options for the prompt; then, it calls the **begin** method to start the prompt dialog. For more information on prompts, see [Prompt user for input](bot-builder-prompts.md).

## End a dialog

The _endDialog_ method ends a dialog by popping it off the stack and returns an optional result to the parent dialog.

It is best practice to explicitly call the _endDialog_ method at the end of the dialog; however, it is not required because the dialog will automatically be popped off the stack for you when you reach the end of the waterfall.

To end a dialog:

# [C#](#tab/csharp)

```csharp
// End the current dialog by popping it off the stack.
await step.EndDialogAsync();
```

# [JavaScript](#tab/javascript)

```javascript
// End the current dialog by popping it off the stack
await dc.endDialog();
```

---

To end a dialog and return information to the parent dialog, include a property bag argument.

# [C#](#tab/csharp)

```csharp
// End the current dialog and return information to the parent dialog.
return await step.EndDialogAsync(new Output
{
    Name = step.Values[Values.Name] as string,
    WorkPlace = step.Values[Values.WorkPlace] as string,
});
```

# [JavaScript](#tab/javascript)

```javascript
// End the current dialog and pass a result to the parent dialog
await dc.endDialog({
    "property1": value1,
    "property2": value2
});
```

---

## Clear the dialog stack

If you want to pop all dialogs off the stack, you can clear the dialog stack by calling the _cancel all dialogs_ method.

# [C#](#tab/csharp)

```csharp
// Pop all dialogs from the current stack.
await step.CancelAllDialogsAsync();
```

# [JavaScript](#tab/javascript)

```javascript
// Pop all dialogs from the current stack.
await dc.cancelAllDialogs();
```

---

## Repeat a dialog

To repeat a dialog, use the _replaceDialog_ method. The dialog context's *replaceDialog* method will pop the current dialog off the stack and push the replacing dialog onto the top of the stack and begin that dialog. This is a great way to handle [complex conversation flows](~/v4sdk/bot-builder-dialog-manage-complex-conversation-flow.md) and a good technique to manage menus.

# [C#](#tab/csharp)

```csharp
// End the current dialog and start the main menu dialog.
await step.ReplaceDialogAsync(MainMenu.Main);
```

# [JavaScript](#tab/javascript)

```javascript
// End the current dialog and start the 'mainMenu' dialog.
await dc.replaceDialog('mainMenu');
```

---

## Next steps

Now that you've learned how to manage simple conversation flows, let's take a look at how you can leverage the _replaceDialog_ method to handle complex conversation flows.

> [!div class="nextstepaction"]
> [Manage complex conversation flow](bot-builder-dialog-manage-complex-conversation-flow.md)
