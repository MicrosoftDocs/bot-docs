---
title: Set up a basic bot for Azure Bot Service | Microsoft Docs
description: Learn how to set up basic for Azure Bot Service.
keywords: Bot Framework, Azure Bot Service, basic bot
author: Toney001
manager: rstand
ms.topic: bot-service-article
ms.prod: botframework
ms.service: Azure Bot Service
ms.date: 3/21/2017
ms.reviewer:
#ROBOTS: Index
---

#Basic bot

The basic bot template shows how to use dialogs to respond to user input. For information about using dialogs, see [Dialogs](bot-framework-dotnet-dialogs.md).
When a user posts a message, it’s sent to the bot’s `Run` method in the **Run.csx** file. Before the bot processes the message, it first <a href="https://docs.botframework.com/en-us/restapi/authentication/" target="_blank">authenticates the request</a>. If the validation fails, the bot responds with "Unauthorized". 

The following shows the code that the template uses to authenticate the request.

[!code-csharp[Authenticate request](../includes/code/azure-basic-bot.cs#authenticateRequest)]

If the request is authentic, the bot processes the message. The message includes an activity type, which the bot uses to decide what action to take. You would update this code if you want to respond to the other activity types. For example, if you save the user state, you’ll need to respond to the `DeleteUserData` message.

[!code-csharp[Process message](../includes/code/azure-basic-bot.cs#processMessage)]

The first message that the channel sends to the bot is a `ConversationUpdate` message that contains the users that are in the conversation. The template shows how you would use this message to welcome the users to the conversation. Because the bot is considered a user in the conversation, the list of users will include the bot. The code identifies the bot and ignores it when welcoming the other users.

[!code-csharp[Conversation update](../includes/code/azure-basic-bot.cs#conversationUpdate)]

Most messages will have a `Message` activity type, and will contain the text and attachments that the user sent. If the message’s activity type is `Message`, the template posts the message to `EchoDialog` in the context of the current message (see **EchoDialog.csx**).

[!code-csharp[Get activity type](../includes/code/azure-basic-bot.cs#getActivityType)]

 The **EchoDialog.csx** file contains the dialog that controls the conversation with the user. When the dialog’s instantiated, the dialog’s `StartAsync` method runs and calls `IDialogContext.Wait` with the continuation delegate that’s called when there is a new message. In the initial case, there is an immediate message available (the one that launched the dialog) and the message is immediately passed to the `MessageReceivedAsync` method.

[!code-csharp[Show Dialog](../includes/code/azure-basic-bot.cs#showDialog)]

The `MessageReceivedAsync` method echoes the user’s input and counts the number of interactions with the user. If the user’s input is the word "reset", the method uses <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/d9/d03/class_microsoft_1_1_bot_1_1_builder_1_1_dialogs_1_1_prompt_dialog.html" target="_blank">PromptDialog</a> to confirm that the user wants to reset the counter. The method calls the `IDialogContext.Wait` method which suspends the bot until it receives the next message.

[!code-csharp[Set counter](../includes/code/azure-basic-bot.cs#setCounter)]


The `AfterResetAsync` method processes the user’s response to `PromptDialog`. If the user confirms the request, the counter is reset and a message is sent to the user confirming the action. The method then calls the `IDialogContext.Wait` method to suspend the bot until it receives the next message, which gets processed by `MessageReceivedAsync`.

[!code-csharp[Send message](../includes/code/azure-basic-bot.cs#sendMessage)]

  ##Next steps
Here are a few things you might consider as next steps when updagint the code.
- Add other prompts to get information from the user.
- Add attachments such as cards to provide a richer user experience.
- Chain together other dialogs (see Dialog Chains).

##Resources

<a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">Bot Builder Samples GitHub Repo</a>
<a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/" target="_blank">Bot Builder SDK C# Reference</a>
<a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">Bot Builder SDK</a>
<a href="https://www.luis.ai/Help" target="_blank">LUIS documentation</a>

