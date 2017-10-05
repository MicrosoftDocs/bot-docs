---
title: Templates in the Azure Bot Service | Microsoft Docs
description: Learn about templates in the Azure Bot Service.
author: kbrandl
ms.author: kibrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 08/04/2017
ms.reviewer: 
---

# Templates in the Azure Bot Service

The Azure Bot Service enables you to quickly and easily create a Consumption plan or App Service plan bot in either C# or Node.js by using one of five templates. 

[!include[Azure Bot Service templates](~/includes/snippet-abs-templates.md)] 

All bots that are created using the Azure Bot Service will initially contain a core set of files specific to the development language and the hosting plan that you choose, along with additional files that contain code that is specific to the selected template.

## Files for a bot created in an App Service plan

Bots created in an App Service plan are based on standard Web apps. Here's a list of some important files you'll find in the zip file (in addition to files that are specific to the selected template).

### Common files (both C# and Node.js)

| Folder | File | Description |
|----|----|----|
| / | **readme.md** | This file contains information about building bots with the Azure Bot Service. Read this file before you use or modify the bot. |
| /PostDeployScripts | **&ast;.&ast;** | Files needed to run some post deployment tasks. Do not touch these files. |

### C# specific files
| Folder | File | Description |
|----|----|----|
| / | **&ast;.sln** | The Microsoft Visual Studio solutions file. It is used locally if you [set up continuous deployment](azure-bot-service-continuous-deployment.md). |
| / | **build.cmd** | This file is needed to deploy your code when you are editing it online via the Azure App Service editor. If you're working locally, you don't need this file. |
| /Dialogs | **&ast;.cs** | The classes that define your bot dialogs. |
| /Controllers | **MessagesController.cs** | The main controller of your bot application. |
| /PostDeployScripts | **&ast;.PublishSettings** | The publish profile for your bot. You can use this file to publish directly from Visual Studio. |

### Node.js specific files

| Folder | File | Description |
|----|----|----|
| / | **app.js** | The main .js file of your bot. |
| / | **package.json** | This file contains the project’s npm references. You can modify this file to add a new reference. |

## Files for a bot created in a Consumption plan

Bots in a Consumption plan are based on Azure Functions. Here's a list of some important files you'll find in the zip file (in addition to files that are specific to the selected template).

### Common files (both C# and Node.js)

| Folder | File | Description |
|----|----|----|
| / | **readme.md** | This file contains information about building bots with the Azure Bot Service. Read this file before you use or modify the bot. |
| /messages | **function.json** | This file contains the function’s bindings. Do not modify this file. |
| /messages | **host.json** | This metadata file contains the global configuration options that affect the function. |
| /PostDeployScripts | **&ast;.&ast;** | Files needed to run some post deployment tasks. Do not touch these files. |

### C# specific files
| Folder | File | Description |
|----|----|----|
| / | **Bot.sln** | The Microsoft Visual Studio solutions file. It is used locally if you [set up continuous deployment](azure-bot-service-continuous-deployment.md). |
| / | **commands.json** | This file contains the commands that start **debughost** in Task Runner Explorer when you open the **Bot.sln** file. If you do not install Task Runner Explorer, you can delete this file. |
| / | **debughost.cmd** | This file contains the commands to load and run your bot. It is used locally if you [set up continuous deployment](azure-bot-service-continuous-deployment.md) and debug your bot locally. For more information, see [Debug a C# bot using the Azure Bot Service on Windows](azure-bot-service-debug-bot.md#debug-csharp-serverless). This file also contains your bot's App ID and password. To debug authentication, set the App ID and password in this file and also specify the App ID and password when testing your bot using the [emulator](debug-bots-emulator.md). |
| /messages | **project.json** | This file contains the project’s NuGet references. You can modify this file to add a new reference. |
| /messages | **project.lock.json** | This file is generated automatically. Do not modify this file. |
| /messages | **run.csx** | Defines the initial Run method that gets executed on an incoming request. |

### Node.js specific files

| Folder | File | Description |
|----|----|----|
| /messages | **index.js** | The main .js file of your bot. |
| /messages | **package.json** | This file contains the project’s npm references. You can modify this file to add a new reference. |

## Additional resources

- [Create a bot using the Basic template](azure-bot-service-serverless-template-basic.md)
- [Create a bot using the Form template](azure-bot-service-serverless-template-form.md)
- [Create a bot using the Language understanding template](azure-bot-service-template-language-understanding.md)
- [Create a bot using the Proactive template](azure-bot-service-template-proactive.md)
- [Create a bot using the Question and Answer template](azure-bot-service-template-question-answer.md)
