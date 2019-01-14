---
title: How Bot Service works | Microsoft Docs
description: Learn about the features and capabilities of Bot Service.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# How Bot Service works

Bot Service provides the core components for creating bots, including the Bot Framework SDK for developing bots and the Bot Framework for connecting bots to channels. Bot Service provides five templates you can choose from when creating your bots with support for .NET and Node.js.

> [!IMPORTANT]
> You must have a Microsoft Azure subscription before you can use Bot Service. If you do not already have a subscription, you can register for a <a href="https://azure.microsoft.com/en-us/free/" target="_blank">free account</a>.

## Hosting plans
Bot Service provides two hosting plans for your bots. You can choose a hosting plan that suits your needs.

### App Service plan

A bot that uses an App Service plan is a standard Azure web app you can set to allocate a predefined capacity with predictable costs and scaling. With a bot that uses this hosting plan, you can:

* Edit bot source code online using an advanced in-browser code editor.
* Download, debug, and re-publish your C# bot using Visual Studio.
* Set up continuous deployment easily for Visual Studio Online and Github.
* Use sample code prepared for the Bot Framework SDK.

### Consumption plan
A bot that uses a Consumption plan is a serverless bot that runs on <a href="http://go.microsoft.com/fwlink/?linkID=747839" target="_blank">Azure Functions</a>, and uses the pay-per-run Azure Functions pricing. A bot that uses this hosting plan can scale to handle huge traffic spikes. You can edit bot source code online using a basic in-browser code editor. For more information about the runtime environment of a Consumption plan bot, see <a target='_blank' href='/azure/azure-functions/functions-scale'>Azure Functions Consumption and App Service plans</a>.

## Templates

Bot Service enables you to quickly and easily create a bot in either C# or Node.js by using one of five templates.

[!INCLUDE [Bot Service templates](~/includes/snippet-abs-templates.md)]

[Learn more](bot-service-concept-templates.md) about the different templates and how they can help you build bots.

## Develop and deploy

By default, Bot Service enables you to develop your bot directly in the browser using the Online Code Editor, without any need for a tool chain. 

You can develop and debug your bot locally with the Bot Framework SDK and an IDE, such as Visual Studio 2017. You can publish your bot directly to Azure using Visual Studio 2017 or the Azure CLI. You can also [set up continuous deployment](bot-service-continuous-deployment.md) with the source control system of your choice, such as VSTS or GitHub. With continuous deployment configured, you can develop and debug in an IDE on your local computer, and any code changes that you commit to source control automatically deploy to Azure.  

> [!TIP]
> After enabling continuous deployment, be sure to modify your code through continuous deployment only and not through other mechanisms to avoid conflict.

## Manage your bot 

During the process of creating a bot using Bot Service, you specify a name for your bot, set up its hosting plan, choose a pricing tier, and configure some other settings. After your bot has been created, you can change its settings, configure it to run on one or more channels, and test it with in Web Chat. 

## Next steps

> [!div class="nextstepaction"]
> [Create a bot with Bot Service](bot-service-quickstart.md)