---
title: Use a prompt with a waterfall | Microsoft Docs
description: Learn how to collect information from the user through prompts included in the Bot Builder SDK for Node.js.
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---

# Prompts and waterfalls

A common pattern is for a bot to ask the user a sequence of questions before performing some action.
The SDK provides a set of built-in prompts to simplify collecting input from a user. You can then use a feature called a *waterfall* to define the sequence in which to prompt the user. 

## Prompt results 

Since built-in prompts are implemented as a dialog, they’ll return the user's response through a call to [session.endDialogWithresult()][EndDialogWithResult]. Any type of dialog message handler can receive the result of a prompt, but waterfalls tend to be the simplest way to handle a prompt result.

Prompts return an [IPromptResult][IPromptResult] to the caller. The user's response will be contained in the [results.response][Result_Response] field and may be null. The exact reason can be determined by examining the [ResumeReason][ResumeReason] returned in [result.resumed][Result_Resumed], but the most common reasons for a null response are: 
* The user cancelled an action by saying something like ‘cancel’ or ‘nevermind’ 
* The user failed to enter a properly formatted response 

## Prompt types

| Prompt type | Description | User response type |
|------|------|------|
| [Prompts.text][PromptsText] | Asks the user for a string of text. | [IPromptTextResult][IPromptTextResult] |
| [Prompts.confirm][PromptsConfirm]  | Asks the user to confirm an action. | [IPromptConfirmResult][IPromptConfirmResult] |
| [Prompts.number][PromptsNumber]  | Asks the user to enter a number. | [IPromptNumberResult][IPromptNumberResult] |
| [Prompts.time][PromptsTime]  | Asks the user for a time or date. The response is an [entity][entity] that can be resolved into a JavaScript Date object using [EntityRecognizer.resolveTime()][ResolveTime].| [IPromptTimeResult][IPromptTimeResult] |
| [Prompts.choice][PromptsChoice]  | Asks the user to choose from a list of choices. | [IPromptChoiceResult][IPromptChoiceResult] |
| [Prompts.attachment][PromptsAttachment]  | Asks the user to upload a picture or video. | [IPromptAttachmentResult][IPromptAttachmentResult] |

## Create a waterfall

In the following example the bot's message handler takes an array of functions, called a *waterfall*, instead of a single function.
When a user sends a message to our bot, the first function in the waterfall will be called. The bot will use the [text()][text] prompt to ask the user for their name. 
The user's response will be passed to the second function in the waterfall which will send the user a greeting customized with their name.
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

If you run the previous waterfall example, you’ll notice that while it will ask for your name and give you a personalized greeting, it won’t remember your name.
After the last step of the waterfall is reached, it will simply start over with the next message. 
To add logic to remember the user's name so that we only have to ask for it once, see [Save user data](~/nodejs/bot-builder-nodejs-save-user-data.md). 


## Additional resources

- [Save user data](~/nodejs/bot-builder-nodejs-save-user-data.md)
- [Prompts class][PromptsRef]


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
[PromptsRef]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts.html
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

[text]: http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts#text