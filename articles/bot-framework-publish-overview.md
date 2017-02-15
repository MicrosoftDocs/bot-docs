---
title: Bot Publishing Process Overview | Microsoft Docs
description: Overview of the tasks required to share your both with others (register bot, deploy bot, configure bot, publish bot).
services: Bot Framework
documentationcenter: BotFramework-Docs
author: kbrandl
manager: rstand

ms.service: Bot Framework
ms.topic: article
ms.workload: Cognitive Services
ms.date: 02/06/2017
ms.author: v-kibran@microsoft.com

---
# Publish a bot


When you've finished developing a bot and you're ready to share it with others, complete the following tasks:

- [Task 1](#register): Register your bot with the Bot Framework
- [Task 2](#deploy): Deploy your bot to the cloud
- [Task 3](#configure): Configure your bot to run on one or more conversation channels
- [Task 4](#publish): Publish your bot to the Bot Directory

> [!NOTE]
> Task 1 (Register your bot) and Task 2 (Deploy your bot) are not applicable for bots that are created by using the Azure Bot Service (since those tasks are included within the functionality that the Azure Bot Service provides).

##<a id="register"></a> Task 1: Register your bot with the Bot Framework

Registration is a simple process wherein you provide some information about your bot and then generate the app ID and password that your bot will use to authenticate with the Bot Framework. 
For a detailed walk through of the registration process, see [Register a bot with the Bot Framework](bot-framework-publish-register.md).

##<a id="deploy"></a> Task 2: Deploy your bot to the cloud

Before others can use your bot, you must deploy it to the cloud. 
For a detailed walk through of the deployment process, see [Deploy a bot to the cloud](bot-framework-publish-deploy.md). 

##<a id="configure"></a> Task 3: Configure your bot to run on one or more conversation channels

After you have registered your bot with the Bot Framework and deployed your bot to the cloud, you can configure it to run on one or more conversation channels. 
For a detailed walk through of the configuration process, see [Configure a bot to run on one or more channels](bot-framework-publish-configure.md). 

##<a id="publish"></a> Task 4: Publish your bot to the directory

After you have registered your bot with the Bot Framework, deployed your bot to the cloud, and configured your bot to run on one or more channels, you can publish it to the Bot Directory. 
The Bot Directory is a public directory of all bots that are registered and published with Microsoft Bot Framework. 
By publishing your bot to the directory, you're making it available for users to find it there and add it to the channel(s) that they use. 
For a detailed walk through of the publication process, see [Publish a bot to the Bot Directory](bot-framework-publish-add-to-directory.md).
