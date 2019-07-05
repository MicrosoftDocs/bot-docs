---
title: Connect a bot to Teams | Microsoft Docs
description: Learn how to configure a bot for access through the Team.
keywords: Teams, bot channel, configure Teams
author: kaiqb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 07/05/2019
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

## Additional information
For Microsoft Teams specific information, see Teams [documentation](https://docs.microsoft.com/en-us/microsoftteams/platform/overview). 
