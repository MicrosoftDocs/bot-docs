---
title: Use QnA Maker to answer questions
description: Learn how bots can answer questions from users without parsing or interpreting the questions. See how to use QnA Maker for this task.
keywords: question and answer, QnA, FAQs, qna maker
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: azure-ai-bot-service
ms.date: 08/08/2024
monikerRange: 'azure-bot-service-4.0'

ROBOTS: NOINDEX
ms.custom:
  - evergreen
---

# Use QnA Maker to answer questions

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

[!INCLUDE [qnamaker-sunset-alert](../includes/qnamaker-sunset-alert.md)]

QnA Maker provides a conversational question and answer layer over your data. This allows your bot to send a question to the QnA Maker and receive an answer without needing to parse and interpret the question intent.

One of the basic requirements in creating your own QnA Maker service is to populate it with questions and answers. In many cases, the questions and answers already exist in content like FAQs or other documentation; other times, you may want to customize your answers to questions in a more natural, conversational way.

This article describes how to use an _existing_ QnA Maker knowledge base from your bot.

For new bots, consider using the [question answering](bot-builder-concept-luis.md#question-answering) feature of Azure Cognitive Service for Language. For information, see [Use question answering to answer questions](../bot-builder-howto-answer-questions.md).

[!INCLUDE [java-python-sunset-alert](../includes/java-python-sunset-alert.md)]

## Prerequisites

- A [QnA Maker](https://www.qnamaker.ai/) account and an existing QnA Maker knowledge base.
- Knowledge of [bot basics](bot-builder-basics.md) and [QnA Maker](/azure/ai-services/qnamaker/overview/overview).
- A copy of the **QnA Maker (simple)** sample in [**C#** (archived)][], [**JavaScript** (archived)][], [**Java** (archived)][], or [**Python** (archived)][].

## About this sample

To use QnA Maker in your bot, you need an existing knowledge base in the [QnA Maker](https://www.qnamaker.ai/) portal. Your bot then can use the knowledge base to answer the user's questions.

For new bot development, consider using [Copilot Studio](/microsoft-copilot-studio/fundamentals-what-is-copilot-studio).
If you need to create a new knowledge base for a Bot Framework SDK bot, see the following Azure AI services articles:

- [What is question answering?](/azure/ai-services/language-service/question-answering/overview)
- [Create an FAQ bot](/azure/ai-services/language-service/question-answering/tutorials/bot-service)
- [Azure Cognitive Language Services Question Answering client library for .NET](https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/cognitivelanguage/Azure.AI.Language.QuestionAnswering#readme)

## [C#](#tab/cs)

:::image type="content" source="./media/qnabot-logic-flow.png" alt-text="C# QnABot logic flow":::

`OnMessageActivityAsync` is called for each user input received. When called, it accesses configuration settings from the sample code's **appsetting.json** file to find the value to connect to your pre-configured QnA Maker knowledge base.

## [JavaScript](#tab/js)

:::image type="content" source="./media/qnabot-js-logic-flow.png" alt-text="JavaScript QnABot logic flow":::

`OnMessage` is called for each user input received. When called, it accesses configuration settings from your sample code's **.env** file.  The qnamaker method `getAnswers` connects your bot to your external QnA Maker knowledge base.

## [Java](#tab/java)

:::image type="content" source="./media/qnabot-logic-flow-java.png" alt-text="Java QnABot logic flow":::

`onMessageActivity` is called for each user input received. When called, it accesses configuration settings from the sample code's **application.properties** file to find the value to connect to your pre-configured QnA Maker knowledge base.

## [Python](#tab/python)

:::image type="content" source="./media/qnabot-python-logic-flow.png" alt-text="Python QnABot logic flow":::

`on_message_activity` is called for each user input received. When called, it accesses configuration settings from your sample code's **config.py** file.  The method `qna_maker.getAnswers` connects your bot to your external QnA Maker knowledge base.

---

The user's input is sent to your knowledge base and the best returned answer is displayed back to your user.

## Obtain values to connect your bot to the knowledge base

> [!TIP]
> The QnA Maker documentation has instructions on how to [create, train, and publish your knowledge base](/azure/ai-services/qnamaker/quickstarts/create-publish-knowledge-base).

1. In the [QnA Maker](https://www.qnamaker.ai/) site, select your knowledge base.
1. With your knowledge base open, select the **SETTINGS** tab. Record the value shown for _service name_. This value is useful for finding your knowledge base of interest when using the QnA Maker portal interface. It's not used to connect your bot app to this knowledge base.
1. Scroll down to find **Deployment details** and record the following values from the Postman sample HTTP request:
   - POST /knowledgebases/\<knowledge-base-id>/generateAnswer
   - Host: \<your-host-url>
   - Authorization: EndpointKey \<your-endpoint-key>

Your host URL will start with `https://` and end with `/qnamaker`, such as `https://<hostname>.azure.net/qnamaker`. Your bot needs the knowledge base ID, host URL, and endpoint key to connect to your QnA Maker knowledge base.

## Update the settings file

First, add the information required to access your knowledge base&mdash;including host name, endpoint key and knowledge base ID (kbId)&mdash;to the settings file. These are the values you saved from the **SETTINGS** tab of your knowledge base in QnA Maker.

If you aren't deploying this for production, you can leave your bot's app ID and password fields blank.

> [!NOTE]
> To add a QnA Maker knowledge base into an existing bot application, be sure to add informative titles for your QnA entries. The "name" value within this section provides the key required to access this information from within your app.

## [C#](#tab/cs)

**appsettings.json**

[**C#** (archived)][]

## [JavaScript](#tab/js)

**.env**

[**JavaScript** (archived)][]

## [Java](#tab/java)

**application.properties**

[**Java** (archived)][]

## [Python](#tab/python)

**config.py**

[**Python** (archived)][]

---

## Set up the QnA Maker instance

First, we create an object for accessing our QnA Maker knowledge base.

## [C#](#tab/cs)

Be sure that the **Microsoft.Bot.Builder.AI.QnA** NuGet package is installed for your project.

In **QnABot.cs**, in the `OnMessageActivityAsync` method, create a QnAMaker instance. The `QnABot` class is also where the names of the connection information, saved in **appsettings.json** above, are pulled in. If you chose different names for your knowledge base connection information in your settings file, be sure to update the names here to reflect your chosen name.

**Bots/QnABot.cs**

[**C#** (archived)][]

## [JavaScript](#tab/js)

Be sure that npm package **botbuilder-ai** is installed for your project.

In the sample, the code for the bot logic is in the **QnABot.js** file.

In the **QnABot.js** file, we use the connection information provided by your .env file to establish a connection to the QnA Maker service: _this.qnaMaker_.

**bots/QnABot.js**

[**JavaScript** (archived)][]

## [Java](#tab/java)

In **QnABot.java**, in the `onMessageActivity` method, create a QnAMaker instance. The `QnABot` class is also where the names of the connection information, saved in **application.properties** above, are pulled in. If you chose different names for your knowledge base connection information in your settings file, be sure to update the names here to reflect your chosen name.

**QnABot.java**

[**Java** (archived)][]

## [Python](#tab/python)

In the **qna_bot.py** file, use the connection information provided by the **config.py** file to establish a connection to the QnA Maker service: `self.qna_maker`.

**bots/qna_bot.py**

[**Python** (archived)][]

---

## Calling QnA Maker from your bot

## [C#](#tab/cs)

When your bot needs an answer from QnAMaker, call the `GetAnswersAsync` method from your bot code to get the appropriate answer based on the current context. If you're accessing your own knowledge base, change the _no answers found_ message below to provide useful instructions for your users.

**Bots/QnABot.cs**

[**C#** (archived)][]

## [JavaScript](#tab/js)

In the **QnABot.js** file, we pass the user's input to the QnA Maker service's `getAnswers` method to get answers from the knowledge base. If QnA Maker returns a response, this is shown to the user. Otherwise, the user receives the message 'No QnA Maker answers were found.'

**bots/QnABot.js**

[**JavaScript** (archived)][]

## [Java](#tab/java)

When your bot needs an answer from QnAMaker, call the `getAnswers` method from your bot code to get the appropriate answer based on the current context. If you're accessing your knowledge base, change the _no answers found_ message to provide helpful instructions for your users.

**QnABot.java**

[**Java** (archived)][]

## [Python](#tab/python)

In the **qna_bot.py** file, we pass the user's input to the QnA Maker service's `get_answers` method to get answers from the knowledge base. If QnA Maker returns a response, this is shown to the user. Otherwise, the user receives the message _No QnA Maker answers were found._

**bots/qna_bot.py**

[**Python** (archived)][]

---

## Test the bot

Run the sample locally on your machine. If you haven't done so already, install the [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/blob/master/README.md#download). For further instructions, refer to the sample's `README` ([**C#** (archived)][], [**JavaScript** (archived)][], [**Java** (archived)][], or [**Python** (archived)][]).

Start the Emulator, connect to your bot, and send messages to your bot. The responses to your questions will vary, based on the information your knowledge base.

:::image type="content" source="../media/emulator-v4/qna-test-bot.png" alt-text="Test sample bot.":::

## Additional information

The **QnA Maker multi-turn** sample ([**C#** multi-turn sample (archived)][], [**JavaScript** multi-turn sample (archived)][], [**Java** multi-turn sample (archived)][], [**Python** multi-turn sample (archived)][]) shows how to use a QnA Maker dialog to support QnA Maker's follow-up prompt and active learning features.

- QnA Maker supports follow-up prompts, also known as multi-turn prompts.
If the QnA Maker knowledge base requires more information from the user, QnA Maker sends context information that you can use to prompt the user. This information is also used to make any follow-up calls to the QnA Maker service.
In version 4.6, the Bot Framework SDK added support for this feature.

  To construct such a knowledge base, see the QnA Maker documentation on how to [Use follow-up prompts to create multiple turns of a conversation](/azure/ai-services/QnAMaker/How-To/multi-turn).

- QnA Maker also supports active learning suggestions, allowing the knowledge base to improve over time.
The QnA Maker dialog supports explicit feedback for the active learning feature.

  To enable this feature on a knowledge base, see the QnA Maker documentation on [Active learning suggestions](/azure/ai-services/qnamaker/).

## Next steps

QnA Maker can be combined with other Azure AI services, to make your bot even more powerful. Bot Framework Orchestrator provides a way to combine QnA with Language Understanding (LUIS) in your bot.

> [!div class="nextstepaction"]
> [Use Orchestrator for intent resolution](./bot-builder-tutorial-orchestrator.md)

[**C#** (archived)]: https://github.com/microsoft/BotBuilder-Samples/tree/main/archive/samples/csharp_dotnetcore/11.qnamaker
[**JavaScript** (archived)]: https://github.com/microsoft/BotBuilder-Samples/tree/main/archive/samples/javascript_nodejs/11.qnamaker
[**Java** (archived)]: https://github.com/microsoft/BotBuilder-Samples/tree/main/archive/samples/java_springboot/11.qnamaker
[**Python** (archived)]: https://github.com/microsoft/BotBuilder-Samples/tree/main/archive/samples/python/11.qnamaker

[**C#** multi-turn sample (archived)]: https://github.com/microsoft/BotBuilder-Samples/tree/main/archive/samples/csharp_dotnetcore/49.qnamaker-all-features
[**JavaScript** multi-turn sample (archived)]: https://github.com/microsoft/BotBuilder-Samples/tree/main/archive/samples/javascript_nodejs/49.qnamaker-all-features
[**Java** multi-turn sample (archived)]: https://github.com/microsoft/BotBuilder-Samples/tree/main/archive/samples/java_springboot/49.qnamaker-all-features
[**Python** multi-turn sample (archived)]: https://github.com/microsoft/BotBuilder-Samples/tree/main/archive/samples/python/49.qnamaker-all-features
