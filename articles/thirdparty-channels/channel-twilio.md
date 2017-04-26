---
title: Connect a bot to Twilio | Microsoft Docs
description: Learn how to configure a bot's connection to Twilio.
author: JaymeMPerlman
ms.author: v-jaype
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date:
ms.reviewer:

---
# Connect a bot to Twilio

Configure a bot to communicate using the Twilio cloud communication platform.

## Log in to or create a Twilio account for sending and receiving SMS messages

https://www.twilio.com/try-twilio

## Create a TwiML Application

https://www.twilio.com/user/account/messaging/dev-tools/twiml-apps/add 

![Create app](~/media/channels/twi-StepTwiml.png)

## Select or add a phone number

https://www.twilio.com/user/account/phone-numbers/incoming 

Click on the number to add it to the TwiML application you created.

![Set phone number](~/media/channels/twi-StepPhone.png)

## Specify application to use for Messaging
In the **Messaging** section, set the **TwiML App** to the name of the TwiML App you just created.
Copy the **Phone Number** value for later use.

![Specify app](~/media/channels/twi-StepPhone2.png)

## Gather credentials

https://www.twilio.com/user/account/settings 

Click on the lock icon to see the Auth Token.

![Gather app credentials](~/media/channels/twi-StepAuth.png)

## Submit credentials

Enter the phone number, accountSID and Auth Token you copied earlier and click **Submit Twilio Credentials**.


Check **Enable this bot on SMS**.

Click **I'm done configuring SMS**

