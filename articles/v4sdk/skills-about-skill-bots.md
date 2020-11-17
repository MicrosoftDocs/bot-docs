---
title: About skill bots | Microsoft Docs
description: Describes how conversational logic in a skill bot can be made available to other bots using the Bot Framework SDK.
keywords: bot skill, skill bot.
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 11/12/2020
monikerRange: 'azure-bot-service-4.0'
---

# About skill bots

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

A _skill_ is a bot that can perform a set of tasks for another bot.
A skill can be consumed by another bot, facilitating reuse.
In this way, you can create a user-facing bot and extend it by consuming your own or 3rd party skills.
With minor modifications, any bot can act as a skill.

Skill bots:

- Implement access restrictions via a claims validator.
- As appropriate, check for initialization parameters in the initial activity's _value_ property.
- Send messages to the user as normal.
- Signal skill completion or cancellation via an `endOfConversation` activity.
  - Provide the return value, if any, in the activity's _value_ property.
  - Provide an error code, if any, in the activity's _code_ property.

For more information, see the [skills overview](skills-conceptual.md) and [about skill consumers](skills-about-skill-consumers.md).)

## Skill actions

Some skills can perform a variety of tasks or _actions_. For example, a to-do skill might allow create, update, view, and delete activities that can be accessed as discrete conversations.

<!--TODO Flesh this out-->

- See how to [implement a skill](skill-implement-skill.md) for a simple skill that implements one action.
- See how to [use dialogs within a skill](skill-actions-in-dialogs.md) for a skill that uses dialogs to implement multiple actions.

## Skill manifests

A _skill manifest_ is a JSON file that describes the actions the skill can perform, its input and output parameters, and the skill's endpoints. With v2.1 of the skill manifest schema, the manifest can also describe proactive activities the skill can send and dispatch models the skill uses.

<!--TODO Flesh this out-->

For information about the skill manifest schema, see how to [write a v2.1 skill manifest](skills-write-manifest-2-1.md) or [write a v2.0 skill manifest](skills-write-manifest-2-0.md).
