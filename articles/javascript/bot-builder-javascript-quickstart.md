---
title: Create a bot using Bot Builder SDK for JavaScript | Microsoft Docs
description: Quickly create a bot using the Bot Builder SDK for JavaScript.
keywords: quickstart, bot builder sdk, getting started
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 10/15/2018
monikerRange: 'azure-bot-service-4.0'
---

# Create a bot with the Bot Builder SDK for JavaScript

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

This quickstart walks you through building a bot by using the Yeoman Bot Builder generator and the Bot Builder SDK for JavaScript, and then testing it with the Bot Framework Emulator. 

## Prerequisites

- [Visual Studio Code](https://www.visualstudio.com/downloads)
- [Node.js](https://nodejs.org/)
- [Yeoman](http://yeoman.io/), which can use a generator to create a bot for you
- [git](https://git-scm.com/)
- [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator)
- Knowledge of [restify](http://restify.com/) and asynchronous programming in JavaScript

> [!NOTE]
> For some installations the install step for restify is giving an error related to node-gyp.
> If this is the case try running `npm install -g windows-build-tools`.

## Create a bot

Open an elevated command prompt, create a directory, and initialize the package for your bot.

```bash
md myJsBots
cd myJsBots
```

Ensure your version of npm is up to date.
```bash
npm i npm
```

Next, install Yeoman and the generator for JavaScript.

```bash
npm install -g yo
npm install -g generator-botbuilder
```

Then, use the generator to create an echo bot.

```bash
yo botbuilder
```

Yeoman prompts you for some information with which to create your bot.

- Enter a name for your bot.
- Enter a description.
- Choose the language for your bot, either `JavaScript` or `TypeScript`.
- Choose the `Echo` template.

Thanks to the template, your project contains all of the code that's necessary to create the bot in this quickstart. You won't actually need to write any additional code.

> [!NOTE]
> For a Basic bot, you'll need a LUIS language model. You can create one on [luis.ai](https://www.luis.ai). After creating the model, update the .bot file. Your bot file should look similar to this [one](../v4sdk/bot-builder-service-file.md). 

## Start your bot

In Powershell/Bash change directories to the one created for your bot, and start it by with `npm start`. At this point, your bot is running locally.

## Start the emulator and connect your bot
1. Start the emulator.
2. Click the **Open Bot** link in the emulator "Welcome" tab.
3. Select the .bot file located in the directory where you created the project.

Send a message to your bot, and the bot will respond back with a message.
![Emulator running](../media/emulator-v4/emulator-running.png)

## Next steps

> [!div class="nextstepaction"]
> [How bots work](../v4sdk/bot-builder-basics.md) 
