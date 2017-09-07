---
title: Migrate a C# bot from a consumption plan to an app service plan | Microsoft Docs
description: Migrate an Azure Bot Service C# bot from a consumption hosting plan to an app service hosting plan.
author: johnd-ms
ms.author: v-jodemp
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 08/16/2017
---
# Migrate a bot from a consumption plan to an app service plan

This article shows you how you can migrate a C# script bot with a consumption plan into a C# bot with an app service plan. 

This migration assumes you have Visual Studio (Community or above).

## Advantages of a bot on an app service plan

Bots on an app service plan run as Azure web apps. Web app bots can do things that consumption plan bots cannot:

- A web app bot can add custom route definitions.
- A web app bot can enable a Websocket server. 
- A bot that uses a consumption hosting plan has the same limitations as all code running on Azure Functions. For more information, see <a target='_blank' href='https://docs.microsoft.com/en-us/azure/azure-functions/functions-scale'>Azure Functions Consumption and App Service plans</a>.

## Download your existing bot source

Follow these steps to download the source code of your existing bot:

1. Within your Azure bot, click the **Settings** tab and expand the **Continuous deployment** section.  
2. Click the blue button to download the zip file that contains the source code for your bot.  
    ![Download the bot zip file](../media/continuous-deployment-consumption-download.png)
3. Extract the contents of the downloaded zip file to a local folder. 


## Create a bot template

Azure Bot Service has the same templates for consumption plan and app service plan bots. To migrate from a consumption plan bot, create a new app service plan bot in Azure Bot Service, based on the same template. Underlying code can differ between the two hosting types for the same template, but the new web app has similar structure and configuration features that your existing bot uses.

## Download the new bot source

Follow these steps to download the source code of the new bot:

1. Within your Azure bot, click the **BUILD** tab, find the **Download source code** section, and click **Download zip file**. 
2. Extract the contents of the downloaded zip file to the local folder.

## Add source files to new solution

Some .csx files might compile and run as .cs files in the new solution. Create a .cs file for each .csx file in your solution, except `run.csx`. You will migrate `run.csx` logic by hand. In your .cs files, you may need to add a class declaration and optional namespace declaration.

## Migrate run.csx logic into your project

C# script projects have a `Run` method where different `ActivityTypes` values are handled. Import your activity handling logic into the `MessageController.Post` method, in `MessageController.cs`.

## Remove #r and #load compiler keywords

C# script files can include a reference module using the `#r` keyword. Remove these lines, and also add these as references to your Visual Studio project. Also remove `#load` keywords, which insert other source code files in a file compilation. Instead, add all `.csx` files as to your project as `.cs` source code.

## Add references from project.json

If your consumption plan bot adds NuGet references in its `project.json` file, add these references to your new Visual Studio solution by right-clicking the project in the Solution Explorer pane, and clicking **Add Reference**.

### Add references that were implicit

An Azure Bot Service bot on a consumption plan implicitly includes these references in all .csx source files. Source migrated into .cs source files may need to add explicit references for these classes:

- `TraceWriter` in the NuGet package `Microsoft.Azure.WebJobs` that provides the `Microsoft.Azure.WebJobs.Host` namespace types. 
- timer triggers in the NuGet package `Microsoft.Azure.WebJobs.Extensions`
- `Newtonsoft.Json`, `Microsoft.ServiceBus`, and other automatically referenced assemblies
- `System.Threading.Tasks` and other automatically imported namespaces

For additional guidance, see *Converting to class files* in <a target='_blank' href='https://blogs.msdn.microsoft.com/appserviceteam/2017/03/16/publishing-a-net-class-library-as-a-function-app/'>Publishing a .NET class library as a Function App</a>.

## Debug your new bot

A bot on an app service plan is much easier than a bot on a consumption plan to debug locally. You can use [the emulator](../debug-bots-emulator.md) to debug your migrated code locally.

## Publish from Visual Studio, or set up continuous deployment

Finally, publish your migrated source code to Azure Bot Service either by importing its `.PublishSettings` file and clicking **Publish**, or by [setting up continuous deployment](azure-bot-service-debug-bot.md).
