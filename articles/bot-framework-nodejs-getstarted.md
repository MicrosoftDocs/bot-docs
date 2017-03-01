---
title: Create a bot with the Bot Builder SDK for Node.js | Microsoft Docs
description: Learn how to create a bot with the Bot Builder SDK for Node.js.
keywords: Bot Framework, Bot Builder, SDK, Node.js, get started
author: kbrandl
manager: rstand
ms.topic: get-started-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/02/2017
ms.reviewer: rstand
#ROBOTS: Index
---
# Create a bot with the Bot Builder SDK for Node.js
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-getstarted.md)
> * [Node.js](bot-framework-nodejs-getstarted.md)
> * [Azure Bot Service](bot-framework-azure-getstarted.md)
>

The [Bot Builder SDK for Node.js](https://github.com/Microsoft/BotBuilder) is a powerful framework for constructing bots that can handle both freeform interactions and more guided ones where the possibilities are explicitly shown to the user. 
It is easy to use and models frameworks like Express & Restify to provide a familiar way for JavaScript developers to write bots.

In this tutorial, we'll walk through the process of developing, testing, and publishing a bot that is created using the Bot Builder SDK for Node.js.

## Get the SDK
We'll start by getting the SDK and installing the necessary dependencies. 
First, create a folder for your bot, navigate to it, and run the following **npm** command:

```
npm init
```

Next, install the Bot Builder SDK and [Restify](http://restify.com/) modules by running the following **npm** commands:

```
npm install --save botbuilder
npm install --save restify
```

## Create your bot
Now that you've got the SDK and prequisites in place, you're ready to write a bot. 
For this walkthrough, you'll create a bot that simply echos back any user input. 
In the folder that you created earlier for your bot, create a new file named **app.js**. 
Then, add the following code to the file: 

> [!NOTE]
> To do: add code sample

## Test your bot

[!include[Get started test your bot](../includes/snippet-getstarted-test-bot.md)]

After installing the emulator, start your bot in a console window:

```
node app.js
```

At this point, your bot is running locally. Next, start the emulator and then connect your bot by completing the following tasks in the emulator:
1. Type http://localhost:3978/api/messages into the address bar. (This is the default endpoint that your bot listens to when hosted locally.)
2. Click **Connect**. (You won't need to specify **Microsoft App ID** and **Microsoft App Password** -- you'll get this information later if/when you register your bot with the framework.)

Now that your bot is running locally and is connected to the emulator, test your bot by typing a few messages in the emulator. 
You should see that the bot responds to each message you send by echoing back your message prefixed with the text *You said*. 
Congratulations -- you've successfully created a bot using the Bot Builder SDK for Node.js! 

## Publish your bot

[!include[Get started publish your bot](../includes/snippet-getstarted-next-steps.md)]

## Next steps

In this tutorial, you created a simple bot using the Bot Builder SDK for Node.js. 
To learn more about building great bots by using the Bot Framework, see: 

> [!NOTE]
> Content coming soon. 
> TO_DO: add list of related topics