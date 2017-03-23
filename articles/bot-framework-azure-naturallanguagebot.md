---
title: Set up a language understanding bot for Azure Bot Service | Microsoft Docs
description: Learn how to set up language understanding for Azure Bot Service.
keywords: Bot Framework, Azure Bot Service, natural language bot. language understanding bog
author: Toney001
manager: rstand
ms.topic: bot-service-article
ms.prod: botframework
ms.service: Azure Bot Service
ms.date: 3/21/2017
ms.reviewer:
#ROBOTS: Index
---

#Language understanding bot


The language understanding bot template shows how to use natural language models (LUIS) to understand user intent. For example, when the user asks your bot to “get news about virtual reality companies,” your bot needs to understand what the user is asking for. LUIS is designed to enable you to very quickly deploy an HTTP endpoint that will interpret the user’s input in terms of:
- The intention the user input conveys, such as "find news".
- The key entities that are present, such as "virtual reality companies" 
LUIS lets you custom design the set of intentions and entities that are relevant to the application, and then guides you through the process of building a language understanding application that returns information about, in the example above, news regarding virtual companies.

When you create the template, Azure Bot Service creates an empty LUIS application for you that always returns "None" initially. These are the typical steps you take:

1. Sign in to LUIS.
2. Click **My applications**.
3. Select the application that the service created for you.
4. Update your model by creating new intents, and then train and publish your LUIS app.

Most messages will have a `Message` activity type that contains the text and attachments the user sent. If the message’s activity type is `Message`, the template posts the message to `BasicLuisDialog` in the context of the current message (see **BasicLuisDialog.csx**).

The routing of the message is identical to the one presented in the [Basic bot template](bot-framework-azure-basic-bot.md).

[!code-csharp[Post Message](../includes/code/azure-understanding-language.cs#postMessage)]

The **BaiscLuisDialog.csx** file contains the root dialog that controls the conversation with the user. The `BasicLuisDialog` object defines an intent method handler for each intent that you define in your LUIS model. 

The naming convention for intent handlers is:

\<intent name\>+Intent. 

`NonIntent` is the name for the intent. In the example below, the dialog will handle the "None" intent which LUIS returns if it cannot determine the intent. You will still need to define the "MyIntent" intent in your LUIS model. The `LuisIntent` method attribute defines the method as an intent handler. Keep in mind, that the name in the `LuisIntent` attribute must match the name you used in your model.

The `BasicLuisDialog` object inherits from the <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/d8/df9/class_microsoft_1_1_bot_1_1_builder_1_1_dialogs_1_1_luis_dialog.html/" target="_blank">LuisDialog</a> object. The `LuisDialog` object contains the `StartAsync` and `MessageReceived` methods. When the dialog’s instantiated, the dialog’s `StartAsync` method runs and calls `IDialogContext.Wait` with the continuation delegate that’s called when there is a new message. In the initial case, there is an immediate message available (the one that launched the dialog) and the message is immediately passed to the `MessageReceived` method in the `LuisDialog` object.

The `MessageReceived` method calls your LUIS application model to determine intent and then calls the appropriate intent handler in the `BasicLuisDialog` object. The handler processes the intent and then waits for the next message from the user.

[!code-csharp[Receive Message](../includes/code/azure-understanding-language.cs#receiveMessage)]

##Resources

<a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">Bot Builder Samples GitHub Repo</a>
<a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/" target="_blank">Bot Builder SDK C# Reference</a>
<a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">Bot Builder SDK</a>
<a href="https://www.luis.ai/Help" target="_blank">LUIS documentation</a>



[LuisDialogLink]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d8/df9/class_microsoft_1_1_bot_1_1_builder_1_1_dialogs_1_1_luis_dialog.html/