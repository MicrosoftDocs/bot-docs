---
layout: page
title: Using Waterfalls to Define the Steps of the Conversation
permalink: /en-us/node/builder/chat/waterfalls/
weight: 1110
parent1: Building your Bot Using the Bot Builder for Node.js
parent2: Chat Bots
---


Waterfalls will likely be the most common form of dialog you use so understanding how they work is a fundamental skill in bot development. A waterfall is a dialog handler that is an array of functions where the results of the first function are passed as input to the second function and so on. You can chain together a series of these functions to create waterfalls of any length. Waterfalls let you collect input from a user using a sequence of steps. A bot is always in a state of providing a user with information or asking a question and then waiting for input. It's waterfalls that drive this back-n-forth flow.

Paired with the built-in [Prompts](/en-us/node/builder/chat/prompts/) you can easily prompt the user with a series of questions. In the following example, the root dialog is a two step waterfall. The first step asks the user's name and the second step greets the user.

{% highlight JavaScript %}
bot.dialog('/', [
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    },
    function (session, results) {
        session.send('Hello %s!', results.response);
    }
]);
{% endhighlight %}

Waterfalls drive the conversation by taking an action that moves the waterfall from one step to the next. Calling a built-in prompt such as [Prompts.text()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts#text) moves the conversation along by passing the user's response to the prompt to the next waterfall step in its `results` parameter. You can also call `session.beginDialog()` (see [Starting and ending dialogs](/en-us/node/builder/chat/dialogs#Starting%20and%20ending%20dialogs)) to start one of your own dialogs to move the conversation to the next step.

{% highlight JavaScript %}
bot.dialog('/', [
    function (session) {
        session.beginDialog('/askName');
    },
    function (session, results) {
        session.send('Hello %s!', results.response);
    }
]);
bot.dialog('/askName', [
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    },
    function (session, results) {
        session.endDialogWithResult(results);
    }
]);
{% endhighlight %}

This achieves the same basic behavior as before but calls a child dialog to prompt for the user's name. That’s somewhat pointless in this example but could be a useful way of partitioning the conversation if you had multiple profile fields that you wanted to populate.  

Because all waterfalls contain a phantom last step which automatically returns the result from the last step, you could simplify the previous example by removing the call to `session.endDialogWithResult()`.

{% highlight JavaScript %}
bot.dialog('/', [
    function (session) {
        session.beginDialog('/askName');
    },
    function (session, results) {
        session.send('Hello %s!', results.response);
    }
]);
bot.dialog('/askName', [
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    }
]);
{% endhighlight %}

The first step of a waterfall can receive arguments passed to the dialog, and every step receives a `next()` function that can be used to advance the waterfall forward manually. The following example shows how to pair these two features together to create an ‘/ensureProfile’ dialog that will verify that a user's profile is complete, and prompts the user for any missing fields. This pattern let's you add fields to the profile later that would be automatically filled in as users message the bot.

{% highlight JavaScript %}
bot.dialog('/', [
    function (session) {
        session.beginDialog('/ensureProfile', session.userData.profile);
    },
    function (session, results) {
        session.userData.profile = results.response;
        session.send('Hello %(name)s! I love %(company)s!', session.userData.profile);
    }
]);
bot.dialog('/ensureProfile', [
    function (session, args, next) {
        session.dialogData.profile = args || {};
        if (!session.dialogData.profile.name) {
            builder.Prompts.text(session, "What's your name?");
        } else {
            next();
        }
    },
    function (session, results, next) {
        if (results.response) {
            session.dialogData.profile.name = results.response;
        }
        if (!session.dialogData.profile.company) {
            builder.Prompts.text(session, "What company do you work for?");
        } else {
            next();
        }
    },
    function (session, results) {
        if (results.response) {
            session.dialogData.profile.company = results.response;
        }
        session.endDialogWithResult({ response: session.dialogData.profile });
    }
]);
{% endhighlight %}

Note that the ‘/ensureProfile’ dialog uses [session.dialogData](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#dialogdata) to temporarily hold the user's profile. This is because if the bot is distributed across multiple compute nodes, every step of the waterfall could be processed by a different compute node. The `dialogData` field ensures that the dialog's state is properly maintained between each step of the conversation. You can store anything you want in this field but you should limit yourself to JavaScript primitives that can be properly serialized. 

Also note that the `next()` function can be passed an **IDialogResult** object so it can mimic any results returned from a built-in prompt or other dialog which sometimes simplifies your bot's control logic.


### Single step waterfall

To create a single step waterfall, pass a single function for your dialog handler (not an array of functions). 

{% highlight JavaScript %}
bot.dialog('/', function (session) {
    session.send("Hello World");
});
{% endhighlight %}