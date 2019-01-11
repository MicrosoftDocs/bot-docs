---
title: Add media to messages | Microsoft Docs
description: Learn how to add media to messages using the Bot Framework SDK.
keywords: media, messages, images, audio, video, files, MessageFactory, rich cards, messages, adaptive cards, hero card, suggested actions
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/17/2018
monikerRange: 'azure-bot-service-4.0' 
---

# Add media to messages

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

Messages exchanged between user and bot can contain media attachments, such as images, video, audio, and files. The Bot Framework SDK supports the task of sending rich messages to the user. To determine the type of rich messages a channel (Facebook, Skype, Slack, etc.) supports, consult the channel's documentation for information about limitations. Refer to [design user experience](../bot-service-design-user-experience.md) for a list of available cards. 

## Send attachments

To send the user content like an image or a video, you can add an attachment or list of attachments to a message.

# [C#](#tab/csharp)

The `Attachments` property of the `Activity` object contains an array of `Attachment` objects that represent the media attachments and rich cards attached to the message. To add a media attachment to a message, create an `Attachment` object for the `message` activity and set the `ContentType`, `ContentUrl`, and `Name` properties. 
The `Attachments` property of the `Activity` object contains an array of `Attachment` objects that represent the media attachments and rich cards attached to the message. To add a media attachment to a message, use the `Attachment` method to create an `Attachment` object for the `message` activity and set the`ContentType`, `ContentUrl`, and `Name` properties. The source code shown here is based on the [Handling Attachments](https://aka.ms/bot-attachments-sample-code) sample. 

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

var reply = turnContext.Activity.CreateReply();

// Create an attachment.
var attachment = new Attachment
    {
        ContentUrl = "imageUrl.png",
        ContentType = "image/png",
        Name = "imageName",
    };

// Add the attachment to our reply.
reply.Attachments = new List<Attachment>() { attachment };

// Send the activity to the user.
await turnContext.SendActivityAsync(reply, cancellationToken);
```

# [JavaScript](#tab/javascript)

The source code shown here is based on the [JS Handling Attachments](https://aka.ms/bot-attachments-sample-code-js) sample.
To send the user a single piece of content like an image or a video, you can send media contained in a URL:

```javascript
const { ActionTypes, ActivityTypes, CardFactory } = require('botbuilder');

// Call function to get an attachment.
const reply = { type: ActivityTypes.Message };
reply.attachments = [this.getInternetAttachment()];
reply.text = 'This is an internet attachment.';
// Send the activity to the user.
await turnContext.sendActivity(reply);

/* function getInternetAttachment - Returns an attachment to be sent to the user from a HTTPS URL */
getInternetAttachment() {
        return {
            name: 'imageName.png',
            contentType: 'image/png',
            contentUrl: 'imageUrl.png'}
}
```

---

If an attachment is an image, audio, or video, the Connector service will communicate attachment data to the channel in a way that enables the [channel](bot-builder-channeldata.md) to render that attachment within the conversation. If the attachment is a file, the file URL will be rendered as a hyperlink within the conversation.

## Send a hero card

Besides simple image or video attachments, you can attach a **hero card**, which allows you to combine images and buttons in one object, and send them to the user.

# [C#](#tab/csharp)

To compose a message with a hero card and button, you can attach a `HeroCard` to a message. The source code shown here is based on the [Handling Attachments](https://aka.ms/bot-attachments-sample-code) sample. 

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

var reply = turnContext.Activity.CreateReply();

// Create a HeroCard with options for the user to choose to interact with the bot.
var card = new HeroCard
{
    Text = "You can upload an image or select one of the following choices",
    Buttons = new List<CardAction>()
    {
        new CardAction(ActionTypes.ImBack, title: "1. Inline Attachment", value: "1"),
        new CardAction(ActionTypes.ImBack, title: "2. Internet Attachment", value: "2"),
        new CardAction(ActionTypes.ImBack, title: "3. Uploaded Attachment", value: "3"),
    },
};

// Add the card to our reply.
reply.Attachments = new List<Attachment>() { card.ToAttachment() };

await turnContext.SendActivityAsync(reply, cancellationToken);
```

# [JavaScript](#tab/javascript)

To compose a message with a hero card and button, you can attach a `HeroCard` to a message. The source code shown here is based on the [JS Handling Attachments](https://aka.ms/bot-attachments-sample-code-js) sample:

```javascript
const { ActionTypes, ActivityTypes, CardFactory } = require('botbuilder');
// build buttons to display.
const buttons = [
            { type: ActionTypes.ImBack, title: '1. Inline Attachment', value: '1' },
            { type: ActionTypes.ImBack, title: '2. Internet Attachment', value: '2' },
            { type: ActionTypes.ImBack, title: '3. Uploaded Attachment', value: '3' }
];

// construct hero card.
const card = CardFactory.heroCard('', undefined,
buttons, { text: 'You can upload an image or select one of the following choices.' });

// add card to Activity.
const reply = { type: ActivityTypes.Message };
reply.attachments = [card];

// Send hero card to the user.
await turnContext.sendActivity(reply);
```

---

## Process events within rich cards

To process events within rich cards, use _card action_ objects to specify what should happen when the user clicks a button or taps a section of the card. Each card action has a _type_ and _value_.

To function correctly, assign an action type to each clickable item on the card. This table lists and describes the available action types and what should be in the associated value property.

| Type | Description | Value |
| :---- | :---- | :---- |
| openUrl | Opens a URL in the built-in browser. | The URL to open. |
| imBack | Sends a message to the bot, and posts a visible response in the chat. | Text of the message to send. |
| postBack | Sends a message to the bot, and may not post a visible response in the chat. | Text of the message to send. |
| call | Initiates a phone call. | Destination for the phone call in this format: `tel:123123123123`. |
| playAudio | Plays audio. | The URL of the audio to play. |
| playVideo | Plays a video. | The URL of video to play. |
| showImage | Displays an image. | The URL of the image to display. |
| downloadFile | Downloads a file. | The URL of the file to download. |
| signin | Initiates an OAuth signin process. | The URL of the OAuth flow to initiate. |

## Hero card using various event types

The following code shows examples using various rich card events.

# [C#](#tab/csharp)

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

var reply = turnContext.Activity.CreateReply();

var card = new HeroCard
{
    Buttons = new List<CardAction>()
    {
        new CardAction(title: "Much Quieter", type: ActionTypes.PostBack, value: "Shh! My Bot friend hears me."),
        new CardAction(ActionTypes.OpenUrl, title: "Azure Bot Service", value: "https://azure.microsoft.com/en-us/services/bot-service/"),
    },
};

```

# [JavaScript](#tab/javascript)

```javascript
const {ActionTypes} = require("botbuilder");

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
);

await context.sendActivity(hero);

```

---

## Send an Adaptive Card
Adaptive Card and MessageFactory are used to send rich messages including texts, images, video, audio and files to communicate with users. However, there are some differences between them. 

First, only some channels support Adaptive Cards, and channels that do support it might partially support Adaptive Cards. For example, if you send an Adaptive Card in Facebook, the buttons won't work while texts and images work well. MessageFactory is just a helper class within the Bot Framework SDK to automate creation steps for you, and supported by most channels. 

Second, Adaptive Card delivers messages in the card format, and the channel determines the layout of the card. The format of messages MessageFactory delivers depends on the channel, and is not necessarily in the card format unless Adaptive Card is part of the attachment. 

To find the latest information on Adaptive Card channel support, see the <a href="http://adaptivecards.io/visualizer/">Adaptive Cards Visualizer</a>.

To use adaptive cards, be sure to add the `Microsoft.AdaptiveCards` NuGet package. 


> [!NOTE]
> You should test this feature with the channels your bot will use to determine whether those channels support adaptive cards.

# [C#](#tab/csharp)

The source code shown here is based on the [Using Adaptive Cards](https://aka.ms/bot-adaptive-cards-sample-code) sample:

```csharp
using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

// Creates an attachment that contains an adaptive card
// filePath is the path to JSON file
private static Attachment CreateAdaptiveCardAttachment(string filePath)
{
    var adaptiveCardJson = File.ReadAllText(filePath);
    var adaptiveCardAttachment = new Attachment()
    {
        ContentType = "application/vnd.microsoft.card.adaptive",
        Content = JsonConvert.DeserializeObject(adaptiveCardJson),
    };
    return adaptiveCardAttachment;
}

// Create adaptive card and attach it to the message 
var cardAttachment = CreateAdaptiveCardAttachment(adaptiveCardJsonFilePath);
var reply = turnContext.Activity.CreateReply();
reply.Attachments = new List<Attachment>() { cardAttachment };

await turnContext.SendActivityAsync(reply, cancellationToken);
```

# [JavaScript](#tab/javascript)

The source code shown here is based on the [JS Using Adaptive Cards](https://aka.ms/bot-adaptive-cards-js-sample-code) sample:

```javascript
const { BotFrameworkAdapter } = require('botbuilder');

// Import AdaptiveCard content.
const FlightItineraryCard = require('./resources/FlightItineraryCard.json');
const ImageGalleryCard = require('./resources/ImageGalleryCard.json');
const LargeWeatherCard = require('./resources/LargeWeatherCard.json');
const RestaurantCard = require('./resources/RestaurantCard.json');
const SolitaireCard = require('./resources/SolitaireCard.json');

// Create array of AdaptiveCard content, this will be used to send a random card to the user.
const CARDS = [
    FlightItineraryCard,
    ImageGalleryCard,
    LargeWeatherCard,
    RestaurantCard,
    SolitaireCard
];
// Select a random card to send.
const randomlySelectedCard = CARDS[Math.floor((Math.random() * CARDS.length - 1) + 1)];
// Send adaptive card.
await context.sendActivity({
      text: 'Here is an Adaptive Card:',
       attachments: [CardFactory.adaptiveCard(randomlySelectedCard)]
});
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
const {MessageFactory, CardFactory} = require('botbuilder');

//  init message object
let messageWithCarouselOfCards = MessageFactory.carousel([
    CardFactory.heroCard('title1', ['imageUrl1'], ['button1']),
    CardFactory.heroCard('title2', ['imageUrl2'], ['button2']),
    CardFactory.heroCard('title3', ['imageUrl3'], ['button3'])
]);

await context.sendActivity(messageWithCarouselOfCards);
```

---

<!-- TODO: Add a media card, such as video or audion. Revisit which examples we put here and link to the 06 through 08 samples. -->

## Additional resources

For detailed information on the card schema, see the [Bot Framework card schema](https://aka.ms/botSpecs-cardSchema).

Sample code can be found here for
cards: [C#](https://aka.ms/bot-cards-sample-code)/[JS](https://aka.ms/bot-cards-js-sample-code),
adaptive cards: [C#](https://aka.ms/bot-adaptive-cards-sample-code)/[JS](https://aka.ms/bot-adaptive-cards-js-sample-code),
attachments: [C#](https://aka.ms/bot-attachments-sample-code)/[JS](https://aka.ms/bot-attachments-sample-code-js),
and suggested actions: [C#](https://aka.ms/SuggestedActionsCSharp)/[JS](https://aka.ms/SuggestedActionsJS).
Refer to Bot Builder Samples repo on [GitHub](https://aka.ms/bot-samples-readme) for additional samples.
