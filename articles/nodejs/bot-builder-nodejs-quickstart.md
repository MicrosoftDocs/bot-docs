---
redirect_url: /bot-framework/javascript/bot-builder-javascript-quickstart
---

<!--

title: Create a bot with the Bot Builder SDK for Node.js | Microsoft Docs
description: Create a bot with the Bot Builder SDK for Node.js, a powerful bot construction framework.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: get-started-article
ms.prod: bot-framework
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'


# Create a bot with the Bot Builder SDK for Node.js

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-quickstart.md)
> - [Node.js](../nodejs/bot-builder-nodejs-quickstart.md)
> - [Bot Service](../bot-service-quickstart.md)
> - [REST](../rest-api/bot-framework-rest-connector-quickstart.md)

The Bot Builder SDK for Node.js is a framework for developing bots. It is easy to use and models frameworks like Express & restify to provide a familiar way for JavaScript developers to write bots.

This tutorial walks you through building a bot by using the Bot Builder SDK for Node.js. You can test the bot in a console window and with the Bot Framework Emulator.

## Prerequisites
Get started by completing the following prerequisite tasks:

1. Install [Node.js](https://nodejs.org).
2. Create a folder for your bot.
3. From a command prompt or terminal, navigate to the folder you just created.
4. Run the following **npm** command:

```nodejs
npm init
```

Follow the prompt on the screen to enter information about your bot and npm will create a **package.json** file that contains the information you provided. 

## Install the SDK
Next, install the Bot Builder SDK for Node.js by running the following **npm** command:

```nodejs
npm install --save botbuilder
```

Once you have the SDK installed, you are ready to write your first bot.

For your first bot, you will create a bot that simply echoes back any user input. To create your bot, follow these steps:

1. In the folder that you created earlier for your bot, create a new file named **app.js**.
2. Open **app.js** in a text editor or an IDE of your choice. Add the following code to the file: 

   [!code-javascript[consolebot code sample Node.js](../includes/code/node-getstarted.js#consolebot)]

3. Save the file. Now you are ready to run and test out your bot.

### Start your bot

Navigate to your bot's directory in a console window and start your bot:

```nodejs
node app.js
```

Your bot is now running locally. Try out your bot by typing a few messages in the console window.
You should see that the bot responds to each message you send by echoing back your message prefixed with the text *"You said:"*.

## Install Restify

Console bots are good text-based clients, but in order to use any of the Bot Framework channels (or run your bot in the emulator), your bot will need to run on an API endpoint. Install <a href="http://restify.com/" target="_blank">restify</a> by running the following **npm** command:

```nodejs
npm install --save restify
```

Once you have Restify in place, you're ready to make some changes to your bot.

## Edit your bot

You will need to make some changes to your **app.js** file. 

1. Add a line to require the `restify` module.
2. Change the `ConsoleConnector` to a `ChatConnector`.
3. Include your Microsoft App ID and App Password.
4. Have the connector listen on an API endpoint.

   [!code-javascript[echobot code sample Node.js](../includes/code/node-getstarted.js#echobot)]

5. Save the file. Now you are ready to run and test out your bot in the emulator.

> [!NOTE] 
> You do not need a **Microsoft App ID** or **Microsoft App Password** to run your bot in the Bot Framework Emulator.

## Test your bot
Next, test your bot by using the [Bot Framework Emulator](../bot-service-debug-emulator.md) to see it in action. The emulator is a desktop application that lets you test and debug your bot on localhost or running remotely through a tunnel.

First, you'll need to [download](https://emulator.botframework.com) and install the emulator. After the download completes, launch the executable and complete the installation process.

### Start your bot

After installing the emulator, navigate to your bot's directory in a console window and start your bot:

```nodejs
node app.js
```
   
Your bot is now running locally.

### Start the emulator and connect your bot
After you start your bot, start the emulator and then connect your bot:

1. Click **create a new bot configuration** link in the emulator window. 

2. Type `http://localhost:port-number/api/messages` into the address bar, where *port-number* matches the port number shown in the browser where your application is running.

3. Click Save and connect. You won't need to specify Microsoft App ID and Microsoft App Password. You can leave these fields blank for now. You'll get this information later when you register your bot.

### Try out your bot

Now that your bot is running locally and is connected to the emulator, try out your bot by typing a few messages in the emulator.
You should see that the bot responds to each message you send by echoing back your message prefixed with the text *"You said:"*.

You've successfully created your first bot using the Bot Builder SDK for Node.js!

## Next steps

> [!div class="nextstepaction"]
> [Bot Builder SDK for Node.js](bot-builder-nodejs-overview.md)

-->
