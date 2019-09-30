---
title: Tutorial to create and deploy a basic bot | Microsoft Docs
description: Learn how to create a basic bot and then deploy it to Azure.
keywords: echo bot, deploy, azure, tutorial
author: Ivorb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/23/2019
monikerRange: 'azure-bot-service-4.0'
---

# Tutorial: Create and deploy a basic bot

[!INCLUDE [applies-to-v4](../includes/applies-to.md)]

This tutorial walks you through creating a basic bot with the Bot Framework SDK and deploying it to Azure. If you've already created a basic bot and have it running locally, skip ahead to the [Deploy your bot](#deploy-your-bot) section.

In this tutorial, you learn how to:

> [!div class="checklist"]
> * Create a basic Echo bot
> * Run and interact with it locally
> * Publish your bot

If you don’t have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

# [C#](#tab/csharp)

[!INCLUDE [dotnet quickstart](~/includes/quickstart-dotnet.md)]

# [JavaScript](#tab/javascript)

[!INCLUDE [javascript quickstart](~/includes/quickstart-javascript.md)]

---

## Deploy your bot

### Prerequisites
[!INCLUDE [deploy prerequisite](~/includes/deploy/snippet-prerequisite.md)]

### Prepare for deployment
[!INCLUDE [deploy prepare intro](~/includes/deploy/snippet-prepare-deploy-intro.md)]

#### 1. Login to Azure
[!INCLUDE [deploy az login](~/includes/deploy/snippet-az-login.md)]

#### 2. Set the subscription
[!INCLUDE [deploy az subscription](~/includes/deploy/snippet-az-set-subscription.md)]

#### 3. Create an App registration
[!INCLUDE [deploy create app registration](~/includes/deploy/snippet-create-app-registration.md)]

#### 4. Deploy via ARM template
You can deploy your bot in a new resource group or an existing resource group. Choose the option that works best for you. 
##### **Deploy via ARM template with new Resource Group**
[!INCLUDE [ARM with new resourece group](~/includes/deploy/snippet-ARM-new-resource-group.md)]

##### **Deploy via ARM template with existing Resource Group**
[!INCLUDE [ARM with existing resourece group](~/includes/deploy/snippet-ARM-existing-resource-group.md)]

#### 5. Prepare your code for deployment
##### **Retrieve or create necessary IIS/Kudu files**
[!INCLUDE [retrieve or create IIS/Kudu files](~/includes/deploy/snippet-IIS-Kudu-files.md)]

##### **Zip up the code directory manually**
[!INCLUDE [zip up code](~/includes/deploy/snippet-zip-code.md)]

### Deploy code to Azure
[!INCLUDE [deploy code to Azure](~/includes/deploy/snippet-deploy-code-to-az.md)]

### Test in Web Chat
[!INCLUDE [test in web chat](~/includes/deploy/snippet-test-in-web-chat.md)]

## Additional resources

[!INCLUDE [additional resources snippet](~/includes/deploy/snippet-additional-resources.md)]

## Next steps
> [!div class="nextstepaction"]
> [Use QnA Maker in your bot to answer questions](bot-builder-tutorial-add-qna.md)