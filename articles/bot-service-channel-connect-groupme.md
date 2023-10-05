---
title: Connect a Bot Framework bot to GroupMe
description: Learn how to configure bots to connect to the GroupMe channel and to communicate with users via the GroupMe group messaging app.
keywords: bot channel, GroupMe, create GroupMe
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: bot-service
ms.topic: how-to
ms.date: 08/05/2022
---

# Connect a bot to GroupMe

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

You can configure your bot to communicate with people through GroupMe. This article describes how to create a GroupMe app using the GroupMe developers site and connect your bot to your GroupMe app in Azure.

## Prerequisites

- An Azure subscription. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A bot published to Azure that you want to connect to GroupMe.
- A GroupMe account. If you don't have a GroupMe account, [sign up for a new account](https://web.groupme.com/signup).

## Create a GroupMe application

1. Go to the GroupMe developers site and sign in to your account.
1. [Create a GroupMe application](https://dev.groupme.com/applications/new) for your bot.
    1. Enter a name for your application.
    1. For the **Callback URL**:
        - For a global bot, enter `https://groupme.botframework.com/Home/Login`.
        - For a regional bot, enter `https://europe.groupme.botframework.com/Home/Login`.
    1. Enter the rest of the information requested.
    1. Agree to GroupMe's terms of use and branding standards.
    1. Select **Save** to complete creation of the app.

### Get your app credentials

Copy the following information from the **Details** tab for your application:

1. Copy your _client ID_. This is the value for the `client_id` query parameter in the **Redirect URL**.
    For example, if your redirect URL is `https://oauth.groupme.com/oauth/authorize?client_id=my-client-id`, then your client ID is `my-client-id`.
1. Copy your **Access Token** value.

## Configure your bot in Azure

To complete this step, you'll need your GroupMe application credentials from the previous step.

1. Open the [Azure portal](https://portal.azure.com/).
1. Open the Azure Bot resource blade for your bot.
1. Open **Channels** and select **GroupMe**.
1. In **GroupMe Channel Configuration**, enter the information you copied in the previous steps.
    1. Under **GroupMe credentials**, enter the **Access Token** and **Client ID** for your GroupMe app.
    1. Optionally, enable direct messaging in the channel. For more information, see [What is a Direct Message in GroupMe?](https://support.microsoft.com/office/what-is-a-direct-message-in-groupme-197fb53e-9699-4e14-a35e-d6fa12ea9875)
    1. Select **Apply**.

## Additional information

For more about applications in GroupMe, see [GroupMe's API Documentation](https://dev.groupme.com/).
