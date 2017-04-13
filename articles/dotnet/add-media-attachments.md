---
title: Add attachments to messages using the Bot Framework Connector service and .NET | Microsoft Docs
description: Learn how to add attachments to messages using the Bot Framework Connector service via the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, Connector, Connector service, attachment, media attachment, image, file, video, audio
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/13/2017
ms.reviewer:
ROBOTS: Index, Follow
---

# Add media attachments to messages

A message exchange between user and bot can contain media attachments (e.g., image, video, audio, file). 
Within an `Activity` object, the `Attachments` property contains an array of `Attachment` objects 
that represent the media attachments (and [rich cards](~/dotnet/add-rich-card-attachments.md)) within the message. 

This article describes how to add media attachments to messages using the Bot Framework Connector service via the 
Bot Builder SDK for .NET. 

> [!NOTE]
> For information about how to add rich cards to messages, see 
> [Add rich cards to messages](~/dotnet/add-rich-card-attachments.md).

## Add an attachment to a message

To include a media attachment within a message, 
create an `Attachment` object and set the `ContentType`, `ContentUrl`, and `Name` properties. 
If an attachment's format is image, audio, or video, the Connector service will communicate 
attachment data to the channel in a way that enables the channel to render that attachment within the conversation. 
If the attachment is a file, the file URL will be rendered as a hyperlink within the conversation.

This code example shows how to add an image to a message:

[!code-csharp[Add media attachment](~/includes/code/dotnet-add-attachments.cs#addMediaAttachment)]

## Additional resources

- [Activity types](~/dotnet/activities.md)
- [Send and receive activities](~/dotnet/connector.md)
- [Create messages](~/dotnet/create-messages.md)
- [Add rich cards to messages](~/dotnet/add-rich-card-attachments.md)
- [Implement channel-specific functionality](~/dotnet/channeldata.md)