---
title: Deploy your bot - Bot Service
description: Learn how to deploy bots to the Azure cloud. See how to prepare bots for deployment, deploy the code to the Azure Web App, and test bots in Web Chat.
keywords: deploy bot, azure deploy bot, publish bot
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 06/09/2020
monikerRange: 'azure-bot-service-4.0'
---

# Deploy your bot

[!INCLUDE [applies-to](./includes/applies-to.md)]

In this article we will show you how to deploy a basic bot to Azure. We will explain how to prepare your bot for deployment, deploy your bot to Azure, and test your bot in Web Chat. It would be useful to read this article before following the steps, so that you fully understand what is involved in deploying a bot.

> [!IMPORTANT]
> Make sure you are using the latest version of the [Azure CLI](https://docs.microsoft.com/cli/azure/?view=azure-cli-latest). If you are using an Azure CLI version older than [2.2.0](https://github.com/MicrosoftDocs/azure-docs-cli/blob/master/docs-ref-conceptual/release-notes-azure-cli.md#march-10-2020), you will encounter errors of CLI commands deprecation. Also, do not mix Azure CLI deployment shown in this article with Azure portal deployment.

## Prerequisites

[!INCLUDE [deploy prerequisite](~/includes/deploy/snippet-prerequisite.md)]

## Prepare for deployment

[!INCLUDE [deploy prepare intro](~/includes/deploy/snippet-prepare-deploy-intro.md)]

### 1. Login to Azure

[!INCLUDE [deploy az login](~/includes/deploy/snippet-az-login.md)]

### 2. Set the subscription

[!INCLUDE [deploy az subscription](~/includes/deploy/snippet-az-set-subscription.md)]

### 3. Create the application registration

[!INCLUDE [deploy create app registration](~/includes/deploy/snippet-create-app-registration.md)]

### 4. Create the bot application service

When creating the bot application service, you can deploy your bot in a new or in an existing resource group, both via the [Azure Resource Manager (ARM) template](https://docs.microsoft.com/azure/azure-resource-manager/templates/overview). An ARM template is a JSON file that declaratively defines one or more Azure resources and that defines dependencies between the deployed resources. Make sure that you have the correct path to your bot project ARM deployment templates directory `DeploymentTemplates`, you need it to assign the value to the template file. Choose the option that works best for you:

* [Deploy via ARM template with new resource group](#deploy-via-arm-template-with-new-resource-group)
* [Deploy via ARM template with existing resource group](#deploy-via-arm-template-with-existing-resource-group)

> [!IMPORTANT]
> Python bots cannot be deployed to a resource group that contains Windows services/bots.  Multiple Python bots can be deployed to the same resource group, but create other services (LUIS, QnA, etc.) in another resource group.

#### **Deploy via ARM template (with **new** Resource Group)**

<!-- ##### Create Azure resources -->
[!INCLUDE [ARM with new resource group](~/includes/deploy/snippet-ARM-new-resource-group.md)]


#### **Deploy via ARM template (with **existing** Resource Group)**

[!INCLUDE [ARM with existing resource group](~/includes/deploy/snippet-ARM-existing-resource-group.md)]


### 5. Prepare your code for deployment

#### 5.1 Retrieve or create necessary IIS/Kudu files

[!INCLUDE [retrieve or create IIS/Kudu files](~/includes/deploy/snippet-IIS-Kudu-files.md)]

#### 5.2 Zip up the code directory manually

[!INCLUDE [zip up code](~/includes/deploy/snippet-zip-code.md)]

## Deploy code to Azure

[!INCLUDE [deploy code to Azure](~/includes/deploy/snippet-deploy-code-to-az.md)]

## Test in Web Chat

[!INCLUDE [test in web chat](~/includes/deploy/snippet-test-in-web-chat.md)]

## Additional information

Deploying your bot to Azure will involve paying for the services you use. The [billing and cost management](https://docs.microsoft.com/azure/billing/) article helps you understand Azure billing, monitor usage and costs, and manage your account and subscriptions.

## Next steps

> [!div class="nextstepaction"]
> [Set up continuous deployment](bot-service-build-continuous-deployment.md)

<!-- ## Appendix

[!INCLUDE [deploy csharp bot to Azure](~/includes/deploy/snippet-deploy-simple-csharp-echo-bot.md)] -->
