---
title: Language understanding
description: Learn how to add artificial intelligence to your bots with Azure AI services to make them more useful and engaging.
keywords: Azure AI services, CLU, LUIS, QnA Maker, custom question answering
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: azure-ai-bot-service
ms.date: 08/08/2024
monikerRange: 'azure-bot-service-4.0'
ms.custom:
  - evergreen
---

# Natural language understanding

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Bots can use various conversational styles, from structured and guided to free-form and open-ended.
Based on what a user says, your bot needs to decide what to do next in its conversation flow.
Azure AI services includes features to help with this task.
These features can help a bot search for information, ask questions, or interpret the user's intent.

The interaction between users and bots is often free-form, and bots need to understand language naturally and contextually.
In an open-ended conversation, there can be a wide range of user replies, and bots can provide more or less structure or guidance.
This table illustrates the difference between guided and open-ended questions.

| Guided                                                                                               | Open-ended                                             |
|:-----------------------------------------------------------------------------------------------------|:-------------------------------------------------------|
| I'm the travel bot. Select one of the following options: find flights, find hotels, find rental car. | I can help you book travel. What would you like to do? |
| Do you need anything else? Click yes or no.                                                          | Do you need anything else?                             |

Azure AI services provides features with which to build intelligent apps, websites, and bots.
Adding these features to your bot can allow your bot to respond to open-ended user input more appropriately.

This article describes support in the Bot Framework SDK for some of the features available in Azure AI services.

- For tips on how to design these features into your bot, see [Design knowledge bots](../bot-service-design-pattern-knowledge-base.md).
- For detailed information about Azure AI services, see the [Azure AI services documentation](/azure/ai-services/).

## General guidance

Azure AI services incorporates evolving technologies.
Azure AI Language integrates various features that were previously implemented as separate services.
This article describes both the newer and older features and services, and where to find more information about each.

| Scenario | Guidance |
|:-|:-|
| New bot development | Consider using Microsoft Copilot Studio, which is designed to support teams where members have a mix of skills and disciplines. For more information, see [Copilot Studio](/microsoft-copilot-studio/fundamentals-what-is-copilot-studio) and [Enable advanced AI features](/microsoft-copilot-studio/advanced-ai-features). |
| New language projects for existing Bot Framework SDK bots | Consider using features of the Azure AI Language service, such as conversational language understanding (CLU) and answering questions. |
| Existing bots with existing language projects | Your language projects will continue to work, but consider migrating to Azure AI Language. For more information, see the [Migrate existing language projects](#migrate-existing-language-projects) section later in this article. |

## Language understanding

Natural language understanding features let you build custom natural language understanding models to predict the overall intention of user's message and extract important information from it.

| Service or feature | Description |
|:-|:-|
| Conversational Language Understanding (CLU) | A feature of the Azure AI Language service. |
| Language Understanding (LUIS) | An Azure AI service. (CLU is an updated version of LUIS.)<br/><br/>LUIS will be retired on 1 October 2025. |

### Conversational Language Understanding (CLU)

Conversational language understanding (CLU) enables users to build custom natural language understanding models to predict the overall intention of an incoming utterance and extract important information from it. CLU only provides the intelligence to understand the input text for the client application and doesn't perform any actions on its own.

To use CLU in your bot, create a language resource and a conversation project, train and deploy your language model, and then implement in your bot a _telemetry recognizer_ that forwards requests to the CLU API.

For more information, see:

- [What is conversational language understanding?](/azure/ai-services/language-service/conversational-language-understanding/overview)
- _Telemetry recognizer_ interface reference for [C#/.NET](/dotnet/api/microsoft.bot.builder.ai.luis.itelemetryrecognizer) or [JavaScript/node.js](/javascript/api/botbuilder-ai/luisrecognizertelemetryclient)
- [Azure Cognitive Language Services Conversations client library for .NET](/dotnet/api/overview/azure/ai.language.conversations-readme)

### Language Understanding (LUIS)

> [!NOTE]
> [Language Understanding (LUIS) will be retired on 1 October 2025](https://azure.microsoft.com/updates/language-understanding-retirement/).
> Beginning 1 April 2023, you won't be able to create new LUIS resources.

LUIS applies custom machine-learning intelligence to a user's conversational, natural language text to predict overall meaning, and pull out relevant, detailed information.

To use LUIS in your bot, create, train, and publish a LUIS app, then add a _LUIS recognizer_ to your bot.

For more information, see:

- [What is Language Understanding (LUIS)?](/azure/ai-services/luis/what-is-luis)
- [Add natural language understanding to your bot](bot-builder-howto-v4-luis.md)

## Questions and answers

Question-and-answer features let you build knowledge bases to answer user questions.
Knowledge bases represent semi-structured content, such as that found in FAQs, manuals, and documents.

| Service or feature | Description |
|:-|:-|
| Question answering | A feature of the Azure AI Language service. |
| QnA Maker | An Azure AI services service. (Question answering is an updated version of QnA Maker.)<br/><br/>Azure AI QnA Maker will be retired on 31 March 2025. |

### Question answering

Question answering provides cloud-based natural language processing (NLP) that allows you to create a natural conversational layer over your data. It's used to find the most appropriate answer for any input from your custom knowledge base of information.

To use question answering in your bot, create and deploy a question answering project, then implement in your bot a _QnA Maker client_ that forwards requests to the question answering API.

For more information, see:


- [Use question answering to answer questions](../bot-builder-howto-answer-questions.md)
- [What is question answering?](/azure/cognitive-services/language-service/question-answering/overview)
- _QnA Maker client_ interface reference for [C#/.NET](/dotnet/api/microsoft.bot.builder.ai.qna.iqnamakerclient) or [JavaScript/node.js](/javascript/api/botbuilder-ai/qnamakerclient)
- [Azure Cognitive Language Services Question Answering client library for .NET](/dotnet/api/overview/azure/ai.language.questionanswering-readme)

### QnA Maker

> [!NOTE]
> [Azure AI QnA Maker will be retired on 31 March 2025](https://azure.microsoft.com/updates/azure-qna-maker-will-be-retired-on-31-march-2025/).
> Beginning 1 October 2022, you won't be able to create new QnA Maker resources or knowledge bases.

QnA Maker has the built-in ability to scrape questions and answers from an existing FAQ site, plus it also allows you to manually configure your own custom list of questions and answers.
QnA Maker has natural language processing abilities, enabling it to even provide answers to questions that are worded slightly differently than expected.
However, it doesn't have semantic language understanding abilities, so it can't determine that a puppy is a type of dog, for example.

To use QnA Maker in your bot, create a QnA Maker service, publish your knowledge base, and add a _QnA Maker_ object to your bot.

For more information, see:

- [What is QnA Maker?](/azure/ai-services/qnamaker/overview/overview)
- [Use QnA Maker to answer questions](bot-builder-howto-qna.md)

## Search

Azure Cognitive Search helps your bot provide users with a rich search experience, including the ability to facet and filter information.

- You can use Azure Cognitive Search as a feature within Azure AI Language.
- You can use the Azure Cognitive Search service directly.

### Azure Cognitive Search

You can use [Azure Cognitive Search](/azure/search/) to create an efficient index with which to search, facet, and filter a data store.

- For how to configure Cognitive Search within Azure AI Language, see [Configure custom question answering enabled resources](/azure/ai-services/language-service/question-answering/how-to/configure-resources).
- For information about the Cognitive Search service, see [What is Azure Cognitive Search?](/azure/search/search-what-is-azure-search).

## Use multiple features together

To build a multi-purpose bot that understands multiple conversational topics, begin with support for each function separately, and then integrate them together.
Scenarios in which a bot might combine multiple features include:

- A bot that provides a set of features, where each feature has its own language model.
- A bot that searches multiple knowledge bases to find answers to a user's questions.
- A bot that integrates different types of features, such as language understanding, answering questions, and search.

This table describes different ways you can integrate multiple features.

| Service or feature | Description |
|:-|:-|
| Orchestration workflow | A feature of the Azure AI Language service that allows you to use multiple question answering, CLU, and LUIS projects together. |
| Bot Framework Orchestrator | An intent-only recognition engine, which you can use to determine which LUIS model or QnA Maker knowledge base can best handle a given message. |
| Custom | You can implement your own logic to decide how best to handle the user's request. |

### Use orchestration workflow

The orchestration workflow applies machine-learning intelligence to enable you to build orchestration models to connect conversational language understanding (CLU) components, question answering projects, and LUIS applications.

To use the orchestration workflow in your bot, create an orchestration workflow project, build your schema, train and deploy your model, then query your model API for intent predictions.

For more information, see:

- [What is orchestration workflow?](/azure/ai-services/language-service/orchestration-workflow/overview)
- [Azure Cognitive Language Services Conversations client library for .NET](/dotnet/api/overview/azure/ai.language.conversations-readme)

### Orchestrator

> [!NOTE]
> [Azure AI QnA Maker will be retired on 31 March 2025](https://azure.microsoft.com/updates/azure-qna-maker-will-be-retired-on-31-march-2025/).
> Beginning 1 October 2022, you won't be able to create new QnA Maker resources or knowledge bases.
>
> [Language Understanding (LUIS) will be retired on 1 October 2025](https://azure.microsoft.com/updates/language-understanding-retirement/).
> Beginning 1 April 2023, you won't be able to create new LUIS resources.

Bot Framework Orchestrator is an intent-only recognition engine. The Bot Framework CLI includes tools to generate a language model for Orchestrator from a collection of QnA Maker knowledge bases and LUIS language models. Your bot can then use Orchestrator to determine which service can best respond to the user's input.

The Bot Framework SDK provides built-in support for LUIS and QnA Maker. This enables you to trigger dialogs or automatically answer questions using LUIS and QnA Maker with minimal configuration.

For more information, see [Use multiple LUIS and QnA models with Orchestrator](bot-builder-tutorial-orchestrator.md).

### Custom logic

There are two main ways to implement your own logic:

1. For each message, call all relevant services that your bot supports. Use the results from the service that has the best confidence score. If the best score is ambiguous, ask the user to choose which response they want.
1. Call each service in a preferred order. Use the first result that has a sufficient confidence score.

> [!TIP]
> When implementing a combination of different service or feature types, test inputs with each of the tools to determine the threshold score for each of your models. The services and features use different scoring criteria, so the scores generated across these tools are not directly comparable.
>
> The LUIS and QnA Maker services normalize scores. So, one score can be _good_ in one LUIS model but not so good in another model.

## Migrate existing language projects

For information on migrating resources from older services to Azure AI Language, see:

- [Migrate from LUIS, QnA Maker, and Text Analytics](/azure/ai-services/language-service/concepts/migrate)
- [Backwards compatibility with LUIS applications](/azure/ai-services/language-service/conversational-language-understanding/how-to/migrate-from-luis)
- [Migrate from QnA Maker to Question Answering](/azure/ai-services/language-service/question-answering/how-to/migrate-qnamaker-to-question-answering)
- [Migrate from QnA Maker to custom question answering](/azure/ai-services/language-service/question-answering/how-to/migrate-qnamaker)

## Additional resources

To manage specific project or resources:

- To manage of Azure resources, go to the [Azure portal](https://ms.portal.azure.com/).
- To manage Azure AI Language projects, go to the [Language Studio portal](https://language.cognitive.azure.com/home).
  - [Conversational language understanding (CLU) projects](https://language.cognitive.azure.com/clu/projects)
  - [Question answering projects](https://language.cognitive.azure.com/questionAnswering/projects)
- To manage LUIS apps, go to the [Language Understanding (LUIS) portal](https://www.luis.ai/).
- To manage QnA Maker knowledge bases, go to the [QnA Maker portal](https://www.qnamaker.ai/).

For documentation for a specific feature or service:

- [What is Azure AI Language?](/azure/ai-services/language-service/overview)
  - [What is conversational language understanding?](/azure/ai-services/language-service/conversational-language-understanding/overview)
  - [What is question answering?](/azure/ai-services/language-service/question-answering/overview)
- [What is Azure Cognitive Search?](/azure/search/search-what-is-azure-search)
- [What is Language Understanding (LUIS)?](/azure/ai-services/luis/what-is-luis)
- [What is QnA Maker?](/azure/ai-services/qnamaker/overview/overview)
