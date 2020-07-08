---
title: Managing state in Adaptive Dialogs
description: Managing state in adaptive dialogs
keywords: bot, managing state, memory scopes, user scope, conversation scope, dialog scope, settings scope, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 05/08/2020
---
<!--
I'd keep prerequisites, managing state, and memory short-hand notation sections in the concept article, and move the rest to a ref topic. Then, possibly add a little more about how memory scopes and paths are used in the abstract to the concept article, common patterns, etc. I'd also convert the list of scopes into a table with short descriptions.
-->
# Managing state in adaptive dialogs

The terms _Stateful_ and _stateless_ are adjectives that describe whether an application is designed to remember one or more preceding events in a given sequence of interactions with a user (or any other activity). Stateful means the application _does_ keep track of the state of its interactions, usually by saving values in memory in the form a property. Stateless means the application does _not_ keep track of the state of its interactions, which means that there is no memory of any previous interactions and all incoming request must contain all relevant information that is required to perform the requested action. You can think of _state_ as the bot's current set of values or contents, such as the conversation ID or the active user's name.

The Bot Framework SDK follows the same paradigms as modern web applications and does not actively manage state, however the Bot Framework SDK does provide some abstractions that make state management much easier to incorporate into your bot. This topic is covered in detail in the Bot Framework SDK [Managing state][managing-state] article, it is recommended that you read and understand the information covered in that article before reading this article.

## Prerequisites

* A general understanding of [How bots work][bot-builder-basics].
* A general understanding of adaptive dialogs. For more information, see [Introduction to adaptive dialogs][introduction] and [Dialog libraries][concept-dialog].
* See the Bot Framework SDK [Managing state][managing-state] article for an overview of state management.

## Managing state

A bot is inherently stateless. For some bots, the bot can either operate without additional information, or the information required is guaranteed to be contained within the incoming activity. For other bots, state (such as where in the conversation we are or the response received from the user) is necessary for the bot to have a useful conversation.

The Bot Framework SDK defines memory scopes to help developers store and retrieve values in the bot's memory, for use when processing loops and branches, when creating dynamic messages, and other behaviors in the bot.

This makes it possible for bots built using the Bot Framework SDK to do things like:

* Pass information between dialogs.
* Store user profiles and preferences.
* Remember things between sessions such as the last search query or the last selection made by the user.

Adaptive dialogs provide a way to access and manage memory and all adaptive dialogs use this model, meaning that all components that read from or write to memory have a consistent way to handle information within the appropriate scopes.

> [!NOTE]
> All memory properties, in all memory scopes, are property bags, meaning you can add properties to them as needed.

Here are the different memory scopes available in adaptive dialogs, each with a link to additional information contained in the memory states reference article:

* [User scope][user-scope] is persistent data scoped to the ID of the user you are conversing with.
* [Conversation scope][conversation-scope] is persistent data scoped to the ID of the conversation you are having.
* [Dialog scope][dialog-scope] persists data for the life of the associated dialog, providing memory space for each dialog to have internal persistent bookkeeping.
* [Turn scope][turn-scope] provides a place to share data for the lifetime of the current turn.
* [Settings scope][settings-scope] represents any settings that are made available to the bot via the platform specific settings configuration system.
* [This scope][this-scope] persists data for the life of the associated action. This is helpful for input actions since their life type typically lasts beyond a single turn of the conversation.
* [Class scope][class-scope] holds the instance properties of the active dialog.

## Memory short-hand notations

There are few short-hand notations supported to access specific memory scopes.

| Symbol | Usage           | Expansion                             | Notes                                                                                                                   |
|--------|-----------------|---------------------------------------|------------------------------------------------------------------------------------------------------------------------ |
| $      | `$userName`     | `dialog.userName`                     | Short hand notation that represents the dialog scope.                                                                   |
| #      | `#intentName`   | `turn.recognized.intents.intentName`  | Short hand used to denote a named intent returned by the recognizer.                                                    |
| @      | `@entityName`   | `turn.recognized.entities.entityName` | `@entityName` returns the first and _only_ the first value found for the entity, immaterial of the value's cardinality. |
| @@     | `@@entityName`  | `turn.recognized.entities.entityName` | `@@entityName` will return the actual value of the entity, preserving the value's cardinality.                          |
| %      | `%propertyName` | `class.propertyName`                  | Used to refer to instance properties (e.g. `MaxTurnCount`, `DefaultValue` etc).                                         |

## Additional information

* For more detailed information on managing state, including examples, see the [Managing state in adaptive dialogs - reference guide][managing-state-ref] article.

<!-- Links to other articles-->
[managing-state-ref]:../adaptive-dialog/adaptive-dialog-prebuilt-actions.md
[bot-builder-basics]:bot-builder-basics.md
[introduction]:bot-builder-adaptive-dialog-introduction.md
[managing-state]:bot-builder-concept-state.md
[recognizers]:bot-builder-concept-adaptive-dialog-recognizers.md
[botframework-activity]:https://github.com/microsoft/botbuilder/blob/master/specs/botframework-activity/botframework-activity.md
[foreach-action]:../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#foreach
[setproperties-action]:../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#setproperties
[concept-dialog]:bot-builder-concept-dialog.md

<!-- Links to the Adaptive dialogs managing state reference article-->
[user-scope]: ../adaptive-dialog/adaptive-dialog-prebuilt-memory-states.md#user-scope
[conversation-scope]: ../adaptive-dialog/adaptive-dialog-prebuilt-memory-states.md#conversation-scope
[dialog-scope]: ../adaptive-dialog/adaptive-dialog-prebuilt-memory-states.md#dialog-scope
[dialog-scope]: ../adaptive-dialog/adaptive-dialog-prebuilt-memory-states.md#dialog-scope
[turn-scope]: ../adaptive-dialog/adaptive-dialog-prebuilt-memory-states.md#turn-scope
[settings-scope]: ../adaptive-dialog/adaptive-dialog-prebuilt-memory-states.md#settings-scope
[this-scope]: ../adaptive-dialog/adaptive-dialog-prebuilt-memory-states.md#this-scope
[class-scope]: ../adaptive-dialog/adaptive-dialog-prebuilt-memory-states.md#class-scope
