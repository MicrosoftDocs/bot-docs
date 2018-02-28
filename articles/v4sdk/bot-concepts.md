---
title: Bot Builder SDK concepts | Microsoft Docs
description: Bot Builder SDK key concepts.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/14/2018
monikerRange: 'azure-bot-service-4.0'

---

# Key concepts in the Bot Builder SDK
This article introduces key concepts in the Bot Builder SDK, one component of the [Azure Bot Service](bot-service-overview.md). For a description of the basic architecture of a bot, see [Bot architecture](bot-architecture.md). 

<!--Note: Organized as an [minimally] extended glossary, thus alphabetized. ?Try to keep each section to one to three paragraphs of "reasonable" length.-->

## Activity
The bot schema defines a JSON `Activity` object to exchange information between the bot and the user. The Bot Builder SDK wraps this information in a language-specific _activity_ object. The most common type of activity is a message, but there are other activity types that indicate when a participant has been added or removed from a conversation, when the bot has been added or removed from a user's contact list, and so on. When your bot receives an activity, it uses the adapter to process the activity.
<!--TODO: Link to Activities overview.-->

## Adapter
All bot applications use an adapter, which is a core component of the [bot architecture](bot-architecture.md). The _bot adapter_ object uses the authentication and connector functionality in the SDK to send activites to the Bot Connector Service and receive activities from it. When your bot receives an activity, it calls the adapter's _process activity_ method. This method creates a context object that includes the activity in the context's request property. Your bot implements a _receive callback_ method that provides logic to process incoming activities. This callback method is passed to the adapter's _process activity_ method. 

## Middleware
You can add _middleware_ as reusable components to your bot by adding it to the adapter at initialization. Middleware allows you to do additional processing on activities either before or after the _recieve callback_ method. The middleware can participate in the pipeline by implementing _context created, receive activity, send activity_ methods. The adapter adds to the context object's responses property any outgoing activities from the bot. The adapter sends these activities back to the user.  

## Connector
A _connector_ object provides access to the Bot Connector Service and enables a bot to communicate with users across specfic channels, such as Skype, Email, Slack, and more.

The Bot Connector Service normalizes messages that the bot sends to a channel, and you can develop your bot in a channel-agnostic way. Normalizing a message involves converting it from the bot schema into the channel’s schema. In cases where the channel does not support all aspects of the framework’s schema, the connector will try to convert the message to a format that the channel supports. For example, if the bot sends a message that contains a card with action buttons to the SMS channel, the connector may render the card as an image and include the actions as links in the message’s text.

The [Channel Inspector](https://docs.botframework.com/en-us/channel-inspector/channels/Skype/) is a web tool that shows you how the connector renders messages on various channels.

## Context
A _context object_ captures information about the incoming activity, any outgoing activities, the sender and receiver, the channel, the conversation, state, and other data needed to process the activity. When an adapter receives an activity, it generates a context object.  

The adapter passes the context object to its middleware and to the application's receive callback method. Any middleware you use or design will implement a method that takes a context object. Similarly, any receive callback method you define for your application will also take a context object. 
<!-- Add specific method name that middleware must implement -->

You use the context object to retrieve information about the activity, the user, the conversation, and so on. Each received activity is processed independently of other incoming activities. Use one or more _state managers_ to persist state between activities.   
<!-- link to state management topic -->

## Conversation object
A conversation identifies a series of activities sent between a bot and a user and represents an interaction between one or more bots and either a _direct_ conversation with a specific user or a _group_ conversation with multiple users.

The Bot Connector Service defines what constitutes a conversation and how a conversation begins and ends.
When the bot generates the context object for an incoming activity, it includes the conversation information.

To start or resume a conversation, use the adapter's _create conversation_ or _continue conversation_ methods, respectively. To create a response activity, use the context's _reply_ method.

<!--TODO: Add information about authentication.
## Security
See [authentication](#) and [user identification](#).
-->

## State
State represents information that your bot saves in order to respond appropriately to incoming messages.
 
Use state to store information about the progress of a multi-step interaction, or a user's preferences, and so on. You can also use state to cache other information, such as status updates, weather forecasts, and so on.

Bot adapters are designed to be stateless so that they can easily be scaled to run across multiple compute nodes.
The SDK provides bot state manager middleware to persist data, and includes Azure Table Storage, file storage, and memory storage that you can use for data storage.
You can also create your own storage components for your bot.

> ![NOTE] File and memory storage don't scale across nodes, and
you should generally avoid saving state using a global variable or function closures.
Doing so will create issues when you want to scale out your bot.
Add stage manager middleware to your adapter, and use the state property of the context object to save data relative to a user or conversation.

## Next steps

