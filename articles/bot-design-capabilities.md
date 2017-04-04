---
title: Bot capabilities | Microsoft Docs
description: Understand bot capabilities, including message logging, proactive messages, calling and IVR, and global message handlers.
keywords: Bot Framework, Bot design, capabilities, message logging, proactive messages, calling and IVR bots, global message handlers
author: matvelloso
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date:
ms.reviewer: rstand
#ROBOTS: Index
---

# Bot capabilities

Your bot may need to do things such as 
intercept messages that are exchanged between user and bot, 
send proactive messages to the user, 
implement global message handlers to gracefully accommodate a user's request for "help", 
or conduct a voice call via Skype. 

## Intercept messages

[!include[Introduction to message logging](~/includes/snippet-message-logging-intro.md)]

For a detailed walkthrough of how to intercept messages, see:
- [Intercept messages using the Bot Builder SDK for .NET](~/dotnet/middleware.md)
- [Intercept messages using the Bot Builder SDK for Node.js](~/nodejs/middleware.md)

##<a id="proactiveMsg"></a> Send proactive messages

[!include[Introduction to proactive messages - part 1](~/includes/snippet-proactive-messages-intro-1.md)]

Consider the following scenario:

![how users talk](~/media/designing-bots/capabilities/proactive1.png)

In this example, the user has previously asked the bot to monitor prices of a hotel in Las Vegas. 
The bot launched a background monitoring task, which has been continuously running for the past several days. 
In the current conversation, the user is booking a trip to London when  
the background task triggers a notification message about a discount for the Las Vegas hotel.
The bot interjects this information into the current conversation, making for a confusing user experience. 

How should the bot have handled this situation? 

- Wait for the current travel booking to finish, then deliver the notification. This approach would be minimally disruptive, but the delay in communicating the information might cause the user to miss out on the low-price opportunity for the Las Vegas hotel. 
- Cancel the current travel booking flow and deliver the notification immediately. This approach delivers the information in a timely fashion but would likely frustrate the user by forcing them start over with their travel booking. 
- Interrupt the current booking, change the topic of conversation to the hotel in Las Vegas until the user responds, and then switch back to the in-progress travel booking and continue from where it was interrupted. This approach may seem like the best choice, but it introduces complexity both for the bot developer and the user.

Most commonly, your bot will use some combination of **ad hoc proactive messages** and **dialog-based proactive messages** to handle situations like this. 

[!include[Introduction to proactive messages - part 2](~/includes/snippet-proactive-messages-intro-2.md)]

For example, consider a bot that needs to initiate a survey at a given point in time. 
When that time arrives, the bot stops the existing conversation with the user and 
redirects the user to a `SurveyDialog`. 
The `SurveyDialog` is added to the top of the dialog stack and takes control of the conversation. 
When the user finishes all required tasks at the `SurveyDialog`, the `SurveyDialog` closes,
 returning control to the previous dialog, where the user can continue with the prior topic of conversation.

A dialog-based proactive message is more than just simple notification. 
In sending the notification, the bot changes the topic of the existing conversation. 
It then must decide whether to resume that conversation later, or to abandon that conversation altogether by resetting the dialog stack. 

For a detailed walkthrough of how to send proactive messages, see:
- [Send proactive messages using the Bot Builder SDK for .NET](~/dotnet/proactive-messages.md)
- [Send proactive messages using the Bot Builder SDK for Node.js](~/nodejs/proactive-messages.md)

##<a id="global-message-handlers"></a> Implement global message handlers

[!include[Introduction to global message handlers](~/includes/snippet-global-handlers-intro.md)]

For a detailed walkthrough of how to implement global message handlers, see:
- [Implement global message handlers using the Bot Builder SDK for .NET](~/dotnet/global-handlers.md)
- [Implement global message handlers using the Bot Builder SDK for Node.js](~/nodejs/global-handlers.md)

## Conduct audio calls

[!include[Introduction to conducting audio calls](~/includes/snippet-audio-call-intro.md)]

For a detailed walk through of how to enable support for audio calls, see [Conduct audio calls with Skype by using the Bot Builder SDK for .NET](~/dotnet/audio-calls.md).
