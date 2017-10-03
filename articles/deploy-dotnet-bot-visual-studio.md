---
title: Deploy a .NET bot to Azure from Visual Studio | Microsoft Docs
description: Learn how to deploy a .NET bot to Azure using Visual Studio's built-in publishing feature.
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 9/29/2017
ms.reviewer: 
---

# Deploy a .NET bot to Azure from Visual Studio
When you build a bot with Visual Studio, you can take advantage of its built-in publishing capability. This tutorial shows you how to deploy a .NET bot to Azure directly from Visual Studio 2017.

> [!NOTE]
> If you created a bot with the Azure Bot Service, your bot deployment was part of the Azure Bot Service bot 
> creation process. For more information, see 
> [Publish a bot to Azure Bot Service](azure-bot-service-continuous-deployment.md).

## Prerequisites

- You must have a Microsoft Azure subscription. If you do not already have a subscription, you can register for a <a href="https://azure.microsoft.com/en-us/free/" target="_blank">free trial</a>. 
- Install Visual Studio 2017. If you do not already have Visual Studio, you can download <a href="https://www.visualstudio.com/downloads/" target="_blank">Visual Studio 2017 Community</a> for free.
- Install **Azure development**. You can choose to have **Azure development** installed while installing Visual Studio or you can add it if you already have Visual Studio installed. You can add it by going to your computer's **Uninstall or change a program**, find **Microsoft Visual Studio 2017** and choose to **Modify** it. When the **Workloads** window shows up, check the box for **Azure development** and click **Modify** to install this resource.

[!include[Pre-deployment considerations](~/includes/snippet-deploy-considerations.md)]

## Step 1: Launch the Microsoft Azure Publishing Wizard in Visual Studio

Open your .NET bot project in Visual Studio. In Solution Explorer, right-click on the project name and select **Publish**. This starts the Microsoft Azure publishing wizard.

![Right-click on the project and choose Publish to start the Microsoft Azure publishing wizard](~/media/deploy-bot-visual-studio/net-dialog.png)

## Step 2: Publish to Azure using the Azure Publishing Wizard

1. Select **Microsoft Azure App Service** as the project type and click **Publish**.<br/><br/>
![Select Microsoft Azure App Service and click Next](~/media/deploy-bot-visual-studio/net-publish.png)

2. Click **Change Type** and change the service's type to **Web App**. Then, name your web app and fill out the rest of the information as appropriate for your implementation. <br/><br/>
![Name your App Service, then click New App Service Plan to define one](~/media/deploy-bot-visual-studio/net-app-service-create.png)

3. Click **Create** to create your app service. Once the service is created, the bot will be published to Azure and your bot's HTML page will be displayed in your default browser. 

Copy the **Site URL** value to the clipboard. You will need this value later to test the connection to the bot using the Emulator.
<br/><br/>
![Validate Connection and click Next to proceed to the final step.](~/media/deploy-bot-visual-studio/net-destination.png)

By default, your bot will be published in a **Release** configuration. If you want to debug your bot, in **Settings**, change **Configuration** to **Debug**.
Click **Save** to save your settings. <br/><br/>
![Select Release Configuration and click Publish](~/media/deploy-bot-visual-studio/net-configuration.png)

## Step 3: Test the connection to your bot

[!include[Verify connection to your bot](~/includes/snippet-verify-deployment-using-emulator.md)]

[!include[Post-deployment next steps](~/includes/snippet-deploy-next-steps.md)]

