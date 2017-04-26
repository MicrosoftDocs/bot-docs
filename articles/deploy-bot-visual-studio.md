---
title: Deploy a bot to Azure from Visual Studio | Microsoft Docs
description: Learn how to deploy a bot to Azure using Visual Studio's built-in publishing feature.
author: RobStand
ms.author: rstand


manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date:
ms.reviewer: rstand

---
# Deploy a bot to Azure from Visual Studio
When you build a bot with Visual Studio, you can take advantage of its built-in publish capability. This tutorial shows you to how deploy a bot to Azure directly from Visual Studio.

## Prerequisites
- Visual Studio
- Azure

[!include[Pre-deployment considerations](~/includes/snippet-deploy-considerations.md)]

## Step 1: Launch the Microsoft Azure Publishing Wizard in Visual Studio

Open your project in Visual Studio. In Solution Explorer, right-click on the project and select **Publish**. This starts the Microsoft Azure publishing wizard.

![Right-click on the project and choose Publish to start the Microsoft Azure publishing wizard](~/media/connector-getstarted-publish-dialog.png)

## Step 2: Use the Azure Publishing Wizard to publish your bot to the cloud

Select **Microsoft Azure App Service** as the project type and click **Next**.<br/><br/>
![Select Microsoft Azure App Service and click Next](~/media/connector-getstarted-publish.png)

Create your App Service by clicking **New…** on the right side of the dialog. <br/><br/>
![Click New to create a new Azure App Service](~/media/connector-getstarted-publish-app-service.png)

Click **Change Type** and change the service's type to Web App.
Then, name your web app and fill out the rest of the information as appropriate for your implementation. <br/><br/>
![Name your App Service, then click New App Service Plan to define one](~/media/connector-getstarted-publish-app-service-create.png)

Click **Create** to create your app service. <br/><br/>
![Create the App Service Plan by clicking Create](~/media/connector-getstarted-publish-app-service-create-spinner.png)

Copy the **Destination URL** value to the clipboard
(you'll need this value later to test the connection to the bot),
and then click **Validate Connection** to verify that the settings have been configured correctly.
If validation is successful, click **Next**. <br/><br/>
![Validate Connection and click Next to proceed to the final step.](~/media/connector-getstarted-publish-destination.png)

By default, your bot will be published in a Release configuration.
(If you want to debug your bot, change **Configuration** to Debug.)
Click **Publish** to publish your bot to Microsoft Azure. <br/><br/>
![Select Release Configuration and click Publish](~/media/connector-getstarted-publish-configuration.png)

> [!NOTE]
> During the publishing process, you will see a number of messages displayed in the Visual Studio 2015 Output window.
> When publishing completes, your bot's HTML page will be displayed in your default browser.

## Step 3: Test the connection to your bot

[!include[Verify connection to your bot](~/includes/snippet-verify-deployment-using-emulator.md)]

[!include[Post-deployment next steps](~/includes/snippet-deploy-next-steps.md)]

