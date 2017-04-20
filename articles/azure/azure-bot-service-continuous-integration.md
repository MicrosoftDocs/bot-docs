---
title: Set up continuous integration for a bot that is built using Azure Bot Service | Microsoft Docs
description: Learn how to set up continuous integration for a bot that is built using Azure Bot Service.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 4/13/2017
ms.reviewer: 
ROBOTS: Index, Follow
---

# Set up continuous integration

By default, Azure Bot Service enables you to develop your bot directly in the browser using the Azure editor, without any need for a local editor or source control. However, Azure editor does not allow you to manage files within your application (e.g., add files, rename files, or delete files). If you want to the ability to manage files within your application, you can set up continuous integration and use the integrated development environment (IDE) and source control system of your choice (e.g., Visual Studio Team, GitHub, Bitbucket). With continuous integration configured, any code changes that you commit to source control will automatically be deployed to Azure. Additionally, you will be able to [debug your bot locally](~/azure/azure-bot-service-debug-bot.md) if you configure continuous integration.

> [!NOTE]
> If you enable continuous integration for your bot, you will no longer be able to edit 
> code in the Azure editor. If you want to edit your code in Azure editor once again, 
> you must [disable continuous integration](#disable-continuous-integration).

This article describes how to enable and disable continuous integration for a bot that you created using Azure Bot Service.

## Enable continuous integration

You can enable continous integration for your application by completing three simple steps.

### Create an empty repository within a source control system

First, create an empty repository within one of the source control systems that Azure supports.

![Source control system](~/media/continuous-integration-sourcecontrolsystem.png)

### Download the source code for your bot

Next, download the source code for your bot from the Azure portal to your local working directory.

1. Within your Azure bot, click the **Settings** tab and expand the **Continuous integration** section. 

2. Click the link to download the zip file that contains the source code for your bot.  

    ![Download the bot zip file](~/media/continuous-integration-download.png)  

3. Extract the contents of the downloaded zip file to the local folder where you are planning to sync your deployment source.

### Choose the deployment source and connect your repository

Finally, choose the deployment source for your bot and connect your repository.

1. Within your Azure bot, click the **Settings** tab and expand the **Continuous integration** section. 

2. Click **Set up integration source**.  

    ![Setup integration source](~/media/continuous-integration-setupclick.png)  

3. Click **Setup**, select the deployment source that corresponds to the source control system where you previously created the empty repository, and complete the steps to connect it.  

    ![Setup integration source](~/media/continuous-integration-sources.png)  

## Disable continuous integration

To disable continuous integration for your application, disconnect the deployment source from your bot by completing these steps:

1. Within your Azure bot, click the **Settings** tab and expand the **Continuous integration** section.
2. Click **Set up integration source**.  

    ![Setup integration source](~/media/continuous-integration-setupclick.png)  

3. Click **Disconnect**.  

    ![Disconnect your deployment source](~/media/continuous-integration-disconnect.png)  

## Additional resources

To learn how to debug your bot locally after you have configured continuous integration, see 
[Debug your bot](~/azure/azure-bot-service-debug-bot.md).

This article has highlighted the specific continuous integration features of Azure Bot Service. For information about continuous integration as it relates to Azure App Services, see <a href="https://azure.microsoft.com/en-us/documentation/articles/app-service-continuous-deployment/" target="_blank">Continuous Deployment to Azure App Service</a>.
