---
title: Publish a bot with the Bot Framework | Microsoft Docs
description: Learn what you need to do to publish and deploy your bot for people to use.
keywords: Bot Framework, Bot Builder, register, deploy, configure, publish, Bot Directory
author: kbrandl
manager: rstand
ms.topic: publish-overview-article
ms.prod: botframework
ms.service: Bot Builder
ms.date:
ms.reviewer: rstand
#ROBOTS: Index
---

# Publish a bot

When you've finished developing a bot and you're ready to share it with others, there are some things you need to do:

1. [Register](#register) your bot with the Bot Framework<br/>
2. [Deploy](#deploy) your bot to the cloud<br/>
3. [Configure](#configure) your bot to run on one or more conversation channels<br/>
4. [Publish](#publish) your bot to the Bot Directory

> [!NOTE]
> If you built your bot using the Azure Bot Service, you don't need to register or deploy your bot.
> Bot registration and deployment are handled as part of the Azure Bot Service bot creation process.

## Register your bot with the Bot Framework

> [!IMPORTANT]
> Review the [Bot Directory publishing guidelines](~/deploy/review-guidelines.md) and keep them in mind when registering and publishing your bot.

Registration is a simple process. You provide some information about your bot and then generate the app ID and password that your bot will use to authenticate with the Bot Framework.
For a detailed walk through of the registration process, see [Register a bot with the Bot Framework](~/portal-register-bot.md).

## Deploy your bot to the cloud

Before others can use your bot, you must deploy it to the cloud. You have a few different options:

- [Deploy from a local git repository](~/azure-deploy-bot-local-git.md) using continuous integration
- [Deploy from Github](~/azure-deploy-bot-github.md) using continous integration
- [Deploy from Visual Studio](~/azure-deploy-bot-visual-studio.md)

## Configure your bot to run on one or more conversation channels

After you have registered your bot with the Bot Framework and deployed your bot to the cloud, you can [configure a bot to run on one or more channels](~/portal-configure-channels.md).

## Publish your bot to the directory

After you have registered your bot with the Bot Framework, deployed your bot to the cloud, and configured your bot to run on one or more channels, you can [publish it to the Bot Directory](~/directory-add-bot.md).
The Bot Directory is a public directory of all bots that are registered and published with Microsoft Bot Framework.
By publishing your bot to the directory, you're making it available for users to find it there and add it to the channel(s) that they use.
