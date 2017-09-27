---
title: Tasks overview | Microsoft Docs
description: Learn about Tasks in Conversation Designer bot.
author: vkannan
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/04/2017
ROBOTS: NoIndex, NoFollow
---

# Tasks in Conversation Designer bots
> [!IMPORTANT]
> Conversation Designer is not available to all customers yet. More details on
> availability of Conversation Designer will come later this year.

Bots in Conversation Designer are composed of a group of related tasks. A **task** is an action your bot performs in response to a specific user request or activity. For example, a cafe bot might include these tasks: “Find locations,” “Book a table,” “Order food,” and “Manage reservations.” Each task corresponds to a different user goal. 

## Triggers and actions
A task will perform an action when a trigger condition is met. All tasks follow this model: **IF trigger**, **DO action**.

A **trigger** can be one of two types:
1. **User joins or leaves a conversation** (activity-initiated): A task triggered by “user joins or leaves a conversation” will perform an action when a user starts or ends a conversation with the bot. This trigger is useful for sending an introductory message to the user. 
2. **User says or types something to the bot** (message-initiated): A task triggered by “user says or types something to the bot” performs an action in response to the user’s message. The user’s message is interpreted by a **recognizer**.

When a trigger condition is met, a task will perform one of the following actions:
- **give a reply**: A reply can be any combination of displayed text, spoken text, or rich card. With this action, the bot sends a reply to the user and completes the task. A reply is a single-turn conversation, meaning the task does not expect follow-up messages from the user.
- **start a dialogue**: A dialogue is a multi-turn conversation between the bot and the user. dialogues are particularly useful when the bot needs to engage in a back-and-forth conversation with a user in order to gather information needed to complete a task.
- **run a script function**: Your bot can execute custom script you write to call a back-end service to complete a task.

## Tips for modeling tasks

1. Every bot should be designed to perform several different tasks for your customer. You should think through the list of those functions and create a task for each of them.
2. To set up triggers, think about how you can detect the customer's intent to perform a task. How will the bot *know* what the customer wants to do?
3. If you are using language understanding as the recognizer, ensure you include sufficient examples for the different ways the user can express the intent to perform a task. "Book a table" should trigger the same action as "make a reservation" or "reserve a table."
4. Consider adding examples to your language understanding intent that are grammatically correct.
5. If you plan on publishing your bot as a Cortana skill, consider adding sample phrases that work well with Cortana triggers. "Ask your bot name to do something". 
6. For code recognizers, write regex patterns to determine user intent. Code recognizers can also return entities that you can use in your task.
7. If **give a reply** is the task *action*, it can optionally include an adaptive card for rendering on supported channels.

## Next step
> [!div class="nextstepaction"]
> [LUIS recognizer](conversation-designer-luis.md)
