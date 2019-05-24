---
title: Configure continuous deployment for Bot Service | Microsoft Docs
description: Learn how to setup continuous deployment from source control for a Bot Service. 
keywords: continuous deployment, publish, deploy, azure portal
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/23/2019
monikerRange: 'azure-bot-service-4.0'
---

# Set up continuous deployment

[!INCLUDE [applies-to](./includes/applies-to.md)]

This article shows you how to configure continuous deployment for your bot. You can enable continuous deployment to automatically deploy code changes from your source repository to Azure. In this topic, we'll cover setting up continuous deployment for GitHub. For information on setting up continous deployment with other source control systems, see the additional resource section at the bottom of this page.

## Prerequisites
- If you don't have an Azure subscription, create a [free account](http://portal.azure.com) before you begin.
- You **must** [deploy your bot to Azure](bot-builder-deploy-az-cli.md) before enabling continous deployment.

## Prepare your repository
Make sure that your repository root has the correct files in your project. This will allow you to get automatic builds from the Azure App Service Kudu build server. 

|Runtime | Root directory files |
|:-------|:---------------------|
| ASP.NET Core | .sln or .csproj |
| Node.js | server.js, app.js, or package.json with a start script |


## Continuous deployment using GitHub
To enable continuous deployment with GitHub, navigate to the **App Service** page for your bot in the Azure portal.

Click **Deployment Center** > **GitHub** > **Authorize**.

![Continous deployment](~/media/azure-bot-build/azure-deployment.png)

In the browser window that opens up, click **Authorize AzureAppService**. 

![Azure Github Permission](~/media/azure-bot-build/azure-deployment-github.png)

After authorizing the **AzureAppService**, go back to **Deployment Center** in the Azure portal.

1. Click **Continue**. 

1. Select **App Service build service**.

1. Click **Continue**.

1. Select **Organization**, **Repository**, and **Branch**.

1. Click **Continue**, and then **Finish** to complete the setup.

At this point, continuous deployment with GitHub is set up. Whenever you commit to the source code repository, your changes will automatically be deployed to the Azure Bot Service.

## Disable continuous deployment

While your bot is configured for continuous deployment, you may not use the online code editor to make changes to your bot. If you want to use the online code editor, you can temporarily disable continuous deployment.

To disable continuous deployment, do the following:
1. In the [Azure portal](https://portal.azure.com), go to your bot's **All App Service settings** blade and click **Deployment Center**. 
1. Click **Disconnect** to disable continuous deployment. To re-enable continuous deployment, repeat the steps from the appropriate sections above.

## Additional resources
- To enable continuous deployment from BitBucket and Azure DevOps Services, see  [continous deployment using Azure App Service](https://docs.microsoft.com/en-us/azure/app-service/deploy-continuous-deployment).


