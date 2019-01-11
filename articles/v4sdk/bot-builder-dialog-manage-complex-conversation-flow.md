---
title: Create advanced conversation flow using branches and loops | Microsoft Docs
description: Learn how to manage a complex conversation flow with dialogs in the Bot Builder SDK.
keywords: complex conversation flow, repeat, loop, menu, dialogs, prompts, waterfalls, dialog set
author: JonathanFingold
ms.author: v-jofing
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 11/28/2018
monikerRange: 'azure-bot-service-4.0'
---

# Create advanced conversation flow using branches and loops

[!INCLUDE [pre-release-label](~/includes/pre-release-label.md)]

In this article, we will show you how to manage complex conversations that branch and loop. We'll also show you how to pass arguments between different parts of the dialog.

## Prerequisites

- [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/blob/master/README.md#download)
- The code in this article is based on the **complex dialog** sample. You'll need a copy of the sample in either [C#](https://aka.ms/cs-complex-dialog-sample) or [JS](https://aka.ms/js-complex-dialog-sample).
- Knowledge of [bot basics](bot-builder-basics.md), the [dialogs library](bot-builder-concept-dialog.md), [dialog state](bot-builder-dialog-state.md), and [.bot](bot-file-basics.md) files.

## About the sample

This sample represents a bot that can sign users up to review up to two companies from a list.

- It asks for the user's name and age, and then _branches_ based on the user's age.
  - If the user is too young, it does not ask the user to review any companies.
  - If the user is old enough, it starts to collect the user's review preferences.
    - It allows the user to select a company to review.
    - If they choose a company, it _loops_ to allow them to choose a second company.
- Finally, it thanks the user for participating.

It uses two waterfall dialogs and a few prompts to manage a complex conversation.

## Configure state for your bot

# [C#](#tab/csharp)

Define the user information we'll collect.

```csharp
public class UserProfile
{
    public string Name { get; set; }
    public int Age { get; set; }

    //The list of companies the user wants to review.
    public List<string> CompaniesToReview { get; set; } = new List<string>();
}
```

Define the class to hold the state management objects and state property accessors for the bot.

```csharp
public class ComplexDialogBotAccessors
{
    public ComplexDialogBotAccessors(ConversationState conversationState, UserState userState)
    {
        ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
        UserState = userState ?? throw new ArgumentNullException(nameof(userState));
    }

    public IStatePropertyAccessor<DialogState> DialogStateAccessor { get; set; }
    public IStatePropertyAccessor<UserProfile> UserProfileAccessor { get; set; }

    public ConversationState ConversationState { get; }
    public UserState UserState { get; }
}
```

Create the state management object and register the accessors class in the `ConfigureServices` method of the `Statup` class.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Register the bot.

    // Create conversation and user state management objects, using memory storage.
    IStorage dataStore = new MemoryStorage();
    var conversationState = new ConversationState(dataStore);
    var userState = new UserState(dataStore);

    // Create and register state accessors.
    // Accessors created here are passed into the IBot-derived class on every turn.
    services.AddSingleton<ComplexDialogBotAccessors>(sp =>
    {
        // Create the custom state accessor.
        // State accessors enable other components to read and write individual properties of state.
        var accessors = new ComplexDialogBotAccessors(conversationState, userState)
        {
            DialogStateAccessor = conversationState.CreateProperty<DialogState>("DialogState"),
            UserProfileAccessor = userState.CreateProperty<UserProfile>("UserProfile"),
        };

        return accessors;
    });
}
```

# [JavaScript](#tab/javascript)

In the **index.js** file, we define the state management objects.

```javascript
const { BotFrameworkAdapter, MemoryStorage, UserState, ConversationState } = require('botbuilder');

// ...

// Define state store for your bot.
const memoryStorage = new MemoryStorage();

// Create user and conversation state with in-memory storage provider.
const userState = new UserState(memoryStorage);
const conversationState = new ConversationState(memoryStorage);

// Create the bot.
const myBot = new MyBot(conversationState, userState);
```

The bot's constructor will create the state property accessors for the bot.

---

## Initialize your bot

Create a _dialog set_ for the bot, to which we'll add all of the dialogs for this example.

# [C#](#tab/csharp)

Create the dialog set within the bot's constructor, adding the prompts and the two waterfall dialogs to the set.

Here, we define each step as a separate method. We'll implement these in the next section.

```csharp
public class ComplexDialogBot : IBot
{
    // Define constants for the bot...

    // Define properties for the bot's accessors and dialog set.
    private readonly ComplexDialogBotAccessors _accessors;
    private readonly DialogSet _dialogs;

    // Initialize the bot and add dialogs and prompts to the dialog set.
    public ComplexDialogBot(ComplexDialogBotAccessors accessors)
    {
        _accessors = accessors ?? throw new ArgumentNullException(nameof(accessors));

        // Create a dialog set for the bot. It requires a DialogState accessor, with which
        // to retrieve the dialog state from the turn context.
        _dialogs = new DialogSet(accessors.DialogStateAccessor);

        // Add the prompts we need to the dialog set.
        _dialogs
            .Add(new TextPrompt(NamePrompt))
            .Add(new NumberPrompt<int>(AgePrompt))
            .Add(new ChoicePrompt(SelectionPrompt));

        // Add the dialogs we need to the dialog set.
        _dialogs.Add(new WaterfallDialog(TopLevelDialog)
            .AddStep(NameStepAsync)
            .AddStep(AgeStepAsync)
            .AddStep(StartSelectionStepAsync)
            .AddStep(AcknowledgementStepAsync));

        _dialogs.Add(new WaterfallDialog(ReviewSelectionDialog)
            .AddStep(SelectionStepAsync)
            .AddStep(LoopStepAsync));
    }

    // Turn handler and other supporting methods...
}
```

# [JavaScript](#tab/javascript)

In the **bot.js** file, define and create the dialog set in the bot's constructor, adding the prompts and the waterfall dialogs to the set.

Here, we define each step as a separate method. We'll implement these in the next section.

```javascript
const { ActivityTypes } = require('botbuilder');
const { DialogSet, WaterfallDialog, TextPrompt, NumberPrompt, ChoicePrompt, DialogTurnStatus } = require('botbuilder-dialogs');

// Define constants for the bot...

class MyBot {
    constructor(conversationState, userState) {
        // Create the state property accessors and save the state management objects.
        this.dialogStateAccessor = conversationState.createProperty(DIALOG_STATE_PROPERTY);
        this.userProfileAccessor = userState.createProperty(USER_PROFILE_PROPERTY);
        this.conversationState = conversationState;
        this.userState = userState;

        // Create a dialog set for the bot. It requires a DialogState accessor, with which
        // to retrieve the dialog state from the turn context.
        this.dialogs = new DialogSet(this.dialogStateAccessor);

        // Add the prompts we need to the dialog set.
        this.dialogs
            .add(new TextPrompt(NAME_PROMPT))
            .add(new NumberPrompt(AGE_PROMPT))
            .add(new ChoicePrompt(SELECTION_PROMPT));

        // Add the dialogs we need to the dialog set.
        this.dialogs.add(new WaterfallDialog(TOP_LEVEL_DIALOG)
            .addStep(this.nameStep.bind(this))
            .addStep(this.ageStep.bind(this))
            .addStep(this.startSelectionStep.bind(this))
            .addStep(this.acknowledgementStep.bind(this)));

        this.dialogs.add(new WaterfallDialog(REVIEW_SELECTION_DIALOG)
            .addStep(this.selectionStep.bind(this))
            .addStep(this.loopStep.bind(this)));
    }

    // Turn handler and other supporting methods...
}
```

---

## Implement the steps for the waterfall dialogs

Now, let's implement the steps for our two dialogs.

### The top-level dialog

The initial, top-level dialog has four steps:

1. Ask for the user's name.
1. Ask for the user's age.
1. Branch based on the user's age.
1. Finally, thank the user for participating and return the collected information.

# [C#](#tab/csharp)

```csharp
// The first step of the top-level dialog.
private static async Task<DialogTurnResult> NameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    // Create an object in which to collect the user's information within the dialog.
    stepContext.Values[UserInfo] = new UserProfile();

    // Ask the user to enter their name.
    return await stepContext.PromptAsync(
        NamePrompt,
        new PromptOptions { Prompt = MessageFactory.Text("Please enter your name.") },
        cancellationToken);
}

// The second step of the top-level dialog.
private async Task<DialogTurnResult> AgeStepAsync(
    WaterfallStepContext stepContext,
    CancellationToken cancellationToken)
{
    // Set the user's name to what they entered in response to the name prompt.
    ((UserProfile)stepContext.Values[UserInfo]).Name = (string)stepContext.Result;

    // Ask the user to enter their age.
    return await stepContext.PromptAsync(
        AgePrompt,
        new PromptOptions { Prompt = MessageFactory.Text("Please enter your age.") },
        cancellationToken);
}

// The third step of the top-level dialog.
private async Task<DialogTurnResult> StartSelectionStepAsync(
    WaterfallStepContext stepContext,
    CancellationToken cancellationToken)
{
    // Set the user's age to what they entered in response to the age prompt.
    int age = (int)stepContext.Result;
    ((UserProfile)stepContext.Values[UserInfo]).Age = age;

    if (age < 25)
    {
        // If they are too young, skip the review-selection dialog, and pass an empty list to the next step.
        await stepContext.Context.SendActivityAsync(
            MessageFactory.Text("You must be 25 or older to participate."),
            cancellationToken);
        return await stepContext.NextAsync(new List<string>(), cancellationToken);
    }
    else
    {
        // Otherwise, start the review-selection dialog.
        return await stepContext.BeginDialogAsync(ReviewSelectionDialog, null, cancellationToken);
    }
}

// The final step of the top-level dialog.
private async Task<DialogTurnResult> AcknowledgementStepAsync(
    WaterfallStepContext stepContext,
    CancellationToken cancellationToken)
{
    // Set the user's company selection to what they entered in the review-selection dialog.
    List<string> list = stepContext.Result as List<string>;
    ((UserProfile)stepContext.Values[UserInfo]).CompaniesToReview = list ?? new List<string>();

    // Thank them for participating.
    await stepContext.Context.SendActivityAsync(
        MessageFactory.Text($"Thanks for participating, {((UserProfile)stepContext.Values[UserInfo]).Name}."),
        cancellationToken);

    // Exit the dialog, returning the collected user information.
    return await stepContext.EndDialogAsync(stepContext.Values[UserInfo], cancellationToken);
}
```

# [JavaScript](#tab/javascript)

```javascript
async nameStep(stepContext) {
    // Create an object in which to collect the user's information within the dialog.
    stepContext.values[USER_INFO] = {};

    // Ask the user to enter their name.
    return await stepContext.prompt(NAME_PROMPT, 'Please enter your name.');
}

async ageStep(stepContext) {
    // Set the user's name to what they entered in response to the name prompt.
    stepContext.values[USER_INFO].name = stepContext.result;

    // Ask the user to enter their age.
    return await stepContext.prompt(AGE_PROMPT, 'Please enter your age.');
}

async startSelectionStep(stepContext) {
    // Set the user's age to what they entered in response to the age prompt.
    stepContext.values[USER_INFO].age = stepContext.result;

    if (stepContext.result < 25) {
        // If they are too young, skip the review-selection dialog, and pass an empty list to the next step.
        await stepContext.context.sendActivity('You must be 25 or older to participate.');
        return await stepContext.next([]);
    } else {
        // Otherwise, start the review-selection dialog.
        return await stepContext.beginDialog(REVIEW_SELECTION_DIALOG);
    }
}

async acknowledgementStep(stepContext) {
    // Set the user's company selection to what they entered in the review-selection dialog.
    const list = stepContext.result || [];
    stepContext.values[USER_INFO].companiesToReview = list;

    // Thank them for participating.
    await stepContext.context.sendActivity(`Thanks for participating, ${stepContext.values[USER_INFO].name}.`);

    // Exit the dialog, returning the collected user information.
    return await stepContext.endDialog(stepContext.values[USER_INFO]);
}
```

---

### The review-selection dialog

The review-selection dialog has two steps:

1. Ask the user to choose a company to review or choose `done` to finish.
1. Repeat this dialog or exit, as appropriate.

In this design, the top-level dialog will always precede the review-selection dialog on the stack, and the review-selection dialog can be though of as a child of the top-level dialog.

# [C#](#tab/csharp)

```csharp
// The first step of the review-selection dialog.
private async Task<DialogTurnResult> SelectionStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    // Continue using the same selection list, if any, from the previous iteration of this dialog.
    List<string> list = stepContext.Options as List<string> ?? new List<string>();
    stepContext.Values[CompaniesSelected] = list;

    // Create a prompt message.
    string message;
    if (list.Count is 0)
    {
        message = $"Please choose a company to review, or `{DoneOption}` to finish.";
    }
    else
    {
        message = $"You have selected **{list[0]}**. You can review an additional company, " +
            $"or choose `{DoneOption}` to finish.";
    }

    // Create the list of options to choose from.
    List<string> options = _companyOptions.ToList();
    options.Add(DoneOption);
    if (list.Count > 0)
    {
        options.Remove(list[0]);
    }

    // Prompt the user for a choice.
    return await stepContext.PromptAsync(
        SelectionPrompt,
        new PromptOptions
        {
            Prompt = MessageFactory.Text(message),
            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
            Choices = ChoiceFactory.ToChoices(options),
        },
        cancellationToken);
}

// The final step of the review-selection dialog.
private async Task<DialogTurnResult> LoopStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    // Retrieve their selection list, the choice they made, and whether they chose to finish.
    List<string> list = stepContext.Values[CompaniesSelected] as List<string>;
    FoundChoice choice = (FoundChoice)stepContext.Result;
    bool done = choice.Value == DoneOption;

    if (!done)
    {
        // If they chose a company, add it to the list.
        list.Add(choice.Value);
    }

    if (done || list.Count is 2)
    {
        // If they're done, exit and return their list.
        return await stepContext.EndDialogAsync(list, cancellationToken);
    }
    else
    {
        // Otherwise, repeat this dialog, passing in the list from this iteration.
        return await stepContext.ReplaceDialogAsync(ReviewSelectionDialog, list, cancellationToken);
    }
}
```

# [JavaScript](#tab/javascript)

```javascript
async selectionStep(stepContext) {
    // Continue using the same selection list, if any, from the previous iteration of this dialog.
    const list = Array.isArray(stepContext.options) ? stepContext.options : [];
    stepContext.values[COMPANIES_SELECTED] = list;

    // Create a prompt message.
    let message;
    if (list.length === 0) {
        message = 'Please choose a company to review, or `' + DONE_OPTION + '` to finish.';
    } else {
        message = `You have selected **${list[0]}**. You can review an addition company, ` +
            'or choose `' + DONE_OPTION + '` to finish.';
    }

    // Create the list of options to choose from.
    const options = list.length > 0
        ? COMPANY_OPTIONS.filter(function (item) { return item !== list[0] })
        : COMPANY_OPTIONS.slice();
    options.push(DONE_OPTION);

    // Prompt the user for a choice.
    return await stepContext.prompt(SELECTION_PROMPT, {
        prompt: message,
        retryPrompt: 'Please choose an option from the list.',
        choices: options
    });
}

async loopStep(stepContext) {
    // Retrieve their selection list, the choice they made, and whether they chose to finish.
    const list = stepContext.values[COMPANIES_SELECTED];
    const choice = stepContext.result;
    const done = choice.value === DONE_OPTION;

    if (!done) {
        // If they chose a company, add it to the list.
        list.push(choice.value);
    }

    if (done || list.length > 1) {
        // If they're done, exit and return their list.
        return await stepContext.endDialog(list);
    } else {
        // Otherwise, repeat this dialog, passing in the list from this iteration.
        return await stepContext.replaceDialog(REVIEW_SELECTION_DIALOG, list);
    }
}
```

---

## Update the bot's turn handler

The bot's turn handler repeats the one conversation flow defined by these dialogs.
When we receive a message from the user:

1. Continue the active dialog, if there is one.
   - If no dialog was active, we clear the user profile and start the top-level dialog.
   - If the active dialog completed, we collect and save the returned information and display a status message.
   - Otherwise, the active dialog is still mid-process, and we don't need to do anything else at the moment.
1. Save the conversation state, so that any updates to the dialog state are persisted.

# [C#](#tab/csharp)

```csharp
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
{
    if (turnContext == null)
    {
        throw new ArgumentNullException(nameof(turnContext));
    }

    if (turnContext.Activity.Type == ActivityTypes.Message)
    {
        // Run the DialogSet - let the framework identify the current state of the dialog from
        // the dialog stack and figure out what (if any) is the active dialog.
        DialogContext dialogContext = await _dialogs.CreateContextAsync(turnContext, cancellationToken);
        DialogTurnResult results = await dialogContext.ContinueDialogAsync(cancellationToken);
        switch (results.Status)
        {
            case DialogTurnStatus.Cancelled:
            case DialogTurnStatus.Empty:
                // If there is no active dialog, we should clear the user info and start a new dialog.
                await _accessors.UserProfileAccessor.SetAsync(turnContext, new UserProfile(), cancellationToken);
                await _accessors.UserState.SaveChangesAsync(turnContext, false, cancellationToken);
                await dialogContext.BeginDialogAsync(TopLevelDialog, null, cancellationToken);
                break;

            case DialogTurnStatus.Complete:
                // If we just finished the dialog, capture and display the results.
                UserProfile userInfo = results.Result as UserProfile;
                string status = "You are signed up to review "
                    + (userInfo.CompaniesToReview.Count is 0
                        ? "no companies"
                        : string.Join(" and ", userInfo.CompaniesToReview))
                    + ".";
                await turnContext.SendActivityAsync(status);
                await _accessors.UserProfileAccessor.SetAsync(turnContext, userInfo, cancellationToken);
                await _accessors.UserState.SaveChangesAsync(turnContext, false, cancellationToken);
                break;

            case DialogTurnStatus.Waiting:
                // If there is an active dialog, we don't need to do anything here.
                break;
        }

        await _accessors.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
    }

    // Processes ConversationUpdate Activities to welcome the user.
    else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
    {
        // Welcome new users...
    }
    else
    {
        // Give a default reply for all other activity types...
    }
}
```

# [JavaScript](#tab/javascript)

```javascript
async onTurn(turnContext) {
    if (turnContext.activity.type === ActivityTypes.Message) {
        // Run the DialogSet - let the framework identify the current state of the dialog from
        // the dialog stack and figure out what (if any) is the active dialog.
        const dialogContext = await this.dialogs.createContext(turnContext);
        const results = await dialogContext.continueDialog();
        switch (results.status) {
            case DialogTurnStatus.cancelled:
            case DialogTurnStatus.empty:
                // If there is no active dialog, we should clear the user info and start a new dialog.
                await this.userProfileAccessor.set(turnContext, {});
                await this.userState.saveChanges(turnContext);
                await dialogContext.beginDialog(TOP_LEVEL_DIALOG);
                break;
            case DialogTurnStatus.complete:
                // If we just finished the dialog, capture and display the results.
                const userInfo = results.result;
                const status = 'You are signed up to review '
                    + (userInfo.companiesToReview.length === 0 ? 'no companies' : userInfo.companiesToReview.join(' and '))
                    + '.';
                await turnContext.sendActivity(status);
                await this.userProfileAccessor.set(turnContext, userInfo);
                await this.userState.saveChanges(turnContext);
                break;
            case DialogTurnStatus.waiting:
                // If there is an active dialog, we don't need to do anything here.
                break;
        }
        await this.conversationState.saveChanges(turnContext);
    } else if (turnContext.activity.type === ActivityTypes.ConversationUpdate) {
        // Welcome new users...
    } else {
        // Give a default reply for all other activity types...
    }
}
```

---

## Test your dialog

1. Run the sample locally on your machine. If you need instructions, refer to the README file for the [C#](https://aka.ms/cs-complex-dialog-sample) or [JS](https://aka.ms/js-complex-dialog-sample) sample.
1. Use the emulator to test the bot as shown below.

![test complex dialog sample](~/media/emulator-v4/test-complex-dialog.png)

## Additional resources

For an introduction on how to implement a dialog, see [implement sequential conversation flow](bot-builder-dialog-manage-conversation-flow.md), which uses a single waterfall dialog and a few prompts to create a simple interaction that asks the user a series of questions.

The Dialogs library includes basic validation for prompts. You can also add custom validation. For more information, see [gather user input using a dialog prompt](bot-builder-prompts.md).

To simplify your dialog code and reuse it multiple bots, you can define portions of a dialog set as a separate class.
For more information, see [reuse dialogs](bot-builder-compositcontrol.md).

## Next steps

You can enhance bots to react to additional input, like "help" or "cancel", that can interrupt the normal flow of the conversation.

> [!div class="nextstepaction"]
> [Handle user interruptions](bot-builder-howto-handle-user-interrupt.md)
