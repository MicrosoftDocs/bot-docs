---
title: Understanding message handlers and dialogs| Microsoft Docs
description: Learn how to start sending and receiving messages by using the default message handler in the Bot Builder SDK for Node.js.
keywords: Bot Framework, dialog, send messages, conversation flow, conversation, node.js, node, Bot Builder, SDK
author: DeniseMak
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/31/2017
ms.reviewer:
#ROBOTS: Index
---
# Understanding message handlers and dialogs

This article teaches you the basics of how to start managing conversation flow using message handlers and dialogs in your bot.

## Using the default message handler

The simplest way to start sending and receiving messages is by using the default message handler. 
To do this, create a new [UniversalBot][UniversalBot] with a function to handle the messages received from a user, 
and pass this object to your [ChatConnector][ChatConnector].

## Respond to user messages

```javascript
var restify = require('restify');
var builder = require('botbuilder');

var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () { });

// Create the chat connector for communicating with the Bot Framework Service
var connector = new builder.ChatConnector({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

// Listen for messages from users 
server.post('/api/messages', connector.listen());

// Create your bot with a function to receive messages from the user
var bot = new builder.UniversalBot(connector, function (session) {
    // echo the user's message
    session.send("You said: %s", session.message.text);
});
```

> [!NOTE] 
> The **ChatConnector** class implements all of the logic needed to communicate with the Bot Framework Connector Service and the [Bot Framework Emulator][emulator]. 
> The [appId][appId] and [appPassword][appPassword] configuration parameters aren’t required when communicating with the emulator but you should ensure that they’re properly configured when you deploy your bot to the cloud. 

Your message handler takes a session object which can be used to read the user's message and compose replies. 
The [session.send()][SessionSend] method, which sends a reply to the user who sent the message, supports a flexible template syntax for formatting strings.
For details about the template syntax, refer to the documentation for the [sprintf][sprintf] library.

The contents of messages aren't limited to text strings. 
Your bot can [send and receive attachments][SendAttachments], as well as present the user with [rich cards][SendCardWithButtons] that contain images and buttons.

## Invoke a root dialog

Dialogs help you encapsulate your bot's conversational logic in manageable components. 
The Bot Builder SDK provides Dialog objects that help you manage conversation flow. The following example shows 
demonstrates how a bot invokes a "root dialog", instead of using the message handler passed to the bot's constructor, and then invokes a child dialog from the root dialog. 
<!-- The following example shows how to wire the basic HTTP GET call to a controller and then invoke the root dialog. -->

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
bot.dialog('/', ...
```

### Invoke a child dialog from the root dialog

Next, the root dialog invokes a 'New Order' dialog. 

```javascript
bot.dialog('/', new builder.IntentDialog()
// Did the user type 'order'?
.matchesAny([/order/i], [ 
    function (session) {
        // Invoke the new order dialog
        session.beginDialog('/newOrder');
    },

    function (session, result) {
        // Store the value that the new order dialog returns
        var resultFromNewOrder = result.response;

        session.send('New order dialog just told me this: %s', resultFromNewOrder);
        // Close the root dialog
        session.endDialog(); 
    }
])
```


## Dialog lifecycle

When a dialog is invoked, it takes control of the conversation flow. 
Every new message will be subject to processing by that dialog until it either closes or redirects to another dialog. 

In Node, you can invoke one dialog from another by using `session.beginDialog()`. 
To close a dialog and remove it from the stack (thereby sending the user back to the prior dialog in the stack), use `session.endDialog()`. 

## Ending a conversation

At any time you can end the current conversation with a user by calling session.endConversation(). 
This will immediately end any active dialogs and reset everything but session.userData, ensuring that the user is returned to a clean state.

It’s useful to give the user a way of ending the conversation themselves by saying a phrase like “goodbye”. 
You can achieve this by adding an `endConversationAction()` to your bot.

```javascript
// Create bot and default message handler
var bot = new builder.UniversalBot(connector, function (session) {
    session.send("Hi... We sell shirts. Say 'show shirts' to see our products.");
}).endConversationAction('goodbye', "Ok... See you next time.", { matches: /^goodbye/i });

```


## Next steps

* [Send attachments][SendAttachments]
* [Send cards][SendCardWithButtons]

* [Ask questions](~/nodejs/prompts.md)

* [Listen for messages using actions]( ~/nodejs/global-handlers.md)


[SendAttachments]: ~/nodejs/send-receive-attachments.md
[SendCardWithButtons]: ~/nodejs/send-card-buttons.md
[sprintf]: https://github.com/alexei/sprintf.js
[emulator]: ~/debug-bots-emulator.md
[appId]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ichatconnectorsettings.html#appid
[appPassword]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ichatconnectorsettings.html#apppassword
[SessionSend]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#send
[UniversalBot]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot.html
[ChatConnector]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.chatconnector