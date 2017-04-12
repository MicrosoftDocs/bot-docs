---
title: Add rich card attachments to messages using the Bot Framework Connector service and .NET | Microsoft Docs
description: Learn how to add rich card attachments to messages using the Bot Framework Connector service via the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, Connector, Connector service, attachment, card, rich card
author: kbrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 04/11/2017
ms.reviewer:
ROBOTS: Index, Follow
---

# Add rich cards to messages

A message exchange between user and bot can contain rich cards that are rendered as a list or carousel. 
Within an `Activity` object, the `Attachments` property contains an array of `Attachment` objects 
that represent the rich cards (and [media attachments](~/dotnet/add-media-attachments.md)) within the message. 

This article describes how to add rich cards to messages using the Bot Framework Connector service via the 
Bot Builder SDK for .NET. 

> [!NOTE]
> For information about how to add media attachments to messages, see 
> [Add media attachments to messages](~/dotnet/add-media-attachments.md).

## Types of rich cards

A rich card comprises a title, description, link, and image(s). 
A message can contain multiple rich cards, displayed in either list format or carousel format.
The Bot Framework currently supports seven types of rich cards: 

| Card type | Description |
|----|----|
| [AnimationCard][animationCard] | A card that can play animated GIFs or short videos. |
| [AudioCard][audioCard] | A card that can play an audio file. |
| [HeroCard][heroCard] | A card that typically contains a single large image, one or more buttons, and text. |
| [ThumbnailCard][thumbnailCard] | A card that typically contains a single thumbnail image, one or more buttons, and text. |
| [ReceiptCard][receiptCard] | A card that enables a bot to provide a receipt to the user. It typically contains the list of items to include on the receipt, tax and total information, and other text. |
| [SignInCard][signinCard] | A card that enables a bot to request that a user sign-in. It typically contains text and one or more buttons that the user can click to initiate the sign-in process. |
| [VideoCard][videoCard] | A card that can play videos. |

> [!TIP]
> To display multiple rich cards in list format, set the activity's `AttachmentLayout` property to "list". 
> To display multiple rich cards in carousel format, set the activity's `AttachmentLayout` property to "carousel". 
> If the channel does not support carousel format, it will display the rich cards in list format, even if the `AttachmentLayout` property specifies "carousel".

## Add a Hero card to a message

The Hero card typically contains a single large image, one or more buttons, and text. 

This code example shows how to create a reply message that contains three Hero cards rendered in carousel format: 

[!code-csharp[Add HeroCard attachment](~/includes/code/dotnet-add-attachments.cs#addHeroCardAttachment)]

## Add a Thumbnail card to a message

The Thumbnail card typically contains a single thumbnail image, one or more buttons, and text. 

This code example shows how to create a reply message that contains two Thumbnail cards rendered in list format: 

[!code-csharp[Add ThumbnailCard attachment](~/includes/code/dotnet-add-attachments.cs#addThumbnailCardAttachment)]

## Add a Receipt card to a message

The Receipt card enables a bot to provide a receipt to the user. 
It typically contains the list of items to include on the receipt, tax and total information, and other text. 

This code example shows how to create a reply message that contains a Receipt card: 

[!code-csharp[Add ReceiptCard attachment](~/includes/code/dotnet-add-attachments.cs#addReceiptCardAttachment)]

## Add a Sign-in card to a message

The Sign-in card enables a bot to request that a user sign-in. 
It typically contains text and one or more buttons that the user can click to initiate the sign-in process. 

This code example shows how to create a reply message that contains a Sign-in card:

[!code-csharp[Add SignInCard attachment](~/includes/code/dotnet-add-attachments.cs#addSignInCardAttachment)]

## Process events within rich cards

The code examples above show how to create buttons within rich cards by using the `CardAction` object. 
By using card actions, you can specify the action that occurs whenever the user clicks a button or taps a 
section of the card. Each `CardAction` object contains these properties:

| Property | Type | Description | 
|----|----|----|
| Type | string | type of action (one of the values specified in the table below) |
| Title | string | title of the button |
| Image | string | image URL for the button |
| Value | string | value needed to perform the specified type of action |

This table lists the valid values for `CardAction.Type` and describes 
the expected contents of `CardAction.Value` for each type:

| CardAction.Type | CardAction.Value | 
|----|----|
| openUrl | URL to be opened in the built-in browser |
| imBack | Text of the message to send to the bot (from the user who clicked the button or tapped the card). This message (from user to bot) will be visible to all conversation participants via the client application that is hosting the conversation. |
| postBack | Text of the message to send to the bot (from the user who clicked the button or tapped the card). This message will not be displayed by the client application that is hosting the conversation. |
| call | Destination for a phone call in this format: **tel:123123123123** |
| playAudio | URL of audio to be played |
| playVideo | URL of video to be played |
| showImage | URL of image to be displayed |
| downloadFile | URL of file to be downloaded |
| signin | URL of OAuth flow to be initiated |

## Additional resources

- [Activity types](~/dotnet/activities.md)
- [Send and receive activities](~/dotnet/connector.md)
- [Create messages](~/dotnet/create-messages.md)
- [Add media attachments to messages](~/dotnet/add-media-attachments.md)
- [Implement channel-specific functionality](~/dotnet/channeldata.md)

[animationCard]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d9/d78/class_microsoft_1_1_bot_1_1_connector_1_1_animation_card.html

[audioCard]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/db/d71/class_microsoft_1_1_bot_1_1_connector_1_1_audio_card.html 

[heroCard]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d4/dab/class_microsoft_1_1_bot_1_1_connector_1_1_hero_card.html 

[thumbnailCard]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/da/da6/class_microsoft_1_1_bot_1_1_connector_1_1_thumbnail_card.html 

[receiptCard]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d0/df9/class_microsoft_1_1_bot_1_1_connector_1_1_receipt_card.html 

[signinCard]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/dc/d03/class_microsoft_1_1_bot_1_1_connector_1_1_signin_card.html 

[videoCard]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d6/da6/class_microsoft_1_1_bot_1_1_connector_1_1_video_card.html
