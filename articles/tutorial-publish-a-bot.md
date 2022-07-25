---
title: "Tutorial: Deploy a basic bot using Azure Bot Service"
description: Learn how to deploy a bot to Azure and test it in Web Chat.
keywords: echo bot, deploy, azure, tutorial
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: how-to
ms.date: 03/03/2022
---

# Tutorial: Publish a basic bot

[!INCLUDE [applies-to-v4](./includes/applies-to-v4-current.md)]

This tutorial covers how to publish a bot to Azure.
This process is also called _deploy your bot to Azure_.
Once your bot is published, you can test it in Azure portal with Web Chat.

You'll learn how to:

> [!div class="checklist"]
> - Sign in to Azure and select a subscription.
> - Add to your project files required for publishing.
> - Compress and deploy your code to Azure.
> - Test your bot in Web Chat in the Azure portal.

## Prerequisites

A bot provisioned in Azure. If you don't have one yet, see [Tutorial: Provision a bot in Azure](./tutorial-provision-a-bot.md).

[!INCLUDE [Azure CLI prerequisites](./includes/az-cli/prereqs.md)]

For Java bots, install [Maven](https://maven.apache.org/).

## Sign in to Azure and select a subscription

If your previous session expired, sign into Azure again and choose the same subscription you used to provision your bot resources.

[!INCLUDE [Sign into Azure and select a subscription](./includes/az-cli/sign-in-select-subscription.md)]

## Prepare your project files

[!INCLUDE [Create Kudu files, compress files](./includes/az-cli/prepare-for-deployment.md)]

## Publish your bot to Azure

[!INCLUDE [deploy code to Azure](./includes/az-cli/deploy-to-azure.md)]

## Test in Web Chat

[!INCLUDE [test in web chat](./includes/deploy/snippet-test-in-web-chat.md)]

## Additional resources

[!INCLUDE [Azure documentation for bot hosting](./includes/azure-docs/apps-and-resources.md)]

### Kudu files

The web app deployment command uses Kudu to deploy C#, JavaScript, and Python bots.
When using the non-configured [zip deploy API](https://github.com/projectkudu/kudu/wiki/Deploying-from-a-zip-file-or-url) to deploy your bot's code, the behavior is as follows:

_Kudu assumes by default that deployments from .zip files are ready to run and don't require extra build steps during deployment, such as npm install or dotnet restore/dotnet publish._

It's important to include your built code with all necessary dependencies in the zip file being deployed; otherwise, your bot will not work as intended. For more information, see the Azure documentation on how to [Deploy files to App Service](/azure/app-service/deploy-zip).

## Clean up resources

If you're not going to use this application again, delete the associated resources with the following steps:

1. In the Azure portal, open the resource group for your bot.
    1. Select **Delete resource group** to delete the group and all the resources it contains.
    1. Enter the _resource group name_ in the confirmation pane, then select **Delete**.
1. If you created a single-tenant or multi-tenant app:
    1. Go to the Azure Active Directory blade.
    1. Locate the app registration you used for your bot, and delete it.

## Next steps

> [!div class="nextstepaction"]
> [Use QnA Maker in your bot to answer questions](./v4sdk/bot-builder-tutorial-add-qna.md)
