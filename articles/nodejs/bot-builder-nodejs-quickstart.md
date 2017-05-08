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

The <a href="https://github.com/Microsoft/BotBuilder" target="_blank">Bot Builder SDK for Node.js</a> is a powerful framework for constructing bots that can handle both freeform interactions and more guided ones where the possibilities are explicitly shown to the user.
It is easy to use and models frameworks like Express & Restify to provide a familiar way for JavaScript developers to write bots.

This tutorial walks you through building a bot by using the Bot Builder SDK for Node.js, and then testing it with the Bot Framework Emulator.

## Install the SDK
To install the SDK and its dependencies, first create a folder for your bot, navigate to it, and run the following **npm** command:

```
npm init
```

Next, install the Bot Builder SDK and <a href="http://restify.com/" target="_blank">Restify</a> modules by running the following **npm** commands:

```
npm install --save botbuilder
npm install --save restify
```
Once you have the SDK and prequisites in place, you're ready to write a bot.

## Create your bot

For this walkthrough, you'll create a bot that simply echos back any user input.
In the folder that you created earlier for your bot, create a new file named **app.js**.
Then, add the following code to the file: 

[!code-javascript[echobot code sample Node.js](~/includes/code/node-getstarted.js#echobot)]

## Test your bot

[!include[Get started test your bot](~/includes/snippet-getstarted-test-bot.md)]

### Start your bot

After installing the emulator, start your bot in a console window:

```
node app.js
```

### Start the emulator and connect your bot

At this point, your bot is running locally. Next, start the emulator and then connect your bot by completing the following tasks in the emulator:
1. Type `http://localhost:3978/api/messages` into the address bar. (This is the default endpoint that your bot listens to when hosted locally.)
2. Click **Connect**. You won't need to specify **Microsoft App ID** and **Microsoft App Password**. You can leave these fields blank for now. You'll get this information later when you register your bot.

### Test your bot

Now that your bot is running locally and is connected to the emulator, test your bot by typing a few messages in the emulator.
You should see that the bot responds to each message you send by echoing back your message prefixed with the text *You said*.

You've successfully created a bot by using the Bot Builder SDK for Node.js!

## Next steps

If you'd like to share your bot with others, you'll need to
[register](~/portal-register-bot.md) it with the Bot Framework and
[deploy](~/publish-bot-overview.md) it to the cloud.

To learn more about building great bots with the Bot Framework, see the following articles:

- [How the Bot Framework works](~/overview-how-bot-framework-works.md)
- [Principles of bot design](~/bot-design-principles.md)
- [Bot Builder SDK for Node.js](~/nodejs/index.md)
- [Deploy and publish bots](~/publish-bot-overview.md)
- [Bot Framework FAQ](~/resources-bot-framework-faq.md)

[Install]: ~/nodejs/index.md#get-the-sdk
