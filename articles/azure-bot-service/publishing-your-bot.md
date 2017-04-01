---
title: publish your bot using Azure Bot Service | Microsoft Docs
description: Learn how to publish your bot using Azure Bot Service.
keywords: Bot Framework, Azure Bot Service, publish to Azure
author: RobStand
manager: rstand
ms.topic: bot-service-article
ms.prod: botframework
ms.service: Azure Bot Service
ms.date: 3/21/2017
ms.reviewer:
#ROBOTS: Index
---

# Publishing your bot

Now that you’ve written your bot and added it to one or more channels, it’s time to publish it to the Bot Directory and Skype apps. But first, we need to do a quick review of your bot. For information about the review process, see Bot review guidelines. You should review the [guidelines](~/deploy/review-guidelines.md) before submitting your bot for review to ensure that it complies with the guidelines.

If your bot passes review, it’s added to the [Bot Directory](~/directory-add-bot.md). The directory is a public directory of all bots that were registered and published with Microsoft Bot Framework. Users can select your bot in the directory and add it to one or more of the configured channels that they use.

The Publish page provides the status of the publishing process. The status transitions from **Unavailable** to **In review** to Published as the bot moves through the publishing process.

![azurepublish](~/media/azure_publish1.png)

The first thing You’ll need to do is provide a PNG icon for your bot. The size of the icon is limited to 30 K.

Then, update the name of your bot and provide a description of it. The bot’s name defaults to the name of the deployment that you specified when you created the bot. You should change the name to something user friendly.

The description is displayed in the directory. Make sure it describes what your bot does in way that would compel a user to talk to it. The description is limited to 512 characters. The first 46 characters are displayed on your bot’s card in the directory, and the full description is displayed in your bot’s details page.

![azurepublish](~/media/azure_publish2.png)

Next, we need details about you. Your company’s name and email are required. It’s important that you provide a reachable email address because this is where we’ll send any correspondance. You must also provide links to your privacy statement and terms of use.

If you provide hashtags, specify them as a comma-delimited list. The list of hashtags is limited to 2,000 characters.

Languages is a comma-delimited list of ISO 639-1 language codes (for example, en for English) that identify the languages that your bot supports. By default, bots support English.

Your bot is available to run in all markets. To limit the markets that your bot can run in, click Show markets and select the markets as appropriate.

![azure publish](~/media/azure_publish3.png)

Last, provide any additional information that you think is relevant to the review, turn on **Automatically show bot in directories after review**, and click **Submit**.

If you need to exit before providing all of the details, click Save so you don’t have to start over.

