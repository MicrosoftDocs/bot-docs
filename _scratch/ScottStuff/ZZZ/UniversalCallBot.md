
layout: page
title: UniversalCallBot
permalink: /en-us/node/builder/calling/UniversalBot/
weight: 1220
parent1: Bot Builder for Node.js
parent2: Calling Bots




## Overview
Skype supports a rich feature called [Calling Bots](/en-us/skype/calling/).  When enabled, users can place a voice call to your bot and interact with it using Interactive Voice Response (IVR).  The Bot Builder for Node.js SDK includes a special [Calling SDK](/en-us/node/builder/calling-reference/modules/_botbuilder_d_) which you can use to add calling features to your chat bot.   

Architecturally, the Calling SDK is very similar to the [Chat SDK](/en-us/node/builder/chat-reference/modules/_botbuilder_d_). They have similar classes, share common constructs like [waterfalls](/en-us/node/builder/chat/waterfall), and you can even use the Chat SDK to send a message to the user you’re on a call with.  The two SDKs are designed to run side-by-side but even though they’re similar, there are significant differences and you should generally avoid mixing classes from the two libraries.  

## Installation
To get started, either install the Bot Builder module using NPM:

    npm install --save botbuilder-calling

Or clone our GitHub repository using Git. This may be preferable over NPM because it provides you with numerous example code fragments and there's a full `demo-skype-calling` bot you can run.

    git clone https://github.com/Microsoft/BotBuilder.git
    cd BotBuilder/Node
    npm install

## Hello World
The "Hello World" for a calling bot looks very similar to "Hello World" for a [chat bot](/en-us/node/builder/guides/core-concepts/#hello-world): 

{% highlight JavaScript %}
var restify = require('restify');
var calling = require('botbuilder-calling');

// Setup Restify Server
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
   console.log('%s listening to %s', server.name, server.url); 
});

// Create calling bot
var connector = new calling.CallConnector({
    callbackUrl: 'https://<your host>/api/calls',
    appId: '<your bots app id>',
    appPassword: '<your bots app password>'
});
var bot = new calling.UniversalCallBot(connector);
server.post('/api/calls', connector.listen());

// Add root dialog
bot.dialog('/', function (session) {
    session.send('Watson... come here!');
});
{% endhighlight %}

The emulator doesn’t currently support testing calling bots so you’ll need to go through most of the steps need to publish your bot to be able to test it.  You can at least run your bot locally during development using a tool like [ngrok](https://ngrok.com/) but you’ll need to use a Skype client to interact with the bot. 

<span style="color:red"><< Is the emulator comment still accurate? >></span>

### setup ngrok
Follow the instructions [here](/en-us/node/builder/guides/core-concepts/#debugging-locally-using-ngrok) to setup ngrok on your machine and prep your environment for debugging.

### register your bot
Follow the instructions outlined [here](/en-us/directory/publishing/) to register your bot and enable the skype channel. You will need provide a messaging endpoint when you register your bot in the [developer portal](http://www.botframework.com){:target="_blank"} and we typically recommend that you pair your calling bot with a chat bot so the chat bot’s endpoint is what you would normally put in that field.  If you’re only registering a calling bot you can simply paste your calling endpoint into that field.  

To enable the actual calling feature you’ll need to go into the skype channel for your bot and turn on the calling feature. You’ll then be provided with a field to copy your calling endpoint into. Make sure you use the https ngrok link for the host portion of your calling endpoint.

### configure your bot
During the registration of your bot you’ll be assigned an app ID & password which you should paste into the connector settings for your hello world bot. You’ll also need to take your full calling link and paste that in for the callbackUrl setting.

### add bot to contacts
On your bots registration page in the developer portal you’ll see a `add to skype` button next to your bots skype channel. Click this link to get you bot added to your contact list in skype.  Once you do that you (and anyone you give the join link to) will be able to communicate with the bot.

### test your bot
You can test your bot using a skype client. You should notice the call icon light up when you click on your bots contact entry (you may have to search for the bot to see it.)  It can take a few minutes for the call icon to light up if you’ve added calling to an existing bot.  

If you press the call button it should dial your bot and you should hear it speak “Watson… come here!” and then hang up.

## Calling Basics
The [UniversalCallBot](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.universalcallbot) and [CallConnector](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callconnector) classes let you author a calling bot in much the same way you would a chat bot. You add dialogs to your bot that are essentially identical to [chat dialogs](/en-us/node/builder/chat/dialogs/). You can add [waterfalls](/en-us/node/builder/chat/dialogs/#waterfall) to your bot and the steps will get a session object just like in chat but this session object is a [CallSession](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession) class which contains added [answer()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#answer), [hangup()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#hangup), and [reject()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#reject) methods for managing the current call. In general, you don’t need to worry about these call control methods though as the CallSession has logic to automatically manage the call for you. The session will automatically answer the call if you take an action like sending a message or calling a built-in prompt. It will also automatically hangup/reject the call if you call [endConversation()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#endconversation) or it detects that you’ve stopped asking the caller questions (you didn’t call a built-in prompt.)

Another difference between calling and chat bots is that while chat bots typically send messages, cards, and keyboards to a user a calling bot deals in [Actions and Outcomes](/en-us/skype/calling/#actions-and-outcomes). Skype calling bots are required to create [workflows](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.iworkflow) that are comprised of one or more [actions](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.iaction).  This is another thing that in practice you don’t have to worry too much about as the Bot Builder calling SDK will manage most of this for you. The [CallSession.send()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#send) method lets you pass either actions or strings which it will turn into [PlayPromptActions](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.playpromptaction).  The session contains auto batching logic to combine multiple actions into a single workflow that’s submitted to the calling service so you can safely call send() multiple times.  And you should rely on the SDK’s built-in [prompts](/en-us/node/builder/calling/prompts/) to collect input from the user as they process all of the outcomes.  




