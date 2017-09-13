---
title: Build tab features of Azure Bot Service | Microsoft Docs
description: Online code editor, source download, and continuous deployment are the build tab features of Azure Bot Service. 
author: johnd-ms
ms.author: v-jodemp
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 08/07/2017
---
# Build tab features of Azure Bot Service

An Azure Bot Service bot that uses an App Service plan includes these features on its **Build** tab.

> [!NOTE]
> The **Build** tab of an Azure Bot Service bot that uses a consumption plan only provides a basic browser-based code editor. 
> You can download source code and configure continuous deployment for a consumption plan bot using features located 
> on its **Settings** tab, in the **Continuous deployment** section.

## Online code editor

The online code editor lets you change the source code of your bot via the [Azure App Service Editor](https://github.com/projectkudu/kudu/wiki/App-Service-Editor). The editor provides IntelliSense in C# source files. 

The online code editor is a great way to start, but if you need advanced debugging and file management, get the bot code locally by downloading it and optionally by setting up continuous deployment.


### Deploy from the online code editor

When you change a source file in the online code editor, you must run the deployment script before your changes take effect. Follow these steps to run the deployment script.

 1. In the App Service Editor, click the Open Console icon.  
    ![Console Icon](~/media/azure-bot-service-console-icon.png)

 2. In the Console window, type **deploy.cmd**, and press the enter key.

## Download source code

You can download a zip file that contains all source files, a Visual Studio solution file, and a configuration file that lets you publish from Visual Studio. Using these files, you can modify and debug your bot in Visual Studio, or your favorite IDE, and test it with the Bot Framework Emulator. Once you are ready to publish, you can setup publishing directly from Visual Studio by importing the publish profile saved in the `.PublishSettings` file within the PostDeployScripts folder. For more information, see [Publish C# bot on App Service plan from Visual Studio](azure-bot-service-continuous-deployment.md).

## Continuous deployment from source control

Continuous deployment lets you re-publish to Azure whenever you check a code change in to your source control service. If you work in a team and need to share code in a source control system, then you should setup continuous deployment and use the integrated development environment (IDE) and source control system of your choice. 

> [!NOTE]
> Some templates provided by the Azure Bot Service, and especially templates on a consumption plan, 
> require additional setup steps to [debug on your local computer](azure-bot-service-debug-bot.md). 

The Azure Bot Service provides a quick way to set up continuous deployment for Visual Studio Online and GitHub, by providing an access token issued to you on those web sites. For other source control systems, select **other** and follow the steps that appear. For more help setting up continuous deployment on a source control other than Visual Studio Online or Github, see [Set up continuous deployment](azure-bot-service-continuous-deployment.md).


> [!WARNING]
> When you use continuous deployment, be sure to only modify code by checking it into your source control service. 
> Do not use the online code editor to change source code when continuous deployment is enabled. 
> For consumption plan bots, the online code editor is read-only when continuous deployment is enabled.
