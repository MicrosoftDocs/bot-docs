---
title: Design knowledge bots
description: Learn about different ways to design a knowledge bot that finds and returns information in response to the user's input or query.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: bot-service
ms.date: 09/01/2022
---

# Design knowledge bots

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

You can design a knowledge bot that covers virtually any topic.
Regardless of the use case for which a knowledge bot is designed, its basic objective is always the same: find and return the information that the user has requested by searching a body of data.

For example, one knowledge bot might answer questions about events such as, "What bot events are there at this conference?", "When is the next Reggae show?", or "Who is Tame Impala?" Another might answer IT-related questions such as "How do I update my operating system?" or "Where do I go to reset my password?". Yet another might answer questions about contacts such as "Who is John Doe?" or "What is Jane Doe's email address?".

This article covers some of the AI capabilities you can add to a bot, such as to let a user search for information, ask questions, or interact with information.
For which Azure Cognitive Services features the Bot Framework SDK supports, see [Natural language understanding](v4sdk/bot-builder-concept-luis.md).

> [!TIP]
> Cognitive Services incorporates evolving technologies. This article describes both newer and older features.

## About confidence scores

Some features enable a bot to return information from a knowledge base or language model to match a user question or query.

For example, if the user asks a music knowledge bot for information about "impala" (instead of band's full name "Tame Impala"), the bot can respond with information that's most likely to be relevant to that input. Similarly, language understanding features can use a language model to extract the likely _intent_ from user input.
For example, if the user asks a travel agent bot to "book a room for three days", the bot might extract a "reserve a room" intent and follow up by collecting details.

Both search and intent recognition return a confidence score, which indicates the level of confidence the engine has that a particular result is correct. Use confidence scores to order results or to respond differently, based on overall confidence in your answer.

> [!NOTE]
> When you use a combination of different service or feature types together, test inputs with each of the tools to determine the threshold score for each of your models. The services and features use different scoring criteria, so the scores generated across these tools are not directly comparable. For example, the QnA Maker service used a confidence range of 0 to 100, while the question answering feature uses a range of 0.0 to 1.0.

- If confidence is high, your bot might respond with "Here's the event that best matches your search" or "I can help you reserve a room" and present the top answer or start asking follow-up questions.
- If confidence is low, your bot might respond with "Hmm... were you looking for any of these events?" or "I can help you with the following things:" and present a list of possible answers or options.

## To filter topics

You can design knowledge bots to help a user narrow and refine a search.
Within a conversation, the bot can ask clarifying questions, present options, and validate outcomes, in a way that basic search can't.

For example, an events bot can find out what type of event the user is interested in by asking a series of questions.
Consider the following exchange:

1. User, "events".
1. Bot, "What are you interested in? Music, Comedy, Film...".
1. User, "Music".
1. Bot, "What type of music are you interested in? Any, Rock/Pop, Hip-hop/Rap, ...".
1. User, "Rock/Pop".
1. Bot, "What day would you like to see Rock/Pop? Friday, Saturday, Sunday, Any".
1. User, "Saturday".
1. Bot, "Here are the Rock/Pop shows for Saturday:", with a list of the found shows.

By processing the user's input in each step and presenting relevant options, the bot guides the user to the information that they're seeking.
Once the bot delivers that information, it can also provide guidance about more efficient ways to find similar information in the future.

> By the way, you can also just type "Rock friday" or search for an event by name.

For information about related Azure services, see [Search](v4sdk/bot-builder-concept-luis.md#search) in the **Natural language understanding** concept article.

## To answer questions

You can design knowledge bots to answer frequently asked questions.
Services that support question and answer features often allow you or your bot to:

- Manage and train a knowledge base.
- Import information into a knowledge base, such as from a data file or web page.
- Guess which answer best maps to the user's question.
- Ask the user follow-up questions to help find the answer they're looking for.

For information about related Azure services, see [Questions and answers](v4sdk/bot-builder-concept-luis.md#questions-and-answers) in the **Natural language understanding** concept article.

## To interpret intent

Some knowledge bots require natural language processing (NLP) capabilities so that they can analyze a user's messages to determine the user's intent and other important information.

In a music playing bot, for example, a user might message "Play Reggae", "Play Bob Marley", or "Play One Love".
You can train a language model to map each of these messages to the intent "playMusic", without being trained with every artist, genre and song name.

Your language model might not understand whether the thing to play, the _entity_, is a genre, artist, or song.
However, your bot can search for that entity using this information, and proceed from there.

For information about related Azure services, see [Language understanding](v4sdk/bot-builder-concept-luis.md#language-understanding) in the **Natural language understanding** concept article.

## To integrate multiple features

Each NLP feature is a powerful tool in its own right.
However, your bot can combine these features and others to provide to your users a more fluid and natural experience.
Use the confidence scores to determine which feature best maps to the user's message, and ask follow-up questions if the best match is ambiguous.

For example, such a bot can let the user:

- Find a show they're interested in attending.
- Get information about the artist, venue, and event.
- Purchase a ticket or sign up for notices of future events.

For information about related Azure services, see [Use multiple features together](v4sdk/bot-builder-concept-luis.md#use-multiple-features-together) in the **Natural language understanding** concept article.

## Explore samples

The [Bot Framework Samples](https://github.com/microsoft/BotBuilder-Samples#readme) repo has a few sample bots that demonstrate language understanding features:

| Sample | Sample Name          | Description                                                                    |
| ------ | -------------------- | ------------------------------------------------------------------------------ |
| 11     | QnA Maker (simple)   | Answer questions as a series of _single-turn_ conversations using QnA Maker.   |
| 13     | Core bot             | Interpret the user's intent using LUIS.                                        |
| 14     | NLP with dispatch    | Dispatch user messages to LUIS or QnA Maker using Orchestrator.                |
| 49     | QnA Maker (advanced) | Answer questions using multi-turn and active learning features in QnA Maker.   |

The [Azure SDK for .NET](https://github.com/Azure/azure-sdk-for-net#readme) and [Azure SDK for Python](https://github.com/Azure/azure-sdk-for-python#readme) repositories also have a few samples:

| Feature | Samples README |
|:-|:-|
| Question answering | [C#](https://github.com/Azure/azure-sdk-for-python/tree/main/sdk/cognitivelanguage/azure-ai-language-questionanswering/samples#readme), [Python](https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/cognitivelanguage/Azure.AI.Language.QuestionAnswering/samples#readme) |
| Conversational language understanding, orchestration workflow | [C#](https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/cognitivelanguage/Azure.AI.Language.QuestionAnswering/samples#readme), [Python](https://github.com/Azure/azure-sdk-for-python/tree/main/sdk/cognitivelanguage/azure-ai-language-conversations/samples#readme) |
