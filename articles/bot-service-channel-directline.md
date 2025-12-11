---
title: About Direct Line channel
titleSuffix: Bot Service
description: Learn about the Bot Framework Direct Line three channels. Select the channel to use to integrate bots into mobile apps, webpages, and other applications.
author: JonathanFingold
ms.author: kunsinghms
manager: shellyha
ms.reviewer: pehecke
ms.service: azure-ai-bot-service
ms.topic: overview
ms.custom:
  - evergreen
ms.update-cycle: 1095-days
---

# About Direct Line

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]
The Bot Framework offers multiple channels with the Direct Line branding. It's important that you select the version that best fits the conversational AI experience you're designing.

- **Direct Line**. This is the standard channel offering of Direct Line. It works by default with bot templates via the [Azure portal](https://ms.portal.azure.com/), bots from the [Bot Builder Samples](https://github.com/Microsoft/BotBuilder-Samples/blob/main/README.md), and bots created with the [Azure CLI](/cli/azure/what-is-azure-cli). This is the Direct Line best suited in the majority of the cases. See [Connect a bot to Direct Line](bot-service-channel-connect-directline.md).
- **Direct Line App Service Extension**. It runs inside the same subscription, App Service, and Azure network as your bot. If you have network isolation requirements, this version of Direct Line may be ideal. Bots and clients require special modifications to work with the Direct Line App Service Extension to ensure traffic never leaves the isolated network. See [Direct Line App Service Extension](bot-service-channel-directline-extension.md).

You can choose which offering of Direct Line is best for you by evaluating the features each offers and the needs of your solution.
Over time these offerings will be simplified.

| Feature      | Direct Line | Direct Line App Service Extension |
|--------------|-------------|-----------------------------------|
| Availability and Licensing | GA | GA  |
| Speech recognition and text-to-speech performance | Standard | Standard |
| Supports legacy web browsers | Yes | Yes |
| Bot Framework SDK support | All v3, v4 | v4.6.3+ required |
| Client SDK support | JS, C# | JS, C# |
| Works with Web Chat  | Yes | Yes |
| VNET | No | Yes |

## Additional resources

- [Connect a bot to Direct Line](bot-service-channel-connect-directline.md)
- [Direct Line App Service Extension](bot-service-channel-directline-extension.md)
