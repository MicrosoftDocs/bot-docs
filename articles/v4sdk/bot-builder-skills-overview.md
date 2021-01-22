---
title: Virtual Assistant Skills Overview - Bot Service
description: Become familiar with Virtual Assistant Skills. Learn about the Bot Framework Skills that are available for use in bots, such as calendar and email skills.
author: darrenj
ms.author: darrenj
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 10/09/2019
monikerRange: 'azure-bot-service-4.0'
---

# Virtual Assistant - Skills Overview

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

## Overview

Developers can compose conversational experiences by stitching together reusable conversational capabilities, known as Skills.

Within an Enterprise, this could be creating one parent bot bringing together multiple sub-bots owned by different teams, or more broadly leveraging common capabilities provided by other developers. With this preview of Skills, developers can create a new bot (typically through the Virtual Assistant template) and add/remove Skills with one command line operation incorporating all Dispatch and Configuration changes.

Skills are themselves bots, invoked remotely and a Skill developer template (.NET, TS) is available to facilitate creation of new Skills.

A key design goal for Skills was to maintain the consistent Activity protocol and ensure the development experience was as close to any normal V4 SDK bot as possible.

![Skills Scenarios](./media/enterprise-template/skills-scenarios.png)

## Bot Framework Skills

At this time you have made available the following Bot Framework Skills, powered by the Microsoft Graph and available in multiple languages.

![Skills Scenarios Build](./media/enterprise-template/skills-at-build.png)

| Name | Description |
| ---- | ----------- |
|[Calendar Skill](https://aka.ms/bf-calendar-skill)|Add calendar capabilities to your assistant. Powered by Microsoft Graph and Google.|
|[Email Skill](https://aka.ms/bf-email-skill)|Add email capabilities to your assistant. Powered by Microsoft Graph and Google.|
|[To Do Skill](https://aka.ms/bf-todo-skill)|Add task management capabilities to your assistant. Powered by Microsoft Graph.|
|[Point of Interest Skill](https://aka.ms/bf-poi-skill)|Find points of interest and directions. Powered by Azure Maps and FourSquare.|
|[Automotive Skill](https://aka.ms/bf-auto-skill)|Industry-vertical Skill for showcasing enabling car feature control.|
|[Experimental Skills](https://aka.ms/bf-experimental-skills)|News, Restaurant Booking and Weather.|

## Getting Started

Refer to the [tutorials](https://aka.ms/bfs-tutorials) to learn how to leverage existing skills and build your own.
