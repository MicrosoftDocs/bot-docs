---
title: Create a bot with the Bot Builder SDK for Node.js | Microsoft Docs
description: Create a bot with the Bot Builder SDK for Node.js, a powerful bot construction framework.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: get-started-article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---

# Create a bot with the Bot Builder SDK for Node.js
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-quickstart.md)
> - [Node.js](../nodejs/bot-builder-nodejs-quickstart.md)
> - [Azure Bot Service](../azure/azure-bot-service-quickstart.md)
> - [REST](../rest-api/bot-framework-rest-connector-quickstart.md)

The Bot Builder SDK for Node.js is a framework for developing bots. It is easy to use and models frameworks like Express & restify to provide a familiar way for JavaScript developers to write bots.

This tutorial walks you through building a bot by using the Bot Builder SDK for Node.js, and then testing it with the Bot Framework Emulator.

## Prerequisites
Get started by completing the following prerequisite tasks:

1. Install [Node.js](https://nodejs.org).
2. Create a folder for your bot.
3. From a command prompt or terminal, navigate to the folder you just created.
4. Run the following **npm** command:
```
npm init
```

Follow the on screen prompt to enter information about your bot and npm will create a **package.json** file that contains the information you provided. 

## Install the SDK
Next, install the Bot Builder SDK for Node.js and <a href="http://restify.com/" target="_blank">restify</a> by running the following **npm** commands:

   ```
   npm install --save botbuilder
   npm install --save restify
   ```

Once you have the SDK and Restify in place, you're ready to write your bot.

## Create your bot

For your first bot, you will create a bot that simply echoes back any user input. To create your bot, follow these steps:

1. In the folder that you created earlier for your bot, create a new file named **app.js**.
2. Open **app.js** in a text editor or an IDE of your choice. Add the following code to the file: 

   [!code-javascript[echobot code sample Node.js](~/includes/code/node-getstarted.js#echobot)]

3. Save the file. Now you are ready to run and test out your bot.

## Test your bot
Next, test your bot by using the [Bot Framework Emulator](~/debug-bots-emulator.md) to see it in action. The emulator is a desktop application that lets you test and debug your bot on localhost or running remotely through a tunnel.

First, you'll need to [download](https://emulator.botframework.com) and install the emulator. After the download completes, launch the executable and complete the installation process.

### Start your bot

After installing the emulator, navigate to your bot's directory in a console window and start your bot:

```
node app.js
```
   
Your bot is now running locally.

### Start the emulator and connect your bot
After you start your bot, connect to your bot in the emulator:

1. Type `http://localhost:3978/api/messages` into the address bar. (This is the default endpoint that your bot listens to when hosted locally.)
2. Click **Connect**. You won't need to specify **Microsoft App ID** and **Microsoft App Password**. You can leave these fields blank for now. You'll get this information later when you [register your bot](~/portal-register-bot.md).

### Try out your bot

Now that your bot is running locally and is connected to the emulator, try out your bot by typing a few messages in the emulator.
You should see that the bot responds to each message you send by echoing back your message prefixed with the text *"You said:"*.

You've successfully created your first bot using the Bot Builder SDK for Node.js!

## Next steps

To learn more about building great bots with the Bot Framework, see the following articles:

- [How the Bot Framework works](~/overview-how-bot-framework-works.md)
- [Principles of bot design](~/bot-design-principles.md)
- [Bot Builder SDK for Node.js](~/nodejs/index.md)
- [Deploy and publish bots](~/publish-bot-overview.md)
- [Bot Builder SDK for Node.js samples](bot-builder-nodejs-samples.md)
- [Bot Framework FAQ](~/resources-bot-framework-faq.md)

