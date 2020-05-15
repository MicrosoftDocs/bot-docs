---
title: Create a bot using adaptive, component, and waterfall dialogs - Bot Service
description: Learn how to manage a conversation flow with conventional and adaptive in the Bot Framework SDK.
keywords: conversation flow, dialogs, component dialogs, custom dialogs, waterfall dialogs, adaptive dialogs
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/07/2020
monikerRange: 'azure-bot-service-4.0'
---

# Create a bot using adaptive, component, waterfall, and custom dialogs

[!INCLUDE[applies-to](../includes/applies-to.md)]

All dialogs derive from a base _dialog_ class.
If you use the _dialog manager_ to run your root dialog, all of the dialog classes can work together.
This article shows how to use component, waterfall, custom, and adaptive dialogs together in one bot.

This article focuses on the code that allows these dialogs to work together. See the [additional information](#additional-information) for articles that cover each type of dialog in more detail.

## Prerequisites

- Knowledge of [bot basics][bot-basics], [managing state][concept-state], the [dialogs library][about-dialogs], and [adaptive dialogs][about-adaptive-dialogs].

<!--
- A copy of the **waterfall or custom dialog with adaptive** sample in either [**C#**][cs-sample], [**JavaScript** (preview)][js-sample]
-->

### Preliminary steps to add an adaptive dialog to a bot

You must follow the steps described below to add an adaptive dialog to a bot.
These steps are covered in more detail in how to [create a bot using adaptive dialogs][basic-adaptive-how-to].

#### [C#](#tab/csharp)

1. Update all Bot Builder NuGet packages to version 4.9.x.
1. Add the `Microsoft.Bot.Builder.Dialogs.Adaptive` package to your bot project.
1. Update the the bot adapter to add storage and the user and conversation state objects to every turn context.
1. Use a dialog manager in the bot code to start or continue the root dialog each turn.

#### [JavaScript](#tab/javascript)

1. Update all Bot Builder npm packages to version 4.9.x.
1. Add the `botbuilder-dialogs-adaptive` package to your bot project.
1. In the bot's on-turn handler:
   1. Create a dialog manager.
   1. Set the dialog manager's storage and user and conversation state properties.
   1. Use the dialog manager to start or continue the root dialog.

---

## About the sample

By way of illustration, this sample combines various dialog types together in one bot.
It does not demonstrate best practices for designing conversation flow.
The sample:

- Defines a custom _slot filling_ dialog class.
- Creates a root component dialog:
  - A waterfall dialog manages the top-level conversation flow.
  - Together, an adaptive dialog and 2 custom slot filling dialogs manage the rest of the conversation flow.

> [!div class="mx-imgBorder"]
> ![dialog flow](media/adaptive-mixed-dialogs-flow.png)

The custom slot-filling dialogs accept a list of properties (slots to fill). Each custom dialog will prompt for any missing values until all of the slots are filled.
The sample _binds_ a property to the adaptive dialog to allow the adaptive dialog to also fill slots.

This article focuses on how the various dialog types work together.
For information about configuring your bot to use adaptive dialogs, see how to [create a bot using adaptive dialogs][basic-adaptive-how-to].
For more on using adaptive dialogs to gather user input, see [about inputs in adaptive dialogs][about-input-dialogs].

## The custom slot-filling dialogs

A custom dialog is any dialog that derives from one of the dialogs classes in the SDK and overrides one or more of the basic dialog methods: _begin dialog_, _continue dialog_, _resume dialog_, or _end dialog_.

When you create the slot-filling dialog, you provide a list of definitions for the _slots_ the dialog will fill.
The dialog overrides the begin, continue, and resume dialog methods to iteratively prompt the user to fill each slot.
When all the slots are filled, the dialog ends and returns the collected information.

Each slot definition includes the name of the dialog prompt with which to collect the information.

The root dialog creates 2 slot-filling dialogs, one to collect the user's full name, and one to collect their address. It also creates the _text prompt_ that these two dialogs use to fill their slots.

### [C#](#tab/csharp)

**Dialogs\SlotDetails.cs**

The `SlotDetails` class describes the information to collect and the prompt with which to collect it.

<!-- [!code-csharp[slot details](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/04.waterfall-or-custom-dialog-with-adaptive/Dialogs/SlotDetails.cs?range=9-39)]
-->

```csharp
/// <summary>
/// A list of SlotDetails defines the behavior of our SlotFillingDialog.
/// This class represents a description of a single 'slot'. It contains the name of the property we want to gather
/// and the id of the corresponding dialog that should be used to gather that property. The id is that used when the
/// dialog was added to the current DialogSet. Typically this id is that of a prompt but it could also be the id of
/// another slot dialog.
/// </summary>
public class SlotDetails
{
    public SlotDetails(string name, string dialogId, string prompt = null, string retryPrompt = null)
        : this(name, dialogId, new PromptOptions
        {
            Prompt = MessageFactory.Text(prompt),
            RetryPrompt = MessageFactory.Text(retryPrompt),
        })
    {
    }

    public SlotDetails(string name, string dialogId, PromptOptions options)
    {
        Name = name;
        DialogId = dialogId;
        Options = options;
    }

    public string Name { get; set; }

    public string DialogId { get; set; }

    public PromptOptions Options { get; set; }
}
```

**Dialogs\SlotFillingDialog.cs**

The `SlotFillingDialog` class derives from the base `Dialog` class.

It tracks the values it has collected, which slot it prompted for last, and details for the slots to fill.

<!-- [!code-csharp[slot-filling fields and constructor](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/04.waterfall-or-custom-dialog-with-adaptive/Dialogs/SlotFillingDialog.cs?range=23-37)]
-->

```csharp
// Custom dialogs might define their own custom state.
// Similarly to the Waterfall dialog we will have a set of values in the ConversationState. However, rather than persisting
// an index we will persist the last property we prompted for. This way when we resume this code following a prompt we will
// have remembered what property we were filling.
private const string SlotName = "slot";
private const string PersistedValues = "values";

// The list of slots defines the properties to collect and the dialogs to use to collect them.
private readonly List<SlotDetails> _slots;

public SlotFillingDialog(string dialogId, List<SlotDetails> slots)
    : base(dialogId)
{
    _slots = slots ?? throw new ArgumentNullException(nameof(slots));
}
```

The core logic for collecting missing information is in the `RunPromptAsync` helper method. When all the information has been collected, it ends the dialog and returns the information.

<!-- [!code-csharp[slot-filling RunPromptAsync](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/04.waterfall-or-custom-dialog-with-adaptive/Dialogs/SlotFillingDialog.cs?range=121-151&highlight=23-24,28-29)]
-->

```csharp
/// <summary>
/// This helper function contains the core logic of this dialog. The main idea is to compare the state we have gathered with the
/// list of slots we have been asked to fill. When we find an empty slot we execute the corresponding prompt.
/// </summary>
/// <param name="dialogContext">A handle on the runtime.</param>
/// <param name="cancellationToken">The cancellation token.</param>
/// <returns>A DialogTurnResult indicating the state of this dialog to the caller.</returns>
private Task<DialogTurnResult> RunPromptAsync(DialogContext dialogContext, CancellationToken cancellationToken)
{
    var state = GetPersistedValues(dialogContext.ActiveDialog);

    // Run through the list of slots until we find one that hasn't been filled yet.
    var unfilledSlot = _slots.FirstOrDefault((item) => !state.ContainsKey(item.Name));

    // If we have an unfilled slot we will try to fill it
    if (unfilledSlot != null)
    {
        // The name of the slot we will be prompting to fill.
        dialogContext.ActiveDialog.State[SlotName] = unfilledSlot.Name;

        // If the slot contains prompt text create the PromptOptions.

        // Run the child dialog
        return dialogContext.BeginDialogAsync(unfilledSlot.DialogId, unfilledSlot.Options, cancellationToken);
    }
    else
    {
        // No more slots to fill so end the dialog.
        return dialogContext.EndDialogAsync(state);
    }
}
```

### [JavaScript](#tab/javascript)

**dialogs/slotDetails.js**

The `SlotDetails` class describes the information to collect and the prompt with which to collect it.

<!-- [!code-javascript[slot details](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/04.waterfall-or-custom-dialog-with-adaptive/dialogs/slotDetails.js?range=4-26)]
-->

```javascript
class SlotDetails {
    /**
     * SlotDetails is a small class that defines a "slot" to be filled in a SlotFillingDialog.
     * @param {string} name The field name used to store user's response.
     * @param {string} promptId A unique identifier of a Dialog or Prompt registered on the DialogSet.
     * @param {string} prompt The text of the prompt presented to the user.
     * @param {string} reprompt (optional) The text to present if the user responds with an invalid value.
     */
    constructor(name, promptId, prompt, reprompt) {
        this.name = name;
        this.promptId = promptId;
        if (prompt && reprompt) {
            this.options = {
                prompt: prompt,
                retryPrompt: reprompt
            };
        } else {
            this.options = {
                prompt: prompt
            };
        }
    }
}
```

**dialogs/slotFillingDialog.js**

The `SlotFillingDialog` class extends the base `Dialog` class.

<!-- [!code-javascript[slot-filling constants](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/04.waterfall-or-custom-dialog-with-adaptive/dialogs/slotFillingDialog.js?range=7-12)]

[!code-javascript[slot-filling constructor](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/04.waterfall-or-custom-dialog-with-adaptive/dialogs/slotFillingDialog.js?range=15-30)]
-->

```javascript
// Custom dialogs might define their own custom state.
// Similarly to the Waterfall dialog we will have a set of values in the ConversationState. However, rather than persisting
// an index we will persist the last property we prompted for. This way when we resume this code following a prompt we will
// have remembered what property we were filling.
const SlotName = 'slot';
const PersistedValues = 'values';

/**
 * This is an example of implementing a custom Dialog class. This is similar to the Waterfall dialog in the framework;
 * however, it is based on a Dictionary rather than a sequential set of functions. The dialog is defined by a list of 'slots',
 * each slot represents a property we want to gather and the dialog we will be using to collect it. Often the property
 * is simply an atomic piece of data such as a number or a date. But sometimes the property is itself a complex object, in which
 * case we can use the slot dialog to collect that compound property.
 * @param {string} dialogId A unique identifier for this dialog.
 * @param {Array} slots An array of SlotDetails that define the required slots.
 */
constructor(dialogId, slots) {
    super(dialogId);

    if (!slots) throw new Error('[SlotFillingDialog]: Missing parameter. slots parameter is required');

    this.slots = slots;
}
```

The core logic for collecting missing information is in the `RunPromptAsync` helper method.
It tracks the values it has collected, which slot it prompted for last, and details for the slots to fill.
When all the information has been collected, it ends the dialog and returns the information.

<!-- [!code-javascript[slot-filling runPrompt](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/04.waterfall-or-custom-dialog-with-adaptive/dialogs/slotFillingDialog.js?range=84-109)]
-->

```javascript
/**
 * This helper function contains the core logic of this dialog. The main idea is to compare the state we have gathered with the
 * list of slots we have been asked to fill. When we find an empty slot we execute the corresponding prompt.
 * @param {DialogContext} dc
 */
async runPrompt(dc) {
    // runPrompt finds the next slot to fill, then calls the appropriate prompt to fill it.
    const state = dc.activeDialog.state;
    const values = state[PersistedValues];

    // Run through the list of slots until we find one that hasn't been filled yet.
    const unfilledSlot = this.slots.filter(function(slot) { return !Object.keys(values).includes(slot.name); });

    // If we have an unfilled slot we will try to fill it
    if (unfilledSlot.length) {
        state[SlotName] = unfilledSlot[0].name;

        // If the slot contains prompt text create the PromptOptions.

        // Run the child dialog
        return await dc.beginDialog(unfilledSlot[0].promptId, unfilledSlot[0].options);
    } else {
        // No more slots to fill so end the dialog.
        return await dc.endDialog(dc.activeDialog.state);
    }
}
```

---

For a more about implementing custom dialogs, see the discussion of the _cancel and help_ dialog in how to [handle user interruptions][interruptions-how-to].

## The root component dialog

The root dialog:

- Defines all the slots to fill, for itself, the 2 slot-filling dialogs, and the adaptive dialog.
- Creates a user state property accessor in which to save the collected information.
- Creates the adaptive dialog, the 2 slot-filling dialogs, a waterfall dialog, and the prompts to use with the waterfall and slot-filling dialogs.
- Sets the waterfall dialog as the initial dialog to run when the component first starts.

The waterfall will aggregate all the collected information and save it user state.

The waterfall and adaptive dialog are described in the following sections.

### [C#](#tab/csharp)

**Dialogs\RootDialog.cs**

The `RootDialog` class is a `ComponentDialog`. It defines the user state property in which to save the collected information.

<!-- [!code-csharp[class and constructor opening](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/04.waterfall-or-custom-dialog-with-adaptive/Dialogs/RootDialog.cs?range=25-32)]
-->

```csharp
public class RootDialog : ComponentDialog
{
    private IStatePropertyAccessor<JObject> _userStateAccessor;

    public RootDialog(UserState userState)
        : base("root")
    {
        _userStateAccessor = userState.CreateProperty<JObject>("result");
```

Its constructor creates an adaptive dialog `adaptiveSlotFillingDialog`. It then creates and adds the rest of the dialogs it uses and adds the adaptive dialog.

<!-- [!code-csharp[add dialogs plus closing](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/04.waterfall-or-custom-dialog-with-adaptive/Dialogs/RootDialog.cs?range=121-139)]
-->

```csharp
        // Add the various dialogs that will be used to the DialogSet.
        AddDialog(new SlotFillingDialog("address", address_slots));
        AddDialog(new SlotFillingDialog("fullname", fullname_slots));
        AddDialog(new TextPrompt("text"));
        AddDialog(new NumberPrompt<int>("number", defaultLocale: Culture.English));
        AddDialog(new NumberPrompt<float>("shoesize", ShoeSizeAsync, defaultLocale: Culture.English));

        // We will instead have adaptive dialog do the slot filling by invoking the custom dialog
        // AddDialog(new SlotFillingDialog("slot-dialog", slots));

        // Add adaptive dialog
        AddDialog(adaptiveSlotFillingDialog);

        // Defines a simple two step Waterfall to test the slot dialog.
        AddDialog(new WaterfallDialog("waterfall", new WaterfallStep[] { StartDialogAsync, DoAdaptiveDialog, ProcessResultsAsync }));

        // The initial child Dialog to run.
        InitialDialogId = "waterfall";
    }
    //...
}
```

### [JavaScript](#tab/javascript)

**dialogs/rootDialog.js**

The `RootDialog` class extends `ComponentDialog`. It defines the user state property in which to save the collected information.

<!-- [!code-javascript[class and constructor opening](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/04.waterfall-or-custom-dialog-with-adaptive/dialogs/rootDialog.js?range=16-23)]
-->

```javascript
const ROOT_DIALOG = 'RootDialog';
const ADAPTIVE_DIALOG = 'AdaptiveDialog';

class RootDialog extends ComponentDialog {
    constructor(userState) {
        super(ROOT_DIALOG);

        this.userStateAccessor = userState.createProperty('result');
```

Its constructor creates an adaptive dialog `adaptiveSlotFillingDialog`. It then creates and adds the rest of the dialogs it uses and adds the adaptive dialog.

<!-- [!code-javascript[add dialogs plus closing](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/04.waterfall-or-custom-dialog-with-adaptive/dialogs/rootDialog.js?range=89-111)]
-->

```javascript
        // Add the various dialogs that will be used to the DialogSet.
        this.addDialog(new SlotFillingDialog('address', addressSlots));
        this.addDialog(new SlotFillingDialog('fullname', fullnameSlots));
        this.addDialog(new TextPrompt('text'));
        this.addDialog(new NumberPrompt('number')); // PromptCultureModels.English.locale
        this.addDialog(new NumberPrompt('shoesize', this.shoeSizeValidator)); // PromptCultureModels.English.locale

        // We will instead have adaptive dialog do the slot filling by invoking the custom dialog
        // AddDialog(new SlotFillingDialog("slot-dialog", slots));

        // Add adaptive dialog
        this.addDialog(adaptiveSlotFillingDialog);

        // Defines a simple two step Waterfall to test the slot dialog.
        this.addDialog(new WaterfallDialog('waterfall', [
            this.startDialog.bind(this),
            this.doAdaptiveDialog.bind(this),
            this.processResults.bind(this)
        ]));

        // The initial child Dialog to run.
        this.initialDialogId = 'waterfall';
    }
    //...
}
```

---

## The waterfall dialog

The waterfall dialog contains 3 steps:

1. Start the "fullname" slot-filling dialog, which will gather and return the user's full name.
1. Record the user's name and start the adaptive dialog, which will gather the rest of the user's information.
1. Write the user's information to the user state property accessor and summarize to the user the collected information.

### [C#](#tab/csharp)

**Dialogs\RootDialog.cs**

<!-- [!code-csharp[Waterfall steps](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/04.waterfall-or-custom-dialog-with-adaptive/Dialogs/RootDialog.cs?range=159-199)]
-->

```csharp
private async Task<DialogTurnResult> StartDialogAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    // Start the child dialog. This will just get the user's first and last name.
    return await stepContext.BeginDialogAsync("fullname", null, cancellationToken);
}

private async Task<DialogTurnResult> DoAdaptiveDialog(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    object adaptiveOptions = null;
    if (stepContext.Result is IDictionary<string, object> result && result.Count > 0)
    {
        adaptiveOptions = new { fullname = result };
    }
    // begin the adaptive dialog. This in-turn will get user's age, shoe-size using adaptive inputs and subsequently
    // call the custom slot filling dialog to fill user address.
    return await stepContext.BeginDialogAsync(nameof(AdaptiveDialog), adaptiveOptions, cancellationToken);
}

private async Task<DialogTurnResult> ProcessResultsAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
{
    // To demonstrate that the slot dialog collected all the properties we will echo them back to the user.
    if (stepContext.Result is IDictionary<string, object> result && result.Count > 0)
    {
        // Now the waterfall is complete, save the data we have gathered into UserState.
        // This includes data returned by the adaptive dialog.
        var obj = await _userStateAccessor.GetAsync(stepContext.Context, () => new JObject());
        obj["data"] = new JObject
        {
            { "fullname",  $"{result["fullname"]}" },
            { "shoesize", $"{result["shoesize"]}" },
            { "address", $"{result["address"]}" },
        };

        await stepContext.Context.SendActivityAsync(MessageFactory.Text(obj["data"]["fullname"].Value<string>()), cancellationToken);
        await stepContext.Context.SendActivityAsync(MessageFactory.Text(obj["data"]["shoesize"].Value<string>()), cancellationToken);
        await stepContext.Context.SendActivityAsync(MessageFactory.Text(obj["data"]["address"].Value<string>()), cancellationToken);
    }

    // Remember to call EndAsync to indicate to the runtime that this is the end of our waterfall.
    return await stepContext.EndDialogAsync();
}
```

### [JavaScript](#tab/javascript)

**dialogs/rootDialog.js**

<!-- [!code-javascript[Waterfall steps](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/04.waterfall-or-custom-dialog-with-adaptive/dialogs/rootDialog.js?range=113-145)]
-->

```javascript
async startDialog(step) {
    // Start the child dialog. This will just get the user's first and last name.
    return await step.beginDialog('fullname');
}

async doAdaptiveDialog(step) {
    let adaptiveOptions;
    if (step.result) {
        adaptiveOptions = { fullname: step.result.values };
    }

    // begin the adaptive dialog. This in-turn will get user's age, shoe-size using adaptive inputs and subsequently
    // call the custom slot filling dialog to fill user address.
    return await step.beginDialog(ADAPTIVE_DIALOG, adaptiveOptions);
}

// This is the second step of the WaterfallDialog.
// It receives the results of the SlotFillingDialog and displays them.
async processResults(step) {
    // Each "slot" in the SlotFillingDialog is represented by a field in step.result.values.
    // The complex that contain subfields have their own .values field containing the sub-values.
    const values = step.result;

    const fullname = values.fullname;
    await step.context.sendActivity(`Your name is ${ fullname.first } ${ fullname.last }.`);

    await step.context.sendActivity(`You wear a size ${ values.shoesize } shoes.`);

    const address = values.address.values;
    await step.context.sendActivity(`Your address is: ${ address.street }, ${ address.city } ${ address.zip }`);

    return await step.endDialog();
}
```

---

## The adaptive dialog

The adaptive dialog defines one trigger that runs when the dialog starts. The trigger will run these actions:

1. Use an input dialog to ask for the user's age.
1. Use an input dialog to ask for the user's shoe size.
1. Start the "address" slot-filling dialog to collect the user's address.
1. Set trigger's result value and end.

Since no other actions will be queued, the adaptive dialog will also end and return this result value.

The adaptive dialog uses a language generator to format text and include values from bot and dialog state. (See about using [generators in adaptive dialogs][lg-in-adaptive] For more information.)

### [C#](#tab/csharp)

**Dialogs\RootDialog.cs**

<!-- [!code-csharp[adaptive dialog and triggers](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/04.waterfall-or-custom-dialog-with-adaptive/Dialogs/RootDialog.cs?range=60-119&hihglight=3,7,10)]
-->

```csharp
// define adaptive dialog
var adaptiveSlotFillingDialog = new AdaptiveDialog();
adaptiveSlotFillingDialog.Id = nameof(AdaptiveDialog);

// Set a language generator
// You can see other adaptive dialog samples to learn how to externalize generation resources into .lg files.
adaptiveSlotFillingDialog.Generator = new TemplateEngineLanguageGenerator();

// add set of actions to perform when the adaptive dialog begins.
adaptiveSlotFillingDialog.Triggers.Add(new OnBeginDialog()
{
    Actions = new List<Dialog>()
    {
        // any options passed into adaptive dialog is automatically available under dialog.xxx
        // get user age
        new NumberInput()
        {
            Property = "dialog.userage",

            // use information passed in to the adaptive dialog.
            Prompt = new ActivityTemplate("Hello ${dialog.fullname.first}, what is your age?"),
            Validations = new List<BoolExpression>()
            {
                "int(this.value) >= 1",
                "int(this.value) <= 150"
            },
            InvalidPrompt = new ActivityTemplate("Sorry, ${this.value} does not work. Looking for age to be between 1-150. What is your age?"),
            UnrecognizedPrompt = new ActivityTemplate("Sorry, I did not understand ${this.value}. What is your age?"),
            MaxTurnCount = 3,
            DefaultValue = "=30",
            DefaultValueResponse = new ActivityTemplate("Sorry, this is not working. For now, I'm setting your age to ${this.defaultValue}"),
            AllowInterruptions = false
        },
        new NumberInput()
        {
            Property = "dialog.shoesize",
            Prompt = new ActivityTemplate("Please enter your shoe size."),
            InvalidPrompt = new ActivityTemplate("Sorry ${this.value} does not work. You must enter a size between 0 and 16. Half sizes are acceptable."),
            Validations = new List<BoolExpression>()
            {
                // size can only between 0-16
                "int(this.value) >= 0 && int(this.value) <= 16",
                // can only full or half size
                "isMatch(string(this.value), '^[0-9]+(\\.5)*$')"
            },
            AllowInterruptions = false
        },
        // get address - adaptive is calling the custom slot filling dialog here.
        new BeginDialog()
        {
            Dialog = "address",
            ResultProperty = "dialog.address"
        },
        // return everything under dialog scope. 
        new EndDialog()
        {
            Value = "=dialog"
        }
    }
}) ;
```

### [JavaScript](#tab/javascript)

**dialogs/rootDialog.js**

<!-- [!code-javascript[adaptive dialog and triggers](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/04.waterfall-or-custom-dialog-with-adaptive/dialogs/rootDialog.js?range=39-87)]
-->

```javascript
// define adaptive dialog
const adaptiveSlotFillingDialog = new AdaptiveDialog(ADAPTIVE_DIALOG);

// Set a language generator
// You can see other adaptive dialog samples to learn how to externalize generation resources into .lg files.
adaptiveSlotFillingDialog.generator = new TemplateEngineLanguageGenerator();

// add set of actions to perform when the adaptive dialog begins.
adaptiveSlotFillingDialog.triggers = [
    new OnBeginDialog([
        // any options passed into adaptive dialog is automatically available under dialog.xxx
        // get user age
        new NumberInput().configure({
            property: new StringExpression('dialog.userage'),
            // use information passed in to the adaptive dialog.
            prompt: new ActivityTemplate('Hello ${dialog.fullname.first}, what is your age?'),
            validations: [
                'int(this.value) >= 1',
                'int(this.value) <= 150'
            ],
            invalidPrompt: new ActivityTemplate('Sorry, ${this.value} does not work. Looking for age to be between 1-150. What is your age?'),
            unrecognizedPrompt: new ActivityTemplate('Sorry, I did not understand ${this.value}. What is your age?'),
            maxTurnCount: new NumberExpression(3),
            defaultValue: new ValueExpression('=30'),
            defaultValueResponse: new ActivityTemplate("Sorry, this is not working. For now, I'm setting your age to ${this.defaultValue}"),
            allowInterruptions: new BoolExpression(false)
        }),
        new NumberInput().configure({
            property: new StringExpression('dialog.shoesize'),
            prompt: new ActivityTemplate('Please enter your shoe size.'),
            invalidPrompt: new ActivityTemplate('Sorry ${this.value} does not work. You must enter a size between 0 and 16. Half sizes are acceptable.'),
            validations: [
                // size can only between 0-16
                'int(this.value) >= 0 && int(this.value) <= 16',
                // can only full or half size
                "isMatch(string(this.value), '^[0-9]+(\\.5)*$')"
            ],
            allowInterruptions: new BoolExpression(false)
        }),
        new BeginDialog().configure({
            dialog: new DialogExpression('address'),
            resultProperty: new StringExpression('dialog.address')
        }),
        // return everything under dialog scope.
        new EndDialog().configure({
            value: new ValueExpression('=dialog')
        })
    ])
];
```

---

## To test the bot

1. If you have not done so already, install the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme).
1. Run the sample locally on your machine.
1. Start the emulator, connect to your bot, and respond to the prompts: first and last name, shoe size, street, city, and zip.
1. The bot will display the information it collected.
1. Send the bot any message to start the process over again.

## Additional information

For more information on how to use each dialog type, see:

| Dialog type | Article
|:-|:-
| Adaptive and input dialogs | [Create a bot using adaptive dialogs][basic-adaptive-how-to].
| Component dialogs | [Manage dialog complexity][component-how-to]
| Custom dialogs | [Handle user interruptions][interruptions-how-to]
| Waterfall and prompt dialogs | [Implement sequential conversation flow][basic-dialog-how-to]

<!--
## Next steps
> [!div class="nextstepaction"]
> [TBD](tbd.md)
-->

<!-- Footnote-style links -->

[bot-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[about-dialogs]: bot-builder-concept-dialog.md
[about-adaptive-dialogs]: bot-builder-adaptive-dialog-Introduction.md
[about-input-dialogs]: bot-builder-concept-adaptive-dialog-Inputs.md

[lg-in-adaptive]: bot-builder-concept-adaptive-dialog-generators.md

[basic-adaptive-how-to]: bot-builder-dialogs-adaptive.md
[basic-dialog-how-to]: bot-builder-dialog-manage-conversation-flow.md
[component-how-to]: bot-builder-compositcontrol.md
[interruptions-how-to]: bot-builder-howto-handle-user-interrupt.md

[cs-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/vishwac/r9/js/experimental/adaptive-dialog/csharp_dotnetcore/04.waterfall-or-custom-dialog-with-adaptive
[js-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/vishwac/r9/js/experimental/adaptive-dialog/javascript_nodejs/19.custom-dialogs
