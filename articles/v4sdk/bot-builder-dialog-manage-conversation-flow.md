---
title: Implement sequential conversation flow | Microsoft Docs
description: Learn how to manage a simple conversation flow with dialogs in the Bot Builder SDK for Node.js.
keywords: simple conversation flow, sequential conversation flow, dialogs, prompts, waterfalls, dialog set
author: JonathanFingold
ms.author: v-jofing
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 11/13/2018
monikerRange: 'azure-bot-service-4.0'
---

# Implement sequential conversation flow

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

You can manage simple and complex conversation flows using the dialogs library.

In a simple interaction, the bot runs through a fixed sequence of steps, and the conversation finishes.
In this article, we use a _waterfall dialog_, a few _prompts_, and a _dialog set_ to create a simple interaction that asks the user a series of questions.
We draw on code from the **multi-turn prompt** [[C#](https://aka.ms/cs-multi-prompts-sample) | [JS](https://aka.ms/js-multi-prompts-sample)] sample.

# [C#](#tab/csharp)

To use dialogs in general, you need the `Microsoft.Bot.Builder.Dialogs` NuGet package for your project or solution.

# [JavaScript](#tab/javascript)

To use dialogs in general, you need the `botbuilder-dialogs` library, which can be downloaded via npm.

To install this package and save it as a dependency, navigate to your project's directory and use this command.

```shell
npm install botbuilder-dialogs --save
```

---
The following sections reflect the steps you would take to implement simple dialogs for most bots:

## Configure your bot

We will need a state property accessor assigned to the dialog set that the bot can use to manage [dialog state](bot-builder-dialog-state.md).

# [C#](#tab/csharp)

We will initialize the state property accessor for the bot's dialog state in the configuration code in the **Startup.cs** file.

We define a `MultiTurnPromptsBotAccessors` class to hold the state management objects and state property accessors for the bot.
Here, we're calling out only portions of the code.

```csharp
public class MultiTurnPromptsBotAccessors
{
    // Initializes a new instance of the class.
    public MultiTurnPromptsBotAccessors(ConversationState conversationState, UserState userState)
    {
        ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
        UserState = userState ?? throw new ArgumentNullException(nameof(userState));
    }

    public IStatePropertyAccessor<DialogState> ConversationDialogState { get; set; }
    public IStatePropertyAccessor<UserProfile> UserProfile { get; set; }

    public ConversationState ConversationState { get; }
    public UserState UserState { get; }
}
```

We register the accessors class in the `ConfigureServices` method of the `Startup` class.
Again, we're calling out only portions of the code.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // ...

    // Create and register state accessors.
    // Accessors created here are passed into the IBot-derived class on every turn.
    services.AddSingleton<MultiTurnPromptsBotAccessors>(sp =>
    {
        // We need to grab the conversationState we added on the options in the previous step
        var options = sp.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;
        var conversationState = options.State.OfType<ConversationState>().FirstOrDefault();
        var userState = options.State.OfType<UserState>().FirstOrDefault();

        // Create the custom state accessor.
        // State accessors enable other components to read and write individual properties of state.
        var accessors = new MultiTurnPromptsBotAccessors(conversationState, userState)
        {
            ConversationDialogState = conversationState.CreateProperty<DialogState>("DialogState"),
            UserProfile = userState.CreateProperty<UserProfile>("UserProfile"),
        };

        return accessors;
    });
}
```

Through dependency injection, the accessors will be available to the bot's constructor code.

# [JavaScript](#tab/javascript)

In the **index.js** file, we define the state management objects.
Here, we're calling out only portions of the code.

```javascript
// Import required bot services. See https://aka.ms/bot-services to learn more about the different part of a bot.
const { BotFrameworkAdapter, ConversationState, MemoryStorage, UserState } = require('botbuilder');

// Define the state store for your bot. See https://aka.ms/about-bot-state to learn more about using MemoryStorage.
// A bot requires a state storage system to persist the dialog and user state between messages.
const memoryStorage = new MemoryStorage();

// Create conversation state with in-memory storage provider.
const conversationState = new ConversationState(memoryStorage);
const userState = new UserState(memoryStorage);

// Create the main dialog, which serves as the bot's main handler.
const bot = new MultiTurnBot(conversationState, userState);
```

The bot's constructor will create the state property accessors for the bot: `this.dialogState`, and `this.userProfile`.

---

## Update the bot turn handler to call the dialog

To run the dialog, the bot's turn handler needs to create a dialog context for the dialog set that contains the dialogs for the bot. (A bot could define multiple dialog sets, but as a general rule, you should just define one for your bot. [Dialogs library](bot-builder-concept-dialog.md) describes key aspects of dialogs.)

# [C#](#tab/csharp)

The dialog is run from the bot's turn handler. The handler first creates a `DialogContext` and either continues the active dialog or begins a new dialog as appropriate. The handler then saves conversation and user state at the end of the turn.

In the `MultiTurnPromptsBot` class, we've defined a `_dialogs` property that contains the dialog set, from which we generate a dialog context. Again, we're showing only part of the turn handler code here.

```csharp
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
{
    // ...
    if (turnContext.Activity.Type == ActivityTypes.Message)
    {
        // Run the DialogSet - let the framework identify the current state of the dialog from
        // the dialog stack and figure out what (if any) is the active dialog.
        var dialogContext = await _dialogs.CreateContextAsync(turnContext, cancellationToken);
        var results = await dialogContext.ContinueDialogAsync(cancellationToken);

        // If the DialogTurnStatus is Empty we should start a new dialog.
        if (results.Status == DialogTurnStatus.Empty)
        {
            await dialogContext.BeginDialogAsync("details", null, cancellationToken);
        }
    }

    // ...
    // Save the dialog state into the conversation state.
    await _accessors.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);

    // Save the user profile updates into the user state.
    await _accessors.UserState.SaveChangesAsync(turnContext, false, cancellationToken);
}
```

# [JavaScript](#tab/javascript)

The bot code uses a few of the classes in the dialogs library.

```javascript
const { ChoicePrompt, DialogSet, NumberPrompt, TextPrompt, WaterfallDialog } = require('botbuilder-dialogs');
```

The dialog is run from the bot's turn handler. The handler first creates a `DialogContext` (`dc`) and either continues the active dialog or begins a new dialog as appropriate. The handler then saves conversation and user state at the end of the turn.

The `MultiTurnBot` class is defined in the **bot.js** file. The constructor for this class adds a `dialogs` property for the dialog set, from which we generate a dialog context. This bot collects the user data once, using the `WHO_ARE_YOU` dialog. Once the user profile is populated, the bot uses the `HELLO_USER` dialog to respond. Again, we're showing only part of the turn handler code here.

```javascript
async onTurn(turnContext) {
    if (turnContext.activity.type === ActivityTypes.Message) {
        // Create a dialog context object.
        const dc = await this.dialogs.createContext(turnContext);

        const utterance = (turnContext.activity.text || '').trim().toLowerCase();

        // ...
        // If the bot has not yet responded, continue processing the current dialog.
        await dc.continueDialog();

        // Start the sample dialog in response to any other input.
        if (!turnContext.responded) {
            const user = await this.userProfile.get(dc.context, {});
            if (user.name) {
                await dc.beginDialog(HELLO_USER);
            } else {
                await dc.beginDialog(WHO_ARE_YOU);
            }
        }
    }

    // ...
    // Save changes to the user state.
    await this.userState.saveChanges(turnContext);

    // End this turn by saving changes to the conversation state.
    await this.conversationState.saveChanges(turnContext);
}
```

---

In the bot's turn handler, we create a dialog context for the dialog set. The dialog context accesses the state cache for the bot, effectively remembering where in the conversation the last turn left off.

If there is an active dialog, dialog context's _continue dialog_ method progresses it, using the user's input that triggered this turn; otherwise, the bot calls the dialog context's _begin dialog_ method to start a dialog.

Finally, we call the _save changes_ method on the state management objects to persist any changes that have happened this turn.

### About dialog and bot state

In this bot, we've defined two state property accessors:

* One created within conversation state for the dialog state property. The dialog state tracks where the user is within the dialogs of a dialog set, and it is updated by the dialog context, such as when we call the begin dialog or continue dialog methods.
* One created within user state for the user profile property. The bot uses this to track information it has about the user, and we explicitly manage this state in our bot code.

The _get_ and _set_ methods of a state property accessor get and set the value of the property in the state management object's cache. The cache is populated the first time the value of a state property is requested in a turn, but it must be persisted explicitly. In order to persist changes to both of these state properties, we call the _save changes_ method of the corresponding state management object.

For more information, see [dialog state](bot-builder-dialog-state.md).

## Initialize your bot and define your dialog

Our simple conversation is modeled as a series of questions posed to the user. The C# and JavaScript versions have slightly different steps:

# [C#](#tab/csharp)

1. Ask them for their name.
1. Ask whether they are willing to provide their age.
1. If so, ask for their age; otherwise, skip this step.
1. Ask whether the information gathered is correct.
1. Send a status message and end.

# [JavaScript](#tab/javascript)

For the `who_are_you` dialog:

1. Ask them for their name.
1. Ask whether they are willing to provide their age.
1. If so, ask for their age; otherwise, skip this step.
1. Send a status message and end.

For the `hello_user` dialog:

1. Display the user information that the bot has gathered.

---

Here are a couple of things to remember when defining your own waterfall steps.

* Each bot turn reflects input from the user, followed by a response from the bot. Thus, you are asking the user for input at the end of a waterfall step, and receiving their answer in the next waterfall step.
* Each prompt is effectively a two-step dialog that presents its prompt and loops until it receives "valid" input. (You can rely on the built-in validation for each type of prompt, or you can add your own custom validation to the prompt. For more information, see [get user input](bot-builder-prompts.md).)

In this sample, the dialog is defined within the bot file and initialized in the bot's constructor.

# [C#](#tab/csharp)

Define an instance property for the dialog set.

```csharp
// The DialogSet that contains all the Dialogs that can be used at runtime.
private DialogSet _dialogs;
```

Create the dialog set within the bot's constructor, adding the prompts and the waterfall dialog to the set.

```csharp
public MultiTurnPromptsBot(MultiTurnPromptsBotAccessors accessors)
{
    _accessors = accessors ?? throw new ArgumentNullException(nameof(accessors));

    // The DialogSet needs a DialogState accessor, it will call it when it has a turn context.
    _dialogs = new DialogSet(accessors.ConversationDialogState);

    // This array defines how the Waterfall will execute.
    var waterfallSteps = new WaterfallStep[]
    {
        NameStepAsync,
        NameConfirmStepAsync,
        AgeStepAsync,
        ConfirmStepAsync,
        SummaryStepAsync,
    };

    // Add named dialogs to the DialogSet. These names are saved in the dialog state.
    _dialogs.Add(new WaterfallDialog("details", waterfallSteps));
    _dialogs.Add(new TextPrompt("name"));
    _dialogs.Add(new NumberPrompt<int>("age"));
    _dialogs.Add(new ConfirmPrompt("confirm"));
}
```

In this sample, we define each step as a separate method. You can also define the steps in-line in the constructor using lambda expressions.

```csharp
private static async Task<DialogTurnResult> NameStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is a Prompt Dialog.
    // Running a prompt here means the next WaterfallStep will be run when the users response is received.
    return await stepContext.PromptAsync("name", new PromptOptions { Prompt = MessageFactory.Text("Please enter your name.") }, cancellationToken);
}

private async Task<DialogTurnResult> NameConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    // Get the current profile object from user state.
    var userProfile = await _accessors.UserProfile.GetAsync(stepContext.Context, () => new UserProfile(), cancellationToken);

    // Update the profile.
    userProfile.Name = (string)stepContext.Result;

    // We can send messages to the user at any point in the WaterfallStep.
    await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Thanks {stepContext.Result}."), cancellationToken);

    // WaterfallStep always finishes with the end of the Waterfall or with another dialog; here it is a Prompt Dialog.
    return await stepContext.PromptAsync("confirm", new PromptOptions { Prompt = MessageFactory.Text("Would you like to give your age?") }, cancellationToken);
}

private async Task<DialogTurnResult> AgeStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    if ((bool)stepContext.Result)
    {
        // User said "yes" so we will be prompting for the age.

        // Get the current profile object from user state.
        var userProfile = await _accessors.UserProfile.GetAsync(stepContext.Context, () => new UserProfile(), cancellationToken);

        // WaterfallStep always finishes with the end of the Waterfall or with another dialog, here it is a Prompt Dialog.
        return await stepContext.PromptAsync("age", new PromptOptions { Prompt = MessageFactory.Text("Please enter your age.") }, cancellationToken);
    }
    else
    {
        // User said "no" so we will skip the next step. Give -1 as the age.
        return await stepContext.NextAsync(-1, cancellationToken);
    }
}


private async Task<DialogTurnResult> ConfirmStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    // Get the current profile object from user state.
    var userProfile = await _accessors.UserProfile.GetAsync(stepContext.Context, () => new UserProfile(), cancellationToken);

    // Update the profile.
    userProfile.Age = (int)stepContext.Result;

    // We can send messages to the user at any point in the WaterfallStep.
    if (userProfile.Age == -1)
    {
        await stepContext.Context.SendActivityAsync(MessageFactory.Text($"No age given."), cancellationToken);
    }
    else
    {
        // We can send messages to the user at any point in the WaterfallStep.
        await stepContext.Context.SendActivityAsync(MessageFactory.Text($"I have your age as {userProfile.Age}."), cancellationToken);
    }

    // WaterfallStep always finishes with the end of the Waterfall or with another dialog, here it is a Prompt Dialog.
    return await stepContext.PromptAsync("confirm", new PromptOptions { Prompt = MessageFactory.Text("Is this ok?") }, cancellationToken);
}

private async Task<DialogTurnResult> SummaryStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    if ((bool)stepContext.Result)
    {
        // Get the current profile object from user state.
        var userProfile = await _accessors.UserProfile.GetAsync(stepContext.Context, () => new UserProfile(), cancellationToken);

        // We can send messages to the user at any point in the WaterfallStep.
        if (userProfile.Age == -1)
        {
            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"I have your name as {userProfile.Name}."), cancellationToken);
        }
        else
        {
            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"I have your name as {userProfile.Name} and age as {userProfile.Age}."), cancellationToken);
        }
    }
    else
    {
        // We can send messages to the user at any point in the WaterfallStep.
        await stepContext.Context.SendActivityAsync(MessageFactory.Text("Thanks. Your profile will not be kept."), cancellationToken);
    }

    // WaterfallStep always finishes with the end of the Waterfall or with another dialog, here it is the end.
    return await stepContext.EndDialogAsync(cancellationToken: cancellationToken);
}
```

# [JavaScript](#tab/javascript)

In this sample, the waterfall dialog is defined within the **bot.js** file.

Define the identifiers to use for the state property accessors, the prompts, and the dialogs.

```javascript
const DIALOG_STATE_PROPERTY = 'dialogState';
const USER_PROFILE_PROPERTY = 'user';

const WHO_ARE_YOU = 'who_are_you';
const HELLO_USER = 'hello_user';

const NAME_PROMPT = 'name_prompt';
const CONFIRM_PROMPT = 'confirm_prompt';
const AGE_PROMPT = 'age_prompt';
```

Define and create the dialog set in the bot's constructor, adding the prompts and the waterfall dialogs to the set.
The `NumberPrompt` includes custom validation to ensure that the user enters an age greater than 0.

```javascript
constructor(conversationState, userState) {
    // Create a new state accessor property. See https://aka.ms/about-bot-state-accessors to learn more about bot state and state accessors.
    this.conversationState = conversationState;
    this.userState = userState;

    this.dialogState = this.conversationState.createProperty(DIALOG_STATE_PROPERTY);

    this.userProfile = this.userState.createProperty(USER_PROFILE_PROPERTY);

    this.dialogs = new DialogSet(this.dialogState);

    // Add prompts that will be used by the main dialogs.
    this.dialogs.add(new TextPrompt(NAME_PROMPT));
    this.dialogs.add(new ChoicePrompt(CONFIRM_PROMPT));
    this.dialogs.add(new NumberPrompt(AGE_PROMPT, async (prompt) => {
        if (prompt.recognized.succeeded) {
            if (prompt.recognized.value <= 0) {
                await prompt.context.sendActivity(`Your age can't be less than zero.`);
                return false;
            } else {
                return true;
            }
        }
        return false;
    }));

    // Create a dialog that asks the user for their name.
    this.dialogs.add(new WaterfallDialog(WHO_ARE_YOU, [
        this.promptForName.bind(this),
        this.confirmAgePrompt.bind(this),
        this.promptForAge.bind(this),
        this.captureAge.bind(this)
    ]));

    // Create a dialog that displays a user name after it has been collected.
    this.dialogs.add(new WaterfallDialog(HELLO_USER, [
        this.displayProfile.bind(this)
    ]));
}
```

Since our dialog step methods reference instance properties, we need to use the `bind` method, so the `this` object resolves correctly within each step method.

In this sample, we define each step as a separate method. You can also define the steps in-line in the constructor using lambda expressions.

```javascript
// This step in the dialog prompts the user for their name.
async promptForName(step) {
    return await step.prompt(NAME_PROMPT, `What is your name, human?`);
}

// This step captures the user's name, then prompts whether or not to collect an age.
async confirmAgePrompt(step) {
    const user = await this.userProfile.get(step.context, {});
    user.name = step.result;
    await this.userProfile.set(step.context, user);
    await step.prompt(CONFIRM_PROMPT, 'Do you want to give your age?', ['yes', 'no']);
}

// This step checks the user's response - if yes, the bot will proceed to prompt for age.
// Otherwise, the bot will skip the age step.
async promptForAge(step) {
    if (step.result && step.result.value === 'yes') {
        return await step.prompt(AGE_PROMPT, `What is your age?`,
            {
                retryPrompt: 'Sorry, please specify your age as a positive number or say cancel.'
            }
        );
    } else {
        return await step.next(-1);
    }
}

// This step captures the user's age.
async captureAge(step) {
    const user = await this.userProfile.get(step.context, {});
    if (step.result !== -1) {
        user.age = step.result;
        await this.userProfile.set(step.context, user);
        await step.context.sendActivity(`I will remember that you are ${ step.result } years old.`);
    } else {
        await step.context.sendActivity(`No age given.`);
    }
    return await step.endDialog();
}

// This step displays the captured information back to the user.
async displayProfile(step) {
    const user = await this.userProfile.get(step.context, {});
    if (user.age) {
        await step.context.sendActivity(`Your name is ${ user.name } and you are ${ user.age } years old.`);
    } else {
        await step.context.sendActivity(`Your name is ${ user.name } and you did not share your age.`);
    }
    return await step.endDialog();
}
```

---

This sample updates the user profile state from within the dialog. This practice can work for a simple bot, but will not work if you want to reuse a dialog across bots.

There are various options for keeping dialog steps and bot state separate. For example, once your dialog gathers complete information, you can:

* Use the _end dialog_ method to provide the collected data as return value back to the parent context. This can be the bot's turn handler or an earlier active dialog on the dialog stack. This is how the prompt classes are designed.
* Generate a request to an appropriate service. This might work well if your bot acts as a front end to a larger service.

## Test your dialog

Build and run your bot locally, then interact with your bot using the [Emulator](../bot-service-debug-emulator.md).

# [C#](#tab/csharp)

1. The bot sends an initial greeting message in response to the conversation update activity in which the user is added to the conversation.
1. Enter `hi` or other input. Since there is not yet an active dialog this turn, the bot starts the `details` dialog.
   * The bot sends the first prompt of the dialog and waits for more input.
1. Answer questions as the bot asks them, progressing through the dialog.
1. The last step of the dialog sends a `Thanks` message, based on your inputs.
   * When the dialog ends, it's removed from the dialog stack, and the bot no longer has an active dialog.
1. Enter `hi` or other input to start the dialog again.

# [JavaScript](#tab/javascript)

1. The bot sends an initial greeting message in response to the conversation update activity in which the user is added to the conversation.
1. Enter `hi` or other input. Since there is not yet an active dialog this turn and no user profile yet, the bot starts the `who_are_you` dialog.
   * The bot sends the first prompt of the dialog and waits for more input.
1. Answer questions as the bot asks them, progressing through the dialog.
1. The last step of the dialog sends a brief confirmation message.
1. Enter `hi` or other input.
   * The bot starts the one-step `hello_user` dialog, which displays information from the collected data and immediately ends.

---

## Next steps

> [!div class="nextstepaction"]
> [Create advance conversation flow using branches and loops](bot-builder-dialog-manage-complex-conversation-flow.md)
