---
title: Azure Bot Service | Microsoft Docs
description: Learn about the Azure Bot Service.
author: kbrandl
ms.author: kibrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 08/04/2017
ms.reviewer: 
---

# Azure Bot Service

The Azure Bot Service accelerates the process of developing a bot. It provisions a web host with one of five bot templates you can modify in 
an integrated environment.

[!include[Azure Bot Service hosting plans](../includes/snippet-abs-hosting-plans.md)] 

## Prerequisites

You must have a Microsoft Azure subscription before you can use the Azure Bot Service. If you do not already have a subscription, you can register for a <a href="https://azure.microsoft.com/en-us/free/" target="_blank">free trial</a>.

## Create a bot in seconds

The Azure Bot Service enables you to quickly and easily create a bot in either C# or Node.js by using one of five templates.

[!include[Azure Bot Service templates](../includes/snippet-abs-templates.md)] 

For more information about templates, see [Templates in the Azure Bot Service](azure-bot-service-templates.md). 
For a step-by-step tutorial that shows how to quickly build and test a simple bot using Azure Bot Service, see [Create a bot with Azure Bot Service](azure-bot-service-quickstart.md).

## Choose development tool(s)

By default, Azure Bot Service enables you to develop your bot directly in the browser using the Azure editor, without any need for a tool chain (i.e., local editor and source control). 
The integrated chat window sits side-by-side with the Azure editor, which lets you test your bot on-the-fly as you write code in the browser. 

Although using Azure editor eliminates the need for an editor and source control on your local computer, Azure editor does not allow you to manage files (e.g., add files, rename files, or delete files). If you have Visual Studio Community or above, you can develop and debug your C# bot locally, and publish your bot to Azure. Also, you can [set up continuous deployment](azure-bot-service-continuous-deployment.md) by using the  source control system of your choice, with easy setup for Visual Studio Online and Github. With continuous deployment configured, you can develop and debug in an IDE on your local computer, and any code changes that you commit to source control automatically deploy to Azure.  

> [!NOTE]
> After enabling continuous deployment, be sure to modify your code through continuous deployment only and not through other mechanisms to avoid conflict.

## Manage your bot 

During the process of creating a bot using Azure Bot Service, you specify a name for your bot and generate its App ID and password. After your bot has been created, you can change its settings, configure it to run on one or more channels, or publish it to one or more channels. 

## Next steps

> [!div class="nextstepaction"]
> [Create a bot with the Azure Bot Service](azure-bot-service-quickstart.md)


## Additional resources


If you encounter problems or have suggestions regarding the Azure Bot Service, see [Support](../resources-support.md) for a list of available resources. 
