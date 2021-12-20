---
title: Entities and activity types in Azure Bot Service
description: Learn how entities store information that bots and channels use when exchanging messages. See how to populate entity properties and how to consume entities.
keywords: mention entities, activity types, consume entities
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: reference
ms.date: 09/09/2021
ms.custom: code-snippets
---

# Entities and activity types

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

Entities are a part of an activity, and provide additional information about the activity or conversation.

> [!NOTE]
> Different parts of the SDK define separate _entity_ classes or elements.
> For LUIS recognition entities, see [Extract entities](v4sdk/bot-builder-howto-v4-luis.md).

## Entities

The *entities* property of a message is an array of open-ended [schema.org](https://schema.org/) objects, which allows the exchange of common contextual metadata between the channel and bot.

### Mention entities

Many channels support the ability for a bot or user to "mention" someone within the context of a conversation.
To mention a user in a message, populate the message's entities property with a *mention* object.
The mention object contains these properties:

| Property | Description |
|--|--|
| Type | Type of the entity ("mention") |
| Mentioned | Channel account object that indicates which user was mentioned |
| Text | Text within the *activity.text* property that represents the mention itself (may be empty or null) |

This code example shows how to add a mention entity to the entities collection.

# [C#](#tab/cs)

```csharp
var entity = new Entity();
entity.SetAs(new Mention()
{
    Text = "@johndoe",
    Mentioned = new ChannelAccount()
    {
        Name = "John Doe",
        Id = "UV341235"
    }
});
entities.Add(entity);
```

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

[Location-related information](https://schema.org/Place) can be conveyed
within a message by populating the message's entities property with either
a *Place* object or a *GeoCoordinates* object.

The place object contains these properties:

| Property | Description |
|--|--|
| Type | Type of the entity ("Place") |
| Address | Description or postal address object (future) |
| Geo | GeoCoordinates |
| HasMap | URL to a map or map object (future) |
| Name | Name of the place |

The geoCoordinates object contains these properties:

| Property | Description |
|--|--|
| Type | Type of the entity ("GeoCoordinates") |
| Name | Name of the place |
| Longitude | Longitude of the location ([WGS 84][WGS-84]) |
| Latitude | Latitude of the location ([WGS 84][WGS-84]) |
| Elevation | Elevation of the location ([WGS 84][WGS-84]) |

This code example shows how to add a place entity to the entities collection:

# [C#](#tab/cs)

```csharp
var entity = new Entity();
entity.SetAs(new Place()
{
    Geo = new GeoCoordinates()
    {
        Latitude = 32.4141,
        Longitude = 43.1123123,
    }
});
entities.Add(entity);
```

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

To consume entities, use either the `dynamic` keyword or strongly typed classes.

This code example shows how to use the `dynamic` keyword to process an entity within the `Entities` property of a message:

```csharp
if (entity.Type == "Place")
{
    dynamic place = entity.Properties;
    if (place.geo.latitude > 34)
        // do something
}
```

This code example shows how to use a strongly typed class to process an entity within the `Entities` property of a message:

```csharp
if (entity.Type == "Place")
{
    Place place = entity.GetAs<Place>();
    GeoCoordinates geo = place.Geo.ToObject<GeoCoordinates>();
    if (geo.Latitude > 34)
        // do something
}
```

# [JavaScript](#tab/js)

This code example shows how to process an entity within the `entity` property of a message:

```javascript
if (entity[0].type === "GeoCoordinates" && entity[0].latitude > 34) {
    // do something
}
```

---

## Activity types

Activities can be of several different types past the most common **message**. Explanations and further details on different activity types can be found in the [Bot Framework Activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md).

[WGS-84]: https://gisgeography.com/wgs84-world-geodetic-system/
