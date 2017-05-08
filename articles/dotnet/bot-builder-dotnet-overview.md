---
title: Bot Builder SDK for .NET | Microsoft Docs
description: Get started with Bot Builder SDK for .NET, a powerful, easy-to-use framework for building bots.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---

# Bot Builder SDK for .NET
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-overview.md)
> - [Node.js](../nodejs/bot-builder-nodejs-overview.md)

The Bot Builder SDK for .NET is a powerful framework for constructing bots that can handle both free-form interactions 
and more guided conversations where the user selects from possible values. 
It is easy to use and leverages C# to provide a familiar way for .NET developers to write bots.

Using the SDK, you can build bots that take advantage of the following SDK features: 

- Powerful dialog system with dialogs that are isolated and composable
- Built-in prompts for simple things such as Yes/No, strings, numbers, and enumerations
- Built-in dialogs that utilize powerful AI frameworks such as <a href="http://luis.ai" target="_blank">LUIS</a>
- FormFlow for automatically generating a bot (from a C# class) that guides the user through the 
conversation, providing help, navigation, clarification, and confirmation as needed

> [!IMPORTANT]
> On July 31, 2017 breaking changes will be implemented in the Bot Framework security protocol. 
> To prevent these changes from adversely impacting your bot, you must ensure that your application is 
> using Bot Builder SDK v3.5 or greater. If you've built a bot using an 
> SDK that you obtained prior to January 5, 2017 (the release date for Bot Builder SDK v3.5), 
> be sure to upgrade to Bot Builder SDK v3.5 or later before July 31, 2017.

## Get the SDK

The SDK is available as a NuGet package and as open source on <a href="https://github.com/Microsoft/BotBuilder" target="_blank">GitHub</a>. 
To install the SDK within a Visual Studio project, complete the following steps:

1. In **Solution Explorer**, right-click the project name and select **Manage NuGet Packages...**.
2. On the **Browse** tab, type "Microsoft.Bot.Builder" into the search box.
3. Select **Microsoft.Bot.Builder** in the list of results, click **Install**, and accept the changes.

## Get code samples

The <a href="https://github.com/Microsoft/BotBuilder" target="_blank">BotBuilder</a> GitHub repository 
contains numerous code samples that show how to build bots using the Bot Builder SDK for .NET. 
To access these code samples, clone the repository and navigate to the **Samples** folder.

```
git clone https://github.com/Microsoft/BotBuilder.git
cd BotBuilder/CSharp/Samples
```

The <a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">BotBuilder-Samples</a> GitHub repository 
contains numerous task-focused code samples that show how to use various features of the Bot Builder SDK for .NET. 
To access these code samples, clone the repository and navigate to the **CSharp** folder.

```
git clone https://github.com/Microsoft/BotBuilder-Samples.git
cd BotBuilder-Samples/CSharp
```

## Next steps

Learn more about building bots using the Bot Builder SDK for .NET by reviewing articles throughout this section, beginning with:

- [Quickstart](~/dotnet/bot-builder-dotnet-quickstart.md): Quickly build and test a simple bot by following instructions in this step-by-step tutorial.
- [Key concepts](~/dotnet/bot-builder-dotnet-concepts.md): Learn about key concepts in the Bot Builder SDK for .NET.

If you encounter problems or have suggestions regarding the Bot Builder SDK for .NET, see [Support](~/resources-support.md) for a list of available resources. 
