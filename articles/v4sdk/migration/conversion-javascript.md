---
title: Migrate an existing v3 JavaScript bot to a new v4 project | Microsoft Docs
description: We take an existing v3 JavaScript bot and migrate it to the v4 SDK, using a new project.
keywords: JavaScript, bot migration, dialogs, v3 bot
author: JonathanFingold
ms.author: v-jofing
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 05/23/2019
monikerRange: 'azure-bot-service-4.0'
---

# Migrate a SDK v3 Javascript bot to v4

In this article we'll port the v3 SDK JavaScript [core-MultiDialogs-v3](https://aka.ms/v3-js-core-multidialog-migration-sample) bot to a new v4 JavaScript bot.
This conversion is broken down into these stages:

1. Create the new project and add dependencies.
1. Update the entry point and define constants.
1. Create the dialogs, reimplementing them using the SDK v4.
1. Update the bot code to run the dialogs.
1. Port the **store.js** utility file.

At the end of this process we will have a working v4 bot. A copy of the converted bot is also in the samples repo, [core-MultiDialogs-v4](https://aka.ms/v4-js-core-multidialog-migration-sample).

The Bot Framework SDK v4 is based on the same underlying REST API as SDK v3. However, SDK v4 is a refactoring of the previous version of the SDK to allow developers more flexibility and control over their bots. Major changes in the SDK include:

- State is managed via state management objects and property accessors.
- How we handle turns has changed, that is, how the bot receives and responds to an incoming activity from the user's channel.
- v4 does not use a `session` object, instead, it has a _turn context_ object that contains information about the incoming activity and can be used to send back a response activity.
- A new dialogs library that is very different from the one in v3. You'll need to convert old dialogs to the new dialog system, using component and waterfall dialogs.

<!-- TODO
For more information about specific changes, see [differences between the v3 and v4 JavaScript SDK](???.md).
-->

> [!NOTE]
> As part of the migration, we also cleaned up some of the code, but we'll just highlight the changes we made to the v3 logic as part of the migration process.

## Prerequisites

- Node.js
- Visual Studio Code
- [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme)

## About this bot

The bot we're migrating demonstrates the use of multiple dialogs to manage conversation flow. The bot can look up flight or hotel information.

- The main dialog asks the user what type of information they're looking for.
- The hotel dialog prompts the user for search parameters, and then performs a mock search.
- The flight dialog generates an error that the bot catches and deals with gracefully.

## Create and open a new v4 bot project

1. You will need a v4 project into which to port the bot code. To create a project locally, see [Create a bot with the Bot Framework SDK for JavaScript](../../javascript/bot-builder-javascript-quickstart.md).

    > [!TIP]
    > You can also create a project on Azure, see [Create a bot with Azure Bot Service](../../bot-service-quickstart.md).
    > However, these two methods result in a slight difference in supporting files. The v4 project for this article was created as a local project.

1. Then open the project in Visual Studio Code.

## Update the package.json file

1. Add a dependency on the **botbuilder-dialogs** package by entering `npm i botbuilder-dialogs` in Visual Studio Code's terminal window.

1. Edit **./package.json** and update the `name`, `version`, `description`, and other properties as desired.

## Update the v4 app entry point

The v4 template creates an **index.js** file for the app entry point and a **bot.js** file for the bot-specific logic. In later steps, we'll rename the **bot.js** file to **bots/reservationBot.js** and add a class for each dialog.

Edit **./index.js**, which is the entry point for our bot app. This will contain the portions of the v3 **app.js** file that set up the HTTP server.

1. In addition to `BotFrameworkAdapter`, import `MemoryStorage` and `ConversationState` from the **botbuilder** package. Also import the bot and main dialog modules. (We'll create these soon, but we need to reference them here.)

    ```javascript
    // Import required bot services.
    // See https://aka.ms/bot-services to learn more about the different parts of a bot.
    const { BotFrameworkAdapter, MemoryStorage, ConversationState } = require('botbuilder');

    // This bot's main dialog.
    const { MainDialog } = require('./dialogs/main')
    const { ReservationBot } = require('./bots/reservationBot');
    ```

1. Define an `onTurnError` handler for the adapter.

    ```javascript
    // Catch-all for errors.
    adapter.onTurnError = async (context, error) => {
        const errorMsg = error.message ? error.message : `Oops. Something went wrong!`;
        // This check writes out errors to console log .vs. app insights.
        console.error(`\n [onTurnError]: ${ error }`);
        // Clear out state
        await conversationState.delete(context);
        // Send a message to the user
        await context.sendActivity(errorMsg);
    };
    ```

    In v4, we use a _bot adapter_ to route incoming activities to the bot. The adapter allows us to catch and react to errors before a turn finishes. Here, we clear conversation state if an application error occurs, which will reset all dialogs and keep the bot from staying in a corrupted conversation state.

1. Replace the template code for creating the bot with this.

    ```javascript
    // Define state store for your bot.
    const memoryStorage = new MemoryStorage();

    // Create conversation state with in-memory storage provider.
    const conversationState = new ConversationState(memoryStorage);

    // Create the base dialog and bot
    const dialog = new MainDialog();
    const reservationBot = new ReservationBot(conversationState, dialog);
    ```

    The in-memory storage layer is now provided by the `MemoryStorage` class, and we need to explicitly create a conversation state management object.

    The dialog definition code has been moved to a `MainDialog` class that we'll define shortly. We'll also migrate the bot definition code into a `ReservationBot` class.

1. Finally, we update the server's request handler to use the adapter to route activities to the bot.

    ```javascript
    // Listen for incoming requests.
    server.post('/api/messages', (req, res) => {
        adapter.processActivity(req, res, async (context) => {
            // Route incoming activities to the bot.
            await reservationBot.run(context);
        });
    });
    ```

    In v4, our bot derives from `ActivityHandler`, which defines the `run` method to receive an activity for a turn.

## Add a constants file

Create a **./const.js** file to hold identifiers for our bot.

```javascript
module.exports = {
    MAIN_DIALOG: 'mainDialog',
    INITIAL_PROMPT: 'initialPrompt',
    HOTELS_DIALOG: 'hotelsDialog',
    INITIAL_HOTEL_PROMPT: 'initialHotelPrompt',
    CHECKIN_DATETIME_PROMPT: 'checkinTimePrompt',
    HOW_MANY_NIGHTS_PROMPT: 'howManyNightsPrompt',
    FLIGHTS_DIALOG: 'flightsDialog',
};
```

In v4, IDs are assigned to dialog and prompt objects, and the dialogs and prompts are invoked by ID.

## Create new dialog files

Create these files:

| File name | Description |
|:---|:---|
| **./dialogs/flights.js** | This will contain the migrated logic for the `hotels` dialog. |
| **./dialogs/hotels.js** | This will contain the migrated logic for the `flights` dialog. |
| **./dialogs/main.js** | This will contain the migrated logic for our bot, and will stand in for the _root_ dialog. |

We have not migrated the support dialog. For an example of how to implement a help dialog in v4, see [Handle user interruptions](../bot-builder-howto-handle-user-interrupt.md?tabs=javascript).

### Implement the main dialog

Iv v3, all bots were built on top of a dialog system. In v4, bot logic and dialog logic is now separate. We've taken what was the _root dialog_ in the v3 bot and made a `MainDialog` class to take its place.

Edit **./dialogs/main.js**.

1. Import the classes and constants we need for the dialog.

    ```javascript
    const { DialogSet, DialogTurnStatus, ComponentDialog, WaterfallDialog,
        ChoicePrompt } = require('botbuilder-dialogs');
    const { FlightDialog } = require('./flights');
    const { HotelsDialog } = require('./hotels');
    const { MAIN_DIALOG,
        INITIAL_PROMPT,
        HOTELS_DIALOG,
        FLIGHTS_DIALOG
    } = require('../const');
    ```

1. Define and export the `MainDialog` class.

    ```javascript
    const initialId = 'mainWaterfallDialog';

    class MainDialog extends ComponentDialog {
        constructor() {
            super(MAIN_DIALOG);

            // Create a dialog set for the bot. It requires a DialogState accessor, with which
            // to retrieve the dialog state from the turn context.
            this.addDialog(new ChoicePrompt(INITIAL_PROMPT, this.validateNumberOfAttempts.bind(this)));
            this.addDialog(new FlightDialog(FLIGHTS_DIALOG));

            // Define the steps of the base waterfall dialog and add it to the set.
            this.addDialog(new WaterfallDialog(initialId, [
                this.promptForBaseChoice.bind(this),
                this.respondToBaseChoice.bind(this)
            ]));

            // Define the steps of the hotels waterfall dialog and add it to the set.
            this.addDialog(new HotelsDialog(HOTELS_DIALOG));

            this.initialDialogId = initialId;
        }
    }

    module.exports.MainDialog = MainDialog;
    ```

    This declares the other dialogs and prompts that the main dialog references directly.

    - The main waterfall dialog that contains the steps for this dialog. When the component dialog starts, it starts its _initial dialog_.
    - The choice prompt that we'll use to ask the user which task they'd like to perform. We've created the choice prompt with a validator.
    - The two child dialogs, flights and hotels.

1. Add a `run` helper method to the class.

    ```javascript
    /**
     * The run method handles the incoming activity (in the form of a TurnContext) and passes it through the dialog system.
     * If no dialog is active, it will start the default dialog.
     * @param {*} turnContext
     * @param {*} accessor
     */
    async run(turnContext, accessor) {
        const dialogSet = new DialogSet(accessor);
        dialogSet.add(this);

        const dialogContext = await dialogSet.createContext(turnContext);
        const results = await dialogContext.continueDialog();
        if (results.status === DialogTurnStatus.empty) {
            await dialogContext.beginDialog(this.id);
        }
    }
    ```

    In v4, a bot interacts with the dialog system by creating a dialog context first, and then calling `continueDialog`. If there is an active dialog, control is passed to it; otherwise, this call simply returns. A result of `empty` indicates that no dialog was active, and so here, we start the main dialog again.

    The `accessor` parameter passes in the accessor for the dialog state property. State for the _dialog stack_ is stored in this property. For more information about how state and dialogs work in v4, see [Managing state](../bot-builder-concept-state.md) and [Dialogs library](../bot-builder-concept-dialog.md), respectively.

1. To the class, add the waterfall steps of the main dialog and the validator for the choice prompt.

    ```javascript
    async promptForBaseChoice(stepContext) {
        return await stepContext.prompt(
            INITIAL_PROMPT, {
                prompt: 'Are you looking for a flight or a hotel?',
                choices: ['Hotel', 'Flight'],
                retryPrompt: 'Not a valid option'
            }
        );
    }

    async respondToBaseChoice(stepContext) {
        // Retrieve the user input.
        const answer = stepContext.result.value;
        if (!answer) {
            // exhausted attempts and no selection, start over
            await stepContext.context.sendActivity('Not a valid option. We\'ll restart the dialog ' +
                'so you can try again!');
            return await stepContext.endDialog();
        }
        if (answer === 'Hotel') {
            return await stepContext.beginDialog(HOTELS_DIALOG);
        }
        if (answer === 'Flight') {
            return await stepContext.beginDialog(FLIGHTS_DIALOG);
        }
        return await stepContext.endDialog();
    }

    async validateNumberOfAttempts(promptContext) {
        if (promptContext.attemptCount > 3) {
            // cancel everything
            await promptContext.context.sendActivity('Oops! Too many attempts :( But don\'t worry, I\'m ' +
                'handling that exception and you can try again!');
            return await promptContext.context.endDialog();
        }

        if (!promptContext.recognized.succeeded) {
            await promptContext.context.sendActivity(promptContext.options.retryPrompt);
            return false;
        }
        return true;
    }
    ```

    The first step of the waterfall asks the user to make a choice, by starting the choice prompt, which is itself a dialog. The second step of the waterfall consumes the result of the choice prompt. It either starts a child dialog (if a choice was made) or ends the main dialog (if the user failed to make a choice).

    The choice prompt will either return the user's choice, if they made a valid choice, or reprompt the user to make the choice again. The validator checks how many times in a row the prompt has been made to the user and allows the prompt to fail out after 3 failed attempts, returning control to the main waterfall dialog.

### Implement the flights dialog

In the v3 bot, the flights dialog was a stub that demonstrated how the bot handles a conversation error. Here, we do the same.

Edit **./dialogs/flights.js**.

```javascript
const { ComponentDialog, WaterfallDialog } = require('botbuilder-dialogs');

const initialId = 'flightsWaterfallDialog';

class FlightDialog extends ComponentDialog {
    constructor(id) {
        super(id);

        // ID of the child dialog that should be started anytime the component is started.
        this.initialDialogId = initialId;

        // Define the conversation flow using a waterfall model.
        this.addDialog(new WaterfallDialog(initialId, [
            async () => {
                throw new Error('Flights Dialog is not implemented and is instead ' +
                    'being used to show Bot error handling');
            }
        ]));
    }
}

exports.FlightDialog = FlightDialog;
```

### Implement the hotels dialog

We keep the same overall flow of the hotel dialog: ask for a destination, ask for a date, ask for the number of nights to stay, and then show the user a list of options that matched their search.

Edit **./dialogs/hotels.js**.

1. Import the classes and constants we'll need for the dialog.

    ```javascript
    const { ComponentDialog, WaterfallDialog, TextPrompt, DateTimePrompt } = require('botbuilder-dialogs');
    const { AttachmentLayoutTypes, CardFactory } = require('botbuilder');
    const store = require('../store');
    const {
        INITIAL_HOTEL_PROMPT,
        CHECKIN_DATETIME_PROMPT,
        HOW_MANY_NIGHTS_PROMPT
    } = require('../const');
    ```

1. Define and export the `HotelsDialog` class.

    ```javascript
    const initialId = 'hotelsWaterfallDialog';

    class HotelsDialog extends ComponentDialog {
        constructor(id) {
            super(id);

            // ID of the child dialog that should be started anytime the component is started.
            this.initialDialogId = initialId;

            // Register dialogs
            this.addDialog(new TextPrompt(INITIAL_HOTEL_PROMPT));
            this.addDialog(new DateTimePrompt(CHECKIN_DATETIME_PROMPT));
            this.addDialog(new TextPrompt(HOW_MANY_NIGHTS_PROMPT));

            // Define the conversation flow using a waterfall model.
            this.addDialog(new WaterfallDialog(initialId, [
                this.destinationPromptStep.bind(this),
                this.destinationSearchStep.bind(this),
                this.checkinPromptStep.bind(this),
                this.checkinTimeSetStep.bind(this),
                this.stayDurationPromptStep.bind(this),
                this.stayDurationSetStep.bind(this),
                this.hotelSearchStep.bind(this)
            ]));
        }
    }

    exports.HotelsDialog = HotelsDialog;
    ```

1. To the class, add a couple of helper functions that we'll use in the dialog steps.

    ```javascript
    addDays(startDate, days) {
        const date = new Date(startDate);
        date.setDate(date.getDate() + days);
        return date;
    };

    createHotelHeroCard(hotel) {
        return CardFactory.heroCard(
            hotel.name,
            `${hotel.rating} stars. ${hotel.numberOfReviews} reviews. From ${hotel.priceStarting} per night.`,
            CardFactory.images([hotel.image]),
            CardFactory.actions([
                {
                    type: 'openUrl',
                    title: 'More details',
                    value: `https://www.bing.com/search?q=hotels+in+${encodeURIComponent(hotel.location)}`
                }
            ])
        );
    }
    ```

    The `createHotelHeroCard` creates a hero card containing information about a hotel.

1. To the class, add the waterfall steps used in the dialog.

    ```javascript
    async destinationPromptStep(stepContext) {
        await stepContext.context.sendActivity('Welcome to the Hotels finder!');
        return await stepContext.prompt(
            INITIAL_HOTEL_PROMPT, {
                prompt: 'Please enter your destination'
            }
        );
    }

    async destinationSearchStep(stepContext) {
        const destination = stepContext.result;
        stepContext.values.destination = destination;
        await stepContext.context.sendActivity(`Looking for hotels in ${destination}`);
        return stepContext.next();
    }

    async checkinPromptStep(stepContext) {
        return await stepContext.prompt(
            CHECKIN_DATETIME_PROMPT, {
                prompt: 'When do you want to check in?'
            }
        );
    }

    async checkinTimeSetStep(stepContext) {
        const checkinTime = stepContext.result[0].value;
        stepContext.values.checkinTime = checkinTime;
        return stepContext.next();
    }

    async stayDurationPromptStep(stepContext) {
        return await stepContext.prompt(
            HOW_MANY_NIGHTS_PROMPT, {
                prompt: 'How many nights do you want to stay?'
            }
        );
    }

    async stayDurationSetStep(stepContext) {
        const numberOfNights = stepContext.result;
        stepContext.values.numberOfNights = parseInt(numberOfNights);
        return stepContext.next();
    }

    async hotelSearchStep(stepContext) {
        const destination = stepContext.values.destination;
        const checkIn = new Date(stepContext.values.checkinTime);
        const checkOut = this.addDays(checkIn, stepContext.values.numberOfNights);

        await stepContext.context.sendActivity(`Ok. Searching for Hotels in ${destination} from 
            ${checkIn.toDateString()} to ${checkOut.toDateString()}...`);
        const hotels = await store.searchHotels(destination, checkIn, checkOut);
        await stepContext.context.sendActivity(`I found in total ${hotels.length} hotels for your dates:`);

        const hotelHeroCards = hotels.map(this.createHotelHeroCard);

        await stepContext.context.sendActivity({
            attachments: hotelHeroCards,
            attachmentLayout: AttachmentLayoutTypes.Carousel
        });

        return await stepContext.endDialog();
    }
    ```

    We've migrated the steps from the v3 hotels dialog into the waterfall steps of our v4 hotels dialog.

## Update the bot

In v4, bots can react to activities outside of the dialog system. The `ActivityHandler` class defines handlers for common types of activities, to make it easier to manage your code.

Rename **./bot.js** to **./bots/reservationBot.js**, and edit it.

1. The file already imports the **ActivityHandler**, which provides a base implementation of a bot.

    ```javascript
    const { ActivityHandler } = require('botbuilder');
    ```

1. Rename the class to `ReservationBot`.

    ```javascript
    class ReservationBot extends ActivityHandler {
        // ...
    }

    module.exports.ReservationBot = ReservationBot;
    ```

1. Update the signature of the constructor, to accept the objects we're receiving.

    ```javascript
    /**
     *
     * @param {ConversationState} conversationState
     * @param {Dialog} dialog
     * @param {any} logger object for logging events, defaults to console if none is provided
    */
    constructor(conversationState, dialog, logger) {
        super();
        // ...
    }
    ```

1. In the constructor, add null parameter checks and define class constructor properties.

    ```javascript
    if (!conversationState) throw new Error('[DialogBot]: Missing parameter. conversationState is required');
    if (!dialog) throw new Error('[DialogBot]: Missing parameter. dialog is required');
    if (!logger) {
        logger = console;
        logger.log('[DialogBot]: logger not passed in, defaulting to console');
    }

    this.conversationState = conversationState;
    this.dialog = dialog;
    this.logger = logger;
    this.dialogState = this.conversationState.createProperty('DialogState');
    ```

    This is where we create the dialog state property accessor that will store state for the dialog stack.

1. In the constructor, update the `onMessage` handler and add an `onDialog` handler.

    ```javascript
    this.onMessage(async (context, next) => {
        this.logger.log('Running dialog with Message Activity.');

        // Run the Dialog with the new message Activity.
        await this.dialog.run(context, this.dialogState);

        // By calling next() you ensure that the next BotHandler is run.
        await next();
    });

    this.onDialog(async (context, next) => {
        // Save any state changes. The load happened during the execution of the Dialog.
        await this.conversationState.saveChanges(context, false);

        // By calling next() you ensure that the next BotHandler is run.
        await next();
    });
    ```

    The `ActivityHandler` routes message activities to `onMessage`. This bot handles all user input via dialogs.

    The `ActivityHandler` calls `onDialog` at the end of the turn, before returning control to the adapter. We need to explicitly save state before exiting the turn. Otherwise, the state changes will not get saved and the dialog will not run properly.

1. Finally, update the `onMembersAdded` handler in the constructor.

    ```javascript
    this.onMembersAdded(async (context, next) => {
        const membersAdded = context.activity.membersAdded;
        for (let cnt = 0; cnt < membersAdded.length; ++cnt) {
            if (membersAdded[cnt].id !== context.activity.recipient.id) {
                await context.sendActivity('Hello and welcome to Contoso help desk bot.');
            }
        }
        // By calling next() you ensure that the next BotHandler is run.
        await next();
    });
    ```

    The `ActivityHandler` calls `onMembersAdded` when it receives a conversation update activity that indicates participants other than the bot were added to the conversation. We update this method to send a greeting message when a user joins the conversation.

## Create the store file

Create the **./store.js** file, used by the hotels dialog. `searchHotels` is a mock hotel search function, same as in the v3 bot.

```javascript
module.exports = {
    searchHotels: destination => {
        return new Promise(resolve => {

            // Filling the hotels results manually just for demo purposes
            const hotels = [];
            for (let i = 1; i <= 5; i++) {
                hotels.push({
                    name: `${destination} Hotel ${i}`,
                    location: destination,
                    rating: Math.ceil(Math.random() * 5),
                    numberOfReviews: Math.floor(Math.random() * 5000) + 1,
                    priceStarting: Math.floor(Math.random() * 450) + 80,
                    image: `https://placeholdit.imgix.net/~text?txtsize=35&txt=Hotel${i}&w=500&h=260`
                });
            }

            hotels.sort((a, b) => a.priceStarting - b.priceStarting);

            // complete promise with a timer to simulate async response
            setTimeout(() => { resolve(hotels); }, 1000);
        });
    }
};
```

## Test the bot in the Emulator

At this point, we should be able to run the bot locally and attach to it with the Emulator.

1. Run the sample locally on your machine.
    If you start a debugging session in Visual Studio Code, logging information is sent to the debug console as you test the bot.
1. Start the emulator and connect to the bot.
1. Send messages to test the main, flight, and hotel dialogs.

## Additional resources

v4 conceptual topics:

- [How bots work](../bot-builder-basics.md)
- [Managing state](../bot-builder-concept-state.md)
- [Dialogs library](../bot-builder-concept-dialog.md)

v4 how-to topics:

- [Send and receive text messages](../bot-builder-howto-send-messages.md)
- [Save user and conversation data](../bot-builder-howto-v4-state.md)
- [Implement sequential conversation flow](../bot-builder-dialog-manage-conversation-flow.md)
