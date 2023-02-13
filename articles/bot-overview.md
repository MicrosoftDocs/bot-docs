---
title: Choose the right chatbot solution for your use case
description: Learn about different chatbot solutions, who they're for, and when to use them.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: overview
ms.service: bot-service
ms.date: 01/04/2023
ms.custom: mode-api, tab-zone-seo
---

# Choose the right chatbot solution for your use case

A chatbot is an application that has written or spoken natural language as its user interface. In other words, a conversation is the means through which questions are answered, requests are serviced, and so on.

This article provides an overview of some of the chatbot solutions Microsoft provides. If you're new to making chatbots, we recommend starting with Power Virtual Agents.

The following table lists Microsoft products and services for building bots, who they support, and a brief description. Following sections describe each product in more detail.

| Product | Audience | Description |
|:-|:-|:-|
| [Power Virtual Agents](#power-virtual-agents) | Fusion teams, citizen developers | Power Virtual Agents is an end-to-end bot-building tool, with built-in natural language understanding models, data connectivity through Power Automate, and support for multiple channels. |
| [Health Bot](#health-bot) | Healthcare organizations | Provides support for healthcare organizations. Health Bot helps you build and deploy compliant, AI-powered virtual health assistants and health bots. |
| [Bot Framework SDK](#bot-framework-sdk) | Developers | Provides a framework for building bots, including tools, templates, and related AI services.|

## Power Virtual Agents

Power Virtual Agents is designed to support _fusion teams_&mdash;where professional developers and various subject matter experts collaborate. It also supports citizen developers and specialized vendors.

Power Virtual Agents is a tool for chatbot development that's included in [Microsoft Power Platform](https://powerplatform.microsoft.com/)&mdash;a business-application platform that incorporates data analysis, solution building, and process automation.
You don't need to write code or understand the details of the underlying AI technologies to build bots in Power Virtual Agents.
Such bots can apply automation and other capabilities within the Power Platform, and you can rapidly develop sophisticated chatbot experiences.

- You can connect virtual agents to various user platforms, such as [Microsoft 365](https://www.microsoft.com/microsoft-365) and [Microsoft Dynamics 365](https://dynamics.microsoft.com/).
- You can use over 600 prebuilt data connectors, available through Power Automate.

For more information about Power Virtual Agents, see the [product overview page](https://powervirtualagents.microsoft.com). For details about pricing, see [Power Virtual Agents pricing](https://powervirtualagents.microsoft.com/pricing/).

## Health Bot

The Health Bot Service is a cloud platform that healthcare organizations can use to build and deploy compliant, AI-powered virtual health assistants and health bots. The service can help organizations improve processes and reduce costs. It offers your users _intelligent_ and _personalized_ access to health-related information and interactions through a natural conversation experience.

The Health Bot Service is ideal for developers in IT departments of healthcare organizations such as providers, pharmaceutical companies, telemedicine providers, and health insurers. Healthcare organizations can use the service to build a _health bot instance_ and integrate it with their systems that patients, provdiders, and other representatives interact with.

The Health Bot Service contains a built-in medical database, including triage protocols. You can also extend a health bot instance to include your own scenarios and integrate with other IT systems and data sources.

For more information about the Health Bot Service, see [Health Bot Overview](/azure/health-bot/overview). For information about pricing models, see [Choosing the right Health Bot plan](/azure/health-bot/resources/pricing-details).

## Bot Framework SDK

Microsoft Bot Framework and Azure Bot Service provide tools to build, test, deploy, and manage intelligent bots. The Bot Framework includes a modular and extensible SDK for building bots, including tools, templates, and related AI services. With this framework, developers can create bots that use speech, understand natural language, handle questions and answers, and more.

Azure Bot Service and the Bot Framework offer:

- The Bot Framework SDK for developing bots.
- Bot Framework tools to cover end-to-end bot development workflow.
- The Bot Connector service to send and receive messages and events between bots and channels.
- Bot deployment and channel configuration in Azure.

Additionally, bots may make use of other Azure services:

- Azure Cognitive Services to build intelligent applications.
- Azure Storage for cloud storage.

For more information about the Bot Framework SDK, see [What is the Bot Framework SDK](bot-service-overview.md).
Once you have created and deployed your bot to Azure, the chatbot service consumes resources.
For details about pricing, see [Azure Bot Services pricing](https://azure.microsoft.com/pricing/details/bot-services/).
Costs associated with the consumption of resources on Azure are in addition to the cost of the chatbot service itself.

## Skill bots

As an advanced scenario, you can create a _skill_ bot that provides features to other bots. You can develop the skill bot and the bot that _consumes_ the skill in different products. For more information about skill bots, see:

- [About skills in the SDK](./v4sdk/skills-conceptual.md)
- [Use a Power Virtual Agents bot as a skill](/power-virtual-agents/advanced-use-pva-as-a-skill)
- [Configure a skill for use in Power Virtual Agents](/power-virtual-agents/configuration-add-skills)
- [Implement a skill with the Bot Framework SDK](./v4sdk/skill-implement-skill.md)

## Next Steps

- [Create and deploy a Power Virtual Agents bot online](/power-virtual-agents/fundamentals-get-started)
- [Create your first Health Bot](/azure/health-bot/quickstart-createyourhealthcarebot)
- [Create a bot with the Bot Framework SDK](bot-service-quickstart-create-bot.md)
