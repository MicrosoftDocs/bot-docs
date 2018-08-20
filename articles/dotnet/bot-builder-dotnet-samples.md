---
title: Sample bots for Bot Builder SDK for .NET | Microsoft Docs
description: Explore sample bots that can help kickstart your bot development with the Bot Builder SDK for .NET.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 05/03/2018
---

# Bot Builder SDK for .NET samples


::: moniker range="azure-bot-service-3.0"

[!INCLUDE [pre-release-label](~/includes/pre-release-label-v3.md)]

These samples demonstrate task-focused bots that show how to take advantage of features in the Bot Builder SDK for .NET. You can use the samples to help you quickly get started with building great bots with rich capabilities.

## Get the samples
To get the samples, clone the [BotBuilder-Samples](https://github.com/Microsoft/BotBuilder-Samples) GitHub repository using Git.

```cmd
git clone https://github.com/Microsoft/BotBuilder-Samples.git
cd BotBuilder-Samples
```

The sample bots built with the Bot Builder SDK for .NET are organized in the **CSharp** directory.

You can also view the samples on GitHub and deploy them to Azure directly.

## Core
These samples show the basic techniques for building rich and capable bots.

Sample | Description
------------ | ------------- 
[Send Attachment](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-SendAttachment) | A sample bot that sends simple media attachments (images) to the user. 
[Receive Attachment](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-ReceiveAttachment) | A sample bot that receives attachments sent by the user and downloads them. 
[Create New Conversation](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-CreateNewConversation)  | A sample bot that starts a new conversation using a previously stored user address.
[Get Members of a Conversation](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-GetConversationMembers) | A sample bot that retrieves the conversation's members list and detects when it changes. 
[Direct Line](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-DirectLine) | A sample bot and a custom client that communicate with each other using the Direct Line API. 
[Direct Line (WebSockets)](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-DirectLineWebSockets) | A sample bot and a custom client that communicate with each other using the Direct Line API + WebSockets. 
[Multi Dialogs](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-MultiDialogs) | A sample bot that shows various kinds of dialogs.
[State API](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-State) | A stateless sample bot that tracks the context of a conversation.
[Custom State API](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-CustomState) | A stateless sample bot that tracks the context of a conversation using a custom storage provider.
[ChannelData](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-ChannelData) | A sample bot that sends native metadata to Facebook using ChannelData.
[AppInsights](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-AppInsights) | A sample bot that logs telemetry to an Application Insights instance.

## Search
This sample shows how to leverage Azure Search in data-driven bots.

Sample | Description
------------ | -------------
[Azure Search](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/demo-Search) | Two sample bots that help the user navigate large amounts of content.


## Cards
These samples show how to send rich cards in the Bot Framework.

Sample | Description
------------ | -------------
[Rich Cards](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/cards-RichCards) | A sample bot that sends several different types of rich cards.
[Carousel of Cards](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/cards-CarouselCards) | A sample bot that sends multiple rich cards within a single message using the Carousel layout.

## Intelligence
These samples show how to add artificial intelligence capabilities to a bot using Bing and Microsoft Cognitive Services APIs.

Sample | Description
------------ | -------------
[LUIS](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/intelligence-LUIS) | A sample bot that uses LuisDialog to integrate with a LUIS.ai application.
[Image Caption](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/intelligence-ImageCaption) | A sample bot that gets an image caption using the Microsoft Cognitive Services Vision API.
[Speech To Text](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/intelligence-SpeechToText)  | A sample bot that gets text from audio using the Bing Speech API.
[Similar Products](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/intelligence-SimilarProducts) | A sample bot that finds visually similar products using the Bing Image Search API. 
[Zummer](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/intelligence-Zummer) | A sample bot that finds wikipedia articles using the Bing Search API.

## Reference implementation
This sample is designed to showcase an end-to-end scenario. It's a great source of code fragments if you're looking to implement more complex features in your bot.


Sample | Description
------------ | -------------
[Contoso Flowers](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/demo-ContosoFlowers) | A sample bot that uses many features of the Bot Framework.

::: moniker-end

::: moniker range="azure-bot-service-4.0"

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

These samples demonstrate task-focused bots that show how to take advantage of features in the Bot Builder SDK v4 for .NET. You can use the samples to help you quickly get started with building great bots with rich capabilities. 

Note: The SDK v4 is being actively developed and should therefore be used for experimentation only. 

To get the samples, clone the [botbuilder-dotnet](https://github.com/Microsoft/botbuilder-dotnet) GitHub repository using Git.
```cmd
git clone https://github.com/Microsoft/botbuilder-dotnet.git
cd botbuilder-dotnet\samples-final
```
The sample bots built with the Bot Builder SDK for .NET are organized in the **samples-final** directory.


::: moniker-end

