---
title: Prompt users for input | Microsoft Docs
description: Learn how to prompt users for input in the Bot Builder SDK for Node.js.
keywords: prompts, dialogs, AttachmentPrompt, ChoicePrompt, ConfirmPrompt, DatetimePrompt, NumberPrompt, TextPrompt, reprompt, validation
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 4/10/2018
monikerRange: 'azure-bot-service-4.0'
---
# Prompt users for input

[!INCLUDE [pre-release-label](~/includes/pre-release-label.md)]

Often bots gather their information through questions posed to the user. You can simply send the user a standard message by using the turn context object's _send activity_ method to ask for a string input; however, the Bot Builder SDK provides a **dialogs** library that you can use to ask for different types for information. This topic details how to use **prompts** to ask a user for input.

This article describes how to use prompts within a dialog. For information on using dialogs in general, see [using dialogs to manage conversation flow](bot-builder-dialog-manage-conversation-flow.md).

## Prompt types

The dialogs library offers a number of different types of prompts, each requesting a different type of response.

| Prompt | Description |
|:----|:----|
| **AttachmentPrompt** | Prompt the user for an attachment such as a document or image. |
| **ChoicePrompt** | Prompt the user to choose from a set of options. |
| **ConfirmPrompt** | Prompt the user to confirm their action. |
| **DatetimePrompt** | Prompt the user for a date-time. Users can respond using natural language such as "Tomorrow at 8pm" or "Friday at 10am". The Bot Framework SDK uses the LUIS `builtin.datetimeV2` prebuilt entity. For more information, see [builtin.datetimev2](https://docs.microsoft.com/en-us/azure/cognitive-services/luis/luis-reference-prebuilt-entities#builtindatetimev2). |
| **NumberPrompt** | Prompt the user for a number. The user can respond with either "10" or "ten". If the response is "ten", for example, the prompt will convert the response into a number and return `10` as a result. |
| **TextPrompt** | Prompt user for a string of text. |

## Add references to prompt library

You can get the **dialogs** library by adding the **dialogs** package to your bot. We cover dialogs in [using dialogs to manage conversation flow](bot-builder-dialog-manage-conversation-flow.md), but we'll use dialogs for our prompts.

# [C#](#tab/csharp)

Install the **Microsoft.Bot.Builder.Dialogs** package from NuGet.

Then, include reference the library from your bot code.

```cs
using Microsoft.Bot.Builder.Dialogs;
```

You can define a dialog as a class or in-line as a property in your bot code file.

The code in this article is written for a dialog defined as a class.
The following examples assume you are adding code to the dialog's constructor.

The main flow of your dialog is its collection of steps and needs to be given an ID. Your bot uses this ID to retrieve the dialog, so it is a good practice to expose this as a constant.

```cs
public class MyDialog : DialogSet
{
    public const string Name = "mainDialog";

    public MyDialog()
    {
        // Define your dialog's prompts and steps here.
    }
}
```

# [JavaScript](#tab/javascript)

Install the dialogs package from NPM:

```cmd
npm install --save botbuilder-dialogs
```

To use **dialogs** in your bot, include it in the bot code.

In the app.js file, add the following.

```javascript
const {DialogSet} = require("botbuilder-dialogs");
const dialogs = new DialogSet();
```

---

## Prompt the user

To prompt a user for input, you can add a prompt to your dialog. For example, you can define a prompt of type **TextPrompt** and give it a dialog ID of **textPrompt**:

Once a prompt dialog is added, you can use it in a simple two step waterfall dialog or use multiple prompts together in a multi-step waterfall. A *waterfall* dialog is simply a way to define a sequence of steps. For more information, see the [using dialogs](bot-builder-dialog-manage-conversation-flow.md#using-dialogs-to-guide-the-user-through-steps) section of [manage conversation flow with dialogs](bot-builder-dialog-manage-conversation-flow.md).

On the first turn, the dialog prompts the user for their name, and on the second turn, the dialog processes the user input as an answer to the prompt.

For example, the following dialog prompts the user for their name and then greets them by name:

# [C#](#tab/csharp)

Each prompt you use in your dialog is also given a name, used by the dialog or your bot to access the prompt. In all of these samples, we are exposing the prompt IDs as constants.

A call to the dialog context's **Prompt** or **End** method signals the end of the step to the dialog. Without these statements, the dialog will not run properly.

```csharp
/// <summary>Defines a simple greeting dialog that asks for the user's name.</summary>
public class MyDialog : DialogSet
{
    /// <summary>The ID of the main dialog in the set.</summary>
    public const string Name = "mainDialog";

    /// <summary>Defines the IDs of the prompts in the set.</summary>
    public struct Inputs
    {
        /// <summary>The ID of the text prompt.</summary>
        public const string Text = "textPrompt";
    }

    /// <summary>Defines the prompts and steps of the dialog.</summary>
    public MyDialog()
    {
        Add(Inputs.Text, new TextPrompt());
        Add(Name, new WaterfallStep[]
        {
            // Each step takes in a dialog context, arguments, and the next delegate.
            async (dc, args, next) =>
            {
                // Prompt for the user's name.
                await dc.Prompt(Inputs.Text, "What is your name?").ConfigureAwait(false);
            },
            async(dc, args, next) =>
            {
                var user = (string)args["Text"];
                await dc.Context.SendActivity($"Hi {user}!").ConfigureAwait(false);
                await dc.End().ConfigureAwait(false);
            }
        });
    }
}
```

# [JavaScript](#tab/javascript)

```javascript
const {TextPrompt} = require("botbuilder-dialogs");
```

```javascript
// Greet user:
// Ask for the user name and then greet them by name.
dialogs.add('textPrompt', new TextPrompt());
dialogs.add('greetings', [
    async function (dc){
        await dc.prompt('textPrompt', 'What is your name?');
    },
    async function(dc, userName){
        await dc.context.sendActivity(`Hi ${userName}!`);
        await dc.end();
    }
]);
```

---

> [!NOTE]
> To start a dialog, get a dialog context, and use its _begin_ method. For more information, see [use dialogs to managed conversation flow](./bot-builder-dialog-manage-conversation-flow.md).

## Reusable prompts

A prompt can be reused to ask for different information using the same type of prompt. For example, the sample code above defined a text prompt and used it to ask the user for their name. If you wanted to, for example, you can also use that same prompt to ask the user for another text string; such as, "Where do you work?".

# [C#](#tab/csharp)

```cs
/// <summary>Defines a simple greeting dialog that asks for the user's name and place of work.</summary>
public class MyDialog : DialogSet
{
    /// <summary>The ID of the main dialog in the set.</summary>
    public const string Name = "mainDialog";

    /// <summary>Defines the IDs of the prompts in the set.</summary>
    public struct Inputs
    {
        /// <summary>The ID of the text prompt.</summary>
        public const string Text = "textPrompt";
    }

    /// <summary>Defines the prompts and steps of the dialog.</summary>
    public MyDialog()
    {
        Add(Inputs.Text, new TextPrompt());
        Add(Name, new WaterfallStep[]
        {
            async (dc, args, next) =>
            {
                // Prompt for the user's name.
                await dc.Prompt(Inputs.Text, "What is your name?").ConfigureAwait(false);
            },
            async(dc, args, next) =>
            {
                var user = (string)args["Text"];

                // Ask them where they work.
                await dc.Prompt(Inputs.Text, $"Hi {user}! Where do you work?").ConfigureAwait(false);
            },
            async(dc, args, next) =>
            {
                var workplace = (string)args["Text"];

                await dc.Context.SendActivity($"{workplace} is a cool place!").ConfigureAwait(false);
                await dc.End().ConfigureAwait(false);
            }
        });
    }
}
```

# [JavaScript](#tab/javascript)

```javascript
// Greet user:
// Ask for the user name and then greet them by name.
// Ask them where they work.
dialogs.add('textPrompt', new botbuilder_dialogs.TextPrompt());
dialogs.add('greetings',[
    async function (dc){
        await dc.prompt('textPrompt', 'What is your name?');
    },
    async function(dc, userName){
        await dc.context.sendActivity(`Hi ${userName}!`);

        // Ask them where they work.
        await dc.prompt('textPrompt', 'Where do you work?');
    },
    async function(dc, workPlace){
        await dc.context.sendActivity(`${workPlace} is a cool place!`);

        await dc.end();
    }
]);
```

---

However, if you wish to pair the prompt to the expected value that prompt is asking, you could give each prompt a unique *dialogId*. A dialog is added with a unique ID. Using different IDs, you can also create multiple **prompt** dialogs of the same type. For example, you could create two **TextPrompt** dialogs for the example above:

# [C#](#tab/csharp)

```cs
/// <summary>The ID of the main dialog in the set.</summary>
public const string Name = "mainDialog";

/// <summary>Defines the IDs of the prompts in the set.</summary>
public struct Inputs
{
    /// <summary>The ID of the name prompt.</summary>
    public const string Name = "namePrompt";

    /// <summary>The ID of the work prompt.</summary>
    public const string Work = "workPrompt";
}

/// <summary>Defines the prompts and steps of the dialog.</summary>
public MyDialog()
{
    Add(Inputs.Name, new TextPrompt());
    Add(Inputs.Work, new TextPrompt());
    Add(Name, new WaterfallStep[]
    {
        async (dc, args, next) =>
        {
            // Prompt for the user's name.
            await dc.Prompt(Inputs.Name, "What is your name?").ConfigureAwait(false);
        },
        async(dc, args, next) =>
        {
            var user = (string)args["Text"];

            // Ask them where they work.
            await dc.Prompt(Inputs.Work, $"Hi {user}! Where do you work?").ConfigureAwait(false);
        },
        async(dc, args, next) =>
        {
            var workplace = (string)args["Text"];

            await dc.Context.SendActivity($"{workplace} is a cool place!").ConfigureAwait(false);
            await dc.End().ConfigureAwait(false);
        }
    });
}
```

# [JavaScript](#tab/javascript)

```javascript
dialogs.add('namePrompt', new TextPrompt());
dialogs.add('workPlacePrompt', new TextPrompt());
```

---

For the sake of code reusability, defining a single `textPrompt` would work for all these prompts because they ask for a text string as a response. However, where the ability to name dialogs comes in handy is when you need to validate the input of the prompt. In which case, the prompts may be using **TextPrompt** but each is looking for a different set of values. Lets take a look at how you can validate prompt responses using a `NumberPrompt`.

## Specify prompt options

When you use a prompt within a dialog step, you can also provide prompt options, such as a reprompt string.

Specifying a reprompt string is useful when user input can fail to satisfy a prompt, either because it is in a format that the prompt can not parse, such as "tomorrow" for a number prompt, or the input fails a validation criteria.

> [!TIP]
> When you create a number prompt, you need to specify the input culture it will use. The number prompt can interpret a wide variety of input, such as "twelve" or "one quarter", as well as "12" and "0.25". The input culture helps the prompt more correctly interpret the user's input.

# [C#](#tab/csharp)

Input cultures are defined in an additional library.

```csharp
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Recognizers.Text;
```

The following code would add a number prompt to an existing dialog set, **dialogs**.

```csharp
dialogs.Add("numberPrompt", new NumberPrompt<int>(Culture.English));
```

Within a dialog step, the following code would prompt the user for input and provide a reprompt string to use if their input cannot be interpreted as a number.

```csharp
await dc.Prompt("numberPrompt", "How many people are in your party?", new PromptOptions()
{
    RetryPromptString = "Sorry, please specify the number of people in your party."
}).ConfigureAwait(false);
```

# [JavaScript](#tab/javascript)

```javascript
const {NumberPrompt} = require("botbuilder-dialogs");
```

```javascript
await dc.prompt('numberPrompt', 'How many people in your party?', { retryPrompt: `Sorry, please specify the number of people in your party.` })
```

```javascript
dialogs.add('numberPrompt', new NumberPrompt());
```

---

In particular, the choice prompt requires some additional information, the list of choices available to the user.

# [C#](#tab/csharp)

This example uses types from the following namespaces.

```csharp
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Prompts.Choices;
using Microsoft.Bot.Schema;
using Microsoft.Recognizers.Text;
using System.Collections.Generic;
```


When we use the **ChoicePrompt** to ask the user to choose between a set of options, we have to provide the prompt with that set of options, provided within a **ChoicePromptOptions** object. Here, we use the **ChoiceFactory** to convert a list of options into an appropriate format.

We are also using a **SuggestedActions** activity as the reprompt, as a way of providing the input options to the user again.


```csharp
/// <summary>Defines a dialog that asks for a choice of color.</summary>
public class MyDialog : DialogSet
{
    /// <summary>The ID of the main dialog in the set.</summary>
    public const string Name = "mainDialog";

    /// <summary>Defines the IDs of the prompts in the set.</summary>
    public struct Inputs
    {
        /// <summary>The ID of the color prompt.</summary>
        public const string Color = "colorPrompt";
    }

    /// <summary>The available colors to choose from.</summary>
    public List<string> Colors = new List<string> { "Green", "Blue" };

    /// <summary>Defines the prompts and steps of the dialog.</summary>
    public MyDialog()
    {
        Add(Inputs.Color, new ChoicePrompt(Culture.English));
        Add(Name, new WaterfallStep[]
        {
            async (dc, args, next) =>
            {
                // Prompt for a color. A choice prompt requires that you specify choice options.
                await dc.Prompt(Inputs.Color, "Please make a choice.", new ChoicePromptOptions()
                {
                    Choices = ChoiceFactory.ToChoices(Colors),
                    RetryPromptActivity =
                        MessageFactory.SuggestedActions(Colors, "Please choose a color.") as Activity
                }).ConfigureAwait(false);
            },
            async(dc, args, next) =>
            {
                var color = (FoundChoice)args["Value"];

                await dc.Context.SendActivity($"You chose {color.Value}.").ConfigureAwait(false);
                await dc.End().ConfigureAwait(false);
            }
        });
    }
}
```

# [JavaScript](#tab/javascript)

```javascript
const {ChoicePrompt} = require("botbuilder-dialogs");
```

```javascript
dialogs.add('choicePrompt', new ChoicePrompt());
```

```javascript
// A choice prompt requires that you specify choice options.
const list = ['green', 'blue'];
await dc.prompt('choicePrompt', 'Please make a choice', list, {retryPrompt: 'Please choose a color.'});
```

---

## Validate a prompt response

You can validate a prompt response before returning the valid value to the next step of the **waterfall**. For example, to validate a **NumberPrompt** within a range of numbers between **6** and **20**, you can have validation logic similar to this:

# [C#](#tab/csharp)

```cs
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Recognizers.Text;
using PromptStatus = Microsoft.Bot.Builder.Prompts.PromptStatus;
```

```cs
/// <summary>Defines a dialog that asks for the number of people in a party.</summary>
public class MyDialog : DialogSet
{
    /// <summary>The ID of the main dialog in the set.</summary>
    public const string Name = "mainDialog";

    /// <summary>Defines the IDs of the prompts in the set.</summary>
    public struct Inputs
    {
        /// <summary>The ID of the party size prompt.</summary>
        public const string Size = "parytySize";
    }

    /// <summary>Defines the prompts and steps of the dialog.</summary>
    public MyDialog()
    {
        // Include a validation function for the party size prompt.
        Add(Inputs.Size, new NumberPrompt<int>(Culture.English, async (context, result) =>
        {
            if (result.Value < 6 || result.Value > 20)
            {
                result.Status = PromptStatus.OutOfRange;
            }
        }));
        Add(Name, new WaterfallStep[]
        {
            async (dc, args, next) =>
            {
                // Prompt for the party size.
                await dc.Prompt(Inputs.Size, "How many people are in your party?", new PromptOptions()
                {
                    RetryPromptString = "Please specify party size between 6 and 20."
                }).ConfigureAwait(false);
            },
            async(dc, args, next) =>
            {
                var size = (int)args["Value"];

                await dc.Context.SendActivity($"Okay, {size} people!").ConfigureAwait(false);
                await dc.End().ConfigureAwait(false);
            }
        });
    }
}
```

Validation can also be encapsulated within it's own private method, and added that way.

```cs
/// <summary>Validates input for the partySize prompt.</summary>
/// <param name="context">The context object for the current turn of the bot.</param>
/// <param name="result">The recognition result from the prompt.</param>
/// <returns>An updated recognition result.</returns>
private static async Task PartySizeValidator(ITurnContext context, Int32Result result)
{
    if (result.Value < 6 || result.Value > 20)
    {
        result.Status = PromptStatus.OutOfRange;
    }
}
```

Within your dialog, specify the method to use to validate input.

```cs
// Include a validation function for the party size prompt.
Add(Inputs.Size, new NumberPrompt<int>(Culture.English, PartySizeValidator));
```

# [JavaScript](#tab/javascript)

```javascript
// Customized prompts with validations
// A number prompt with validation for valid party size within a range.
dialogs.add('partySizePrompt', new botbuilder_dialogs.NumberPrompt( async (context, value) => {
    try {
        if(value < 6 ){
            throw new Error('Party size too small.');
        }
        else if(value > 20){
            throw new Error('Party size too big.')
        }
        else {
            return value; // Return the valid value
        }
    } catch (err) {
        await context.sendActivity(`${err.message} <br/>Please provide a valid number between 6 and 20.`);
        return undefined;
    }
}));
```

---

Likewise, if you want to validate a **DatetimePrompt** response for a date and time in the future, you can have validation logic similar to this:

# [C#](#tab/csharp)

```cs
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Recognizers.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DateTimeResult = Microsoft.Bot.Builder.Prompts.DateTimeResult;
using PromptStatus = Microsoft.Bot.Builder.Prompts.PromptStatus;
```

```cs
/// <summary>Validates input for the reservationTime prompt.</summary>
/// <param name="context">The context object for the current turn of the bot.</param>
/// <param name="result">The recognition result from the prompt.</param>
/// <returns>An updated recognition result.</returns>
private static async Task TimeValidator(ITurnContext context, DateTimeResult result)
{
    if (result.Resolution.Count == 0)
    {
        await context.SendActivity("Sorry, I did not recognize the time that you entered.").ConfigureAwait(false);
        result.Status = PromptStatus.NotRecognized;
    }

    // Find any recognized time that is not in the past.
    var now = DateTime.Now;
    DateTime time = default(DateTime);
    var resolution = result.Resolution.FirstOrDefault(
        res => DateTime.TryParse(res.Value, out time) && time > now);

    if (resolution != null)
    {
        // If found, keep only that result.
        result.Resolution.Clear();
        result.Resolution.Add(resolution);
    }
    else
    {
        // Otherwise, flag the input as out of range.
        await context.SendActivity("Please enter a time in the future, such as \"tomorrow at 9am\"").ConfigureAwait(false);
        result.Status = PromptStatus.OutOfRange;
    }
}
```

```csharp
Add(Inputs.Time, new DateTimePrompt(Culture.English, TimeValidator));
```

Further examples can be found in our [samples repo](https://github.com/Microsoft/botbuilder-dotnet).

# [JavaScript](#tab/javascript)

```JavaScript
// A date and time prompt with validation for date/time in the future.
dialogs.add('dateTimePrompt', new botbuilder_dialogs.DatetimePrompt( async (context, values) => {
    try {
        if (values.length < 0) { throw new Error('missing time') }
        if (values[0].type !== 'datetime') { throw new Error('unsupported type') }
        const value = new Date(values[0].value);
        if (value.getTime() < new Date().getTime()) { throw new Error('in the past') }
        return value;
    } catch (err) {
        await context.sendActivity(`Please enter a valid time in the future like "tomorrow at 9am".`);
        return undefined;
    }
}));
```

Further examples can be found in our [samples repo](https://github.com/Microsoft/botbuilder-js).

---

> [!TIP]
> Date time prompts can resolve to a few different dates if the user gives an ambigious answer. Depending on what you're using it for, you may want to check all the resolutions provided by the prompt result, instead of just the first.

You can use the similar techniques to validate prompt responses for any of the prompt types.

## Save user data

When you prompt for user input, you have several options on how to handle that input. For instance, you can consume and discard the input, you can save it to a global variable, you can save it to a volatile or in-memory storage container, you can save it to a file, or you can save it to an external database. For more information on how to save user data, see [Manage user data](bot-builder-howto-v4-state.md).

## Next steps

Now that you know how to prompt a user for input, lets enhance the bot code and user experience by managing various conversation flows through dialogs.

> [!div class="nextstepaction"]
> [Manage conversation flow with dialogs](bot-builder-dialog-manage-conversation-flow.md)