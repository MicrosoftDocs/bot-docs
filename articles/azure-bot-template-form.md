---
title: Set up a form bot for Azure Bot Service | Microsoft Docs
description: Learn how to set up a form for Azure Bot Service.
keywords: Bot Framework, Azure Bot Service, form bot
author: RobStand
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 3/21/2017
ms.reviewer:
#ROBOTS: Index
---

# Guide users through a conversation with the form bot

The form bot template demonstrates how to use a form to perform a guided conversation with the user. Typically, you would use a guided conversation when you need to ask the user a series of clarifying questions, like when ordering a sandwich with many options and ingredients.

Most messages will have a `Message` activity type that contains the text and attachments the user sent. If the message’s activity type is `Message`, the template posts the message to `MainDialog` in the context of the current message. The **MainDialog.csx** file contains the root dialog that controls the conversation with the user.

The routing of the message is identical to the one presented in the [Basic bot template](~/azure-bot-service/basic-bot.md).

[!code-csharp[Post message](~/includes/code/azure-form-bot.cs#postMessage)]

The `MainDialog` object inherits from the `BasicForm` dialog, which defines the form (see **BasicForm.csx**).  When the dialog is instantiated, the dialog’s `StartAsync` method runs and calls `IDialogContext.Wait` with the continuation delegate that’s called when there is a new message. In the initial case, there is an immediate message available (the one that launched the dialog) and the message is immediately passed to the `MessageReceivedAsync` method.

[!code-csharp[Message received](~/includes/code/azure-form-bot.cs#messageReceived)]

The `MessageReceivedAsync` method creates the form and starts asking the questions. The `Call` method starts the form and specifies the delegate that handles the completed form. After the `FormComplete` method finishes processing the user’s input, the method calls the `IDialogContext.Wait` method, which suspends the bot until it receives the next message.

[!code-csharp[Create form](~/includes/code/azure-form-bot.cs#createForm)]

The `BasicForm` object defines the form. The `public` properties define the questions to ask. The `Prompt` property attribute contains the prompt text that’s shown to the user. Anything within curly brackets ({}) are substitution characters. For example, {&} tells the form to use the property’s name in the prompt. If the property’s data type is an enumeration, {||} tells the form to display the enumeration’s values as the list of possible values that the user can choose from. For example, the data type for the `Color` property is `ColorOptions`. When the form asks the user for their favorite car color, the form will display **1. Red**, **2. White**, and **3. Blue** as possible values. For more information about substitution strings, see <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/forms.html#patterns" target="_blank">Pattern Language</a>.

[!code-csharp[Ask form questions](~/includes/code/azure-form-bot.cs#askQuestions)]


##Additional Resources

- <a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">Bot Builder Samples GitHub Repo </a>
- <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/" target="_blank">Bot Builder SDK C# Reference</a>- 
- <a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">Bot Builder SDK</a>
