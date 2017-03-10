---
title: Using the Bot Framework Connector service with .NET | Microsoft Docs
description: Learn about using the Bot Framework Connector service via the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, Connector, Connector service
author: kbrandl
manager: rstand
ms.topic: develop-dotnet-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 03/09/2017
ms.reviewer: rstand
#ROBOTS: Index
---

# Connector service

The Bot Framework Connector service provides a REST API with a common JSON schema 
that enables a bot to communicate across multiple channels such as Skype, Email, Slack, and more. 
The Connector facilitates communication between bot and user, 
by relaying messages from bot to channel and from channel to bot. 

The following articles provide information about using the Connector-related components of the Bot Builder SDK for .NET. 

- [Send and receive activities](bot-framework-dotnet-send-and-receive.md)
- [Add attachments to messages](bot-framework-dotnet-add-attachments.md)
- [Process button and card actions](bot-framework-dotnet-cardactions.md)
- [Implement channel-specific functionality](bot-framework-dotnet-channeldata.md)
- [Secure your bot](bot-framework-dotnet-security.md)

> [!TIP]
> While it is possible to construct a bot by exclusively using the Connector-related components of 
> the Bot Builder SDK for .NET, be sure to also consider potential benefits of using the 
> [Dialog](bot-framework-dotnet-dialogs.md) and/or [FormFlow](bot-framework-dotnet-formflow.md) constructs 
> that the SDK provides.