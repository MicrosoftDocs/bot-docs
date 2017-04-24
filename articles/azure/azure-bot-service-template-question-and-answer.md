---
title: Create a question and answer bot using Azure Bot Service | Microsoft Docs
description: Learn how to create a question and answer bot using Azure Bot Service.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 04/18/2017
ms.reviewer: 
ROBOTS: Index, Follow
---

# Create a bot using the Question and Answer template

To create a bot that can answer frequently asked questions (FAQs), choose the [Question and Answer template](~/azure/azure-bot-service-templates.md) when creating the bot using Azure Bot Service. This article describes how to specify a knowledge base for your bot and provides a walkthrough of the code that is automatically generated when you create a bot using the Question and Answer template.

> [!NOTE]
> A bot that is created using the Question and Answer template routes messages 
> in the same manner as described for the [Basic template](~/azure/azure-bot-service-template-basic.md).

## Specify a knowledge base for your bot

To enable your bot to answer FAQs, you must connect it to a knowledge base of questions and answers. Using [QnA Maker][qnaMaker], you can either manually configure your own custom list of questions and answers or scrape questions and answers from an existing FAQ site or structured document. For more information about creating a knowledge base for your bot using QnA Maker, see the QnA Maker [documentation][qnaMakerDocs]. 

When you use the Question and Answer template to create a bot with the Azure Bot Service, you can connect to an existing knowledge base that you have already created using [QnA Maker][qnaMaker] or create a new (empty) knowledge base.

## Code walkthrough

Most activities that the bot receives will be of [type](~/dotnet/bot-builder-dotnet-activities.md) `Message` and will contain the text and attachments that the user sent to the bot. To process an incoming message, the bot posts the message to `BasicQnAMakerDialog` (in **BasicQnAMakerDialog.csx**). 

This code snippet uses C# to post an incoming message to `BasicQnAMakerDialog`.

[!code-csharp[process message](~/includes/code/azure-bot-service-template-question-and-answer.cs#processMessage)]

This code snippet uses Node.js to post an incoming message to `BasicQnAMakerDialog`.

[!code-javascript[process message](~/includes/code/azure-bot-service-template-question-and-answer.js#processMessage)]

### BasicQnAMakerDialog.csx

The `BasicQnAMakerDialog` object inherits from the `QnAMakerDialog` object, which contains the `StartAsync` and `MessageReceived` methods. When the dialog is instantiated, its `StartAsync` method runs and calls `IDialogContext.Wait` with the continuation delegate that will be called when there is a new message. In the initial case, there is an immediate message available (the one that launched the dialog) and the message is immediately passed to the `MessageReceived` method. The `MessageReceived` method calls the QnA Maker service and returns the response to the user.

> [!TIP]
> For C#, the QnAMaker Dialog is distributed via the `Microsoft.Bot.Builder.CognitiveServices` NuGet package. 
> For Node.js, the QnAMaker Dialog is distributed via the `botbuilder-cognitiveservices` **npm** module.

The call to invoke the QnA Maker service can include up to four parameters:

| Parameter | Required or optional | Description |
|----|----|----|
| Subscription Key | required | The subscription key that you acquire when you register with QnA Maker. (QnA Maker assigns each registered user a unique subscription key for metering purposes.) |
| Knowledge Base ID | required | The unique identifier for the QnA Maker knowledge base to which you want to connect. |
| Default Message | optional | The message to show if no match is found in the knowledge base. |
| Score Threshold | optional | A value between 0-1 that represents the threshold value of the match confidence score returned by the service. You can use this parameter to control the relevance of the responses. |

This code snippet uses C# to invoke the QnA Maker service.

[!code-csharp[BasicQnAMakerDialog](~/includes/code/azure-bot-service-template-question-and-answer.cs#BasicQnAMakerDialog)]

This code snippet uses Node.js to invoke the QnA Maker service.

[!code-javascript[BasicQnAMakerDialog](~/includes/code/azure-bot-service-template-question-and-answer.js#BasicQnAMakerDialog)]

## Extend default functionality

The Question and Answer template provides a good foundation that you can build upon and customize to create a bot that can answer frequently asked questions (FAQs). To learn more about using QnA Maker with your bot, see [Design knowledge bots](~/bot-design-pattern-knowledge-base.md#qna-maker) and [QnA Maker][qnaMaker].

## Additional resources

- [Create a bot with the Azure Bot Service](~/azure/azure-bot-service-quickstart.md)
- [Templates in the Azure Bot Service](~/azure/azure-bot-service-templates.md)
- [Design knowledge bots](~/bot-design-pattern-knowledge-base.md)
- <a href="https://qnamaker.ai/" target="_blank">QnA Maker</a>
- <a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">Bot Builder Samples GitHub repository</a>
- [Bot Builder SDK for .NET](~/dotnet/index.md)
- [Bot Builder SDK for Node.js](~/nodejs/index.md)

[qnaMaker]: https://qnamaker.ai/

[qnaMakerDocs]:[https://qnamaker.ai/Documentation]