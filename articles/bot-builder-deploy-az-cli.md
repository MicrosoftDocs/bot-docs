---
title: Deploy your bot - Bot Service
description: Learn how to deploy bots to the Azure cloud. See how to prepare bots for deployment, deploy the code to the Azure Web App, and test bots in Web Chat.
keywords: deploy bot, Azure deploy bot, publish bot
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 07/26/2021
monikerRange: 'azure-bot-service-4.0'
---

# Deploy your bot

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article demonstrates how to deploy a basic bot to Azure. It explains how to prepare your bot for deployment, deploy your bot to Azure, and test your bot in Web Chat. Read through this article before following the steps, so that you fully understand what is involved in deploying a bot.

> [!IMPORTANT]
> Use the latest version of the [Azure CLI](/cli/azure/). If you are using an Azure CLI version older than [2.2.0](https://github.com/MicrosoftDocs/azure-docs-cli/blob/master/docs-ref-conceptual/release-notes-azure-cli.md#march-10-2020), you might encounter errors. Also, don't mix the Azure CLI deployment shown in this article with Azure portal deployment.

## Prerequisites

[!INCLUDE [deploy prerequisite](includes/deploy/snippet-prerequisite.md)]

## Prepare for deployment

This article assumes that you have a bot ready to be deployed. For information on how to create a simple echo bot, see [Create a bot with the Bot Framework SDK](bot-service-quickstart-create-bot.md). You can also use one of the samples provided in the [Bot Framework Samples](https://github.com/Microsoft/BotBuilder-Samples/blob/master/README.md) repository.

[!INCLUDE [deploy prepare intro](includes/deploy/snippet-prepare-deploy-intro.md)]

### Log in to Azure

[!INCLUDE [deploy az log in](includes/deploy/snippet-az-login.md)]

### Set the subscription

[!INCLUDE [deploy az subscription](includes/deploy/snippet-az-set-subscription.md)]

<a id="create-app-registration"></a>

### Create an app registration

[!INCLUDE [deploy create app registration](includes/deploy/snippet-create-app-registration.md)]

### Deploy via ARM template

When creating the bot application service, you can deploy your bot in a new or in an existing resource group, both via the [Azure Resource Manager (ARM) template](/azure/azure-resource-manager/templates/overview). An ARM template is a JSON file that declaratively defines one or more Azure resources and that defines dependencies between the deployed resources. Make sure that you have the correct path to your bot project ARM deployment templates directory `DeploymentTemplates`, you need it to assign the value to the template file. Choose the option that works best for you:

* [Deploy via ARM template with new resource group](#deploy-via-arm-template-with-new-resource-group)
* [Deploy via ARM template with existing resource group](#deploy-via-arm-template-with-existing-resource-group)

> [!IMPORTANT]
> Python and Java bots cannot be deployed to a resource group that contains Windows services/bots. Multiple Python bots can be deployed to the same resource group, but you need to create other services (LUIS, QnA, etc.) in another resource group.

### Deploy via ARM template with new resource group

<!-- ##### Create Azure resources -->
[!INCLUDE [ARM with new resource group](includes/deploy/snippet-ARM-new-resource-group.md)]

### Deploy via ARM template with existing resource group

[!INCLUDE [ARM with existing resource group](includes/deploy/snippet-ARM-existing-resource-group.md)]

## Prepare your code for deployment

### Assign app ID and password

[!INCLUDE [assign app ID and password](includes/deploy/snippet-assign-appid-password.md)]

### Retrieve or create necessary IIS/Kudu files

[!INCLUDE [prepare project](includes/deploy/snippet-IIS-Kudu-files.md)]

### Zip up the code directory manually

[!INCLUDE [package project](includes/deploy/snippet-zip-code.md)]

## Deploy bot to Azure

[!INCLUDE [deploy code to Azure](includes/deploy/snippet-deploy-code-to-az.md)]

## Test in Web Chat

[!INCLUDE [test in web chat](includes/deploy/snippet-test-in-web-chat.md)]

## Additional information

Deploying your bot to Azure will involve paying for the services you use. The [billing and cost management](/azure/billing/) article helps you understand Azure billing, monitor usage and costs, and manage your account and subscriptions.
See also [Azure Command-Line Interface (CLI) documentation](/cli/azure/) and [Azure CLI release notes](/cli/azure/release-notes-azure-cli).

## Next steps

> [!div class="nextstepaction"]
> [Set up continuous deployment](bot-service-build-continuous-deployment.md)

<!-- ## Appendix

[!INCLUDE [deploy csharp bot to Azure](includes/deploy/snippet-deploy-simple-csharp-echo-bot.md)] -->
