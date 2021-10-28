---
title: Language recognizer reference for Bot Framework bots
description: Adaptive dialogs and language recognizers work together to interpret user intent and to react fluidly to user input. This article describes builtin recognizers in the Bot Framework SDK.
keywords: bot, recognizers, adaptive dialogs
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: reference
ms.service: bot-service
ms.date: 10/18/2021
ms.custom: abs-meta-21q1
monikerRange: 'azure-bot-service-4.0'
---

# Language recognizer reference for Bot Framework bots

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Language recognizers let your bot interpret user input. Adaptive dialogs and language recognizers work together to interpret user intent and to react fluidly to user input. This article describes the builtin recognizers in the Bot Framework SDK and some of their key properties.

For information about how recognizer are used, see [Language understanding](/composer/concept-language-understanding) in the Bot Framework Composer documentation.

## Cross-trained recognizer set

The cross-trained recognizer set compares recognition results from more than one recognizer to decide a winner. Given a collection of recognizers, the cross-trained recognizer will:

* Promote the recognition result of one of the recognizers if all other recognizers defer recognition to a single recognizer. To defer recognition, a recognizer can return the `None` intent or an explicit `DeferToRecognizer_recognizerId` as intent.
* Raise an `OnChooseIntent` event to allow your code to choose which recognition result to use. Each recognizer's results are returned via the `turn.recognized.candidates` property. This enables you to choose the most appropriate result.

## Default recognizer

The default recognizer was created to replace the following recognizers:

* **LUIS recognizer** - to extract intents and entities from a user's utterance based on the defined Language Understanding (LUIS) service.
* **QnA Maker recognizer** - to extract intents from a user's utterance based on the defined QnA Maker service.
* **Cross-trained recognizer set** - to compare recognition results from more than one recognizer to decide a winner.

## LUIS recognizer

Language Understanding (LUIS) is a cloud-based API service that applies custom machine-learning intelligence to a user's conversational, natural language text to predict overall meaning, and pull out relevant, detailed information. The LUIS recognizer enables you to extract intents and entities from a user's utterance based on the defined LUIS application, which you train in advance.

> [!TIP]
> For more information about how to incorporate language understanding into your bot using LUIS, see:
>
> * [Add LUIS for language understanding][update-the-recognizer-type-to-luis]
> * [LUIS.ai][4] is a machine learning-based service that enables you to build natural language capabilities into your bot.
> * [What is LUIS][5]
> * [Create a new LUIS app in the LUIS portal][11]
> * [Language Understanding][6]
> * [.lu file format][7]
> * [Adaptive expressions][8]

## Multi-language recognizer

When building a sophisticated multi-lingual bot, you'll typically have one recognizer for each language and locale. The Multi-language recognizer enables you to easily specify the recognizer to use based on the [locale][3] property on the incoming activity from a user.

For more information, see the [Multilingual support](/composer/how-to-use-multiple-language) article in the Composer documentation.

## Orchestrator recognizer

[Orchestrator][] is a language understanding solution optimized for conversational AI applications. It replaces the Bot Framework Dispatcher. The Orchestrator recognizer enables you to extract an intent from a user's utterance, which could be used to route to an appropriate skill or recognizer, such as LUIS or QnA Maker.

> [!TIP]
> For more information about how to incorporate language understanding into your bot using Orchestrator, see:
>
> * [What is Orchestrator][Orchestrator]
> * [BF Orchestrator CLI][15]

## QnA Maker recognizer

[QnAMaker.ai][12] is one of the [Microsoft Cognitive Services][13] that enables you to create rich question-answer pairs from existing content - documents, URLs, PDFs, and so on. You can use the QnA Maker recognizer to integrate with the service.

> [!NOTE]
> The QnA Maker recognizer will emit a `QnAMatch` event, which you can handle with an `OnQnAMatch` trigger.
> The entire QnA Maker response will be available in the `answer` property.

## Recognizer set

Sometimes you might need to run more than one recognizer on every turn of the conversation. The recognizer set does exactly that. All recognizers are run on each turn of the conversation and the result is a union of all recognition results.

## Regular expression (regex) recognizer

The _Regex recognizer_ uses regular expressions to extract intent and entity data from an utterance.

The Regex recognizer consists primarily of:

* `Intents`. The `Intents` object contains a list of `IntentPattern` objects, and these `IntentPattern` objects consist of an `Intent` property that is the name of the intent, and a `Pattern` property that contains a regular expression used to parse the utterance to determine intent.
* `Entities`. The `Entities` object contains a list of `EntityRecognizer` objects. The Bot Framework SDK defines several `EntityRecognizer` classes to help you determine the entities contained in a user's utterance:
  * `AgeEntityRecognizer`
  * `ConfirmationEntityRecognizer`
  * `CurrencyEntityRecognizer`
  * `DateTimeEntityRecognizer`
  * `DimensionEntityRecognizer`
  * `EmailEntityRecognizer`
  * `EntityRecognizer`
  * `EntityRecognizerSet`
  * `GuidEntityRecognizer`
  * `HashtagEntityRecognizer`
  * `IpEntityRecognizer`
  * `MentionEntityRecognizer`
  * `NumberEntityRecognizer`
  * `NumberRangeEntityRecognizer`
  * `OrdinalEntityRecognizer`
  * `PercentageEntityRecognizer`
  * `PhoneNumberEntityRecognizer`
  * `RegExEntityRecognizer`
  * `TemperatureEntityRecognizer`
  * `TextEntity`
  * `TextEntityRecognizer`
  * `UrlEntityRecognizer`

> [!TIP]
>
> * The Regex recognizer emits a "None" intent when the input utterance does not match any defined intent. You can create an `OnIntent` trigger with `Intent = "None"` to handle this scenario.
> * The Regex recognizer is useful for testing and quick prototyping. For more sophisticated bots we recommend using the Language Understanding (LUIS) recognizer.
> * You might find the [Regular expression language quick reference][2] helpful.

## Additional Information

* [What is LUIS][5]
* [Language Understanding][6]
* [.lu file format][7]
* [Adaptive expressions][8]
* [Extract data from utterance text with intents and entities][9]
* [Add natural language understanding (LU) to your bot][10]
* [Add natural language generation (LG) to your bot][1]

<!-- Footnote-style links -->
[1]:../v4sdk/bot-builder-concept-adaptive-dialog-generators.md
[2]:/dotnet/standard/base-types/regular-expression-language-quick-reference
[3]:https://github.com/microsoft/botbuilder/blob/master/specs/botframework-activity/botframework-activity.md#locale
[4]:https://luis.ai
[5]:/azure/cognitive-services/luis/what-is-luis
[6]:../v4sdk/bot-builder-concept-luis.md
[7]:../file-format/bot-builder-lu-file-format.md
[8]:../v4sdk/bot-builder-concept-adaptive-expressions.md
[9]:/azure/cognitive-services/luis/luis-concept-data-extraction?tabs=V2
[10]:../v4sdk/bot-builder-howto-v4-luis.md
[11]:/azure/cognitive-services/luis/luis-how-to-start-new-app
[12]:https://qnamaker.ai
[13]:https://azure.microsoft.com/services/cognitive-services/
[Orchestrator]:/composer/concept-orchestrator
[15]:https://github.com/microsoft/botframework-cli/tree/main/packages/orchestrator
[cross-train-concepts]: ../v4sdk/bot-builder-concept-cross-train.md
[luis-to-luis-cross-training]: ../v4sdk/bot-builder-concept-cross-train.md#luis-to-luis-cross-training
[qnamaker-cross-train]: https://github.com/microsoft/botframework-cli/tree/main/packages/cli#bf-qnamakercross-train
[bf-luiscross-train]: https://github.com/microsoft/botframework-cli/tree/main/packages/cli#bf-luiscross-train
[cs-sample-todo-bot]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/adaptive-dialog/08.todo-bot-luis-qnamaker
[howto-cross-train]: ../v4sdk/bot-builder-howto-cross-train.md
[update-the-recognizer-type-to-luis]: /composer/how-to-add-luis#update-the-recognizer-type-to-luis
