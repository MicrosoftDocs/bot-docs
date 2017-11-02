---
title: Key concepts in the Bot Builder SDK for .NET | Microsoft Docs
description: Understand the key concepts and tools for building and deploying conversational bots available in the Bot Builder SDK for .NET.
author: kbrandl
ms.author: kibrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 08/04/2017
ms.reviewer:
---

# Key concepts in the Bot Builder SDK for .NET
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-concepts.md)
> - [Node.js](../nodejs/bot-builder-nodejs-concepts.md)

This article introduces key concepts in the Bot Builder SDK for .NET.

## Connector

The [Bot Framework Connector](bot-builder-dotnet-connector.md) provides a single REST API that enables a bot to communicate across multiple channels such as Skype, Email, Slack, and more. It facilitates communication between bot and user by relaying messages from bot to channel and from channel to bot. 

In the Bot Builder SDK for .NET, the [Connector][connectorLibrary] library enables access to the Connector. 

## Activity

[!include[Activity concept overview](../includes/snippet-dotnet-concept-activity.md)]
For details about Activities in the Bot Builder SDK for .NET, 
see [Activities overview](bot-builder-dotnet-activities.md).

## Dialog

When you create a bot using the Bot Builder SDK for .NET, you can use [dialogs](bot-builder-dotnet-dialogs.md) to model 
a conversation and manage [conversation flow](../bot-design-conversation-flow.md#dialog-stack). 
A dialog can be composed of other dialogs to maximize reuse, and a dialog context maintains the [stack of dialogs](../bot-design-conversation-flow.md) that are active in the conversation at any point in time. 
A conversation that comprises dialogs is portable across computers, which makes it possible for your bot implementation to scale. 

In the Bot Builder SDK for .NET, the [Builder][builderLibrary] library enables you to manage dialogs.

## FormFlow

You can use [FormFlow](bot-builder-dotnet-formflow.md) within the Bot Builder SDK for .NET to streamline of building a bot that collects information from the user. 
For example, a bot that takes sandwich orders must collect several pieces of information from the user such as type of bread, choice of toppings, size, and so on. Given basic guidelines, FormFlow can automatically generate the dialogs necessary to manage a guided conversation like this.

## State

[!include[State concept overview](../includes/snippet-dotnet-concept-state.md)]
For details about managing state using the Bot Builder SDK for .NET, 
see [Manage state data](bot-builder-dotnet-state.md).

## Naming conventions

The Bot Builder SDK for .NET library uses strongly-typed, Pascal-cased naming conventions. 
However, the JSON messages that are transported back and forth over the wire use camel-case naming conventions. 
For example, the C# property **ReplyToId** is serialized as **replyToId** in the JSON message that's 
transported over the wire.

## Security

You should ensure that your bot's endpoint can only be called by the Bot Framework Connector service. 
For more information on this topic, see [Secure your bot](bot-builder-dotnet-security.md).

## Next steps

Now you know the concepts behind every bot. You can quickly [build a bot using Visual Studio](bot-builder-dotnet-quickstart.md) using a template. Next, study each key concept in more detail, starting with dialogs.

> [!div class="nextstepaction"]
> [Dialogs in the Bot Builder SDK for .NET](bot-builder-dotnet-dialogs.md)

[connectorLibrary]: https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.connector?view=botbuilder-3.11.0

[builderLibrary]: https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.dialogs?view=botbuilder-3.11.0