---
title: JavaScript v3 to v4 migration quick reference | Microsoft Docs
description: An outline of the major differences in the v3 and v4 JavaScript Bot Framework SDK.
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

# JavaScript migration quick reference

The BotBuilder Javascript SDK v4 introduces several fundamental changes that affect how bots are authored. The purpose of this guide is to provide a quick reference for common differences between accomplishing a task in the v3 and v4 SDKs.

- How information passes between a bot and channels has changed.
    In v3, you used the _connector_ and _session_ objects.
    In v4, these are replaced by the _adapter_ and _turn context_ objects.

- Also, dialogs and bot instances have been further decoupled.
    In v3, dialogs were registered directly in the bot's constructor.
    In v4, you now pass dialogs into bot instances as arguments, providing greater compositional flexibility.

- Moreover, v4 provides an `ActivityHandler` class, which helps automate the handling of different types of activities, such as _message_, _conversation update_, and _event_ activities.

These improvements result in changes in syntax for developing bots in Javascript, especially around creating bot objects, defining dialogs, and coding event handling logic.

The rest of this topic compares the constructs in the JavaScript Bot Framework SDK v3 to their equivalent in v4.

## To listen for incoming requests

### v3

```javascript
var connector = new builder.ChatConnector({
    appId: process.env.MicrosoftAppId,
    appPassword: process.env.MicrosoftAppPassword
});

server.post('/api/messages', connector.listen());
```

### v4

```javascript
const adapter = new BotFrameworkAdapter({
    appId: process.env.MicrosoftAppId,
    appPassword: process.env.MicrosoftAppPassword
});

server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (context) => {
        await bot.run(context);
    });
});
```

## To create a bot instance

### v3

```javascript
var bot = new builder.UniversalBot(connector, [ ...DIALOG_STEPS ]);
```

### v4

```javascript
// Define the bot class as extending ActivityHandler.
const { ActivityHandler } = require('botbuilder');

class MyBot extends ActivityHandler {
    // ...
}

// Instantiate a new bot instance.
const bot = new MyBot(conversationState, dialog);
```

## To send a message to a user

### v3

```javascript
session.send('Hello and welcome to the help desk bot.');
```

### v4

```javascript
await context.sendActivity('Hello and welcome to the help desk bot.');
```

## To define a default dialog

### v3

```javascript
var bot = new builder.UniversalBot(connector, [
    // Add default dialog waterfall steps...
]);
```

### v4

```javascript
// In the main dialog class, define a run method.
async run(turnContext, accessor) {
    const dialogSet = new DialogSet(accessor);
    dialogSet.add(this);

    const dialogContext = await dialogSet.createContext(turnContext);
    const results = await dialogContext.continueDialog();
    if (results.status === DialogTurnStatus.empty) {
        await dialogContext.beginDialog(this.id);
    }
}

// Pass conversation state management and a main dialog objects to the bot (in index.js).
const bot = new MyBot(conversationState, dialog);

// Inside the bot's constructor, add the dialog as a member property and define a DialogState property accessor.
this.dialog = dialog;
this.dialogState = this.conversationState.createProperty('DialogState');

// Inside the bot's message handler, invoke the dialog's run method, passing in the turn context and DialogState accessor.
this.onMessage(async (context, next) => {
    // Run the Dialog with the new message Activity.
    await this.dialog.run(context, this.dialogState);
    await next();
});
```

## To start a child dialog

The parent dialog resumes after the child dialog ends.

### v3

```javascript
session.beginDialog(DIALOG_ID);
```

### v4

```javascript
await context.beginDialog(DIALOG_ID);
```

## To replace a dialog

The current dialog is replaced on the stack by the new dialog.

### v3

```javascript
session.replaceDialog(DIALOG_ID);
```

### v4

```javascript
await context.replaceDialog(DIALOG_ID);
```

## To end a dialog

### v3

```javascript
session.endDialog();
```

### v4

You can include an optional return value.

```javascript
await context.endDialog(returnValue);
```

## To register a dialog with a bot instance

### v3

```javascript
// Create the bot.
var bot = new builder.UniversalBot(connector, [
    // ...
]};

// Add the dialog to the bot.
bot.dialog('myDialog', require('./mydialog'));
```

### v4

```javascript
// In the main dialog class constructor.
this.addDialog(new MyDialog(DIALOG_ID));

// In the app entry point file (index.js)
const { MainDialog } = require('./dialogs/main');

// Create the base dialog and bot, passing the main dialog as an argument.
const dialog = new MainDialog();
const reservationBot = new ReservationBot(conversationState, dialog);
```

## To prompt a user for input

### v3

```javascript
var builder = require('botbuilder');
builder.Prompts.text(session, 'Please enter your destination');
```

### v4

```javascript
const { TextPrompt } = require('botbuilder-dialogs');

// In the dialog's constructor, register the prompt.
this.addDialog(new TextPrompt('initialPrompt'));

// In the dialog step, invoke the prompt.
return await stepContext.prompt(
    'initialPrompt', {
        prompt: 'Please enter your destination'
    }
);
```

## To retrieve the user's response to a prompt

### v3

```javascript
function (session, result) {
    var destination = result.response;
    // ...
}
```

### v4

```javascript
// In the dialog step after the prompt step, retrieve the result from the waterfall step context.
async nextStep(stepContext) {
    const destination = stepContext.result;
    // ...
}
```

## To save information to dialog state

### v3

```javascript
session.dialogData.destination = results.response;
```

### v4

```javascript
// In a dialog, set the value in the waterfall step context.
stepContext.values.destination = destination;
```

## To write changes in state to the persistance layer

### v3

```javascript
// State data is saved by default at the end of the turn.
// Do this to save it manually.
session.save();
```

### v4

```javascript
// State changes are not saved automatically at the end of the turn.
await this.conversationState.saveChanges(context, false);
await this.userState.saveChanges(context, false);
```

## To create and register state storage

### v3

```javascript
var builder = require('botbuilder');

// Create conversation state with in-memory storage provider.
var inMemoryStorage = new builder.MemoryBotStorage();

// Create the base dialog and bot
var bot = new builder.UniversalBot(connector, [ /*...*/ ]).set('storage', inMemoryStorage);
```

### v4

```javascript
const { MemoryStorage } = require('botbuilder');

// Create the memory layer object.
const memoryStorage = new MemoryStorage();

// Create the conversation state management object, using the storage provider.
const conversationState = new ConversationState(memoryStorage);

// Create the base dialog and bot.
const dialog = new MainDialog();
const reservationBot = new ReservationBot(conversationState, dialog);
```

## To catch an error thrown from a dialog

### v3

```javascript
// Set up the error handler.
session.on('error', function (err) {
    session.send('Failed with message:', err.message);
    session.endDialog();
});

// Throw an error.
session.error('An error has occurred.');
```

### v4

```javascript
// Set up the error handler, using the adapter.
adapter.onTurnError = async (context, error) => {
    const errorMsg = error.message

    // Clear out conversation state to avoid keeping the conversation in a corrupted bot state.
    await conversationState.delete(context);

    // Send a message to the user.
    await context.sendActivity(errorMsg);
};

// Throw an error.
async () => {
    throw new Error('An error has occurred.');
}
```

## To register activity handlers

### v3

```javascript
bot.on('conversationUpdate', function (message) {
    // ...
}

bot.on('contactRelationUpdate', function (message) {
    // ...
}
```

### v4

```javascript
// In the bot's constructor, add the handlers.
this.onMessage(async (context, next) => {
    // ...
}
  
this.onDialog(async (context, next) => {
    // ...
}

this.onMembersAdded(async (context, next) => {
    // ...
}
```

## To send attachments

### v3

```javascript
var message = new builder.Message()
    .attachments(hotelHeroCards
    .attachmentLayout(builder.AttachmentLayout.carousel));
```

### v4

```javascript
await context.sendActivity({
    attachments: hotelHeroCards,
    attachmentLayout: AttachmentLayoutTypes.Carousel
});
```
