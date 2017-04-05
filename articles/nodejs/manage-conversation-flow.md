---
title: Manage conversation flow | Microsoft Docs
description: Learn how to manage conversation flow using dialogs and the Bot Builder SDK for Node.js.
keywords: Bot Framework, dialog, messages, conversation flow, conversation, node.js, node, Bot Builder, SDK
author: DeniseMak
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 02/17/2017
ms.reviewer:
#ROBOTS: Index
---
# Manage conversation flow

This article describes how to send messages and manage conversation flow using the Bot Builder SDK for Node.js.

## Using the default message handler

The simplest way to start sending and receiving messages is by using the default message handler. 
To do this, create a new [UniversalBot][UniversalBot] with a function to handle the messages received from a user, 
and pass this object to your [ChatConnector][ChatConnector].

```javascript
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () { });

var connector = new builder.ChatConnector({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

server.post('/api/messages', connector.listen());

// Create your bot with a function to receive messages from the user
var bot = new builder.UniversalBot(connector, function (session) {
    session.send("You said: %s", session.message.text);
});
```

Your message handler takes a session object which can be used to read the user's message and compose replies. 
The session.send() method, which sends a reply to the user who sent the message, supports a flexible template syntax for formatting strings.
For details about the template syntax, refer to the documentation for the [sprintf][sprintf] library.

The contents of messages aren't limited to text strings. 
Your bot can [send and receive attachments][SendAttachments], as well as present the user with [cards containing images and buttons][SendCardWithButtons].

## Listen for commands

The Bot Builder SDK provides a number of other constructs that are designed to help you implement some of the more common patterns found in bots. 
One of those patterns is the ability to perform some action in response to the user sending a command.
First let’s look at how we might extend the sample code above to support letting the user ask for “help”. 

```javascript
// Create your bot with a function to receive messages from the user
var bot = new builder.UniversalBot(connector, function (session) {
var msg = session.message;
    if (/^help/i.test(msg.text || '')) {
        // Send user help message
        session.send("I'll repeat back anything you say or send.");
    } else if (msg.attachments && msg.attachments.length > 0) {
        // Echo back attachment
        session.send({
            text: "You sent:",
            attachments: [
                msg.attachments[0]
            ]
        });
    } else {
        // Echo back uesrs text
        session.send("You said: %s", session.message.text);
    }
});
```
We’ve extended our sample to include a regular expression that looks for the users to send a message starting with “help”, to which it will reply with a help message. 
This is one way to listen for commands but you can imagine that as the list of commands your bot supports grows, this function can quickly get large and difficult to follow.
Instead, you can use the SDK’s dialog system, along with a global handler called a *trigger action*, to accomplish the same thing. 

```javascript
// Create your bot with a function to receive messages from the user
var bot = new builder.UniversalBot(connector, function (session) {
    var msg = session.message;
    if (msg.attachments && msg.attachments.length > 0) {
        // Echo back attachment
        session.send({
            text: "You sent:",
            attachments: [
                msg.attachments[0]
            ]
        });
    } else {
        // Echo back uesrs text
        session.send("You said: %s", session.message.text);
    }
});

// Add help dialog
bot.dialog('help', function (session) {
    session.send("I'll repeat back anything you say or send.").endDialog();
}).triggerAction({ matches: /^help/i });

```

Instead of adding logic to the main message handler for our bot, we’ve add a dialog named ‘help’ that 
has a [triggerAction](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialog#triggeraction) handler that specifies 
a [matches](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.itriggeractionoptions#matches) clause with our regular expression. 

> [!NOTE]
> Your can also add *recognizers* to your bot to determine the user's intent as soon as they send a message. If you use recognizers, **triggerAction** can be associated with a *named intent* instead of a regular expression. See [Recognize user intent][RecognizeUserIntent] for more information.


Dialogs let you break your bots logic into logical components designed to perform a single task. In this example the ‘help’ dialog is designed to provide the user with help anytime they ask for it.
The trigger action defines the rules for when this dialog should be started, which is anytime the user says “help”.
The actual code for the dialog is very similar to our original sample but includes a call to [endDialog()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#enddialog) after the call to send(). 

When a dialog is triggered it receives all future messages from the user until a call to endDialog() is made. 
This behavior is one of the primary advantages to using dialogs but the call to endDialog() is sometimes overlooked, 
leading to the appearance of being stuck. In our example, anytime the user asks for “help” we’ll send a quick message and then end the dialog to return control back to our main message handler. 

## Ask questions

Another common pattern is for a bot to ask the user a sequence of questions before performing some action.
The SDK provides a set of built-in prompts to simplify collecting input from a user. You can then use a feature called a *waterfall* to define the sequence in which to prompt the user. 


### Prompts

These built-in prompts are implemented as a dialog so they’ll return the users response through a call to [session.endDialogWithresult()][EndDialogWithResult]. Any type of dialog message handler can receive the result of a prompt but waterfalls tend to be the simplest way to handle a prompt result.

Prompts return to the caller an [IPromptResult][IPromptResult]. The user's response will be contained in the [results.response][Result_Response] field and may be null. There are multiple reasons for the response to be null. The built-in prompts allow the user to cancel an action by saying something like ‘cancel’ or ‘nevermind’ which results in a null response. Or the user may fail to enter a properly formatted response which can also result in a null response. The exact reason can be determined by examining the [ResumeReason][ResumeReason] returned in [result.resumed][Result_Resumed].

| Prompt type | Description | User response type |
|------|------|------|
| [Prompts.text][PromptsText] | Asks the user for a string of text. | [IPromptTextResult][IPromptTextResult] |
| [Prompts.confirm][PromptsConfirm]  | Asks the user to confirm an action. | [IPromptConfirmResult][IPromptConfirmResult] |
| [Prompts.number][PromptsNumber]  | Asks the user to enter a number. | [IPromptNumberResult][IPromptNumberResult] |
| [Prompts.time][PromptsTime]  | Asks the user for a time or date. The response is an [entity][entity] that can be resolved into a JavaScript Date object using [EntityRecognizer.resolveTime()][ResolveTime].| [IPromptTimeResult][IPromptTimeResult] |
| [Prompts.choice][PromptsChoice]  | Asks the user to choose from a list of choices. | [IPromptChoiceResult][IPromptChoiceResult] |
| [Prompts.attachment][PromptsAttachment]  | Asks the user to upload a picture or video. | [IPromptAttachmentResult][IPromptAttachmentResult] |


### Using a waterfall

In the following example the bot's message handler takes an array of functions (a waterfall) instead of a single function.
When a user sends a message to our bot, the first function in our waterfall will get called and we can use 
the [text()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts#text) prompt to ask the user for their name. 
Their response will be passed to the second function in our waterfall which will send the user a customized greeting.
This cascading sequence of questions and responses is what gives the waterfall feature its name. 

```javascript
// Create your bot with a waterfall to ask user their name
var bot = new builder.UniversalBot(connector, [
    function (session) {
        builder.Prompts.text(session, "Hello... What's your name?");
    },
    function (session, results) {
        var name = results.response;
        session.send("Hi %s!", name);
    }
]);

```


If you run the previous waterfall example, you’ll notice that while it will ask for your name and give you a personalized greeting it won’t remember your name.
After the last step of the waterfall is reached it will simply start over with the next message. 
To add logic to remember the user's name so that we only have to ask it once, see [Save user data](~/nodejs/save-user-data.md) for an example. 

<!-- TODO: Make this a section on global handlers/ actions -->

## Handling cancel

Waterfalls are powerful but what if the bot is asking the user a series of questions and the user decides they’d like to cancel what they’re doing?
You can easily support that by adding a [cancelAction()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialog#cancelaction) to your dialog.
We can update our ‘buyButtonClick’ dialog with a cancel action that triggers anytime the user says the word “cancel”. 

```javascript
// Add dialog to handle 'Buy' button click
bot.dialog('buyButtonClick', [ ... waterfall steps ... ])
    .triggerAction({ matches: /(buy|add)\s.*shirt/i })
    .cancelAction('cancelBuy', "Ok... Item canceled", { matches: /^cancel/i });

```

The important thing to note is that this action will only be triggered if the dialog is active, so in our case, this is only when the user is being asked what size shirt they’d like.

## Confirming interruptions

By default, when the user says something that triggers a dialog it will automatically interrupt any dialogs that are active. 
In most cases you’ll find that this is desirable, but they’re may be times where you’re doing something large or complex and you’d like to just confirm that 
canceling the active dialog is what the user wants to do.
This can be easily accomplished by adding a [confirmPrompt](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.itriggeractionoptions#confirmprompt) 
option to your triggerAction() or cancelAction(). 

```javascript
// Add dialog to handle 'Buy' button click
bot.dialog('buyButtonClick', [ ... waterfall steps ... ])
    .triggerAction({ 
        matches: /(buy|add)\s.*shirt/i
        confirmPrompt: "This will cancel adding the current item. Are you sure?" 
    })
    .cancelAction('cancelBuy', "Ok... Item canceled", { 
        matches: /^cancel/i,
        confirmPrompt: "are you sure?" 
    });

```


The cancel action's confirmPrompt will be used anytime the user says “cancel” and the trigger action's confirmPrompt will be used anytime another dialog 
is triggered (including the ‘buyButtonClick’ dialog itself) and attempts to interrupt the dialog. 

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

### Ending a conversation

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

## Send a typing indicator for long-running tasks

<!-- TODO: This is related to sending images -->

Users of your bot will expect a timely response to their message. If your bot performs some long running task like calling a server or executing a query, without giving the user some indication that the bot heard them, the user could get impatient and send additional messages to the bot. Many channels support the sending of a typing indication to simply show the user that their message was received and is being processed.

The following example demonstrates how to send a typing indication using [session.sendTyping()][SendTyping].  


```javascript

// Create bot and default message handler
var bot = new builder.UniversalBot(connector, function (session) {
    session.sendTyping();
    setTimeout(function () {
        session.send("Hello there...");
    }, 3000);
});


```





## Additional resources

- [Send and receive attachments][SendAttachments]
- [Send cards with buttons][SendCardWithButtons]
- [Recognize user intent][RecognizeUserIntent]
- [Save user data](~/nodejs/save-user-data.md)

<!-- TODO: Update links to point to new docs when available -->

[SendAttachments]: ~/nodejs/send-receive-attachments.md
[SendCardWithButtons]: ~/nodejs/send-card-buttons.md
[RecognizeUserIntent]: ~/nodejs/recognize-intent.md
[SaveUserData]: ~/nodejs/save-user-data.md



[UniversalBot]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot.html
[ChatConnector]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.chatconnector.html
[sprintf]: http://www.diveintojavascript.com/projects/javascript-sprintf
[Session]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session


[SendTyping]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#sendtyping
[EndDialogWithResult]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#enddialogwithresult
[IPromptResult]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptresult.html
[Result_Response]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptresult.html#reponse
[ResumeReason]: https://docs.botframework.com/en-us/node/builder/chat-reference/enums/_botbuilder_d_.resumereason.html
[Result_Resumed]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptresult.html#resumed

[entity]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ientity.html
[ResolveTime]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.entityrecognizer.html#resolvetime
[PromptsText]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts.html#text
[IPromptTextResult]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iprompttextresult.html
[PromptsConfirm]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts.html#confirm
[IPromptConfirmResult]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptconfirmresult.html
[PromptsNumber]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts.html#number 
[IPromptNumberResult]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptnumberresult.html
[PromptsTime]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts.html#time
[IPromptTimeResult]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iprompttimeresult.html
[PromptsChoice]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts.html#choice
[IPromptChoiceResult]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptchoiceresult.html
[PromptsAttachment]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts.html#attachment
[IPromptAttachmentResult]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptattachmentresult.html

