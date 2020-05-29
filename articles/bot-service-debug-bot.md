---
title: Debug a bot - Bot Service
description: Learn how to debug a bot built using Bot Service.
author: v-ducvo
ms.author: kamrani
keywords: Bot Framework SDK, debug bot, test bot, bot emulator, emulator
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 2/26/2019
---

# Debug a bot

This article describes how to debug your bot using an integrated development environment (IDE) such as Visual Studio or Visual Studio Code and the Bot Framework Emulator. While you can use these methods to debug any bot locally, this article uses a [C#](~/dotnet/bot-builder-dotnet-sdk-quickstart.md), [Javascript](~/javascript/bot-builder-javascript-quickstart.md), or [Python](~/python/bot-builder-python-quickstart.md) bot created in the quickstart.

> [!NOTE]
> In this article, we use the Bot Framework Emulator to send and receive messages from the bot during debugging. If you are looking for other ways to debug your bot using the Bot Framework Emulator, please read the [Debug with the Bot Framework Emulator](https://docs.microsoft.com/azure/bot-service/bot-service-debug-emulator) article.

## Prerequisites

- Download and install the [Bot Framework Emulator](https://aka.ms/Emulator-wiki-getting-started).
- Download and install [Visual Studio Code](https://code.visualstudio.com) or [Visual Studio](https://www.visualstudio.com/downloads) (Community Edition or above).

<!-- ### Debug a JavaScript bot using command-line and emulator

To run a JavaScript bot using the command line and testing the bot with the emulator, do the following:
1. From the command line, change directory to your bot project directory.
1. Start the bot by running the command **node app.js**.
1. Start the emulator and connect to the bot's endpoint (e.g.: **http://localhost:3978/api/messages**). If this is the first time you are running
the bot then click **File > New Bot** and follow the instructions on screen. Otherwise, click **File > Open Bot** to open an existing bot.
Since this bot is running locally on your computer, you can leave the **MicrosoftAppId** and **MicrosoftAppPassword** fields blank.
For more information, see [Debug with the Emulator](bot-service-debug-emulator.md).
1. From the emulator, send your bot a message (e.g.: send the message "Hi").
1. Use the **Inspector** and **Log** panels on the right side of the emulator window to debug your bot. For example, clicking on any of the messages bubble (e.g.: the "Hi" message bubble in the screenshot below) will show you the detail of that message in the **Inspector** panel. You can use it to view requests and responses as messages are exchanged between the emulator and the bot. Alternatively, you can click on any of the linked text in the **Log** panel to view the details in the **Inspector** panel.

   ![Inspector panel on the Emulator](~/media/bot-service-debug-bot/emulator_inspector.png) -->

::: moniker range="azure-bot-service-3.0"

## [C#](#tab/csharp)

[!INCLUDE [csharp vscode](../includes/bot-service-debug-bot/csharp-vscode.md)]
[!INCLUDE [csharp visual studio](../includes/bot-service-debug-bot/csharp-vs.md)]
[!INCLUDE [csharp consumption](../includes/bot-service-debug-bot/csharp-vscode.md)]

## [JavaScript](#tab/javascript)

[!INCLUDE [javascript vscode](../includes/bot-service-debug-bot/js-vscode.md)]

---

:::moniker-end
::: moniker range="azure-bot-service-4.0"

## [C#](#tab/csharp)

[!INCLUDE [csharp vscode](../includes/bot-service-debug-bot/csharp-vscode.md)]
[!INCLUDE [csharp visual studio](../includes/bot-service-debug-bot/csharp-vs.md)]

## [JavaScript](#tab/javascript)

[!INCLUDE [javascript vscode](../includes/bot-service-debug-bot/js-vscode.md)]

## [Python](#tab/python)

[!INCLUDE [javascript vscode](../includes/bot-service-debug-bot/python-vscode.md)]

---
:::moniker-end

## Additional resources

- See [troubleshoot general problems](bot-service-troubleshoot-bot-configuration.md) and the other troubleshooting articles in that section.
- See the how to [Debug with the Emulator](bot-service-debug-emulator.md).

## Next steps

> [!div class="nextstepaction"]
> [Debug your bot using transcript files](v4sdk/bot-builder-debug-transcript.md).
