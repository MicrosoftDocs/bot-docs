---
title: Use the defaul message handler | Microsoft Docs
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
# Using the default message handler

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

## Additional resources

* [Send attachments][SendAttachments]
* [Send cards][SendCardWithButtons]


[SendAttachments]: ~/nodejs/send-receive-attachments.md
[SendCardWithButtons]: ~/nodejs/send-card-buttons.md
[sprintf]: https://github.com/alexei/sprintf.js
[emulator]: ~/debug-bots-emulator.md
[appId]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ichatconnectorsettings.html#appid
[appPassword]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ichatconnectorsettings.html#apppassword
[SessionSend]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#send
[UniversalBot]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot.html
[ChatConnector]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.chatconnector