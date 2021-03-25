---
title: Tutorial to deploy a basic bot - Bot Service
description: Learn how to deploy bots to Azure. See the steps to prepare for deployment, deploy, and test bots.
keywords: echo bot, deploy, azure, tutorial
author: mmiele
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/02/2020
---

# Tutorial: Deploy a basic bot

[!INCLUDE [applies-to-v4](~/includes/applies-to-v4-current.md)]

This tutorial describes how to deploy a basic bot to Azure. It explains how to prepare your bot for deployment, deploy your bot to Azure, and test your bot using Web Chat.
It would be useful if you read this article before following the steps, so that you fully understand what is involved in deploying a bot.

- If you've not created a basic bot yet, read the [Tutorial: Create  a basic bot](bot-builder-tutorial-create-basic-bot.md) article.
- If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

You will learn how to:
> [!div class="checklist"]
> * Prepare a basic bot for deployment
> * Deploy the bot to Azure
> * Test it using Web Chat

> [!IMPORTANT]
> Make sure you are using the latest version of the [Azure CLI](/cli/azure/). If you are using an Azure CLI version older than [2.2.0](https://github.com/MicrosoftDocs/azure-docs-cli/blob/master/docs-ref-conceptual/release-notes-azure-cli.md#march-10-2020), you will encounter errors of CLI commands deprecation. Also, do not mix Azure CLI deployment shown in this article with Azure portal deployment.

## Prerequisites

[!INCLUDE [deploy prerequisites](../includes/deploy/snippet-prerequisite.md)]

## Prepare for deployment

This article assumes that you have a bot ready to be deployed. For information on how to create a simple echo bot, see the [Tutorial: Create a basic bot](bot-builder-tutorial-create-basic-bot.md) article. You can also use one of the samples provided in the [Bot Framework Samples](https://github.com/Microsoft/BotBuilder-Samples/blob/master/README.md) repository.

[!INCLUDE [deploy prepare intro](../includes/deploy/snippet-prepare-deploy-intro.md)]

### 1. Login to Azure

[!INCLUDE [deploy az login](../includes/deploy/snippet-az-login.md)]

### 2. Set the subscription

[!INCLUDE [deploy az subscription](../includes/deploy/snippet-az-set-subscription.md)]

### 3. Create an App registration

[!INCLUDE [deploy create app registration](../includes/deploy/snippet-create-app-registration.md)]

### 4. Deploy via ARM template

When creating the bot application service, you can deploy your bot in a new or in an existing resource group, both via the [Azure Resource Manager (ARM) template](/azure/azure-resource-manager/templates/overview). An ARM template is a JSON file that declaratively defines one or more Azure resources and that defines dependencies between the deployed resources. Make sure that you have the correct path to your bot project ARM deployment templates directory **DeploymentTemplates**. You need it to assign the value to the template file. Choose the option that works best for you:

- [Deploy via ARM template with new resource group](#deploy-via-arm-template-with-new-resource-group)
- [Deploy via ARM template with existing resource group](#deploy-via-arm-template-with-existing-resource-group)

> [!IMPORTANT]
> Python bots can't be deployed to a resource group that contains Windows services/bots. Multiple Python bots can be deployed to the same resource group, but you need to create other services (LUIS, QnA, and so on) in another resource group.

##### **Deploy via ARM template with new Resource Group**

[!INCLUDE [ARM with new resource group](../includes/deploy/snippet-ARM-new-resource-group.md)]

##### **Deploy via ARM template with existing Resource Group**

[!INCLUDE [ARM with existing resource group](../includes/deploy/snippet-ARM-existing-resource-group.md)]

### 5. Prepare your code for deployment

#### **Retrieve or create necessary IIS/Kudu files**

[!INCLUDE [retrieve or create IIS/Kudu files](../includes/deploy/snippet-IIS-Kudu-files.md)]

#### **Zip up the code directory manually**

[!INCLUDE [zip up code](../includes/deploy/snippet-zip-code.md)]

## Deploy bot to Azure

[!INCLUDE [deploy code to Azure](../includes/deploy/snippet-deploy-code-to-az.md)]

## Test in Web Chat

[!INCLUDE [test in web chat](../includes/deploy/snippet-test-in-web-chat.md)]

## Additional resources

[!INCLUDE [additional resources snippet](../includes/deploy/snippet-additional-resources.md)]

## Next steps

> [!div class="nextstepaction"]
> [Use QnA Maker in your bot to answer questions](bot-builder-tutorial-add-qna.md)
