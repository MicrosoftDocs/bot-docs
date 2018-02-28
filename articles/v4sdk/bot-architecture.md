---
title: Bot architecture | Microsoft Docs
description: Understand bot architecture.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/21/2018
monikerRange: 'azure-bot-service-4.0'
---
# Bot architecture
The Microsoft Bot Builder SDK is one component of the [Azure Bot Service](bot-service-overview.md) and is a modular and extensible framework for developing and debugging bots. A bot is an application than can communicate conversationally with a user. A bot may be as simple as a basic pattern matching algorithm that returns a response, or it may include artificial intelligence, complex conversational state, and integration with existing business services.

The SDK defines a bot framework adapter object that can communicate with a user over various channels. The adapter also defines a middleware collection for adding reusable components to your application in a modular way.

The Bot Builder SDK is available in [C#](https://github.com/Microsoft/botbuilder-dotnet), [JavaScript](https://github.com/Microsoft/botbuilder-js), [Python](https://github.com/Microsoft/botbuilder-python), and [Java](https://github.com/Microsoft/botbuilder-java).

## The BotFrameworkAdapter

Your application should pass an incoming request as an activity, with associated authentication information, to the adapter. The adapter handles authentication, processes the activity, and sends any outbound activities through a connector object back to a channel.

You can connect your bot to your business logic using a receive callback method. Your bot can optionally send activities back to the caller. The adapter sends this information to a callback URL provided by the channel.

> ![NOTE] Your application and the adapter will handle requests asynchronously; however, your business logic does not need to be request-response driven.

A _conversation_ contains a series of _activities_ that represents an interaction between a bot and a user. The incoming request from the user is converted to an _activity_ that the bot can use. Your bot can send activities back to the user.

Your bot communicates with a user over a _channel_, the application the user interacts with when accessing your bot, such as text/sms, Skype, Slack, and others.

## Middleware
Use middleware to add reusable, application-independent components to your application in a modular way.

- The adapter processes activities through its middleware layers.
- Middleware can establish and persist state and react to incoming requests.
- Middleware can short circuit a pipeline, so that subsequent middleware is not called.
- You add middleware to the adapter at initialization time.
- The order in which you add middleware to the adapter determines the order in which the adapter calls the middleware.
- The SDK provides some predefined middleware, but you can define your own.

The SDK is designed around middleware that represents application-independent components, such as:

- State managers that persist and restore state information on the context object. The SDK includes conversation and user state managers.
- Intent recognizers that analyze user messages to extrapolate the user's intent. The SDK includes LUIS, QnA Maker, and RegEx recognizers.
- Translation middleware that can recognize the input language and translate to another language, such as one that your application understands.

## Pipelines

When the adapter receives an activity, it creates a context object and calls middleware via the following pipelines:

1. Context created – adds information to the context object and performs other pre-processing.
1. Receive activity – processes the request and can create responses.
1. Send activity – persists information from the context object and performs other post-processing.

Each middleware component in a pipeline is responsible for invoking the next component in the pipeline, or short-circuiting the chain if appropriate.

If the receive activity pipeline is not short circuited, the adapter calls your receive callback. If the middleware or your callback created responses to the activity, the adapter sends them after the post activity pipeline.
