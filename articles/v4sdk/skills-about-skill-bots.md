---
title: About skill bots | Microsoft Docs
description: Describes how conversational logic in one bot can be used by another bot using the Bot Framework SDK.
keywords: bot skill, skill bot.
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 04/28/2020
monikerRange: 'azure-bot-service-4.0'
---

# About skill bots

[!INCLUDE [applies-to-v4](../includes/applies-to.md)]

A _skill_ is a bot that can perform a set of tasks for another bot.
A skill can be consumed by another bot, facilitating reuse.
In this way, you can create a user-facing bot and extend it by consuming your own or 3rd party skills.
With minor modifications, any bot can act as a skill.

Skill bots:

- Implement access restrictions via a claims validator.
- As appropriate, check for initialization parameters in the initial activity's _value_ property.
- Send messages to the user as normal, via `message` activities.
- Signal skill completion or cancellation via an `endOfConversation` activity.
  - Provide the return value, if any, in the activity's _value_ property.
  - Provide an error code, if any, in the activity's _code_ property.

## Skill actions

Some skills can perform a variety of tasks or _actions_. For example, a to-do skill might allow create, update, view, and delete activities that can be accessed as discrete conversations.

<!--TODO Flesh this out-->

You publish skill and skill consumer bots separately.

See how to [implement a skill](skill-implement-skill.md) for a simple skill that implements one action.

<!-- Waiting on merge of PR 2079:
- See how to [use dialogs within a skill](skill-actions-in-dialogs.md) for a skill that uses dialogs to implement multiple actions.
-->

## Skill manifests

Since a skill consumer does not necessarily have access to the skill code, use a skill manifest to describe the activities the skill can receive and generate, its input and output parameters, and the skill's endpoints.

<!--TODO Flesh this out-->

For information about the skill manifest schema, see how to [write a v2.1 skill manifest](skills-write-manifest-2-1.md) (preview) or [write a v2.0 skill manifest](skills-write-manifest-2-0.md).
