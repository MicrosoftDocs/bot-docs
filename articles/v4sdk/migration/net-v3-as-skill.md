---
title: Convert a .NET v3 bot to a skill
description: Shows how to convert existing .NET v3 bots to skills and consume them from a .NET v4 bot.
keywords: .net, bot migration, skills, v3 bot
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 02/19/2020
monikerRange: 'azure-bot-service-4.0'
---

# Convert a .NET v3 bot to a skill

Converting an existing v3 bot to a skill takes just a few steps. It allows other bots to access the v3 bot, while allowing users to still access the v3 bot. This can be less work than migrating the v3 bot to the v3 SDK. Any v4 bot designed to do so can consume a skill.

The skill consumer and skill use the Bot Connector service to communicate with each other. The consumer and skill can be implemented in different languages, such as .NET and JavaScript.

This article describes how to convert 3 sample .NET v3 bots to skills and to create a v4 skill consumer that can access these skills.

To migrate a .NET bot from v3 to v4, see how to [Migrate a .NET v3 bot to a .NET Framework v4 bot](conversion-framework.md).
To convert a JavaScript v3 bot to a skill, see how to [Convert a JavaScript v3 bot to a skill](javascript-v3-as-skill.md).

## Prerequisites

- Visual Studio
- Copies of the v3 .NET sample bots to convert: [EchoBot](https://aka.ms/v3-cs-echo-bot), [PizzaBot](https://aka.ms/v3-cs-pizza-bot), and [SimpleSandwichBot](https://aka.ms/v3-cs-simple-sandwich-bot).
- A copy of the v4 .NET sample skill consumer: [SimpleRootBot](https://aka.ms/cs-simple-root-bot).
- An Azure subscription. If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

## About the bots

In this article, each v3 bot is updated to act as a skill. A v4 skill consumer is included, so that you can test the converted bots as skills.

- The **EchoBot** echoes back messages it receives. As a skill, it _completes_ when it receives an "end" or "stop" message.
- The **PizzaBot** walks a user through ordering a pizza. As a skill, it sends the user's order back to the parent when finished.
- The **SimpleSandwichBot** walks a user through ordering a sandwich. As a skill, it sends the user's order back to the parent when finished.

Also, a v4 skill consumer, the **SimpleRootBot**, demonstrates how to consume the skills and allows you to test them.

To convert an existing bot to a skill bot takes just a few steps, as outlined in the next couple sections. For more in-depth information, see [about skills](../skills-conceptual.md).

## Create Azure resources for the bots

Bot-to-bot authentication requires that each participating bot has a valid app ID and password.

1. Create a Bot Channels Registration for all 4 bots.
1. Record the app ID and password for each one.

## Convert each v3 bot to a skill

For each v3 bot, you will:

- Update the bot's configuration file to set the bot's app ID and password and to add an _allowed callers_ property.
- Add claims validation. This will restrict access to the skill so that only users or your root bot can access the skill.
- Modify the bot's messages controller to handle `endOfConversation` activities from the root bot.
- Modify the bot code to return an `endOfConversation` activity when the skill completes.
- Add a manifest file that describes the expected inputs and outputs of the skill.
  The current manifest schema is [skill-manifest-2.0.0.json](https://github.com/microsoft/botframework-sdk/blob/master/schemas/skills/skill-manifest-2.0.0.json).

### Convert the echo bot

In this project the root dialog was moved from the **V3EchoBot\\Controllers\\MessagesController.cs** file to a new **V3EchoBot\\Dialogs\\RootDialog.cs** file.

1. Open your copy of the EchoBot project.

1. To the configuration file, add the echo bot's app ID and password. Also in app settings, add an `EchoBotallowedCallers` property and add the simple root bot's app ID to its value.

   **V3EchoBot\\Web.config**

   [!code-csharp[app settings](~/../botbuilder-samples/MigrationV3V4/CSharp/Skills/V3EchoBot/Web.config?range=11-16)]

1. Add a claims validator and a supporting allowed callers class.

   **V3EchoBot\\Authentication\\CustomAllowedCallersClaimsValidator.cs**

   This performs the claims validation and throws an `UnauthorizedAccessException` if validation fails.

   [!code-csharp[claims validator](~/../botbuilder-samples/MigrationV3V4/CSharp/Skills/V3EchoBot/Authentication/CustomAllowedCallersClaimsValidator.cs?range=4-72&highlight=48-66)]

   **V3EchoBot\\Authentication\\CustomSkillAuthenticationConfiguration.cs**

   This loads the allowed callers information from the configuration file.

   [!code-csharp[allowed callers](~/../botbuilder-samples/MigrationV3V4/CSharp/Skills/V3EchoBot/Authentication/CustomSkillAuthenticationConfiguration.cs?range=4-20)]

1. Update the `MessagesController` class.

   **V3EchoBot\\Controllers\\MessagesController.cs**

   Update the using statements.

   [!code-csharp[using statements](~/../botbuilder-samples/MigrationV3V4/CSharp/Skills/V3EchoBot/Controllers/MessagesController.cs?range=4-15)]

   In the `HandleSystemMessage` method, add a condition to handle an `endOfConversation` message. This allows the skill to clear state and release resources when the conversation is ended from the skill consumer.

   [!code-csharp[on end of conversation](~/../botbuilder-samples/MigrationV3V4/CSharp/Skills/V3EchoBot/Controllers/MessagesController.cs?range=49-64)]

1. Modify the bot code to allow the skill to flag that the conversation is complete when it receives an "end" or "stop" message from the user. The skill should also clear state and release resources when it ends the conversation.

   **V3EchoBot\\Dialogs\\RootDialog.cs**
   [!code-csharp[message received](~/../botbuilder-samples/MigrationV3V4/CSharp/Skills/V3EchoBot/Dialogs/RootDialog.cs?range=21-41)]

1. Create a manifest.

   **V3EchoBot\\wwwroot\\echo-bot-manifest.json**
   [!code-csharp[manifest](~/../botbuilder-samples/MigrationV3V4/CSharp/Skills/V3EchoBot/wwwroot/echo-bot-manifest.json)]

### Convert the pizza bot

1. Open your copy of the PizzaBot project.
1. Add a claims validator.

   **V3PizzaBot\\Authentication\\CustomAllowedCallersClaimsValidator.cs**
   <!--TODO Insert code link-->

   **V3PizzaBot\\Authentication\\CustomSkillAuthenticationConfiguration.cs**
   <!--TODO Insert code link-->

1. Add a `SkillsHelper` class that can send

   **V3PizzaBot\\SkillsHelper.cs**
   <!--TODO Insert code link-->

1. Replace the implementation of the `MessagesController` class.

   **V3PizzaBot\\Controllers\\MessagesController.cs**

   Update the using statements.

   <!--TODO Insert code link.-->

   Update the class attribute from `BotAuthentication` to `SkillBotAuthentication`.

   <!--TODO Insert code link.-->

   Modify the bot code to send an 

   <!--TODO Insert code link.-->

1. Modify the bot code.

   **V3PizzaBot\\Dialogs\\RootDialog.cs**
   <!--TODO Insert code link.-->

1. Create a manifest.

   **V3PizzaBot\\wwwroot\\echo-bot-manifest.json**
   <!--TODO Insert code link.-->

### To convert the sandwich bot

## Create the v4 root bot

## Test the root bot
