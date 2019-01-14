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
ms.date: 11/21/2018
monikerRange: 'azure-bot-service-4.0'
---
# Gather user input using a dialog prompt

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

Gathering information by posing questions is one of the main ways a bot interacts with users. The *dialogs* library makes it easy to ask questions, as well as validate the response to make sure it matches a specific data type or meets custom validation rules. This topic details how to create and call prompts from a waterfall dialog.

## Prerequisites

- The code in this article is based on the **DialogPromptBot** sample. You'll need a copy of the sample in either [C#](https://aka.ms/dialog-prompt-cs) or [JS](https://aka.ms/dialog-prompt-js).
- A basic understanding of the [dialogs library](bot-builder-concept-dialog.md) and how to [manage conversations](bot-builder-dialog-manage-conversation-flow.md) is required. 
- [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator) for testing.

## Using prompts

A dialog can use a prompt only if both the dialog and prompt are in the same dialog set. You can use the same prompt in multiple steps within a dialog and in multiple dialogs in the same dialog set. However, you associate custom validation with a prompt at initialization time. To use different validation for the same type of prompt, you need multiple instances of the prompt type, each with its own validation code.

### Define a state property accessor for the dialog state

# [C#](#tab/csharp)

The Dialog Prompt sample used in this article prompts the user for reservation information. To manage party size and date, we define an inner class for reservation information in the DialogPromptBot.cs file.

```csharp
public class Reservation
{
    public int Size { get; set; }

    public string Date { get; set; }
}
```

Next, we add a state property accessor for the reservation data.

```csharp
public class DialogPromptBotAccessors
{
    public DialogPromptBotAccessors(ConversationState conversationState)
    {
        ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
    }

    public static string DialogStateAccessorKey { get; } = "DialogPromptBotAccessors.DialogState";
    public static string ReservationAccessorKey { get; } = "DialogPromptBotAccessors.Reservation";

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

    // Create and register state accesssors.
    // Acessors created here are passed into the IBot-derived class on every turn.
    services.AddSingleton<BotAccessors>(sp =>
    {
        // ...

        // Create the custom state accessor.
        // State accessors enable other components to read and write individual properties of state.
        var accessors = new BotAccessors(conversationState)
        {
            DialogStateAccessor = conversationState.CreateProperty<DialogState>(DialogPromptBotAccessors.DialogStateAccessorKey),
            ReservationAccessor = conversationState.CreateProperty<DialogPromptBot.Reservation>(DialogPromptBotAccessors.ReservationAccessorKey),
        };

        return accessors;
    });
}
```

# [JavaScript](#tab/javascript)

No changes to the HTTP service code required for JavaScript, we can leave our index.js file as is.

In bot.js, we include the  `require` statements needed for the dialog prompt bot. 
```javascript
const { ActivityTypes } = require('botbuilder');
const { DialogSet, WaterfallDialog, NumberPrompt, DateTimePrompt, ChoicePrompt, DialogTurnStatus } = require('botbuilder-dialogs');
```

Add identifiers for the state property accessors, dialogs, and prompts.

```javascript
// Define identifiers for state property accessors.
const DIALOG_STATE_ACCESSOR = 'dialogStateAccessor';
const RESERVATION_ACCESSOR = 'reservationAccessor';

// Define identifiers for dialogs and prompts.
const RESERVATION_DIALOG = 'reservationDialog';
const PARTY_SIZE_PROMPT = 'partySizePrompt';
const LOCATION_PROMPT = 'locationPrompt';
const RESERVATION_DATE_PROMPT = 'reservationDatePrompt';
```

---

### Create a dialog set and prompts

In general, you create and add prompts and dialogs to your dialog set when you initialize your bot. The dialog set can then resolve the prompt's ID when the bot receives input from the user.

# [C#](#tab/csharp)

In the `DialogPromptBot` class define identifiers for dialogs, prompts, and dialog set.
```csharp
private const string ReservationDialog = "reservationDialog";
private const string PartySizePrompt = "partyPrompt";
private const string LocationPrompt = "locationPrompt";
private const string ReservationDatePrompt = "reservationDatePrompt";

private readonly DialogSet _dialogSet;
```

In the bot's constructor, create the dialog set, add the prompts, and add the reservation dialog. We include the custom validation when we create the prompts, and we will implement the validation functions later.

```csharp
// The following code creates prompts and adds them to an existing dialog set. The DialogSet contains all the dialogs that can 
// be used at runtime. The prompts also references a validation method is not shown here.

public DialogPromptBot(DialogPromptBotAccessors accessors, ILoggerFactory loggerFactory)
{
   // ...

    // Create the dialog set and add the prompts, including custom validation.
    _dialogSet = new DialogSet(_accessors.DialogStateAccessor);
    _dialogSet.Add(new NumberPrompt<int>(PartySizePrompt, PartySizeValidatorAsync));
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

```javascript
constructor(conversationState) {
    // Creates our state accessor properties.
    this.dialogStateAccessor = conversationState.createProperty(DIALOG_STATE_ACCESSOR);
    this.reservationAccessor = conversationState.createProperty(RESERVATION_ACCESSOR);
    this.conversationState = conversationState;
    // ...
    }
```

Next, create the dialog set and add the prompts, including custom validation.

```javascript
    // ...
    this.dialogSet = new DialogSet(this.dialogStateAccessor);
    this.dialogSet.add(new NumberPrompt(PARTY_SIZE_PROMPT, this.partySizeValidator));
    this.dialogSet.add(new ChoicePrompt (LOCATION_PROMPT));
    this.dialogSet.add(new DateTimePrompt(RESERVATION_DATE_PROMPT, this.dateValidator));
    // ...
```

Then define the steps of the waterfall dialog and add it to the set.

```javascript
    // ...
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

# [C#](#tab/csharp)

In the DialogPromptBot.cs file, we implement the `PromptForPartySizeAsync`, `PromptForLocationAsync`, `PromptForReservationDateAsync`, and `AcknowledgeReservationAsync` steps of the waterfall dialog.

Here we are only showing `PromptForPartySizeAsync` and `PromptForLocationAsync` that are two consecutive step delegates of a waterfall dialog.

```csharp
private async Task<DialogTurnResult> PromptForPartySizeAsync(WaterfallStepContext stepContext)
{
    // Prompt for the party size. The result of the prompt is returned to the next step of the waterfall.
    // If the input is not valid, the prompt is restarted, causing it to reprompt for input
    // and this set of steps is repeated next turn. Otherwise, the prompt ends and returns a _dialog turn result_ object 
    // to the parent dialog. Control passes to the next step of your waterfall dialog, with the result of the prompt 
    // available in the waterfall step context's _result_ property.
    return await stepContext.PromptAsync(
        PartySizePrompt,
        new PromptOptions
        {
            Prompt = MessageFactory.Text("How many people is the reservation for?"),
            RetryPrompt = MessageFactory.Text("How large is your party?"),
        },
        cancellationToken);
}

private async Task<DialogTurnResult> PromptForLocationAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    // Record the party size information in the current dialog state.
    int size = (int)stepContext.Result;
    stepContext.Values["size"] = size;

    return await stepContext.PromptAsync(
        "locationPrompt",
        new PromptOptions
        {
            Prompt = MessageFactory.Text("Please choose a location."),
            RetryPrompt = MessageFactory.Text("Sorry, please choose a location from the list."),
            Choices = ChoiceFactory.ToChoices(new List<string> { "Redmond", "Bellevue", "Seattle" }),
        },
        cancellationToken);
}
```

The example above shows how to use a choice prompt, providing all three properties. The `PromptForLocationAsync` method is used as a step in a waterfall dialog, and our dialog set contains both the waterfall dialog and a choice prompt with an ID of `locationPrompt`.

# [JavaScript](#tab/javascript)

Here, `PARTY_SIZE_PROMPT` and `LOCATION_PROMPT` are the IDs of the prompts, and `promptForPartySize` and `promptForLocation` are two consecutive step functions of a waterfall dialog.

```javascript
async promptForPartySize(stepContext) {
    // Prompt for the party size. The result of the prompt is returned to the next step of the waterfall.
    return await stepContext.prompt(
        PARTY_SIZE_PROMPT, {
            prompt: 'How many people is the reservation for?',
            retryPrompt: 'How large is your party?'
        });
}

async promptForLocation(stepContext) {
    // Prompt for location
    return await stepContext.prompt(
        LOCATION_PROMPT, 'Select a location.', ['Redmond', 'Bellevue', 'Seattle']
    );
}
```

---

The second parameter of the _prompt_ method takes a _prompt options_ object, which has the following properties.

| Property | Description |
| :--- | :--- |
| _prompt_ | The initial activity to send the user, to ask for their input. |
| _retry prompt_ | The activity to send the user if their first input did not validate. |
| _choices_ | A list of choices for the user to choose from, for use with a choice prompt. |

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
_dialogSet = new DialogSet(_accessors.DialogStateAccessor);
_dialogSet.Add(new NumberPrompt<int>(PartySizePrompt, PartySizeValidatorAsync));
_dialogSet.Add(new ChoicePrompt(LocationPrompt));
_dialogSet.Add(new DateTimePrompt(ReservationDatePrompt, DateValidatorAsync));
// ...
```

# [JavaScript](#tab/javascript)

```javascript
// ...
this.dialogSet = new DialogSet(this.dialogStateAccessor);
this.dialogSet.add(new NumberPrompt(PARTY_SIZE_PROMPT, this.partySizeValidator));
this.dialogSet.add(new ChoicePrompt (LOCATION_PROMPT));
this.dialogSet.add(new DateTimePrompt(RESERVATION_DATE_PROMPT, this.dateValidator));
// ...
```

---

**Party-size validator**

We limit reservations to parties of 6 to 20 people.

# [C#](#tab/csharp)

```csharp
private async Task<bool> PartySizeValidatorAsync(
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
    int size = promptContext.Recognized.Value;
    if (size < 6 || size > 20)
    {
        await promptContext.Context.SendActivityAsync(
            "Sorry, we can only take reservations for parties of 6 to 20.",
            cancellationToken: cancellationToken);
        return false;
    }

    return true;
}
```

# [JavaScript](#tab/javascript)

```javascript
async partySizeValidator(promptContext) {
    // Check whether the input could be recognized as an integer.
    if (!promptContext.recognized.succeeded) {
        await promptContext.context.sendActivity(
            "I'm sorry, I do not understand. Please enter the number of people in your party.");
        return false;
    }
    if (promptContext.recognized.value % 1 != 0) {
        await promptContext.context.sendActivity(
            "I'm sorry, I don't understand fractional people.");
        return false;
    }
    // Check whether the party size is appropriate.
    var size = promptContext.recognized.value;
    if (size < 6 || size > 20) {
        await promptContext.context.sendActivity(
            'Sorry, we can only take reservations for parties of 6 to 20.');
        return false;
    }

    return true;
}
```

---

**Date time validation**

In the reservation date validator, we limit reservations to an hour or more from the current time. We are keeping the first resolution that matches our criteria, and clearing the rest. The validation code below is not exhaustive, and it works best for input that parses to a date and time. It does demonstrate some of the options for validating a date-time prompt, and your implementation will depend on what information you are trying to collect from the user.

# [C#](#tab/csharp)

```csharp
// Validates whether the reservation date is appropriate.
// Reservations must be made at least an hour in advance.
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
    DateTime earliest = DateTime.Now.AddHours(1.0);
    DateTimeResolution value = promptContext.Recognized.Value.FirstOrDefault(v =>
        DateTime.TryParse(v.Value ?? v.Start, out DateTime time) && DateTime.Compare(earliest,time) <= 0);
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

### Update the turn handler

Update the bot's turn handler to start the dialog and accept a return value from the dialog when it completes. Here we assume the user is interacting with a bot, the bot has an active waterfall dialog, and the next step in the dialog uses a prompt.

1. When the user sends a message to the bot, it does the following:
   1. The bot's turn handler creates a dialog context and call its _continue_ method.
   1. Control passes to the next step in the active dialog, which in this case is your waterfall dialog.
   1. The step calls its waterfall step context's _prompt_ method to ask the user for input.
   1. The waterfall step context pushes the prompt onto the stack and starts it.
   1. The prompt sends an activity to the user to ask for their input.
1. When the user sends their next message to the bot, it does the following:
   1. The bot's turn handler creates a dialog context and call its _continue_ method.
   1. Control passes to the next step in the active dialog, which is the second turn of the prompt.
   1. The prompt validates the user's input.

**Handling prompt results**

What you do with the prompt result depends on why you requested the information from the user. Options include:

- Use the information to control the flow of your dialog, such as when the user responds to a confirm or choice prompt.
- Cache the information in the dialog's state, such as setting a value in the waterfall step context's _values_ property, and then return the collected information when the dialog ends.
- Save the information to bot state. This would require you to design your dialog to have access to the bot's state property accessors.

# [C#](#tab/csharp)

```csharp
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
{
    switch (turnContext.Activity.Type)
    {
        // On a message from the user:
        case ActivityTypes.Message:

            // Get the current reservation info from state.
            Reservation reservation = await _accessors.ReservationAccessor.GetAsync(
                turnContext, () => null, cancellationToken);

            // Generate a dialog context for our dialog set.
            DialogContext dc = await _dialogSet.CreateContextAsync(turnContext, cancellationToken);

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
                        $"We'll see you {reservation.Date}.",
                        cancellationToken: cancellationToken);
                }
            }
            else
            {
                // Continue the dialog.
                DialogTurnResult dialogTurnResult = await dc.ContinueDialogAsync(cancellationToken);

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
                        $"Your party of {reservation.Size} is confirmed for {reservation.Date}.",
                        cancellationToken: cancellationToken);
                }
            }

            // Save the updated dialog state into the conversation state.
            await _accessors.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            break;

        // Handle other incoming activity types as appropriate to your bot.
        default:
            await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
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
                        `We'll see you ${reservation.date}.`);
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
                        `confirmed for ${dialogTurnResult.result.date}.`);
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
