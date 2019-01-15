---
title: Connect a bot to Skype | Microsoft Docs
description: Learn how to configure a bot for access through the Skype interface.
keywords: skype, bot channels, configure skype, publish, connect to channels
author: v-ducvo
ms.author: RobStand
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 10/11/2018
---

# Connect a bot to Skype

Skype keeps you connected with users through instant messaging, phone, and video calls. Extend this functionality by building bots that users can discover and interact with through the Skype interface.

To add the Skype channel, open the bot in the [Azure Portal](https://portal.azure.com/), click the **Channels** blade, and then click **Skype**.

![Add Skype channel](~/media/channels/skype-addchannel.png)

This will take you to the **Configure Skype** settings page.

![Configure Skype channel](~/media/channels/skype_configure.png)

You need to configure settings in **Web control**, **Messaging**, **Calling**, **Groups** and **Publish**. Let's go over them one by one.

## Web control

To embed the bot into your website, click the **Get embed code** button from the **Web control** section. This will direct you to the Skype for Developers page. Follow the instructions there to get the embed code.

## Messaging

This section configures how your bot sends and receives messages in Skype.

## Calling

This section configures the calling feature of Skype in your bot. You can specify whether **Calling** is enabled for your bot and if enabled, whether IVR functionality or Real Time Media functionality is to be used.

## Groups

This section configures whether your bot can be added to a group and how it behaves in a group for messaging and is also used to enable Group Video Calls for Calling bots.

## Publish

This section configures the publish settings of your bot. All fields labeled with a * are required fields.

Bots in **Preview** are limited to 100 contacts. If you need more than 100 contacts, submit your bot for review. Clicking **Submit for Review** will automatically make your bot searchable in Skype if accepted. If your request cannot be approved, you will be notified as to what you need to change before it can be approved.

> [!TIP]
> If you are wanting to submit your bot for review, keep in mind it must meet the [skype certification checklist](https://github.com/Microsoft/skype-dev-bots/blob/master/certification/CHECKLIST.md) before it will be accepted.

After finishing configuration, click **Save** and accept the **Terms of Service**. The Skype channel is now added to your bot.

## Next steps

* [Skype for Business](bot-service-channel-connect-skypeforbusiness.md)
