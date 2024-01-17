---
title: Connect a Bot Framework bot to Slack
description: Learn how to configure bots to connect to the Slack channel and communicate with users via Slack.
keywords: connect a bot, bot channel, Slack bot, Slack messaging app, slack adapter
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: bot-service
ms.topic: how-to
ms.date: 08/05/2022
---

# Connect a bot to Slack

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

You can configure your bot to communicate with people through a Slack app. This article describes how to create a Slack app using Slack, connect your bot to your Slack app in Azure, and test your bot in Slack.

This article shows how to add a Slack channel to your bot in the Azure portal. For information on how to use a custom channel adapter, see [Additional information](#additional-information).

## Prerequisites

- An Azure subscription. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A bot published to Azure that you want to connect to Slack.
- Access to a Slack workspace with sufficient permissions to create and manage applications at [https://api.slack.com/apps](https://api.slack.com/apps). If you don't have access to a Slack environment, you can [create a workspace](https://www.slack.com).

## Create an app in Slack

You first create an application in Slack, which generates the information you need to configure the Slack channel for your bot in Azure.

1. Go to [Your Apps](https://api.slack.com/apps) panel and sign in to your Slack account.
1. Select **Create New App**, or **Create an App** if this is your first application.
    1. On the **Create an app** page, select **From scratch**.
    1. On the **Name app & choose workspace** page, for **App Name**, enter the name of your Slack application.
    1. For **Pick a workspace to develop your app in**, select a workspace for your app.
    1. Review and agree to the Slack API terms of service.
    1. Select **Create App**.

### Add a new redirect URL

Once the app is created, add a redirect URL to your app.

1. Select **OAuth & Permissions**.
1. In the resulting pane, under **Redirect URLs**, select **Add a new Redirect URL**.
1. In the input box, enter one of the following redirect URLs and select **Add**.
    - For a global bot, enter `https://slack.botframework.com`.
    - For a regional bot, enter `https://europe.slack.botframework.com` or `https://india.slack.botframework.com`.
1. Select **Save URLs**.

### Subscribe to bot events

After you add the redirect URL, subscribe your app to bot events to have Slack notify your bot of user activities at the URL you specify.
Subscribe to events based on the features your bot will use in Slack.

1. Select **Event Subscriptions**.
1. In the resulting pane, toggle **Enable Events** to **On**.
1. For **Request URL**, enter one of the following request URLs, where `{bot-name}` is the display name for your Azure Bot resource, without the braces.
    - For a global bot, enter `https://slack.botframework.com/api/Events/{bot-name}`.
    - For a regional bot, enter `https://europe.slack.botframework.com/api/Events/{bot-name}` or `https://india.slack.botframework.com/api/Events/{bot-name}`.
1. Under **Subscribe to bot events**, select **Add Bot User Event**, then subscribe to events. For example:
    - `member_joined_channel`
    - `member_left_channel`
    - `message.channels`
    - `message.groups`
    - `message.im`
    - `message.mpim`
1. Select **Save Changes**.

### Enable sending messages to the bot by the users

After you subscribe to bot events, enable users to message your bot.

1. Select **App Home**.
1. In the resulting pane, in the **Show Tabs** section under the **Messages Tab**, enable **Allow users to send Slash commands and messages from the messages tab**.

### Add and configure interactive messages

Optionally, enable interactive messages.

1. Select **Interactivity & Shortcuts**.
1. For **Request URL**:
    - For a global bot, enter `https://slack.botframework.com/api/Actions`.
    - For a regional bot, enter `https://europe.slack.botframework.com/Actions` or `https://india.slack.botframework.com/Actions`.
1. Select **Save changes**.

### Copy your app information

You'll need the following information to add the Slack channel to your bot. Always copy and store app credentials in a safe place.

1. Select **Basic Information**.
1. In the resulting pane, under **App Credentials**, locate **Client ID**, **Client Secret**, and **Signing Secret**.
1. Now, select **OAuth & Permissions**.
1. In the resulting pane, locate the **Scopes** section. Record the **Bot Token Scopes** for your app.

## Configure your bot in Azure

To complete this step, you'll need your Slack application credentials from the previous step.

1. Open the [Azure portal](https://portal.azure.com/).
1. Open the Azure Bot resource blade for your bot.
1. Open **Channels** and select **Slack**.
1. In **Slack Channel Configuration**, enter the information you copied in the previous steps.
    1. Enter the required Slack credentials for the application you created in Slack.
    1. Optionally, provide a **Landing Page URL** that Slack users will be redirected to after they add your bot.
    1. The **OAuth & Permissions Redirect URL** and **Event Subscription Request URL** values should match the values you entered in Slack to [add the redirect URL](#add-a-new-redirect-url) and to [subscribe to bot events](#subscribe-to-bot-events).

    :::image type="content" source="media/channels/slack-SubmitCredentials.png" alt-text="submit credentials":::

1. Select **Apply**.
1. You're redirected to Slack to finish installing your Slack app.
    - If the requested permissions look correct, select **Allow**.

Your bot's now configured to communicate with users in Slack.
Users in the workspace can now interact with your bot via the Slack app.

## Test your application in Slack

1. Sign in to the Slack workspace where you installed your app.
1. Under **Apps**, select your app.
1. In the resulting pane, send messages to the application.

## Additional information

> [!NOTE]
> As of June 2020 Slack channel supports Slack V2 permission scopes, which allow the bot to specify its capabilities and permissions in a more granular way. All newly configured Slack channels will use the V2 scopes. To switch your bot to the V2 scopes, delete and recreate the Slack channel configuration in the Azure portal **Channels** blade.

For more information about Slack support for bots, see the Slack API documentation:

- [Developer docs and guides](https://api.slack.com/docs)
- [Permission scopes](https://api.slack.com/scopes)
- [Understanding OAuth scopes for Bots](https://api.slack.com/tutorials/understanding-oauth-scopes-bot)

## Connect a bot to Slack using the Slack adapter

As well as the channel available in the Azure AI Bot Service to connect your bot with Slack, the [Bot Builder Community repos](https://github.com/BotBuilderCommunity/) define a custom channel adapter for Slack.

- For information on the C# adapter, see [the Adapters section](https://github.com/BotBuilderCommunity/botbuilder-community-dotnet#adapters) in the .NET community repo.
- For information on the JavaScript adapter, see [the Adapters section](https://github.com/BotBuilderCommunity/botbuilder-community-js#adapters) in the JavaScript community repo.
