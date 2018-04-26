---
title: Tutorial - Prompt users for input | Microsoft Docs
description: Learn how to prompt users for input in the Bot Builder SDK.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 4/23/2018
monikerRange: 'azure-bot-service-4.0'
---

# Prompt users for input

To build an interactive chat bot, your bot can response based on user input and it could ask user for information. This tutorial will show you how to ask user a question using the `prompts` library through `dialogs`.

## Prerequisit 

This tutorial will build on the basic "Echo" bot you created through the [Get Started](~/bot-service-quickstart.md) experience.

## Get the package

Navigate to your bot's project folder and install the `botbuilder-dialogs` package from NPM:

```cmd
npm install --save botbuilder-dialogs
```

## Import package to bot

Open the **app.js** file and include the `botbuilder-dialogs` library in the bot code.

```javascript
const botbuilder_dialogs = require('botbuilder-dialogs');
```

This object will give you access to the `DialogSet` and `prompts` library that you will use to ask the user questions.

## Instantiate a dialogs object

Instantiate a `dialogs` object. You will use this dialog object to manage the question and aswer process.

```javascript
const dialogs = new botbuilder_dialogs.DialogSet();
```

## Define a waterfall dialog

To ask a question, you will need at least a two step **waterfall** dialog. A **waterfall** dialog is a dialg that is defined with an array of two functions that defined the steps of the waterfall. For this tutorial, you will construction a two step waterfall dialog where the first step ask the user for their name and the second step greet the user by name.

```javascript
// Greet user:
// Ask for the user name and then greet them by name.
dialogs.add('greetings',[
    async function (dc){
        await dc.prompt('textPrompt', 'What is your name?');
    },
    async function(dc, results){
        var userName = results;
        await dc.context.sendActivity(`Hello ${userName}!`);
        await dc.end(); // Ends the dialog
    }
]);
```

The question is asked using a `textPrompt` method that came with the `prompts` library. The `Prompts` library offer a set of prompts that allows you to ask user for various types of information. For more information about other prompt types, see [Prompt user for input](~/javascript/bot-builder-javascript-prompts.md).

For the prompting to work, you will need to add prompt to the `dialogs` object with the dialogId `textPrompt` aand define it with the prompt as a `TextPrompt()`.

```javascript
dialogs.add('textPrompt', new botbuilder_dialogs.TextPrompt());
```

Now that you have defined your `dialogs` to ask a question, you need call on the dialog to start the prompting process.

## Start the dialog

Modify your "Echo" bot's `processActivity()` method to look like this:

```javascript
// Listen for incoming activity 
server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (context) => {
        if (context.activity.type === 'message') {
            // State will store all of your information 
            const state = conversationState.get(context);
            const dc = dialogs.createContext(context, state);

            if(context.activity.text.match(/hi/ig)){
                await dc.begin('greeting');
            }
            else{
                // Continue executing the "current" dialog, if any.
                await dc.continue();
            }
        }
    });
});
```

Once the user says "Hi" then the bot will start the `greeting` dialog. The first step of the `greeting` dialog prompts the user for their name. The user will send a reply with their name as the `text` message. On the reply turn, the `if` statement will fail and execution will fall into the `else` clause. In this case, the bot calls the `dc.continue()` method to continue to the next step of the waterfall. The second step of the waterfall, as you have defined it, will greet the user by their name and ends the dialog.

## Next steps
Now that you know how to prompt user for input, lets expand on this and ask user multiple questions.

> [!div class="nextstepaction"]
> [Prompt user for multiple inputs](bot-builder-tutorial-waterfall.md)
