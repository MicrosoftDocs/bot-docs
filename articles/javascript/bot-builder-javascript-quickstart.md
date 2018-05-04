---
title: Create a bot using Bot Builder SDK for JavaScript | Microsoft Docs
description: Quickly create a bot using the Bot Builder SDK for JavaScript.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/21/2018
monikerRange: 'azure-bot-service-4.0'
---


# Create a bot with the Bot Builder SDK v4 (preview) for JavaScript

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

This quickstart walks you through building a bot by using the Yeoman Bot Builder generator and the Bot Builder SDK for JavaScript, and then testing it with the Bot Framework Emulator. This is based off the [Microsoft Bot Builder SDK v4](https://github.com/Microsoft/botbuilder-js).

## Pre-requisites
- [Visual Studio Code](https://www.visualstudio.com/downloads)
- [Node.js and npm](https://nodejs.org/en/)
- [Yeoman](http://yeoman.io/), which can use a generator to create a bot for you
- [Bot Emulator](https://github.com/Microsoft/BotFramework-Emulator)
- Knowledge of [restify](https://http://restify.com/) and asynchronous programming in JavaScript

The Bot Builder SDK for JavaScript consists of a series of [packages](https://github.com/Microsoft/botbuilder-js/tree/master/libraries) which can be installed from NPM using a special `@preview` tag.

# Create a bot
<!--
Paste the code below into a file called `app.js`:

```JavaScript
const botbuilder = require('botbuilder');
const restify = require('restify');

// Create server
let server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log(`${server.name} listening to ${server.url}`);
});

// Create adapter (it's ok for MICROSOFT_APP_ID and MICROSOFT_APP_PASSWORD to be blank for now)  
const adapter = new botbuilder.BotFrameworkAdapter({ 
    appId: process.env.MICROSOFT_APP_ID, 
    appPassword: process.env.MICROSOFT_APP_PASSWORD 
});


// Listen for incoming activity 
server.post('/api/messages', (req, res) => {
    // Route received activity from adapter for processing
    adapter.processActivity(req, res, async (context) => {
        if (context.activity.type === 'message') {
            await context.sendActivity(`Hello World!`);
        }
    });
});
```
-->

First, open a terminal or command prompt and create a directory and initialize the package for your v4 bots.

```bash
md myJsBots
cd myJsBots
npm init
```

Next, install the preview bits of the SDK and restify from npm.

```bash
npm install --save botbuilder@preview
npm install --save restify
```

> [!NOTE]
> For some installations the install step for restify is giving an error related to gyp.
> If this is the case try running `npm install -g windows-build-tools`.

Next, install Yeoman and the generator for JavaScript.

```bash
npm install yo
npm install generator-botbuilder-js
```

Then, use the generator to create an echo bot.

```bash
yo botbuilder-js
```

Yeoman prompts you for some information with which to create your bot.
-   Enter a name for your bot.
-   Enter a description.
-   Choose the language for your bot, either `JavaScript` or `TypeScript`.
-   Choose the template to use. Currently, `Echo` is the only template, but others will be added soon.

Yeoman creates your bot in a new folder.

## Explore code

When you open your newly created bot folder, you will see `app.js` file. This file contains an Echo bot code, npm packages that were installed for you, a server which is created using restify that will listen on port 3978, an adapter that can be left blank for this example and conversation state middleware. 

In the following code, conversation state middleware uses in-memory storage. It reads and writes the state object to storage. The count variable keeps track of the number of messages sent to the bot. You can use a similar technique to maintain state in between turns. 

**app.js**
```javascript
// Packages are installed for you
const { BotFrameworkAdapter, MemoryStorage, ConversationState } = require('botbuilder');
const restify = require('restify');

// Create server
let server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log(`${server.name} listening to ${server.url}`);
});

// Create adapter
const adapter = new BotFrameworkAdapter({ 
    appId: process.env.MICROSOFT_APP_ID, 
    appPassword: process.env.MICROSOFT_APP_PASSWORD 
});

// Add conversation state middleware
const conversationState = new ConversationState(new MemoryStorage());
adapter.use(conversationState);
```

The following code listens for incoming request and checks the incoming activity type before sending a reply to the user.

```javascript
// Listen for incoming requests 
server.post('/api/messages', (req, res) => {
    // Route received request to adapter for processing
    adapter.processActivity(req, res, (context) => {
        // This bot is only handling Messages
        if (context.activity.type === 'message') {
        
            // Get the conversation state
            const state = conversationState.get(context);
            
            // If state.count is undefined set it to 0, otherwise increment it by 1
            const count = state.count === undefined ? state.count = 0 : ++state.count;
            
            // Echo back to the user whatever they typed.
            return context.sendActivity(`${count}: You said "${context.activity.text}"`);
        } else {
            // Echo back the type of activity the bot detected if not of type message
            return context.sendActivity(`[${context.activity.type} event detected]`);
        }
    });
});
```

## Start your bot

Change directories to the one created for your bot, and start it.

```bash
cd <bot directory>
node app.js
```

## How to interact with your bot

Start the emulator and connect it to your bot by entering the URL from the previous step. Your URL will look something like `http://localhost:portNumber/api/messages`. Send "Hi" to your bot, and the bot will respond with "Turn 1: You sent Hi" to the message.
