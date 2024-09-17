---
title: Debug a channel using a tunnel
description: Understand how to debug a channel using a tunnel
keywords: debugging, channel, tunnel
author: jameslew
ms.author: jameslew
manager: kjette
ms.reviewer: cyanderson
ms.topic: how-to
ms.service: azure-ai-bot-service
ms.date: 09/17/2024
monikerRange: 'azure-bot-service-4.0'
ms.custom:
  - evergreen
---

# Debug a bot from any channel using a tunnel

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

While your bot is in development, you can use an IDE and the Bot Framework Emulator to chat with your bot locally and inspect the messages your bot sends and receives.
If your bot is in production, you can debug your bot from any channel using a tunnel. The seamless connection of your bot to multiple channels is a key feature available in the Bot Framework.

This article describes how to debug your bot locally using a tunnel and a C# [EchoBot](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/02.echo-bot) in a channel connected to your bot. This article uses [Microsoft Teams](channel-connect-teams.md) as an example channel.


## Prerequisites

- A subscription to [Microsoft Azure](https://azure.microsoft.com/).
- Install a tunneling software such as [Dev Tunnels](https://aka.ms/devtunnels).
- A C# [Echo bot](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/02.echo-bot), configured as a multi-tenant app, and connected to any [channel](bot-service-manage-channels.md).

## Configure a tunnel

[Dev Tunnels](https://aka.ms/devtunnels) is a cross-platform application that can create a tunneling or forwarding URL, so that internet requests reach your local machine. Use devtunnel to forward messages from external channels on the web directly to your local machine to allow debugging, as opposed to the standard messaging endpoint configured in the Azure portal.

1. Open a terminal with access to `devtunnel` CLI.

1. Run devtunnel with the following command to create a new tunnel.

    ```console
    devtunnel host -a -p 3978
    ```

    > [!NOTE]
    > The port specified is the port your bot is running on. You may use any localhost port you'd like.


1. When devtunnel starts, copy and save the public forwarding URL for later.

   :::image type="content" source="media/debug-devtunnel/devtunnel-forwarding-url.png" alt-text="devtunnel forwarding URL":::

## Configure in Azure portal

While devtunnel is running, sign in to your Azure portal and view your bot settings to do some configuration.

1. Select your bot resource connected to your local bot.

1. Locate **Settings/Configuration**. Copy and paste the devtunnel forwarding URL in the **Messaging endpoint** field. Ensure that you maintain "/api/messages" at the end of the URL.

   :::image type="content" source="media/debug-devtunnel/messaging-endpoint.png" alt-text="Messaging endpoint":::

1. Select **Apply**.

## Test

At this point, incoming messages from to your bot from external channels will now be sent to your local bot. The sample bot we'll use to demonstrate this is already configured live for **Microsoft Teams**. Read [Connect a bot to Microsoft Teams](channel-connect-teams.md) about connecting a local bot with **Microsoft Teams** channel.

Locally, you can set breakpoints in Visual Studio. Expanding the text property from the incoming activity object, you'll see that the message you sent the bot from teams is being intercepted locally for you to debug.

:::image type="content" source="media/debug-devtunnel/breakpoint.png" alt-text="Set breakpoints":::

From here, you can debug normally, and run your code step by step. Use this to debug your bot from any channel.

## Additional information

- [Connect a bot to channels](bot-service-manage-channels.md)
- [Debug your bot locally using an IDE](bot-service-debug-bot.md)
- [Debug a bot using the Bot Framework Emulator](bot-service-debug-emulator.md)
- [Debug a bot with inspection middleware](bot-service-debug-inspection-middleware.md)
