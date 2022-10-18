---
title: Key concepts in the Bot Framework Direct Line API 3.0  - Azure Bot Service
description: Learn about version 3.0 of the Bot Framework Direct Line API. View information on authentication, starting conversations, messages, and developer resources.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: bot-service
ms.date: 11/01/2021
---

# Key concepts in Direct Line API 3.0

You can enable communication between your bot and your own client application by using the Direct Line API. This article introduces key concepts in Direct Line API 3.0 and provides information about relevant developer resources. You can build a client using the SDK, REST API, or Web Chat.

## Authentication

Direct Line API 3.0 requests can be authenticated either by using a **secret** that you obtain from the Direct Line channel configuration page in the [Azure portal](https://portal.azure.com) or by using a **token** that you obtain at runtime. For more information, see [Authentication](bot-framework-rest-direct-line-3-0-authentication.md).

## Starting a conversation

Direct Line conversations are explicitly opened by clients and may run as long as the bot and client participate and have valid credentials. For more information, see [Start a conversation](bot-framework-rest-direct-line-3-0-start-conversation.md).

## Sending messages

Using Direct Line API 3.0, a client can send messages to your bot by issuing `HTTP POST` requests. A client may send a single message per request. For more information, see [Send an activity to the bot](bot-framework-rest-direct-line-3-0-send-activity.md).

## Receiving messages

Using Direct Line API 3.0, a client can receive messages from your bot either via `WebSocket` stream or by issuing `HTTP GET` requests. Using either of these techniques, a client may receive multiple messages from the bot at a time as part of an `ActivitySet`. For more information, see [Receive activities from the bot](bot-framework-rest-direct-line-3-0-receive-activities.md).

## Developer resources

### Client libraries

The Bot Framework provides client libraries that facilitate access to Direct Line API 3.0 via C# and Node.js.

- To use the .NET client library within a Visual Studio project, install the `Microsoft.Bot.Connector.DirectLine` [NuGet package](https://www.nuget.org/packages/Microsoft.Bot.Connector.DirectLine).s

- To use the Node.js client library, install the `botframework-directlinejs` library using [NPM](https://www.npmjs.com/package/botframework-directlinejs) (or download the [source](https://github.com/Microsoft/BotFramework-DirectLineJS)).

### Web chat control

The Bot Framework provides a control that enables you to embed a Direct-Line-powered bot into your client application. For more information, see the [Microsoft Bot Framework WebChat control](https://github.com/Microsoft/BotFramework-WebChat).
