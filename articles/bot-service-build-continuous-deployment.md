---
title: Configure continuous deployment
description: Learn how to set up continuous deployment from source control for a Bot Service.
keywords: continuous deployment, publish, deploy, azure portal
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 10/26/2022
monikerRange: 'azure-bot-service-4.0'
---

# Set up continuous deployment

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to configure continuous deployment. You can enable continuous deployment to automatically deploy code changes from your source repository to Azure.

This article covers setting up continuous deployment for GitHub. For information on setting up continuous deployment with other source control systems, see  [continuous deployment to Azure App Service](/azure/app-service/deploy-continuous-deployment).

[!INCLUDE [java-python-sunset-alert](includes/java-python-sunset-alert.md)]

## Prerequisites

- If you don't have an Azure subscription, create a [free account](https://portal.azure.com) before you begin.
- Before setting up continuous deployment, [Deploy your bot to Azure](bot-builder-deploy-az-cli.md) _at least once_.
- A GitHub account and a repository to use for your bot.

## Prepare your GitHub repository

Add your bot project to your GitHub repository.

> [!IMPORTANT]
> To enable automatic builds from the build provider, your _repository root_ must contain specific files for your project.
>
> | Runtime      | Root directory files                                               |
> |:-------------|:-------------------------------------------------------------------|
> | ASP.NET Core | **.sln** or **.csproj**                                            |
> | Node.js      | **server.js**, **app.js**, or **package.json** with a start script |
> | Java         | **pom.xml**                                                        |
> | Python       | **app.py**                                                         |

## Set up continuous deployment with GitHub

1. Go to the [Azure portal](https://portal.azure.com/).
1. Open the **App Service** blade for your bot.
1. Under **Deployment**, select **Deployment Center** to open the **Deployment Center** blade.
1. Select the **Settings** tab.
   1. For **Source**, select **GitHub**.
   1. Change the build provider:
      1. Select **Change provider**.
      1. Select **App Service Build Service**, then **OK**.

   1. If you haven't connected to GitHub from Azure before, select **Authorize** to authorize Azure App Service to access your GitHub account.
   1. Check that the **Signed in as** field shows your correct GitHub account.

       To sign into and authorize a different account, select **Change account**.

   1. For **Organization**, **Repository**, and **Branch**, select the GitHub organization, repository, and branch that contains your bot project.
   1. Select **Save**.

At this point, continuous deployment with GitHub is set up. New commits in the selected repository and branch now deploy continuously into your App Service app. You can track the commits and deployments on the **Logs** tab.

:::image type="content" source="media/bot-service-build-continuous-deployment/cicd-configured.png" alt-text="Screenshot of the Deployment Center blade, with the source and build provider configured.":::

## Disable continuous deployment

While your bot is configured for continuous deployment, you may not use the online code editor to make changes to your bot. If you want to use the online code editor, you can temporarily disable continuous deployment.

To disable continuous deployment:

1. Go to the [Azure portal](https://portal.azure.com/).
1. Open the **App Service** blade for your bot.
1. Under **Deployment**, select **Deployment Center** to open the **Deployment Center** blade.
1. Select the **Settings** tab.
1. Select **Disconnect** to disable continuous deployment.

To re-enable continuous deployment, repeat the steps from [Set up continuous deployment with GitHub](#set-up-continuous-deployment-with-github).
