---
title: About Direct Line channel
titleSuffix: Bot Service
description: Learn about the Bot Framework Direct Line three channels. Select the channel to use to integrate bots into mobile apps, webpages, and other applications.
services: bot-service
author: mmiele
ms.author: v-mimiel
manager: kamrani
ms.service: bot-service
ms.topic: conceptual
ms.date: 10/05/2020
---

# About Direct Line

The Bot Framework offers multiple channels with the Direct Line branding. It is important that you select the version that best fits the conversational A.I. experience you are designing.

- **Direct Line**. This is the standard channel offering of Direct Line. It works by default with bot templates via the [Azure portal](https://ms.portal.azure.com/), bots from the [Bot Builder Samples](https://github.com/Microsoft/BotBuilder-Samples/blob/main/README.md), and bots created with the [Azure CLI](https://docs.microsoft.com/cli/azure/what-is-azure-cli). This is the Direct Line best suited in the majority of the cases. See [Connect a bot to Direct Line](bot-service-channel-connect-directline.md).
- **Direct Line Speech**. It provides text to speech and speech to text services within the channel. It allows a client to stream audio directly to the channel which will then be converted to text and sent to the bot. The Direct Line Speech is also able to convert text messages from the bot into audio messages as spoken by a variety of A.I. powered voices. Combined, this makes Direct Line Speech capable of having audio only conversations with clients. See [Connect a bot to Direct Line Speech](bot-service-channel-connect-directlinespeech.md).
- **Direct Line App Service Extension**. It runs inside the same subscription, App Service, and Azure network as your bot. If you have network isolation requirements this version of Direct Line may be ideal. Bots and clients require special modifications to work with the Direct Line App Service Extension to ensure traffic never leaves the isolated network. See [Direct Line App Service Extension](bot-service-channel-directline-extension.md).

You can choose which offering of Direct Line is best for you by evaluating the features each offers and the needs of your solution.
Over time these offerings will be simplified.

| Feature                    | Direct Line | Direct Line App Service Extension | Direct Line Speech |
|----------------------------|-------------|-----------------------------------|--------------------|
| Availability and Licensing    | GA | Public preview, no SLA  | GA |
| Speech recognition and text-to-speech performance | Standard | Standard | High performance |
| Supports legacy web browsers | Yes | At GA | Yes |
| Bot Framework SDK support | All v3, v4 | v4.63+ required | v4.63+ required |
| Client SDK support    | JS, C# | JS, C# | C++, C#, Unity, JS|
| Works with Web Chat  | Yes | Yes | No|
| VNET | No | Preview | No |

## Additional resources

- [Connect a bot to Direct Line](bot-service-channel-connect-directline.md)
- [Connect a bot to Direct Line Speech](bot-service-channel-connect-directlinespeech.md)
- [Direct Line App Service Extension](bot-service-channel-directline-extension.md)
