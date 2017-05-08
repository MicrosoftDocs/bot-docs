---
title: Deploy and publish a bot | Microsoft Docs
description: Learn how to deploy and publish a bot.
keywords: Bot Framework, Bot Builder, register, deploy, configure, publish, Bot Directory
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date:
ms.reviewer:
---
# Deploy and publish bots

After you have built and tested your bots, you need to register your bot,  deploy it to the cloud, configure it, and make it available to others in the Bot Directory.

> [!NOTE]
> If you built your bot using the Azure Bot Service, you don't need to register or deploy your bot.
> Bot registration and deployment are handled as part of the Azure Bot Service bot creation process.

## Register your bot with the Bot Framework

[Registering a bot](~/portal-register-bot.md) is a simple process. You provide some information about your bot and then generate the app ID and password that your bot will use to authenticate with the Bot Framework.

## Deploy your bot to the cloud

Before others can use your bot, you must deploy it to the cloud. You can deploy it to Azure, or to any other cloud service. These articles describe various techniques for deploying your bot to Azure: 

- [Deploy from a local git repository](~/deploy-bot-local-git.md) using continuous integration
- [Deploy from GitHub](~/deploy-bot-github.md) using continuous integration
- [Deploy from Visual Studio](~/deploy-bot-visual-studio.md)

## Configure your bot to run on one or more conversation channels

After you have registered your bot with the Bot Framework and deployed your bot to the cloud, you can [configure a bot to run on one or more channels](~/portal-configure-channels.md).
