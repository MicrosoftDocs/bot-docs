---
title: Publish your bot to Azure
description: Learn how to deploy bots to the Azure cloud. See how to prepare bots for deployment, deploy the code to the Azure Web App, and test bots in Web Chat.
keywords: deploy bot, Azure deploy bot, provision bot, publish bot
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 03/03/2022
ms.custom: abs-meta-21q1, devx-track-azurecli
monikerRange: 'azure-bot-service-4.0'
ms.devlang: azurecli
---

# Publish your bot to Azure

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article demonstrates how to deploy a basic bot to Azure. It explains how to create resources for your bot, prepare your bot for deployment, deploy your bot to Azure, and test your bot in Web Chat.

This article assumes that you have a bot ready to be deployed. For information on how to create a simple echo bot, see [Create a bot with the Bot Framework SDK](bot-service-quickstart-create-bot.md). You can also use one of the samples provided in the [Bot Framework Samples](https://github.com/Microsoft/BotBuilder-Samples/blob/master/README.md) repository.

## Prerequisites

[!INCLUDE [Azure CLI prerequisites](./includes/az-cli/prereqs.md)]

Starting with Bot Framework SDK 4.14.1.2, when you create a bot from a VSIX or Yeoman template, the resulting project contains ARM templates.
The samples and any new bots created from a Bot Framework template contain a _deployment templates_ directory that contains the ARM templates.
You'll use the ARM templates to provision many of your bot resources.

For Java bots, install [Maven](https://maven.apache.org/).

## Sign in to Azure and select a subscription

[!INCLUDE [Sign into Azure and select a subscription](./includes/az-cli/sign-in-select-subscription.md)]

<a id="create-app-registration"></a>

## Create an identity resource

[!INCLUDE [Create a user-assigned managed identity or an Azure AD app registration](./includes/az-cli/create-identity-resource.md)]

## Create resources with an ARM template

You'll use an Azure Resource Manager (ARM) template to create an app service and Azure Bot resource.

Your bot project's _deployment templates_ directory contains two ARM templates.

- One creates resources in a _new_ resource group, with a new app service plan.
- One creates resources in an _existing_ resource group, with a new or existing app service plan.

Choose the option that works best for you.
This step can take a few minutes to complete.

[!INCLUDE [bot-resource-type-tip](includes/bot-resource-type-tip.md)]

### [New resource group](#tab/newgroup)

[!INCLUDE [ARM with new resource group](./includes/az-cli/arm-provision-to-new-rg.md)]

### [Existing resource group](#tab/existinggroup)

[!INCLUDE [ARM with existing resource group](./includes/az-cli/arm-provision-to-existing-rg.md)]

---

## Update project configuration settings

[!INCLUDE [app ID and password](./includes/authentication/azure-bot-appid-password.md)]

## Prepare your project files

[!INCLUDE [retrieve or create IIS/Kudu files](./includes/az-cli/prepare-for-deployment.md)]

## Publish your bot

[!INCLUDE [deploy code to Azure](./includes/az-cli/deploy-to-azure.md)]

## Test in Web Chat

[!INCLUDE [test in web chat](includes/deploy/snippet-test-in-web-chat.md)]

## Additional information

### Resource locations

Some of the commands require a location or region parameter.

- Use `az account list-locations` to list supported regions for the current subscription.
- Use `az config set defaults.location=<location>` to set the default location to use for all `az` commands.

To create bots with data residency in Europe or the US, use "westeurope" or "westus2", respectively, for the location for the resource group and all bot resources.
For more information about data residency, see [Answering Europe's Call: Storing and Processing EU Data in the EU](https://blogs.microsoft.com/eupolicy/2021/05/06/eu-data-boundary/).

### Azure documentation

[!INCLUDE [Azure documentation for bot hosting](./includes/azure-docs/apps-and-resources.md)]

## Clean up resources

If you're not going to continue to use this application, delete the associated resources with the following steps:

1. In the Azure portal, open the resource group for your bot.
    1. Select **Delete resource group** to delete the group and all the resources it contains.
    1. Enter the _resource group name_ in the confirmation pane, then select **Delete**.
1. If you created a single-tenant or multi-tenant app:
    1. Go to the Azure Active Directory blade.
    1. Locate the app registration you used for your bot, and delete it.

## Next steps

> [!div class="nextstepaction"]
> [Set up continuous deployment](bot-service-build-continuous-deployment.md)
