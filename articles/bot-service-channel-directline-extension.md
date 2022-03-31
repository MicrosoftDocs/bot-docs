---
title: Direct Line App Service extension
titleSuffix: Bot Service
description: Become familiar with the Direct Line App Service extension. See how to use streaming extensions to connect directly to hosted bots. View additional resources.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: Daniel.Evans
ms.service: bot-service
ms.topic: conceptual
ms.date: 05/19/2020
---

# Direct Line App Service extension

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

The Direct Line App Service extension allows clients to connect directly with the host, where the bot is located. It runs inside the same subscription, app service, and Azure network as your bot and provides network isolation and, in some cases, improved performance. The client application uses [WebSocket protocol](https://tools.ietf.org/html/rfc6455) to communicate with the bot. The following picture shows the overall architecture:

![Direct Line App Service extension architecture](./media/channels/direct-line-extension-architecture.png "Direct Line App Service extension")

> [!NOTE]
> If you don't require network isolation and want to use the standard channel over the HTTPS protocol, refer to [Connect a bot to Direct Line](bot-service-channel-connect-directline.md).

The Direct Line App Service extension adds a new set of streaming extensions to the Bot Framework protocol, replacing exchanging messages via HTTP with a transport that allows bidirectional requests to be sent over a _persistent WebSocket_.

Before streaming extensions, the Direct Line API offered one way for a client to send Activities to Direct Line and two ways for a client to retrieve Activities from Direct Line. The messages were sent via an HTTP POST, and received by either an HTTP GET (polling) or by opening a WebSocket to receive ActivitySets.
Streaming extensions expand on the use of the WebSocket and allow all messaging communication to be sent on that WebSocket. Streaming extensions can also be used between channel services and the bot.

The Direct Line App Service extension is pre-installed on all instances of Azure App Services in every data center around the world. It's maintained and managed by Microsoft without additional deployment work for the customer. It is disabled on Azure App Services by default, but it can be easily turned on to connect to your hosted bot.

## See Also

|Name|Description|
|---|---|
|[Configure .NET bot for extension](bot-service-channel-directline-extension-net-bot.md)|Update a .NET bot to work with named pipes, and enable the Direct Line App Service extension in the Azure App Service resource where the bot is hosted.  |
|[Configure Node.js bot for extension](bot-service-channel-directline-extension-node-bot.md)|Update a Node.js bot to work with named pipes and enable the Direct Line App Service extension in the Azure App Service resource where the bot is hosted.  |
|[Create .NET client with extension](bot-service-channel-directline-extension-net-client.md)|Create a .NET client in C# which connects to the Direct Line App Service extension.|
|[Use extension with Web Chat](bot-service-channel-directline-extension-webchat-client.md)|Use Web Chat with the Direct Line App Service extension.|
|[Use extension within VNET](bot-service-channel-directline-extension-vnet.md)|Use the Direct Line App Service extension with an Azure Virtual Network (VNET).|

## Additional resources

- [Connect a bot to Direct Line](bot-service-channel-connect-directline.md)
- [Connect a bot to Direct Line Speech](bot-service-channel-connect-directlinespeech.md)
