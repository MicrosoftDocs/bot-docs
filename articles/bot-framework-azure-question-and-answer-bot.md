---
title: Set up a question and answer bot for Azure Bot Service | Microsoft Docs
description: Learn how to set up questions and answer for Azure Bot Service.
keywords: Bot Framework, Azure Bot Service, question and answer bot
author: Toney001
manager: rstand
ms.topic: bot-service-article
ms.prod: botframework
ms.service: Azure Bot Service
ms.date: 3/21/2017
ms.reviewer:
#ROBOTS: Index
---

# Build a bot that answers questions from an FAQ

The question and answer bot template demonstrates how to use the <a href="https://qnamaker.ai" target="_blank">QnA Maker</a> tool to create a bot than answers questions like from a company's FAQ URL. QnA Maker tool lets you ingest your existing FAQ content to create the source of the questions and answers.

When you create the template, Azure Bot Service lets you either select an existing FAQ as the knowledge base for the bot or create one manually from the <a href="https://qnamaker.ai" target="_blank">QnA Maker portal</a>. 

The routing of the message is identical to the one presented in the [basic bot template](bot-framework-azure-basic-bot.md). Most messages will have a `Message` activity type, and will contain the text and attachments the user sent. If the message’s activity type is `Message`, the template posts the message to `BasicQnAMakerDialog` in the context of the current message (see **BasicQnAMakerDialog.csx**).


[!code-csharp[Route message](../includes/code/azure-question-and-answer.cs#routeMessage)] 

[!code-JavaScript[Route message](../includes/code/azure-question-and-answer.js#routeMessage)]



The `BasicQnAMakerDialog` object inherits from the `QnAMakerDialog` object. The `QnAMakerDialog` object contains the `StartAsync` and `MessageReceived` methods. When the dialog’s instantiated, the dialog’s `StartAsync` method runs and calls `IDialogContext.Wait` with the continuation delegate that’s called when there is a new message. In the initial case, there is an immediate message available (the one that launched the dialog) and the message is immediately passed to the `MessageReceived` method (in the `QnAMakerDialog` object).

The `MessageReceived` method calls your QnA Maker service and returns the response to the user.

`QnAMaker` Dialog is distributed in a separate NuGet package called `Microsoft.Bot.Builder.CognitiveServices` for C#, and the `npm` module is called `botbuilder-cognitiveservices` for Node.js.

These parameters are passed when invoking the QnA Maker service.

- Subscription Key: Each registered user on QnA Maker is assigned an unique subscription key for metering.
- Knowledge Base ID: Each knowledge base created is assigned a unique subscription key by the tool.
- Default Message (optional): Message to show if there is no match in the knowledge base.
- Score Threshold (optional): Threshold value of the match confidence score returned by the service. It ranges from 0-1. This is useful in controlling the relevance of the responses.

[!code-csharp[Set message received](../includes/code/azure-question-and-answer.cs#setMessageReceived)]

[!code-JavaScript[Set message received](../includes/code/azure-question-and-answer.js#setMessageReceived)]


## Additional resources

- <a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">Bot Builder Samples GitHub Repo </a>
- <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/" target="_blank">Bot Builder SDK C# Reference</a>
- <a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">Bot Builder SDK</a>