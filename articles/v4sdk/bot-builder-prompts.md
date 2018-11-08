---
title: Use dialog library to gather user input | Microsoft Docs
description: Learn how to prompt users for input using the Dialogs library in the Bot Builder SDK.
keywords: prompts, dialogs, AttachmentPrompt, ChoicePrompt, ConfirmPrompt, DatetimePrompt, NumberPrompt, TextPrompt, reprompt, validation
author: JonathanFingold
ms.author: v-jofing
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 11/02/2018
monikerRange: 'azure-bot-service-4.0'
---
# Use dialog library to gather user input

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

Gathering information by posting questions is one of the main ways a bot interacts with users. It is possible to do this directly by using the [turn context](~/v4sdk/bot-builder-basics.md#defining-a-turn) object's _send activity_ method and then process the next incoming message as the response. However, the Bot Builder SDK provides a [dialogs library](bot-builder-concept-dialog.md) that provides methods designed to make it easier to ask questions, and to make sure the response matches a specific data type or meets custom validation rules. This topic details how to achieve this using prompt objects to ask a user for input.

This article describes how to create prompts and call them from within a dialog.
For how to prompt for input without using dialogs, see [prompt the user for input using your own prompts](bot-builder-primitive-prompts.md).
For how to use dialogs in general, see [use dialogs to manage simple conversation flow](bot-builder-dialog-manage-conversation-flow.md).

## Prompt types

Behind the scenes, prompts are a two-step dialog. First, the prompt asks for input; second, it returns the valid value, or restarts from the top with a re-prompt.

The dialogs library offers a number of basic prompts, each used for collecting a different type of response.

| Prompt | Description | Returns |
|:----|:----|:----|
| _Attachment prompt_ | Asks for one or more attachments, such as a document or image. | A collection of _attachment_ objects. |
| _Choice prompt_ | Asks for a choice from a set of options. | A _found choice_ object. |
| _Confirm prompt_ | Asks for a confirmation. | A Boolean value. |
| _Date-time prompt_ | Asks for a date-time. | A collection of _date-time resolution_ objects. |
| _Number prompt_ | Asks for a number. | A numeric value. |
| _Text prompt_ | Asks for general text input. | A string. |

The library also includes an _OAuth prompt_ for obtaining an _OAuth token_ with which to access another application on behalf of the user. For more about authentication, see how to [add authentication to your bot](bot-builder-authentication.md).

The basic prompts can interpret natural language input, such as "ten" or "a dozen" for a number, or "tomorrow" or "Friday at 10am" for a date-time.

## Using prompts

A dialog can use a prompt only if both the dialog and prompt are in the same dialog set.

1. Define a state property accessor for your dialog state.
1. Create a dialog set.
1. Create your prompts, and add them to the dialog set.
1. Create a dialog that will use your prompts, and add it to the dialog set.
1. Within the dialog, add calls to the prompts and to retrieve the prompt results.

This article discusses how to create your prompts and how to call them from a waterfall dialog.
For more information about dialogs in general, see the [dialogs library](bot-builder-concept-dialog.md) article.
For a discussion of a complete bot that uses dialogs and prompts, see how to [use dialogs to manage simple conversation flow](bot-builder-dialog-manage-conversation-flow.md).

You can use the same prompt in multiple steps within a dialog and in multiple dialogs in the same dialog set.
However, you associate custom validation with a prompt at initialization time.
So if you need different validation for the same type of prompt, you need multiple instances of the prompt type, each with its own validation code.

### Create a prompt

To prompt a user for input, define a prompt using one of the built-in classes, such as the _text prompt_, and add it to your dialog set.

* The prompt has a fixed ID. (Identifiers must be unique within a dialog set.)
* The prompt can have a custom validator. (See [custom validation](#custom-validation).)
* For some prompts, you can specify a _default locale_.

In general, create and add prompts and dialogs to your dialog set when you initialize your bot. The dialog set can then resolve the prompt's ID when the bot receives input from the user.

For example, the following code creates a two text prompts and adds them to an existing dialog set. The second text prompt references a validation method that is not shown here.

# [C#](#tab/csharp)

Here, `_dialogs` contains an existing dialog set, and `NameValidator` is a validation method.

```csharp
_dialogs.Add(new TextPrompt("nickNamePrompt"));
_dialogs.Add(new TextPrompt("namePrompt", NameValidator));
```

# [JavaScript](#tab/javascript)

Here, `this.dialogs` contains an existing dialog set, and `NameValidator` is a validation function.

```javascript
this.dialogs.add(new TextPrompt('nickNamePrompt'));
this.dialogs.add(new TextPrompt('namePrompt', NameValidator));
```

---

#### Locales

The locale is used to determine language-specific behavior of the **choice**, **confirm**, **date-time**, and **number** prompts. For any given input from the user:

* If the channel provided a _locale_ property in user's message, then that is used.
* Otherwise, if the prompt's _default locale_ is set, by providing it when calling the prompt's constructor or by setting it later, then that is used.
* Otherwise, English ("en-us") is used as the locale.

> [!NOTE]
> The locale is a 2, 3, or 4 character ISO 639 code that represents a language or language family.

### Call a prompt from a waterfall dialog

Once a prompt is added, call it in one step of a waterfall dialog, and get the prompt result in the following dialog step.
To call a prompt from within a waterfall step, call the _waterfall step context_ object's _prompt_ method. The first parameter is the ID of the prompt to use, and the second parameter contains the options for the prompt, such as the text used to ask the user for input.

Assume the user is interacting with a bot, the bot has an active waterfall dialog, and the next step in the dialog uses a prompt.

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
      * If their input is not valid, the prompt is restarted, causing it to reprompt for input, and this set of steps is repeated next turn.
      * Otherwise, the prompt ends and returns a _dialog turn result_ object to the parent dialog. Control passes to the next step of your waterfall dialog, with the result of the prompt available in the waterfall step context's _result_ property.

<!--
> [!NOTE]
> A waterfall step delegate takes a _waterfall step context_ parameter and returns a _dialog turn result_.
> A prompt's result is contained within the prompt's return value (a dialog turn result object) when it ends.
> The waterfall dialog provides the return value in the waterfall step context parameter when it calls the next waterfall step.
-->

When a prompt returns, the waterfall step context's _result_ property is set to the return value of the prompt.

This example shows parts of two consecutive waterfall steps. The first uses the prompt to ask the user for their name. The second gets the return value of the prompt.

# [C#](#tab/csharp)

Here, `name` is the ID of a text prompt, and `NameStepAsync` and `GreetingStepAsync` are two consecutive step delegates of a waterfall dialog.

```csharp
private static async Task<DialogTurnResult> NameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    // ...

    // Prompt for the user's name.
    return await stepContext.PromptAsync(
        "name",
         new PromptOptions { Prompt = MessageFactory.Text("Please enter your name.") },
         cancellationToken);
}

private static async Task<DialogTurnResult> GreetingStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    // Get the user's name from the prompt result.
    string name = (string)stepContext.Result;
    await stepContext.Context.SendActivityAsync(
        MessageFactory.Text($"Pleased to meet you, {name}."),
         cancellationToken);

    // ...
}
```

# [JavaScript](#tab/javascript)

Here, `name` is the ID of a text prompt, and `nameStep` and `greetingStep` are two consecutive step functions of a waterfall dialog.

```javascript
async nameStep(step) {
    // ...

    return await step.prompt('name', 'Please enter your name.');
}

async greetingStep(step) {
    // Get the user's name from the prompt result.
    const name = step.result;
    await step.context.sendActivity(`Pleased to meet you, ${name}.`);

    // ...
}
```

---

### Call a prompt from the bot's turn handler

It is possible to call a prompt directly from your turn handler, by using the the dialog context's _prompt_ method.
You would need to call the dialog context's _continue dialog_ method on the next turn and review its return value, a _dialog turn result_ object. For an example of how to do this, see the prompt validations sample ([C#](https://aka.ms/cs-prompt-validation-sample) | [JS](https://aka.ms/js-prompt-validation-sample)), or see how to [prompt the user for input using your own prompts](bot-builder-primitive-prompts.md) for a alternative approach.

## Prompt options

The second parameter of the _prompt_ method takes a _prompt options_ object, which has the following properties.

| Property | Description |
| :--- | :--- |
| _prompt_ | The initial activity to send the user, to ask for their input. |
| _retry prompt_ | The activity to send the user if their first input did not validate. |
| _choices_ | A list of choices for the user to choose from, for use with a choice prompt. |

In general, the prompt and retry prompt properties are activities, though there is some variation on how this is handled in different programming languages.

You should always specify the initial prompt activity to send the user.

Specifying a retry prompt is useful when the user's input can fail to validate, either because it is in a format that the prompt can not parse, such as "tomorrow" for a number prompt, or the input fails a validation criteria. In this case, if no retry prompt was provided, the prompt will use the initial prompt activity to re-prompt the user for input.

For a choice prompt, you should always provide the list of available choices.

This example shows how to use a choice prompt, providing all three properties. The _favorite color_ method is used as a step in a waterfall dialog, and our dialog set contains both the waterfall dialog and a choice prompt with an ID of `colorChoice`.

# [C#](#tab/csharp)

```csharp
private static async Task<DialogTurnResult> FavoriteColorAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    // ...

    return await stepContext.PromptAsync(
        "colorChoice",
        new PromptOptions {
            Prompt = MessageFactory.Text("Please choose a color."),
            RetryPrompt = MessageFactory.Text("Sorry, please choose a color from the list."),
            Choices = ChoiceFactory.ToChoices(new List<string> { "blue", "green", "red" }),
        },
        cancellationToken);
}
```

# [JavaScript](#tab/javascript)

In the JavaScript SDK, you can provide a string for both the `prompt` and `retryPrompt` properties. The prompt converts these to message activities for you.

```javascript
async favoriteColor(step) {
    // ...

    return await step.prompt('colorChoice', {
        prompt: 'Please choose a color:',
        retryPrompt: 'Sorry, please choose a color from the list.',
        choices: [ 'red', 'green', 'blue' ]
    });
}
```

---

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

### Setup

We need to do a little setup before adding our validation code.

# [C#](#tab/csharp)

In your bot's **.cs** file, define an inner class for reservation information.

```csharp
public class Reservation
{
    public int Size { get; set; }

    public string Date { get; set; }
}
```

In **BotAccessors.cs**, add a state property accessor for the reservation data.

```csharp
public class BotAccessors
{
    public BotAccessors(ConversationState conversationState)
    {
        ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
    }

    public static string DialogStateAccessorKey { get; } = "BotAccessors.DialogState";
    public static string ReservationAccessorKey { get; } = "BotAccessors.Reservation";

    public IStatePropertyAccessor<DialogState> DialogStateAccessor { get; set; }
    public IStatePropertyAccessor<ReservationBot.Reservation> ReservationAccessor { get; set; }

    public ConversationState ConversationState { get; }
}
```

In **Startup.cs**, update `ConfigureServices` to set the accessors.

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
            DialogStateAccessor = conversationState.CreateProperty<DialogState>(BotAccessors.DialogStateAccessorKey),
            ReservationAccessor = conversationState.CreateProperty<ReservationBot.Reservation>(BotAccessors.ReservationAccessorKey),
        };

        return accessors;
    });
}
```

# [JavaScript](#tab/javascript)

No changes to the HTTP service code required for JavaScript, we can leave our **index.js** file as is.

In **bot.js**, update the require statements and add identifiers for the state property accessors.

```javascript
const { ActivityTypes } = require('botbuilder');
const { DialogSet, WaterfallDialog, NumberPrompt, DateTimePrompt, DialogTurnStatus } = require('botbuilder-dialogs');

// Define identifiers for our state property accessors.
const DIALOG_STATE_ACCESSOR = 'dialogStateAccessor';
const RESERVATION_ACCESSOR = 'reservationAccessor';
```

---

In your bot file, add identifiers for our dialogs and prompts.

# [C#](#tab/csharp)

```csharp
// Define identifiers for our dialogs and prompts.
private const string ReservationDialog = "reservationDialog";
private const string PartySizePrompt = "partyPrompt";
private const string ReservationDatePrompt = "reservationDatePrompt";
```

# [JavaScript](#tab/javascript)

```javascript
// Define identifiers for our dialogs and prompts.
const RESERVATION_DIALOG = 'reservationDialog';
const PARTY_SIZE_PROMPT = 'partySizePrompt';
const RESERVATION_DATE_PROMPT = 'reservationDatePrompt';
```

---

### Define the prompts and dialogs

In the bot's constructor code, create the dialog set, add the prompts, and add the reservation dialog.
We include the custom validation when we create the prompts. We will implement the validation functions next.

# [C#](#tab/csharp)

```csharp
public ReservationBot(BotAccessors accessors, ILoggerFactory loggerFactory)
{
    // ...
    _accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));

    // Create the dialog set and add the prompts, including custom validation.
    _dialogSet = new DialogSet(_accessors.DialogStateAccessor);
    _dialogSet.Add(new NumberPrompt<int>(PartySizePrompt, PartySizeValidatorAsync));
    _dialogSet.Add(new DateTimePrompt(ReservationDatePrompt, DateValidatorAsync));

    // Define the steps of the waterfall dialog and add it to the set.
    WaterfallStep[] steps = new WaterfallStep[]
    {
        PromptForPartySizeAsync,
        PromptForReservationDateAsync,
        AcknowledgeReservationAsync,
    };
    _dialogSet.Add(new WaterfallDialog(ReservationDialog, steps));
}
```

# [JavaScript](#tab/javascript)

```javascript
constructor(conversationState) {
    // Creates our state accessor properties.
    // See https://aka.ms/about-bot-state-accessors to learn more about the bot state and state accessors.
    this.dialogStateAccessor = conversationState.createProperty(DIALOG_STATE_ACCESSOR);
    this.reservationAccessor = conversationState.createProperty(RESERVATION_ACCESSOR);
    this.conversationState = conversationState;

    // Create the dialog set and add the prompts, including custom validation.
    this.dialogSet = new DialogSet(this.dialogStateAccessor);
    this.dialogSet.add(new NumberPrompt(PARTY_SIZE_PROMPT, partySizeValidator));
    this.dialogSet.add(new DateTimePrompt(RESERVATION_DATE_PROMPT, dateValidator));

    // Define the steps of the waterfall dialog and add it to the set.
    this.dialogSet.add(new WaterfallDialog(RESERVATION_DIALOG, [
        this.promptForPartySize.bind(this),
        this.promptForReservationDate.bind(this),
        this.acknowledgeReservation.bind(this),
    ]));
}
```

---

### Implement validation code

Implement the party-size validator. We'll limit reservations to parties of 6 to 20 people.

# [C#](#tab/csharp)

```csharp
/// <summary>Validates whether the party size is appropriate to make a reservation.</summary>
/// <param name="promptContext">The validation context.</param>
/// <param name="cancellationToken">A cancellation token that can be used by other objects
/// or threads to receive notice of cancellation.</param>
/// <returns>A task that represents the work queued to execute.</returns>
/// <remarks>Reservations can be made for groups of 6 to 20 people.
/// If the task is successful, the result indicates whether the input was valid.</remarks>
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

The date-time prompt returns a list or array of the possible _date-time resolutions_ that match the user input. For example, 9:00 could mean 9 AM or 9 PM, and Sunday is also ambiguous. In addition, a date-time resolution can represent a date, a time, a date-time, or a range. The date-time prompt uses the [Microsoft/Recognizers-Text](https://github.com/Microsoft/Recognizers-Text) to parse the user input.

Implement the reservation-date validator. We'll limit reservations to an hour or more from the current time. We are keeping the first resolution that matches our criteria, and clearing the rest.

This validation code is not exhaustive. It works best for input that parses to a date and time. It demonstrates some of the options for validating a date-time prompt, and your implementation will depend on what information you are trying to collect from the user.

# [C#](#tab/csharp)

```csharp
/// <summary>Validates whether the reservation date is appropriate.</summary>
/// <param name="promptContext">The validation context.</param>
/// <param name="cancellationToken">A cancellation token that can be used by other objects
/// or threads to receive notice of cancellation.</param>
/// <returns>A task that represents the work queued to execute.</returns>
/// <remarks>Reservations must be made at least an hour in advance.
/// If the task is successful, the result indicates whether the input was valid.</remarks>
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

### Implement the dialog steps

Use the prompts that we added to the dialog set. We added validation to the prompts when we created them in the bot's constructor. The first time the prompt asks for user input, it sends the _prompt_ activity from the options provided. If validation fails, it sends the _retry prompt_ activity to ask the user for different input.

# [C#](#tab/csharp)

```csharp
/// <summary>First step of the main dialog: prompt for party size.</summary>
/// <param name="stepContext">The context for the waterfall step.</param>
/// <param name="cancellationToken">A cancellation token that can be used by other objects
/// or threads to receive notice of cancellation.</param>
/// <returns>A task that represents the work queued to execute.</returns>
/// <remarks>If the task is successful, the result contains information from this step.</remarks>
private async Task<DialogTurnResult> PromptForPartySizeAsync(
    WaterfallStepContext stepContext,
    CancellationToken cancellationToken = default(CancellationToken))
{
    // Prompt for the party size. The result of the prompt is returned to the next step of the waterfall.
    return await stepContext.PromptAsync(
        PartySizePrompt,
        new PromptOptions
        {
            Prompt = MessageFactory.Text("How many people is the reservation for?"),
            RetryPrompt = MessageFactory.Text("How large is your party?"),
        },
        cancellationToken);
}

/// <summary>Second step of the main dialog: record the party size and prompt for the
/// reservation date.</summary>
/// <param name="stepContext">The context for the waterfall step.</param>
/// <param name="cancellationToken">A cancellation token that can be used by other objects
/// or threads to receive notice of cancellation.</param>
/// <returns>A task that represents the work queued to execute.</returns>
/// <remarks>If the task is successful, the result contains information from this step.</remarks>
private async Task<DialogTurnResult> PromptForReservationDateAsync(
    WaterfallStepContext stepContext,
    CancellationToken cancellationToken = default(CancellationToken))
{
    // Record the party size information in the current dialog state.
    int size = (int)stepContext.Result;
    stepContext.Values["size"] = size;

    // Prompt for the reservation date. The result of the prompt is returned to the next step of the waterfall.
    return await stepContext.PromptAsync(
        ReservationDatePrompt,
        new PromptOptions
        {
            Prompt = MessageFactory.Text("Great. When will the reservation be for?"),
            RetryPrompt = MessageFactory.Text("What time should we make your reservation for?"),
        },
        cancellationToken);
}

/// <summary>Third step of the main dialog: return the collected party size and reservation date.</summary>
/// <param name="stepContext">The context for the waterfall step.</param>
/// <param name="cancellationToken">A cancellation token that can be used by other objects
/// or threads to receive notice of cancellation.</param>
/// <returns>A task that represents the work queued to execute.</returns>
/// <remarks>If the task is successful, the result contains information from this step.</remarks>
private async Task<DialogTurnResult> AcknowledgeReservationAsync(
    WaterfallStepContext stepContext,
    CancellationToken cancellationToken = default(CancellationToken))
{
    // Retrieve the reservation date.
    DateTimeResolution resolution = (stepContext.Result as IList<DateTimeResolution>).First();
    string time = resolution.Value ?? resolution.Start;

    // Send an acknowledgement to the user.
    await stepContext.Context.SendActivityAsync(
        "Thank you. We will confirm your reservation shortly.",
        cancellationToken: cancellationToken);

    // Return the collected information to the parent context.
    Reservation reservation = new Reservation
    {
        Date = time,
        Size = (int)stepContext.Values["size"],
    };
    return await stepContext.EndDialogAsync(reservation, cancellationToken);
}
```

# [JavaScript](#tab/javascript)

```javascript
async promptForPartySize(stepContext) {
    // Prompt for the party size. The result of the prompt is returned to the next step of the waterfall.
    return await stepContext.prompt(
        PARTY_SIZE_PROMPT, {
            prompt: 'How many people is the reservation for?',
            retryPrompt: 'How large is your party?'
        });
}

async promptForReservationDate(stepContext) {
    // Record the party size information in the current dialog state.
    stepContext.values.size = stepContext.result;

    // Prompt for the reservation date. The result of the prompt is returned to the next step of the waterfall.
    return await stepContext.prompt(
        RESERVATION_DATE_PROMPT, {
            prompt: 'Great. When will the reservation be for?',
            retryPrompt: 'What time should we make your reservation for?'
        });
}

async acknowledgeReservation(stepContext) {
    // Retrieve the reservation date.
    const resolution = stepContext.result[0];
    const time = resolution.value || resolution.start;

    // Send an acknowledgement to the user.
    await stepContext.context.sendActivity(
        'Thank you. We will confirm your reservation shortly.');

    // Return the collected information to the parent context.
    return await stepContext.endDialog({ date: time, size: stepContext.values.size });
}
```

---

### Update the turn handler

Update the bot's turn handler to start the dialog and accept a return value from the dialog when it completes.

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
        default:
            break;
    }
}
```

---

Further examples can be found in our [samples repo](https://aka.ms/bot-samples-readme).

You can use the similar techniques to validate prompt responses for any of the prompt types.

## Handling prompt results

What you do with the prompt result depends on why you requested the information from the user. Options include:

* Use the information to control the flow of your dialog, such as when the user responses to a confirm or choice prompt.
* Cache the information in the dialog's state, such as setting a value in the waterfall step context's _values_ property, and then return the collected information when the dialog ends.
* Save the information to bot state. This would require you to design your dialog to have access to the bot's state property accessors.

See the additional resources for topics and samples that cover these scenarios.

## Additional resources

* [Manage simple conversation flow](bot-builder-dialog-manage-conversation-flow.md)
* [Manage complex conversation flow](bot-builder-dialog-manage-complex-conversation-flow.md)
* [Create an integrated set of dialogs](bot-builder-compositcontrol.md)
* [Persist data in dialogs](bot-builder-tutorial-persist-user-inputs.md)
* The **multi-turn prompt** sample ([C#](https://aka.ms/cs-multi-prompts-sample) | [JS](https://aka.ms/js-multi-prompts-sample))

## Next steps

Now that you know how to prompt a user for input, lets enhance the bot code and user experience by managing various conversation flows through dialogs.

> [!div class="nextstepaction"]
> [Manage complex conversation flow](bot-builder-dialog-manage-complex-conversation-flow.md)
