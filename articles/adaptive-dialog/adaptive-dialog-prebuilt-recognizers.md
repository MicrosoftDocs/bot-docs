---
title: Recognizers in adaptive dialogs - reference guide
description: Describing the adaptive dialog prebuilt recognizers
keywords: bot, recognizers, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 04/30/2021
monikerRange: 'azure-bot-service-4.0'
---

# Recognizers in adaptive dialogs - reference guide

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Recognizers enable your bot to understand user input and an adaptive dialog can have one or more recognizers configured. For additional information on recognizers see the [Language understanding](/composer/concept-language-understanding) article in the Composer documentation.

## RegexRecognizer

The _RegEx recognizer_ gives you the ability to extract intent and entity data from an utterance based on regular expression patterns.

`RegexRecognizer` consists primarily of:

* `Intents`. The `Intents` object contains a list of `IntentPattern` objects and these `IntentPattern` objects consist of an `Intent` property which is the name of the intent, and a `Pattern` property that contains a regular expression used to parse the utterance to determine intent.
* `Entities`. The `Entities` object contains a list of `EntityRecognizer` objects.  The Bot Framework SDK defines several `EntityRecognizer` classes to help you determine the entities contained in a users utterance:
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
> * `RegexRecognizer` will emit a 'None' intent when the input utterance does not match any defined intent. You can create an `OnIntent` trigger with `Intent = "None"` to handle this scenario.
> * `RegexRecognizer` is useful for testing and quick prototyping. For more sophisticated bots we recommend using the LUIS recognizer.
> * You might find the regular expression language (RegEx) [Quick Reference][2] helpful.

## Default recognizer

The default recognizer was created to replace the following recognizers:

* **None** - do not use recognizer.
* **LUIS recognizer** - to extract intents and entities from a user's utterance based on the defined LUIS application.
* **QnA Maker recognizer** - to extract intents from a user's utterance based on the defined QnAMaker application.
* **Cross-trained recognizer set** - to compare recognition results from more than one recognizer to decide a winner.

## LUIS recognizer

Language Understanding Intelligent Service (LUIS) is a cloud-based API service that applies custom machine-learning intelligence to a user's conversational, natural language text to predict overall meaning, and pull out relevant, detailed information. The LUIS recognizer enables you to extract intents and entities from a users utterance based on the defined LUIS application, which you train in advance.

> [!TIP]
> The following information will help you learn more about how to incorporate language understanding (LU) into your bot using LUIS:
>
> * [Add LUIS for language understanding][update-the-recognizer-type-to-luis]
* [LUIS.ai][4] is a machine learning-based service that enables you to build natural language capabilities into your bot.
> * [What is LUIS][5]
> * [Create a new LUIS app in the LUIS portal][11]
> * [Language Understanding][6]
> * [.lu file format][7]
> * [Adaptive expressions][8]

## QnA Maker Recognizer

[QnAMaker.ai][12] is one of the [Microsoft Cognitive Services][13] that enables you to create rich question-answer pairs from existing content - documents, URLs, PDFs, and so on. You can use the QnA Maker recognizer to integrate with the service.

> [!NOTE]
> QnA Maker Recognizer will emit a `QnAMatch`event which you can handle with an `OnQnAMatch` trigger.
> The entire QnA Maker response will be available in the `answer` property.

## Orchestrator recognizer

[Orchestrator][14] is a language understanding solution optimized for conversational AI applications.  It is a replacement of the Bot Framework Dispatcher, introduced in 2018.  The Orchestrator recognizer enables you to extract an intent from a users utterance, which could be used to route to an appropriate skill or recognizer, such as LUIS or QnA Maker.

> [!TIP]
> The following information will help you learn more about how to incorporate language understanding into your bot using Orchestrator:
>
> * [What is Orchestrator][14]
> * [BF Orchestrator CLI][15]

## Multi-language recognizer

When building a sophisticated multi-lingual bot, you will typically have one recognizer tied to a specific language and locale. The Multi-language recognizer enables you to easily specify the recognizer to use based on the [locale][3] property on the incoming activity from a user.

For information see the [Multilingual support](/composer/how-to-use-multiple-language) article in the Composer documentation.

## Recognizer set

Sometimes you might need to run more than one recognizer on every turn of the conversation. The recognizer set does exactly that. All recognizers are run on each turn of the conversation and the result is a union of all recognition results.

## Cross-trained recognizer set

The cross-trained recognizer set compares recognition results from more than one recognizer to decide a winner. Given a collection of recognizers, the cross-trained recognizer will:

* Promote the recognition result of one of the recognizer if all other recognizers defer recognition to a single recognizer. To defer recognition, a recognizer can return the `None` intent or an explicit `DeferToRecognizer_recognizerId` as intent.
* Raise an `OnChooseIntent` event to allow your code to choose which recognition result to use. Each recognizer's results are returned via the `turn.recognized.candidates` property. This enables you to choose the most appropriate result.

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
[14]:https://aka.ms/bf-orchestrator
[15]:https://github.com/microsoft/botframework-cli/tree/main/packages/orchestrator
[cross-train-concepts]: ../v4sdk/bot-builder-concept-cross-train.md
[luis-to-luis-cross-training]: ../v4sdk/bot-builder-concept-cross-train.md#luis-to-luis-cross-training
[qnamaker-cross-train]: https://github.com/microsoft/botframework-cli/tree/main/packages/cli#bf-qnamakercross-train
[bf-luiscross-train]: https://github.com/microsoft/botframework-cli/tree/main/packages/cli#bf-luiscross-train
[cs-sample-todo-bot]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/adaptive-dialog/08.todo-bot-luis-qnamaker
[howto-cross-train]: ../v4sdk/bot-builder-howto-cross-train.md
[update-the-recognizer-type-to-luis]: /composer/how-to-add-luis#update-the-recognizer-type-to-luis
