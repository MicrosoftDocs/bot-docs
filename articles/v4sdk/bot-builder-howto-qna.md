---
title: Use QnA Maker to answer questions | Microsoft Docs
description: Learn how to use QnA maker in your bot.
keywords: question and answer, QnA, FAQs, middleware 
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 10/08/2018
monikerRange: 'azure-bot-service-4.0'
---

# Use QnA Maker to answer questions

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

You can use QnA Maker service to add question and answer support to your bot. One of the basic requirements in creating your own QnA Maker service is to seed it with questions and answers. In many cases, the questions and answers already exist in content like FAQs or other documentation. Other times you would like to customize your answers to questions in a more natural, conversational way.

## Prerequisites
- Create a [QnA Maker](https://www.qnamaker.ai/) account
- Download QnA Maker sample [[C#](https://aka.ms/cs-qna) | [JavaScript](https://aka.ms/js-qna-sample)]

## Create a QnA Maker service and publish a knowledge base

After creating the QnA Maker account, follow the instructions for creating [QnA Maker service](https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/how-to/set-up-qnamaker-service-azure) and [knowledge base](https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/quickstarts/create-publish-knowledge-base). 

After publishing your knowledge base, you need to record the following values to programmatically to connect your bot to the knowledge base.
- In the [QnA Maker](https://www.qnamaker.ai/) site, select your knowledge base.
- With your knowledge base open, select the **Settings**. Record the value shown for _service name_ as <your_kb_name>
- Scroll down to find **Deployment details** and record the following values:
   - POST /knowledgebases/<your_knowledge_base_id>/generateAnswer
   - Host: https://<you_hostname>.azurewebsites.net/qnamaker
   - Authorization: EndpointKey <your_endpoint_key>

## Installing Packages

Before we get coding, make sure you have the packages necessary for QnA Maker.

# [C#](#tab/cs)

Add the following [NuGet package](https://docs.microsoft.com/en-us/nuget/tools/package-manager-ui) to your bot.

* `Microsoft.Bot.Builder.AI.QnA`

# [JavaScript](#tab/js)

QnA Maker features are in the `botbuilder-ai` package. You can add this package to your project via npm:

```shell
npm install --save botbuilder-ai
```

---

## Using CLI tools to update your .bot configuration

An alternate method for obtaining your knowledge base access values is to use the [qnamaker](https://aka.ms/botbuilder-tools-qnaMaker) and [msbot](https://aka.ms/botbuilder-tools-msbot-readme) BotBuilder CLI tools to get metadata about your knowledge base and add it to your .bot file.

1. Open a terminal or command prompt and navigate to the root directory for your bot project.
2. Run `qnamaker init` to create a QnA Maker resource file (**.qnamakerrc**). It will prompt you for your QnA Maker subscription key.
3. Run the following command to download your metadata and add it to your bot's configuration file.

    ```shell
    qnamaker get kb --kbId <your-kb-id> --msbot | msbot connect qna --stdin [ --secret <your-secret>]
    ```
If you've encrypted your configuration file, you will need to provide your secret key to update the file.

## Using QnA Maker
A reference to the QnA Maker is first added when initializing the bot. Then we can call it within our bot logic.

# [C#](#tab/cs)
Open the QnA Maker sample you downloaded earlier. We will modify this code as needed.
First, add the information required to access your knowledge base including hostname, endpoint key and knowledge base Id (KbId) into the `BotConfiguration.bot`. These are the values you saved from the **Settings** of your knowledge base in QnA Maker.

```json
{
  "name": "QnABotSample",
  "services": [
    {
      "type": "endpoint",
      "name": "development",
      "endpoint": "http://localhost:3978/api/messages",
      "appId": "",
      "id": "1",
      "appPassword": ""
    },
    {
      "type": "qna",
      "name": "QnABot",
      "KbId": "<YOUR_KNOWLEDGE_BASE_ID>",
      "Hostname": "https://<YOUR_HOSTNAME>.azurewebsites.net/qnamaker",
      "EndpointKey": "<YOUR_ENDPOINT_KEY>"
    }
  ],
  "version": "2.0",
  "padlock": ""
}
```

Next, we create a QnA Maker instance in `Startup.cs`. This grabs the above mentioned information from the `BotConfiguration.bot` file. These strings can also be hard coded for testing.

```csharp
private static BotServices InitBotServices(BotConfiguration config)
{
    var qnaServices = new Dictionary<string, QnAMaker>();
    foreach (var service in config.Services)
    {
        switch (service.Type)
        {
            case ServiceTypes.QnA:
            {
                // Create a QnA Maker that is initialized and suitable for passing
                // into the IBot-derived class (QnABot).
                var qna = (QnAMakerService)service;
                if (qna == null)
                {
                    throw new InvalidOperationException("The QnA service is not configured correctly in your '.bot' file.");
                }

                if (string.IsNullOrWhiteSpace(qna.KbId))
                {
                    throw new InvalidOperationException("The QnA KnowledgeBaseId ('kbId') is required to run this sample. Please update your '.bot' file.");
                }

                if (string.IsNullOrWhiteSpace(qna.EndpointKey))
                {
                    throw new InvalidOperationException("The QnA EndpointKey ('endpointKey') is required to run this sample. Please update your '.bot' file.");
                }

                if (string.IsNullOrWhiteSpace(qna.Hostname))
                {
                    throw new InvalidOperationException("The QnA Host ('hostname') is required to run this sample. Please update your '.bot' file.");
                }

                var qnaEndpoint = new QnAMakerEndpoint()
                {
                    KnowledgeBaseId = qna.KbId,
                    EndpointKey = qna.EndpointKey,
                    Host = qna.Hostname,
                };

                var qnaMaker = new QnAMaker(qnaEndpoint);
                qnaServices.Add(qna.Name, qnaMaker);
                break;
            }
        }
    }
    var connectedServices = new BotServices(qnaServices);
    return connectedServices;
}
```

Then we need to give your bot this QnAMaker instance. Open up `QnABot.cs`, and at the top of the file add the following code. If you are accessing your own knowledge base, change the _welcome_ message shown below to provide useful initial instructions for your users.

```csharp
public class QnABot : IBot
{
    public static readonly string QnAMakerKey = "QnABot";
    private const string WelcomeText = @"This bot will introduce you to QnA Maker.
                                         Ask a quesiton to get started.";
    private readonly BotServices _services;
    public QnABot(BotServices services)
    {
        _services = services ?? throw new System.ArgumentNullException(nameof(services));
        Console.WriteLine($"{_services}");
        if (!_services.QnAServices.ContainsKey(QnAMakerKey))
        {
            throw new System.ArgumentException($"Invalid configuration. Please check your '.bot' file for a QnA service named '{QnAMakerKey}'.");
        }
    }
}
```

# [JavaScript](#tab/js)
Open the QnA Maker sample you downloaded earlier. We will modify this code as needed.
In our sample, the startup code is in an **index.js** file, the code for the bot logic is in a **bot.js** file, and additional configuration information is in the **qnamaker.bot** file.

After following the instructions for creating your knowledge base and for updating your **.bot** file, your **qnamaker.bot** file should include a service entry for your QnA Maker knowledge base.

```json
{
    "name": "qnamaker",
    "description": "",
    "services": [
        {
            "type": "endpoint",
            "name": "development",
            "id": "1",
            "appId": "",
            "appPassword": "",
            "endpoint": "http://localhost:3978/api/messages"
        },
        {
            "type": "qna",
            "name": "<YOUR_KB_NAME>",
            "kbId": "<YOUR_KNOWLEDGE_BASE_ID>",
            "endpointKey": "<YOUR_ENDPOINT_KEY>",
            "hostname": "https://<YOUR_HOSTNAME>.azurewebsites.net/qnamaker",
            "id": "221"
        }
    ],
    "padlock": "",
    "version": "2.0"
}
```

In the **index.js** file, we read in the configuration information to generate the QnA Maker service and initialize the bot.

Update the value of `QNA_CONFIGURATION` to the name of your knowledge base, as it appears in your configuration file.

```js
// QnA Maker knowledge base name as specified in .bot file.
const QNA_CONFIGURATION = '<YOUR_KB_NAME>';

// Get endpoint and QnA Maker configurations by service name.
const endpointConfig = botConfig.findServiceByNameOrId(BOT_CONFIGURATION);
const qnaConfig = botConfig.findServiceByNameOrId(QNA_CONFIGURATION);

// Map the contents to the required format for `QnAMaker`.
const qnaEndpointSettings = {
    knowledgeBaseId: qnaConfig.kbId,
    endpointKey: qnaConfig.endpointKey,
    host: qnaConfig.hostname
};

// Create adapter...

// Create the QnAMakerBot.
let bot;
try {
    bot = new QnAMakerBot(qnaEndpointSettings, {});
} catch (err) {
    console.error(`[botInitializationError]: ${ err }`);
    process.exit();
}
```

We then create the HTTP server and listen for incoming requests, which will generate calls to our bot logic.

```js
// Create HTTP server.
let server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function() {
    console.log(`\n${ server.name } listening to ${ server.url }.`);
    console.log(`\nGet Bot Framework Emulator: https://aka.ms/botframework-emulator.`);
    console.log(`\nTo talk to your bot, open qnamaker.bot file in the emulator.`);
});

// Listen for incoming requests.
server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (turnContext) => {
        await bot.onTurn(turnContext);
    });
});
```

---

## Calling QnA Maker from your bot

# [C#](#tab/cs)

When your bot needs an answer from QnAMaker, call `GetAnswersAsync()` from your bot code to get the appropriate answer based on the current context. If you are accessing your own knowledge base, change the _no answers_ message below to provide useful instructions for your users.

```csharp
// Check QnA Maker model
var response = await _services.QnAServices[QnAMakerKey].GetAnswersAsync(turnContext);
if (response != null && response.Length > 0)
{
    await turnContext.SendActivityAsync(response[0].Answer, cancellationToken: cancellationToken);
}
else
{
    var msg = @"No QnA Maker answers were found. This example uses a QnA Maker Knowledge Base that focuses on smart light bulbs.
                To see QnA Maker in action, ask the bot questions like 'Why won't it turn on?' or 'I need help'.";
    await turnContext.SendActivityAsync(msg, cancellationToken: cancellationToken);
}

    /// ...
```

# [JavaScript](#tab/js)

In the **bot.js** file, we pass the user's input to the QnA Maker service's `generateAnswer` method to get answers from the knowledge base. If you are accessing your own knowledge base, change the _no answers_ and _welcome_ messages below to provide useful instructions for your users.

```javascript
const { ActivityTypes, TurnContext } = require('botbuilder');
const { QnAMaker, QnAMakerEndpoint, QnAMakerOptions } = require('botbuilder-ai');

/**
 * A simple bot that responds to utterances with answers from QnA Maker.
 * If an answer is not found for an utterance, the bot responds with help.
 */
class QnAMakerBot {
    /**
     * The QnAMakerBot constructor requires one argument (`endpoint`) which is used to create an instance of `QnAMaker`.
     * @param {QnAMakerEndpoint} endpoint The basic configuration needed to call QnA Maker. In this sample the configuration is retrieved from the .bot file.
     * @param {QnAMakerOptions} config An optional parameter that contains additional settings for configuring a `QnAMaker` when calling the service.
     */
    constructor(endpoint, qnaOptions) {
        this.qnaMaker = new QnAMaker(endpoint, qnaOptions);
    }

    /**
     * Every conversation turn for our QnAMakerBot will call this method.
     * @param {TurnContext} turnContext Contains all the data needed for processing the conversation turn.
     */
    async onTurn(turnContext) {
        // By checking the incoming Activity type, the bot only calls QnA Maker in appropriate cases.
        if (turnContext.activity.type === ActivityTypes.Message) {
            // Perform a call to the QnA Maker service to retrieve matching Question and Answer pairs.
            const qnaResults = await this.qnaMaker.generateAnswer(turnContext.activity.text);

            // If an answer was received from QnA Maker, send the answer back to the user.
            if (qnaResults[0]) {
                await turnContext.sendActivity(qnaResults[0].answer);

            // If no answers were returned from QnA Maker, reply with help.
            } else {
                await turnContext.sendActivity('No QnA Maker answers were found. This example uses a QnA Maker Knowledge Base that focuses on smart light bulbs. To see QnA Maker in action, ask the bot questions like "Why won\'t it turn on?" or "I need help."');
            }

        // If the Activity is a ConversationUpdate, send a greeting message to the user.
        } else if (turnContext.activity.type === ActivityTypes.ConversationUpdate &&
                   turnContext.activity.recipient.id !== turnContext.activity.membersAdded[0].id) {
            await turnContext.sendActivity('Welcome to the QnA Maker sample! Ask me a question and I will try to answer it.');

        // Respond to all other Activity types.
        } else if (turnContext.activity.type !== ActivityTypes.ConversationUpdate) {
            await turnContext.sendActivity(`[${ turnContext.activity.type }]-type activity detected.`);
        }
    }
}

module.exports.QnAMakerBot = QnAMakerBot;
```

---

Ask your bot questions to see the replies from your QnA Maker service. For more information on testing and publishing your QnA service, see the QnA Maker article on [testing a knowledge base](https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/how-to/test-knowledge-base).

## Next steps

QnA Maker can be combined with other Cognitive Services, to make your bot even more powerful. The Dispatch tool provides a way to combine QnA with Language Understanding (LUIS) in your bot.

> [!div class="nextstepaction"]
> [Combine LUIS and QnA services using the Dispatch tool](./bot-builder-tutorial-dispatch.md)
