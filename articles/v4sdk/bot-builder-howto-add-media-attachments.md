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

The `Attachments` <!--<xref:Microsoft.Bot.Schema.Activity.Attachments*>-->
property of the `Activity` <!--<xref:Microsoft.Bot.Schema.Activity>-->
object contains an array of `Attachment` <!--<xref:Microsoft.Bot.Schema.Attachment>-->
objects that represent the media attachments and rich cards attached to the message. To add a media attachment to a message, use the `MessageFactory` <!--<xref:Microsoft.Bot.Builder.MessageFactory>-->
class `Attachment` <!--<xref:Microsoft.Bot.Builder.MessageFactory>-->
method to create an `Attachment` object for the `message` activity and set the
`ContentType` <!--<xref:Microsoft.Bot.Schema.Attachment.ContentType*>-->,
`ContentUrl` <!--<xref:Microsoft.Bot.Schema.Attachment.Content*>-->,
and `Name` <!--<xref:Microsoft.Bot.Schema.Attachment.Name*>--> properties.

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
```

```csharp
// Create the activity and add an attachment.
var activity = MessageFactory.Attachment(
    new Attachment
    {
        ContentUrl = "imageUrl.png",
        ContentType = "image/png",
        Name = "imageName",
    }
);

// Send the activity to the user.
await context.SendActivityAsync(activity, token);
```

The message factory's `Attachment` method can also send a list of attachments, stacked on top of each other.

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
```

```csharp
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
let imageOrVideoMessage = MessageFactory.contentUrl('imageUrl.png', 'image/png')

// Send the activity to the user.
await context.sendActivity(imageOrVideoMessage);
```

To send a list of attachments, stacked one on top of another:
<!-- TODO: Convert the hero cards to image attachments in this example. -->

```javascript
// require MessageFactory and CardFactory from botbuilder.
const {MessageFactory, CardFactory} = require('botbuilder');

let messageWithCarouselOfCards = MessageFactory.list([
    CardFactory.heroCard('title1', ['imageUrl1.png'], ['button1']),
    CardFactory.heroCard('title2', ['imageUrl2.png'], ['button2']),
    CardFactory.heroCard('title3', ['imageUrl3.png'], ['button3'])
]);

await context.sendActivity(messageWithCarouselOfCards);
```

---

If an attachment is an image, audio, or video, the Connector service will communicate attachment data to the channel in a way that enables the [channel](~/v4sdk/bot-builder-channeldata.md) to render that attachment within the conversation. If the attachment is a file, the file URL will be rendered as a hyperlink within the conversation.

## Send a hero card

Besides simple image or video attachments, you can attach a **hero card**, which allows you to combine images and buttons in one object, and send them to the user.

# [C#](#tab/csharp)

To compose a message with a hero card and button, you can attach a
`HeroCard` <!--<xref:Microsoft.Bot.Schema.HeroCard>-->
to a message:

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
```

```csharp
// Create the activity and attach a Hero card.
var activity = MessageFactory.Attachment(
    new HeroCard(
        title: "heroCardTitle",
        images: new CardImage[] { new CardImage { Url = "imageUrl.png" } },
        buttons: new CardAction[]
        {
            new CardAction
            {
                Type = ActionTypes.OpenUrl,
                Title = "Azure Bot Service Documentation",
                Value = "https://docs.microsoft.com/azure/bot-service/?view=azure-bot-service-4.0",
            },
        })
    .ToAttachment());

// Send the activity as a reply to the user.
await context.SendActivityAsync(activity, token);
```

# [JavaScript](#tab/javascript)

To compose a message with a hero card and button, you can attach a
`HeroCard` to a message:

```javascript
// require MessageFactory and CardFactory from botbuilder.
const {MessageFactory} = require('botbuilder');
const {CardFactory} = require('botbuilder');

const message = MessageFactory.attachment(
    CardFactory.heroCard(
        'heroCardTitle',
        CardFactory.images(['imageUrl.png']),
        CardFactory.actions([
            {
                type: 'ActionTypes.OpenUrl',
                title: 'Azure Bot Service Documentation',
                value: 'https://docs.microsoft.com/azure/bot-service/?view=azure-bot-service-4.0'
            }
        ])
    )
);

await context.sendActivity(message);
```

---

<!--Lifted from the RESP API documentation-->

A rich card comprises a title, description, link, and images. A message can contain multiple rich cards, displayed in either list format or carousel format. The Bot Builder SDK currently supports a wide range of rich cards. To see a listing of rich cards and channels in which they are supported, see [Design UX elements](../bot-service-design-user-experience.md).

> [!TIP]
> To determine the type of rich cards that a channel supports and see how the channel renders rich cards, see the [Channel Inspector](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-channel-inspector?view=azure-bot-service-4.0). Consult the channel's documentation for information about limitations on the contents of cards (for example, the maximum number of buttons or maximum length of title).

## Process events within rich cards

To process events within rich cards, use _card action_ objects to specify what should happen when the user clicks a button or taps a section of the card.

To function correctly, assign an action type to each clickable item on the card. This table lists the valid values for the type property of a card action object and describes the expected contents of the value property for each type.

| Type | Value |
| :---- | :---- |
| openUrl | URL to be opened in the built-in browser. Responds to Tap or Click by opening the URL. |
| imBack | Text of the message to send to the bot (from the user who clicked the button or tapped the card). This message (from user to bot) will be visible to all conversation participants via the client application that is hosting the conversation. |
| postBack | Text of the message to send to the bot (from the user who clicked the button or tapped the card). Some client applications may display this text in the message feed, where it will be visible to all conversation participants. |
| call | Destination for a phone call in this format: `tel:123123123123` Responds to Tap or Click by initiating a call.|
| playAudio | URL of audio to be played. Responds to Tap or Click by playing the audio. |
| playVideo | URL of video to be played. Responds to Tap or Click by playing the video. |
| showImage | URL of image to be displayed. Responds to Tap or Click by displaying the image. |
| downloadFile | URL of file to be downloaded.  Responds to Tap or Click by downloading the file. |
| signin | URL of OAuth flow to be initiated. Responds to Tap or Click by initiating signin. |

## Hero card using various event types

The following code shows examples using various rich card events.

# [C#](#tab/csharp)

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
```

```csharp
// Create the activity and attach a Hero card.
var activity = MessageFactory.Attachment(
    new HeroCard(
        title: "Holler Back Buttons",
        images: new CardImage[] { new CardImage(url: "imageUrl.png") },
        buttons: new CardAction[]
        {
            new CardAction(title: "Shout Out Loud", type: ActionTypes.ImBack, value: "You can ALL hear me!"),
            new CardAction(title: "Much Quieter", type: ActionTypes.PostBack, value: "Shh! My Bot friend hears me."),
            new CardAction
            {
                Type = ActionTypes.OpenUrl,
                Title = "Azure Bot Service Documentation",
                Value = "https://docs.microsoft.com/azure/bot-service/?view=azure-bot-service-4.0",
            },
        })
    .ToAttachment());

// Send the activity as a reply to the user.
await context.SendActivityAsync(activity, token);
```

# [JavaScript](#tab/javascript)

```javascript
const {ActionTypes} = require("botbuilder");
```

```javascript
const hero = MessageFactory.attachment(
    CardFactory.heroCard(
        'Holler Back Buttons',
        ['https://example.com/whiteShirt.jpg'],
        [{
            type: ActionTypes.ImBack,
            title: 'ImBack',
            value: 'You can ALL hear me! Shout Out Loud'
        },
        {
            type: ActionTypes.PostBack,
            title: 'PostBack',
            value: 'Shh! My Bot friend hears me. Much Quieter'
        },
        {
            type: ActionTypes.OpenUrl,
            title: 'OpenUrl',
            value: 'https://en.wikipedia.org/wiki/{cardContent.Key}'
        }]
    )
    CardFactory.actions([{
        type: 'ActionTypes.OpenUrl',
        title: 'Azure Bot Service Documentation',
        value: 'https://docs.microsoft.com/azure/bot-service/?view=azure-bot-service-4.0'
    }])
);

await context.sendActivity(hero);

```

---

## Send an Adaptive Card

You can also send an Adaptive Card as an attachment. Currently not all channels support adaptive cards.
To find the latest information on Adaptive Card channel support,
see the [Adaptive Cards Visualizer](http://adaptivecards.io/visualizer/).

# [C#](#tab/csharp)

To use adaptive cards, be sure to add the `Microsoft.AdaptiveCards` NuGet package.

> [!NOTE]
> You should test this feature with the channels your bot will use to determine whether those channels support adaptive cards.

```csharp
using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
```

```csharp
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
await context.SendActivityAsync(activity, token);
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
using Microsoft.Bot.Schema;
```

```csharp
// Create the activity and attach a set of Hero cards.
var activity = MessageFactory.Carousel(
    new Attachment[]
    {
        new HeroCard(
            title: "cardTitle1",
            images: new CardImage[] { new CardImage(url: "imageUrl1.png") },
            buttons: new CardAction[]
            {
                new CardAction(title: "buttonTitle1", type: ActionTypes.ImBack, value: "buttonValue1"),
            })
        .ToAttachment(),
        new HeroCard(
            title: "cardTitle2",
            images: new CardImage[] { new CardImage(url: "imageUrl2.png") },
            buttons: new CardAction[]
            {
                new CardAction(title: "buttonTitle2", type: ActionTypes.ImBack, value: "buttonValue2")
            })
        .ToAttachment(),
        new HeroCard(
            title: "cardTitle3",
            images: new CardImage[] { new CardImage(url: "imageUrl3.png") },
            buttons: new CardAction[]
            {
                new CardAction(title: "buttonTitle3", type: ActionTypes.ImBack, value: "buttonValue3")
            })
        .ToAttachment()
    });

// Send the activity as a reply to the user.
await context.SendActivityAsync(activity, token);
```

# [JavaScript](#tab/javascript)

```javascript
// require MessageFactory and CardFactory from botbuilder.
const {MessageFactory, CardFactory} = require('botbuilder');

//  init message object
let messageWithCarouselOfCards = MessageFactory.carousel([
    CardFactory.heroCard(
        'cardTitle1',
        CardFactory.images(['imageUrl1.png']),
        CardFactory.actions([
            {
                type: 'openUrl',
                title: 'buttonTitle1',
                value: 'buttonValue1',
                type: ActionTypes.ImBack
            }
        ])
    ),
    CardFactory.heroCard(
        'cardTitle2',
        CardFactory.images(['imageUrl2.png']),
        CardFactory.actions([
            {
                type: 'openUrl',
                title: 'buttonTitle2',
                value: 'buttonValue2',
                type: ActionTypes.ImBack
            }
        ])
    ),
    CardFactory.heroCard(
        'cardTitle3',
        CardFactory.images(['imageUrl3.png']),
        CardFactory.actions([
            {
                type: 'openUrl',
                title: 'buttonTitle3',
                value: 'buttonValue3',
                type: ActionTypes.ImBack
            }
        ])
    )
]);

await context.sendActivity(messageWithCarouselOfCards);
```

---

## Additional resources

[Preview features with the Channel Inspector](../bot-service-channel-inspector.md)

---