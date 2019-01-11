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
> This topic applies to v4 version of the SDK. 

Once your Bot is deployed and it starts to process messages you will see telemetry flowing into the Application Insights instance within your Resource Group. 

This telemetry can be viewed within the Application Insights Blade in the Azure portal and using Log Analytics. In addition, the same telemetry can be used by PowerBI to provide more general business insights into the usage of your Bot.

An example PowerBI dashboard is provided at [Conversational AI Telemetry](https://aka.ms/botPowerBiTemplate). 

This is provided for example purposes and demonstrates how you can start to create your own insights. Over time we'll enhance these visualisations. 


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
    - FromName
    - ConversationId
    - ConversationName
    - Locale
    - Text
    - RecipientId
    - RecipientName
```
  
```
-BotMessageSent
    - ActivityId,
    - Channel
    - RecipientId
    - ConversationId
    - ConversationName
    - Locale
    - RecipientId
    - RecipientName
    - ReplyToId
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
    - DialogId
```

```
- QnAMaker
    - ActivityId
    - ConversationId
    - OriginalQuestion
    - FromName
    - ArticleFound
    - Question
    - Answer
    - Score
```