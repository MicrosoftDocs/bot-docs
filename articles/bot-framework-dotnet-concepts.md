---
title: Key concepts in the Bot Builder SDK for .NET | Microsoft Docs
description: Learn about key concepts in the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, key concepts, core concepts, connector, activity, dialog
author: kbrandl
manager: rstand
ms.topic: develop-dotnet-article
ms.prod: botframework
ms.service: Bot Builder
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

For details about using the Connector via the Bot Builder SDK for .NET, see [Send and receive activities](bot-framework-dotnet-connector.md).

##<a id="activity"></a> Activity

[!include[Activity concept overview](../includes/snippet-dotnet-concept-activity.md)]
For details about Activities in the Bot Builder SDK for .NET, 
see [Activity types](bot-framework-dotnet-activities.md).

## Dialog

> [!NOTE]
> Content coming soon...

In the Bot Builder SDK for .NET, the <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html" target="_blank">Builder</a> library enables you to manage dialogs.

For details about using dialogs within the Bot Builder SDK for .NET, see 
[Dialogs](bot-framework-dotnet-dialogs.md).

### FormFlow

> [!NOTE]
> Content coming soon...

In the Bot Builder SDK for .NET, the <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html" target="_blank">Builder</a> library enables you to implement FormFlow.

For details about using FormFlow within the BotBuilder SDK for .NET, see [FormFlow](bot-framework-dotnet-formflow.md).

## State

[!include[State concept overview](../includes/snippet-dotnet-concept-state.md)]
For details about managing state using the Bot Builder SDK for .NET, 
see [Manage state](bot-framework-dotnet-state.md).

## Naming conventions

The Bot Builder SDK for .NET library uses strongly-typed, pascal-cased naming conventions. 
However, the JSON messages that are transported back and forth over the wire use camel-case naming conventions. 
For example, the C# property **ReplyToId** is serialized as **replyToId** in the JSON message that's 
transported over the wire.

## Security

You should ensure that your bot's endpoint can only be called by the Bot Framework Connector service. 
For more information on this topic, see [Security](bot-framework-dotnet-security.md).

## Additional resources

> [!NOTE]
> Content coming soon...




