---
title: Overview of the Enterprise Bot Template | Microsoft Docs
description: Learn about the capabilities of the Enterprise Bot template
author: darrenj
ms.author: darrenj
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 09/18/2018
monikerRange: 'azure-bot-service-4.0'
---
# Enterprise Bot Template 

> [!NOTE]
> This topics applies to v4 version of the SDK. 

Creation of a high quality conversational experience requires a foundational set of capabilities. To help you succeed with building great conversational experiences, we have created an Enterprise Bot Template. This template brings together all of the best practices and supporting components we've identified through building of conversational experiences. 

This template greatly simplifies the creation of a new bot project. The template will provide the following out of box capabilities, leveraging [Bot Builder SDK v4](https://github.com/Microsoft/botbuilder) and [Bot Builder Tools](https://github.com/Microsoft/botbuilder-tools).

Feature | Description |
------------ | -------------
Introduction Message | Introduction message with an Adaptive Card on conversation start. It explains the bots capabilities and provides buttons to guide initial questions. Developers can then customize this as appropriate.
Automated typing indicators  | Send visual typing indicators during conversations and repeat for long running operations.
.bot file driven configuration | All configuration information for your Bot e.g. LUIS, Dispatcher Endpoints, Application Insights is wrapped up inside the .bot file and used to drive the Startup of your Bot.
Basic conversational intents  | Base intents (Greeting, Goodbye, Help, Cancel, etc.) in English, French, Italian, German, Spanish. These are provided in .LU (language understanding) files enabling easy modification.
Basic conversational responses  | Responses to basic conversational intents abstracted into separate View classes. These will move to the new language generation (LG) files in the future.
Inappropriate content or PII (personally identifiable information) detection  |Detect inappropriate or PII data in incoming conversations through use of [Content Moderator](https://azure.microsoft.com/en-us/services/cognitive-services/content-moderator/) in a middleware component.
Transcripts  | Transcripts of all converstartion stored in Azure Storage
Dispatcher | An integrated [Dispatch](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-tutorial-dispatch?view=azure-bot-service-4.0&tabs=csaddref%2Ccsbotconfig) model to identify whether a given utterance should be processed by LUIS + Code or passed to QnAMaker.
QnAMAker Integration  | Integration with [QnAMaker](https://www.qnamaker.ai) to answer general questions from a Knowledgebase which can be leverage existing data sources (e.g. PDF manuals).
Conversational Insights  | Integration with [Application Insights](https://azure.microsoft.com/en-gb/services/application-insights/) to collect telemetry for all conversations and an example PowerBI dashboard to get you started with insights into your conversational experiences.

In addition, all of the Azure resources required for the Bot are automatically deployed: Bot registration, Azure App Service, LUIS, QnAMaker, Content Moderator, CosmosDB, Azure Storage, and Application Insights. Additionally, base LUIS, QnAMaker, and Dispatch models are created, trained, and published to enable immediate testing of basic intents and routing.

Once the template is created and deployment steps are executed you can hit F5 to test end-to-end. This provides a solid base from which to start your conversational experience, reducing multiple days' worth of effort that each project had to undertake and raises the conversational quality bar.

To get started, continue [create the project](bot-builder-enterprise-template-create-project.md). To learn more about the best practice and learnings that have driven the above features, see [template details](bot-builder-enterprise-template-overview-detail.md) topic. 

Full source code for the Enterprise Bot Template is available at this [GitHub location](https://github.com/Microsoft/AI/tree/master/templates/Enterprise-Template)
