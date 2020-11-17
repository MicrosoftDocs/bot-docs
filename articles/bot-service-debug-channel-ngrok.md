---
title: Debug a channel using ngrok - Bot Service
description: Understand how to debug a channel using ngrok
keywords: debugging, channel, ngrok
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservices: sdk
ms.date: 11/17/2020
monikerRange: 'azure-bot-service-4.0'
---

# Debug a bot from any channel using ngrok

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

When your bot is in development, you can [debug your bot locally using an IDE](bot-service-debug-bot.md) such as Visual Studio or Visual Studio Code. You can also [debug a bot using the Bot Framework Emulator](bot-service-debug-emulator.md) by chatting with your bot locally and inspect the messages your bot sends and receives. You can even make slight updates of your bot's code and enable the feature to [debug a bot with inspection middleware](bot-service-debug-inspection-middleware.md).

When your bot is already in production, you can debug your bot from any [channel](bot-service-manage-channels.md) using **ngrok**. The seamless connection of your bot to multiple channels is a key feature available in Bot Framework. In this article, we will show you how to locally debug your bot from any channel your production bot is configured for, using **ngrok**. We used an [EchoBot](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/02.echo-bot) sample connected to [Microsoft Teams Channel](channel-connect-teams.md) throughout the article for instruction.

<!-- the Bot Framework Emulator uses an instance of the [Web Chat control](https://github.com/Microsoft/BotFramework-WebChat), which is only used in DirectLine, or embedded into web sites using a standard or custom configuration. Popular third party channels such as Slack, Facebook Messenger, Kik, etc. all implement their own chat channel user interfaces. In this article, we’ll discuss how you can locally debug your bot from any channel your production bot is configured for, using [ngrok](https://ngrok.com/docs). -->

## Prerequisites

* A subscription to [Microsoft Azure](https://azure.microsoft.com/).
* Install [ngrok](https://ngrok.com/).
* A [basic bot](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/02.echo-bot) connected to any [channel](bot-service-manage-channels.md).

## Run ngrok

[**ngrok**](https://ngrok.com/docs) is a cross-platform application that "allows you to expose a web server running on your local machine to the internet." Essentially, what we’ll be doing is using **ngrok** to forward messages from external channels on the web directly to our local machine to allow debugging, as opposed to the standard messaging endpoint configured in the Azure portal.

1. Open a terminal and navigate to the folder where your **ngrok** executable is.

2. Run **ngrok** with the following command to create a new tunnel.

    ```cmd
    ngrok http 3978 -host-header="localhost:3978"
    ```

    > [!NOTE]
    > Please note that the port specified is the port your bot is running on. You may use any localhost port you'd like.

3. When **ngrok** starts, copy and save the public forwarding URL for later.

    > [!div class="mx-imgBorder"]
    > ![ngrok forwarding url](./media/debug-ngrok/ngrok-forwarding-url.png)

## Configure in Azure portal

While **ngrok** is running, login to your Azure portal and view your bot settings to do some configuration.

1. Select your **Bot Channels Registration** connected to your local bot.

2. Scroll down to **Configuration**. Copy and paste the **ngrok** forwarding URL in the **Messaging endpoint** field. Ensure that you maintain "/api/messages" at the end of the URL.

    > [!div class="mx-imgBorder"]
    > ![messaging endpoint](./media/debug-ngrok/messaging-endpoint.png)

3. Scroll up and select **Save**.

## Test

At this point, incoming messages from to your bot from external channels will now be sent to your local bot. The sample bot we’ll use to demonstrate this is already configured live for **Microsoft Teams**. Read [Connect a bot to Microsoft Teams](channel-connect-teams.md) about connecting a local bot with **Microsoft Teams** channel.

> [!div class="mx-imgBorder"]
> ![teams channel](./media/debug-ngrok/teams-channel.png)

Locally, you can set breakpoints in Visual Studio. Expanding the text property from the incoming activity object, you will see that the message you sent the bot from teams is being intercepted locally for you to debug.

> [!div class="mx-imgBorder"]
> ![set breakpoints](./media/debug-ngrok/breakpoint.png)

From here you can debug normally, and run your code step by step. You can use this to debug your bot from any channel.

> [!div class="mx-imgBorder"]
> ![debug continue](./media/debug-ngrok/debug-continue.png)

## Additional information

* [Connect a bot to channels](bot-service-manage-channels.md)
