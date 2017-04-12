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
ROBOTS: Index, Follow
---
# Using prompts

A common pattern is for a bot to ask the user a sequence of questions before performing some action.
The SDK provides a set of built-in prompts to simplify collecting input from a user. You can then use a feature called a *waterfall* to define the sequence in which to prompt the user. 

## Prompts

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


## Using a waterfall

In the following example the bot's message handler takes an array of functions (a waterfall) instead of a single function.
When a user sends a message to our bot, the first function in our waterfall will get called and we can use 
the [text()][text] prompt to ask the user for their name. 
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


## Additional resources

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

[text]: http://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts#text