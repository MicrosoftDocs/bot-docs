---
title: Azure Bot Service tutorial to have a bot answer questions | Microsoft Docs
description: Tutorial to use QnA Maker in your bot to answer questions.
keywords: QnA Maker, question and answer, knowledge base
author: JonathanFingold
ms.author: v-jofing
manager: kamrani
ms.topic: tutorial
ms.service: bot-service
ms.subservice: sdk
ms.date: 01/15/2019
monikerRange: 'azure-bot-service-4.0'
---

# Tutorial: Use QnA Maker in your bot to answer questions

You can use the QnA Maker service and a knowledge base to add question-and-answer support to your bot. When you create your knowledge base, you seed it with questions and answers.

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Create a QnA Maker service and knowledge base
> * Add knowledge base information to your .bot file
> * Update your bot to query the knowledge base
> * Re-publish your bot

If you don’t have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

## Prerequisites

* The bot created in the [previous tutorial](bot-builder-tutorial-basic-deploy.md). We will add a question-and-answer feature to the bot.
* Some familiarity with QnA Maker is helpful. We will use the QnA Maker portal to create, train, and publish the knowledge base to use with the bot.

You should already have the prerequisites for the previous tutorial:

[!INCLUDE [deployment prerequisites snippet](~/includes/deploy/snippet-prerequisite.md)]

## Sign in to QnA Maker portal

<!-- This and the next step are close duplicates of what's in the QnA How-To -->

Sign into to the [QnA Maker portal](https://qnamaker.ai/) with your Azure credentials.

## Create a QnA Maker service and knowledge base

We will import an existing knowledge base definition from the QnA Maker sample in the [Microsoft/BotBuilder-Samples](https://github.com/Microsoft/BotBuilder-Samples) repo.

1. Clone or copy the samples repo to your computer.
1. In the QnA Maker portal, **Create a knowledge base**.
   1. If necessary, create a QnA service. (You can use an existing QnA Maker service or create a new one for this tutorial.) For more detailed QnA Maker instructions, see [Create a QnA Maker service](https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/how-to/set-up-qnamaker-service-azure) and [Create, train, and publish your QnA Maker knowledge base](https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/quickstarts/create-publish-knowledge-base).
   1. Connect your QnA service to your knowledge base.
   1. Name your knowledge base.
   1. To populate your knowledge base, use the **BotBuilder-Samples\samples\csharp_dotnetcore\11.qnamaker\CognitiveModels\smartLightFAQ.tsv** file from the samples repo.
   1. Click **Create your kb** to create the knowledge base.
1. **Save and train** your knowledge base.
1. **Publish** your knowledge base.

   The knowledge base is now ready for your bot to use. Record the knowledge base ID, endpoint key, and hostname. You'll need these for the next step.

## Add knowledge base information to your .bot file

Add to your .bot file the information required to access your knowledge base.

1. Open the .bot file in an editor.
1. Add a `qna` element to the `services` array.

    ```json
    {
        "type": "qna",
        "name": "<your-knowledge-base-name>",
        "kbId": "<your-knowledge-base-id>",
        "hostname": "<your-qna-service-hostname>",
        "endpointKey": "<your-knowledge-base-endpoint-key>",
        "subscriptionKey": "<your-azure-subscription-key>",
        "id": "<a-unique-id>"
    }
    ```

    | Field | Value |
    |:----|:----|
    | type | Must be `qna`. This indicates that this service entry describes a QnA knowledge base. |
    | name | The name you assigned to your knowledge base. |
    | kbId | The knowledge base ID that the QnA Maker portal generated for you. |
    | hostname | The host URL that the QnA Maker portal generated. Use the complete URL, starting with `https://` and ending with `/qnamaker`. |
    | endpointKey | The endpoint key that the QnA Maker portal generated for you. |
    | subscriptionKey | The ID for the subscription that you used when you created the QnA Maker service in Azure. |
    | id | A unique ID not already used for one of the other services listed in your .bot file, such as "201". |

1. Save your edits.

## Update your bot to query the knowledge base

Update your initialization code to load the service information for your knowledge base.

# [C#](#tab/csharp)

1. Add the **Microsoft.Bot.Builder.AI.QnA** NuGet package to your project.
1. Rename your class that implements **IBot** to `QnaBot`.
1. Rename the class that contains the accessors for your bot to `QnaBotAccessors`.
1. In your **Startup.cs** file, add these namespace references.
    ```csharp
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Bot.Builder.AI.QnA;
    using Microsoft.Bot.Builder.Integration;
    ```
1. And, modify the **ConfigureServices** method to initialize and register the knowledge bases defined in the **.bot** file. Note that these first few lines were moved from the body of the `services.AddBot<QnaBot>(options =>` call come to before it.
    ```csharp
    public void ConfigureServices(IServiceCollection services)
    {
        var secretKey = Configuration.GetSection("botFileSecret")?.Value;
        var botFilePath = Configuration.GetSection("botFilePath")?.Value;

        // Loads .bot configuration file and adds a singleton that your Bot can access through dependency injection.
        var botConfig = BotConfiguration.Load(botFilePath ?? @".\jfEchoBot.bot", secretKey);
        services.AddSingleton(sp => botConfig ?? throw new InvalidOperationException($"The .bot config file could not be loaded. ({botConfig})"));

        // Initialize the QnA knowledge bases for the bot.
        services.AddSingleton(sp => {
            var qnaServices = new List<QnAMaker>();
            foreach (var qnaService in botConfig.Services.OfType<QnAMakerService>())
            {
                qnaServices.Add(new QnAMaker(qnaService));
            }
            return qnaServices;
        });

        services.AddBot<QnaBot>(options =>
        {
            // Retrieve current endpoint.
            // ...
        });

        // Create and register state accessors.
        // ...
    }
    ```
1. In your **QnaBot.cs** file, add these namespace references.
    ```csharp
    using System.Collections.Generic;
    using Microsoft.Bot.Builder.AI.QnA;
    ```
1. Add a `_qnaServices` property and initialize it in the bot's constructor.
    ```csharp
    private readonly List<QnAMaker> _qnaServices;

    /// ...
    public QnaBot(QnaBotAccessors accessors, List<QnAMaker> qnaServices, ILoggerFactory loggerFactory)
    {
        // ...
        _qnaServices = qnaServices;
    }
    ```
1. Modify the turn handler to query any registered knowledge bases against the user's input. When your bot needs an answer from QnAMaker, call `GetAnswersAsync` from your bot code to get the appropriate answer based on the current context. If you are accessing your own knowledge base, change the _no answers_ message below to provide useful instructions for your users.
    ```csharp
    public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
    {
        if (turnContext.Activity.Type == ActivityTypes.Message)
        {
            foreach(var qnaService in _qnaServices)
            {
                var response = await qnaService.GetAnswersAsync(turnContext);
                if (response != null && response.Length > 0)
                {
                    await turnContext.SendActivityAsync(
                        response[0].Answer,
                        cancellationToken: cancellationToken);
                    return;
                }
            }

            var msg = "No QnA Maker answers were found. This example uses a QnA Maker knowledge base that " +
                "focuses on smart light bulbs. Ask the bot questions like 'Why won't it turn on?' or 'I need help'.";

            await turnContext.SendActivityAsync(msg, cancellationToken: cancellationToken);
        }
        else
        {
            await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
        }
    }
    ```

# [JavaScript](#tab/javascript)

1. Open a terminal or command prompt to the root directory for your project.
1. Add the **botbuilder-ai** npm package to your project.
    ```shell
    npm i botbuilder-ai
    ```
1. In the **index.js** file, add this require statement.
    ```javascript
    const { QnAMaker } = require('botbuilder-ai');
    ```
1. Read in the configuration information to generate the QnA Maker services.
    ```javascript
    // Read bot configuration from .bot file.
    // ...

    // Initialize the QnA knowledge bases for the bot.
    // Assume each QnA entry in the .bot file is well defined.
    const qnaServices = [];
    botConfig.services.forEach(s => {
        if (s.type == 'qna') {
            const endpoint = {
                knowledgeBaseId: s.kbId,
                endpointKey: s.endpointKey,
                host: s.hostname
            };
            const options = {};
            qnaServices.push(new QnAMaker(endpoint, options));
        }
    });

    // Get bot endpoint configuration by service name
    // ...
    ```
1. Update the bot construction to pass in the QnA services.
    ```javascript
    // Create the bot.
    const myBot = new MyBot(qnaServices);
    ```
1. In the **bot.js** file, add a constructor.
    ```javascript
    constructor(qnaServices) {
        this.qnaServices = qnaServices;
    }
    ```
1. And, update your turn handler to query your knowledge bases for an answer.
    ```javascript
    async onTurn(turnContext) {
        if (turnContext.activity.type === ActivityTypes.Message) {
            for (let i = 0; i < this.qnaServices.length; i++) {
                // Perform a call to the QnA Maker service to retrieve matching Question and Answer pairs.
                const qnaResults = await this.qnaServices[i].getAnswers(turnContext);

                // If an answer was received from QnA Maker, send the answer back to the user and exit.
                if (qnaResults[0]) {
                    await turnContext.sendActivity(qnaResults[0].answer);
                    return;
                }
            }
            // If no answers were returned from QnA Maker, reply with help.
            await turnContext.sendActivity('No QnA Maker answers were found. '
                + 'This example uses a QnA Maker Knowledge Base that focuses on smart light bulbs. '
                + `Ask the bot questions like "Why won't it turn on?" or "I need help."`);
        } else {
            await turnContext.sendActivity(`[${ turnContext.activity.type } event detected]`);
        }
    }
    ```

---

### Test the bot locally

At this point your bot should be able to answer some questions. Run the bot locally and open it in the Emulator.

![test qna sample](~/media/emulator-v4/qna-test-bot.png)

## Re-publish your bot

We can now republish your bot.

[!INCLUDE [publish snippet](~/includes/deploy/snippet-publish.md)]

### Test the published bot

After you publish the bot, give Azure a minute or two to update and start the bot.

1. Use the Emulator to test the production endpoint for your bot, or use the Azure portal to test the bot in Web Chat.

   In either case, you should see the same behavior as you did when you tested it locally.

## Clean up resources

<!-- In the first tutorial, we should tell them to use a new resource group, so that it is easy to clean up resources. We should also mention in this step in the first tutorial not to clean up resources if they are continuing with the sequence. -->

If you're not going to continue to use this application, delete
the associated resources with the following steps:

1. In the Azure portal, open the resource group for your bot.
1. Click **Delete resource group** to delete the group and all the resources it contains.
1. In the confirmation pane, enter the resource group name, and click **Delete**.

## Next steps

For information on how to add features to your bot, see the articles in the how-to develop section.
> [!div class="nextstepaction"]
> [Next steps button](bot-builder-howto-send-messages.md)
