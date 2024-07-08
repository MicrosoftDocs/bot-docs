---
title: Use question answering to answer questions
description: Learn how bots can answer questions from users without parsing or interpreting the questions. See how to use question answering for this task.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 12/08/2022
monikerRange: 'azure-bot-service-4.0'
ms.custom:
  - evergreen
---

# Use question answering to answer questions

[!INCLUDE [applies-to-v4](./includes/applies-to-v4-current.md)]

The [question answering](v4sdk/bot-builder-concept-luis.md#question-answering) feature of Azure Cognitive Service for Language provides cloud-based natural language processing (NLP) that allows you to create a natural conversational layer over your data. It's used to find the most appropriate answer for any input from your custom knowledge base of information.

This article describes how to use the question answering feature in your bot.

## Prerequisites

- If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A language resource in [Language Studio](https://language.cognitive.azure.com/), with the custom question answering feature enabled.
- A copy of the **Custom Question Answering** sample in [**C#**][cs sample] or [**JavaScript**][js sample].

[cs sample]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/12.customQABot
[js sample]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/javascript_nodejs/12.customQABot

## About this sample

To use question answering in your bot, you need an existing knowledge base. Your bot then can use the knowledge base to answer the user's questions.

If you need to create a new knowledge base for a Bot Framework SDK bot, see the README for the custom question answering sample.

## [C#](#tab/cs)

:::image type="content" source="./media/bot-builder-howto-answer-questions/flow-diagram-cs.png" alt-text="C# question answering bot logic flow.":::

`OnMessageActivityAsync` is called for each user input received. When called, it accesses configuration settings from the sample code's **appsetting.json** file and connects to your knowledge base.

## [JavaScript](#tab/js)

:::image type="content" source="./media/bot-builder-howto-answer-questions/flow-diagram-js.png" alt-text="JavaScript question answering bot logic flow.":::

`OnMessage` is called for each user input received. When called, it accesses configuration settings from your sample code's **.env** file and connects to your knowledge base.

