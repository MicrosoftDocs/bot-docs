---
title: Connect a bot to Skype | Microsoft Docs
description: Learn how to configure a bot for access through the Skpye interface.
author: v-ducvo
ms.author: RobStand
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 2/1/2018
---

# Connect a bot to Skype

Skype keeps you connected with users through instant messaging, phone, and video calls. Extend this functionality by building bots that users can discover and interact with through the Skype interface.

To add the Skype channel, open the bot in the [Azure Portal](https://portal.azure.com/), click the **Channels** blade, and then click **Skype**. This will take you to the **Configure Skype** settings page. Fill out all the necessary information about your bot and click **Save** to connect the Skype channel. Accept the **Terms of Service** and the Skype channel will be added to your bot.

![Add Skype channel](~/media/channels/skype-addchannel.png)

## Web control

To embed the bot into your website, you can get the code by click the **Get embed code** button from the **Web control** section.

## Messaging 

This section configures how your bot sends and received messages in Skype.

## Calling

This section configures the calling feature of Skype in your bot. You can specify whether **Calling** is enabled fo your bot and if enabled, whether IVR functionality or Real Time Media functionality is to be used.

## Groups

This section configures whether your bot can be added to a group and how it behaves in a group for messaging and is also used to enable Group Video Calls for Calling bots.

## Publish

This section configures the publish settings of your bot. All fields labeled with a * are required fields.

Bots in **Preview** are limited to 100 contacts. If you need more than 100 contacts, submit your bot for review. Clicking **Submit for Review** will automatically make your bot searchable in Skype if accepted. If your request cannot be approved, you will be notified as to what you need to change before it can be approved. 

## Next steps

* [Skype for Business](bot-service-channel-connect-skypeforbusiness.md)
