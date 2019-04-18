---
title: Tutorial to create and deploy a basic bot | Microsoft Docs
description: Learn how to create a basic bot and then deploy it to Azure.
keywords: echo bot, deploy, azure, tutorial
author: Ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 04/18/2019
monikerRange: 'azure-bot-service-4.0'
---

# Tutorial: Create and deploy a basic bot

[!INCLUDE [applies-to-v4](../includes/applies-to.md)]

This tutorial walks you through creating a basic bot with the Bot Framework SDK, and deploying it to Azure. If you've already created a basic bot and have it running locally, skip ahead to the [Deploy your bot](#deploy-your-bot) section.

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

Now, deploy your bot to Azure.

### Prerequisites

[!INCLUDE [prerequisite snippet](~/includes/deploy/snippet-prerequisite.md)]

### Login to Azure CLI and set your subscription

You've already created and tested a bot locally, and now you want to deploy it to Azure.

[!INCLUDE [az login snippet](~/includes/deploy/snippet-az-login.md)]

### Create a Web App Bot

If you don't already have a resource group to which to publish your bot, create one:

[!INCLUDE [az create group snippet](~/includes/deploy/snippet-az-create-group.md)]

[!INCLUDE [az create web app snippet](~/includes/deploy/snippet-create-web-app.md)]

Before proceeding, read the instructions that apply to you based on the type of email account you use to log in to Azure.

#### MSA email account

If you are using an [MSA](https://en.wikipedia.org/wiki/Microsoft_account) email account, you will need to create the app ID and app password on the Application Registration Portal to use with `az bot create` command.

[!INCLUDE [create bot msa snippet](~/includes/deploy/snippet-create-bot-msa.md)]

#### Business or school account

[!INCLUDE [create bot snippet](~/includes/deploy/snippet-create-bot.md)]

### Download the bot from Azure

Next, download the bot you just created. 
[!INCLUDE [download bot snippet](~/includes/deploy/snippet-download-bot.md)]

[!INCLUDE [download keys snippet](~/includes/snippet-abs-key-download.md)]

### Decrypt the downloaded .bot file and use in your project

The sensitive information in the .bot file is encrypted, so let's decrypt it so we can use it easily. 

First, navigate to the dowloaded bot directory.

[!INCLUDE [decrypt bot snippet](~/includes/deploy/snippet-decrypt-bot.md)]

### Test your bot locally

At this point, your bot should work the same way it did with the old `.bot` file. Make sure that it works as expected with the new `.bot` file.

You should now see a *Production* endpoint in the emulator. If you don't, you're probably still using the old `.bot` file.

### Publish your bot to Azure

<!-- TODO: re-encrypt your .bot file? -->

[!INCLUDE [publish snippet](~/includes/deploy/snippet-publish.md)]

<!-- TODO: If we tell them to re-encrypt, this step is not necessary. -->

[!INCLUDE [clear encryption snippet](~/includes/deploy/snippet-clear-encryption.md)]

Now, you should be able to test your bot in Webchat.

## Additional resources

[!INCLUDE [additional resources snippet](~/includes/deploy/snippet-additional-resources.md)]

## Next steps
> [!div class="nextstepaction"]
> [Add services to your bot](bot-builder-tutorial-add-qna.md)

