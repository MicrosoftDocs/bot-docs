---
title: Detailed overview of the Enterprise Bot Template | Microsoft Docs
description: Learn about the design decisions behind the Enterprise Bot Template
author: darrenj
ms.author: darrenj
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 09/18/2018
monikerRange: 'azure-bot-service-4.0'
---
# Enterprise Bot Template - Overview

> [!NOTE]
> This topic applies to v4 version of the SDK.

The Enterprise Bot Template provides a solid foundation of the best practices and services needed to create a conversational experience, reducing effort and raising the quality bar. The template leverages [Bot Builder SDK v4](https://github.com/Microsoft/botbuilder) and [Bot Builder Tools](https://github.com/Microsoft/botbuilder-tools) to provide the following features:

Feature      | Description |
------------ | -------------
Introduction | Introduction message with an [Adaptive Card]() on conversation start
Typing indicators  | Automated visual typing indicators during conversations and repeat for long running operations
Base LUIS model  | Supports common intents such as **Cancel**, **Help**, **Escalate**, etc.
Base dialogs | Dialog flows for capturing basic user information as well as interruption logic for cancel and help intents
Base responses  | Text and speech responses for base intents and dialogs
FAQ | Integration with [QnA Maker](https://www.qnamaker.ai) to answer general questions from a knowledgebase
Chit-chat | A professional chit-chat model to provide standard answers to common queries ([learn more](https://docs.microsoft.com/azure/cognitive-services/qnamaker/how-to/chit-chat-knowledge-base))
Dispatcher | An integrated [Dispatch](https://docs.microsoft.com/azure/bot-service/bot-builder-tutorial-dispatch?view=azure-bot-service-4.0&tabs=csaddref%2Ccsbotconfig) model to identify whether a given utterance should be processed by LUIS or QnA Maker.
Language support | Available in English, French, Italian, German, Spanish and Chinese
Transcripts | Transcripts of all conversations stored in Azure Storage
Telemetry  | [Application Insights](https://azure.microsoft.com/en-gb/services/application-insights/) integration to collect telemetry for all conversations
Analytics | An example Power BI dashboard to get you started with insights into your conversational experiences.
Automated deployment | Easy deployment of all aforementioned services using [Bot Builder Tools](https://github.com/Microsoft/botbuilder-tools)

# Background

## Introduction Card
An introduction card improves conversations by providing an overview of the bot's capabilities and providing sample utterances to make getting started easy. It also establishes the bot's personality to the user.

## Base LUIS model with common intents
The base LUIS model included in the template covers a selection of the most common intents that most bots will need to handle. The following intents are available to use in your bot out-of-the-box:

Intent       | Sample Utterances |
-------------|-------------|
Cancel       |*cancel*, *nevermind*|
Escalate     |*can I talk to a person?*|
FinishTask   |*done*, *all finished*|
GoBack       |*go back*|
Help         |*can you help me?*|
Repeat       |*can you say that again?*|
SelectAny    |*any of these*|
SelectItem   |*the first one*|
SelectNone   |*none of these*|
ShowNext     |*show more*|
ShowPrevious |*show previous*|
StartOver    |*restart*|
Stop         |*stop*|

## QnA Maker

[QnA Maker](https://www.qnamaker.ai/) provides the ability for non-developers to generate a collection of question and answer pairs for use in a bot. This knowledge can be imported from FAQ data sources and product manuals, or created manually within the QnA Maker portal.

Two example QnA Maker models are provided in the template, one for FAQ and one for [Professional Chit-chat](https://docs.microsoft.com/azure/cognitive-services/qnamaker/how-to/chit-chat-knowledge-base).

## Dispatch

The Dispatch service is used to manage routing between multiple LUIS models and QnA Maker knowledgebases by extracting utterances from each service and creating a central dispatch LUIS model.

This enables a bot to quickly identify which component should handle a given utterance and ensures QnA Maker data is considered at the top level of intent processing rather than at the bottom of a hierarchy.

This Dispatch tool also enables evaluation of your models which highlights overlapping utterances and discrepencies across services.

## Telemetry

Providing insights into bot conversations can help you understand the levels of user engagement, what features users are using, and the questions the bot is unable to handle.

[Application Insights](https://docs.microsoft.com/azure/azure-monitor/app/app-insights-overview) captures operational telemetry for your bot service as well as conversation specific events. These events can be aggregated into actionable information using tools like [Power BI](https://powerbi.microsoft.com/en-us/what-is-power-bi/). An example Power BI dashboard is available for use with the Enterprise Bot Template that demonstrates this capability.

# Next Steps
Refer to [Getting Started](bot-builder-enterprise-template-getting-started.md) to learn how to create and deploy your Enterprise Bot.

# Resources
Full source code for the Enterprise Bot Template can be found on [GitHub](https://github.com/Microsoft/AI/tree/master/templates/Enterprise-Template).
