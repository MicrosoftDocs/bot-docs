---
title: Add attachments to messages using the Bot Framework Connector service and .NET | Microsoft Docs
description: Learn how to add attachments to messages using the Bot Framework Connector service via the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, Connector, Connector service, attachment, card, rich card
author: kbrandl
manager: rstand
ms.topic: develop-dotnet-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 03/09/2017
ms.reviewer:
#ROBOTS: Index
---

# Add attachments to messages

> [!NOTE]
> Content coming soon...
> 
> To do: include 'attachment-related' content from [here](https://docs.botframework.com/en-us/csharp/builder/sdkreference/activities.html).

A message exchange between the user and bot can be as simple as an exchange of text strings or as complex as a multiple card carousel with buttons and actions. For most bots, the **Text** field is the only **Activity** field that you need to worry about. 
A person sent you some text or your bot is sending back some text. 
If your bot converses in a language other than English, you'd also need to set the **Locale** field.

The **Activity** object also includes the **Attachments** field, which may contain an array of **Attachment** objects. Attachments can be a simple media type such as an image or video, or they can be rich card types such as a HeroCard or Receipt Card. 
A rich card is made up of a title, link, description and image. 
Additionally, you can use the **AttachmentLayout** field to specify whether to display the cards as a list or a carousel.

To pass a simple media attachment (for example, an image, audio, video or file) to an activity, you create an **Attachment** object and set the **ContentType**, **ContentUrl**, and **Name** fields as shown below.

[!code-csharp[Add media attachment](../includes/code/dotnet-add-attachments.cs#addMediaAttachment)]

The following example shows how to add a HeroCard as an attachment to a reply message.

[!code-csharp[Add HeroCard attachment](../includes/code/dotnet-add-attachments.cs#addHeroCardAttachment)]
