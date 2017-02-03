---
layout: page
title: Skype Calling Bots
permalink: /en-us/node/builder/calling-bots/
weight: 1200
parent1: Building your Bot Using the Bot Builder for Node.js
parent2: Skype Calling Bots
---

<span style="color:red">This needs to be the top-level Calling Bots topic.</span>


Skype enables users to place a voice call to your bot and interact with it using Interactive Voice Response (IVR). For information about using this feature, see [Skype Calling Bots](/en-us/skype/calling/). Bot Builder for Node.js includes the [Calling SDK](/en-us/node/builder/calling-reference/modules/_botbuilder_d_) which developers use to add the calling feature to their chat bot. Before working with Calling Bots, you should be familiar with [Chat Bots](/en-us/node/builder/chat-bots/) and Bot Builder concepts in general.  


<div class="docs-text-note"> <strong>IMPORTANT</strong>: Architecturally, the Calling SDK is very similar to the <a href="/en-us/node/builder/chat-reference/modules/_botbuilder_d_" target="_blank">Chat SDK</a>. They have similar classes, share common constructs such as <a href="/en-us/node/builder/chat/waterfalls/" target="_blank">waterfalls</a>, and you can even use the Chat SDK to send a message to the user that you’re on a call with. The two SDK’s are designed to run side-by-side but even though they’re similar, there are significant differences and you should generally avoid mixing classes from the two libraries.</div>

### Installation

To get started, install the Bot Builder Calling module using NPM.

```
npm install --save botbuilder-calling
```

Or, instead of installing the Bot Builder module, you can clone the BotBuilder GitHub repository using Git. The advantage of this option is that it provides you with numerous example code fragments and bots (for example, see [demo-skype-calling](/en-us/samples/)).

```
git clone https://github.com/Microsoft/BotBuilder.git
cd BotBuilder/Node
npm install
```

### Hello, World!

The following shows a simple Hello, World! calling bot that's very similar to Hello, World! [chat bot](/en-us/node/builder/getting-started/) example. 

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

### Calling basics

Creating a calling bot is essentially the same as creating a chat bot except that the class names are different. To create a calling bot, instantiate the [UniversalCallBot](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.universalcallbot) and [CallConnector](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callconnector) classes the same as you would for a chat bot. Then, add dialogs and dialog handlers just as you would for a chat bot. This example uses [waterfalls](/en-us/node/builder/chat/waterfalls) for the handler. The session object is a [CallSession](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession) class which contains the [answer()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#answer), [hangup()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#hangup), and [reject()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#reject) call control methods for managing the current call. 

In general, you don’t need to worry about calling the call control methods because **CallSession** includes logic that automatically manages the call for you. For example, the session will automatically answer the call if you send a message or call a built-in prompt. The session will also automatically hangup/reject the call if you call [endConversation()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#endconversation) or it detects that you’ve stopped asking the caller questions (you stopped sending built-in prompts).

A difference between calling and chat bots is that chat bots typically send messages, cards, and keyboards to a user while calling bots use [Actions and Outcomes](/en-us/skype/calling/#actions-and-outcomes). Skype calling bots are required to create [workflows](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.iworkflow) that are comprised of one or more [actions](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.iaction). But in practice, you don’t have to worry too much about this because the calling SDK manages most of this for you. The [CallSession.send()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#send) method lets you pass either actions or strings which it will turn into [PlayPromptActions](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.playpromptaction). The session contains auto batching logic to combine multiple actions into a single workflow that’s submitted to the calling service so you can safely call `send()` multiple times. You should rely on the SDK’s built-in [prompts](/en-us/node/builder/calling/prompts/) to collect input from the user because they process all of the outcomes.  

<span style="color:red">It would be nice if we showed an IVR example or link to a sample.</span>

### Register your bot

When you [register](/en-us/registration/) your bot, you need to provide its messaging endpoint. If you pair your calling bot with a chat bot (recommended), you'd set the messaging endpoint to the chat bot’s endpoint. If you register only a calling bot, you'd paste your calling bot's endpoint into the messaging endpoint field.  

<span style="color:red">How do you pair your calling bot with a chat bot?</span>

Part of the registration process involves getting an app ID and password for your bot. Paste the ID and password into the Hello, World! example.

<span style="color:red">Don't they just use the environment variables for app ID and password? Should the example change to use environment variables to match the chat bot examples?</span>

### Configure your bot on the Skype channel

<span style="color:red">Wouldn't all of this be covered by the Skype Bots content?</span>

After registering your bot, access the bot in the developer portal under **My bots**. Click **Edit** on the Skype channel and turn on the calling feature. Get the calling link and update the `callbackUrl` setting in the example with the URL.

<span style="color:red">Is it join link or calling link (skype contacts section uses join)?</span>


### Add the bot to Skype contacts

On the bot's registration page in the developer portal, click **Add to Skype** to add the bot to your contacts list in Skype. Afterwards, you (and anyone you give the join link to) will be able to communicate with the bot.

### Test your bot

You can test your bot using a Skype client. Find your bot in the list of contacts and click it. If it's working, you should notice the call icon light up. Note that it can take a few minutes for the call icon to light up if you’ve added calling to an existing bot.  

If you press the call button, it should dial your bot and you should hear it speak “Watson… come here!” and then hang up.

<span style="color:red">Figuratively or literally hear it speak?</span>
  
