---
title: Use a dialog to consume a skill | Microsoft Docs
description: Learn how to consume a skill using dialogs, using the Bot Framework SDK.
keywords: skills
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 04/02/2020
monikerRange: 'azure-bot-service-4.0'
---

# Use a dialog to consume a skill

[!INCLUDE[applies-to](../includes/applies-to.md)]

This article demonstrates how to use a _skill dialog_ within a root bot use a skill bot.
The skill bot can handle both message and event activities.
It demonstrates how to post activities from the parent bot to the skill bot and return the skill responses to the user.
For a sample skill manifest and information about implementing the skill, see how to [use dialogs within a skill]().

For information about using a skill bot outside of dialogs, see how to [implement a skill consumer](skill-implement-consumer.md).

## Prerequisites

- Knowledge of [bot basics](bot-builder-basics.md), [how skills bots work](skills-conceptual.md), and how to [implement a skill](skill-implement-skill.md).
- An Azure subscription. If you don't have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A copy of the **skills skillDialog** sample in [**C#**](https://aka.ms/skills-using-dialogs-cs), [**JavaScript**](https://aka.ms/skills-using-dialogs-js) or [**Python**](https://aka.ms/skills-using-dialogs-py).

## About this sample

The **skills skillDialog** sample includes projects for two bots:

- The _dialog root bot_, which uses a _skill dialog_ class to consume a skill.
- The _dialog skill bot_, which uses a dialog to handle activities coming from skill consumers.

This article focuses on these aspects of the root bot:

- Use a _skill dialog_ class to manage the skill: to send message and event activities and to cancel the skill.
- Use a _skills configuration_ class to load skill definitions and to configure the _skill dialog_.

For information about using the _skill client_, _skill handler_, and _skill conversation ID factory_ classes, see how to [implement a skill consumer](skill-implement-consumer.md), as the process is the same.

For information about logging activities coming from a skill, see how to [manage logging in a skill consumer]().

### [C#](#tab/cs)

![Skill consumer class diagram](./media/skill-dialog/dialog-root-bot-cs.png)

### [JavaScript](#tab/js)

![Skill consumer class diagram](./media/skill-dialog/dialog-root-bot-js.png)

### [Python](#tab/python)

![Skill consumer class diagram](./media/skill-dialog/dialog-root-bot-py.png)

---

For information about the echo skill bot, see how to [Implement a skill](skill-implement-skill.md).

## Resources
