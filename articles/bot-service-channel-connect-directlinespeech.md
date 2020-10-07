---
title: Connect a bot to Direct Line Speech
titleSuffix: Bot Service
description: Learn how to connect a bot to the Direct Line Speech channel for user's voice interaction with high reliability and low latency.
services: bot-service
author: trrwilson
manager: nitinme
ms.service: bot-service
ms.topic: conceptual
ms.date: 10/07/2020
ms.author: v-mimiel
---

# Connect a bot to Direct Line Speech

[!INCLUDE[applies-to-v4](includes/applies-to.md)]

This article describes how to connect a bot to the **Direct Line Speech channel**. Use this channel to allow client applications to interact with a bot via voice.

Once you have built your bot, onboarding it with Direct Line Speech will enable low latency, high reliability connection with client applications using the [Speech SDK](https://aka.ms/speech-service-docs). These connections are optimized for voice in, voice out conversational experiences. For more information on Direct Line Speech and how to build client applications, visit the [custom voice-first virtual assistant](https://aka.ms/cognitive-services-voice-assistants) page.

## Prerequisites

 The Direct Line Speech channel requires a **Cognitive Services** resource, specifically a **speech** cognitive service resource. You can either use an existing resource or create a new one. To create a new speech resource follow these steps:

1. In your browser, navigate to the [Azure portal create resources](https://ms.portal.azure.com/#create/hub).
1. In the left panel. click **Create a resource**.
1. In the right panel, in the search box, enter **speech**.
1. In the the drop-down list select **Speech**. The following is displayed:

    ![create speech cognitive resource](media/voice-first-virtual-assistants/create-speech-cognitive-resource.PNG "Create speech cognitive resource")

1. Click **Create** and follow the wizard steps.

    For additional information, see [Create a Cognitive Services resource](https://docs.microsoft.com/azure/cognitive-services/cognitive-services-apis-create-account).

## Add the Direct Line Speech channel

1. To add the Direct Line Speech Channel, first open the bot in the [Azure Portal](https://portal.azure.com), From your resources, select your **Bot Channel Registration** resource. Click on **Channels** in the configuration blade.

    ![highlight of the location for selecting channels to connect to](media/voice-first-virtual-assistants/bot-service-channel-directlinespeech-selectchannel.png "selecting channels")

1. In the channel selection page, find and click `Direct Line Speech` to choose the channel.

    ![selecting direct line speech channel](media/voice-first-virtual-assistants/bot-service-channel-directlinespeech-connectspeechchannel.png "connecting Direct Line Speech")

1. Configure the Direct Line Speech as shown in the picture below.

    ![configure direct line speech channel](media/voice-first-virtual-assistants/bot-service-channel-directlinespeech-cognitivesericesaccount-selection.png "selecting Cognitive Services resource")

1. Once you've reviewed the terms of use, click `Save` to confirm your channel selection. This will add the channel to the bot.

    ![saving the enabled of Direct Line Speech channel](media/voice-first-virtual-assistants/bot-service-channel-directlinespeech-added.png "direct line speech channel added")

## Enable the Bot Framework Protocol Streaming Extensions

With the Direct Line Speech channel connected to your bot, you now need to enable Bot Framework Protocol Streaming Extensions support for optimal, low-latency interaction.

1. In the left panel, select **Settings**.
1. In the right panel, check the box by the **Enable Streaming Endpoint**.

    ![enable the streaming protocol](media/voice-first-virtual-assistants/bot-service-channel-directlinespeech-enablestreamingsupport.png "enable streaming extension support")

1. At the top of the page, click **Save**.

1. Navigate to the bot app service.
1. In the left panel, in the **App Service Settings** category, select **Configuration**.

    ![navigate to app service settings](media/voice-first-virtual-assistants/bot-service-channel-directlinespeech-configureappservice.png "configure the app service")

1. In the right panel, select the `General settings` tab.
1. Set `Web sockets` to **On**.

    ![enable websockets for the app service](media/voice-first-virtual-assistants/bot-service-channel-directlinespeech-enablewebsockets.png "enable websockets")

1. Click `Save` at the top of the configuration page.

1. The Bot Framework Protocol Streaming Extensions are now enabled for your bot. You are now ready to update your bot code and [integrate Streaming Extensions support](https://aka.ms/botframework/addstreamingprotocolsupport) to an existing bot project.

## Adding protocol support to your bot

With the Direct Line Speech channel connected and support for the Bot Framework Protocol Streaming Extensions enabled, all that's left is to add code to your bot to support the optimized communication. Follow the instructions on [adding Streaming Extensions support to your bot](https://aka.ms/botframework/addstreamingprotocolsupport) to ensure full compatibility with Direct Line Speech.


