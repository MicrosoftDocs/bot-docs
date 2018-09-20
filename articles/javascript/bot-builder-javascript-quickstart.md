---
title: Create a bot using Bot Builder SDK for JavaScript | Microsoft Docs
description: Quickly create a bot using the Bot Builder SDK for JavaScript.
keywords: quickstart, bot builder sdk, getting started
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 08/30/2018
monikerRange: 'azure-bot-service-4.0'
---

# Create a bot with the Bot Builder SDK for JavaScript

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

This quickstart walks you through building a bot by using the Yeoman Bot Builder generator and the Bot Builder SDK for JavaScript, and then testing it with the Bot Framework Emulator. 

## Prerequisites

- [Visual Studio Code](https://www.visualstudio.com/downloads)
- [Node.js](https://nodejs.org/en/)
- [Yeoman](http://yeoman.io/), which can use a generator to create a bot for you
- [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator)
- Knowledge of [restify](http://restify.com/) and asynchronous programming in JavaScript

> [!NOTE]
> For some installations the install step for restify is giving an error related to node-gyp.
> If this is the case try running `npm install -g windows-build-tools`.

The Bot Builder SDK for JavaScript consists of a series of [packages](https://github.com/Microsoft/botbuilder-js/tree/master/libraries) which can be installed from NPM.

## Create a bot

Open an elevated command prompt, create a directory, and initialize the package for your bot.

```bash
md myJsBots
cd myJsBots
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
- Choose the template to use. Currently, `Echo` is the only template, but others will be added soon.

Yeoman creates your bot in a new folder. When you open your newly created bot folder, you will see an `app.js` file. This `app.js` file will contain all the code needed to run a bot app. This file contains an echo bot that will echo back whatever you input as well as increment a counter.

Thanks to the template, your project contains all of the code that's necessary to create the bot in this quickstart. You won't actually need to write any additional code.

## Start your bot

Change directories to the one created for your bot, and start it.

```bash
cd <bot directory>
node app.js
```
At this point, your bot is running locally. 

## Start the emulator and connect your bot
1. Start the emulator.
2. Click the **Open Bot** link in the emulator "Welcome" tab.
3. Select the .bot file located in the directory where you created the project.

Send a message to your bot, and the bot will respond back with a message.
![Emulator running](../media/emulator-v4/emulator-running.png)

## Next steps
<<<<<<< HEAD

Next, [deploy your bot to azure](../bot-builder-howto-deploy-azure.md) or jump into the concepts that explain a bot and how it works.

=======
>>>>>>> origin/Ignite2018
> [!div class="nextstepaction"]
> [Anatomy of a bot](../v4sdk/bot-builder-anatomy.md) 
