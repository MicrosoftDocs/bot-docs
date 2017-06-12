---
title: Add media attachments to messages | Microsoft Docs
description: Learn how to add media attachments to messages using the Bot Connector service. 
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
> - [REST](../rest-api/bot-framework-rest-connector-add-media-attachments.md)

Bots and channels typically exchange text strings but some channels also support exchanging attachments, which lets your bot send richer messages to users. For example, your bot can send media attachments (e.g., images, videos, audio, files) and [rich cards](bot-framework-rest-connector-add-rich-cards.md). This article describes how to add media attachments to messages using the Bot Connector service.

> [!TIP]
> To determine the type and number of attachments that a channel supports, and how the channel renders attachments, 
> see the [Channel Inspector][ChannelInspector].

## Add a media attachment  

To add a media attachment to a message, create an [Attachment][Attachment] object, set the `name` property, set the `contentUrl` property to the URL of the media file, and set the `contentType` property to the appropriate media type (e.g., **image/png**, **audio/wav**, **video/mp4**). Then within the [Activity][Activity] object that represents your message, specify your [Attachment][Attachment] object within the `attachments` array. 

The following example shows a request that sends a message containing text and a single image attachment. In this example request, `https://smba.trafficmanager.net/apis` represents the base URI; the base URI for requests that your bot issues may be different. For details about setting the base URI, see [API Reference](bot-framework-rest-connector-api-reference.md#base-uri).

```http
POST https://smba.trafficmanager.net/apis/v3/conversations/abcd1234/activities/5d5cdc723
Authorization: Bearer ACCESS_TOKEN
Content-Type: application/json
```

```json
{
    "type": "message",
    "from": {
        "id": "12345678",
        "name": "sender's name"
    },
    "conversation": {
        "id": "abcd1234",
        "name": "conversation's name"
   },
   "recipient": {
        "id": "1234abcd",
        "name": "recipient's name"
    },
    "text": "Here's a picture of the duck I was telling you about.",
    "attachments": [
        {
            "contentType": "image/png",
            "contentUrl": "http://aka.ms/Fo983c",
            "name": "duck-on-a-rock.jpg"
        }
    ],
    "replyToId": "5d5cdc723"
}
```

For channels that support inline binaries of an image, you can set the `content` property of the `Attachment` to a base64 binary of the image (for example, **data:image/png;base64,iVBORw0KGgo…**). The channel will display the image or the image's URL next to the message's text string.

You can attach a video file or audio file to a message by using the same process as described above for an image file. Depending on the channel, the video and audio may be played inline or it may be displayed as a link.

> [!NOTE] 
> Your bot may also receive messages that contain media attachments.
> For example, a message that your bot receives may contain an attachment 
> if the channel enables the user to upload a photo to be analyzed or a document to be stored.

## Additional resources

- [Create messages](bot-framework-rest-connector-create-messages.md)
- [Send and receive messages](bot-framework-rest-connector-send-and-receive-messages.md)
- [Add rich cards to messages](bot-framework-rest-connector-add-rich-cards.md)
- [Channel Inspector][ChannelInspector]

[ChannelInspector]: https://docs.botframework.com/en-us/channel-inspector/channels/Skype/#navtitle

[Activity]: bot-framework-rest-connector-api-reference.md#activity-object
[Attachment]: bot-framework-rest-connector-api-reference.md#attachment-object
