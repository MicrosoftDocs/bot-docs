---
title: Respond to user messages | Microsoft Docs
description: Learn how to start sending and receiving messages by using message handlers in the Bot Builder SDK for Node.js.
keywords: Bot Framework, dialog, send messages, conversation flow, conversation, node.js, node, Bot Builder, SDK
author: DeniseMak
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/31/2017
ms.reviewer:
ROBOTS: Index, Follow
---
# Respond to user messages

This article teaches you the basics of how to start responding to user messages in your bot.

## Use the default message handler

The simplest way to start sending and receiving messages is by using the default message handler. 
To do this, create a new [UniversalBot][UniversalBot] with a function to handle the messages received from a user, 
and pass this object to your [ChatConnector][ChatConnector].


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

## Next steps

* **Message handlers** - For a bot that's more complex that the previous example, you may want to use different forms of message handlers. You can add an *action* to a dialog to listen for user input as it occurs. See [Listen for messages using actions](global-handlers.md) for information on using actions in your bot. Another form is a *waterfall*, which is a common way to guide the user through a series of steps or prompt the user with a series of questions. See [Ask questions](prompts.md) for information on waterfalls.
* **Attachments and cards** - The contents of messages aren't limited to text strings. Your bot can [send and receive attachments][SendAttachments], as well as present the user with [rich cards][SendCardWithButtons] that contain images and buttons.



## Additional resources

* [session.send][SessionSend]
* [sprintf][sprintf]
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