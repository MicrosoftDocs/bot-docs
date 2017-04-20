---
title: Templates in the Azure Bot Service | Microsoft Docs
description: Learn about templates in the Azure Bot Service.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 4/13/2017
ms.reviewer: 
ROBOTS: Index, Follow
---

# Templates in the Azure Bot Service

The Azure Bot Service enables you to quickly and easily create a bot in either C# or Node.js by using one of five templates. 

[!include[Azure Bot Service templates](~/includes/snippet-abs-templates.md)] 

All bots that are created using the Azure Bot Service will initially contain a core set of files (specific to the development language that you choose), along with additional files that contain code that is specific to the selected template.

> [!NOTE]
> Azure Bot Service is powered by the serveless infrastructure of Azure functions and employs the same runtime concepts. 
> For more information, see the <a href="https://azure.microsoft.com/en-us/documentation/articles/functions-reference/" target="_blank">Azure Functions developers guide</a>.

## Files for a C# bot

If you choose C# as the language for your bot, the application that is created will contain these files (in addition to files that are specific to the selected template):

| File | Description |
|----|----|
| **Bot.sln** | The Microsoft Visual Studio solutions file. It is used locally if you [set up continuous integration](~/azure/azure-bot-service-continuous-integration.md). |
| **commands.json** | This file contains the commands that start **debughost** in Task Runner Explorer when you open the **Bot.sln** file. If you do not install Task Runner Explorer, you can delete this file. |
| **debughost.cmd** | This file contains the commands to load and run your bot. It is used locally if you [set up continuous integration](~/azure/azure-bot-service-continuous-integration.md) and debug your bot locally. For more information, see [Debug a C# bot using the Azure Bot Service on Windows](~/azure/azure-bot-service-debug-bot.md#debug-csharp). This file also contains your bot's App ID and password. To debug authentication, set the App ID and password in this file and also specify the App ID and password when testing your bot using the [emulator}(~/debug-bots-emulator.md). |
| **function.json** | This file contains the function’s bindings. Do not modify this file. |
| **host.json** | This metadata file contains the global configuration options that affect the function. |
| **project.json** | This file contains the project’s NuGet references. You can modify this file to add a new reference. |
| **project.lock.json** | This file is generated automatically. Do not modify this file. |
| **readme.md** | This file contains information about building bots with the Azure Bot Service. Read this file before you use or modify the bot. |

## Files for a Node.js bot

If you choose Node.js as the language for your bot, the application that is created will contain these files (in addition to files that are specific to the selected template):

| File | Description |
|----|----|
| **function.json** | This file contains the function’s bindings. Do not modify this file. |
| **host.json** | This metadata file contains the global configuration options that affect the function. |
| **project.json** | This file contains the project’s NuGet references. You can modify this file to add a new reference. |

## Additional resources

- [Create a basic bot](azure-bot-service-template-basic.md)
- [Create a form bot](azure-bot-service-template-form.md)
- [Create a language understanding bot](azure-bot-service-template-language-understanding.md)
- [Create a proactive bot](azure-bot-service-template-proactive.md)
- [Create a question and answer bot](azure-bot-service-template-question-and-answer.md)
