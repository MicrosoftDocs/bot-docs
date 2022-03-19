---
title: Connect a bot to Twilio - Bot Service
description: Learn how to configure bots to use Twilio to communicate with people. See how to connect bots to Twilio through a Twilio adapter or a TwiML application.
keywords: Twilio, bot channels, SMS, App, phone, configure Twilio, cloud communication, text
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: mainguy
ms.service: bot-service
ms.topic: how-to
ms.date: 11/18/2021
---

# Connect a bot to Twilio

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

You can configure your bot to communicate with people using the Twilio cloud communication platform.

## Connect a bot to Twilio using the Azure portal

To configure a bot to communicate using Twilio, create a TwilML application and then connect the bot.

To learn more about developing for Twilio, see the [Twilio SMS](https://www.twilio.com/docs/sms) documentation.

### Create a TwiML application

If you don't have a Twilio account, create a [new account](https://www.twilio.com/try-twilio).

Create a [TwiML application](https://support.twilio.com/hc/articles/223180928-How-Do-I-Create-a-TwiML-App-) following the instructions.

![Create app](media/channels/twi-StepTwiml.png)

Under **Properties**, enter a **FRIENDLY NAME**. In this tutorial, "My TwiML app" is an example. The **REQUEST URL** under **Voice** can be left empty. Under **Messaging**, the **Request URL** should be `https://sms.botframework.com/api/sms`.

### Select or add a phone number

Follow the instruction in the [Twilio documentation](https://support.twilio.com/hc/articles/223180048-Adding-a-Verified-Phone-Number-or-Caller-ID-with-Twilio) to add a verified caller ID via the console site. After you finish, you will see your verified number in **Active Numbers** under **Manage Numbers**.

![Set phone number](media/channels/twi-StepPhone.png)

### Specify application to use for voice and messaging

Click the number and go to **Configure**. Under both Voice and Messaging, set **CONFIGURE WITH** to be TwiML App and set **TWIML APP** to be My TwiML app. After you finish, click **Save**.

![Specify application](media/channels/twi-StepPhone2.png)

Go back to **Manage Numbers**, you will see the configuration of both Voice and Messaging are changed to TwiML App.

![Specified number](media/channels/twi-StepPhone3.png)

### Gather credentials

Go back to the [console homepage](https://www.twilio.com/console/), you will see your Account SID and Auth Token on the project dashboard, as shown below.

![Gather app credentials](media/channels/twi-StepAuth.png)

### Submit credentials

In a separate window, return to the [Bot Framework](https://dev.botframework.com/) site.

- Select **My bots** and choose the Bot that you want to connect to Twilio. This will direct you to the Azure portal.
- Select **Channels** under **Bot Management**. Click the Twilio (SMS) icon.
- Enter the Phone Number, Account SID, and Auth Token you record earlier. After you finish, click **Save**.

![Submit credentials](media/channels/twi-StepSubmit.png)

When you have completed these steps, your bot will be successfully configured to communicate with users using Twilio.

## Connect a bot to Twilio using the Twilio adapter

As well as the channel available in the Azure Bot Service to connect your bot with Twilio, the [Bot Builder Community repos](https://github.com/BotBuilderCommunity/) define a custom channel adapter for Twilio.

- For information on the C# adapter, see [the Adapters section](https://github.com/BotBuilderCommunity/botbuilder-community-dotnet#adapters) in the .NET community repo.
- For information on the JavaScript adapter, see [the Adapters section](https://github.com/BotBuilderCommunity/botbuilder-community-js#adapters) in the JavaScript community repo.
