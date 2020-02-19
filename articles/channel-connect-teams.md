---
title: Connect a bot to Teams - Bot Service
description: Learn how to configure a bot for access through the Team.
keywords: Teams, bot channel, configure Teams
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 08/26/2019
---
# Connect a bot to Teams

To add the Microsoft Teams channel, open the bot in the [Azure portal](https://portal.azure.com), click the **Channels** blade, and then 
click **Teams**.

![Add Teams channel](media/teams/connect-teams-channel.png)

Next, click **Save**.

![Save Teams channel](media/teams/save-teams-channel.png)

After adding the Teams channel, go to the **Channels** page and click on **Get bot embed code**.

![Get embed code](media/teams/get-embed-code.png)

- Copy the _https_ part of the code that is showin in the **Get bot embed code** dialog. For example, `https://teams.microsoft.com/l/chat/0/0?users=28:b8a22302e-9303-4e54-b348-343232`. 

- In the browser, paste this address and then choose the Microsoft Teams app (client or web) that you use to add the bot to Teams. You should be able to see the bot listed as a contact that you can send messages to and recieves messages from in Microsoft Teams. 

> [!IMPORTANT] 
> Adding a bot by GUID, for anything other than testing purposes, is not recommended. Doing so severely limits the functionality of a bot. Bots in production should be added to Teams as part of an app. See [Create a bot](https://docs.microsoft.com/microsoftteams/platform/concepts/bots/bots-create) and [Test and debug your Microsoft Teams bot](https://docs.microsoft.com/microsoftteams/platform/concepts/bots/bots-test).


## Additional information
For Microsoft Teams specific information, see Teams [documentation](https://docs.microsoft.com/microsoftteams/platform/overview). 
