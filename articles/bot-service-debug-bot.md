---
title: How to debug a Bot Framework SDK bot
description: Learn how to use Bot Framework Emulator to debug bots. See how to set breakpoints in IDEs and how to exchange messages with bots during debugging.
keywords: Bot Framework SDK, debug bot, test bot, bot emulator, emulator
author: kunsinghms
ms.author: kunsingh
manager: shellyha
ms.reviewer: pehecke
ms.topic: how-to
ms.service: azure-ai-bot-service
monikerRange: "azure-bot-service-4.0"
ms.custom:
  - evergreen
ms.update-cycle: 1095-days
---

# Debug an SDK-first bot

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to debug your bot using an IDE, such as Visual Studio or Visual Studio Code, and the Bot Framework Emulator. Use these methods to debug a bot locally. This article uses an echo bot, such as the one created in the [Create a bot](bot-service-quickstart-create-bot.md) quickstart.

> [!NOTE]
> In this article, we use the Bot Framework Emulator to send and receive messages from the bot during debugging. If you're looking for other ways to debug your bot using the Bot Framework Emulator, please read the [Debug with the Bot Framework Emulator](bot-service-debug-emulator.md) article.

[!INCLUDE [java-python-sunset-alert](includes/java-python-sunset-alert.md)]

## Prerequisites

- Download and install the [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/blob/master/README.md).
- Download and install [Visual Studio Code](https://code.visualstudio.com) or [Visual Studio](https://www.visualstudio.com/downloads).

## [C#](#tab/csharp)

### Set C# breakpoints in Visual Studio Code

In Visual Studio Code, you can set breakpoints and run the bot in debug mode to step through your code. To set breakpoints in Visual Studio Code, do the following:

1. Launch Visual Studio Code and open your bot project folder.
1. Set breakpoints as necessary. To set a breakpoint, hover your mouse over the column to the left of the line numbers. A small red dot will appear. If you select the dot, the breakpoint is set. If you select the dot again, the breakpoint is removed.

   :::image type="content" source="media/bot-service-debug-bot/csharp-breakpoint-set.png" alt-text="A screenshot of a C# breakpoint set in Visual Studio Code.":::

1. From the menu bar, select **Run**, then **Start Debugging**. Your bot will start running in debugging mode from the Terminal in Visual Studio Code.
1. Start the Bot Framework Emulator and connect to your bot as described in how to [Debug with the Bot Framework Emulator](bot-service-debug-emulator.md).
1. From the Emulator, send your bot a message (for example, send the message "Hi"). Execution will stop at the line where you place the breakpoint.

   :::image type="content" source="media/bot-service-debug-bot/breakpoint-caught.png" alt-text="A screenshot of a C# bot in Visual Studio Code, paused at a break point.":::

### Set C# breakpoints in Visual Studio

In Visual Studio, you can set breakpoints and run the bot in debug mode to step through your code. To set breakpoints in Visual Studio, do the following:

1. Navigate to your bot folder and open the **.sln** file. This will open the solution in Visual Studio.
1. From the menu bar, select **Build** and select **Build Solution**.
1. In the **Solution Explorer**, select the **.cs** file and set breakpoints as necessary. This file defines your main bot logic. To set a breakpoint, hover your mouse over the column to the left of the line numbers. A small dot will appear. If you select the dot, the breakpoint is set. If you select the dot again, the breakpoint is removed.

   :::image type="content" source="media/bot-service-debug-bot/breakpoint-set-vs.png" alt-text="A screenshot of a C# breakpoint set in Visual Studio.":::

1. From the menu, select **Debug**, then **Start Debugging**. At this point, the bot is running locally.
1. Start the Bot Framework Emulator and connect to your bot as described in the section above.
1. From the Emulator, send your bot a message, such as "Hi". Execution will stop at the line where you place the breakpoint.

   :::image type="content" source="media/bot-service-debug-bot/breakpoint-caught-vs.png" alt-text="A screenshot of a C# bot in Visual Studio, paused at a break point.":::

## [JavaScript](#tab/javascript)

### Set JavaScript breakpoints in Visual Studio Code

In Visual Studio Code, you can set breakpoints and run the bot in debug mode to step through your code. To set breakpoints in Visual Studio Code, do the following:

1. Launch Visual Studio Code and open your bot project folder.
1. From the menu bar, select **Debug** and then select **Start Debugging**. If you're prompted to select a runtime engine to run your code, select **Node.js**. At this point, the bot is running locally.
1. Select the **.js** file and set breakpoints as necessary. To set a breakpoint, hover your mouse over the column to the left of the line numbers. A small red dot will appear. If you select the dot, the breakpoint is set. If you select the dot again, the breakpoint is removed.

   :::image type="content" source="media/bot-service-debug-bot/breakpoint-set.png" alt-text="A screenshot of a JavaScript breakpoint set in Visual Studio Code.":::

1. Start the Bot Framework Emulator and connect to your bot as described in the [Debug with the Bot Framework Emulator](bot-service-debug-emulator.md) article.
1. From the Emulator, send your bot a message (for example, send the message "Hi"). Execution will stop at the line where you place the breakpoint.

   :::image type="content" source="media/bot-service-debug-bot/breakpoint-caught.png" alt-text="A screenshot of a JavaScript bot in Visual Studio Code, paused at a break point.":::

## [Python](#tab/python)

### Set Python breakpoints in Visual Studio Code

In Visual Studio Code, you can set breakpoints and run the bot in debug mode to step through your code. See also [Create a bot with the Bot Framework SDK for Python](~/python/bot-builder-python-quickstart.md).

1. Install the [Python extension](https://marketplace.visualstudio.com/items?itemName=ms-python.python) in Visual Studio Code, if you haven't already done so. This extension provides rich support for Python in Visual Studio Code, including debugging.
1. Launch Visual Studio Code and open your bot project folder.
1. Set breakpoints as necessary. To set a breakpoint, hover your mouse over the column to the left of the line numbers. A small red dot will appear. If you select the dot, the breakpoint is set. If you select the dot again, the breakpoint is removed.

   :::image type="content" source="media/bot-service-debug-bot/bot-debug-python-breakpoints.png" alt-text="A screenshot of a Python breakpoint set in Visual Studio Code.":::

1. Select the `app.py` file.
1. From the menu bar, select **Debug** and then select **Start Debugging**.
1. Select **Python File** to debug the currently selected file.
1. Start the Bot Framework Emulator and connect to your bot as described in the [Debug with the Bot Framework Emulator](bot-service-debug-emulator.md) article.
1. From the Emulator, send your bot a message (for example, send the message "Hi"). Execution will stop at the line where you place the breakpoint.

   :::image type="content" source="media/bot-service-debug-bot/bot-debug-python-breakpoint-caught.png" alt-text="A screenshot of a Python bot in Visual Studio Code, paused at a break point.":::

For more information, see [Debug your Python code](/visualstudio/python/debugging-python-in-visual-studio).

---

## Additional resources

- [Troubleshoot bot configuration issues](bot-service-troubleshoot-bot-configuration.md) and the other troubleshooting articles in that section.
- [Debug with the Emulator](bot-service-debug-emulator.md).

## Next steps

> [!div class="nextstepaction"]
> [Debug your bot using transcript files](v4sdk/bot-builder-debug-transcript.md).
