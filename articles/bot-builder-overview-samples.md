---
title: Bot Builder sample bots | Microsoft Docs
description: Explore a large selection of sample bots that can help kickstart your bot development with the Bot Builder SDK.
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
---
# Bot Builder sample bots

These samples demonstrate task-focused bots that show how to take advantage of features in the Bot Builder SDK for .NET and the Bot Builder SDK for Node.js. You can use the samples to help you quickly get started with building great bots with rich capabilities.

## Get the samples
To get the samples, clone the [BotBuilder-Samples](https://github.com/Microsoft/BotBuilder-Samples) GitHub repository using Git.

```
git clone https://github.com/Microsoft/BotBuilder-Samples.git
cd BotBuilder-Samples
```

The samples are organized by language in directories: **CSharp** for the Bot Builder SDK for .NET and **Node** for the Bot Builder SDK for Node.js.

You can also view the samples on GitHub and deploy them to Azure directly.

## Core
These samples show the basic techniques for building rich and capable bots.

Sample | Description | C# | Node
------------ | ------------- | :-----------: | :-----------:
Send Attachment | A sample bot that sends simple media attachments (images) to the user. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-SendAttachment) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/core-SendAttachment)
Receive Attachment | A sample bot that receives attachments sent by the user and downloads them. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-ReceiveAttachment) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/core-ReceiveAttachment)
Create New Conversation | A sample bot that starts a new conversation using a previously stored user address. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-CreateNewConversation) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/core-CreateNewConversation)
Get Members of a Conversation | A sample bot that retrieves the conversation's members list and detects when it changes. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-GetConversationMembers) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/core-GetConversationMembers)
Direct Line | A sample bot and a custom client that communicate with each other using the Direct Line API. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-DirectLine) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/core-DirectLine)
Direct Line (WebSockets) | A sample bot and a custom client that communicate with each other using the Direct Line API + WebSockets. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-DirectLineWebSockets) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/core-DirectLineWebSockets)
Multi Dialogs | A sample bot that shows various kinds of dialogs. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-MultiDialogs) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/core-MultiDialogs)
State API | A stateless sample bot that tracks the context of a conversation. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-State) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/core-State)
Custom State API | A stateless sample bot that tracks the context of a conversation using a custom storage provider. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-CustomState) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/core-CustomState)
ChannelData | A sample bot that sends native metadata to Facebook using ChannelData. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-ChannelData) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/core-ChannelData)
AppInsights | A sample bot that logs telemetry to an Application Insights instance. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-AppInsights) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/core-AppInsights)

## Search
This sample shows how to leverage Azure Search in data-driven bots.

Sample | Description | C# | Node
------------ | ------------- | :-----------: | :-----------:
Azure Search | Two sample bots that help the user navigate large amounts of content. | [View samples](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/demo-Search) | [View samples](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/demo-Search)


## Cards
These samples show how to send rich cards in the Bot Framework.

Sample | Description | C# | Node
------------ | ------------- | :-----------: | :-----------:
Rich Cards | A sample bot that sends several different types of rich cards. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/cards-RichCards) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/cards-RichCards)
Carousel of Cards | A sample bot that sends multiple rich cards within a single message using the Carousel layout. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/cards-CarouselCards) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/cards-CarouselCards)

## Intelligence
These samples show how to add artificial intelligence capabilities to a bot using Bing and Microsoft Cognitive Services APIs.

Sample | Description | C# | Node
------------ | ------------- | :-----------: | :-----------:
LUIS | A sample bot that uses LuisDialog to integrate with a LUIS.ai application. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/intelligence-LUIS) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/intelligence-LUIS)
Image Caption | A sample bot that gets an image caption using the Microsoft Cognitive Services Vision API. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/intelligence-ImageCaption) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/intelligence-ImageCaption)
Speech To Text | A sample bot that gets text from audio using the Bing Speech API. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/intelligence-SpeechToText) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/intelligence-SpeechToText)
Similar Products | A sample bot that finds visually similar products using the Bing Image Search API. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/intelligence-SimilarProducts) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/intelligence-SimilarProducts)
Zummer | A sample bot that finds wikipedia articles using the Bing Search API.  | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/intelligence-Zummer) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/intelligence-Zummer)

## Reference implementation
This sample is designed to showcase an end-to-end scenario. It's a great source of code fragments if you're looking to implement more complex features in your bot.

Sample | Description | C# | Node
------------ | ------------- | :-----------: | :-----------:
Contoso Flowers | A sample bot that uses many features of the Bot Framework. | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/demo-ContosoFlowers) | [View sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/demo-ContosoFlowers)

