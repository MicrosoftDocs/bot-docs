---
title: Designing conversation flow | Microsoft Docs
description: Understand the fundamental concepts of designing conversation flow by using dialogs in the Microsoft Bot Framework.
keywords: Bot Framework, bot design, dialog, conversation flow, conversation
author: matvelloso
manager: rstand

# the ms.topic should be the section of the IA that the article is in, with the suffix -article. Some examples:
# get-started article, sdk-reference-article
ms.topic: design-ui-and-ux-article

ms.prod: botframework

# The ms.service should be the Bot Framework technology area covered by the article, e.g., Bot Builder, LUIS, Azure Bot Service
ms.service: Bot Builder

# Date the article was updated
ms.date: 02/16/2017

# Alias of the document reviewer. Change to the appropriate person.
ms.reviewer: rstand

# Include the following line commented out
#ROBOTS: Index
---
# Designing conversation flow

## Introduction to bot UI

In a traditional application, the user interface (UI) is typically comprised of screens. 
A single app or website can use one or more screens as needed in order to exchange information with the user. 
Most applications start with a main screen where users initially land and provide navigation that leads to other screens for various functions like starting a new order, browsing products, looking for help, etc.

Like apps and websites, bots have a UI -- but it's comprised of **dialogs**, rather than screens. 
Dialogs enable the bot developer to logically separate various areas of bot functionality, much like screens do for traditional apps and websites. 
For example, you may design one dialog to contain the logic that helps the user browse for products, and a separate dialog to contain the logic that helps the user create a new order. 

Dialogs may or may not have graphical interfaces. 
They may contain buttons, text, and/or other elements, or may be entirely speech-based. 
Additionally, one dialog can invoke another, just like one screen can invoke another in an app or website.

## Using dialogs to manage conversation flow

The following diagram shows the screen flow of a traditional application compared to the dialog flow of a bot. 

![bot](media/designing-bots/core/dialogs-screens.png)

In a traditional application, everything begins with the "main screen." 
Next, the "main screen" invokes the "new order screen." 
At that point, the "new order screen" takes control, and remains in control until it either closes or invokes other screens. 
If the "new order screen" closes, the user is returned back to the "main screen."

In a bot, everything begins with the "root dialog." 
Next, the "root dialog" invokes the "new order dialog." 
At that point, the "new order dialog" takes control of the conversation, and remains in control until it either closes or invokes other dialogs. 
If the "new order dialog" closes, control of the conversation is returned back to the "root dialog." 

For a detailed walk through of managing conversation flow using dialogs and the Bot Builder SDK, see:

- [Manage conversation flow using dialogs with the Bot Builder SDK for .NET](bot-framework-dotnet-howto-manage-conversation-flow.md)
- [Manage conversation flow using dialogs with the Bot Builder SDK for Node.js](bot-framework-nodejs-howto-manage-conversation-flow.md)

## Dialog characteristics

### Dialog persistence

When a dialog is invoked, it takes control of the conversation flow. 
Every new message will be subject to processing by that dialog until until it either closes or redirects to another dialog. 

### Dialog stacks 

> [!NOTE]
> To do: finish article.

