---
title: Use multiple LUIS and QnA models | Microsoft Docs
description: Learn how to use LUIS and QnA maker in your bot.
keywords: Luis, QnA, Dispatch tool, multiple services, route intents
author: DeniseMak
ms.author: v-demak
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 11/26/2018
monikerRange: 'azure-bot-service-4.0'
---

# Use multiple LUIS and QnA models

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

In this tutorial, we demonstrate how to use the Dispatch service to route utterances when there are multiple LUIS models and QnA maker services for different scenarios supported by a bot. In this case, we configure dispatch with multiple LUIS models for conversations around home automation and weather information, plus QnA maker service to answer questions based on a FAQ text file as input. This sample combines the following services.

| Name | Description |
|------|------|
| HomeAutomation | A LUIS app that recognizes a home automation intent with associated entity data.|
| Weather | A LUIS app that recognizes the `Weather.GetForecast` and `Weather.GetCondition` intents with location data.|
| FAQ  | A QnA Maker KB that provides answers to simple questions about the bot. |

## Prerequisites

- The code in this article is based on the **NLP with Dispatch** sample. You'll need a copy of the sample in either [C#](https://aka.ms/dispatch-sample-cs) or [JS](https://aka.ms/dispatch-sample-js).
- Knowledge of [bot basics](bot-builder-basics.md), [natural language processing](bot-builder-howto-v4-luis.md), [QnA Maker](bot-builder-howto-qna.md), and [.bot](bot-file-basics.md) file is required.
- The [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/blob/master/README.md#download) for testing.

## Create the services and test the bot

Follow the **README** instructions for [C#](https://github.com/Microsoft/BotBuilder-Samples/blob/master/samples/csharp_dotnetcore/14.nlp-with-dispatch/README.md) or [JS](https://github.com/Microsoft/BotBuilder-Samples/blob/master/samples/javascript_nodejs/14.nlp-with-dispatch/README.md) to build and run the sample using the emulator. 

For your reference, here are some of the questions and commands that are covered by the services we've included:

* QnA Maker
  * `hi`, `good morning`
  * `what are you`, `what do you do`
* LUIS (home automation)
  * `turn on bedroom light`
  * `turn off bedroom light`
  * `make some coffee`
* LUIS (weather)
  * `whats the weather in redmond washington`
  * `what's the forecast for london`
  * `show me the forecast for nebraska`

### Connecting to the services from your bot

To connect to the Dispatch, LUIS, and QnA Maker services, your bot pulls information from the **.bot** file.

# [C#](#tab/csharp)

In **Startup.cs**, `ConfigureServices` reads in the configuration file, and `InitBotServices` uses that information to initialize the services. Each time it's created, the bot is initialized with the registered `BotServices` object. Here are the relevant parts of these two methods.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    //...
    var botConfig = BotConfiguration.Load(botFilePath ?? @".\nlp-with-dispatch.bot", secretKey);
    services.AddSingleton(sp => botConfig
        ?? throw new InvalidOperationException($"The .bot config file could not be loaded. ({botConfig})"));

    // ...
    
    var connectedServices = InitBotServices(botConfig);
    services.AddSingleton(sp => connectedServices);
    
    services.AddBot<NlpDispatchBot>(options =>
    {
          
          // The Memory Storage used here is for local bot debugging only. 
          // When the bot is restarted, everything stored in memory will be gone.

          Storage dataStore = new MemoryStorage();

          // ...

          // Create Conversation State object.
          // The Conversation State object is where we persist anything at the conversation-scope.

          var conversationState = new ConversationState(dataStore);
          options.State.Add(conversationState);
     });
}

```
The following code initializes the bot's references to external services. For example, LUIS and QnaMaker services are created here. These external services are configured using the `BotConfiguration` class (based on the contents of your ".bot" file.

```csharp
private static BotServices InitBotServices(BotConfiguration config)
{
    var qnaServices = new Dictionary<string, QnAMaker>();
    var luisServices = new Dictionary<string, LuisRecognizer>();

    foreach (var service in config.Services)
    {
        switch (service.Type)
        {
            case ServiceTypes.Luis:
                {
                    // ...
                    var app = new LuisApplication(luis.AppId, luis.AuthoringKey, luis.GetEndpoint());
                    var recognizer = new LuisRecognizer(app);
                    luisServices.Add(luis.Name, recognizer);
                    break;
                }

            case ServiceTypes.Dispatch:
                // ...
                var dispatchApp = new LuisApplication(dispatch.AppId, dispatch.AuthoringKey, dispatch.GetEndpoint());

                // Since the Dispatch tool generates a LUIS model, we use the LuisRecognizer to resolve the
                // dispatching of the incoming utterance.
                var dispatchARecognizer = new LuisRecognizer(dispatchApp);
                luisServices.Add(dispatch.Name, dispatchARecognizer);
                break;

            case ServiceTypes.QnA:
                {
                    // ...
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

    return new BotServices(qnaServices, luisServices);
}
```

<!--
# [JavaScript](#tab/javascript)

```javascript
```
-->

---

### Calling the services from your bot

The bot logic checks the user input against the combined Dispatch model.

# [C#](#tab/csharp)

In the **NlpDispatchBot.cs** file, the bot's constructor gets the `BotServices` object that we registered at startup.

```csharp
private readonly BotServices _services;

public NlpDispatchBot(BotServices services)
{
    _services = services ?? throw new System.ArgumentNullException(nameof(services));

    //...
}
```

In the bot's `OnTurnAsync` method, we check incoming messages from the user against the Dispatch model.

```csharp
// Get the intent recognition result
var recognizerResult = await _services.LuisServices[DispatchKey].RecognizeAsync(context, cancellationToken);
var topIntent = recognizerResult?.GetTopScoringIntent();

if (topIntent == null)
{
    await context.SendActivityAsync("Unable to get the top intent.");
}
else
{
    await DispatchToTopIntentAsync(context, topIntent, cancellationToken);
}
```

<!--
# [JavaScript](#tab/javascript)

```javascript
```
-->

---

### Working with the recognition results

# [C#](#tab/csharp)

When the model produces a result, it indicates which service can most appropriately process the utterance. The code in this bot routes the request to the corresponding service, and then summarizes the response from the called service.

```csharp
// Depending on the intent from Dispatch, routes to the right LUIS model or QnA service.
private async Task DispatchToTopIntentAsync(
    ITurnContext context,
    (string intent, double score)? topIntent,
    CancellationToken cancellationToken = default(CancellationToken))
{
    const string homeAutomationDispatchKey = "l_Home_Automation";
    const string weatherDispatchKey = "l_Weather";
    const string noneDispatchKey = "None";
    const string qnaDispatchKey = "q_sample-qna";

    switch (topIntent.Value.intent)
    {
        case homeAutomationDispatchKey:
            await DispatchToLuisModelAsync(context, HomeAutomationLuisKey);

            // Here, you can add code for calling the hypothetical home automation service, passing in any entity
            // information that you need.
            break;
        case weatherDispatchKey:
            await DispatchToLuisModelAsync(context, WeatherLuisKey);

            // Here, you can add code for calling the hypothetical weather service,
            // passing in any entity information that you need
            break;
        case noneDispatchKey:
            // You can provide logic here to handle the known None intent (none of the above).
            // In this example we fall through to the QnA intent.
        case qnaDispatchKey:
            await DispatchToQnAMakerAsync(context, QnAMakerKey);
            break;

        default:
            // The intent didn't match any case, so just display the recognition results.
            await context.SendActivityAsync($"Dispatch intent: {topIntent.Value.intent} ({topIntent.Value.score}).");
            break;
    }
}

// Dispatches the turn to the request QnAMaker app.
private async Task DispatchToQnAMakerAsync(
    ITurnContext context,
    string appName,
    CancellationToken cancellationToken = default(CancellationToken))
{
    if (!string.IsNullOrEmpty(context.Activity.Text))
    {
        var results = await _services.QnAServices[appName].GetAnswersAsync(context).ConfigureAwait(false);
        if (results.Any())
        {
            await context.SendActivityAsync(results.First().Answer, cancellationToken: cancellationToken);
        }
        else
        {
            await context.SendActivityAsync($"Couldn't find an answer in the {appName}.");
        }
    }
}


// Dispatches the turn to the requested LUIS model.
private async Task DispatchToLuisModelAsync(
    ITurnContext context,
    string appName,
    CancellationToken cancellationToken = default(CancellationToken))
{
    await context.SendActivityAsync($"Sending your request to the {appName} system ...");
    var result = await _services.LuisServices[appName].RecognizeAsync(context, cancellationToken);

    await context.SendActivityAsync($"Intents detected by the {appName} app:\n\n{string.Join("\n\n", result.Intents)}");

    if (result.Entities.Count > 0)
    {
        await context.SendActivityAsync($"The following entities were found in the message:\n\n{string.Join("\n\n", result.Entities)}");
    }
}
```

<!--
# [JavaScript](#tab/javascript)

```javascript
```
-->

---

## Evaluate the dispatcher's performance

Sometimes there are user messages that are provided as examples in both the LUIS apps and the QnA maker services, and the combined LUIS app that Dispatch generates won't perform well for those inputs. You can check your app's performance using the `eval` option.

```shell
dispatch eval
```

Running `dispatch eval` generates a **Summary.html** file that provides statistics on the predicted performance of the language model. You can run `dispatch eval` on any LUIS app, not just LUIS apps created by the dispatch tool.

### Edit intents for duplicates and overlaps

Review example utterances that are flagged as duplicates in **Summary.html**, and remove similar or overlapping examples. For example, let's say that in the `Home Automation` LUIS app  requests like "turn my lights on" map to a "TurnOnLights" intent, but requests like "Why won't my lights turn on?" map to a "None" intent so that they can be passed on to QnA Maker. When you combine the LUIS app and the QnA Maker service using dispatch, you need to do one of the following:

* Remove the "None" intent from the original `Home Automation` LUIS app, and add the utterances in that intent to the "None" intent in the dispatcher app.
* If you don't remove the "None" intent from the original LUIS app, you need to add logic in your bot to pass those messages that match that intent on to the QnA maker service.


## Additional resources 

**Delete resources:** This sample creates a number of applications and resources that you can delete using the steps listed below, but you should not delete resources that *any other apps or services* rely on. 

_LUIS resources_
1. Sign in to the [luis.ai](https://www.luis.ai) portal.
1. Go to the _My Apps_ page.
1. Select the apps created by this sample.
   * `Home Automation`
   * `Weather`
   * `NLP-With-Dispatch-BotDispatch`
1. Click _Delete_, and click _Ok_ to confirm.

_QnA Maker resources_
1. Sign in to the [qnamaker.ai](https://www.qnamaker.ai/) portal.
1. Go to the _My knowledge bases_ page.
1. Click the delete button for the `Sample QnA` knowledge base, and click _Delete_ to confirm.

**Best practice:** To improve services used in this sample, refer to best practice for [LUIS](https://docs.microsoft.com/en-us/azure/cognitive-services/luis/luis-concept-best-practices), and [QnA Maker](https://docs.microsoft.com/en-us/azure/cognitive-services/qnamaker/concepts/best-practices).
