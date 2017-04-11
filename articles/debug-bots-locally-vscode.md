---
title: Debug locally with Visual Studio Code | Microsoft Docs
description: Learn how to use Visual Studio Code to debug a bot built using the Bot Builder SDK.
keywords:
author: DeniseMak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 02/21/2017
ms.reviewer: 
#ROBOTS: Index
---
# Debug a bot with Visual Studio Code
The [Bot Framework Emulator](~/debug-bots-emulator.md) provides useful features for debugging your bots. You can also use the debugger included in [Visual Studio Code](https://code.visualstudio.com/) to debug your your bot's source code. 

> [!NOTE]
> Visual Studio Code has built-in debugging support for the Node.js runtime. 
> This article guides you through debugging a bot built with Node.js. For debugging a bot built with C#, you will need the C# extension. You can find more information in the [VS Code documentation][VSCodeDebug]. 

The Bot Builder SDK for Node.js provides the [ConsoleConnector][ConsoleConnector] class to help you debug your bot by running it in a console window. This guide will walk you through that process.

## Launch VS Code
For purposes of this walkthrough we’ll use the [ConsoleConnector](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/hello-ConsoleConnector) example from the Bot Builder SDK for Node.js. After you install VS Code on your machine, open your bot's project in VS Code by using **Open Folder** in the File menu.

![Step 1: Launch VS Code](~/media/debug-vscode/builder-debug-step1.png)

## Launch your bot
The ConsoleConnector example illustrates running a bot on multiple platforms which is the key to debugging your bot locally. To debug locally you need a version of your bot that can run from a console window using the [ConsoleConnector][ConsoleConnector] class. For the TodoBot we can run it locally by launching the textBot.js class. To debug this class using VS Code, launch Node.js with the `\-\-debug-brk` flag, which causes it to immediately break. From a console window, type `node \-\-debug-brk textBot.js`.

![Step 2: Launch Bot](~/media/debug-vscode/builder-debug-step2.png)

## Configure VS Code
Before you can debug your paused bot, you’ll need to configure the VS Code Node.js debugger. VS Code knows your project is using Node.js, but there are multiple configurations for launching Node.js, so you'll go through a one-time setup for each project (that is, each folder).  

To set up the debugger:
1. Select the debug tab on the lower left and click the run button. 
2. VS Code prompts you to pick your debug environment. Select **Node.js**. You don't need to change the default settings.
3. You should see a .vscode folder added to your project. If you don't want this checked into your Git repository, add a '/**/.vscode' entry to your .gitignore file.

![Step 3: Configure VS Code](~/media/debug-vscode/builder-debug-step3.png)

## Attach the debugger
Configuring the debugger added two debug modes: **Launch** and **Attach**. Since our bot is paused in a console window, select the **Attach** mode and click the run button again.

![Step 4: Attach Debugger](~/media/debug-vscode/builder-debug-step4.png)

## Debug your bot
When VS Code attaches to your bot, it pauses on the first line of code. Now you’re ready to set breakpoints and debug your bot! 
You can communicate with your bot from the console window. Try switching back to the console window that your bot is running in and say “hello”.

![Step 5: Debug Bot](~/media/debug-vscode/builder-debug-step5.png)

[ConsoleConnector]: (https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.consoleconnector.html)

[VSCodeDebug]: (https://code.visualstudio.com/docs/editor/debugging)