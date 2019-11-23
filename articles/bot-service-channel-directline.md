---
title: About Direct Line channel
titleSuffix: Bot Service
description: Direct Line channel features
services: bot-service
author: ivorb
manager: kamrani
ms.service: bot-service
ms.topic: conceptual
ms.date: 11/01/2019
ms.author: kamrani
---

# About Direct Line

The Bot Framework Direct Line channel is an easy way to integrate your bot into your mobile app, webpage, or other application.
Direct Line is available in three forms:
- Direct Line service – a global, robust service for most developers
- Direct Line App Service Extensions – dedicated Direct Line functionality for security and performance (available in public preview)
- Direct Line Speech – optimized for high-performance speech (GA)

You can choose which offering of Direct Line is best for you by evaluating the features each offers and the needs of your solution. 
Over time these offerings will be simplified.

|                            | Direct Line | Direct Line App Service Extension | Direct Line Speech |
|----------------------------|-------------|-----------------------------------|--------------------|
| Availability and Licensing    | General availability | Public preview, no SLA  | GA |
| Speech recognition and text-to-speech performance | Standard | Standard | High performance |
| Supports legacy web browsers | Yes | At GA | Yes |
| Bot Framework SDK support | All v3, v4 | v4.63+ required | v4.63+ required |
| Client SDK support    | JS, C# | JS, C# | C++, C#, Unity, JS|
| Works with Web Chat  | Yes | Yes | No|
| VNET | No | Preview | No |


## Addtional resources
- [Connect a bot to Direct Line](bot-service-channel-connect-directline.md)
- [Connect a bot to Direct Line Speech](bot-service-channel-connect-directlinespeech.md)
