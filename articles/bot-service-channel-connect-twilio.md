---
title: Connect a bot to Twilio - Bot Service
description: Learn how to configure bots to use Twilio to communicate with people with a TwiML application or the Twilio adapter.
keywords: Twilio, bot channels, SMS, App, phone, configure Twilio, cloud communication, text
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: mainguy
ms.service: bot-service
ms.topic: how-to
ms.date: 08/05/2022
---

# Connect a bot to Twilio

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

You can configure your bot to communicate with people using the Twilio cloud communication platform. This article describes how to configure a bot to communicate using Twilio by creating a TwiML application and connecting the bot in the Azure portal.

## Prerequisites

- An Azure account. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- An existing bot published to Azure.

## Create a TwiML application

1. If you don't have a Twilio account, [create a new account](https://www.twilio.com/try-twilio). If you already have a Twilio account, continue to the next step.
1. Follow the instructions to [create a TwiML application](https://support.twilio.com/hc/articles/223180928-How-Do-I-Create-a-TwiML-App-).
    - Enter a **Friendly Name** for your TwiML app.
    - Under **Voice Configuration**, leave the **Request URL** empty
    - Under **Messaging Configuration**, set the **Request URL**:
      - For a global bot, enter `https://sms.botframework.com/api/sms`.
      - For a regional bot, enter `https://europe.sms.botframework.com/api/sms`.

### Select or add a phone number

Follow the instructions to [add a verified caller via the Console](https://support.twilio.com/hc/articles/223180048-Adding-a-Verified-Phone-Number-or-Caller-ID-with-Twilio). You can skip this if you already have a verified caller ID.

After you finish, you'll see your verified number in **Verified Caller IDs**.

### Specify TwiML app to use for voice and messaging

After adding a verified caller ID, configure your number's setting to use the TwiML app you created.

1. Select **Active numbers** under **Phone Numbers > Manage**. Select the number and go to **Configure**.
1. Under both **Voice & Fax** and **Messaging**, set **Configure With** to ***TwiML App**. Then set **TwiML APP** to the TwiML app you created earlier. After you finish, select **Save**.
1. Select **Active Numbers** again. You'll see the **Active Configuration** of both **Voice** and **Messaging** are set to your TwiML App.

### Gather credentials from Twilio

1. Go back to the [Twilio Console homepage](https://www.twilio.com/console/)
1. Under **Account Info**, you'll see your **Account SID** and **Auth Token** on the project dashboard, shown below. Copy and save these values for later steps.

    :::image type="content" source="media/channels/twi-StepAuth.png" alt-text="Gather app credentials from Twilio Console":::

### Enter Twilio credentials in the Azure portal

Now that you have the necessary values from Twilio, connect your bot to Twilio in the Azure portal.

1. In a separate window or tab, go to the [Azure portal](https://portal.azure.com/).
1. Select the bot that you want to connect to Twilio.
1. Under **Settings**, select **Channels**, then select the **Twilio (SMS)** icon from the list of **Available Channels**.
1. Enter the **Phone Number**, **Account Sid**, and **Auth Token** you saved earlier. After you finish, select **Apply**.

    :::image type="content" source="media/channels/twi-StepSubmit.png" alt-text="Enter Twilio credentials in Azure":::

Your bot's now successfully configured to communicate with Twilio users.

## Test your bot in Twilio

To test whether your bot is connected to Twilio correctly, send an SMS message to your Twilio number. When your bot receives the message, it sends a message back to you, echoing the text from your message.

## Additional information

To learn more about developing for Twilio, see the [Twilio SMS documentation](https://www.twilio.com/docs/sms).

### Connect a bot to Twilio using the Twilio adapter

In addition to using the available Azure Bot Service channel to connect your bot with Twilio, the [Bot Builder Community repos](https://github.com/BotBuilderCommunity/) define a custom channel adapter for Twilio.

- For information on the C# adapter, see [the Adapters section in the .NET community repo](https://github.com/BotBuilderCommunity/botbuilder-community-dotnet#adapters).
- For information on the JavaScript adapter, see [the Adapters section in the JavaScript community repo](https://github.com/BotBuilderCommunity/botbuilder-community-js#adapters).
