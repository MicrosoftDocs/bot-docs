---
title: Create a bot with the Bot Builder SDK for .NET | Microsoft Docs
description: Create a bot with the Bot Builder SDK for .NET, a powerful bot construction framework.
keywords: Bot Builder SDK, create a bot, quickstart, .NET, getting started, C# bot
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: get-started-article
ms.prod: bot-framework
ms.date: 08/30/2018
monikerRange: 'azure-bot-service-4.0'
---

# Create a bot with the Bot Builder SDK for .NET
[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

This quickstart walks you through building a bot by using the C# template, and then testing it with the Bot Framework Emulator. 

## Prerequisites
- Visual Studio [2017](https://www.visualstudio.com/downloads)
- Bot Builder SDK v4 template for [C#](https://marketplace.visualstudio.com/items?itemName=BotBuilder.botbuilderv4)
- Bot Framework [Emulator](https://github.com/Microsoft/BotFramework-Emulator/releases)
- Knowledge of [ASP.Net Core](https://docs.microsoft.com/aspnet/core/) and asynchronous programming in [C#](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/index)

## Create a bot
Install BotBuilderVSIX.vsix template that you downloaded in the prerequisites section. 

In Visual Studio, create a new bot project.

![Visual Studio project](../media/azure-bot-quickstarts/bot-builder-dotnet-project.png)

> [!TIP] 
> If needed, update [NuGet packages](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio).

Thanks to the template, your project contains all of the code that's necessary to create the bot in this quickstart. You won't actually need to write any additional code.

## Start your bot in Visual Studio

When you click the run button, Visual Studio will build the application, deploy it to localhost, and launch the web browser to display the application's `default.htm` page. At this point, your bot is running locally.

## Start the emulator and connect your bot

Next, start the emulator and then connect to your bot in the emulator:

1. Click the **Open Bot** link in the emulator "Welcome" tab. 
2. Select the .bot file located in the directory where you created the Visual Studio solution.

## Interact with your bot

Send a message to your bot, and the bot will respond back with a message.
![Emulator running](../media/emulator-v4/emulator-running.png)

## Next steps
If you'd like to review the source code that was used in this quickstart, take a look at [anatomy of a bot](../v4sdk/bot-builder-anatomy.md) topic. 

> [!div class="nextstepaction"]
> [Deploy to Azure](../bot-builder-howto-deploy-azure.md)
