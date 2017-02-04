---
title: Bot Framework Create Bot | Microsoft Docs
description: Walkthrough of creating a simple bot using the Bot Builder SDK for Node.js.
services: Bot Framework
documentationcenter: BotFramework-Docs
author: kbrandl
manager: rstand

ms.service: Bot Framework
ms.topic: article
ms.workload: Cognitive Services
ms.date: 02/02/2017
ms.author: v-kibran@microsoft.com

---
# Create a bot with the Bot Builder SDK for Node.js
> [!div class="op_single_selector"]
> * [C#](bot-framework-dotnet-getstarted.md)
> * [Node.js](bot-framework-nodejs-getstarted.md)
> * [Azure Bot Service](bot-framework-azure-getstarted.md)
>

The [Bot Builder SDK for Node.js](https://github.com/Microsoft/BotBuilder) is a powerful framework for constructing bots that can handle both freeform interactions and more guided ones where the possibilities are explicitly shown to the user. 
It is easy to use and models frameworks like Express & Restify to provide a familiar way for JavaScript developers to write bots.

In this tutorial, we'll walk through the process of developing, testing, and publishing a bot that is created using the Bot Builder SDK for Node.js.

## Get the SDK
We'll start by getting the SDK and installing the necessary dependencies. 
First, create a folder for your bot, navigate to it, and run the following **npm** command:

```
npm init
```

Next, install the Bot Builder SDK and [Restify](http://restify.com/) modules by running the following **npm** commands:

```
npm install --save botbuilder
npm install --save restify
```

## Create your bot
Now that you've got the SDK and prequisites in place, you're ready to write a bot. 
For this walkthrough, you'll create a bot that simply echos back any user input. 
In the folder that you created earlier for your bot, create a new file named **app.js**. 
Then, add the following code to the file: 

```javascript 
var restify = require('restify');
var builder = require('botbuilder');

// Setup Restify Server
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
   console.log('%s listening to %s', server.name, server.url); 
});
  
// Create chat bot
var connector = new builder.ChatConnector({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});
var bot = new builder.UniversalBot(connector);
server.post('/api/messages', connector.listen());

// Bot dialogs
bot.dialog('/', function (session) {
    session.send("You said: '" + session.message.text + "'");
});
```

## Test your bot
Next, let's test your bot by using the [Bot Framework Emulator](bot-framework-emulator.md) to see it in action. 
The emulator is a desktop application that lets you test and debug your bot on localhost or running remotely through a tunnel. 
You'll need to install the the emulator by following the instructions [here](bot-framework-emulator.md).

After installing the emulator, start your bot in a console window:

```
node app.js
```

At this point, your bot is running locally. Next, start the emulator and then connect your bot by completing the following tasks in the emulator:
1. Select http://localhost:3978/api/messages from the address bar. (This is the default endpoint that your bot listens to when hosted locally.)
2. Click **Connect**. (You won't need to specify **Microsoft App ID** and **Microsoft App Password** -- you'll get this information later if/when you register your bot with the framework.)

Now that your bot is running locally and is connected to the emulator, test your bot by typing a few messages in the emulator. 
You should see that the bot responds to each message you send by echoing back your message prefixed with the text *You said*. 
Congratulations -- you've successfully created an *echo bot* using the Bot Builder SDK for Node.js! 

## Next steps
Content coming soon. 

### Deploy your bot
Content coming soon.

### Register your bot
Content coming soon.

### Configure your bot
Content coming soon.

### Publish your bot
Content coming soon.

## Dive deeper
Content coming soon.