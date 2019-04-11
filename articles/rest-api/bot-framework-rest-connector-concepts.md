---
title: Key concepts in the Bot Connector service and Bot State service | Microsoft Docs
description: Understand key concepts in the Bot Framework's Bot Connector service and Bot State service. 
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 01/16/2019
---

# Key concepts

You can use the Bot Connector service and the Bot State service to communicate with users across multiple channels such as Skype, Email, Slack, and more. This article introduces key concepts in the Bot Connector service and Bot State service.

> [!IMPORTANT]
> The Bot Framework State Service API is not recommended for production environments, and may be deprecated in a future release. It is recommended that you update your bot code to use the in-memory storage for testing purposes or use one of the **Azure Extensions** for production bots. For more information, see the **Manage state data** topic for [.NET](~/dotnet/bot-builder-dotnet-state.md) or [Node](~/nodejs/bot-builder-nodejs-state.md) implementation.

## Bot Connector service

The Bot Connector service enables your bot to exchange messages with channels configured in the <a href="https://dev.botframework.com/" target="_blank">Bot Framework Portal</a>. It uses industry-standard REST and JSON over HTTPS and enables authentication with JWT Bearer tokens. For detailed information about how to use the Bot Connector service, see [Authentication](bot-framework-rest-connector-authentication.md) and the remaining articles in this section.

### Activity

The Bot Connector service exchanges information between between bot and channel (user) by passing an [Activity][Activity] object. The most common type of activity is **message**, but there are other activity types that can be used to communicate various types of information to a bot or channel. For details about Activities in the Bot Connector service, see [Activities overview](bot-framework-rest-connector-activities.md).

## Bot State service

The Bot State service enables your bot to store and retrieve state data that is associated with a user, a conversation, or a specific user within the context of a specific conversation. It uses industry-standard REST and JSON over HTTPS and enables authentication with JWT Bearer tokens. Any data that you save by using the Bot State service will be stored in Azure and encrypted at rest.

The Bot State service is only useful in conjunction with the Bot Connector service. That is, it can only be used to store state data that is related to conversations that your bot conducts using the Bot Connector service. For detailed information about how to use the Bot State service, see [Authentication](bot-framework-rest-connector-authentication.md) and [Manage state data](bot-framework-rest-state.md).

## Authentication

Both the Bot Connector service and the Bot State service enable authentication with JWT Bearer tokens. For detailed information about how to authenticate outbound requests that your bot sends to the Bot Framework, how to authenticate inbound requests that your bot receives from the Bot Framework, and more, see [Authentication](bot-framework-rest-connector-authentication.md). 

## Client libraries

The Bot Framework provides client libraries that can be used to build bots in either C# or Node.js. 

- To build a bot using C#, use the [Bot Framework SDK for C#](../dotnet/bot-builder-dotnet-overview.md). 
- To build a bot using Node.js, use the [Bot Framework SDK for Node.js](../nodejs/index.md). 

In addition to modeling the Bot Connector service and the Bot State service, each Bot Framework SDK also provides a powerful system for building dialogs that encapsulate conversational logic, built-in prompts for simple things such as Yes/No, strings, numbers, and enumerations, built-in support for powerful AI frameworks such as <a href="https://www.luis.ai/" target="_blank">LUIS</a>, and more. 

> [!NOTE]
> As an alternative to using the C# SDK or Node.js SDK, you can generate your own client library in the language of your choice by using the <a href="https://aka.ms/connector-swagger-file" target="_blank">Bot Connector Swagger file</a> and the <a href="https://aka.ms/state-swagger-file" target="_blank">Bot State Swagger file</a>.

## Additional resources

Learn more about building bots using the Bot Connector service and Bot State service by reviewing articles throughout this section, beginning with [Authentication](bot-framework-rest-connector-authentication.md). If you encounter problems or have suggestions regarding the Bot Connector service or Bot State service, see [Support](../bot-service-resources-links-help.md) for a list of available resources. 

[Activity]: bot-framework-rest-connector-api-reference.md#activity-object