---
layout: page
title: Publishing your bot
permalink: en-us/azure-bot-service/manage/publish/
weight: 13075
parent1: Azure Bot Service
parent2: Manage
---

Now that you've written your bot and added it to one or more [channels](/en-us/azure-bot-service/manage/channels/), it's time to publish it to Bot Directory and Skype apps. But first, we need to do a quick review of your bot. For information about the review process, see [Bot review guidelines](/en-us/directory/review-guidelines/). You should review the guidelines before submitting your bot for review to ensure that it complies with the guidelines. 

If your bot passes review, it’s added to [Bot Directory](https://bots.botframework.com/){:target="_blank"}. The directory is a public directory of all bots that were registered and published with Microsoft Bot Framework. Users can select your bot in the directory and add it to one or more of the configured channels that they use.

The Publish page provides the status of the publishing process. The status transitions from Unavailable to In review to Published as the bot moves through the publishing process.

[![publish 1](/en-us/images/azure-bots/azure_publish1.png)](/en-us/images/azure-bots/azure_publish1.png)

The first thing you'll need to do is provide a PNG icon for your bot. The size of the icon is limited to 30 KB.  

Then, update the name of your bot and provide a description of it. The bot's name defaults to the name of the deployment that you specified when you created the bot. You should change the name to something user friendly.  

The description is displayed in the directory so it needs to describe what your bot does. The description is limited to 512 characters. The first 46 characters are displayed on your bot's card in the directory, and the full description is displayed in your bot's details page.

[![publish 2](/en-us/images/azure-bots/azure_publish2.png)](/en-us/images/azure-bots/azure_publish2.png)

Next, we need details about you, the publisher. Your company's name and email are required. It's important that you provide a reachable email address because this is where we'll send any correspondance. You must also provide links to your privacy statement and terms of use.

If you provide hashtags, specify them as a comma-delimited list. The list of hashtags is limited to 2,000 characters.

Languages is a comma-delimited list of ISO 639-1 language codes (for example, `en` for English) that identify the languages that your bot supports. By default, bots support English.

Your bot is available to run in all markets. To limit the markets that your bot can run in, click **Show markets** and select the markets as appropriate.

[![publish 3](/en-us/images/azure-bots/azure_publish3.png)](/en-us/images/azure-bots/azure_publish3.png)

Lastly, provide any additional information that you think is relevant to the review, turn on **Automatically show bot in directories after review**, and click **Submit**.

If you need to exit before providing all of the details, click **Save** so you don't have to start over.