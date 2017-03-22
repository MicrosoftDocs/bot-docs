---
title: Publish a bot to the Bot Directory | Microsoft Docs
description: Learn how to add a bot to the Bot Directory.
keywords: Bot Framework, Bot Builder, publish bot, Bot Framework Portal, Bot Directory
author: kbrandl
manager: rstand
ms.topic: publish-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/06/2017
ms.reviewer:
#ROBOTS: Index
---

# Publish a bot to the Bot Directory

After you have [registered](bot-framework-publish-register.md) your bot with the Bot Framework, 
[deployed](bot-framework-publish-deploy.md) your bot to the cloud, 
and [configured](bot-framework-publish-configure.md) your bot to run on one or more channels, 
you can publish it to the <a href="https://bots.botframework.com/" target="_blank">Bot Directory</a>. 
The Bot Directory is a public directory of all bots that are registered and published with Microsoft Bot Framework. 
By publishing your bot to the Bot Directory, you're making it available for users to find it there and add it to the channel(s) that they use.

Publishing a bot to the Bot Directory is a three-step process:

[Step 1](#submit): You submit your bot for publication<br/>
[Step 2](#review): Microsoft reviews your bot to verify that it meets the requirements for publication to the Bot Directory<br/>
[Step 3](#publish): Microsoft adds your bot to the directory

##<a id="submit"></a> Step 1: Submit your bot for publication

> [!IMPORTANT]
> Before you proceed with the submission process, ensure that your bot complies with the [requirements that Microsoft will evaluate](#guidelines) during the review process. 

When you are ready to submit your bot for publication, start the process by completing the following steps:

1. Sign in to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Portal</a>.
2. Click **My bots**.
3. Select the bot that you want to publish.
4. On the bot dashboard, click **Publish**.

Then, on the Publish your bot page:

In the first section, you have the option to update the icon, name, and description that you provided when you registered your bot. 
If you've already provided this information and there are no changes, you can proceed to the next section.

In the next section, provide information about you, the publisher.

In the final section, enable **Automatically show bot in directories after review** and provide any additional information that should be considered during the review process. 

Click **Submit for review** to submit your bot for publication. 

> [!TIP]
> If you need to exit before providing all of the details, click **Save** so you don't have to start over.

> [!NOTE]
> The bot dashboard displays the current status of the publishing process. 
> The status transitions from Unavailable to In review to Published as the bot moves through the publishing process.

##<a id="review"></a> Step 2: Microsoft reviews your bot

After you submit your bot for publication, Microsoft reviews the bot to verify that it meets the [requirements for publication](#guidelines). 
When the review is complete, Microsoft will send you an email with the results of the review. 

If any issues were found during review, the email message will list the issues that you need to resolve before the bot can be published to the directory. 
Resolve the issues and [submit your bot for publication](#submit) again. 

If no issues were found during review, [Microsoft adds your bot to the Bot Directory](#publish). 

###<a id="guidelines"></a> Review guidelines

1. Your bot must do something meaningful that adds value to the user; it may not simply echo the user's input.

2. The bot's profile image, name, hastags and description must NOT:  
  
    - Be offensive or explicit (see [Developer Code of Conduct](https://aka.ms/bf-conduct))  
    - Include third party trademarks, service marks or logos  
    - Impersonate or imply endorsement by a third party  
    - Use names unrelated to the bot  
    - Use Microsoft logos, trademarks or service marks unless you have permission from Microsoft  
    - Be too long or verbose (the description may not exceed 512 characters)

3. Microsoft Bot Framework does not currently support payments within bots.  
  
    - Your bot may not transmit financial instrument details through the bot interface.  
    - However, your bot may provide links to secure payment services but you must disclose this in your bot's terms of use and privacy policy (and any profile page or website for the bot) before a user agrees to use your bot.  
    - You may not publish bots on Skype that include links or otherwise direct users to payment services for the purchase of digital goods.  

4. Your Terms of Service link is required for submission and publication to the Bot Directory. If your bot handles users' personal data, you must also provide a link to an applicable privacy policy that is in accordance with all applicable laws, regulations and policy requirements. In addition, you will need to ensure that you follow the privacy notice requirements as communicated in the <a href="https://aka.ms/bf-conduct" target="_blank">Developer Code of Conduct</a> for Microsoft Bot Framework.

5. The bot must operate as described in its description, profile, terms of use and privacy policy. You must notify Microsoft in advance if you make any material changes to your bot. Microsoft has the right, in its sole discretion, to intermittently review bots in the Bot Directory and remove bots from the directory without notice.

6. Your bot must operate in accordance with the requirements set forth in the <a href="http://aka.ms/bf-terms" target="_blank">Online Services Agreement</a> and Developer Code of Conduct for Microsoft Bot Framework.

7. Changes made to your bot's registration may require your bot to be reviewed again to ensure that it continues to meet the requirements stated here.

8. Although Microsoft will review your bot to confirm it meets certain minimum requirements prior to publication on the Bot Directory, you are solely responsible for: (1) your bot; (2) its content and actions; (3) compliance with all applicable laws; (4) compliance with any third party terms and conditions; and (5) compliance with the Online Services Agreement, Privacy Statement and Developer Code of Conduct for Microsoft Bot Framework. Microsoft's review and publication of your bot to the Bot Directory is not an endorsement of your bot.

> [!IMPORTANT]
> Microsoft will send all bot-related communications to the email address(es) that you specified when you submitted your bot publication request, 
> so be sure to monitor incoming email.

##<a id="publish"></a> Step 3: Microsoft adds your bot the the directory

When your bot passes review, Microsoft adds it to the Bot Directory. 
At that point, it is available for users to find it there and add it to the channel(s) that they use.