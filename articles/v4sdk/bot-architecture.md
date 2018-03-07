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
## Architecture
The Bot Builder SDK v4 is a modular and extensible framework for developing bots. It provides libraries, samples, and tools to help you build and debug bots. With Bot Builder SDK v4, you can create bots in C#, JavaScript, Java, and Python.  
A bot is an application than can communicate conversationally with a user. A bot may be as simple as a basic pattern matching algorithm that returns a response, or it may include artificial intelligence, complex conversational state, and integration with existing business services.

The SDK defines a _bot adapter_ object that can communicate with a user over various channels, and defines a _middleware_ collection that allows you to add reusable components to your application.

### The bot adapter
The adapter handles authentication, processes the activity, and sends any outbound activities through a connector back to the channel. Your application should pass an incoming request as an activity, with associated authentication information, to the adapter. To provide application-specific logic to process incoming activities, you implement a _receive callback_ method, and pass it to the adapter's _process activity_ method.

Note: Your application and the adapter will handle requests asynchronously; however, your business logic does not need to be request-response driven.

### Middleware
Middleware provides the ability to add reusable components to your application.
- You add middleware to the adapter at initialization time.
- The adapter processes activities through its middleware layers.
- Middleware can establish and persist state and react to incoming requests.
- Middleware can short circuit a pipeline, so that subsequent middleware is not called.
- The order in which you add middleware to the adapter determines the order in which the adapter calls the middleware.
- The SDK provides some predefined middleware, but you can define your own.

The SDK is designed around middleware that represents application-independent components, such as:
- State middleware that persist and restore state information on the context object. The SDK includes conversation and user state middleware.
- Intent recognizers that analyze user messages to extrapolate the user's _intent_. The SDK includes LUIS, QnA Maker, and RegEx recognizers.
- Translation middleware that can recognize the input language and translate to another language, such as one that your application understands.

### Pipelines
When the adapter receives an activity, it creates a context object and calls middleware via the following pipelines:
1.	Context created – adds information to the context object and performs other pre-processing.
2.	Receive activity – processes the request and can create responses.
3.	Post activity – persists information from the context object and performs other post-processing.

Each middleware component in a pipeline is responsible for invoking the next component in the pipeline, or short-circuiting the chain if appropriate.

If the receive activity pipeline is not short circuited, the adapter calls your receive callback.
If the middleware or your callback created responses to the activity, the adapter sends them after the post activity pipeline.
