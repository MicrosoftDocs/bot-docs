---
title: Create a bot with the Bot Builder SDK for .NET | Microsoft Docs
description: Learn how to create a bot with the Bot Builder SDK for .NET.
keywords: Bot Framework, Bot Builder, SDK, .NET
author: kbrandl
manager: rstand
ms.topic: get-started-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/27/2017
ms.reviewer: rstand
#ROBOTS: Index
---
# Create a bot with the Bot Builder SDK for .NET
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-getstarted.md)
> * [Node.js](bot-framework-nodejs-getstarted.md)
> * [Azure Bot Service](bot-framework-azure-getstarted.md)
>

> [!NOTE]
> This article contains preliminary (draft) content that 
> will change significantly prior to publication.

The Bot Builder SDK for .NET is a powerful framework for constructing bots that can handle both freeform interactions and more guided ones where the possibilities are explicitly shown to the user. 
It is easy to use and leverages C# to provide a familiar way for .NET developers to write bots.

In this tutorial, we'll walk through the process of developing, testing, and publishing a bot that is created using the Bot Builder SDK for .NET.

## Prerequisites

> [!NOTE]
> Draft content.
> 
> To do: explain that the Bot Application template provides the code for the bot. 
> (We'll walk through that code later in this tutorial.) Or, alternatively -- perhaps do NOT use the 
> Application Template and instead walk through the steps required to create a project from scratch?

1. Install Visual Studio 2015 (latest update) - you can download the community version here for free: www.visualstudio.com

2. Update all VS extensions to their latest versions Tools->Extensions and Updates->Updates

3. Download and install the Bot Application template.

    - Download the file from the direct download link [here](http://aka.ms/bf-bc-vstemplate)
    - Save the zip file to your Visual Studio 2015 templates directory which is traditionally in "%USERPROFILE%\Documents\Visual Studio 2015\Templates\ProjectTemplates\Visual C#\"

## Create a new project and get the SDK

> [!NOTE]
> Draft content.
>  
> In this tutorial, we'll install the SDK using NuGet Package Manager in Visual Studio...
> SDK source is available here: https://github.com/Microsoft/botbuilder. 

1. Open Visual Studio.

2. Create a new C# project using the new Bot Application template. 

3. Update the Microsoft.Bot.Builder package to the latest version.
    -  Right-click on your project and select "Manage NuGet Packages".
    - In the "Browse" tab, type "Microsoft.Bot.Builder".
    - Click the "Upgrade" button and accept the changes.

## Create your bot

> [!NOTE]
> Draft content.
> 
> Either explain the code that you get 'for free' via the Application Template,
> or walk through creating all that code from scratch (i.e., without using the Application Template).

## Test your bot

> [!NOTE]
> Draft content.
> 
> To do: create 'snippet' file(s) to share common content between the article and Node.js get started article?

Next, let's test your bot by using the [Bot Framework Emulator](bot-framework-emulator.md) to see it in action. 
The emulator is a desktop application that lets you test and debug your bot on localhost or running remotely through a tunnel. 
You'll need to install the the emulator by following the instructions [here](bot-framework-emulator.md).

After installing the emulator, start your bot via Visual Studio by using a browser as the application host. 

> [!NOTE]
> To do: add image (screenshot) from [here](https://docs.botframework.com/en-us/csharp/builder/sdkreference/gettingstarted.html) -- under "Emulator".
>
> To do: add other descriptive content about this process in Visual Studio (from same page linked to above).

At this point, your bot is running locally. Next, start the emulator and then connect your bot by completing the following tasks in the emulator:
1. Select http://localhost:3979/api/messages from the address bar. (This is the default endpoint that your bot listens to when hosted locally.)
2. Click **Connect**. (You won't need to specify **Microsoft App ID** and **Microsoft App Password** -- you'll get this information later if/when you register your bot with the framework.)

Now that your bot is running locally and is connected to the emulator, test your bot by typing a few messages in the emulator. 
You should see that the bot responds to each message you send by echoing back your message prefixed with the text *You sent* 
and ending with the text *which was ## characters* (where *##* is the total number of characters in the message that you sent). 
Congratulations -- you've successfully created a bot using the Bot Builder SDK for .NET! 

## Next steps

[!include[Get started next steps after testing](../includes/snippet-getstarted-next-steps.md)]

## Dive deeper
In this tutorial, you created a simple bot using the Bot Builder SDK for .NET. 
To continue getting started and learn more about building great bots, see: 

> [!NOTE]
> Content coming soon. 
> TO_DO: add list of related topics