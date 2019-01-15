---
title: Sample bots for Bot Framework SDK for Node.js | Microsoft Docs
description: Explore a large selection of sample bots that can help kickstart your bot development with the Bot Framework SDK for Node.js.
author: v-ducvo
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0' 
---
# Bot Framework SDK for Node.js samples

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

These samples demonstrate task-focused bots that show how to take advantage of features in the Bot Framework SDK for Node.js. You can use the samples to help you quickly get started with building great bots with rich capabilities.

## Get the samples
To get the samples, clone the [BotBuilder-Samples](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples) GitHub repository using Git.

```cmd
git clone -b v3-sdk-samples https://github.com/Microsoft/BotBuilder-Samples.git
cd BotBuilder-Samples
```

The sample bots built with the Bot Framework SDK for Node.js are organized in the **Node** directory.

You can also view the samples on GitHub and deploy them to Azure directly.

## Core
These samples show the basic techniques for building rich and capable bots.

Sample | Description
------------ | ------------- 
[Send Attachment](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/core-SendAttachment) | A sample bot that sends simple media attachments (images) to the user. 
[Receive Attachment](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/core-ReceiveAttachment) | A sample bot that receives attachments sent by the user and downloads them. 
[Create New Conversation](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/core-CreateNewConversation)  | A sample bot that starts a new conversation using a previously stored user address.
[Get Members of a Conversation](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/core-GetConversationMembers) | A sample bot that retrieves the conversation's members list and detects when it changes. 
[Direct Line](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/core-DirectLine) | A sample bot and a custom client that communicate with each other using the Direct Line API. 
[Direct Line (WebSockets)](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/core-DirectLineWebSockets) | A sample bot and a custom client that communicate with each other using the Direct Line API + WebSockets. 
[Multi Dialogs](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/core-MultiDialogs) | A sample bot that shows various kinds of dialogs.
[State API](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/core-State) | A stateless sample bot that tracks the context of a conversation.
[Custom State API](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/core-CustomState) | A stateless sample bot that tracks the context of a conversation using a custom storage provider.
[ChannelData](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/core-ChannelData) | A sample bot that sends native metadata to Facebook using ChannelData.
[AppInsights](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/core-AppInsights) | A sample bot that logs telemetry to an Application Insights instance.

## Search
This sample shows how to leverage Azure Search in data-driven bots.

Sample | Description
------------ | -------------
[Azure Search](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/demo-Search) | Two sample bots that help the user navigate large amounts of content.


## Cards
These samples show how to send rich cards in the Bot Framework.

Sample | Description
------------ | -------------
[Rich Cards](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/cards-RichCards) | A sample bot that sends several different types of rich cards.
[Carousel of Cards](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/cards-CarouselCards) | A sample bot that sends multiple rich cards within a single message using the Carousel layout.

## Intelligence
These samples show how to add artificial intelligence capabilities to a bot using Bing and Microsoft Cognitive Services APIs.

Sample | Description
------------ | -------------
[LUIS](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/intelligence-LUIS) | A sample bot that uses LuisDialog to integrate with a LUIS.ai application.
[Image Caption](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/intelligence-ImageCaption) | A sample bot that gets an image caption using the Microsoft Cognitive Services Vision API.
[Speech To Text](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/intelligence-SpeechToText)  | A sample bot that gets text from audio using the Bing Speech API.
[Similar Products](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/intelligence-SimilarProducts) | A sample bot that finds visually similar products using the Bing Image Search API. 
[Zummer](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/intelligence-Zummer) | A sample bot that finds wikipedia articles using the Bing Search API.

## Reference implementation
This sample is designed to showcase an end-to-end scenario. It's a great source of code fragments if you're looking to implement more complex features in your bot.


Sample | Description
------------ | -------------
[Contoso Flowers](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/Node/demo-ContosoFlowers) | A sample bot that uses many features of the Bot Framework.

