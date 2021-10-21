---
title: Connect a bot to Direct Line Speech
description: Learn how to connect a bot to the Direct Line Speech channel for user's voice interaction with high reliability and low latency.
services: bot-service
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.service: bot-service
ms.topic: conceptual
ms.date: 10/18/2021
---

# Connect a bot to Direct Line Speech

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to connect a bot to the Direct Line Speech channel. Use this channel to allow users to interact with a bot via voice.

Once you've built your bot, onboarding it with Direct Line Speech will enable low latency, high reliability connection with client applications using the [Speech SDK](/azure/cognitive-services/speech-service/). These connections are optimized for voice in and voice out conversational experiences. For more information on Direct Line Speech and how to build client applications, visit the [custom voice-first virtual assistant](/azure/cognitive-services/Speech-Service/voice-assistants) page.

## Prerequisites

 The Direct Line Speech channel requires a **Cognitive Services** resource, specifically a **Speech** cognitive service resource. You can either use an existing resource or create a new one. To create a new speech resource:

1. Go to the [Azure portal](https://ms.portal.azure.com/#create/hub) to create resources.
1. In the right pane, enter "speech" in the search box.
1. Select **Speech** from the list. It will open a page describing this resource type.

    ![create speech cognitive resource](media/voice-first-virtual-assistants/create-speech-cognitive-resource.PNG "create speech cognitive service")

1. Select **Create** and follow the wizard steps.

    For more information, see [Create a Cognitive Services resource](/azure/cognitive-services/cognitive-services-apis-create-account).

## Add the Direct Line Speech channel

1. In your browser, navigate to the [Azure portal](https://portal.azure.com).
1. From your resources, select the **Azure Bot** resource.
1. Select the **Channels** pane.

    ![selecting channels](media/voice-first-virtual-assistants/bot-channels-li.png "selecting channels")

1. In right panel, select **Direct Line Speech**.

    ![selecting direct line speech channel](media/voice-first-virtual-assistants/bot-service-channel-directlinespeech-connectspeechchannel.png "connecting Direct Line Speech")

1. Add your Speech Cognitive Service to the Direct Line Speech channel. See [Prerequisites](#prerequisites) for more about the Speech resource.

1. Once you've reviewed the terms of use, select **Save** to confirm your channel selection. This adds the channel to your bot.

    ![saving the enabled of Direct Line Speech channel](media/voice-first-virtual-assistants/bot-service-channel-directlinespeech-added.png "added direct line speech channel")

## Enable the Bot Framework Protocol Streaming Extensions

With the Direct Line Speech channel connected to your bot, you now need to enable Bot Framework Protocol Streaming Extensions support for optimal, low-latency interaction.

1. In the left pane, select **Settings**.
1. In the right pane, select **Enable Streaming Endpoint**.

    ![enable the streaming protocol](media/voice-first-virtual-assistants/streaming-endpoint.png "enable streaming extension support")

1. At the bottom of the page, select **Apply**.

1. Navigate to the bot app service.
1. In the App Service instance, under the **Settings** category, select **Configuration**.

    ![navigate to app service settings](media/voice-first-virtual-assistants/bot-service-channel-directlinespeech-configureappservice.png "configure the app service")

1. In the right pane, select the **General settings** tab.
1. Set **Web sockets** to **On**.

    ![enable websockets for the app service](media/voice-first-virtual-assistants/bot-service-channel-directlinespeech-enablewebsockets.png "enable websockets")

1. Select `Save` at the top of the configuration page.

The Bot Framework Protocol Streaming Extensions are now enabled for your bot. You're now ready to update your bot code and [integrate Streaming Extensions support](directline-speech-bot.md) to an existing bot project.

## Example

If you followed all the steps described, you can now talk to the bot using the client application downloadable at this location: [Windows Voice Assistant Client](https://github.com/Azure-Samples/Cognitive-Services-Voice-Assistant/blob/master/clients/csharp-wpf/README.md#windows-voice-assistant-client).

The following picture shows the client application interface when communicating with a simple echo bot. See also [Voice-enable your bot using the Speech SDK](/azure/cognitive-services/speech-service/tutorial-voice-enable-your-bot-speech-sdk).

![voice assistant client](media/voice-first-virtual-assistants/voice-assistant-client.png "voice assistant client")

## Adding protocol support to your bot

> [!NOTE]
> The following step is only needed for bots built before the the v4.8 SDK release.

With the Direct Line Speech channel connected and support for the Bot Framework Protocol Streaming Extensions enabled, all that's left is to add code to your bot to support the optimized communication. Follow the instructions on [adding Streaming Extensions support to your bot](directline-speech-bot.md) to ensure full compatibility with Direct Line Speech.
