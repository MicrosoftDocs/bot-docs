---
title: Set up continuous integration for Azure Bot Service | Microsoft Docs
description: Learn how to set up continuous integration for Azure Bot Service.
keywords: Bot Framework, Azure Bot Service, continuous integration
author: RobStand
manager: rstand
ms.topic: bot-service-article
ms.prod: bot-framework
ms.service: Azure Bot Service
ms.date: 3/21/2017
ms.reviewer:
#ROBOTS: Index
---

#Set up continuous integration

If Azure’s code editor does not meet your development needs, you can set up continuous integration using your favorite IDE. 

There are three steps you need to follow, which will be covered in the sections below.

1. Create an empty repository in your favorite source control system.
2. Download the bot code.
3. Choose the deployment source and connect your repository.

If, after setting up continuous integration, you need to disconnect your deployment source from your bot, see the section below on how to disconnect your deployment source.

>[!NOTE] 
This document highlights the specific continuous integration features of Azure Bot Service. To get information about continuous integration as it relates to Azure App Services, see <a href="https://azure.microsoft.com/en-us/documentation/articles/app-service-continuous-deployment/" target="_blank">Continuous Deployment to Azure App Service</a>".

##Create an empty repository in your favorite source control system

The first step is to create an empty repository. At the time of this writing, Azure supports the following source control systems.

![continuous integration sourcecontrol system.png](~/media/continuous-integration-sourcecontrolsystem.png)

##Download the bot code
1. Download the bot code zip file from the Settings tab of your Azure bot.  ![continuous integration download.png](~/media/continuous-integration-download.png)
2. Unzip the bot code file to the local folder where you are planning to sync your deployment source.

##Choose the deployment source and connect your repository

1. Click the **Settings** tab within your Azure bot, and expand the **Continuous integration** section.
2. Click Set up integration source. ![continuous integration setup click](~/media/continuous-integration-setupclick.png) 
3. Click **Setup**, select your deployment source, and follow the steps to connect it. Make sure you select the repository type that you created in step 1. ![continuous integration sources](~/media/continuous-integration-sources.png)

##Disconnect your deployment source

If for any reason you need to disconnect your deployment source from your bot, open the **Settings** tab, expand the **Continuous integration** section, click **Set up integration source**, and then click **Disconnect** in the resulting blade.

>[!NOTE]
>When you setup continuous integration, the Azure's bot editor will be disabled. To re-enable it, you will need to disconnect your deployment source.

![continuous integration disconnect](~/media/continuous-integration-disconnect.png)

##Next steps
Learn how to [debug your local code](~/azure-bot-service/debug.md).