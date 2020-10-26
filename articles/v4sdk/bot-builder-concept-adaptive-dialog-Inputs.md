---
title: Asking for user input in adaptive dialogs
description: Collecting input from users using adaptive dialogs
keywords: bot, user, actions, input, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 04/27/2020
monikerRange: 'azure-bot-service-4.0'
---

# Asking for user input in adaptive dialogs

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

The Bot Framework SDK defines a variety of input dialogs for collecting and validating user input. Input dialogs are a type of adaptive dialog action.

> [!IMPORTANT]
> Actions (_trigger actions_) are dialogs, and as such have all of the power and flexibility you need to create a fully functional and robust conversation flow. While the actions included in the Bot Framework SDK are extensive, you can also create your own custom actions to perform virtually any specialized task or process you need.

## Prerequisites

* [Introduction to adaptive dialogs][introduction]
* [Events and triggers in adaptive dialogs][triggers]
* [Actions in adaptive dialogs][actions]
* [Memory scopes and managing state in adaptive dialogs][managing-state]
* Familiarity with [Language Generation templates][lg-templates]
* Familiarity with [Adaptive expressions][adaptive-expressions]

> [!TIP]
> This syntax defined in the [Language Generation templates][lg-templates], which includes [Adaptive expressions][adaptive-expressions], is used in the `ActivityTemplate` object that is required for several parameters that are used in most of the input actions provided in the Bot Framework SDK.

## Inputs

Similar to [prompts][prompts], you can use _inputs_ in adaptive dialogs to ask for and collect input from a user, validate it, and accept it into memory. An input:

* Binds the prompt result to a property in a [state management][managing-state] scope.
* Prompts the user only if the result property doesn't already have a value.
* Saves the input to the specified property if the input from user matches the type of entity expected.
* Accepts validation constraints such as min, max, and so on.
* Can use as input locally relevant intents within a dialog as well as use interruption as a technique to bubble up user response to an appropriate parent dialog that can handle it.

<!-- TODO P0.5: For more information, see [about interruptions in adaptive dialogs](./ all-about-interruptions.md). -->

The adaptive dialogs library defines the following input types:

* [The input base class][inputdialog]. The base class that all of the input classes derive from.
* [Text][textinput]. To ask for any ***text based*** user input.
* [Number][numberinput]. To ask for any ***numeric based*** user input.
* [Confirmation][confirminput]. To request a ***confirmation*** from the user.
* [Multiple choice][multiple-choice]. To request a selection from a ***set of options***.
* [File or attachment][attachmentinput]. To request/enable a user to **upload a file**.
* [Date or time][datetimeinput]. To request a ***date and or time*** from a user.
* [Oauth login][oauthinput]. To enable your users to **sign into a secure site**.

## Additional information

* For more detailed information on inputs, including code examples, see the article [Adaptive dialogs prebuilt inputs][prebuilt-inputs].
* To learn more about expressions see the article [Adaptive expressions][adaptive-expressions].

[introduction]:bot-builder-adaptive-dialog-introduction.md
[triggers]:bot-builder-concept-adaptive-dialog-triggers.md
[actions]:bot-builder-concept-adaptive-dialog-actions.md
[prompts]:https://aka.ms/bot-builder-concept-dialog#prompts
[authentication]:https://aka.ms/azure-bot-authentication
[add-authentication]:https://aka.ms/azure-bot-add-authentication
[managing-state]:bot-builder-concept-adaptive-dialog-memory-states.md
[recognizers]:bot-builder-concept-adaptive-dialog-recognizers.md
[lg-templates]:bot-builder-concept-adaptive-dialog-generators.md
[adaptive-expressions]:bot-builder-concept-adaptive-expressions.md
[prebuilt-inputs]: ../adaptive-dialog/adaptive-dialog-prebuilt-inputs.md
[inputdialog]: ../adaptive-dialog/adaptive-dialog-prebuilt-inputs.md#inputdialog
[textinput]: ../adaptive-dialog/adaptive-dialog-prebuilt-inputs.md#textinput
[numberinput]: ../adaptive-dialog/adaptive-dialog-prebuilt-inputs.md#numberinput
[confirminput]: ../adaptive-dialog/adaptive-dialog-prebuilt-inputs.md#confirminput
[multiple-choice]: ../adaptive-dialog/adaptive-dialog-prebuilt-inputs.md#choiceinput
[attachmentinput]: ../adaptive-dialog/adaptive-dialog-prebuilt-inputs.md#attachmentinput
[datetimeinput]: ../adaptive-dialog/adaptive-dialog-prebuilt-inputs.md#datetimeinput
[oauthinput]: ../adaptive-dialog/adaptive-dialog-prebuilt-inputs.md#oauthinput
