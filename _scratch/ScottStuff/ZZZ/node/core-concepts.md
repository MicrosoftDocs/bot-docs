
layout: page
title: Core Concepts
permalink: /en-us/node/builder/guides/core-concepts/
weight: 2610
parent1: Bot Builder for Node.js
parent2: Guides

* TOC
{:toc}

## Overview
Bot Builder is a framework for building conversational applications (“Bots”) using Node.js. From simple command based bots to rich natural language bots the framework provides all of the features needed to manage the conversational aspects of a bot. You can easily connect bots built using the framework to your users wherever they converse, from SMS to Skype to Slack and more...

## Installation
To get started either install the Bot Builder module via NPM:

    npm install --save botbuilder

Or clone our GitHub repository using Git. This may be preferable over NPM as it will provide you with numerous example code fragments and bots:

    git clone https://github.com/Microsoft/BotBuilder.git
    cd BotBuilder/Node
    npm install

## Hello World
Once the Bot Builder module is installed we can get things started by building our first “Hello World” bot called HelloBot. The first decision we need to make is what kind of bot do we want to build? Bot Builder lets you build bots for a variety of platforms but for our HelloBot we're just going to interact with it though the command line so we're going to create a UniversalBot bound to an instance of a ConsoleConnector: 

{% highlight JavaScript %}
var builder = require('botbuilder');

var connector = new builder.ConsoleConnector().listen();
var bot = new builder.UniversalBot(connector);
{% endhighlight %}
 
The UniversalBot class implements all of logic to manage the bots' conversations with users. You can bind the UniversalBot to a variety of channels using connectors. For this guide we'll just chat with the bot from a console window so we'll use the ConsoleConnector class. In the future when you're ready to deploy your bot to real channels you'll want to swap out the ConsoleConnector for a ChatConnector configured with your bots App ID & Password  from the Bot Framework portal. 

Now that we have our bot & connector setup, we need to add a dialog to our newly created bot object. Bot Builder breaks conversational applications up into components called dialogs. If you think about building a conversational application in the way you'd think about building a web application, each dialog can be thought of as route within the conversational application. As users send messages to your bot the framework tracks which dialog is currently active and will automatically route the incoming message to the active dialog. For our HelloBot we'll just add single root '/' dialog that responds to any message with “Hello World”.

{% highlight JavaScript %}
var builder = require('botbuilder');

var connector = new builder.ConsoleConnector().listen();
var bot = new builder.UniversalBot(connector);
bot.dialog('/', function (session) {
    session.send('Hello World');
});
{% endhighlight %}

We can now run our bot and interact with it from the command line. So run the bot and type 'hello':

    node app.js
    hello
    Hello World

## Collecting Input
It's likely that you're going to want your bot to be a little smarter than HelloBot currently is so let's give HelloBot the ability to ask the user their name and then provide them with a personalized greeting. To do that we're going to introduce a new concept called a [waterfall](/en-us/node/builder/chat/dialogs/#waterfall) which will prompt the user for some information and then wait for their response:

{% highlight JavaScript %}
var builder = require('botbuilder');

var connector = new builder.ConsoleConnector().listen();
var bot = new builder.UniversalBot(connector);
bot.dialog('/', [
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    },
    function (session, results) {
        session.send('Hello %s!', results.response);
    }
]);
{% endhighlight %}

By passing an array of functions for our dialog handler a waterfall is setup where the results of the first function are passed to the input of the second function. We can chain together a series of these functions into steps that create waterfalls of any length.

To actually wait for the users input we're using one of the SDK's built in [prompts](/en-us/node/builder/chat/prompts/). We're using a simple text prompt which will capture anything the user types but the SDK a wide range of built-in prompt types. If we run our updated HelloBot we know see that our bot asks us for our name and then gives us a personalized greeting.  

    node app.js
    hello
    Hi! What is your name?
    John
    Hello John!
    hi there
    Hi! What is your name?

The problem now is that if you say hello multiple times the bot doesn't remember our name. Let's fix that.

## Adding Dialogs and Memory
Bot Builder lets you break your bots' conversation with a user into parts called dialogs. You can chain dialogs together to have sub-conversations with the user or to accomplish some micro task. For HelloBot we're going to add a new `/profile` dialog that guides the user through filling out their profile information.  This information needs to be stored somewhere so we can either return it to the caller as the output from our dialog using `session.endDialog({ response: { name: ‘John' } })` or we can store it globally using the SDK's built-in storage system.  In our case we want to remember this information globally for a user so we're going to store it off [session.userData](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#userdata). This also gives us a convenient way to trigger that we should ask the user to fill out their profile.

{% highlight JavaScript %}
var builder = require('botbuilder');

var connector = new builder.ConsoleConnector().listen();
var bot = new builder.UniversalBot(connector);
bot.dialog('/', [
    function (session, args, next) {
        if (!session.userData.name) {
            session.beginDialog('/profile');
        } else {
            next();
        }
    },
    function (session, results) {
        session.send('Hello %s!', session.userData.name);
    }
]);

bot.dialog('/profile', [
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    },
    function (session, results) {
        session.userData.name = results.response;
        session.endDialog();
    }
]);
{% endhighlight %}

Now when we run HelloBot it will prompt us to enter our name once and it will then remember our name the next time we chat with the bot.

    node app.js
    hello
    Hi! What is your name?
    John
    Hello John!
    hi there
    Hello John!

The SDK includes several ways of persisting data relative to a user or conversation:

* __userData__ stores information globally for the user across all conversations.
* __conversationData__ stores information globally for a single conversation. This data is visible to everyone within the conversation so care should be used to what's stored there. It's disabled by default and needs to be enabled using the bots [persistConversationData](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iuniversalbotsettings.html#persistconversationdata) setting.
* __privateConversationData__ stores information globally for a single conversation but its private data for the current user. This data spans all dialogs so it's useful for storing temporary state that you want cleaned up when the conversation ends.
* __dialogData__ persists information for a single dialog instance.  This is essential for storing temporary information in between the steps of a waterfall.

Bots built using Bot Builder are designed to be stateless so that they can easily be scaled to run across multiple compute nodes. Because of that you should generally avoid the temptation to save state using a global variable or function closure. Doing so will create issues when you want to scale out your bot. Instead leverage the data bags above to persist temporary and permanent state. 

Now that HelloBot can remember things let's try to make it better understand the user.

## Determining Intent
One of the keys to building a great bot is effectively determining the users intent when they ask your bot to do something.  Bot Builder includes a powerful [IntentDialog](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.intentdialog.html) class designed to assist with that task. The IntentDialog class lets you determine the users intent using a combination of two techniques. You can pass a regular expression to [IntentDialog.matches()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.intentdialog.html#matches) the users message text will be compared against that RegEx.  If it matches then the handler associated with that expression will be triggered.

Regular expressions are nice but for even more powerful intent recognition you can leverage machine learning via [LUIS](https://www.luis.ai/){:target="_blank"} by plugging a [LuisRecognizer](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.luisrecognizer.html) into your IntentDialog (explore the examples to see this in action.) The IntentDialog also supports a combination of RegEx's and recognizer plugins and will use scoring heuristics to identify the most likely handler to invoke. If no intents are predicted or the score is below a [threshold](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iintentdialogoptions.html#intentthreshold) the dialogs [onDefault()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.intentdialog.html#ondefault) handler will be invoked. 

{% highlight JavaScript %}
var builder = require('botbuilder');

var connector = new builder.ConsoleConnector().listen();
var bot = new builder.UniversalBot(connector);
var intents = new builder.IntentDialog();
bot.dialog('/', intents);

intents.matches(/^change name/i, [
    function (session) {
        session.beginDialog('/profile');
    },
    function (session, results) {
        session.send('Ok... Changed your name to %s', session.userData.name);
    }
]);

intents.onDefault([
    function (session, args, next) {
        if (!session.userData.name) {
            session.beginDialog('/profile');
        } else {
            next();
        }
    },
    function (session, results) {
        session.send('Hello %s!', session.userData.name);
    }
]);

bot.dialog('/profile', [
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    },
    function (session, results) {
        session.userData.name = results.response;
        session.endDialog();
    }
]);
{% endhighlight %}

Our HelloBot has been updated to use an IntentDialog that will look for the user to say “change name”.  As you can see the handler for this intent is just another waterfall which means you can do any sequence of prompts/actions in response to an intent being triggered.  We moved our old dialogs waterfall to the intent dialogs onDefault() handler but it otherwise remains unchanged.  Now when we run HelloBot we get a flow like this:

    node app.js
    hello
    Hi! What is your name?
    John
    Hello John!
    change name
    Hi! What is your name?
    Steve
    Ok... Changed your name to Steve
    Hi
    Hello Steve!

## Publishing to the Bot Framework Service
Now that we have a fairly functional HelloBot (it does an excellent job of greeting users) we should publish it the Bot Framework Service so that we can talk to it across various communication channels. Code wise we'll need to update our bot to use a properly configured [ChatConnector](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.chatconnector.html):

{% highlight JavaScript %}
var builder = require('botbuilder');
var restify = require('restify');

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

//=========================================================
// Bots Dialogs
//=========================================================

var intents = new builder.IntentDialog();
bot.dialog('/', intents);

intents.matches(/^change name/i, [
    function (session) {
        session.beginDialog('/profile');
    },
    function (session, results) {
        session.send('Ok... Changed your name to %s', session.userData.name);
    }
]);

intents.onDefault([
    function (session, args, next) {
        if (!session.userData.name) {
            session.beginDialog('/profile');
        } else {
            next();
        }
    },
    function (session, results) {
        session.send('Hello %s!', session.userData.name);
    }
]);

bot.dialog('/profile', [
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    },
    function (session, results) {
        session.userData.name = results.response;
        session.endDialog();
    }
]);
{% endhighlight %}

Our updated bot now requires the use of [Restify](http://restify.com/){:target="_blank"} so we'll need to install that. From a console window in our bots directory type:

    npm install --save restify

While our bot has a bit of new setup code, all of our bots dialogs stay the same.  To complete the conversion, you'll need to register your bot with the developer portal for the Bot Framework and then configure the ChatConnector with your bots App ID & Password. You pass these to the bot via environment variables when running locally or via your hosting sites web config when deployed to the cloud.

If you're running Windows you can use the [Bot Framework Emulator](/en-us/tools/bot-framework-emulator/){:target="_blank"} to locally test your changes and verify you have everything properly configured prior to deploying your bot. Make sure you set the App ID & Password within the emulator to match your bots configured App ID & Password.

