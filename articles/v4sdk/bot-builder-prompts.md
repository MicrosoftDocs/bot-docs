---
title: Gather user input using a dialog prompt| Microsoft Docs
description: Learn how to prompt users for input using the Dialogs library in the Bot Framework SDK.
keywords: prompts, prompt, user input, dialogs, AttachmentPrompt, ChoicePrompt, ConfirmPrompt, DatetimePrompt, NumberPrompt, TextPrompt, reprompt, validation
author: JonathanFingold
ms.author: v-jofing
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 02/19/2019
monikerRange: 'azure-bot-service-4.0'
---
# Gather user input using a dialog prompt

[!INCLUDE[applies-to](../includes/applies-to.md)]

Gathering information by posing questions is one of the main ways a bot interacts with users. The *dialogs* library makes it easy to ask questions, as well as validate the response to make sure it matches a specific data type or meets custom validation rules. This topic details how to create and call prompts from a waterfall dialog.

## Prerequisites

- The code in this article is based on the **DialogPromptBot** sample. You'll need a copy of either the [C# sample](https://aka.ms/dialog-prompt-cs) or [JS sample](https://aka.ms/dialog-prompt-js).
- A basic understanding of the [dialogs library](bot-builder-concept-dialog.md) and how to [manage conversations](bot-builder-dialog-manage-conversation-flow.md) is required.
- The [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator) for testing.

## Using prompts

A dialog can use a prompt only if both the dialog and prompt are in the same dialog set. You can use the same prompt in multiple steps within a dialog and in multiple dialogs in the same dialog set. However, you associate custom validation with a prompt at initialization time. To use different validation for the same type of prompt, you need multiple instances of the prompt type, each with its own validation code.

### Define a state property accessor for the dialog state

# [C#](#tab/csharp)

The Dialog Prompt sample used in this article prompts the user for reservation information. To manage party size and date, we define an inner class for reservation information in the DialogPromptBot.cs file.

```csharp
public class Reservation
{
    public int Size { get; set; }

    public string Location { get; set; }

    public string Date { get; set; }
}
```

Next, we add a state property accessor for the reservation data.

```csharp
public class DialogPromptBotAccessors
{
    public DialogPromptBotAccessors(ConversationState conversationState)
    {
        ConversationState = conversationState
            ?? throw new ArgumentNullException(nameof(conversationState));
    }

    public static string DialogStateAccessorKey { get; }
        = "DialogPromptBotAccessors.DialogState";
    public static string ReservationAccessorKey { get; }
        = "DialogPromptBotAccessors.Reservation";

    public IStatePropertyAccessor<DialogState> DialogStateAccessor { get; set; }
    public IStatePropertyAccessor<DialogPromptBot.Reservation> ReservationAccessor { get; set; }

    public ConversationState ConversationState { get; }
}
```

In Startup.cs, we update `ConfigureServices` method to set the accessors.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // ...

    IStorage dataStore = new MemoryStorage();
    var conversationState = new ConversationState(dataStore);

    // Create and register state accesssors.
    services.AddSingleton<DialogPromptBotAccessors>(sp =>
    {
        // State accessors enable other components to read and write individual properties of state.
        var accessors = new DialogPromptBotAccessors(conversationState)
        {
            DialogStateAccessor =
                conversationState.CreateProperty<DialogState>(
                    DialogPromptBotAccessors.DialogStateAccessorKey),
            ReservationAccessor =
                conversationState.CreateProperty<DialogPromptBot.Reservation>(
                    DialogPromptBotAccessors.ReservationAccessorKey),
        };

        return accessors;
    });

    // ...
}
```

# [JavaScript](#tab/javascript)

No changes to the HTTP service code required for JavaScript, we can leave our index.js file as is.

In bot.js, we include the  `require` statements needed for the dialog prompt bot.

```javascript
const { ActivityTypes } = require('botbuilder');
const { DialogSet, WaterfallDialog, NumberPrompt, DateTimePrompt, ChoicePrompt, DialogTurnStatus }
    = require('botbuilder-dialogs');
```

Add identifiers for the state property accessors, dialogs, and prompts.

```javascript
// Define identifiers for our state property accessors.
const DIALOG_STATE_ACCESSOR = 'dialogStateAccessor';
const RESERVATION_ACCESSOR = 'reservationAccessor';

// Define identifiers for our dialogs and prompts.
const RESERVATION_DIALOG = 'reservationDialog';
const SIZE_RANGE_PROMPT = 'rangePrompt';
const LOCATION_PROMPT = 'locationPrompt';
const RESERVATION_DATE_PROMPT = 'reservationDatePrompt';
```

---

### Create a dialog set and prompts

In general, you create and add prompts and dialogs to your dialog set when you initialize your bot. The dialog set can then resolve the prompt's ID when the bot receives input from the user.

# [C#](#tab/csharp)

In the `DialogPromptBot` class, define identifiers for dialogs, prompts, and values to be tracked within the dialog.

```csharp
// Define identifiers for our dialogs and prompts.
private const string ReservationDialog = "reservationDialog";
private const string PartySizePrompt = "partySizePrompt";
private const string SizeRangePrompt = "sizeRangePrompt";
private const string LocationPrompt = "locationPrompt";
private const string ReservationDatePrompt = "reservationDatePrompt";

// Define keys for tracked values within the dialog.
private const string LocationKey = "location";
private const string PartySizeKey = "partySize";
```

In the bot's constructor, create the dialog set, add the prompts, and add the reservation dialog. We include the custom validation when we create the prompts, and we will implement the validation functions later.

```csharp
private readonly DialogSet _dialogSet;
private readonly DialogPromptBotAccessors _accessors;

// ...

// Initializes a new instance of the <see cref="DialogPromptBot"/> class.
public DialogPromptBot(DialogPromptBotAccessors accessors, ILoggerFactory loggerFactory)
{
    // ...

    _accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));

    // Create the dialog set and add the prompts, including custom validation.
    _dialogSet = new DialogSet(_accessors.DialogStateAccessor);

    _dialogSet.Add(new NumberPrompt<int>(PartySizePrompt, PartySizeValidatorAsync));
    _dialogSet.Add(new NumberPrompt<int>(SizeRangePrompt, RangeValidatorAsync));
    _dialogSet.Add(new ChoicePrompt(LocationPrompt));
    _dialogSet.Add(new DateTimePrompt(ReservationDatePrompt, DateValidatorAsync));

    // Define the steps of the waterfall dialog and add it to the set.
    WaterfallStep[] steps = new WaterfallStep[]
    {
        PromptForPartySizeAsync,
        PromptForLocationAsync,
        PromptForReservationDateAsync,
        AcknowledgeReservationAsync,
    };

    _dialogSet.Add(new WaterfallDialog(ReservationDialog, steps));
}
```

# [JavaScript](#tab/javascript)

In the constructor, create state accessor properties.
Next, create the dialog set and add the prompts, including custom validation.
Then define the steps of the waterfall dialog and add it to the set.

```javascript
constructor(conversationState) {
    // Creates our state accessor properties.
    // See https://aka.ms/about-bot-state-accessors to learn more about the bot state and state accessors.
    this.dialogStateAccessor = conversationState.createProperty(DIALOG_STATE_ACCESSOR);
    this.reservationAccessor = conversationState.createProperty(RESERVATION_ACCESSOR);
    this.conversationState = conversationState;

    // Create the dialog set and add the prompts, including custom validation.
    this.dialogSet = new DialogSet(this.dialogStateAccessor);
    this.dialogSet.add(new NumberPrompt(SIZE_RANGE_PROMPT, this.rangeValidator));
    this.dialogSet.add(new ChoicePrompt(LOCATION_PROMPT));
    this.dialogSet.add(new DateTimePrompt(RESERVATION_DATE_PROMPT, this.dateValidator));

    // Define the steps of the waterfall dialog and add it to the set.
    this.dialogSet.add(new WaterfallDialog(RESERVATION_DIALOG, [
        this.promptForPartySize.bind(this),
        this.promptForLocation.bind(this),
        this.promptForReservationDate.bind(this),
        this.acknowledgeReservation.bind(this),
    ]));
}
```

---

### Implement dialog steps

In the main bot file, we implement each of our steps of the waterfall dialog. After a prompt is added, we call it in one step of a waterfall dialog, and get the prompt result in the following dialog step. To call a prompt from within a waterfall step, call the _waterfall step context_ object's _prompt_ method. The first parameter is the ID of the prompt to use, and the second parameter contains the options for the prompt, such as the text used to ask the user for input.

These methods show:

- How to call a prompt from a waterfall step, including how to pass _prompt options_.
- How to provide additional parameters to a custom validator using the _validations_ property.
- How to provide the choices for a choice prompt using the _choices_ property.

# [C#](#tab/csharp)

In the **DialogPromptBot.cs** file, we implement the steps of the waterfall dialog.

Here we show the first two steps of the waterfall, `PromptForPartySizeAsync` and `PromptForLocationAsync`.

```csharp
// First step of the main dialog: prompt for party size.
private async Task<DialogTurnResult> PromptForPartySizeAsync(
    WaterfallStepContext stepContext,
    CancellationToken cancellationToken = default(CancellationToken))
{
    // Prompt for the party size. The result of the prompt is returned to the next step of the waterfall.
    return await stepContext.PromptAsync(
        SizeRangePrompt,
        new PromptOptions
        {
            Prompt = MessageFactory.Text("How many people is the reservation for?"),
            RetryPrompt = MessageFactory.Text("How large is your party?"),
            Validations = new Range { Min = 3, Max = 8 },
        },
        cancellationToken);
}

// Second step of the main dialog: prompt for location.
private async Task<DialogTurnResult> PromptForLocationAsync(
    WaterfallStepContext stepContext,
    CancellationToken cancellationToken)
{
    // Record the party size information in the current dialog state.
    var size = (int)stepContext.Result;
    stepContext.Values[PartySizeKey] = size;

    // Prompt for the location.
    return await stepContext.PromptAsync(
        LocationPrompt,
        new PromptOptions
        {
            Prompt = MessageFactory.Text("Please choose a location."),
            RetryPrompt = MessageFactory.Text("Sorry, please choose a location from the list."),
            Choices = ChoiceFactory.ToChoices(new List<string> { "Redmond", "Bellevue", "Seattle" }),
        },
        cancellationToken);
}
```

# [JavaScript](#tab/javascript)

In the **bot.js** file, we implement the steps of the waterfall dialog.

Here we show the first two steps of the waterfall, `promptForPartySize` and `promptForLocation`.

```javascript
async promptForPartySize(stepContext) {
    // Prompt for the party size. The result of the prompt is returned to the next step of the waterfall.
    return await stepContext.prompt(
        SIZE_RANGE_PROMPT, {
            prompt: 'How many people is the reservation for?',
            retryPrompt: 'How large is your party?',
            validations: { min: 3, max: 8 },
        });
}

async promptForLocation(stepContext) {
    // Record the party size information in the current dialog state.
    stepContext.values.size = stepContext.result;

    // Prompt for location.
    return await stepContext.prompt(LOCATION_PROMPT, {
        prompt: 'Please choose a location.',
        retryPrompt: 'Sorry, please choose a location from the list.',
        choices: ['Redmond', 'Bellevue', 'Seattle'],
    });
}
```

---

The second parameter of the _prompt_ method takes a _prompt options_ object, which has the following properties.

| Property | Description |
| :--- | :--- |
| _prompt_ | The initial activity to send the user, to ask for their input. |
| _retry prompt_ | The activity to send the user if their first input did not validate. |
| _choices_ | A list of choices for the user to choose from, for use with a choice prompt. |
| _validations_ | Additional parameters to use with a custom validator. |

In general, the prompt and retry prompt properties are activities, though there is some variation on how this is handled in different programming languages.

You should always specify the initial prompt activity to send the user.

Specifying a retry prompt is useful for when the user's input fails to validate, either because it is in a format that the prompt can not parse, such as "tomorrow" for a number prompt, or the input fails a validation criteria. In this case, if no retry prompt was provided, the prompt will use the initial prompt activity to re-prompt the user for input.

For a choice prompt, you should always provide the list of available choices.

## Custom validation

You can validate a prompt response before returning the value to the next step of the **waterfall**. A validator function has a _prompt validator context_ parameter and returns a Boolean, indicating whether the input passes validation.

The prompt validator context includes the following properties:

| Property | Description |
| :--- | :--- |
| _Context_ | The current turn context for the bot. |
| _Recognized_ | A _prompt recognizer result_ that contains information about the user input, as processed by the recognizer. |
| _Options_ | Contains the _prompt options_ that were provided in the call to start the prompt. |

The prompt recognizer result has the following properties:

| Property | Description |
| :--- | :--- |
| _Succeeded_ | Indicates whether the recognizer was able to parse the input. |
| _Value_ | The return value from the recognizer. If necessary, the validation code can modify this value. |

### Implement validation code

You associate custom validation with a prompt at initialization time, in the bot's constructor.

# [C#](#tab/csharp)

```csharp
// ...
_dialogSet.Add(new NumberPrompt<int>(SizeRangePrompt, RangeValidatorAsync));
// ...
_dialogSet.Add(new DateTimePrompt(ReservationDatePrompt, DateValidatorAsync));
// ...
```

# [JavaScript](#tab/javascript)

```javascript
// ...
this.dialogSet.add(new NumberPrompt(SIZE_RANGE_PROMPT, this.rangeValidator));
// ...
this.dialogSet.add(new DateTimePrompt(RESERVATION_DATE_PROMPT, this.dateValidator));
// ...
```

---

**Party-size validator**

We limit the size of parties that can make a reservation. The valid range is defined by the _validations_ property that we used to call the party size prompt.

# [C#](#tab/csharp)

```csharp
// Validates whether the party size is appropriate to make a reservation.
private async Task<bool> RangeValidatorAsync(
    PromptValidatorContext<int> promptContext,
    CancellationToken cancellationToken)
{
    // Check whether the input could be recognized as an integer.
    if (!promptContext.Recognized.Succeeded)
    {
        await promptContext.Context.SendActivityAsync(
            "I'm sorry, I do not understand. Please enter the number of people in your party.",
            cancellationToken: cancellationToken);
        return false;
    }

    // Check whether the party size is appropriate.
    var size = promptContext.Recognized.Value;
    var validRange = promptContext.Options.Validations as Range;
    if (size < validRange.Min || size > validRange.Max)
    {
        await promptContext.Context.SendActivitiesAsync(
            new Activity[]
            {
                MessageFactory.Text($"Sorry, we can only take reservations for parties " +
                    $"of {validRange.Min} to {validRange.Max}."),
                promptContext.Options.RetryPrompt,
            },
            cancellationToken: cancellationToken);
        return false;
    }

    return true;
}
```

# [JavaScript](#tab/javascript)

```javascript
async rangeValidator(promptContext) {
    // Check whether the input could be recognized as an integer.
    if (!promptContext.recognized.succeeded) {
        await promptContext.context.sendActivity(
            "I'm sorry, I do not understand. Please enter the number of people in your party.");
        return false;
    }
    else if (promptContext.recognized.value % 1 != 0) {
        await promptContext.context.sendActivity(
            "I'm sorry, I don't understand fractional people.");
        return false;
    }

    // Check whether the party size is appropriate.
    var size = promptContext.recognized.value;
    if (size < promptContext.options.validations.min
        || size > promptContext.options.validations.max) {
        await promptContext.context.sendActivity(
            'Sorry, we can only take reservations for parties of '
            + `${promptContext.options.validations.min} to `
            + `${promptContext.options.validations.max}.`);
        await promptContext.context.sendActivity(promptContext.options.retryPrompt);
        return false;
    }

    return true;
}
```

---

**Date time validation**

In the reservation date validator, we limit reservations to an hour or more from the current time. We are keeping the first resolution that matches our criteria, and clearing the rest.

This validation code is not exhaustive, and it works best for input that parses to a date and time. It does demonstrate some of the options for validating a date-time prompt, and your implementation will depend on what information you are trying to collect from the user.

# [C#](#tab/csharp)

```csharp
// Validates whether the reservation date is appropriate.
private async Task<bool> DateValidatorAsync(
    PromptValidatorContext<IList<DateTimeResolution>> promptContext,
    CancellationToken cancellationToken = default(CancellationToken))
{
    // Check whether the input could be recognized as an integer.
    if (!promptContext.Recognized.Succeeded)
    {
        await promptContext.Context.SendActivityAsync(
            "I'm sorry, I do not understand. Please enter the date or time for your reservation.",
            cancellationToken: cancellationToken);

        return false;
    }

    // Check whether any of the recognized date-times are appropriate,
    // and if so, return the first appropriate date-time.
    var earliest = DateTime.Now.AddHours(1.0);
    var value = promptContext.Recognized.Value.FirstOrDefault(v =>
        DateTime.TryParse(v.Value ?? v.Start, out var time) && DateTime.Compare(earliest, time) <= 0);

    if (value != null)
    {
        promptContext.Recognized.Value.Clear();
        promptContext.Recognized.Value.Add(value);
        return true;
    }

    await promptContext.Context.SendActivityAsync(
            "I'm sorry, we can't take reservations earlier than an hour from now.",
            cancellationToken: cancellationToken);

    return false;
}
```

# [JavaScript](#tab/javascript)

```javascript
async dateValidator(promptContext) {
    // Check whether the input could be recognized as an integer.
    if (!promptContext.recognized.succeeded) {
        await promptContext.context.sendActivity(
            "I'm sorry, I do not understand. Please enter the date or time for your reservation.");
        return false;
    }

    // Check whether any of the recognized date-times are appropriate,
    // and if so, return the first appropriate date-time.
    const earliest = Date.now() + (60 * 60 * 1000);
    let value = null;
    promptContext.recognized.value.forEach(candidate => {
        // TODO: update validation to account for time vs date vs date-time vs range.
        const time = new Date(candidate.value || candidate.start);
        if (earliest < time.getTime()) {
            value = candidate;
        }
    });
    if (value) {
        promptContext.recognized.value = [value];
        return true;
    }

    await promptContext.context.sendActivity(
        "I'm sorry, we can't take reservations earlier than an hour from now.");
    return false;
}
```

---

The date-time prompt returns a list or array of the possible _date-time resolutions_ that match the user input. For example, 9:00 could mean 9 AM or 9 PM, and Sunday is also ambiguous. In addition, a date-time resolution can represent a date, a time, a date-time, or a range. The date-time prompt uses the [Microsoft/Recognizers-Text](https://github.com/Microsoft/Recognizers-Text) to parse the user input.

## Update the turn handler

Update the bot's turn handler to start the dialog and accept a return value from the dialog when it completes. Here we assume the user is interacting with a bot, the bot has an active waterfall dialog, and the next step in the dialog uses a prompt.

When the user sends a message to the bot, it does the following:

1. The bot retrieves state information.
1. The bot creates a dialog context
    - If there's no active dialog, it starts the dialog unless the user already made a reservation.
    - If there is an active dialog, the bot continues it. If the dialog ends the reservation details are recorded to the state cache.
1. The bot saves any changes to state.

When a step in dialog calls the step context's _prompt_ method:

1. A new instance of the prompt is created, put on the dialog stack, and started. (The main dialog waits for the prompt to end before continuing.)
1. The prompt sends an activity to the user, asking them for input.

When input is sent to the prompt:

1. The prompt attempts to process the input, according to the type of prompt, such as a number prompt or a choice prompt, and so on.
1. If the prompt includes custom validation, the the custom validation code is run.
1. If the input passes all validation, the prompt ends, returning the processed input; otherwise, the prompt starts itself over again.

**Handling prompt results**

What you do with the prompt result depends on why you requested the information from the user. Options include:

- Use the information to control the flow of your dialog, such as when the user responds to a confirm or choice prompt.
- Cache the information in the dialog's state, such as setting a value in the waterfall step context's _values_ property, and then return the collected information when the dialog ends.
- Save the information to bot state. This would require you to design your dialog to have access to the bot's state property accessors.

# [C#](#tab/csharp)

```csharp
public async Task OnTurnAsync(
    ITurnContext turnContext,
    CancellationToken cancellationToken = default(CancellationToken))
{
    switch (turnContext.Activity.Type)
    {
        // On a message from the user:
        case ActivityTypes.Message:

            // Get the current reservation info from state.
            var reservation = await _accessors.ReservationAccessor.GetAsync(
                turnContext,
                () => null,
                cancellationToken);

            // Generate a dialog context for our dialog set.
            var dc = await _dialogSet.CreateContextAsync(turnContext, cancellationToken);

            if (dc.ActiveDialog is null)
            {
                // If there is no active dialog, check whether we have a reservation yet.
                if (reservation is null)
                {
                    // If not, start the dialog.
                    await dc.BeginDialogAsync(ReservationDialog, null, cancellationToken);
                }
                else
                {
                    // Otherwise, send a status message.
                    await turnContext.SendActivityAsync(
                        $"We'll see you on {reservation.Date}.",
                        cancellationToken: cancellationToken);
                }
            }
            else
            {
                // Continue the dialog.
                var dialogTurnResult = await dc.ContinueDialogAsync(cancellationToken);

                // If the dialog completed this turn, record the reservation info.
                if (dialogTurnResult.Status is DialogTurnStatus.Complete)
                {
                    reservation = (Reservation)dialogTurnResult.Result;
                    await _accessors.ReservationAccessor.SetAsync(
                        turnContext,
                        reservation,
                        cancellationToken);

                    // Send a confirmation message to the user.
                    await turnContext.SendActivityAsync(
                        $"Your party of {reservation.Size} is confirmed for " +
                        $"{reservation.Date} in {reservation.Location}.",
                        cancellationToken: cancellationToken);
                }
            }

            // Save the updated dialog state into the conversation state.
            await _accessors.ConversationState.SaveChangesAsync(
                turnContext, false, cancellationToken);
            break;
    }
}
```

# [JavaScript](#tab/javascript)

```javascript
async onTurn(turnContext) {
    switch (turnContext.activity.type) {
        case ActivityTypes.Message:
            // Get the current reservation info from state.
            const reservation = await this.reservationAccessor.get(turnContext, null);

            // Generate a dialog context for our dialog set.
            const dc = await this.dialogSet.createContext(turnContext);

            if (!dc.activeDialog) {
                // If there is no active dialog, check whether we have a reservation yet.
                if (!reservation) {
                    // If not, start the dialog.
                    await dc.beginDialog(RESERVATION_DIALOG);
                }
                else {
                    // Otherwise, send a status message.
                    await turnContext.sendActivity(
                        `We'll see you on ${reservation.date}.`);
                }
            }
            else {
                // Continue the dialog.
                const dialogTurnResult = await dc.continueDialog();

                // If the dialog completed this turn, record the reservation info.
                if (dialogTurnResult.status === DialogTurnStatus.complete) {
                    await this.reservationAccessor.set(
                        turnContext,
                        dialogTurnResult.result);

                    // Send a confirmation message to the user.
                    await turnContext.sendActivity(
                        `Your party of ${dialogTurnResult.result.size} is ` +
                        `confirmed for ${dialogTurnResult.result.date} in ` +
                        `${dialogTurnResult.result.location}.`);
                }
            }

            // Save the updated dialog state into the conversation state.
            await this.conversationState.saveChanges(turnContext, false);
            break;
        case ActivityTypes.EndOfConversation:
        case ActivityTypes.DeleteUserData:
            break;
        default:
            break;
    }
}
```

---

You can use the similar techniques to validate prompt responses for any of the prompt types.

## Test your bot

1. Run the sample locally on your machine. If you need instructions, refer to the README file for [C#](https://aka.ms/dialog-prompt-cs) or [JS](https://aka.ms/dialog-prompt-js).
2. Start the emulator, send messages as shown below to test the bot.

![test dialog prompt sample](~/media/emulator-v4/test-dialog-prompt.png)

## Additional resources

To call a prompt directly from your turn handler, see the _prompt-validations_ sample in [C#](https://aka.ms/cs-prompt-validation-sample) or [JS](https://aka.ms/js-prompt-validation-sample).

The dialog library also includes an _OAuth prompt_ for obtaining an _OAuth token_ with which to access another application on behalf of the user. For more about authentication, see how to [add authentication](bot-builder-authentication.md) to your bot.

## Next steps

> [!div class="nextstepaction"]
> [Create advance conversation flow using branches and loops](bot-builder-dialog-manage-complex-conversation-flow.md)
