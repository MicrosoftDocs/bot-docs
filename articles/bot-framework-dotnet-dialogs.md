---
title: Dialogs in the Bot Builder SDK for .NET | Microsoft Docs
description: Learn about Dialogs in the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, dialog, conversation modeling
author: kbrandl
manager: rstand
ms.topic: develop-dotnet-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 03/17/2017
ms.reviewer:
#ROBOTS: Index
---

> [!WARNING]
> The content in this article is still under development. The article may have errors in content, 
> formatting, and copy. The content may change dramatically as the article is developed.

# Dialogs

This article explores the fundamentals of dialogs in the Bot Builder SDK for .NET and walks through 
two examples of bots that use dialogs to manage conversation flow. 

## Introduction

When you create a bot using the Bot Builder SDK for .NET, you can use dialogs to model 
a conversation and manage [conversation flow](bot-framework-design-core-dialogs.md). 
Each dialog is an abstraction that encapsulates its own state in a C# class that implements **IDialog**. 
A dialog can be composed with other dialogs to maximize reuse, and a dialog context maintains the [stack of dialogs](bot-framework-design-core-dialogs.md#stack) that are active in the conversation at any point in time. 

A conversation that is composed of dialogs is portable across computers, which makes it possible for your bot implementation to scale. 
When you use dialogs in the Bot Builder SDK for .NET, conversation state 
(the dialog stack and the state of each dialog in the stack) is automatically stored 
using the Bot Framework State service. This enables your bot to be stateless, 
much like a web application that does not need to store session state in web server memory. 

## Echo bot

Consider the following example, which describes how to change the echo bot in the 
[Get started](bot-framework-dotnet-getstarted.md) tutorial so that it uses dialogs instead of the 
<a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/db/dbb/namespace_microsoft_1_1_bot_1_1_connector.html" target="_blank">Connector</a> library to exchange messages with the user.

> [!TIP]
> To follow along with this example, use the instructions in the 
> [Get started](bot-framework-dotnet-getstarted.md) tutorial to create a bot, and then 
> update its **MessagesController.cs** file as described below.

### MessagesController.cs 

In the Bot Builder SDK for .NET, the <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html" target="_blank">Builder</a> library enables you to implement dialogs. 
To access the relevant classes, import the **Dialogs** namespace.

[!code-csharp[Using statement](../includes/code/dotnet-dialogs.cs#usingStatement)]

Next, add the following class to **MessagesController.cs** to represent the conversation. 

[!code-csharp[EchoDialog class](../includes/code/dotnet-dialogs.cs#echobot1)]

Then, wire the **EchoDialog** class to the **Post** method by calling the **Conversation.SendAsync** method.

[!code-csharp[Post method](../includes/code/dotnet-dialogs.cs#echobot2)]

### Implementation details 

The **Post** method is marked **async** because Bot Builder uses the C# facilities for handling 
asynchronous communication. 
It returns a **Task** object, which represents the task that is responsible for sending replies to the 
passed-in message. 
If there is an exception, the **Task** that is returned by the method will contain the exception information. 

The **Conversation.SendAsync** method is key to implementing dialogs with the Bot Builder SDK 
for .NET. It follows the <a href="https://en.wikipedia.org/wiki/Dependency_inversion_principle" target="_blank">dependency inversion principle</a> and performs the following steps:

1. Instantiates the required components  
2. Deserializes the conversation state (the dialog stack and the state of each dialog in the stack) from **IBotDataStore**
3. Resumes the conversation process where the bot suspended and waits for a message
4. Sends the replies
5. Serializes the updated conversation state and saves it back to **IBotDataStore**

When the conversation first starts, the dialog does not contain state, 
so **Conversation.SendAsync** constructs **EchoDialog** and calls its **StartAsync** method. 
The **StartAsync** method calls **IDialogContext.Wait** with the continuation delegate 
to specify the method that should be called when a new message is received (**MessageReceivedAsync**). 

The **MessageReceivedAsync** method waits for a message, posts a response, and waits for the next message. 
Every time **IDialogContext.Wait** is called, the bot enters a suspended state and can be restarted on any 
computer that receives the message. 

A bot that's created by using the code samples above will reply to each message that the user sends by simply 
echoing back the user's message prefixed with the text 'You said: '. 
Because the bot is created using dialogs, it can evolve to support more complex conversations without having 
to explicitly manage state.

## Echo bot with state

This next example builds upon the one above by adding the ability to track dialog state. 
If you update the **EchoDialog** class as shown in the following code sample, 
the bot will reply to each message that the user sends by echoing back the user's 
message prefixed with a number (**count**) followed by the text 'You said: '. 
The bot will continue to increment **count** with each reply, until the user elects to reset the count.

### MessagesController.cs 

[!code-csharp[EchoDialog class](../includes/code/dotnet-dialogs.cs#echobot3)]

### Implementation details

As in the first example, the **MessageReceivedAsync** method is called when a new message is received. 
This time though, the **MessageReceivedAsync** method evaluates the user's message before responding. 
If the user's message is "reset", the built-in **Prompts.Confirm** prompt spawns a sub-dialog that 
asks the user to confirm the count reset. 
The sub-dialog has its own private state that does not interfere with the parent dialog's state. 
When the user responds to the prompt, the result of the sub-dialog is passed to the **AfterResetAync** method, 
which sends a message to the user to indicate whether or not the count was reset and then 
calls **IDialogContext.Wait** with a continuation back to **MessageReceivedAsync** on the next message.

## Additional resources

- [Key concepts in the Bot Builder SDK for .NET](bot-framework-dotnet-concepts.md)
- [Designing conversation flow](bot-framework-design-core-dialogs.md)
- [Manage conversation flow using dialogs](bot-framework-dotnet-howto-manage-conversation-flow.md)
- [Bot Framework troubleshooting guide](bot-framework-troubleshooting-guide.md#implement-dialogs)
- <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html" target="_blank">Builder</a> library