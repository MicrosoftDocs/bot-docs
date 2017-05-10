---
title: Add rich card attachments to messages | Microsoft Docs
description: Learn how to add rich cards to messages using the Bot Builder SDK for .NET.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---

# Add rich card attachments to messages
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-add-rich-card-attachments.md)
> - [Node.js](../nodejs/bot-builder-nodejs-send-rich-cards.md)
> - [REST](../rest-api/bot-framework-rest-connector-add-rich-cards.md)

A message exchange between user and bot can contain one or more rich cards rendered as a list or carousel. 
The `Attachments` property of the `Activity` object contains an array of `Attachment` objects that represent the rich cards and media attachments within the message. 

> [!NOTE]
> For information about how to add media attachments to messages, see 
> [Add media attachments to messages](~/dotnet/bot-builder-dotnet-add-media-attachments.md).

## Types of rich cards

The Bot Framework currently supports eight types of rich cards: 

| Card type | Description |
|----|----|
| <a href="http://adaptivecards.io" target="_blank">AdaptiveCard</a> | A card that can contain any combination of text, speech, images, buttons, and input fields.  |
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

## Process events within rich cards

To process events within rich cards, define `CardAction` objects to specify what should happen when the user clicks a button or taps a section of the card. Each `CardAction` object contains these properties:

| Property | Type | Description | 
|----|----|----|
| Type | string | type of action (one of the values specified in the table below) |
| Title | string | title of the button |
| Image | string | image URL for the button |
| Value | string | value needed to perform the specified type of action |

> [!NOTE]
> Buttons within Adaptive Cards are not created using `CardAction` objects, 
> but instead using the schema that is defined by <a href="http://adaptivecards.io" target="_blank">Adaptive Cards</a>. 
> See [Add an Adaptive Card to a message](#adaptive-card) for an example that shows how to 
> add buttons to an Adaptive Card.

This table lists the valid values for `CardAction.Type` and describes 
the expected contents of `CardAction.Value` for each type:

| CardAction.Type | CardAction.Value | 
|----|----|
| openUrl | URL to be opened in the built-in browser |
| imBack | Text of the message to send to the bot (from the user who clicked the button or tapped the card). This message (from user to bot) will be visible to all conversation participants via the client application that is hosting the conversation. |
| postBack | Text of the message to send to the bot (from the user who clicked the button or tapped the card). Some client applications may display this text in the message feed, where it will be visible to all conversation participants. |
| call | Destination for a phone call in this format: **tel:123123123123** |
| playAudio | URL of audio to be played |
| playVideo | URL of video to be played |
| showImage | URL of image to be displayed |
| downloadFile | URL of file to be downloaded |
| signin | URL of OAuth flow to be initiated |

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

##<a id="adaptive-card"></a> Add an Adaptive card to a message

The Adaptive Card can can contain any combination of text, speech, images, buttons, and input fields. 
Adaptive Cards are created using the JSON format specified in <a href="http://adaptivecards.io" target="_blank">Adaptive Cards</a>, which gives you full control over card content and format. 

To create an Adaptive Card using .NET, install the `Microsoft.AdaptiveCards` NuGet package. Then, leverage the information in the <a href="http://adaptivecards.io" target="_blank">Adaptive Cards</a> repository to understand Adaptive Card schema, explore Adaptive Card elements, and see JSON samples that can be used to create cards of varying composition and complexity. Additionally, you can use the Interactive Visualizer to design Adaptive Card payloads and preview card output.

This code example shows how to create a message that contains an Adaptive Card for a calendar reminder: 

[!code-csharp[Add Adaptive Card attachment](~/includes/code/dotnet-add-attachments.cs#addAdaptiveCardAttachment)]

The resulting card contains three blocks of text, an input field (choice list), and three buttons:

![Adaptive Card calendar reminder](~/media/adaptive-card-reminder.png)

## Additional resources

- <a href="http://adaptivecards.io" target="_blank">Adaptive Cards</a>
- [Activities overview](~/dotnet/bot-builder-dotnet-activities.md)
- [Create messages](~/dotnet/bot-builder-dotnet-create-messages.md)
- [Add media attachments to messages](~/dotnet/bot-builder-dotnet-add-media-attachments.md)

[animationCard]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d9/d78/class_microsoft_1_1_bot_1_1_connector_1_1_animation_card.html

[audioCard]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/db/d71/class_microsoft_1_1_bot_1_1_connector_1_1_audio_card.html 

[heroCard]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d4/dab/class_microsoft_1_1_bot_1_1_connector_1_1_hero_card.html 

[thumbnailCard]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/da/da6/class_microsoft_1_1_bot_1_1_connector_1_1_thumbnail_card.html 

[receiptCard]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d0/df9/class_microsoft_1_1_bot_1_1_connector_1_1_receipt_card.html 

[signinCard]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/dc/d03/class_microsoft_1_1_bot_1_1_connector_1_1_signin_card.html 

[videoCard]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d6/da6/class_microsoft_1_1_bot_1_1_connector_1_1_video_card.html