---
layout: page
title: Chat Bots
permalink: /en-us/node/builder/chat-bots/
weight: 1100
parent1: Building your Bot Using the Bot Builder for Node.js
parent2: Chat Bots
---

<span style="color:red">This needs to be the top-level Chat Bots topic.</span>


A chat bot is simply an application that interacts with a user in a conversational format. The conversation can be guided or free-form. Bot Builder is a framework that provides all of the features for building conversational bots using Node.js.

The [UniversalBot](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot.html) class forms the brains of your bot. It's responsible for managing all of the conversations your bot has with a user. The first step in building your bot is to create a connector. The [ChatConnector](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.chatconnector) class lets your bot interact with users on any of the [channels](/enu-us/channels/) that the framework supports, and the [emulator](/en-us/tools/bot-framework-emulator/).

### Chat connector

The following snippet from the Hello, World! example, shows creating a **ChatConnector** object and passing it as a parameter to the **UniversalBot** constructor.

{% highlight JavaScript %}
var restify = require('restify');
var builder = require('botbuilder');

//=========================================================
// Bot Setup
//=========================================================

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

...

{% endhighlight %}

The example gets the `appId` and `appPassword` settings from environment variables. When your bot is deployed, you must specify an app ID and password. But when you're first developing your bot and running it locally in the emulator, you don't need to specify the ID and password . You get an app ID and password when you register your bot with the framework (see [Registering your Bot](/en-us/registration/)).

If you deploy your bot to [Microsoft Azure](https://azure.microsoft.com), you can store the ID and password in **Application settings**. When testing your bots security settings locally, you'll need to manually set the environment variables in the console window that you're using to run your bot, or if you're using Visual Studio Code, you can add them to the “env” section of your [launch.json](https://code.visualstudio.com/Docs/editor/debugging#_launch-configurations) file.

<span style="color:red"><< I think we need to describe what's happening in the following two lines of code.

1. server.listen(process.env.port \|\| process.env.PORT \|\| 3978, function ()
2. server.post('/api/messages', connector.listen());
>></span>

### Console connector

The [ConsoleConnector](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.consoleconnector) class configures UniversalBot to interact with the user from a console window.  The console connector is primarily useful for quick testing of a bot. The following snippet shows creating creating a **ConsoleConnector** object and passing it as a parameter to the **UniversalBot** constructor..

{% highlight JavaScript %}
var builder = require('botbuilder');

var connector = new builder.ConsoleConnector().listen();
var bot = new builder.UniversalBot(connector);

. . .

{% endhighlight %}

If you’re debugging your bot using VSCode you’ll want to start your bot using a command similar to node `--debug-brk app.js` and then you’ll want to start the debugger using [attach mode](https://code.visualstudio.com/docs/editor/debugging#_node-debugging).



### What next?

Now that the bot and connector are set up, the next step is adding dialogs to the bot. If you think about building a conversational application in the way you'd think about building a web application, each dialog can be thought of as route within the conversational application. As users send messages to your bot the framework tracks which dialog is currently active and will automatically route the incoming message to the active dialog. For details about adding dialogs to your bot, see [Using Dialogs to Manage the Conversation]( /en-us/node/builder/chat/dialogs/).
