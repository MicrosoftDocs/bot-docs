---
title: Add media to messages | Microsoft Docs
description: Learn how to add media to messages using the Bot Builder SDK.
keywords: media, messages, images, audio, video, files, MessageFactory, rich messages
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 04/03/2018
monikerRange: 'azure-bot-service-4.0' 
---

# Add media to messages

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

A message exchange between user and bot can contain media attachments, such as images, video, audio, and files. The Bot Builder SDK includes a new `Microsoft.Bot.Builder.MessageFactory` class, designed to ease the task of sending rich messages to the user.

## Send attachments

To send the user content like an image or a video, you can add an attachment or list of attachments to a message.

# [C#](#tab/csharp)

The `Attachments` property of the `Activity` object contains an array of `Attachment` objects that represent the media attachments and rich cards attached to the message. To add a media attachment to a message, create an `Attachment` object for the `message` activity and set the `ContentType`, `ContentUrl`, and `Name` properties. 

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

// Create the activity and add an attachment.
var activity = MessageFactory.Attachment(
    new Attachment { ContentUrl = "imageUrl.png", ContentType = "image/png" }
);

// Send the activity to the user.
await context.SendActivity(activity);
```

The message factory's `Attachment` method can also send a list of attachments, stacked on on top of another.

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

// Create the activity and add an attachment.
var activity = MessageFactory.Attachment(new Attachment[]
{
    new Attachment { ContentUrl = "imageUrl1.png", ContentType = "image/png" },
    new Attachment { ContentUrl = "imageUrl2.png", ContentType = "image/png" },
    new Attachment { ContentUrl = "imageUrl3.png", ContentType = "image/png" }
});

// Send the activity to the user.
await context.SendActivity(activity);
```

# [JavaScript](#tab/javascript)

To send the user a single piece of content like an image or a video, you can send media contained in a URL:

```javascript
const {MessageFactory} = require('botbuilder');
let imageOrVideoMessage = MessageFactory.contentUrl('imageUrl.png', 'image/jpeg')

// Send the activity to the user.
await context.sendActivity(imageOrVideoMessage);
```

To send a list of attachments, stacked one on top of another:
<!-- TODO: Convert the hero cards to image attachments in this example. -->

```javascript
// require MessageFactory and CardFactory from botbuilder.
const {MessageFactory} = require('botbuilder');
const {CardFactory} = require('botbuilder');

let messageWithCarouselOfCards = MessageFactory.list([
    CardFactory.heroCard('title1', ['imageUrl1'], ['button1']),
    CardFactory.heroCard('title2', ['imageUrl2'], ['button2']),
    CardFactory.heroCard('title3', ['imageUrl3'], ['button3'])
]);

await context.sendActivity(messageWithCarouselOfCards);
```

---

If an attachment is an image, audio, or video, the Connector service will communicate attachment data to the channel in a way that enables the [channel](~/v4sdk/bot-builder-channeldata.md) to render that attachment within the conversation. If the attachment is a file, the file URL will be rendered as a hyperlink within the conversation.

## Send a hero card

Besides simple image or video attachments, you can attach a **hero card**, which allows you to combine images and buttons in one object, and send them to the user.

# [C#](#tab/csharp)

To compose a message with a hero card and button, you can attach a `HeroCard` to a message:

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

// Create the activity and attach a Hero card.
var activity = MessageFactory.Attachment(
    new HeroCard(
        title: "White T-Shirt",
        images: new CardImage[] { new CardImage(url: "imageUrl.png") },
        buttons: new CardAction[]
        {
            new CardAction(title: "buy", type: ActionTypes.ImBack, value: "buy")
        })
    .ToAttachment());

// Send the activity as a reply to the user.
await context.SendActivity(activity);
```

# [JavaScript](#tab/javascript)

To send the user a card and button, you can attach a `heroCard` to the message:

```javascript
// require MessageFactory and CardFactory from botbuilder.
const {MessageFactory} = require('botbuilder');
const {CardFactory} = require('botbuilder');

const message = MessageFactory.attachment(
     CardFactory.heroCard(
        'White T-Shirt',
        ['https://example.com/whiteShirt.jpg'],
        ['buy']
    )
 );

await context.sendActivity(message);
```

---

<!--Lifted from the RESP API documentation-->

A rich card comprises a title, description, link, and images. A message can contain multiple rich cards, displayed in either list format or carousel format. The Bot Framework currently supports these types of rich cards:

> [!TIP]
> To determine the type of rich cards that a channel supports and see how the channel renders rich cards, see the [Channel Inspector](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-channel-inspector?view=azure-bot-service-4.0). Consult the channel's documentation for information about limitations on the contents of cards (for example, the maximum number of buttons or maximum length of title).

# [C#](#tab/csharp)

| Card type | Description |
| :---- | :---- |
| AnimationCard | A card that can play animated GIFs or short videos. |
| AudioCard | A card that can play an audio file. |
| HeroCard | A card that typically contains a single large image, one or more buttons, and text. |
| ThumbnailCard | A card that typically contains a single thumbnail image, one or more buttons, and text. |
| ReceiptCard | A card that enables a bot to provide a receipt to the user. It typically contains the list of items to include on the receipt, tax and total information, and other text. |
| SignInCard | A card that enables a bot to request that a user sign-in. It typically contains text and one or more buttons that the user can click to initiate the sign-in process. |
| VideoCard | A card that can play videos.

# [JavaScript](#tab/javascript)

| Card type | Description |
| :---- | :---- |
| animationCard | A card that can play animated GIFs or short videos. |
| audioCard | A card that can play an audio file. |
| heroCard | A card that typically contains a single large image, one or more buttons, and text. |
| thumbnailCard | A card that typically contains a single thumbnail image, one or more buttons, and text. |
| receiptCard | A card that enables a bot to provide a receipt to the user. It typically contains the list of items to include on the receipt, tax and total information, and other text. |
| signInCard | A card that enables a bot to request that a user sign-in. It typically contains text and one or more buttons that the user can click to initiate the sign-in process. |
| videoCard | A card that can play videos.

---

### Process events within rich cards

To process events within rich cards, use _card action_ objects to specify what should happen when the user clicks a button or taps a section of the card.

To function correctly, assign an action type to each clickable item on the card. This table lists the valid values for the type property of a card action object and describes the expected contents of the value property for each type.

| Type | Value |
| :---- | :---- |
| openUrl | URL to be opened in the built-in browser |
| imBack | Text of the message to send to the bot (from the user who clicked the button or tapped the card). This message (from user to bot) will be visible to all conversation participants via the client application that is hosting the conversation. |
| postBack | Text of the message to send to the bot (from the user who clicked the button or tapped the card). Some client applications may display this text in the message feed, where it will be visible to all conversation participants. |
| call | Destination for a phone call in this format: `tel:123123123123` |
| playAudio | URL of audio to be played |
| playVideo | URL of video to be played |
| showImage | URL of image to be displayed |
| downloadFile | URL of file to be downloaded |
| signin | URL of OAuth flow to be initiated |

## Send an Adaptive Card

You can also send an Adaptive Card as an attachment; however, only a few channels currently support adaptive cards.

# [C#](#tab/csharp)
To use adaptive cards, be sure to add the `Microsoft.AdaptiveCards` NuGet package.


> [!NOTE]
> You should test this feature with the channels your bot will use to determine whether those channels support adaptive cards.

```csharp
using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

// Create the activity and attach an Adaptive Card.
var card = new AdaptiveCard();
card.Body.Add(new TextBlock()
{
    Text = "<title>",
    Size = TextSize.Large,
    Wrap = true,
    Weight = TextWeight.Bolder
});
card.Body.Add(new TextBlock() { Text = "<message text>", Wrap = true });
card.Body.Add(new TextInput()
{
    Id = "Title",
    Value = string.Empty,
    Style = TextInputStyle.Text,
    Placeholder = "Title",
    IsRequired = true,
    MaxLength = 50
});
card.Actions.Add(new SubmitAction() { Title = "Submit", DataJson = "{ Action:'Submit' }" });
card.Actions.Add(new SubmitAction() { Title = "Cancel", DataJson = "{ Action:'Cancel'}" });

var activity = MessageFactory.Attachment(new Attachment(AdaptiveCard.ContentType, content: card));

// Send the activity as a reply to the user.
await context.SendActivity(activity);
```

# [JavaScript](#tab/javascript)

```javascript
const {CardFactory} = require("botbuilder");

const message = CardFactory.adaptiveCard({
    "$schema": "http://adaptivecards.io/schemas/adaptive-card.json",
    "version": "1.0",
    "type": "AdaptiveCard",
    "speak": "Your flight is confirmed for you from San Francisco to Amsterdam on Friday, October 10 8:30 AM",
    "body": [
        {
            "type": "TextBlock",
            "text": "Passenger",
            "weight": "bolder",
            "isSubtle": false
        },
        {
            "type": "TextBlock",
            "text": "Sarah Hum",
            "separator": true
        },
        {
            "type": "TextBlock",
            "text": "1 Stop",
            "weight": "bolder",
            "spacing": "medium"
        },
        {
            "type": "TextBlock",
            "text": "Fri, October 10 8:30 AM",
            "weight": "bolder",
            "spacing": "none"
        },
        {
            "type": "ColumnSet",
            "separator": true,
            "columns": [
                {
                    "type": "Column",
                    "width": 1,
                    "items": [
                        {
                            "type": "TextBlock",
                            "text": "San Francisco",
                            "isSubtle": true
                        },
                        {
                            "type": "TextBlock",
                            "size": "extraLarge",
                            "color": "accent",
                            "text": "SFO",
                            "spacing": "none"
                        }
                    ]
                },
                {
                    "type": "Column",
                    "width": "auto",
                    "items": [
                        {
                            "type": "TextBlock",
                            "text": " "
                        },
                        {
                            "type": "Image",
                            "url": "http://messagecardplayground.azurewebsites.net/assets/airplane.png",
                            "size": "small",
                            "spacing": "none"
                        }
                    ]
                },
                {
                    "type": "Column",
                    "width": 1,
                    "items": [
                        {
                            "type": "TextBlock",
                            "horizontalAlignment": "right",
                            "text": "Amsterdam",
                            "isSubtle": true
                        },
                        {
                            "type": "TextBlock",
                            "horizontalAlignment": "right",
                            "size": "extraLarge",
                            "color": "accent",
                            "text": "AMS",
                            "spacing": "none"
                        }
                    ]
                }
            ]
        },
        {
            "type": "ColumnSet",
            "spacing": "medium",
            "columns": [
                {
                    "type": "Column",
                    "width": "1",
                    "items": [
                        {
                            "type": "TextBlock",
                            "text": "Total",
                            "size": "medium",
                            "isSubtle": true
                        }
                    ]
                },
                {
                    "type": "Column",
                    "width": 1,
                    "items": [
                        {
                            "type": "TextBlock",
                            "horizontalAlignment": "right",
                            "text": "$1,032.54",
                            "size": "medium",
                            "weight": "bolder"
                        }
                    ]
                }
            ]
        }
    ]
});

// send adaptive card as attachment 
await context.sendActivity({ attachments: [message] })
```

---

## Send a carousel of cards

Messages can also include multiple attachments in a carousel layout, which places the attachments side by side and allows the user to scroll across.

# [C#](#tab/csharp)

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

// Create the activity and attach a set of Hero cards.
var activity = MessageFactory.Carousel(
    new Attachment[]
    {
        new HeroCard(
            title: "title1",
            images: new CardImage[] { new CardImage(url: "imageUrl1.png") },
            buttons: new CardAction[]
            {
                new CardAction(title: "button1", type: ActionTypes.ImBack, value: "item1")
            })
        .ToAttachment(),
        new HeroCard(
            title: "title2",
            images: new CardImage[] { new CardImage(url: "imageUrl2.png") },
            buttons: new CardAction[]
            {
                new CardAction(title: "button2", type: ActionTypes.ImBack, value: "item2")
            })
        .ToAttachment(),
        new HeroCard(
            title: "title3",
            images: new CardImage[] { new CardImage(url: "imageUrl3.png") },
            buttons: new CardAction[]
            {
                new CardAction(title: "button3", type: ActionTypes.ImBack, value: "item3")
            })
        .ToAttachment()
    });

// Send the activity as a reply to the user.
await context.SendActivity(activity);
```

# [JavaScript](#tab/javascript)

```javascript
// require MessageFactory and CardFactory from botbuilder.
const {MessageFactory} = require('botbuilder');
const {CardFactory} = require('botbuilder');

//  init message object
let messageWithCarouselOfCards = MessageFactory.carousel([
    CardFactory.heroCard('title1', ['imageUrl1'], ['button1']),
    CardFactory.heroCard('title2', ['imageUrl2'], ['button2']),
    CardFactory.heroCard('title3', ['imageUrl3'], ['button3'])
]);

await context.sendActivity(messageWithCarouselOfCards);
```

---

## Send suggested actions

You can also create a message that contains a list of suggested actions (also known as "quick replies") that will be shown to the user for a single turn of the conversation.

# [C#](#tab/csharp)

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

// Create the activity and add suggested actions and summary text.
var activity = MessageFactory.SuggestedActions(
    new CardAction[]
    {
        new CardAction(title: "red", type: ActionTypes.ImBack, value: "red"),
        new CardAction( title: "green", type: ActionTypes.ImBack, value: "green"),
        new CardAction(title: "blue", type: ActionTypes.ImBack, value: "blue")
    }, text: "Choose a color");
activity.Summary = "This message contains a set of suggested actions";

// Send the activity as a reply to the user.
await context.SendActivity(activity);
```

# [JavaScript](#tab/javascript)

```javascript
//  init message object
const basicMessage = MessageFactory.suggestedActions(
    ['Option one', 'Option two', 'Option 3'], 'Please choose one option');

await context.sendActivity(basicMessage) // send the message
```

---

## Additional resources
[Preview features with the Channel Inspector](../bot-service-channel-inspector.md)

