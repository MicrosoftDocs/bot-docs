---
title: Configure continuous deployment for Bot Service | Microsoft Docs
description: Learn how to setup continuous deployment from source control for a Bot Service. 
keywords: continuous deployment, publish, deploy, azure portal
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 09/18/2018
---

# Set up continuous deployment
If your code is checked into **GitHub** or **Azure DevOps (formerly Visual Studio Team Services)**, use continous deployment to automatically deploy code changes from your source repository to Azure. In this topic, we'll cover setting up continuous deployment for **GitHub** and **Azure DevOps**.

> [!NOTE]
> Once continuous deployment is set up, the online code editor in the Azure portal becomes *READ ONLY*.

## Continuous deployment using GitHub

To set up continuous deployment using GitHub, do the following:

1. Use the GitHub repository that contains the source code you want to deploy to Azure. You can either [fork](https://help.github.com/articles/fork-a-repo/) an existing repository or create your own and upload related source code into your GitHub repository.
2. In the [Azure portal](https://portal.azure.com), go to your bot's **Build** blade and click **Configure continuous deployment**. 
3. Click **Setup**.
   
   ![Setup continuous deployment](~/media/azure-bot-build/continuous-deployment-setup.png)

4. Click **Choose Source** and select **GitHub**.

   ![Choose GitHub](~/media/azure-bot-build/continuous-deployment-setup-github.png)

5. Click **Authorization** then click the **Authorize** button and follow the prompts to give Azure authorization to access your GitHub account.

6. Click **Choose project** and select a project.

7. Click **Choose branch** and select a branch.

8. Click **OK** to complete the setup process.

Now your continuous deployment with GitHub setup is complete. Whenever you commit to the source code repository, your changes will automatically be deployed to the Azure Bot Service.

## Continuous deployment using Azure DevOps

1. In the [Azure portal](https://portal.azure.com), from your bot's **Build** blade, click **Configure continuous deployment**. 
2. Click **Setup**.
   
   ![Setup continuous deployment](~/media/azure-bot-build/continuous-deployment-setup.png)

3. Click **Choose Source** and select **Visual Studio Team Services**. Please keep in mind that Visual Studio Team Services is now Azure DevOps Services.

   ![Choose Visual Studio Team Services](~/media/azure-bot-build/continuous-deployment-setup-vs.png)

4. Click **Choose your account** and select an account.

> [!NOTE]
> If you do not see your account, make sure it is linked to your Azure subscription. To link your account to your Azure subscription, go to the Azure Portal, open **Azure DevOps Services organizations (formerly Team Services)**. You will see a list of organizations you have in your Azure DevOps. Click into the one with the source code of the bot you want to deploy, you will find a **Connect AAD** button. If the organization you choose is not linked to your Azure subscription, this button will be active. So click this button to set up the connection. You may need to wait some time before it takes effect.

5. Click **Choose project** and select a project.

> [!NOTE]
> Only VSTS Git projects are supported.

6. Click **Choose branch** and select a branch.
7. Click **OK** to complete the setup process.

   ![Visual Studio configuration](~/media/azure-bot-build/continuous-deployment-setup-vs-configuration.png)

Now your continuous deployment with Azure DevOps setup is complete. Whenever you commit, your changes will automatically be deployed to Azure.

## Disable continuous deployment

While your bot is configured for continuous deployment, you may not use the online code editor to make changes to your bot. If you want to use the online code editor, you can temporarily disable continuous deployment.

To disable continuous deployment, do the following:

1. From your bot's **Build** blade, click **Configure continuous deployment**. 
2. Click **Disconnect** to disable continuous deployment. To re-enable continuous deployment, repeat the steps from the appropriate sections above.

## Additional information
- [Azure DevOps](https://docs.microsoft.com/en-us/azure/devops/?view=vsts)
