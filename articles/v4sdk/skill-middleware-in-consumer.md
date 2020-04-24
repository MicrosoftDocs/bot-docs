---
title: Use middleware in a skill consumer | Microsoft Docs
description: Learn how to use middleware in a skill consumer, using the Bot Framework SDK.
keywords: skills
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 04/23/2020
monikerRange: 'azure-bot-service-4.0'
---

# Use middleware in a skill consumer

[!INCLUDE[applies-to](../includes/applies-to.md)]

Since a skill consumer exchanges activities with both users and one or more skill bots, you need to take a few extra steps when adding middleware to a skill consumer.
This article demonstrates how to properly use middleware within a skill consumer.

For a discussion of the skill consumer that uses this middleware, see how to [use a dialog to consume a skill](skill-use-skilldialog.md).

## Prerequisites

- Knowledge of [bot basics](bot-builder-basics.md), [how skills bots work](skills-conceptual.md), and how to [implement a skill consumer](skill-implement-consumer.md).
- An Azure subscription. If you don't have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A copy of the **skills skillDialog** sample in [**C#**](https://aka.ms/skills-using-dialogs-cs), [**JavaScript**](https://aka.ms/skills-using-dialogs-js) or [**Python**](https://aka.ms/skills-using-dialogs-py).

## About this sample
