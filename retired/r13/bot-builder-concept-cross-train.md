---
title: Cross training your LUIS and QnA Maker models
description: Describing the concepts behind a bot that is cross trained to use both LUIS to LUIS and LUIS to QnA Maker recognizers
keywords: LUIS, QnA Maker, qna, bot, cross-train, cross train, adaptive dialogs, lu
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: how-to
ms.service: bot-service
ms.date: 11/1/2020
monikerRange: 'azure-bot-service-4.0'
---

# Cross training your LUIS and QnA Maker models

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

You can cross train your .lu and .qna files so that each adaptive dialog knows about the capabilities of the other adaptive dialogs in your bot. This enables the active adaptive dialog to defer to another recognizer, either in the same dialog or another, when a user enters any request or input that it is unable to handle on its own.

## Introduction to cross training

Adaptive dialogs offer a dialog-centric way to model conversations, with each adaptive dialog having its own language understanding (LU) model. While this gives bot developers tremendous flexibility, it can also present some challenges. Cross training can be helpful when an adaptive dialog might want to know when the user expresses something that could be handled by another dialog within the bot. Here are some examples where cross training could be useful.

- A bot that lets users cancel the current conversational flow or ask for help, without duplicating the cancel or help intents in every adaptive dialog.
- A bot that supports non-linear conversations, in which a user may think of something they had previously forgotten or simply change their mind mid-way through a task.
- A bot that allows a user to correct information they previously provided.

In the article [Handling interruptions in adaptive dialogs][interruptions], the concept of interruptions in adaptive dialogs is introduced. It explains how a parent adaptive dialog can be consulted when the active adaptive dialog's recognizer does not find a suitable match. In this case the active dialog does not know if a parent or sibling dialog can respond, so to find out, it must send utterances or question to its parent using the Bot Framework's _consultation_ mechanism.

Cross training can build on and improve on the capabilities provided by interruptions in a few ways:

1. **Cross-dialog training**. By cross training the LU models of all the adaptive dialogs in your bot, you give every dialog the ability to know if other dialogs are capable of responding to a user request. In this way the bot does not need to consult all the way up the dialog stack to find out if another dialog can best process a given user input. This is described in more detail in the [LUIS-to-LUIS Cross training](#luis-to-luis-cross-training) section.

1. **In-dialog training**. This is cross training different language understanding engines within the same dialog. LUIS and QnA Maker are different language understanding engines. Once the models for each are cross trained, the recognizer for both can be consulted to determine which is best suited to respond to a user request. This is described in more detail in the [Cross train LUIS and QnA Maker models](#cross-train-luis-and-qna-maker-models) section.

> [!TIP]
>
> If the language understanding models associated with the various adaptive dialogs in a bot are not cross trained, no utterances or questions from other dialogs will be considered unless the recognizer of the active dialog returns an unknown intent. When the models have been cross trained, utterances and questions from parent and sibling dialogs will always be considered because they are associated with a new intent created in the active dialog's language understanding model. A parent no longer must be consulted to find out if it can respond to the users input. When this new intent is returned, the Bot Framework knows to consult the parent or sibling to handle it.

## LUIS-to-LUIS cross training

By cross training the LUIS models in your bot, you improve your bot's ability to handle _Global interrupts_. This means that if there are no triggers in the active adaptive dialog that can handle the intent returned by the recognizer, the bot will bubble it up to the dialog's parent dialog, using adaptive dialogs _consultation mechanism_.  If the parent dialog does not have a trigger to handle the intent, it continues to bubble up until it reaches the root dialog. Once the intent is handled, the conversation flow continues where it left off. For a more detailed discussion on Global interrupts see [Handling interruptions globally][Global-interrupts] in the _handling interruptions in adaptive dialogs_ concepts article. For LUIS models cross trained with other LUIS models, you will use the [LUIS recognizer][luis-recognizer].

Common uses for global interrupts include creating basic dialog management features such as help or cancel in the root dialog that are then available to any of its child dialogs, as mentioned in the introduction. Global interrupts also enable users to seamlessly change the direction of the conversational flow. Consider the following example in this fictional travel bot.

### Travel bot

The following example travel bot demonstrates cross training a bot with multiple adaptive dialogs, each with its own LUIS model.

This bot consists of three adaptive dialogs, each with their own LUIS model:

![Travel bot diagram](./media/adaptive-dialogs/travel-bot.png)

1. RootDialog
    - Intents: `BookFlight`, `BookHotel`.
1. flightDialog
    - Intents: `flightDestination`, `departureTime`.
1. hotelDialog
    - Intents: `hotelLocation`, `hotelRating`.

#### Travel bot customer scenario

The following exchange demonstrates a potential customer usage scenario that cannot be handled by the bot, but could be after cross training the bot's LUIS models.

> **Bot**: Hello, how can I help you today?
>
> **User**: I want to book a flight
>
> **Bot**: Great, I can help you with that. What is your departure date?

The bot will be expecting an answer to the question _What is your departure date?_, something like "February 29", but how will it handle an interruption to the conversational flow when the user responds with something that the flight dialog cannot handle? Something like the following:

>
> **User**: I need to reserve a room first

In the above example, when the user requested to book a flight, the root dialog's recognizer returned the `BookFlight` intent. This runs `flightDialog`, an adaptive dialog that processes booking flights, but knows nothing about hotels. When the user then requests a hotel reservation, the flight dialog cannot understand since the utterance "I need to reserve a room first" does not match any intents in the flight dialogs LUIS model.

After cross training the .lu files, your bot will now be able to detect that the user is requesting something that another dialog can respond to, so it bubbles the request up to its parent, in this case the root dialog, which detects the `BookHotel` intent. This runs `hotelDialog`, an adaptive dialog that processes hotel reservations. Once the hotel dialog completes the hotel reservation request, control is passed back to the flight dialog to complete the flight booking.

#### Cross train the LUIS models of the travel bot

To enable this fictional travel bot to handle the interruption in the previous example, you need to update the flight dialog's LUIS model, contained in the **flightDialog.lu** file, to include a new intent named `_interruption`, then add the utterances for the `BookHotel` intent. The **flightDialog.lu** file is used to create your LUIS application associated with the flight dialog.

> [!TIP]
>
> Cross training all the LUIS models in a typical bot can be a very involved and tedious process. There is a command included with the Bot Framework command line interface (BF CLI) that automates this work for you. This is discussed in detail in the [The Bot Framework CLI cross-train command](#the-bot-framework-cli-cross-train-command) section below.

Before this update, the example flight booking .lu file looks like this:

```lu
# flightDestination
- book a flight

# departureTime
- I need to depart next thursday
```

After cross training with the hotel booking .lu file, it would look like this:

```lu
# flightDestination
- book a flight

# departureTime
- I need to depart next thursday

# _Interruption
- reserve a hotel room
```

The utterance _reserve a hotel room_ is associated with the `_interruption` intent. When the `_interruption` intent is detected, it bubbles up any utterance associated with it to its parent dialog, whose recognizer returns the `BookHotel` intent. When cross training LUIS to LUIS, you need to include all user utterances from all intents from the dialog you are cross training with.

![Travel bot diagram after cross training](./media/adaptive-dialogs/after-cross-train.png)

> [!IMPORTANT]
>
> LUIS predictions are influenced by the number of utterances in each intent. If you have an intent with 100 example utterances and an intent with 20 example utterances, the 100-utterance intent will have a higher rate of prediction and will more likely be selected. This can impact cross trained models because all utterances from the models of all parent and sibling dialogs become utterances of the new `_Interruption` intent. In some cases, this can result in a parent or sibling dialog responding to the user when acceptable matches would have been returned by the current dialogs recognizer prior to cross training. Minimize the effects of this, if needed, by limiting the number of example utterances in the new `_Interruption` intent.

## Cross train LUIS and QnA Maker models

A well designed bot can answer relevant product or service questions asked by a user, regardless what dialog is currently active. LUIS is ideal for handling conversational flows while QnA Maker is ideal for handling user and frequently asked questions. Having access to both in your adaptive dialogs can improve the bots ability to meet user needs.

To enable this capability, _cross train_ your .lu and .qna files to include the information required by the recognizer to determine which response, LUIS or QnA Maker, is the best suited for the user. For a LUIS model cross trained with a QnA Maker model, you will use the [Cross-trained recognizer set][cross-trained-recognizer-set-concept].

Before creating your LUIS applications and QnA Maker knowledge base, you need to _cross train_ your .lu and .qna files to include the information required by your bot's recognizer to determine whether the LUIS or the QnA Maker response is best suited for the user.

For each adaptive dialog that has an associated .lu and .qna file, the following updates are made when cross training these files:

1. In .lu files, a new intent named `DeferToRecognizer_qna_<dialog-file-name>` is added. Each question and question variation from the corresponding .qna file becomes an utterance associated with that new intent.<!-- Answers are not copied to the .lu file from the .qna file.-->

1. In .qna files, a new answer named `intent=DeferToRecognizer_luis_<dialog-file-name>` is added, along with each utterance from every intent in the corresponding .lu file. These utterances become questions associated with that answer. Additionally, all utterances from referenced .lu files also become questions associated with that answer.

When a user converses with the bot, the `CreateCrossTrainedRecognizer` recognizer sends that user input to both LUIS and the QnA Maker knowledge base to be processed.

### Recognizer responses

The following table shows the matrix of possible responses and the resulting action taken by the bot.

<!--![Response table](./media/adaptive-dialogs/luis-qna-maker-runtime-cross-train-results-matrix.png)-->

| LUIS recognizer returns | QnA Maker recognizer returns | Final results                     |
| ----------------------- | ---------------------------- | --------------------------------- |
| Valid LUIS intent       |  Defer To **LUIS** intent    |  Select response from **LUIS** recognizer. Handle using the `OnIntent` trigger.|
| Defer to **QnA Maker** intent | Valid QnA Maker intent |  Select response from **QnA Maker** recognizer. Handle using the `OnQnAMatch` trigger.|
| Defer to **QnA Maker** intent | Defer To **LUIS** intent | The `UnknownIntent` event is emitted by the recognizer. Handle using the `OnUnknownIntent` trigger.|
| Valid LUIS intent | Valid QnA Maker intent | The `ChooseIntent` event is emitted by the recognizer. Handle using the `OnChooseIntent` trigger.|

> [!IMPORTANT]
>
> Cross training all the LUIS and QnA Maker models in a typical bot can be a very involved and tedious process. There is a command included with the Bot Framework command line interface (BF CLI) that automates this work for you. This is discussed in detail in the [The Bot Framework CLI cross-train command](#the-bot-framework-cli-cross-train-command) section below.
>
> Running this command on a bot project that has both LUIS and QnA Maker models will automatically cross-train LUIS to LUIS across all adaptive dialogs across the entire project as well as LUIS to QnA Maker cross training within each adaptive dialogs that have both models, meaning both .lu and .qna files.

### Cross train multiple LUIS and QnA Maker models

Cross training a bot with both LUIS and QnA Maker models improves on global interruptions as described previously in [LUIS to LUIS cross training](#luis-to-luis-cross-training). This also applies to QnA Maker. For example:

- When the root dialog's LUIS model is cross trained with the root dialog's QnA Maker model, the command creates the `DeferToRecognizer_qna` intent in RootDialog.lu, with all questions listed as utterances.
- Next, when the root dialog's child is cross trained, it picks up those intents and in turn passes them to its child dialog and this continues until there are no more child dialogs.
- When a user asks any question associated with RootDialog.qna when the active dialog is a child or descendant, the active dialog will not be able to respond, but because it has been cross-trained it will be aware that another dialog is able to respond and will then bubble the question up to its parent. The question in turn bubbles all the way up to the root dialog, which answers the question before returning control back to the previous conversational flow.

The advantage of global interruptions in this scenario is the ability it provides to use a QnA Maker knowledge base associated with the root dialog to handle all questions the user may have, regardless of where they are in their conversation with the bot.

> [!NOTE]
>
> Global interruptions result in multiple transactions to both the LUIS and QnA Maker services. The deeper the dialog hierarchy, the more transactions will potentially occur for a given user request. This increase in transactions may be something to consider when designing your bot.

## The Bot Framework CLI cross-train command

Cross training your bot can quickly become a challenging and error prone task in even a minimally complex bot, especially when you are still making frequent updates to the LUIS or QnA Maker models. The Bot Framework SDK provides a tool to automate this process. For information on the Bot Framework CLI cross-train command, refer to the _Cross-trained recognizer set_ section of the [Recognizers in adaptive dialogs - reference guide](../adaptive-dialog/adaptive-dialog-prebuilt-recognizers.md#cross-trained-recognizer-set).

## Source code updates

After cross training your LUIS and QnA Maker models, you need to be sure and use the Cross-trained recognizer set as the adaptive dialogs recognizer in your source code, as explained in the [Recognizers in adaptive dialogs - reference guide][crosstrainedrecognizerset-ref-guide].

## Additional information

- How to [create a bot cross trained to use both LUIS and QnA Maker recognizers](bot-builder-howto-cross-train.md)
- The [Cross-trained recognizer set][crosstrainedrecognizerset-ref-guide] section of the Recognizers in adaptive dialogs - reference guide.

<!------------------------------------------------------------------------------------------------------------->
[intents]: bot-builder-concept-adaptive-dialog-recognizers.md#intents
[utterances]: bot-builder-concept-adaptive-dialog-recognizers.md#utterances
[interruptions]: bot-builder-concept-adaptive-dialog-interruptions.md
[Global-interrupts]: bot-builder-concept-adaptive-dialog-interruptions.md#handling-interruptions-globally
[luis]: /azure/cognitive-services/luis/what-is-luis
[luis-recognizer]: bot-builder-concept-adaptive-dialog-recognizers.md#luis-recognizer
[cross-trained-recognizer-set-concept]: /azure/cognitive-services/luis/what-is-luis
[luis-recognizer]: bot-builder-concept-adaptive-dialog-recognizers.md#cross-trained-recognizer-set
[luis-build]: bot-builder-howto-bf-cli-deploy-luis.md#create-and-train-a-luis-app-then-publish-it-using-the-build-command
[bf-luiscross-train]: https://github.com/microsoft/botframework-cli/tree/main/packages/cli#bf-luiscross-train
[lu-templates]: ../file-format/bot-builder-lu-file-format.md
[qnamaker]: /azure/cognitive-services/QnAMaker/Overview/overview
[qnamaker-recognizer]: bot-builder-concept-adaptive-dialog-recognizers.md#qna-maker-recognizer
[qna-file-format]: ../file-format/bot-builder-qna-file-format.md
[qnamaker-build]: bot-builder-howto-bf-cli-deploy-qna.md#create-a-qna-maker-knowledge-base-and-publish-it-to-production-using-the-build-command
[recognizer]: bot-builder-concept-adaptive-dialog-recognizers.md
[cross-trained-recognizer-set-concept]: bot-builder-concept-adaptive-dialog-recognizers.md#cross-trained-recognizer-set
[crosstrainedrecognizerset-ref-guide]: ../adaptive-dialog/adaptive-dialog-prebuilt-recognizers.md#cross-trained-recognizer-set
[bf-cli]: bf-cli-overview.md
[language-generation]: bot-builder-concept-adaptive-dialog-generators.md
