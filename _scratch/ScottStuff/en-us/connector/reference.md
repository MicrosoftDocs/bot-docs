---
layout: page
title: Reference
permalink: /en-us/connector/reference/
weight: 3080
parent1: Building your Bot Using the Bot Connector REST API
---


Bot Connector Service is a component of Bot Framework that sits between your bot and the channels (for example, SMS, Skype, Slack, and Facebook) that you configured when you registered your bot. The connector service is responsible for exchanging messages between your bot and the channel, and when appropriate, normalizing the messages.

To send message to and receive messages from the connector, you use the Bot Connector REST API. The API is a collection of endpoints and JSON objects that you use to start conversations, send messages, add attachments, and get members of the conversation.

<div class="docs-text-note"> <strong>NOTE</strong>: If you use C# or Node, you should use the <a href="/en-us/csharp/builder/sdkreference/Getting%20Started.md" target="_blank">Bot Builder SDK for C#</a> or the <a href="/en-us/node/builder/getting-started.md" target="_blank">Bot Builder SDK for Node.js</a> instead of this REST API.</div>

See the following sections for [endpoints](#endpoints), [headers](#headers), and [classes](#classes) that you use to write your bot. 


<a id="endpoints" />

### Endpoints

The following is the base URI that you use in all connector REST requests.

`https://api.botframework.com`

The following are the URI templates that you use to access connector resources.

|**Template**|**Verb**|**Description**|**Resource**
|/v3/conversation|POST|Use to start a private or group conversation. For information, see [Stating a Conversation](../conversation).|Request: [Conversation](#conversation-object)<br/>Response: [Identification](#identification)
|/v3/conversation/{conversationId}/activities|POST|Use to send a message to members of the conversation. Use this only if you're not replying to a message from another member of the conversation. For information, see [Sending Messages](../messages).|Request: [Activity](#activity)
|/v3/conversation/{conversationId}/activities/{activityId}|POST|Use to reply to a message that a member of the conversation sent. For information, see [Sending Messages](../messages).|Request: [Activity](#activity)
|/v3/conversation/{conversationId}/members|GET|Use to get members of the conversation.|Response: [ChannelAccount](#channelaccount)
|/v3/conversation/{conversationId}/activities/{activityId}/members|GET|Use to get members of an activity.|Response: [ChannelAccount](#channelaccount)
|/v3/botstate/{channelId}/users/{userId}|DELETE|Use to delete a user's state data from the User Data store and the Private Conversation store. For information, see [Deleting the user's state data](../userdata/#deletinguserdata).|Response: [Identification](#identification)
||GET|Use to get a user's state data from User Data store. For information, see [Getting user data](../userdata/#gettinguserdata).|Response: [UserData](#userdata)
||POST|Use to overwrite a user's state data in the User Data Store. For information, see [Saving user data](../userdata/#savinguserdata).|Request: [UserData](#userdata)<br/>Response: [UserData](#userdata)
|/v3/botstate/{channelId}/conversations/{conversationId}|GET|Use to get a conversation's state data from the Conversation store. For information, see [Getting conversation data](../userdata/#gettingconversationdata).|Response: [UserData](#userdata)
||POST|Use to overwrite a conversation's state data in the Conversation store. For information, see [Saving conversation data](../userdata/#savingconversationdata).|Request: [UserData](#userdata)<br/>Response: [UserData](#userdata)
|/v3/botstate/{channelId}/conversations/{conversationId}/users/{userId}|GET|Use to get a user's state data from the Private Conversation store. For information, see [Getting private conversation data](../userdata/#gettingprivateconversationdata).|Response: [UserData](#userdata)
||POST|Use to overwrite a user's state data in the Private Conversation store. For information, see [Saving private conversation data](../userdata/#savingprivateconversationdata).|Request: [UserData](#userdata)<br/>Response: [UserData](#userdata)

Replace {conversationId} with the conversation's ID. Get the ID from the message's [conversation](#conversation) property. Or, if your bot starts the conversion, get the ID from the response (see the [Identification](#identification) object).

Replace {activityId} with the message's ID. Get the ID from the message's [id](#id) property.

Replace {channelId} with the channel's ID. Get the ID from the message's [channelId](#channelid) property.

Replace {userId} with the user's ID. Get the ID from the message's [from](#from) property. 



<a id="headers" />

### Headers

In addition to the standard HTTP request and response headers, the connector API uses the following headers.

|**Header**|**Description**
|Authorization|Required request header.<br/><br/>Contains the client authentication token that's used to authenticate your bot. For information, see [Authentication](#../authentication).
|X-Correlating-OperationId|Response header.<br/><br/>Contains the ID of the log entry that provides details of the request. You should always capture this ID if an error occurs. If you are not able to determine and resolve the issue, include this ID along with the other information that you provide the Support team. 


<a id="classes" />

### Classes

The Bot Connector REST API uses the following JSON objects:

|**Object**|**Description**
|[Activity](#activity)|Defines a message that bots and users send to each other.
|[AnimationCard](#animationcard)|Defines a card that's capable of playing animated GIFs or short videos.
|[Attachment](#attachment)|Defines additional information to include in a message.
|[AudioCard](#audiocard)|Defines a card that's capable of playing an audio file.
|[CardAction](#cardaction)|Defines an action to perform.
|[CardImage](#cardimage)|Defines an image to display on a card.
|[ChannelAccount](#channelaccount)|Defines a bot or user account on the channel.
|[Conversation](#conversation-object)|Defines the bot and users that you want to start the conversation with.
|[ConversationAccount](#conversationaccount)|Defines a conversation in a channel.
|[Error](#error)|Defines an error.
|[Fact](#fact)|Defines a key-value pair that contains a fact.
|[GeoCoordinates](#geocoordinates)|Defines a geographical location using World Geodetic System (WSG84) coordinates.
|[HeroCard](#herocard)|Defines a card with a large image, title, text, and action buttons.
|[Identification](#identification)|Defines a resource's identity.
|[MediaUrl](#mediaurl)|Defines the URL to a media's source.
|[Mention](#mention)|Defines a user or bot that was mentioned in the conversation.
|[Place](#place)|Defines a place that was mentioned in the conversation.
|[ReceiptCard](#receiptcard)|Defines a card that contains a receipt for a purchase.
|[ReceiptItem](#receiptitem)|Defines a line item of the receipt.
|[SignInCard](#signincard)|Defines a card that lets a user sign in to a service.
|[ThumbnailCard](#thumbnailcard)|Defines a card with a thumbnail image, title, text, and action buttons.
|[ThumbnailUrl](#thumbnailurl)|Defines the URL to an image's source.
|[UserData](#userdata)|Defines user data that contains user state and preferences.
|[VideoCard](#videocard)|Defines a card that's capable of playing videos.



<a id="activity" />

#### Activity

Defines a message that bots and users send to each other.

|**Property**|**Description**|**Type**
|<a id="action" />action|The action to apply or that was applied. Use the [type](#activity_type) property to determine context for the action. For example, if **type** is contactRelationUpdate, the action would be `add` if the user added your bot to their contacts list, or `remove` if they removed your bot from their contacts list.|String
|<a id="attachments" />attachments|A list of additional information to include in the message. For example, bots can use this property to include an image to display with the text or to send a rich card that contains text, images, and buttons. For limits imposed on attachments, see the channel's documentation.<br/><br/>A channel can use this property to send the bot an image, video, or file.<br/><br/>For more information, see [Adding Attachments to a Message](../attachments).|[Attachment](#attachment) array
|<a id="attachmentlayout" />attachmentLayout|The layout of the rich cards specified in the **attachments** property. Set this property only if **attachments** contains rich cards such as a Hero card or Receipt card. The following are the possible values.<br/><br/>&emsp;`carousel`&mdash;Display the list of cards horizontally in a carousel. If the channel does not support carousels, the cards are displayed in a vertical list.<br/><br/>&emsp;`list`&mdash;Display the list of cards in a vertical list. List is the default layout.|String
|<a id="channeldata" />channelData|An object that contains channel-specific content. Some channels provide features that require additional information that is not covered by using standard attachments. For those cases, set this property to the channel-specific content as defined in the channel's documentation. For more information, see [Adding Channel Data](../channeldata).|Object
|<a id="channelid" />channelId|An ID that identifies the channel. Channels set this property.|String
|<a id="conversation" />conversation|The ID and name of the conversation.|[ConversationAccount](#conversation)
|<a id="entities" />entities|A list of entities that were mentioned in the conversation. This may contain any [Schema.org](http://schema.org) object. For example, this may include [Mention](#mention) objects and [Place](#place) objects. **Mention** objects identify someone that was mentioned in the conversation, and **Place** objects identify a place that was mentioned in the conversation.|Object[]
|<a id="from" />from|The member of the conversation that is sending the message.|[ChannelAccount](#account)
|<a id="id" />id|An ID that identifies this message. The channel sets this ID. If you're replying to a message, set the **replyToId** property with this ID.|String
|<a id="membersadded" />membersAdded|A list of users that joined the conversation. The message includes this property only if **type** is set to conversationUpdate and users joined the conversation.|[ChannelAccount](#account) array
|<a id="membersremoved" />membersRemoved|A list of users that left the conversation. The message includes this property only if **type** is set to conversationUpdate and users left the conversation.|[ChannelAccount](#account) array
|<a id="historyDisclosed" />historyDisclosed|If true, history is disclosed; otherwise, false. The default is false.|Boolean
|<a id="locale" />locale|The locale of the language to use for the message's display strings. The channel sends this hint, so your bot may provide display strings in the language used by the user. The locale in the form, \<language\>-\<country\>. For example, en-US for English United States. The default is en-US.|String
|<a id="recipient" />recipient|The member of the conversation that the message is being sent to.|[ChannelAccount](#account)
|<a id="replytoid" />replyToId|The ID of the message that you're replying to. Set this to the ID of the message you're replying to. Note that not all channels support threaded replies. In these cases, the channel will ignore this ID and use time ordered semantics (**timestamp**).|String
|<a id="serviceurl" />serviceUrl|The URL of the channel's service. Only channels set this property.|String
|<a id="summary" />summary|A summary of the information in the message. For example, if the bot is an email bot, the summary may be the first 50 characters of the email message.|String
|<a id="text" />text|The text of what the user said or the bot's reply. If your bot includes rich card attachments, you typically don't include this property in the reply. For limits imposed on the content of the message, see the channel's documentation.|String
|<a id="textformat" />textFormat|The type of formatting characters in the display strings that the bot sends to the channel. The following are the possible formats.<br/><br/>&emsp;`markdown`&mdash;The display strings may contain markdown formatting characters. Markdown is the default. The strings may include the following formatting characters:<br/><br/>&emsp;** (bold)<br/>&emsp;* (italic)<br/>&emsp;##### (headers 1-5)<br/>&emsp;\-\-\- (horizontal rule)<br/>&emsp;~~ (strikethrough)<br/>&emsp;- (unordered list)<br/>&emsp;1. (ordered list)<br/>&emsp;\` (inline code)<br/>&emsp;> (block quotes)<br/>&emsp;\[]() (link)<br/>&emsp;\!\[]() (image link)<br/><br/>Not all channels can render all Markdown, and some channels use a variation of Markdown (for example, Slack uses a single asterisk (\*) for bold and an underscore (\_) for italic). If possible, the channel will provide a reasonable approximation of the formatting (for example, bold will be represented as **\*bold\***). If the channel cannot render the Markdown, the displayed text will include the Markdown characters.<br/><br/>For more information about Markdown, see [Markdown Cheatsheet]( https://github.com/adam-p/markdown-here/wiki/Markdown-Cheatsheet).<br/><br/>&emsp;`plain`&mdash;The display strings do not contain formatting characters. The channel should display the strings as plain text.<br/><br/>&emsp;`xml`&mdash;The display strings may contain XML formatting tags. The strings may include the following formatting tags:<br/><br/>&emsp;\<b\> (bold)<br/>&emsp;\<i\> (italic)<br/>&emsp;\<s\> (strikethrough)<br/>&emsp;\<u\> (underscore)<br/>&emsp;\<a\> (hyperlink)<br/><br/>Not all channels support XML formatting tags.|String
|<a id="timestamp" />timestamp|The UTC date and time that the message is sent. The format is YYYY-MM-DDThh:mm:ss.nnnnnnnZ. The Connector sets this property; if you set this property, the connector will overwrite it.|String
|<a id="topicname" />topicName|The topic of conversation. Typically, the topic is set when the bot or user starts the conversation. The bot or user can set this field to change the topic. See the channel's documentation for usage.|String
|<a id="type" />type|The type of message. The following are the types of messages that the user or bot may send.<br/><br/>&emsp;`contactRelationUpdate`&mdash;The channel sends this message to indicate that the user added or removed your bot from their contacts list in the channel. If the user added the bot to the user's contacts list, the [action](#action) property is set to _add_; otherwise, **action** is set to _remove_.<br/><br/>&emsp;`conversationUpdate`&mdash;The channel sends this message to indicate that one or more users joined or left the conversation, or the topic name changed. For a list of users that joined the conversation, see the [addedMembers](#addedmembers) property; otherwise, see [removedMembers](#removedmembers) property. You can use this message to welcome new users to the conversation. Note that some channels may send this message at the start of the conversation, and the list of members may contain your bot.<br/><br/>&emsp;`deleteUserData`&mdash;The channel sends this message when the user wants your bot to delete all of their personally identifiable information (PII). For more information, see [Deleting user state data](../userdata/#deleteuserdata). Typically, after you delete the user's data, you send them a message indicating that you deleted their data.<br/><br/>&emsp;`message`&mdash;The bot or user sends this message to advance the conversation. For example, the users sends a message asking a question the bot sends a message back with the answer. Most messages are of this type.<br/><br/>&emsp;`ping`&mdash;The channel may send this message to verify that the bot is accessible. Your bot should respond with HTTP status code 200 OK. If your bot verifies the authenticity of the sender, you may also return status code 401 Unauthorized or 403 Forbidden.<br/><br/>&emsp;`typing`&mdash;The channel or bot sends this message to indicate to the other that they're working on a reply. Not all channels support this message type.|String


<a id="animationcard" />

#### AnimationCard

Defines a card that's capable of playing animated GIFs or short videos.

|**Property**|**Description**|**Type**
|autoloop|A Boolean value that determines whether to replay the list of animated GIFs when the last one ends. Set to **true** to automatically replay the animation; otherwise, **false**. The default is **true**.|Boolean
|autostart|A Boolean value that determines whether to automatically play the animation when the card is displayed. Set to **true** to automatically play the animation; otherwise, **false**. The default is **true**.|Boolean
|buttons|A list of buttons that let the user perform one or more actions. The channel determines the number of buttons that you may specify.|[CardAction](#cardaction) array
|image|A thumbnail image to display on the card.|[ThumbnailUrl](#thumbnailurl)
|media|A list of animated GIFs to play.|[MediaUrl](#mediaurl) array
|shareable|A Boolean value that determines whether the animation may be shared with others. Is **true** if the animation may be shared; otherwise, **false**. The default is **true**.|Boolean
|subtitle|A subtitle to display under the card's title. |String
|text|The descriptive text or prompt that's displayed under the card's title or subtitle.|String
|title|The title of the card.|String


<a id="attachment" />

#### Attachment

Defines additional information to include in a message.

|**Property**|**Description**|**Type**
|content|The content of the attachment. If the attachment is a rich card, set this property to the rich card object. This property and the **contentUrl** property are mutually exclusive.|Object
|contentType|The media type of the content in the attachment. Set this property to the known media types such as image/png, audio/wav, and video/mp4, or the following vendor-specific types if the attachment is a rich card.<br/><br/>&emsp;`application/vnd.microsoft.card.animation`&mdash;The attachment is a rich card that plays animation. Set the **content** property to a [AnimationCard](#animationcard) object.<br/><br/>&emsp;`application/vnd.microsoft.card.audio`&mdash;The attachment is a rich card that plays audio files. Set the **content** property to a [AudioCard](#audiocard) object.<br/><br/>&emsp;`application/vnd.microsoft.card.hero`&mdash;The attachment is a Hero card. Set the **content** property to a [HeroCard](#herocard) object.<br/><br/>&emsp;`application/vnd.microsoft.card.thumbnail`&mdash;The attachment is a Thumbnail card. Set the **content** property to a [ThumbnailCard](#thumbnailcard) object.<br/><br/>&emsp;`application/vnd.microsoft.com.card.receipt`&mdash;The attachment is a Receipt card. Set the **content** property to a [ReceiptCard](#receiptcard) object.<br/><br/>&emsp;`application/vnd.microsoft.com.card.signin`&mdash;The attachment is a user Sign In card. Set the **content** property to a [SignInCard](#signincard) object.<br/><br/>&emsp;`application/vnd.microsoft.card.video`&mdash;The attachment is a rich card that plays videos. Set the **content** property to a [VideoCard](#videocard) object.|String
|contentUrl|A URL to the content of the attachment. For example, if the attachment is an image, this is the URL to where the image is located. This property supports the HTTP, HTTPS, File, and Data protocols.|String
|name|The name of the attachment.|String
|thumbnailUrl|A URL to a thumbnail image that the channel can use if it supports using an alternative smaller form of **content** or **contentUrl**. For example, if you set **contentType** to application/word and **contentUrl** to the location of the Word document, you might include a thumbnail image that represents the doc. The channel could display the thumbnail image instead of the doc. When the user clicks the image, the channel would open the doc.|String



<a id="audiocard" />

#### AudioCard

Defines a card that's capable of playing an audio file.

|**Property**|**Description**|**Type**
|aspect|The aspect ratio of the thumbnail in the **image** property. The possible values are 16:9 or 9:16.|String
|autoloop|A Boolean value that determines whether to replay the list of audio files when the last one ends. Set to **true** to automatically replay the audio; otherwise, **false**. The default is **true**.|Boolean
|autostart|A Boolean value that determines whether to automatically play the audio when the card is displayed. Set to **true** to automatically play the audio; otherwise, **false**. The default is **true**.|Boolean
|buttons|A list of buttons that let the user perform one or more actions. The channel determines the number of buttons that you may specify.|[CardAction](#cardaction) array
|image|A thumbnail image to display on the card.|[ThumbnailUrl](#thumbnailurl)
|media|A list of audio files to play.|[MediaUrl](#mediaurl) array
|shareable|A Boolean value that determines whether the audio files may be shared with others. Is **true** if the audio may be shared; otherwise, **false**. The default is **true**.|Boolean
|subtitle|A subtitle to display under the card's title. |String
|text|The descriptive text or prompt that's displayed under the card's title or subtitle.|String
|title|The title of the card.|String



<a id="cardaction" />

#### CardAction

Defines an action to perform. A card, a card's image, and a card's buttons can all specify an action.

|**Property**|**Description**|**Type**
|image|The URL of an image to display on the button. The image will display to the left of the button's text or it will be centered if you don't specify text for the button. The channel's documentation will indicate whether buttons support an image.|String
|title|The buttons text. If the action defines a card's tap action, do not specify the title.|String
|type|The type of action to perform. The following are the possible types of actions.<br/><br/>&emsp;`downloadFile`&mdash;Downloads a file to the user's device. The **value** property specifies the URL of the file to download. The channel is responsible for the download experience.<br/><br/>&emsp;`imBack`&mdash;Sends a message from the user to the bot. The message is displayed as part of the conversation. The **value** property contains the message's text. You might use this action if the buttons represent a set of answers to a question. For example, the bot asks for go-status and the card contains thumbs-up or thumbs-down buttons.<br/><br/>&emsp;`openUrl`&mdash;Opens a webpage. The **value** property specifies the URL of the webpage to open. The channel determines whether the webpage is opened in an in-app browser or the device's default browser.<br/><br/>&emsp;`playAudio`&mdash;Plays an audio clip. The **value** property contains the URL of the audio clip. You would use this action instead of `openUrl` if the channel supports an inline player.<br/><br/>&emsp;`playVideo`&mdash;Plays a video clip. The **value** property contains the URL of the video. You would use this action instead of `openUrl` if the channel supports an inline player.<br/><br/>&emsp;`postBack`&mdash;Sends a message from the user to the bot. The message is not displayed as part of the conversation. The **value** property contains the message's text.<br/><br/>Note that if the channel doesn't support `postBack`, the channel will treat the message as `imBack`. This generally won't change how your bot behaves but it does mean that if you're including data such as an order ID in your `postBack`, it may be visible on certain channels when you didn't expect it to be.|String
|value|The contents of the action. The contents depends on the type of action to perform. For example, if **type** is openUrl, you'd set **value** to the URL of the webpage to open.|String


<a id="cardimage" />

#### CardImage

Defines an image to display on a card.

|**Property**|**Description**|**Type**
|alt|A description of the image. You should include the description to support accessibility.|String
|tap|The action to perform if the user taps or clicks the image.|[CardAction](#cardaction)
|url|The URL to the source of the image. This property may also contain the base64 binary of the image (for example, data:image/png;base64,iVBORw0KGgo…).|String


<a id="channelaccount" />

#### ChannelAccount

Defines a bot or user account on the channel. 

|**Property**|**Description**|**Type**
|id|The ID that identifies the bot or user. The ID is unique per channel.|String
|name|The name of the bot or user.|String

<a id="conversation-object" />

#### Conversation

Defines the bot and users that you want to start the conversation with. Use this object when your bot starts the conversation.

|**Property**|**Description**|**Type**
|bot|The name and ID of the bot.|[ChannelAccount](#account)
|isGroup|A Boolean value that indicates whether this is a group conversation. Set to **true** if this is a group conversation; otherwise, **false** if this is a private conversation. The default is **false**. To start a group conversation, the channel must support group conversations.|Boolean
|members|A list of users to participate in the conversation. The list must contain a single user unless **isGroup** is **true**. The list may include other bots.|[ChannelAccount](#account)
|topicName|The title of the conversation.|String


<a id="conversationaccount" />

#### ConversationAccount

Defines a conversation in a channel.

|**Property**|**Description**|**Type**
|id|The ID that identifies the conversation. The ID is unique per channel. If the channel starts the conversion, it sets this ID; otherwise, the bot sets this property to the ID that it gets back in the response when it starts the conversation (see [Starting a conversation](../conversation)).|String
|isGroup|A Boolean value that indicates whether this is a group conversation. Set to **true** if this is a group conversation; otherwise, **false** if this is a private conversation. The default is **false**.|Boolean
|name|A display name that can be used to identify the conversation.|String


<a id="error" />

#### Error

Defines an error.

|**Property**|**Description**|**Type**
|message|A description of the error.|String


<a id="fact" />

#### Fact

Defines a key-value pair that contains a fact.

|**Property**|**Description**|**Type**
|key|The name of the fact. For example, Check-in. The key is used as a label when displaying the fact's value.|String
|value|The fact's value. For example, 10 October 2016.|String


<a id="geocoordinates" />

#### GeoCoordinates

Defines a geographical location using World Geodetic System (WSG84) coordinates.

|**Property**|**Description**|**Type**
|elevation|The elevation of the location.|Double
|name|The name of the location.|String
|latitude|The latitude of the location.|Double
|longitude|The longitude of the location.|Double
|type|The object's type. This is set to, GeoCoordinates.|String


<a id="herocard" />

#### HeroCard

Defines a card with a large image, title, text, and action buttons. 

|**Property**|**Description**|**Type**
|buttons|A list of buttons that let the user perform one or more actions. The channel determines the number of  buttons that you may specify.|[CardAction](#cardaction) array
|images|A list of images to display on the card. Hero cards contain only one image.|[CardImage](#cardimage) array
|subtitle|A subtitle to display under the card's title.|String
|tap|The action to perform if the user taps or clicks the card. This can be the same action as one of the buttons or a different action.|[CardAction](#cardaction)
|text|The descriptive text or prompt that's displayed under the card's title or subtitle.|String
|title|The title of the card.|String


<a id="identification" />

#### Identification

Defines a resource's identity. 

|**Property**|**Description**|**Type**
|id|The resource's identifier.|String


<a id="mediaurl" />

#### MediaUrl

Defines the URL to a media's source.

|**Property**|**Description**|**Type**
|profile|A hint that describes the media's content.|String
|url|The URL to the source of the media.|String



<a id="mention" />

#### Mention

Defines a user or bot that was mentioned in the conversation.

|**Property**|**Description**|**Type**
|mentioned|The user or bot that was mentioned in the conversation. Note that some channels such as Slack assign names per conversation, so it is possible that your bot's mentioned name may be different from your bot's actual name (see the message's [recipient](#recipient) property). For example, your bot's name may be _The cool bot_, but your bot's nickname on the channel may be _coolbot_. However, the account IDs for both would be the same. |[ChannelAccount](#account)
|text|The user or bot as mentioned in the conversation. For example, if the message is “@ColorBot pick me a new color", this property would be set to @ColorBot. Not all channels set this property.|String
|type|The object's type. This is set to, Mention.|String


<a id="place" />

#### Place

Defines a place that was mentioned in the conversation.

|**Property**|**Description**|**Type**
|geo|The geographical coordinates of the place.|[GeoCoordinates](#geocoordinates)
|name|The name of the place.|String
|type|The object's type. This is set to, Place.|String


<a id="receiptcard" />

#### ReceiptCard

Defines a card that contains a receipt for a purchase. 

|**Property**|**Description**|**Type**
|buttons|A list of buttons that let the user perform one or more actions. The channel determines the number of buttons that you may specify.|[CardAction](#cardaction) array
|facts|A list of key-value pairs that contain information that's related to the purchase. For example, if the receipt is for a hotel stay, the list of facts could be the checked-in date and checked-out date. The channel determines the number of facts that you may specify.|[Fact](#fact) array
|items|A list of purchased items.|[ReceiptItem](#receiptitem) array
|tap|The action to perform if the user taps or clicks the card. This can be the same action as one of the buttons or a different action.|[CardAction](#cardaction)
|tax|The amount of tax applied to the purchase. The tax should be a currency-formatted string.|String
|title|The title displayed at the top of the receipt.|String
|total|The total purchase price, including all applicable taxes. The total should be a currency-formatted string.|String
|vat|The amount of value added tax (VAT) applied to the purchase. The tax should be a currency-formatted string.|String


<a id="receiptitem" />

#### ReceiptItem

Defines a line item of a receipt.

|**Property**|**Description**|**Type**
|image|A thumbnail image to display next to the line item.|[CardImage](#cardimage)
|price|The total price of all units purchased. The price should be a currency-formatted string.|String
|quantity|The number of units purchased. The quantity should be a formatted number string.|String
|subtitle|The subtitle to be displayed under the line item's title.|String
|tap|The action to perform if the user taps or clicks the line item.|[CardAction](#cardaction)
|text|A description of the line item.|String
|title|The title of the line item.|String


<a id="signincard" />

#### SignInCard

Defines a card that lets a user sign in to a service. 

|**Property**|**Description**|**Type**
|buttons|A list of buttons that let the user sign in to a service. The channel determines the number of buttons that you may specify.|[CardAction](#cardaction) array
|text|A description or prompt to include on the sign in card.|String


<a id="thumbnailcard" />

#### ThumbnailCard

Defines a card with a thumbnail image, title, text, and action buttons. 

|**Property**|**Description**|**Type**
|buttons|A list of buttons that let the user perform one or more actions. The channel determines the number of buttons that you may specify.|[CardAction](#cardaction) array
|images|A list of thumbnail images to display on the card. The channel determines the number of thumbnails that you may specify.|[CardImage](#cardimage) array
|subtitle|A subtitle to display under the card's title. |String
|tap|The action to perform if the user taps or clicks the card. This can be the same action as one of the buttons or a different action.|[CardAction](#cardaction)
|text|The descriptive text or prompt that's displayed under the card's title or subtitle.|String
|title|The title of the card.|String


<a id="thumbnailurl" />

#### ThumbnailUrl

Defines the URL to an image's source.

|**Property**|**Description**|**Type**
|alt|A description of the image. You should include the description to support accessibility.|String
|url|The URL to the source of the image. This property may also contain the base64 binary of the image (for example, data:image/png;base64,iVBORw0KGgo…).|String


<a id="userdata" />

#### UserData

Defines user data that contains user state and preferences. 

|**Property**|**Description**|**Type**
|data|The user's data that you want to save (for example, user state and preferences). You may save a maximum of 32 KB of data.|Object
|eTag|The entity tag value that you can use for concurrency control of the user data. The GET and POST response will always include an ETag value. You may specify the ETag value in a POST request to ensure that you're updating the latest copy of the user's data. For more information, see [Saving User State Data](../userdata).|String



<a id="videocard" />

#### VideoCard

Defines a card that's capable of playing videos.

|**Property**|**Description**|**Type**
|autoloop|A Boolean value that determines whether to replay the list of videos when the last one ends. Set to **true** to automatically replay the videos; otherwise, **false**. The default is **true**.|Boolean
|autostart|A Boolean value that determines whether to automatically play the videos when the card is displayed. Set to **true** to automatically play the videos; otherwise, **false**. The default is **true**.|Boolean
|buttons|A list of buttons that let the user perform one or more actions. The channel determines the number of buttons that you may specify.|[CardAction](#cardaction) array
|image|A thumbnail image to display on the card.|[ThumbnailUrl](#thumbnailurl)
|media|A list of videos to play.|[MediaUrl](#mediaurl) array
|shareable|A Boolean value that determines whether the videos may be shared with others. Is **true** if the videos may be shared; otherwise, **false**. The default is **true**.|Boolean
|subtitle|A subtitle to display under the card's title. |String
|text|The descriptive text or prompt that's displayed under the card's title or subtitle.|String
|title|The title of the card.|String


<a id="errorcodes" />

### Error codes

The Connector returns the following standard HTTP status codes. All error responses will include an [Error](#error) object that contains a description of the error. 

|**Status code**|**Description**
|200|The request succeeded.
|201|The request succeeded.
|202|The request has been accepted for processing.
|400|The request was malformed or otherwise incorrect.
|401|The bot is not unauthorized to make the request.
|403|The bot is not unauthorized to make the request.
|404|The resource was not found.
|500|An internal server error occurred.
|503|The service is unavailable.  