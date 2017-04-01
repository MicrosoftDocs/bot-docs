---
title: Set up a proactive bot for Azure Bot Service | Microsoft Docs
description: Learn how to set up proactive bot for Azure Bot Service.
keywords: Bot Framework, Azure Bot Service, proactive bot
author: RobStand
manager: rstand
ms.topic: bot-service-article
ms.prod: botframework
ms.service: Azure Bot Service
ms.date: 3/21/2017
ms.reviewer:
#ROBOTS: Index
---

# Build a bot that alerts the user

The most common bot interaction happens when a user starts a conversation with a bot. But a bot can start a conversation, too. Suppose you wanted a bot to contact the user based on a triggered event, such as a lengthy job order or a pizza that is ready to be picked up? In these cases, the bot is being "proactive" by starting the conversation and waiting for the user's reply.

The proactive bot template provides all the Azure resources you need to enable a very simple proactive interaction. This diagram demonstrates how triggered events work.

<p align="center">
![Proactive template diagram](media/azure-bot-proactive-diagram.png)</p>


When you create a proactive bot with the Azure Bot Service, you will find these Azure resources in your resource group:
- Azure Storage, which is used to create the queue.
- Azure Bot Service, which is your bot. This contains the logic that receives the message from the user, adds the message with required properties (such as recipient and the user’s message) to the Azure queue, receives the triggers from Azure Functions, and then sends back the message it received from the trigger’s payload.
- Azure Function App (a `queueTrigger` Azure Function), which is triggered whenever there is a message in the queue and then communicates to the Bot service via Direct Line. This function uses bot binding to send the message as part of the trigger’s payload. 


## Receiving a message from the user and adding it to an Azure Storage Queue

This code sample demonstrates how the proactive bot:
1. Receives the message from the user.
2. Adds it to an Azure Storage Queue.
3. Sends back an acknowledgment to the user. 

Notice that the message is wrapped in an object that contains all the information needed to send the message back to the user on the right channel (`ResumptionCookie` for C# and `session.message.address` for Node.js).

[!code-csharp[Receive Message](../includes/code/azure-proactive-bot.cs#receiveMessage)] 

[!code-JavaScript[Receive Message](../includes/code/azure-proactive-bot.js#receiveMessage)] 

## Receiving the message back from the Azure Function

This code sample demonstrates how to receive the message from the trigger function.

[!code-csharp[Receive message from trigger](../includes/code/azure-proactive-bot.cs#receiveTrigger)] 


[!code-JavaScript[Receive message from trigger](../includes/code/azure-proactive-bot.js#receiveTrigger)] 

## Triggering an Azure Function with the queue, and sending the message back to the user

After the message is added to the queue, the function is triggered. The message is then removed from the queue and sent back to the user. If you inspect the function’s configuration file, you will see that it contains an input binding of type `queueTrigger` and an output binding of type `bot`.

The **functions.json** configuration file looks like this.

[!code-JavaScript[Queue Trigger json](../includes/code/azure-proactive-bot.js#queueTriggerJson)] 

[!code-csharp[return Message](../includes/code/azure-proactive-bot.cs#returnMessage)] 

[!code-JavaScript[Queue Trigger](../includes/code/azure-proactive-bot.js#queueTrigger)] 

## Additional resources

- <a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">Bot Builder Samples GitHub Repo </a>
- <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/" target="_blank">Bot Builder SDK C# Reference</a>
- <a href="https://github.com/Microsoft/BotBuilder-Samples" target="_blank">Bot Builder SDK</a>
