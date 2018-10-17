---
title: Prompt users for input using the Dialogs library | Microsoft Docs
description: Learn how to prompt users for input using the Dialogs library in the Bot Builder SDK for Node.js.
keywords: prompts, dialogs, AttachmentPrompt, ChoicePrompt, ConfirmPrompt, DatetimePrompt, NumberPrompt, TextPrompt, reprompt, validation
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 9/25/2018
monikerRange: 'azure-bot-service-4.0'
---
# Prompt users for input using the Dialogs library

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

Gathering information by posting questions is one of the main ways a bot interacts with users. It is possible to do this directly by using the [turn context](~/v4sdk/bot-builder-basics.md#defining-a-turn) object's _send activity_ method and then process the next incoming message as the response. However, the Bot Builder SDK provides a **dialogs** library that provides methods designed to make it easier to ask questions, and to make sure the response matches a specific data type or meets custom validation rules. This topic details how to achieve this using **prompts** to ask a user for input.

This article describes how to use prompts within a dialog. For information on using dialogs in general, see [using dialogs to manage simple conversation flow](bot-builder-dialog-manage-conversation-flow.md).

## Prompt types

The dialogs library offers a number of different types of prompts, each used for collecting a different type of response.

| Prompt | Description |
|:----|:----|
| **AttachmentPrompt** | Prompt the user for an attachment such as a document or image. |
| **ChoicePrompt** | Prompt the user to choose from a set of options. |
| **ConfirmPrompt** | Prompt the user to confirm their action. |
| **DatetimePrompt** | Prompt the user for a date-time. Users can respond using natural language such as "Tomorrow at 8pm" or "Friday at 10am". The Bot Framework SDK uses the LUIS `builtin.datetimeV2` prebuilt entity. For more information, see [builtin.datetimev2](https://docs.microsoft.com/azure/cognitive-services/luis/luis-reference-prebuilt-entities#builtindatetimev2). |
| **NumberPrompt** | Prompt the user for a number. The user can respond with either "10" or "ten". If the response is "ten", for example, the prompt will convert the response into a number and return `10` as a result. |
| **TextPrompt** | Prompt user for a string of text. |

## Add references to prompt library

You can get the **dialogs** library by adding the **botbuilder-dialogs** package to your bot. We cover dialogs in [using dialogs to manage simple conversation flow](bot-builder-dialog-manage-conversation-flow.md), but we'll use dialogs for our prompts.

# [C#](#tab/csharp)

Install the **Microsoft.Bot.Builder.Dialogs** package from NuGet.

Then, include reference to the library from your bot code.

```cs
using Microsoft.Bot.Builder.Dialogs;
```

You'll need to set up the conversation dialog state via accessors. We won't dive into this code much here, but more on that can be found in the [state](bot-builder-howto-v4-state.md) article.

Within your bot options in **Startup.cs**, first define your state objects, then add the singleton to provide the accessor class to the bot constructor. The class for `BotAccessor` simply stores the conversation and user state, along with accessors for each of those items. The full class definition is provided in the sample linked at the end of this article. 

```cs
    services.AddBot<MultiTurnPromptsBot>(options =>
    {
        InitCredentialProvider(options);

        // Create and add conversation state.
        var convoState = new ConversationState(dataStore);
        options.State.Add(convoState);

        // Create and add user state.
        var userState = new UserState(dataStore);
        options.State.Add(userState);
    });

    services.AddSingleton(sp =>
    {
        // We need to grab the conversationState we added on the options in the previous step
        var options = sp.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;
        if (options == null)
        {
            throw new InvalidOperationException("BotFrameworkOptions must be configured prior to setting up the State Accessors");
        }

        var conversationState = options.State.OfType<ConversationState>().FirstOrDefault();
        if (conversationState == null)
        {
            throw new InvalidOperationException("ConversationState must be defined and added before adding conversation-scoped state accessors.");
        }

        var userState = options.State.OfType<UserState>().FirstOrDefault();
        if (userState == null)
        {
            throw new InvalidOperationException("UserState must be defined and added before adding user-scoped state accessors.");
        }

        // The dialogs will need a state store accessor. Creating it here once (on-demand) allows the dependency injection
        // to hand it to our IBot class that is create per-request.
        var accessors = new BotAccessors(conversationState, userState)
        {
            ConversationDialogState = conversationState.CreateProperty<DialogState>("DialogState"),
            UserProfile = userState.CreateProperty<UserProfile>("UserProfile"),
        };

        return accessors;
    });
```

Then, in your bot code, define the following objects for the dialog set.

```cs
    private readonly BotAccessors _accessors;

    /// <summary>
    /// The <see cref="DialogSet"/> that contains all the Dialogs that can be used at runtime.
    /// </summary>
    private DialogSet _dialogs;

    /// <summary>
    /// Initializes a new instance of the <see cref="MultiTurnPromptsBot"/> class.
    /// </summary>
    /// <param name="accessors">A class containing <see cref="IStatePropertyAccessor{T}"/> used to manage state.</param>
    public MultiTurnPromptsBot(BotAccessors accessors)
    {
        _accessors = accessors ?? throw new ArgumentNullException(nameof(accessors));

        // The DialogSet needs a DialogState accessor, it will call it when it has a turn context.
        _dialogs = new DialogSet(accessors.ConversationDialogState);

        // ...
        // other constructor items
        // ...
    }
```

# [JavaScript](#tab/javascript)

Create a JavaScript bot using the Echo template. For more information, see the [JavaScript quickstart](../javascript/bot-builder-javascript-quickstart.md).

Install the dialogs package from npm:

```cmd
npm install --save botbuilder-dialogs
```

To use **dialogs** in your bot, include it in the bot code.

1. In your **bot.js** file, add the following.

    ```javascript
    // Import components from the dialogs library.
    const { DialogSet, TextPrompt, WaterfallDialog } = require("botbuilder-dialogs");

    // Name for the dialog state property accessor.
    const DIALOG_STATE_PROPERTY = 'dialogState';

    // Define the names for the prompts and dialogs for the dialog set.
    const TEXT_PROMPT = 'textPrompt';
    const MAIN_DIALOG = 'mainDialog';
    ```

    The _dialog set_ will contain the dialogs for this bot, and we'll use the _text prompt_ to ask the user for input. We'll also need a dialog state property accessor that the dialog set can use to keep track of its state.

1. update your bot's constructor code. We'll add more to this shortly.

    ```javascript
      constructor(conversationState) {
        // Track the conversation state object.
        this.conversationState = conversationState;

        // Create a state property accessor for the dialog set.
        this.dialogState = conversationState.createProperty(DIALOG_STATE_PROPERTY);
    }
    ```

---

## Prompt the user

To prompt a user for input, define a prompt using one of the built-in classes like **TextPrompt**, then add it to your dialog set and assign it a dialog ID.

Once a prompt is added, use it in a two-step waterfall dialog, A *waterfall* dialog is a way to define a sequence of steps. Multiple prompts can be chained together to create multi-step conversations. For more information, see the [using dialogs](bot-builder-dialog-manage-conversation-flow.md#using-dialogs-to-guide-the-user-through-steps) section of [manage simple conversation flow with dialogs](bot-builder-dialog-manage-conversation-flow.md).

For example, the following dialog prompts the user for their name, and then uses the response to greet them. On the first turn, the dialog prompts the user for their name. The user's response is passed as a parameter to the second step function, which processes the input and sends the personalized greeting.

# [C#](#tab/csharp)

Each prompt you use in your dialog is also given a name, used by the dialog or your bot to access the prompt. In all of these samples, we are exposing the prompt IDs as constants.

Within your bot constructor, add definitions for your two step waterfall and the prompt for the dialog to use. Here we're adding them as independent functions, but they can be defined as an inline lambda if preferred.

```csharp
 public MultiTurnPromptsBot(BotAccessors accessors)
{
    _accessors = accessors ?? throw new ArgumentNullException(nameof(accessors));

    // The DialogSet needs a DialogState accessor, it will call it when it has a turn context.
    _dialogs = new DialogSet(accessors.ConversationDialogState);

    // This array defines how the Waterfall will execute.
    var waterfallSteps = new WaterfallStep[]
    {
        NameStepAsync,
        SayHiAsync,
    };

    _dialogs.Add(new WaterfallDialog("details", waterfallSteps));
    _dialogs.Add(new TextPrompt("name"));
}
```

Then, define your two waterfall steps within your bot. For the text prompt, you're specifying the *name* ID of the `TextPrompt` you defined above. Notice the method names match those of the `WaterfallStep[]` above. Future samples here won't include that code, but know that for additional steps you need to add the method name in the correct order in that `WaterfallStep[]`.

```cs
    private static async Task<DialogTurnResult> NameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is a Prompt Dialog.
        // Running a prompt here means the next WaterfallStep will be run when the users response is received.
        return await stepContext.PromptAsync("name", new PromptOptions { Prompt = MessageFactory.Text("Please enter your name.") }, cancellationToken);
    }

    private static async Task<DialogTurnResult> SayHiAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        await stepContext.Context.SendActivityAsync($"Hi {stepContext.Result}");

        return await stepContext.EndDialogAsync(cancellationToken);
    }
```

# [JavaScript](#tab/javascript)

1. In the bot's constructor, create the dialog set and add a text prompt and a waterfall dialog to it.

    ```javascript
    // Create the dialog set, and add the prompt and the waterfall dialog.
    this.dialogs = new DialogSet(this.dialogState)
        .add(new TextPrompt(TEXT_PROMPT))
        .add(new WaterfallDialog(MAIN_DIALOG, [
            async (step) => {
                // The results of this prompt will be passed to the next step.
                return await step.prompt(TEXT_PROMPT, 'What is your name?');
            },
            async (step) => {
                // The result property contains the result from the previous step.
                const userName = step.result;
                await step.context.sendActivity(`Hi ${userName}!`);
                return await step.endDialog();
            }
        ]));
    ```

1. Update the bot's turn handler to run the dialog.

    ```javascript
    async onTurn(turnContext) {
        // See https://aka.ms/about-bot-activity-message to learn more about the message and other activity types.
        if (turnContext.activity.type === ActivityTypes.Message) {
            // Create a dialog context for the dialog set.
            const dc = await this.dialogs.createContext(turnContext);
            // Continue the dialog if it's active.
            await dc.continueDialog();
            if (!turnContext.responded) {
                // Otherwise, start the dialog.
                await dc.beginDialog(MAIN_DIALOG);
            }
        } else {
            // Send a default message for activity types that we don't handle.
            await turnContext.sendActivity(`[${turnContext.activity.type} event detected]`);
        }
        // Save state changes
        await this.conversationState.saveChanges(turnContext);
        }
    }
    ```

---

> [!NOTE]
> To start a dialog, get a dialog context, and use its _begin dialog_ method. For more information, see [use dialogs to manage simple conversation flow](./bot-builder-dialog-manage-conversation-flow.md).

## Reusable prompts

A prompt can be reused to ask different questions as long as the answers are the same type. For example, the sample code above defined a text prompt and used it to ask the user for their name. You can also use that same prompt to ask the user for another text string such as, "Where do you work?".

# [C#](#tab/csharp)

In the example, the ID for our text prompt, *name*, isn't helpful for code clarity. However, it is a good example that your prompt ID can be whatever you choose.

Now, our methods include a third step to ask where our user works.

```cs
    private static async Task<DialogTurnResult> NameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is a Prompt Dialog.
        // Running a prompt here means the next WaterfallStep will be run when the users response is received.
        return await stepContext.PromptAsync("name", new PromptOptions { Prompt = MessageFactory.Text("Please enter your name.") }, cancellationToken);
    }

    private static async Task<DialogTurnResult> WorkAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        await stepContext.Context.SendActivityAsync($"Hi {stepContext.Result}!");

        return await stepContext.PromptAsync("name", new PromptOptions { Prompt = MessageFactory.Text("Where do you work?") }, cancellationToken);
    }

    private static async Task<DialogTurnResult> SayHiAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
    {
        await stepContext.Context.SendActivityAsync($"{stepContext.Result} is a cool place!");

        return await stepContext.EndDialogAsync(cancellationToken);
    }
```

# [JavaScript](#tab/javascript)

In the bot's constructor, modify the waterfall to ask a second question.

```javascript
// Create the dialog set, and add the prompt and the waterfall dialog.
this.dialogs = new DialogSet(this.dialogState)
    .add(new TextPrompt(TEXT_PROMPT))
    .add(new WaterfallDialog(MAIN_DIALOG, [
    async (step) => {
        // Ask the user for their name.
        return await step.prompt(TEXT_PROMPT, 'What is your name?');
    },
    async (step) => {
        // Acknowledge their response and ask for their place of work.
        const userName = step.result;
        return await step.prompt(TEXT_PROMPT, `Hi ${userName}; where do you work?`);
    },
    async (step) => {
        // Acknowledge their response and exit the dialog.
        const workPlace = step.result;
        await step.context.sendActivity(`${workPlace} is a cool place!`);
        return await step.endDialog();
    }
    ]));
```

---

If you need to use multiple different prompts, give each prompt a unique *dialogId*. Each dialog or prompt added to a dialog set needs a unique ID. You can also create multiple **prompt** dialogs of the same type. For example, you could create two **TextPrompt** dialogs for the example above:

# [C#](#tab/csharp)

```cs
_dialogs.Add(new WaterfallDialog("details", waterfallSteps));
_dialogs.Add(new TextPrompt("name"));
_dialogs.Add(new TextPrompt("workplace"));
```

# [JavaScript](#tab/javascript)

For instance, you could replace this.

```javascript
.add(new TextPrompt(TEXT_PROMPT))
```

With the following.

```javascript
.add(new TextPrompt('namePrompt'))
.add(new TextPrompt('workPlacePrompt'))
```

And then update the respective waterfall steps to use those prompts by their respective names.

---

For the sake of code reusability, defining a single `TextPrompt` would work for all these prompts because they all expect a text as a response. The ability to name dialogs comes in handy when you need to apply different validation rules to the input of the prompts. Let's take a look at how you can validate prompt responses using a `NumberPrompt`.

## Specify prompt options

When you use a prompt within a dialog step, you can also provide prompt options, such as a reprompt string.

Specifying a reprompt string is useful when user input can fail to satisfy a prompt, either because it is in a format that the prompt can not parse, such as "tomorrow" for a number prompt, or the input fails a validation criteria. The number prompt can interpret a wide variety of input, such as "twelve" or "one quarter", as well as "12" and "0.25".

The locale is an optional parameter on certain prompts, like **NumberPrompt**. This can help the prompt parse input more accurately, but is not required.

# [C#](#tab/csharp)

The following code would add a number prompt to an existing dialog set, **_dialogs**.

```csharp
_dialogs.Add(new NumberPrompt<int>("age"));
```

Within a dialog step, the following code would prompt the user for input and provide a reprompt string to use if their input cannot be interpreted as a number.

```csharp
return await stepContext.PromptAsync(
    "age",
    new PromptOptions {
        Prompt = MessageFactory.Text("Please enter your age."),
        RetryPrompt = MessageFactory.Text("I didn't get that. Please enter a valid age."),
    },
    cancellationToken);
```

# [JavaScript](#tab/javascript)

Import the `NumberPrompt` class from the dialogs library.

```javascript
const { NumberPrompt } = require("botbuilder-dialogs");
```

Use the number prompt in your waterfall dialog, specifying both the initial and retry prompt strings.

```javascript
// Create the dialog set, and add the prompt and the waterfall dialog.
this.dialogs = new DialogSet(this.dialogState)
    .add(new NumberPrompt('partySize'))
    .add(new WaterfallDialog(MAIN_DIALOG, [
    async (step) => {
        // Ask the user for their party size.
        return await step.prompt('partySize', {
            prompt: 'How many people in your party?',
            retryPrompt: 'Sorry, please specify the number of people in your party.'
        });
    },
    async (step) => {
        // Acknowledge their response and exit the dialog.
        const partySize = step.result;
        await step.context.sendActivity(`That's a party of ${partySize}, thanks.`);
        return await step.endDialog();
    }
]));
```

---

The choice prompt has an additional required parameter: the list of choices available to the user.

# [C#](#tab/csharp)

When we use the **ChoicePrompt** to ask the user to choose between a set of options, we have to provide the prompt with that set of options, provided within a **PromptOptions** object. Here, we use the **ChoiceFactory** to convert a list of options into an appropriate format.

```csharp
private static async Task<DialogTurnResult> FavoriteColorAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    await stepContext.Context.SendActivityAsync($"Hi {stepContext.Result}!");

    return await stepContext.PromptAsync(
        "color",
        new PromptOptions {
            Prompt = MessageFactory.Text("What's your favorite color?"),
            Choices = ChoiceFactory.ToChoices(new List<string> { "blue", "green", "red" }),
        },
        cancellationToken);
}
```

# [JavaScript](#tab/javascript)

Import the `NumberPrompt` class from the dialogs library.

```javascript
const { ChoicePrompt } = require("botbuilder-dialogs");
```

Use the choice prompt in your waterfall dialog, specifying the available choices.

```javascript
// Create the dialog set, and add the prompt and the waterfall dialog.
const list = ['green', 'blue', 'red', 'yellow'];
this.dialogs = new DialogSet(this.dialogState)
    .add(new ChoicePrompt('choicePrompt'))
    .add(new WaterfallDialog(MAIN_DIALOG, [
    async (step) => {
        // Ask the user for their party size.
        return await step.prompt('choicePrompt', {
            prompt: 'Please choose a color:',
            retryPrompt: 'Sorry, please choose a color from the list.',
            choices: list
        });
    },
    async (step) => {
        // Acknowledge their response and exit the dialog.
        const choice = step.result;
        await step.context.sendActivity(`That's ${choice.value}, thanks.`);
        return await step.endDialog();
    }
]));
```

---

## Validate a prompt response

You can validate a prompt response before returning the value to the next step of the **waterfall**. For example, to validate a **NumberPrompt** within a range of numbers between **6** and **20**, you would include a validation function similar to this:

# [C#](#tab/csharp)

Change when the prompt is added to your dialog set to include the validator function

```cs
_dialogs.Add(new NumberPrompt<int>("partySize", PartySizeValidatorAsync));
```

Validation is then defined as its own method, indicating true or false depending on if it passed the validation. If it gets a false returned, it will reprompt the user.

```cs
private Task<bool> PartySizeValidatorAsync(PromptValidatorContext<int> promptContext, CancellationToken cancellationToken)
{
    var result = promptContext.Recognized.Value;

    if (result < 6 || result > 20)
    {
        return Task.FromResult(false);
    }

    return Task.FromResult(true);
}
```

# [JavaScript](#tab/javascript)

Add a validation method when your create the prompt.

```javascript
// Create the dialog set, and add the prompt and the waterfall dialog.
this.dialogs = new DialogSet(this.dialogState)
    .add(new NumberPrompt('partySizePrompt', async (promptContext) =>                                                 {
        // Check to make sure a value was recognized.
        if (promptContext.recognized.succeeded) {
            const value = promptContext.recognized.value;
            try {
                if (value < 6) {
                    throw new Error('Party size too small.');
                } else if (value > 20) {
                    throw new Error('Party size too big.')
                } else {
                    return true; // Indicate that this is a valid value.
                }
            } catch (err) {
                await promptContext.context.sendActivity(`${err.message} <br/>Please provide a valid number between 6 and 20.`);
                return false; // Indicate that this is invalid.
            }
        } else {
            return false;
        }
    }))
    .add(new WaterfallDialog(MAIN_DIALOG, [
        async (step) => {
            // Ask the user for their party size.
            return await step.prompt('partySizePrompt', {
                prompt: 'How large is your party?',
                retryPrompt: 'Sorry, please specify a size between 6 and 20.'
            });
        },
        async (step) => {
            // Acknowledge their response and exit the dialog.
            const size = step.result;
            await step.context.sendActivity(`That's a party of ${size}, thanks.`);
            return await step.endDialog();
        }
    ]));
```

---

Likewise, if you want to validate a **DatetimePrompt** response for a date and time in the future, you can have validation logic similar to this:

# [C#](#tab/csharp)

```cs
    private Task<bool> DateTimeValidatorAsync(PromptValidatorContext<IList<DateTimeResolution>> prompt, CancellationToken cancellationToken)
    {
        if (prompt.Recognized.Succeeded)
        {
            var resolution = prompt.Recognized.Value.First();

            // Verify that the Timex received is within the desired bounds, compared to today.
            var now = DateTime.Now;
            DateTime.TryParse(resolution.Value, out var time);

            if (time < now)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
```

```csharp
_dialogs.Add(new DateTimePrompt("date", DateTimeValidatorAsync));
```

Further examples can be found in our [samples repo](https://aka.ms/bot-samples-readme).

# [JavaScript](#tab/javascript)

```javascript
const { DateTimePrompt } = require("botbuilder-dialogs");
```

```JavaScript
// Create the dialog set, and add the prompt and the waterfall dialog.
this.dialogs = new DialogSet(this.dialogState)
    .add(new DateTimePrompt('dateTimePrompt', async (promptContext) => {
        try {
            if (!promptContext.recognized.succeeded) { throw new Error('Value not recognized.') }
            const values = promptContext.recognized.value;
            if (!Array.isArray(values) || values.length < 0) { throw new Error('Value missing.'); }
            if ((values[0].type !== 'datetime') && (values[0].type !== 'date')) { throw new Error('Unsupported type.'); }
            const now = new Date();
            const value = new Date(values[0].value);
            if (value.getTime() < now.getTime()) { throw new Error('Value in the past.') }

            // update the return value of the prompt to be a real date object
            promptContext.recognized.value = [value];
            return true; // indicate valid
        } catch (err) {
            await promptContext.context.sendActivity(`${err} Please specify a date or a date and time in the future, like tomorrow at 9am.`);
            return false; // indicate invalid
        }
    }))
    .add(new WaterfallDialog(MAIN_DIALOG, [
        async (step) => {
            // Ask the user for their party size.
            return await step.prompt('dateTimePrompt', 'When would you like to schedule that for?');
        },
        async (step) => {
            // Acknowledge their response and exit the dialog.
            const time = step.result;
            await step.context.sendActivity(`That's ${time}, thanks.`);
            return await step.endDialog();
        }
    ]));
```

Further examples can be found in our [samples repo](https://aka.ms/bot-samples-readme).

---

> [!TIP]
> Date time prompts can resolve to a few different dates if the user gives an ambiguous answer. Depending on what you're using it for, you may want to check all the resolutions provided by the prompt result, instead of just the first.

You can use the similar techniques to validate prompt responses for any of the prompt types.

## Save user data

When you prompt for user input, you have several options on how to handle that input. For instance, you can consume and discard the input, you can save it to a global variable, you can save it to a volatile or in-memory storage container, you can save it to a file, or you can save it to an external database. For more information on how to save user data, see [Manage user data](bot-builder-howto-v4-state.md).

## Additional resources

For a complete sample using some of these prompts, see the Multi Turn Prompt Bot for [C#](https://aka.ms/cs-multi-prompts-sample) or [JavaScript](https://aka.ms/js-multi-prompts-sample).

## Next steps

Now that you know how to prompt a user for input, lets enhance the bot code and user experience by managing various conversation flows through dialogs.

> [!div class="nextstepaction"]
> [Manage simple conversation flow with dialogs](bot-builder-dialog-manage-conversation-flow.md)
