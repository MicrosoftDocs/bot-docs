---
title: Connect a Bot Framework bot to Omnichannel
description: Learn how to configure bots to use the omnichannel capabilities of the Chat Add-in for Dynamics 365 Customer Service.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: azure-ai-bot-service
ms.topic: how-to
ms.date: 12/27/2022
ms.custom:
  - template-how-to
  - evergreen
---

# Connect a bot to the Omnichannel channel

The Omnichannel channel lets you connect your bot to omnichannel capabilities of the Chat Add-in for Dynamics 365 Customer Service.

## Prerequisites

- Knowledge of [Basics of the Bot Framework Service](v4sdk/bot-builder-basics.md) and how to [Create a bot with the Bot Framework SDK](bot-service-quickstart-create-bot.md).
- The bot to connect to the channel.
- If you don't have an Azure account, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

## Connect your bot to the Omnichannel channel

To register your bot with your Omnichannel for Customer Service environment:

1. In the Azure portal, go to your bot resource.
1. Open the **Channels** blade and select **Omnichannel**.
1. On the **Configure Omnichannel** blade, select **Apply**.
1. Once you see the success message, close the **Configure Omnichannel** blade.

## Integrate your bot with the omnichannel feature

Perform the final steps to set up your bot user and routing in your Dynamics 365 organization.

For instructions, see [Integrate an Azure bot](/dynamics365/customer-service/configure-bot) in the Omnichannel for Customer Service docs.

## Additional information

You can design your bot so that it can hand off the conversation to a human agent. For more information, see [Transition conversations from bot to human](bot-service-design-pattern-handoff-human.md).

## Next steps

- For information about channel support in the Bot Connector Service, see [Connect a bot to channels](bot-service-manage-channels.md).
- For information about building bots, see [How bots work](v4sdk/bot-builder-basics.md) and the [Create a bot with the Bot Framework SDK](bot-service-quickstart-create-bot.md) quickstart.
- For information about deploying bots, see [Deploy your bot](bot-builder-deploy-az-cli.md) and [Set up continuous deployment](bot-service-build-continuous-deployment.md).
