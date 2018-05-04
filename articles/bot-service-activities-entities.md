---
title: Entities and activity types | Microsoft Docs
description: Entities and activity types.
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 03/01/2018
---

# Entities and activity types

Entities are a part of an activity, and provide additional information about the activity or conversation.

[!include[Entity boilerplate](includes/snippet-entity-boilerplate.md)] 

## Entities

The *Entities* property of a message is an array of open-ended <a href="http://schema.org/" target="_blank">schema.org</a> 
objects which allows the exchange of common contextual metadata between the channel and bot.

### Mention entities

Many channels support the ability for a bot or user to "mention" someone within the context of a conversation. 
To mention a user in a message, populate the message's entities property with a *mention* object. 
The mention object contains these properties: 

| Property | Description | 
|----|----|
| Type | type of the entity ("mention") | 
| Mentioned | channel account object that indicates which user was mentioned | 
| Text | text within the *activity.text* property that represents the mention itself (may be empty or null) |

This code example shows how to add a mention entity to the entities collection.

# [C#](#tab/cs)
[!code-csharp[set Mention](includes/code/dotnet-create-messages.cs#setMention)]

> [!TIP]
> When attempting to determine user intent, the  bot may want to ignore that portion
> of the message where it is mentioned. Call the `GetMentions` method and evaluate
> the `Mention` objects returned in the response.

# [JavaScript](#tab/js)
```javascript
var entity = context.activity.entities;

const mention = {
    type: "Mention",
    text: "@johndoe",
    mentioned: {
        name: "John Doe",
        id: "UV341235"
    }
}

entity = [mention];
```

---

### Place objects

<a href="https://schema.org/Place" target="_blank">Location-related information</a> can be conveyed 
within a message by populating the message's entities property with either 
a *Place* object or a *GeoCoordinates* object. 

The place object contains these properties:

| Property | Description | 
|----|----|
| Type | type of the entity ("Place") |
| Address | description or postal address object (future) | 
| Geo | GeoCoordinates | 
| HasMap | URL to a map or map object (future) |
| Name | name of the place |

The geoCoordinates object contains these properties:

| Property | Description | 
|----|----|
| Type | type of the entity ("GeoCoordinates") |
| Name | name of the place |
| Longitude | longitude of the location (<a href="https://en.wikipedia.org/wiki/World_Geodetic_System" target="_blank">WGS 84</a>) | 
| Longitude | latitude of the location (<a href="https://en.wikipedia.org/wiki/World_Geodetic_System" target="_blank">WGS 84</a>) | 
| Elevation | elevation of the location (<a href="https://en.wikipedia.org/wiki/World_Geodetic_System" target="_blank">WGS 84</a>) | 

This code example shows how to add a place entity to the entities collection:

# [C#](#tab/cs)
[!code-csharp[set GeoCoordinates](includes/code/dotnet-create-messages.cs#setGeoCoord)]

# [JavaScript](#tab/js)
```javascript
var entity = context.activity.entities;

const place = {
    elavation: 100,
    type: "GeoCoordinates",
    name : "myPlace",
    latitude: 123,
    longitude: 234
};

entity = [place];

```

---

### Consume entities

# [C#](#tab/cs)
To consume entities, use either the `dynamic` keyword or strongly-typed classes.

This code example shows how to use the `dynamic` keyword to process an entity within the `Entities` property of a message:

[!code-csharp[examine entity using dynamic keyword](includes/code/dotnet-create-messages.cs#examineEntity1)]

This code example shows how to use a strongly-typed class to process an entity within the `Entities` property of a message:

[!code-csharp[examine entity using typed class](includes/code/dotnet-create-messages.cs#examineEntity2)]

# [JavaScript](#tab/js)
This code example shows how to process an entity within the `entity` property of a message:
```javascript
if (entity[0].type === "GeoCoordinates" && entity[0].latitude > 34) {
    // do something
}
```

---

## Activity types

This code example show how to process an activity of type **message**:

# [C#](#tab/cs)
```cs
if (context.Activity.Type == ActivityTypes.Message){
    // do something
}
```

# [JavaScript](#tab/js)
```js
if(context.activity.type === 'message'){
    // do something
}
```

---

Activities can be of several different types past the most common **message**. There are several activity types:

| Activity.Type | Interface | Description |
|-----|-----|-----|
| [message](#message) | IMessageActivity(C#) <br> Activity(JS) | Represents a communication between bot and user. |
| [contactRelationUpdate](#contactRelationUpdate) | IContactRelationUpdateActivity(C#) <br> Activity(JS) | Indicates that the bot was added or removed from a user's contact list. |
| [conversationUpdate](#conversationUpdate) | IConversationUpdateActivity(C#) <br> Activity(JS) | Indicates that the bot was added to a conversation, other members were added to or removed from the conversation, or conversation metadata has changed. |
| [deleteUserData](#deleteUserData) | n/a | Indicates to a bot that a user has requested that the bot delete any user data it may have stored. |
| [endOfConversation](#endOfConversation) | IEndOfConversationActivity(C#) <br> Activity(JS) | Indicates the end of a conversation. |
| [event](#event) | IEventActivity(C#) <br> Activity(JS) | Represents a communication sent to a bot that is not visible to the user. |
| [invoke](#invoke) | IInvokeActivity(C#) <br> Activity(JS) | Represents a communication sent to a bot to request that it perform a specific operation. This activity type is reserved for internal use by the Microsoft Bot Framework. |
| [messageReaction](#messageReaction) | IMessageReactionActivity(C#) <br> Activity(JS) | Indicates that a user has reacted to an existing activity. For example, a user clicks the "Like" button on a message. |
| [ping](#ping) | n/a | Represents an attempt to determine whether a bot's endpoint is accessible. |
| [typing](#typing) | ITypingActivity(C#) <br> Activity(JS) | Indicates that the user or bot on the other end of the conversation is compiling a response. |

## message

Your bot will send message activities to communicate information to and receive message activities from users. 
Some messages may simply consist of plain text, while others may contain richer content such as [text to be spoken](v4sdk/bot-builder-howto-send-messages.md#send-a-spoken-message), [suggested actions](v4sdk/bot-builder-howto-add-suggested-actions.md), [media attachments](v4sdk/bot-builder-howto-add-media-attachments.md), [rich cards](v4sdk/bot-builder-howto-add-media-attachments.md#send-a-hero-card), and [channel-specific data](dotnet/bot-builder-dotnet-channeldata.md).

## contactRelationUpdate

A bot receives a contact relation update activity whenever it is added to or removed from a user's contact list. The value of the activity's action property (add | remove) indicates whether the bot has been added or removed from the user's contact list.

## conversationUpdate

A bot receives a conversation update activity whenever it has been added to a conversation, 
other members have been added to or removed from a conversation, 
or conversation metadata has changed. 

If members have been added to the conversation, the activity's members added property will contain an array of 
channel account objects to identify the new members. 

To determine whether your bot has been added to the conversation (i.e., is one of the new members), evaluate whether the recipient Id value for the activity (i.e., your bot's id) 
matches the Id property for any of the accounts in the members added array.

If members have been removed from the conversation, the members removed property will contain an array of channel account objects to identify the removed members. 

> [!TIP]
> If your bot receives a conversation update activity indicating that a user has joined the conversation, 
> you may choose to have it respond by sending a welcome message to that user. 

## deleteUserData

A bot receives a delete user data activity when a user requests deletion of any data that the bot has previously persisted for him or her. If your bot receives this type of activity, it should delete any personally identifiable information (PII) that it has previously stored for the user that made the request.

## endOfConversation 

A bot receives an end of conversation activity to indicate that the user has ended the conversation. A bot may send an end of Conversation activity to indicate to the user that the conversation is ending. 

## event

Your bot may receive an event activity from an external process or service that wants to 
communicate information to your bot without that information being visible to users. The 
sender of an event activity typically does not expect the bot to acknowledge receipt in any way.

## invoke

Your bot may receive an invoke activity that represents a request for it to perform a specific operation. 
The sender of an invoke activity typically expects the bot to acknowledge receipt via HTTP response. 
This activity type is reserved for internal use by the Microsoft Bot Framework.

## messageReaction

Some channels will send message reaction activities to your bot when a user reacted to an existing activity. For example, a user clicks the "Like" button on a message. The reply toId property will indicate which activity the user reacted to.

The message reaction activity may correspond to any number of message reaction types that the channel defined. For example, "Like" or "PlusOne" as reaction types that a channel may send. 

## ping

A bot receives a ping activity to determine whether its endpoint is accessible. The bot should respond with HTTP status code 200 (OK), 403 (Forbidden), or 401 (Unauthorized).

## typing

A bot receives a typing activity to indicate that the user is typing a response. 
A bot may send a typing activity to indicate to the user that it is working to fulfill a request or compile a response. 

## Additional resources

- <a href="/dotnet/api/microsoft.bot.connector.activity" target="_blank">Activity class</a>
