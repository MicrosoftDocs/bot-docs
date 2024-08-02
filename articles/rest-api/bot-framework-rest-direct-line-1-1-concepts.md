---
title: Key concepts in the Bot Framework Direct Line API 1.1  - Bot Service
description: Learn about version 1.1 of the Bot Framework Direct Line API. View information on authentication, starting conversations, messages, and developer resources. 
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: azure-ai-bot-service
ms.date: 11/01/2021
ms.custom:
  - evergreen
---

# Key concepts in Direct Line API 1.1

You can enable communication between your bot and your own client application by using the Direct Line API.

> [!IMPORTANT]
> This article introduces key concepts in Direct Line API 1.1 and provides information about relevant developer resources. If you're creating a new connection between your client application and bot, use [Direct Line API 3.0](bot-framework-rest-direct-line-3-0-concepts.md) instead.

## Authentication

Direct Line API 1.1 requests can be authenticated either by using a *secret* that you obtain from the Direct Line channel configuration page in the [Azure portal](https://portal.azure.com) or by using a *token* that you obtain at runtime.  For more information, see [Authentication](bot-framework-rest-direct-line-1-1-authentication.md).

## Starting a conversation

Direct Line conversations are explicitly opened by clients and may run as long as the bot and client participate and have valid credentials. For more information, see [Start a conversation](bot-framework-rest-direct-line-1-1-start-conversation.md).

## Sending messages

Using Direct Line API 1.1, a client can send messages to your bot by issuing `HTTP POST` requests. A client may send a single message per request. For more information, see [Send a message to the bot](bot-framework-rest-direct-line-1-1-send-message.md).

## Receiving messages

Using Direct Line API 1.1, a client can receive messages by polling with `HTTP GET` requests. In response to each request, a client may receive multiple messages from the bot as part of a `MessageSet`. For more information, see [Receive messages from the bot](bot-framework-rest-direct-line-1-1-receive-messages.md).

## Developer resources

### Client library

The Bot Framework provides a client library that facilitates access to Direct Line API 1.1 via C#. To use the client library within a Visual Studio project, install the `Microsoft.Bot.Connector.DirectLine` [v1.x NuGet package](https://www.nuget.org/packages/Microsoft.Bot.Connector.DirectLine/1.1.1).

As an alternative to using the C# client library, you can generate your own client library in the language of your choice by using the [Direct Line API 1.1 Swagger file](https://docs.botframework.com/restapi/directline/swagger.json).
