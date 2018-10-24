---
title: Conduct audio calls | Microsoft Docs
description: Learn how to conduct audio calls with Skype in a bot using Node.js
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/17
monikerRange: 'azure-bot-service-3.0'
---

# Support audio calls with Skype

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

Skype supports a rich feature called Calling Bots.  When enabled, users can place a voice call to your bot and interact with it using Interactive Voice Response (IVR).  The Bot Builder for Node.js SDK includes a special [Calling SDK][calling_sdk] which developers can use to add calling features to their chat bot.   

The Calling SDK is very similar to the [Chat SDK][chat_sdk]. They have similar classes, share common constructs and you can even use the Chat SDK to send a message to the user you’re on a call with.  The two SDKs are designed to run side-by-side but while they are similar, there are some significant differences and you should generally avoid mixing classes from the two libraries.  

## Create a calling bot
The following example code shows how the "Hello World" for a calling bot looks very similar to a regular chat bot. 

```javascript
var restify = require('restify');
var calling = require('botbuilder-calling');

// Setup Restify Server
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
   console.log(`${server.name} listening to ${server.url}`); 
});

// Create calling bot
var connector = new calling.CallConnector({
    callbackUrl: 'https://<your host>/api/calls',
    appId: '<your bots app id>',
    appPassword: '<your bots app password>'
});
var bot = new calling.UniversalCallBot(connector);
server.post('/api/calls', connector.listen());

// Add root dialog
bot.dialog('/', function (session) {
    session.send('Watson... come here!');
});
```

> [!NOTE]
> To find your bot's **AppID** and **AppPassword**, see [MicrosoftAppID and MicrosoftAppPassword](~/bot-service-manage-overview.md#microsoftappid-and-microsoftapppassword).

The emulator doesn’t currently support testing calling bots. To test your bot, you’ll need to go through most of the steps required to publish your bot.  You will also need to use a Skype client to interact with the bot. 

### Enable the Skype channel
[Register your bot](../bot-service-quickstart-registration.md) and enable the Skype channel. You will need to provide a messaging endpoint when you register your bot. It is recommended that you pair your calling bot with a chat bot so the chat bot’s endpoint is what you would normally put in that field.  If you’re only registering a calling bot you can simply paste your calling endpoint into that field.  

To enable the actual calling feature you’ll need to go into the Skype channel for your bot and turn on the calling feature. You’ll then be provided with a field to copy your calling endpoint into. Make sure you use the https ngrok link for the host portion of your calling endpoint.

During the registration of your bot you’ll be assigned an app ID & password which you should paste into the connector settings for your hello world bot. You’ll also need to take your full calling link and paste that in for the callbackUrl setting.

### Add bot to contacts
On your bot's registration page in the developer portal you’ll see an **add to Skype** button next to your bots Skype channel. Click the button to add your bot to your contact list in Skype.  Once you do that you (and anyone you give the join link to) will be able to communicate with the bot.

### Test your bot
You can test your bot using a Skype client. You should notice the call icon light up when you click on your bots contact entry (you may have to search for the bot to see it.)  It can take a few minutes for the call icon to light up if you’ve added calling to an existing bot.  

If you press the call button it should dial your bot and you should hear it speak “Watson… come here!” and then hang up.

## Calling basics
The [UniversalCallBot](http://docs.botframework.com/en-us/node/builder/calling-reference/classes/_botbuilder_d_.universalcallbot) and [CallConnector](http://docs.botframework.com/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callconnector) classes let you author a calling bot in much the same way you would a chat bot. You add dialogs to your bot that are essentially identical to [chat dialogs](bot-builder-nodejs-manage-conversation-flow.md). You can add [waterfalls](bot-builder-nodejs-prompts.md) to your bot. There is a session object, the [CallSession](http://docs.botframework.com/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession) class, which contains added [answer()](http://docs.botframework.com/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#answer), [hangup()](http://docs.botframework.com/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#hangup), and [reject()](http://docs.botframework.com/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#reject) methods for managing the current call. In general, you don’t need to worry about these call control methods though as the CallSession has logic to automatically manage the call for you. The session will automatically answer the call if you take an action like sending a message or calling a built-in prompt. It will also automatically hangup/reject the call if you call [endConversation()](http://docs.botframework.com/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#endconversation) or it detects that you’ve stopped asking the caller questions (you didn’t call a built-in prompt.)

Another difference between calling and chat bots is that while chat bots typically send messages, cards, and keyboards to a user a calling bot deals in Actions and Outcomes. Skype calling bots are required to create [workflows](http://docs.botframework.com/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.iworkflow) that are comprised of one or more [actions](http://docs.botframework.com/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.iaction).  This is another thing that in practice you don’t have to worry too much about as the Bot Builder calling SDK will manage most of this for you. The [CallSession.send()](http://docs.botframework.com/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#send) method lets you pass either actions or strings which it will turn into [PlayPromptActions](http://docs.botframework.com/en-us/node/builder/calling-reference/classes/_botbuilder_d_.playpromptaction).  The session contains auto batching logic to combine multiple actions into a single workflow that’s submitted to the calling service so you can safely call send() multiple times.  And you should rely on the SDK’s built-in [prompts](bot-builder-nodejs-prompts.md) to collect input from the user as they process all of the outcomes.  

[calling_sdk]: http://docs.botframework.com/en-us/node/builder/calling-reference/modules/_botbuilder_d_
[chat_sdk]: http://docs.botframework.com/en-us/node/builder/chat-reference/modules/_botbuilder_d_
