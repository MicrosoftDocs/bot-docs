---
title: Key concepts in the Bot Framework's Direct Line API  | Microsoft Docs
description: Understand key concepts in the Bot Framework's Direct Line API. 
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer: 
---

# Key concepts

You can enable communication between your bot and your own client application by using the Direct Line API. This article introduces key concepts in the Direct Line API.

## Direct Line API

[Direct Line API 3.0][DirectLine3] is the newest version of the Direct Line API. 

> [!NOTE]
> [Direct Line API 1.1][DirectLine11] is currently still supported, but if you are creating a new connection between your bot and client application, use Direct Line API 3.0.

Your client can use the Direct Line API to send messages to your bot via an `HTTP POST` request. Your bot can use the Direct Line API to receive messages from a client either via `WebSocket` stream or by polling using `HTTP GET`. 

## Authentication

The Direct Line API enables you to authenticate requests either by using a **secret** that you obtain from the Direct Line channel configuration page in the <a href="https://dev.botframework.com/" target="_blank">Bot Framework Portal</a> or by using a **token** that you obtain at runtime. 

## Client library

The Bot Framework provides a client library that facilitates access to the Direct Line API using C#. To use the client library within a Visual Studio project, install the `Microsoft.Bot.Connector.DirectLine` <a href="https://www.nuget.org/packages/Microsoft.Bot.Connector.DirectLine" target="_blank">NuGet package</a>. 

As an alternative to using the C# client library, you can generate your own client library in the language of your choice by using the <a href="https://docs.botframework.com/en-us/restapi/directline3/swagger.json" target="_blank">Direct Line Swagger file</a>.

### Web chat control 

The Bot Framework also provides a control that enables you to embed a Direct-Line-powered bot into your client application. For more information, see the <a href="https://github.com/Microsoft/BotFramework-WebChat" target="_blank">Microsoft Bot Framework WebChat control</a>.

## Additional resources

If you encounter problems or have suggestions regarding the Direct Line API, see [Support](~/resources-support.md) for a list of available resources. 

[DirectLine3]: https://docs.botframework.com/en-us/restapi/directline3/#navtitle

[DirectLine11]: https://docs.botframework.com/en-us/restapi/directline/#navtitle