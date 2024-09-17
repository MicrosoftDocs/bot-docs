---
title: Debugging guidelines - Bot Service
description: View bot debugging tips, such as using the Emulator and transcripts to inspect behavior. Understand potential middleware, state, and activity handler errors.
keywords: debugging bots, botframework debugging
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: azure-ai-bot-service
ms.date: 11/01/2021
monikerRange: 'azure-bot-service-4.0'
ms.custom:
  - evergreen
---

# Debugging guidelines

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Bots are complex apps, with many parts working together. Like any other complex app, this can lead to some interesting bugs or cause your bot to behave differently than expected.

Debugging, your bot can sometimes be a difficult task. Every developer has their own preferred way to accomplish that task.
The guidelines below are suggestions that apply to most bots.

After verifying that your bot works, the next step is connecting it to a channel. To do this, you can deploy your bot to a staging server, and create your own direct line client for your bot to connect to. For more information, see [Connect a bot to a Direct Line](../bot-service-channel-connect-directline.md).

Creating your own client allows you to define the inner workings of the channel, and test how your bot responds to certain activity exchanges. Once connected to your client, run your tests to set up your bot state and verify your features. If your bot utilizes a feature like speech, using these channels can offer a way to verify that functionality.

> [!NOTE]
> When deploying a bot to Azure, the [Web Chat](bot-builder-webchat-overview.md) channel is provisioned by default.

Use of both the [Bot Framework Emulator](../bot-service-debug-emulator.md) and [Web Chat](bot-builder-webchat-overview.md) via Azure portal here can provide further insight into how your bot performs while interacting with different channels.

Debugging your bot works similarly to other multi-threaded apps, with the ability to set breakpoints or use features like the immediate window.

Bots follow an event driven programming paradigm, which can be hard to rationalize if you're not familiar with it. The idea of your bot being stateless, multi-threaded, and dealing with async/await calls can result in unexpected bugs. While debugging your bot works similarly to other multi-threaded apps, we'll cover some suggestions, tools, and resources to help.

## Understanding bot activities with the Emulator

Your bot deals with different types of [activities](bot-builder-basics.md#the-activity-processing-stack) besides the normal _message_ activity. Understanding those activities will help you code your bot efficiently and allows you to verify the activities your bot is sending and receiving are what you expect.
Using the Emulator will show you what those activities are, when they happen, and what information they contain.
For more information, see [Debug with the Emulator](../bot-service-debug-emulator.md).

## Saving and retrieving user interactions with transcripts

Azure blob transcript storage provides a specialized resource where you can both [store and retrieve transcripts](bot-builder-howto-v4-storage.md) containing interactions between your users and your bot.

Additionally, once user input interactions have been stored, you can use Azure's "_storage explorer_" to manually view data contained in transcripts stored within your blob transcript store. The following example opens "_storage explorer_" from settings for "_mynewtestblobstorage_." To open a saved user input select:    Blob Container > ChannelId > TranscriptId > ConversationId

:::image type="content" source="./media/examine_transcript_text_in_azure.png" alt-text="Example of a transcript entry stored in a blob transcript store.":::

This opens the stored user conversation input in JSON format. User input is preserved together with the key "_text:_."
For more information on creating and using a bot transcript file, see [Debug your bot using transcript files](bot-builder-debug-transcript.md).

## How middleware works

[Middleware](bot-builder-concept-middleware.md) may not be intuitive when first attempting to use it, particularly regarding the continuation, or short-circuiting, of execution. Middleware can execute on the leading or trailing edge of a turn, with a call to the `next()` delegate dictating when execution is passed to the bot logic.

If you're using multiple pieces of middleware and it's how your pipeline is oriented, the delegate may pass execution to a different piece of middleware. Details on [the bot middleware pipeline](bot-builder-concept-middleware.md#the-bot-middleware-pipeline) can help make that idea clearer.

If the `next()` delegate isn't called, that's referred to as [short circuit routing](bot-builder-concept-middleware.md#short-circuiting). This happens when the middleware satisfies the current activity and determines it's not necessary to pass execution on.

Understanding when, and why, middleware short-circuits can help indicate which piece of middleware should come first in your pipeline. Additionally, understanding what to expect is important for built-in middleware provided by the SDK or other developers. Some find it helpful to try creating your own middleware first to experiment a bit before diving into the built-in middleware.

For more information on how to debug a bot using inspection middleware, see [Debug a bot with inspection middleware](../bot-service-debug-inspection-middleware.md).  

<!-- Snip: QnA was once implemented as middleware.
For example [QnA maker](bot-builder-howto-qna.md) is designed to handle certain interactions and short-circuit the pipeline when it does, which can be confusing when first learning how to use it.
-->

## Understanding state

Keeping track of state is an important part of your bot, particularly for complex tasks. In general, best practice is to process activities as quickly as possible and let the processing complete so that state gets persisted. Activities can be sent to your bot at nearly the same time, and that can introduce confusing bugs because of the asynchronous architecture.

Most importantly, make sure that state is persisting in a way that matches your expectations. Depending on where your persisted state lives, storage emulators for [Cosmos DB](/azure/cosmos-db/local-emulator) and [Azure Table storage](/azure/storage/common/storage-use-emulator) can help you verify that state before using production storage.

>[!IMPORTANT]
> The _Cosmos DB storage_ class has been deprecated. Containers originally created with CosmosDbStorage had no partition key set, and were given the default partition key of _\/_partitionKey_.
>
> Containers created with _Cosmos DB storage_ can be used with _Cosmos DB partitioned storage_. Read [Partitioning in Azure Cosmos DB](/azure/cosmos-db/partitioning-overview) for more information.
>
> Also note that, unlike the legacy Cosmos DB storage, the Cosmos DB partitioned storage doesn't automatically create a database within your Cosmos DB account. You need to [create a new database manually](/azure/cosmos-db/create-cosmosdb-resources-portal), but skip manually creating a container since _CosmosDbPartitionedStorage_ will create the container for you.

## How to use activity handlers

Activity handlers can introduce another layer of complexity, particularly since each activity runs on an independent thread (or web workers, depending on your language). Depending on what your handlers are doing, this can cause issues where the current state isn't what you expect.

Built-in state gets written at the end of a turn, however any activities generated by that turn are executing independently of the turn pipeline. Often this doesn't impact us, but if an activity handler changes state we need the state written to contain that change. In that case, the turn pipeline can wait on the activity to finish processing before completing to make sure it records the correct state for that turn.

The _send activity_ method, and its handlers, pose a unique problem. Simply calling _send activity_ from within the _on send activities_ handler causes an infinite forking of threads. There are ways you can work around that problem, such as by appending additional messages to the outgoing information or writing out to another location like the console or a file to avoid crashing your bot.

## Debugging a production bot

When your bot is in production, you can debug your bot from any channel using **Dev Tunnels**. The seamless connection of your bot to multiple channels is a key feature available in Bot Framework. For more information, see [Debug a bot from any channel using devtunnel](../bot-service-debug-channel-devtunnel.md) and [Debug a skill or skill consumer](skills-debug-skill-or-consumer.md).

## Next steps

> [!div class="nextstepaction"]
> [How to unit test bots](unit-test-bots.md)

## Additional resources

* [Debugging in Visual Studio](/visualstudio/debugger/index)
* [Debugging, Tracing, and Profiling](/dotnet/framework/debug-trace-profile/) for the bot framework
* Use the [ConditionalAttribute](/dotnet/api/system.diagnostics.conditionalattribute) for methods you don't want to include in production code
* Use tools like [Fiddler](https://www.telerik.com/fiddler) to see network traffic
* [Troubleshoot general problems](../bot-service-troubleshoot-bot-configuration.md) and the other troubleshooting articles in that section
