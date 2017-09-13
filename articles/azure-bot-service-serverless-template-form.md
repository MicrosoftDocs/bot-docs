---
title: Create a form bot using Azure Bot Service | Microsoft Docs
description: Learn how to create a form bot using Azure Bot Service.
author: kbrandl
ms.author: kibrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 08/04/2017
ms.reviewer: 
---

# Create a bot using the Form template

To create a bot that collects input from a user via a guided conversation, choose the [Form template](azure-bot-service-templates.md) when creating the bot using Azure Bot Service. A form bot is designed to collect a specific set of information from the user. For example, a bot that is designed to obtain a user's sandwich order would need to collect information such as type of bread, choice of toppings, size of sandwich, etc. This article provides a walkthrough of the code that is automatically generated when you create a bot using the Form template and describes some ways in which you might extend the bot's default functionality.

> [!NOTE]
> If you choose C# as your development language, the bot will use [FormFlow](~/dotnet/bot-builder-dotnet-formflow.md) to manage 
> the guided conversation. If you choose Node.js as your development language, the bot will use 
> [waterfalls](~/nodejs/bot-builder-nodejs-prompts.md) to manage the guided conversation. 
> A bot that is created using the Form template routes messages in the same manner as described for the 
> [Basic template](azure-bot-service-serverless-template-basic.md).

> [!TIP]
> This article examines the source code provided by the Form  template for serverless bots. For examination of the Form template for a web app C# bot, see
> [Basic features of FormFlow](~/dotnet/bot-builder-dotnet-formflow.md).


## Code walkthrough

Most activities that the bot receives will be of [type](~/dotnet/bot-builder-dotnet-activities.md) `Message` and will will contain the text and attachments that the user sent to the bot. To process an incoming message, the bot posts the message to `MainDialog` (in **MainDialog.csx**). 

[!code-csharp[process Message activity](~/includes/code/azure-bot-service-serverless-template-form.cs#processMessage)]

### MainDialog.csx

`MainDialog` inherits from the `BasicForm` dialog (in **BasicForm.csx**), which defines the form. **MainDialog.csx** contains the root dialog that controls the conversation with the user. When the dialog is instantiated, its `StartAsync` method runs and calls `IDialogContext.Wait` with the continuation delegate that will be called when there is a new message. In the initial case, there is an immediate message available (the one that launched the dialog) and the message is immediately passed to the `MessageReceivedAsync` method.

[!code-csharp[MainDialog](~/includes/code/azure-bot-service-serverless-template-form.cs#mainDialog)]

The `MessageReceivedAsync` method creates the form and begins to ask the user the questions that are defined by the form. The `Call` method starts the form and specifies the delegate that handles the completed form. When the `FormComplete` method finishes processing the user's input, it calls the `IDialogContext.Wait` method to suspend the bot until it receives the next message.

[!code-csharp[Collect input from user](~/includes/code/azure-bot-service-serverless-template-form.cs#collectUserInput)]

### BasicForm.csx

The `BasicForm` class defines the form. Its public properties define the information that the bot needs to collect from the user. Each `Prompt` property uses [Pattern Language](~/dotnet/bot-builder-dotnet-formflow-pattern-language.md) to customize the text that prompts the user for the corresponding piece of information.

[!code-csharp[BasicForm](~/includes/code/azure-bot-service-serverless-template-form.cs#basicForm)]

## Extend default functionality

The Form template provides a good foundation that you can build upon and customize to create a bot that collects input from a user via a guided conversation. For example, you could edit the `BasicForm` class within **BasicForm.csx** to define the set of information that your bot needs to collect from the user. To learn more about FormFlow, see [FormFlow in .NET](~/dotnet/bot-builder-dotnet-formflow.md).

## Additional resources

- [Create a bot with the Azure Bot Service](azure-bot-service-quickstart.md)
- [Templates in the Azure Bot Service](azure-bot-service-templates.md)
- [Basic features of FormFlow (.NET)](~/dotnet/bot-builder-dotnet-formflow.md) 
- [Prompts and waterfalls (Node.js)](~/nodejs/bot-builder-nodejs-prompts.md)
- <a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">Bot Builder Samples GitHub repository</a>
- [Bot Builder SDK for .NET](~/dotnet/bot-builder-dotnet-overview.md)
- [Bot Builder SDK for Node.js](~/nodejs/index.md)
