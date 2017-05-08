---
redirect_url: /bot-framework/deploy-bot-overview
title: Deploy a bot to the cloud | Microsoft Docs
description: Learn how to deploy a bot to Azure.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date:
ms.reviewer:

---
# Deploy a bot to the cloud

After you have built and tested your bots, you need to deploy it to the cloud.

> [!NOTE]
> Bots built using the Azure Bot Service do not need to be registered or deployed.
> Bot registration and deployment are handled as part of the Azure Bot Service bot creation process.

## Deploy your bot to the cloud

Before others can use your bot, you must deploy it to the cloud. You can deploy it to Azure or to any other cloud service. These articles describe various techniques for deploying your bot to Azure: 

- [Deploy from a local git repository](~/deploy-bot-local-git.md) using continuous integration
- [Deploy from GitHub](~/deploy-bot-github.md) using continuous integration
- [Deploy from Visual Studio](~/deploy-bot-visual-studio.md)

## Connect the bot to one or more channels

After registering the bot with the Bot Framework and deployed it bot to the cloud, [configure the bot to connect on one or more channels](~/portal-configure-channels.md).

## Make a bot discoverable
To make a bot discoverable, [connect it to the Bing channel](~/channels/channel-bing.md). Users will be able to find the bot using Bing search and then interact with it using the channels it is configured to support.
Note that not all bots should be discoverable. For example, a bot designed for private use by company employees should not be made generally available through Bing. 

## Publish a bot
The process of publishing a bot to a channel means the bot has been submitted for review and approved.

To view the status of a review, open the bot in the [developer portal](https://dev.botframework.com/) and click **Channels**.

* Bing: Publish from the [configuration page](~/channels/channel-bing.md). 
* Cortana: Publish from the [Cortana dashboard](https://aka.ms/cortana-publish)
* Skype for Business: Publish from the [configuration page](~/channels/channel-skypeForBusiness.md). 

> [TIP]
> If the bot is not approved, the result will link to the reason why. After making the required changes, resubmit the bot for review.
