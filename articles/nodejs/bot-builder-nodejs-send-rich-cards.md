---
title: Add rich card attachments to messages | Microsoft Docs
description: Learn how to send interactive, engaging rich cards using the Bot Framework SDK for Node.js.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Add rich card attachments to messages

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]


> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-add-rich-card-attachments.md)
> - [Node.js](../nodejs/bot-builder-nodejs-send-rich-cards.md)
> - [REST](../rest-api/bot-framework-rest-connector-add-rich-cards.md)

Several channels, like Skype & Facebook, support sending rich graphical cards to users with interactive buttons that the user clicks to initiate an action. 
The SDK provides several message and card builder classes which can be used to create and send cards. The Bot Framework Connector Service will render these cards using schema native to the channel, supporting cross-platform communication. If the channel does not support cards, such as SMS, the Bot Framework will do its best to render a reasonable experience to users. 

## Types of rich cards 
The Bot Framework currently supports eight types of rich cards: 

| Card type | Description |
|------|------|
| <a href="/adaptive-cards/get-started/bots">Adaptive Card</a> | A customizable card that can contain any combination of text, speech, images, buttons, and input fields.  See [per-channel support](/adaptive-cards/get-started/bots#channel-status). |
| [Animation Card][animationCard] | A card that can play animated GIFs or short videos. |
| [Audio Card][audioCard] | A card that can play an audio file. |
| [Hero Card][heroCard] | A card that typically contains a single large image, one or more buttons, and text. |
| [Thumbnail Card][thumbnailCard] | A card that typically contains a single thumbnail image, one or more buttons, and text.|
| [Receipt Card][receiptCard] | A card that enables a bot to provide a receipt to the user. It typically contains the list of items to include on the receipt, tax and total information, and other text. |
| [Signin Card][signinCard] | A card that enables a bot to request that a user sign-in. It typically contains text and one or more buttons that the user can click to initiate the sign-in process. |
| [Video Card][videoCard] | A card that can play videos. |

## Send a carousel of Hero cards
The following example shows a bot for a fictional t-shirt company and shows how to send a carousel of cards in response to the user saying “show shirts”. 

```javascript
// Create your bot with a function to receive messages from the user
// Create bot and default message handler
var bot = new builder.UniversalBot(connector, function (session) {
    session.send("Hi... We sell shirts. Say 'show shirts' to see our products.");
});

// Add dialog to return list of shirts available
bot.dialog('showShirts', function (session) {
    var msg = new builder.Message(session);
    msg.attachmentLayout(builder.AttachmentLayout.carousel)
    msg.attachments([
        new builder.HeroCard(session)
            .title("Classic White T-Shirt")
            .subtitle("100% Soft and Luxurious Cotton")
            .text("Price is $25 and carried in sizes (S, M, L, and XL)")
            .images([builder.CardImage.create(session, 'http://petersapparel.parseapp.com/img/whiteshirt.png')])
            .buttons([
                builder.CardAction.imBack(session, "buy classic white t-shirt", "Buy")
            ]),
        new builder.HeroCard(session)
            .title("Classic Gray T-Shirt")
            .subtitle("100% Soft and Luxurious Cotton")
            .text("Price is $25 and carried in sizes (S, M, L, and XL)")
            .images([builder.CardImage.create(session, 'http://petersapparel.parseapp.com/img/grayshirt.png')])
            .buttons([
                builder.CardAction.imBack(session, "buy classic gray t-shirt", "Buy")
            ])
    ]);
    session.send(msg).endDialog();
}).triggerAction({ matches: /^(show|list)/i });
```
This example uses the [Message][Message] class to build a carousel.  
The carousel is comprised of a list of [HeroCard][heroCard] classes that contain an image, text, and a single button that triggers buying the item.  
Clicking the **Buy** button triggers sending a message so we need to add a second dialog to catch the button click. 

## Handle button input

The `buyButtonClick` dialog will be triggered any time a message is received that starts with “buy” or “add” and is followed by something containing the word “shirt”. 
The dialog starts by using a couple of regular expressions to look for the color and optional size shirt that the user asked for.
This added flexibility lets you support both button clicks and natural language messages from the user like “please add a large gray shirt to my cart”.
If the color is valid but the size is unknown, the bot prompts the user to pick a size from a list before adding the item to the cart. 
Once the bot has all the information it needs, it puts the item onto a cart that’s persisted using **session.userData** and then sends the user a confirmation message.

```javascript
// Add dialog to handle 'Buy' button click
bot.dialog('buyButtonClick', [
    function (session, args, next) {
        // Get color and optional size from users utterance
        var utterance = args.intent.matched[0];
        var color = /(white|gray)/i.exec(utterance);
        var size = /\b(Extra Large|Large|Medium|Small)\b/i.exec(utterance);
        if (color) {
            // Initialize cart item
            var item = session.dialogData.item = { 
                product: "classic " + color[0].toLowerCase() + " t-shirt",
                size: size ? size[0].toLowerCase() : null,
                price: 25.0,
                qty: 1
            };
            if (!item.size) {
                // Prompt for size
                builder.Prompts.choice(session, "What size would you like?", "Small|Medium|Large|Extra Large");
            } else {
                //Skip to next waterfall step
                next();
            }
        } else {
            // Invalid product
            session.send("I'm sorry... That product wasn't found.").endDialog();
        }   
    },
    function (session, results) {
        // Save size if prompted
        var item = session.dialogData.item;
        if (results.response) {
            item.size = results.response.entity.toLowerCase();
        }

        // Add to cart
        if (!session.userData.cart) {
            session.userData.cart = [];
        }
        session.userData.cart.push(item);

        // Send confirmation to users
        session.send("A '%(size)s %(product)s' has been added to your cart.", item).endDialog();
    }
]).triggerAction({ matches: /(buy|add)\s.*shirt/i });
```

<!-- 

> [!NOTE]
> When sending a message that contains images, keep in mind that some channels download images before displaying a message to the user.   
> As a result, a message containing an image followed immediately by a message without images may sometimes be flipped in the user's feed.
> For information on how to avoid messages being sent out of order, see [Message ordering][MessageOrder].  

-->
## Add a message delay for image downloads
Some channels tend to download images before displaying a message to the user so that if you send a message containing an image followed immediately by a message without images you’ll sometimes see the messages flipped in the user's feed. To minimize the chance of this you can try to insure that your images are coming from content deliver networks (CDNs) and avoid the use of overly large images. In extreme cases you may even need to insert a 1-2 second delay between the message with the image and the one that follows it. You can make this delay feel a bit more natural to the user by calling **session.sendTyping()** to send a typing indicator before starting your delay. 

<!-- 
To learn more about sending a typing indicator, see [How to send a typing indicator](bot-builder-nodejs-send-typing-indicator.md).
-->

The Bot Framework implements a batching to try to prevent multiple messages from the bot from being displayed out of order. <!-- Unfortunately, not all channels can guarantee this. --> When your bot sends multiple replies to the user, the individual messages will be automatically grouped into a batch and delivered to the user as a set in an effort to preserve the original order of the messages. This automatic batching waits a default of 250ms after every call to **session.send()** before initiating the next call to **send()**.

The message batching delay is configurable. To disable the SDK’s auto-batching logic, set the default delay to a large number and then manually call **sendBatch()** with a callback to invoke after the batch is delivered.

## Send an Adaptive card

The Adaptive Card can contain any combination of text, speech, images, buttons, and input fields. 
Adaptive Cards are created using the JSON format specified in <a href="http://adaptivecards.io" target="_blank">Adaptive Cards</a>, which gives you full control over card content and format. 

To create an Adaptive Card using Node.js, leverage the information within the <a href="http://adaptivecards.io" target="_blank">Adaptive Cards</a> site to understand Adaptive Card schema, explore Adaptive Card elements, and see JSON samples that can be used to create cards of varying composition and complexity. Additionally, you can use the Interactive Visualizer to design Adaptive Card payloads and preview card output.

This code example shows how to create a message that contains an Adaptive Card for a calendar reminder: 

[!code-javascript[Add Adaptive Card attachment](../includes/code/node-send-card-buttons.js#addAdaptiveCardAttachment)]

The resulting card contains three blocks of text, an input field (choice list), and three buttons:

![Adaptive Card calendar reminder](../media/adaptive-card-reminder.png)

## Additional resources

* [Preview features with the Channel Inspector][inspector]
* <a href="http://adaptivecards.io" target="_blank">Adaptive Cards</a>
* [AnimationCard][animationCard]
* [AudioCard][audioCard]
* [HeroCard][heroCard]
* [ThumbnailCard][thumbnailCard]
* [ReceiptCard][receiptCard]
* [SigninCard][signinCard]
* [VideoCard][videoCard]
* [Message][Message]
* [How to send attachments](bot-builder-nodejs-send-receive-attachments.md)

[MessageOrder]: bot-builder-nodejs-manage-conversation-flow.md#message-ordering
[Message]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message
[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage

[animationCard]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.animationcard.html 

[audioCard]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.audiocard.html 

[heroCard]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.herocard.html

[thumbnailCard]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.thumbnailcard.html 

[receiptCard]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.receiptcard.html 

[signinCard]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.signincard.html 

[videoCard]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.videocard.html

[inspector]: ../bot-service-channel-inspector.md
