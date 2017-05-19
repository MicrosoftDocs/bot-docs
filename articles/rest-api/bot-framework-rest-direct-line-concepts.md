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

> [!NOTE]
> [Direct Line API 3.0][DirectLine3] is the newest version of the Direct Line API. 
> [Direct Line API 1.1][DirectLine11] is currently still supported, but if you are creating a new connection between your bot and client application, use Direct Line API 3.0.

## Authentication

The Direct Line API enables you to authenticate requests either by using a **secret** that you obtain from the Direct Line channel configuration page in the <a href="https://dev.botframework.com/" target="_blank">Bot Framework Portal</a> or by using a **token** that you obtain at runtime. 

## Starting a conversation

Direct Line conversations are explicitly opened by clients and may run as long as the bot and client participate and have valid credentials. 

## Sending and receiving messages

Using the Direct Line API, a client can send messages to your bot by issuing `HTTP POST` requests. With Direct Line API 1.0, a client can receive messages by polling with `HTTP GET` requests, and with Direct Line API 3.0, a client can receive messages either by polling with `HTTP GET` requests or via `WebSocket` stream.

## Additional resources

If you encounter problems or have suggestions regarding the Direct Line API, see [Support](~/resources-support.md) for a list of available resources. 

[DirectLine3]: https://docs.botframework.com/en-us/restapi/directline3/#navtitle

[DirectLine11]: https://docs.botframework.com/en-us/restapi/directline/#navtitle