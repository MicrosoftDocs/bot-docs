---
title: Create a bot - Bot Service
description: Create a bot with the Bot Framework SDK for .NET, 
keywords: Bot Framework SDK, create a bot, quickstart, .NET, Javascript, Java, Python, getting started, 
author: mmiele
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 01/02/2021
zone_pivot_groups: programming-languages-set-sdk
---

# Create a bot

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article shows how to build a bot using the Bot Framework SDK and how to test it with the Bot Framework Emulator.

## Prerequisites

::: zone pivot="programming-language-csharp"
[!INCLUDE [dotnet prerequisites](../includes/quickstart/dotnet/quickstart-dotnet-prerequisites.md)]
::: zone-end

::: zone pivot="programming-language-nodejs"
[!INCLUDE [javascript prerequisites](../includes/quickstart/javascript/quickstart-javascript-prerequisites.md)]
::: zone-end

::: zone pivot="programming-language-java"
[!INCLUDE [java prerequisites](../includes/quickstart/java/quickstart-java-prerequisites.md)]
::: zone-end

::: zone pivot="programming-language-python"
[!INCLUDE [python prerequisites](../includes/quickstart/python/quickstart-python-prerequisites.md)]
::: zone-end

## Create a bot

::: zone pivot="programming-language-csharp"
[!INCLUDE [dotnet quickstart create bot](../includes/quickstart/dotnet/quickstart-dotnet-create-bot.md)]
::: zone-end

::: zone pivot="programming-language-nodejs"
[!INCLUDE [javascript quickstart create bot](../includes/quickstart/javascript/quickstart-javascript-create-bot.md)]
::: zone-end

::: zone pivot="programming-language-java"
[!INCLUDE [java quickstart create bot](../includes/quickstart/java/quickstart-java-create-bot.md)]
::: zone-end

::: zone pivot="programming-language-python"
[!INCLUDE [python quickstart create bot](../includes/quickstart/python/quickstart-python-create-bot.md)]
::: zone-end

## Start your bot

::: zone pivot="programming-language-csharp"
[!INCLUDE [dotnet quickstart start bot](../includes/quickstart/dotnet/quickstart-dotnet-start-bot.md)]
::: zone-end

::: zone pivot="programming-language-nodejs"
[!INCLUDE [javascript quickstart start bot](../includes/quickstart/javascript/quickstart-javascript-start-bot.md)]
::: zone-end

::: zone pivot="programming-language-java"
[!INCLUDE [java quickstart start bot](../includes/quickstart/java/quickstart-java-start-bot.md)]
::: zone-end

::: zone pivot="programming-language-python"
[!INCLUDE [python quickstart start bot](../includes/quickstart/python/quickstart-python-start-bot.md)]
::: zone-end

## Start the Emulator and connect your bot

[!INCLUDE [start emulator](../includes/quickstart/common/quickstart-start-emulator.md)]

## Additional resources

- See the [.NET Core SDK Templates](https://github.com/microsoft/BotBuilder-Samples/tree/main/generators/dotnet-templates#readme) README for more information about the .NET Core templates.
- See how to [debug a bot](../bot-service-debug-channel-ngrok.md) for how to debug using Visual Studio or Visual Studio Code and the Bot Framework Emulator.
- See [Tunneling (ngrok)](https://github.com/Microsoft/BotFramework-Emulator/wiki/Tunneling-(ngrok)) for information on how to install ngrok.

## Next steps

> [!div class="nextstepaction"]
> [Deploy your bot to Azure](../bot-builder-deploy-az-cli.md)
