---
title: Bot capabilities | Microsoft Docs
description: Understand bot capabilities including message logging, proactive messages, calling and IVR, and global message handlers.
keywords: Bot Framework, Bot design, capabilities, message logging, proactive messages, calling and IVR bots, global message handlers
author: matvelloso
manager: rstand
ms.topic: design-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/21/2017
ms.reviewer: rstand
#ROBOTS: Index
---

# Bot capabilities

Regardless of the specific use case(s) that your bot is designed to support, 
it may require the ability to do things such as 
intercept messages that are exchanged between user and bot, 
send proactive messages to the user, 
implement global message handlers to gracefully accommodate a user's request for "help", 
and/or conduct a voice call via Skype. 
This article describes each of these bot capabilities and links to related how-to articles that specify implementation details. 

## Intercept messages

[!include[Introduction to message logging](../includes/snippet-message-logging-intro.md)]

For a detailed walk through of how to intercept messages, see:
- [Intercept messages using the Bot Builder SDK for .NET](bot-framework-dotnet-howto-middleware.md)
- [Intercept messages using the Bot Builder SDK for Node.js](bot-framework-nodejs-howto-middleware.md)

##<a id="proactiveMsg"></a> Send proactive messages

[!include[Introduction to proactive messages - part 1](../includes/snippet-proactive-messages-intro-1.md)]

Consider the following scenario:

![how users talk](media/designing-bots/capabilities/proactive1.png)

In this example, the user has previously asked the bot to monitor prices of a hotel in Las Vegas. 
At that time, the bot launched a background monitoring task, and it has been continuously running for the past several days. 
In the current conversation, the user is in the midst of booking a trip to London, when  
the background task triggers a notification message about a discount for the Las Vegas hotel.
The bot interjects this information into the current conversation, making for a confusing user experience. 

How should the bot have handled this situation? 

- Wait for the current travel booking to finish, then deliver the notification. This approach would be minimally disruptive, but the delay in communicating the information might cause the user to miss out on the low price opportunity for the Las Vegas hotel. 
- Cancel the current travel booking flow and deliver the notification immediately. This approach delivers the information in a timely fashion, but would likely frustrate the user by forcing them start over with their travel booking. 
- Interrupt the current booking, change the topic of conversation to the hotel in Las Vegas until the user responds, and then switch back to the in-progress travel booking and continue from where it was interrupted. This approach may seem like the best choice, but it introduces complexity both for the bot developer and the user.

Most commonly, your bot will use some combination of **ad hoc proactive messages** and **dialog-based proactive messages** to handle situations like this. 

[!include[Introduction to proactive messages - part 2](../includes/snippet-proactive-messages-intro-2.md)]

For a detailed walk through of how to send proactive messages, see:
- [Send proactive messages using the Bot Builder SDK for .NET](~/bot-framework-dotnet-howto-proactive-messages.md)
- [Send proactive messages using the Bot Builder SDK for Node.js](~/bot-framework-nodejs-howto-proactive-messages.md)

##<a id="global-message-handlers"></a> Implement global message handlers

[!include[Introduction to global message handlers](../includes/snippet-global-handlers-intro.md)]

For a detailed walk through of how to implement global message handlers, see:
- [Implement global message handlers using the Bot Builder SDK for .NET](bot-framework-dotnet-howto-global-handlers.md)
- [Implement global message handlers using the Bot Builder SDK for Node.js](bot-framework-nodejs-howto-global-handlers.md)

## Conduct audio calls

[!include[Introduction to conducting audio calls](../includes/snippet-audio-call-intro.md)]

For a detailed walk through of how to enable support for audio calls, see [Conduct audio calls with Skype by using the Bot Builder SDK for .NET](bot-framework-dotnet-howto-audio-calls.md).
