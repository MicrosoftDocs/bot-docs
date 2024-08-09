---
title: Add a chatbot to M365 (preview)
description: Connect your bot to M365 so people in your organization can interact with it in Microsoft Copilot Studio preview.
keywords: "Publish, channel, M365"
ms.date: 08/08/2024
ms.topic: how-to
author: KendalBond007
ms.author: jameslew
ms.reviewer: yiba
manager: iawilt
ms.custom:
  - publication
  - authoring
  - ceX
  - bap-template
  - evergreen
ms.collection: virtual-agent
ms.service: copilot-studio
---

# Connect a Bot to Microsoft 365

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

You can configure your bot to communicate with people in Microsoft 365 applications. This article describes how to connect your bot to your M365 Extensions Channel in Azure, and then test your bot in Outlook. You'll be able to run your [message extensions](/microsoftteams/platform/messaging-extensions/what-are-messaging-extensions?tabs=dotnet) in Outlook and other Microsoft 365 channels with the help of this channel.

## Prerequisites

- An Azure subscription. If you don't already have one, create a free account before you begin.

- A bot published to Azure.

- A developer tenant in Outlook with custom app uploading or sideloading enabled. For more information, see [Prepare your Microsoft 365 tenant](/microsoftteams/platform/concepts/build-and-test/prepare-your-o365-tenant).

## Configure your bot in Azure

1. Open the [Azure portal](https://ms.portal.azure.com/#home).

2. Open the Azure Bot resource blade for your bot.

3. Open **Channels** and select **M365 Extensions Channel**:

   1. Select **Apply**.

## Test your bot in Outlook

To test your bot in Outlook, you need to publish the app in Teams and update the [Teams developer manifest](/microsoftteams/platform/resources/schema/manifest-schema) schema version to greater than 1.13 to enable your Teams message extension to run in Outlook.

For instructions on how to publish your app, see the Teams overview of how to [Distribute your Microsoft Teams app](/microsoftteams/platform/concepts/deploy-and-publish/apps-publish-overview). It and the associated articles cover how to:

- Choose and configure install options for your bot.

- Create your Teams app manifest, icon, and package.

- Upload your app to Teams.

- Publish your app to your org or to the Teams store.

For more about Message extensions in Outlook, see [Extend a Teams message extension across Microsoft 365 - Teams | Microsoft Learn](/microsoftteams/platform/m365-apps/extend-m365-teams-message-extension?tabs=manifest-teams-toolkit)

## Additional information

- For more about creating bots for Teams, see [Bots in Microsoft Teams](/microsoftteams/platform/bots/what-are-bots).

- To provide feedback and find more resources, see [Microsoft Teams developer community channels](/microsoftteams/platform/feedback).
