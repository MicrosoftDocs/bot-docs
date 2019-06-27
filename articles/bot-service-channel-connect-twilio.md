---
title: Connect a bot to Twilio | Microsoft Docs
description: Learn how to configure a bot's connection to Twilio.
keywords: Twilio, bot channels, SMS, App, phone, configure Twilio, cloud communication, text
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 10/9/2018
---

# Connect a bot to Twilio

You can configure your bot to communicate with people using the Twilio cloud communication platform.

## Log in to or create a Twilio account for sending and receiving SMS messages

If you don't have a Twilio account, <a href="https://www.twilio.com/try-twilio" target="_blank">create a new account</a>.

## Create a TwiML Application

<a href="https://support.twilio.com/hc/en-us/articles/223180928-How-Do-I-Create-a-TwiML-App-" target="_blank">Create a TwiML application</a> following the instructions.

![Create app](~/media/channels/twi-StepTwiml.png)

Under **Properties**, enter a **FRIENDLY NAME**. In this tutorial we use "My TwiML app" as an example. The **REQUEST URL** under Voice can be left empty. Under **Messaging**, the **Request URL** should be `https://sms.botframework.com/api/sms`.

## Select or add a phone number

Follow the instructions <a href = "https://support.twilio.com/hc/en-us/articles/223180048-Adding-a-Verified-Phone-Number-or-Caller-ID-with-Twilio" target="_blank">here</a> to add a verified caller ID via the console site. After you finish, you will see your verified number in **Active Numbers** under **Manage Numbers**.

![Set phone number](~/media/channels/twi-StepPhone.png)

## Specify application to use for Voice and Messaging

Click the number and go to **Configure**. Under both Voice and Messaging, set **CONFIGURE WITH** to be TwiML App and set **TWIML APP** to be My TwiML app. After you finish, click **Save**.

![Specify application](~/media/channels/twi-StepPhone2.png)

Go back to **Manage Numbers**, you will see the configuration of both Voice and Messaging are changed to TwiML App.

![Specified number](~/media/channels/twi-StepPhone3.png)


## Gather credentials

Go back to the [console homepage](https://www.twilio.com/console/), you will see your Account SID and Auth Token on the project dashboard, as shown below.

![Gather app credentials](~/media/channels/twi-StepAuth.png)

## Submit credentials

In a separate window, return to the Bot Framework site at https://dev.botframework.com/. 

- Select **My bots** and choose the Bot that you want to connect to Twilio. This will direct you to the Azure portal.
- Select **Channels** under **Bot Management**. Click the Twilio (SMS) icon.
- Enter the Phone Number, Account SID, and Auth Token you record earlier. After you finish, click **Save**.

![Submit credentials](~/media/channels/twi-StepSubmit.png)

When you have completed these steps, your bot will be successfully configured to communicate with users using Twilio.

## Also available as an adapter

This channel is also [available as an adapter](https://botkit.ai/docs/v4/platforms/twilio-sms.html). To help you choose between an adapter and a channel, see [Currently available adapters](bot-service-channel-additional-channels.md#currently-available-adapters).