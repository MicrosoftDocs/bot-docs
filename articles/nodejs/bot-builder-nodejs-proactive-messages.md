---
title: Send proactive messages | Microsoft Docs
description:  Learn how to interrupt the current conversation flow with a proactive message using the Bot Framework SDK for Node.js
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---
# Send proactive messages
[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-proactive-messages.md)
> - [Node.js](../nodejs/bot-builder-nodejs-proactive-messages.md)

[!INCLUDE [Introduction to proactive messages - part 1](../includes/snippet-proactive-messages-intro-1.md)]

## Types of proactive messages

[!INCLUDE [Introduction to proactive messages - part 2](../includes/snippet-proactive-messages-intro-2.md)]

## Send an ad hoc proactive message

The following code samples show how to send an ad hoc proactive message by using the Bot Framework SDK for Node.js.

To be able to send an ad hoc message to a user, the bot must first collect and save information about the user from the current conversation. 
The **address** property of the message includes all of the information that the bot will need to send an ad hoc message to the user later. 

```javascript
bot.dialog('adhocDialog', function(session, args) {
    var savedAddress = session.message.address;

    // (Save this information somewhere that it can be accessed later, such as in a database, or session.userData)
    session.userData.savedAddress = savedAddress;

    var message = 'Hello user, good to meet you! I now know your address and can send you notifications in the future.';
    session.send(message);
})
```

> [!NOTE]
> The bot can store the user data in any manner as long as the bot can access it later.

After the bot has collected information about the user, it can send an ad hoc proactive message to the user at any time. 
To do so, it simply retrieves the user data that it stored previously, constructs the message, and sends it.

```javascript
var inMemoryStorage = new builder.MemoryBotStorage();

var bot = new builder.UniversalBot(connector)
                .set('storage', inMemoryStorage); // Register in-memory storage 

function sendProactiveMessage(address) {
    var msg = new builder.Message().address(address);
    msg.text('Hello, this is a notification');
    msg.textLocale('en-US');
    bot.send(msg);
}
```

> [!TIP]
> An ad hoc proactive message can be initiated like from 
> asynchronous triggers such as http requests, timers, queues or from anywhere else that the developer chooses.

## Send a dialog-based proactive message

The following code samples show how to send a dialog-based proactive message by using the Bot Framework SDK for Node.js. You can find the complete working example in the [startNewDialog](https://aka.ms/js-startnewdialog-sample-v3) folder.

To be able to send a dialog-based message to a user, the bot must first collect (and save) information from the current conversation. 
The `session.message.address` object includes all of the information that the bot will need to send a dialog-based proactive message to the user. 

```javascript
// proactiveDialog dialog
bot.dialog('proactiveDialog', function (session, args) {

    savedAddress = session.message.address;

    var message = 'Hey there, I\'m going to interrupt our conversation and start a survey in five seconds...';
    session.send(message);

    message = `You can also make me send a message by accessing: http://localhost:${server.address().port}/api/CustomWebApi`;
    session.send(message);

    setTimeout(() => {
        startProactiveDialog(savedAddress);
    }, 5000);
});
```

When it is time to send the message, the bot creates a new dialog and adds it to the top of the dialog stack. The new dialog takes control of the conversation, delivers the proactive message, closes, and then returns control to the previous dialog in the stack. 

```javascript
// initiate a dialog proactively 
function startProactiveDialog(address) {
    bot.beginDialog(address, "*:survey");
}
```

> [!NOTE]
> The code sample above requires a custom file, **botadapter.js**, which you can [download from GitHub](https://aka.ms/js-botadaptor-file-v3).

The survey dialog controls the conversation until it finishes. 
Then, it closes (by calling `session.endDialog()`), thereby returning control back to the previous dialog. 


```javascript
// handle the proactive initiated dialog
bot.dialog('survey', function (session, args, next) {
  if (session.message.text === "done") {
    session.send("Great, back to the original conversation");
    session.endDialog();
  } else {
    session.send('Hello, I\'m the survey dialog. I\'m interrupting your conversation to ask you a question. Type "done" to resume');
  }
});
```

## Sample code

For a complete sample that shows how to send proactive messages using the Bot Framework SDK for Node.js, see the <a href="https://aka.ms/js-proactivemessages-sample-v3" target="_blank">Proactive Messages sample</a> in GitHub. 
Within the Proactive Messages sample, <a href="https://aka.ms/js-simplesendmessage-sample-v3" target="_blank">simpleSendMessage</a> shows how to send an ad-hoc proactive message and <a href="https://aka.ms/js-startnewdialog-sample-v3" target="_blank">startNewDialog</a> shows how to send a dialog-based proactive message.

## Additional resources

- [Designing conversation flow](../bot-service-design-conversation-flow.md)
