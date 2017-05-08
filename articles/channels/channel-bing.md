---
title: Connect a bot to Bing | Microsoft Docs
description: Learn how to configure and publish a bot to the Bing channel.
author: JaymeMPerlman
ms.author: v-jaype
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date:
ms.reviewer:
---
# Connect a bot to Bing 

Publishing a bot to Bing allows users to discover and interact with the bot via a web chat control.

> [!NOTE] 
> Read the [Best Practices][practices], [Review Guidelines][review], [Terms of Use][terms] and [Code of Conduct][conduct] before publishing the bot. 

## General bot information
All fields marked with an asterisk (*) are required. Bots must be [registered](~/portal-register-bot.md) on the Bot Framework before they can be connected to Bing.

Open the bot on the [Developer Portal](https://dev.botframework.com/), click the **Channels** tab, and then click **Bing**.

![General bot information](~/media/channels/bing-general.png)

### Display Name 
Enter a name for this bot. This is the name that will appear in search results.

### Description  
Describe the bot's purpose and function. The bot must operate as described in its bot description.

### Website 
Link to a website with more information about this bot.

## Bot category
Categories provide another way for users to find bots on the Bing Bot directory, Users can filter by category to find all "entertainment bots", "music bots", "news bots", and so on.

![Bot category](~/media/channels/bing-category.png)

### Select category 
Choose the most appropriate category for this bot.
### Tags for this bot 
Enter appropriate tags to help users find this bot.<!--to help users search for bot? keywords?-->

## Publisher information
<!--The bot publisher is defined as the entity making the bot available to end users.-->
This is the contact information Microsoft will use to communicate with the bot's publisher. 

![Bot publisher](~/media/channels/bing-publisher.png)

### Publisher name 
Enter the name of person to contact about this bot.
### Publisher email 
Enter an email address to use for bot-related communications.
### Publisher phone  
Enter a phone number to use for bot-related communications.

## Privacy and terms of use
These are required for published bots.

![Privacy statement](~/media/channels/bing-privacy.png)
 
 ### Privacy Statement URL 

If this bot handles users' personal data, provide a link to the applicable privacy policy. The [Code of Conduct][conduct] contains third party resource links to help create a privacy policy.

### Terms of use URL 

A link to the bot's Terms of Service is required. The [Terms of Use][terms] contains sample terms to help create an appropriate Terms of Service document.

### Submit for review

Click **Submit for review**.

After submitting this bot for review, click the **Test on Bing** link to preview a sample of how the bot will appear on Bing. The bot logo will not appear in the sample, but it will appear once the bot is published.

> [!NOTE] 
> Do **not** distribute the test link. Its only purpose is to provide a preview before the bot is approved.

The review process takes a few business days and you may be contacted. Bots that are approved and enabled will appear in Bing search results. Bots that are not approved may be resubmitted if the required changes are made.

[conduct]: http://aka.ms/bf-conduct
[practices]: http://docs.botframework.com/directory/best-practices/
[review]: http://docs.botframework.com/directory/review-guidelines/
[terms]: https://aka.ms/bf-terms