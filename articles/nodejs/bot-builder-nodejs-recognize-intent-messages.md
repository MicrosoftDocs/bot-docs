---
title: Recognize intent from message content | Microsoft Docs
description: Learn how to recognize the user's intent by using regular expressions or checking the message content.
author: DeniseMak
ms.author: v-demak
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Recognize user intent from message content

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

When your bot receives a message from a user, your bot can use a **recognizer** to examine the message and determine intent. The intent provides a mapping from messages to dialogs to invoke. This article explains how to recognize intent using regular expressions or by inspecting the content of a message. For example, a bot can use regular expressions to check if a message contains the word "help" and invoke a help dialog. A bot can also check the properties of the user message, for example, to see if the user sent an image instead of text and invoke an image processing dialog. 

> [!NOTE]
> For information on recognizing intent using LUIS, see [Recognize intents and entities with LUIS](bot-builder-nodejs-recognize-intent-luis.md) 


## Use the built-in regular expression recognizer
Use [RegExpRecognizer][RegExpRecognizer] to detect the user's intent using a regular expression. You can pass multiple expressions to the recognizer to support multiple languages. 

> [!TIP]
> See [Support localization](bot-builder-nodejs-localization.md) for more information on localizing your bot.

The following code creates a regular expression recognizer named `CancelIntent` and adds it to your bot. The recognizer in this example provides regular expressions for both the `en_us` and `ja_jp` locales. 

[!code-js[Add a regular expression recognizer (JavaScript)](../includes/code/node-regex-recognizer.js#addRegexRecognizer)]

Once the recognizer is added to your bot, attach a [triggerAction][triggerAction] to the dialog that you want the bot to invoke when the recognizer detects the intent. Use the [matches][matches] option to specify the intent name, as shown in the following code:

[!code-js[Map the CancelIntent recognizer to a cancel dialog (JavaScript)](../includes/code/node-regex-recognizer.js#bindCancelDialogToRegexRecognizer)]

Intent recognizers are global, which means that the recognizer will run for every message received from the user. If a recognizer detects an intent that is bound to a dialog using a `triggerAction`, it can trigger interruption of the currently active dialog. Allowing and handling interruptions is a flexible design that accounts for what users really do.

> [!TIP] 
> To learn how a `triggerAction` works with dialogs, see [Managing conversation flow](bot-builder-nodejs-manage-conversation-flow.md). To learn more about the various actions you can associate with a recognized intent, [Handle user actions](bot-builder-nodejs-dialog-actions.md).

## Register a custom intent recognizer
You can also implement a custom recognizer. This example adds a simple recognizer that looks for the user to say 'help' or 'goodbye' but you could easily add a recognizer that does more complex processing, such as one that recognizes when the user sends an image. 


[!code-js[Add a custom recognizer (JavaScript)](../includes/code/node-howto-recognize-intent.js#addCustomRecognizer)]

Once you've registered a recognizer, you can associate the recognizer with an action using a `matches` clause.

[!code-js[Bind intents to actions (JavaScript)](../includes/code/node-howto-recognize-intent.js#bindIntentsToActions)]

## Disambiguate between multiple intents

Your bot can register more than one recognizer. Notice that the custom recognizer example involves assigning a numerical score to each intent. This is done since your bot may have more than one recognizer, and the Bot Framework SDK provides built-in logic to disambiguate between intents returned by multiple recognizers. The score assigned to an intent is typically between 0.0 and 1.0, but a custom recognizer may define an intent greater than 1.1 to ensure that that intent will always be chosen by the Bot Framework SDK's disambiguation logic. 

By default, recognizers run in parallel, but you can set recognizeOrder in [IIntentRecognizerSetOptions][IntentRecognizerSetOptions] so the process quits as soon as your bot finds one that gives a score of 1.0.

The Bot Framework SDK includes a [sample][DisambiguationSample] that demonstrates how to provide custom disambiguation logic in your bot by implementing [IDisambiguateRouteHandler][IDisambiguateRouteHandler].

## Next steps
The logic for using regular expressions and inspecting message contents can become complex, especially if your bot's conversational flow is open-ended. To help your bot handle a wider variety of textual and spoken input from users, you can use an intent recognition service like [LUIS][LUIS] to add natural language understanding to your bot.

> [!div class="nextstepaction"]
> [Recognize intents and entities with LUIS](bot-builder-nodejs-recognize-intent-luis.md)


[LUIS]: https://www.luis.ai/

[triggerAction]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialog.html#triggeraction

[matches]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.itriggeractionoptions.html#matches

[node-js-bot-how-to]: bot-builder-nodejs-recognize-intent-luis.md

[LUISAzureDocs]: /azure/cognitive-services/LUIS/Home

[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage

[IntentRecognizerSetOptions]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iintentrecognizersetoptions.html

[LuisRecognizer]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.luisrecognizer

[LUISSample]: https://aka.ms/v3-js-luisSample

[LUISConcepts]: https://docs.botframework.com/en-us/node/builder/guides/understanding-natural-language/

[DisambiguationSample]: https://aka.ms/v3-js-onDisambiguateRoute

[IDisambiguateRouteHandler]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.idisambiguateroutehandler.html

[RegExpRecognizer]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.regexprecognizer.html

[AlarmBot]: https://aka.ms/v3-js-luisSample
