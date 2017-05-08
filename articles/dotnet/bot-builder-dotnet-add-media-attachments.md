---
title: Add media attachments to messages | Microsoft Docs
description: Learn how to add media attachments to messages using the Bot Builder SDK for .NET.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---

# Add media attachments to messages
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-add-media-attachments.md)
> - [Node.js](../nodejs/bot-builder-nodejs-send-receive-attachments.md)

A message exchange between user and bot can contain media attachments (e.g., image, video, audio, file). 
The `Attachments` property of the `Activity` object contains an array of `Attachment` objects 
that represent the media attachments and rich cards within to the message. 

> [!NOTE]
> For information about how to add rich cards to messages, see 
> [Add rich cards to messages](~/dotnet/bot-builder-dotnet-add-rich-card-attachments.md).

## Add a media attachment  

To add a media attachment to a message, create an `Attachment` object for the `message` activity and set 
the `ContentType`, `ContentUrl`, and `Name` properties. 

[!code-csharp[Add media attachment](~/includes/code/dotnet-add-attachments.cs#addMediaAttachment)]

If an attachment is an image, audio, or video, the Connector service will communicate attachment data to the channel in a way that enables the [channel](~/dotnet/bot-builder-dotnet-channeldata.md) to render that attachment within the conversation. If the attachment is a file, the file URL will be rendered as a hyperlink within the conversation.

## Additional resources

- [Activities overview](~/dotnet/bot-builder-dotnet-activities.md)
- [Create messages](~/dotnet/bot-builder-dotnet-create-messages.md)
- [Add rich cards to messages](~/dotnet/bot-builder-dotnet-add-rich-card-attachments.md)
