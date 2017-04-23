---
title: Add media attachments to messages | Microsoft Docs
description: Learn how to add media attachments to messages using the Bot Framework Connector service and the Bot Builder SDK for .NET.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
---

# Add media attachments to messages

A message exchange between user and bot can contain media attachments (e.g., image, video, audio, file).

The `Attachments` property of the `Activity` object contains an array of `Attachment` objects
that represent the media attachments and [rich cards](~/dotnet/add-rich-card-attachments.md) attached to the message.

> [!NOTE]
> For information about how to add rich cards to messages, see
> [Add rich cards to messages](~/dotnet/add-rich-card-attachments.md).

## Add a media attachment  
Create an `Attachment` object for the `message` activity.
Set the `ContentType`, `ContentUrl`, and `Name` properties.

If an attachment is image, audio, or video, the Connector service will communicate attachment data to the channel in a way that enables the [channel](~/dotnet/channeldata.md) to render that attachment within the conversation.
If the attachment is a file, the file URL will be rendered as a hyperlink within the conversation.

## Example
This code example shows how to add an image to a message:

[!code-csharp[Add media attachment](~/includes/code/dotnet-add-attachments.cs#addMediaAttachment)]

## Additional resources

- [Activity types](~/dotnet/activities.md)
- [Send and receive activities](~/dotnet/connector.md)
- [Create messages](~/dotnet/create-messages.md)
- [Add rich cards to messages](~/dotnet/add-rich-card-attachments.md)
- [Implement channel-specific functionality](~/dotnet/channeldata.md)
