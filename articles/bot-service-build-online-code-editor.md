---
title: Build a bot with the Azure online code editor | Microsoft Docs
description: Learn how to build your bot using the Online code editor in Bot Service. 
keywords: online code editor, azure portal, functions bot
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: tools
ms.date: 09/21/2018
---

# Edit a bot with online code editor

You can use the online code editor to build your bot without needing an IDE. This topic will show you how to open your bot code in the online code editor. 

## Edit bot source code in online code editor

To edit a bot's source code in the online code editor, do the following for the specific type of bot you have.

### Web App Bot
1. Sign into the [Azure portal](http://portal.azure.com) and open the blade for the bot.
2. Under the **Bot Management** section, Click **Build**.
3. Click **Open online code editor**. This will open the bot's code in a new browser window. 

   ![Open online code editor](~/media/azure-bot-build/open-online-code-editor.png)

   Depending on the language of the bot, the file structure under the **WWWRoot** directory will be different. For example, if you have a C# bot, your **WWWRoot** may look something like this:

   ![C# file structure](~/media/azure-bot-build/cs-wwwroot-structure.png)

   If you have a Node.js bot, your **WWWRoot** may look something like this:

   ![Node.js file structure](~/media/azure-bot-build/node-wwwroot-structure.png)

4. Make code changes. For example, for C# bots, you can a .cs file. For Node.js bots, you can start with the App.js file.

   > [!NOTE]
   > While you can make code changes to current source files in the project, it is not possible to create new source files using the online code editor. To add new source files to the bot, you need to [download the source](bot-service-build-download-source-code.md) project, add your files and publish the changes back to Azure.

5. For C# bots that are on a **Consumption plan** and all Node.js bots, the bot is automatically saved. 

6. For C# bots on an **App service** plan, open the **Console** blade and send the **build.cmd** command. 

   ![Build project in console blade](~/media/azure-bot-build/cs-console-build-cmd.png)
 
   > [!NOTE]
   > If this command fails to build, try restarting your bot's app service and try building again. To restart your app service, from your bot's blade, click **All App service settings** then click the **Restart** button.
   > ![Restart a web app](~/media/azure-bot-build/open-online-code-editor-restart-appservice.png)

7. Switch back to Azure portal and click **Test in Web Chat** to test out your changes. If you already have the Web Chat open for this bot, click **Start over** to see the new changes.

### Functions Bot

1. Sign into the [Azure portal](http://portal.azure.com) and open the blade for the bot.
2. Under the **Bot Management** section, Click **Build**.
3. Click **Open this bot in Azure Functions**. This will open the bot with the <a href="http://go.microsoft.com/fwlink/?linkID=747839" target="_blank">Azure Functions</a> UI. 
4. Make code changes. For example, update the function's messages code. The screen shot below shows the Messages code for a Node.js Functions Bot.

   ![Functions Bot Messages code editor](~/media/azure-bot-build/functions-messages-code.png)

5. Save your code changes.
6. Switch back to Azure portal and click **Test in Web Chat** to test out your changes. If you already have the Web Chat open for this bot, click **Start over** to see the new changes.

## Next steps
Now that you know how to edit your bot code using the Online Code Editor, you can also build your bot locally using your favorite IDE.

> [!div class="nextstepaction"]
> [Download the bot source code](bot-service-build-download-source-code.md)
