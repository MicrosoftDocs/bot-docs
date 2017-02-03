---
layout: page
title: Using Prompts to Get Data from the User
permalink: /en-us/node/builder/chat/prompts/
weight: 1115
parent1: Building your Bot Using the Bot Builder for Node.js
parent2: Chat Bots
---


Bot Builder comes with the following built-in prompts that you can use to collect input from a user.  

|**Prompt Type**     | **Description**                                   
| -------------------| ---------------------------------------------
|[Prompts.text](#promptstext) | Asks the user to enter a string of text.      
|[Prompts.confirm](#promptsconfirm) | Asks the user to confirm an action.  
|[Prompts.number](#promptsnumber) | Asks the user to enter a number.
|[Prompts.time](#promptstime) | Asks the user for the time or date.
|[Prompts.choice](#promptschoice) | Asks the user to choose from a list of choices.       
|[Prompts.attachment](#promptsattachment) | Asks the user to upload a picture or video.       

These built-in prompts are implemented as [dialogs](/en-us/node/builder/chat/dialogs/) so they’ll return the user's response through a call to [session.endDialogWithresult()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#enddialogwithresult). Any dialog handler can receive the result of a dialog but [waterfalls](/en-us/node/builder/chat/waterfalls/) tend to be the simplest way to handle a prompt result.  

Prompts return to the caller an [IPromptResult](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptresult.html) interface. The [results.response](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptresult.html#reponse) field contains the user's response. Because the built-in prompts let the user cancel an action by saying something like ‘cancel’ or ‘nevermind’, the response may be null. The response can also be null if the user fails to enter a properly formatted response. To determine the exact reason, examine the [ResumeReason](/en-us/node/builder/chat-reference/enums/_botbuilder_d_.resumereason.html) returned in [result.resumed](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptresult.html#resumed).

### Prompts.text()

The [Prompts.text()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts.html#text) method asks the user for a string of text. The prompt returns the user's response as an [IPromptTextResult](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iprompttextresult.html) interface.

{% highlight JavaScript %}
builder.Prompts.text(session, "What is your name?");
{% endhighlight %}

### Prompts.confirm()

The [Prompts.confirm()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts.html#confirm) method asks the user to confirm an action with a yes/no response. The prompt returns the user's response as an [IPromptConfirmResult](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptconfirmresult.html) interface.

{% highlight JavaScript %}
builder.Prompts.confirm(session, "Are you sure you wish to cancel your order?");
{% endhighlight %}

### Prompts.number()

The [Prompts.number()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts.html#number) method asks the user to reply with a number. The prompt returns the user's response as an [IPromptNumberResult](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptnumberresult.html).

{% highlight JavaScript %}
builder.Prompts.number(session, "How many would you like to order?");
{% endhighlight %}

### Prompts.time()

The [Prompts.time()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts.html#time) method asks the user to reply with the time. The prompt returns the user's response as an [IPromptTimeResult](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iprompttimeresult.html) interface. The framework uses the [Chrono](http://wanasit.github.io/pages/chrono/) library to parse the user's response and supports both relative (“in 5 minutes”) and non-relative (“june 6th at 2pm”) types of responses.

The [results.response](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iprompttimeresult.html#response) contains an [entity](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ientity.html) object which contains the date and time. To resolve the date and time into a JavaScript **Date** object, use the [EntityRecognizer.resolveTime()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.entityrecognizer.html#resolvetime) method.

{% highlight JavaScript %}
bot.dialog('/createAlarm', [
    function (session) {
        session.dialogData.alarm = {};
        builder.Prompts.text(session, "What would you like to name this alarm?");
    },
    function (session, results, next) {
        if (results.response) {
            session.dialogData.name = results.response;
            builder.Prompts.time(session, "What time would you like to set an alarm for?");
        } else {
            next();
        }
    },
    function (session, results) {
        if (results.response) {
            session.dialogData.time = builder.EntityRecognizer.resolveTime([results.response]);
        }
        
        // Return alarm to caller  
        if (session.dialogData.name && session.dialogData.time) {
            session.endDialogWithResult({ 
                response: { name: session.dialogData.name, time: session.dialogData.time } 
            }); 
        } else {
            session.endDialogWithResult({
                resumed: builder.ResumeReason.notCompleted
            });
        }
    }
]);
{% endhighlight %}

### Prompts.choice()

The [Prompts.choice()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts.html#choice) method asks the user to choose from a list of choices. The prompt returns the user's response as an [IPromptChoiceResult](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptchoiceresult.html) interface. To specify the style of the list that's shown to the user, use the [IPromptOptions.listStyle](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptoptions.html#liststyle) property. The user can express their choice by either entering the number associated with the choice or the choice's name itself. Both full and partial matches of the option's name are supported.

To specify the list of choices, you can use a pipe-delimited ('\|') string:

{% highlight JavaScript %}
builder.Prompts.choice(session, "Which color?", "red|green|blue");
{% endhighlight %}

Or, an array of strings:

{% highlight JavaScript %}
builder.Prompts.choice(session, "Which color?", ["red","green","blue"]);
{% endhighlight %}

Or, an Object map. With this option, the object's keys are used to determine the choice.

{% highlight JavaScript %}
var salesData = {
    "west": {
        units: 200,
        total: "$6,000"
    },
    "central": {
        units: 100,
        total: "$3,000"
    },
    "east": {
        units: 300,
        total: "$9,000"
    }
};

bot.dialog('/', [
    function (session) {
        builder.Prompts.choice(session, "Which region would you like sales for?", salesData); 
    },
    function (session, results) {
        if (results.response) {
            var region = salesData[results.response.entity];
            session.send("We sold %(units)d units for a total of %(total)s.", region); 
        } else {
            session.send("ok");
        }
    }
]);
{% endhighlight %}

### Prompts.attachment()

The [Prompts.attachment()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts.html#attachment) method asks the user to upload a file attachment like an image or video. The prompt returns the user's response as an [IPromptAttachmentResult](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptattachmentresult.html) interface.

{% highlight JavaScript %}
builder.Prompts.attachment(session, "Upload a picture for me to transform.");
{% endhighlight %}


