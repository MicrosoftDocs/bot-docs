---
title: Configure speech priming - Bot Service
description: Learn how to configure speech priming for your bot service using the Azure Portal.
keywords: speech priming, speech recognition, LUIS
author: v-royhar
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/13/2017
---

# Configure speech priming

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

Speech priming improves the recognition of spoken words and phrases that are commonly used in a bot.

For speech-enabled bots that use the [Web Chat](bot-service-channel-connect-webchat.md) and [Cortana](~/bot-service-channel-connect-cortana.md) channels, speech priming uses examples specified in Language Understanding ([LUIS](https://www.luis.ai/)) apps to improve speech recognition accuracy for important words.

Your bot may already be integrated with a LUIS app, or you can choose to create a LUIS app to associate with your bot for speech priming. The LUIS app contains examples of what you expect users to say to your bot. Important words that you want the bot to recognize should be labeled as entities. For example, in a chess bot you want to make sure that when the user says "Move knight", it isn’t interpreted as "Move night". The LUIS app should include examples in which "knight" is labeled as an entity.

> [!IMPORTANT]
> - To use speech priming with the Web Chat channel, you must use the Bing Speech service. See [Enable speech in the Web Chat channel](bot-service-channel-connect-webchat-speech.md) for an explanation of how to use the Bing Speech service.
> - Speech priming only applies to bots configured for the Cortana channel or the Web Chat channel. Priming is not supported from non U.S. regional LUIS apps including: eu.luis.ai and au.luis.ai

## Change the list of LUIS apps your bot uses

To change the list of LUIS apps used by Bing Speech with your bot, do the following:

1. Click **Speech priming** on the bot service blade. The list of LUIS apps available to you will appear.
1. Select the LUIS apps you want Bing Speech to use.
    1. To select a LUIS app in the list, hover over the LUIS model until a checkbox appears, and then check the checkbox.
    1. To select a LUIS app that is not on the list, scroll to the bottom and enter the LUIS Application ID GUID into the text box.
1. Click **Save** to save the list of LUIS apps associated with Bing Speech for your bot.

![The speech priming panel](~/media/bot-service-manage-speech-priming/speech-priming.png)

## Additional Resources

- [Enable speech in the Web Chat channel](~/bot-service-channel-connect-webchat-speech.md)
- [Speech Support in Bot Framework – Web Chat to Directline, to Cortana](https://blog.botframework.com/2017/06/26/Speech-To-Text/)
- [Language Understanding Intelligent Service](https://www.luis.ai)
