---
title: Dialogs overview | Microsoft Docs
description: Learn how to use dialogs within the Bot Builder SDK for Node.js to model conversations and manage conversation flow.
author: v-ducvo
ms.author: v-ducvo
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 06/19/2017
---

# Dialogs in the Bot Builder SDK for Node.js
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-dialogs.md)
> - [Node.js](../nodejs/bot-builder-nodejs-dialog-overview.md)

Dialogs in the Bot Builder SDK for Node.js allow you to model conversations and manage conversation flow. You can use dialogs to break your bot's logic into separate components, with each dialog designed to perform a single task. 

## Dialog stack

A bot interacts with users through a series of dialogs, which are maintained in a dialog stack. When a dialog is called, the dialog is pushed onto the dialog stack. The bot tracks where it is in the conversation using a dialog stack that’s persisted to the bot's storage system. When the dialog explicitly ends, the dialog is popped off the stack and the bot continues where it left off with the current dialog on the stack.

When the bot receives the first message from a user, it will push the bot's [default dialog](#default-dialog-and-child-dialogs) onto the stack and pass the message to that dialog. The dialog can either process the incoming message and send a reply directly to the user or it can start another dialog.

The `session` object includes several methods for managing the dialog stack, which changes where the bot is conversationally with the user. After you become accustomed to working with the dialog stack, you can use a combination of dialogs and stack manipulation methods to achieve just about any conversation flow you need.

## Dialog handlers

The dialog handler manages the flow of a conversation. To progress through a conversation, the dialog handler directs the flow by starting and ending dialogs. Conversation flow can take many forms:
- It can originate from the [default dialog](#default-dialog-and-child-dialogs).
- It can be redirected from one dialog to another.
- It can implement a [waterfall](bot-builder-nodejs-dialog-waterfall.md) pattern, which guides the user through a series of steps or [prompts](bot-builder-nodejs-dialog-prompt.md) the user with a series of questions.
- It can use [actions](bot-builder-nodejs-global-handlers.md) within dialogs to listen for user input as it occurs. 

## Dialog lifecycle

When a dialog is invoked, it is pushed onto the dialog stack and it takes control of the conversation flow. Every new message will be subject to processing by that dialog until it either closes or redirects to another dialog. 

You can invoke one dialog from another by using `session.beginDialog()`. To close a dialog and remove it from the stack, use `session.endDialog()`. This will return the user to the previous dialog in the stack.

> [!IMPORTANT] 
> If you do not explicitly call `endDialog()` at the end of the dialog, the bot will cycle through and restart that dialog again.

## Default dialog and child dialogs
The default dialog is executed automatically when the bot receives an initial message or when there are no other dialogs on the stack. You can define the default dialog when you create the `UniversalBot`. The following code shows how to define a default dialog with the [UniversalBot](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot.html#constructor).

```javascript
var bot = new builder.UniversalBot(connector, [
    function (session) {
        ...
    }]);
```

Depending on the nature of the request, the bot will redirect to other child dialogs and eventually return back to the default dialog at the end of the conversation.

The following code shows a bot that contains two dialogs, a default dialog and a `profile` dialog.

```javascript
var builder = require('botbuilder');

var connector = new builder.ConsoleConnector().listen();
var bot = new builder.UniversalBot(connector, [
    function (session, args, next) {
        if (!session.userData.name) {
            session.beginDialog('profile');
        } else {
            next();
        }
    },
    function (session, results) {
        session.send('Hello %s!', session.userData.name);
    }
]);

bot.dialog('profile', [
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    },
    function (session, results) {
        session.userData.name = results.response;
        session.endDialog();
    }
]);
```

At run time, the [dialog handler](#dialog-handlers) will route the user messages to the default dialog. This function receives a [`session`](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html) object which contains information that the bot can use to inspect the user's message, send a reply to the user, save state on behalf of the user, or redirect to another dialog. In this case, the default dialog checks to see if it knows the identity of the current user. If it does, then it greets the user by name. Otherwise, it redirects the user to the `profile` dialog, which prompts for the user's name. When the user replies, the bot persists this information in the [`session.userData`](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#userdata) object and then ends the dialog. This returns the conversation to the default dialog, which sends a personalized greeting message to the user.

The bot maintains a stack of dialogs for each conversation. A conversation's dialog stack helps the bot know where to route the user's reply. The built-in prompts will let the user cancel an action by saying something like "nevermind" or "cancel". It’s up to the dialog that called the prompt to determine what "cancel" means. To detect that the user canceled a prompt, you can check the [`ResumeReason`](http://docs.botframework.com/en-us/node/builder/chat-reference/enums/_botbuilder_d_.resumereason.html) code returned in [`result.resumed`](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptresult.html#resumed) or simply check that [`result.response`](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptresult.html#response) is not `null`. There are actually a number of reasons that can cause the prompt to return without a response, so checking for a `null` response tends to be the best approach. In this example bot, if the user responds with "cancel", the bot would simply prompt for name again because the response would be `null`. 

## Starting and ending dialogs

To start another dialog (and push it onto the stack), use [`session.beginDialog()`](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#begindialog). To end a dialog (and remove it from the stack, returning control to the calling dialog), use either [`session.endDialog()`](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#enddialog) or [`session.endDialogWithResult()`](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#enddialogwithresult).

You can implement dialogs using the [waterfall](bot-builder-nodejs-dialog-waterfall.md) pattern as a simple, effective mechanism for driving conversations forward. The following code uses two waterfalls to prompt for the user's name and respond with a custom greeting. 

```javascript
bot.dialog('greetings', [
    function (session) {
        session.beginDialog('askName');
    },
    function (session, results) {
        session.send('Hello %s!', results.response);
    }
]);
bot.dialog('askName', [
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    },
    function (session, results) {
        session.endDialogWithResult(results);
    }
]);
```

The following snippet shows the output when this code sample is run using the [Bot Framework Emulator](../debug-bots-emulator.md).

```
restify listening to http://[::]:3978
ChatConnector: message received.
session.beginDialog(greetings)
/ - waterfall() step 1 of 2
/ - session.beginDialog(askName)
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

The output shows that the user sent two messages to the bot. The first message pushed the default dialog onto the stack, entering step 1 of the first waterfall. That step called `beginDialog()` and pushed the `askName` dialog onto the stack, entering step 1 of the second waterfall. That step then called [`Prompts.text()`](https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.__global.iprompts.html#text) to ask the user their name. [`Prompts`](bot-builder-nodejs-dialog-prompt.md) are themselves dialogs. In the output, the number of dots prefixing each line indicates the current depth of the dialog stack. You can tell the current stack depth by the number of dots prefixing each line, such as `..Prompts.text`.

When the user replies to the prompt for name, the `text()` prompt returns the user's input to the second waterfall using `endDialogWithResult()`. The waterfall then passes this value to step 2 which itself calls `endDialogWithResult()` to pass it back to the first waterfall. The first waterfall passes that result to step 2 which responds with the personalized greeting to the user.

This example uses `session.endDialogWithResult()` to return control back to the calling dialog and pass back a value (the user's input). Alternative approaches for returning control to the calling dialog include:

* Pass complex values back to the calling dialog by using `session.endDialogWithResult({ response: { name: 'joe smith', age: 37 } })`.
* Send the user a message by using `session.endDialog("ok… operation canceled")`.
* Simply return control to the calling dialog by using `session.endDialog()`.

When you start a dialog by using `session.beginDialog()`, you can optionally pass a set of arguments which lets you call dialogs the same way you would a function. The following example builds upon the previous example to show how to use arguments. This example prompts the user for profile information and then stores it for future conversations.

```javascript
bot.dialog('profile', [
    function (session) {
        session.beginDialog('ensureProfile', session.userData.profile);
    },
    function (session, results) {
        session.userData.profile = results.profile;
        session.send('Hello %s!', session.userData.profile.name);
    }
]);
bot.dialog('ensureProfile', [
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
        session.endDialogWithResult({ response: session.dialogData.profile })
    }
]);
```

The example uses [`session.userData`](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#userdata) to remember the user's `profile` between conversations. The default dialog gets the user's profile data and passes it to the `ensureProfile` dialog in the `beginDialog` call. 

If a dialog collects data over multiple steps, it should use [`session.dialogData`](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#dialogdata) to temporarily hold values being collected. The `ensureProfile` dialog uses `dialogData` to hold the `profile` object. Each step passes the `next()` function which is used to skip the step if the profile already contains that data element. For example, if the profile already contains the user's name, the waterfall proceeds to the next step and collects the user's email address.

After the profile dialog collects all of the profile data, it returns the profile data back to the default dialog which saves the profile in `userData`. For more information on storing user data, see [Manage state data](bot-builder-nodejs-state.md).

## Additional resources

- [Manage conversation flow](bot-builder-nodejs-dialog-manage-conversation.md)
- [Replace dialogs](bot-builder-nodejs-dialog-replace.md)
- [Define conversation steps with waterfalls](bot-builder-nodejs-dialog-waterfall.md)
- [Prompt for user input](bot-builder-nodejs-dialog-prompt.md)
- [Send and receive messages](bot-builder-nodejs-use-default-message-handler.md)
- [Manage state data](bot-builder-nodejs-state.md)
