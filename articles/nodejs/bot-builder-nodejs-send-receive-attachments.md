---
title: Send and receive attachments | Microsoft Docs
description: Learn how to send and receive messages containing attachments using the Bot Framework SDK for Node.js.
author: DeniseMak
ms.author: v-demak
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Send and receive attachments

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-add-media-attachments.md)
> - [Node.js](../nodejs/bot-builder-nodejs-send-receive-attachments.md)
> - [REST](../rest-api/bot-framework-rest-connector-add-media-attachments.md)

A message exchange between user and bot can contain media attachments, such as images, video, audio, and files. The types of attachments that can be sent varies by channel, but these are the basic types:

* **Media and Files**: You can send files like images, audio and video by setting **contentType** to the MIME type of the [IAttachment object][IAttachment] and then passing a link to the file in **contentUrl**.
* **Cards**: You can send a rich set of visual cards <!-- and custom keyboards --> by setting the **contentType** to the desired card's type and then pass the JSON for the card. If you use one of the rich card builder classes like **HeroCard**, the attachment is automatically filled in for you. See [send a rich card](bot-builder-nodejs-send-rich-cards.md) for an example of this.

## Add a media attachment
The message object is expected to be an instance of an [IMessage][IMessage] and it's most useful to send the user a message as an object when you’d like to include an attachment like an image. Use the [session.send()][SessionSend] method to send messages in the form of a JSON object. 

## Example

The following example checks to see if the user has sent an attachment, and if they have, it will echo back any image contained in the attachment. You can test this with the Bot Framework Emulator by sending your bot an image.

```javascript
// Create your bot with a function to receive messages from the user
var bot = new builder.UniversalBot(connector, function (session) {
    var msg = session.message;
    if (msg.attachments && msg.attachments.length > 0) {
     // Echo back attachment
     var attachment = msg.attachments[0];
        session.send({
            text: "You sent:",
            attachments: [
                {
                    contentType: attachment.contentType,
                    contentUrl: attachment.contentUrl,
                    name: attachment.name
                }
            ]
        });
    } else {
        // Echo back users text
        session.send("You said: %s", session.message.text);
    }
});
```
## Additional resources

* [Preview features with the Channel Inspector][inspector]
* [IMessage][IMessage]
* [Send a rich card][SendRichCard]
* [session.send][SessionSend]

[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage
[SendRichCard]: bot-builder-nodejs-send-rich-cards.md
[SessionSend]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#send
[IAttachment]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iattachment.html
[inspector]: ../bot-service-channel-inspector.md
