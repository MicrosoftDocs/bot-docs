---
redirect_url: https://github.com/Microsoft/botbuilder-tools/blob/master/packages/LUISGen/src/npm/readme.md
---

<!--

---
title: Extract typed LUIS results | Microsoft Docs
description: Learn how to use LUIS to extract entities with the Bot Framework SDK.
keywords: intents, entities, LUISGen, extract
author: DeniseMak
ms.author: v-demak
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: tools
ms.date: 5/16/17
monikerRange: 'azure-bot-service-4.0'
---
# Extract intents and entities using LUISGen

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

Besides recognizing intent, a LUIS app can also extract entities, which are important words for fulfilling a user's request. For example, in the example of a restaurant reservation, the LUIS app might be able to extract the party size, reservation date or restaurant location from the user's message.


You can use the [LUISGen tool](https://aka.ms/botbuilder-tools-luisgen) to generate classes that make it easier to extract entities from LUIS in your bot's code.

At a Node.js command line, install `luisgen` to the global path.
```
npm install -g luisgen
```

# [C#](#tab/cs)

## Generate a LUIS results class

Download the [CafeBot LUIS sample](https://aka.ms/contosocafebot-luis), and in its root folder, run LUISGen:

```
luisgen Assets\LU\models\LUIS\cafeLUISModel.json -cs ContosoCafeBot.CafeLUISModel
```

## Examine the generated code
This generates **cafeLUISModel.cs**, which you can add to your project. It provides a `cafeLuisModel` class for getting strongly-typed results from LUIS.

This class has an enum for getting the intents defined in the LUIS app.
```cs
public enum Intent {
    Book_Table,
    Greeting,
    None,
    Who_are_you_intent
};
```
It also has an `Entities` property. Since there can be multiple occurrences of an entity in a user's message, the `_Entities` class defines an array for each type of entity.
```cs
public class _Entities
{
    // Simple entities
    public string[] partySize;

    // Built-in entities
    public Microsoft.Bot.Builder.Ai.Luis.DateTimeSpec[] datetime;
    public double[] number;

    // Lists
    public string[][] cafeLocation;

    // Instance
    public class _Instance
    {
        public Microsoft.Bot.Builder.Ai.Luis.InstanceData[] partySize;
        public Microsoft.Bot.Builder.Ai.Luis.InstanceData[] datetime;
        public Microsoft.Bot.Builder.Ai.Luis.InstanceData[] number;
        public Microsoft.Bot.Builder.Ai.Luis.InstanceData[] cafeLocation;
    }
    [JsonProperty("$instance")]
    public _Instance _instance;
}
public _Entities Entities;
```

> [!NOTE]
> All the entity types are arrays, because LUIS may detect more than one entity of a specified type in a user's utterance.
> For example, if the user says "make reservations for 5pm tomorrow and 9pm next Saturday", both "5pm tomorrow" and "9pm next Saturday" are returned in the `datetime` results.
>

|Entity | Type | Example | Notes |
|-------|-----|------|---|
|partySize| string[]| Party of `four`| A simple entity recognizes strings. In this example, Entities.partySize[0] is `"four"`.
|datetime| [DateTimeSpec](https://docs.microsoft.com/dotnet/api/microsoft.bot.builder.ai.luis.datetimespec?view=botbuilder-4.0.0-alpha)[]| reservation for at `9pm tomorrow`| Each **DateTimeSpec** object has a timex field with the possible value of times specified in **timex** format. More information on timex can be found here: http://www.timeml.org/publications/timeMLdocs/timeml_1.2.1.html#timex3      More information on the library which does the recognition can be found here: https://github.com/Microsoft/Recognizers-Text
|number| double[]| Party of `four` which includes `2` children | `number` will identify all numbers, not just size of the party. <br/> In the utterance "Party of four which includes 2 children", `Entities.number[0]` is 4, and `Entities.number[1]` is 2.
|cafelocation| string[][] | Reservation at the `Seattle` location.| cafeLocation is a list entity, which means that it contains recognized members of lists. It is an array of arrays, in case a recognized entity is a member of more than one list. For example, "reservation in Washington" could correspond to a list for Washington state and for Washington D.C.

The `_Instance` property provides [InstanceData](https://docs.microsoft.com/dotnet/api/microsoft.bot.builder.ai.luis.instancedata?view=botbuilder-4.0.0-alpha) for more detail on each recognized entity.

## Check intents in your bot
In **CafeBot.cs**, Take a look at the code within `OnTurn`. You can see where the bot calls LUIS and checks intents to decide which dialog to begin. The LUIS results from the call to [`Recognize`](https://docs.microsoft.com/dotnet/api/microsoft.bot.builder.ai.luis.luisrecognizer?view=botbuilder-4.0.0-alpha#methods) are passed as an argument to the `BookTable` dialog.



```cs
if(!context.Responded)
{
    // call LUIS and get results
    LuisRecognizer lRecognizer = createLUISRecognizer();
    // Use the generated class as the type parameter to Recognize()
    cafeLUISModel lResult = await lRecognizer.Recognize<cafeLUISModel>(utterance, ct);
    Dictionary<string,object> lD = new Dictionary<string,object>();
    if(lResult != null) {
        lD.Add("luisResult", lResult);
    }

    // top level dispatch
    switch (lResult.TopIntent().intent)
    {
        case cafeLUISModel.Intent.Greeting:
            await context.SendActivity("Hello!");
            if (userState.sendCards) await context.SendActivity(CreateCardResponse(context.Activity, createWelcomeCardAttachment()));
            break;

        case cafeLUISModel.Intent.Book_Table:
            await dc.Begin("BookTable", lD);
            break;

        case cafeLUISModel.Intent.Who_are_you_intent:
            await context.sendActivity("I'm the Contoso Cafe bot.");
            break;

        case cafeLUISModel.Intent.None:
        default:
            await getQnAResult(context);
            break;
    }
}
```

## Extract entities in a dialog

Now take a look at `Dialogs/BookTable.cs`. The `BookTable` dialog contains a sequence of waterfall steps, each of which checks for an entity in the LUIS results passed to `args`. If the entity isn't found, the dialog skips prompting for it by calling `next()`. If it's found, the dialog prompts for it, and the user's answer to the prompt is received in the next waterfall step.

```cs
    Dialogs.Add("BookTable",
        new WaterfallStep[]
        {
            async (dc, args, next) =>
            {
                dc.ActiveDialog.State = new Dictionary<string, object>();
                IDictionary<string,object> state = dc.ActiveDialog.State;

                // add any LUIS entities to active dialog state.
                if(args.ContainsKey("luisResult")) {
                    cafeLUISModel lResult = (cafeLUISModel)args["luisResult"];
                    updateContextWithLUIS(lResult, ref state);
                }

                // prompt if we do not already have cafelocation
                if(state.ContainsKey("cafeLocation")) {
                    state["bookingLocation"] = state["cafeLocation"];
                    await next();
                } else {
                    await dc.Prompt("choicePrompt", "Which of our locations would you like?", promptOptions);
                }
            },
            async (dc, args, next) =>
            {
                var state = dc.ActiveDialog.State;
                if(!state.ContainsKey("cafeLocation")) {
                    var choiceResult = (FoundChoice)args["Value"];
                    state["bookingLocation"] = choiceResult.Value;
                }
                bool promptForDateTime = true;
                if(state.ContainsKey("datetime")) {
                    // validate timex
                    var inputdatetime = new string[] {(string)state["datetime"]};
                    var results = evaluateTimeX((string[])inputdatetime);
                    if(results.Count != 0) {
                        var timexResolution = results.First().TimexValue;
                        var timexProperty = new TimexProperty(timexResolution.ToString());
                        var bookingDateTime = $"{timexProperty.ToNaturalLanguage(DateTime.Now)}";
                        state["bookingDateTime"] = bookingDateTime;
                        promptForDateTime = false;
                    }
                }
                // prompt if we do not already have date and time
                if(promptForDateTime) {
                    await dc.Prompt("timexPrompt", "When would you like to arrive? (We open at 4PM.)",
                                    new PromptOptions { RetryPromptString = "We only accept reservations for the next 2 weeks and in the evenings between 4PM - 8PM" });
                } else {
                    await next();
                }

            },
            async (dc, args, next) =>
            {
                var state = dc.ActiveDialog.State;
                if(!state.ContainsKey("datetime")) {
                    var timexResult = (TimexResult)args;
                    var timexResolution = timexResult.Resolutions.First();
                    var timexProperty = new TimexProperty(timexResolution.ToString());
                    var bookingDateTime = $"{timexProperty.ToNaturalLanguage(DateTime.Now)}";
                    state["bookingDateTime"] = bookingDateTime;
                }
                // prompt if we already do not have party size
                if(state.ContainsKey("partySize")) {
                    state["bookingGuestCount"] = state["partySize"];
                    await next();
                } else {
                    await dc.Prompt("numberPrompt", "How many in your party?");
                }
            },
            async (dc, args, next) =>
            {
                var state = dc.ActiveDialog.State;
                if(!state.ContainsKey("partySize")) {
                    state["bookingGuestCount"] = args["Value"];
                }

                await dc.Prompt("confirmationPrompt", $"Thanks, Should I go ahead and book a table for {state["bookingGuestCount"].ToString()} guests at our {state["bookingLocation"].ToString()} location for {state["bookingDateTime"].ToString()} ?");
            },
            async (dc, args, next) =>
            {
                var dialogState = dc.ActiveDialog.State;

                // TODO: Verify user said yes to confirmation prompt

                // TODO: book the table!

                await dc.Context.SendActivity($"Thanks, I have {dialogState["bookingGuestCount"].ToString()} guests booked for our {dialogState["bookingLocation"].ToString()} location for {dialogState["bookingDateTime"].ToString()}.");
            }
        }
    );
}

// This helper method updates dialog state with any LUIS results
private void updateContextWithLUIS(cafeLUISModel lResult, ref IDictionary<string,object> dialogContext) {
    if(lResult.Entities.cafeLocation != null && lResult.Entities.cafeLocation.GetLength(0) > 0) {
        dialogContext.Add("cafeLocation", lResult.Entities.cafeLocation[0][0]);
    }
    if(lResult.Entities.partySize != null && lResult.Entities.partySize.GetLength(0) > 0) {
        dialogContext.Add("partySize", lResult.Entities.partySize[0][0]);
    } else {
        if(lResult.Entities.number != null && lResult.Entities.number.GetLength(0) > 0) {
            dialogContext.Add("partySize", lResult.Entities.number[0]);
        }
    }
    if(lResult.Entities.datetime != null && lResult.Entities.datetime.GetLength(0) > 0) {
        dialogContext.Add("datetime", lResult.Entities.datetime[0].Expressions[0]);
    }
}
```
## Run the sample

Open `ContosoCafeBot.sln` in Visual Studio 2017, and run the bot. Use the [Bot Framework Emulator](https://docs.microsoft.com/azure/bot-service/bot-service-debug-emulator) to connect to the sample bot.

In the emulator, say `reserve a table` to start the reservation dialog.

![run the bot](media/how-to-luisgen/run-bot.png)

# [TypeScript](#tab/js)

Download the [CafeBot_LUIS sample](https://aka.ms/contosocafebot-typescript-luis-dialogs), and in its root folder, run LUISGen:

```
luisgen cafeLUISModel.json -ts CafeLUISModel
```

This generates **CafeLUISModel.ts**, which you can add to your project. You can get a typed result from the LUIS recognizer using the types in the generated file.


```typescript
// call LUIS and get typed results
await luisRec.recognize(context).then(async (res : any) =>
{
    // get a typed result
    var typedresult = res as CafeLUISModel;

```

## Pass the typed result to a dialog

Examine the code in **luisbot.ts**. In the `processActivity` handler, the bot passes the typed result to a dialog.

```typescript
// Listen for incoming requests
server.post('/api/messages', (req, res) => {
    // Route received request to adapter for processing
    adapter.processActivity(req, res, async (context) => {
        const isMessage = context.activity.type === 'message';

        // Create dialog context
        const state = conversationState.get(context);
        const dc = dialogs.createContext(context, state);

        if (!isMessage) {
            await context.sendActivity(`[${context.activity.type} event detected]`);
        }

        // Check to see if anyone replied.
        if (!context.responded) {
            await dc.continue();
            // if the dialog didn't send a response
            if (!context.responded && isMessage) {


                await luisRec.recognize(context).then(async (res : any) =>
                {
                    var typedresult = res as CafeLUISModel;
                    let topIntent = LuisRecognizer.topIntent(res);
                    switch (topIntent)
                    {
                        case Intents.Book_Table: {
                            await context.sendActivity("Top intent is Book_Table ");
                            await dc.begin('reserveTable', typedresult);
                            break;
                        }

                        case Intents.Greeting: {
                            await context.sendActivity("Top intent is Greeting");
                            break;
                        }

                        case Intents.Who_are_you_intent: {
                            await context.sendActivity("Top intent is Who_are_you_intent");
                            break;
                        }
                        default: {
                            await dc.begin('default', topIntent);
                            break;
                        }
                    }

                }, (err) => {
                    // there was some error
                    console.log(err);
                }
                );
            }
        }
    });
});
```

## Check for existing entities in a dialog

In **luisbot.ts**, the `reserveTable` dialog calls a `SaveEntities` helper function to check for entities detected by the LUIS app. If the entities are found, they're saved to dialog state. Each waterfall step in the dialog checks if an entity was saved to dialog state, and if not, prompts for it.

```typescript
dialogs.add('reserveTable', [
    async function(dc, args, next){
        var typedresult = args as CafeLUISModel;

        // Call a helper function to save the entities in the LUIS result
        // to dialog state
        await SaveEntities(dc, typedresult);

        await dc.context.sendActivity("Welcome to the reservation service.");

        if (dc.activeDialog.state.dateTime) {
            await next();
        }
        else {
            await dc.prompt('dateTimePrompt', "Please provide a reservation date and time.");
        }
    },
    async function(dc, result, next){
        if (!dc.activeDialog.state.dateTime) {
            // Save the dateTimePrompt result to dialog state
            dc.activeDialog.state.dateTime = result[0].value;
        }

        // If we don't have party size, ask for it next
        if (!dc.activeDialog.state.partySize) {
            await dc.prompt('textPrompt', "How many people are in your party?");
        } else {
            await next();
        }
    },
    async function(dc, result, next){
        if (!dc.activeDialog.state.partySize) {
            dc.activeDialog.state.partySize = result;
        }
        // Ask for the reservation name next
        await dc.prompt('textPrompt', "Whose name will this be under?");
    },
    async function(dc, result){
        dc.activeDialog.state.Name = result;

        // Save data to conversation state
        var state = conversationState.get(dc.context);

        // Copy the dialog state to the conversation state
        state = dc.activeDialog.state;

        // TODO: Add in <br/>Location: ${state.cafeLocation}
        var msg = `Reservation confirmed. Reservation details:
            <br/>Date/Time: ${state.dateTime}
            <br/>Party size: ${state.partySize}
            <br/>Reservation name: ${state.Name}`;

        await dc.context.sendActivity(msg);
        await dc.end();
    }
]);
```

The `SaveEntities` helper function checks for `datetime` and `partysize` entities. The `datetime` entity is a [prebuilt entity](https://docs.microsoft.com/azure/cognitive-services/luis/luis-reference-prebuilt-entities#builtindatetimev2).

```typescript
// Helper function that saves any entities found in the LUIS result
// to the dialog state
async function SaveEntities( dc: DialogContext<TurnContext>, typedresult) {
    // Resolve entities returned from LUIS, and save these to state
    if (typedresult.entities)
    {
        console.log(`Entities found.`);
        let datetime = typedresult.entities.datetime;

        if (datetime) {
            // Use the first date or time found in the utterance
            if (datetime[0].timex) {
                timexValues = datetime[0].timex;
                // Datetime results from LUIS are represented in timex format:
                // http://www.timeml.org/publications/timeMLdocs/timeml_1.2.1.html#timex3
                // More information on the library which does the recognition can be found here:
                // https://github.com/Microsoft/Recognizers-Text

                if (datetime[0].type === "datetime") {
                    // To parse timex, here you use the resolve and creator from
                    // @microsoft/recognizers-text-data-types-timex-expression
                    // The second parameter is an array of constraints the results must satisfy
                    var resolution = Resolver.evaluate(
                        // array of timex values to evaluate. There may be more than one: "today at 6" can be 6AM or 6PM.
                        timexValues,
                        // constrain results to times between 4pm and 8pm
                        [Creator.evening]);
                    if (resolution[0]) {
                        // toNaturalLanguage takes the current date into account to create a friendly string
                        dc.activeDialog.state.dateTime = resolution[0].toNaturalLanguage(new Date());
                        // You can also use resolution.toString() to format the date/time.
                    } else {
                        // time didn't satisfy constraint.
                        dc.activeDialog.state.dateTime = null;
                    }
                }
                else  {
                    console.log(`Type ${datetime[0].type} is not yet supported. Provide both the date and the time.`);
                }
            }
        }
        let partysize = typedresult.entities.partySize;
        if (partysize) {
            console.log(`partysize entity detected.${partysize}`);
            // use first partySize entity that was found in utterance
            dc.activeDialog.state.partySize = partysize[0];
        }
        let cafelocation = typedresult.entities.cafeLocation;

        if (cafelocation) {
            console.log(`location entity detected.${cafelocation}`);
            // use first cafeLocation entity that was found in utterance
            dc.activeDialog.state.cafeLocation = cafelocation[0][0];
        }
    }
}
```

## Run the sample

1. If you don't have the TypeScript compiler installed, install it using this command:

```
npm install --global typescript
```

2. Install dependencies before you run the bot, by running `npm install` in the root directory of the sample:

```
npm install
```

3. From the root directory, build the sample using `tsc`. This will generate `luisbot.js`.

4. Run `luisbot.js` in the `lib` directory.

5. Use the [Bot Framework Emulator](https://docs.microsoft.com/azure/bot-service/bot-service-debug-emulator) to run the sample.

6. In the emulator, say `reserve a table` to start the reservation dialog.

![run the bot](media/how-to-luisgen/run-bot.png)

---


## Additional resources

For more background on LUIS, see [Language Understanding](./bot-builder-concept-luis.md).


## Next steps

> [!div class="nextstepaction"]
> [Combine LUIS and QnA using the Dispatch tool](./bot-builder-tutorial-dispatch.md)

-->
