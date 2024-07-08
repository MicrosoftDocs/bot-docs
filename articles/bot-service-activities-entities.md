---
title: Entities and activity types in Azure AI Bot Service
description: Learn how entities store information that bots and channels use when exchanging messages. See how to populate entity properties and how to consume entities.
keywords: mention entities, activity types, consume entities
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: reference
ms.date: 10/10/2022
ms.custom:
  - code-snippets
  - evergreen
---

# Entities and activity types

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

Entities are a part of an activity, and provide additional information about the activity or conversation.

> [!NOTE]
> Different parts of the SDK define separate _entity_ classes or elements.

## Entities

The _entities_ property of a message is an array of open-ended [schema.org](https://schema.org/) objects, which allows the exchange of common contextual metadata between the channel and bot.

### Mention entities

Many channels support the ability for a bot or user to "mention" someone within the context of a conversation.
To mention a user in a message, populate the message's entities property with a _mention_ object.
The mention object contains these properties:

| Property  | Description                                                                                        |
|