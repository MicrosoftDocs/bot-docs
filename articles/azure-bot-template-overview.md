---
title: Overview of Bot templates for Azure Bot Service | Microsoft Docs
description: Learn about Bot templates for Azure Bot Service.
keywords: Bot Framework, Azure Bot Service, continuous integration
author: RobStand
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 3/21/2017
ms.reviewer:
#ROBOTS: Index
---

# Templates for Azure Bot Service

Bot templates are the quickest way to get your first bots up and talking. They are based on Azure Bot Service and are powered by <a href="https://docs.microsoft.com/en-us/azure/azure-functions/functions-reference" target="_blank">Azure functions</a>. 

There's no need to use a desktop code editor. The online Azure editor is all you need. Keep in mind, though, that the Azure editor does not allow you to add new files or rename and delete existing files. 

If you need to manage the files, you should set up [continuous integration](~/azure-bot-service/continuous-integration.md), which would let you use the IDE and source control of your choice, sush as Visual Studio Team, GitHub, and Bitbucket. Continuous integration will automatically deploy to Azure the changes you commit to source control. Once you have set up continuous integration, you can [debug your bot locally](~/azure-bot-service/debug.md).

> [!NOTE]
> After configuring continuous integration, you will no longer be able to update the bot in the Azure editor.

## Bot templates to get you started
Azure Bot Service has five bot templates to get you started creating your bots. If you're new to bots, start with the basic bot and then grow your bot coding skills from there. 

### Basic bot

Use the [basic bot template](~/azure-bot-template-basic.md) to create a simple bot. You'll learn the basics about managing a conversation flow by using dialogs that respond to user input. 

### Form bot

Use the [form bot template](~/azure-bot-template-form.md) to create a guided conversation to collect user input. A form bot for buying a bicycle, for example, might ask the user questions like "What terrain do you plan on riding over?" Or "What size tires?" After the user answers the questions, various models  of bicycles are displayed.

### Language understanding bot

Use the [language understanding bot template](~/azure-bot-service/natural-language-bot.md) to create a bot that uses the natural language LIUS models to understand user intent. For example, if a user asks the bot, "Get environmental news about Montana," your bot needs to understand that the user is asking for environmental and not, say, political news about only one specific state.

### Proactive bot

Use the [proactive bot template](~/azure-bot-service/proactive-bot.md) to create a bot that alerts the user to events. For example, if a user has ordered a pizza, the bot will alert the user when the pizza is ready to pick up.
 
### Question and answer bot

Use the [question and answer bot template](~/azure-bot-service/question-and-answer-bot.md) to create a bot that displays question and answer pairs based upon a link you supply to your company's FAQ. When a user asks question to your bot, it responds with answers from the content that was displayed in the FAQ. 

## Next steps

Review the articles in this section to learn more about building bots using the Azure Bot Service.

- Follow a [step-by-step tutorial](~/azure-bot-service/getstarted.md) so that you can quickly build and test a simple bot.
- [Manage conversation flow using dialogs](~/dotnet/manage-conversation-flow.md)

If you encounter problems or have suggestions regarding Azure Bot Service, 
see [Support](~/resources-support.md) for a list of available resources. 
