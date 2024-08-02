---
title: Register a Bot Framework bot with Azure
description: If you don't currently host your bot in Azure, you can still make it available in Azure. To do so, you enter in Azure the web address where your bot is hosted.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: azure-ai-bot-service
ms.date: 03/15/2022
ms.custom:
  - abs-meta-21q1
  - evergreen
monikerRange: 'azure-bot-service-4.0'
---

# Register a bot with Azure

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

If you don't currently host your bot in Azure, you can still make it available in Azure and use Azure to connect your bot to channels. To do so, enter in Azure the web address where your bot is hosted.

This article shows how to register such a bot with Azure AI Bot Service.

> [!IMPORTANT]
> You only need to register a bot if it's not hosted in Azure.
> Bots created using the Azure CLI are already registered with the Azure AI Bot Service.

[!INCLUDE [identity-app-type-support](./includes/azure-bot-resource/identity-app-type-support.md)]

This article doesn't describe how to create or deploy the bot to register. For more information, see:

- The [Create a bot](bot-service-quickstart-create-bot.md) quickstart
- The [Deploy a basic bot](bot-builder-deploy-az-cli.md) tutorial

[!INCLUDE [Azure bot resource](includes/azure-bot-resource/azure-bot-resource.md)]

## Manual app registration

A manual registration is necessary when:

- You're unable to make the registrations in your organization and need another party to create the App ID for the bot you're building.
- You need to manually create your own app ID and password.

## Update the bot

To update your bot's configuration file to include its app ID and password, see [Application ID and password](bot-service-manage-settings.md#bot-identity-information) in how to **Configure bot registration settings**.

## Additional information

See these articles for more information about Azure applications in general.

| Subject | Article |
|:-|:-|
| App registration | [Quickstart: Register an application with the Microsoft identity platform](/azure/active-directory/develop/quickstart-register-app) |
| Managed identities | [What are managed identities for Azure resources?](/azure/active-directory/managed-identities-azure-resources/overview) |
| Single-tenant and multi-tenant apps | [Tenancy in Microsoft Entra ID](/azure/active-directory/develop/single-and-multi-tenant-apps) |

## Next steps

> [!div class="nextstepaction"]
> [Manage a bot](bot-service-manage-overview.md)
