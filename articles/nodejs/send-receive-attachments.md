---
title: Send and receive attachments | Microsoft Docs
description: Learn how to send and receive messages containing attachments using the Bot Builder SDK for Node.js.
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 02/24/2017
ms.reviewer:
ROBOTS: Index, Follow
---

# Send and receive attachments

<!-- TODO: Supported attachments table -->
<!--
There are four types of rich cards.
| Card type | Description |
|------|------|
|[HeroCard](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.herocard.html) | The Hero card typically contains a single large image, one or more buttons, and text. |
|[ReceiptCard](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.receiptcard.html) | The Receipt card enables a bot to provide a receipt to the user. It typically contains the list of items to include on the receipt, tax and total information, and other text. |
|[SigninCard](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.signincard.html) | The Sign-in card enables a bot to request that a user sign-in. It typically contains text and one or more buttons that the user can click to initiate the sign-in process. |
|[ThumbnailCard](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.thumbnailcard.html) | The Thumbnail card typically contains a single small image, one or more buttons, and text.|
-->

The types of attachments that can be sent varies by channel, but these are the basic types:

* **Media and Files**: You can send files like images, audio and video by setting **contentType** to the MIME type of the [IAttachment object][IAttachment] and then passing a link to the file in **contentUrl**.
* **Cards**: You can send a rich set of visual cards <!-- and custom keyboards --> by setting the **contentType** to the desired card's type and then pass the JSON for the card. If you use one of the rich card builder classes like **HeroCard**, the attachment is automatically filled in for you. See [send a rich card](send-card-buttons.md) for an example of this.

The message object is expected to be an instance of an [IMessage][IMessage] and it's most useful to send the user a message as an object when you’d like to include an attachment like an image. Use the [session.send()][SessionSend] method to send messages in the form of a JSON object. 

## Send attachment example

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

* [IMessage][IMessage]
* [Send a rich card][SendRichCard]
* [session.send][SessionSend]

[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage
[SendRichCard]: send-card-buttons.md
[SessionSend]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#send
[IAttachment]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iattachment.html