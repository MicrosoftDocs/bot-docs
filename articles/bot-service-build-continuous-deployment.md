---
title: Configure continuous deployment for Bot Service - Bot Service
description: Learn how to set up continuous deployment from source control for a Bot Service.
keywords: continuous deployment, publish, deploy, azure portal
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 11/01/2021
monikerRange: 'azure-bot-service-4.0'
---

# Set up continuous deployment

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to configure continuous deployment for your bot. You can enable continuous deployment to automatically deploy code changes from your source repository to Azure.

This article covers setting up continuous deployment for GitHub. For information on setting up continuous deployment with other source control systems, see [Additional resources](#additional-resources).

## Prerequisites

- If you don't have an Azure subscription, create a [free account](https://portal.azure.com) before you begin.
- Before setting up continuous deployment, [Deploy your bot to Azure](bot-builder-deploy-az-cli.md) _at least once_.

## Prepare your repository

Make sure that your repository root has the correct files in your project. This will allow you to get automatic builds from the build provider.

| Runtime      | Root directory files                                   |
|:-------------|:-------------------------------------------------------|
| ASP.NET Core | .sln or .csproj                                        |
| Node.js      | server.js, app.js, or package.json with a start script |
| Java         | pom.xml                                                |
| Python       | app.py                                                 |

## Continuous deployment using GitHub

To enable continuous deployment with GitHub, navigate to the **App Service** page for your bot in the Azure portal.

1. Select **Deployment Center** > **GitHub** > **Authorize**.

    :::image type="content" source="media/azure-bot-build/azure-deployment.png" alt-text="Continuous deployment":::

    1. In the browser window that opens up, select **Authorize AzureAppService**.

        :::image type="content" source="media/azure-bot-build/azure-deployment-github.png" alt-text="Azure GitHub permission":::

    1. After authorizing the **AzureAppService**, go back to **Deployment Center** in the Azure portal.

1. Select **Continue**.

    :::image type="content" source="media/azure-bot-build/azure-deployment-continue.png" alt-text="Continue to build provider":::

1. On the **Build provider** page, select the build provider you want to use and select **Continue**.

1. On the **Configure** page, enter the required information and select **Continue**. The information required will depend on which source control service and build provider you chose.

1. On the **Summary** page, review the settings and then select **Finish**.

At this point, continuous deployment with GitHub is set up. New commits in the selected repository and branch now deploy continuously into your App Service app. You can track the commits and deployments on the **Deployment Center** page.

## Disable continuous deployment

While your bot is configured for continuous deployment, you may not use the online code editor to make changes to your bot. If you want to use the online code editor, you can temporarily disable continuous deployment.

To disable continuous deployment, do the following:

1. In the [Azure portal](https://portal.azure.com), go to your bot's **All App Service settings** blade and select **Deployment Center**.
1. Select **Disconnect** to disable continuous deployment. To re-enable continuous deployment, repeat the steps from the appropriate sections above.

## Additional resources

- For more information about continuous deployment in Azure, see  [continuous deployment to Azure App Service](/azure/app-service/deploy-continuous-deployment).
- When you use GitHub actions for the build provider, a workflow is created in your repository. You can learn more about using [GitHub Actions](https://help.github.com/en/actions) on the GitHub site.
