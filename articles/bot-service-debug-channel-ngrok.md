---
title: Debug a channel using ngrok
description: Understand how to debug a channel using ngrok
keywords: debugging, channel, ngrok
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 08/18/2022
monikerRange: 'azure-bot-service-4.0'
---

# Debug a bot from any channel using ngrok

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

While your bot is in development, you can use an IDE and the Bot Framework Emulator to chat with your bot locally and inspect the messages your bot sends and receives.
If your bot is in production, you can debug your bot from any channel using _ngrok_. The seamless connection of your bot to multiple channels is a key feature available in the Bot Framework.

This article describes how to debug your bot locally using ngrok and a C# [EchoBot](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/02.echo-bot) in a channel connected to your bot. This article uses [Microsoft Teams](channel-connect-teams.md) as an example channel.

> [!NOTE]
> The Bot Framework Emulator and ngrok don't support user-assigned managed identity or single-tenant bots.

## Prerequisites

- A subscription to [Microsoft Azure](https://azure.microsoft.com/).
- Install [ngrok](https://ngrok.com/).
- A C# [Echo bot](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/02.echo-bot), configured as a multi-tenant app, and connected to any [channel](bot-service-manage-channels.md).

## Run ngrok

[ngrok](https://ngrok.com/docs) is a cross-platform application that can create a tunneling or forwarding URL, so that internet requests reach your local machine. Use ngrok to forward messages from external channels on the web directly to your local machine to allow debugging, as opposed to the standard messaging endpoint configured in the Azure portal.

1. Open a terminal and go to the folder with the ngrok executable.

1. Run ngrok with the following command to create a new tunnel.

    ```console
    ngrok http 3978 --host-header rewrite
    ```

    > [!NOTE]
    > Please note that the port specified is the port your bot is running on. You may use any localhost port you'd like.

1. When ngrok starts, copy and save the public forwarding URL for later.

    :::image type="content" source="media/debug-ngrok/ngrok-forwarding-url.png" alt-text="ngrok forwarding URL":::

## Configure in Azure portal

While ngrok is running, sign in to your Azure portal and view your bot settings to do some configuration.

1. Select your bot resource connected to your local bot.

1. Scroll down to **Configuration**. Copy and paste the ngrok forwarding URL in the **Messaging endpoint** field. Ensure that you maintain "/api/messages" at the end of the URL.

    :::image type="content" source="media/debug-ngrok/messaging-endpoint.png" alt-text="Messaging endpoint":::

1. Scroll up and select **Save**.

## Test

At this point, incoming messages from to your bot from external channels will now be sent to your local bot. The sample bot we'll use to demonstrate this is already configured live for **Microsoft Teams**. Read [Connect a bot to Microsoft Teams](channel-connect-teams.md) about connecting a local bot with **Microsoft Teams** channel.

:::image type="content" source="media/debug-ngrok/teams-channel.png" alt-text="Teams channel":::

Locally, you can set breakpoints in Visual Studio. Expanding the text property from the incoming activity object, you'll see that the message you sent the bot from teams is being intercepted locally for you to debug.

:::image type="content" source="media/debug-ngrok/breakpoint.png" alt-text="Set breakpoints":::

From here, you can debug normally, and run your code step by step. Use this to debug your bot from any channel.

## Additional information

- [Connect a bot to channels](bot-service-manage-channels.md)
- [Debug your bot locally using an IDE](bot-service-debug-bot.md)
- [Debug a bot using the Bot Framework Emulator](bot-service-debug-emulator.md)
- [Debug a bot with inspection middleware](bot-service-debug-inspection-middleware.md)
