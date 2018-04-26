---
title: Configure continuous deployment for Bot Service | Microsoft Docs
description: Learn how to setup continuous deployment from source control for a Bot Service. 
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 03/08/2018
---

# Set up continuous deployment

Continuous deployment allows you to develop your bot locally. Continuous deployment is useful if your bot is checked into a source control like **GitHub** or **Visual Studio Team Services**. As you check your changes back into your source repository, your changes will automatically be deployed to Azure.

> [!NOTE]
> Once continuous deployment is set up, the [online code editor](bot-service-build-online-code-editor.md) becomes *READ ONLY*.

This topic will show you how to set up continuous deployment for **GitHub** and **Visual Studio Team Services**.

## Continuous deployment using GitHub

To set up continuous deployment using GitHub, do the following:

1. [Fork](https://help.github.com/articles/fork-a-repo/) the GitHub repository that contains the code you want to deploy to Azure.
2. From the Azure portal, go to your bot's **Build** blade and click **Configure continuous deployment**. 
3. Click **Setup**.
   
   ![Setup continuous deployment](~/media/azure-bot-build/continuous-deployment-setup.png)

4. Click **Choose source** and choose **GitHub**.

   ![Choose GitHub](~/media/azure-bot-build/continuous-deployment-setup-github.png)

5. Click **Authorization** then click the **Authorize** button and follow the prompts to give Azure authorization to access your GitHub account.

Your continuous deployment with GitHub setup is complete. Whenever you commit, your changes will automatically be deployed to Azure.

## Continuous deployment using Visual Studio

1. From your bot's **Build** blade, click **Configure continuous deployment**. 
2. Click **Setup**.
   
   ![Setup continuous deployment](~/media/azure-bot-build/continuous-deployment-setup.png)

3. Click **Choose source** and choose **Visual Studio Team Services**.

   ![Choose Visual Studio Team Services](~/media/azure-bot-build/continuous-deployment-setup-vs.png)

4. Click **Choose your account** and choose an account.
5. Click **Choose a project** and choose a project.
6. click **Choose Branch** and choose a branch to branch.
7. Click **OK** to complete the setup process.

   ![Visual Studio configuration](~/media/azure-bot-build/continuous-deployment-setup-vs-configuration.png)

Your continuous deployment with Visual Studio Team Services setup is complete. Whenever you commit, your changes will automatically be deployed to Azure.

## Disable continuous deployment

While your bot is configured for continuous deployment, you may not use the online code editor to make changes to your bot. If you want to use the online code editor, you can temporarily disable continuous deployment.

To disable (or re-enable) continuous deployment, do the following:

1. From your bot's **Build** blade, click **Configure continuous deployment**. 
2. Click **Disconnect** to disable continuous deployment. To re-enable continuous deployment, repeat these steps.

## Next steps
Now that your bot is set up for continuous deployment, test your code using the online Web Chat.

> [!div class="nextstepaction"]
> [Test in Web Chat](bot-service-manage-test-webchat.md)
