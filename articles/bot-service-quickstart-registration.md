---
title: Register a Bot Framework SDK bot with Azure
description: Learn how to register a bot when you develop and host it in Azure.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 11/19/2021
ms.custom: abs-meta-21q1
monikerRange: 'azure-bot-service-4.0'
---

# Register a bot with Azure

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article shows how to register a bot with the Azure Bot Service when you develop and host it in Azure.

If the bot is hosted elsewhere, you can also make it available in Azure and connect it to the supported channels. You supply the web address where your bot is hosted.

> [!IMPORTANT]
> You only need to register a bot if it is not hosted in Azure.
> Bots created using Azure CLI are already registered with the Azure Bot Service.

This article doesn't describe how to create or deploy the bot to register. For more information, see:

- The [Create a bot](bot-service-quickstart-create-bot.md) quickstart
- The [Deploy a basic bot](bot-builder-deploy-az-cli.md) tutorial

[!INCLUDE [Azure bot resource](includes/azure-bot-resource/azure-bot-resource.md)]

## Manual app registration

A manual registration is necessary when:

- You're unable to make the registrations in your organization and need another party to create the App ID for the bot you're building.
- You need to manually create your own app ID and password.

## Update the bot

To update your bot's configuration file to include its app ID and password, see [Application ID and password](bot-service-manage-settings.md#application-id-and-password) in how to **Configure bot registration settings**.

## Additional information

See these articles for more information about Azure applications in general.

| Subject | Article |
|:-|:-|
| App registration | [Quickstart: Register an application with the Microsoft identity platform](/azure/active-directory/develop/quickstart-register-app) |
| Managed identities | [What are managed identities for Azure resources?](/azure/active-directory/managed-identities-azure-resources/overview) |
| Single-tenant and multi-tenant apps | [Tenancy in Azure Active Directory](/azure/active-directory/develop/single-and-multi-tenant-apps) |

## Next steps

> [!div class="nextstepaction"]
> [Manage a bot](bot-service-manage-overview.md)
