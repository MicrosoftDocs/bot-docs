---
title: Bot Framework SDK for .NET | Microsoft Docs
description: Get started with Bot Framework SDK for .NET, a powerful, easy-to-use framework for building bots.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Bot Framework SDK for .NET

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-overview.md)
> - [Node.js](../nodejs/bot-builder-nodejs-overview.md)
> - [REST](../rest-api/bot-framework-rest-overview.md)

The Bot Framework SDK for .NET is a powerful framework for constructing bots that can handle both free-form interactions 
and more guided conversations where the user selects from possible values. 
It is easy to use and leverages C# to provide a familiar way for .NET developers to write bots.

Using the SDK, you can build bots that take advantage of the following SDK features: 

- Powerful dialog system with dialogs that are isolated and composable
- Built-in prompts for simple things such as Yes/No, strings, numbers, and enumerations
- Built-in dialogs that utilize powerful AI frameworks such as <a href="http://luis.ai" target="_blank">LUIS</a>
- FormFlow for automatically generating a bot (from a C# class) that guides the user through the 
conversation, providing help, navigation, clarification, and confirmation as needed

> [!IMPORTANT]
> On July 31, 2017 breaking changes have been implemented in the Bot Framework security protocol. 
> To prevent these changes from adversely impacting your bot, you must ensure that your application is 
> using Bot Framework SDK v3.5 or greater. If you've built a bot using an 
> SDK that you obtained prior to January 5, 2017 (the release date for Bot Framework SDK v3.5), 
> be sure to upgrade to Bot Framework SDK v3.5 or later.

## Get the SDK

The SDK is available as a NuGet package and as open source on <a href="https://github.com/Microsoft/BotBuilder" target="_blank">GitHub</a>.

> [!IMPORTANT]
> The Bot Framework SDK for .NET requires .NET Framework 4.6 or newer. If you are adding the SDK to an existing project
> targeting a lower version of the .NET Framework, you will need to update your project to target .NET Framework 4.6 first.

To install the SDK within a Visual Studio project, complete the following steps:

1. In **Solution Explorer**, right-click the project name and select **Manage NuGet Packages...**.
2. On the **Browse** tab, type "Microsoft.Bot.Builder" into the search box.
3. Select **Microsoft.Bot.Builder** in the list of results, click **Install**, and accept the changes.

## Get code samples

This SDK includes [sample source code](bot-builder-dotnet-samples.md) that uses the features of the Bot Framework SDK for .NET.

## Next steps

Learn more about building bots using the Bot Framework SDK for .NET by reviewing articles throughout this section, beginning with:

- [Quickstart](bot-builder-dotnet-quickstart.md): Quickly build and test a simple bot by following instructions in this step-by-step tutorial.
- [Key concepts](bot-builder-dotnet-concepts.md): Learn about key concepts in the Bot Framework SDK for .NET.

If you encounter problems or have suggestions regarding the Bot Framework SDK for .NET, see [Support](../bot-service-resources-links-help.md) for a list of available resources. 
