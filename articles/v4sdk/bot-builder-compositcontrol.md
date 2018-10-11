---
title: Create an integrated set of dialogs | Microsoft Docs
description: Learn how to modularize your bot logic using dialog container in the Bot Builder SDK for Node.js and C#.
keywords: composite control, modular bot logic
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 10/08/2018
monikerRange: 'azure-bot-service-4.0'
---

# Create an integrated set of dialogs

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

Imagine that you are creating a hotel bot that handles multiple tasks such as greeting the user, reserving a dinner table, ordering food, setting an alarm, displaying the current weather and many others. You can handle each of these tasks within your bot using one dialog object; however, this can make your dialog code too large and cluttered.

You can turn portions of your conversation flow into component dialogs, breaking up a large dialog set into more manageable pieces. This can streamline your code and make it easier to debug and allow multiple teams to simultaneously work on the bot.
You can also create a library of component dialogs for reuse in other dialog sets and bots.

In this example we will be creating a hotel bot that combines check-in, wake-up, and reserve-table component dialogs.

This article builds on information about managing [simple](bot-builder-dialog-manage-conversation-flow.md) and [complex](bot-builder-dialog-manage-complex-conversation-flow.md) conversation flows.

## Create your project

We start with the echo bot template and include the dialogs library in the project.

# [C#](#tab/csharp)

We'll start from the **EchoBot** template. For instructions, see the [quickstart for .NET](../dotnet/bot-builder-dotnet-sdk-quickstart.md).

To use dialogs, install the `Microsoft.Bot.Builder.Dialogs` NuGet package for your project or solution.
Then reference the dialogs library in using statements in your code files as necessary.

Rename the **EchoBotWithCounter.cs** file to **HotelBot.cs** and  rename the class to **HotelBot**.

# [JavaScript](#tab/javascript)

We'll start from the **Echo** template. For instructions, see the [quickstart for JavaScript](../javascript/bot-builder-javascript-quickstart.md).

The `botbuilder-dialogs` library can be downloaded from npm. To install the `botbuilder-dialogs` library, run the following npm command:

```cmd
npm install --save botbuilder-dialogs
```

---

## Managing state

There are many ways to set up state management for a bot that uses composite dialogs. In this bot, each component dialog will return an object as the dialog result. The calling context manages the returned values. For more information about state management, see [Save state using conversation and user properties](bot-builder-howto-v4-state.md).

# [C#](#tab/csharp)

Each dialog will collect some information, and the bot's turn handler or main menu will save this information to the user state. We'll define component dialogs for check in, table reservation, and setting an alarm. Each one will return an object of an appropriate class. Add each of the following classes to your project as a new C# class module.

```csharp
/// <summary>
/// User state information.
/// </summary>
public class UserInfo
{
    public GuestInfo Guest { get; set; }
    public TableInfo Table { get; set; }
    public WakeUpInfo WakeUp { get; set; }
}

/// <summary>
/// State information associated with the check-in dialog.
/// </summary>
public class GuestInfo
{
    public string Name { get; set; }
    public string Room { get; set; }
}

/// <summary>
/// State information associated with the reserve-table dialog.
/// </summary>
public class TableInfo
{
    public string Number { get; set; }
}

/// <summary>
/// State information associated with the wake-up call dialog.
/// </summary>
public class WakeUpInfo
{
    public string Time { get; set; }
}
```

Rename **EchoBotAccessors.cs** to **BotAccessors.cs** and update the class name **BotAccessors**.

Then, update the file using this code. We need accessors for both dialog state and for the user's information we collect.

```csharp
using System;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

/// <summary>
/// Contains the state objects and the state property accessors for the bot.
/// </summary>
public class BotAccessors
{
    // The property accessor keys to use.
    public const string UserInfoAccessorName = "HotelBot.UserInfo";
    public const string DialogStateAccessorName = "HotelBot.DialogState";

    /// <summary>
    /// Initializes a new instance of the <see cref="BotAccessors"/> class.
    /// Contains the <see cref="ConversationState"/> and associated <see cref="IStatePropertyAccessor{T}"/>.
    /// </summary>
    /// <param name="conversationState">The state object that stores the counter.</param>
    public BotAccessors(ConversationState conversationState, UserState userState)
    {
        ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
        UserState = userState ?? throw new ArgumentNullException(nameof(userState));
    }

    /// <summary>Gets or sets the state property accessor for the user information we're tracking.</summary>
    /// <value>Accessor for user information.</value>
    public IStatePropertyAccessor<UserInfo> UserInfoAccessor { get; set; }

    /// <summary>Gets or sets the state property accessor for the dialog state.</summary>
    /// <value>Accessor for the dialog state.</value>
    public IStatePropertyAccessor<DialogState> DialogStateAccessor { get; set; }

    /// <summary>Gets the conversation state for the bot.</summary>
    /// <value>The conversation state for the bot.</value>
    public ConversationState ConversationState { get; }

    /// <summary>Gets the user state for the bot.</summary>
    /// <value>The user state for the bot.</value>
    public UserState UserState { get; }
}
```

In the **Startup.cs** file, update the code in the `ConfigureServices` method that sets up state and the bot's state property accessors.

```csharp
using Microsoft.Bot.Builder.Dialogs;

public void ConfigureServices(IServiceCollection services)
{
    services.AddBot<HotelBot>(options =>
    {
        //...

        // The Memory Storage used here is for local bot debugging only. When the bot
        // is restarted, everything stored in memory will be gone.
        IStorage dataStore = new MemoryStorage();

        // Create conversation and user state objects.
        options.State.Add(new ConversationState(dataStore));
        options.State.Add(new UserState(dataStore));
    });

    // Create and register state accessors.
    // Accessors created here are passed into the IBot-derived class on every turn.
    services.AddSingleton<BotAccessors>(sp =>
    {
        var options = sp.GetRequiredService<IOptions<BotFrameworkOptions>>().Value;
        var conversationState = options.State.OfType<ConversationState>().FirstOrDefault();
        var userState = options.State.OfType<UserState>().FirstOrDefault();

        // Create the custom state accessor.
        // State accessors enable other components to read and write individual properties of state.
        var accessors = new BotAccessors(conversationState, userState)
        {
            UserInfoAccessor = userState.CreateProperty<UserInfo>(BotAccessors.UserInfoAccessorName),
            DialogStateAccessor = conversationState.CreateProperty<DialogState>(BotAccessors.DialogStateAccessorName),
        };

        return accessors;
    });
}
```

# [JavaScript](#tab/javascript)

Each dialog will collect some information, and the bot's turn handler or main menu will save this information to the user state. We'll define component dialogs for check in, table reservation, and setting an alarm. Each one will return an object with appropriate properties. We will aggregate these properties in the bot's user state.

In your **bot.js** file, update your bot constructor to create state property accessors to track user state and dialog state.

```javascript
// Define the identifiers for our state property accessors.
const DIALOG_STATE_PROPERTY = 'dialogStatePropertyAccessor';
const USER_INFO_PROPERTY = 'userInfoPropertyAccessor';

constructor(conversationState, userState) {
    // Record the conversation and user state management objects.
    this.conversationState = conversationState;
    this.userState = userState;

    // Create our state property accessors.
    this.dialogStateAccessor = conversationState.createProperty(DIALOG_STATE_PROPERTY);
    this.userInfoAccessor = userState.createProperty(USER_INFO_PROPERTY);
}
```

In your **index.js** file, update the imported classes from the `botbuilder` library and the code that creates the state objects and your bot:

```javascript
// Import required bot services.
const { BotFrameworkAdapter, MemoryStorage, ConversationState, UserState } = require('botbuilder');
```

```javascript
// Define state store for your bot.
const memoryStorage = new MemoryStorage();

// Create conversation and user state with in-memory storage provider.
const conversationState = new ConversationState(memoryStorage);
const userState = new UserState(memoryStorage);

// Create the bot.
const myBot = new MyBot(conversationState, userState);
```

---

## Define the check-in component dialog

First, we start with a simple check-in dialog that will ask the user for their name and what room they will be staying in. We create a `CheckInDialog` class that extends `ComponentDialog`. This class has a constructor that defines the name of the root dialog, which we'll define as a waterfall dialog with three steps.

These are the steps for the check-in dialog.

1. Ask for the guest's name.
1. Ask for the room they'd like to stay in.
1. Send a confirmation message and complete the dialog.

# [C#](#tab/csharp)

Add a `CheckInDialog` class to your project, using the code below.

Throughout this dialog we can write to a local state object, accessible through the `Values` property of the step context. When the dialog completes, the local state object is be disposed of. Therefore, we return a value from the dialog that contains the guest info.

```csharp
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

public class CheckInDialog : ComponentDialog
{
    private const string GuestKey = nameof(CheckInDialog);
    private const string TextPrompt = "textPrompt";

    // You can start this from the parent using the dialog's ID.
    public CheckInDialog(string id)
        : base(id)
    {
        InitialDialogId = Id;

        // Define the prompts used in this conversation flow.
        AddDialog(new TextPrompt(TextPrompt));

        // Define the conversation flow using a waterfall model.
        WaterfallStep[] waterfallSteps = new WaterfallStep[]
        {
            NameStepAsync,
            RoomStepAsync,
            FinalStepAsync,
        };
        AddDialog(new WaterfallDialog(Id, waterfallSteps));
    }

    private static async Task<DialogTurnResult> NameStepAsync(
        WaterfallStepContext step,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        // Clear the guest information and prompt for the guest's name.
        step.Values[GuestKey] = new GuestInfo();
        return await step.PromptAsync(
            TextPrompt,
            new PromptOptions
            {
                Prompt = MessageFactory.Text("What is your name?"),
            },
            cancellationToken);
    }

    private static async Task<DialogTurnResult> RoomStepAsync(
        WaterfallStepContext step,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        // Save the name and prompt for the room number.
        string name = step.Result as string;
        ((GuestInfo)step.Values[GuestKey]).Name = name;
        return await step.PromptAsync(
            TextPrompt,
            new PromptOptions
            {
                Prompt = MessageFactory.Text($"Hi {name}. What room will you be staying in?"),
            },
            cancellationToken);
    }

    private static async Task<DialogTurnResult> FinalStepAsync(
        WaterfallStepContext step,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        // Save the room number and "sign off".
        string room = step.Result as string;
        ((GuestInfo)step.Values[GuestKey]).Room = room;

        await step.Context.SendActivityAsync(
            "Great, enjoy your stay!",
            cancellationToken: cancellationToken);

        // End the dialog, returning the guest info.
        return await step.EndDialogAsync(
            (GuestInfo)step.Values[GuestKey],
            cancellationToken);
    }
}
```

# [JavaScript](#tab/javascript)

Create a **checkInDialog.js** file, and add to it a `CheckInDialog` class that extends `ComponentDialog`.

Throughout this dialog we can write to a local state object, accessible through the `values` property of the step context. When the dialog completes, the local state object is be disposed of. Therefore, we return a value from the dialog that contains the guest info.

```JavaScript
const { ComponentDialog, TextPrompt, WaterfallDialog } = require('botbuilder-dialogs');

class CheckInDialog extends ComponentDialog {
    constructor(dialogId) {
        super(dialogId);

        // ID of the child dialog that should be started anytime the component is started.
        this.initialDialogId = dialogId;

        // Define the prompts used in this conversation flow.
        this.addDialog(new TextPrompt('textPrompt'));

        // Define the conversation flow using a waterfall model.
        this.addDialog(new WaterfallDialog(dialogId, [
            async function (step) {
                // Clear the guest information and prompt for the guest's name.
                step.values.guestInfo = {};
                return await step.prompt('textPrompt', "What is your name?");
            },
            async function (step) {
                // Save the name and prompt for the room number.
                step.values.guestInfo.userName = step.result;
                return await step.prompt('textPrompt', `Hi ${step.result}. What room will you be staying in?`);
            },
            async function (step) {
                // Save the room number and "sign off".
                step.values.guestInfo.roomNumber = step.result;
                await step.context.sendActivity(`Great! Enjoy your stay in room ${step.result}!`);

                // End the dialog, returning the guest info.
                return await step.endDialog(step.values.guestInfo);
            }
        ]));
    }
}

exports.CheckInDialog = CheckInDialog;
```

---

## Define the reserve-table and wake-up component dialogs

One benefit of using a component dialog is the ability to use differing dialogs together. Since each `DialogSet` maintains an exclusive set of `dialogs`, sharing or cross referencing `dialogs` cannot be done easily. This is where the component dialog comes in. You can encapsulate complex or intricate aspects of conversation flow in component dialogs, which can make dialog management and maintenance easier. We'll create the other two component dialogs: one to ask the user which table they would like to reserve for dinner, and one to create a wake up call. Again, we'll use a separate class for each dialog, and each dialog will extend the main `ComponentDialog`.

We'll design these to accept guest information in the dialog options when they are started.

These are the steps for the reserve-table dialog.

1. Ask for the table to reserve.
1. Send a confirmation message and complete the dialog, returning the table number.

These are the steps for the wake-up dialog.

1. Ask for the wake-up time to set for them.
1. Send a confirmation message and complete the dialog, returning the alarm time.

# [C#](#tab/csharp)

Add a `ReserveTableDialog` class to your project, using the code below.

We'll get the guest's name from the options object that is passed in when the dialog is started. Then, we'll return a value from the dialog, one that contains the table number this time.

```csharp
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;

public class ReserveTableDialog : ComponentDialog
{
    private const string TablePrompt = "choicePrompt";

    public ReserveTableDialog(string id)
        : base(id)
    {
        InitialDialogId = Id;

        // Define the prompts used in this conversation flow.
        AddDialog(new ChoicePrompt(TablePrompt));

        // Define the conversation flow using a waterfall model.
        WaterfallStep[] waterfallSteps = new WaterfallStep[]
        {
                TableStepAsync,
                FinalStepAsync,
        };
        AddDialog(new WaterfallDialog(Id, waterfallSteps));
    }

    private static async Task<DialogTurnResult> TableStepAsync(
        WaterfallStepContext step,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        string greeting = step.Options is GuestInfo guest
                && !string.IsNullOrWhiteSpace(guest?.Name)
                ? $"Welcome {guest.Name}" : "Welcome";

        string prompt = $"{greeting}, How many diners will be at your table?";
        string[] choices = new string[] { "1", "2", "3", "4", "5", "6" };
        return await step.PromptAsync(
            TablePrompt,
            new PromptOptions
            {
                Prompt = MessageFactory.Text(prompt),
                Choices = ChoiceFactory.ToChoices(choices),
            },
            cancellationToken);
    }

    private static async Task<DialogTurnResult> FinalStepAsync(
        WaterfallStepContext step,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        // ChoicePrompt returns a FoundChoice object.
        string table = (step.Result as FoundChoice).Value;

        // Send a confirmation message.
        await step.Context.SendActivityAsync(
            $"Sounds great;  we will reserve a table for you for {table} diners.",
            cancellationToken: cancellationToken);

        // End the dialog, returning the table info.
        return await step.EndDialogAsync(
            new TableInfo { Number = table },
            cancellationToken);
    }
}
```

Add a `SetAlarmDialog` class to your project, using the code below.

We'll get the guest's room number from the options object that is passed in when the dialog is started. Then, we'll return a value from the dialog, one that contains the time they want a wake up call set for.

```csharp
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

public class SetAlarmDialog : ComponentDialog
{
    private const string AlarmPrompt = "dateTimePrompt";

    public SetAlarmDialog(string id)
        : base(id)
    {
        InitialDialogId = Id;

        // Define the prompts used in this conversation flow.
        // Ideally, we'd add validation to this prompt.
        AddDialog(new DateTimePrompt(AlarmPrompt));

        // Define the conversation flow using a waterfall model.
        WaterfallStep[] waterfallSteps = new WaterfallStep[]
        {
                AlarmStepAsync,
                FinalStepAsync,
        };

        AddDialog(new WaterfallDialog(Id, waterfallSteps));
    }

    private static async Task<DialogTurnResult> AlarmStepAsync(
        WaterfallStepContext step,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        string greeting = step.Options is GuestInfo guest
                && !string.IsNullOrWhiteSpace(guest?.Name)
                ? $"Hi {guest.Name}" : "Hi";

        string prompt = $"{greeting}. When would you like your alarm set for?";
        return await step.PromptAsync(
            AlarmPrompt,
            new PromptOptions { Prompt = MessageFactory.Text(prompt) },
            cancellationToken);
    }

    private static async Task<DialogTurnResult> FinalStepAsync(
        WaterfallStepContext step,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        // Ambiguous responses can generate multiple results.
        var resolution = (step.Result as IList<DateTimeResolution>)?.FirstOrDefault();

        // Time ranges have a start and no value.
        var alarm = resolution.Value ?? resolution.Start;
        string roomNumber = (step.Options as GuestInfo)?.Room;

        // Send a confirmation message.
        await step.Context.SendActivityAsync(
            $"Your alarm is set to {alarm} for room {roomNumber}.",
            cancellationToken: cancellationToken);

        // End the dialog, returning the alarm info.
        return await step.EndDialogAsync(
            new WakeUpInfo { Time = alarm },
            cancellationToken);
    }
}
```

# [JavaScript](#tab/javascript)

Create a **reserveTableDialog.js** file, and add to it a `ReserveTableDialog` class that extends `ComponentDialog`.

We'll get the guest's name from the options object that is passed in when the dialog is started. Then, we'll return a value from the dialog, one that contains the table number this time.

```JavaScript
const { ComponentDialog, ChoicePrompt, WaterfallDialog } = require('botbuilder-dialogs');

class ReserveTableDialog extends ComponentDialog {
    constructor(dialogId) {
        super(dialogId);

        // ID of the child dialog that should be started anytime the component is started.
        this.initialDialogId = dialogId;

        // Define the prompts used in this conversation flow.
        this.addDialog(new ChoicePrompt('choicePrompt'));

        // Define the conversation flow using a waterfall model.
        this.addDialog(new WaterfallDialog(dialogId, [
            async function (step) {
                // Welcome the user and ask for their table preference.
                const greeting = step.options && step.options.userName ? `Welcome ${step.options.userName}` : `Welcome`;

                const promptOptions = {
                    prompt: `${greeting}, How many diners will be at your table?`,
                    reprompt: 'That was not a valid choice, please select a table size between 1 and 6 guests.',
                    choices: ['1', '2', '3', '4', '5', '6']
                };
                return await step.prompt('choicePrompt', promptOptions);
            },
            async function (step) {
                const choice = step.result;

                // Send a confirmation message.
                const tableNumber = choice.value;
                await step.context.sendActivity(`Sounds great, we will reserve a table for you for ${tableNumber} diners.`);

                // End the dialog, returning the table info.
                return await step.endDialog({ table: tableNumber });
            }
        ]));
    }
}

exports.ReserveTableDialog = ReserveTableDialog;
```

Create a **setAlarmDialog.js** file, and add to it a `SetAlarmDialog` class that extends `ComponentDialog`.

We'll get the guest's room number from the options object that is passed in when the dialog is started. Then, we'll return a value from the dialog, one that contains the time they want a wake up call set for.

```JavaScript
const { ComponentDialog, DateTimePrompt, WaterfallDialog } = require('botbuilder-dialogs');

class SetAlarmDialog extends ComponentDialog {
    constructor(dialogId) {
        super(dialogId);

        // ID of the child dialog that should be started anytime the component is started.
        this.initialDialogId = dialogId;

        // Define the prompts used in this conversation flow.
        this.dialogs.add(new DateTimePrompt('datePrompt'));

        this.dialogs.add(new WaterfallDialog(dialogId, [
            async function (step) {
                step.values.wakeUp = {};
                if (step.options && step.options.roomNumber) {
                    step.values.roomNumber = step.options.roomNumber;
                }

                const greeting = step.options && step.options.userName ? `Hi ${step.options.userName}` : `Hi`;
                return await step.prompt('datePrompt', `${greeting}. What time would you like your alarm to be set?`);
            },
            async function (step) {
                // Ambiguous responses can generate multiple results.
                const resolution = step.result[0];

                // Time ranges have a start and no value.
                const alarm = resolution.value ? resolution.value : resolution.start;
                const roomNumber = step.values.roomNumber;

                // Send a confirmation message.
                await step.context.sendActivity(`Your alarm is set to ${alarm} for room ${roomNumber}.`);

                // End the dialog, returning the alarm info.
                return await step.endDialog({ alarm: alarm });
            }]));

        // Defining the prompt used in this conversation flow
    }
}

exports.SetAlarmDialog = SetAlarmDialog;
```

---

## Add the component dialogs to the bot

Now that we've defined our three component dialogs, we can use them in our bot.

- All three dialogs are added to our bot's main dialog set.
- When a new conversation starts, we don't have an active dialog and the bot's on turn logic takes over.
- If we don't have guest information for the user, we start the check-in dialog.
- Once we have guest information, the main dialog takes over, repeatedly allowing the user to start the reserve-table or wake-up dialog.

We'll update the logic for the bot's on-turn handler.

1. Get user state.
1. Continue any active dialog.
1. If a dialog completed this turn, it should be the check-in dialog.
   1. Store the guest information.
   1. Start the main dialog.
1. If the bot has not sent a message to the user yet, then there is no active dialog running.
    1. If we don't have the guest's information, start the check-in dialog.
    1. Otherwise, start the main dialog.
1. Save any state changes that might have occurred.

These are the steps for the main dialog.

1. Ask the guest what they'd like to do: reserve a table or set a wake up call.
1. Start the corresponding child dialog, or send an _input not understood_ message and start over from the first step.
1. Process the return value from the child dialog and restart the main dialog.

# [C#](#tab/csharp)

In the **HotelBot.cs** file update the using statements.

```csharp
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
```

Update the initialization code and define the bot's dialog set and main dialog.

```csharp
// Define the IDs for the dialogs in the bot's dialog set.
private const string MainDialogId = "mainDialog";
private const string CheckInDialogId = "checkInDialog";
private const string TableDialogId = "tableDialog";
private const string AlarmDialogId = "alarmDialog";

// Define the dialog set for the bot.
private readonly DialogSet _dialogs;

// Define the state accessors and the logger for the bot.
private readonly BotAccessors _accessors;
private readonly ILogger _logger;

/// <summary>
/// Initializes a new instance of the <see cref="HotelBot"/> class.
/// </summary>
/// <param name="accessors">Contains the objects to use to manage state.</param>
/// <param name="loggerFactory">A <see cref="ILoggerFactory"/> that is hooked to the Azure App Service provider.</param>
public HotelBot(BotAccessors accessors, ILoggerFactory loggerFactory)
{
    if (loggerFactory == null)
    {
        throw new System.ArgumentNullException(nameof(loggerFactory));
    }

    _logger = loggerFactory.CreateLogger<HotelBot>();
    _logger.LogTrace($"{nameof(HotelBot)} turn start.");
    _accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));

    // Define the steps of the main dialog.
    WaterfallStep[] steps = new WaterfallStep[]
    {
        MenuStepAsync,
        HandleChoiceAsync,
        LoopBackAsync,
    };

    // Create our bot's dialog set, adding a main dialog and the three component dialogs.
    _dialogs = new DialogSet(_accessors.DialogStateAccessor)
        .Add(new WaterfallDialog(MainDialogId, steps))
        .Add(new CheckInDialog(CheckInDialogId))
        .Add(new ReserveTableDialog(TableDialogId))
        .Add(new SetAlarmDialog(AlarmDialogId));
}

private static async Task<DialogTurnResult> MenuStepAsync(
    WaterfallStepContext stepContext,
    CancellationToken cancellationToken = default(CancellationToken))
{
    // Present the user with a set of "suggested actions".
    List<string> menu = new List<string> { "Reserve Table", "Wake Up" };
    await stepContext.Context.SendActivityAsync(
        MessageFactory.SuggestedActions(menu, "How can I help you?"),
        cancellationToken: cancellationToken);
    return Dialog.EndOfTurn;
}

private async Task<DialogTurnResult> HandleChoiceAsync(
    WaterfallStepContext stepContext,
    CancellationToken cancellationToken = default(CancellationToken))
{
    // Get the user's info. (Since the type factory is null, this will throw if state does not yet have a value for user info.)
    UserInfo userInfo = await _accessors.UserInfoAccessor.GetAsync(stepContext.Context, null, cancellationToken);

    // Check the user's input and decide which dialog to start.
    // Pass in the guest info when starting either of the child dialogs.
    string choice = (stepContext.Result as string)?.Trim()?.ToLowerInvariant();
    switch (choice)
    {
        case "reserve table":
            return await stepContext.BeginDialogAsync(TableDialogId, userInfo.Guest, cancellationToken);

        case "wake up":
            return await stepContext.BeginDialogAsync(AlarmDialogId, userInfo.Guest, cancellationToken);

        default:
            // If we don't recognize the user's intent, start again from the beginning.
            await stepContext.Context.SendActivityAsync(
                "Sorry, I don't understand that command. Please choose an option from the list.");
            return await stepContext.ReplaceDialogAsync(MainDialogId, null, cancellationToken);
    }
}

private async Task<DialogTurnResult> LoopBackAsync(
    WaterfallStepContext stepContext,
    CancellationToken cancellationToken = default(CancellationToken))
{
    // Get the user's info. (Because the type factory is null, this will throw if state does not yet have a value for user info.)
    UserInfo userInfo = await _accessors.UserInfoAccessor.GetAsync(stepContext.Context, null, cancellationToken);

    // Process the return value from the child dialog.
    switch (stepContext.Result)
    {
        case TableInfo table:
            // Store the results of the reserve-table dialog.
            userInfo.Table = table;
            await _accessors.UserInfoAccessor.SetAsync(stepContext.Context, userInfo, cancellationToken);
            break;
        case WakeUpInfo alarm:
            // Store the results of the set-wake-up-call dialog.
            userInfo.WakeUp = alarm;
            await _accessors.UserInfoAccessor.SetAsync(stepContext.Context, userInfo, cancellationToken);
            break;
        default:
            // We shouldn't get here, since these are no other branches that get this far.
            break;
    }

    // Restart the main menu dialog.
    return await stepContext.ReplaceDialogAsync(MainDialogId, null, cancellationToken);
}
```

And, update the bot's turn handler to use the dialog set.

```csharp
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
{
    if (turnContext.Activity.Type == ActivityTypes.Message)
    {
        // Establish dialog state from the conversation state.
        DialogContext dc = await _dialogs.CreateContextAsync(turnContext, cancellationToken);

        // Get the user's info.
        UserInfo userInfo = await _accessors.UserInfoAccessor.GetAsync(turnContext, () => new UserInfo(), cancellationToken);

        // Continue any current dialog.
        DialogTurnResult dialogTurnResult = await dc.ContinueDialogAsync();

        // Process the result of any complete dialog.
        if (dialogTurnResult.Status is DialogTurnStatus.Complete)
        {
            switch (dialogTurnResult.Result)
            {
                case GuestInfo guestInfo:
                    // Store the results of the check-in dialog.
                    userInfo.Guest = guestInfo;
                    await _accessors.UserInfoAccessor.SetAsync(turnContext, userInfo, cancellationToken);
                    break;
                default:
                    // We shouldn't get here, since the main dialog is designed to loop.
                    break;
            }
        }

        // Every dialog step sends a response, so if no response was sent,
        // then no dialog is currently active.
        else if (!turnContext.Responded)
        {
            if (string.IsNullOrEmpty(userInfo.Guest?.Name))
            {
                // If we don't yet have the guest's info, start the check-in dialog.
                await dc.BeginDialogAsync(CheckInDialogId, null, cancellationToken);
            }
            else
            {
                // Otherwise, start our bot's main dialog.
                await dc.BeginDialogAsync(MainDialogId, null, cancellationToken);
            }
        }

        // Save the new turn count into the conversation state.
        await _accessors.ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
        await _accessors.UserState.SaveChangesAsync(turnContext, false, cancellationToken);
    }
    else
    {
        await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
    }
}
```

# [JavaScript](#tab/javascript)

In our bot file, **bot.js**, we need to import not just the classes we'll use from the SDK, but also the classes we defined for our component dialogs.

```JavaScript
const { ActivityTypes, MessageFactory } = require('botbuilder');
const { DialogSet, WaterfallDialog, Dialog, DialogTurnStatus } = require('botbuilder-dialogs');

// Import our component dialogs.
const { CheckInDialog } = require("./checkInDialog");
const { ReserveTableDialog } = require("./reserveTableDialog");
const { SetAlarmDialog } = require("./setAlarmDialog");
```

We'll also need to create a dialog set and add all the dialogs we'll be using to it.

We're defining the waterfall steps of the main dialog as functions in the class, instead of in-line. We are using `bind()` on these functions so that from within the function, `this` resolves correctly.

Here's our updated bot constructor.

```JavaScript
constructor(conversationState, userState) {
    // Record the conversation and user state management objects.
    this.conversationState = conversationState;
    this.userState = userState;

    // Create our state property accessors.
    this.dialogStateAccessor = conversationState.createProperty(DIALOG_STATE_PROPERTY);
    this.userInfoAccessor = userState.createProperty(USER_INFO_PROPERTY);

    // Create our bot's dialog set, adding a main dialog and the three component dialogs.
    this.dialogs = new DialogSet(this.dialogStateAccessor)
        .add(new CheckInDialog('checkInDialog'))
        .add(new ReserveTableDialog('reserveTableDialog'))
        .add(new SetAlarmDialog('setAlarmDialog'))
        .add(new WaterfallDialog('mainDialog', [
            this.promptForChoice.bind(this),
            this.startChildDialog.bind(this),
            this.saveResult.bind(this)
    ]));
}
```

Below the bot constructor, add the following code that implements steps for the main dialog.

```JavaScript
async promptForChoice(step) {
    const menu = ["Reserve Table", "Wake Up"];
    await step.context.sendActivity(MessageFactory.suggestedActions(menu, 'How can I help you?'));
    return Dialog.EndOfTurn;
}

async startChildDialog(step) {
    // Get the user's info.
    const user = await this.userInfoAccessor.get(step.context);
    // Check the user's input and decide which dialog to start.
    // Pass in the guest info when starting either of the child dialogs.
    switch (step.result) {
        case "Reserve Table":
            return await step.beginDialog('reserveTableDialog', user.guestInfo);
            break;
        case "Wake Up":
            return await step.beginDialog('setAlarmDialog', user.guestInfo);
            break;
        default:
            await step.context.sendActivity("Sorry, I don't understand that command. Please choose an option from the list.");
            return await step.replaceDialog('mainDialog');
            break;
    }
}

async saveResult(step) {
    // Process the return value from the child dialog.
    if (step.result) {
        const user = await this.userInfoAccessor.get(step.context);
        if (step.result.table) {
            // Store the results of the reserve-table dialog.
            user.table = step.result.table;
        } else if (step.result.alarm) {
            // Store the results of the set-wake-up-call dialog.
            user.alarm = step.result.alarm;
        }
        await this.userInfoAccessor.set(step.context, user);
    }
    // Restart the main menu dialog.
    return await step.replaceDialog('mainDialog'); // Show the menu again
}
```

And now update your bot's turn handler:

```JavaScript
async onTurn(turnContext) {
    if (turnContext.activity.type === ActivityTypes.Message) {
        const user = await this.userInfoAccessor.get(turnContext, {});
        const dc = await this.dialogs.createContext(turnContext);
        const dialogTurnResult = await dc.continueDialog();
        if (dialogTurnResult.status === DialogTurnStatus.complete) {
            user.guestInfo = dialogTurnResult.result;
            await this.userInfoAccessor.set(turnContext, user);
            await dc.beginDialog('mainDialog');
        } else if (!turnContext.responded) {
            if (!user.guestInfo) {
                await dc.beginDialog('checkInDialog');
            } else {
                await dc.beginDialog('mainDialog');
            }
        }
        // Save state changes
        await this.conversationState.saveChanges(turnContext);
        await this.userState.saveChanges(turnContext);
    }
}
```

---

As you can see, the component dialogs are added to the bot's main dialog in a fashion similar to how you add [prompts](bot-builder-prompts.md) to a dialog. You can add as many child dialogs to your main dialog as you want. Each module would add additional capabilities and services that the bot can offer to your users.

## Next steps

Now that you know how to use component dialogs, let's take a look at how to use Language Understanding (LUIS) to help your bot decide when to begin the dialogs.

> [!div class="nextstepaction"]
> [Use LUIS for Language Understanding](./bot-builder-howto-v4-luis.md)
