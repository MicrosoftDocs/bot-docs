---
title: Save a bot | Microsoft Docs
description: Learn how to save and train the language understanding model and prime speech recognition in an Conversation Designer bot.
author: vkannan
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/04/2017
ROBOTS: NoIndex, NoFollow
---

# Saving your Conversation Designer bot

While working the in the conversation designer, all your work is cached in browser memory. To commit your changes, click the **Save** button located in the upper right corner of the designer window. To avoid loosing work, it is recommended that you save your work often. Besides clicking the **Save** button, you may also save your work by using the **CTRL+S** keyboard short-cut.

## Trains LUIS and primes speech recognition

Clicking the **Save** button will save changes to the bot. Unlike the keyboard short-cut, clicking the **Save** button also instruct Conversation Designer to perform the following tasks:

- Trains any LUIS intents and entities for the bot and publishes the LUIS model locally in your Azure bot service (if needed).
- Updates the conversation runtime to use the new LUIS model.
- Prepares and sends your example utterances to Microsoft Cognitive Services, which vastly improves speech recognition accuracy for this bot.

## Next step
> [!div class="nextstepaction"]
> [Test bot](conversation-designer-debug-bot.md)
