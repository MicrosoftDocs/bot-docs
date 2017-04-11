---
title: Create messages using the Bot Framework Connector service and .NET | Microsoft Docs
description: Learn how to create messages using the Bot Framework Connector service via the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, Connector, Connector service, activity, message, create message, message properties
author: kbrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/13/2017
ms.reviewer: rstand
#ROBOTS: Index
---

# Create messages

Your bot will send **message** [activities](~/dotnet/activities.md) to communicate information to users, 
and likewise, will also receive **message** activities from users. 
Some messages may simply consist of plain text, while others may contain richer content such as 
[media attachments, buttons, and cards](~/dotnet/add-attachments.md). 
This article describes some of the commonly-used message properties.

## Message text and format

To create a basic message that contains only plain text, simply specify the `Text` property (as contents of the message) 
and the `Locale` property (as the locale of the sender). 

[!code-csharp[Set message properties](~/includes/code/dotnet-create-messages.cs#setBasicProperties)]

The `TextFormat` property of a message can be used to specify the format of the text. 
`TextFormat` defaults to "markdown" (interpret text using markdown formatting standards), 
but you can alternatively specify either "plain" (interpret text as plain text) or "xml" (interpret text as XML markup).

These styles are supported with `TextFormat` of "markdown":

| Style | Markdown | Example | 
| ---- | ---- | ---- | 
| bold | \*\*text\*\* | **text** |
| italic | \*text\* | *text* |
| header (1-5) | # H1 | #H1 |
| strikethrough | \~\~text\~\~ | ~~text~~ |
| horizontal rule | --- |  |
| unordered list | \* text |  <ul><li>text</li></ul> |
| preformatted text | \`text\` |  |
| blockquote | \> text | <blockquote>text</blockquote> |
| hyperlink | \[bing](http://www.bing.com) | [bing](http://www.bing.com) |
| image link| \[duck](http://aka.ms/Fo983c) | [duck](http://aka.ms/Fo983c) |

> [!NOTE]
> In cases where a channel is not capable of rendering the formatting that's specified by markdown, 
> the message will be rendered using reasonable approximation. For example, if markdown specifies **bold** 
> text for a message that's sent via text messaging, the text will be rendered as \*bold\*. 
> 
> Channels that support fixed width formats and HTML will also render standard table markdown. 
> Other channels may not properly render tables.

`TextFormat` of "xml" is supported only by the Skype channel. 
These styles are supported with `TextFormat` of "xml":

| Style | Markdown | Example | 
|----|----|----|----|
| bold | \<b\>text\</b\> | **text** | 
| italic | \<i\>text\</i\> | *text* |
| underline | \<u\>text\</u\> | <u>text</u> |
| strikethrough | \<s\>text\</s\> | <s>text</s> |
| hyperlink | \<a href="http://www.bing.com"\>bing\</a\> | <a href="http://www.bing.com">bing</a> |

## Message attachments

The `Attachments` property of a message activity can be used to send and receive simple media attachments 
(e.g., image, audio, video, file) and rich cards. 
For details, see [Add attachments to messages](~/dotnet/add-attachments.md).

## Message entities

The `Entities` property of a message is an array of open ended <a href="http://schema.org/" target="_blank">schema.org</a> 
objects which allows the exchange of common contextual metadata between the channel and bot.

### Mention entities

Many channels support the ability for a bot or user to "mention" someone within the context of a conversation. 

To mention a user in a message, populate the message's `Entities` property with a `Mention` object. 
The `Mention` object contains these properties: 

| Property | Description | 
|----|----|
| type | type of the entity ("mention") | 
| mentioned | `ChannelAccount` object that indicates which user was mentioned | 
| text | text within the `Activity.Text` property that represents the mention itself (may be empty or null) |

This code example shows how to add a `Mention` entity to the `Entities` collection.

[!code-csharp[set Mention](~/includes/code/dotnet-create-messages.cs#setMention)]

> [!TIP]
> You bot may want to identify when it is mentioned in a message, so that it may 
> ignore that portion of the message when attempting to determine user intent. 
> To determine whether your bot was mentioned in a message, call the `GetMentions` method 
> and evaluate the `Mention` objects that are returned in the response.

### Place entities

<a href="https://schema.org/Place" target="_blank">Location-related information</a> can be conveyed 
within a message by populating the message's `Entities` property with either 
a `Place` object or a `GeoCoordinates` object. 

The `Place` object contains these properties:

| Property | Description | 
|----|----|
| Type | type of the entity ("Place") |
| Address | description or `PostalAddress` object (future) | 
| Geo | GeoCoordinates | 
| HasMap | URL to a map or `Map` object (future) |
| Name | name of the place |

The `GeoCoordinates` object contains these properties:

| Property | Description | 
|----|----|
| Type | type of the entity ("GeoCoordinates") |
| Name | name of the place |
| Longitude | longitude of the location (<a href="https://en.wikipedia.org/wiki/World_Geodetic_System" target="_blank">WGS 84</a>) | 
| Longitude | latitude of the location (<a href="https://en.wikipedia.org/wiki/World_Geodetic_System" target="_blank">WGS 84</a>) | 
| Elevation | elevation of the location (<a href="https://en.wikipedia.org/wiki/World_Geodetic_System" target="_blank">WGS 84</a>) | 

This code example shows how to add a `Place` entity to the `Entities` collection:

[!code-csharp[set GeoCoordinates](~/includes/code/dotnet-create-messages.cs#setGeoCoord)]

### Consuming entities

To consume entities, use either the `dynamic` keyword or strongly-typed classes.

This code example shows how to use the `dynamic` keyword to process an entity within the `Entities` property of a message:

[!code-csharp[examine entity using dynamic keyword](~/includes/code/dotnet-create-messages.cs#examineEntity1)]

This code example shows how to use a strongly-typed class to process an entity within the `Entities` property of a message:

[!code-csharp[examine entity using typed class](~/includes/code/dotnet-create-messages.cs#examineEntity2)]

## Message channel data

The `ChannelData` property of a message activity can be used to implement channel-specific functionality. 
For details, see [Implement channel-specific functionality](~/dotnet/channeldata.md).

## Additional resources

- [Activity types](~/dotnet/activities.md)
- [Send and receive activities](~/dotnet/connector.md)
- [Add attachments to messages](~/dotnet/add-attachments.md)
- [Implement channel-specific functionality](~/dotnet/channeldata.md)