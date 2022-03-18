---
title: Configure an Azure Bot Service bot to run on one or more channels
description: A channel connects a communication application to a bot. Learn how to configure a bot to run a channel using the Azure portal, Direct Line, or a custom adapter.
keywords: bot, channel, Azure portal, Direct Line, custom adapter
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: mainguy
ms.service: bot-service
ms.topic: how-to
ms.date: 09/30/2021
ms.custom: abs-meta-21q1
monikerRange: 'azure-bot-service-4.0'
---

# Configure a bot to run on one or more channels

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

A channel is a connection between a communication application and a bot. A bot, registered with Azure, uses channels to help the bot communicate with users. You can configure a bot to connect to any of the standard channels such as Alexa, Facebook Messenger, and Slack. For more information, see [Azure Bot registration](bot-service-quickstart-registration.md). You can also connect a bot to your communication application using Direct Line as the channel. For more information, see [Connect a bot to Direct Line](bot-service-channel-connect-directline.md).

The Bot Framework allows you to develop a bot in a channel-agnostic way by normalizing messages that the bot sends to a channel.

- The service or an adapter translates communication between the Bot Framework Activity schema and the channel's schema.
- If the channel doesn't support all aspects of the activity schema, the Bot Connector Service tries to convert the message to a format that the channel does support. For example, if the bot sends a message that contains a card with action buttons to the email channel, the connector might send the card as an image and include the actions as links in the body of the email.
- For most channels, you must provide channel configuration information to run a bot on the channel. Most channels require that a bot have an account on the channel. Others, like Facebook Messenger, require a bot to have an application registered with the channel.

To configure a bot to connect to a channel, complete the following steps:

1. Sign in to the [Azure portal](https://portal.azure.com).
1. Select the bot that you want to configure.
1. In the left pane, select **Channels** under **Bot Management**.
1. In the right pane, select the icon of the channel you want to add to your bot.

    :::image type="content" source="./media/channels/connect-to-channels.png" alt-text="Connect to channels":::

After you've configured the channel, users on that channel can start using your bot.

## Channels list

The connection steps are different for each channel. See the related article in the table below more information.

|Channel|Description|
|:-|:-|
|[Alexa](bot-service-channel-connect-alexa.md) <img width="150px"/>|Communicate with users through Alexa devices that support Custom Skills.|
|[Direct Line](bot-service-channel-directline.md)| Integrate a bot into a mobile app, web page, or other applications.|
|[Facebook](bot-service-channel-connect-facebook.md)|Connect a bot to both Facebook Messenger and Facebook Workplace, so that it can communicate with users on both platforms.|
|[Kik](bot-service-channel-connect-groupMe.md)|Configure a bot to communicate with users through the Kik messaging app.|
|[LINE](bot-service-channel-connect-line.md)|Configure a bot to communicate with users through the LINE app.|
|[Microsoft Teams](channel-connect-teams.md)|Configure a bot to communicate with users through Microsoft Teams.|
|[Office 365 email](bot-service-channel-connect-email.md)|Enable a bot to communicate with users via Office 365 email.|
|[Omnichannel](bot-service-channel-omnichannel.md)|Integrate a bot to start a conversation with a customer, provide automated responses, and then shift the conversation to a human agent if required.|
|[Search](bot-service-channel-connect-search.md)|Enable a bot to answer user queries via Dynamics 365 federated search.|
|[Skype](bot-service-channel-connect-skype.md)|Configure a bot to communicate with users through Skype.|
|[Skype for Business](bot-service-channel-connect-skypeforbusiness.md)|Configure a bot to communicate with users through Skype for Business.|
|[Slack](bot-service-channel-connect-slack.md)|Configure a bot to communicate with users through Slack.|
|[Telegram](bot-service-channel-connect-telegram.md)|Configure a bot to communicate with users through Telegram.|
|[Telephony](bot-service-channel-connect-telephony.md)|Configure a bot to communicate with users through the Bot Framework Telephony channel.|
|[Twilio](bot-service-channel-connect-twilio.md)|Configure a bot to communicate with users through the Twilio cloud communication platform.|
|[WeChat](bot-service-channel-connect-wechat.md)|Configure a bot to communicate with users using the WeChat platform.|
|[Web Chat](bot-service-channel-connect-webchat.md)| Automatically configured for you when you create a bot with the Bot Framework Service.|
|[Additional channels](bot-service-channel-additional-channels.md)|Additional channels available as an adapter through [Botkit provided platforms](https://github.com/howdyai/botkit/blob/main/packages/docs/platforms/index.md) and [community repositories](https://botkit.ai/docs/v4/platforms/).|

## Select the protocol schema transformation version

As described above, a channel converts incoming messages from other services to the Bot Framework protocol schema. Likewise, messages sent by the bot to other services are transformed from the Bot Framework native schema to the format of these services. This process is called _schema transformation_. The Bot Framework Service maintains backward compatibility of the protocol to avoid changing the behavior of existing bots.

Occasionally, a change in the schema transformation process needs to take place that can, potentially, change the behavior of the existing bots. An example of such a change could be any bug fix, if some of the users have taken a dependency on the existing (however erroneous) behavior. Another example of such a change would be updates or improvements in other services that would benefit bots; however adopting these updates can, potentially, change the existing behavior.

By controlling the _schema transformation version_ of their bots, bot developers can control when (if ever) to enable new behavior. By default, newly created bots get the most recent schema transformation version. Existing bots can be upgraded to the newest version when they're ready to take advantage of the improvements introduced in this version. Any bot can be upgraded or downgraded at any time.

You can change your bot's schema transformation version in the **Configuration** pane:

:::image type="content" source="./media/channels/schema-transform-version.png" alt-text="The Schema Transformation Version field in the Configuration pane":::

### Supported schema transformation versions

- **Version 1.3**
  - Date introduced: May 2021
  - Changes:
    - Direct Line: Remove Deserialize/Reserialize of Adaptive Cards. The content of Adaptive Cards will be passed to the client as is.

- **Version 1.2**
  - Date introduced: April 2021
  - Changes:
    - Slack channel: Attachment name is used for Message Text value.
    - Facebook channel: Upgrade to [Facebook Graph API v9.0](https://developers.facebook.com/docs/graph-api/changelog/version9.0/).

- **Version 1.1**
  - Date introduced: April 2021
  - Changes:
    - Telegram channel: Use [MarkdownV2 syntax](https://core.telegram.org/bots/api#markdownv2-style) for all markdown.

- **Version 1.0**
  - Original version

## Connect your bot to one or more channels

The publishing process is different for each channel. For more information, see the article for each specific channel.

## Next steps

The SDK includes samples that you can use to build bots. Visit the [Samples repo on GitHub](https://github.com/Microsoft/BotBuilder-samples) to see a list of samples.
