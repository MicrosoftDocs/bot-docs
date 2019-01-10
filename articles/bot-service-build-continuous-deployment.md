---
title: Configure continuous deployment for Bot Service | Microsoft Docs
description: Learn how to setup continuous deployment from source control for a Bot Service. 
keywords: continuous deployment, publish, deploy, azure portal
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/06/2018
---

# Set up continuous deployment
If your code is checked into **GitHub** or **Azure DevOps (formerly Visual Studio Team Services)**, use continous deployment to automatically deploy code changes from your source repository to Azure. In this topic, we'll cover setting up continuous deployment for **GitHub** and **Azure DevOps**.

> [!NOTE]
> The scenario covered in this article assumes that you have deployed your bot to Azure, and now you want to enable continous deployment for that bot. Also, know that after continuous deployment is set up, the online code editor in the Azure portal becomes read-only.

## Continuous deployment using GitHub

To set up continuous deployment using GitHub repository that contains the source code you want to deploy to Azure, do the following:

1. In the [Azure portal](https://portal.azure.com), go to your bot's **All App service settings** blade and click **Deployment options (Classic)**. 

1. Click **Choose Source** and select **GitHub**.

   ![Choose GitHub](~/media/azure-bot-build/continuous-deployment-setup-github.png)

1. Click **Authorization** then click the **Authorize** button and follow the prompts to give Azure authorization to access your GitHub account.

1. Click **Choose project** and select a project.

1. Click **Choose branch** and select a branch.

1. Click **OK** to complete the setup process.

Now your continuous deployment with GitHub setup is complete. Whenever you commit to the source code repository, your changes will automatically be deployed to the Azure Bot Service.

## Continuous deployment using Azure DevOps

1. In the [Azure portal](https://portal.azure.com), go to your bot's **All App service settings** blade and click **Deployment options (Classic)**. 
2. Click **Choose Source** and select **Visual Studio Team Services**. Please keep in mind that Visual Studio Team Services is now Azure DevOps Services.

   ![Choose Visual Studio Team Services](~/media/azure-bot-build/continuous-deployment-setup-vs.png)

3. Click **Choose your account** and select an account.

> [!NOTE]
> If you do not see your account listed, you'll need to [link your account to your Azure subscription](https://docs.microsoft.com/en-us/azure/devops/organizations/accounts/connect-organization-to-azure-ad?view=vsts&tabs=new-nav). Note that only VSTS Git projects are supported.

4. Click **Choose project** and select a project.
5. Click **Choose branch** and select a branch.
6. Click **OK** to complete the setup process.

   ![Visual Studio configuration](~/media/azure-bot-build/continuous-deployment-setup-vs-configuration.png)

Now your continuous deployment with Azure DevOps setup is complete. Whenever you commit, your changes will automatically be deployed to Azure.

## Disable continuous deployment

While your bot is configured for continuous deployment, you may not use the online code editor to make changes to your bot. If you want to use the online code editor, you can temporarily disable continuous deployment.

To disable continuous deployment, do the following:
1. In the [Azure portal](https://portal.azure.com), go to your bot's **All App service settings** blade and click **Deployment options (Classic)**. 
2. Click **Disconnect** to disable continuous deployment. To re-enable continuous deployment, repeat the steps from the appropriate sections above.

## Additional information
- Visual Studio Team Services is now [Azure DevOps Services](https://docs.microsoft.com/en-us/azure/devops/?view=vsts)
