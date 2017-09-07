---
title: Deploy a bot to Azure from a local git repository | Microsoft Docs
description: Learn how to deploy a bot to Azure from your local git repository.
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 08/04/2017
---

# Deploy a bot to Azure from a local git repository

Azure allows continuous deployment from your Git repository to Azure.
With continuous deployment, when you change and build your bot's code, the bot will automatically deploy to Azure.
This tutorial shows you how to deploy a bot to Azure via continuous deployment from a local Git repository.

> [!NOTE]
> If you created a bot with the Azure Bot Service, your bot deployment was part of the Azure Bot Service bot 
> creation process.

## Prerequisites

You must have a Microsoft Azure subscription before you can deploy a bot to Azure. If you do not already have a subscription, you can register for a <a href="https://azure.microsoft.com/en-us/free/" target="_blank">free trial</a>. You will also need <a href="https://git-scm.com/downloads" target="_blank">Git</a>.

[!include[Pre-deployment considerations](~/includes/snippet-deploy-considerations.md)]

## Step 1: Install the Azure CLI

Download and <a href="https://docs.microsoft.com/en-us/azure/xplat-cli-install" target="_blank">install Azure CLI 2.0</a>. 

## Step 2: Create and configure an Azure site

Open a command prompt, Powershell, or other console and log in to your Azure account using the following command:

```azurecli
az login
```

Afer you log in, run the following command to create a resource group. Specify the name of the resource group you want to create and the location, such as *westus*, where your bot will be deployed.

```azurecli
az group create --location <Location> --name <MyResourceGroup>
```

Run the following command to create a plan. Specify a name for the plan and the resource group name you specified in the previous step.

```azurecli
az appservice plan create --name <MyPlan> --resource-group <MyResourceGroup> --sku FREE
```

The next step is to create a new Azure site. Find your bot's handle in the [Bot Framework Portal](https://dev.botframework.com/) on your bot's **SETTINGS** page. Run the following command with your bot's handle, the name of the resource group, and the name of your plan:

```azurecli
az webapp create --name <MyBotHandle> --resource-group <MyResourceGroup> --plan <MyPlan>
```

Git requires a user name-password pair to deploy a bot. The user name and password are account-level. They are different from your Azure subscription credentials. The user name must be unique, and the password must be at least eight characters long, with two of the following three elements: letters, numbers, symbols. You create this deployment user only once and you can use it for all your Azure deployments. Record the user name and password. You need them to deploy the bot later. 

Create a deployment account with the following command with a user name and a password:

```azurecli
az webapp deployment user set --user-name <UserName> --password <Password>
```

> [!TIP]
> If you get a **'Conflict'. Details: 409 error**, change the user name. If you get a **'Bad Request'. Details: 400 error**, 
> use a stronger password. 

Find the URL where Git will push your source code changes by running the following command:

```azurecli
az webapp deployment source config-local-git --name <MyBotHandle> --resource-group <MyResourceGroup> --query url --output tsv
```

Your URL will look similar to `https://<UserName>@<MyBotHandle>.scm.azurewebsites.net/<MyBotHandle>.git`.

Connect your local Git repository to the bot hosted on Azure. A dialog will appear where you must provide the user name and password credentials you created earlier.

```cmd
cd <MyGitRepo>
git remote add azure <URLResultFromLastStep>
git push azure master
``` 

## Step 3: Commit changes to Git and push to the Azure site

To update the site with your latest changes, run the following commands to commit the changes and push those changes to the Azure site:

```cmd
git add .
git commit -m "<your commit message>"
git push azure master
```

A dialog will appear where you must provide the user name and password credentials you created earlier.

## Step 4: Test the connection to your bot

[!include[Verify connection to your bot](~/includes/snippet-verify-deployment-using-emulator.md)]

> [!NOTE]
> Because your bot runs on Azure, you must enter its **app ID** and **password** into the emulator to connect, and because it's running remotely, you might need to provide the emulator with a path to **ngrok** tunneling software on your computer.


[!include[Post-deployment next steps](~/includes/snippet-deploy-next-steps.md)]
