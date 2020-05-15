---
title: Configure continuous deployment for Bot Service - Bot Service
description: Learn how to setup continuous deployment from source control for a Bot Service. 
keywords: continuous deployment, publish, deploy, azure portal
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 03/23/2020
monikerRange: 'azure-bot-service-4.0'
---

# Set up continuous deployment

[!INCLUDE [applies-to](./includes/applies-to.md)]

This article shows you how to configure continuous deployment for your bot. You can enable continuous deployment to automatically deploy code changes from your source repository to Azure. In this topic, we'll cover setting up continuous deployment for GitHub. For information on setting up continuous deployment with other source control systems, see the additional resource section at the bottom of this page.

## Prerequisites

- If you don't have an Azure subscription, create a [free account](https://portal.azure.com) before you begin.
- You **must** [deploy your bot to Azure](bot-builder-deploy-az-cli.md) before enabling continuous deployment.

## Prepare your repository

Make sure that your repository root has the correct files in your project. This will allow you to get automatic builds from the build provider.

|Runtime | Root directory files |
|:-------|:---------------------|
| ASP.NET Core | .sln or .csproj |
| Node.js | server.js, app.js, or package.json with a start script |
| Python | app.py |

## Continuous deployment using GitHub

To enable continuous deployment with GitHub, navigate to the **App Service** page for your bot in the Azure portal.

1. Click **Deployment Center** > **GitHub** > **Authorize**.

   ![Continous deployment](~/media/azure-bot-build/azure-deployment.png)

   1. In the browser window that opens up, click **Authorize AzureAppService**.

      ![Azure Github Permission](~/media/azure-bot-build/azure-deployment-github.png)

   1. After authorizing the **AzureAppService**, go back to **Deployment Center** in the Azure portal.

1. Click **Continue**.

      > [!div class="mx-imgBorder"]
      > ![Continue to build provider](~/media/azure-bot-build/azure-deployment-continue.png)

1. On the **Build provider** page, select the build provider you want to use and click **Continue**.

    > [!IMPORTANT]
    > With the release of the Bot Framework 4.8 SDK, the .NET Bot Framework samples now target the .NET Core 3.1 SDK.
    > Not all Azure data centers are configured to build such bots.
    >
    > See the map of [.NET Core on App Service](https://aspnetcoreon.azurewebsites.net/) for the centers in which you can build .NET Core 3.1 apps using Kudu. (All centers can run .NET Core 3.1 apps.)
    >
    > If you you are deploying a bot that targets the .NET Core 3.1 SDK and you are deploying to a center that can't build .NET Core 3.1 apps using Kudu, use either **GitHub Actions (Preview)** or **Azure Pipelines (Preview)** for your build provider.

    > [!div class="mx-imgBorder"]
    > ![Select build provider](~/media/azure-bot-build/azure-deployment-build-provider.png)

1. On the **Configure** page, enter the required information and click **Continue**. The information required will depend on which source control service and build provider you chose.

1. On the **Summary** page, review the settings and then click **Finish**.

At this point, continuous deployment with GitHub is set up. New commits in the selected repository and branch now deploy continuously into your App Service app. You can track the commits and deployments on the **Deployment Center** page.

## Disable continuous deployment

While your bot is configured for continuous deployment, you may not use the online code editor to make changes to your bot. If you want to use the online code editor, you can temporarily disable continuous deployment.

To disable continuous deployment, do the following:

1. In the [Azure portal](https://portal.azure.com), go to your bot's **All App Service settings** blade and click **Deployment Center**.
1. Click **Disconnect** to disable continuous deployment. To re-enable continuous deployment, repeat the steps from the appropriate sections above.

## Additional resources

- For more information about continuous deployment in Azure, see  [continuous deployment to Azure App Service](https://docs.microsoft.com/azure/app-service/deploy-continuous-deployment).
- When you use GitHub actions for the build provider, a workflow is created in your repository. You can learn more about using [GitHub Actions](https://help.github.com/en/actions) on the GitHub site.
