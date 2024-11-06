---
title: Connect a bot to Azure Communication Services
description: Learn how to configure Bot Framework bots to use Azure Communication Services to communicate with users.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: azure-ai-bot-service
ms.topic: how-to
ms.date: 01/04/2023
ms.custom:
  - template-how-to
  - evergreen
---

# Connect a bot to Azure Communication Services

Azure Communication Services provides the APIs and SDKs to build multi-participant collaboration applications that can include a bot. This bot can greet users, perform intake tasks to determine the topic, and answer questions. Azure Communication Services allows you to hand off the conversation to a human agent. It also enables a supervisor or additional bot to monitor or participate in the conversation if necessary. Additionally, Azure Communication Services offers a [UI library](https://azure.github.io/communication-ui-library) to easily build conversational user experiences that can be embedded into your existing web pages.


## Prerequisites

- Knowledge of the [Basics of the Bot Framework Service](v4sdk/bot-builder-basics.md) and how to [Create a bot with the Bot Framework SDK](bot-service-quickstart-create-bot.md).
- A published bot that you want to connect to the channel.
- An Azure account with a current subscription. If you don't have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- An Azure Communication Services resource. If you don't have one, see [Create an Azure Communication Services resource](/azure/communication-services/quickstarts/create-communication-resource).
- .NET 6.0 or later.
- Visual Studio 2022 or later.

## Connect your bot to Azure Communication Services

1. In the Azure portal, go to your bot resource.
1. Open the **Channels** blade and select **Communication Services - Chat**.
1. On the Configure Communication blade, connect to your Azure Communication Services resource and save. For more information, see the Azure Communication Services documentation on how to  [enable the Azure Communication Services channel](/azure/communication-services/quickstarts/chat/quickstart-botframework-integration#step-3---enable-azure-communication-services-chat-channel).

Your bot is now registered with Azure Communication Services.

## Create a chat app and add your bot as a participant

Now that your bot is registered with Azure Communication Services, you can create a chat thread with your bot as a participant. 
For more information, see [Add a bot to your chat app](/azure/communication-services/quickstarts/chat/quickstart-botframework-integration) in the Azure Communication Services documentation.

## Additional information

The Azure Communications Services channel has a limit of 28 KB for message activities.

## Next steps

- For information on how to hand off the conversation to a human agent, see [Transition conversations from bot to human](bot-service-design-pattern-handoff-human.md) for more information.
- For information about channel support in the Bot Connector Service, see [Connect a bot to channels](bot-service-manage-channels.md).
- For information about building bots, see [How bots work](v4sdk/bot-builder-basics.md) and the [Create a bot with the Bot Framework SDK](bot-service-quickstart-create-bot.md) quickstart.
- For information about deploying bots, see [Deploy your bot](bot-builder-deploy-az-cli.md) and [Set up continuous deployment](bot-service-build-continuous-deployment.md).
- For information about [Azure Communication Services UI Library](https://azure.github.io/communication-ui-library)
