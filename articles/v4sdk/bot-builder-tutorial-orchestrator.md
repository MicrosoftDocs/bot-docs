---
title: Use multiple LUIS and QnA Maker projects with Orchestrator
description: Learn how bots can use multiple LUIS models and QnA Maker knowledge bases. See how to use Orchestrator to route user input to the correct model.
keywords: Luis, QnA, Orchestrator, multiple services, route intents
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

# Use multiple LUIS and QnA models with Orchestrator

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

[!INCLUDE [qnamaker-sunset-alert](../includes/qnamaker-sunset-alert.md)]

[!INCLUDE [luis-sunset-alert](../includes/luis-sunset-alert.md)]

If a bot uses multiple Language Understanding (LUIS) models and QnA Maker knowledge bases, you can use Bot Framework Orchestrator to determine which LUIS model or QnA Maker knowledge base best matches the user input. You can use the `bf orchestrator` CLI command to create an Orchestrator snapshot file, then use the snapshot file to route user input to the correct model at run time.

This article describes how to use an _existing_ QnA Maker knowledge base with Orchestrator.

- For new bots, consider using the [question answering](bot-builder-concept-luis.md#question-answering) and [orchestration workflow](bot-builder-concept-luis.md#use-orchestration-workflow) features of Azure AI Language.
- For more information about Orchestrator, see [Intent recognition with Orchestrator in Composer][orchestrator].
- For more information about the `bf orchestrator` command, see [the Bot Framework CLI README][bf-orchestrator-cli].

## Prerequisites

- A [luis.ai](https://www.luis.ai/) account to author LUIS apps.
- A [QnA Maker](https://www.qnamaker.ai/) account and an existing QnA Maker knowledge base.
- A copy of the **NLP with Orchestrator** sample in [**C#** (archived)][] or [**JavaScript** (archived)][].
- Knowledge of [bot basics](bot-builder-basics.md), [LUIS][howto-luis], and [QnA Maker][howto-qna].
- Install the command-line [BF CLI][bf-cli].



## About this sample

This sample is based on a predefined set of LUIS and QnA Maker projects.
However, to use QnA Maker in your bot, you need an existing knowledge base in the [QnA Maker](https://www.qnamaker.ai/) portal.
Your bot then can use the knowledge base to answer the user's questions.

For new bot development, consider using [Copilot Studio](/microsoft-copilot-studio/fundamentals-what-is-copilot-studio).
If you need to create a new knowledge base for a Bot Framework SDK bot, see the following Azure AI services articles:

- [What is question answering?](/azure/ai-services/language-service/question-answering/overview)
- [Create an FAQ bot](/azure/ai-services/language-service/question-answering/tutorials/bot-service)
- [Azure Cognitive Language Services Question Answering client library for .NET](https://github.com/Azure/azure-sdk-for-net/tree/main/sdk/cognitivelanguage/Azure.AI.Language.QuestionAnswering#readme)

## [C#](#tab/cs)

:::image type="content" source="./media/tutorial-orchestrator/class-diagram-cs.png" alt-text="C# class diagram.":::

`OnMessageActivityAsync` is called for each user input received. This module finds the top scoring user intent and passes that result on to `DispatchToTopIntentAsync`. DispatchToTopIntentAsync, in turn, calls the appropriate app handler.

- `ProcessSampleQnAAsync` - for bot FAQ questions.
- `ProcessWeatherAsync` - for weather queries.
- `ProcessHomeAutomationAsync` - for home lighting commands.

## [JavaScript](#tab/js)

:::image type="content" source="./media/tutorial-orchestrator/class-diagram-js.png" alt-text="JavaScript class diagram.":::

`onMessage` is called for each user input received. This module finds the top scoring user intent and passes that result on to `dispatchToTopIntentAsync`. `dispatchToTopIntentAsync`, in turn, calls the appropriate app handler

- `processSampleQnA` - for bot FAQ questions.
- `processWeather` - for weather queries.
- `processHomeAutomation` - for home lighting commands.

---

The handler calls the LUIS or QnA Maker service and returns the generated result back to the user.

## Create LUIS apps

Before you can create an Orchestrator snapshot file, you need LUIS apps and QnA knowledge bases created and published. The sample bot referenced in this article uses the following models, included with the _NLP With Orchestrator_ sample in the `\CognitiveModels` folder:

| Name | Description |
|------|------|
| HomeAutomation | A LUIS app that recognizes a home automation intent with associated entity data.|
| Weather | A LUIS app that recognizes weather-related intents with location data.|
| QnAMaker  | A QnA Maker knowledge base that provides answers to simple questions about the bot. |

### Create the LUIS apps

Create LUIS apps from the _HomeAutomation_ and _Weather_ .lu files in the _cognitive models_ directory of the sample.

1. Run the following command to import, train and publish the app to the production environment.

    ```console
    bf luis:build --in CognitiveModels --authoringKey <YOUR-KEY> --botName <YOUR-BOT-NAME>
    ```

1. Record the application IDs, display names, authoring key, and location.

For more information, see how to **Create a LUIS app in the LUIS portal** and **Obtain values to connect to your LUIS app** in [Add natural language understanding to your bot](bot-builder-howto-v4-luis.md) and the LUIS documentation on how to [train](/azure/ai-services/LUIS/how-to/train-test) and [publish](/azure/ai-services/LUIS/how-to/publish) an app to the production environment.

## Obtain values to connect your bot to the knowledge base

[!INCLUDE [qnamaker-sunset-alert](../includes/qnamaker-sunset-alert.md)]

You need an existing knowledge base and your QnA Maker hostname and endpoint key.

> [!TIP]
> The QnA Maker documentation has instructions on how to [create, train, and publish your knowledge base](/azure/ai-services/qnamaker/quickstarts/create-publish-knowledge-base).

## Create the Orchestrator snapshot file

The CLI interface for the Orchestrator tool creates the Orchestrator snapshot file for routing to the correct LUIS or QnA Maker app at run time.

1. Install the latest supported version of the [Visual C++ Redistributable package](https://support.microsoft.com/help/2977003/the-latest-supported-visual-c-downloads)
1. Open a command prompt or terminal window, and change directories to the sample directory
1. Make sure you have the current version of npm and the Bot Framework CLI.

    ```console
    npm i -g npm
    npm i -g @microsoft/botframework-cli
    ```

1. Download Orchestrator base model file

    ```console
    mkdir model
    bf orchestrator:basemodel:get --out ./model
    ```

1. Create the Orchestrator snapshot file

    ```console
    mkdir generated
    bf orchestrator:create --hierarchical --in ./CognitiveModels --out ./generated --model ./model
    ```

## [C#](#tab/cs)

### Installing packages

Prior to running this app for the first time ensure that several NuGet packages are installed:

- **Microsoft.Bot.Builder**
- **Microsoft.Bot.Builder.AI.Luis**
- **Microsoft.Bot.Builder.AI.QnA**
- **Microsoft.Bot.Builder.AI.Orchestrator**

### Manually update your appsettings.json file

Once all of your service apps are created, the information for each needs to be added into your 'appsettings.json' file. The initial sample for [**C#** (archived)][] code contains an empty appsettings.json file:

**appsettings.json**

[**C#** (archived)][]

For each of the entities shown below, add the values you recorded earlier in these instructions:

```json
"QnAKnowledgebaseId": "<knowledge-base-id>",
"QnAEndpointKey": "<qna-maker-resource-key>",
"QnAEndpointHostName": "<your-hostname>",

"LuisHomeAutomationAppId": "<app-id-for-home-automation-app>",
"LuisWeatherAppId": "<app-id-for-weather-app>",
"LuisAPIKey": "<your-luis-endpoint-key>",
"LuisAPIHostName": "<your-dispatch-app-region>",
```

When all changes are complete, save this file.

## [JavaScript](#tab/js)

### Installing packages

Prior to running this app for the first time, install required npm packages.

```console
npm install
```

### Manually update your .env file

Once all of your service apps are created, the information for each needs to be added into your '.env' file. The initial sample for [**JavaScript** (archived)][] code contains an empty .env file.

**.env**

[**JavaScript** (archived)][]

Add your service connection values as shown below:

```ini
QnAKnowledgebaseId="<knowledge-base-id>"
QnAEndpointKey="<qna-maker-resource-key>"
QnAEndpointHostName="<your-hostname>"

WeatherLuisAppId=<app-id-for-weather-app>
HomeAutomationLuisAppId=<app-id-for-home-automation-app>
LuisAPIKey=<your-luis-endpoint-key>
LuisAPIHostName=<your-dispatch-app-region>
```

When all changes are in place, save this file.

---

### Connect to the services from your bot

To connect to the LUIS, and QnA Maker services, your bot pulls information from the settings file.

## [C#](#tab/cs)

In **BotServices.cs**, the information contained within configuration file _appsettings.json_ is used to connect your Orchestrator bot to the `HomeAutomation`,  `Weather` and `SampleQnA` services. The constructors use the values you provided to connect to these services.

**BotServices.cs**

[**C#** (archived)][]

## [JavaScript](#tab/js)

In **dispatchBot.js** the information contained within configuration file _.env_ is used to connect your dispatch bot to the _LuisRecognizer(HomeAutomation/Weather)_ and _QnAMaker_ services. The constructors use the values you provided to connect to these services.

**bots/dispatchBot.js**

[**JavaScript** (archived)][]

---

### Call the services from your bot

For each input  from your user, the bot logic passes in user input to Orchestrator Recognizer, finds the top returned intent, and uses that information to call the appropriate service for the input.

## [C#](#tab/cs)

In the **DispatchBot.cs** file whenever the `OnMessageActivityAsync` method is called, we check the incoming user message and get the top intent from Orchestrator Recognizer. We then pass the `topIntent` and  `recognizerResult` on to the correct method to call the service and return the result.

**bots\DispatchBot.cs**

[**C#** (archived)][]

## [JavaScript](#tab/js)

In the **dispatchBot.js** `onMessage` method, we check the incoming user message and get the top intent from Orchestrator Recognizer.  We then pass this on by calling _dispatchToTopIntentAsync_.

[**JavaScript** (archived)][]

---

### Work with the recognition results

## [C#](#tab/cs)

When the Orchestrator recognizer produces a result, it indicates which service can most appropriately process the utterance. The code in this bot routes the request to the corresponding service, and then summarizes the response from the called service. Depending on the _intent_ returned from Orchestrator, this code uses the returned intent to route to the correct LUIS model or QnA service.

**bots\DispatchBot.cs**

[**C#** (archived)][]

The `ProcessHomeAutomationAsync` and `ProcessWeatherAsync` methods use the user input contained within the turn context to get the top intent and entities from the correct LUIS model.

The `ProcessSampleQnAAsync` method uses the user input contained within the turn context to generate an answer from the knowledge base and display that result to the user.

## [JavaScript](#tab/js)

When the Orchestrator recognizer produces a result, it indicates which service can most appropriately process the utterance. The code in this sample uses the recognized _topIntent_ to show how to route the request on to the corresponding service.

**bots/dispatchBot.js**

[**JavaScript** (archived)][]

The `processHomeAutomation` and `processWeather` methods use the user input contained within the turn context to get the top intent and entities from the correct LUIS model.

The `processSampleQnA` method uses the user input contained within the turn context to generate an answer from the knowledge base and display that result to the user.

---

> [!NOTE]
> If this were a production application, this is where the selected LUIS methods would connect to their specified service, pass in the user input, and process the returned LUIS intent and entity data.

## Test your bot

1. Using your development environment, start the sample code. Note the _localhost_ address shown in the address bar of the browser window opened by your App: `https://localhost:<Port_Number>`.
1. Open Bot Framework Emulator, click on **Open Bot** button.  
1. In the **Open a bot** dialog box, enter your bot endpoint URL, such as `http://localhost:3978/api/messages`. Click **Connect**.
1. For your reference, here are some of the questions and commands that are covered by the services built for your bot:

    - QnA Maker
      - `hi`, `good morning`
      - `what are you`, `what do you do`
    - LUIS (home automation)
      - `turn on bedroom light`
      - `turn off bedroom light`
      - `make some coffee`
    - LUIS (weather)
      - `whats the weather in redmond washington`
      - `what's the forecast for london`
      - `show me the forecast for nebraska`

## Route user utterance to QnA Maker

1. In the Emulator, enter the text `hi` and submit the utterance. The bot submits this query to Orchestrator and gets back a response indicating which child app should get this utterance for further processing.

1. By selecting the `Orchestrator Recognition Trace` line in the log, you can see the JSON response in the Emulator. The Orchestrator result is displayed in the Inspector.

    ```json
    {
    "type": "trace",
    "timestamp": "2021-05-01T06:26:04.067Z",
    "serviceUrl": "http://localhost:58895",
    "channelId": "emulator",
    "from": {
      "id": "36b2a460-aa43-11eb-920f-7da472b36492",
      "name": "Bot",
      "role": "bot"
    },
    "conversation": {
      "id": "17ef3f40-aa46-11eb-920f-7da472b36492|livechat"
    },
    "recipient": {
      "id": "5f8c6123-2596-45df-928c-566d44426556",
      "role": "user"
    },
    "locale": "en-US",
    "replyToId": "1a3f70d0-aa46-11eb-8b97-2b2a779de581",
    "label": "Orchestrator Recognition",
    "valueType": "OrchestratorRecognizer",
    "value": {
      "text": "hi",
      "alteredText": null,
      "intents": {
        "QnAMaker": {
          "score": 0.9987310956576168
        },
        "HomeAutomation": {
          "score": 0.3402091165577196
        },
        "Weather": {
          "score": 0.24092200496795158
        }
      },
      "entities": {},
      "result": [
        {
          "Label": {
            "Type": 1,
            "Name": "QnAMaker",
            "Span": {
              "Offset": 0,
              "Length": 2
            }
          },
          "Score": 0.9987310956576168,
          "ClosestText": "hi"
        },
        {
          "Label": {
            "Type": 1,
            "Name": "HomeAutomation",
            "Span": {
              "Offset": 0,
              "Length": 2
            }
          },
          "Score": 0.3402091165577196,
          "ClosestText": "make some coffee"
        },
        {
          "Label": {
            "Type": 1,
            "Name": "Weather",
            "Span": {
              "Offset": 0,
              "Length": 2
            }
          },
          "Score": 0.24092200496795158,
          "ClosestText": "soliciting today's weather"
        }
      ]
    },
    "name": "OrchestratorRecognizerResult",
    "id": "1ae65f30-aa46-11eb-8b97-2b2a779de581",
    "localTimestamp": "2021-04-30T23:26:04-07:00"
    }
    ```

   Because the utterance, `hi`, is part of the Orchestrator's **QnAMaker** intent, and is selected as the `topScoringIntent`, the bot will make a second request, this time to the QnA Maker app, with the same utterance.

1. Select the `QnAMaker Trace` line in the Emulator log. The QnA Maker result displays in the Inspector.

    ```json
    {
        "questions": [
            "hi",
            "greetings",
            "good morning",
            "good evening"
        ],
        "answer": "Hello!",
        "score": 1,
        "id": 96,
        "source": "QnAMaker.tsv",
        "metadata": [],
        "context": {
            "isContextOnly": false,
            "prompts": []
        }
    }
    ```

[howto-luis]: bot-builder-howto-v4-luis.md
[howto-qna]: bot-builder-howto-qna.md

[**C#** (archived)]: https://github.com/microsoft/BotBuilder-Samples/tree/main/archive/samples/csharp_dotnetcore/14.nlp-with-orchestrator
[**JavaScript** (archived)]: https://github.com/microsoft/BotBuilder-Samples/tree/main/archive/samples/javascript_nodejs/14.nlp-with-orchestrator
[**Java** (archived)]: https://github.com/microsoft/BotBuilder-Samples/tree/main/archive/samples/java_springboot/14.nlp-with-orchestrator
[**Python** (archived)]: (https://github.com/microsoft/BotBuilder-Samples/tree/main/archive/samples/python/14.nlp-with-orchestrator

[orchestrator]: /composer/concept-orchestrator
[bf-cli]: https://github.com/microsoft/botframework-cli
[bf-orchestrator-cli]: https://github.com/microsoft/botframework-cli/tree/main/packages/orchestrator#readme
