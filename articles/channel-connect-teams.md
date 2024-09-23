---
title: Connect a Bot Framework bot to Microsoft Teams
description: Learn how to configure bots to connect to the Microsoft Teams channel and communicate with users via Teams.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: azure-ai-bot-service
ms.topic: how-to
ms.date: 12/20/2022
ms.custom:
  - evergreen
---

# Connect a bot to Microsoft Teams

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

You can configure your bot to communicate with people via Microsoft Teams. This article describes how to create a Teams app in Teams, connect your bot to your Teams app in Azure, and then test your bot in Teams.

## Prerequisites

- An Azure subscription. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A bot published to Azure that you want to connect to Teams.
- A developer tenant in Teams with custom app uploading or sideloading enabled. For more information, see [Prepare your Microsoft 365 tenant](/microsoftteams/platform/concepts/build-and-test/prepare-your-o365-tenant).
- A valid Teams app package. For more information, see [Upload your app in Microsoft Teams](/microsoftteams/platform/concepts/deploy-and-publish/apps-upload).

## Configure your bot in Azure

1. Open the [Azure portal](https://portal.azure.com/).
1. Open the Azure Bot resource blade for your bot.
1. Open **Channels** and select **Microsoft Teams**:
    1. Read and agree to the terms of service.
    1. On the **Messaging** tab, select the cloud environment for your bot. For more information, see the [Post build](/microsoftteams/platform/concepts/app-fundamentals-overview) section of **Plan your app with Teams features**.
    1. Select **Apply**.
1. Select **Get bot embed code**, locate the embed code for Teams, and then copy the _https_ part of the code. For example, `https://teams.microsoft.com/l/chat/0/0?users=28:b8a22302e-9303-4e54-b348-343232`. You can use this code to test the bot in Teams.

> [!TIP]
>
> - The **Calling** tab supports the Teams calling feature. For more information, see [Register calls and meetings bot for Microsoft Teams](/microsoftteams/platform/bots/calls-and-meetings/registering-calling-bot).
> - The **Publish** tab contains information about how to publish your Teams app to the Teams Store.
> - The **Microsoft Azure operated by 21Vianet** does not support the **Get bot embed code** feature. To test the Teams channel, create the Teams app and deploy it using the steps mentioned below.

## Test your bot in Teams

Bots in production should be added to Teams as part of a Teams app. For more information, see [Test your app](/microsoftteams/platform/concepts/build-and-test/test-app-overview).

> [!IMPORTANT]
> Adding a bot by GUID, for anything other than testing purposes, isn't recommended. Doing so severely limits the functionality of a bot. Bots in production should be added to Teams as part of an app.

1. In your browser, open the URL you copied from your embed code, then choose the Microsoft Teams app (client or web) that you use to add the bot to Teams. You should be able to see the bot listed as a contact that you can send messages to and receive messages from in Microsoft Teams.
1. Interact with your bot in Teams.

> [!TIP]
> Use one bot channel registration per environment, since your endpoint changes when you switch between local development, staging, and production environments.
>
> Deleting the Teams channel registration will cause a new pair of keys to be generated when it's re-enabled. This invalidates all 29:xxx and a:xxx IDs that the bot may have stored for proactive messaging.

## Publish your bot in Teams

For instructions on how to publish your app, see the Teams overview of how to [Distribute your Microsoft Teams app](/microsoftteams/platform/concepts/deploy-and-publish/apps-publish-overview). It and the associated articles cover how to:

- Choose and configure install options for your bot
- Create your Teams app manifest, icon, and package
- Upload your app to Teams
- Publish your app to your org or to the Teams store

## Additional information

- For more about Teams app development, see [Build apps for Microsoft Teams](/microsoftteams/platform/overview) and [Get started](/microsoftteams/platform/get-started/get-started-overview).
- For more about creating bots for Teams, see [Bots in Microsoft Teams](/microsoftteams/platform/bots/what-are-bots).
- For more about publishing and testing a bot in Teams, see [Distribute your Microsoft Teams app](/microsoftteams/platform/concepts/deploy-and-publish/apps-publish-overview) and [Test your app](/microsoftteams/platform/concepts/build-and-test/test-app-overview).
- To provide feedback and find additional resources, see [Microsoft Teams developer community channels](/microsoftteams/platform/feedback).
