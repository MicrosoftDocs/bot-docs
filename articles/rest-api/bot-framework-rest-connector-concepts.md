---
title: Key concepts in the Bot Connector API
description: Understand key concepts in the Bot Framework Connector service and Bot State service. 
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: conceptual
ms.date: 10/11/2022
---

# Key concepts in the Bot Connector API

The Bot Framework and the Azure AI Bot Service allow your bot to communicate with users on Teams, Facebook, and more. Channels are available in two forms:

- As a service included as part of Azure AI Bot Service.
- As adapter libraries for use with the Bot Framework SDK.

This article focuses on the standard channels included in the Azure AI Bot Service.

## Bot Framework Channels

Bot Framework channels enable your bot to exchange messages with channels configured in the [Azure portal](https://portal.azure.com). It uses industry-standard REST and JSON over HTTPS and enables authentication with JWT Bearer tokens. For detailed information about how to use the Bot Connector service, see [Authentication](bot-framework-rest-connector-authentication.md) and the remaining articles in this section.

### Activity

The Connector service exchanges information between bot and channel (user) by passing an [Activity][Activity] object. The most common type of activity is **message**, but there are other activity types that can be used to communicate various types of information to a bot or channel. For details about Activities in the Bot Connector service, see [Activities overview](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md).

## Authentication

The Bot Framework Service uses JWT Bearer tokens for authentication. For detailed information about how to authenticate outbound requests that your bot sends to the Bot Framework and how to authenticate inbound requests that your bot receives from the Bot Framework, see [Authentication](bot-framework-rest-connector-authentication.md).

## Client libraries

The Bot Framework provides client libraries that can be used to build bots in C#, JavaScript, Python, and Java.

- [Bot Framework SDK for C#](/dotnet/api/).
- [Bot Framework SDK for Node.js](/javascript/api/botbuilder/).
- [Bot Framework SDK for Python](/python/api/).
- [Bot Framework SDK for Java](https://github.com/microsoft/botbuilder-java#readme).

[!INCLUDE [java-python-sunset-alert](../includes/java-python-sunset-alert.md)]

In addition to simplifying calls to Bot Framework REST APIs, each Bot Framework SDK also provides support for building dialogs that encapsulate conversational logic, built-in prompts for simple things such as Yes/No, strings, numbers, and enumerations, built-in support for powerful AI frameworks such as [LUIS](https://www.luis.ai/), and more.

[!INCLUDE [qnamaker-sunset-alert](../includes/qnamaker-sunset-alert.md)]

[!INCLUDE [luis-sunset-alert](../includes/luis-sunset-alert.md)]

> [!NOTE]
> As an alternative to using the these SDKs, you can generate your own client library in the language of your choice by using the [Bot Connector Swagger file](https://github.com/Microsoft/botbuilder-dotnet/blob/master/libraries/Swagger/ConnectorAPI.json) or code direct to its REST API.

## Bot State service

The Microsoft Bot Framework State service is retired as of March 30, 2018. Previously, bots built on the Azure AI Bot Service or the Bot Builder SDK had a default connection to this service hosted by Microsoft to store bot state data. Bots will need to be updated to use their own state storage.

## Additional information

Learn more about building bots using the Connector service by reviewing articles throughout this section, beginning with [Authentication](bot-framework-rest-connector-authentication.md). If you encounter problems or have suggestions regarding the Connector service, see [Support](../bot-service-resources-links-help.md) for a list of available resources.

[Activity]: bot-framework-rest-connector-api-reference.md#activity-object
