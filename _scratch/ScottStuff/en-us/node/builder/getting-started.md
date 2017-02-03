---
layout: page
title: Getting Started
permalink: /en-us/node/builder/getting-started/
weight: 1005
parent1: Building your Bot Using the Bot Builder for Node.js
---

### Get the SDK

To get the SDK, create a folder for your bot and navigate to it. Then, run the following npm command:

```
npm init
```

Next, install the Bot Builder and [Restify](http://restify.com/) modules using the following npm commands:

```
npm install --save botbuilder
npm install --save restify
```

Or, instead of installing the Bot Builder module, you can clone our GitHub repository using Git. The advantage of this option is that it provides you with numerous example code fragments and bots.

```
git clone https://github.com/Microsoft/BotBuilder.git
cd BotBuilder/Node
npm install
```

### Write the Hello, World! bot

In your bot's folder, create a file named app.js. Then, add the following code to it. The code simply responds with Hello, World! to any user input.
 
{% highlight JavaScript %}
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

// Bots Dialogs

bot.dialog('/', function (session) {
    session.send("Hello, World!");
});
{% endhighlight %}

<span style="color:red"><< Is appId and appPassword null at this point? >></span>

<span style="color:red"><< Is it okay/normal to keep your password in an environment variable? >></span>

<span style="color:red"><< Need to explain somewhere the different ports in server.listen >></span>

For information about chat bots, see [Chat Bots](/en-us/node/builder/chat-bots/).

For informaton about chat bot dialogs, see [Using Dialogs to Manage the Conversation](/en-us/node/builder/chat/dialogs/).

### Test your bot

To test your bot, use Bot Framework Emulator. The emulator is a desktop application that lets you test and debug your bot on localhost or running remotely through a tunnel. For details about installing the emulator, see [Bot Framework Emulator](/en-us/tools/bot-framework-emulator/){:target="_blank"}.

After installing the emulator, see [If you use Node.js...](/en-us/tools/bot-framework-emulator#usingnode) for details about running your bot in the emulator.

Start the emulator and say "hello" to your bot.

## Publish your bot

If the Hello, World! bot was useful, and you wanted to share it with others, the following are the next steps. 

* [Deploy](/en-us/deploy/) your bot to the cloud
* [Register](/en-us/registration/) your bot with the framework
* [Configure](/en-us/channels/) your bot to run on one or more conversation channels
* [Publish](/en-us/directory/publishing/) your bot to Bot Directory

NOTE: After registering your bot with Bot Framework, you'll need to update the bot's `appId` and `appPassword` environment variables with the ID and password you were given.

## Dive deeper 

Now it's time to dive deeper to learn how to build great bots.
To dive deeper and learn how to build great bots, see:

* [Using Dialogs to Manage the Conversation](/en-us/node/builder/chat/dialogs/)
* [Determining User Intent](/en-us/node/builder/chat/IntentDialog/)
* [Using Prompts to Get Data from the User](/en-us/node/builder/chat/prompts/)
* [Using Waterfalls to Define the Steps of the Conversation](/en-us/node/builder/chat/waterfalls/)
* [Chat SDK Reference](/en-us/node/builder/chat-reference/modules/_botbuilder_d_.html)
* [Calling SDK Reference](/en-us/node/builder/calling-reference/modules/_botbuilder_d_.html)
* [Bot Builder on GitHub](https://github.com/Microsoft/BotBuilder)
