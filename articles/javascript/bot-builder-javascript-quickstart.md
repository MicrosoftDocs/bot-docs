---
title: Create a bot using Bot Framework SDK for JavaScript | Microsoft Docs
description: Quickly create a bot using the Bot Framework SDK for JavaScript.
keywords: quickstart, bot framework sdk, getting started
author: jonathanfingold
ms.author: v-jofing
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 10/30/2018
monikerRange: 'azure-bot-service-4.0'
---

# Create a bot with the Bot Framework SDK for JavaScript

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

This quickstart walks you through building a single bot by using the Yeoman Bot Builder generator and the Bot Framework SDK for JavaScript, and then testing it with the Bot Framework Emulator.

## Prerequisites

- [Visual Studio Code](https://www.visualstudio.com/downloads)
- [Node.js](https://nodejs.org/)
- [Yeoman](http://yeoman.io/), which uses a generator to create a bot for you
- [git](https://git-scm.com/)
- [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator)
- Knowledge of [restify](http://restify.com/) and asynchronous programming in JavaScript

> [!NOTE]
> For some installations the install step for restify is giving an error related to node-gyp.
> If this is the case try running this command with elevated permissions:
> ```bash
> npm install -g windows-build-tools
> ```

## Create a bot

To create your bot and initialize its packages

1. Open a terminal or elevated command prompt.
1. If you don't already have a directory for your JavaScript bots, create one and change directories to it. (We're creating a directory for your JavaScript bots in general, even though we're only creating one bot in this tutorial.)

   ```bash
   md myJsBots
   cd myJsBots
   ```

1. Ensure your version of npm is up to date.

   ```bash
   npm install -g npm
   ```

1. Next, install Yeoman and the generator for JavaScript.

   ```bash
   npm install -g yo generator-botbuilder
   ```

1. Then, use the generator to create an echo bot.

   ```bash
   yo botbuilder
   ```

Yeoman prompts you for some information with which to create your bot. For this tutorial, use the default values.

- Enter a name for your bot. (myChatBot)
- Enter a description. (Demonstrate the core capabilities of the Microsoft Bot Framework)
- Choose the language for your bot. (JavaScript)
- Choose the template to use. (Echo)

Thanks to the template, your project contains all of the code that's necessary to create the bot in this quickstart. You won't actually need to write any additional code.

> [!NOTE]
> If you choose to create a `Basic` bot, you'll need a LUIS language model. You can create one on [luis.ai](https://www.luis.ai). After creating the model, update the .bot file. Your bot file should look similar to this [one](../v4sdk/bot-builder-service-file.md).

## Start your bot

In a terminal or command prompt change directories to the one created for your bot, and start it with `npm start`. At this point, your bot is running locally.

## Start the Emulator and connect your bot

1. Start the Bot Framework Emulator.
2. Click the **Open Bot** link in the emulator "Welcome" tab.
3. Select the .bot file located in the directory where you created the project.

Send a message to your bot, and the bot will respond back with a message.
![Emulator running](../media/emulator-v4/js-quickstart.png)

## Additional resources

See [tunneling (ngrok)](https://github.com/Microsoft/BotFramework-Emulator/wiki/Tunneling-(ngrok)) for how to connect to a bot hosted remotely.

## Next steps

> [!div class="nextstepaction"]
> [How bots work](../v4sdk/bot-builder-basics.md)
