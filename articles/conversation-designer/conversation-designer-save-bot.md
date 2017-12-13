---
title: Save a Conversation Designer bot | Microsoft Docs
description: Learn how to save and train the language understanding model and prime speech recognition in a Conversation Designer bot.
author: vkannan
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 12/13/2017
ROBOTS: NoIndex, NoFollow
---

# Saving your Conversation Designer bot
> [!IMPORTANT]
> Conversation Designer is not available to all customers yet. More details on
> availability of Conversation Designer will come later this year.

While working in Conversation Designer, all your work is cached in browser memory. To commit your changes, click the **Save** button located in the upper left corner of the left navigation menu. To avoid loosing work, it is recommended that you save your work often. Besides clicking the **Save** button, you may also save your work by using the **CTRL+S** keyboard short-cut.

## Trains LUIS and primes speech recognition

Clicking the **Save** button will save changes to the bot and performs a few additional tasks. Unlike the keyboard short-cut, clicking the **Save** button also instruct Conversation Designer to perform the following tasks:

- Trains any new LUIS intents and entities for the bot and publishes the LUIS model locally in your Bot Service (if needed). These intents may be added in Conversation Designer or in the bot's corresponding [LUIS](https://luis.ai) app.
- Updates the conversation runtime to use the new LUIS model.
- Priming speech recognition by preparing and sending your example utterances to Microsoft Cognitive Services, which vastly improves speech recognition accuracy for this bot.

## Next step
> [!div class="nextstepaction"]
> [Test bot](conversation-designer-debug-bot.md)
