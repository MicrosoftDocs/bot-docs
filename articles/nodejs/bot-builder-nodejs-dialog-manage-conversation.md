---
title: Manage a conversation with dialogs | Microsoft Docs
description: Learn how to manage a conversation between a bot and a user with dialogs in the Bot Builder SDK for Node.js.
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 04/25/2017

---
# Manage a conversation with dialogs

Bot Builder uses dialogs to manage a bot's conversation with a user. To understand dialogs, its easiest to think of them as the equivalent of routes for a website. All bots will have at least one root `/` dialog just like all websites typically have at least one root `/` route. When the bot receives a message from the user, it will be routed to the root `/` dialog for processing. For most bots, the single root `/` dialog is all that’s needed, but just like websites often have multiple routes, bots will often have multiple dialogs.

## Multiple dialogs

The following example shows a bot with two dialogs. 

```javascript
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
```

The first message from a user will be routed to the Dialog Handler for the root ‘/’ dialog. This function receives a [session](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html) object that the bot uses to inspect the user's message, send a reply to the user, save state on behalf of the user, or redirect to another dialog.

When a user starts a conversation with the bot, it first checks to see whether the user is known by checking the `name` property of the [session.userData](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#userdata) object. The bot persists the `userData` object across all of the user's interactions with the bot. If this is the first time the user has used the bot, the bot calls [session.beginDialog()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#begindialog) to start the `/profile` dialog, which asks the user their name.

The `/profile` dialog is implemented as a [waterfall](#waterfall). The fisrt step of the waterfall simply calls [Prompts.text()](bot-builder-nodejs-prompts-input-data.md#promptstext) to ask the user their name. This built-in prompt is just another dialog that gets redirected to.

The bot maintains a stack of dialogs for each conversation. If you were to inspect the bot's dialog stack at this point, it would look like [`/`, `/profile`, `BotBuilder:Prompts`]. The conversations dialog stack helps the bot know where to route the user's reply to. 

When the user replies with their name, the prompt will call [session.endDialogWithResult()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#enddialogwithresult) with the user's response. This response is then passed as an argument to the second step of the `/profile` dialog's waterfall. In this step, the bot saves the user's name to the `session.userData.name` property and return control back to the root `/` dialog through a call to [endDialog()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#enddialog). At that point, the next step of the root `/` dialog's waterfall will be executed, which sends the user a greeting.

Note that the built-in prompts will let the user cancel an action by saying something like "nevermind" or "cancel". It’s up to the dialog that called the prompt to determine what "cancel" means. To detect that the user canceled a prompt, you can check the [ResumeReason](http://docs.botframework.com/en-us/node/builder/chat-reference/enums/_botbuilder_d_.resumereason.html) code returned in [result.resumed](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptresult.html#resumed) or simply check that [result.response](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptresult.html#response) isn't null. There are actually a number of reasons that can cause the prompt to return without a response, so checking for a null response tends to be the best approach. In this example bot, if the user responds with "nevermind", the bot would simply ask them for their name again. 

## Dialog stack

With Bot Builder SDK, you use dialogs to organize your bot's conversations with the user. The bot tracks where it is in the conversation using a stack that’s persisted to the bot's [storage system](#bot-storage).  When the bot receives the first message from a user it will push the bots [default dialog](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iuniversalbotsettings.html#defaultdialogid) onto the stack and pass that dialog the user's message. The dialog can either process the incoming message and send a reply directly to the user or it can start other dialogs which will guide the user through a series of questions that collect input from the user needed to complete some task. 

The session includes several methods for managing the bots dialog stack and therefore manipulate where the bot is conversationally with the user. After you get the hang of working with the dialog stack, you can use a combination of dialogs and the session's stack manipulation methods to achieve just about any conversational flow you can dream of.


## Starting and ending dialogs

You can use [session.beginDialog()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#begindialog) to call a dialog (pushing it onto the stack) and then either [session.endDialog()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#enddialog) or [session.endDialogWithResults()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#enddialogwithresults) to return control back to the caller (popping off the stack). When paired with [waterfalls](bot-builder-nodejs-waterfall-conversation-steps.md) you have a simple mechanism for driving conversations forward. 

The following code uses two waterfalls to prompt the user for their name and then responds with a custom greeting. 

```javascript
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
```

If you run the example using the emulator, the output will be similar to the following results.

```
restify listening to http://[::]:3978
ChatConnector: message received.
session.beginDialog(/)
/ - waterfall() step 1 of 2
/ - session.beginDialog(/askName)
./askName - waterfall() step 1 of 2
./askName - session.beginDialog(BotBuilder:Prompts)
..Prompts.text - session.send()
..Prompts.text - session.sendBatch() sending 1 messages

ChatConnector: message received.
..Prompts.text - session.endDialogWithResult()
./askName - waterfall() step 2 of 2
./askName - session.endDialogWithResult()
/ - waterfall() step 2 of 2
/ - session.send()
/ - session.sendBatch() sending 1 messages
```

The output shows that the user sent two messages to the bot. The first message pushed the default root (`/`) dialog onto the stack, entering step 1 of the first waterfall. That step called `beginDialog()` and pushed the `/askName` dialog onto the stack, entering step 1 of the second waterfall. That step then called [Prompts.text()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts#text) to ask the user their name. Prompts are themselves dialogs. You can tell the current stack depth by the number of dots prefixing each line, such as `..Prompts.text`.

When the user replies with their name, the `text()` prompt returns the user's input to the second waterfall using `endDialogWithResult()`. The waterfall then passes this value to step 2 which itself calls `endDialogWithResult()` to pass it back to the first waterfall. The first waterfall passes that result to step 2 which responds with the personalized greeting to the user.

The example uses `session.endDialogWithResult()` to return control back to the caller and passes back a value (the user's input). There are a few other options for returning control back to the caller.

* Pass your own complex values back to the caller using `session.endDialogWithResult({ response: { name: 'joe smith', age: 37 } })`
* Send the user a message using `session.endDialog("ok… operation canceled")`
* Simply return control to the caller using `session.endDialog()`

When calling a dialog with `session.beginDialog()`, you can optionally pass a set of arguments which lets you call dialogs in much the same way you would a function. The following example updates the previous exmple to show how to use arguments. The example prompts the user for their profile information and then stores it for future conversations.

```javascript
bot.dialog('/', [
    function (session) {
        session.beginDialog('/ensureProfile', session.userData.profile);
    },
    function (session, results) {
        session.userData.profile = results.profile;
        session.send('Hello %s!', session.userData.profile.name);
    }
]);
bot.dialog('/ensureProfile', [
    function (session, args, next) {
        session.dialogData.profile = args || {};
        if (!args.profile.name) {
            builder.Prompts.text(session, "Hi! What is your name?");
        } else {
            next();
        }
    },
    function (session, results, next) {
        if (results.response) {
            session.dialogData.profile.name = results.response;
        }
        if (!args.profile.email) {
            builder.Prompts.text(session, "What's your email address?");
        } else {
            next();
        }
    },
    function (session, results) {
        if (results.response) {
            session.dialogData.profile.email = results.response;
        }
        session.endDialogWithResults({ repsonse: session.dialogData.profile })
    }
]);
```

The example uses [session.userData](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#userdata) to remember the user's `profile` between conversations. The root dialog gets the user's profile data and passes it to the “/ensureProfile” dialog in the `beginDialog()` call. 

If a dialog collects data over multiple steps, it should use [session.dialogData](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#dialogdata) to temporarily hold values being collected. The `/ensureProfile` dialog uses `dialogData` to hold the `profile` object. Each step passes the `next()` function which is used to skip the step if the profile already contains that data element. For example, if the profile already contains the user's name, the waterfall skips to the next step and collects the user's email address if it doesn't exist.

After the profile dialog collects all of the profile data, it returns the profile data back to the root dialog which saves the profile in `userData`. 

## Replacing dialogs

The [session.replaceDialog()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#replacedialog) method lets you end the current dialog and replace it with a new one without returning to the caller. Typically, you'd use this to create loops.

The Bot Builder SDK includes a useful set of built-in prompts but there may be times when you’ll want to create your own custom prompts that add custom validation logic. Using a combination of `Prompts.text()` and `session.replaceDialog()` you can easily build new prompts.  The following example shows how to use `replaceDialog` to build a fairly flexible phone number prompt.

```javascript
bot.dialog('/', [
    function (session) {
        session.beginDialog('/phonePrompt');
    },
    function (session, results) {
        session.send('Got it... Setting number to %s', results.response);
    }
]);
bot.dialog('/phonePrompt', [
    function (session, args) {
        if (args && args.reprompt) {
            builder.Prompts.text(session, "Enter the number using a format of either: '(555) 123-4567' or '555-123-4567' or '5551234567'")
        } else {
            builder.Prompts.text(session, "What's your phone number?");
        }
    },
    function (session, results) {
        var matched = results.response.match(/\d+/g);
        var number = matched ? matched.join('') : '';
        if (number.length == 10 || number.length == 11) {
            session.endDialogWithResult({ response: number });
        } else {
            session.replaceDialog('/phonePrompt', { reprompt: true });
        }
    }
]);
```

The stack-based model for managing the conversation with a user is useful but not always the best approach for every bot. If you need more of a state machine model, you can use `replaceDialog` to mimic a state machine model to move from one state or location to the next as shown in the following example. 

```javascript
bot.dialog('/', [
    function (session) {
        session.send("You're in a large clearing. There's a path to the north.");
        builder.Prompts.choice(session, "command?", ["north", "look"]);
    },
    function (session, results) {
        switch (results.repsonse.entity) {
            case "north":
                session.replaceDialog("/room1");
                break;
            default:
                session.replaceDialog("/");
                break;
        }
    }
]);
bot.dialog('/room1', [
    function (session) {
        session.send("There's a small house here surrounded by a white fence with a gate. There's a path to the south and west.");
        builder.Prompts.choice(session, "command?", ["open gate", "south", "west", "look"]);
    },
    function (session, results) {
        switch (results.repsonse.entity) {
            case "open gate":
                session.replaceDialog("/room2");
                break;
            case "south":
                session.replaceDialog("/");
                break;
            case "west":
                session.replaceDialog("/room3");
                break;
            default:
                session.replaceDialog("/room1");
                break;
        }
    }
]);
```

The example creates a separate dialog for every location and moves from location to location using `replaceDialog()`. You can also make the example more data driven as shown in the following example.

```javascript
var world = {
    "room0": { 
        description: "You're in a large clearing. There's a path to the north.",
        commands: { north: "room1", look: "room0" }
    },
    "room1": {
        description: "There's a small house here surrounded by a white fence with a gate. There's a path to the south and west.",
        commands: { "open gate": "room2", south: "room0", west: "room3", look: "room1" }
    }
}

bot.dialog('/', [
    function (session, args) {
        session.beginDialog("/location", { location: "room0" });
    },
    function (session, results) {
        session.send("Congratulations! You made it out!");
    }
]);
bot.dialog('/location', [
    function (session, args) {
        var location = world[args.location];
        session.dialogData.commands = location.commands;
        builder.Prompts.choice(session, location.description, location.commands);
    },
    function (session, results) {
        var destination = session.dialogData.commands[results.response.entity];
        session.replaceDialog("/location", { location: destination });
    }
]);
```

## Canceling dialogs

Sometimes you may want to do more extensive stack manipulation. For example, you can use the [session.cancelDialog()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#canceldialog) to end a dialog at any arbitrary point in the dialog stack and optionally start a new dialog in its place. You can call `session.cancelDialog('/placeOrder')` with the ID of a dialog to cancel. The stack will be searched backwards and the first occurrence of that dialog will be canceled causing that dialog plus all of its children to be removed from the stack. Control will be returned to the original caller and they can check for a [results.resumed](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptresult.html#resumed) code equal to [ResumeReason.notCompleted](http://docs.botframework.com/en-us/node/builder/chat-reference/enums/_botbuilder_d_.resumereason.html#notcompleted) to detect the cancellation.

You can also pass the zero-based index of the dialog to cancel. Calling `session.cancelDialog(0, '/storeHours')` with an index of 0 and the ID of a new dialog to start lets you easily terminate any active task and start a new one in its place.

## Ending conversations

The [session.endConversation()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#endconversation) method provides a convenient method for quickly terminating a conversation with a user. This could be in response to the user saying “goodbye” or because you’ve simply completed the user's task. While you can technically end the conversation using `session.cancelDialog(0)`, there are a few advantages of using `endConversation()` instead.

The `endConversation()` method not only clears the dialog stack, it also clears the entire [session.privateConversationData](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#privateconversationdata) variable that gets persisted to storage. That means you can use `privateConversationData` to cache state relative to the current task, and as long as you call `endConversation()` when the task completes, the state is automatically cleaned up.

When you call `endConversation`, you can pass a message to the user (for example, `session.endConversation("Ok… Goodbye.")`). 

Also note that any time your bot throws an exception, `endConversation()` gets called with a [configurable](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iuniversalbotsettings.html#dialogerrormessage) error message in an effort to return the bot to a consistent state.

## Additional resources