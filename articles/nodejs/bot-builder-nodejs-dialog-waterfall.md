---
title: Define conversation steps with waterfalls | Microsoft Docs
description: Learn how to use waterfalls to define the steps of a conversation in a bot with the Bot Builder SDK for Node.js
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 04/25/2017
---
# Define conversation steps with waterfalls

A waterfall is a specific type of dialog handler. A waterfall contains an array of functions where the *results* of the first function are passed as *input* to the next function and so on. The bot prompts the user for input, waits for a response, and then passes the result to the next step. Waterfalls are most commonly used to collect information from the user. 

## Prompt the user with a series of questions
The Bot Builder SDK provides built-in [prompts](bot-builder-nodejs-dialog-prompt.md) to easily ask the user a series of questions. 
Waterfalls guide the conversation by prompting the user's progress from one step to the next. 

This sample dialog is a two-step waterfall. The first step asks for the user's name and the second step greets the user by name.

```javascript
bot.dialog('/', [
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    },
    function (session, results) {
        session.send('Hello %s!', results.response);
    }
]);
```
 You can also create a child dialog to advance the conversation to the next step. This is a useful way of partitioning the conversation if you have multiple fields to populate. The following example calls `session.beginDialog()` to bring up a child dialog instead of a prompt.  

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

All waterfalls contain a phantom last step which automatically returns the result from the final step. 
The previous example can be simplified by removing the call to `session.endDialogWithResult()`.

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
    }
]);
```

## Advance the waterfall
The first step of a waterfall can receive arguments passed to the dialog. 

Every step after that receives a `next()` function that can be used to advance the waterfall to the next step. Note that the `next()` function can receive an `IDialogResult` object containing results already returned from a previous dialog. Your bot doesn't have to ask for the user's name more than once; it can just pass the results from the first time.

The following example shows how to pair these two features together to create an `/ensureProfile` dialog that walks the user through completing a user profile. The bot prompts the user for responses and populates the profile fields with the results it receives. When the user has finished, the bot verifies that the user's profile is complete. If the profile is not complete, the user is prompted for the missing information. 

```javascript
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
```
If the bot is distributed across multiple compute nodes, every step of the waterfall could be processed by a different node. The `/ensureProfile` dialog uses [session.dialogData](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#dialogdata) to temporarily store the user's profile. The `dialogData` field ensures that the dialog's state is properly maintained between each step of the conversation. While you can store anything in the `dialogData` field, you should limit yourself to JavaScript primitives that can be properly serialized. 

## Additional resources
- [Prompts class][PromptsRef]

[PromptsRef]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts.html