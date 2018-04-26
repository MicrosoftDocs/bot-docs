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
# Create a bot with the Bot Builder SDK for JavaScript
The Bot Builder SDK for JavaScript is a framework for developing bots. It is easy to use and models frameworks like Express & restify to provide a familiar way for JavaScript developers to write bots. This quickstart walks you through building a bot and then testing the bot with the Bot Framework Emulator.

## Pre-requisites
The Bot Builder SDK for JavaScript consists of a series of [packages](https://github.com/Microsoft/botbuilder-js/tree/master/libraries) which can be installed from NPM using a special `@preview` tag. To get started first initialize the package for your v4 bot:

```bash
md myBot
cd myBot
npm init
```

Next install the preview bits of the SDK and restify from npm:

```bast
npm install --save botbuilder@preview
npm install --save restify
```

(Note: for some installations the install step for restify is giving an error related to gyp. If this is the case try running this: npm install -g windows-build-tools.)

# Create a bot
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

Now start your bot:

```bash
node app.js
```

To interact with your bot, download the [Bot Framework Emulator](https://emulator.botframework.com/), start it up, connect to your bot, and say "hello".
