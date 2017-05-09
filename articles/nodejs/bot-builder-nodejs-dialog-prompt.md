---
title: Prompt users for input | Microsoft Docs
description: Learn how to use prompts to collect user input in your bot with the Bot Builder SDK for Node.js
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 05/03/2017
---
# Prompt users for input

The SDK provides a set of built-in prompts to simplify collecting input from a user. 

> [!NOTE] 
> A *prompt* is best used when the bot is going to perform an action in direct response to the input: Stop, Repeat, Delete.
> To ask the user a *series* of questions, create a [waterfall](bot-builder-nodejs-dialog-waterfall.md).

## Prompt results 
Built-in prompts are implemented as [dialogs](bot-builder-nodejs-dialog-manage-conversation.md). They return the user's response by calling [session.endDialogWithresult()][EndDialogWithResult]. Any type of dialog message handler can receive the result of a prompt.

Prompts return an [IPromptResult][IPromptResult] to the caller. The user's response will be contained in the [results.response][Result_Response] field and may be `null`. 

The most common reasons for a `null` response are: 
* The user cancelled an action by saying something like ‘cancel’ or ‘nevermind’ 
* The user entered an improperly formatted response

The exact reason for a `null` response can be determined by examining the [ResumeReason][ResumeReason] returned in [result.resumed][Result_Resumed]. There are actually a number of reasons that can cause the prompt to return without a response, so checking for  [result.response](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptresult.html#response) isn't null tends to be the best approach.

## Prompt types
The Bot Builder SDK for Node.js includes the following built-in prompts:

|**Prompt Type**     | **Description**                              |     
| -------------------| ---------------------------------------------
|[Prompts.text][PromptsText] | Asks the user to enter a string of text. |     
|[Prompts.confirm][PromptsConfirm] | Asks the user to confirm an action.| 
|[Prompts.number][PromptsNumber] | Asks the user to enter a number.     |
|[Prompts.time][PromptsTime] | Asks the user for the time or date.      |
|[Prompts.choice][PromptsChoice] | Asks the user to choose from a list of choices.    |
|[Prompts.attachment][PromptsAttachment] | Asks the user to upload a picture or video.|       
The following sections provide more details about each of these prompt types.

### Text String: `Prompts.text()`
The [Prompts.text()][PromptsText] method asks the user for a **string of text**. The prompt returns the user's response as an [IPromptTextResult][IPromptTextResult] interface.

```javascript
builder.Prompts.text(session, "What is your name?");
```

### Yes / No:  `Prompts.confirm()`

The [Prompts.confirm()][PromptsConfirm] method asks the user to confirm an action with a **yes/no** response. The prompt returns the user's response as an [IPromptConfirmResult](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptconfirmresult.html) interface.

```javascript
builder.Prompts.confirm(session, "Are you sure you wish to cancel your order?");
```
### Numerical response: `Prompts.number()`

The [Prompts.number()][PromptsNumber] method asks the user to reply with a **number**. The prompt returns the user's response as an [IPromptNumberResult](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptnumberresult.html).

```javascript
builder.Prompts.number(session, "How many would you like to order?");
```

### Time and Date: `Prompts.time()`

The [Prompts.time()][PromptsTime] method asks the user to reply with the **time**. The prompt returns the user's response as an [IPromptTimeResult](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iprompttimeresult.html) interface. The framework uses the [Chrono](https://github.com/wanasit/chrono) library to parse the user's response and supports both relative ("in 5 minutes") and non-relative ("June 6th at 2pm") types of responses.

The [results.response](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iprompttimeresult.html#response) contains an [entity](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ientity.html) object, which contains the date and time. To resolve the date and time into a JavaScript `Date` object, use the [EntityRecognizer.resolveTime()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.entityrecognizer.html#resolvetime) method.

```javascript
bot.dialog('/createAlarm', [
    function (session) {
        session.dialogData.alarm = {};
        builder.Prompts.text(session, "What would you like to name this alarm?");
    },
    function (session, results, next) {
        if (results.response) {
            session.dialogData.name = results.response;
            builder.Prompts.time(session, "What time would you like to set an alarm for?");
        } else {
            next();
        }
    },
    function (session, results) {
        if (results.response) {
            session.dialogData.time = builder.EntityRecognizer.resolveTime([results.response]);
        }
        
        // Return alarm to caller  
        if (session.dialogData.name && session.dialogData.time) {
            session.endDialogWithResult({ 
                response: { name: session.dialogData.name, time: session.dialogData.time } 
            }); 
        } else {
            session.endDialogWithResult({
                resumed: builder.ResumeReason.notCompleted
            });
        }
    }
]);
```
### Choose from a list: `Prompts.choice()`

The [Prompts.choice()][PromptsChoice] method asks the user to **choose from a list** of choices. The prompt returns the user's response as an [IPromptChoiceResult](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptchoiceresult.html) interface. To specify the style of the list that's shown to the user, use the [IPromptOptions.listStyle](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptoptions.html#liststyle) property. The user can express their choice by either entering the number associated with the choice or the choice's name itself. Both full and partial matches of the option's name are supported.

To specify the list of choices, you can use a pipe-delimited (`|`) string:

```javascript
builder.Prompts.choice(session, "Which color?", "red|green|blue");
```

An array of strings:

```javascript
builder.Prompts.choice(session, "Which color?", ["red","green","blue"]);
```

Or an object map. With this option, the object's keys are used to determine the choice.

```javascript
var salesData = {
    "west": {
        units: 200,
        total: "$6,000"
    },
    "central": {
        units: 100,
        total: "$3,000"
    },
    "east": {
        units: 300,
        total: "$9,000"
    }
};

bot.dialog('/', [
    function (session) {
        builder.Prompts.choice(session, "Which region would you like sales for?", salesData); 
    },
    function (session, results) {
        if (results.response) {
            var region = salesData[results.response.entity];
            session.send("We sold %(units)d units for a total of %(total)s.", region); 
        } else {
            session.send("ok");
        }
    }
]);
```
### Upload an attachment: `Prompts.attachment()`

The [Prompts.attachment()][PromptsAttachment] method asks the user to *upload a file attachment* like an image or video. The prompt returns the user's response as an [IPromptAttachmentResult](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptattachmentresult.html) interface.

```javascript
builder.Prompts.attachment(session, "Upload a picture for me to transform.");
```

## Additional resources
- [Define conversation steps with waterfalls](bot-builder-nodejs-dialog-waterfall.md)
- [Send and receive attachments](bot-builder-nodejs-send-receive-attachments.md)
- [Save user data](~/nodejs/bot-builder-nodejs-save-user-data.md)
- [Prompts interface][PromptsRef]


[SendAttachments]: ~/nodejs/bot-builder-nodejs-send-receive-attachments.md
[SendCardWithButtons]: ~/nodejs/bot-builder-nodejs-send-rich-cards.md
[RecognizeUserIntent]: ~/nodejs/bot-builder-nodejs-recognize-intent.md
[SaveUserData]: ~/nodejs/bot-builder-nodejs-save-user-data.md

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

[PromptsRef]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.__global.iprompts.html

[PromptsText]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.__global.iprompts.html#text

[IPromptTextResult]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iprompttextresult.html

[PromptsConfirm]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.__global.iprompts.html#confirm

[IPromptConfirmResult]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptconfirmresult.html

[PromptsNumber]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.__global.iprompts.html#number

[IPromptNumberResult]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptnumberresult.html

[PromptsTime]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.__global.iprompts.html#time

[IPromptTimeResult]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iprompttimeresult.html

[PromptsChoice]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.__global.iprompts.html#choice

[IPromptChoiceResult]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptchoiceresult.html

[PromptsAttachment]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.__global.iprompts.html#attachment

[IPromptAttachmentResult]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptattachmentresult.html
