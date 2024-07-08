---
title: Debug your bot using transcript files
description: Learn how to use transcript files to debug bots. See how to create and retrieve these files, which provide detailed sets of user interactions and bot responses.
keywords: debugging, faq, transcript file, emulator
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: bot-service
ms.date: 08/10/2022
monikerRange: 'azure-bot-service-4.0'
ms.custom:
  - evergreen
---

# Debug your bot using transcript files

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

One of the keys to successful testing and debugging a bot is your ability to record and examine the set of conditions that occur when running your bot. This article discusses the creation and use of a bot transcript file to provide a detailed set of user interactions and bot responses for testing and debugging.

## The bot transcript file

A bot transcript file is a specialized JSON file that preserves the interactions between a user and your bot. A transcript file preserves not only the contents of a message, but also interaction details such as the user ID, channel ID, channel type, channel capabilities, time of the interaction, and so on. All of this information can then be used to help find and resolve issues when testing or debugging your bot.

## Creating/Storing a bot transcript file

This article shows how to create bot transcript files using the [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator). Transcript files may also be created programmatically; see [Blob transcript storage](./bot-builder-howto-v4-storage.md#blob-transcript-storage) to read more concerning that approach. In this article, we'll use the Bot Framework sample code for [Multi Turn Prompt Bot](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/05.multi-turn-prompt) that requests a user's mode of transportation, name and age, but any code that can be accessed using Microsoft's Bot Framework Emulator may be used to create a transcript file.

To begin this process, ensure that the bot code you want to test is running within your development environment.

1. Start the Emulator.
1. On the **Welcome** tab, select **Open Bot**.
1. Enter the address of the port to which your bot is listening, followed by `/api/messages`, for instance, `http://localhost:3978/api/messages`.

    If your bot is configured with a Microsoft app ID and password, enter the ID and password in the **Open a bot** dialog. Otherwise, the Emulator won't be able to connect to your bot.

1. Select **Connect** to connect the Emulator to your bot.

    :::image type="content" source="./media/emulator_open_bot_configuration.png" alt-text="Screenshot of dialog for connecting to a bot from the Emulator.":::

Test your code by interacting with your bot in the Emulator. After you've entered all of the user interactions you want to preserve, use the Bot Framework Emulator to create and save a transcript file containing this conversation.

1. In the **Live Chat** tab, select **Save transcript**.

    :::image type="content" source="./media/emulator_transcript_save.png" alt-text="Screenshot of a conversation and the 'save transcript' button in the Emulator.":::

1. Choose a location and name for your transcript file and select **Save**.

    :::image type="content" source="./media/emulator_transcript_saveas_ursula.png" alt-text="Screenshot of the 'save conversation transcript' dialog.":::

All of the user interactions and bot responses that you entered to test your code with the Emulator have now been saved into a transcript file that you can later reload to help debug interactions between your user and your bot.

## Retrieving a bot transcript file

When you open a transcript file, the Emulator loads the saved conversation into a new tab.

To retrieve a bot transcript file:

1. Open the Emulator.
1. From the menu, select **File** then **Open Transcript**.
1. Use the **Open transcript file** to select and open the transcript file you want to retrieve.

:::image type="content" source="./media/emulator_transcript_retrieve.png" alt-text="Screenshot of the 'open transcript file' dialog.":::

## Debug using transcript file

With your transcript file loaded, you're now ready to debug interactions that you captured between a user and your bot.

1. Select any user or bot message, or activity recorded in the Emulator's _log_ pane.
1. The Emulator will display the activity information in the _inspector_ pane. The activity information is the payload of the HTTP request for the activity.

    A message activity includes:

   - The activity type
   - The time the activity was sent from or received by the channel
   - Information about the user's channel
   - Information about the sender and receiver of the activity, in the `from` and `recipient` fields, respectively
   - Information specific to the type of activity, such as the message text for a message activity.

This detailed level of information allows you to follow the step-by-step interactions between the user's input and your bot's response, which is useful for debugging situations where your bot either didn't respond in the manner that you anticipated or didn't respond to the user at all. Having both these values and a record of the steps leading up to the failed interaction allows you to step through your code, find the location where your bot doesn't respond as anticipated, and resolve those issues.

Using transcript files together with the Bot Framework Emulator is just one of the many tools you can use to help you test and debug your bot's code and user interactions.

## Additional information

For more testing and debugging information, see:

- [Bot testing and debugging guidelines](./bot-builder-testing-debugging.md)
- [Debug with the Bot Framework Emulator](../bot-service-debug-emulator.md)
- [Troubleshoot general problems](../bot-service-troubleshoot-bot-configuration.md) and the other troubleshooting articles in that section.
- [Debugging in Visual Studio](/visualstudio/debugger/index)
