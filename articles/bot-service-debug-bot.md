---
title: How to debug a bot - Bot Service
description: Learn how to use the Bot Framework Emulator to debug bots. See how to set breakpoints in IDEs and how to exchange messages with bots during debugging.
author: v-ducvo
ms.author: kamrani
keywords: Bot Framework SDK, debug bot, test bot, bot emulator, emulator
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 11/17/2020
monikerRange: "azure-bot-service-4.0"
---

# Debugging a bot

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to debug your bot using an integrated development environment (IDE) such as Visual Studio or Visual Studio Code and the Bot Framework Emulator. While you can use these methods to debug any bot locally, this article uses an echo bot, such as the one created in the [Create a bot](bot-service-quickstart-create-bot.md) quickstart.

> [!NOTE]
> In this article, we use the Bot Framework Emulator to send and receive messages from the bot during debugging. If you are looking for other ways to debug your bot using the Bot Framework Emulator, please read the [Debug with the Bot Framework Emulator](bot-service-debug-emulator.md) article.

## Prerequisites

- Download and install the [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/blob/master/README.md).
- Download and install [Visual Studio Code](https://code.visualstudio.com) or [Visual Studio](https://www.visualstudio.com/downloads) (Community Edition or above).

<!-- ### Debug a JavaScript bot using command-line and Emulator

To run a JavaScript bot using the command line and testing the bot with the Emulator, do the following:
1. From the command line, change directory to your bot project directory.
1. Start the bot by running the command **node app.js**.
1. Start the Emulator and connect to the bot's endpoint (e.g.: **http://localhost:3978/api/messages**). If this is the first time you are running
the bot then click **File > New Bot** and follow the instructions on screen. Otherwise, click **File > Open Bot** to open an existing bot.
Since this bot is running locally on your computer, you can leave the **MicrosoftAppId** and **MicrosoftAppPassword** fields blank.
For more information, see [Debug with the Emulator](bot-service-debug-emulator.md).
1. From the Emulator, send your bot a message (e.g.: send the message "Hi").
1. Use the **Inspector** and **Log** panels on the right side of the Emulator window to debug your bot. For example, clicking on any of the messages bubble (e.g.: the "Hi" message bubble in the screenshot below) will show you the detail of that message in the **Inspector** panel. You can use it to view requests and responses as messages are exchanged between the Emulator and the bot. Alternatively, you can click on any of the linked text in the **Log** panel to view the details in the **Inspector** panel.

   ![Inspector panel on the Emulator](media/bot-service-debug-bot/emulator_inspector.png) -->

## [C#](#tab/csharp)

[!INCLUDE [csharp vscode](includes/bot-service-debug-bot/csharp-vscode.md)]

[!INCLUDE [csharp visual studio](includes/bot-service-debug-bot/csharp-vs.md)]

## [JavaScript](#tab/javascript)

[!INCLUDE [javascript vscode](includes/bot-service-debug-bot/js-vscode.md)]

## [Java](#tab/java)

[!INCLUDE [java vscode](includes/bot-service-debug-bot/java-vscode.md)]

## [Python](#tab/python)

[!INCLUDE [python vscode](includes/bot-service-debug-bot/python-vscode.md)]

---

## Additional resources

- See [troubleshoot general problems](bot-service-troubleshoot-bot-configuration.md) and the other troubleshooting articles in that section.
- See the how to [Debug with the Emulator](bot-service-debug-emulator.md).

## Next steps

> [!div class="nextstepaction"]
> [Debug your bot using transcript files](v4sdk/bot-builder-debug-transcript.md).
