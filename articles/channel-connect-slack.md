---
title: Connect a bot to Slack | Microsoft Docs
description: Learn how to configure a bot's connection to Slack.
author: JaymeMPerlman
ms.author: v-jaype
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 06/06/2017
---

# Connect a bot to Slack

You can configure your bot to communicate with people using the Slack messaging app.

[!include[Channel Inspector intro](~/includes/snippet-channel-inspector.md)]

## Log in to Slack and create a Slack Application

Log into Slack and [create a Slack application](https://api.slack.com/applications/new).

![Set up bot](~/media/channels/slack-NewApp.png)

## Create application and assign a Development Slack team

Enter an App Name and select a Development Slack Team. If you are not already a member of a Development Slack Team, [create or join a team](https://slack.com/).

![Create app](~/media/channels/slack-CreateApp.png)

Completing this process creates your app and generates a Client ID and a Client Secret.

## Add a new Redirect URL

1. Select the **OAuth & Permissions** tab.
2. Select **Add a new Redirect URL**.
3. Enter https://slack.botframework.com, then click **Add**
4. Click **Save URLs**.

![Add Redirect URL](~/media/channels/slack-RedirectURL.png)

## Create a Slack Bot User

Click the **Bot Users** tab and then click **Add a Bot User**.

![Create bot](~/media/channels/slack-CreateBot.png)

Adding a Bot User allows you to assign a user name for your bot and choose whether it is always shown as online. Click **Add Bot User** to validate your settings, and then click **Save Changes**.

![Create bot](~/media/channels/slack-CreateBot-AddBotUser.png)

## Add and Configure Interactive Messages (optional)

If your bot will use Slack-specific functionality such as buttons, click the **Interactive Messages** tab and enable interactive messages.

![Enable messages](~/media/channels/slack-EnableMessages.png)

1. Enter the Request URL for Interactive Messages. The Request URL should be https://slack.botframework.com/api/Actions.
2. Click the **Enable Interactive Messages** button.
3. Click  **Save changes** once the URL is successfully validated.

![Enable messages](~/media/channels/slack-MessageURL.png)
