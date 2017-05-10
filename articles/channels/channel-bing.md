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
Connecting your bot to the Bing channel allows people to discover and chat with your bot directly in the Bing search results page. You can build Bing bots using the [Bot Builder SDK](~/bot-builder-overview-getstarted.md) and connect them to the Bing channel. The Bing bot development experience allows you to publish your bots to Bing. People will also be able to chat with the bot directly on Bing.com.

![Bing business bot](~/media/channels/bing_business_bot.png)

To find a bot on Bing, go to [Bing.com](https://www.bing.com/) and enter a search query in the form of "*BotName* bot." For example, if the bot’s name is "Contoso," enter "Contoso bot."

The bot will appear in Bing search results as a rich answer when the user queries specifically for the bot.
Click **Chat on Bing** to launch the bot.

![Bing search results](~/media/channels/bing-contosoResult.png)

If a website has been associated with the bot, the link to the bot will appear **underneath** the website's Bing search result. Click the link to the bot to launch the bot.

## Publish your bot on Bing
Publishing a bot on Bing is easy. Open the bot on the [Developer Portal](https://dev.botframework.com/bots), click the Channels tab, and then click Bing.

![Connect to Bing](~/media/channels/connect-to-bing.png)

All fields marked with an asterisk (*) are required. Bots must be [registered](~/portal-register-bot.md) on the Bot Framework before they can be connected to Bing.

> [!NOTE] 
> Be sure you read the [Review Guidelines](~/portal-bot-review-guidelines.md), [Terms of Use][terms] and [Code of Conduct][conduct] before publishing a bot on Bing. 

Open the bot on the [Bot Framework Portal](https://dev.botframework.com/), click the **Channels** tab, and then click **Bing**.

![General bot information](~/media/channels/bing-general.png)

### Display Name 
Enter a name for this bot. This is the name that will appear in search results.

### Long Description  
Describe the bot's purpose and function. The bot must operate as described in its bot description.

### Website 
Link to a website with more information about this bot. There will be an additional verification step to link this bot to the site.

## Bot category
Categories provide another way for users to find bots on the Bing Bot directory. Users can filter by category to find all "entertainment bots", "music bots", "news bots", and so on.

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

### Submit for review and publish

Click **Submit for review**.

After submitting this bot for review, click the **Test on Bing** link to preview a sample of how the bot will appear on Bing. The bot logo will not appear in the sample, but it will appear once the bot is published.

> [!NOTE] 
> Do **not** distribute the test link. Its only purpose is to provide a preview before the bot is approved.

The review process takes a few business days and you may be contacted. After approval, users can find the bot in Bing search results and use Bing, or any other supported channel, to interact with the bot. 

Publishing to the Bing channel makes the bot discoverable by the widest possible audience. However, not all bots should be easily discoverable. For example, a bot designed for use by company employees should not be made generally available. A link to the bot can be privately distributed instead.

> [!TIP]
> To view the status of a review, open the bot in the [developer portal](https://dev.botframework.com/) and click **Channels**.
> If the bot is not approved, the result will link to the reason why. After making the required changes, resubmit the bot for review.


![Bing website search results](~/media/channels/bing-contosoWeb.png)

[conduct]: http://aka.ms/bf-conduct
[practices]: http://docs.botframework.com/directory/best-practices/
[review]: http://docs.botframework.com/directory/review-guidelines/
[terms]: https://aka.ms/bf-terms
