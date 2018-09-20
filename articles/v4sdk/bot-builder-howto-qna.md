---
title: Use QnA Maker to answer questions | Microsoft Docs
description: Learn how to use QnA maker in your bot.
keywords: question and answer, QnA, FAQs, middleware 
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 09/18/2018
monikerRange: 'azure-bot-service-4.0'
---

# Use QnA Maker to answer questions

To add simple question and answer support to your bot, you can use the [QnA Maker](https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/home) service.

One of the basic requirements in writing your own QnA Maker service is to seed it with questions and answers. In many cases, the questions and answers already exist in content like FAQs or other documentation. Other times you would like to customize your answers to questions in a more natural, conversational way.

## Create a QnA Maker service

First create an account and sign in at [QnA Maker](https://qnamaker.ai/). Then navigate to [Set up a QnA Maker service](https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/how-to/set-up-qnamaker-service-azure), followed by the article linked at the bottom, [Create and publish a knowledge base](https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/quickstarts/create-publish-knowledge-base). Follow those instructions to create your Azure QnA service, then come back here for how to use QnA Maker with your bot.

## Installing Packages

Before we get coding, make sure you have the packages necessary for QnA Maker.

# [C#](#tab/cs)

[Add a reference](https://docs.microsoft.com/en-us/nuget/tools/package-manager-ui) to v4 prerelease version of the following NuGet packages:

* `Microsoft.Bot.Builder.AI.QnA`

# [JavaScript](#tab/js)

Either of these services can be added to your bot using the botbuilder-ai package. You can add this package to your project via npm:

* `npm install --save botbuilder@preview`
* `npm install --save botbuilder-ai@preview`

---


## Using QnA Maker

A refernce to QnA Maker is first added. Then we can call it within our bot logic.

# [C#](#tab/cs)

While it's possible to both create and call your QnAMaker app on every turn, it's better coding practice to register your QnA service as a singleton and then pass them as parameters to your bot's constructor. We'll show that method here as it's slightly more complicated.

First, we create a QnA service singleton in **Startup.cs**. This grabs the information from the `appsettings.json` file, but those strings can be hard coded for testing. 

```csharp
    // Create and register a QnA service and knowledgebase
    services.AddSingleton(sp =>
    {
        var section = this.Configuration.GetSection("QnA");
        var hostPath = section["HostPath"];
        var endpointKey = section["EndpointKey"];
        var knowledgebaseId = section["KbId"];
        var qnaOptions = float.TryParse(section["ScoreThreshold"], out float scoreThreshold)
            ? new QnAMakerOptions { ScoreThreshold = scoreThreshold, Top = 1 } : null;

        return new QnAMaker(
            new QnAMakerEndpoint
            {
                EndpointKey = endpointKey,
                Host = hostPath,
                KnowledgeBaseId = knowledgebaseId,
            },
            qnaOptions);
    });
```

Next, we need to give your bot this QnAMaker instance. Open up **EchoBot.cs**, and at the top of the file add the following code. For reference the class heading and [state](bot-builder-howto-v4-state.md) items are also included, but we won't explain them here.

```csharp
public class EchoBot : IBot
{
    /// <summary>
    /// Gets the state accessor
    /// </summary>
    private IStatePropertyAccessor<EchoState> EchoStateAccessor { get; }

    /// <summary>
    /// Gets the Contoso Cafe QnA service and knowledgebase.
    /// </summary>
    private QnAMaker QnA { get; } = null;

    public EchoBot(ConversationState state, QnAMaker qna)
    {
        EchoStateAccessor = state.CreateProperty<EchoState>("EchoBot.EchoState");
        this.QnA = qna ?? throw new ArgumentNullException(nameof(qna));
    }
```

# [JavaScript](#tab/js)

First require/import in the [QnAMaker](https://github.com/Microsoft/botbuilder-js/tree/master/doc/botbuilder-ai/classes/botbuilder_ai.qnamaker.md) class:

```js
const { QnAMaker } = require('botbuilder-ai');
```

Create a `QnAMaker` by initializing it with a string based on the HTTP request for your QnA service. You can copy an example request for your service from the [QnA Maker portal](https://qnamaker.ai) under Settings > Deployment details.

Copy the example HTTP request, and get the knowledge base ID, subscription key, and host to use when you initialize the `QnAMaker`.

**Preview**
```js
const qna = new QnAMaker(
    {
        knowledgeBaseId: '<KNOWLEDGE-BASE-ID>',
        endpointKey: '<QNA-SUBSCRIPTION-KEY>',
        host: 'https://westus.api.cognitive.microsoft.com/qnamaker/v2.0'
    },
    {
        // set this to true to send answers from QnA Maker
        answerBeforeNext: true
    }
);
```

---

## Calling QnA Maker from your bot

# [C#](#tab/cs)

When your bot needs an answer from QnAMaker, call `GetAnswersAsync()` from your bot code to get the appropriate answer based on the current context.

```csharp
    var answers = await this.QnA.GetAnswersAsync(context);

    if (answers is null)
    {
        // Output trace information to the Emulator.
        // This does not generate a response to the user.
        await context.TraceActivityAsync("Call to the QnA Maker service failed.", System.Threading.CancellationToken.None);
    }
    else if (answers.Any())
    {
        // If the service produced one or more answers, send the first one.
        await context.SendActivityAsync(answers[0].Answer);
    }

    /// ...
```

See the [QnA Maker sample](https://aka.ms/qna-cs-bot-sample) for a sample bot.

# [JavaScript](#tab/js)

For control over how and when QnA Maker is called, you can call `qna.generateAnswer()`. This method allows you to get more detail on the answers from QnA Maker if so desired.

```js
// Listen for incoming activity 
server.post('/api/messages', (req, res) => {
    // Route received activity to adapter for processing
    adapter.processActivity(req, res, async (context) => {
        if (context.activity.type === 'message') {
            // Get all the answers QnA Maker finds
            var results = await qna.generateAnswer(context.activity.text);
            
            if (results && results.length > 0) {
                await context.sendActivity(results[0].answer);
            } else {
                await context.sendActivity(`I don't know.`);
            }    
        }
    });
});
```

---

Ask your bot questions to see the replies from your QnA Maker service. For more information on testing and publishing your QnA service, see the QnA Maker article on [testing a knowledge base](https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/how-to/test-knowledge-base).

## Additional resources

For a sample bot using QnA Maker, see the code for [[C#](https://aka.ms/cs-qna)] or [[JavaScript](https://aka.ms/js-qna-sample)].

## Next steps

QnA Maker can be combined with other Cognitive Services, to make your bot even more powerful. The Dispatch tool provides a way to combine QnA with Language Understanding (LUIS) in your bot.

> [!div class="nextstepaction"]
> [Combine LUIS and QnA services using the Dispatch tool](./bot-builder-tutorial-dispatch.md)
