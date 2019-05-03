---
title: Entities and activity types | Microsoft Docs
description: Entities and activity types.
keywords: mention entities, activity types, consume entities
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 03/01/2018
---
# Entities and activity types

Entities are a part of an activity, and provide additional information about the activity or conversation.

[!include[Entity boilerplate](includes/snippet-entity-boilerplate.md)]

## Entities

The *entities* property of a message is an array of open-ended <a href="http://schema.org/" target="_blank">schema.org</a>
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

There are several activity types; activities can be of several different types past the most common **message**. Explanations and further details can be found on the [Activity schema page](https://aka.ms/botSpecs-activitySchema).

::: moniker range="azure-bot-service-3.0"

## Additional resources

- <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/dc/d2f/class_microsoft_1_1_bot_1_1_connector_1_1_activity.html" target="_blank">Activity class</a>
::: moniker-end
