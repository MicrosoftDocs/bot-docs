---
title: Connect a bot to Direct Line Speech
description: Learn how to connect a bot to the Direct Line Speech channel for user's voice interaction with high reliability and low-latency.
services: bot-service
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: Daniel.Evans
ms.topic: conceptual
ms.service: azure-ai-bot-service
ms.date: 03/30/2022
ms.custom:
  - evergreen
---

# Connect a bot to Direct Line Speech

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to connect a bot to the Direct Line Speech channel. Use this channel to allow users to interact with a bot via voice.

Once you've built your bot, onboarding it with Direct Line Speech will enable low-latency, high reliability connection with client applications using the [Speech SDK](/azure/ai-services/speech-service/speech-sdk). These connections are optimized for voice in and voice out conversational experiences. For more information on Direct Line Speech and how to build client applications, visit the [custom voice-first virtual assistant](/azure/ai-services/speech-service/voice-assistants) page.

## Prerequisites

- An Azure account. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- An existing bot published to Azure.
- An [Azure AI Speech](/azure/ai-services/speech-service) resource. You can either [create a new Speech resource](https://portal.azure.com/#create/Microsoft.CognitiveServicesSpeechServices) in Azure or use an existing one.

## Add the Direct Line Speech channel

1. In the [Azure portal](https://portal.azure.com), select the **Azure Bot** resource.
1. Under **Settings**, select the **Channels** pane. Then select **Direct Line Speech**.
1. Add your Speech resource to the Direct Line Speech channel by entering the values on the page. Select the links under each field for more information.
1. Select **Apply** to confirm your channel selection. This adds the Direct Line Speech channel to your bot.

## Enable the Bot Framework Protocol Streaming Extensions

With the Direct Line Speech channel connected to your bot, you now need to enable Bot Framework Protocol Streaming Extensions support for optimal, low-latency interaction.

1. Under **Settings** select **Configuration**.
1. Select **Enable Streaming Endpoint**. Then select **Apply**.
1. Now go to the bot's app service.
1. In the App Service instance, under the **Settings** category, select **Configuration**.
1. Select the **General settings** tab. Then set **Web sockets** to **On**.
1. Select **Save** at the top of the configuration page.

The Bot Framework Protocol Streaming Extensions are now enabled for your bot. You're now ready to update your bot code and [integrate Streaming Extensions support](directline-speech-bot.md) to an existing bot project.

## Example

If you followed all the steps, you can now talk to the bot using the client application downloadable at [Windows Voice Assistant Client](https://github.com/Azure-Samples/Cognitive-Services-Voice-Assistant/blob/master/clients/csharp-wpf/README.md#windows-voice-assistant-client). For more information see [Voice-enable your bot using the Speech SDK](/azure/ai-services/speech-service/tutorial-voice-enable-your-bot-speech-sdk).

## Adding protocol support to your bot

> [!NOTE]
> The following step is only needed for bots built before the the v4.8 SDK release.

With the Direct Line Speech channel connected and support for the Bot Framework Protocol Streaming Extensions enabled, all that's left is to add code to your bot to support the optimized communication. Follow the instructions on [adding Streaming Extensions support to your bot](directline-speech-bot.md) to ensure full compatibility with Direct Line Speech.
