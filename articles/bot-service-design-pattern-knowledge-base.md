---
title: Design knowledge bots | Microsoft Docs
description: Learn about different ways to design a knowledge bot that finds and returns information in response to the user's input or query.
author: matvelloso
ms.author: mateusv
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: cognitive-services
ms.date: 12/13/2017
 
---

# Design knowledge bots

A knowledge bot can be designed to provide information about virtually any topic. 
For example, one knowledge bot might answer questions about events such as, "What bot events are there at this conference?", "When is the next Reggae show?", or "Who is Tame Impala?" 
Another might answer IT-related questions such as "How do I update my operating system?" or "Where do I go to reset my password?" 
Yet another might answer questions about contacts such as "Who is John Doe?" or "What is Jane Doe's email address?" 

Regardless of the use case for which a knowledge bot is designed, its basic objective is always the same: find and return the information that the user has requested 
by leveraging a body of data, such as relational data in a SQL database, 
JSON data in a non-relational store, or PDFs in a document store. 

## Search

Search functionality can be a valuable tool within a bot. 

First, "fuzzy search" enables a bot to return information that's likely to be relevant to the user's question, without requiring that the user provide precise input. 
For example, if the user asks a music knowledge bot for information about "impala" (instead of "Tame Impala"), the bot can respond with information that's most likely to be relevant to that input.

![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/fuzzySearch2.png)

Search scores indicate the level of confidence for the results of a specific search, 
enabling a bot to order its results accordingly, or even tailor its communication based upon confidence level. 
For example, if confidence level is high, the bot may respond with "Here is the event that best matches your search:".

![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/searchScore2.png)

If confidence level is low, the bot may respond with "Hmm... were you looking for any of these events?"

![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/searchScore1.png)

### Using Search to Guide a Conversation

If your motivation for building a bot is to enable basic search engine functionality, 
then you may not need a bot at all. What does a conversational interface offer that users can't get from a typical search engine in a web browser? 

Knowledge bots are generally most effective when they are designed to guide the conversation. 
A conversation is composed of a back-and-forth exchange between user and bot, which presents the bot 
with opportunities to ask clarifying questions, present options, and validate outcomes 
in a way that a basic search is incapable of doing. 
For example, the following bot guides a user through a conversation that facets and filters a dataset until it 
locates the information that the user is seeking.

![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/guidedConvo1.png)

![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/guidedConvo2.png)

![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/guidedConvo3.png)

![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/guidedConvo4.png)

By processing the user's input in each step and presenting the relevant options, the bot guides the user to the information that they're seeking. Once the bot delivers that information, it can even provide guidance about more efficient ways to find similar information in the future. 

![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/Training.png)

### Azure Search

By using <a href="https://azure.microsoft.com/en-us/services/search/" target="_blank">Azure Search</a>, 
you can create an efficient search index that a bot can easily search, facet, and filter. 
Consider a search index that is created using the Azure portal.

![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/search3.PNG)

You want to be able to access all properties of the data store, so you set each property as "retrievable." 
You want to be able to find musicians by name, so you set the **Name** property as "searchable." 
Finally, you want to be able to facet filter over musicians' eras, so you mark the **Eras** property as both "facetable" and "filterable." 

Faceting determines the values that exist in the data store for a given property, along with the magnitude of each value. 
For example, this screenshot shows that there are 5 distinct eras in the data store:

![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/facet.png)

Filtering, in turn, selects only the specified instances of a certain property. 
For example, you could filter the result set above to contain only items where **Era** is equal to "Romantic." 

> [!NOTE]
> See <a href="https://github.com/ryanvolum/AzureSearchBot" target="_blank">a sample bot</a> 
> for a complete example of a knowledge bot that is created using Azure Document DB, Azure Search, and the 
> Microsoft Bot Framework.
> 
> For the sake of simplicity, the example above shows a search index that is created using the Azure portal. 
> Indices can also be created programatically.

## QnA Maker

Some knowledge bots may simply aim to answer frequently asked questions (FAQs). 
<a href="https://www.microsoft.com/cognitive-services/en-us/qnamaker" target="_blank">QnA Maker</a> 
is a powerful tool that's designed specifically for this use case. 
QnA Maker has the built-in ability to scrape questions and answers from an existing FAQ site, plus it also allows you to manually configure your own custom list of questions and answers. 
QnA Maker has natural language processing abilities, enabling it to even provide answers to questions that are worded slightly differently than expected. 
However, it does not have semantic language understanding abilities. 
It cannot determine that a puppy is a type of dog, for example. 

Using the QnA Maker web interface, you can configure a knowledge base with three question and answer pairs: 

![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/KnowledgeBaseConfig.png)

Then, you can test it by asking a series of questions: 

![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/exampleQnAConvo.png)

The bot correctly answers the questions that directly map to the ones that were configured in the knowledge base. However, it incorrectly responds to the question "can I bring my tea?", because this question is most similar in structure to the question "can I bring my vodka?." The reason QnA Maker gives an incorrect answer is that it does not inherently understand the meaning of words. It does not know that "tea" is a type of nonalcoholic drink. Therefore, it answers "Alcohol is not allowed."

> [!TIP]
> Create your QnA pairs and then test and re-train your bot by using 
> the "Inspect" button under the conversation to select an alternative answer for each 
> incorrect answer that is given. 

## LUIS

Some knowledge bots require natural language processing (NLP) capabilities so that they can 
analyze a user's messages to determine the user's intent. 
[Language Understanding (LUIS)](https://www.luis.ai) provides a fast and effective means of adding NLP capabilities to bots. 
LUIS enables you to use existing, pre-built models from Bing and Cortana whenever they meet your needs, as well as allowing you to create specialized models of your own. 

When working with huge datasets, it's not necessarily feasible to train an NLP model with every variation of an entity. 
In a music playing bot, for example, a user might message "Play Reggae", "Play Bob Marley", or "Play One Love". 
Although a bot could map each of these messages to the intent "playMusic", 
without being trained with every artist, genre and song name, 
an NLP model would not be able to identify whether the entity is a genre, artist or song. 
By using an NLP model to identify the generic entity of type "music", the bot could search 
its data store for that entity, and proceed from there. 

## Combining Search, QnA Maker, and/or LUIS

Search, QnA Maker and LUIS are each powerful tools in their own right, but they can 
also be combined to build knowledge bots that possess more than one of those capabilities.

### LUIS and Search

In the music festival bot example [covered earlier](#search), 
the bot guides the conversation by showing buttons that represent the lineup. 
However, this bot could also incorporate natural language understanding by using LUIS 
to determine intent and entities within questions such as "what kind of music does Romit Girdhar play?". The bot could then search against an Azure Search index using musician name. 
 
It wouldn't be feasible to train the model with every possible musician name since there are so many potential values, but you could provide enough representative examples for LUIS to properly identify the entity at hand.  For example, consider that you train your model by providing examples of musicians: 

![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/answerGenre.png)
![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/answerGenreOneWord.png)

When you test this model with new utterances like, "what kind of music do the beatles play?", 
LUIS successfully determines the intent "answerGenre" and the identifies entity "the beatles." 
However, if you submit a longer question such as "what kind of music does the devil makes three play?",
LUIS identifies "the devil" as the entity.

![Dialog Structure](~/media/bot-service-design-pattern-knowledge-base/devilMakesThreeScore.png)

By training the model with example entities that are representative of the underlying dataset, you 
can increase the accuracy of your bot's language understanding. 

> [!TIP]
> In general, it is better for the model to err by identifying excess words in its entity recognition, e.g., identify "John Smith please" from the utterance "Call John Smith please", 
> rather than identify too few words, e.g., identify "John" from the utterance "Call John Smith please". 
> The search index will ignore irrelevant words such as "please" in the phrase "John Smith please". 

### LUIS and QnA Maker

Some knowledge bots might use QnA Maker to answer basic questions in combination with LUIS to determine intents, extract entities and invoke more elaborate dialogs. For example, consider a simple IT Help Desk bot. This bot may use QnA Maker to answer basic questions about Windows or Outlook, but it might also need to facilitate scenarios like password reset, which require intent recognition and back-and-forth communication between user and bot. There are a few ways that a bot may implement a hybrid of LUIS and QnA Maker:

1. Call both QnA Maker and LUIS at the same time, and respond to the user by using information from the first one that returns a score of a specific threshold. 
2. Call LUIS first, and if no intent meets a specific threshold score, i.e., "None" intent is triggered, then call QnA Maker. Alternatively, create a LUIS intent for QnA Maker, feeding your LUIS model with example QnA questions that map to "QnAIntent." 
3. Call QnA Maker first, and if no answer meets a specific threshold score, then call LUIS. 

The Bot Framework SDK provide built-in support for LUIS and QnA Maker. This enables you to trigger dialogs or automatically answer questions using LUIS and/or QnA Maker without having to implement custom calls to either tool. See the [Dispatch Tool Tutorial](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-tutorial-dispatch?view=azure-bot-service-4.0) for more information.

> [!TIP]
> When implementing a combination of LUIS, QnA Maker, and/or Azure Search, 
> test inputs with each of the tools to determine the threshold score for each of your models. 
> LUIS, QnA Maker, and Azure Search each generate scores by using a different scoring criteria, so 
> the scores generated across these tools are not directly comparable. 
> Additionally, LUIS and QnA Maker normalize scores. A certain score may be considered 'good' 
> in one LUIS model but not so in another model. 

## Sample code

- For a sample that shows how to create a basic knowledge bot using the Bot Framework SDK for .NET, see the <a href="https://aka.ms/qna-with-appinsights" target="_blank">Knowledge Bot sample</a> in GitHub. 
<!-- TODO: Do not have a current bot sample to work with this
- For a sample that shows how to create more complex knowledge bots using the Bot Framework SDK for .NET, see the <a href="https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/demo-Search" target="_blank">Search-powered Bots sample</a> in GitHub.
-->

[qnamakerTemplate]: https://docs.botframework.com/en-us/azure-bot-service/templates/qnamaker/#navtitle
