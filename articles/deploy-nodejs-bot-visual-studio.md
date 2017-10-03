---
title: Deploy a Node.js bot to Azure from Visual Studio | Microsoft Docs
description: Learn how to deploy a Node.js bot to Azure using Visual Studio's built-in publishing feature.
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 9/29/2017
ms.reviewer: 
---

# Deploy a Node.js bot to Azure from Visual Studio
When you build a bot with Visual Studio, you can take advantage of its built-in publishing capability. This tutorial shows you how to deploy a Node.js bot to Azure directly from Visual Studio 2017.

## Prerequisites

- You must have a Microsoft Azure subscription. If you do not already have a subscription, you can register for a <a href="https://azure.microsoft.com/en-us/free/" target="_blank">free trial</a>. 
- Install Visual Studio 2017. If you do not already have Visual Studio, you can download <a href="https://www.visualstudio.com/downloads/" target="_blank">Visual Studio 2017 Community</a> for free.
- Install **Azure development**. You can choose to have **Azure development** installed while installing Visual Studio or you can add it if you already have Visual Studio installed. You can add it by going to your computer's **Uninstall or change a program**, find **Microsoft Visual Studio 2017** and choose to **Modify** it. When the **Workloads** window shows up, check the box for **Azure development** and click **Modify** to install this resource.

[!include[Pre-deployment considerations](~/includes/snippet-deploy-considerations.md)]

## Step 1: Launch the Microsoft Azure Publishing Wizard in Visual Studio

1. Create a new **Blank Azure Node.js Web Application** project in Visual Studio. This process only works with this project type.<br/><br/>
![Create a new Node.js project](~/media/deploy-bot-visual-studio/node-create-project.png)

2. Update your **package.json** file to add these two dependencies to the project. 

  ```json
  "dependencies": {
    "botbuilder": "^3.7.0",
    "restify": "^4.3.0"
  }
  ```

3. Build the project. Then, in Solution Explorer, right-click **npm** and click **Install Missing npm Packages**. This will install the two dependencies you added in the previous step.

4. Add your bot code to the project as necessary.

5. In Solution Explorer, right-click on the project name and select **Publish**. This starts the Microsoft Azure publishing wizard.<br/><br/>
![Right-click on the project and choose Publish to start the Microsoft Azure publishing wizard](~/media/deploy-bot-visual-studio/node-dialog.png)

## Step 2: Publish to Azure using the Azure Publishing Wizard

1. Select **Microsoft Azure App Service** as the project type. Choosing this option will open the **App Service** window.<br/><br/>
![Select Microsoft Azure App Service and click Next](~/media/deploy-bot-visual-studio/node-publish.png)

2. Click **New** to create your **App Service**. <br/><br/>
![Click New to create an App Service](~/media/deploy-bot-visual-studio/node-app-service.png)

3. Fill in the form as appropriate for your implementation. 

  - Click **New** to create a new **Resource Group** or **App Service Plan** if necessary. 
  - Click **Change Type** and change the service's type to **Web App**.
  - Click **Create** to create your app service.<br/><br/> 
  ![Create App Service screen](~/media/deploy-bot-visual-studio/node-app-service-create.png)

4. Validate connection and copy the **Destination URL** value to the clipboard. You will need this value later to test the connection to the bot using the Emulator.<br/><br/>
![Validate Connection and verify input.](~/media/deploy-bot-visual-studio/node-publish-to-azure.png)

5. Click **Settings**. By default, your bot will be published in a **Release** configuration. If you want to debug your bot, in **Settings**, change **Configuration** to **Debug**.<br/><br/>
![Select Debug Configuration and click Publish](~/media/deploy-bot-visual-studio/node-configuration.png)

5. Click **Publish** to deploy your bot to Azure.

## Step 3: Test the connection to your bot

[!include[Verify connection to your bot](~/includes/snippet-verify-deployment-using-emulator.md)]

[!include[Post-deployment next steps](~/includes/snippet-deploy-next-steps.md)]

