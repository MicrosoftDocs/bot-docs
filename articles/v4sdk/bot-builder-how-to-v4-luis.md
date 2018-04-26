---
title: Using LUIS for Language Understanding | Microsoft Docs
description: Learn how to use LUIS for natural language understanding with the Bot Builder SDK.
author: ivorb
ms.author: v-demak
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 2/16/17
monikerRange: 'azure-bot-service-4.0'
---

# Using LUIS for Language Understanding

The ability to understand what your user means conversationally and contextually can be a difficult task, but can give your bot a more natural conversation feel. Language Understanding, called LUIS, enables you to do just that so that your bot can recognize the intent of user messages, allow for more natural language from your user, and better direct the conversation flow. If you need more background on how LUIS integrates with a bot, see [language understanding for bots](./bot-builder-concept-LUIS.md). 

This topic walks you through setting up a simple bot that uses LUIS to recognize a few different intents and respond appropriately.

## Installing Packages

First, make sure you have the packages necessary for LUIS.

# [C#](#tab/csref)

[Add a reference](https://docs.microsoft.com/en-us/nuget/tools/package-manager-ui) to v4 prerelease version of the following NuGet packages:

* `Microsoft.Bot.Builder.Integration.AspNet.Core`
* `Microsoft.Bot.Builder.Luis`

# [JavaScript](#tab/jsref)

You can add reference to botbuilder and botbuilder-ai package in your project via npm:

* `npm install --save botbuilder@preview`
* `npm install --save botbuilder-ai@preview`

---


## Set up the LUIS service

First, set up a _LUIS app_, which is a service you create at [www.luis.ai](https://www.luis.ai). That LUIS app can be trained for certain intents it should be able to recognize. Details on how to create your LUIS app can be found on the [LUIS site](https://www.luis.ai).

For this example, you'll just use a demo LUIS app that can recognize Help, Cancel, and Weather intents; the app ID is already in the sample code. You will need to have a Cognitive Services key that you can get by logging in to [www.luis.ai](https://www.luis.ai) and copying the key from **User settings** > **Authoring Key**.

Configure your bot to call your model for every message received from a user by simply adding it to your bot's middleware stack. The middleware stores the recognition results on the context object, and can then be accessed by your bot logic.

# [C#](#tab/csharp)
Update the `ConfigureServices` method in your `Startup.cs` file to add a `LuisRecognizerMiddleware` object bound to your LUIS model. 

<!--Modify the code for the ConfigureServices method in `Startup.cs` in the Microsoft.Bot.Samples.EchoBot-AspNetCore sample.-->

**Startup.cs**
```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Builder.LUIS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton(_ => Configuration);
    services.AddBot<AiBot>(options =>
    {
        options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);

        options.Middleware.Add(
            new LuisRecognizerMiddleware(
                // replace <subscriptionKey> with your Authoring Key
                // your key is at https://www.luis.ai under User settings > Authoring Key
                new LuisModel("eb0bf5e0-b468-421b-9375-fdfb644c512e", // app ID
                    "<subscriptionKey>", 
                    new Uri("https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/"))));
    });

    // ...
}
```

# [JavaScript](#tab/jsmiddleware)

First require/import in the [LuisRecognizer](https://github.com/Microsoft/botbuilder-js/tree/master/doc/botbuilder-ai/classes/botbuilder_ai.luisrecognizer.md) class and create an instance for your LUIS model:

```javascript
const { LuisRecognizer } = require('botbuilder-ai');

const model = new LuisRecognizer({
     appId: 'eb0bf5e0-b468-421b-9375-fdfb644c512e', // app ID
     subscriptionKey: '<your subscription>',
     // replace subscriptionKey with your Authoring Key
     // your key is at https://www.luis.ai under User settings > Authoring Key
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

LUIS language understanding is now plugged into your bot. Next, let's look at how to get the intent from the LUIS model stored on the context object.


## Get the intent from the turn context

The results from LUIS are then accessible within your bot logic:

# [C#](#tab/csluisbot)

Replace code in `OnTurn` with the following:

```cs
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.LUIS;
using Microsoft.Bot.Schema;
using System.Threading.Tasks;

namespace Microsoft.Bot.Samples
{
    public class AiBot : IBot
    {
        public async Task OnTurn(IBotContext context)
        {
            if (context.Request.Type is ActivityTypes.Message)
            {
                // add app logic when LUIS doesn't return a result
                var result = context.Get<RecognizerResult>
                    (LuisRecognizerMiddleware.LuisRecognizerResultKey);
                var topIntent = result?.GetTopScoringIntent();
                switch ((topIntent != null) ? topIntent.Value.key : null)
                {
                    case null:
                        // Add app logic when there is no result.
                        break;
                    case "None":
                        await context.SendActivity("Sorry, I don't understand.");
                        break;
                    case "Help":
                        await context.SendActivity("<here's some help>");
                        break;
                    case "Cancel":
                        // Cancel the process.
                        await context.SendActivity("<cancelling the process>");
                        break;
                    default:
                        // Add app logic for the recognition results.
                        await context.SendActivity($"Intent: {topIntent.Value.key} ({topIntent.Value.score}).");
                        break;
                }
            }
        }
    }
}
```

# [JavaScript](#tab/jsluis)

```javascript
// Listen for incoming activity 
server.post('/api/messages', (req, res) => {
    // Route received activity to adapter for processing
    adapter.processActivity(req, res, async (context) => {
        if (context.activity.type === 'message') {
            const results = model.get(context);
            const topIntent = LuisRecognizer.topIntent(results);
            switch (topIntent) {
                case 'None':
                    //Add app logic when there is no result
                    await context.sendActivity("<null case>")
                    break;
                case 'Cancel':
                    await context.sendActivity("<cancelling the process>")
                    break;
                case 'Help':
                    await context.sendActivity("<here's some help>");
                    break;
                case 'Weather':
                    await context.sendActivity("Handle the weather report");
                    break;
                default:
                    // Add app logic for recognition results.
                    await context.sendActivity("<defaults>");
            }
        }
    });
});
```

Any intents recognized in the utterance will be returned as a map of intent names to scores and can be accessed from `results.intents`. A static `LuisRecognizer.topIntent()` method is provided to help simplify finding the top scoring intent for a result set.
Any entities recognized will be returned as a map of entity names to values and accessed via `results.entities`. Additional entity metadata can be returned by passing a `verbose=true` setting when creating the LuisRecognizer. The added metadata can then be accessed via `results.entities.$instance`.

---


## Using intent with conversation state
<!-- TBD, complete example -->
A simple way to track the topic of a conversation is to save it in conversation state. You can use the intent from LUIS to help you set conversation state data, such as whether a conversation topic has been started.

# [C#](#tab/csFlag)

```csharp

public class ConversationStateInfo 
{
    public bool WeatherTopicStarted  { get; set; }
    public bool HelpTopicStarted  { get; set; }
    public bool CancelTopicStarted  { get; set; }
}


public async Task OnTurn(IBotContext context)
        {
            if (context.Request.Type is ActivityTypes.Message)
            {
                var conversationState = context.GetConversationState<ConversationStateInfo>() ?? new        ConversationStateInfo();

                var result = context.Services.Get<RecognizerResult>(LuisRecognizerMiddleware.LuisRecognizerResultKey);
                var topIntent = result?.GetTopScoringIntent();
                switch ((topIntent != null) ? topIntent.Value.key : null)
                {
                    case null:
                        // Add app logic when there is no result.
                        break;
                    case "Weather":
                        conversationState.WeatherTopicStarted = true;
                        await context.SendActivity($"The weather is {this.weatherReport}.");
                        break;
                    case "Help":
                        conversationState.HelpTopicStarted = true;
                        await context.SendActivity("<here's some help>");
                        break;
                    case "Cancel":
                        // Cancel the process.
                        conversationState.CancelTopicStarted = true;
                        await context.SendActivity("<cancelling the process>");                         
                        break;
                    default:
                        // Add app logic for the recognition results.
                        await context.SendActivity($"Intent: {topIntent.Value.key} ({topIntent.Value.score}).");
                        break;
                }

                // do some other logic based on the topic flags in conversation state 
            }
        }

// ...
}
```

# [JavaScript](#tab/jsFlag)

```javascript
// Add conversation state middleware
const conversationState = new ConversationState(new MemoryStorage());
adapter.use(conversationState);


// Listen for incoming activities 
server.post('/api/messages', (req, res) => {
    // Route received activity to adapter for processing
    adapter.processActivity(req, res, async(context) => {
        if (context.activity.type === 'message') {
            const results = model.get(context);
            const topIntent = LuisRecognizer.topIntent(results);
            switch (topIntent) {
                case 'None':
                    //Add app logic when there is no result
                    await context.sendActivity("<null case>")
                    break;
                case 'Cancel':
                    conversationState.cancelTopicStarted = true;
                    await context.sendActivity("<cancelling the process>")
                    break;
                case 'Help':
                    conversationState.helpTopicStarted = true;
                    await context.sendActivity("<here's some help>");
                    break;
                case 'Weather':
                    conversationState.weatherTopicStarted = true;
                    await context.sendActivity("Handle the weather report");
                    break;
                default:
                    // Add app logic for recognition results.
                    await context.sendActivity("<defaults>");
            }

            // do some other logic based on the topic flags in conversation state 

        }
    });

    // ...
});
```

---
## Using LUIS with dialogs

<!-- 
# [C#](#tab/csludialog)

```csharp

// C# TBD

// ...
}
```

# [JavaScript](#tab/jsludialog)
-->

First, create a LUIS app and add it to your bot using `adapter.use`:

```javascript
const { BotFrameworkAdapter, ConversationState, MemoryStorage, TurnContext } = require('botbuilder');
const { LuisRecognizer, QnAMaker } = require('botbuilder-ai');
const { DialogSet } = require('botbuilder-dialogs');
const restify = require('restify');

// Create server
let server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log(`${server.name} listening to ${server.url}`);
});

// Create adapter
const adapter = new BotFrameworkAdapter({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});

// Create LuisRecognizer 
// The LUIS application is public, meaning you can use your own subscription key to test the applications.

const luisRecognizer = new LuisRecognizer({
    appId: '0b18ab4f-5c3d-4724-8b0b-191015b48ea9',
    subscriptionKey: process.env.LUIS_SUBSCRIPTION_KEY,
    serviceEndpoint: 'https://westus.api.cognitive.microsoft.com/',
    verbose: true
});

// Add the recognizer to your bot
adapter.use(luisRecognizer);

// create conversation state
const conversationState = new ConversationState(new MemoryStorage());
adapter.use(conversationState);

// register some dialogs for usage with the LUIS apps that are being dispatched to
const dialogs = new DialogSet();
```

Next, add dialogs:

```javascript

dialogs.add('HomeAutomation.TurnOn', [
    async (dialogContext) => {
        const state = conversationState.get(dialogContext.context);
        state.homeAutomationTurnOn = state.homeAutomationTurnOn ? state.homeAutomationTurnOn + 1 : 1;
        await dialogContext.context.sendActivity(`${state.homeAutomationTurnOn}: You reached the "HomeAutomation.TurnOn" dialog.`);

        await dialogContext.end();
    }
]);

dialogs.add('Weather.GetForecast', [
    async (dialogContext) => {
        const state = conversationState.get(dialogContext.context);
        state.weatherGetForecast = state.weatherGetForecast ? state.weatherGetForecast + 1 : 1;
        await dialogContext.context.sendActivity(`${state.weatherGetForecast}: You reached the "Weather.GetForecast" dialog.`);

        await dialogContext.end();
    }
]);

dialogs.add('None', [
    async (dialogContext) => {
        const state = conversationState.get(dialogContext.context);
        
        await dialogContext.context.sendActivity(`${state.None}: You reached the "None" dialog.`);

        await dialogContext.end();
    }
]);
```

In your bot, invoke each dialog based on the recognized intent:
```javascript
// Listen for incoming Activities
server.post('/api/messages', (req, res) => {
    adapter.processActivity(req, res, async (context) => {
        if (context.activity.type === 'message') {
            const state = conversationState.get(context);
            const dc = dialogs.createContext(context, state);

            // Retrieve the LUIS results from our dispatcher LUIS application
            const luisResults = luisRecognizer.get(context);

            // Extract the top intent from LUIS and use it to select which dialog to start
            const topIntent = LuisRecognizer.topIntent(luisResults);

            await dc.continue();

            switch (topIntent) {
                case 'l_homeautomation':
                    
                    await dc.begin("HomeAutomation.TurnOn");
                    break;
                case 'l_weather':
                    
                    await dc.begin("Weather.GetForecast");
                    break;
                case 'none':
                    await dc.begin("None");
                    break;
            }
        }
    });
});
```
<!--

---

-->
## Additional resources

If you want to write directly to storage, instead of using the state manager, see [How to write directly to storage](./bot-builder-how-to-v4-storage.md).

For more background on storage, see [Storage in the Bot Builder SDK](./bot-builder-storage-concept.md)

<!-- Links -->
For coding and testing storage locally, try the [Azure Storage Emulator](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator).

## Next steps

LUIS can be combined with other Cognitive Services, to make your bot even more powerful. For an example of this, check out [Combining LUIS and QnA Maker](bot-builder-combine-luis-and-qna.md).


