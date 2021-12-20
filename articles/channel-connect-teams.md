---
title: Connect a bot to Microsoft Teams in Bot Framework SDK
description: Learn how to connect a bot to Microsoft Teams. Set up the bot as a contact to exchange messages.
keywords: Teams, bot channel, configure Teams
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: mainguy
ms.service: bot-service
ms.topic: how-to
ms.date: 09/02/2021
---

# Connect a bot to Microsoft Teams

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

Bots in production should be added to Microsoft Teams as part of an app. Read [Create a bot](/microsoftteams/platform/concepts/bots/bots-create) and [Test and debug your Microsoft Teams bot](/microsoftteams/platform/concepts/bots/bots-test) in the Microsoft Teams documents for more information.

## Test a bot in Microsoft Teams

> [!IMPORTANT]
> Adding a bot by GUID, for anything other than testing purposes, is not recommended. Doing so severely limits the functionality of a bot. Bots in production should be added to Teams as part of an app. Read [Create a bot](/microsoftteams/platform/concepts/bots/bots-create) and [Test and debug your Microsoft Teams bot](/microsoftteams/platform/concepts/bots/bots-test) in the Microsoft Teams documents for more information.

To add the Microsoft Teams channel, open the bot in the [Azure portal](https://portal.azure.com), click the **Channels** blade, and then
click **Teams**.

![Add Teams channel](media/teams/connect-teams-channel.png)

Next, click **Save**.

![Save Teams channel](media/teams/save-teams-channel.png)

After adding the Teams channel, go to the **Channels** page and click on **Get bot embed code**.

![Get embed code](media/teams/get-embed-code.png)

- Copy the _https_ part of the code that is shown in the **Get bot embed code** dialog. For example, `https://teams.microsoft.com/l/chat/0/0?users=28:b8a22302e-9303-4e54-b348-343232`.

- In the browser, paste this address and then choose the Microsoft Teams app (client or web) that you use to add the bot to Teams. You should be able to see the bot listed as a contact that you can send messages to and receives messages from in Microsoft Teams.

> [!NOTE]
> When switching between local development, staging, or production environments, use one bot channel registration per environment, to avoid changing the endpoint repeatedly.
>
> Deleting the Teams channel registration will cause a new pair of keys to be generated when it is re-enabled. This invalidates all 29:xxx and a:xxx IDs that the bot may have stored for proactive messaging.

## Additional information

- For Microsoft Teams specific information, see [Build apps for Microsoft Teams](/microsoftteams/platform/overview). 
- To provide feedback and find additional resources, see [Microsoft Teams developer community channels](/microsoftteams/platform/feedback). 
