---
title: Tasks overview | Microsoft Docs
description: Learn about Tasks in Conversation Designer.
author: vkannan
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/04/2017

---

# Tasks in Conversation Designer

A Task represents a single conversation topic that your bot supports. For example, a cafe bot would have individual tasks to "find locations," "book a table," "order food," and "manage reservations."

## Trigger actions
Every task is modeled the same way: IF ***trigger*** then DO ***action***. 

Supported ***trigger*** types are: 

- **User says**: This type uses [Language Understanding](conversation-designer-luis.md) that enables you to use LUIS - a machine learning based natural language understanding solution.
- **Script trigger**: This type uses [Code recognizer](conversation-designer-code-recognizer.md) that allows you to write custom script that runs as part of your bot. Script triggers can be edited directly within the Conversation Designer and provide a powerful model for extending your bot and integrating with business logic.

Supported ***actions*** in response to the trigger for the task firing are:

- [Simple Response](conversation-designer-actions.md#simple-response): Defines a simple response sent to the user in the form of some text that is either displayed or spoken. A simple response can also include an [adaptive card](conversation-designer-adaptive-cards.md) that enables bots to communicate with their rich content.
- [Script action](conversation-designer-actions.md#script-action): Write custom script to call back-end service to complete task.
- [Dialog](conversation-designer-dialogs.md): Build and execute a conversation model using a dialog flow.

## Add a task

To add a new task, from the left tree panel, select **Add** and then select **Task**.
<!-- TODO: Insert screenshot --> 

## Tips for modeling tasks

1. Every bot should be designed to perform several different tasks for your customer. You should think through the list of those functions and create a task for each of them.
2. To set up triggers, think about how you can detect the customer's intent to perform a task. How will the bot "know" what the customer wants to do?
3. If you are using language understanding as the trigger type, ensure you include sufficient examples for the different ways the user can express the intent to perform a task. "Book a table" should trigger the same action as "make a reservation" or "reserve a table."
4. Consider adding examples to your language understanding trigger that are grammatically correct.
5. If you plan on publishing your bot as a Cortana skill, consider adding sample phrases that work well with Cortana triggers. "Ask *your bot name* to *do something*". 
6. For code recognizers, write regex patterns to determine user intent. Code recognizers can also return entities that you can use in your task.
7. If simple response is the task *action*, it can optionally include an adaptive card for rendering on supported channels.

## Next step
> [!div class="nextstepaction"]
> [Task actions](conversation-designer-actions.md)
