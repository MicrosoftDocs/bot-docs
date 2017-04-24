---
title: Create a language understanding bot using Azure Bot Service | Microsoft Docs
description: Learn how to create a language understanding bot using Azure Bot Service.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 4/17/2017
ms.reviewer: 
ROBOTS: Index, Follow
---

# Create a bot using the Language understanding template

If a user sends a message such as "get news about virtual reality companies," your bot can use LUIS to interpret the meaning of the message. Using <a href="https://www.luis.ai" target="_blank">LUIS</a>, you can quickly deploy an HTTP endpoint that will interpret user input in terms of the intention that it conveys (find news) and the key entities that are present (virtual reality companies). LUIS enables you to specify the set of intents and entities that are relevant to your application, and then guides you through the process of building a language understanding application.

To create a bot that uses natural language models (LUIS) to understand user intent, choose the [Language understanding template](~/azure/azure-bot-service-templates.md) when creating the bot using Azure Bot Service. This article provides a walkthrough of the code that is automatically generated when you create a bot using the Language understanding template.

## Customize the LUIS application model

When you create a bot using the Language understanding template, Azure Bot Service creates a corresponding LUIS application that is empty (i.e., that always returns `None`). To update your LUIS application model so that it is capable of interpreting user input, you must sign-in to <a href="https://www.luis.ai" target="_blank">LUIS</a>, click **My applications**, select the application that the service created for you, and then create intents, specify entities, and train and publish the application.

> [!NOTE]
> A bot that is created using the Language understanding template routes messages in the same manner as described for the 
> [Basic template](~/azure/azure-bot-service-template-basic.md).

## Code walkthrough

Most activities that the bot receives will be of [type](~/dotnet/bot-builder-dotnet-activities.md) `Message` and will contain the text and attachments that the user sent to the bot. To process an incoming message, the bot posts the message to `BasicLuisDialog` (in **BasicLuisDialog.csx**). 

[!code-csharp[process Message activity](~/includes/code/azure-bot-service-template-language-understanding.cs#processMessage)]

### BasicLuisDialog.csx

**BasicLuisDialog.csx** contains the root dialog that controls the conversation with the user. The `BasicLuisDialog` object defines an intent method handler for each intent that you define in your LUIS application model. The naming convention for intent handlers is **\<intent name\>+Intent** (for example, `NoneIntent`). The `LuisIntent` method attribute defines a method as an intent handler and the name specified in the `LuisIntent` attribute must match the name of the corresponding intent that is specified in the LUIS application model. In this example, the dialog will handle the `None` intent (which LUIS returns if it cannot determine the intent) and the `MyIntent` intent (which you will need to define in your LUIS application model). 

The `BasicLuisDialog` object inherits from the [LuisDialog][LuisDialog] object, which contains the `StartAsync` and `MessageReceived` methods. When the dialog is instantiated, its `StartAsync` method runs and calls `IDialogContext.Wait` with the continuation delegate that will be called when there is a new message. In the initial case, there is an immediate message available (the one that launched the dialog) and the message is immediately passed to the `MessageReceived` method (in the `LuisDialog` object).

The `MessageReceived` method calls your LUIS application model to determine intent and then calls the appropriate intent handler in the `BasicLuisDialog` object. The handler processes the intent and then waits for the next message from the user.

[!code-csharp[BasicLuisDialog class](~/includes/code/azure-bot-service-template-language-understanding.cs#BasicLuisDialog)]

## Extend default functionality

The Language understanding template provides a good foundation that you can build upon to create a bot that is capable of using natural language models to understand user intent. To learn more about developing bots with LUIS in .NET, see [Enable language understanding with LUIS](~/dotnet/bot-builder-dotnet-luis-dialogs.md). To learn more about developing bots with LUIS in Node.js, see [Recognize user intent](~/nodejs/bot-builder-nodejs-recognize-intent.md).

## Additional resources

- [Create a bot with the Azure Bot Service](~/azure/azure-bot-service-quickstart.md)
- [Templates in the Azure Bot Service](~/azure/azure-bot-service-templates.md)
- <a href="https://www.luis.ai" target="_blank">LUIS</a>
- [Enable language understanding in .NET](~/dotnet/bot-builder-dotnet-luis-dialogs.md)
- [Recognize user intent in Node.js](~/nodejs/bot-builder-nodejs-recognize-intent.md)
- <a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">Bot Builder Samples GitHub repository</a>
- [Bot Builder SDK for .NET](~/dotnet/index.md)
- [Bot Builder SDK for Node.js](~/nodejs/index.md)

[LuisDialog]: https://docs.botframework.com//en-us/csharp/builder/sdkreference/d8/df9/class_microsoft_1_1_bot_1_1_builder_1_1_dialogs_1_1_luis_dialog.html
