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

The Bot Framework Connector service provides a single API that enables a bot to 
communicate across multiple channels such as Skype, Email, Slack, and more. 
It facilitates communication between bot and user, 
by relaying messages from bot to channel and from channel to bot. 
For details about using the Connector service via the Bot Builder SDK for .NET, see [Connector service](bot-framework-dotnet-connector.md).

##<a id="activity"></a> Activity

[!include[Activity concept overview](../includes/snippet-dotnet-concept-activity.md)]
For details about sending and receiving activities by using the Connector service via Bot Builder SDK for .NET, 
see [Send and receive activities](bot-framework-dotnet-send-and-receive.md).

## Dialog

> [!NOTE]
> Content coming soon...

For details about using dialogs within the Bot Builder SDK for .NET, see 
[Dialogs](bot-framework-dotnet-dialogs.md).

### FormFlow

> [!NOTE]
> Content coming soon...

For details about using FormFlow within the BotBuilder SDK for .NET, see [FormFlow](bot-framework-dotnet-formflow.md).

## Other

### Naming conventions

The Bot Builder SDK for .NET library uses strongly-typed, pascal-cased naming conventions. 
However, the JSON messages that are transported back and forth over the wire use camel-case naming conventions. 
For example, the C# property **ReplyToId** is serialized as **replyToId** in the JSON message that's 
transported over the wire.

### Security

You should ensure that your bot's endpoint can only be called by the Bot Framework Connector service. 
For more information on this topic, see [Secure your bot](bot-framework-dotnet-security.md).

## Additional resources

> [!NOTE]
> Content coming soon...




