---
title: Configure a bot to run on one or more channels - Bot Service
description: Learn how to configure a bot to run on one or more channels using the Bot Framework Portal.
keywords: bot channels, configure, facebook messenger, kik, slack, azure portal
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 07/31/2019
---

# Connect a bot to channels

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

A channel is a connection between communication applications and a bot. A bot, registered with Azure, uses channels to facilitate the communication with users.

You can configure a bot to connect to any of the standard channels such as Alexa, Facebook Messenger, and Slack. For more information, see [Bot channels registration](bot-service-quickstart-registration.md).

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
> |:-|:-|
> |[Alexa](bot-service-channel-connect-alexa.md) <img width="150px"/>|Communicate with users through Alexa devices that support Custom Skills.|
> |[Direct Line](bot-service-channel-directline.md)| Integrate a bot into a mobile app, web page, or other applications.|
> |[Office 365 email](bot-service-channel-connect-email.md)|Enable a bot to communicate with users via Office 365 email.|
> |[Facebook](bot-service-channel-connect-facebook.md)|Connect a bot to both Facebook Messenger and Facebook Workplace, so that it can communicate with users on both platforms.|
> |[Kik](bot-service-channel-connect-groupMe.md)|Configure a bot to communicate with users through the Kik messaging app.|
> |[LINE](bot-service-channel-connect-line.md)|Configure a bot to communicate with users through the LINE app.|
> |[Microsoft Teams](channel-connect-teams.md)|Configure a bot to communicate with users through Microsoft Teams.|
> |[Skype](bot-service-channel-connect-skype.md)|Configure a bot to communicate with users through Skype.|
> |[Skype for Business](bot-service-channel-connect-skypeforbusiness.md)|Configure a bot to communicate with users through Skype for Busines.|
> |[Slack](bot-service-channel-connect-slack.md)|Configure a bot to communicate with users through Slack.|
> |[Telegram](bot-service-channel-connect-telegram.md)|Configure a bot to communicate with users through Telegram.|
> |[Telephony](bot-service-channel-connect-telephony.md)|Configure a bot to communicate with users through the Bot Framework Telephony channel.|
> |[Twilio](bot-service-channel-connect-twilio.md)|Configure a bot to communicate with users through the Twilio cloud communication platform.|
> |[WeChat](bot-service-channel-connect-wechat.md)|Configure a bot to communicate with users using the WeChat platform.|
> |[Web Chat](bot-service-channel-connect-webchat.md)| Automatically configured for you when you create a bot with the Bot Framework Service.|
> |[Webex](bot-service-adapter-connect-webex.md)|Configure a bot to communicate with users using the Webex.|
> |[Additional channels](bot-service-channel-additional-channels.md)|Additional channels available as an adapter through [provided platforms](https://botkit.ai/docs/v4/platforms/) via Botkit and [community repositories](https://botkit.ai/docs/v4/platforms/).|

## Bot schema transformation version

As described above, a channel converts incoming messages from other services to the Bot Framework protocol schema. Likewise, messages sent by the bot to other services are transformed from the Bot Framework native schema to the format of these services. This process is called _schema transformation_. The Bot Framework Service maintains backward compatibility of the protocol in order to avoid changing behavior of the existing bots.

Occasionally, a change in the schema transformation process needs to take place that can, potentially, change the behavior of the existing bots. An example of such a change could be any bug fix if a subset of the users have taken a dependency on the existing (however erroneous) behavior. Another example of such a change would be updates or improvements in other services that would benefit bots; however adopting these updates can, potentially, change the existing behavior.

By controlling the _schema transformation version_ of their bots, bot developers can control when (if ever) to enable new behavior. By default, newly created bots get the most recent schema transformation version. Existing bots can be upgraded to the newest version when they are ready to take advantage of the improvements introduced in this version. Any bot can be upgraded or downgraded at any time.

You can change your bot's schema transformation version in the **Configuration** pane:

:::image type="content" source="./media/channels/schema-transform-version.png" alt-text="The Schema Transformation Version field in the Configuration pane":::

### Supported schema transformation versions

| Schema transformation version   | Date introduced | Description     |
| --------------------------------| ----------- | --------------------------------------------------------------- |
| 1.1                             | April 2021  | Change Telegram channel to use [MarkdownV2 syntax](https://core.telegram.org/bots/api#markdownv2-style). |
| 1.0                             | --  | Initial version |

## Publish a bot

The publishing process is different for each channel.

[!INCLUDE [publishing](./includes/snippet-publish-to-channel.md)]

## Additional resources

The SDK includes samples that you can use to build bots. Visit the [Samples repo on GitHub](https://github.com/Microsoft/BotBuilder-samples) to see a list of samples.
