---
title: Manage conversation flow with dialogs| Microsoft Docs
description: Learn how to combine complex conversation flows into manageable Dialogs using the Bot Builder SDK for Node.js.
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---

# Manage message flow with dialogs

Dialogs help you encapsulate your bot's conversational logic into manageable components. The Bot Builder SDK provides [Dialog][Dialogclass] objects that help you manage conversation flow. 

## Root and child dialogs

Bots typically have at least one root ‘/’ dialog. When the framework receives a message from the user it will be routed to this root ‘/’ dialog for processing. For many bots, this single root ‘/’ dialog is all that’s needed but bots will often have multiple dialogs.

The following example shows demonstrates how a bot invokes a root dialog instead of using the message handler passed to the bot's constructor. It then invokes a child dialog from the root dialog. The first message from a user will be routed to the dialog handler for the root ‘/’ dialog. This function gets passed a [session][session] object which can be used to inspect the user's message, send a reply to the user, save state on behalf of the user, or redirect to another dialog.

```javascript
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () { });

var connector = new builder.ChatConnector({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

var bot = new builder.UniversalBot(connector);
server.post('/api/messages', connector.listen());

// root dialog
bot.dialog('/', [
    function (session, args, next) {
        if (!session.userData.name) {
            // The root dialog invokes a 'profile' dialog.
            session.beginDialog('/profile');
        } else {
            next();
        }
    },
    function (session, results) {
        session.send('Hello %s!', session.userData.name);
    }
]);

// profile dialog
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

## Dialog handlers

Message handlers for a dialog can take different forms. You can add an *action* to a dialog to listen for user input as it occurs. See [Listen for messages using actions](bot-builder-nodejs-global-handlers.md) for information on using actions in your bot.

Another form is a *waterfall*, which is a common way to guide the user through a series of steps or prompt the user with a series of questions. See [waterfall prompt sequences](bot-builder-nodejs-prompts.md) for information on waterfalls.


## Dialog lifecycle

When a dialog is invoked, it takes control of the conversation flow. 
Every new message will be subject to processing by that dialog until it either closes or redirects to another dialog. 

In Node, you can invoke one dialog from another by using `session.beginDialog()`. 
To close a dialog and remove it from the stack, use `session.endDialog()`. This will return the user to the previous dialog in the stack.

## Ending a conversation

You can end the current conversation with a user at any time by calling `session.endConversation()`. 
This will immediately end any active dialogs and reset everything but `session.userData`, ensuring that the user is returned to a clean state.

It’s useful to give the user a way to end the conversation themselves by saying a phrase like “goodbye”. 
You can achieve this by adding an `endConversationAction()` to your bot.

```javascript
// Create bot and default message handler
var bot = new builder.UniversalBot(connector, function (session) {
    session.send("Hi... We sell shirts. Say 'show shirts' to see our products.");
}).endConversationAction('goodbye', "Ok... See you next time.", { matches: /^goodbye/i });

```


## Additional resources

* [Send attachments][SendAttachments]
* [Send cards][SendCardWithButtons]

* [Ask questions](~/nodejs/bot-builder-nodejs-prompts.md)

* [Listen for messages using actions]( ~/nodejs/bot-builder-nodejs-global-handlers.md)


[DialogClass]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialog.html
[session]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session
[SendAttachments]: ~/nodejs/bot-builder-nodejs-send-receive-attachments.md
[SendCardWithButtons]: ~/nodejs/bot-builder-nodejs-send-rich-cards.md
[sprintf]: https://github.com/alexei/sprintf.js
[emulator]: ~/debug-bots-emulator.md
[appId]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ichatconnectorsettings.html#appid
[appPassword]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ichatconnectorsettings.html#apppassword
[SessionSend]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#send
[UniversalBot]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot.html
[ChatConnector]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.chatconnector