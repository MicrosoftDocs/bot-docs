---
title: Azure Bot Service | Microsoft Docs
description: Learn about the Azure Bot Service.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 4/12/2017
ms.reviewer: 
ROBOTS: Index, Follow
---

# Azure Bot Service

The Azure Bot Service provides an integrated environment that is purpose-built for bot development, enabling you to build, connect, test, deploy, and manage bots, all from one place. 
It is powered by Microsoft Bot Framework and <a href="http://go.microsoft.com/fwlink/?linkID=747839" target="_blank">Azure Functions</a>, which means that your bot will run in a serverless environment on Azure that will scale based upon demand.

> [!NOTE]
> The Azure Bot Service is currently in preview so it should not be used for building production bots.

## Prerequisites

You must have a Microsoft Azure subscription before you can use the Azure Bot Service. If you do not already have a subscription, you can register for a <a href="https://azure.microsoft.com/en-us/free/" target="_blank">free trial</a>.

## Create a bot in seconds

The Azure Bot Service enables you to quickly and easily create a bot in either C# or Node.js by using one of five templates.

[!include[Azure Bot Service templates](~/includes/snippet-abs-templates.md)] 

For more information about templates, see [Templates in the Azure Bot Service](~/azure/azure-bot-service-templates.md). 
For a step-by-step tutorial that shows how to quickly build and test a simple bot using Azure Bot Service, see [Create a bot with Azure Bot Service](~/azure/azure-bot-service-quickstart.md).

## Choose development tool(s)

By default, Azure Bot Service enables you to develop your bot directly in the browser using the Azure editor, without any need for a tool chain (i.e., local editor and source control). 
The integrated chat window sits side-by-side with the Azure editor, which lets you test your bot on-the-fly as you write code in the browser. 

Although using Azure editor eliminates the need for a local editor and source control, 
Azure editor does not allow you to manage files (e.g., add files, rename files, or delete files). 
If you want to the ability to manage files within your application, you can [set up continuous integration](azure-bot-service-continuous-integration.md) and use the integrated development environment (IDE) and source control system of your choice (e.g., Visual Studio Team, GitHub, Bitbucket). With continuous integration configured, any code changes that you commit to source control will automatically be deployed to Azure. Additionally, you will be able to [debug your bot locally](~/azure/azure-bot-service-debug-bot.md) if you configure continuous integration. 

> [!NOTE]
> If you configure continuous integration for your bot, you will no longer be able to edit code in the Azure editor.

## Manage your bot 

During the process of creating a bot using Azure Bot Service, you specify a name for your bot and generate its App ID and password. After your bot has been created, you can update its settings, configure it to run on one or more channels, or publish it to one or more channels. 

## Next steps

Learn more about building bots using the Azure Bot Service by reviewing these articles: 

- [Create a bot with Azure Bot Service](~/azure/azure-bot-service-quickstart.md)
- [Templates in the Azure Bot Service](~/azure/azure-bot-service-templates.md)
- [Set up continuous integration](~/azure/azure-bot-service-continuous-integration.md)
- [Debug your bot](~/azure/azure-bot-service-debug-bot.md)

If you encounter problems or have suggestions regarding the Azure Bot Service, see [Support](~/resources-support.md) for a list of available resources. 
