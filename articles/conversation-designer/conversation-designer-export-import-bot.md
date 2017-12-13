---
title: Import and export a Conversation Designer bot | Microsoft Docs
description: Learn how to import and export Conversation Designer bots.
author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 12/13/2017
ROBOTS: NoIndex, NoFollow
---

# Export and import a Conversation Designer bot

Bots in Conversation Designer can be exported to your computer as a .zip file. This allows you to backup your bot. You can later restore your bot to a previous state using any one of the exported .zip file. 

## Export a Conversation Designer bot

Exporting allows you to save the current state of your Conversation Designer bot to your local computer. 

To export a Conversation Designer bot, do the following:
1. On the upper-right corner of the navigation panel, click the ellipses (**...**).
2. Click **Export**. The server will gather the necessary information and return with an option for you to save the .zip file.
3. Save the exported .zip file to your local computer.

Your bot is now saved in a .zip file on your computer. You can later restore the bot to this state by [importing](#import-a-conversation-designer-bot) it back into the bot.

## Import a Conversation Designer bot

Importing allows you to restore the Conversation Designer bot to a previous state. Importing will replace the current bot with the importing bot. If you do not want to loose what you currently have in the current bot then make sure you [export](#export-a-conversation-designer-bot) the current bot before performing an import operation.

To import a Conversation Designer bot, do the following:
1. On the upper-right corner of the navigation panel, click the ellipses (**...**).
2. Click **Import**. 
3. If you want to save your work in the current bot, choose the **Backup and import** option. This option will save the current bot to your local computer then it will ask you for the location of the .zip file to import. Otherwise, choose **Import without backing up**.

Your bot is now imported.

> [!NOTE]
> You can only import bots that are exported by Conversation Designer.

## Import a LUIS app into a Conversation Designer bot

If you have a LUIS app and you would like to use it in your Conversation Designer bot, you can import your LUIS app into your Conversation Designer bot. Conceptually, this process requires you to export your LUIS app and Conversation Designer bot then swap the content of your bot's **luis.model** file with your **luis.json** file. Then, import your changes back into your Conversation Designer bot. Essentially, replacing the LUIS intents in your bot with that of your LUIS app. Because of this, it is advised that you perform this import operation before you start to customize your bot's LUIS intents; otherwise, all your work will be overwritten by this import operation.

> [!NOTE]
> If you are making changes in a [LUIS](https://luis.ai) app that is associated with your bot (each Conversation Designer bot has a corresponding LUIS app), you do not need to perform these steps. Instead, all you need to do is to go into your Conversation Designer bot and click [**Save**](conversation-designer-save-bot.md).

To import your LUIS app into your Conversation Designer bot, do the following:

1. From [LUIS.ai](https://luis.ai), open your LUIS app then click, **SETTINGS**.
2. Choose the **Versions** of the app you want to use and click the **{ }** action icon. This action will download the **luis.json** file to your local computer. 
3. From Conversation Designer, either [create a new bot](conversation-designer-create-bot.md#create-a-conversation-designer-bot) or open an existing bot.
4. [Export](#export-a-conversation-designer-bot) your bot. This will export your bot as .zip file to your computer.
5. Navigate to your exported .zip file and extract it.
6. Open the **luis.model** file in a text editor and replace the content of this file with the content of the **luis.json** file. Save the file.
7. Zip up the folder of your Conversation Designer bot.
8. [Import](#import-a-conversation-designer-bot) your bot back into your Conversation Designer bot.

Your bot can now use the new LUIS intents you just imported.

## Next step
> [!div class="nextstepaction"]
> [Tasks](conversation-designer-tasks.md)
