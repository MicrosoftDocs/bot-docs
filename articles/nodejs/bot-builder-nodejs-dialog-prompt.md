---
title: Prompt for user input | Microsoft Docs
description: Learn how to use prompts to collect user input with the Bot Builder SDK for Node.js.
author: v-ducvo
ms.author: v-ducvo
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 06/20/2017
---

# Prompt for user input

The Bot Builder SDK for Node.js provides a set of built-in prompts to simplify collecting inputs from a user. 

A *prompt* is used whenever a bot needs input from the user. You can use prompts to ask a user for a series of inputs by chaining the prompts in a waterfall. You can use prompts in conjunction with [waterfall](bot-builder-nodejs-dialog-waterfall.md) to help you [manage conversation flow](bot-builder-nodejs-manage-conversation-flow.md) in your bot. 

This article will help you understand how prompts work and how you can use them to collect information from users.

## Prompts and responses

Whenever you need input from a user, you can send a prompt, wait for the user to respond with input, and then process the input and send a response to the user.

The following code sample prompts the user for their name and responds with a greeting message.

```javascript
bot.dialog('greetings', [
    // Step 1
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    },
    // Step 2
    function (session, results) {
        session.endDialog('Hello %s!', results.response);
    }
]);
```

Using this basic construct, you can model your conversation flow by adding as many prompts and responses as your bot requires.

## Prompt results 

Built-in prompts are implemented as [dialogs](bot-builder-nodejs-dialog-overview.md) that return the user's response in the `results.response` field. For JSON objects, responses are returned in the `results.response.entity` field. Any type of [dialog handler](bot-builder-nodejs-dialog-overview.md#dialog-handlers) can receive the result of a prompt. Once the bot receives a response, it can consume it or pass it back to the calling dialog by calling the [`session.endDialogWithResult`][EndDialogWithResult] method.

The following code sample shows how to return a prompt result to the calling dialog by using the `session.endDialogWithResult` method. In this example, the `greetings` dialog uses the prompt result that the `askName` dialog returns to greet the user by name.

```javascript
// Ask the user for their name and greet them by name.
bot.dialog('greetings', [
    function (session) {
        session.beginDialog('askName');
    },
    function (session, results) {
        session.endDialog('Hello %s!', results.response);
    }
]);
bot.dialog('askName', [
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    },
    function (session, results) {
        session.endDialogWithResult(results);
    }
]);
```

### Null response

Prompts return an [IPromptResult][IPromptResult] to the caller, where the user's response is contained within the [results.response][Result_Response] field. 

At times, the `results.response` field may be `null`. The most common reasons for a `null` response are: 

* The user cancelled an action by saying something such as "cancel" or "nevermind".
* The user entered an improperly formatted response.
* The user have exceeded the maximum number of retries.

To determine the reason for a `null` response, examine the [ResumeReason][ResumeReason] that is returned in [result.resumed][Result_Resumed] field. For example:

```javascript
if(builder.ResumeReason[results.resumed] === "canceled"){
    // The previous prompt was canceled. 
}
```

Since there are a number of reasons that a prompt may return with a `null` response, it is good practice to always check for a `null` value in the [results.response][Result_Response] field. In cases where the response is `null`, you might choose to examine the `results.resumed` field to determine the reason for the `null` response so that your bot can handle each case specifically.

The following code sample shows how to determine the meaning of the `results.resumed` field value in the context of the `greetings` dialog.

```javascript
// Ask the user for their name and greet them by name.
bot.dialog('greetings', [
    function (session) {
        session.beginDialog('askName');
    },
    function (session, results) {
        if(!results.response){
            switch(builder.ResumeReason[results.resumed]){
                case "canceled":
                    session.endDialog("Ok. Canceled.");
                    break;
                case "back":
                    session.send("Ok, Let's back up a step.");
                    session.replaceDialog("greetings"); 
                    break;
                case "forward":
                    session.endDialog("Ok. skipping.");
                    break;
                case "notCompleted":
                    session.endDialog("Sorry, I didn't catch your name.");
                    break;
                // Other cases to catch...
            }
        }
        else{
            session.endDialog('Hello %s!', results.response);
        }
    }
]);
bot.dialog('askName', [
    function (session) {
        builder.Prompts.text(session, 'Hi! What is your name?');
    },
    function (session, results) {
        if(results.response){
            if(results.response.match(/^back$/i)){
                // User request to go back a step
                session.endDialogWithResult({
                    response: null,
                    resumed: builder.ResumeReason.back
                });
            }
            else if(results.response.match(/^forward$/i)){
                // User request to skip this step
                session.endDialogWithResult({
                    response: null,
                    resumed: builder.ResumeReason.forward
                });
            }
            else{
                session.endDialogWithResult(results);
            }
        }
        else{
            // We didn't get a response
            session.endDialogWithResult({
                resumed: builder.ResumeReason.notCompleted
            });
        }
    }
])
.cancelAction("cancel", "Type 'Hi' to continue.", 
    {
        matches: /^cancel$/i,
        confirmPrompt: "Are you sure?"
    }
);
```

## Prompt types
The Bot Builder SDK for Node.js includes several different types of built-in prompts. 

|**Prompt type**     | **Description**                              |     
| -------------------| ---------------------------------------------
|[Prompts.text][PromptsText] | Asks the user to enter a string of text. |     
|[Prompts.confirm][PromptsConfirm] | Asks the user to confirm an action.| 
|[Prompts.number][PromptsNumber] | Asks the user to enter a number.     |
|[Prompts.time][PromptsTime] | Asks the user for a time or date/time.      |
|[Prompts.choice][PromptsChoice] | Asks the user to choose from a list of options.    |
|[Prompts.attachment][PromptsAttachment] | Asks the user to upload a picture or video.|       

The following sections provide additional details about each type of prompt.

### Prompts.text

Use the [Prompts.text()][PromptsText] method to ask the user for a **string of text**. The prompt returns the user's response as an [IPromptTextResult][IPromptTextResult].

```javascript
builder.Prompts.text(session, "What is your name?");
```

### Prompts.confirm

Use the [Prompts.confirm()][PromptsConfirm] method to ask the user to confirm an action with a **yes/no** response. The prompt returns the user's response as an [IPromptConfirmResult](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptconfirmresult.html).

```javascript
builder.Prompts.confirm(session, "Are you sure you wish to cancel your order?");
```

### Prompts.number

Use the [Prompts.number()][PromptsNumber] method to ask the user for a **number**. The prompt returns the user's response as an [IPromptNumberResult](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptnumberresult.html).

```javascript
builder.Prompts.number(session, "How many would you like to order?");
```

### Prompts.time

Use the [Prompts.time()][PromptsTime] method to ask the user for a **time** or **date/time**. The prompt returns the user's response as an [IPromptTimeResult](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iprompttimeresult.html). The framework uses the [Chrono](https://github.com/wanasit/chrono) library to parse the user's response and supports both relative responses (e.g., "in 5 minutes") and non-relative responses (e.g., "June 6th at 2pm").

The [results.response](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iprompttimeresult.html#response) field, which represents the user's response, contains an [entity](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ientity.html) object that specifies the date and time. To resolve the date and time into a JavaScript `Date` object, use the [EntityRecognizer.resolveTime()](http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.entityrecognizer.html#resolvetime) method.

> [!TIP] 
> The time that the user enters is converted to UTC time based upon the time zone of the server that hosts the bot. Since the server may be located in a different time zone than the user, be sure to take time zones into consideration. To convert date and time to the user's local time, consider asking the user what time zone they are in.

```javascript
bot.dialog('createAlarm', [
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

### Prompts.choice

Use the [Prompts.choice()][PromptsChoice] method to ask the user to **choose from a list of options**. The user can convey their selection either by entering the number associated with the option that they choose or by entering the name of the option that they choose. Both full and partial matches of the option's name are supported. The prompt returns the user's response as an [IPromptChoiceResult](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptchoiceresult.html). 

To specify the style of the list that is presented to the user, set the [IPromptOptions.listStyle](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptoptions.html#liststyle) property. The following table shows the `ListStyle` enumeration values for this property.

| Index | Name | Description |
| ---- | ---- | ---- |
| 0 | none | No list is rendered. Use this style of list when the list is included as part of a prompt. |
| 1 | inline | Choices are rendered as an inline list of the form "1. red, 2. green, or 3. blue". |
| 2 | list | Choices are rendered as a numbered list. |
| 3 | button | On channels that support buttons, choices are rendered as buttons. On channels that do not support buttons, choices are rendered as text. |
| 4 | auto | The style is selected automatically based on the channel and number of options. | 

You can access this enum from the `builder` object or you can specify the style by name. For example, both statements in the following code snippet accomplish the same thing.

```javascript
// ListStyle specified as enum
builder.Prompts.choice(session, "Which color?", "red|green|blue", builder.ListStyle.button);

// ListStyle specified by name
builder.Prompts.choice(session, "Which color?", "red|green|blue", "button");
```

To specify the list of options, you can use a pipe-delimited (`|`) string, an array of strings, or an object map.

A pipe-delimited string: 

```javascript
builder.Prompts.choice(session, "Which color?", "red|green|blue");
```

An array of strings:

```javascript
builder.Prompts.choice(session, "Which color?", ["red","green","blue"]);
```

An object map: 

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

bot.dialog('getSalesData', [
    function (session) {
        builder.Prompts.choice(session, "Which region would you like sales for?", salesData); 
    },
    function (session, results) {
        if (results.response) {
            var region = salesData[results.response.entity];
            session.send("We sold %(units)d units for a total of %(total)s.", region); 
        } else {
            session.send("OK");
        }
    }
]);
```

### Prompts.attachment

Use the [Prompts.attachment()][PromptsAttachment] method to ask the user to upload a file such an image or video. The prompt returns the user's response as an [IPromptAttachmentResult](http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ipromptattachmentresult.html).

```javascript
builder.Prompts.attachment(session, "Upload a picture for me to transform.");
```

## Additional resources
- [Dialogs overview](bot-builder-nodejs-dialog-overview.md)
- [Manage conversation flow](bot-builder-nodejs-dialog-manage-conversation-flow.md)
- [Replace dialogs](bot-builder-nodejs-dialog-replace.md)
- [Define conversation steps with waterfalls](bot-builder-nodejs-dialog-waterfall.md)
- [Listen for messages by using actions](bot-builder-nodejs-global-handlers.md)
- [Manage state data](bot-builder-nodejs-state.md)
- [Prompts Reference interface][PromptsRef]


[SendAttachments]: bot-builder-nodejs-send-receive-attachments.md
[SendCardWithButtons]: bot-builder-nodejs-send-rich-cards.md
[RecognizeUserIntent]: bot-builder-nodejs-recognize-intent.md
[SaveUserData]: bot-builder-nodejs-save-user-data.md

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
