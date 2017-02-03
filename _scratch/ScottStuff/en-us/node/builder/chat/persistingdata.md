---
layout: page
title: Persisting User Data
permalink: /en-us/node/builder/chat/userdata/
weight: 1140
parent1: Building your Bot Using the Bot Builder for Node.js
parent2: Chat Bots
---

At some point, your bot will likely need to store data about the user or conversation. The SDK provides the following built-in storage system.

* __userData__ stores information globally for the user across all conversations.
* __conversationData__ stores information globally for a single conversation. This data is visible to everyone within the conversation so don't store personally identifiable information (PII) here. This store is disabled by default and needs to be enabled using the bot's [persistConversationData](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iuniversalbotsettings.html#persistconversationdata) setting.
* __privateConversationData__ stores information globally for a combination of user and conversation. This data spans all dialogs so it's useful for storing temporary state that you want cleaned up when the conversation ends.
* __dialogData__ persists information for a single dialog instance. This is essential for storing temporary information in between the steps of a waterfall.

<span style="color:red">Not sure that i understand private storage - this is temporary storage that's always gone when the conversation ends? If yes, is this the SDKs implementation or is this behavior the same with the REST API?</span>

<span style="color:red">Need to include the 32 KB limit based on answers to my questions elsewhere.</span>


The following example shows how to use [session.userData](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#userdata) to store user data. 

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

<div class="docs-text-note"> <strong>IMPORTANT</strong>: Bots built using Bot Builder are designed to be stateless so that they can easily scale to run across multiple compute nodes. Because of that you should generally avoid the temptation to save state using a global variable or function closure. Doing so will create issues when you want to scale out your bot. Instead, leverage the built-in storage above to persist temporary and permanent state. </div>
 
<span style="color:red">I pulled the following from Dialogs which seems to indicate that they should use dialogData. Which usage should we show or do both have merit? If both, we need to clearly state the case (when/why) for each.</span>

The bot persists  **userData** across all of the user’s interactions and between conversations. 

If a dialog collects data over multiple steps, it should use [session.dialogData](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#dialogdata) to temporarily hold values being collected in the step. After all of the steps finish, the data returns back to the root dialog, which saves the data in `userData`. 

<span style="color:red">I thought that I also read somewhere that you'd use dialogData because each step could occur on different compute nodes. If true, we need to include that bit here too.</span>
