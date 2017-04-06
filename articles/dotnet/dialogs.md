---
title: Dialogs in the Bot Builder SDK for .NET | Microsoft Docs
description: Learn about Dialogs in the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, dialog, conversation modeling
author: kbrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/17/2017
ms.reviewer:
#ROBOTS: Index
---

# Dialogs

This article explores the fundamentals of dialogs in the Bot Builder SDK for .NET. 



When you create a bot using the Bot Builder SDK for .NET, you can use dialogs to model 
a conversation and manage [conversation flow](~/bot-design-conversation-flow.md). 
Each dialog is an abstraction that encapsulates its own state in a C# class that implements `IDialog`. 
A dialog can be composed with other dialogs to maximize reuse, and a dialog context maintains the [stack of dialogs](~/bot-design-conversation-flow.md#stack) that are active in the conversation at any point in time. 

A conversation that comprises dialogs is portable across computers, which makes it possible for your bot implementation to scale. 
When you use dialogs in the Bot Builder SDK for .NET, conversation state 
(the dialog stack and the state of each dialog in the stack) is automatically stored 
using the Bot Framework State service. This enables your bot to be stateless, 
much like a web application that does not need to store session state in web server memory. 

## Examples

### Echo bot

Consider this echo bot example, which describes how to change the bot that's created in the 
[Get started](~/dotnet/getstarted.md) tutorial so that it uses dialogs to 
exchange messages with the user.

> [!TIP]
> To follow along with this example, use the instructions in the 
> [Get started](~/dotnet/getstarted.md) tutorial to create a bot, and then 
> update its **MessagesController.cs** file as described below.

#### MessagesController.cs 

In the Bot Builder SDK for .NET, the <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html" target="_blank">Builder</a> library enables you to implement dialogs. 
To access the relevant classes, import the `Dialogs` namespace.

[!code-csharp[Using statement](~/includes/code/dotnet-dialogs.cs#usingStatement)]

Next, add this `EchoDialog` class to **MessagesController.cs** to represent the conversation. 

[!code-csharp[EchoDialog class](~/includes/code/dotnet-dialogs.cs#echobot1)]

Then, wire the `EchoDialog` class to the `Post` method by calling the `Conversation.SendAsync` method.

[!code-csharp[Post method](~/includes/code/dotnet-dialogs.cs#echobot2)]

#### Implementation details 

The `Post` method is marked `async` because Bot Builder uses the C# facilities for handling 
asynchronous communication. 
It returns a `Task` object, which represents the task that is responsible for sending replies to the 
passed-in message. 
If there is an exception, the `Task` that is returned by the method will contain the exception information. 

The `Conversation.SendAsync` method is key to implementing dialogs with the Bot Builder SDK 
for .NET. It follows the <a href="https://en.wikipedia.org/wiki/Dependency_inversion_principle" target="_blank">dependency inversion principle</a> and performs these steps:

1. Instantiates the required components  
2. Deserializes the conversation state (the dialog stack and the state of each dialog in the stack) from `IBotDataStore`
3. Resumes the conversation process where the bot suspended and waits for a message
4. Sends the replies
5. Serializes the updated conversation state and saves it back to `IBotDataStore`

When the conversation first starts, the dialog does not contain state, 
so `Conversation.SendAsync` constructs `EchoDialog` and calls its `StartAsync` method. 
The `StartAsync` method calls `IDialogContext.Wait` with the continuation delegate 
to specify the method that should be called when a new message is received (`MessageReceivedAsync`). 

The `MessageReceivedAsync` method waits for a message, posts a response, and waits for the next message. 
Every time `IDialogContext.Wait` is called, the bot enters a suspended state and can be restarted on any 
computer that receives the message. 

A bot that's created by using the code samples above will reply to each message that the user sends by simply 
echoing back the user's message prefixed with the text 'You said: '. 
Because the bot is created using dialogs, it can evolve to support more complex conversations without having 
to explicitly manage state.

### Echo bot with state

This next example builds upon the one above by adding the ability to track dialog state. 
When the `EchoDialog` class is updated as shown in the code sample below, 
the bot will reply to each message that the user sends by echoing back the user's 
message prefixed with a number (`count`) followed by the text 'You said: '. 
The bot will continue to increment `count` with each reply, until the user elects to reset the count.

#### MessagesController.cs 

[!code-csharp[EchoDialog class](~/includes/code/dotnet-dialogs.cs#echobot3)]

#### Implementation details

As in the first example, the `MessageReceivedAsync` method is called when a new message is received. 
This time though, the `MessageReceivedAsync` method evaluates the user's message before responding. 
If the user's message is "reset", the built-in `Prompts.Confirm` prompt spawns a sub-dialog that 
asks the user to confirm the count reset. 
The sub-dialog has its own private state that does not interfere with the parent dialog's state. 
When the user responds to the prompt, the result of the sub-dialog is passed to the `AfterResetAsync` method, 
which sends a message to the user to indicate whether or not the count was reset and then 
calls `IDialogContext.Wait` with a continuation back to `MessageReceivedAsync` on the next message.

## Dialog context

The `IDialogContext` interface that is passed into each dialog method 
provides access to the services that a dialog requires to save state and communicate with the channel. 
The `IDialogContext` interface comprises three interfaces: 
<a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/db/d9b/interface_microsoft_1_1_bot_1_1_builder_1_1_dialogs_1_1_internals_1_1_i_bot_data.html" target="_blank">Internals.IBotData</a>, 
<a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/d9/d2c/interface_microsoft_1_1_bot_1_1_builder_1_1_dialogs_1_1_internals_1_1_i_bot_to_user.html" target="blank">Internals.IBotToUser</a>, 
and <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/de/db4/interface_microsoft_1_1_bot_1_1_builder_1_1_dialogs_1_1_internals_1_1_i_dialog_stack.html" target="_blank">Internals.IDialogStack</a>. 

### Internals.IBotData

The `Internals.IBotData` interface provides access to the 
per-user, per-conversation, and private conversation state data that's maintained by Connector. 
Per-user state data is useful for storing user data that is not related to a specific conversation, 
while per-conversation data is useful for storing general data about a conversation, and 
private conversation data is useful for storing user data that is related to a specific conversation. 

### Internals.IBotToUser

`Internals.IBotToUser` provides methods to send a message from bot to user. 
Messages may be sent inline with the response to the web API method call or 
directly by using the [Connector client](~/dotnet/connector.md#create-client). 
Sending and receiving messages through the dialog context ensures that the `Internals.IBotData` state is passed through the Connector.

### Internals.IDialogStack

`Internals.IDialogStack` provides methods to manage the [dialog stack](~/bot-design-conversation-flow.md#stack). A majority of the time, the dialog stack will 
automatically be managed for you. However, there may be cases where you want to explictly manage the stack. 
For example, you might want to call a child dialog and add it to the 
top of the dialog stack, mark the current dialog as complete (thereby removing it from the dialog stack and returning the result to the prior dialog in the stack), 
suspend the current dialog until a message from the user arrives, or even reset the dialog stack altogether.

## Serialization

The dialog stack and the state of all active dialogs are serialized to the per-user, per-conversation <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/d7/dea/interface_microsoft_1_1_bot_1_1_builder_1_1_dialogs_1_1_i_bot_data_bag.html" target="_new">IBotDataBag</a>. 
The serialized blob is persisted in the messages that the bot sends to and receives from 
the [Connector](~/dotnet/concepts.md#connector). 
To be serialized, a `Dialog` class must include the `[Serializable]` attribute. 
All `IDialog` implementations in the <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html" target="_blank">Builder library</a> are marked as serializable. 

The [Chain methods](#dialog-chains) provide a fluent interface to dialogs that is usable in LINQ query syntax. 
The compiled form of LINQ query syntax often uses anonymous methods. 
If these anonymous methods do not reference the environment of local variables, then these anonymous methods have no state and are trivially serializable. 
However, if the anonymous method captures any local variable in the environment, 
the resulting closure object (generated by the compiler) is not marked as serializable. 
In this situation, Bot Builder will throw a `ClosureCaptureException` to identify the issue.

To use reflection to serialize classes that are not marked as serializable, the 
Builder library includes a reflection-based serialization surrogate that you can use to register with 
<a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/df/dd3/namespace_microsoft_1_1_bot_1_1_builder_1_1_autofac.html" target="_blank">Autofac</a>.

[!code-csharp[Serialization](~/includes/code/dotnet-dialogs.cs#serialization)]

##<a id="dialog-chains"></a> Dialog chains

While you can explicitly manage the stack of active dialogs by using `IDialogStack.Call<R>` and `IDialogStack.Done<R>`, you can also implicitly manage the stack of 
active dialogs by using these fluent 
<a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/de/dab/class_microsoft_1_1_bot_1_1_builder_1_1_dialogs_1_1_chain.html" target="_blank">Chain</a> methods.

| Method | Type | Notes |
|----|----|----|
|Chain.Select<T, R>| LINQ | Supports "select" and "let" in LINQ query syntax.|
|Chain.SelectMany<T, C, R>   | LINQ    | Supports successive "from" in LINQ query syntax.|
|Chain.Where<T>              | LINQ    | Supports "where" in LINQ query syntax.|
|Chain.From<T>               | Chains  | Instantiates a new instance of a dialog.|
|Chain.Return<T>             | Chains  | Returns a constant value into the chain.|
|Chain.Do<T>                 | Chains  | Allows for side-effects within the chain.|
|Chain.ContinueWith<T, R>    | Chains  | Simple chaining of dialogs.|
|Chain.Unwrap<T>             | Chains  | Unwrap a dialog nested in a dialog.|
|Chain.DefaultIfException<T> | Chains  | Swallows an exception from the previous result and returns default(T).|
|Chain.Loop<T>               | Branch  | Loops the entire chain of dialogs.|
|Chain.Fold<T>               | Branch  | Folds results from an enumeration of dialogs into a single result.|
|Chain.Switch<T, R>          | Branch  | Supports branching into different dialog chains.|
|Chain.PostToUser<T>         | Message | Posts a message to the user.|
|Chain.WaitToBot<T>          | Message | Waits for a message to the bot.|
|Chain.PostToChain<T>        | Message | Starts a chain with a message from the user.|

### Examples 

The LINQ query syntax uses the `Chain.Select<T, R>` method.

[!code-csharp[Chain.Select](~/includes/code/dotnet-dialogs.cs#chain1)]

Or the `Chain.SelectMany<T, C, R>` method.

[!code-csharp[Chain.SelectMany](~/includes/code/dotnet-dialogs.cs#chain2)]

The `Chain.PostToUser<T>` and `Chain.WaitToBot<T>` methods post messages from the bot to the user and vice versa.

[!code-csharp[Chain.PostToUser](~/includes/code/dotnet-dialogs.cs#chain3)]

The `Chain.Switch<T, R>` method branches the conversation dialog flow.

[!code-csharp[Chain.Switch](~/includes/code/dotnet-dialogs.cs#chain4)]

If `Chain.Switch<T, R>` returns a nested `IDialog<IDialog<T>>`, then the inner `IDialog<T>` can be unwrapped with `Chain.Unwrap<T>`. This allows branching conversations to different paths of chained dialogs, possibly of unequal length. This example shows a more complete example of branching dialogs written in the fluent chain style with implicit stack management.

[!code-csharp[Chain.Switch](~/includes/code/dotnet-dialogs.cs#chain5)]

## Additional resources

- [Key concepts in the Bot Builder SDK for .NET](~/dotnet/concepts.md)
- [Designing conversation flow](~/bot-design-conversation-flow.md)
- [Manage conversation flow using dialogs](~/dotnet/manage-conversation-flow.md)
- [Integrate LUIS with dialogs](~/dotnet/luis-dialogs.md)
- [Bot Framework troubleshooting guide](~/troubleshoot-general-problems.md#implement-dialogs)
- <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html" target="_blank">Builder library</a>