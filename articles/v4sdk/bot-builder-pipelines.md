---
title: Middleware and pipelines within the Bot Builder SDK | Microsoft Docs
description: Describes the middleware and pipelines a bot uses to process activities within the Bot Builder SDK.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/22/2018
monikerRange: 'azure-bot-service-4.0'
---

# Middleware and pipelines

An incoming request from the user is converted to an activity that the bot can process.
In response, your bot can send activities back to the user.
The bot uses _middleware_ and _pipelines_ to divide its processing logic into a sequence of smaller tasks.

![NOTE] The bot processes incoming activities independently of each other.

## Middleware

Use middleware to add reusable components to your bot in a modular way.
Middleware is added to the pipelines via the bot adapter's _use_ method.
The order in which each pipeline calls the middleware is the order in which you added the middleware to the bot.

A pipeline calls a middleware object only if the middleware implements the appropriate interface or method to participate on the pipeline, as follows.

| Pipeline | C# interface | Node.js method |
|:---|:---|:---|
| Context created | `IContextCreated` | `contextCreated()` |
| Receive activity | `IReceiveActivity` | `receiveActivity()` |
| Send activity | `ISendActivity` | `postActivity()` |

See the language-specific reference documentation for more detailed information about each method and interface.

## Middleware sequencing

On each pipeline, each middleware object has the first-and-last chance to act with respect to the middleware objects that follow it in the pipeline.
When the adapter calls a middleware object, the parameters are a context object and an _next_ delegate.
(The next delegate is implemented as an asynchronous method in .NET and as a promise in Node.js.)
When you call the next delegate from your middleware, the adapter calls the next middleware on the current pipeline.
Once the subsequent middleware processing is complete, control returns to the middleware object that called the next delegate.

Your middleware can _short circuit_ the pipeline by not calling the next delegate.
In this case, the adapter calls none of the following middleware on the pipeline for the current activity.

## Pipelines

When the bot adapter receives a user request, it creates a context object, which  represents the request activity and the conversation state, and then runs the middleware pipelines in the following sequence.

1. Context created – adds information to the context object.
1. Receive activity – preprocesses the request.
1. Send activity – persists information from the context object and sends out any response activities.

If you provide an _on receive activity_ callback to the adapter, the adapter runs the callback if the receive activity pipeline completes without short circuiting before it starts the send activity pipeline.

![NOTE] If there are responses generated, the bot sends them to the adapter as the last step on the send activity pipeline.

## Proactive messages

To trigger your bot _proactively_, in response to application-internal logic, use the adapter's _continue conversation_ method, which takes an existing conversation reference object and a _callback_ method.
The bot creates a new context object and then runs the middleware pipelines in the following sequence. 

1. Context created
1. Send activity

The bot runs the proactive callback after the context created pipeline completes and before the send activity pipeline starts.

## See also

- Activities
- Bot architecture
- Bots and adapters
- Context
- Proactive messages
- State
