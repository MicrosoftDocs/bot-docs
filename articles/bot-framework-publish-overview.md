---
title: Publish a bot with the Bot Framework | Microsoft Docs
description: Learn the tasks required to share your both with others (register bot, deploy bot, configure bot, publish bot).
keywords: Bot Framework, Bot Builder, register bot, deploy bot, configure bot, publish bot, Bot Directory
author: kbrandl
manager: rstand
ms.topic: publish-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/06/2017
ms.reviewer:
#ROBOTS: Index
---

# Publish a bot

When you've finished developing a bot and you're ready to share it with others, complete the following tasks:

[Task 1](#register): Register your bot with the Bot Framework<br/>
[Task 2](#deploy): Deploy your bot to the cloud<br/>
[Task 3](#configure): Configure your bot to run on one or more conversation channels<br/>
[Task 4](#publish): Publish your bot to the Bot Directory

> [!NOTE]
> Task 1 (Register your bot) and Task 2 (Deploy your bot) are not applicable for bots that are created by using the Azure Bot Service
> (since bot registration and deployment are handled as part of the Azure Bot Service bot creation process).

##<a id="register"></a>Register your bot with the Bot Framework

> [!IMPORTANT]
> Review the [Bot Directory publishing guidelines](bot-framework-publish-review-guidelines.md) and keep them in mind when registering and publishing your bot.

Registration is a simple process wherein you provide some information about your bot and then generate the app ID and password that your bot will use to authenticate with the Bot Framework.
For a detailed walk through of the registration process, see [Register a bot with the Bot Framework](bot-framework-publish-register.md).

##<a id="deploy"></a>Deploy your bot to the cloud

Before others can use your bot, you must deploy it to the cloud.
For a detailed walk through of the deployment process, see [Deploy a bot to the cloud](bot-framework-publish-deploy.md).

##<a id="configure"></a>Configure your bot to run on one or more conversation channels

After you have registered your bot with the Bot Framework and deployed your bot to the cloud, you can configure it to run on one or more conversation channels.
For a detailed walk through of the configuration process, see [Configure a bot to run on one or more channels](bot-framework-publish-configure.md).

##<a id="publish"></a>Publish your bot to the directory

After you have registered your bot with the Bot Framework, deployed your bot to the cloud, and configured your bot to run on one or more channels, you can publish it to the Bot Directory.
The Bot Directory is a public directory of all bots that are registered and published with Microsoft Bot Framework.
By publishing your bot to the directory, you're making it available for users to find it there and add it to the channel(s) that they use.
For a detailed walk through of the publication process, see [Publish a bot to the Bot Directory](bot-framework-publish-add-to-directory.md).
