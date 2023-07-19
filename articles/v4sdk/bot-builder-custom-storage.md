---
title: Implement custom storage for your bot
description: Learn how to use version 4.0 of the Bot Framework SDK to store bot state data. Understand the default framework. See how to expand support.
keywords: custom, storage, state, dialog
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: bot-service
ms.date: 10/26/2022
monikerRange: 'azure-bot-service-4.0'
---

# Implement custom storage for your bot

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

A bot's interactions fall into three areas: the exchange of activities with Azure AI Bot Service, the loading and saving of bot and dialog state with a memory store, and integration with back-end services.

:::image type="content" source="../media/scale-out/scale-out-interaction.png" alt-text="Interaction diagram outlining relationship between the Azure AI Bot Service, a bot, a memory store, and other services.":::

This article explores how to extend the semantics between the Azure AI Bot Service and the bot's memory state and storage.

[!INCLUDE [java-python-sunset-alert](../includes/java-python-sunset-alert.md)]

## Prerequisites

- Knowledge of [Basics of the Microsoft Bot Framework](bot-builder-basics.md), [Event-driven conversations using an activity handler](bot-activity-handler-concept.md), and [Managing state](bot-builder-concept-state.md).
- A copy of the scale-out sample in [C#](https://github.com/Microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/42.scaleout), [Python](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/python/42.scaleout), or [Java](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/42.scaleout).

This article focuses on the C# version of the sample.

## Background

The Bot Framework SDK includes a default implementation of bot state and memory storage.
This implementation fits the needs of applications where the pieces are used together with a few lines of initialization code, as demonstrated in many of the samples.

The SDK is a framework and not an application with fixed behavior.
In other words, the implementation of many of the mechanisms in the framework is a default implementation and not the only possible implementation.
The framework doesn't dictate the relationship between the exchange of activities with Azure AI Bot Service and the loading and saving of any bot state.

This article describes one way to modify the semantics of the default state and storage implementation when it doesn't quite work for your application.
The scale-out sample provides an alternate implementation of state and storage that has different semantics than the default ones.
This alternate solution sits equally well in the framework.
Depending on your scenario, this alternate solution may be more appropriate for the application you're developing.

## Behavior of the default adapter and storage provider

With the default implementation, on receiving an activity, the bot loads the state corresponding to the conversation.
It then runs the dialog logic with this state and the inbound activity.
In the process of running the dialog, one or more outbound activities are created and immediately sent.
When the processing of the dialog is complete, the bot saves the updated state, overwriting the old state.

:::image type="content" source="../media/scale-out/scale-out-default.png" alt-text="Sequence diagram showing the default behavior of a bot and its memory store.":::

However, a few things can go wrong with this behavior.

- If the save operation fails for some reason, then state has implicitly slipped out of sync with what the user sees on the channel.
  The user has seen responses from the bot and believes that the state has moved forward, but it hasn't.
  This error can be worse than if the state update succeeded but the user didn't receive the response messages.

  Such state errors can have implications for your conversation design.
  For example, the dialog might require extra, otherwise redundant, confirmation exchanges with the user.

- If the implementation is deployed _scaled out_ across multiple nodes, the state can accidentally get overwritten.
  This error can be confusing because the dialog will likely have sent activities to the channel carrying confirmation messages.

  Consider a pizza order bot, where the bot asks user for topping choices, and the user sends two rapid messages: one to add mushrooms and one to add cheese.
  In a scaled-out scenario, multiple instances of the bot might be active, and the two user messages could be handled by two separate instances on separate machines.
  Such a conflict is referred to as a _race condition_, where one machine might overwrite the state written by another.
  However, because the responses were already sent, the user received confirmation that both mushroom and cheese were added to their order.
  Unfortunately, when the pizza arrives, it only contains mushroom or cheese, but not both.

## Optimistic locking

The scale-out sample introduces some locking around the state.
The sample implements _optimistic locking_, which lets each instance run as if it were the only one running and then check for any concurrency violations.
This locking may sound complicated, but known solutions exist, and you can use cloud storage technologies and the right extension points in the Bot Framework.

The sample uses a standard HTTP mechanism based on the entity tag header (ETag).
Understanding this mechanism is crucial to understanding the code that follows. The following diagram illustrates the sequence.

:::image type="content" source="../media/scale-out/scale-out-precondition-failed.png" alt-text="Sequence diagram showing a race condition, with the second update failing.":::

The diagram has two clients that are performing an update to some resource.

1. When a client issues a GET request and a resource is returned from the server, the server includes an ETag header.

   The ETag header is an opaque value that represents the state of the resource.
   If a resource is changed, the server updates its ETag for the resource.

1. When the client wants to persist a state change, it issues a POST request to the server, with the ETag value in an `If-Match` precondition header.
1. If the request's ETag value doesn't match the server's, then the precondition check fails with a `412` (Precondition Failed) response.

   This failure indicates that the current value on server no longer matches the original value the client was operating on.

1. If the client receives a precondition failed response, the client typically gets a fresh value for the resource, applies the update it wanted, and attempts to post the resource update again.

   This second POST request succeeds if no other client has updated the resource. Otherwise, the client can try again.

This process is called _optimistic_ because the client, once it has a resource, proceeds to do its processing&mdash;the resource itself isn't _locked_, as other clients can access it without any restriction.
Any contention between clients over what the state of the resource should be isn't determined until the processing has been done.
In a distributed system, this strategy is often more optimal than the opposite _pessimistic_ approach.

The optimistic locking mechanism as described assumes that your program logic can be safely retried.
The ideal situation is where these service requests are _idempotent_.
In computer science, an idempotent operation is one that has no extra effect if it's called more than once with the same input parameters.
Pure HTTP REST services that implement the GET, PUT, and DELETE requests are often idempotent.
If a service request won't produce extra effects, then requests can be safely re-executed as part of a retry strategy.

The scale-out sample and the remainder of this article assume that the backend services your bot uses are all idempotent HTTP REST services.

## Buffering outbound activities

Sending an activity isn't an idempotent operation.
The activity is often a message that relays information to the user, and repeating the same message two or more times might be confusing or misleading.

Optimistic locking implies that your bot logic may need to be rerun multiple times.
To avoid sending any given activity multiple times, wait for the state update operation to succeed before sending activities to the user.
Your bot logic should look something like the following diagram.

:::image type="content" source="../media/scale-out/scale-out-buffer.png" alt-text="Sequence diagram with messages being sent after dialog state is saved.":::

Once you build a retry loop into your dialog execution, you have the following behavior when there's a precondition failure on the save operation.

:::image type="content" source="../media/scale-out/scale-out-save.png" alt-text="Sequence diagram with messages being sent after a retry attempt succeeds.":::

With this mechanism in place, the pizza bot from the earlier example should never send an erroneous positive acknowledgment of a pizza topping being added to an order.
Even with the bot deployed across multiple machines, the optimistic locking scheme effectively serializes the state updates.
In the pizza bot, the acknowledgment from adding an item can now even reflect the full state accurately.
For example, if the user quickly types "cheese" and then "mushroom", and these messages are handled by two different instances of the bot, the last instance to complete can include "a pizza with cheese and mushroom" as part of its response.

This new custom storage solution does three things that the default implementation in the SDK doesn't do:

1. It uses ETags to detect contention.
1. It retries the processing when an ETag failure is detected.
1. It waits to send outbound activities until it has successfully saved state.

The remainder of this article describes the implementation of these three parts.

## Implement ETag support

First, define an interface for our new store that includes ETag support.
The interface helps use the dependency injection mechanisms in ASP.NET.
Starting with the interface allows you to implement separate versions for unit tests and for production.
For example, the unit test version might run in memory and not require a network connection.

The interface consists of _load_ and _save_ methods.
Both methods will use a _key_ parameter to identify the state to load from or save to storage.

- _Load_ will return the state value and associated ETag.
- _Save_ will have parameters for the state value and associated ETag and return a Boolean value that indicates whether the operation succeeded.
  The return value won't serve as a general error indicator, but instead as a specific indicator of precondition failure.
  Checking the return code will part of the logic of the retry loop.

To make the storage implementation widely applicable, avoid placing serialization requirements on it.
However, many modern storage services support JSON as the content-type.
In C#, you can use the `JObject` type to represent a JSON object.
In JavaScript or TypeScript, JSON is a regular native object.

Here's a definition of the custom interface.

**IStore.cs**

[!code-csharp[IStore](~/../botbuilder-samples/samples/csharp_dotnetcore/42.scaleout/IStore.cs?range=14-19)]

Here's an implementation for Azure Blob Storage.

**BlobStore.cs**

[!code-csharp[BlobStore](~/../botbuilder-samples/samples/csharp_dotnetcore/42.scaleout/BlobStore.cs?range=18-101)]

Azure Blob Storage does much of the work. Each method checks for a specific exception to meet the expectations of the calling code.

- The `LoadAsync` method, in response to a storage exception with a _not found_ status code, returns a null value.
- The `SaveAsync` method, in response to a storage exception with a _precondition failed_ code, returns `false`.

## Implement a retry loop

The design of the retry loop implements the behavior shown in the sequence diagrams.

1. On receiving an activity, create a key for the conversation state.

    The relationship between an activity and the conversation state is the same for the custom storage as for the default implementation.
    Therefore, you can construct the key the same way that the default state implementation does.

1. Attempt to load the conversation state.
1. Run the bot's dialogs and capture the outbound activities to send.
1. Attempt to save the conversation state.
    - On success, send the outbound activities and exit.
    - On failure, repeat this process from the step to load the conversation state.

      The new load of conversation state gets a new and current ETag and conversation state. The dialog is rerun, and the save state step has a chance to succeed.

Here's an implementation for the message activity handler.

**ScaleoutBot.cs**

[!code-csharp[OnMessageActivity](~/../botbuilder-samples/samples/csharp_dotnetcore/42.scaleout/Bots/ScaleOutBot.cs?range=43-72)]

> [!NOTE]
> The sample implements dialog execution as a function call.
> A more sophisticated approach might be to define an interface and use dependency injection.
> For this example, however, the static function emphasizes the functional nature of this optimistic locking approach.
> In general, when you implement the crucial parts of your code in a functional way, you improve its chances to work successfully on networks.

## Implement an outbound activity buffer

The next requirement is to buffer outbound activities until after a successful save operation happens, which requires a custom adapter implementation.
The custom `SendActivitiesAsync` method shouldn't send the activities to the use, but add the activities to a list.
Your dialog code won't need modification.

- In this particular scenario, the _update activity_ and _delete activity_ operations aren't supported and the associated methods will throw _not implemented_ exceptions.
- The return value from the send activities operation is used by some channels to allow a bot to modify or delete a previously sent message, for example, to disable buttons on cards displayed in the channel. These message exchanges can get complicated, particularly when state is required, and are outside the scope of this article.
- Your dialog creates and uses this custom adapter, so it can buffer activities.
- Your bot's turn handler will use a more standard `AdapterWithErrorHandler` to send the activities to the user.

Here's an implementation of the custom adapter.

**DialogHostAdapter.cs**

[!code-csharp[DialogHostAdapter](~/../botbuilder-samples/samples/csharp_dotnetcore/42.scaleout/DialogHostAdapter.cs?range=19-46)]

## Use your custom storage in a bot

The last step is to use these custom classes and methods with existing framework classes and methods.

- The main retry loop becomes part of your bot's `ActivityHandler.OnMessageActivityAsync` method and includes your custom storage through dependency injection.
- The dialog hosting code is added to `DialogHost` class that exposes a static `RunAsync` method. The dialog host:
  - Takes the inbound activity and the old state and then returns the resulting activities and new state.
  - Creates the custom adapter and otherwise runs the dialog in the same way as the SDK does.
  - Creates a custom state property accessor, a shim that passes the dialog state into the dialog system.
    The accessor uses reference semantics to pass an accessor handle to the dialog system.

> [!TIP]
> The JSON serialization is added inline to the hosting code to keep it outside of the pluggable storage layer, so that different implementations can serialize differently.

Here's an implementation of the dialog host.

**DialogHost.cs**

[!code-csharp[DialogHost](~/../botbuilder-samples/samples/csharp_dotnetcore/42.scaleout/DialogHost.cs?range=22-72)]

And finally, here's an implementation of the custom state property accessor.

**RefAccessor.cs**

[!code-csharp[RefAccessor](~/../botbuilder-samples/samples/csharp_dotnetcore/42.scaleout/RefAccessor.cs?range=22-60)]

## Additional information

The scale-out sample is available from the Bot Framework samples repo on GitHub in [C#](https://github.com/Microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/42.scaleout), [Python](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/python/42.scaleout), and [Java](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/42.scaleout).
