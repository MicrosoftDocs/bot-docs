---
title: Key concepts in the Bot Builder SDK for .NET | Microsoft Docs
description: Learn about key concepts in the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, key concepts, core concepts, connector, activity, dialog
author: kbrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/08/2017
ms.reviewer:
#ROBOTS: Index
---

# Key concepts in the Bot Builder SDK for .NET

This article introduces key concepts in the Bot Builder SDK for .NET.

##<a id="connector"></a> Connector

The Bot Framework Connector provides a single REST API that enables a bot to 
communicate across multiple channels such as Skype, Email, Slack, and more. 
It facilitates communication between bot and user, 
by relaying messages from bot to channel and from channel to bot. 
In the Bot Builder SDK for .NET, the <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/db/dbb/namespace_microsoft_1_1_bot_1_1_connector.html" target="_blank">Connector</a> library enables access to the Connector. 

For details about using the Connector via the Bot Builder SDK for .NET, see [Send and receive activities](~/dotnet/connector.md).

##<a id="activity"></a> Activity

[!include[Activity concept overview](~/includes/snippet-dotnet-concept-activity.md)]
For details about Activities in the Bot Builder SDK for .NET, 
see [Activity types](~/dotnet/activities.md).

## Dialog

When you create a bot using the Bot Builder SDK for .NET, you can use dialogs to model 
a conversation and manage [conversation flow](~/design/core-dialogs.md). 
A dialog can be composed with other dialogs to maximize reuse, and a dialog context maintains the [stack of dialogs](~/design/core-dialogs.md#stack) that are active in the conversation at any point in time. 
A conversation that comprises dialogs is portable across computers, which makes it possible for your bot implementation to scale. In the Bot Builder SDK for .NET, the <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html" target="_blank">Builder</a> library enables you to manage dialogs.

For details about using dialogs within the Bot Builder SDK for .NET, see 
[Dialogs](~/dotnet/dialogs.md).

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
For more information on this topic, see [Security](~/dotnet/security.md).

## Additional resources

- [Bot Builder SDK for .NET](~/dotnet/index.md)
- [Create a bot with the Bot Builder SDK for .NET](~/dotnet/getstarted.md)
- [Activity types](~/dotnet/activities.md)
- [Dialogs](~/dotnet/dialogs.md)
- [Designing conversation flow](~/design/core-dialogs.md)
- [Security](~/dotnet/security.md)



