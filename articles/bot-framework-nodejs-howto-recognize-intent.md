---
title: Recognize intent | Microsoft Docs
description: Learn how to recognize the user's intent in a conversational application (bot).
keywords: bot framework, bot, intent, recognize, recognizer
author: DeniseMak
manager: rstand
ms.topic: develop-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/24/2017
ms.reviewer: rstand
#ROBOTS: Index
---

# Recognize user intent using the Bot Builder SDK for Node.js

<!-- Need to create NET stub.
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-send-card-buttons.md)  
> * [Node.js](bot-framework-nodejs-howto-send-card-buttons.md)
>
--> 
## Introduction

When the users ask your bot for something, like "help" or "find news", your bot needs to understand what the user is asking for. 
You can design your bot to recognize a set of intents that interpret the user’s input in terms of the intention it conveys.
This article demonstrates how to register a custom intent recognizer that will be run for every message received from the user. 
Custom recognizers can return a named intent that can be used to trigger actions and dialogs within the bot.


## Example: Register a custom intent recognizer
This specific example adds a recognizer that looks for the user to say 'help' or 'goodbye' but you could easily add a 
recognizer that looks for the user to send an image or calls an external web service to determine the user's intent.


> [!NOTE]
> This content will be updated.

<!-- TODO: use code snippet standard -->
```javascript

var builder = require('../../core/');

// Create bot and default message handler
var connector = new builder.ConsoleConnector().listen();
var bot = new builder.UniversalBot(connector, function (session) {
    session.send("You said: '%s'. Try asking for 'help' or say 'goodbye' to quit", session.message.text);
});



// Install a custom recognizer to look for user saying 'help' or 'goodbye'.
bot.recognizer({
  recognize: function (context, done) {
  var intent = { score: 0.0 };

        if (context.message.text) {
            switch (context.message.text.toLowerCase()) {
                case 'help':
                    intent = { score: 1.0, intent: 'Help' };
                    break;
                case 'goodbye':
                    intent = { score: 1.0, intent: 'Goodbye' };
                    break;
            }
        }
        done(null, intent);
    }
});



// Add a help dialog with a trigger action that is bound to the 'Help' intent
bot.dialog('helpDialog', function (session) {
    session.endDialog("This bot will echo back anything you say. Say 'goodbye' to quit.");
}).triggerAction({ matches: 'Help' });



// Add a global endConversation() action that is bound to the 'Goodbye' intent
bot.endConversationAction('goodbyeAction', "Ok... See you later.", { matches: 'Goodbye' });

```


## Additional resources

In this article, we discussed how to register a custom intent recognizer using the Bot Builder SDK for Node.js. 
To learn more, see:

> [!NOTE]
> To do: Add links to related content 

[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage