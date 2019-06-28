---
title: About Direct Line channel
titleSuffix: Bot Service
description: Direct Line channel features
services: bot-service
author: ivorb
manager: kamrani
ms.service: bot-service
ms.subservice: bot-service
ms.topic: conceptual
ms.date: 05/02/2019
ms.author: v-ivorb
---

## About Direct Line

The Bot Framework Direct Line channel is an easy way to integrate your bot into your mobile app, webpage, or other application.
Direct Line is available in three forms:
- Direct Line service – a global, robust service for most developers
- Direct Line App Service Extensions – dedicated Direct Line functionality for security and performance (available in private preview starting May 2019)
- Direct Line Speech – optimized for high-performance speech (available in private preview starting May 2019)

You can choose which offering of Direct Line is best for you by evaluating the features each offers and the needs of your solution. 
Over time these offerings will be simplified.

|                            | Direct Line | Direct Line App Service Extension | Direct Line Speech |
|----------------------------|-------------|-----------------------------------|--------------------|
| GA availability and SLA    | General availability | Private preview, no SLA  | Private preview, no SLA |
| Speech recognition and text-to-speech performance | Standard | Standard | High performance |
| Integrated OAuth           | Yes | Yes | No |
| Integrated telemetry       | Yes | Yes | No |
| Supports legacy web browsers | Yes | No | No |
| Bot Framework SDK support | All v3, v4 | v4.5+ required | v4.5+ required |
| Client SDK support    | JS, C# | JS, C# | C++, C#, Unity |
| Works with Web Chat  | Yes | Yes | No|
| Conversation history API | Yes | Yes| No|
| VNET | No | Preview* | No |

_* Direct Line App Service Extensions can be used in VNETs but do not yet allow restriction of outbound calls._

## Addtional resources
- [Connect a bot to Direct Line](bot-service-channel-connect-directline.md)
- [Connect a bot to Direct Line Speech](bot-service-channel-connect-directlinespeech.md)
