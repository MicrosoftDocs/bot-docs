---
title: Bot Builder basics | Microsoft Docs
description: Bot Builder SDK basic structure.
keywords: turn context, bot structure, receiving input
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 04/18/2018
monikerRange: 'azure-bot-service-4.0'
---

# Basic bot structure
[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

Azure Bot Service and the Bot Builder SDK provides libraries, samples, and tools to help you build and debug bots. However, before we get into too much detail on any of those, it's important to understand the basic structure of a bot and how they all work together. These concepts apply regardless of which programming language you choose. Links to more in depth content are provided throughout; this article will provide an initial mental framework for how a bot operates.

From the ground up, let's explore the basic structure of a bot.

## Creation of your bot

A bot can be created in a variety of ways, such as within the [Azure portal](https://portal.azure.com), in Visual Studio, or through [command line tools](../bot-builder-tools-az-cli.md). Once created, bots can be run on a local machine, on Azure, or on another cloud service. They all function very similarly, regardless of where it's run and how it's built.

## Interaction with your bot

A bot inherently doesn't have any visible UI in the way a website or app does, and instead is interacted with through a [conversation](bot-concepts.md#activities-and-conversations) with the user. Depending on where the user is connected from (which we call a [channel](bot-concepts.md), but we won't get into that here), certain information is sent back and forth between the user and your bot.

Each unit of information is called an **activity** within our bot, and an activity can come in various forms. Activities include both communication from the user, which is referred to as a **message**, or additional information wrapped up in a handful of other [activity types](../bot-service-activities-entities.md). This additional information can include when a new party joins or leaves the conversation, when a conversation ends, etc. Those types of communication from the user's connection are sent by the underlying system, without the user needing to do anything.

The bot receives the communication and wraps it up in an activity object, with the correct type, to give it to your bot code. All of the other activity types provide useful information, however the most interesting and most common activity is the **message** activity from the user.

## Receiving user input

When we receive a message activity from the user, we need to understand what they're sending us. The most straighforward way is when we receive a message, simply match it to a string. Based on which string it is, we can choose to do something, depending on what your bot is trying to achieve. This may include responding to the user, updating some variable or resource, saving it to [storage](bot-builder-storage-concept.md), and so on.

There are more complex ways to recognize user input, such as [LUIS](bot-builder-concept-luis.md) or [QnA Maker](bot-builder-howto-qna.md), but string matching is the simplest.

## Defining a turn

Receiving an activity, and subsequently processing it through your bot, is called a **turn**; this represents one complete cycle of your bot. A turn ends when all execution is done, the activity is fully processed and all the layers of the bot have completed.

When the activity is received, the bot creates a **turn context** to provide information about the activity and give context to the current turn we are processing. That turn context exists for the duration of the turn, and then is disposed of, marking the end of the turn.

The [turn context](bot-builder-concept-activity-processing.md#turn-context) contains a handful of information, and the same object is used through all the layers of your bot. This is helpful because this turn context object can be, and should be, used to store information needed later in that turn if needed.

> [!IMPORTANT]
> Each **turn** is independent of each other, executing on their own, and have the potential to overlap. Each turn will have it's own turn context, but it's worth considering the complexity that introduces in some situations.

## Where to go from here

This article avoided a lot of details, such as how [activites are processed](bot-builder-concept-activity-processing.md), different [conversation types](bot-builder-conversations.md), keeping track of the [state of your conversation](bot-builder-storage-concept.md) for more intelligent conversations, and so on. The rest of the conceptual topics build on this foundation, and cover the rest of the ideas needed to understand both bots and the Azure Bot Service. You can follow the next steps sections to build up your knowledge, or you can jump around to what intruiges you, try out the [quickstart](../bot-service-quickstart.md) to build your first bot, or dive into the [develop](bot-builder-howto-send-messages.md) docs. 

## Next steps

Next, the Bot Connector Service allows your bot to communicate with users on different platforms.

> [!div class="nextstepaction"]
> [Channels and the Bot Connector Service](bot-concepts.md)

