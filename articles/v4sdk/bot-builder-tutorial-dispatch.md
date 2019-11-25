--- 
title: Use multiple LUIS and QnA models | Microsoft Docs
description: Learn how to use LUIS and QnA maker in your bot.
keywords: Luis, QnA, Dispatch tool, multiple services, route intents
author: diberry
ms.author: diberry
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 11/22/2019
monikerRange: 'azure-bot-service-4.0'
---

# Use multiple LUIS and QnA models

[!INCLUDE[applies-to](../includes/applies-to.md)]

If a bot uses multiple LUIS models and QnA Maker knowledge bases (knowledge bases), you can use Dispatch tool to determine which LUIS model or QnA Maker knowledge base best matches the user input. The dispatch tool does this by creating a single LUIS app to route user input to the correct model. For more information about the Dispatch, including the CLI commands, refer to the [README][dispatch-readme].

## Prerequisites

- Knowledge of [bot basics](bot-builder-basics.md), [LUIS][howto-luis], and [QnA Maker][howto-qna].
- [Dispatch tool](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/Dispatch)
- A copy of the **NLP with Dispatch** from either the [C# Sample][cs-sample] or [JS Sample][js-sample] code repository.
- A [luis.ai](https://www.luis.ai/) account to publish LUIS apps.
- A [QnA Maker](https://www.qnamaker.ai/) account to publish the QnA knowledge base.

## About this sample

This sample is based on a predefined set of LUIS and QnA Maker Apps.

## [C#](#tab/cs)

![Code sample logic flow](./media/tutorial-dispatch/dispatch-logic-flow.png)

`OnMessageActivityAsync` is called for each user input received. This module finds the top scoring user intent and passes that result on to `DispatchToTopIntentAsync`. DispatchToTopIntentAsync, in turn, calls the appropriate app handler

- `ProcessSampleQnAAsync` - for bot faq questions.
- `ProcessWeatherAsync` - for weather queries.
- `ProcessHomeAutomationAsync` - for home lighting commands.

## [JavaScript](#tab/js)

![Code sample logic flow](./media/tutorial-dispatch/dispatch-logic-flow-js.png)

`onMessage` is called for each user input received. This module finds the top scoring user intent and passes that result on to `dispatchToTopIntentAsync`. dispatchToTopIntentAsync, in turn, calls the appropriate app handler

- `processSampleQnA` - for bot faq questions.
- `processWeather` - for weather queries.
- `processHomeAutomation` - for home lighting commands.

---

The handler calls the LUIS or QnA Maker service and returns the generated result back to the user.

## Create LUIS apps and QnA knowledge base

Before you can create the dispatch model, you'll need to have your LUIS apps and QnA knowledge bases created and published. In this article, we'll publish the following models that are included with the _NLP With Dispatch_ sample in the `\CognitiveModels` folder:

| Name | Description |
|------|------|
| HomeAutomation | A LUIS app that recognizes a home automation intent with associated entity data.|
| Weather | A LUIS app that recognizes weather-related intents with location data.|
| QnAMaker  | A QnA Maker knowledge base that provides answers to simple questions about the bot. |

### Create LUIS apps

1. Log into the [LUIS web portal](https://www.luis.ai/). Under the _My apps_ section, select the Tab _Import new app_. The following Dialog Box will appear:

    ![Import LUIS json file](./media/tutorial-dispatch/import-new-luis-app.png)

2. Select the button _Choose app file_, navigate to the CognitiveModel folder of your sample code and select the file 'HomeAutomation.json'. Leave the optional name field blank.

3. Select _Done_.

4. Once LUIS opens up your Home Automation app, select the _Train_ button. This will train your app using the set of utterances you just imported using the 'home-automation.json' file.

5. When training is complete, select the _Publish_ button. The following Dialog Box will appear:

    ![Publish LUIS app](./media/tutorial-dispatch/publish-luis-app.png)

6. Choose the 'production' environment and then select the _Publish_ button.

7. Once your new LUIS app has been published, select the _MANAGE_ Tab. From the 'Application Information' page, record the values `Application ID` as "_app-id-for-app_" and `Display name` as "_name-of-app_". From the 'Key and Endpoints' page, record the values `Authoring Key` as "_your-luis-authoring-key_" and `Region` as "_your-region_". These values will later be used within your 'appsetting.json' file.

8. Once completed, _Train_ and _Publish_ both your LUIS **Home Automation** app and your LUIS **Weather** app by repeating the above steps for 'Weather.json' file.

### Create QnA Maker knowledge base

The first step to setting up a QnA Maker knowledge base is to set up a QnA Maker service in Azure. To do that, follow the step-by-step instructions found [here](https://aka.ms/create-qna-maker).

Once your QnA Maker Service has been created in Azure, you need to record the Cognitive Services _Key 1_ provided for your QnA Maker service. This will be used as \<azure-qna-service-key1> when adding the QnA Maker app to your dispatch application.

Learn more about the [two different types of keys](https://docs.microsoft.com/azure/cognitive-services/qnamaker/how-to/set-up-qnamaker-service-azure#types-of-keys-in-qna-maker) used with QnA Maker.

The following steps provide you with this key:

![Select Cognitive Service](./media/tutorial-dispatch/select-qna-cognitive-service.png)

1. From within your Azure portal, select your QnA Maker cognitive service.

    ![Select Cognitive Service Keys](./media/tutorial-dispatch/select-cognitive-service-keys.png)

1. Select the Keys icon found under the _Resource Management_ section on the left-hand menu.

    ![Select Cognitive Service Key1](./media/tutorial-dispatch/select-cognitive-service-key1.png)

1. Copy the value of _Key 1_ to your clipboard and save this locally. this will later be used for the (-k) key value \<azure-qna-service-key1> when adding the QnA Maker app to your dispatch application.

1. Now sign in to the [QnAMaker web portal](https://qnamaker.ai).

1. At step 2, select the following:

    - Your Azure AD account.
    - Your Azure subscription name.
    - The name you created for your QnA Maker service. (If your Azure QnA service does not initially appear in this pull down list, try refreshing the page.)

    ![Create QnA Step 2](./media/tutorial-dispatch/create-qna-step-2.png)

1. At step 3, provide a name for your QnA Maker knowledge base. For this example use the name 'sample-qna'.

    ![Create QnA Step 3](./media/tutorial-dispatch/create-qna-step-3.png)

1. At step 4, select the option _+ Add File_, navigate to the CognitiveModel folder of your sample code, and select the file 'QnAMaker.tsv'. There is an additional selection to add a _Chit-chat_ personality to your knowledge base but our example does not include this option.

    ![Create QnA Step 4](./media/tutorial-dispatch/create-qna-step-4.png)

1. At step 5, select _Create your knowledge base_.

1. Once the knowledge base is created from your uploaded file, select _Save and train_ and when finished, select the _PUBLISH_ Tab and publish your app.

1. Once your QnA Maker app is published, select the _SETTINGS_ Tab, and scroll down to 'Deployment details'. Record the following values from the _Postman_ Sample HTTP request.

    ```text
    POST /knowledge bases/<knowledge-base-id>/generateAnswer
    Host: <your-hostname>  // NOTE - this is a URL.
    Authorization: EndpointKey <qna-maker-resource-key>
    ```

    The full URL string for your hostname will look like "https://<host-id>.azure.net/qnamaker". These values will later be used within your `appsettings.json` or `.env` file.

## Dispatch app needs read access to existing apps

The dispatch tool needs authoring access to read the existing LUIS and QnA Maker apps in order to create a new parent LUIS app that dispatches to the LUIS and QnA Maker apps. This access is provided with the app IDs and authoring keys.

### Service authoring keys

The **authoring key** is only used for creating and editing the models. You need an ID and key for each of the two LUIS apps and the QnA Maker app.

|App|Location of information|
|--|--|
|LUIS|**App ID** - found in the [LUIS portal](https://www.luis.ai) for each app, Manage -> Application Information<br>**Authoring Key** - found in the LUIS portal, top-right corner, select your own User, then Settings.|
|QnA Maker| **App ID** - found in the [QnA Maker portal](https://http://qnamaker.ai) on the Settings page after you publish the app. This is the ID found in first part of the POST command after the knowledgebase. An example of where to find the app ID is `POST /knowledgebases/<APP-ID>/generateAnswer`.<br>**Authoring Key** - found in the Azure portal, for the QnA Maker resource, under the **Keys**. You only need one of the keys.|

The authoring key is not used to get a prediction score or confidence score from the published application. You need the endpoint keys for this action. The **[endpoint keys](#service-endpoint-keys)** are found and used later in this tutorial.

Learn more about the [two different types of keys](https://docs.microsoft.com/azure/cognitive-services/qnamaker/how-to/set-up-qnamaker-service-azure#types-of-keys-in-qna-maker) used with QnA Maker.

## Create the dispatch model

The CLI interface for the dispatch tool creates the model for dispatching to the correct LUIS or QnA Maker app.

1. Open a command prompt or terminal window, and change directories to the **CognitiveModels** directory
1. Make sure you have the current version of npm and the Dispatch tool.

    ```cmd
    npm i -g npm
    npm i -g botdispatch
    ```

1. Use `dispatch init` to initialize create a `.dispatch` file for your dispatch model. Create this using a filename you will recognize.

    ```cmd
    dispatch init -n <filename-to-create> --luisAuthoringKey "<your-luis-authoring-key>" --luisAuthoringRegion <your-region>
    ```

1. Use `dispatch add` to add your LUIS apps and QnA Maker knowledge bases to the `.dispatch` file.

    ```cmd
    dispatch add -t luis -i "<app-id-for-weather-app>" -n "<name-of-weather-app>" -v <app-version-number> -k "<your-luis-authoring-key>" --intentName l_Weather
    dispatch add -t luis -i "<app-id-for-home-automation-app>" -n "<name-of-home-automation-app>" -v <app-version-number> -k "<your-luis-authoring-key>" --intentName l_HomeAutomation
    dispatch add -t qna -i "<knowledge-base-id>" -n "<knowledge-base-name>" -k "<azure-qna-service-key1>" --intentName q_sample-qna
    ```

1. Use `dispatch create` to generate a dispatch model from the `.dispatch` file.

    ```cmd
    dispatch create
    ```

1. Publish the dispatch LUIS app, just created.

## Use the dispatch LUIS app

The generated LUIS app defines intents for each of the child apps and the knowledge base, as well as a _none_ intent for when the utterance doesn't have a good fit.

- `l_HomeAutomation`
- `l_Weather`
- `None`
- `q_sample-qna`

These services need to be published under the correct names for the bot to run properly.
The bot needs information about the published services, so that it can access those services.

### Service endpoint keys

The bot needs the query prediction endpoints for the three LUIS apps (dispatch, weather, and home automation) and the single QnA Maker knowledge base. Use the following table to find the endpoint keys:

|App|Query endpoint key location|
|--|--|
|LUIS|In the LUIS portal, for each LUIS app, in the Manage section, select **Keys and Endpoint settings** to find the keys associated with each app. If you are following this tutorial, the endpoint key is the same key as the `<your-luis-authoring-key>`. The authoring key allows for 1000 endpoint hits then expires.|
|QnA Maker|In the QnA Maker portal, for the knowledge base, in the Manage settings, use the key value shows in the Postman settings for the **Authorization** header, without the text of `EndpointKey`.|

These values are used in the **appsettings.json** for C# and the **.env** file for javascript.

## [C#](#tab/cs)

### Installing packages

Prior to running this app for the first time ensure that several NuGet packages are installed:

- **Microsoft.Bot.Builder**
- **Microsoft.Bot.Builder.AI.Luis**
- **Microsoft.Bot.Builder.AI.QnA**

### Manually update your appsettings.json file

Once all of your service apps are created, the information for each needs to be added into your 'appsettings.json' file. The initial [C# Sample][cs-sample] code contains an empty appsettings.json file:

**appsettings.json**

[!code-json[AppSettings](~/../botbuilder-samples/samples/csharp_dotnetcore/14.nlp-with-dispatch/AppSettings.json?range=8-17)]

For each of the entities shown below, add the values you recorded earlier in these instructions:

**appsettings.json**

```json
"MicrosoftAppId": "",
"MicrosoftAppPassword": "",
  
"QnAKnowledgebaseId": "<knowledge-base-id>",
"QnAEndpointKey": "<qna-maker-resource-key>",
"QnAEndpointHostName": "<your-hostname>",

"LuisAppId": "<app-id-for-dispatch-app>",
"LuisAPIKey": "<your-luis-endpoint-key>",
"LuisAPIHostName": "<your-dispatch-app-region>",
```

When all changes are complete, save this file.

## [JavaScript](#tab/js)

### Installing packages

Prior to running this app for the first time you will need to install several npm packages.

```powershell
npm install --save botbuilder
npm install --save botbuilder-ai
```

To use the .env configuration file, your bot needs an extra package included:

```powershell
npm install --save dotenv
```

### Manually update your .env file

Once all of your service apps are created, the information for each needs to be added into your '.env' file. The initial [JavaScript Sample][js-sample] code contains an empty .env file. 

**.env**  
[!code-file[EmptyEnv](~/../botbuilder-samples/samples/javascript_nodejs/14.nlp-with-dispatch/.env?range=1-10)]

Add your service connection values as shown below:

**.env**

```file
MicrosoftAppId=""
MicrosoftAppPassword=""

QnAKnowledgebaseId="<knowledge-base-id>"
QnAEndpointKey="<qna-maker-resource-key>"
QnAEndpointHostName="<your-hostname>"

LuisAppId=<app-id-for-dispatch-app>
LuisAPIKey=<your-luis-endpoint-key>
LuisAPIHostName=<your-dispatch-app-region>
```

When all changes are in place, save this file.

---

### Connect to the services from your bot

To connect to the Dispatch, LUIS, and QnA Maker services, your bot pulls information from the settings file (either the `appsettings.json` or the `.env` file).

## [C#](#tab/cs)

In **BotServices.cs**, the information contained within configuration file _appsettings.json_ is used to connect your dispatch bot to the `Dispatch` and `SampleQnA` services. The constructors use the values you provided to connect to these services.

**BotServices.cs**

[!code-csharp[ReadConfigurationInfo](~/../botbuilder-samples/samples/csharp_dotnetcore/14.nlp-with-dispatch/BotServices.cs?range=16-31)]

## [JavaScript](#tab/js)

In **dispatchBot.js** the information contained within configuration file _.env_ is used to connect your dispatch bot to the _LuisRecognizer(dispatch)_ and _QnAMaker_ services. The constructors use the values you provided to connect to these services.

**bots/dispatchBot.js**

[!code-javascript[ReadConfigurationInfo](~/../botbuilder-samples/samples/javascript_nodejs/14.nlp-with-dispatch/bots/dispatchBot.js?range=11-26)]

---

> [!NOTE]
> By default the `includeApiResults` parameter is set to false, meaning the recognizer will only return basic information about entities / intents. If you require the full response from LUIS (such as the `ConnectedServiceResult` used later in this tutorial), then set this parameter to true. This will then add the full response from the LUIS service into the Properties collection on the `RecognizerResult`.

### Call the services from your bot

For each input  from your user, the bot logic checks user input against the combined Dispatch model, finds the top returned intent, and uses that information to call the appropriate service for the input.

## [C#](#tab/cs)

In the **DispatchBot.cs** file whenever the `OnMessageActivityAsync` method is called, we check the incoming user message against the Dispatch model. We then pass the Dispatch Model's `topIntent` and  `recognizerResult` on to the correct method to call the service and return the result.

**bots\DispatchBot.cs**

[!code-csharp[OnMessageActivity](~/../botbuilder-samples/samples/csharp_dotnetcore/14.nlp-with-dispatch/bots/DispatchBot.cs?range=26-36)]

## [JavaScript](#tab/js)

In the **dispatchBot.js** `onMessage` method, we check the user input message against the Dispatch model, find the _topIntent_, then pass this on by calling _dispatchToTopIntentAsync_.

**bots/dispatchBot.js**

[!code-javascript[onMessage](~/../botbuilder-samples/samples/javascript_nodejs/14.nlp-with-dispatch/bots/dispatchBot.js?range=31-44)]

---

### Work with the recognition results

## [C#](#tab/cs)

When the model produces a result, it indicates which service can most appropriately process the utterance. The code in this bot routes the request to the corresponding service, and then summarizes the response from the called service. Depending on the _intent_ returned from Dispatch, this code uses the returned intent to route to the correct LUIS model or QnA service.

**bots\DispatchBot.cs**

[!code-csharp[DispatchToTop](~/../botbuilder-samples/samples/csharp_dotnetcore/14.nlp-with-dispatch/bots/DispatchBot.cs?range=51-69)]

If method `ProcessHomeAutomationAsync` or `ProcessWeatherAsync` are invoked, they are passed the results from the dispatch model within _luisResult.ConnectedServiceResult_. The specified method then provides user feedback showing the dispatch model top intent, plus a ranked listing of all intents and entities that were detected.

If method `q_sample-qna` is invoked, it uses the user input contained within the turnContext to generate an answer from the knowledge base and display that result to the user.

## [JavaScript](#tab/js)

When the model produces a result, it indicates which service can most appropriately process the utterance. The code in this sample uses the recognized _topIntent_ to show how to route the request on to the corresponding service.

**bots/dispatchBot.js**  
[!code-javascript[dispatchToTopIntentAsync](~/../botbuilder-samples/samples/javascript_nodejs/14.nlp-with-dispatch/bots/dispatchBot.js?range=61-77)]

If method `processHomeAutomation` or `processWeather` are invoked, they are passed the results from the dispatch model within _recognizerResult.luisResult_. The specified method then provides user feedback showing the dispatch model's top intent, plus a ranked listing of all intents and entities that were detected.

If method `q_sample-qna` is invoked, it uses the user input contained within the turnContext to generate an answer from the knowledge base and display that result to the user.

---

> [!NOTE]
> If this were a production application, this is where the selected LUIS methods would connect to their specified service, pass in the user input, and process the returned LUIS intent and entity data.

## Test your bot

1. Using your development environment, start the sample code. Note the _localhost_ address shown in the address bar of the browser window opened by your App: "https://localhost:<Port_Number>". 
1. Open your Bot Framework Emulator, then select `Create a new bot configuration`. A `.bot` file enables you to use the _Inspector_ in the bot emulator to see the JSON returned from LUIS and QnA Maker.
1. In the **New bot configuration** dialog box, enter your bot name, and your endpoint URL, such as `http://localhost:3978/api/messages`. Save the file at the root of your bot sample code project.
1. Open the bot file and add sections for your LUIS and QnA Maker apps. Use [this example file](https://github.com/microsoft/botbuilder-tools/blob/master/packages/MSBot/docs/sample-bot-file.json) as a template for settings. Save the changes.
1. Select the bot name in the **My Bots** list to access your running bot. For your reference, here are some of the questions and commands that are covered by the services built for your bot:

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

## Dispatch for user utterance to QnA Maker

1. In the bot emulator, enter the text `hi` and submit the utterance. The bot submits this query to the dispatch LUIS app and gets back a response indicating which child app should get this utterance for further processing.

1. By selecting the `LUIS Trace` line in the log, you can see the LUIS response in the bot emulator . The LUIS result from the dispatch LUIS app displays in the Inspector.

    ```json
    {
      "luisResponse": {
        "entities": [],
        "intents": [
          {
            "intent": "q_sample-qna",
            "score": 0.9489713
          },
          {
            "intent": "l_HomeAutomation",
            "score": 0.0612499453
          },
          {
            "intent": "None",
            "score": 0.008567564
          },
          {
            "intent": "l_Weather",
            "score": 0.0025761195
          }
        ],
        "query": "Hi",
        "topScoringIntent": {
          "intent": "q_sample-qna",
          "score": 0.9489713
        }
      }
    }
    ```

    Because the utterance, `hi`, is part of the dispatch LUIS app's **q_sample-qna** intent, and is selected as the `topScoringIntent`, the bot will make a second request, this time to the QnA Maker app, with the same utterance.

1. Select the `QnAMaker Trace` line in the bot emulator log. The QnA Maker result displays in the Inspector.

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

## Resolving incorrect top intent from Dispatch

Once your bot is running, it is possible to improve the bot's performance by removing similar or overlapping utterances between the dispatched apps.
<!--For example, let's say that in the `Home Automation` LUIS app requests like "turn my lights on" map to a "TurnOnLights" intent, but requests like "Why won't my lights turn on?" map to a "None" intent so that they can be passed on to QnA Maker. These two utterances are too close for the dispatch LUIS app to determine if the correct child app is the LUIS app or the QnA Maker app.

When you combine the LUIS app and the QnA Maker app using dispatch, you need to do _one_ of the following:

- Remove the "None" intent from the child `Home Automation` LUIS app, and instead add the utterances from that intent to the "None" intent in the dispatcher app.
- Add logic in your bot to pass the messages that match the Dispatch LUIS app's "None" intent on to the QnA maker service. Compare the score of the Dispatch LUIS app's score and the score of the QnA Maker app. Use the highest score. This effectively removes QnA Maker from the Dispatch cycle.

Either of the above two actions will reduce the number of times that your bot responds back to your users with the message, 'Couldn't find an answer.'
-->
You can use the [Dispatch][dispatch-readme] command-line tool to test and evaluate your dispatch model.

### To update or create a new LUIS model

This sample is based on a preconfigured LUIS model. Additional information to help you update this model, or create a new LUIS model, can be found [here](https://aka.ms/create-luis-model#updating-your-cognitive-models).

After updating the underlying models (QnA or LUIS) run `dispatch refresh` to update your Dispatch LUIS app. `dispatch refresh` is basically the same command as `dispatch create` except no new LUIS app ID is created. 

Note that utterances that were added directly in LUIS will not be retained when running `dispatch refresh`. To keep those extra utterances in the Dispatch app add those utterances in a text file (one utterance per line), and then add the file to Dispatch by running the command:

```powershell
dispatch add -t file -f <file path> --intentName <target intent name, ie l_General>
```

Once the file with extra utterances is added to Dispatch the utterances will stay with every refresh.

### To delete resources

This sample creates a number of applications and resources that you can delete using the steps listed below, but you should not delete resources that *any other apps or services* rely on.

To delete LUIS resources:

1. Sign in to the [luis.ai](https://www.luis.ai) portal.
1. Go to the _My Apps_ page.
1. Select the apps created by this sample.
   - `Home Automation`
   - `Weather`
   - `NLP-With-Dispatch-BotDispatch`
1. Click _Delete_, and click _Ok_ to confirm.

To delete QnA Maker resources:

1. Sign in to the [qnamaker.ai](https://www.qnamaker.ai/) portal.
1. Go to the _My knowledge bases_ page.
1. Click the delete button for the `Sample QnA` knowledge base, and click _Delete_ to confirm.

### Best practice

To improve services used in this sample, refer to best practice for [LUIS](https://docs.microsoft.com/azure/cognitive-services/luis/luis-concept-best-practices), and [QnA Maker](https://docs.microsoft.com/azure/cognitive-services/qnamaker/concepts/best-practices).

<!-- Foot-note style links -->

[howto-luis]: bot-builder-howto-v4-luis.md
[howto-qna]: bot-builder-howto-qna.md

[cs-sample]: https://aka.ms/dispatch-sample-cs
[js-sample]: https://aka.ms/dispatch-sample-js

[dispatch-readme]: https://aka.ms/dispatch-command-line-tool
<!--[dispatch-evaluate]: https://aka.ms/dispatch-command-line-tool#evaluating-your-dispatch-model-->