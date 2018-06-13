---
title: Create a bot using Bot Builder SDK for JavaScript | Microsoft Docs
description: Quickly create a bot using the Bot Builder SDK for JavaScript.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 05/05/2018
monikerRange: 'azure-bot-service-4.0'
---


# Create a bot with the Bot Builder SDK v4 (preview) for JavaScript
[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

This quickstart walks you through building a bot by using the Yeoman Bot Builder generator and the Bot Builder SDK for JavaScript, and then testing it with the Bot Framework Emulator. This is based off the [Microsoft Bot Builder SDK v4](https://github.com/Microsoft/botbuilder-js).

## Pre-requisites
- [Visual Studio Code](https://www.visualstudio.com/downloads)
- [Node.js](https://nodejs.org/en/)
- [Yeoman](http://yeoman.io/), which can use a generator to create a bot for you
- [Bot Emulator](https://github.com/Microsoft/BotFramework-Emulator)
- Knowledge of [restify](https://http://restify.com/) and asynchronous programming in JavaScript

The Bot Builder SDK for JavaScript consists of a series of [packages](https://github.com/Microsoft/botbuilder-js/tree/master/libraries) which can be installed from NPM using a special `@preview` tag.

# Create a bot


Open an elevated command prompt, create a directory, and initialize the package for your bot.

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
npm install -g yo
npm i -g generator-botbuilder@preview
```

Then, use the generator to create an echo bot.

```bash
yo botbuilder
```

Yeoman prompts you for some information with which to create your bot.
-   Enter a name for your bot.
-   Enter a description.
-   Choose the language for your bot, either `JavaScript` or `TypeScript`.
-   Choose the template to use. Currently, `Echo` is the only template, but others will be added soon.

Yeoman creates your bot in a new folder.

## Explore code

When you open your newly created bot folder, you will see an `app.js` file. This `app.js` file will contain all the code needed to run a bot app. This file contains an echo bot that will echo back whatever you input as well as increment a counter. 

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

## Start the emulator and connect your bot
At this point, your bot is running locally. Next, start the emulator and then connect to your bot in the emulator:
1. Click **create a new bot configuration** link in the emulator "Welcome" tab. 

2. Enter a **Bot name** and enter the directory path to your bot code. The bot configuration file will be saved to this path.

3. Type `http://localhost:port-number/api/messages` into the **Endpoint URL** field, where *port-number* matches the port number shown in the browser where your application is running.

4. Click **Connect** to connect to your bot. You won't need to specify **Microsoft App ID** and **Microsoft App Password**. You can leave these fields blank for now. You'll get this information later when you register your bot.

Send "Hi" to your bot, and the bot will respond with "Turn 1: You sent Hi" to the message.

## Next steps

Next, jump into the concepts that explain a bot and how it works.

> [!div class="nextstepaction"]
> [Basic Bot concepts](../v4sdk/bot-builder-basics.md)
