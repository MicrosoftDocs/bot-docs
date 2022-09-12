---
title: Connect a bot to LINE - Bot Service
description: Learn how to connect bots to LINE. See how to configure bots to communicate with people through the LINE app.
keywords: connect a bot, bot channel, LINE bot, credentials, configure, phone
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: bot-service
ms.topic: how-to
ms.date: 03/22/2022
---

# Connect a bot to LINE

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

You can configure your bot to communicate with people through the LINE app. This article describes how to create a LINE channel using the LINE Developers Console, connect your bot to your LINE channel in Azure, and test your bot in the LINE mobile app.

## Prerequisites

- An Azure account. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- An existing bot published to Azure.
- A mobile device with the LINE app and a QR reader.

## Create a channel in the LINE Developers Console

To build bots with LINE, you need to create a channel in the LINE Developers Console. Follow the steps in the documentation below that are relevant to you before continuing:

- If you already have a LINE Official account: [Adding a channel to your existing LINE Official Account](https://developers.line.biz/en/docs/messaging-api/getting-started/#using-oa-manager).
- If you don't have a LINE account: [Creating a channel on the LINE Developers Console](https://developers.line.biz/en/docs/messaging-api/getting-started/#using-console).

## Get values from your channel settings

Once you've confirmed your channel settings, you'll be directed to your channel's **Basic settings** page.

1. Scroll down to find the **Channel secret**. Copy the value and save it for later steps.
1. Scroll back up and select the **Messaging settings** tab.
1. At the bottom of the page you'll see a **Channel access token (long lived)** field, with an **Issue** button. Select that button to get your access token.
1. Copy and save the access token for later steps.

## Connect your LINE channel to your Azure bot

After obtaining the values above, you're ready to connect your Azure bot to LINE in the Azure portal.

1. Sign in to the [Azure portal](https://portal.azure.com/) and find your bot. Then select **Channels**.
1. Select **LINE** from the **Available channels** list.
1. Enter the **Channel Secret** and **Channel Access Token** you saved earlier. Then select **Apply**.
1. If your bot is successfully connected, the **Webhook URL** will appear. Copy and save the URL for later steps.

## Configure LINE webhook settings

After connecting your channel in Azure and obtaining your webhook URL, return to the LINE Developers Console to configure LINE webhook setting.

1. Go back to the [LINE Developers console](https://developers.line.biz/console/).
1. Select the channel you created earlier from **Recently visited channels**.
1. Select the **Messaging API** setting and scroll down to **Webhook settings**. Enter the **Webhook URL** from Azure and select **Update**.
1. Select the **Verify** button under the URL. A success message will appear if the webhook URL is properly configured.
1. Then enable **Use webhook**, shown below:

    :::image type="content" source="./media/channels/LINE-webhook-settings.png"  alt-text="LINE Webhook settings":::

    > [!IMPORTANT]
    > In LINE Developers Console, set the webhook URL before you enable **Use webhook**. Enabling webhooks with an empty URL won't set the enabled status, even though the UI may say otherwise.

1. After adding a webhook URL and enabling **Use webhook**, reload this page and verify that the changes were set correctly.

## Test your bot

Once you've completed these steps, your bot will be successfully configured to communicate with users on LINE. The steps below explain how to test your bot.

### Add your bot to your LINE mobile app

To test your bot, you need to use the LINE mobile app.

1. Scroll up in the **Messaging API** tab to see the bot's QR code.
1. Using a mobile device with the LINE app installed, scan the QR code and select the link that appears.
1. You should now be able to interact with your bot in your mobile LINE app and test your bot.

### Automatic messages

When you start testing your bot, it may send unexpected messages that aren't the ones you specified in the `conversationUpdate` activity.

To avoid sending these messages, take the following steps:

1. Go to the LINE Developers Console and select your channel. Then select the **Messaging API** tab.
1. Scroll down to the **LINE Official Account features** section. Find **Auto-reply messages** and select the **Edit** link.
1. A new page titled **Response settings** will open up. Under **Detailed settings** set **Auto-response** to *Disabled*.

    :::image type="content" source="./media/channels/LINE-detailed-settings.png"  alt-text="LINE Detailed settings":::

1. Alternatively, you can choose to keep these messages. Select **Auto-response message settings** to edit the auto response message.

## Additional information

### Troubleshooting

- If your bot isn't responding to any of your messages, go to your bot in Azure portal, and select **Test in Web Chat**.  
  - If the bot works there but doesn't respond in LINE, reload your LINE Developer Console page and repeat the webhook instructions above. Be sure you set the **Webhook URL** before enabling webhooks.
  - If the bot doesn't work in Web Chat, debug the bot issue and then finish configuring your LINE channel.
