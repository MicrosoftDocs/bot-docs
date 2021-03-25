---
title: Azure Bot Service tutorial to have a bot answer questions - Bot Service
description: Learn how to add question-and-answer support to bots. See how to use QnA Maker and a knowledge base with a bot so that the bot can answer questions.
keywords: QnA Maker, question and answer, knowledge base
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: tutorial
ms.service: bot-service
ms.date: 03/23/2020
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

You should also already have the [prerequisites for the previous tutorial](/azure/bot-service/bot-builder-tutorial-deploy-basic-bot#prerequisites).

## Create a QnA Maker service and knowledge base

You'll import an existing knowledge base definition from the QnA Maker sample in the [BotBuilder-Samples](https://github.com/Microsoft/BotBuilder-Samples) repo.

1. Clone or copy the samples repo to your computer.
1. Sign into to the [QnA Maker portal](https://qnamaker.ai/) with your Azure credentials.
1. Select **Create a knowledge base** in the QnA Maker portal. You'll see a form with the following:
   1. If necessary, create a QnA service. (You can use an existing QnA Maker service or create a new one for this tutorial.) For more detailed QnA Maker instructions, see [Create a QnA Maker service](https://docs.microsoft.com/azure/cognitive-services/qnamaker/how-to/set-up-qnamaker-service-azure) and [Create, train, and publish your QnA Maker knowledge base](https://docs.microsoft.com/azure/cognitive-services/qnamaker/quickstarts/create-publish-knowledge-base).
   1. Connect your QnA service to your knowledge base.
   1. Name your knowledge base.
   1. To populate your knowledge base, use the **smartLightFAQ.tsv** file from the samples repo. If you have downloaded the samples, upload the file **smartLightFAQ.tsv** from your computer.
   1. Select **Create your kb** to create the knowledge base.
1. Select the **Save and train** button.
1. Select **PUBLISH** to publish your knowledge base.

Once your QnA Maker app is published, select **SETTINGS**, and scroll down to *Deployment details*. Copy the following values from the *Postman* HTTP example request.

```text
POST /knowledgebases/<knowledge-base-id>/generateAnswer
Host: <your-hostname>  // NOTE - this is a URL ending in /qnamaker.
Authorization: EndpointKey <qna-maker-resource-key>
```

The full URL string for your hostname will look like "https://<your-hostname>.azure.net/qnamaker".

These values will be used within your **appsettings.json** or **.env** file in the next step.

The knowledge base is now ready for your bot to use.

## Add knowledge base information to your bot

Beginning with bot framework v4.3 Azure no longer provides a .bot file as part of your downloaded bot source code. Use the following instructions connect your CSharp, JavaScript or Python bot to your knowledge base.

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
| QnAEndpointHostName | The host URL that the QnA Maker portal generated. Use the complete URL, starting with `https://` and ending with `/qnamaker`. The full URL string will look like `https://< >.azure.net/qnamaker`. |

Now save your edits.

## Update your bot to query the knowledge base

Update your initialization code to load the service information for your knowledge base.

# [C#](#tab/csharp)

1. Add the **Microsoft.Bot.Builder.AI.QnA** NuGet package to your project.

   You can do this via the NuGet package manager or the command line:

   ```cmd
   dotnet add package Microsoft.Bot.Builder.AI.QnA
   ```

   For more information on NuGet, see the [NuGet documentation](https://docs.microsoft.com/nuget/#pivot=start&panel=start-all).

1. In your **Startup.cs** file, add the following namespace reference.

   ```csharp
   using Microsoft.Bot.Builder.AI.QnA;
   ```

1. Modify the `ConfigureServices` method in **Startup.cs** to create a `QnAMakerEndpoint` object that connects to the knowledge base defined in the **appsettings.json** file.

   ```csharp
   // Create QnAMaker endpoint as a singleton
   services.AddSingleton(new QnAMakerEndpoint
   {
      KnowledgeBaseId = Configuration.GetValue<string>($"QnAKnowledgebaseId"),
      EndpointKey = Configuration.GetValue<string>($"QnAAuthKey"),
      Host = Configuration.GetValue<string>($"QnAEndpointHostName")
    });

   ```

1. In your **EchoBot.cs** file, add the following namespace references.

   ```csharp
   using System.Linq;
   using Microsoft.Bot.Builder.AI.QnA;
   ```

1. Add an `EchoBotQnA` connector and initialize it in the bot's constructor in **EchoBot.cs**.

   ```csharp
   public QnAMaker EchoBotQnA { get; private set; }
   public EchoBot(QnAMakerEndpoint endpoint)
   {
      // connects to QnA Maker endpoint for each turn
      EchoBotQnA = new QnAMaker(endpoint);
   }
   ```

1. Next, create a new method named `AccessQnAMaker` after the `OnMembersAddedAsync` method, as follows:

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
         await turnContext.SendActivityAsync(MessageFactory.Text("Sorry, could not find an answer in the Q and A system."), cancellationToken);
      }
   }
   ```

1. Call your new method `AccessQnAMaker` from `OnMessageActivityAsync` as follows:

   ```csharp
   protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
   {
      await turnContext.SendActivityAsync(MessageFactory.Text($"Echo: {turnContext.Activity.Text}"), cancellationToken);

      await AccessQnAMaker(turnContext, cancellationToken);
   }
   ```

# [JavaScript](#tab/javascript)

1. Open a terminal or command prompt to the root directory for your project.
1. Add the **botbuilder-ai** npm package to your project.

   ```shell
   npm i botbuilder-ai
   ```

1. In **index.js**, following the _// Create Adapter_ section, add the following code to read your **.env** file's configuration information. This is needed to generate the QnA Maker services.

   ```javascript
   // Map knowledge base endpoint values from .env file into the required format for `QnAMaker`.
   const configuration = {
      knowledgeBaseId: process.env.QnAKnowledgebaseId,
      endpointKey: process.env.QnAAuthKey,
      host: process.env.QnAEndpointHostName
   };

   ```

1. Next in **index.js**, update the bot constructor to pass in the QnA services configuration information.

   ```javascript
   // Create the main dialog.
   const myBot = new MyBot(configuration, {});
   ```

1. In your **bot.js** file, add this require for QnAMaker

   ```javascript
   const { QnAMaker } = require('botbuilder-ai');
   ```

1. Next in **bot.js**, modify the constructor to handle the configuration parameters required to create a QnAMaker connector, and throw an error if these parameters are not provided.

   ```javascript
      class MyBot extends ActivityHandler {
         constructor(configuration, qnaOptions) {
            super();
            if (!configuration) throw new Error('[QnaMakerBot]: Missing parameter. configuration is required');
            // now create a qnaMaker connector.
            this.qnaMaker = new QnAMaker(configuration, qnaOptions);
   ```

1. Finally in **bot.js**,  update your `onMessage` function to query your knowledge bases for an answer. Pass each user input to your QnA Maker knowledge base, and return the first QnA Maker response back to the user.

    ```javascript
    this.onMessage(async (context, next) => {
        // send user input to QnA Maker.
        const qnaResults = await this.qnaMaker.getAnswers(context);

        // If an answer was received from QnA Maker, send the answer back to the user.
        if (qnaResults[0]) {
            await context.sendActivity(`QnAMaker returned response: ' ${ qnaResults[0].answer}`);
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

# [Python](#tab/python)

1. Make sure you've installed the packages as described in the samples repository README file.
1. Add the **botbuilder-ai** reference to the **requirements.txt** file, as shown below.

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

   ```python
   from flask import Config

   from botbuilder.ai.qna import QnAMaker, QnAMakerEndpoint
   from botbuilder.core import ActivityHandler, MessageFactory, TurnContext
   from botbuilder.schema import ChannelAccount
   ```

1. Next, in **bot.py_** add an `__init__` function to instantiate a `qna-maker` object. Using the configuration parameters provided in the **_config.py_** file.

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

   ```python
   await turn_context.send_activity("Hello and welcome to QnA!")
   ```

---

### Test the bot locally

At this point your bot should be able to answer some questions. Run the bot locally and open it in the Emulator.

![test qna sample](./media/qna-test-bot.png)

## Republish your bot

You can now republish your bot back to Azure. You need to zip your project folder and then run the command to deploy your bot to Azure. For details please read the [Deploy your bot](../bot-builder-deploy-az-cli.md) article.

### Zip your project folder

[!INCLUDE [zip up code](~/includes/deploy/snippet-zip-code.md)]

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

[!INCLUDE [deploy code to Azure](~/includes/deploy/snippet-deploy-code-to-az.md)]

<!-- # [C#](#tab/csharp)
```cmd
az webapp deployment source config-zip --resource-group "resource-group-name" --name "bot-name-in-azure" --src "c:\bot\mybot.zip"
```

# [JavaScript](#tab/javascript)

[!INCLUDE [publish snippet](~/includes/deploy/snippet-publish-js.md)]

# [Python](#tab/python)

az webapp deployment source config-zip --resource-group "resource_group_name" --name "unique_bot_name" --src "zi

### Test the published bot

After you publish the bot, give Azure a minute or two to update and start the bot.

Use the Emulator to test the production endpoint for your bot, or use the Azure portal to test the bot in Web Chat.
In either case, you should see the same behavior as you did when you tested it locally.

## Clean up resources

<!-- In the first tutorial, we should tell them to use a new resource group, so that it is easy to clean up resources. We should also mention in this step in the first tutorial not to clean up resources if they are continuing with the sequence. -->

If you're not going to continue to use this application, delete
the associated resources with the following steps:

1. In the Azure portal, open the resource group for your bot.
2. Select **Delete resource group** to delete the group and all the resources it contains.
3. Enter the _resource group name_ in the confirmation pane, then select **Delete**.

## Next steps

For information on how to add features to your bot, see the **Send and receive text message** article and the other articles in the how-to develop section.
> [!div class="nextstepaction"]
> [Send and receive text message](bot-builder-howto-send-messages.md)
