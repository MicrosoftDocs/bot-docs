---
title: Bot architecture | Microsoft Docs
description: Understand bot architecture.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/14/2018
monikerRange: 'azure-bot-service-4.0'
---
## Bot architecture
The Microsoft Bot Builder SDK is one component of the [Azure Bot Service](bot-service-overview.md), which allows you to create a _bot_, an application that users can interact with in a conversational way. Bots can communicate conversationally with text, cards, or speech. A bot may be as simple as basic pattern matching with a response, or it may be a sophisticated weaving of artificial intelligence techniques with complex conversational state tracking and integration to existing business services. 

The following diagram illustrates elements of a bot application, and the following sections describe these in more detail.

<!-- ![Overview of bot architecture](./media/concept-bot-architecture/bot-overview.png) -->

<img src="./media/concept-bot-architecture/bot-overview.png" width="400" height="400">

The SDK defines a _bot object_, which provides a way to add your application logic. The bot uses an _adapter_ to facilitate conversation between the bot and users and handles _authentication_. The bot uses _middleware_ and _pipelines_ to divide your application logic into a sequence of smaller tasks.

A _conversation_ contains a series of _activities_ that represents an interaction between a bot and a user. The incoming request from the user is converted to an _activity_ that the bot can use. Your bot can send activities back to the user.

Your bot communicates with a user over a _channel_, the application the user interacts with when accessing your bot, such as text/sms, Skype, Slack, and others.

#### Middleware and pipelines
You use middleware to add reusable, components to your application in a modular way. The bot processes activities through its middleware layers. The middleware can establish and persist context and react to incoming requests.
 
You add middleware to the bot object at initialization time, and the order in which you add middleware to the bot determines the order in which the bot calls the middleware. The SDK provides some predefined middleware, but you can define your own.

The bot is stateless. However, when the bot receives an activity, it generates a _context_ object that captures various parameters of the conversation. Each received activity is processed independently of other incoming activities. Your bot can use a state manager to persist state between activities.

The bot object invokes middleware via the following pipelines:
1. Context created – establishes and enriches a context object that represents the request and conversation state.
1. Receive activity – operates on the request.
1. Send activity – persists context and updates conversation state.

> ![NOTE] You use the _on receive_ method of the _bot object_ to add application logic between the receive and send activity pipeline. 
The bot handles requests asynchronously; however, your underlying application logic does not need to be request-response driven.

## Bot Builder SDK
The Bot Builder SDK is available in [C#](https://github.com/Microsoft/botbuilder-dotnet), [JavaScript](https://github.com/Microsoft/botbuilder-js), [Python](https://github.com/Microsoft/botbuilder-python), and [Java](https://github.com/Microsoft/botbuilder-java).
