---
title: Use question answering to answer questions
description: Learn how bots can answer questions from users without parsing or interpreting the questions. See how to use question answering for this task.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: azure-ai-bot-service
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

---

The user's input is sent to your knowledge base and the best returned answer is displayed back to your user.

## Get your knowledge base connection settings

1. In [Language Studio](https://language.cognitive.azure.com/), open your language resource.
1. Copy the following information to your bot's configuration file:

    - The host name for your language endpoint.
    - The `Ocp-Apim-Subscription-Key`, which is your endpoint key.
    - The project name, which acts as your knowledge base ID.

Your host name is the part of the endpoint URL between `https://` and `/language`, for example, `https://<hostname>/language`. Your bot needs the project name, host URL, and endpoint key to connect to your knowledge base.

> [!TIP]
> If you aren't deploying this for production, you can leave your bot's app ID and password fields blank.

## Set up and call the knowledge base client

Create your knowledge base client, then use the client to retrieve answers from the knowledge base.

## [C#](#tab/cs)

Be sure that the **Microsoft.Bot.Builder.AI.QnA** NuGet package is installed for your project.

In **QnABot.cs**, in the `OnMessageActivityAsync` method, create a knowledge base client. Use the turn context to query the knowledge base.

**Bots/CustomQABot.cs**

:::code language="csharp" source="~/../botbuilder-samples/samples/csharp_dotnetcore/12.customQABot/Bots/CustomQABot.cs" range="65-72":::

## [JavaScript](#tab/js)

Be sure that npm package **botbuilder-ai** is installed for your project.

In **CustomQABot.js**, in the constructor, create a knowledge base client. In the **onMessage** method, use the turn context to query the knowledge base.

**bots/CustomQABot.js**

:::code language="javascript" source="~/../botbuilder-samples/samples/javascript_nodejs/12.customQABot/bots/CustomQABot.js" range="12-16":::

:::code language="javascript" source="~/../botbuilder-samples/samples/javascript_nodejs/12.customQABot/bots/CustomQABot.js" range="46-48":::

---

## Test the bot

Run the sample locally on your machine. If you haven't done so already, install the [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/blob/master/README.md#download). For further instructions, refer to the sample's `README` ([C#][CS readme] or [JavaScript][JS readme]).

Start the Emulator, connect to your bot, and send messages to your bot. The responses to your questions will vary, based on the information your knowledge base.

[CS readme]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/12.customQABot#readme
[JS readme]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/javascript_nodejs/12.customQABot#readme

## Additional information

The **Custom Question Answering, all features** sample ([**C#**][CS adv readme] or [**JavaScript**][JS adv readme]) shows how to use a _QnA Maker dialog_ to support a knowledge base's follow-up prompt and active learning features.

- Question answering supports follow-up prompts, also known as multi-turn prompts. If the knowledge base requires more information from the user, the service sends context information that you can use to prompt the user. This information is also used to make any follow-up calls to the service.
- Question answering also supports active learning suggestions, allowing the knowledge base to improve over time. The QnA Maker dialog supports explicit feedback for the active learning feature.

[CS adv readme]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/48.customQABot-all-features#readme
[JS adv readme]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/javascript_nodejs/48.customQABot-all-features#readme