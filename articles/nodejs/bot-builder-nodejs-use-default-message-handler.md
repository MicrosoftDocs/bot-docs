---
title: Send and receive messages | Microsoft Docs
description: Learn how to send and receive messages in the Bot Builder SDK for Node.js.
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---
# Send and receive messages

The simplest way to start sending and receiving messages is by using the default message handler. 

## Use the default message handler
Create a new [UniversalBot][UniversalBot] with a function to handle the messages received from a user 
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

* **Message handlers** - For a bot that's more complex than the previous example, you may want to use different forms of message handlers. You can add an *action* to a dialog to listen for user input as it occurs. See [Handle user actions](bot-builder-nodejs-dialog-actions.md) for information on using actions in your bot. Another form is a [waterfall](bot-builder-nodejs-dialog-waterfall.md), which is a common way to guide the user through a series of steps or prompt the user with a series of questions before taking action. 

* **Attachments, rich cards, suggested actions, and speech** - The contents of messages aren't limited to text strings. Your bot can also [send and receive attachments][SendAttachments], present the user with [rich cards][SendCardWithButtons] that contain images and buttons, [send suggested actions](bot-builder-nodejs-send-suggested-actions.md), and [send text to be spoken by your bot](bot-builder-nodejs-text-to-speech.md) on a speech-enabled channel.

## Additional resources

* [session.send][SessionSend]
* [sprintf][sprintf]
* [Send attachments][SendAttachments]
* [Add rich cards to messages][SendCardWithButtons]
* [Add speech to messages](bot-builder-nodejs-text-to-speech.md)
* [Send suggested actions](bot-builder-nodejs-send-suggested-actions.md)
* [Ask questions](bot-builder-nodejs-prompts.md)
* [Handle user actions](bot-builder-nodejs-dialog-actions.md)

[SendAttachments]: bot-builder-nodejs-send-receive-attachments.md
[SendCardWithButtons]: bot-builder-nodejs-send-rich-cards.md
[sprintf]: https://github.com/alexei/sprintf.js
[emulator]: ../debug-bots-emulator.md
[appId]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ichatconnectorsettings.html#appid
[appPassword]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ichatconnectorsettings.html#apppassword
[SessionSend]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#send
[UniversalBot]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot.html
[ChatConnector]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.chatconnector
