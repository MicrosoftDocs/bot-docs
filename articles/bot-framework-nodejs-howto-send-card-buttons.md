---
title: Send and receive attachments | Microsoft Docs
description: Learn how to send and receive cards that contain buttons in a conversational application (bot).
keywords: bot framework, bot, cards, rich card, buttons, send messages, image
author: DeniseMak
manager: rstand
ms.topic: develop-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/24/2017
ms.reviewer:
#ROBOTS: Index
---

# Send and receive rich cards using the Bot Builder SDK for Node.js

<!-- Need to create NET stub.
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-send-card-buttons.md)  
> * [Node.js](bot-framework-nodejs-howto-send-card-buttons.md)
>
--> 
## Introduction

Several channels, like Skype & Facebook, support sending rich graphical cards to users with interactive buttons that the user clicks to initiate an action. 
The SDK provides a rich set of message and card builder classes which can be used to send cards in a cross platform way. The Bot Framework Service will render these cards using schema native to the channel.
For channels that don’t support cards, like SMS, the Bot Framework will do its best to render a reasonable experience to users. 



<!-- TODO: Supported card types include -->
> [!NOTE]
> To do: Add links to info on supported card types.

> [!NOTE]
> To do: Add image.

## Example: Sending a carousel of cards
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
This example uses the [Message](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message) class to build a carousel.  
The carousel is comprised of a list of [HeroCard](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.herocard) classes that contain an image, text, and a single button that triggers buying the item.  
Clicking the “Buy” button triggers sending a message so we need to add a second dialog to catch the button click. 

The ‘buyButtonClick’ dialog will be triggered anytime a message is received that starts with “buy” or “add” and is followed by something containing the word “shirt”. 
The dialog starts by using a couple of regular expressions to look for the color and optional size shirt that the user asked for.
This added flexibility lets us support both button clicks and natural language messages from the user like “please add a large gray shirt to my cart”.
If the color is valid but the size is unknown, we’ll prompt the user to pick a size from a list before adding the item to the cart. 
Once we have all the information needed, we’ll push the item onto a cart that’s persisted using session.userData and then send them a confirmation message.

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



## Additional resources

In this article, we discussed how to send rich cards by using the Bot Builder SDK for Node.js. 
To learn more, see:

> [!NOTE]
> To do: Add links to related content 

[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage