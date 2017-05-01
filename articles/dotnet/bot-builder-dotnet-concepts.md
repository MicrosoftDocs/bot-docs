---
title: Key concepts in the Bot Builder SDK for .NET | Microsoft Docs
description: Understand the key concepts and tools for building and deploying conversational bots available in the Bot Builder SDK for .NET.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---

# Key concepts in the Bot Builder SDK for .NET

This article introduces key concepts in the Bot Builder SDK for .NET.

## Connector

The [Bot Framework Connector](~/dotnet/bot-builder-dotnet-connector.md) provides a single REST API that enables a bot to communicate across multiple channels such as Skype, Email, Slack, and more. It facilitates communication between bot and user by relaying messages from bot to channel and from channel to bot. 

In the Bot Builder SDK for .NET, the [Connector][connectorLibrary] library enables access to the Connector. 

## Activity

[!include[Activity concept overview](~/includes/snippet-dotnet-concept-activity.md)]
For details about Activities in the Bot Builder SDK for .NET, 
see [Activities overview](~/dotnet/bot-builder-dotnet-activities.md).

## Dialog

When you create a bot using the Bot Builder SDK for .NET, you can use [dialogs](~/dotnet/bot-builder-dotnet-dialogs.md) to model 
a conversation and manage [conversation flow](~/bot-design-conversation-flow.md#dialog-stack). 
A dialog can be composed of other dialogs to maximize reuse, and a dialog context maintains the [stack of dialogs](~/bot-design-conversation-flow.md) that are active in the conversation at any point in time. 
A conversation that comprises dialogs is portable across computers, which makes it possible for your bot implementation to scale. 

In the Bot Builder SDK for .NET, the [Builder][builderLibrary] library enables you to manage dialogs.

## FormFlow

You can use [FormFlow](~/dotnet/bot-builder-dotnet-formflow.md) within the Bot Builder SDK for .NET to streamline of building a bot that collects information from the user. 
For example, a bot that takes sandwich orders must collect several pieces of information from the user such as type of bread, choice of toppings, size, and so on. Given basic guidelines, FormFlow can automatically generate the dialogs necessary to manage a guided conversation like this.

## State

[!include[State concept overview](~/includes/snippet-dotnet-concept-state.md)]
For details about managing state using the Bot Builder SDK for .NET, 
see [Manage state](~/dotnet/bot-builder-dotnet-state.md).

## Naming conventions

The Bot Builder SDK for .NET library uses strongly-typed, Pascal-cased naming conventions. 
However, the JSON messages that are transported back and forth over the wire use camel-case naming conventions. 
For example, the C# property **ReplyToId** is serialized as **replyToId** in the JSON message that's 
transported over the wire.

## Security

You should ensure that your bot's endpoint can only be called by the Bot Framework Connector service. 
For more information on this topic, see [Secure your bot](~/dotnet/bot-builder-dotnet-security.md).

## Additional resources

- [Bot Builder SDK for .NET](~/dotnet/bot-builder-dotnet-overview.md)
- [Create a bot with the Bot Builder SDK for .NET](~/dotnet/bot-builder-dotnet-quickstart.md)
- [Activities overview](~/dotnet/bot-builder-dotnet-activities.md)
- [Dialogs](~/dotnet/bot-builder-dotnet-dialogs.md)
- [Design and control conversation flow](~/bot-design-conversation-flow.md)
- [Secure your bot](~/dotnet/bot-builder-dotnet-security.md)



[connectorLibrary]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/db/dbb/namespace_microsoft_1_1_bot_1_1_connector.html

[builderLibrary]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html