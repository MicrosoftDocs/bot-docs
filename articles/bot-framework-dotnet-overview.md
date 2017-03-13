---
title: Bot Builder SDK for .NET | Microsoft Docs
description: Learn about the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK
author: kbrandl
manager: rstand
ms.topic: develop-dotnet-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 03/08/2017
ms.reviewer:
#ROBOTS: Index
---

# Bot Builder SDK for .NET

## Introduction

The Bot Builder SDK for .NET is a powerful framework for constructing bots that can handle both free-form interactions 
and more guided conversations where the user selects from possible values. 
It is easy to use and leverages C# to provide a familiar way for .NET developers to write bots.

Using the SDK, you can build bots that are stateless (which enables them to scale) 
and take advantage of the following SDK features: 

- Powerful dialog system with dialogs that are isolated and composable
- Built-in prompts for simple things such as Yes/No, strings, numbers, and enumerations
- Built-in dialogs that utilize powerful AI frameworks such as <a href="http://luis.ai" target="_blank">LUIS</a>
- FormFlow for automatically generating a bot (from a C# class) that guides the user through the 
conversation, providing help, navigation, clarification, and confirmation as needed

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

Learn more about building bots using the Bot Builder SDK for .NET by 
reviewing articles throughout this section, beginning with:

- [Get Started](bot-framework-dotnet-getstarted.md): Quickly build and test a simple bot by following instructions in this step-by-step tutorial.
- [Key concepts](bot-framework-dotnet-concepts.md): Learn about key concepts in the Bot Builder SDK for .NET.

If you encounter problems or have suggestions regarding the Bot Builder SDK for .NET, 
see [Support](bot-framework-resources-support.md) for a list of available resources. 
