---
title: Tutorial to answer questions with the Bot Framework SDK and QnA Maker
description: Learn how to add question-and-answer support to bots. See how to use QnA Maker and a knowledge base with a bot so that the bot can answer questions.
keywords: QnA Maker, question and answer, knowledge base
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: tutorial
ms.date: 09/14/2021
monikerRange: 'azure-bot-service-4.0'
---

# Tutorial: Use QnA Maker in your bot to answer questions

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

You can use the QnA Maker service to create a knowledge base to add question-and-answer support to your bot. When you create your knowledge base, you seed it with questions and answers.

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Create a QnA Maker service and knowledge base
> * Add knowledge base information to your configuration file
> * Update your bot to query the knowledge base
> * Republish your bot

If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

## Prerequisites

* The bot created in the [previous tutorial](bot-builder-tutorial-create-basic-bot.md). You'll add a question-and-answer feature to the bot.
* Some familiarity with [QnA Maker](/azure/cognitive-services/qnamaker/overview/overview) is helpful. You'll use the [QnA Maker portal](https://qnamaker.ai/) to create, train, and publish the knowledge base to use with the bot.
* Familiarity with [QnA bot creation](/azure/cognitive-services/qnamaker/tutorials/create-qna-bot) using Azure Bot Service.
* Installed [Maven](https://maven.apache.org/), for Java only.

You should also already have the [prerequisites for the previous tutorial](/azure/bot-service/bot-builder-tutorial-deploy-basic-bot#prerequisites).

## Create a QnA Maker service and knowledge base

You'll import an existing knowledge base definition from the QnA Maker sample in the [BotBuilder-Samples](https://github.com/Microsoft/BotBuilder-Samples) repo.

1. Clone or copy the samples repo to your computer.
1. Sign into to the [QnA Maker portal](https://qnamaker.ai/) with your Azure credentials.
1. Select **Create a knowledge base** in the QnA Maker portal.
    1. If necessary, create a QnA service. (You can use an existing QnA Maker service or create a new one for this tutorial.) For more detailed QnA Maker instructions, see [Create a QnA Maker service](/azure/cognitive-services/qnamaker/how-to/set-up-qnamaker-service-azure) and [Create, train, and publish your QnA Maker knowledge base](/azure/cognitive-services/qnamaker/quickstarts/create-publish-knowledge-base).
    1. Connect your QnA Maker service to your knowledge base.
    1. Name your knowledge base.
    1. To populate your knowledge base, use the [smartLightFAQ.tsv](https://github.com/microsoft/BotBuilder-Samples/blob/main/samples/csharp_dotnetcore/11.qnamaker/CognitiveModels/smartLightFAQ.tsv) file from the samples repo. If you have downloaded the samples, upload the file **smartLightFAQ.tsv** from your computer.
    1. Select **Create your kb** to create the knowledge base.
1. Select **Save and train**.
1. Select **PUBLISH** to publish your knowledge base.

Once your QnA Maker app is published, select **SETTINGS**, and scroll down to *Deployment details*. Copy the following values from the *Postman* HTTP example request.

```text
POST /knowledgebases/<knowledge-base-id>/generateAnswer
Host: <your-hostname>  // NOTE - this is a URL ending in /qnamaker.
Authorization: EndpointKey <qna-maker-resource-key>
```

The full URL string for your hostname will look like "https://_\<knowledge-base-name>_.azure.net/qnamaker".

These values will be used within your bot configuration file in the next step.

The knowledge base is now ready for your bot to use.

## Add knowledge base information to your bot

Use the following instructions connect your C#, JavaScript, Java, or Python bot to your knowledge base.

# [C#](#tab/csharp)

Add the following values to you **appsetting.json** file:

```json
{
  "MicrosoftAppId": "",
  "MicrosoftAppPassword": "",
  "ScmType": "None",

  "QnAKnowledgebaseId": "knowledge-base-id",
  "QnAAuthKey": "qna-maker-resource-key",
  "QnAEndpointHostName": "your-hostname" // This is a URL ending in /qnamaker
}
```

# [JavaScript](#tab/javascript)

Add the following values to your **.env** file:

```text
MicrosoftAppId=""
MicrosoftAppPassword=""
ScmType=None

QnAKnowledgebaseId="knowledge-base-id"
QnAAuthKey="qna-maker-resource-key"
QnAEndpointHostName="your-hostname" // This is a URL ending in /qnamaker
```

# [Java](#tab/java)

Add the following values to your **application.properties** file:

```java
MicrosoftAppId=
MicrosoftAppPassword=
QnAKnowledgebaseId="knowledge-base-id"
QnAEndpointKey="qna-maker-resource-key"
QnAEndpointHostName="your-hostname" // This is a URL ending in /qnamaker
server.port=3978
```

# [Python](#tab/python)

Add the following values to your **config.py** file:

```python
class DefaultConfig:
    """ Bot Configuration """
    PORT = 3978
    APP_ID = os.environ.get("MicrosoftAppId", "")
    APP_PASSWORD = os.environ.get("MicrosoftAppPassword", "")

    QNA_KNOWLEDGEBASE_ID = os.environ.get("QnAKnowledgebaseId", "")
    QNA_ENDPOINT_KEY = os.environ.get("QnAEndpointKey", "")
    QNA_ENDPOINT_HOST = os.environ.get("QnAEndpointHostName", "")
```

---

| Field | Value |
|:----|:----|
| QnAKnowledgebaseId | The knowledge base ID that the QnA Maker portal generated for you. |
| QnAAuthKey (QnAEndpointKey in Python)  | The endpoint key that the QnA Maker portal generated for you. |
| QnAEndpointHostName | The host URL that the QnA Maker portal generated. Use the complete URL, starting with `https://` and ending with `/qnamaker`. The full URL string will look like "https://_\<knowledge-base-name>_.azure.net/qnamaker". |

Now save your edits.

## Update your bot to query the knowledge base

Update your initialization code to load the service information for your knowledge base.

# [C#](#tab/csharp)

1. Add the **Microsoft.Bot.Builder.AI.QnA** NuGet package to your project.

    You can do this via the NuGet package manager or the command line:

    ```cmd
    dotnet add package Microsoft.Bot.Builder.AI.QnA
    ```

    For more information on NuGet, see the [NuGet documentation](/nuget/#pivot=start&panel=start-all).

1. In your **Startup.cs** file, add the following namespace reference.

    **Startup.cs**

    ```csharp
    using Microsoft.Bot.Builder.AI.QnA;
    ```

1. Modify the `ConfigureServices` method in **Startup.cs** to create a `QnAMakerEndpoint` object that connects to the knowledge base defined in the **appsettings.json** file.

    **Startup.cs**

    ```csharp
    // Create QnA Maker endpoint as a singleton
    services.AddSingleton(new QnAMakerEndpoint
    {
        KnowledgeBaseId = Configuration.GetValue<string>("QnAKnowledgebaseId"),
        EndpointKey = Configuration.GetValue<string>("QnAAuthKey"),
        Host = Configuration.GetValue<string>("QnAEndpointHostName")
    });
    ```

1. In your **EchoBot.cs** file, add the following namespace references.

    **Bots\\EchoBot.cs**

    ```csharp
    using System.Linq;
    using Microsoft.Bot.Builder.AI.QnA;
    ```

1. Add an `EchoBotQnA` property and add a constructor to initialize it.

    **EchoBot.cs**

    ```csharp
    public QnAMaker EchoBotQnA { get; private set; }

    public EchoBot(QnAMakerEndpoint endpoint)
    {
       // connects to QnA Maker endpoint for each turn
       EchoBotQnA = new QnAMaker(endpoint);
    }
    ```

1. Add an `AccessQnAMaker` method:

    **EchoBot.cs**

    ```csharp
    private async Task AccessQnAMaker(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
    {
       var results = await EchoBotQnA.GetAnswersAsync(turnContext);
       if (results.Any())
       {
          await turnContext.SendActivityAsync(MessageFactory.Text("QnA Maker Returned: " + results.First().Answer), cancellationToken);
       }
       else
       {
          await turnContext.SendActivityAsync(MessageFactory.Text("Sorry, could not find an answer in the knowledge base."), cancellationToken);
       }
    }
    ```

1. Update the `OnMessageActivityAsync` method to call the new `AccessQnAMaker` method.

    **EchoBot.cs**

    ```csharp
    protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
    {
        var replyText = $"Echo: {turnContext.Activity.Text}";
        await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);

        await AccessQnAMaker(turnContext, cancellationToken);
    }
    ```

# [JavaScript](#tab/javascript)

1. Open a terminal or command prompt to the root directory for your project.
1. Add the **botbuilder-ai** npm package to your project.

    ```shell
    npm i botbuilder-ai
    ```

1. In **index.js**, following the call to create the `BotFrameworkAdapter`, add the following code to read your **.env** file configuration information needed to generate the QnA Maker services.

    **index.js**

    ```javascript
    // Map knowledge base endpoint values from .env file into the required format for `QnAMaker`.
    const configuration = {
       knowledgeBaseId: process.env.QnAKnowledgebaseId,
       endpointKey: process.env.QnAAuthKey,
       host: process.env.QnAEndpointHostName
    };
    ```

1. Modify the call to create the bot to pass in the QnA services configuration information.

    **index.js**

    ```javascript
    // Create the main dialog.
    const myBot = new EchoBot(configuration, {});
    ```

1. In your **bot.js** file, import `QnAMaker from **botbuilder-ai**.

    **bot.js**

    ```javascript
    const { QnAMaker } = require('botbuilder-ai');
    ```

1. Modify the constructor to take the configuration parameters required to create a QnA Maker connector and throw an error if these parameters are not provided.

    **bot.js**

    ```javascript
    class EchoBot extends ActivityHandler {
        constructor(configuration, qnaOptions) {
            super();
            if (!configuration) throw new Error('[QnaMakerBot]: Missing parameter. configuration is required');

            // now create a QnAMaker connector.
            this.qnaMaker = new QnAMaker(configuration, qnaOptions);
    ```

1. Finally in **bot.js**,  update your `onMessage` function to query your knowledge bases for an answer. Pass each user input to your QnA Maker knowledge base, and return the first QnA Maker response back to the user.

    ```javascript
    this.onMessage(async (context, next) => {
        const replyText = `Echo: ${ context.activity.text }`;
        await context.sendActivity(MessageFactory.text(replyText, replyText));

        // send user input to QnA Maker.
        const qnaResults = await this.qnaMaker.getAnswers(context);

        // If an answer was received from QnA Maker, send the answer back to the user.
        if (qnaResults[0]) {
            await context.sendActivity(`QnA Maker Returned: ' ${ qnaResults[0].answer}`);
        }
        else {
            // If no answers were returned from QnA Maker, reply with help.
            await context.sendActivity('No QnA Maker response was returned.'
                + 'This example uses a QnA Maker Knowledge Base that focuses on smart light bulbs. '
                + `Ask the bot questions like "Why won't it turn on?" or "I need help."`);
        }
        await next();
    });
    ```

# [Java](#tab/java)

1. Add the **com.microsoft.bot.bot-ai-qna** package to your project.

    To do this add the package to your Maven **pom.xml** file:

    ```xml
    <dependency>
        <groupId>com.microsoft.bot</groupId>
        <artifactId>bot-ai-qna</artifactId>
        <version>4.13.0</version>
    </dependency>
    ```

1. In your **EchoBot.cs** file, add the following namespace references.

    **EchoBot.java**

    ```java
    import com.microsoft.bot.ai.qna.QnAMaker;
    import com.microsoft.bot.ai.qna.QnAMakerEndpoint;
    import com.microsoft.bot.ai.qna.QnAMakerOptions;
    ```

1. Add an `qnaMaker` member and add a constructor to initialize it.

    **EchoBot.java**

    ```java
    QnAMaker qnaMaker;

    public EchoBot(Configuration configuration) {
        QnAMakerEndpoint qnAMakerEndpoint = new QnAMakerEndpoint();
        qnAMakerEndpoint.setKnowledgeBaseId(configuration.getProperty("QnAKnowledgebaseId"));
        qnAMakerEndpoint.setEndpointKey(configuration.getProperty("QnAEndpointKey"));
        qnAMakerEndpoint.setHost(configuration.getProperty("QnAEndpointHostName"));

        qnaMaker = new QnAMaker(qnAMakerEndpoint, null);

    }
    ```
1. Update **Application.java** to provide an instance of the Configuration to the new EchoBot constructor.

    **Application.java**

    ```java
    @Bean
    public Bot getBot(Configuration configuration) {
        return new EchoBot(configuration);
    }    
    ```

1. Back in the **Echobot.java** file, add an `AccessQnAMaker` method:

    **EchoBot.java**

    ```java
    private CompletableFuture<Void> accessQnAMaker(TurnContext turnContext) {
        return qnaMaker.getAnswers(turnContext, null).thenCompose(results -> {
            if (results.length > 0) {
                return turnContext
                    .sendActivity(MessageFactory.text(String.format("QnA Maker Returned: %s" + results[0].getAnswer())))
                    .thenApply(result -> null);
            } else {
                return turnContext
                    .sendActivity(MessageFactory.text("Sorry, could not find an answer in the knowledge base."))
                    .thenApply(result -> null);
            }
        });
    }
    ```

1. Update the `onMessageActivity` method to call the new `accessQnAMaker` method.

    **EchoBot.java**

    ```csharp
    @Override
    protected CompletableFuture<Void> onMessageActivity(TurnContext turnContext) {
        return turnContext.sendActivity(MessageFactory.text("Echo: " + turnContext.getActivity().getText()))
            .thenCompose(sendResult -> {
                return accessQnAMaker(turnContext);
            });
    }    ```

# [Python](#tab/python)

1. Make sure you've installed the packages as described in the samples repository `README` file.
1. Add the **botbuilder-ai** reference to the **requirements.txt** file, as shown below.

    **requirements.txt**
    <!-- Removed version numbers -->
    ```text
    botbuilder-core
    botbuilder-ai
    flask
    ```

    Notice that the versions may vary.

1. In the **app.py** file, modify the bot instance creation as shown below.

    ```python
    # Create the bot
    BOT = EchoBot(CONFIG)
    ```

1. In the **bot.py** file import `QnAMaker` and `QnAMakerEndpoint` from **botbuilder.ai.qna**, then `Config` from **flask**, as shown below.

    **bot.py**

    ```python
    from flask import Config

    from botbuilder.ai.qna import QnAMaker, QnAMakerEndpoint
    from botbuilder.core import ActivityHandler, MessageFactory, TurnContext
    from botbuilder.schema import ChannelAccount
    ```

1. Next, in **bot.py_** add an `__init__` function to instantiate a `qna-maker` object. Using the configuration parameters provided in the **_config.py_** file.

    **bot.py**

    ```python
    def __init__(self, config: Config):
        self.qna_maker = QnAMaker(
            QnAMakerEndpoint(
                knowledge_base_id=config.QNA_KNOWLEDGEBASE_ID,
                endpoint_key=config.QNA_ENDPOINT_KEY,
                host=config.QNA_ENDPOINT_HOST,
        )
    )
   ```

1. Next, in **bot.py**, update `on_message_activity` to query your knowledge base for an answer. Pass each user input to your QnA Maker knowledge base, and return the first QnA Maker response to the user.

    **bot.py**

    ```python
    async def on_message_activity(self, turn_context: TurnContext):
        # The actual call to the QnA Maker service.
        response = await self.qna_maker.get_answers(turn_context)
        if response and len(response) > 0:
            await turn_context.send_activity(MessageFactory.text(response[0].answer))
        else:
            await turn_context.send_activity("No QnA Maker answers were found.")
    ```

1. Optionally, update the welcome message in `on_members_added_activity` for example:

    **bot.py**

    ```python
    await turn_context.send_activity("Hello and welcome to QnA!")
    ```

---

## Test your bot locally

At this point, your bot should be able to answer some questions. Run the bot locally and open it in the Emulator.

:::image type="content" source="./media/qna-test-bot.png" alt-text="Sample interaction with the bot and QnA Maker.":::

## Republish your bot

You can now republish your bot back to Azure. Zip your project folder and then run the command to deploy your bot to Azure. For details, see how to [Deploy your bot](../bot-builder-deploy-az-cli.md).

### Zip your project folder

[!INCLUDE [zip up code](../includes/deploy/snippet-zip-code.md)]

<!-- > [!IMPORTANT]
> Before creating a zip of your project files, make sure that you are _in_ the correct folder.
> - For C# bots, it is the folder that has the .csproj file.
> - For JS bots, it is the folder that has the app.js or index.js file.
> - For Python bots, it is the folder that has the app.py file.
>
> Select all the files and zip them up while in that folder, then run the command while still in that folder.
>
> If your root folder location is incorrect, the **bot will fail to run in the Azure portal**. -->

### Deploy your code to Azure

> [!TIP]
> If your session token expired, run `az login` again. If you are not using your default subscription, also reset your subscription.

[!INCLUDE [deploy code to Azure](../includes/deploy/snippet-deploy-code-to-az.md)]

<!--
## Clean up resources

In the first tutorial, we should tell them to use a new resource group, so that it is easy to clean up resources. We should also mention in this step in the first tutorial not to clean up resources if they are continuing with the sequence.
-->

If you're not going to continue to use this application, delete
the associated resources with the following steps:

1. In the Azure portal, open the resource group for your bot.
2. Select **Delete resource group** to delete the group and all the resources it contains.
3. Enter the _resource group name_ in the confirmation pane, then select **Delete**.

## Next steps

For information on how to add features to your bot, see the **Send and receive text message** article and the other articles in the how-to develop section.
> [!div class="nextstepaction"]
> [Send and receive text message](bot-builder-howto-send-messages.md)
