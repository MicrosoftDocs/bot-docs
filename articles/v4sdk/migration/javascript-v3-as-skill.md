---
title: Convert a JavaScript v3 bot to a skill
description: Shows how to convert existing JavaScript v3 bots to skills and consume them from a JavaScript v4 bot.
keywords: JavaScript, bot migration, skills, v3 bot
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 02/19/2020
monikerRange: 'azure-bot-service-4.0'
---

# Convert a JavaScript v3 bot to a skill

Converting an existing v3 bot to a skill takes just a few steps. It allows other bots to access the v3 bot, while allowing users to still access the v3 bot. This can be less work than migrating the v3 bot to the v3 SDK. Any v4 bot designed to do so can consume a skill.

The skill consumer and skill use the Bot Connector service to communicate with each other. The consumer and skill can be implemented in different languages, such as .NET and JavaScript. See [about skills](../skills-conceptual.md) for more information.

This article describes how to convert 3 sample JavaScript v3 bots to skills and also how to create a v4 skill consumer that can access these skills.

To migrate a JavaScript bot from v3 to v4, see how to [Migrate a JavaScript v3 bot to a v4 bot](conversion-javascript.md).
To convert a .NET v3 bot to a skill, see how to [Convert a JavaScript v3 bot to a skill](net-v3-as-skill.md).
