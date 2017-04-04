---
title: Create a bot with the Bot Builder SDK for Node.js | Microsoft Docs
description: Learn how to create a bot with the Bot Builder SDK for Node.js.
keywords: Bot Framework, Bot Builder, SDK, Node.js, get started
author: kbrandl
manager: rstand
ms.topic: get-started-article
ms.prod: bot-framework

ms.date: 02/02/2017
ms.reviewer:
#ROBOTS: Index
---
# Create a bot with the Bot Builder SDK for Node.js


The <a href="https://github.com/Microsoft/BotBuilder" target="_blank">Bot Builder SDK for Node.js</a> is a powerful framework for constructing bots that can handle both freeform interactions and more guided ones where the possibilities are explicitly shown to the user.
It is easy to use and models frameworks like Express & Restify to provide a familiar way for JavaScript developers to write bots.

In this tutorial, we'll walk through the process of building a bot by using the Bot Builder SDK for Node.js
and testing it with the Bot Framework Emulator.

## Install the SDK
To start, [install the SDK][Install] and the necessary dependencies. Once you have the SDK and prequisites in place, you're ready to write a bot.

## Create your bot
For this walkthrough, you'll create a bot that simply echos back any user input.
In the folder that you created earlier for your bot, create a new file named **app.js**.
Then, add the following code to the file: 

[!include[echobot code sample Node.js](~/includes/code/node-echobot.md)]

## Test your bot

[!include[Get started test your bot](~/includes/snippet-getstarted-test-bot.md)]

### Start your bot

After installing the emulator, start your bot in a console window:

```
node app.js
```

### Start the emulator and connect your bot

At this point, your bot is running locally. Next, start the emulator and then connect your bot by completing the following tasks in the emulator:
1. Type http://localhost:3978/api/messages into the address bar. (This is the default endpoint that your bot listens to when hosted locally.)
2. Click **Connect**. (You won't need to specify **Microsoft App ID** and **Microsoft App Password** -- you can leave these fields blank for now. You'll get this information later if/when you register your bot with the framework.)

### Test your bot

Now that your bot is running locally and is connected to the emulator, test your bot by typing a few messages in the emulator.
You should see that the bot responds to each message you send by echoing back your message prefixed with the text *You said*.

Congratulations -- you've successfully created a bot by using the Bot Builder SDK for Node.js!

## Next steps

In this tutorial, you created a simple bot by using the Bot Builder SDK for Node.js
and verified the bot's functionality by using the Bot Framework Emulator.
If you'd like to share your bot with others, you'll need to
[register](~/portal-register-bot.md) it with the Bot Framework and
[deploy](~/publish-bot-overview.md) it to the cloud.

To learn more about building great bots with the Bot Framework, see the following articles:

- [Key concepts in the Bot Framework](~/bot-framework-concepts-overview.md)
- [Introduction to bot design](~/bot-design-principles.md)
- [Bot Builder SDK for Node.js](~/nodejs/index.md)
- [Publish a bot to the Bot Framework](~/publish-bot-overview.md)
- [Bot Framework FAQ](~/bot-framework-faq.md)


[Install]: ~/nodejs/index.md#install-the-sdk