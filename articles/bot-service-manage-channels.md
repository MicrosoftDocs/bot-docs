---
title: Configure a bot to run on one or more channels - Bot Service
description: Learn how to configure a bot to run on one or more channels using the Bot Framework Portal.
keywords: bot channels, configure, cortana, facebook messenger, kik, slack, azure portal
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 07/31/2019
---

# Connect a bot to channels

A channel is a connection between communication applications and a bot. A bot, registered with Azure, uses channels to facilitate the communication with users.

You can configure a bot to connect to any of the standard channels such as Alexa, Cortana, Facebook Messenger, and Slack. For more information, see [Bot channels registration](bot-service-quickstart-registration.md).

In addition to the provided channels, you can also connect a bot to your communication application using **Direct Line** as the channel.

The Bot Framework allows you to develop a bot in a channel-agnostic way by normalizing messages that the bot sends to a channel. This involves the following:

- Convert the messages from the Bot Framework schema into the channel's schema.
- If the channel does not support all aspects of the Bot Framework schema, the Bot Connector service tries to convert the message to a format that the channel does support. For example, if the bot sends a message that contains a card with action buttons to the email channel, the connector may send the card as an image and include the actions as links in the message's text.
- For most channels, you must provide channel configuration information to run a bot on the channel. Most channels require that a bot have an account on the channel. Others, like Facebook Messenger, require a bot to have an application registered with the channel also.

To configure a bot to connect to a channel, complete the following steps:

1. Sign in to the [Azure Portal](https://portal.azure.com).
2. Select the bot that you want to configure.
3. In the Bot Service blade, click **Channels** under **Bot Management**.
4. Click the icon of the channel you want to add to your bot.

    ![Connect to channels](./media/channels/connect-to-channels.png)

After you've configured the channel, users on that channel can start using your bot.

## Connect a bot to a channel

The connection steps are different for each channel. See the related article in the table below more information. 

> [!div class="mx-tdBreakAll"]
> |Channel|Description|
> |-------------|----------|
> |[Alexa](bot-service-channel-connect-alexa.md) <img width="150px"/>|Communicate with users through Alexa devices that support Custom Skills.|
> |[Cortana](bot-service-channel-connect-cortana.md)| Send and receive voice messages in addition to textual conversation.|
> |[Direct Line](bot-service-channel-directline.md)| Integrate a bot into a mobile app, web page, or other applications.|
> |[Office 365 email](bot-service-channel-connect-email.md)|Enable a bot to communicate with users via Office 365 email.|
> |[Facebook](bot-service-channel-connect-facebook.md)|Connect a bot to both Facebook Messenger and Facebook Workplace, so that it can communicate with users on both platforms.|
> |[Kik](bot-service-channel-connect-groupMe.md)|Configure a bot to communicate with users through the Kik messaging app.|
> |[LINE](bot-service-channel-connect-line.md)|Configure a bot to communicate with users through the LINE app.|
> |[Microsoft Teams](channel-connect-teams.md)|Configure a bot to communicate with users through Microsoft Teams.|
> |[Skype](bot-service-channel-connect-skype.md)|Configure a bot to communicate with users through Skype.|
> |[Skype for Business](bot-service-channel-connect-skypeforbusiness.md)|Configure a bot to communicate with users through Skype for Busines.|
> |[Slack](bot-service-channel-connect-slack.md)|Configure a bot to communicate with users through Slack.|
> |[Telegram](bot-service-channel-connect-telegram.md)|Configure a bot to communicate with people through Telegram.|
> |[Twilio](bot-service-channel-connect-twilio.md)|Configure a bot to communicate with people using the Twilio cloud communication platform.|
> |[WeChat](bot-service-channel-connect-wechat.md)|Configure a bot to communicate with people using the WeChat platform.|
> |[Web Chat](bot-service-channel-connect-webchat.md)|When creating a bot with the Framework Bot Service, the Web Chat channel is automatically configured for you.|
> |[Webex](bot-service-adapter-connect-webex.md)|Configure a bot to communicate with people using the Webex.|
> |[Additional channels](bot-service-channel-additional-channels.md)|Additional channels available as an adapter, both through our [provided platforms](https://botkit.ai/docs/v4/platforms/) via Botkit, or accessible through the [community repositories](https://botkit.ai/docs/v4/platforms/)|


## Publish a bot
The publishing process is different for each channel.

[!INCLUDE [publishing](./includes/snippet-publish-to-channel.md)]

## Additional resources

The SDK includes samples that you can use to build bots. Visit the [Samples repo on GitHub](https://github.com/Microsoft/BotBuilder-samples) to see a list of samples.
