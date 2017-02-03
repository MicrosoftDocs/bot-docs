---
layout: page
title: Determining User Intent
permalink: /en-us/node/builder/chat/IntentDialog/
weight: 1130
parent1: Building your Bot Using the Bot Builder for Node.js
parent2: Chat Bots
---



To determine user intent, you'd use the [IntentDialog](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.intentdialog.html) class. This class uses the LUIS model to listen for the user to say specific keywords or phrases. Intent dialogs are useful for building more open ended bots that support natural language style understanding. For more information, see [Understanding Natural Language](/en-us/natural-language/).

> __NOTE:__ The [CommandDialog](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.commanddialog) and [LuisDialog](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.luisdialog) classes of v1.x have been deprecated. These classes will continue to function but developers are encouraged to upgrade to the more flexible **IntentDialog** class at their earliest convenience.

### Matching regular expressions

The [IntentDialog.matches()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.intentdialog.html#matches) method lets you trigger a handler when the user's utterance matches a regular expression. The following examples show various handlers that are triggered when the regular expression matches.

A waterfall handler for cases when you need to collect input from the user.

{% highlight JavaScript %}
var intents = new builder.IntentDialog();
bot.dialog('/', intents);

intents.matches(/^echo/i, [
    function (session) {
        builder.Prompts.text(session, "What would you like me to say?");
    },
    function (session, results) {
        session.send("Ok... %s", results.response);
    }
]);
{% endhighlight %}

A simple closure that behaves as a one step waterfall. 

{% highlight JavaScript %}
var intents = new builder.IntentDialog();
bot.dialog('/', intents);

intents.matches(/^version/i, function (session) {
    session.send('Bot version 1.2');
});
{% endhighlight %}

A [DialogAction](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialogaction.html) class that can provide a shortcut for implementing simpler closures. For information about dialog actions, see [Dialog Actions](/en-us/node/builder/chat/dialogs-options/#Dialog%20actions).

{% highlight JavaScript %}
var intents = new builder.IntentDialog();
bot.dialog('/', intents);

intents.matches(/^version/i, builder.DialogAction.send('Bot version 1.2'));
{% endhighlight %}

Or, the ID of a dialog to redirect to.

{% highlight JavaScript %}
bot.dialog('/', new builder.IntentDialog()
    .matches(/^add/i, '/addTask')
    .matches(/^change/i, '/changeTask')
    .matches(/^delete/i, '/deleteTask')
    .onDefault(builder.DialogAction.send("I'm sorry. I didn't understand."))
);
{% endhighlight %}

## Intent recognizers

You can configure the **IntentDialog** class to use cloud-based intent recognition services such as [LUIS](http://luis.ai) through an extensible set of [recognizer](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iintentrecognizer.html) plugins. Out of the box, Bot Builder comes with a [LuisRecognizer](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.luisrecognizer). To use the LUIS recognizer, create a **LuisRecognizer** instance that points to your model, and then pass the recognizer into your **IntentDialog**. 

{% highlight JavaScript %}
var recognizer = new builder.LuisRecognizer('<your models url>');
var intents = new builder.IntentDialog({ recognizers: [recognizer] });
bot.dialog('/', intents);

intents.matches('Help', '/help');
{% endhighlight %}

Intent recognizers return matches as named intents. To match an intent from a recognizer, you pass the name of the intent you want to handle to [IntentDialog.matches()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.intentdialog#matches) as a _string_ instead of a _RegExp_. This lets you mix matching regular expressions with your cloud-based recognition model. To improve performance, regular expressions are always evaluated before cloud-based recognizers, and an exact match regular expression will avoid calling the cloud-based recognizers all together.

You can tie together multiple LUIS models by passing in an array of recognizers. To control the order in which the recognizers are evaluated, use the [recognizeOrder](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iintentdialogoptions#recognizeorder) option. By default, the recognizers will be evaluated in parallel and the recognizer returning the intent with the highest [score](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iintent#score) will be matched. You can change `recognizeOrder` to series and the recognizers will be evaluated in series. Any recognizer that returns an intent with a score of 1.0 will prevent the recognizers after it from being evaluated.

<span style="color:red"><< does the "score of 1.0" case apply only to series and not parallel? >></span>

> __NOTE:__ Instead of adding a matches() handler for LUIS’s “None” intent, you should add a [onDefault()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.intentdialog#ondefault) handler. This is because a LUIS model will often return a very high score for the None intent if it doesn’t understand the user's utterance. In the scenario where you’ve configured the **IntentDialog** with multiple recognizers, that could cause the None intent to win out over a non-None intent from a different model that had a slightly lower score. Because of this the **LuisRecognizer** class suppresses the None intent all together. If you explicitly register a handler for “None” it will never be matched. The onDefault() handler, however can achieve the same effect because it essentially gets triggered when all of the models report a top intent of “None”.

### Entity recognition

In addition to identifying a user's intention, LUIS can identify entities. If LUIS identifies entities, they're passed to the intent handler using the [args](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iintentrecognizerresult) parameter. Bot Builder includes an [EntityRecognizer](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.entityrecognizer.html) class to simplify working with entities. 

### Finding entities

You can use [EntityRecognizer.findEntity()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.entityrecognizer.html#findentity) and [EntityRecognizer.findAllEntities()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.entityrecognizer.html#findallentities) to search for entities of a specific type by name.

{% highlight JavaScript %}
var recognizer = new builder.LuisRecognizer('<your models url>');
var intents = new builder.IntentDialog({ recognizers: [recognizer] });
bot.dialog('/', intents);

intents.matches('AddTask', [
    function (session, args, next) {
        var task = builder.EntityRecognizer.findEntity(args.entities, 'TaskTitle');
        if (!task) {
            builder.Prompts.text(session, "What would you like to call the task?");
        } else {
            next({ response: task.entity });
        }
    },
    function (session, results) {
        if (results.response) {
            // ... save task
            session.send("Ok... Added the '%s' task.", results.response);
        } else {
            session.send("Ok");
        }
    }
]);
{% endhighlight %}

### Resolving dates and times

LUIS has a powerful built-in date and time entity recognizer that can recognize a wide range of relative and absolute dates expressed using natural language. The issue is that when LUIS returns the dates and times, it does so by returning their component parts. So if the user says “june 5th at 9am” it will return separate entities for the date and time components. To get the actual resolved date, you'd call the [EntityRecognizer.resolveTime()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.entityrecognizer.html#resolvetime) method. This method will try to convert an array of entities to a valid JavaScript Date object. If it can’t resolve the entities to a valid Date, it will return null.

{% highlight JavaScript %}
var recognizer = new builder.LuisRecognizer('<your models url>');
var intents = new builder.IntentDialog({ recognizers: [recognizer] });
bot.dialog('/', intents);

intents.matches('SetAlarm', [
    function (session, args, next) {
        var time = builder.EntityRecognizer.resolveTime(args.entities);
        if (!time) {
            builder.Prompts.time(session, 'What time would you like to set the alarm for?');
        } else {
            // Saving date as a timestamp between turns since session.dialogData could get serialized.
            session.dialogData.timestamp = time.getTime();
            next();
        }
    },
    function (session, results) {
        var time;
        if (results.response) {
            time = builder.EntityRecognizer.resolveTime([results.response]);
        } else if (session.dialogData.timestamp) {
            time = new Date(session.dialogData.timestamp);
        }
        
        // Set the alarm
        if (time) {

            // .... save alarm
            
            // Send confirmation to user
            var isAM = time.getHours() < 12;
            session.send('Setting alarm for %d/%d/%d %d:%02d%s',
                time.getMonth() + 1, time.getDate(), time.getFullYear(),
                isAM ? time.getHours() : time.getHours() - 12, time.getMinutes(), isAM ? 'am' : 'pm');
        } else {
            session.send('Ok... no problem.');
        }
    }
]);
{% endhighlight %}

### Matching list items

Bot Builder includes a powerful [choices](/en-us/node/builder/dialogs/Prompts/#promptschoice) prompt which lets you present a list of choices that the user can choose from. LUIS makes it easy to map a user's choice to a named entity but it doesn’t validate whether the user entered a valid choice. You can use [EntityRecognizer.findBestMatch()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.entityrecognizer.html#findbestmatch) and [EntityRecognizer.findAllMatches()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.entityrecognizer.html#findallmatches) to verify that the user entered a valid choice. These methods are the same methods used by the choice prompt and offer a lot of flexibility when matching a user's utterance to a value in a list.

List items can be matched using a case insensitive exact match. For example, if the choices are [“Red”,”Green”,”Blue”], the user can say “red” to match the “Red” item; a partial match where “blu” is matched to “Blue”; or a reverse partial match where “the green one” is matched to “Green”. Internally, the match functions calculate a coverage score when evaluating partial matches. For the “blu” case, the coverage score would have been 0.75, and for the “the green one” case, the coverage score would have been 0.88. The minimum score needed to trigger a match is 0.6 but this can be adjusted for each match.

<span style="color:red"><< How do you "adjust" the threshold? >></span>

Each match returs an [IFindMatchResult](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ifindmatchresult.html) interface, which contains the [entity](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ifindmatchresult.html#entity), [index](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ifindmatchresult.html#index), and [score](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ifindmatchresult.html#score) of the matched choice.

{% highlight JavaScript %}
var recognizer = new builder.LuisRecognizer('<your models url>');
var intents = new builder.IntentDialog({ recognizers: [recognizer] });
bot.dialog('/', intents);

intents.matches('DeleteTask', [
    function (session, args, next) {
        // Process optional entities received from LUIS
        var match;
        var entity = builder.EntityRecognizer.findEntity(args.entities, 'TaskTitle');
        if (entity) {
            match = builder.EntityRecognizer.findBestMatch(tasks, entity.entity);
        }
        
        // Prompt for task name
        if (!match) {
            builder.Prompts.choice(session, "Which task would you like to delete?", tasks);
        } else {
            next({ response: match });
        }
    },
    function (session, results) {
        if (results.response) {
            delete tasks[results.response.entity];
            session.send("Deleted the '%s' task.", results.response.entity);
        } else {
            session.send('Ok... no problem.');
        }
    }
]);
{% endhighlight %}


### onBegin & onDefault handlers

The **IntentDialog** lets you register an [onBegin](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.intentdialog.html#onbegin) handler that's notified anytime the dialog is first loaded for a conversation and an [onDefault](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.intentdialog.html#ondefault) handler that's notified anytime the user's utterance failed to match one of the registered patterns.

The `onBegin` handler is invoked when you call [session.beginDialog()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#begindialog) for a dialog. This gives the dialog an opportunity to process optional arguments passed to beginDialog(). To continue executing the dialog's default logic, be sure to call the `next()` function that's passed to the handler. 

{% highlight JavaScript %}
intents.onBegin(function (session, args, next) {
    session.dialogData.name = args.name;
    session.send("Hi %s...", args.name);
    next();
});
{% endhighlight %}

The `onDefault` handler is invoked anytime the user's utterance doesn’t match one of the registered patterns. The handler can be a waterfall, closure, DialogAction, or the ID of a dialog to redirect to.

{% highlight JavaScript %}
    intents.onDefault(builder.DialogAction.send("I'm sorry. I didn't understand."));
{% endhighlight %}

The `onDefault` handler can also be used to manually process intents since it's passed all of the raw recognizer results in the [args]( /en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iintentrecognizerresult) parameter.
