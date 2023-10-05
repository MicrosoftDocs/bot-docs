---
title: Conversational user experience in the Bot Framework SDK
description: Learn what makes a great conversational user experience and how to design bots that delight your users.
keywords: conversational user experience, design guide, best practices, bot design 
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: quvanwal
ms.service: bot-service
ms.topic: overview
ms.date: 07/28/2022
---

# Conversational user experience

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

The Bot Framework enables developers to create conversational bots, virtual agents, digital assistants, and all other dialog interfaces&mdash;offering flexible, accessible, and powerful ways to connect with customers, employees, and one another.
Whether you're a novice or a veteran developer, the concepts described in this section will offer insights to help you craft an effective, responsible, inclusive, and, we hope, delightful experience that tackles various business scenarios.

We define _conversational user experience (CUX)_ as a modality of interaction that's based on natural language.
When interacting with each other, human beings use conversation to communicate ideas, concepts, data, and emotional information.
CUX allows us to interact with our devices, apps, and digital services the way we communicate with each other, using phrasing and syntax via voice and text or chat that come naturally.

Other modalities can burden users with the task of learning interaction behaviors that are meaningful to the system: the syntax of a command line, the information architecture of a graphical user interface, or the touch affordances of a device.
CUX turns the tables.
Instead of users having to learn the system, it's the system that learns.
It learns what we teach it about human language&mdash;patterns of speech, colloquialisms, chit-chat, even abusive words&mdash;so that it can respond appropriately.

## A great conversational bot

Most successful bots have at least one thing in common: a great conversational user experience.
CUX can be multi-modal&mdash;employing text or voice, with or without visual, auditory, or touch enabled components.
But fundamentally, CUX is human language.

> [!TIP]
> Regardless of the type of bot you're creating, make CUX a top priority.

If you're designing a bot, assume that users will prefer the bot experience over alternative experiences like apps, websites, phone calls with live agents, and other means of addressing their particular queries.
Therefore, ensuring a great conversational user experience should be your number one priority when designing a bot.
Some key considerations include:

- Does the bot easily solve the user's problem with minimal back and forth turns?
- Does the bot solve the user's problem better/easier/faster than any of the alternative experiences?
- Does the bot run on the devices and platforms the user cares about?
- Is the bot discoverable and easy to invoke?
- Does the bot guide the user when they're stuck; either via handover to a live agent or by providing relevant help?

Users care when the bot solves their query. A great conversational bot doesn't require users to type too much, talk too much, repeat themselves several times, or explain things that the bot should automatically know and remember.

## The CUX guide

The CUX guide, contains guidance on designing a bot. This guidance aligns with best practices and capitalizes on lessons learned.
The authors and designers of this guidance are drawing from combined decades of experience building and deploying conversational UX for various types of bots, virtual agents, and other conversational experience projects, including Bot Framework Templates, Microsoft Virtual Assistant, Personality Chat, and others.

> [!TIP]
> Download the [CUX Guide Microsoft.pdf](https://github.com/microsoft/botframework-sdk/raw/main/docs/CUX%20Guide%20Microsoft.pdf)

This CUX guide is divided loosely into a few different sections. The CUX guide includes:

- An introduction to CUX, ethics and inclusive design.
- A brainstorming worksheet and guidelines for planning and designing.
- Practical development tips for building CUX experiences.

Read the topics in order, or jump to the area that addresses your needs.

> [!NOTE]
> A note on terminology: the guide explores several kinds of conversational experiences, including bots, virtual agents, and digital assistants.
We use those terms relatively interchangeably because the principles of CUX design in this guidance apply to all, but we recognize there are distinctions in the industry.
> Our intention is to offer guidance that will help with most text-based conversational experiences, regardless of their intent.

## Next steps

Now that you're familiar with conversational user experiences, learn more about [designing the first interaction](bot-service-design-first-interaction.md).
