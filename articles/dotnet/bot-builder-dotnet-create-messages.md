---
title: Create messages with the Bot Framework SDK for .NET | Microsoft Docs
description: Learn about commonly-used message properties within the Bot Framework SDK for .NET.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Create messages

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

Your bot will send **message** [activities](bot-builder-dotnet-activities.md) to communicate information to users, and likewise, will also receive **message** activities from users. 
Some messages may simply consist of plain text, while others may contain richer content such as [text to be spoken](bot-builder-dotnet-text-to-speech.md), [suggested actions](bot-builder-dotnet-add-suggested-actions.md), 
[media attachments](bot-builder-dotnet-add-media-attachments.md), [rich cards](bot-builder-dotnet-add-rich-card-attachments.md), and [channel-specific data](bot-builder-dotnet-channeldata.md). 

This article describes some of the commonly-used message properties.

## Customizing a message

To have more control over the text formatting of your messages, you can create a custom message using the [Activity](https://docs.botframework.com/en-us/csharp/builder/sdkreference/dc/d2f/class_microsoft_1_1_bot_1_1_connector_1_1_activity.html) object and set the properties necessary before sending it to the user.

This sample shows how to create a custom `message` object and set the `Text`, `TextFormat`, and `Local` properties.

[!code-csharp[Set message properties](../includes/code/dotnet-create-messages.cs#setBasicProperties)]

The `TextFormat` property of a message can be used to specify the format of the text. The `TextFormat` property can be set to **plain**, **markdown**, or **xml**. The default value for `TextFormat` is **markdown**. 

## Attachments

The `Attachments` property of a message activity can be used to send and receive simple media attachments 
(image, audio, video, file) and rich cards. 
For details, see [Add media attachments to messages](bot-builder-dotnet-add-media-attachments.md) and 
[Add rich cards to messages](bot-builder-dotnet-add-rich-card-attachments.md).

## Entities

The `Entities` property of a message is an array of open-ended <a href="http://schema.org/" target="_blank">schema.org</a> 
objects which allows the exchange of common contextual metadata between the channel and bot.

### Mention entities

Many channels support the ability for a bot or user to "mention" someone within the context of a conversation. 
To mention a user in a message, populate the message's `Entities` property with a `Mention` object. 
The `Mention` object contains these properties: 

| Property | Description | 
|----|----|
| Type | type of the entity ("mention") | 
| Mentioned | `ChannelAccount` object that indicates which user was mentioned | 
| Text | text within the `Activity.Text` property that represents the mention itself (may be empty or null) |

This code example shows how to add a `Mention` entity to the `Entities` collection.

[!code-csharp[set Mention](../includes/code/dotnet-create-messages.cs#setMention)]

> [!TIP]
> When attempting to determine user intent, the  bot may want to ignore that portion
> of the message where it is mentioned. Call the `GetMentions` method and evaluate
> the `Mention` objects returned in the response.

### Place objects

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
| Latitude | latitude of the location (<a href="https://en.wikipedia.org/wiki/World_Geodetic_System" target="_blank">WGS 84</a>) | 
| Elevation | elevation of the location (<a href="https://en.wikipedia.org/wiki/World_Geodetic_System" target="_blank">WGS 84</a>) | 

This code example shows how to add a `Place` entity to the `Entities` collection:

[!code-csharp[set GeoCoordinates](../includes/code/dotnet-create-messages.cs#setGeoCoord)]

### Consume entities

To consume entities, use either the `dynamic` keyword or strongly-typed classes.

This code example shows how to use the `dynamic` keyword to process an entity within the `Entities` property of a message:

[!code-csharp[examine entity using dynamic keyword](../includes/code/dotnet-create-messages.cs#examineEntity1)]

This code example shows how to use a strongly-typed class to process an entity within the `Entities` property of a message:

[!code-csharp[examine entity using typed class](../includes/code/dotnet-create-messages.cs#examineEntity2)]

## Channel data

The `ChannelData` property of a message activity can be used to implement channel-specific functionality. 
For details, see [Implement channel-specific functionality](bot-builder-dotnet-channeldata.md).

## Text to speech

The `Speak` property of a message activity can be used to specify the text to be spoken by your bot on a speech-enabled channel. The `InputHint` property of a message activity can be used to control the state of the client's microphone and input box (if any). For details, see [Add speech to messages](bot-builder-dotnet-text-to-speech.md).

## Suggested actions

The `SuggestedActions` property of a message activity can be used to present buttons that the user can tap to provide input. Unlike buttons that appear within rich cards (which remain visible and accessible to the user even after being tapped), buttons that appear within the suggested actions pane will disappear after the user makes a selection. For details, see [Add suggested actions to messages](bot-builder-dotnet-add-suggested-actions.md).

## Next steps

A bot and a user can send messages to each other. When the message is more complex, your bot can send a rich card in a message to the user. Rich cards cover many presentation and interaction scenarios commonly needed in most bots.

> [!div class="nextstepaction"]
> [Send a rich card in a message](bot-builder-dotnet-add-rich-card-attachments.md)

## Additional resources

- [Activities overview](bot-builder-dotnet-activities.md)
- [Send and receive activities](bot-builder-dotnet-connector.md)
- [Add media attachments to messages](bot-builder-dotnet-add-media-attachments.md)
- [Add rich cards to messages](bot-builder-dotnet-add-rich-card-attachments.md)
- [Add speech to messages](bot-builder-dotnet-text-to-speech.md)
- [Add suggested actions to messages](bot-builder-dotnet-add-suggested-actions.md)
- [Implement channel-specific functionality](bot-builder-dotnet-channeldata.md)
- <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/dc/d2f/class_microsoft_1_1_bot_1_1_connector_1_1_activity.html" target="_blank">Activity class</a>
- <a href="/dotnet/api/microsoft.bot.connector.imessageactivity" target="_blank">IMessageActivity interface</a>

