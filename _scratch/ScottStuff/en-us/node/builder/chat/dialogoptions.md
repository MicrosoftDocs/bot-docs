---
layout: page
title: Dialog Options
permalink: /en-us/node/builder/chat/dialogs-options/
weight: 1107
parent1: Building your Bot Using the Bot Builder for Node.js
parent2: Chat Bots
---

<span style="color:red">This needs to be a child of Dialogs.</span>


The following are options for working with dialogs.

### Dialog object

For more specialized dialogs, you can add an instance of a class that derives from the [Dialog](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialog.html) base class. This gives maximum flexibility for how your bot behaves because the built-in prompts and even waterfalls are implemented internally as dialogs.

<span style="color:red">I'm not sure what this section is saying. When i first started reading it, I thought it was suggesting that i could create my own dialog. But the example just shows that the dialog handler can be another dialog. Is this correct? If so, i think we show this elsewhere and we should remove this.</span>


{% highlight JavaScript %}
bot.dialog('/', new builder.IntentDialog()
    .matches(/^hello/i, function (session) {
        session.send("Hi there!");
    })
    .onDefault(function (session) {
        session.send("I didn't understand. Say hello to me!");
    }));
{% endhighlight %}

### SimpleDialog

<span style="color:red">We need to define "closure" and "phantom step".</span>

Implementing a new dialog from scratch can be tricky because there are a lot of things to consider. To try and cover the bulk of the scenarios not covered by waterfalls, the SDK includes the [SimpleDialog](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.simpledialog) class. The closure passed to this class works similarly to a waterfall step with the exception that the results from calling a built-in prompt or other dialog will be passed back to one closure. Unlike a waterfall, there’s no phantom step that the conversation is advanced to. This is powerful but also dangerous because your bot can easily get stuck in a loop, so use care.

The following example simply converts a user's input to base64. It sits in a tight loop prompting the user for a string which it then encodes. 
 
{% highlight JavaScript %}
bot.dialog('/', new builder.SimpleDialog(function (session, results) {
    if (results && results.response) {
        session.send(results.response.toString('base64'));
    }
    builder.Prompts.text(session, "What would you like to base64 encode?");
}));
{% endhighlight %}


### Dialog actions

Dialog actions offer shortcuts to implementing common actions. The [DialogAction](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialogaction.html) class provides a set of static methods that return a closure which can be passed to anything that accepts a dialog handler. This includes but is not limited to [UniversalBot.dialog()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot.html#dialog), [Library.dialog()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.library.html#dialog), [IntentDialog.matches()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.intentdialog.html#matches), and [IntentDialog.onDefault()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.intentdialog.html#ondefault).


|**Action Type**     | **Description**                                   
| -------------------| ---------------------------------------------
|[DialogAction.send](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialogaction.html#send) | Sends a static message to the user.      
|[DialogAction.beginDialog](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialogaction.html#begindialog) | Passes control of the conversation to a new dialog.  
|[DialogAction.endDialog](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialogaction.html#enddialog) | Ends the current dialog.
|[DialogAction.validatedPrompt](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialogaction.html#validatedprompt) | Creates a custom prompt by wrapping one of the built-in prompts with a validation routine.