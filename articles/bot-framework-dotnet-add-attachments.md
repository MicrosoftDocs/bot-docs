---
title: Add attachments to messages using the Bot Framework Connector service and .NET | Microsoft Docs
description: Learn how to add attachments to messages using the Bot Framework Connector service via the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, Connector, Connector service, attachment, card, rich card
author: kbrandl
manager: rstand
ms.topic: develop-dotnet-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 03/13/2017
ms.reviewer:
#ROBOTS: Index
---

# Add attachments to messages

A message exchange between user and bot can 
contain media attachments (e.g., image, video, audio, file) 
or even rich cards that are rendered as a list or carousel. 
This article describes how to add attachments to messages using the Bot Framework Connector service via the 
Bot Builder SDK for .NET. 

## Types of attachments

Within an **Activity** object, the **Attachments** property contains an array of **Attachment** objects 
that represent any attachments on the message. 
An attachment may be a media attachment (e.g., image, video, audio, file) or a rich card attachment.

## Sending media attachments

To include a media attachment within a message, 
simply create an **Attachment** object and set the **ContentType**, **ContentUrl**, and **Name** properties. 
The following code example shows how to add an image to a message.

[!code-csharp[Add media attachment](../includes/code/dotnet-add-attachments.cs#addMediaAttachment)]

> [!NOTE]
> If an attachment's format is image, audio, or video, the Connector service will communicate 
> attachment data to the channel in a way that enables the channel to render that attachment within the conversation. 
> If the attachment is a file, the file URL will be rendered as a hyperlink within the conversation.

## Sending rich cards

A rich card is composed of a title, description, link, and image(s). 
A message can contain multiple rich cards, displayed in either list format or carousel format.
The Bot Framework currently supports four types of rich cards: Hero card, Thumbnail card, Receipt card, and Sign-in card.

> [!TIP]
> To display multiple rich cards in list format, set the activity's **AttachmentLayout** property to "list". 
> To display multiple rich cards in carousel format, set the activity's **AttachmentLayout** property to "carousel". 
> If the channel does not support carousel format, it will display the rich cards in list format, even if the **AttachmentLayout** property specifies "carousel".

### Hero card

The Hero card typically contains a single large image, one or more buttons, and text. 

The following code sample shows how to create a reply message that contains three Hero cards rendered in carousel format. 

[!code-csharp[Add HeroCard attachment](../includes/code/dotnet-add-attachments.cs#addHeroCardAttachment)]

### Thumbnail card

The Thumbnail card typically contains a single small image, one or more buttons, and text. 

The following code sample shows how to create a reply message that contains two Thumbnail cards rendered in list format. 

[!code-csharp[Add ThumbnailCard attachment](../includes/code/dotnet-add-attachments.cs#addThumbnailCardAttachment)]

### Receipt card

The Receipt card enables a bot to provide a receipt to the user. 
It typically contains the list of items to include on the receipt, tax and total information, and other text. 

The following code sample shows how to create a reply message that contains a Receipt card. 

[!code-csharp[Add ReceiptCard attachment](../includes/code/dotnet-add-attachments.cs#addReceiptCardAttachment)]

### Sign-in card

The Sign-in card enables a bot to request that a user sign-in. 
It typically contains text and one or more buttons that the user can click to initiate the sign-in process. 

The following code sample shows how to create a reply message that contains a Sign-in card.

[!code-csharp[Add SignInCard attachment](../includes/code/dotnet-add-attachments.cs#addSignInCardAttachment)]

## Processing events within rich cards

The code examples above show how to create buttons within rich cards by using the **CardAction** object. 
By using card actions, you can specify the action that occurs whenever the user clicks a button or taps a 
section of the card. Each **CardAction** object contains the following properties:

| Property | Type | Description | 
|----|----|----|
| Type | string | type of action (one of the values specified in the table below) |
| Title | string | title of the button |
| Image | string | image URL for the button |
| Value | string | value needed to perform the specified type of action |

The following table lists the valid values for **CardAction.Type** and describes 
the expected contents of **CardAction.Value** for each type.

| CardAction.Type | CardAction.Value | 
|----|----|
| openUrl | URL to be opened in the built-in browser |
| imBack | Text of the message to send to the bot (from the user who clicked the button or tapped the card). This message (from user to bot) will be visible to all conversation participants via the client application that is hosting the conversation. |
| postBack | Text of the message to send to the bot (from the user who clicked the button or tapped the card). This message will not be displayed by the client application that is hosting the conversation. |
| call | Destination for a phone call in the following format: **tel:123123123123** |
| playAudio | URL of audio to be played |
| playVideo | URL of video to be played |
| showImage | URL of image to be displayed |
| downloadFile | URL of file to be downloaded |
| signin | URL of OAuth flow to be initiated |

## Additional resources

- [Send and receive activities](bot-framework-dotnet-send-and-receive.md)
- [Compose messages](bot-framework-dotnet-compose-messages.md)
- [Implement channel-specific functionality](bot-framework-dotnet-channeldata.md)