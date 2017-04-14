---
title: Key concepts in the Bot Builder SDK for .NET | Microsoft Docs
description: Explore key concepts for the Bot Builder SDK for .NET.
author: kbrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/08/2017
ms.reviewer:
ROBOTS: Index, Follow
---

# Key concepts in the Bot Builder SDK for .NET

This article introduces key concepts in the Bot Builder SDK for .NET.

## Connector

The Bot Framework Connector provides a single REST API that enables a bot to 
communicate across multiple channels such as Skype, Email, Slack, and more. 
It facilitates communication between bot and user, 
by relaying messages from bot to channel and from channel to bot. 
In the Bot Builder SDK for .NET, the [Connector][connectorLibrary] library enables access to the Connector. 

For details about using the Connector via the Bot Builder SDK for .NET, see [Send and receive activities](~/dotnet/connector.md).

## Activity

[!include[Activity concept overview](~/includes/snippet-dotnet-concept-activity.md)]
For details about Activities in the Bot Builder SDK for .NET, 
see [Activity types](~/dotnet/activities.md).

## Dialog

When you create a bot using the Bot Builder SDK for .NET, you can use dialogs to model 
a conversation and manage [conversation flow](~/bot-design-conversation-flow.md). 
A dialog can be composed with other dialogs to maximize reuse, and a dialog context maintains the [stack of dialogs](~/bot-design-conversation-flow.md) that are active in the conversation at any point in time. 
A conversation that comprises dialogs is portable across computers, which makes it possible for your bot implementation to scale. In the Bot Builder SDK for .NET, the [Builder][builderLibrary] library enables you to manage dialogs.

For details about using dialogs within the Bot Builder SDK for .NET, see 
[Dialogs](~/dotnet/dialogs.md).

## FormFlow

You can use FormFlow within the Bot Builder SDK for .NET to streamline the process of building a bot that 
is designed to collect a specific set of information from the user. For example, you might use FormFlow to 
create a bot that is designed to obtain a user's sandwich order. In this scenario, the bot 
needs to collect several pieces of information from the user such as type of bread, choice of toppings, 
size of sandwich, etc. 
FormFlow can automatically generate the dialogs that are necessary to manage a guided conversation like this, 
based upon guidelines that you specify. 

For details about using FormFlow within the BotBuilder SDK for .NET, see [FormFlow](~/dotnet/formflow.md).

## State

[!include[State concept overview](~/includes/snippet-dotnet-concept-state.md)]
For details about managing state using the Bot Builder SDK for .NET, 
see [Manage state](~/dotnet/state.md).

## Naming conventions

The Bot Builder SDK for .NET library uses strongly-typed, pascal-cased naming conventions. 
However, the JSON messages that are transported back and forth over the wire use camel-case naming conventions. 
For example, the C# property **ReplyToId** is serialized as **replyToId** in the JSON message that's 
transported over the wire.

## Security

You should ensure that your bot's endpoint can only be called by the Bot Framework Connector service. 
For more information on this topic, see [Secure your bot](~/dotnet/security.md).

## Additional resources

- [Bot Builder SDK for .NET](~/dotnet/index.md)
- [Create a bot with the Bot Builder SDK for .NET](~/dotnet/getstarted.md)
- [Activity types](~/dotnet/activities.md)
- [Dialogs](~/dotnet/dialogs.md)
- [Designing conversation flow](~/bot-design-conversation-flow.md)
- [Secure your bot](~/dotnet/security.md)



[connectorLibrary]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/db/dbb/namespace_microsoft_1_1_bot_1_1_connector.html

[builderLibrary]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html