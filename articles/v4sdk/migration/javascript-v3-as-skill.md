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

This article describes how to convert 2 sample JavaScript v3 bots to skills and to create a v4 skill consumer that can access these skills.
To convert a .NET v3 bot to a skill, see how to [Convert a .NET v3 bot to a skill](net-v3-as-skill.md).
To migrate a JavaScript bot from v3 to v4, see how to [Migrate a Javascript v3 bot to a v4 bot](conversion-javascript.md).

## Prerequisites

- Visual Studio Code.
- Node.js.
- An Azure subscription. If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A copy of the v4 JavaScript sample skill consumer: [**v4-root-bot**](https://aka.ms/js-simple-root-bot). <!--TODO Create aka link once there's a target-->

## About the bots

In this article, each v3 bot is created to act as a skill. A v4 skill consumer is included, so that you can test the converted bots as skills.

- The **v3-skill-bot** echoes back messages it receives. As a skill, it _completes_ when it receives an "end" or "stop" message. The bot to convert is based on a minimal v3 bot.
- The **v3-booking-bot-skill** allows the user to book a flight or a hotel. As a skill, it sends collected information back to the parent when finished.

Also, a v4 skill consumer, the **v4-root-bot**, demonstrates how to consume the skills and allows you to test them.

To use the skill consumer to test the skills, all 3 bots need to be running at the same time. The bots can be tested locally using the Bot Framework Emulator, with each bot using a different local port.
