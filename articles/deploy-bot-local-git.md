---
title: Deploy a bot to Azure from a local git repository | Microsoft Docs
description: Learn how to deploy a bot to Azure from your local git repository.
author: RobStand
ms.author: rstand


manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date:
ms.reviewer: rstand
ROBOTS: Index, Follow
---
# Deploy a bot to Azure from a local git repository

Azure allows continuous integration of your Git repository with your Azure deployment.
With continuous integration, when you change and build your bot's code, the bot will automatically deploy to Azure.
This tutorial shows you how to deploy a bot to Azure via continuous integration from local Git.

[!include[Pre-deployment considerations](~/includes/snippet-deploy-considerations.md)]

## Step 1: Install the Azure CLI

Install the Azure CLI by following the instructions <a href="https://docs.microsoft.com/en-us/azure/xplat-cli-install" target="_blank">here</a>.

## Step 2: Create and configure an Azure site

Login to your Azure account by running this command at the command prompt and following the instructions:

```
azure login
```

Next, run the following command to create a new Azure site and configure it for Git (where *\<appname\>* is the name of the site that you want to create):

```
azure site create --git <appname>
```

> [!TIP]
> The URL of the site that is created will be in the following format: *https://appname.azurewebsites.net*.

## Step 3: Commit changes to Git and push to the Azure site

To update the site with your latest changes, run the following commands to commit the changes and push those changes to the Azure site:

```
git add .
git commit -m "<your commit message>"
git push azure master
```

You will be prompted to enter your deployment credentials.
If you don't yet have them, you can configure them on the Azure Portal by following these steps:

1. Login to the <a href="http://portal.azure.com" target="_blank">Azure Portal</a>
2. Click the site that you've just created
3. Open the **Settings** blade
4. In the **PUBLISHING** section, click **Deployment credentials**, specify a username and password, and click save  
![Deployment credentials](~/media/publishing-your-bot-deployment-credentials.png)
5. Return to the command prompt and provide the deployment credentials as requested

After you've entered your deployment credentials at the command prompt, your bot will be deployed to the Azure site.

## Step 4: Test the connection to your bot

[!include[Verify connection to your bot](~/includes/snippet-verify-deployment-using-emulator.md)]

[!include[Post-deployment next steps](~/includes/snippet-deploy-next-steps.md)]