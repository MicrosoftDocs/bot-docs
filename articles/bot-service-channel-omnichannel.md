---
title: Connect a Bot Framework bot to Omnichannel
description: Learn how to configure bots to use Omnichannel to communicate with users in your Dynamics 365 organization.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: mainguy
ms.service: bot-service
ms.topic: how-to
ms.date: 09/30/2021
ms.custom: template-how-to
---

# Connect a bot to Omnichannel

Omnichannel lets you connect your bot to your Dynamics 365 organization.

## Prerequisites

- Knowledge of [Basics of the Bot Framework Service](v4sdk/bot-builder-basics.md) and how to [Create a bot with the Bot Framework SDK](bot-service-quickstart-create-bot.md).
- The bot to connect to the channel.
- If you don't have an Azure account, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

## Connect your bot to Omnichannel

The following instructions show you how to connect a bot to Omnichannel.

1. In the Azure portal, go to your bot resource.
1. Open the **Channels (Preview)** pane and select **Omnichannel**.
1. On the **Configure Omnichannel** pane, select **Apply**.
1. Once you see the success message, close the **Configure Omnichannel** pane.

Your bot is now registered with Omnichannel.

## Set up your bot user and routing

Perform the final steps to set up your bot user and routing in your Dynamics 365 organization.

For instructions, see [Integrate an Azure bot](/dynamics365/customer-service/configure-bot) in the Omnichannel docs.

## Additional information

You can design your bot so that it can hand off the conversation to a human agent. See [Transition conversations from bot to human](/azure/bot-service/bot-service-design-pattern-handoff-human) for more information.

## Next steps

- For information about channel support in the Bot Connector Service, see [Connect a bot to channels](bot-service-manage-channels.md).
- For information about building bots, see [How bots work](v4sdk/bot-builder-basics.md) and the [Create a bot with the Bot Framework SDK](bot-service-quickstart-create-bot.md) quickstart.
- For information about deploying bots, see [Deploy your bot](bot-builder-deploy-az-cli.md) and [Set up continuous deployment](bot-service-build-continuous-deployment.md).
