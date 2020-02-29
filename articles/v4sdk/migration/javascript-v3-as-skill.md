---
title: Convert a JavaScript v3 bot to a skill
description: Shows how to convert existing JavaScript v3 bots to skills and consume them from a JavaScript v4 bot.
keywords: JavaScript, bot migration, skills, v3 bot
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 02/19/2020
monikerRange: 'azure-bot-service-4.0'
---

# Convert a JavaScript v3 bot to a skill

This article describes how to convert 2 sample JavaScript v3 bots to skills and to create a v4 skill consumer that can access these skills.
To convert a .NET v3 bot to a skill, see how to [Convert a .NET v3 bot to a skill](net-v3-as-skill.md).
To migrate a JavaScript bot from v3 to v4, see how to [Migrate a Javascript v3 bot to a v4 bot](conversion-javascript.md).

## Prerequisites

- Visual Studio Code.
- Node.js.
- An Azure subscription. If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A copy of the v4 JavaScript sample skill consumer: [**v4-root-bot**](https://aka.ms/js-simple-root-bot). <!--TODO Create aka link once there's a target-->

## About the bots

In this article, each v3 bot is created to act as a skill. A v4 skill consumer is included, so that you can test the converted bots as skills.

- The **v3-skill-bot** echoes back messages it receives. As a skill, it _completes_ when it receives an "end" or "stop" message. The bot to convert is based on a minimal v3 bot.
- The **v3-booking-bot-skill** allows the user to book a flight or a hotel. As a skill, it sends collected information back to the parent when finished.

Also, a v4 skill consumer, the **v4-root-bot**, demonstrates how to consume the skills and allows you to test them.

To use the skill consumer to test the skills, all 3 bots need to be running at the same time. The bots can be tested locally using the Bot Framework Emulator, with each bot using a different local port.

## Create Azure resources for the bots

Bot-to-bot authentication requires that each participating bot has a valid app ID and password.

1. Create a Bot Channels Registration for the bots as needed.
1. Record the app ID and password for each one.

## Conversion process

To convert an existing bot to a skill bot takes just a few steps, as outlined in the next couple sections. For more in-depth information, see [about skills](../skills-conceptual.md).

- Update the bot's `.env` file to set the bot's app ID and password and to add a _root bot app ID_ property.
- Add claims validation. This will restrict access to the skill so that only users or your root bot can access the skill. See the [additional information](#additional-information) section for more information about default and custom claims validation.
- Modify the bot's messages controller to handle `endOfConversation` activities from the root bot.
- Modify the bot code to return an `endOfConversation` activity when the skill completes.
- Whenever the skill completes, if it has conversation state or maintains resources, it should clear its conversation state and release resources.
- Optionally add a manifest file.
  Since a skill consumer does not necessarily have access to the skill code, use a skill manifest to describe the activities the skill can receive and generate, its input and output parameters, and the skill's endpoints.
  The current manifest schema is [skill-manifest-2.0.0.json](https://github.com/microsoft/botframework-sdk/blob/master/schemas/skills/skill-manifest-2.0.0.json).

## Convert the echo bot

See [Skills/v3-skill-bot](https://aka.ms/v3-js-echo-skill) for an example of a v3 echo bot that has been converted to a basic skill. <!--TODO Create aka link once there's a target-->

1. Create a simple JavaScript v3 bot project.

   **v3-skill-bot/app.js**

   [!code-javascript[require statements](~/../botbuilder-samples/MigrationV3V4/Node/Skills/v3-skill-bot/app.js?range=5-7)]

1. Set the bot to run locally on port 3979.

   **v3-skill-bot/app.js**

   [!code-javascript[Setup server and port](~/../botbuilder-samples/MigrationV3V4/Node/Skills/v3-skill-bot/app.js?range=9-13)]

1. To the configuration file, add the echo bot's app ID and password. Also, add a `ROOT_BOT_APP_ID` property and add the simple root bot's app ID to its value.

   **v3-skill-bot/.env.example**

   [!code[.env file](~/../botbuilder-samples/MigrationV3V4/Node/Skills/v3-skill-bot/.env.example)]

1. Create the chat connector for the bot. This one uses the default authentication configuration. The `allowedCallers` parameter is an array of the app IDs of the bots allowed to use this skill. If the first value of this array is '*', then any bot could use this skill.

   **v3-skill-bot/app.js**

   [!code-javascript[chat connector](~/../botbuilder-samples/MigrationV3V4/Node/Skills/v3-skill-bot/app.js?range=24-30&highlight=6)]

1. Update the message handler to send an `endOfConversation` activity when the user chooses to end the skill.

   **v3-skill-bot/app.js**

   [!code-javascript[universal bot](~/../botbuilder-samples/MigrationV3V4/Node/Skills/v3-skill-bot/app.js?range=35-46&highlight=6)]

1. Since this bot does not maintain conversation state and does not create any resources for the conversation, the bot does not need to handle any `endOfConversation` activities that it receives from the skill consumer.

1. Use this manifest for the echo bot. Set the endpoint app ID to the bot's app ID.

   **v3-skill-bot/manifest/v3-skill-bot-manifest.json**

   [!code-json[manifest](~/../botbuilder-samples/MigrationV3V4/Node/Skills/V3EchoBot/v3-skill-bot/manifest/v3-skill-bot-manifest.json?highlight=22)]

   > [!TIP]
   > The manifest lists the local endpoint for the bot. The port used for the `endpointUrl` should match the port you set in the project's properties.
   >
   > If you published this bot, you would update the `SkillEndpoint` to match the published endpoint.
