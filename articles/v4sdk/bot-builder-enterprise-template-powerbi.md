---
title: Conversational Analytics using PowerBI | Microsoft Docs
description: Learn about how the Enterprise Bot template makes use of Application Insights to enable insights through PowerBI
author: darrenj
ms.author: darrenj
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 09/18/2018
monikerRange: 'azure-bot-service-4.0'
---

# Enterprise Bot Template - Conversational Analytics using PowerBI Dashboard and Application Insights

> [!NOTE]
> This topics applies to v4 version of the SDK. 

Once your Bot is deployed and it starts to process messages you will see telemetry flowing into the Application Insights instance within your Resource Group. 

This telemetry can be viewed within the Application Insights Blade in the Azure portal and using Log Analytics. In addition, the same telemetry can be used by PowerBI to provide more general business insights into the usage of your Bot.

An example PowerBI dasboard is provided within the PowerBI folder of your created project. This is provided for example purposes and demonstrates how you can start to create your own insights. Over time we'll enhance these visualisations. 

## Getting Started

- Download PowerBI Desktop from [here](https://powerbi.microsoft.com/en-us/desktop/)
 
- Retrieve an ```Application Id``` for the Application Insights resource used by your Bot. You can get this by navigating to the API Access page under the Configure Section of the Application Insights Azure Blade.

Double click the provided PowerBI template file located within the PowerBI folder of your Solution. You'll be prompted for the ```Application Id``` that you retrieved in the previous step. Complete authentication if prompted using your Azure subscription credentials, you may need to click on the Organizational Account setting to sign-in.

The resulting dashboard is now linked to your Application Insights instance and you should see initial insights within the dashboard if messages have been sent and received.

>Note the Sentiment visualisation will not show data as the currently deployment script doesn't enable Sentiment when publishing the LUIS model. If you [re-publish](https://docs.microsoft.com/en-us/azure/cognitive-services/luis/luis-how-to-publish-app) the LUIS model and enable Sentiment this will work.

## Middleware Processing

Telemetry wrappers around the QnAMaker and LuisRecognizer classes are provided to ensure consistent telemetry output regardless of scenario and enable a standard dashboard to work across each project.

```TelemetryLuisRecognizer``` and ```TelemetryQnAMaker``` both provide properties on the constructor enabling a developer to disable logging of usernames and original messages. This will hwoever reduce the amount of insight available.

## Telemetry Captured

4 distinct telemetry events are captured through use of  ```TelemetryLuisRecognizer``` and ```TelemetryQnAMaker``` which are enabled by default in the Enterprise template. 

Each LUIS Intent used by your project wil be prefixed with LuisIntent. to enable easy identification of Intents by the dashboard.

```
-BotMessageReceived
    - ActivityId
    - Channel
    - FromId
    - Conversationid
    - ConversationName
    - Locale
    - UserName
    - Text
```
  
```
-BotMessageSent
    - ActivityId,
    - Channel
    - RecipientId
    - Conversationid
    - ConversationName
    - Locale
    - ReceipientName
    - Text
```

```
- LuisIntent.*
    - ActivityId
    - Intent
    - IntentScore
    - SentimentLabel
    - SentimentScore
    - ConversationId
    - Question
```

```
- QnAMaker
    - ActivityId
    - ConversationId
    - OriginalQuestion
    - UserName
    - QnAItemFound
    - Question
    - Answer
    - Score
```