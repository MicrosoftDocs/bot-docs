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
ms.date: 01/15/2019
monikerRange: 'azure-bot-service-4.0'
---

# Use multiple LUIS and QnA models

[!INCLUDE[applies-to](../includes/applies-to.md)]

In this tutorial, we demonstrate how to use the Dispatch service to route utterances when there are multiple LUIS models and QnA maker services for different scenarios supported by a bot. In this case, we configure Dispatch with multiple LUIS models for conversations around home automation and weather information, plus QnA maker service to answer questions based on a FAQ text file as input. This sample combines the following services.

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

You may follow the **README** instructions for [C#](https://aka.ms/dispatch-sample-readme-cs) or [JS](https://aka.ms/dispatch-sample-readme-js) to create this bot using Command Line Interface calls, or follow the steps below to manually create your bot using the Azure, LUIS, and QnAMaker User Interfaces.

 ### Create your bot using service UI
 
To begin manually creating your bot, download the following 4 files located in the GitHub [BotFramework-Samples](https://aka.ms/botdispatchgitsamples) repository into a local folder:
[home-automation.json](https://aka.ms/dispatch-home-automation-json), 
[weather.json](https://aka.ms/dispatch-weather-json), 
[nlp-with-dispatchDispatch.json](https://aka.ms/dispatch-dispatch-json), 
[QnAMaker.tsv](https://aka.ms/dispatch-qnamaker-tsv)
One method to accomplish this is to open the GitHub repository link above, click on **BotFramework-Samples**, then "Clone or download" the repository to your local machine. Note that these files are in a different repository than the sample mentioned in the prerequisites.

### Manually create LUIS apps

Log into the [LUIS web portal](https://www.luis.ai/). Under section _My apps_ select the Tab _Import new app_. The following Dialog Box will appear:

![Import LUIS json file](./media/tutorial-dispatch/import-new-luis-app.png)

select the button _Choose app file_ and select the downloaded file 'home-automation.json'. Leave the optional name field blank. Select _Done_.

Once LUIS opens up your Home Automation app, select the _Train_ button. This will train your app using the set of utterances you just imported using the 'home-automation.json' file.

When training is complete, select the _Publish_ button. The following Dialog Box will appear:

![Publish LUIS app](./media/tutorial-dispatch/publish-luis-app.png)

Choose the 'production' environment and then select the _Publish_ button.

Once your new LUIS app has been published, select the _MANAGE_ Tab. From the 'Application Information' page, record the values `Application ID` and `Display name`. From the 'Key and Endpoints' page, record the values `Authoring Key` and `Region`. These values will later be used by your 'nlp-with-dispatch.bot' file.

Once completed, _Train_ and _Publish_ both your LUIS weather app and your LUIS dispatch app by repeating these same steps for the locally downloaded 'weather.json' and 'nlp-with-dispatchDispatch.json' files.

### Manually create QnA Maker app

The first step to setting up a QnA Maker knowledge base is to first set up a QnA Maker service in Azure. To do that, follow the step-bystep instructions found [here](https://aka.ms/create-qna-maker). Now log into the [QnAMaker web portal](https://qnamaker.ai). Move down to Step 2

![Create QnA Step 2](./media/tutorial-dispatch/create-qna-step-2.png)

and select
1. Your Azure AD account.
1. Your Azure subscription name.
1. The name you created for your QnA Maker service. (If your Azure QnA service does not initially appear in this pull down list, try refreshing the page.) 

Move to Step 3

![Create QnA Step 3](./media/tutorial-dispatch/create-qna-step-3.png)

Provide a name for your QnA Maker knowledgebase. For this example we will be using the name 'sample-qna'.

Move to Step 4

![Create QnA Step 4](./media/tutorial-dispatch/create-qna-step-4.png)

select the option _+ Add File_ and select the downloaded file 'QnAMaker.tsv'

There is an additional selection to add a _Chit-chat_ personality to your knowledgebase but our example does not include this option.

Select _Save and train_ and when finished select the _PUBLISH_ Tab and publish your app.

Once your QnA Maker app is published, select the _SETTINGS_ Tab, and scroll down to 'Deployment details'. Record the following values from the _Postman_ Sample HTTP request.

```
POST /knowledgebases/<Your_Knowledgebase_Id>/generateAnswer
Host: <Your_Hostname>
Authorization: EndpointKey <Your_Endpoint_Key>
```
These values will later be used by your 'nlp-with-dispatch.bot' file.

### Manually update your .bot file

Once all of your service apps are created, the information for each needs to be added into your 'nlp-with-dispatch.bot' file. Open this file within the C# or JS Sample file that you previously downloaded. Add the following values to each section of "type": "luis" or "type": "dispatch"

```
"appId": "<Your_Recorded_App_Id>",
"authoringKey": "<Your_Recorded_Authoring_Key>",
"subscriptionKey": "<Your_Recorded_Authoring_Key>",
"version": "0.1",
"region": "<Your_Recorded_Region>",
```

For the section of "type": "qna" Add the following values:

```
"type": "qna",
"name": "sample-qna",
"id": "201",
"kbId": "<Your_Recorded_Knowledgebase_Id>",
"subscriptionKey": "<Your_Azure_Subscription_Key>", // Used when creating your QnA service.
"endpointKey": "<Your_Recorded_Endpoint_Key>",
"hostname": "<Your_Recorded_Hostname>"
```

When all changes are inplace, save this file.

### Test your bot

Now run the sample using the emulator. Once the emulator is opened, select the 'nlp-with-dispatch.bot' file.

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
The following code initializes the bot's references to external services. For example, LUIS and QnaMaker services are created here. These external services are configured using the `BotConfiguration` class (based on the contents of your `.bot` file.)

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

# [JavaScript](#tab/javascript)

The sample code uses predefined naming constants to identify the various sections of your `.bot` file. If you have modified any section names from the original sample namings in your _nlp-with-dispatch.bot_ file, be sure to locate the associated constant declaration in the **bot.js**, **homeAutomation.js**, **qna.js**, or **weather.js** file and change that entry to the modified name.  

```javascript
// In file bot.js
// this is the LUIS service type entry in the .bot file.
const DISPATCH_CONFIG = 'nlp-with-dispatchDispatch';

// In file homeAutomation.js
// this is the LUIS service type entry in the .bot file.
const LUIS_CONFIGURATION = 'Home Automation';

// In file qna.js
// Name of the QnA Maker service in the .bot file.
const QNA_CONFIGURATION = 'sample-qna';

// In file weather.js
// this is the LUIS service type entry in the .bot file.
const WEATHER_LUIS_CONFIGURATION = 'Weather';
```

In **bot.js** the information contained within configuration file _nlp-with-dispatch.bot_ is used to connect your dispatch bot to the various services. Each constructor looks for and uses the appropriate sections of the configuration file based on the section names detailed above.

```javascript
class DispatchBot {
    constructor(conversationState, userState, botConfig) {
        //...
        this.homeAutomationDialog = new HomeAutomation(conversationState, userState, botConfig);
        this.weatherDialog = new Weather(botConfig);
        this.qnaDialog = new QnA(botConfig);

        this.conversationState = conversationState;
        this.userState = userState;

        // dispatch recognizer
        const dispatchConfig = botConfig.findServiceByNameOrId(DISPATCH_CONFIG);
        //...
```
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

# [JavaScript](#tab/javascript)

In the **bot.js** `onTurn` method, we check for incoming messages from the user. If type _ActivityType.Message_ is received, this message is sent out via the bot's _dispatchRecognizer_.

```javascript
if (turnContext.activity.type === ActivityTypes.Message) {
    // determine which dialog should fulfill this request
    // call the dispatch LUIS model to get results.
    const dispatchResults = await this.dispatchRecognizer.recognize(turnContext);
    const dispatchTopIntent = LuisRecognizer.topIntent(dispatchResults);
    //...
 }
```
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

# [JavaScript](#tab/javascript)

When the model produces a result, it indicates which service can most appropriately process the utterance. The code in this bot routes the request to the corresponding service.

```javascript
switch (dispatchTopIntent) {
   case HOME_AUTOMATION_INTENT:
      await this.homeAutomationDialog.onTurn(turnContext);
      break;
   case WEATHER_INTENT:
      await this.weatherDialog.onTurn(turnContext);
      break;
   case QNA_INTENT:
      await this.qnaDialog.onTurn(turnContext);
      break;
   case NONE_INTENT:
      default:
      // Unknown request
       await turnContext.sendActivity(`I do not understand that.`);
       await turnContext.sendActivity(`I can help with weather forecast, turning devices on and off and answer general questions like 'hi', 'who are you' etc.`);
 }
 ```

 In `homeAutomation.js`
 
 ```javascript
 async onTurn(turnContext) {
    // make call to LUIS recognizer to get home automation intent + entities
    const homeAutoResults = await this.luisRecognizer.recognize(turnContext);
    const topHomeAutoIntent = LuisRecognizer.topIntent(homeAutoResults);
    // depending on intent, call turn on or turn off or return unknown
    switch (topHomeAutoIntent) {
       case HOME_AUTOMATION_INTENT:
          await this.handleDeviceUpdate(homeAutoResults, turnContext);
          break;
       case NONE_INTENT:
       default:
         await turnContext.sendActivity(`HomeAutomation dialog cannot fulfill this request.`);
    }
}
```

In `weather.js`

```javascript
async onTurn(turnContext) {
   // Call weather LUIS model.
   const weatherResults = await this.luisRecognizer.recognize(turnContext);
   const topWeatherIntent = LuisRecognizer.topIntent(weatherResults);
   // Get location entity if available.
   const locationEntity = (LOCATION_ENTITY in weatherResults.entities) ? weatherResults.entities[LOCATION_ENTITY][0] : undefined;
   const locationPatternAnyEntity = (LOCATION_PATTERNANY_ENTITY in weatherResults.entities) ? weatherResults.entities[LOCATION_PATTERNANY_ENTITY][0] : undefined;
   // Depending on intent, call "Turn On" or "Turn Off" or return unknown.
   switch (topWeatherIntent) {
      case GET_CONDITION_INTENT:
         await turnContext.sendActivity(`You asked for current weather condition in Location = ` + (locationEntity || locationPatternAnyEntity));
         break;
      case GET_FORECAST_INTENT:
         await turnContext.sendActivity(`You asked for weather forecast in Location = ` + (locationEntity || locationPatternAnyEntity));
         break;
      case NONE_INTENT:
      default:
         wait turnContext.sendActivity(`Weather dialog cannot fulfill this request.`);
   }
}
```

In `qna.js`

```javascript
async onTurn(turnContext) {
   // Call QnA Maker and get results.
   const qnaResult = await this.qnaRecognizer.generateAnswer(turnContext.activity.text, QNA_TOP_N, QNA_CONFIDENCE_THRESHOLD);
   if (!qnaResult || qnaResult.length === 0 || !qnaResult[0].answer) {
       await turnContext.sendActivity(`No answer found in QnA Maker KB.`);
       return;
    }
    // respond with qna result
    await turnContext.sendActivity(qnaResult[0].answer);
}
```
---

## Edit intents to improve performance

Once your bot is running, it is possible to improve the bot's performance by removing similar or overlapping utterances. For example, let's say that in the `Home Automation` LUIS app  requests like "turn my lights on" map to a "TurnOnLights" intent, but requests like "Why won't my lights turn on?" map to a "None" intent so that they can be passed on to QnA Maker. When you combine the LUIS app and the QnA Maker service using dispatch, you need to do one of the following:

* Remove the "None" intent from the original `Home Automation` LUIS app, and instead add the utterances from that intent to the "None" intent in the dispatcher app.
* If you don't remove the "None" intent from the original LUIS app, you will instead need to add logic into your bot to pass the messages that match your "None" intent on to the QnA maker service.

Either of the above two actions will reduce the number of times that your bot responds back to your users with the message, 'Couldn't find an answer.' 

## Additional resources

**Update or create a new LUIS model:** This sample is based on a preconfigured LUIS model. Additional information to help you update this model, or create a new LUIS model, can be found [here](https://aka.ms/create-luis-model#updating-your-cognitive-models
).

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