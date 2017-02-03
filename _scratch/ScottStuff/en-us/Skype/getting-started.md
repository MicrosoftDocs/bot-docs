---
layout: page
title: Getting Started
permalink: /en-us/skype/getting-started/
weight: 5100
parent1: Channels
parent2: Skype bots
---

You've come to the right place to build and publish a Skype bot - with cards, groups and calling - using the Microsoft Bot Framework.

* TOC
{:toc}

## Creating a Skype bot

All bots created using the Microsoft Bot Framework are automatically configured and ready to work in Skype.

See the [Microsoft Bot Framework Overview](/en-us/) to learn how to:

1. Build a bot using the [C# SDK](/en-us/csharp/builder/sdkreference/), [Node.js SDK](/en-us/node/builder/chat-reference/modules/_botbuilder_d_.html) or [Skype REST API](/en-us/skype/chat)
2. Test it using the [Bot Framework Emulator](/en-us/tools/bot-framework-emulator/)
3. Deploy the bot to a cloud service, such as [Microsoft Azure](https://azure.microsoft.com/en-gb/)
4. [Register](https://dev.botframework.com/bots/new) the bot with the Microsoft Bot Framework, which will automatically add Skype as a channel. **When you first register a bot it will be in Preview**, which means it can be added by up to 100 users using an [add button or URL](#add-button-or-URL). To remove the limit you can easily publish it in Skype using the Microsoft Bot Framework.
5. [Add the bot](#adding-a-bot) to the latest version of Skype for mobile or desktop platforms - and test

## Publishing

Publish your bot from the [Bot Dashboard](https://dev.botframework.com/bots) to remove user limits and request for it to be shown in the Skype app bot directory and Microsoft bot directory. You can choose to automatically show in directories, or enable it later from the Bot Dashboard. Skype will review your bot to remove the limit, and may select it for the Skype app bot directory and/or Microsoft bot directory.

Please make sure to read the [Best Practices](http://docs.botframework.com/directory/best-practices/),  [Review Guidelines](http://docs.botframework.com/directory/review-guidelines/),  [Terms of use](https://aka.ms/bf-terms) and [Code of Conduct](http://aka.ms/bf-conduct) before publishing your bot.

The [Terms of Use](https://aka.ms/bf-terms) contains Sample Terms you can use to help create your own Terms of Service and the [Code of Conduct](http://aka.ms/bf-conduct) contains links to third party Privacy Statement resources.

## Finding and adding bots in Skype

Users can find and add your Skype bot in several ways.

### Add button or URL
{:.no_toc}

You can create an add button [here](https://www.skype.com/en/developer/create-addbot-button/) or copy and paste the add URL from the bot dashboard which will look like this https://join.skype.com/bot/YourAppID e.g.[https://join.skype.com/bot/29415286-5a43-4a00-9dc5-bcbc2ce1f59e](https://join.skype.com/bot/29415286-5a43-4a00-9dc5-bcbc2ce1f59e).

![Add button](/en-us/images/skype/skype-add-to-button.png)

When a user taps or clicks the add button or add URL they will be prompted to sign into Skype if necessary and redirected to a page with information about your bot and its capabilities. After they tap to add your bot they will be redirected straight into a chat with the bot in a [Skype app](#) or the [Skype web app](http://web.skype.com).

![Add to contacts](/en-us/images/skype/skype-add-to-contacts.png)

### Skype bot directory
{:.no_toc}

In specific markets (Currently USA, Canada, UK, Ireland, Australia, and New Zealand) Skype users with the most recent app version will see an Add bots button which opens the Skype bot directory.

![Search for a bot](/en-us/images/skype/skype-bot-search.png)

![Find a bot](/en-us/images/skype/skype-bot-find.png)

![View bot profile](/en-us/images/skype/skype-bot-profile.png)

![Welcome page](/en-us/images/skype/skype-bot-welcome.png)

### Microsoft bot directory
{:.no_toc}

Bots that are approved for Skype may also appear in the [Microsoft bot directory](https://bots.botframework.com/)

### How a bot appears in Skype
{:.no_toc}

Once a bot has been added it will appear in your conversation history in the same way as a chat with another user, but with a hexagonal avatar. 

![Bot in Skype](/en-us/images/skype/skype-bot-avatar.png)

<div class="docs-text-note">Bots in Skype always appear online and do not have a mood message</div>

## Messages

Your bot can send rich text, emoticons, pictures and cards to a user or group. Users can send rich text and pictures to your bot. You can specify the type of content your bot can handle in the Skype settings page for your bot. 

|  |From user to bot	|From bot to user                                          
|-------------|:------------------:|:---------------:|---------------
|Rich text| ✔ | ✔ | Including emoticons :)
|Pictures | ✔ | ✔ | PNG, JPEG or GIF up to 20Mb
|Video | Coming soon | ✔ | MP4, AAC+h264 up to 15Mb (approx. 1 minute), plus JPEG thumbnail
|Cards | ✔ | ✔ |

### Basic format
{:.no_toc}

Each Skype user is assigned a unique ID for your bot, which is sent along with the Display Name with every message.

{% highlight json %}

{
  "text": "Hello (wave)",
  "id": "1466182688092",
  "type": "message/text",
  "timestamp": "2016-06-17T16:58:08.74Z",
  "channelId": "skype",
  "serviceUrl": "https://apis.skype.com",
  "from": {
    "id": "29:2hJJkjmGn4ljB2X7YYEju-sgFwgvnISvE6G3abGde8ts",
    "name": "Display Name"
  },
  "conversation": {
    "id": "29:2hJJkjmGn4ljB2X7YYEju-sgFwgvnISvE6G3abGde8ts"
  },
  "recipient": {
    "id": "28:29415286-5a43-4a00-9dc5-bcbc2ce1f59e",
    "name": "Trivia Master"
  }
}
{% endhighlight %}

The from field contains the unique user ID (prefixed by 29) and user Display Name. The to field contains the App ID (prefixed by 28: which indicates a bot in Skype) and bot Display Name. In Skype your bot is addressed using the Bot Framework App ID (a GUID).

<div class="docs-text-note"><b>Note:</b> You cannot currently use slash (“/”) commands as part of conversations with your bot. This is a reserved character in Skype.</div>

### Rich text
{:.no_toc}

Skype supports all the [text properties](#) supported by the Microsoft Bot Framework.

### Skype emoticons 
{:.no_toc}

[Skype emoticons](https://support.skype.com/en/faq/FA12330/what-is-the-full-list-of-emoticons) can be sent by using emoticon keyword in parentheses.

{% highlight json %}
{
  "text": "(heart)"
}
{% endhighlight %}

![Heart emoticon ](/en-us/images/skype/skype-bot-heart.png)

<div class="docs-text-note"><b>Note </b>If a user sends your bot an emoticon it may include &lt;ss&gt; tags around the emoticon, which can be ignored e.g. &lt;ss type=\"skype\"&gt;(wave)&lt;/ss&gt;</div>

<div class="docs-text-note"><b>Note </b>Sending <a href="https://support.skype.com/en/faq/FA34582/what-are-mojis" target="_blank">Skype Mojis</a> (short, expressive video clips) is not currently supported.</div>

### Welcome messages
{:.no_toc}

To send a welcome message to a user listen for the contactRelationUpdate [activity](/en-us/csharp/builder/sdkreference/activities.html). To send a welcome message to a group list for the conversationUpdate [activity](/en-us/csharp/builder/sdkreference/activities.html).

### Pictures and videos
{:.no_toc}

Pictures and videos are sent by adding [attachments](/en-us/csharp/builder/sdkreference/activities.html#attachmentscardsactions) to a message. 

Pictures can be PNG, JPEG or GIF up to 20Mb.

Videos can be MP4, AAC+h264 up to 15Mb (approx. 1 minute), plus JPEG thumbnail

## Cards and buttons

Skype supports the following cards which may have several properties and attachments. You can find information on how to use cards in the [.NET SDK](/en-us/csharp/builder/sdkreference/activities.html#richcards) and [Node.js SDK](/en-us/node/builder/guides/examples/#demo-skype-calling) docs. 

* Hero card
* Thumbnail card
* Carousel card (with hero or thumbnail images)
* Sign in card
* Receipt card

<div class="docs-text-note"><b>Note:</b> Images sent to Skype cards need to be stored on an HTTPS endpoint</div>
<div class="docs-text-note"><b>Note:</b> Skype cards do not currently support postBack actions</div>

### Images
{:.no_toc}
Images are scaled up or down in size while maintaining the aspect ratio to cover the image area, and then cropped from center to achieve the image aspect ratio for the card.

Images should be HTTPS, up to 1024x1024, up to 1MB in size, and PNG or JPEG.

Property|Type|Description
url|URL|URL to the image. **Must be HTTPS**
alt|String|Accessible description of the image
value|String|Action assigned to the image

### Buttons
{:.no_toc}
Buttons are shown at the bottom of the card - in a single row if they fit, or stacked. Button text is always on a single line and will be trimmed if too long. If more buttons than can be supported by the card are included the will not be shown.

### Actions
{:.no_toc}

Property|Type|Description
type|string|Required field. One of openURL (opens the given URL), imBack (posts a message in the chat to the bot that sent the card), call (skype or phone number), showImage (for images only, displays the image), signin (sign in card only).
title|String|Text description that appears on the button
tap|Action object|Value depending on the type of action. For openURL is a URL, for signin is the URL to the authentication flow, for imBack is a user defined string, for call can be "skype:skypeid" or "tel:telephone", for showImage not required.

### Sending context with actions
{:.no_toc}

It can be useful to send context back to your bot (e.g. a request ID) without showing this information to the user in a message. To do this you can append hidden XML to the visible string shown to the user, which is only seen by your bot.

    Visible message &lt;context hiddenId='10'/&gt;


### Hero card
{:.no_toc}

The [hero card](/en-us/csharp/builder/sdkreference/activities.html#herocard) renders a title, subtitle, text, large image and buttons.

![Hero card](/en-us/images/skype/skype-bot-hero-card.png)

The hero card provides a very flexible layout, for example it might contain:

* Image, title, subtitle, text + 3 buttons
* Title, subtitle, text + 5 buttons
* Title + 6 buttons
* Image + 6 buttons

Property|Type|Description
title|Rich text|Title of the card. Maximum 2 lines
subtitle|Rich text|Subtitle appears just below the Title. Maximum 2 lines
text|Rich text|Text appears just below the subtitle. 2, 4 or 6 lines depending on whether title and subtitle are specified
images:[]|Array of images|Image displayed at top of card. Aspect ratio 16:9
buttons:[]|Array of action objects|Set of actions applicable to the current card. 3 buttons up to a maximum of 6 (+2 if no image is shown, +1 if title or subtitle not included, +2 if text is not included)
tap|Action object|This action will be activated when the user taps on the card itself

### Thumbnail card
{:.no_toc}

The [thumbnail card](/en-us/csharp/builder/sdkreference/activities.html#thumbnailcard) renders a title, subtitle, text, small thumbmail image and buttons.

![Thumbnail card](/en-us/images/skype/skype-bot-thumbnail-card.png)

Property|Type|Description
title|Rich text|Title of the card. Maximum 2 lines
subtitle|Rich text|Subtitle appears just below the Title. Maximum 2 lines
text|Rich text|Text appears just below the subtitle. 2, 4 or 6 lines depending on whether title and subtitle are specified
images:[]|Array of images|Image displayed at top of card. The image aspect ratio in a thumbnail card is 1:1
buttons:[]|Array of action objects|Set of actions applicable to the current card. Maximum 3 buttons.
tap|Action object|This action will be activated when the user taps on the card itself

### Carousel
{:.no_toc}

The [carousel card](/en-us/csharp/builder/sdkreference/activities.html) can be used to show a carousel of images and text, with associated action buttons.

![Carousel card](/en-us/images/skype/skype-bot-carousel-card.png)

Properties are the same as for the hero or thumbnail card.

### Sign in
{:.no_toc}

![Sign in card](/en-us/images/skype/skype-bot-signin-card.png)

The [sign in card](/en-us/csharp/builder/sdkreference/attachments.html#signincard) can be used to initiate an authentication flow with predefined images and  title.

Property|Type|Description
text|Rich text|Text appears just below the subtitle. 2 lines maximum
buttons:[]|Array of action objects|Single button of type signin

### Receipt
{:.no_toc}

The [receipt card](/en-us/csharp/builder/sdkreference/attachments.html#receiptcard) can be used to send a receipt.

![Receipt card](/en-us/images/skype/skype-bot-receipt-card.png)

**Property**|**Type**|**Description**
|:----------------------------:|---------------------------------|---------------------------------
title|Rich text|Title of the card.
subtitle|Rich text|Appears below title.
text|Rich text|Appears below substitle.
facts:[]|Array of Fact key-value pairs|Fact key is left aligned, value is right aligned
items:[]|Array of Purchased objects|Properties: title (Maximum 2 lines), subtitle (1 line), text (Up to 6 lines depending if title, subtitle and price are present), price, image (1:1 aspect ratio), tap
total|Rich text|Total purchase amount, mandatory for receipt card.
tax|Rich text|Amount of tax charged.
vat|Rich text|Amount of vat charged.
buttons:[]|Array of action objects|Set of actions applicable to the current card
tap|Action object|This action will be activated when the user taps on the card itself

## Groups 

A bot can be enabled for groups in the Skype settings for the bot. It can be added to a group chat in the same way as adding a participant to a chat. In a group the bot will only receive messages directly addressed to it e.g. "@YourBot This is the message". It will not receive other messages sent by group participants or notifications of users joining or leaving the group.

![At mention](/en-us/images/skype/skype-bot-at-mention.png)

To enable a bot to be added to a group chat you need to add this capability in Settings. Go to your Bot Dashboard and Edit the Skype channel. 

![Add groups](/en-us/images/skype/skype-settingsgroups.png)

## Calling (Preview)

You can build Skype bots that can receive and handle voice calls using the [.NET SDK](http://docs.botframework.com/en-us/csharp/builder/sdkreference/calling.html), [Node.js SDK](#) or [Skype  API](http://docs.botframework.com/en-us/skype/calling/#navtitle).

Each time a Skype user places a call to your bot, the Skype bot platform will notify the bot using the calling webhook you specify in settings. In response the bot can provide a set of basic actions called a workflow.

Supported actions:

* Answer
* Play prompt
* Record audio
* Speech to text
* DTMF tones
* Hang up

The Skype bot platform will execute the actions on the bot's behalf according to the workflow.

If the workflow is successful, Skype will post a result of the last action to your calling webhook. For example, if the last action was to record an audio message the result will be audio content. 

During a voice call your bot can decide after each result on how to continue interaction with the Skype user. 

<div class="docs-text-note"><b>Note:</b> Skype bots with calling enabled are for preview only cannot currently be published. To publish a bot in Skype you will need to disable calling in the Skype settings for your bot and then set published to enabled in the bot dashboard. </div>

<div class="docs-text-note"><b>Note:</b> Bots can handle 1:1 calls, but not group calls</div>

## Bing Entity and Intent Detection (Preview)

You can choose to have entities and intents automatically detected and sent along with the message text to your bot. A single intent and several entities may be returned.

{% highlight json %}

{
  "text": "What's the weather like in Prague today?",
  "id": "8995507496412",
  "entities": [
    {
      "type": "intent/bing.dialog.api",
      "data": {
        "domain": "prebuilt",
        "intent": "microsoft.weather.check_weather",
        "confidence": 0.00755978003144264,
        "entities": [
          {
            "name": "microsoft.generic.absolute_location",
            "value": "Prague",
            "source": "Prague"
          },
          {
            "name": "microsoft.generic.date_range",
            "value": "today",
            "source": "today"
          }
        ]
      }
    }
  ],
  "type": "message/text",
  "timestamp": "2016-06-17T22:55:06.656Z",
  "channelId": "skype",
  "serviceUrl": "https://df-apis.skype.com",
  "from": {
    "id": "29:2hJJkjmGn4ljB2X7YYEju-sgFwgvnISvE6G3abGde8ts",
    "name": "Display Name"
  },
  "conversation": {
    "id": "29:2hJJkjmGn4ljB2X7YYEju-sgFwgvnISvE6G3abGde8ts"
  },
  "recipient": {
    "id": "28:29415286-5a43-4a00-9dc5-bcbc2ce1f59e",
    "name": "Trivia Master"
  },
}
{% endhighlight %}

To turn on Bing Entity and Intent Detection go to the Skype settings page for your bot.

![Turn on Bing Entity and Intent Detection](/en-us/images/skype/skype-settingsbing.png)

## Bot settings

### Disabling a bot
{:.no_toc}

To stop your bot receiving messages and remove it from directories go to your Bot Dashboard and Edit the Skype channel. Disable the bot in settings.

![Disable bot](/en-us/images/skype/skype-settingsdisabled.png)

### Deleting a bot
{:.no_toc}

To delete your bot completely go to your Bot Dashboard and Edit the Skype channel. Select the Delect button at the bottom.
