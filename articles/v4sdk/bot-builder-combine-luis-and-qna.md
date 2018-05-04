---
title: Using LUIS and QnA Maker | Microsoft Docs
description: Learn how to use LUIS and QnA maker in your bot.
author: DeniseMak
ms.author: v-demak
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 04/13/2018
monikerRange: 'azure-bot-service-4.0'
---

# Combining LUIS and QnA Maker in your bot

You can combine both LUIS and QnA Maker together in your bot. You can use LUIS as the fallback for QnA, QnA as the fallback for LUIS or use the Dispatch tool create a new LUIS app that dispatches messages to both.

## Prequisites

First, make sure you have the packages necessary for LUIS and QnA Maker.

# [C#](#tab/csref)

[Add a reference](https://docs.microsoft.com/en-us/nuget/tools/package-manager-ui) to v4 prerelease version of the following NuGet packages:

* `Microsoft.Bot.Builder.Integration.AspNet.Core`
* `Microsoft.Bot.Builder.Ai.QnA` (required for QnA Maker)
* `Microsoft.Bot.Builder.Ai.Luis` (required for LUIS)

# [JavaScript](#tab/jsref)

Either of these services can be added to your bot using the botbuilder-ai package. You can add this package to your project via npm:

* `npm install --save botbuilder@preview`
* `npm install --save botbuilder-ai@preview`

---

## Using QnA with LUIS as fallback

Start with the QnA bot described in the [QnA article]. Then modify the code to add LUIS middleware.

# [C#](#tab/csluis)

Update the `ConfigureServices` method in your `Startup.cs` file to add a `LuisRecognizerMiddleware` object bound to your LUIS model. The call to add the `LuisRecognizerMiddleware` should come after `middleware.Add(new QnAMakerMiddleware(qnaOptions));`, because you're processing the results from QnAMaker first.

**Startup.cs**
```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.Ai.LUIS;
using Microsoft.Bot.Builder.Ai.QnA;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBot<QnAMakerBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);

                var qnaOptions = new QnAMakerMiddlewareOptions
                {
                    // add subscription key and knowledge base id
                    SubscriptionKey = "YOUR-QNA-SUBSCRIPTION-KEY",
                    KnowledgeBaseId = "YOUR-QNA-KB-ID",
                    EndActivityRoutingOnAnswer = true
                };

                var middleware = options.Middleware;

                middleware.Add(new QnAMakerMiddleware(qnaOptions));


                middleware.Add(
                new LuisRecognizerMiddleware(
                    new LuisModel("YOUR-LUIS-APP-ID", "YOUR-SUBSCRIPTION-KEY", new System.Uri("https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/"))));

            });
```

# [JavaScript](#tab/jsluis)

> [!NOTE] 
> This section is a placeholder. The actual Node.Js content is coming soon. 

Require/import the the QnAMaker class and create an instance bound to your knowledge base:

```javascript
const { QnAMaker } = require('botbuilder-ai');

const qna = new QnAMaker({
    knowledgeBaseId: '<knowledge bases id>',
    subscriptionKey: '<your subscription>',
    top: 1
});
```

You can configure your bot to automatically call QnA Maker by simply adding it to your bot's middleware stack:

```javascript
// The QnA Middleware will run first
adapter.use({
    async onTurn(context, next) {
        // Checking if the user typed a message
        if(context.activity.type === "message"){
            //If the qna has an answer it will reply
            var handled = await qna.answer(context)
            if (!handled) {
                // If the qna does not have an answer it will move to the LUIS logic
                console.log('QnA answer not found, calling luis')
            }
        }
        await next();
    }
})
```

This will call QnA Maker in the middleware at every turn. The QnA maker will reply to the user if the message can be handled (the QnA Maker has an answer). If it is unabled to be handled, the rest of the bot's logic will run. For even more control over how and when QnA Maker is called, you can call qna.answer() directly from within your bot's logic instead of installing it as a piece of middleware:

```javascript
// Listen for incoming activity 
server.post('/api/messages', (req, res) => {
    // Route received activity to adapter for processing
    adapter.processActivity(req, res, async (context) => {
        if (context.activity.type === 'message') {
            var handled = await qna.answer(context)
                if (!handled) {
                    await context.sendActivity(`No QnA answer found. Checking intents from LUIS`);
                    // Add LUIS logic here
                }
        }
    });
});
```
To add LUIS to the fallback, require/import in the [LuisRecognizer](https://github.com/Microsoft/botbuilder-js/tree/master/doc/botbuilder-ai/classes/botbuilder_ai.luisrecognizer.md) class and create an instance for your LUIS model:

```javascript
const { LuisRecognizer } = require('botbuilder-ai');

const model = new LuisRecognizer({
     appId: '<models app id'>,
     subscriptionKey: '<your subscription>',
     serviceEndpoint: 'https://westus.api.cognitive.microsoft.com'
});
```

Add the model to the middleware stack:

```javascript
adapter.use(model);
```


---

> [!NOTE] 
> Use the app ID, subscription ID, and URL for your LUIS model. The location based URL will be "https://<region>.api.cognitive.microsoft...", where region is the region associated with the key you are using. Some examples of regions are `westus`, `westcentralus`, `eastus2`, and `southeastasia`.
>
>You can find your base URL by logging into the [LUIS site](https://www.luis.ai), going to the **Publish** tab, and looking at the **Endpoint** column under **Resources and Keys**. The base URL is the portion of the **Endpoint URL** before the subscription ID and other parameters.

Then update your bot logic to get LUIS results:

# [C#](#tab/csqnaluisbot)

Add logic to your bot code to get results from LUIS when QnAMaker didn't return an answer:

```cs
        public async Task OnTurn(ITurnContext context)
        {
            // At this point, the QnA Maker middleware has already been run. If the incoming
            // Activity was a message, the middleware called out to QnA Maker looking for 
            // an answer. If an answer was found, the Responded flag on the context will be set 
            // and we do nothing here. If the middleware did NOT find a match, then it's 
            // up to the bot to send something to the user.
            // In this case we send information about the LUIS intents. 
            switch (context.Activity.Type)
            {
                case ActivityTypes.Message:
                    if (context.Activity.Type == ActivityTypes.Message && context.Responded == false)
                    {
                        // add app logic when QnA Maker doesn't find an answer
                        await context.SendActivity("No good match found in the KB.");
                        var messageActivity = context.Activity.AsMessageActivity();

                        var luisResult = context.Services.Get<RecognizerResult>(LuisRecognizerMiddleware.LuisRecognizerResultKey);

                        if (luisResult != null)
                        {
                            (string key, double score) topItem = luisResult.GetTopScoringIntent();
                            await context.SendActivity($"The **top intent** was: **'{topItem.key}'**, with score **{topItem.score}**");

                            await context.SendActivity($"Intent scores:");
                            var intentsResult = new System.Collections.Generic.List<string>();
                            foreach (var intent in luisResult.Intents)
                            {
                                intentsResult.Add($"* '{intent.Key}', score {intent.Value}");
                            }
                            await context.SendActivity(string.Join("\n\n", intentsResult));
                        }
                    }

                    break;
                case ActivityTypes.ConversationUpdate:
                    foreach (var newMember in context.Activity.MembersAdded)
                    {
                        if (newMember.Id != context.Activity.Recipient.Id)
                        {
                            await context.SendActivity("Hello and welcome to the QnA Maker Sample bot, using LUIS as fallback.");
                        }
                    }
                    break;
            }
        }
```

# [JavaScript](#tab/jsqnaluisbot)

Then you can access the results from LUIS within your bot logic:
> [!NOTE] 
> This section is a placeholder. Updates to this Node.Js content are coming soon. 

```javascript

// Listen for incoming activity 
server.post('/api/messages', (req, res) => {
    // Route received activity to adapter for processing
    adapter.processActivity(req, res, async (context) => {
        if (context.activity.type === 'message' && context.responded == false) {
            // At this point, the QnA Maker middleware has already been run. If the incoming
            // activity was a message, the middleware called out to QnA Maker looking for 
            // an answer. If an answer was found, the responded flag on the context will be set 
            // and we do nothing here. If the middleware did NOT find a match, then it's 
            // up to the bot to send something to the user.
            // In this case we send information about the LUIS intents. 

            const results = model.get(context);
            const topIntent = LuisRecognizer.topIntent(results);
            switch (topIntent){
                case 'None':
                    await context.sendActivity("LUIS could not find an answer to this message")
                    break;
                case 'Turn off the light':
                    await context.sendActivity("you want to turn off the light");
                    break;
                case 'Turn on the light':
                    await context.sendActivity("you want to turn on the light");
                    break;
                default:
                    await context.sendActivity(`Top intent is ${topIntent}.`)
            }
        }
    });
});

```

Any intents recognized in the utterance will be returned as a map of intent names to scores and can be accessed from `results.intents`. A static `LuisRecognizer.topIntent()` method is provided to help simplify finding the top scoring intent for a result set. Here we created a variable holding the topIntent and check which response to reply with using a switch statement. 
Any entities recognized will be returned as a map of entity names to values and accessed via `results.entities`. Additional entity metadata can be returned by passing a `verbose=true` setting when creating the LuisReocgnizer. The added metadata can then be accessed via `results.entities.$instance`.

---

## Using LUIS with QnA Maker as fallback

# [C#](#tab/csqna)

Start with the code in the [LUIS how-to article]. Then when your app detects the 'None' intent, or any other intent that you want to use as the fallback case, query QnAMaker.



```csharp
    public class LuisQnAMakerBot : IBot
    {
        private readonly QnAMaker _qnaMaker;
        private QnAMakerOptions qnaOptions = new QnAMakerOptions
        {
            // add subscription key and knowledge base id
            SubscriptionKey = "6f24c2ab835645c38e2b28b7c24e08fa",
            // Help, Cancel, drive me.
            KnowledgeBaseId = "7afa2371-a656-4180-9ee1-f4eeed741b97" // Was: LUIS FAQ "5b7c43b5-d7df-424d-b0bb-b05e522ef65d",
        };

        public QnAMakerBot()
        {
            _qnaMaker = new QnAMaker(qnaOptions, null);
        }
        

        public async Task OnTurn(ITurnContext context)
        {
            switch (context.Activity.Type)
            {
                case ActivityTypes.Message:
                    if (context.Activity.Type == ActivityTypes.Message)
                    {
                        var luisResult = context.Services.Get<RecognizerResult>(LuisRecognizerMiddleware.LuisRecognizerResultKey);

                        if (luisResult != null)
                        {
                            (string key, double score) topItem = luisResult.GetTopScoringIntent();
                            // Replace "None" with the name of your QnA fallback intent, which matches requests that you want to send to QnA.
                            // If top intent matches this fallback intent, pass the result to QnA
                            if (String.Equals(topItem.key, "None"))
                            {
                                await context.SendActivity($"The **top intent** was: **'{topItem.key}'**. I'm searching QnA for an answer.");
                                
                                // pass to QnA if LUIS detects the None intent
                                var messageActivity = context.Activity.AsMessageActivity();
                                if (!string.IsNullOrEmpty(messageActivity.Text))
                                {
                                    var results = await _qnaMaker.GetAnswers(messageActivity.Text.Trim()).ConfigureAwait(false);
                                    if (results.Any())
                                    {

                                        await context.SendActivity(results.First().Answer);

                                    }
                                }
                            } else {
                                // Add logic for other intents
                                await context.SendActivity($"The **top intent** was: **'{topItem.key}'**, with score **{topItem.score}**");
                            }


                        }

        // ...
```


A [full sample of QnA Maker](https://github.com/Microsoft/botbuilder-dotnet/tree/master/samples/Microsoft.Bot.Samples.Ai.QnA) and it's source code can be found in the sample folder of our repo.

# [JavaScript](#tab/jsqna)

> [!NOTE] 
> This section is a placeholder. Updated Node.Js content is coming soon. 

Start with the code in the [LUIS how-to article]. Then when your app detects the 'None' intent, or any other intent that you want to use as the fallback case, query QnAMaker.


First require/import in the [QnAMaker](https://github.com/Microsoft/botbuilder-js/tree/master/doc/botbuilder-ai/classes/botbuilder_ai.qnamaker.md) class and create an instance bound to your knowledge base:

```js
const { QnAMaker } = require('botbuilder-ai');

const qna = new QnAMaker({
    knowledgeBaseId: '<knowledge bases id>',
    subscriptionKey: '<your subscription>',
    top: 1
});
```

You can configure your bot to automatically call QNA maker as your bot's fallback request processing logic by simply adding it to your bots middleware stack:

```js
adapter.use(qna);
```

This will only call QnA Maker if your bot receives the QnA fallback intent. For even more control over how and when QnA Maker is called, you can call `qna.answer()` directly from within your bots logic instead of installing it as a piece of middleware:

```js
// Listen for incoming requests 
server.post('/api/messages', (req, res) => {
    // Route received request to adapter for processing
    adapter.processActivity(req, res, async (context) => {
        if (context.activity.type === 'message') {
            const results = model.get(context);
            switch (LuisRecognizer.topIntent(results)) {
                // Use 'None' as the QnA fallback intent
                case 'None':
                    var handled = await qna.answer(context)
                    if (handled) {
                        await context.sendActivity(handled);
                    }
                    else {
                        await context.sendActivity(`No KB article found.`);
                    }
                case 'Weather':
                    const city = results.entities.city;
                    await replyWithWeather(context, city);
                default:
                    await replyWithHelp(context);
            }
        }
    });
});

```
Another way to customize the QnA Maker is with `qna.generateAnswer()`. This method allows you to choose how to respond with an answer as well as what to do when there is no suitable answer to the question.


```js
// Listen for incoming requests 
server.post('/api/messages', (req, res) => {
    // Route received request to adapter for processing
    adapter.processActivity(req, res, async (context) => {
        if (context.activity.type === 'message') {
            const results = model.get(context);
            switch (LuisRecognizer.topIntent(results)) {
                // Use 'None' as the QnA fallback intent
                case 'None':
                // FOR MORE CUSTOMIZATION
                    var results = await qna.generateAnswer(context.activity.text);
                        if (results && results.length > 0) {
                            await context.sendActivity(results[0].answer);
                        } else {
                            await context.sendActivity(`Couldn't find an answer from QnA.`);
                        }
                case 'Weather':
                    const city = results.entities.city;
                    await replyWithWeather(context, city);
                default:
                    await replyWithHelp(context);
            }
        }
    });
});

```
---

## Use Dispatch to merge LUIS apps and QnA

If you have multiple LUIS apps, or a combination of LUIS apps and QnA services, you can use the Dispatch tool to combine them into one LUIS app, using the pattern in [Using LUIS with QnA Maker as fallback](#using-luis-with-qna-maker-as-fallback).


For info on the Dispatch tool, see [Combine multiple conversation models using Dispatch](./bot-builder-tutorial-dispatch.md). 

