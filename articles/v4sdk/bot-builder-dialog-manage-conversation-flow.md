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

You can manage both simple and complex conversation flows using the dialogs library. In a simple conversation flow, the user starts from the first step of a *waterfall*, continues through to the last step, and the conversational exchange finishes. Dialogs can also handle [complex conversation flows](~/v4sdk/bot-builder-dialog-manage-complex-conversation-flow.md), where portions of the dialog can branch and loop.

<!-- TODO: We need a dialogs conceptual topic to link to, so we can reference that here, in place of describing what they are and what their features are in a how-to topic. -->

<!-- TODO: This paragraph belongs in a conceptual topic. -->
A dialog is like a function in a program. It is generally designed to perform a specific operation, in a specific order, and it can be invoked as often as it is needed. Using dialogs enables the bot developer to guide conversational flow. You can chain multiple dialogs together to handle just about any conversation flow that you want your bot to handle. The **Dialogs** library in the Bot Builder SDK includes built-in features such as _prompts_ and _waterfall dialogs_ to help you manage conversation flow. You can use prompts to ask users for different types of information. You can use a waterfall to combine multiple steps together in a sequence.

In this article, we use _dialog sets_ to create a conversation flow that contains both prompts and waterfall steps. We have two example dialogs. The first is a one-step dialog that performs an operation that requires no user input. The second is a multi-step dialog that prompts the user for some information.

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
npm install --save botbuilder-dialogs@preview
```

To use **dialogs** in your bot, include this in your **app.js** file.

```javascript
const botbuilder_dialogs = require('botbuilder-dialogs');
```

---

## Create a dialog stack

In this first example, we'll create a one-step dialog that can add two numbers together and display the result.

To use dialogs, you must first create a *dialog set*.

# [C#](#tab/csharp)

The `Microsoft.Bot.Builder.Dialogs` library provides a `DialogSet` class.
Create an **AdditionDialog** class, and add the using statements we'll need.
You can add named dialogs and sets of dialogs to a dialog set, and then access them by name later.

```csharp
using Microsoft.Bot.Builder.Dialogs;
```

Derive the class from **DialogSet**, and define the IDs and keys we'll use to identify the dialogs and input information for this dialog set.

```csharp
/// <summary>Defines a simple dialog for adding two numbers together.</summary>
public class AdditionDialog : DialogSet
{
    /// <summary>The ID of the main dialog in the set.</summary>
    public const string Main = "additionDialog";

    /// <summary>Defines the IDs of the input arguments.</summary>
    public struct Inputs
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
const dialogs = new botbuilder_dialogs.DialogSet();
```

The call above will create a **DialogSet** with a default **dialog stack** named `dialogStack`.
If you want to name your stack, you can pass it in as a parameter to **DialogSet()**. For example:

```javascript
const dialogs = new botbuilder_dialogs.DialogSet("myStack");
```

---

Creating a dialog only adds the dialog definition to the set. The dialog is not run until it is pushed onto the dialog stack by calling a _begin_ or _replace_ method.

The dialog name (for example, `addTwoNumbers`) must be unique within each dialog set. You can define as many dialogs as necessary within each set. If you want to create multiple dialog sets and have them work seemlessly together, see [Create modular bot logic](bot-builder-compositcontrol.md).

The dialog library defines the following dialogs:

* A **prompt** dialog where the dialog uses at least two functions, one to prompt the user for input and the other to process the input. You can string these together using the **waterfall** model.
* A **waterfall** dialog defines a sequence of _waterfall steps_, which run in order. A waterfall dialog can have a single step, in which case it can be thought of as a simple, one-step dialog.

## Create a single-step dialog

Single-step dialogs can be useful for capturing single-turn conversational flows. This example creates a bot that can detect if the user says something like "1 + 2", and starts an `addTwoNumbers` dialog to reply with "1 + 2 = 3".

# [C#](#tab/csharp)

Values are passed into and returned from dialogs as `IDictionary<string,object>` property bags.

To create a simple dialog within a dialog set, use the `Add` method. The following adds a one-step waterfall named `addTwoNumbers`.

This step assumes that the dialog arguments getting passed in contain `first` and `second` properties that represent the numbers to be added.

Add the following constructor to the **AdditionDialog** class.

```csharp
/// <summary>Defines the steps of the dialog.</summary>
public AdditionDialog()
{
    Add(Main, new WaterfallStep[]
    {
        async (dc, args, next) =>
        {
            // Get the input from the arguments to the dialog and add them.
            var x =(double)args[Inputs.First];
            var y =(double)args[Inputs.Second];
            var sum = x + y;

            // Display the result to the user.
            await dc.Context.SendActivity($"{x} + {y} = {sum}");

            // End the dialog.
            await dc.End();
        }
    });
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

Add a static property to the class for the addition dialog.

```cs
private static AdditionDialog AddTwoNumbers { get; } = new AdditionDialog();
```

To call the dialog from within your bot's `OnTurn` method, modify `OnTurn` to contain the following:

```cs
public async Task OnTurn(ITurnContext context)
{
    // Handle any message activity from the user.
    if (context.Activity.Type is ActivityTypes.Message)
    {
        // Get the conversation state from the turn context.
        var conversationState = context.GetConversationState<ConversationData>();

        // Generate a dialog context for the addition dialog.
        var dc = AddTwoNumbers.CreateContext(context, conversationState.DialogState);

        // Call a helper function that identifies if the user says something
        // like "2 + 3" or "1.25 + 3.28" and extract the numbers to add.
        if (TryParseAddingTwoNumbers(context.Activity.Text, out double first, out double second))
        {
            // Start the dialog, passing in the numbers to add.
            var args = new Dictionary<string, object>
            {
                [AdditionDialog.Inputs.First] = first,
                [AdditionDialog.Inputs.Second] = second
            };
            await dc.Begin(AdditionDialog.Main, args);
        }
        else
        {
            // Echo back to the user whatever they typed.
            await context.SendActivity($"You said '{context.Activity.Text}'");
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

If you're using the EchoBot template, change the name of the **EchoState** class to **ConversationData** and modify it to contain the following.

```cs
using System.Collections.Generic;

/// <summary>
/// Class for storing conversation state.
/// </summary>
public class ConversationData
{
    /// <summary>Property for storing dialog state.</summary>
    public Dictionary<string, object> DialogState { get; set; } = new Dictionary<string, object>();
}
```

# [JavaScript](#tab/javascript)

Start with the JS template described in [Create a bot with the Bot Builder SDK v4](../javascript/bot-builder-javascript-quickstart.md). In **app.js**, add a statement to require `botbuilder-dialogs`.

```js
const {DialogSet} = require('botbuilder-dialogs');
```

In **app.js**, add the following code that defines a simple dialog named `addTwoNumbers` that belongs to the `dialogs` set:

```javascript
const dialogs = new DialogSet("myDialogStack");

// Show the sum of two numbers.
dialogs.add('addTwoNumbers', [async function (dc, numbers){
        var sum = Number.parseFloat(numbers[0]) + Number.parseFloat(numbers[1]);
        await dc.context.sendActivity(`${numbers[0]} + ${numbers[1]} = ${sum}`);
        await dc.end();
    }]
);
```

Replace the code in **app.js** for processing the incoming activity with the following. The bot calls a helper function to check if the incoming message looks like a request for adding two numbers. If it does, it passes the numbers in the argument to `DialogContext.begin`.

```js
// Listen for incoming activity 
server.post('/api/messages', (req, res) => {
    // Route received activity to adapter for processing
    adapter.processActivity(req, res, async (context) => {
        const isMessage = context.activity.type === 'message';
        // State will store all of your information
        const convoState = conversationState.get(context);
        const dc = dialogs.createContext(context, convoState);

        if (isMessage) {
            // TryParseAddingTwoNumbers checks if the message matches a regular expression
            // and if it does, returns an array of the numbers to add
            var numbers = await TryParseAddingTwoNumbers(context.activity.text); 
            if (numbers != null && numbers.length >=2 )
            {
                await dc.begin('addTwoNumbers', numbers);
            }
            else {
                // Just echo back the user's message if they're not adding numbers
                const count = (convoState.count === undefined ? convoState.count = 0 : ++convoState.count);
                return context.sendActivity(`Turn ${count}: You said "${context.activity.text}"`);
            }
        }
        else {
            return context.sendActivity(`[${context.activity.type} event detected]`);
        }

        if (!context.responded) {
            await dc.continue();
            // if the dialog didn't send a response
            if (!context.responded && isMessage) {
                await dc.context.sendActivity(`Hi! I'm the add 2 numbers bot. Say something like "What's 2+3?"`);
            }
        }
    });
});

```

Add the helper function to **app.js**. The helper function just uses a simple regular expression to detect if the user's message is a request to add 2 numbers. If the regular expression matches, it returns an array that contains the numbers to add.

```javascript
async function TryParseAddingTwoNumbers(message) {
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

In this next example, we create a multi-step dialog to prompt the user for information.

### Create a dialog with waterfall steps

A **waterfall** is a specific implementation of a dialog that is most commonly used to collect information from the user or guide the user through a series of tasks. The tasks are implemented as an array of functions where the result of the first function is passed as arguments into the next function, and so on. Each function typically represents one step in the overall process. At each step, the bot [prompts the user for input](bot-builder-prompts.md), waits for a response, and then passes the result to the next step.

For example, the following code sample defines three functions in an array that represents the three steps of a **waterfall**. After each prompt, the bot acknowledges the user's input but did not save the input. If you want to persist user inputs, see [Persist user data](bot-builder-tutorial-persist-user-inputs.md) for more details.

# [C#](#tab/csharp)

This shows a constructor for a greeting dialog, where **GreetingDialog** derives from **DialogSet**, **Inputs.Text** contains the ID we're using for the **TextPrompt** object, and **Main** contains the ID for the greeting dialog itself.

```csharp
public GreetingDialog()
{
    // Include a text prompt.
    Add(Inputs.Text, new TextPrompt());

    // Define the dialog logic for greeting the user.
    Add(Main, new WaterfallStep[]
    {
        async (dc, args, next) =>
        {
            // Ask for their name.
            await dc.Prompt(Inputs.Text, "What is your name?");
        },
        async (dc, args, next) =>
        {
            // Get the prompt result.
            var name = args["Text"] as string;

            // Acknowledge their input.
            await dc.Context.SendActivity($"Hi, {name}!");

            // Ask where they work.
            await dc.Prompt(Inputs.Text, "Where do you work?");
        },
        async (dc, args, next) =>
        {
            // Get the prompt result.
            var work = args["Text"] as string;

            // Acknowledge their input.
            await dc.Context.SendActivity($"{work} is a fun place.");

            // End the dialog.
            await dc.End();
        }
    });
}
```

# [JavaScript](#tab/javascript)

```javascript
// Greet user:
// Ask for the user name and then greet them by name.
// Ask them where they work.
dialogs.add('greetings',[
    async function (dc){
        await dc.prompt('textPrompt', 'What is your name?');
    },
    async function(dc, results){
        var userName = results;
        await dc.context.sendActivity(`Hi ${userName}!`);
        await dc.prompt('textPrompt', 'Where do you work?');
    },
    async function(dc, results){
        var workPlace = results;
        await dc.context.sendActivity(`${workPlace} is a fun place.`);
        await dc.end(); // Ends the dialog
    }
]);

dialogs.add('textPrompt', new botbuilder_dialogs.TextPrompt());
```

---

The signature for a **waterfall** step is as follows:

| Parameter | Description |
| :---- | :----- |
| `dc` | The dialog context. |
| `args` | Optional, contains the arguments passed into the step. |
| `next` | Optional, a method that allows you to proceed to the next step of the waterfall without prompting. You can provide an *args* argument when you call this method, This allows you to pass arguments to the next step in the waterfall. |

Each step must call one of the following methods before returning: the *next()* delegate or one of the dialog context methods *begin*, *end*, *prompt*, or *replace*; otherwise, the bot will be stuck in that step. That is, if a function does not finish with one of these methods then all user input will cause this step to be re-executed each time the user sends the bot a message.

When you reached the end of the waterfall, it is best practice to return with the _end_ method so that the dialog can be popped off the stack. See [End a dialog](#end-a-dialog) section below for more information. Likewise, to proceed from one step to the next, the waterfall step must end with either a prompt or explicitly call the _next_ delegate to advance the waterfall.

## Start a dialog

To start a dialog, pass the *dialogId* you want to start into the dialog context's _begin_, _prompt_, or _replace_ method. The _begin_ method will push the dialog onto the top of the stack, while the _replace_ method will pop the current dialog off the stack and push the replacing dialog onto the stack.

To start a dialog without arguments:

# [C#](#tab/csharp)

```csharp
// Start the greetings dialog.
await dc.Begin("greetings");
```

# [JavaScript](#tab/javascript)

```javascript
// Start the 'greetings' dialog.
await dc.begin('greetings');
```

---

To start a dialog with arguments:

# [C#](#tab/csharp)

```csharp
// Start the greetings dialog, passing in a property bag.
await dc.Begin("greetings", args);
```

# [JavaScript](#tab/javascript)

```javascript
// Start the 'greetings' dialog with the 'userName' passed in.
await dc.begin('greetings', userName);
```

---

To start a **prompt** dialog:

# [C#](#tab/csharp)

Here, **Inputs.Text** contains the ID of a **TextPrompt** that is in the same dialog set.

```csharp
// Ask a user for their name.
await dc.Prompt(Inputs.Text, "What is your name?");
```

# [JavaScript](#tab/javascript)

```javascript
// Ask a user for their name.
await dc.prompt('textPrompt', "What is your name?");
```

---

Depending on the type of prompt you are starting, the prompt's argument signature may be different. The **DialogSet.prompt** method is a helper method. This method takes in arguments and constructs the appropriate options for the prompt; then, it calls the **begin** method to start the prompt dialog. For more information on prompts, see [Prompt user for input](bot-builder-prompts.md).

## End a dialog

The _end_ method ends a dialog by popping it off the stack and returns an optional result to the parent dialog.

It is best practice to explicitly call the _end_ method at the end of the dialog; however, it is not required because the dialog will automatically be popped off the stack for you when you reach the end of the waterfall.

To end a dialog:

# [C#](#tab/csharp)

```csharp
// End the current dialog by popping it off the stack.
await dc.End();
```

# [JavaScript](#tab/javascript)

```javascript
// End the current dialog by popping it off the stack
await dc.end();
```

---

To end a dialog and return information to the parent dialog, include a property bag argument.

# [C#](#tab/csharp)

```csharp
// End the current dialog and return information to the parent dialog.
await dc.end(new Dictionary<string, object>
    {
        ["property1"] = value1,
        ["property2"] = value2
    });
```

# [JavaScript](#tab/javascript)

```javascript
// End the current dialog and pass a result to the parent dialog
await dc.end({
    "property1": value1,
    "property2": value2
});
```

---

## Clear the dialog stack

If you want to pop all dialogs off the stack, you can clear the dialog stack by calling the _end all_ method.

# [C#](#tab/csharp)

```csharp
// Pop all dialogs from the current stack.
await dc.EndAll();
```

# [JavaScript](#tab/javascript)

```javascript
// Pop all dialogs from the current stack.
await dc.endAll();
```

---

## Repeat a dialog

To repeat a dialog, use the _replace_ method. The dialog context's *replace* method will pop the current dialog off the stack and push the replacing dialog onto the top of the stack and begin that dialog. This is a great way to handle [complex conversation flows](~/v4sdk/bot-builder-dialog-manage-complex-conversation-flow.md) and a good technique to manage menus.

# [C#](#tab/csharp)

```csharp
// End the current dialog and start the main menu dialog.
await dc.Replace("mainMenu");
```

# [JavaScript](#tab/javascript)

```javascript
// End the current dialog and start the 'mainMenu' dialog.
await dc.replace('mainMenu');
```

---

## Next steps

Now that you've learned how to manage simple conversation flows, lets take a look at how you can leverage the _replace_ method to handle complex conversation flows.

> [!div class="nextstepaction"]
> [Manage complex conversation flow](bot-builder-dialog-manage-complex-conversation-flow.md)
