---
title: Create an Azure Bot resource in the Azure portal
description: Learn how to use the Azure portal to create a bot resource for the Azure AI Bot Service, an integrated, dedicated bot development environment.
keywords: Quickstart, create bot resource, bot service, Azure Bot
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: quickstart
ms.service: bot-service
ms.date: 07/22/2022
ms.custom: abs-meta-21q1
---

# Use the Azure portal to Create an Azure Bot resource

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

The _Azure Bot_ resource (_bot resource_) allows you to register your bot with Azure AI Bot Service and to connect your bot to channels. You can build, connect, and manage bots to interact with your users wherever they are, from your app or website to Teams, Messenger and many other channels.

This article describes how to create a bot resource through the Azure portal.

- To learn how to create a bot, see the [Create a bot with the Bot Framework SDK](../bot-service-quickstart-create-bot.md) quickstart.
- For information on how to provision and publish a bot to Azure, see how to [Deploy your bot in Azure](../bot-builder-deploy-az-cli.md).

## Managing resources

When you create a bot resource, Azure creates associated resources.
Some of the resources created depend on how you decide to manage your bot's identity.

[!INCLUDE [identity-app-type-support](../includes/azure-bot-resource/identity-app-type-support.md)]

## Prerequisites

- If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- The Bot Framework SDK for C# or JavaScript version 4.15.0 or later, for user-assigned managed identity and single-tenant bots.

[!INCLUDE [azure bot resource](../includes/azure-bot-resource/azure-bot-resource.md)]

## Additional information

- For information about identity management with Entra ID, see [What is Entra ID?](/azure/active-directory/fundamentals/active-directory-whatis).
- For information about Azure App Service and App Service plans, see the [App Service overview](/azure/app-service/overview).
- For information about Azure resources and how they're managed in general, see the [Azure Resource Manager overview](/azure/azure-resource-manager/management/overview).

> [!NOTE]
> The Bot Framework Composer and Bot Framework Emulator currently only support multi-tenant bots.
> The Bot Framework SDK for C# or JavaScript version 4.15.0 or later is required for user-assigned managed identity and single-tenant bots.

### Skill support

[!INCLUDE [skills-and-identity-types](../includes/skills-and-identity-types.md)]

For information on how to configure a skill or skill consumer, see [Implement a skill](skill-implement-skill.md) or [Implement a skill consumer](skill-implement-consumer.md).

## Next steps

- [Create a bot with Bot Framework Composer](/composer/quickstart-create-bot)
- [Create a bot with the Bot Framework SDK](../bot-service-quickstart-create-bot.md)
